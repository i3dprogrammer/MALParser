using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Dto.Utility;

namespace MALParser.Dto
{
    public class RecommendationsPage
    {
        public BaseAnimeInfo BaseAnimeInfo { get; set; }

        public RecommendationsPage(BaseAnimeInfo baseAnimeInfo)
        {
            this.BaseAnimeInfo = baseAnimeInfo;
        }

        public List<RecommendationInfo> Recommendations { get; set; }
    }

    public class RecommendationInfo
    {
        public int RecommendedUsers { get; set; }
        public LinkInfo RecommendationLink { get; set; }
        public LinkInfo AnimeImageLink { get; set; }
        public List<PersonDescriptionInfo> Reviews { get; set; }
    }
}
