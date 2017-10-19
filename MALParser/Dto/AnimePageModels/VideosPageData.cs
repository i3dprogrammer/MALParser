using MALParser.Dto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.Dto.AnimePageModels
{
    public class VideosPageData : BaseAnimeInfoActor
    {
        public VideosPageData(AnimePageHeader baseAnimeInfo) : base(baseAnimeInfo) { }

        /// <summary>
        /// THIS DOESN'T WORK, refer to parsing episode pages instead.
        /// </summary>
        [Obsolete]
        public List<LinkInfo> Episodes { get; set; } = new List<LinkInfo>();
        public List<LinkInfo> Promotions { get; set; } = new List<LinkInfo>();
    }
}
