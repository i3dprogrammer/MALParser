using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MALParser.Dto;
using System.Linq;
using MALParser.Dto.Utility;
using MALParser.Dto.AnimePageModels;

namespace UnitTesting.Anime
{
    [TestClass]
    public class VideosUnitTest
    {
        [TestMethod]
        public void AnimeVideosPage_ParsedCorrectly()
        {
            string testLink = "https://myanimelist.net/anime/5114/Fullmetal_Alchemist__Brotherhood/video";
            VideosPageData page = MALParser.AnimePage.Videos.Parse(testLink);


            Assert.AreEqual(page.Promotions.Count, 2);
        }
    }
}
