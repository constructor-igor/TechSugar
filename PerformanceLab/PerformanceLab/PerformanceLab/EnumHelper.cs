using System;

namespace PerformanceLab
{
    public class EnumHelper
    {
        public static TEnumInt32 IntValue2EnumValue<TEnumInt32>(int intValue)
        {
            TEnumInt32 res = (TEnumInt32)Enum.ToObject(typeof(TEnumInt32), intValue);
            if (!Enum.IsDefined(typeof(TEnumInt32), intValue))
            {
                Type enType = typeof(TEnumInt32);
                object[] attibutes = enType.GetCustomAttributes(true);
                bool bFlags = false;
                foreach (object obj in attibutes)
                {
                    bFlags = (obj is FlagsAttribute);
                    if (bFlags)
                        break;
                }
                if (!bFlags)
                    throw new ArgumentException("The argument is not contained in Enum", "intValue");
            }
            return res;
        }
        public static bool IntValue2EnumValue<TEnumInt32>(int intValue, out TEnumInt32 enValue)
            where TEnumInt32 : new()
        {
            bool bRes = false;
            enValue = new TEnumInt32();

            TEnumInt32 res = (TEnumInt32)Enum.ToObject(typeof(TEnumInt32), intValue);
            if (!Enum.IsDefined(typeof(TEnumInt32), intValue))
            {
                Type enType = typeof(TEnumInt32);
                object[] attibutes = enType.GetCustomAttributes(true);
                //enType.IsDefined(typeof (FlagsAttribute), false);
                bool bFlags = false;
                foreach (object obj in attibutes)
                {
                    bFlags = (obj is FlagsAttribute);
                    if (bFlags)
                        break;
                }
                //if (!bFlags)
                //throw new ArgumentException("The argument is not contained in Enum", "intValue");
                bRes = false;
                if (bFlags)
                {
                    enValue = res;
                    bRes = true;
                }
            }
            else
            {
                enValue = res;
                bRes = true;
            }
            return bRes;
        }
    }

    public class LegacyEnumService : IEnumService
    {
        #region Implementation of IEnumService
        public bool IntValue2EnumValue<TEnumInt32>(int intValue, out TEnumInt32 enValue) where TEnumInt32 : new()
        {
            return EnumHelper.IntValue2EnumValue(intValue, out enValue);
        }
        #endregion
    }
}