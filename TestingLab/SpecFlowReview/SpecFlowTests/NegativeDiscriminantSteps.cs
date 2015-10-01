using System;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SpecFlowTests
{
    [Binding]
    public class NegativeDiscriminantSteps
    {
        private double a;
        private double b;
        private double c;
        private string result;


        [Given(@"I have entered (.*) into the a")]
        public void GivenIHaveEnteredIntoTheA(int a)
        {
            this.a = a;
        }

        [Given(@"I have entered (.*) into b")]
        public void GivenIHaveEnteredIntoB(int b)
        {
            this.b = b;
        }

        [Given(@"I have entered (.*) into c")]
        public void GivenIHaveEnteredIntoC(int c)
        {
            this.c = c;
        }

        [When(@"I press solve")]
        public void WhenIPressSolve()
        {
            double d = b * b - 4 * a * c;
            if (d < 0)
            {
                result = "Negative Discriminant";
            }
            else if (d==0)
            {
                result = String.Format("x={0}", -b/2*a);
            }
        }

        [Then(@"the result should be error ""(.*)""")]
        public void ThenTheResultShouldBeError(string expectedResult)
        {
            Assert.AreEqual(expectedResult, result);
        }
    }
}
