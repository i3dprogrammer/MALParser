using MALParser.Dto.AnimePageModels;
using MALParser.Dto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.Dto.Utility
{
    public class BaseAnimeInfoActor
    {
        public AnimePageHeader BaseAnimeInfo { get; set; }
        public BaseAnimeInfoActor(AnimePageHeader baseAnimeInfo)
        {
            this.BaseAnimeInfo = baseAnimeInfo;
        }
    }
}
