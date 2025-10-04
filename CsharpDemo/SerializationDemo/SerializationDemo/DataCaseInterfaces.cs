namespace SerializationDemo
{
    public interface IDataCaseFactory<T>
    {
        T CreateCustomData(bool circle);
        void Compare(T expected, T actual);
    }
}
