using System.Text.Json;
using System.Text.Json.Serialization;

namespace SerializationDemo
{
    public class SystemSerializer: ICustomSerializer
    {
        public string Serialize(CustomData data)
        {
            string jsonContent = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true, // for pretty printing
                ReferenceHandler = ReferenceHandler.Preserve,
                IncludeFields = true
            });
            return jsonContent;
        }
        public CustomData Deserialize(string jsonContent)
        {
            CustomData deserialized = JsonSerializer.Deserialize<CustomData>(jsonContent, new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                IncludeFields = true
            });
            return deserialized;
        }
    }
}