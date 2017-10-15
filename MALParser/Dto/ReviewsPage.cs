using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Dto.Utility;

namespace MALParser.Dto
{
    public class ReviewsPage : BaseAnimeInfo
    {
        public List<ReviewInfo> Reviews { get; set; }
    }

    public class ReviewInfo
    {
        public LinkInfo AllReviewsByThisGuy { get; set; }
        public DateTime Date { get; set; }
        public int OverallRating { get; set; }
        public int StoryRating { get; set; }
        public int AnimationRating { get; set; }
        public int SoundRating { get; set; }
        public int CharacterRating { get; set; }
        public int EnjoymentRating { get; set; }
        public PersonDescriptionInfo Description { get; set; }
        public string EpisodesSeen { get; set; }
        public int PeopleFoundHelpful { get; set; }
    }
}
