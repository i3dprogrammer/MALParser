using System;
using MALParser.Dto;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Linq;
using MALParser.Dto.Utility;

namespace MALParser.Anime
{
    public class Videos
    {
        private static HttpClient client = new HttpClient();

        public static async Task<VideosPage> ParseAsync(string link)
        {
            return AnalyzeDocument(await client.GetStringAsync(link));
        }

        public static VideosPage Parse(string link)
        {
            return AnalyzeDocument(client.GetStringAsync(link).Result);
        }

        private static VideosPage AnalyzeDocument(string HTMLCode)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTMLCode);

            VideosPage page = new VideosPage(Header.Parse(HTMLCode));

            try
            {
                foreach (var promo in doc.DocumentNode.Descendants("div").Where(x => x.GetAttributeValue("class", "") == "video-list-outer po-r pv"))
                    page.Promotions.Add(new LinkInfo(promo.Descendants("a").First().GetAttributeValue("href", "")));
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            return page;
        }
    }
}