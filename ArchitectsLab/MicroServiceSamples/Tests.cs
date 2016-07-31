using System;
using NUnit.Framework;

namespace MicroServiceSamples
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void StraightforwardApproach()
        {
            string modelId = "model1";
            StraightforwardApproachService service = new StraightforwardApproachService();
            string result = service.Execute(modelId);
            Console.WriteLine("result: {0}", result);
            Assert.That(result, Is.EqualTo("main(S1(parameters1), S2(model1, S2A(model1:parameters1), S2B(parameters1)))"));
        }

        [Test]
        public void StandardApproach()
        {
            string modelId = "model1";
            StandardApproachService service = new StandardApproachService();
            string result = service.Execute(modelId);
            Console.WriteLine("result: {0}", result);
            Assert.That(result, Is.EqualTo("main(S1(parameters1), S2(model1, S2A(model1:parameters1), S2B(parameters1)))"));
        }

        [Test]
        public void MicroServicesApproach()
        {
            string modelId = "model1";
            MicroServicesApproachService service = new MicroServicesApproachService();
            string result = service.Execute(modelId);
            Console.WriteLine("result: {0}", result);
            Assert.That(result, Is.EqualTo("main(S1(parameters1), S2(model1, S2A(model1:parameters1), S2B(parameters1)))"));
        }
    }
}
