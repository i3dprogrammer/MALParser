using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MALParser.Dto;

namespace UnitTesting.Anime
{
    [TestClass]
    public class HeaderUnitTest
    {
        [TestMethod]
        public void HeaderAnimeInfo_ParsedCorrectly()
        {
            string testLink = "https://myanimelist.net/anime/5114/Fullmetal_Alchemist__Brotherhood/";
            DetailsPageData testPage = MALParser.AnimePage.Details.Parse(testLink);

            //Header Navbar
            Assert.IsNotNull(testPage.BaseAnimeInfo.Link_Details);
            Assert.IsNotNull(testPage.BaseAnimeInfo.Link_Videos);
            Assert.IsNotNull(testPage.BaseAnimeInfo.Link_Episodes);
            Assert.IsNotNull(testPage.BaseAnimeInfo.Link_Reviews);
            Assert.IsNotNull(testPage.BaseAnimeInfo.Link_Recommendations);
            Assert.IsNotNull(testPage.BaseAnimeInfo.Link_Stats);
            Assert.IsNotNull(testPage.BaseAnimeInfo.Link_CharactersAndStaff);
            Assert.IsNotNull(testPage.BaseAnimeInfo.Link_News);
            Assert.IsNotNull(testPage.BaseAnimeInfo.Link_Forum);
            Assert.IsNotNull(testPage.BaseAnimeInfo.Link_Featured);
            Assert.IsNotNull(testPage.BaseAnimeInfo.Link_Clubs);
            Assert.IsNotNull(testPage.BaseAnimeInfo.Link_Pictures);

            //Header Sidebar
            Assert.IsNotNull(testPage.BaseAnimeInfo.Title);
            Assert.IsNotNull(testPage.BaseAnimeInfo.ImageLink);
            Assert.IsNotNull(testPage.BaseAnimeInfo.Type);
            Assert.IsNotNull(testPage.BaseAnimeInfo.Episodes);
            Assert.IsNotNull(testPage.BaseAnimeInfo.Status);
            Assert.IsNotNull(testPage.BaseAnimeInfo.Aired);
            Assert.IsNotNull(testPage.BaseAnimeInfo.Premiered);
            Assert.IsNotNull(testPage.BaseAnimeInfo.Broadcast);
            Assert.IsNotNull(testPage.BaseAnimeInfo.Source);
            Assert.IsNotNull(testPage.BaseAnimeInfo.Duration);
            Assert.IsNotNull(testPage.BaseAnimeInfo.AgeRating);

            //TODO: Parse ranked
            testPage.BaseAnimeInfo.Ranked = 0;
            Assert.AreNotEqual(testPage.BaseAnimeInfo.Score, -1);
            Assert.AreNotEqual(testPage.BaseAnimeInfo.UsersVoted, -1);
            Assert.AreNotEqual(testPage.BaseAnimeInfo.Ranked, -1);
            Assert.AreNotEqual(testPage.BaseAnimeInfo.Popularity, -1);
            Assert.AreNotEqual(testPage.BaseAnimeInfo.Members, -1);
            Assert.AreNotEqual(testPage.BaseAnimeInfo.Favorites, -1);

            Assert.AreEqual(testPage.BaseAnimeInfo.Studios.Count, 1);
            Assert.AreEqual(testPage.BaseAnimeInfo.Genres.Count, 8);
            Assert.AreEqual(testPage.BaseAnimeInfo.Licensors.Count, 2);
            Assert.AreEqual(testPage.BaseAnimeInfo.Producers.Count, 4);
        }
    }
}
