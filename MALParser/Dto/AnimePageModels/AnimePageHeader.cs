using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Dto.Utility;

namespace MALParser.Dto.AnimePageModels
{
    public class AnimePageHeader : BaseAnimeEntry
    {
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

        public string EnglishTitle { get; set; }
        public string SynonymsTitle { get; set; }
        public string JapaneseTitle { get; set; }

        public string Premiered { get; set; }

        public List<LinkInfo> Producers { get; set; } = new List<LinkInfo>();
        public List<LinkInfo> Licensors { get; set; } = new List<LinkInfo>();

        public string Duration { get; set; }
        public string AgeRating { get; set; }

        public int UsersVoted { get; set; } = -1;
        public int Ranked { get; set; } = -1;
        public int Popularity { get; set; } = -1;
        public int Favorites { get; set; } = -1;
    }
}
