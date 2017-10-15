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
        public LinkInfo Adaptation { get; set; }
        public LinkInfo Prequel { get; set; }
        public LinkInfo Sequel { get; set; }
        public List<LinkInfo> SideStory { get; set; }
        public List<LinkInfo> SpinOff { get; set; }
        public List<LinkInfo> AlternativeVersion { get; set; }
        public List<LinkInfo> OtherLinks { get; set; }
        public List<string> OpeningTheme { get; set; }
        public List<string> EndingTheme { get; set; }
        public List<CharacterInfo> PresentedCharacters { get; set; }
        public List<PersonInfo> PresentedStaff { get; set; }
        public List<ReviewInfo> PresentedReviews { get; set; }
        public List<RecommendationInfo> PresentedRecommendations { get; set; }
    }
}
