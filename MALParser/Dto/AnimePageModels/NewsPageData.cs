using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Dto.Utility;

namespace MALParser.Dto.AnimePageModels
{
    public class NewsPageData : BaseAnimeInfoActor
    {
        public NewsPageData(AnimePageHeader baseAnimeInfo) : base(baseAnimeInfo) { }

        public List<NewsInfo> News { get; set; }
    }

    public class NewsInfo
    {
        public LinkInfo NewsBy { get; set; }
        public LinkInfo Title { get; set; }
        public LinkInfo ImageLink { get; set; }
        public string Description { get; set; }
        public DateTime DatePublished { get; set; }
        public LinkInfo DiscussLink { get; set; }
    }
}
