namespace StatusValidationEngine
{
    public class Funded : StatusBase
    {
        public Funded()
        {
            ValidTransitions = new[]
            {
                OfferStatus.Approved,
                OfferStatus.Active
            };
        }

        protected override WorkflowValidationResult PreValidate(OfferDto offer)
        {
            var baseValidationResult = base.PreValidate(offer);
            if (!baseValidationResult.IsValid)
            {
                return baseValidationResult;
            }

            //TODO: validation code specific to this status

            return baseValidationResult;
        }

        public override WorkflowValidationResult Validate(OfferStatus newStatus, OfferDto offer)
        {
            var baseValidationResult = base.Validate(newStatus, offer);
            if (!baseValidationResult.IsValid)
            {
                return baseValidationResult;
            }
            //TODO: validation code specific to this status

            return baseValidationResult;
        }

        protected override OfferStatus CurrentStatus => OfferStatus.Funded;

        protected override OfferStatus[] ValidTransitions { get; }
    }
}
