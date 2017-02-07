using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace csharp_tips
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class FeatureAttribute : Attribute
    {
        public readonly string FeatureName;
        public FeatureAttribute(string featureName)
        {
            FeatureName = featureName;
        }
    }

    public interface ICustomForm
    {
        
    }

    [Feature("X")]
    [Feature("Y")]
    public class CustomFormA : ICustomForm
    {
        
    }

    [Feature("X")]
    public class CustomFormB : ICustomForm
    {
        
    }


    [TestFixture]
    public class AttributeSamples
    {
        [Test]
        public void Test()
        {
            ICustomForm customFormA = new CustomFormA();
            Assert.That(IsFormEnabled(customFormA, disabledFeatures: new List<string> { "X" }), Is.False);
            Assert.That(IsFormEnabled(customFormA, disabledFeatures: new List<string> { "Y" }), Is.False);

            ICustomForm customFormB = new CustomFormB();
            Assert.That(IsFormEnabled(customFormB, disabledFeatures: new List<string> { "X" }), Is.False);
            Assert.That(IsFormEnabled(customFormB, disabledFeatures: new List<string> { "Y" }), Is.True);
        }

        private bool IsFormEnabled(ICustomForm form, List<string> disabledFeatures)
        {
            Attribute[] formAttributes = Attribute.GetCustomAttributes(form.GetType());
            foreach (Attribute formAttribute in formAttributes)
            {
                if (formAttribute is FeatureAttribute)
                {
                    string featureName = (formAttribute as FeatureAttribute).FeatureName;
                    if (disabledFeatures.Contains(featureName))
                        return false;
                }
            }
            return true;
        }
    }
}