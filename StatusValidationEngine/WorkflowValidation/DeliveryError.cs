namespace StatusValidationEngine
{
    public class DeliveryError : StatusBase
    {
        public DeliveryError()
        {
            ValidTransitions = new[]
            {
                OfferStatus.Viewed,
                OfferStatus.Locked
            };
        }

        protected override OfferStatus CurrentStatus => OfferStatus.DeliveryError;

        protected override OfferStatus[] ValidTransitions { get; }
    }

}
