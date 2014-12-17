// <copyright file="SmartUniteTestSampleTest.cs">Copyright ©  2014</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VS2015News;

namespace VS2015News
{
    [TestClass]
    [PexClass(typeof(SmartUniteTestSample))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class SmartUniteTestSampleTest
    {
        [PexMethod]
        [PexAllowedException(typeof(NotImplementedException))]
        public int ReturnMidItem([PexAssumeUnderTest]SmartUniteTestSample target, int[] data)
        {
            int result = target.ReturnMidItem(data);
            return result;
            // TODO: add assertions to method SmartUniteTestSampleTest.ReturnMidItem(SmartUniteTestSample, Int32[])
        }
        [PexMethod]
        public int Calc(
            [PexAssumeUnderTest]SmartUniteTestSample target,
            int x,
            int y
        )
        {
            int result = target.Calc(x, y);
            return result;
            // TODO: add assertions to method SmartUniteTestSampleTest.Calc(SmartUniteTestSample, Int32, Int32)
        }
        [PexMethod]
        public int ReturnSizeOfArray([PexAssumeUnderTest]SmartUniteTestSample target, int[] data)
        {
            int result = target.ReturnSizeOfArray(data);
            return result;
            // TODO: add assertions to method SmartUniteTestSampleTest.ReturnSizeOfArray(SmartUniteTestSample, Int32[])
        }
    }
}
