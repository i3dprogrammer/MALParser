using MALParser.Dto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.Dto
{
    public class EpisodesPageData
    {
        public BaseAnimeInfo BaseAnimeInfo { get; set; }

        public EpisodesPageData(BaseAnimeInfo baseAnimeInfo)
        {
            this.BaseAnimeInfo = baseAnimeInfo;
        }

        public List<EpisodeInfo> Episodes { get; set; } = new List<EpisodeInfo>();

        public bool NextPageAvailable { get { return NextPageLink != null; } }
        public bool PreviousPageAvailable { get { return PreviousPageLink != null; } }

        internal LinkInfo NextPageLink { get; set; }
        internal LinkInfo PreviousPageLink { get; set; }

        public async Task<EpisodesPageData> ParseNextPageAsync()
        {
            if (NextPageLink == null)
                throw new Exception("Cannot parse next page because it does not exist!");

            return await MALParser.Anime.Episodes.ParseAsync(NextPageLink.Path);
        }

        public EpisodesPageData ParseNextPage()
        {
            if (NextPageLink == null)
                throw new Exception("Cannot parse next page because it does not exist!");

            return MALParser.Anime.Episodes.Parse(NextPageLink.Path);
        }

        public async Task<EpisodesPageData> ParsePreviousPageAsync()
        {
            if (PreviousPageLink == null)
                throw new Exception("Cannot parse previous page because it does not exist!");

            return await MALParser.Anime.Episodes.ParseAsync(PreviousPageLink.Path);
        }

        public EpisodesPageData ParsePreviousPage()
        {
            if (PreviousPageLink == null)
                throw new Exception("Cannot parse previous page because it does not exist!");

            return MALParser.Anime.Episodes.Parse(PreviousPageLink.Path);
        }
    }

    public class EpisodeInfo
    {
        public int Number { get; set; } = -1;
        public LinkInfo WatchLink { get; set; }
        public DateTime AirDate { get; set; }
        public string EnglishTitle { get; set; }
        //public string RomajiTitle { get; set; }
        //public string JapaneseTitle { get; set; }
    }
}
