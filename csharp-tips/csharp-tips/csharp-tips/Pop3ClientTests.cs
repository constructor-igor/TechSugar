using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenPop.Mime;
using OpenPop.Pop3;

namespace csharp_tips
{
    //
    //  References:
    //  http://www.codeproject.com/Articles/1895/POP-Client-as-a-C-Class
    //  http://www.yetesoft.com/free-email-marketing-resources/pop-smtp-server-settings/
    //

    [TestFixture]
    public class Pop3ClientTests
    {
        [Test]
        public void Test()
        {
            POP3Client.POP3client demoPop3Client = new POP3Client.POP3client("pop.gmail.com", 995, "USER", "PASSWORD");
            string status = demoPop3Client.connect();
            Console.WriteLine("status: {0}", status);

            Console.WriteLine("****Issuing STAT");
            Console.WriteLine(demoPop3Client.STAT());
            Console.WriteLine("****Issuing LIST");
            Console.WriteLine(demoPop3Client.LIST());
            Console.WriteLine("****Issuing RETR 700...this" +
                              " will cause the POP3 server to gack a "
                              + "hairball since there is no message 700");
            Console.WriteLine(demoPop3Client.RETR(700));
            // this will cause the pop3 server to throw 
            // an error since there is no message 700

            Console.WriteLine("****Issuing RETR 7");
            Console.WriteLine(demoPop3Client.RETR(7));
            Console.WriteLine("****Issuing QUIT");
            Console.WriteLine(demoPop3Client.QUIT());
        }
    }

    //
    //  References:
    //  http://hpop.sourceforge.net/
    //  Install-Package OpenPop.NET

    [TestFixture]
    public class OpenPopNetClientTests
    {
        [Test]
        public void Test()
        {
            const string hostName = "pop.gmail.com";
            int port = 995;
            bool useSsl = true;
            string userName = "USER";
            string password = "PASSWORD";

            using(Pop3Client client = new Pop3Client())
            {
                // Connect to the server
                client.Connect(hostName, port, useSsl);

                // Authenticate ourselves towards the server
                client.Authenticate(userName, password);

                // Get the number of messages in the inbox
                int messageCount = client.GetMessageCount();

                // We want to download all messages
                List<Message> allMessages = new List<Message>(messageCount);

                // Messages are numbered in the interval: [1, messageCount]
                // Ergo: message numbers are 1-based.
                // Most servers give the latest message the highest number
                for (int i = messageCount; i > 0; i--)
                {
                    allMessages.Add(client.GetMessage(i));
                }

                allMessages.ForEach(m=>Console.WriteLine("Display name: {0}", m.Headers.From.DisplayName));
            }
        }
    }
}