using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MALParser.Dto;

namespace UnitTesting.Anime
{
    [TestClass]
    public class PicturesUnitTest
    {
        [TestMethod]
        public void AnimePicturesPage_ParsedCorrectly()
        {
            string testLink = "https://myanimelist.net/anime/5114/Fullmetal_Alchemist__Brotherhood/pics";
            PicturesPageData page = MALParser.AnimePage.Pictures.Parse(testLink);

            Assert.AreEqual(page.Pictures.Count, 8);
            page.Pictures.ForEach(x =>
            {
                Assert.IsNotNull(x.Path);
            });
        }
    }
}
