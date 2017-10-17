using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MALParser.Dto;

namespace UnitTesting.Anime
{
    [TestClass]
    public class StatsUnitTest
    {
        [TestMethod]
        public void AnimeStatsPage_ParsedCorrectly()
        {
            string testLink = "https://myanimelist.net/anime/5114/Fullmetal_Alchemist__Brotherhood/stats";
            StatsPageData page = MALParser.AnimePage.Stats.Parse(testLink);

            Assert.AreEqual(page.SummaryStats.Count, 6);
            Assert.AreEqual(page.ScoreStats.Count, 10);
        }
    }
}
