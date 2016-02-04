using NUnit.Extensions.Forms;
using NUnit.Framework;
using TaskInUI;

namespace NunitFormsTests
{
    //
    // https://github.com/constructor-igor/TechSugar/issues/60
    //
    [TestFixture]
    public class MyUITests//: NUnitFormTest
    {
        [Test]
        public void DummyTest()
        {
            Assert.Pass("dummy test");
        }
        [Test]
        public void Test()
        {
            MainForm testedForm = new MainForm();
            testedForm.Show();

            ButtonTester buttonTester = new ButtonTester("button1", "MainForm");
            Assert.That(buttonTester.Text, Is.EqualTo("Start \"Worker\""));
            buttonTester.Click();
        }
    }
}
