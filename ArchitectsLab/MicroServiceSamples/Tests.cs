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
        }

        [Test]
        public void StandardApproach()
        {
            string modelId = "1";
            StandardApproachService service = new StandardApproachService();
            string result = service.Execute(modelId);
            Console.WriteLine("result: {0}", result);
        }

        [Test]
        public void MicroServicesApproach()
        {
            string modelId = "1";
            MicroServicesApproachService service = new MicroServicesApproachService();
            string result = service.Execute(modelId);
            Console.WriteLine("result: {0}", result);
        }
    }
}
