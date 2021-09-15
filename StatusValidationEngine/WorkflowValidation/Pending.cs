namespace StatusValidationEngine
{
    public class Pending : StatusBase
    {
        public Pending()
        {
            ValidTransitions = new[]
            {
                OfferStatus.Sent,
                OfferStatus.Viewed,
                OfferStatus.Expired,
                OfferStatus.DeliveryError,
                OfferStatus.Locked
            };
        }

        protected override OfferStatus CurrentStatus => OfferStatus.Pending;

        protected override OfferStatus[] ValidTransitions { get; }
    }

}
