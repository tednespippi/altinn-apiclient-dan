using System.Text.Json;
using Altinn.ApiClients.Dan.Interfaces;

namespace Altinn.ApiClients.Dan.Models
{
    /// <summary>
    /// Implementation of IDanDeserializer using System.Text.Json
    /// </summary>
    public class SystemTextJsonDeserializer : IDanDeserializer
    {
        /// <summary>
        /// Serializer options for System.Text.Json
        /// </summary>
        public JsonSerializerOptions SerializerOptions { get; set; }

        /// <inheritdoc />
        public T Deserialize<T>(string json) where T : new()
        {
            return SerializerOptions != null 
                ? JsonSerializer.Deserialize<T>(json, SerializerOptions) 
                : JsonSerializer.Deserialize<T>(json);
        }
    }
}
