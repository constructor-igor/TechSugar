using Newtonsoft.Json;

namespace SerializationDemo
{
    public class NewtonSerializer : ICustomSerializer
    {
        private readonly JsonSerializerSettings m_settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects
        };

        public string Serialize(CustomData data)
        {
            string json = JsonConvert.SerializeObject(data, m_settings);
            return json;
        }

        public CustomData Deserialize(string jsonContent)
        {
            CustomData deserialized = JsonConvert.DeserializeObject<CustomData>(jsonContent, m_settings);
            return deserialized;
        }
    }
}
