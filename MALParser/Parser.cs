using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MALParser
{
    public class Parser
    {
        HttpClient client;

        public Parser()
        {
            client = new HttpClient();
        }
    }
}
