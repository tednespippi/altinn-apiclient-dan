using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Altinn.ApiClients.Dan.Models
    {
    /// <summary>
    /// The accreditation returned from a successful authorization
    /// </summary>
    [DataContract]
    public class Accreditation
    {
        /// <summary>
        /// Gets or sets the identifier for the accreditation
        /// </summary>
        [DataMember(Name = "id")]
        public string AccreditationId { get; set; }

        /// <summary>
        /// Gets or sets the party requesting the evidence
        /// </summary>
        [DataMember(Name = "requestor")]
        public string Requestor { get; set; }

        /// <summary>
        /// Gets or sets the party the evidence is requested for
        /// </summary>
        [Required]
        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the aggregate status for all evidence codes in the accreditation, excluding asynchronous evidence codes.
        /// </summary>
        [Required]
        [DataMember(Name = "aggregateStatus")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DataSetStatusCode AggregateStatus { get; set; }

        [DataMember(Name = "isDirectHarvest")]
        public bool IsDirectHarvest { get; set; }

        /// <summary>
        /// Gets or sets a list of evidence codes associated with the accreditation. Only supplied when requesting a single accreditation.
        /// </summary>
        [DataMember(Name = "evidenceCodes")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<DataSet> EvidenceCodes { get; set; }

        /// <summary>
        /// Gets or sets when the accreditation was created
        /// </summary>
        [Required]
        [DataMember(Name = "issued")]
        public DateTime? Issued { get; set; }

        /// <summary>
        /// Gets or sets when the accreditation was last changed. Usually means the time at which an consent request was answered.
        /// </summary>
        [Required]
        [DataMember(Name = "lastChanged")]
        public DateTime? LastChanged { get; set; }

        /// <summary>
        /// Gets or sets how long the accreditation is valid
        /// </summary>
        [Required]
        [DataMember(Name = "validTo")]
        public DateTime ValidTo { get; set; }

        /// <summary>
        /// Gets or sets TED reference
        /// </summary>
        [DataMember(Name = "consentReference")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ConsentReference { get; set; }


        /// <summary>
        /// Gets or sets arbitrary reference provided in the authorization call
        /// </summary>
        [DataMember(Name = "externalReference")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ExternalReference { get; set; }

        /// <summary>
        /// Gets or sets a list of evidenceCodes that have been verified
        /// </summary>
        [DataMember(Name = "evidenceCodesWithVerifiedLegalBasis")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> EvidenceCodesWithVerifiedLegalBasis { get; set; }

        /// <summary>
        /// Used by the JSON serializer, causes the above field to only be included if non-empty
        /// </summary>
        /// <returns>True if the list is not empty</returns>
        public bool ShouldSerializeEvidenceCodesWithVerifiedLegalBasis()
        {
            return EvidenceCodesWithVerifiedLegalBasis?.Count > 0;
        }

        /// <summary>
        /// Gets or sets the authorization code used with consents
        /// </summary>
        [DataMember(Name = "authorizationCode")]
        public string AuthorizationCode { get; set; }

        /// <summary>
        /// Gets or sets the owner (organization number) for this accreditation. Matched against enterprise certificates.
        /// </summary>
        [DataMember(Name = "Owner")]
        public string Owner { get; set; }

        /// <summary>
        /// The selected language for the accreditation, used for consent request texts and notifications, no-nb, no-nn or en
        /// </summary>
        [DataMember(Name = "languageCode")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string LanguageCode { get; set; }
    }
}
