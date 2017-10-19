using HtmlAgilityPack;
using MALParser.Dto.AnimePageModels;
using MALParser.Dto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.AnimePage
{
    public static class Characters
    {
        private static HttpClient client = new HttpClient();

        public static async Task<CharactersPageData> ParseAsync(string link)
        {
            return AnalyzeDocument(await client.GetStringAsync(link));
        }

        public static CharactersPageData Parse(string link)
        {
            return AnalyzeDocument(client.GetStringAsync(link).Result);
        }

        private static CharactersPageData AnalyzeDocument(string HTMLCode)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTMLCode);

            CharactersPageData page = new CharactersPageData(Header.AnalyzeHeader(HTMLCode));

            //Parse characters
            try
            {
                var charactersNode = doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "js-scrollfix-bottom-rel");
                foreach (var table in charactersNode.ChildNodes.Where(x => x.Name == "table" && x.Descendants("td").Count() != 2))
                {
                    //Left part
                    CharacterInfo charInfo = new CharacterInfo();
                    var charLink = table.Descendants("td").ElementAt(1).Descendants("a").First().GetAttributeValue("href", "");
                    charInfo.CharacterName = table.Descendants("td").ElementAt(1).Descendants("a").First().InnerText.Trim();
                    charInfo.CharacterLink = new LinkInfo(charLink, charInfo.CharacterName);
                    charInfo.CharacterRole = table.Descendants("td").ElementAt(1).Descendants("small").First().InnerText.Trim();
                    charInfo.ImageLink = new LinkInfo(table.Descendants("img").First().GetAttributeValue("src", ""));

                    //Right part
                    charInfo.VoiceOvers = new List<PersonInfo>();
                    foreach (var vo in table.Descendants("tr").First().ChildNodes.Where(x => x.Name == "td").Last().Descendants("tr"))
                    {
                        var person = new PersonInfo();
                        person.Name = vo.Descendants("td").First().Descendants("a").First().InnerText.Trim();
                        person.Link = new LinkInfo(vo.Descendants("td").First().Descendants("a").First().GetAttributeValue("href", ""), person.Name);
                        person.Role = vo.Descendants("td").First().Descendants("small").First().InnerText.Trim();
                        person.ImageLink = new LinkInfo(vo.Descendants("img").First().GetAttributeValue("src", ""));
                        charInfo.VoiceOvers.Add(person);
                    }
                    page.Characters.Add(charInfo);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            //Parse staff
            try
            {
                var staffNode = doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "js-scrollfix-bottom-rel");
                foreach (var table in staffNode.ChildNodes.Where(x => x.Name == "table" && x.Descendants("td").Count() == 2))
                {
                    Dto.AnimePageModels.PersonInfo person = new Dto.AnimePageModels.PersonInfo();
                    person.ImageLink = new LinkInfo(table.Descendants("img").First().GetAttributeValue("src", ""));
                    person.Name = table.Descendants("td").ElementAt(1).Descendants("a").First().InnerText.Trim();
                    person.Link = new LinkInfo(table.Descendants("td").ElementAt(1).Descendants("a").First().GetAttributeValue("href", ""), person.Name);
                    person.Role = table.Descendants("td").ElementAt(1).Descendants("small").First().InnerText.Trim();
                    page.Staff.Add(person);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            return page;
        }
    }
}
