using MALParser.Dto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.Dto
{
    public class VideosPageData
    {
        public BaseAnimeInfo BaseAnimeInfo { get; set; }

        public VideosPageData(BaseAnimeInfo baseAnimeInfo)
        {
            this.BaseAnimeInfo = baseAnimeInfo;
        }

        /// <summary>
        /// THIS DOESN'T WORK, refer to parsing episode pages instead.
        /// </summary>
        public List<LinkInfo> Episodes { get; set; } = new List<LinkInfo>();
        public List<LinkInfo> Promotions { get; set; } = new List<LinkInfo>();
    }
}
