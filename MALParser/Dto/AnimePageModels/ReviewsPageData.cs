using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Dto.Utility;

namespace MALParser.Dto.AnimePageModels
{
    public class ReviewsPageData : BaseAnimeInfoActor
    {
        public ReviewsPageData(AnimePageHeader baseAnimeInfo) : base(baseAnimeInfo) { }

        public List<ReviewInfo> Reviews { get; set; } = new List<ReviewInfo>();

        public bool NextPageAvailable { get { return NextPageLink != null; } }
        public bool PreviousPageAvailable { get { return PreviousPageLink != null; } }

        internal LinkInfo NextPageLink { get; set; }
        internal LinkInfo PreviousPageLink { get; set; }

        public async Task<ReviewsPageData> ParseNextPageAsync()
        {
            if (NextPageLink == null)
                throw new Exception("Cannot parse next page because it does not exist!");

            return await AnimePage.Reviews.ParseAsync(NextPageLink.Path);
        }

        public ReviewsPageData ParseNextPage()
        {
            if (NextPageLink == null)
                throw new Exception("Cannot parse next page because it does not exist!");

            return AnimePage.Reviews.Parse(NextPageLink.Path);
        }

        public async Task<ReviewsPageData> ParsePreviousPageAsync()
        {
            if (PreviousPageLink == null)
                throw new Exception("Cannot parse previous page because it does not exist!");

            return await AnimePage.Reviews.ParseAsync(PreviousPageLink.Path);
        }

        public ReviewsPageData ParsePreviousPage()
        {
            if (PreviousPageLink == null)
                throw new Exception("Cannot parse previous page because it does not exist!");

            return AnimePage.Reviews.Parse(PreviousPageLink.Path);
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
