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
            service.Execute(modelId);
        }

        [Test]
        public void StandardApproach()
        {
            string modelId = "1";
            StandardApproachService standardApproachService = new StandardApproachService();
            standardApproachService.Execute(modelId);
        }
    }
}
