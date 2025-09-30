using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SerializationDemo
{
    public class GeneralConverter<T> : JsonConverter<T>
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var localOptions = new JsonSerializerOptions
            {
                IncludeFields = true
            };

            using (var doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;

                if (!root.TryGetProperty("$type", out var typeProperty))
                {
                    throw new JsonException("Missing $type property for polymorphic deserialization.");
                }

                var typeName = typeProperty.GetString();
                var actualType = Type.GetType(typeName);

                if (actualType == null)
                {
                    throw new JsonException($"Unable to resolve type '{typeName}'.");
                }

                var json = root.GetRawText();
                var result = JsonSerializer.Deserialize(json, actualType, localOptions);

                if (result == null)
                {
                    throw new JsonException("Deserialization returned null.");
                }
                return (T)result;
            }
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            var actualType = value.GetType();
            var typeName = actualType.AssemblyQualifiedName;

            JsonElement element = JsonSerializer.SerializeToElement(value, value.GetType(), options);

            using (var doc = JsonDocument.Parse(JsonSerializer.Serialize(value, actualType, options)))
            {
                writer.WriteStartObject();
                // Write type info
                writer.WriteString("$type", typeName);
                // Write all properties
                foreach (var property in doc.RootElement.EnumerateObject())
                {
                    property.WriteTo(writer);
                }

                writer.WriteEndObject();
            }
        }
    }

    public class GeneralSystemSerializer : ICustomSerializer
    {
        private readonly JsonSerializerOptions m_options;
        public GeneralSystemSerializer()
        {
            m_options = new JsonSerializerOptions
            {
                WriteIndented = true, // for pretty printing
                ReferenceHandler = ReferenceHandler.Preserve,
                IncludeFields = true
            };
            m_options.Converters.Add(new GeneralConverter<IPosition>());
            m_options.Converters.Add(new GeneralConverter<ICustomSubData>());
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