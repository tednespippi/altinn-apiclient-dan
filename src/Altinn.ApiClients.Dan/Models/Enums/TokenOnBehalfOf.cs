using System.Runtime.Serialization;

namespace Altinn.ApiClients.Dan.Models.Enums
{
    /// <summary>
    /// Enum to indicate what party DAN (as in Digitaliseringsdirektoratet) should attempt to fetch an supplier access token on behalf of from Maskinporten
    /// </summary>
    public enum TokenOnBehalfOf
    {
        /// <summary>
        /// Attempt to fetch a token on behalf of the owner (the party making the actual request to DAN)
        /// </summary>
        [EnumMember(Value = "owner")]
        Owner,

        /// <summary>
        /// Attempt to fetch a token on behalf of the supplied requestor in the authorization request
        /// </summary>
        [EnumMember(Value = "requestor")]
        Requestor,

        /// <summary>
        /// Attempt to fetch a token on behalf of the supplied subject in the authorization request
        /// </summary>
        [EnumMember(Value = "subject")]
        Subject
    }
}
