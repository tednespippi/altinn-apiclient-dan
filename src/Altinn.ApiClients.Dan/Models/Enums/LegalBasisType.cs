using System.Runtime.Serialization;

namespace Altinn.ApiClients.Dan.Models.Enums
{
    /// <summary>
    /// The type of legal basis
    /// </summary>
    public enum LegalBasisType
    {
        /// <summary>
        /// Legal basis is a ESPD document
        /// </summary>
        [EnumMember(Value = "ESPD")] Espd = 1,

        /// <summary>
        /// CPV code use for procurements
        /// </summary>
        [EnumMember(Value = "cpv")] Cpv = 2,
    }
}