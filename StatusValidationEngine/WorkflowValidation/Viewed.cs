namespace StatusValidationEngine
{
    public class Viewed : StatusBase
    {
        public Viewed()
        {
            ValidTransitions = new[]
            {
                OfferStatus.Viewed,
                OfferStatus.FundingSelected,
                OfferStatus.Expired,
                OfferStatus.Locked
            };
        }

        protected override OfferStatus CurrentStatus => OfferStatus.Viewed;

        protected override OfferStatus[] ValidTransitions { get; }
    }

}
