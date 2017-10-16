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
        public LinkInfo Link_Details { get; set; }
        public LinkInfo Link_Videos { get; set; }
        public LinkInfo Link_Episodes { get; set; }
        public LinkInfo Link_Reviews { get; set; }
        public LinkInfo Link_Recommendations { get; set; }
        public LinkInfo Link_Stats { get; set; }
        public LinkInfo Link_CharactersAndStaff { get; set; }
        public LinkInfo Link_News { get; set; }
        public LinkInfo Link_Forum { get; set; }
        public LinkInfo Link_Featured { get; set; }
        public LinkInfo Link_Clubs { get; set; }
        public LinkInfo Link_Pictures { get; set; }
        public LinkInfo Link_MoreInfo { get; set; }

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
        public string AgeRating { get; set; }

        //Statistics
        public float Score { get; set; } = -1;
        public int UsersVoted { get; set; } = -1;
        public int Ranked { get; set; } = -1;
        public int Popularity { get; set; } = -1;
        public int Members { get; set; } = -1;
        public int Favorites { get; set; } = -1;
    }
}
