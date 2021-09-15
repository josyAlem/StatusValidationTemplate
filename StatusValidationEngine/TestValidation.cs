using System;
using System.Collections.Generic;
using System.Text;

namespace StatusValidationEngine
{
    public class TestValidation
    {

        public void RunValidation() {

            string newOfferStatus="";
            string oldOfferStatus = "";
            OfferStatus newStatus = (OfferStatus)Enum.Parse(typeof(OfferStatus), newOfferStatus);
            OfferStatus oldStatus = (OfferStatus)Enum.Parse(typeof(OfferStatus), oldOfferStatus);
            Console.WriteLine("ho");
        }
    }
}
