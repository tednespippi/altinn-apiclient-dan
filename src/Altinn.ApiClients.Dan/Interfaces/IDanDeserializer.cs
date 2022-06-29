namespace Altinn.ApiClients.Dan.Interfaces
{
    /// <summary>
    /// Interface that various deserializers should implement
    /// </summary>
    public interface IDanDeserializer
    {
        /// <summary>
        /// Returns the deserialized model indicated by the type param for the supplied JSON string
        /// </summary>
        /// <param name="json">The JSON string used for deserializing</param>
        /// <typeparam name="T">The model to map to</typeparam>
        /// <returns>The model indicated by the type param</returns>
        T Deserialize<T>(string json) where T : new();
    }
}
