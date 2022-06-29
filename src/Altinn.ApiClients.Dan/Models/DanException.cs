using System;
using System.Text.Json;
using Refit;

namespace Altinn.ApiClients.Dan.Models
{
    /// <summary>
    /// The main DAN exception object.
    /// </summary>
    public class DanException : Exception
    {
        /// <inheritdoc />
        public DanException(string message) : base(message) {}

        /// <inheritdoc />
        public DanException(string message, Exception innerException) : base(message, innerException) {}

        /// <summary>
        /// An Error object containing additional error information, if any
        /// </summary>
        public Error Error { get; set; }

        /// <summary>
        /// Transforms an ApiException from Refit to a DanException
        /// </summary>
        /// <param name="exception">The ApiException from Refit</param>
        /// <returns>A DanException</returns>
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

            var message = error!.Description;
            if (error.DetailCode != null)
            {
                message += $"; detailCode: {error.DetailCode}";
            }

            if (error.DetailDescription != null)
            {
                message += $"; detailDescription: {error.DetailDescription}";
            }

            if (error.InnerExceptionMessage != null)
            {
                message += $"; innerException: {error.InnerExceptionMessage}";
            }

            return new DanException(message) { Error = error };
        }
    }
}
