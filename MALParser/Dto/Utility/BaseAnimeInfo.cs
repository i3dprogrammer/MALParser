using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.Dto.Utility
{
    public class BaseAnimeInfo
    {
        //Links area
        public LinkInfo DetailsLink { get; set; }
        public LinkInfo VideosLink { get; set; }
        public LinkInfo EpisodesLink { get; set; }
        public LinkInfo ReviewsLink { get; set; }
        public LinkInfo RecommendationsLink { get; set; }
        public LinkInfo StatsLink { get; set; }
        public LinkInfo CharactersAndStaffLink { get; set; }
        public LinkInfo NewsLink { get; set; }
        public LinkInfo ForumLink { get; set; }
        public LinkInfo FeaturedLink { get; set; }
        public LinkInfo ClubsLink { get; set; }
        public LinkInfo PicturesLink { get; set; }

        //Main Info
        public string Title { get; set; }
        public LinkInfo ImageLink { get; set; }
        public string EnglishTitle { get; set; }
        public string SynonymsTitle { get; set; }
        public string JapaneseTitle { get; set; }
        public string Type { get; set; } //Should add enum to this..
        public string Episodes { get; set; }
        public string Status { get; set; } //Should add enum to this..
        public string Aired { get; set; }
        public string Premiered { get; set; }
        public string Broadcast { get; set; }
        public List<LinkInfo> Producers { get; set; } = new List<LinkInfo>();
        public List<LinkInfo> Licensors { get; set; } = new List<LinkInfo>();
        //Usually only 1 studio, yet to find a link with two
        public List<LinkInfo> Studios { get; set; } = new List<LinkInfo>();
        public SourceType Source { get; set; }
        public List<LinkInfo> Genres { get; set; } = new List<LinkInfo>();
        public string Duration { get; set; }
        public string AdultyRating { get; set; }

        //Statistics
        public float Score { get; set; }
        public int UsersVoted { get; set; }
        public int Ranked { get; set; }
        public int Popularity { get; set; }
        public int Members { get; set; }
        public int Favorites { get; set; }
    }
}
