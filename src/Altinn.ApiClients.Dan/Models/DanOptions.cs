using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altinn.ApiClients.Dan.Models
{
    public class DanOptions
    {
        public string SubscriptionKey { get; set; }
        public string ClientId { get; set; }
        public string Scopes { get; set; }
    }
}
