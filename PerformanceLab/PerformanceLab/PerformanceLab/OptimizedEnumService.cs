using System;

namespace PerformanceLab
{
    public class OptimizedEnumService : IEnumService
    {
        #region Implementation of IEnumService
        public bool IntValue2EnumValue<TEnumInt32>(int intValue, out TEnumInt32 enValue) where TEnumInt32 : new()
        {
            Type enumType = typeof(TEnumInt32);
            bool canBeConverted = Enum.IsDefined(enumType, intValue) || enumType.IsDefined(typeof(FlagsAttribute), false);
            enValue = canBeConverted ? (TEnumInt32)Enum.ToObject(enumType, intValue) : new TEnumInt32();
            return canBeConverted;
        }
        #endregion
    }
}