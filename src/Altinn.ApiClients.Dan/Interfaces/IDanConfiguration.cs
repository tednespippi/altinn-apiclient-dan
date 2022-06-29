namespace Altinn.ApiClients.Dan.Interfaces
{
    /// <summary>
    /// Interface for the configuration object for DAN
    /// </summary>
    public interface IDanConfiguration
    {
        /// <summary>
        /// Returns the IDanDeserializer implementation that should be used for deserializing data received from DAN API when mapping to a model
        /// </summary>
        public IDanDeserializer Deserializer { get; }
    }
}
