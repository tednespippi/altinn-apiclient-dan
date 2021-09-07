using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Altinn.ApiClients.Dan.Models.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Altinn.ApiClients.Dan.Models
{
    /// <summary>
    /// Describing the format and containing the value of an evidence
    /// </summary>
    [DataContract]
    public class DataSetParameter
    {
        /// <summary>
        /// A name describing the evidence parameter
        /// </summary>
        [Required]
        [DataMember(Name = "evidenceParamName")]
        public string DataSetParamName { get; set; }

        /// <summary>
        /// The value for the evidence parameter, if used in context of a evidence code request
        /// </summary>
        [DataMember(Name = "value")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Value { get; set; }

        /// <summary>
        /// The format of the evidence parameter
        /// </summary>
        [DataMember(Name = "paramType")]
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DataSetParamType? ParamType { get; set; }
    }
}