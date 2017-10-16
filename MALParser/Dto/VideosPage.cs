using MALParser.Dto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.Dto
{
    public class VideosPage
    {
        public BaseAnimeInfo BaseAnimeInfo { get; set; }

        public VideosPage(BaseAnimeInfo baseAnimeInfo)
        {
            this.BaseAnimeInfo = baseAnimeInfo;
        }

        public List<LinkInfo> Episodes { get; set; } = new List<LinkInfo>();
        public List<LinkInfo> Promotions { get; set; } = new List<LinkInfo>();
    }
}
