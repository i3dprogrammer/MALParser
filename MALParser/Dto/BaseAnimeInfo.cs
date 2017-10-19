using MALParser.Dto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.Dto
{
    public class BaseAnimeInfo
    {
        //Links area

        //Main Info
        public string Title { get; set; }
        public LinkInfo ImageLink { get; set; }
        public string EnglishTitle { get; set; }
        public string Type { get; set; } //Should add enum to this..
        public string Episodes { get; set; }
        public string Status { get; set; } //Should add enum to this..
        public string Aired { get; set; }
        public string Premiered { get; set; }
        public string Broadcast { get; set; }
        public string Synopsis { get; set; }

        //Usually only 1 studio, yet to find a link with two
        public List<LinkInfo> Studios { get; set; } = new List<LinkInfo>();
        public SourceType Source { get; set; }
        public List<string> Genres { get; set; } = new List<string>();

        //Statistics
        public float Score { get; set; } = -1;
        public int Members { get; set; } = -1;
    }
}
