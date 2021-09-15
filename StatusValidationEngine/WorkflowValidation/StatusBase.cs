using System;
using System.Linq;

namespace StatusValidationEngine
{
    /// <summary>
    /// This class provides the mechanism to validate eLoan OfferStatus Workflow transitions. There are two methods: PreValidate() and Validate()
    /// PreValidate() is called to ensure the we can transition an Offer from any Status in THIS StatusBase object
    /// Validate() is called to ensure that we can transition an Offer from THIS StatusBase object into a requested new status
    /// </summary>
    public abstract class StatusBase
    {
        protected StatusBase()
        {
            ValidTransitions = new OfferStatus[] { }; // No Valid Transitions by default
            AllowExpiredTransition = false;
        }

        protected abstract OfferStatus CurrentStatus { get; }

        /// <summary>
        /// Override this property to create valid State Transitions for inherited classes
        /// </summary>
        protected virtual OfferStatus[] ValidTransitions { get; }

        /// <summary>
        /// Override to allow a status transition into this Status if the Offer is Expired
        /// </summary>
        protected virtual bool AllowExpiredTransition { get; }

        /// <summary>
        /// Validates that we can transition an eLoan INTO this status.
        /// </summary>
        /// <returns></returns>
        protected virtual WorkflowValidationResult PreValidate(OfferDto offer)
        {
            // Don't allow a transition into this Status if the Offer is expired (unless explicitly allowed)
            if (offer.IsExpired && !AllowExpiredTransition)
            {
                return new WorkflowValidationResult
                {
                    IsValid = false,
                    ValidationMessage = $"Cannot transition from Status ({offer.OfferStatusId}) to ({CurrentStatus}). Offer is expired.",
                    Offer = offer
                };
            }

            return new WorkflowValidationResult
            {
                IsValid = true, 
                Offer = offer
            };
        }

        /// <summary>
        /// Base implementation simply validates against ValidTransitions collection. Override to implement custom Status functionality
        /// </summary>
        /// <param name="newStatus"></param>
        /// <param name="offer"></param>
        /// <returns></returns>
        public virtual WorkflowValidationResult Validate(OfferStatus newStatus, OfferDto offer)
        {
            // Validate that we can move into this new status based on the current Offer details
            StatusBase preValidationStatus;
            try
            {
                preValidationStatus = StatusBaseFactory.CreateStatus(newStatus);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                const string msg = "Unexpected error encountered creating PreValidation State Transition object.";
               
                throw new Exception(msg,ex);
            }

            var preValidationResult = preValidationStatus.PreValidate(offer);
            if (!preValidationResult.IsValid)
            {
                return preValidationResult;
            }

            var baseStatusChangeResult = ValidTransitions.Contains(newStatus);
            if (!baseStatusChangeResult)
            {
                return new WorkflowValidationResult
                {
                    IsValid = false,
                    ValidationMessage = $"Cannot transition from Status ({offer.OfferStatusId}) to ({newStatus}). Unexpected workflow.",
                    Offer = offer
                };
            }
            
            return new WorkflowValidationResult()
            {
                Offer = offer,
                IsValid = true
            };
        }
    }
}
