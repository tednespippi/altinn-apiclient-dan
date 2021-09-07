using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Altinn.ApiClients.Dan.Models
{
    /// <summary>
    /// Describing an EvidenceCode and what values it carries. When used in context of a Accreditation, also includes the timespan of which the evidence is available
    /// </summary>
    [DataContract]
    public class DataSet
    {
        /// <summary>
        /// Name of the evidence code
        /// </summary>
        [Required]
        [DataMember(Name = "evidenceCodeName")]
        public string DataSetName { get; set; }

        /// <summary>
        /// Arbitrary text describing the purpose and content of the evidence code
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }        

        /// <summary>
        /// If the evidence code has any parameters
        /// </summary>
        [DataMember(Name = "parameters")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<DataSetParameter> Parameters { get; set; }

        /// <summary>
        /// Whether or not the evidence code has been flagged as representing data not available by simple lookup.
        /// This causes Core to perform an explicit call to the harvester function of the evidence source to 
        /// initialize or check for status.
        /// </summary>
        [DataMember(Name = "isAsynchronous")]
        public bool IsAsynchronous { get; set; }


        /// <summary>
        /// The values associated with this evidence code
        /// </summary>
        [Required]
        [DataMember(Name = "values")]
        public List<DataSetValues> Values { get; set; }
        
    }
}
