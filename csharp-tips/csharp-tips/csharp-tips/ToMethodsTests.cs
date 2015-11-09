using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Net.Mail;
using System.Threading;
using System.Windows.Forms;
using csharp_tips.SendToProviders;
using NUnit.Framework;

/*
 * 
 * http://stackoverflow.com/questions/16705124/clipboard-task-error
 * 
 * */

namespace csharp_tips
{
    [TestFixture]
    public class ToMethodsTests
    {
        [Test]
        public void FileToClipboard()
        {
            string testDataFilePath = @"..\..\Data\test.txt";
            StringCollection paths = new StringCollection { testDataFilePath };
            Assert.That(File.Exists(testDataFilePath));
            Assert.That(() => Clipboard.SetFileDropList(paths), Throws.TypeOf<ThreadStateException>());
        }

        ///
        /// http://stackoverflow.com/questions/20328598/open-default-mail-client-along-with-a-attachment
        /// http://stackoverflow.com/questions/7357123/get-current-users-email-address-in-net
        /// https://msdn.microsoft.com/en-us/library/system.directoryservices.accountmanagement.userprincipal(v=vs.110).aspx
        ///
        [Test]
        public void FileToEmailClientViaMailMessage()
        {
            string testDataFilePath = @"..\..\Data\test.txt";

            //string mailAddress = WindowsIdentity.GetCurrent().Name;
            string mailAddress = UserPrincipal.Current.EmailAddress;
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(mailAddress);
            mailMessage.Subject = String.Format("File {0} attached", Path.GetFileName(testDataFilePath));
            mailMessage.IsBodyHtml = false;
            //mailMessage.Body = "<span style='font-size: 12pt; color: red;'>My HTML formatted body</span>";
            mailMessage.Attachments.Add(new Attachment(testDataFilePath));

            var filename = Path.Combine(Path.GetTempPath(), "mymessage.eml");
            mailMessage.Save(filename);
            Process.Start(filename);
        }

        [Test]
        public void FileToEmailOutlook()
        {
            string testDataFilePath = @"..\..\Data\test.txt";
            string fullPathToDataFile = Path.GetFullPath(testDataFilePath);

            Assert.That(File.Exists(testDataFilePath), Is.True);
            Assert.That(File.Exists(fullPathToDataFile), Is.True);

            var outlookPath = @"OUTLOOK.EXE";
            var command = String.Format(@"/a ""{0}"" /m ""to@me.com&cc=cc@.com&subject=subject&body=body""", fullPathToDataFile);
            Process.Start(outlookPath, command);
        }

        //
        // format: mailto:some.guy@someplace.com?subject=an email&body=see attachment&attachment="/files/audio/attachment.mp3"
        //
        [Test]
        public void FileToEmailClient()
        {
            string testDataFilePath = @"..\..\Data\test.txt";
            Assert.That(File.Exists(testDataFilePath), Is.True);
            string subject = String.Format("File {0} attached", Path.GetFileName(testDataFilePath));
            string body = "body";
            string command = String.Format("mailto:?subject={0}&body={1}&attachment=\"{2}\"", subject, body, testDataFilePath);
            Process.Start(command);
        }

        //
        // from comments http://www.codeproject.com/Articles/3839/SendTo-mail-recipient
        //
        [Test]
        public void TestEmailController()
        {
            const string TEST_DATA_FILE_PATH = @"..\..\Data\test.txt";
            int result = EmailController.SendMail(Path.GetFullPath(TEST_DATA_FILE_PATH), "send file test", "EMAIL@DOMAIL");
            Console.WriteLine("result: {0}", result);
        }

        [Test]
        public void TestMAPI()
        {
            const string TEST_DATA_FILE_PATH = @"..\..\Data\test.txt";

            string body = "Body Content here";
            var file = TEST_DATA_FILE_PATH;
            string[]  attachments = { file };
            string[] recipients = { "EMAIL@DOMAIL" };
            string subject = "Subject here";
            MAPI mapi = new MAPI();
            int result = mapi.ComposeMail(recipients, subject, body, attachments);
            Console.WriteLine("result: code={0}, status={1}", result, mapi.GetLastError());
        }
    }
}