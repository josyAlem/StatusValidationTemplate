using System;
using System.Threading.Tasks;


namespace StatusValidationEngine
{
    public class WorkflowValidationService 
    {

        public WorkflowValidationService()
        {
        }

        /// <summary>
        /// Validates a Status transition for a given OfferId/Code and new Status
        /// </summary>
        /// <param name="newOfferStatus"></param>
        /// <param name="offerId"></param>
        /// <param name="offerCode"></param>
        /// <returns>True if valid transition, False if invalid transition, NULL if Offer not found</returns>
        public  WorkflowValidationResult ValidateOfferStatusChange(OfferStatus newOfferStatus,
            OfferDetailDto offer)
        {


            StatusBase offerStatus;
            try
            {
                offerStatus = StatusBaseFactory.CreateStatus(offer.OfferStatusId);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                const string msg = "Unexpected error encountered creating Validation State Transition object.";
                throw new Exception(msg, ex);
            }

            var result = offerStatus.Validate(newOfferStatus, offer);
            return result;
        }
    }
}
