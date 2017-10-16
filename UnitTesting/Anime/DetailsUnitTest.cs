using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MALParser.Dto;

namespace UnitTesting.Anime
{
    [TestClass]
    public class DetailsUnitTest
    {
        [TestMethod]
        public void AnimeDetailsPage_ParsedCorrectly()
        {
            string testLink = "https://myanimelist.net/anime/5114/Fullmetal_Alchemist__Brotherhood/";
            AnimePage testPage = MALParser.Anime.Details.Parse(testLink);

            Assert.IsNotNull(testPage.PromotionalVideo);
            Assert.IsNotNull(testPage.Description);

            //TODO: Parse background.
            testPage.Background = "";
            Assert.IsNotNull(testPage.Background);
            Assert.AreEqual(testPage.Adaptation.Count, 1);
            Assert.AreEqual(testPage.AlternativeVersion.Count, 1);
            Assert.AreEqual(testPage.SideStory.Count, 2);
            Assert.AreEqual(testPage.SpinOff.Count, 1);

            Assert.AreEqual(testPage.PresentedCharacters.Count, 10);
            Assert.AreEqual(testPage.PresentedStaff.Count, 4);

            Assert.AreEqual(testPage.OpeningTheme.Count, 5);
            Assert.AreEqual(testPage.EndingTheme.Count, 7);

            Assert.AreEqual(testPage.PresentedReviews.Count, 4);
            Assert.AreEqual(testPage.PresentedRecommendations.Count, 24);
        }
    }
}
