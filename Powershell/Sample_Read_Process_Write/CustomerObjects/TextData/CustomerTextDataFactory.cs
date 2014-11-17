using System.IO;
using Customer.Interfaces;

namespace Customer.TextData
{
    public class CustomerTextDataFactory : ICustomerTextDataFactory<CustomerTextData>
    {
        public CustomerTextData LoadFromFile(string filePath)
        {
            var fileContent = File.ReadAllText(filePath);
            return new CustomerTextData(fileContent);
        }
    }
}