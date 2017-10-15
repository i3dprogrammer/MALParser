using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Dto.Utility;

namespace MALParser.Dto
{
    public class StatsPage : BaseAnimeInfo
    {
        public int Watching { get; set; }
        public int Completed { get; set; }
        public int Onhold { get; set; }
        public int Dropped { get; set; }
        public int PlatToWatch { get; set; }
        public int Total { get; set; }

        //TODO: Add the rest of the page.
    }
}
