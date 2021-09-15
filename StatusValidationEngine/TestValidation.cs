using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace StatusValidationEngine
{
    public static class TestValidation
    {
        public static void RunValidation() {

            Validate("ValidWorkflow");
            Validate("InvalidWorkflow");
        }
        private static void Validate(string workFlowType) {

            List<object[]> testDataDtos= GetData(workFlowType).ToList();
          
            WorkflowValidation svc = new WorkflowValidation();
            
            Console.WriteLine(workFlowType);

            foreach (var dto in testDataDtos)
            {
                string oldStatusString = dto[0].ToString();
                string newStatusString = dto[1].ToString();
                OfferStatus oldStatus = (OfferStatus)Enum.Parse(typeof(OfferStatus), oldStatusString);
                OfferStatus newStatus = (OfferStatus)Enum.Parse(typeof(OfferStatus), newStatusString);
                OfferDto offerDto = new OfferDto
                {
                    IsExpired = false,
                    OfferStatusId = oldStatus
                };
                var result=svc.ValidateOfferStatusChange(newStatus, offerDto);

                if (result.IsValid == (bool)dto[2])
                {
                    string msg = String.Format("From status [{0}] to status [{1}] =>IsValid [{2}] ", oldStatusString, newStatusString, result.IsValid);
                    Console.WriteLine(msg);
                }
              else
                {
                    string msg = String.Format("Failed validation From status [{0}] to status [{1}] =>IsValid [{2}] ", oldStatusString, newStatusString, result.IsValid);
                    Console.WriteLine(msg);
                }
            }
            Console.WriteLine("*******************************");


        }

        private static IEnumerable<object[]> GetData(string workFlowType)
        {
            // Get the absolute path to the JSON file 
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"TestData.json"); if (File.Exists(filePath) == false)
            {
                throw new FileNotFoundException(filePath + " File not found");
            }

            // Load the file 

            var fileData = File.ReadAllText(filePath);

            if (string.IsNullOrEmpty(workFlowType))

            {
                //whole file is the data 
                return JsonConvert.DeserializeObject<List<object[]>>(fileData);
            }

            // Only use the specified property as the data 

            var allData = JObject.Parse(fileData);

            var data = allData[workFlowType];

            return data.ToObject<List<object[]>>();

        }
    }
}
