using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    public class AcceptUpdateRequest
    {
        public string token { get; set; }
        public int ID { get; set; }
        public int idfriend_request { get; set; }
        public bool bitAccept { get; set; }
    }
}
