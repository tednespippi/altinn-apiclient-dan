using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Altinn.ApiClients.Dan.Models
{
    /// <summary>
    /// The authorization model that must be supplied to that /authorization REST endpoint
    /// </summary>
    [DataContract]
    public class AuthorizationRequest 
    {
        /// <summary>
        /// The party requesting the evidence
        /// </summary>
        [DataMember(Name = "requestor")]
        public string Requestor { get; set; }

        /// <summary>
        /// The party the evidence is requested for
        /// </summary>
        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        /// <summary>
        /// The requested evidence
        /// </summary>
        [DataMember(Name = "evidenceRequests")]
        public List<DataSetRequest> DataSetRequests { get; set; }

        /// <summary>
        /// List of legal basis proving legal authority for the requested evidence
        /// </summary>
        [DataMember(Name = "legalBasisList")]
        public List<LegalBasis> LegalBasisList { get; set; }

        /// <summary>
        /// How long the accreditation should be valid. Also used for duration of consent (date part only).
        /// </summary>
        [DataMember(Name = "validTo")]
        public DateTime? ValidTo { get; set; }

        /// <summary>
        /// TED reference number, if applicable
        /// </summary>
        [DataMember(Name = "consentReference")]
        public string ConsentReference { get; set; }

        /// <summary>
        /// Arbitrary reference which will be saved with the Accreditation
        /// </summary>
        [DataMember(Name = "externalReference")]
        public string ExternalReference { get; set; }

        [DataMember(Name = "languageCode")]
        public string LanguageCode { get; set; }
    }
}