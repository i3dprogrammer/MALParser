using HtmlAgilityPack;
using MALParser.Dto;
using MALParser.Dto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MALParser
{
    public class Search
    {
        private static HttpClient client = new HttpClient();

        public static async Task<SearchListData> ParseAsync(string link)
        {
            return AnalyzeDocument(await client.GetStringAsync(link), link);
        }

        public static SearchListData Parse(string link)
        {
            return AnalyzeDocument(client.GetStringAsync(link).Result, link);
        }

        private static SearchListData AnalyzeDocument(string HTMLCode, string link)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTMLCode);

            SearchListData list = new SearchListData();

            try
            {
                foreach (var animeNode in doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "js-categories-seasonal js-block-list list").Descendants("tr").Skip(1))
                {
                    CoreAnimeEntry info = new CoreAnimeEntry();
                    info.ImageLink = new LinkInfo(animeNode.Descendants("td").ElementAt(0).Descendants("img").First().GetAttributeValue("src", ""));
                    info.Title = HtmlEntity.DeEntitize(animeNode.Descendants("td").ElementAt(1).Descendants("a").First().InnerText);
                    info.AnimeLink = new LinkInfo(animeNode.Descendants("td").ElementAt(1).Descendants("a").First().GetAttributeValue("href", ""));
                    info.Synopsis = HtmlEntity.DeEntitize(animeNode.Descendants("td").ElementAt(1).Descendants("div").First(x => x.GetAttributeValue("class", "") == "pt4").InnerText);
                    info.Type = animeNode.Descendants("td").ElementAt(2).InnerText;
                    info.Episodes = animeNode.Descendants("td").ElementAt(3).InnerText;
                    float score = -1;
                    float.TryParse(animeNode.Descendants("td").ElementAt(4).InnerText, out score);
                    info.Score = score;
                    info.Members = Utility.GetIntFromString(animeNode.Descendants("td").ElementAt(5).InnerText);
                    Console.WriteLine(info.Title);
                    list.Animes.Add(info);
                }

                if (doc.DocumentNode.Descendants().Any(x => x.GetAttributeValue("class", "") == "ac mt8"))
                {
                    int currOffset = 0;
                    if (link.Contains("&show="))
                        currOffset = Utility.GetIntFromString(link.Split('=').Last());
                    else
                        currOffset = 0;

                    link = link.Replace("&show=" + currOffset, "");

                    if (HtmlEntity.DeEntitize(doc.DocumentNode.Descendants().First(x => x.GetAttributeValue("class", "") == "ac mt8").InnerText).Contains("»"))
                        list.NextPageLink = new LinkInfo(link + "&show=" + (currOffset + 50));
                    if (HtmlEntity.DeEntitize(doc.DocumentNode.Descendants().First(x => x.GetAttributeValue("class", "") == "ac mt8").InnerText).Contains("«"))
                        list.PreviousPageLink = new LinkInfo(link + "&show=" + (currOffset - 50));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace + link);
            }

            return list;
        }

    }
}
