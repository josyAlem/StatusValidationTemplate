namespace StatusValidationEngine
{
    public class Approved : StatusBase
    {
        public Approved()
        {
            ValidTransitions = new[]
            {
                OfferStatus.Active
            };
        }

        protected override OfferStatus CurrentStatus => OfferStatus.Approved;

        protected override OfferStatus[] ValidTransitions { get; }

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
