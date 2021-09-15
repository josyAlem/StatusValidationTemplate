using System.Collections.Generic;

namespace StatusValidationEngine
{
    public enum OfferStatus
    {
        /// <summary>
        /// The Offer has only been inserted into the database, and no attempt to send it to the Customer has been made
        /// </summary>
        Pending = 1,

        /// <summary>
        /// The Offer has been successfully sent to Customer by at one Delivery Method (Sms, MobilePush, soon-to-be Email)
        /// Note that this does not observe Opt-Outs by the Customer at the Provider level (i.e. Sms request was sent successfully, but the Customer is opted out--we don't know this)
        /// </summary>
        Sent = 2,

        /// <summary>
        /// The Customer has passed the CWP validation screen and has viewed the loan terms prior to adding a Funding Method
        /// </summary>
        Viewed = 3,

        /// <summary>
        /// The Customer has either added a Debit Card or already has a Security Trust card, has selected one or the other, and clicked the Select Funding Method button.
        /// This is all encapsulated in the HPG.Web code, and they are responsible for calling into this endpoint. This will change once "new" HPG is implemented.
        /// </summary>
        FundingSelected = 4,

        /// <summary>
        /// The Customer has signed their loan documents via Docusign. This status is set by the Docusign Webhook once a given "event" is received. As of 8/3/21, that event is the "Sent" Docusign event.
        /// </summary>
        Signed = 5,

        /// <summary>
        /// Once the Offer has successfully been set to Signed, the Docusign Webhook subsequently calls the Disburse Funds endpoint. eLoan calls into HPG API to disburse funds.
        /// If the Disbursement was approved, the Offer is set to Funded. If there was an error or the Disbursement was declined, the card is invalidated in HPG, the Docusign contract is Voided,
        /// and the Offer is set back to Viewed
        /// </summary>
        Funded = 6,

        /// <summary>
        /// Set by the Branch Nightly Batch process. Once the Offer is Funded, the live_check_tbl.cleared_bank_yn flag is set to 'y'.
        /// This is the cue for the Nightly Batch process to convert the Offer into an actual Branch loan. The "converted" branchId/accountId is also set during this process
        /// </summary>
        Approved = 7,

        /// <summary>
        /// Set by the Branch User. After Nightly Batch has been executed, the Branch User is prompted by some mechanism to "activate" the loan.
        /// This creates a BranchTransactionId which is passed into HPG for auditing purposes.
        /// Note: it is additionally a valid transition to skip Approved and move straight from Funded to Active (in the event that Nightly Batch fails)
        /// </summary>
        Active = 8,

        /// <summary>
        /// Set by the eLoan Console app. The Offer is no longer valid. This could be because it has reached its expiration date, or because an Active Account was detected for the given Ssn/Dob on the Offer
        /// </summary>
        Expired = 9,

        /// <summary>
        /// Set by the Console app. The Offer wasn't able to be successfully delivered by any delivery method.
        /// </summary>
        DeliveryError = 100,

        /// <summary>
        /// Set by CWP. The user failed x number of times to Validate their ssn/dob/offercode on the initial CWP page. x is a configurable value in the appsettings. The Offer is Locked for 24 hours
        /// </summary>
        Locked = 101
    }

    public static class OfferStatusExtensions
    {
        private static List<OfferStatus> _canExpireStatusList;

        /// <summary>
        /// Get Offer Statuses that are valid for Campaign Reminders sent by the Console app
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool IsEligibleForReminder(this OfferStatus status)
        {
            return status == OfferStatus.Sent || status == OfferStatus.Viewed || status == OfferStatus.FundingSelected;
        }

        public static bool CanExpire(this OfferStatus status)
        {
            var expirationList = GetExpirationEligibleStatuses();
            return expirationList.Contains(status);
        }

        /// <summary>
        /// OfferStatus that are eligible to be transitioned to Expired
        /// </summary>
        /// <returns></returns>
        public static List<OfferStatus> GetExpirationEligibleStatuses()
        {
            return _canExpireStatusList ??= new List<OfferStatus>
            {
                OfferStatus.Pending,
                OfferStatus.Sent,
                OfferStatus.Viewed,
                OfferStatus.FundingSelected,
                OfferStatus.DeliveryError,
                OfferStatus.Locked
            };
        }
    }

    #region Valid Status Transitions
    /*
   * Pending:
   *    Sent
   *    Viewed
   *    Expired
   *    DeliveryError
   *    Locked
   * Sent:
     *   Viewed
     *   Expired
     *   Locked
   * Viewed:
     *   Viewed
     *   FundingSelected
     *   Expired
     *   Locked
   * Funding Selected
     *   Funding Selected
     *   Signed
     *   Expired
     *   Locked
   * Signed
     *   Funded
   * Funded
     *    Approved
     *    Active
   * Approved
     *    Active
   * Active
     *    ------
     *
   * Expired
     *    ------
   * DeliveryError
     *   Viewed
     *   Locked
   * Locked
     *    Pending
     *    Sent
     *    Viewed
     *    Expired
   */
    #endregion
}
