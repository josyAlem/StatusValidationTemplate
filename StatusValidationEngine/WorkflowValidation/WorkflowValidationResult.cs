namespace StatusValidationEngine
{
    public class WorkflowValidationResult
    {
        public bool IsValid { get; set; }

        public OfferDto Offer { get; set; }

        public string ValidationMessage { get; set; }
    }
}
