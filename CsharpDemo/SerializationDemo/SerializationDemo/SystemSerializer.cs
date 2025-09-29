using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SerializationDemo
{
    public class PositionConverter : JsonConverter<IPosition>
    {
        public override IPosition Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var doc = JsonDocument.ParseValue(ref reader))
            {
                var json = doc.RootElement.GetRawText();
                return JsonSerializer.Deserialize<Position>(json, options); // assumes Position is the only implementation
            }
        }

        public override void Write(Utf8JsonWriter writer, IPosition value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (Position)value, options); // cast to concrete type
        }
    }

    public class CustomSubDataConverter : JsonConverter<ICustomSubData>
    {
        public override ICustomSubData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (var doc = JsonDocument.ParseValue(ref reader))
            {
                var json = doc.RootElement.GetRawText();
                return JsonSerializer.Deserialize<CustomSubData>(json, options); // assumes Position is the only implementation
            }
        }

        public override void Write(Utf8JsonWriter writer, ICustomSubData value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, (CustomSubData)value, options); // cast to concrete type
        }
    }

    public class SystemSerializer: ICustomSerializer
    {
        private readonly JsonSerializerOptions m_options;
        public SystemSerializer()
        {
            m_options = new JsonSerializerOptions
            {
                WriteIndented = true, // for pretty printing
                ReferenceHandler = ReferenceHandler.Preserve,
                IncludeFields = true
            };
            m_options.Converters.Add(new PositionConverter());
            m_options.Converters.Add(new CustomSubDataConverter());
        }
        public string Serialize(CustomData data)
        {
            string jsonContent = JsonSerializer.Serialize(data, m_options);
            return jsonContent;
        }
        public CustomData Deserialize(string jsonContent)
        {
            CustomData deserialized = JsonSerializer.Deserialize<CustomData>(jsonContent, m_options);
            return deserialized;
        }
    }
}