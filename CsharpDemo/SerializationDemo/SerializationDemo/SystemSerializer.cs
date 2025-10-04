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
                var root = doc.RootElement;
                if (!root.TryGetProperty("Type", out var typeProp))
                    throw new JsonException("Missing Type discriminator");

                var localOptions = new JsonSerializerOptions
                {
                    IncludeFields = true
                };

                string typeDiscriminator = typeProp.GetString();

                switch (typeDiscriminator)
                {
                    case "CustomSubData1": 
                        return JsonSerializer.Deserialize<CustomSubData1>(root.GetRawText(), localOptions);
                    case "CustomSubData2":
                        return JsonSerializer.Deserialize<CustomSubData2>(root.GetRawText(), localOptions);
                }
                throw new NotSupportedException($"Type discriminator {typeDiscriminator} is not supported for deserialization.");
            }
        }

        public override void Write(Utf8JsonWriter writer, ICustomSubData value, JsonSerializerOptions options)
        {
            string typeDiscriminator;
            if (value is CustomSubData1)
                typeDiscriminator = "CustomSubData1";
            else if (value is CustomSubData2)
                typeDiscriminator = "CustomSubData2";
            else
                throw new NotSupportedException($"Type {value.GetType().FullName} is not supported for serialization.");
            using (var jsonDoc = JsonDocument.Parse(JsonSerializer.Serialize(value, value.GetType(), options)))
            {
                writer.WriteStartObject();
                writer.WriteString("Type", typeDiscriminator);
                foreach (var prop in jsonDoc.RootElement.EnumerateObject())
                {
                    prop.WriteTo(writer);
                }
                writer.WriteEndObject();
            }
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
        public string Serialize<T>(T data)
        {
            string jsonContent = JsonSerializer.Serialize(data, m_options);
            return jsonContent;
        }
        public T Deserialize<T>(string jsonContent)
        {
            T deserialized = JsonSerializer.Deserialize<T>(jsonContent, m_options);
            return deserialized;
        }
    }
}