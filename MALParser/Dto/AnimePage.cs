using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Dto.Utility;

namespace MALParser.Dto
{
    public class AnimePage
    {
        public BaseAnimeInfo BaseAnimeInfo { get; set; }

        public AnimePage(BaseAnimeInfo baseAnimeInfo)
        {
            this.BaseAnimeInfo = baseAnimeInfo;
        }

        //Details
        public LinkInfo PromotionalVideo { get; set; }
        public string Description { get; set; }
        public string Background { get; set; }

        //TODO: Should drop all this and change it to Dictionary<string, List<LinkInfo>> for easier usage?
        //TODO: Shouldn't initalize all of them like that. Initialize the used ones only!
        public List<LinkInfo> Adaptation { get; set; } = new List<LinkInfo>();
        public List<LinkInfo> Prequel { get; set; } = new List<LinkInfo>();
        public List<LinkInfo> Sequel { get; set; } = new List<LinkInfo>();
        public List<LinkInfo> SideStory { get; set; } = new List<LinkInfo>();
        public List<LinkInfo> SpinOff { get; set; } = new List<LinkInfo>();
        public List<LinkInfo> AlternativeVersion { get; set; } = new List<LinkInfo>();
        public List<LinkInfo> OtherLinks { get; set; } = new List<LinkInfo>();
        public List<LinkInfo> ParentStory { get; set; } = new List<LinkInfo>();
        public List<LinkInfo> Summary { get; set; } = new List<LinkInfo>();

        public List<CharacterInfo> PresentedCharacters { get; set; } = new List<CharacterInfo>();
        public List<PersonInfo> PresentedStaff { get; set; } = new List<PersonInfo>();

        public List<string> OpeningTheme { get; set; } = new List<string>();
        public List<string> EndingTheme { get; set; } = new List<string>();

        public List<ReviewInfo> PresentedReviews { get; set; } = new List<ReviewInfo>();
        public List<RecommendationInfo> PresentedRecommendations { get; set; } = new List<RecommendationInfo>();

    }
}
