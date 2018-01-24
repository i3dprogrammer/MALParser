using MALParser.Dto.SearchModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Search;

namespace MALParser.Search
{
    public class AnimeSearchManager
    {
        private AnimeType m_type { get; set; }
        private int m_score { get; set; }
        private AnimeStatus m_status { get; set; }
        private Producers m_producer { get; set; }
        private AnimeAgeRating m_ageRate { get; set; }
        private List<Genres> m_genres { get; set; }
        private string m_searchQuery { get; set; }
        private int m_excludeGenres { get; set; }
        private char m_startLetter { get; set; }
        private bool m_upcoming { get; set; }
        private bool m_justAdded { get; set; }

        private const string m_searchLink = "https://myanimelist.net/anime.php?";

        public AnimeSearchManager()
        {
            m_startLetter = '\\';
            m_score = 0;
            m_excludeGenres = 0;
            m_searchQuery = "";
            m_genres = new List<Genres>();
        }

        public AnimeSearchManager SetAnimeType(AnimeType type)
        {
            m_type = type;
            return this;
        }

        public AnimeSearchManager SetScore(int score)
        {
            m_score = score;
            return this;
        }
        public AnimeSearchManager SetStatus(AnimeStatus status)
        {
            m_status = status;
            return this;
        }

        public AnimeSearchManager SetProducer(Producers producer)
        {
            m_producer = producer;
            return this;
        }

        public AnimeSearchManager SetAgeRating(AnimeAgeRating rate)
        {
            m_ageRate = rate;
            return this;
        }

        public AnimeSearchManager SetSearchQuery(string query)
        {
            m_searchQuery = query;
            return this;
        }

        public AnimeSearchManager SetGenres(params Genres[] genres)
        {
            foreach (var genre in genres)
                m_genres.Add(genre);
            return this;
        }

        public AnimeSearchManager SetExcludeGenres(bool exclude)
        {
            m_excludeGenres = (exclude ? 1 : 0);
            return this;
        }

        public AnimeSearchManager SetStartLetter(char letter)
        {
            m_startLetter = letter;
            return this;
        }

        public AnimeSearchManager SetUpcomingOnly(bool upcoming)
        {
            m_upcoming = upcoming;
            return this;
        }

        public AnimeSearchManager SetJustAddedOnly(bool justAdded)
        {
            m_justAdded = justAdded;
            return this;
        }

        private string GenerateLink()
        {
            string gLink = m_searchLink + $@"q={m_searchQuery}&type={(int)m_type}&score={(int)m_score}&status={(int)m_status}&p={(int)m_producer}&c[]=a&c[]=b&c[]=c&c[]=f&gx={m_excludeGenres}";

            if (m_startLetter != '\\')
                gLink += "&letter=" + m_startLetter;
            if (m_justAdded)
                gLink += "&o=9&cv=2&w=1";
            if (m_upcoming)
                gLink += "&o=2&cv=1&w=0&sd=7&sm=10&sy=2017";

            m_genres.ForEach(x => gLink += "&genre[]=" + (int)x);

            return gLink;
        }

        private bool CheckSettings()
        {
            if (m_type == AnimeType.Any && m_score == 0 && m_status == AnimeStatus.Any && m_producer == Producers._Any && m_ageRate == AnimeAgeRating.Any && m_searchQuery == "" && m_upcoming == false && m_justAdded == false && m_startLetter == '\\')
            {
                if (m_genres.Count == 0)
                    throw new Exception("Cannot start a search because there was no settings selected.");
                else if (m_genres.Count == 1)
                    throw new Exception("This is not a search, this is genre listing. Please use the correct parser.");
                else
                    return true;
            }

            return true;
        }

        public SearchListData Search()
        {
            CheckSettings();

            return AnimeSearchParser.Parse(GenerateLink());
        }

        public async Task<SearchListData> SearchAsync()
        {
            CheckSettings();

            return await AnimeSearchParser.ParseAsync(GenerateLink());
        }

        public void Reset()
        {
            m_type = AnimeType.Any;
            m_score = 0;
            m_status = AnimeStatus.Any;
            m_producer = Producers._Any;
            m_ageRate = AnimeAgeRating.Any;
            m_genres = new List<Genres>();
            m_searchQuery = "";
            m_excludeGenres = 0;
            m_startLetter = '\\';
            m_upcoming = false;
            m_justAdded = false;
        }
    }
}
