using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Dto.Utility;

namespace MALParser.Dto.AnimePageModels
{
    public class StatsPageData : BaseAnimeInfoActor
    {
        public StatsPageData(AnimePageHeader baseAnimeInfo) : base(baseAnimeInfo) { }

        public Dictionary<AnimeStatsType, long> SummaryStats { get; set; } = new Dictionary<AnimeStatsType, long>();

        public Dictionary<int, long> ScoreStats { get; set; } = new Dictionary<int, long>();
    }
}
