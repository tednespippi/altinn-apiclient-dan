namespace Altinn.ApiClients.Dan.Models
{
    /// <summary>
    /// Error object used to describe various errors
    /// </summary>
    public class Error 
    {
        /// <summary>
        /// An error code indicating the type of error
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// Human-readable description of the error
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A detail code, usually from the plugin handling the request, giving more information about the type of error occuring
        /// </summary>
        public string DetailCode { get; set; }

        /// <summary>
        /// Human-readable description of the detail error code
        /// </summary>
        public string DetailDescription { get; set; }

        /// <summary>
        /// Stack-trace, if available. Only available in non-production enviroments.
        /// </summary>
        public string StackTrace { get; set; }

        /// <summary>
        /// Inner exception message, if available. Only available in non-production enviroments.
        /// </summary>
        public string InnerExceptionMessage { get; set; }

        /// <summary>
        /// Inner stack-trace, if available. Only available in non-production enviroments.
        /// </summary>
        public string InnerExceptionStackTrace { get; set; }
    }
}
