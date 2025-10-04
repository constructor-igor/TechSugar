namespace SerializationDemo
{
    public interface ICustomSerializer
    {
        void AddConverter<T>();
        string Serialize<T>(T data);
        T Deserialize<T>(string jsonContent);
    }
}