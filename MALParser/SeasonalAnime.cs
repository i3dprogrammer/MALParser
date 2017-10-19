using HtmlAgilityPack;
using MALParser.Dto.AnimePageModels;
using MALParser.Dto.Utility;
using MALParser.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MALParser
{
    public static class SeasonalAnime
    {
        private static HttpClient client = new HttpClient();
        public static async Task<List<BaseAnimeInfo>> ParseAsync(string link)
        {
            return AnalyzeDocument(await client.GetStringAsync(link));
        }

        public static List<BaseAnimeInfo> Parse(string link)
        {
            return AnalyzeDocument(client.GetStringAsync(link).Result);
        }

        private static List<BaseAnimeInfo> AnalyzeDocument(string HTMLCode)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTMLCode);

            List<BaseAnimeInfo> list = new List<BaseAnimeInfo>();

            try
            {
                foreach (var animeNode in doc.DocumentNode.Descendants("div").Where(x => x.GetAttributeValue("class", "") == "seasonal-anime js-seasonal-anime"))
                {
                    BaseAnimeInfo page = new BaseAnimeInfo();
                    page.Title = Utility.FixString(HtmlEntity.DeEntitize(animeNode.Descendants("p").First(x => x.GetAttributeValue("class", "") == "title-text").InnerText));
                    page.Episodes = Utility.FixString(HtmlEntity.DeEntitize(animeNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "eps").InnerText));
                    if (animeNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "source").InnerText.Length > 1)
                        page.Source = (SourceType)Enum.Parse(typeof(SourceType), Utility.FixEnum(animeNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "source").InnerText));
                    LinkInfo link = new LinkInfo();
                    link.Name = Utility.FixString(HtmlEntity.DeEntitize(animeNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "producer").InnerText));
                    if(animeNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "producer").Descendants("a").Any())
                        link.Path = Utility.GetCorrectLinkFormat(animeNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "producer").Descendants("a").First().GetAttributeValue("href", ""));
                    page.Studios.Add(link);
                    animeNode.Descendants("span").Where(x => x.GetAttributeValue("span", "") == "genre").ToList().ForEach(x =>
                    {
                        page.Genres.Add(Utility.FixString(x.InnerText));
                    });
                    page.ImageLink = new LinkInfo(animeNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "image").Descendants().First().GetAttributeValue("src", ""));
                    page.Synopsis = HtmlEntity.DeEntitize(animeNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "synopsis js-synopsis").InnerText);
                    float score = -1;
                    float.TryParse(animeNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "score").InnerText, out score);
                    page.Members = Utility.GetIntFromString(animeNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "member fl-r").InnerText);

                    string[] str = HtmlEntity.DeEntitize(animeNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "info").InnerText).Split('-');

                    page.Type = Utility.FixString(str[0].Replace(" ", ""));
                    page.Aired = Utility.FixString(str[1].Split(',').Take(2).Aggregate((x, y) => x + ", " + y));
                    if (str[1].Split(',').Length > 2)
                        page.Broadcast = Utility.FixString(str[1].Split(',')[2]);

                    list.Add(page);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            return list;
        }
    }
}
