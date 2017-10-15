using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Dto.Utility;

namespace MALParser.Dto
{
    public class AnimePage : BaseAnimeInfo
    {
        //Details
        public LinkInfo PromotionalVideo { get; set; }
        public string Description { get; set; }
        public string Background { get; set; }
        public List<LinkInfo> Adaptation { get; set; } = new List<LinkInfo>();
        public List<LinkInfo> Prequel { get; set; } = new List<LinkInfo>();
        public List<LinkInfo> Sequel { get; set; } = new List<LinkInfo>();
        public List<LinkInfo> SideStory { get; set; } = new List<LinkInfo>();
        public List<LinkInfo> SpinOff { get; set; } = new List<LinkInfo>();
        public List<LinkInfo> AlternativeVersion { get; set; } = new List<LinkInfo>();
        public List<LinkInfo> OtherLinks { get; set; } = new List<LinkInfo>();
        public List<string> OpeningTheme { get; set; }
        public List<string> EndingTheme { get; set; }
        public List<CharacterInfo> PresentedCharacters { get; set; } = new List<CharacterInfo>();
        public List<PersonInfo> PresentedStaff { get; set; } = new List<PersonInfo>();
        public List<ReviewInfo> PresentedReviews { get; set; } = new List<ReviewInfo>();
        public List<RecommendationInfo> PresentedRecommendations { get; set; } = new List<RecommendationInfo>();
    }
}
