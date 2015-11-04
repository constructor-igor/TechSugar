using System;
using NUnit.Framework;

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
            POP3Client.POP3client demoPop3Client = new POP3Client.POP3client();
            Console.WriteLine(demoPop3Client.connect("pop.gmail.com"));
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
            
        }
    }
}