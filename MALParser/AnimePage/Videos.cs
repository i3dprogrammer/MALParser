using System;
using MALParser.Dto.AnimePageModels;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Linq;
using MALParser.Dto.Utility;

namespace MALParser.AnimePage
{
    internal static class Videos
    {
        private static HttpClient client = new HttpClient();

        public static async Task<VideosPageData> ParseAsync(string link)
        {
            return AnalyzeDocument(await client.GetStringAsync(link));
        }

        public static VideosPageData Parse(string link)
        {
            return AnalyzeDocument(client.GetStringAsync(link).Result);
        }

        private static VideosPageData AnalyzeDocument(string HTMLCode)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTMLCode);

            VideosPageData page = new VideosPageData(Header.AnalyzeHeader(HTMLCode));

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