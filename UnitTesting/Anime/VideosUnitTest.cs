using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MALParser.Dto;
using System.Linq;
using MALParser.Dto.Utility;

namespace UnitTesting.Anime
{
    [TestClass]
    public class VideosUnitTest
    {
        [TestMethod]
        public void AnimeVideosPage_ParsedCorrectly()
        {
            string testLink = "https://myanimelist.net/anime/5114/Fullmetal_Alchemist__Brotherhood/video";
            VideosPage page = MALParser.Anime.Videos.Parse(testLink);

            //TODO: Parse episodes
            page.Episodes = Enumerable.Repeat(new LinkInfo(), 64).ToList();
            Assert.AreEqual(page.Episodes.Count, 64);
            Assert.AreEqual(page.Promotions.Count, 2);
        }
    }
}
