using System;

namespace StatusValidationEngine
{
    public static class StatusBaseFactory
    {
        public static StatusBase CreateStatus(OfferStatus status)
        {
            return status switch
            {
                OfferStatus.Pending => new Pending(),
                OfferStatus.Sent => new Sent(),
                OfferStatus.Viewed => new Viewed(),
                OfferStatus.FundingSelected => new FundingSelected(),
                OfferStatus.Signed => new Signed(),
                OfferStatus.Funded => new Funded(),
                OfferStatus.Approved => new Approved(),
                OfferStatus.Active => new Active(),
                OfferStatus.Expired => new Expired(),
                OfferStatus.DeliveryError => new DeliveryError(),
                OfferStatus.Locked => new Locked(),
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Unexpected error encountered creating State Transition object.")
            };
        }
    }
}
