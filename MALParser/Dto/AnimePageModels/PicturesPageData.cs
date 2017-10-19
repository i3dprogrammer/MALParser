using MALParser.Dto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.Dto.AnimePageModels
{
    public class PicturesPageData : BaseAnimeInfoActor
    {
        public PicturesPageData(AnimePageHeader baseAnimeInfo) : base(baseAnimeInfo) { }

        public List<LinkInfo> Pictures { get; set; } = new List<LinkInfo>();

    }
}
