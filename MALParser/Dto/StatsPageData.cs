using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Dto.Utility;

namespace MALParser.Dto
{
    public class StatsPageData
    {
        public BaseAnimeInfo BaseAnimeInfo { get; set; }

        public StatsPageData(BaseAnimeInfo baseAnimeInfo)
        {
            this.BaseAnimeInfo = baseAnimeInfo;
        }

        public Dictionary<AnimeSummaryStats, long> SummaryStats { get; set; } = new Dictionary<AnimeSummaryStats, long>();

        public Dictionary<int, long> ScoreStats { get; set; } = new Dictionary<int, long>();
    }
}
