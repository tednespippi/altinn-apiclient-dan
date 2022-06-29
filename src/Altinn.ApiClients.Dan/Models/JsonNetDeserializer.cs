using Newtonsoft.Json;
using Altinn.ApiClients.Dan.Interfaces;

namespace Altinn.ApiClients.Dan.Models
{
    /// <summary>
    /// Implementation of IDanDeserializer using Newtonsoft.Json (JSON.NET)
    /// </summary>
    public class JsonNetDeserializer : IDanDeserializer
    {
        /// <summary>
        /// Serializer settings for Newtonsoft.Json
        /// </summary>
        public JsonSerializerSettings SerializerSettings { get; set; }

        /// <inheritdoc />
        public T Deserialize<T>(string json) where T : new()
        {
            return SerializerSettings != null 
                ? JsonConvert.DeserializeObject<T>(json, SerializerSettings)
                : JsonConvert.DeserializeObject<T>(json);
        }
    }
}
