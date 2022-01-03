using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Altinn.ApiClients.Dan.Interfaces;
using Microsoft.Extensions.Options;

namespace Altinn.ApiClients.Dan.Models
{
    public class SystemTextJsonDeserializer : IDanDeserializer
    {
        public JsonSerializerOptions SerializerOptions { get; set; }

        public T Deserialize<T>(string json) where T : new()
        {
            return SerializerOptions != null 
                ? JsonSerializer.Deserialize<T>(json, SerializerOptions) 
                : JsonSerializer.Deserialize<T>(json);
        }
    }
}
