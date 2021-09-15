namespace StatusValidationEngine
{
    public class Expired : StatusBase
    {
        protected override OfferStatus CurrentStatus => OfferStatus.Expired;

        protected override bool AllowExpiredTransition => true;
    }
}
