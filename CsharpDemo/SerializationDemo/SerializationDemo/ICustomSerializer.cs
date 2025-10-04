namespace SerializationDemo
{
    public interface ICustomSerializer
    {
        string Serialize<T>(T data);
        T Deserialize<T>(string jsonContent);
    }
}