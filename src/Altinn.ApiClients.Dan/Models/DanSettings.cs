namespace Altinn.ApiClients.Dan.Models
{
    /// <summary>
    /// Settings object for DAN including the subscription key and chosen environment
    /// </summary>
    public class DanSettings
    {
        /// <summary>
        /// The subscription key to be used
        /// </summary>
        public string SubscriptionKey { get; set; }

        /// <summary>
        /// The requested environment. Valid values: dev, staging, prod
        /// </summary>
        public string Environment { get; set; }
    }
}