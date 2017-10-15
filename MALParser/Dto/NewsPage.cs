﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Dto.Utility;

namespace MALParser.Dto
{
    public class NewsPage : BaseAnimeInfo
    {
        public List<NewsInfo> News { get; set; }
    }

    public class NewsInfo
    {
        public LinkInfo Title { get; set; }
        public LinkInfo ImageLink { get; set; }
        public PersonDescriptionInfo Description { get; set; }
        public DateTime DatePublished { get; set; }
        public LinkInfo DiscussLink { get; set; }
    }
}
