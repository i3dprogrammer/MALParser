using MALParser.Dto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Search;

namespace MALParser.Dto.SearchModels
{
    public class SearchListData
    {
        public List<BaseAnimeEntry> Animes { get; set; } = new List<BaseAnimeEntry>();

        public bool NextPageAvailable { get { return NextPageLink != null; } }
        public bool PreviousPageAvailable { get { return PreviousPageLink != null; } }

        internal LinkInfo NextPageLink { get; set; }
        internal LinkInfo PreviousPageLink { get; set; }

        public async Task<SearchListData> ParseNextPageAsync()
        {
            if (NextPageLink == null)
                throw new Exception("Cannot parse next page because it does not exist!");

            return await AnimeSearchParser.ParseAsync(NextPageLink.Path);
        }

        public SearchListData ParseNextPage()
        {
            if (NextPageLink == null)
                throw new Exception("Cannot parse next page because it does not exist!");

            return AnimeSearchParser.Parse(NextPageLink.Path);
        }

        public async Task<SearchListData> ParsePreviousPageAsync()
        {
            if (PreviousPageLink == null)
                throw new Exception("Cannot parse previous page because it does not exist!");

            return await AnimeSearchParser.ParseAsync(NextPageLink.Path);
        }

        public SearchListData ParsePreviousPage()
        {
            if (PreviousPageLink == null)
                throw new Exception("Cannot parse previous page because it does not exist!");

            return AnimeSearchParser.Parse(NextPageLink.Path);
        }
    }
}
