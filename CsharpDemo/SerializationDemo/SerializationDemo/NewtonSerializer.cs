using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace SerializationDemo
{
    public class NewtonSerializer : ICustomSerializer
    {
        private readonly JsonSerializerSettings m_settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects,
            Formatting = Formatting.Indented,
            PreserveReferencesHandling = PreserveReferencesHandling.All,
            ObjectCreationHandling = ObjectCreationHandling.Replace,
            ContractResolver = new ArrayAsListContractResolver()
        };

        public string Serialize<T>(T data)
        {
            string json = JsonConvert.SerializeObject(data, m_settings);
            return json;
        }

        public T Deserialize<T>(string jsonContent)
        {
            T deserialized = JsonConvert.DeserializeObject<T>(jsonContent, m_settings);
            return deserialized;
        }

        public void AddConverter<T>()
        {
           
        }
        private class ArrayAsListContractResolver : DefaultContractResolver
        {
            protected override JsonContract CreateContract(Type objectType)
            {
                if (objectType.IsArray)
                {
                    var contract = base.CreateArrayContract(objectType);
                    contract.IsReference = false;
                    return contract;
                }
                return base.CreateContract(objectType);
            }
        }
    }

    //public class PolymorphicConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type objectType)
    //    {
    //        return true; // Обрабатываем любые типы
    //    }

    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        if (value == null)
    //        {
    //            writer.WriteNull();
    //            return;
    //        }

    //        var jo = JObject.FromObject(value, serializer);
    //        jo.AddFirst(new JProperty("$type", value.GetType().AssemblyQualifiedName));
    //        jo.WriteTo(writer);
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        if (reader.TokenType == JsonToken.Null) return null;

    //        var jo = JObject.Load(reader);

    //        // Определяем реальный тип через $type
    //        if (!jo.TryGetValue("$type", out var typeToken))
    //            throw new JsonSerializationException("Missing $type for polymorphic deserialization.");

    //        var typeName = typeToken.ToString();
    //        var actualType = Type.GetType(typeName);
    //        if (actualType == null)
    //            throw new JsonSerializationException($"Cannot find type '{typeName}'");

    //        // Убираем $type перед стандартной десериализацией
    //        jo.Remove("$type");

    //        return jo.ToObject(actualType, serializer);
    //    }
    //}
    //public class NewtonSerializer2: ICustomSerializer
    //{
    //    private JsonSerializerSettings m_settings;
    //    public NewtonSerializer2()
    //    {
    //        m_settings = new JsonSerializerSettings
    //        {
    //            TypeNameHandling = TypeNameHandling.None, // Мы используем свой $type
    //            PreserveReferencesHandling = PreserveReferencesHandling.All, // циклические ссылки
    //            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
    //            Formatting = Formatting.Indented,
    //            Converters = new List<JsonConverter>
    //            {
    //                new PolymorphicConverter()
    //            }
    //        };
    //    }
    //    #region ICustomSerializer
    //    public string Serialize<T>(T data)
    //    {
    //        string json = JsonConvert.SerializeObject(data, m_settings);
    //        return json;
    //    }

    //    public T Deserialize<T>(string jsonContent)
    //    {
    //        T deserialized = JsonConvert.DeserializeObject<T>(jsonContent, m_settings);
    //        return deserialized;
    //    }

    //    public void AddConverter<T>()
    //    {
    //        throw new NotImplementedException();
    //    }
    //    #endregion
    //}
}
