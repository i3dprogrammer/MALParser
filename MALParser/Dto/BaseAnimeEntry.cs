using MALParser.Dto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.Dto
{
    public class BaseAnimeEntry : CoreAnimeEntry
    {
        //Links area

        //Main Info
        public string Status { get; set; } //Should add enum to this..
        public string Aired { get; set; }
        public string Broadcast { get; set; }

        //Usually only 1 studio, yet to find a link with two
        public List<LinkInfo> Studios { get; set; } = new List<LinkInfo>();
        public AnimeSourceType Source { get; set; }
        public List<string> Genres { get; set; } = new List<string>();

        public override string ToString()
        {
            return $"#{Type} - {Title} -> {Aired} + {Broadcast}";
        }
    }
}
