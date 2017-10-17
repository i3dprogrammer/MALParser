using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Dto.Utility;

namespace MALParser.Dto
{
    public class ReviewsPage
    {
        public BaseAnimeInfo BaseAnimeInfo { get; set; }

        public ReviewsPage(BaseAnimeInfo baseAnimeInfo)
        {
            this.BaseAnimeInfo = baseAnimeInfo;
        }

        public List<ReviewInfo> Reviews { get; set; } = new List<ReviewInfo>();

        //public int PageNumber { get; set; } = -1;

        public bool NextPageAvailable { get { return NextPageLink != null; } }
        public bool PreviousPageAvailable { get { return PreviousPageLink != null; } }

        internal LinkInfo NextPageLink { get; set; }
        internal LinkInfo PreviousPageLink { get; set; }

        public async Task<ReviewsPage> ParseNextPageAsync()
        {
            if (NextPageLink == null)
                throw new Exception("Cannot parse next page because it does not exist!");

            return await MALParser.Anime.Reviews.ParseAsync(NextPageLink.Path);
        }

        public ReviewsPage ParseNextPage()
        {
            if (NextPageLink == null)
                throw new Exception("Cannot parse next page because it does not exist!");

            return MALParser.Anime.Reviews.Parse(NextPageLink.Path);
        }

        public async Task<ReviewsPage> ParsePreviousPageAsync()
        {
            if (PreviousPageLink == null)
                throw new Exception("Cannot parse previous page because it does not exist!");

            return await MALParser.Anime.Reviews.ParseAsync(PreviousPageLink.Path);
        }

        public ReviewsPage ParsePreviousPage()
        {
            if (PreviousPageLink == null)
                throw new Exception("Cannot parse previous page because it does not exist!");

            return MALParser.Anime.Reviews.Parse(PreviousPageLink.Path);
        }
    }

    public class ReviewInfo
    {
        public LinkInfo AllReviewsByThisGuy { get; set; }
        public DateTime Date { get; set; }
        public int OverallRating { get; set; } = -1;
        public int StoryRating { get; set; } = -1;
        public int AnimationRating { get; set; } = -1;
        public int SoundRating { get; set; } = -1;
        public int CharacterRating { get; set; } = -1;
        public int EnjoymentRating { get; set; } = -1;
        public string ReviewDescription { get; set; }
        public LinkInfo Reviewer { get; set; }
        public string EpisodesSeen { get; set; }
        public int PeopleFoundHelpful { get; set; } = -1;
        public LinkInfo ImageLink { get; set; }
    }
}
