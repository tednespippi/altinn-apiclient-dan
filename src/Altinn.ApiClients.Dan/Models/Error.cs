using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Altinn.ApiClients.Dan.Models
{
    public class Error 
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public string StackTrace { get; set; }
        public string InnerStackTrace { get; set; }
    }
}
