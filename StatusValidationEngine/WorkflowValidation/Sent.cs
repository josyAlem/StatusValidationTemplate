namespace StatusValidationEngine
{
    public class Sent : StatusBase
    {
        public Sent()
        {
            ValidTransitions = new[]
            {
                OfferStatus.Viewed,
                OfferStatus.Expired,
                OfferStatus.Locked
            };
        }

        protected override OfferStatus CurrentStatus => OfferStatus.Sent;

        protected override OfferStatus[] ValidTransitions { get; }
    }
}
