using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Altinn.ApiClients.Dan.Interfaces;

namespace Altinn.ApiClients.Dan.Models
{
    public class DanConfiguration : IDanConfiguration
    {
        public IDanDeserializer Deserializer { get; set; }
    }
}
