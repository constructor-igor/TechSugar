using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace SerializationDemo
{
    public class GeneralConverter<T> : JsonConverter<T>
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            //var localOptions = CreateReadSafetyOptions(options);
            var localOptions = options;

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

                var fixedJson = FixLists(root).GetRawText();
                //string fixedJson = root.GetRawText();
                var result = JsonSerializer.Deserialize(fixedJson, actualType, localOptions);
    
                // var json = root.GetRawText();
                // var result = JsonSerializer.Deserialize(json, actualType, localOptions);
                
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
            //var localOptions = CreateWriteSafetyOptions(options);
            var localOptions = options;

            using (var doc = JsonDocument.Parse(JsonSerializer.Serialize(value, actualType, localOptions)))
            {
                writer.WriteStartObject();
                writer.WriteString("$type", typeName);
                foreach (var property in doc.RootElement.EnumerateObject())
                {
                    property.WriteTo(writer);
                }
                writer.WriteEndObject();
            }
        }

        private JsonSerializerOptions CreateReadSafetyOptions(JsonSerializerOptions options)
        {
            var safeOptions = new JsonSerializerOptions
            {
                IncludeFields = true,
            };
            for (int i = safeOptions.Converters.Count - 1; i >= 0; i--)
            {
                if (safeOptions.Converters[i].GetType() == this.GetType())
                    safeOptions.Converters.RemoveAt(i);
            }
            return safeOptions;
        }

        private JsonSerializerOptions CreateWriteSafetyOptions(JsonSerializerOptions options)
        {
            var safeOptions = new JsonSerializerOptions(options);
            for (int i = safeOptions.Converters.Count - 1; i >= 0; i--)
            {
                if (safeOptions.Converters[i].GetType() == this.GetType())
                    safeOptions.Converters.RemoveAt(i);
            }
            return safeOptions;
        }
        
        private JsonElement FixLists(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Object)
            {
                using (var doc = JsonDocument.Parse("{}"))
                {
                    var obj = new Dictionary<string, JsonElement>();
    
                    foreach (var prop in element.EnumerateObject())
                    {
                        if (prop.Value.ValueKind == JsonValueKind.Object &&
                            prop.Value.TryGetProperty("$values", out var values))
                        {
                            // Replace object with $values array
                            obj[prop.Name] = values;
                        }
                        else
                        {
                            obj[prop.Name] = FixLists(prop.Value);
                        }
                    }
    
                    var json = JsonSerializer.Serialize(obj);
                    using (var newDoc = JsonDocument.Parse(json))
                    {
                        return newDoc.RootElement.Clone();
                    }
                }
            } else
            if (element.ValueKind == JsonValueKind.Array)
            {
                var list = new List<JsonElement>();
                foreach (var item in element.EnumerateArray())
                {
                    list.Add(FixLists(item));
                }
                var json = JsonSerializer.Serialize(list);
                using (var newDoc = JsonDocument.Parse(json))
                {
                    return newDoc.RootElement.Clone();
                }
            }
            return element.Clone();
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
                //ReferenceHandler = ReferenceHandler.Preserve,
                //ReferenceHandler = ReferenceHandler.IgnoreCycles,
                IncludeFields = true,
            };
            // m_options.Converters.Add(new GeneralConverter<ICustomData>());
            // m_options.Converters.Add(new GeneralConverter<IPosition>());
            // m_options.Converters.Add(new GeneralConverter<ICustomSubData>());
        }
        public void AddConverter<T>()
        {
            m_options.Converters.Add(new GeneralConverter<T>());
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