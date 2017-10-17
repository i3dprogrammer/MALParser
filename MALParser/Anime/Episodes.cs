using HtmlAgilityPack;
using MALParser.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.Anime
{
    public class Episodes
    {
        private static HttpClient client = new HttpClient();

        public static async Task<EpisodesPageData> ParseAsync(string link)
        {
            return AnalyzeDocument(await client.GetStringAsync(link), link);
        }

        public static EpisodesPageData Parse(string link)
        {
            return AnalyzeDocument(client.GetStringAsync(link).Result, link);
        }

        private static EpisodesPageData AnalyzeDocument(string HTMLCode, string link)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTMLCode);

            EpisodesPageData page = new EpisodesPageData(Header.Parse(HTMLCode));

            try
            {
                var mainNode = doc.DocumentNode.Descendants("table").First(x => x.GetAttributeValue("class", "") == "mt8 episode_list js-watch-episode-list ascend")
                    .Descendants("tr").Where(x => x.GetAttributeValue("class", "") == "episode-list-data");
                

                for(int i = 0; i < mainNode.Count(); i++)
                {
                    EpisodeInfo ep = new EpisodeInfo();

                    var episodeNum = mainNode.ElementAt(i).Descendants("td").First(x => x.GetAttributeValue("class", "") == "episode-number nowrap");
                    var episodeTitle = mainNode.ElementAt(i).Descendants("td").First(x => x.GetAttributeValue("class", "") == "episode-title");
                    var episodeAired = mainNode.ElementAt(i).Descendants("td").First(x => x.GetAttributeValue("class", "") == "episode-aired nowrap");

                    ep.Number = Utility.GetIntFromString(episodeNum.InnerText);
                    ep.EnglishTitle = HtmlEntity.DeEntitize(episodeTitle.Descendants("a").First().InnerText);
                    ep.WatchLink = new Dto.Utility.LinkInfo(episodeTitle.Descendants("a").First().GetAttributeValue("href", ""));
                    DateTime date;
                    DateTime.TryParse(episodeAired.InnerText, out date);
                    ep.AirDate = date;

                    page.Episodes.Add(ep);
                }

                if(doc.DocumentNode.Descendants().Any(x => x.GetAttributeValue("class", "") == "mt12 mb12"))
                {
                    int currOffset = Utility.GetIntFromString(doc.DocumentNode.Descendants().First(x => x.GetAttributeValue("class", "") == "link current").GetAttributeValue("href", "").Split('=').Last());

                    var links = doc.DocumentNode.Descendants().Where(x => x.GetAttributeValue("class", "") == "link");
                    int minOffset = Utility.GetIntFromString(links.First().GetAttributeValue("href", "").Split('=').Last());
                    int maxOffset = Utility.GetIntFromString(links.Last().GetAttributeValue("href", "").Split('=').Last());

                    string newLink = link.Split('/').Take(link.Split('/').Length - 1).Aggregate((x, y) => x + "/" + y) + "/episode?offset=";
                    
                    if (currOffset > minOffset)
                        page.PreviousPageLink = new Dto.Utility.LinkInfo(newLink + (currOffset-100));
                    if(currOffset < maxOffset)
                        page.NextPageLink = new Dto.Utility.LinkInfo(newLink + (currOffset + 100));

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
