using MALParser.Dto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.Dto
{
    public class PicturesPage
    {
        public BaseAnimeInfo BaseAnimeInfo { get; set; }

        public PicturesPage(BaseAnimeInfo baseAnimeInfo)
        {
            this.BaseAnimeInfo = baseAnimeInfo;
        }

        public List<LinkInfo> Pictures { get; set; } = new List<LinkInfo>();

    }
}
