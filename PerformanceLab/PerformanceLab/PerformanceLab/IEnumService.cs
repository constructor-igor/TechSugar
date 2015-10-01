namespace PerformanceLab
{
    public interface IEnumService
    {
        bool IntValue2EnumValue<TEnumInt32>(int intValue, out TEnumInt32 enValue) where TEnumInt32 : new();
    }
}