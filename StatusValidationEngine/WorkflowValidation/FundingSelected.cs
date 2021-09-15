namespace StatusValidationEngine
{
    public class FundingSelected : StatusBase
    {
        public FundingSelected()
        {
            ValidTransitions = new[]
            {
                OfferStatus.FundingSelected,
                OfferStatus.Signed,
                OfferStatus.Expired,
                OfferStatus.Locked
            };
        }

        protected override OfferStatus CurrentStatus => OfferStatus.FundingSelected;

        protected override OfferStatus[] ValidTransitions { get; }
    }
}
