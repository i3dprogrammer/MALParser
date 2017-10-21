using MALParser.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MALParser
{
    public class AnimeSearchManager
    {
        public AnimeType Type { get; set; }
        public int Score { get; set; }
        public AnimeStatus Status { get; set; }
        public Producers Producer { get; set; }
        public AnimeAgeRating AgeRate { get; set; }
        public List<EntryGenre> Genres { get; set; }
        public string SearchQuery { get; set; }
        private int m_excludeGenres { get; set; }
        private const string m_searchLink = "https://myanimelist.net/anime.php?";
        public AnimeSearchManager()
        {
            Score = 0;
            m_excludeGenres = 0;
            SearchQuery = "";
            Genres = new List<EntryGenre>();
        }

        public AnimeSearchManager SetType(AnimeType type)
        {
            Type = type;
            return this;
        }

        public AnimeSearchManager SetScore(int score)
        {
            Score = score;
            return this;
        }
        public AnimeSearchManager SetStatus(AnimeStatus status)
        {
            Status = status;
            return this;
        }

        public AnimeSearchManager SetProducer(Producers producer)
        {
            Producer = producer;
            return this;
        }

        public AnimeSearchManager SetAgeRating(AnimeAgeRating rate)
        {
            AgeRate = rate;
            return this;
        }

        public AnimeSearchManager SetSearchQuery(string query)
        {
            SearchQuery = query;
            return this;
        }

        public AnimeSearchManager AddGenres(params EntryGenre[] genres)
        {
            foreach (var genre in genres)
                Genres.Add(genre);
            return this;
        }

        public AnimeSearchManager SetExcludeGenres(bool exclude)
        {
            m_excludeGenres = (exclude ? 1 : 0);
            return this;
        }

        public string GenerateLink()
        {
            string gLink = m_searchLink + $@"q={SearchQuery}&type={(int)Type}&score={(int)Score}&status={(int)Status}&p={(int)Producer}&c[]=a&c[]=b&c[]=c&c[]=f&gx={m_excludeGenres}";
                        
            Genres.ForEach(x => gLink += "&genre[]=" + (int)x);
            return gLink;
        }

        public SearchListData Search()
        {
            return SearchParser.Parse(GenerateLink());
        }

        public async Task<SearchListData> SearchAsync()
        {
            return await SearchParser.ParseAsync(GenerateLink());
        }

    }
}
