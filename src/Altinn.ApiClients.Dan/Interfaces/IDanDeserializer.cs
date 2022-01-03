using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altinn.ApiClients.Dan.Interfaces
{
    public interface IDanDeserializer
    {
        T Deserialize<T>(string json) where T : new();
    }
}
