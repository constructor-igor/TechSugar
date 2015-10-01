using System;
using NUnit.Framework;

namespace PerformanceLab
{
    [TestFixture(typeof(LegacyEnumService))]
    [TestFixture(typeof(OptimizedEnumService))]
    public class EnumHelperTests<T> where T:IEnumService
    {
        [Test]
        public void Enum1_1_True()
        {
            int intValue = 1;
            EnumDataTypes.Enum1 value;
            bool enumDefined = EnumHelper.IntValue2EnumValue<EnumDataTypes.Enum1>(intValue, out value);

            Assert.That(enumDefined, Is.True);
            Assert.That(value, Is.EqualTo(EnumDataTypes.Enum1.Enum11));
        }
        [Test]
        public void Enum1_2_False()
        {
            int intValue = 2;
            EnumDataTypes.Enum1 value;
            bool enumDefined = EnumHelper.IntValue2EnumValue<EnumDataTypes.Enum1>(intValue, out value);

            Assert.That(enumDefined, Is.False);
            Assert.That(value, Is.EqualTo(default(EnumDataTypes.Enum1)));
        }
        [Test]
        public void EnumFlag_2_True()
        {
            int intValue = 2;
            EnumDataTypes.EnumFlag value;
            bool enumDefined = EnumHelper.IntValue2EnumValue<EnumDataTypes.EnumFlag>(intValue, out value);

            Assert.That(enumDefined, Is.True);
            Assert.That(value, Is.EqualTo(EnumDataTypes.EnumFlag.EnumFlag2));
        }
        [Test]
        public void EnumFlag_10_True()
        {
            int intValue = 10;
            EnumDataTypes.EnumFlag value;
            bool enumDefined = EnumHelper.IntValue2EnumValue<EnumDataTypes.EnumFlag>(intValue, out value);

            Assert.That(enumDefined, Is.True);
            Assert.That((int)value, Is.EqualTo(intValue));
        }
        [Test]
        public void input_object_ArgumentException()
        {
            int intValue = 10;
            object value;
            Assert.That(() => EnumHelper.IntValue2EnumValue(intValue, out value), Throws.ArgumentException);
        }
        [Test]
        public void EnumAttributes_ArgumentException()
        {
            int intValue = 10;
            EnumDataTypes.EnumAttributes value;
            bool enumDefined = EnumHelper.IntValue2EnumValue(intValue, out value);

            Assert.That(enumDefined, Is.False);
            Assert.That(value, Is.EqualTo(default(EnumDataTypes.EnumAttributes)));
        }
    }

    [TestFixture]
    public class GenericEnumTests
    {
        [Test]
        public void AssertEnumAndIntTrue()
        {
            int intValue = 11;
            AssertEnumSample enumValue = (AssertEnumSample)Enum.ToObject(typeof(AssertEnumSample), intValue);
            //Assert.That(enumValue, Is.EqualTo(intValue));
            Assert.That(intValue, Is.EqualTo(enumValue));
        }

        [Flags]
        private enum AssertEnumSample
        {
            Value2 = 2,
            Value8 = 8,
        }
    }
}