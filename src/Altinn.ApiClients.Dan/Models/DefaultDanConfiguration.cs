using Altinn.ApiClients.Dan.Interfaces;

namespace Altinn.ApiClients.Dan.Models
{
    /// <summary>
    /// The default DAN configutation implementation using System.Text.Json
    /// </summary>
    public class DefaultDanConfiguration : IDanConfiguration
    {
        /// <inheritdoc />
        public IDanDeserializer Deserializer => new SystemTextJsonDeserializer();
    }
}
