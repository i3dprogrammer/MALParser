using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MALParser.Dto;

namespace UnitTesting.Anime
{
    [TestClass]
    public class ReviewsUnitTest
    {
        [TestMethod]
        public void AnimeReviewsPage_ParsedCorrectly()
        {
            string testLink = "https://myanimelist.net/anime/5114/Fullmetal_Alchemist__Brotherhood/reviews";
            ReviewsPageData page = MALParser.Anime.Reviews.Parse(testLink);

            Assert.AreEqual(page.Reviews.Count, 20);

            page.Reviews.ForEach(x =>
            {
                Assert.IsNotNull(x.ReviewDescription);
                Assert.IsNotNull(x.Reviewer);

                Assert.IsNotNull(x.EpisodesSeen);
                Assert.IsNotNull(x.AllReviewsByThisGuy);
                Assert.AreNotEqual(x.Date.Ticks, 0);
                Assert.AreNotEqual(x.AnimationRating, -1);
                Assert.AreNotEqual(x.CharacterRating, -1);
                Assert.AreNotEqual(x.EnjoymentRating, -1);
                Assert.AreNotEqual(x.OverallRating, -1);
                Assert.AreNotEqual(x.SoundRating, -1);
                Assert.AreNotEqual(x.StoryRating, -1);
            });
            Assert.AreEqual(page.NextPageAvailable, true);
            Assert.AreEqual(page.PreviousPageAvailable, false);
            
        }
    }
}
