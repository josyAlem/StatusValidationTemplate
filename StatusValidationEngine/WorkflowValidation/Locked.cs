namespace StatusValidationEngine
{
    public class Locked : StatusBase
    {
        public Locked()
        {
            ValidTransitions = new[]
            {
                OfferStatus.Pending,
                OfferStatus.Sent,
                OfferStatus.Viewed,
                OfferStatus.Expired
            };
        }

        protected override OfferStatus CurrentStatus => OfferStatus.Locked;

        protected override OfferStatus[] ValidTransitions { get; }
    }
}
