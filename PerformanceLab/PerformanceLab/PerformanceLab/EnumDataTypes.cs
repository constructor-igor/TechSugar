using System;

namespace PerformanceLab
{
    public class EnumDataTypes
    {
        public enum Enum1 { Enum10, Enum11 };

        [Flags]
        public enum EnumFlag
        {
            EnumFlagNone = 0,
            EnumFlag1 = 1,
            EnumFlag2 = 2,
            EnumFlag3 = EnumFlag1 + EnumFlag2,
        }

        [Serializable]
        public enum EnumAttributes
        {
            EnumAttributes0,
            EnumAttributes1
        }

    }
}