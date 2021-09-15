namespace StatusValidationEngine
{
    public class Active : StatusBase
    {
        protected override OfferStatus CurrentStatus => OfferStatus.Active;

        protected override bool AllowExpiredTransition => true;

        protected override WorkflowValidationResult PreValidate(OfferDetailDto offer)
        {
            var baseResponse = base.PreValidate(offer);
            if (!baseResponse.IsValid)
            {
                return baseResponse;
            }
            //TODO: validation code specific to this status
            return baseResponse;
        }

        public override WorkflowValidationResult Validate(OfferStatus newStatus, OfferDetailDto offer)
        {
            var baseValidationResult = base.Validate(newStatus, offer);
            if (!baseValidationResult.IsValid)
            {
                return baseValidationResult;
            }

          //TODO: validation code specific to this status

            return baseValidationResult;
        }
    }
}
