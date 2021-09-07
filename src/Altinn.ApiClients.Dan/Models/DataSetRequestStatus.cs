using System;
using System.Runtime.Serialization;

namespace Altinn.ApiClients.Dan.Models
{
    /// <summary>
    /// Evidence Request as part of an Authorization model
    /// </summary>
    [DataContract]
    public class DataSetRequestStatus
    {
        /// <summary>
        /// The name of the evidence code this status refers to
        /// </summary>
        [DataMember(Name = "evidenceCodeName")]
        public string DataSetName { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name = "status")]
        public DataSetStatusCode Status { get; set; }

        /// <summary>
        /// From when the evidence code is available
        /// </summary>
        [DataMember(Name = "validFrom")]
        public DateTime? ValidFrom { get; set; }

        /// <summary>
        /// Until when the evidence code is available
        /// </summary>
        [DataMember(Name = "validTo")]
        public DateTime? ValidTo { get; set; }

        /// <summary>
        /// Whether or not the authorization included legal basis. For now, this is always ESPD
        /// </summary>
        [DataMember(Name = "didSupplyLegalBasis")]
        public bool? DidSupplyLegalBasis { get; set; }
    }
}