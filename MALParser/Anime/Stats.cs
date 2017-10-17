using HtmlAgilityPack;
using MALParser.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.AnimePage
{
    public class Stats
    {
        private static HttpClient client = new HttpClient();

        public static async Task<StatsPageData> ParseAsync(string link)
        {
            return AnalyzeDocument(await client.GetStringAsync(link));
        }

        public static StatsPageData Parse(string link)
        {
            return AnalyzeDocument(client.GetStringAsync(link).Result);
        }

        private static StatsPageData AnalyzeDocument(string HTMLCode)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTMLCode);

            StatsPageData page = new StatsPageData(Header.Parse(HTMLCode));

            try
            {
                var mainNode = doc.DocumentNode.Descendants().First(x => x.GetAttributeValue("class", "") == "js-scrollfix-bottom-rel");
                foreach(var spaceit_pad in mainNode.ChildNodes.Where(x => x.GetAttributeValue("class", "") == "spaceit_pad"))
                {
                    var text = spaceit_pad.InnerText.Replace(" ", "").Replace("-", "");
                    var summaryType = (AnimeSummaryStats)Enum.Parse(typeof(AnimeSummaryStats), text.Split(':')[0]);
                    page.SummaryStats.Add(summaryType, Utility.GetIntFromString(text.Split(':')[1]));
                }

                mainNode = mainNode.ChildNodes.First(x => x.Name == "table");
                foreach (var tr in mainNode.Descendants("tr"))
                {
                    var text = tr.Descendants("small").First().InnerText;
                    var index = tr.Descendants("td").First().InnerText;
                    page.ScoreStats.Add(Utility.GetIntFromString(index), Utility.GetIntFromString(text));
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
