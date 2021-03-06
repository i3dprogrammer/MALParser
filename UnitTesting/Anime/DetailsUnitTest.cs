﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MALParser.Dto;
using MALParser.Dto.AnimePageModels;

namespace UnitTesting.Anime
{
    [TestClass]
    public class DetailsUnitTest
    {
        [TestMethod]
        public void AnimeDetailsPage_ParsedCorrectly()
        {
            string testLink = "https://myanimelist.net/anime/5114/Fullmetal_Alchemist__Brotherhood/";
            DetailsPageData testPage = MALParser.AnimePage.Details.Parse(testLink);

            Assert.IsNotNull(testPage.PromotionalVideo);
            Assert.IsNotNull(testPage.Description);

            //TODO: Parse background.
            testPage.Background = "";
            Assert.IsNotNull(testPage.Background);
            Assert.AreEqual(testPage.RelatedAnime[MALParser.RelatedAnime.Adaptation].Count, 1);
            Assert.AreEqual(testPage.RelatedAnime[MALParser.RelatedAnime.Alternativeversion].Count, 1);
            Assert.AreEqual(testPage.RelatedAnime[MALParser.RelatedAnime.Sidestory].Count, 2);
            Assert.AreEqual(testPage.RelatedAnime[MALParser.RelatedAnime.Spinoff].Count, 1);

            Assert.AreEqual(testPage.PresentedCharacters.Count, 10);
            Assert.AreEqual(testPage.PresentedStaff.Count, 4);

            Assert.AreEqual(testPage.OpeningTheme.Count, 5);
            Assert.AreEqual(testPage.EndingTheme.Count, 7);

            Assert.AreEqual(testPage.PresentedReviews.Count, 4);
            Assert.AreEqual(testPage.PresentedRecommendations.Count, 24);
        }
    }
}
