using System;
using NUnit.Framework;

namespace NUnit_v3_samples
{
    public class NUnitIssue360
    {
        [Test]
        [LoginAs]
        public void Test()
        {
            Console.WriteLine("NUnitIssue360:start");
            Assert.Pass();
            Console.WriteLine("NUnitIssue360:finish");
        }
    }

    //[Serializable]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class LoginAsAttribute : PropertyAttribute
    {
        public LoginAsAttribute()
            : base(new Role())
        {
        }
    }

    public class Role
    {
    }

}