using NUnit.Framework;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace csharp_tips
{
    [TestFixture]
    public class OutlookSamples
    {
        [Test, Explicit]
        public void SendEmail()
        {
            Outlook.Application application = new Outlook.ApplicationClass();
            Outlook.MailItem mail = application.CreateItem(Outlook.OlItemType.olMailItem) as Outlook.MailItem;
            mail.Subject = "subject";
            Outlook.AddressEntry currentUser = application.Session.CurrentUser.AddressEntry;
            mail.Recipients.Add(currentUser.Name);
            mail.Send();
        }
    }
}