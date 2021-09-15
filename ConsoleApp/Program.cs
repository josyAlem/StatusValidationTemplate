using StatusValidationEngine;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            WorkflowValidationService svc = new WorkflowValidationService();
            OfferDetailDto offer = new OfferDetailDto { 
            IsExpired=false,
             OfferStatusId=OfferStatus.Active
            };
            OfferStatus newStatus = OfferStatus.Approved;
            var validationResponse =  svc.ValidateOfferStatusChange(newStatus, offer);
            Console.WriteLine("Validation Result=" + validationResponse.IsValid);
            Console.WriteLine("Validation message=" + validationResponse.ValidationMessage);
           

        }


    }
}
