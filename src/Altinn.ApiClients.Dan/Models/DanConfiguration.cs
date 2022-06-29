using Altinn.ApiClients.Dan.Interfaces;

namespace Altinn.ApiClients.Dan.Models
{
    /// <summary>
    /// Default implementation of IDanConfiguration
    /// </summary>
    public class DanConfiguration : IDanConfiguration
    {
        /// <inheritdoc />
        public IDanDeserializer Deserializer { get; set; }
    }
}
