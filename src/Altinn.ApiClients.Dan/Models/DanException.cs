using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Refit;

namespace Altinn.ApiClients.Dan.Models
{
    public class DanException : Exception
    { 
        public DanException(string message) : base(message) {}
        public DanException(string message, Exception innerException) : base(message, innerException) {}

        public Error Error { get; set; }

        public static DanException FromApiException(ApiException exception)
        {
            Error error;

            if (!string.IsNullOrEmpty(exception.Content))
            {
                try
                {
                    error = JsonSerializer.Deserialize<Error>(exception.Content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
                catch (JsonException)
                {
                    error = new Error { Code = -2, Description = "Unknown error: " + exception.Content };
                }
            }
            else
            {
                error = new Error { Code = -1, Description = "Unknown error" };
            }

            return new DanException(error!.Description) { Error = error };
        }
    }
}
