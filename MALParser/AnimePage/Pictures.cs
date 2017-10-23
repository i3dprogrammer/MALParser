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
    internal static class Pictures
    {
        private static HttpClient client = new HttpClient();

        public static async Task<PicturesPageData> ParseAsync(string link)
        {
            return AnalyzeDocument(await client.GetStringAsync(link));
        }

        public static PicturesPageData Parse(string link)
        {
            return AnalyzeDocument(client.GetStringAsync(link).Result);
        }

        private static PicturesPageData AnalyzeDocument(string HTMLCode)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTMLCode);

            PicturesPageData page = new PicturesPageData(Header.AnalyzeHeader(HTMLCode));

            try
            {
                var picsNode = doc.DocumentNode.Descendants("div").Where(x => x.GetAttributeValue("class", "") == "picSurround");
                foreach (var node in picsNode)
                    page.Pictures.Add(new LinkInfo(node.Descendants("a").First().GetAttributeValue("href", "")));

            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            return page;

        }
    }
}
