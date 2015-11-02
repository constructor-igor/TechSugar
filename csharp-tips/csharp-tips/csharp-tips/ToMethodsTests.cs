using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Net.Mail;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
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
        public void FileToEmailClient()
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
    }

    public static class MailUtility
    {
        //Extension method for MailMessage to save to a file on disk
        public static void Save(this MailMessage message, string filename, bool addUnsentHeader = true)
        {
            using (var filestream = File.Open(filename, FileMode.Create))
            {
                if (addUnsentHeader)
                {
                    var binaryWriter = new BinaryWriter(filestream);
                    //Write the Unsent header to the file so the mail client knows this mail must be presented in "New message" mode
                    binaryWriter.Write(System.Text.Encoding.UTF8.GetBytes("X-Unsent: 1" + Environment.NewLine));
                }

                var assembly = typeof(SmtpClient).Assembly;
                var mailWriterType = assembly.GetType("System.Net.Mail.MailWriter");

                // Get reflection info for MailWriter constructor
                var mailWriterConstructor = mailWriterType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new[] { typeof(Stream) }, null);

                // Construct MailWriter object with our FileStream
                var mailWriter = mailWriterConstructor.Invoke(new object[] { filestream });

                // Get reflection info for Send() method on MailMessage
                var sendMethod = typeof(MailMessage).GetMethod("Send", BindingFlags.Instance | BindingFlags.NonPublic);

                sendMethod.Invoke(message, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { mailWriter, true, true }, null);

                // Finally get reflection info for Close() method on our MailWriter
                var closeMethod = mailWriter.GetType().GetMethod("Close", BindingFlags.Instance | BindingFlags.NonPublic);

                // Call close method
                closeMethod.Invoke(mailWriter, BindingFlags.Instance | BindingFlags.NonPublic, null, new object[] { }, null);
            }
        }
    }
}