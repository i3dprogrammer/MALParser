using MALParser.Dto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.Dto
{
    public class AnimeListData
    {
        public List<BaseAnimeEntry> Animes { get; set; } = new List<BaseAnimeEntry>();


        public bool NextPageAvailable { get { return NextPageLink != null; } }
        public bool PreviousPageAvailable { get { return PreviousPageLink != null; } }

        internal LinkInfo NextPageLink { get; set; }
        internal LinkInfo PreviousPageLink { get; set; }

        public async Task<AnimeListData> ParseNextPageAsync()
        {
            if (NextPageLink == null)
                throw new Exception("Cannot parse next page because it does not exist!");

            return await AnimeListParser.ParseAsync(NextPageLink.Path);
        }

        public AnimeListData ParseNextPage()
        {
            if (NextPageLink == null)
                throw new Exception("Cannot parse next page because it does not exist!");

            return AnimeListParser.Parse(NextPageLink.Path);
        }

        public async Task<AnimeListData> ParsePreviousPageAsync()
        {
            if (PreviousPageLink == null)
                throw new Exception("Cannot parse previous page because it does not exist!");

            return await AnimeListParser.ParseAsync(NextPageLink.Path);
        }

        public AnimeListData ParsePreviousPage()
        {
            if (PreviousPageLink == null)
                throw new Exception("Cannot parse previous page because it does not exist!");

            return AnimeListParser.Parse(NextPageLink.Path);
        }
    }
}
