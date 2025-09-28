namespace SerializationDemo
{
    public interface ICustomSerializer
    {
        string Serialize(CustomData data);
        CustomData Deserialize(string jsonContent);
    }
}