using MALParser.Dto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.Dto
{
    public class CoreAnimeEntry
    {
        public string Title { get; set; }
        public LinkInfo ImageLink { get; set; }
        public LinkInfo AnimeLink { get; set; }
        public string Type { get; set; } //Should add enum to this..
        public string Synopsis { get; set; }
        public string Episodes { get; set; }

        public float Score { get; set; } = -1;
        public int Members { get; set; } = -1;
    }
}
