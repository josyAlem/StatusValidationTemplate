using System.Collections.Generic;

namespace StatusValidationEngine
{
    public enum OfferStatus
    {
   
        Pending = 1,       
        Sent = 2,      
        Viewed = 3,       
        FundingSelected = 4,
        Signed = 5,     
        Funded = 6,      
        Approved = 7,      
        Active = 8,      
        Expired = 9,      
        DeliveryError = 100,
        Locked = 101
    }

    public static class OfferStatusExtensions
    {
        private static List<OfferStatus> _canExpireStatusList;

        
        public static bool IsEligibleForReminder(this OfferStatus status)
        {
            return status == OfferStatus.Sent || status == OfferStatus.Viewed || status == OfferStatus.FundingSelected;
        }

        public static bool CanExpire(this OfferStatus status)
        {
            var expirationList = GetExpirationEligibleStatuses();
            return expirationList.Contains(status);
        }

      
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
