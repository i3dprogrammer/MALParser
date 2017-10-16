using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Dto.Utility;

namespace MALParser.Dto
{
    public class RecommendationsPage : BaseAnimeInfo
    {
        public List<RecommendationInfo> Recommendations { get; set; }
    }

    public class RecommendationInfo
    {
        public LinkInfo RecommendationLink { get; set; }
        public LinkInfo AnimeImageLink { get; set; }
        public List<PersonDescriptionInfo> Reviews { get; set; }
    }
}
