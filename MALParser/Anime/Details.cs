#define test
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MALParser.Dto.Utility;

namespace MALParser.Anime
{
    public class Details
    {
        private static HttpClient client = new HttpClient();

        public static async Task<Dto.AnimePage> ParseAsync(string link)
        {
            return AnalyzeDocument(await client.GetStringAsync(link));
        }

        public static Dto.AnimePage Parse(string link)
        {
            return AnalyzeDocument(client.GetStringAsync(link).Result);
        }

        private static Dto.AnimePage AnalyzeDocument(string HTMLCode)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTMLCode);

            Dto.AnimePage page = Header.Parse(HTMLCode);

            //Statistics PARSED from header, not needed.
            //Score = float.Parse(doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "fl-l score").InnerText),
            //UsersVoted = Utility.GetIntFromString(doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "fl-l score").GetAttributeValue("data-user", "0")),
            //Ranked = Utility.GetIntFromString(doc.DocumentNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "numbers ranked").InnerText),
            //Popularity = Utility.GetIntFromString(doc.DocumentNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "numbers popularity").InnerText),
            //Members = Utility.GetIntFromString(doc.DocumentNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "numbers members").InnerText)
            page.Description = HtmlEntity.DeEntitize(doc.DocumentNode.Descendants("span").First(x => x.GetAttributeValue("itemprop", "") == "description").InnerText);
            page.PromotionalVideo = new LinkInfo(doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "video-promotion").Descendants("a").First().Attributes["href"].Value);
            
#if tests
            Console.WriteLine(page.Title);
            Console.WriteLine(page.ImageLink.Path);
            Console.WriteLine(page.Description);
            Console.WriteLine(page.PromotionalVideo.Path);
            Console.WriteLine(page.EnglishTitle);
            Console.WriteLine(page.SynonymsTitle);
            Console.WriteLine(page.JapaneseTitle);
            Console.WriteLine(page.Type);
            Console.WriteLine(page.Episodes);
            Console.WriteLine(page.Status);
            Console.WriteLine(page.Aired);
            Console.WriteLine(page.Premiered);
            Console.WriteLine(page.Broadcast);
            Console.WriteLine(page.Source);
            Console.WriteLine(page.Duration);
            Console.WriteLine(page.AdultyRating);
            Console.WriteLine(page.Score);
            Console.WriteLine(page.Ranked);
            Console.WriteLine(page.Popularity);
            Console.WriteLine(page.Members);
            Console.WriteLine(page.Favorites);
#endif
            return page;
        }

    }
}
