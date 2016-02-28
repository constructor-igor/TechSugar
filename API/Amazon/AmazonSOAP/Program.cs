using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amazon.PAAPI
{
    /*
     * downloaded from
     * */

    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate Amazon ProductAdvertisingAPI client
            AWSECommerceServicePortTypeClient amazonClient = new AWSECommerceServicePortTypeClient();

            // prepare an ItemSearch request
            ItemSearchRequest request = new ItemSearchRequest
            {
                SearchIndex = "Books",
                Title = "WCF",
                ResponseGroup = new[] {"Small"}
            };

            ItemSearch itemSearch = new ItemSearch
            {
                Request = new[] {request},
                AWSAccessKeyId = ConfigurationManager.AppSettings["accessKeyId"],
                AssociateTag = "ReplaceWithYourValue"
            };

            // send the ItemSearch request
            ItemSearchResponse response = amazonClient.ItemSearch(itemSearch);

            // write out the results from the ItemSearch request
            foreach (var item in response.Items[0].Item)
            {
                Console.WriteLine(item.ItemAttributes.Title);
            }
            Console.WriteLine("done...enter any key to continue>");
            Console.ReadLine();
        }
    }
}
