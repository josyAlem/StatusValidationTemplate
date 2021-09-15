namespace StatusValidationEngine
{
    public class WorkflowValidationResult
    {
        public bool IsValid { get; set; }

        public OfferDetailDto Offer { get; set; }

        public string ValidationMessage { get; set; }
    }
}
