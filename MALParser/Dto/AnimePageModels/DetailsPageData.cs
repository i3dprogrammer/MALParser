using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Dto.Utility;

namespace MALParser.Dto.AnimePageModels
{
    public class DetailsPageData : BaseAnimeInfoActor
    {
        public DetailsPageData(AnimePageHeader baseAnimeInfo) : base(baseAnimeInfo)
        {
        }

        //Details
        public LinkInfo PromotionalVideo { get; set; }
        public string Description { get; set; }
        public string Background { get; set; }
        public List<string> EndingTheme { get; set; } = new List<string>();
        public List<string> OpeningTheme { get; set; } = new List<string>();
        public List<ReviewInfo> PresentedReviews { get; set; } = new List<ReviewInfo>();
        public List<PersonInfo> PresentedStaff { get; set; } = new List<PersonInfo>();
        public List<CharacterInfo> PresentedCharacters { get; set; } = new List<CharacterInfo>();
        public List<RecommendationInfo> PresentedRecommendations { get; set; } = new List<RecommendationInfo>();
        public Dictionary<RelatedAnime, List<LinkInfo>> RelatedAnime { get; set; } = new Dictionary<MALParser.RelatedAnime, List<LinkInfo>>();

    }
}
