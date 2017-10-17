using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MALParser.Dto;

namespace UnitTesting.Anime
{
    [TestClass]
    public class CharactersUnitTest
    {
        [TestMethod]
        public void AnimeCharactersPage_ParsedCorrectly()
        {
            string testLink = "https://myanimelist.net/anime/5114/Fullmetal_Alchemist__Brotherhood/characters";
            CharactersPageData page = MALParser.AnimePage.Characters.Parse(testLink);
            Assert.AreNotEqual(page.Characters.Count, 0);
            Assert.AreNotEqual(page.Staff.Count, 0);

            page.Characters.ForEach(x =>
            {
                Assert.IsNotNull(x.CharacterLink);
                Assert.IsNotNull(x.CharacterName);
                Assert.IsNotNull(x.CharacterRole);
                Assert.IsNotNull(x.ImageLink);
                x.VoiceOvers.ForEach(y =>
                {
                    Assert.IsNotNull(y.ImageLink);
                    Assert.IsNotNull(y.Link);
                    Assert.IsNotNull(y.Name);
                    Assert.IsNotNull(y.Role);
                });
            });

            page.Staff.ForEach(x =>
            {
                Assert.IsNotNull(x.ImageLink);
                Assert.IsNotNull(x.Link);
                Assert.IsNotNull(x.Name);
                Assert.IsNotNull(x.Role);
            });
        }
    }
}
