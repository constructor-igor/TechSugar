using System.Threading.Tasks;
using NUnit.Framework;

namespace NUnit_v3_samples
{
    [TestFixture]
    public class AsyncSamplesTests
    {
        [Test]
        public void OneSimpleSyncTest()
        {
            var eightBall = new EightBall();
            var answer = eightBall.ShouldIChangeJobSync();

            Assert.That(answer, Is.True );
        }

        [TestCase(ExpectedResult=true)]
        public async Task<bool> OneSimpleASyncTest()
        {
            var eightBall = new EightBall();
            return await eightBall.ShouldIChangeJobASync();
            //return answer;
            //answer.Wait();

            //Assert.That(answer, Is.True);

            // why am I still here?
        }

        [Test]
        public async Task EqualToTest()
        {
            Assert.That(5, Is.EqualTo(6));
        }
    }

    public class EightBall
    {
        public bool ShouldIChangeJobSync()
        {
            return true;
        }
        public Task<bool> ShouldIChangeJobASync()
        {
            Task<bool> workingTask = new Task<bool>(() => true);
            workingTask.Start();
            return workingTask;            
        }
    }
}