using MALParser.Dto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.Dto
{
    public class PicturesPageData
    {
        public BaseAnimeInfo BaseAnimeInfo { get; set; }

        public PicturesPageData(BaseAnimeInfo baseAnimeInfo)
        {
            this.BaseAnimeInfo = baseAnimeInfo;
        }

        public List<LinkInfo> Pictures { get; set; } = new List<LinkInfo>();

    }
}
