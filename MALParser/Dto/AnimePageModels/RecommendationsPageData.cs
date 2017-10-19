using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Dto.Utility;

namespace MALParser.Dto.AnimePageModels
{
    public class RecommendationsPageData : BaseAnimeInfoActor
    {
        public RecommendationsPageData(AnimePageHeader baseAnimeInfo) : base(baseAnimeInfo) { }

        public List<RecommendationInfo> Recommendations { get; set; }
    }

    public class RecommendationInfo
    {
        public int RecommendedUsers { get; set; }
        public LinkInfo RecommendationAnime { get; set; }
        public LinkInfo AnimeImageLink { get; set; }
        public List<DescriptionInfo> Reviews { get; set; }
    }

    public class DescriptionInfo
    {
        public string Description { get; set; }
        public LinkInfo By { get; set; }
    }
}
