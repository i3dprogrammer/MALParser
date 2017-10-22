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
    internal class SearchParser
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
            HtmlNode.ElementsFlags.Remove("option");

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTMLCode);

            SearchListData list = new SearchListData();

            try
            {
                foreach (var animeNode in doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "js-categories-seasonal js-block-list list").Descendants("tr").Skip(1))
                {
                    CoreAnimeEntry info = new CoreAnimeEntry()
                    {
                        ImageLink = new LinkInfo(animeNode.Descendants("td").ElementAt(0).Descendants("img").First().GetAttributeValue("src", "")),
                        Title = HtmlEntity.DeEntitize(animeNode.Descendants("td").ElementAt(1).Descendants("a").First().InnerText),
                        AnimeLink = new LinkInfo(animeNode.Descendants("td").ElementAt(1).Descendants("a").First().GetAttributeValue("href", "")),
                        Synopsis = HtmlEntity.DeEntitize(animeNode.Descendants("td").ElementAt(1).Descendants("div").First(x => x.GetAttributeValue("class", "") == "pt4").InnerText),
                        Type = Utility.FixString(animeNode.Descendants("td").ElementAt(2).InnerText),
                        Episodes = animeNode.Descendants("td").ElementAt(3).InnerText
                    };
                    float.TryParse(animeNode.Descendants("td").ElementAt(4).InnerText, out float score);
                    info.Score = score;
                    info.Members = Utility.GetIntFromString(animeNode.Descendants("td").ElementAt(5).InnerText);
                    list.Animes.Add(info);
                }

                //TODO: Fix Pages
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
                Console.WriteLine(ex.Message + ex.StackTrace + "\n" + link);
            }

            return list;
        }

    }
}

/* Get producers
                System.IO.StreamWriter writer = new System.IO.StreamWriter("enums.txt");
                foreach (var t in doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("id", "") == "advancedSearch").Descendants("tr").ElementAt(3).Descendants("option"))
                {
                    var e = "_" + new String(Utility.FixString(t.InnerText).Replace(" ", "").Where(x => char.IsLetter(x) || char.IsDigit(x)).ToArray()) + " = " + t.GetAttributeValue("value", "") + ",";
                    Console.WriteLine(e);
                    writer.WriteLine(e);
                }
                writer.Close();


  Get Genres
                System.IO.StreamWriter writer = new System.IO.StreamWriter("genres.txt");
                foreach (var t in doc.DocumentNode.Descendants("table").First(x => x.GetAttributeValue("class", "") == "space_table").Descendants("label"))
                {
                    var e = "_" + new String(Utility.FixString(t.InnerText).Replace(" ", "").Where(x => char.IsLetter(x) || char.IsDigit(x)).ToArray()) + " = " + t.GetAttributeValue("for", "").Split('-')[1] + ",";
                    Console.WriteLine(e);
                    writer.WriteLine(e);
                }
                writer.Close();

     */
