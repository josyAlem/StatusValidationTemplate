namespace StatusValidationEngine
{
    public class Signed : StatusBase
    {
        public Signed()
        {
            ValidTransitions = new[]
            {
                OfferStatus.Funded
            };
        }

        protected override OfferStatus CurrentStatus => OfferStatus.Signed;

        protected override OfferStatus[] ValidTransitions { get; }
    }
}
