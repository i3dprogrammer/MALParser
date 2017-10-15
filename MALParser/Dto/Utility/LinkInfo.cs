using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.Dto.Utility
{
    public class LinkInfo
    {
        public string Path { get; set; }
        public string Name { get; set; }

        public LinkInfo() { }

        public LinkInfo(string path)
        {
            Path = path;
        }

        public LinkInfo(string path, string name)
        {
            Path = path;
            Name = name;
        }
    }
}
