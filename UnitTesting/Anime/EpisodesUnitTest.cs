using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MALParser.Dto;

namespace UnitTesting.Anime
{
    [TestClass]
    public class EpisodesUnitTest
    {
        [TestMethod]
        public void AnimeEpisodesPage_ParsedCorrectly()
        {
            string testLink = "https://myanimelist.net/anime/21/One_Piece/episode";
            EpisodesPageData page = MALParser.AnimePage.Episodes.Parse(testLink);

            Assert.AreEqual(page.NextPageAvailable, true);
            Assert.AreEqual(page.PreviousPageAvailable, false);

            Assert.AreNotEqual(page.Episodes.Count, 0);

            page.Episodes.ForEach(x =>
            {
                Assert.IsNotNull(x.EnglishTitle);
                Assert.IsNotNull(x.WatchLink);
                Assert.AreNotEqual(x.Number, -1);
            });
        }
    }
}
