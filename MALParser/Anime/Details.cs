#define tests
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

            Dto.AnimePage page = new Dto.AnimePage();
            page.Title = HtmlEntity.DeEntitize(doc.DocumentNode.Descendants("span").First(x => x.GetAttributeValue("itemprop", "") == "name").InnerText);
            page.ImageLink = new LinkInfo(doc.DocumentNode.Descendants("img").First(x => x.GetAttributeValue("itemprop", "") == "image").Attributes["src"].Value);
            //page.Description = HtmlEntity.DeEntitize(doc.DocumentNode.Descendants("span").First(x => x.GetAttributeValue("itemprop", "") == "description").InnerText);
            page.Score = float.Parse(doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "fl-l score").InnerText);
            page.UsersVoted = Utility.GetIntFromString(doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "fl-l score").GetAttributeValue("data-user", "0"));
            page.Ranked = Utility.GetIntFromString(doc.DocumentNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "numbers ranked").InnerText);
            page.Popularity = Utility.GetIntFromString(doc.DocumentNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "numbers popularity").InnerText);
            page.Members = Utility.GetIntFromString(doc.DocumentNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "numbers members").InnerText);
            //page.PromotionalVideo = new LinkInfo(doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "video-promotion").Descendants("a").First().Attributes["href"].Value);

            HtmlNode startDivNode = doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "spaceit_pad");
            int start = doc.DocumentNode.Descendants("div").TakeWhile(x => x != startDivNode).Count();
            while (true)
            {
                HtmlNode node = doc.DocumentNode.Descendants("div").ElementAt(start++);
                string str = HtmlEntity.DeEntitize(node.InnerText);
                string val;
                if (!str.Contains(":"))
                    continue;
                else
                    val = str.Split(':').Skip(1).Aggregate((x, y) => x + ":" + y).Trim();
                
                var classified = Utility.Classify(str);

                switch (classified)
                {
                    case Utility.FieldName.English:
                        page.EnglishTitle = val;
                        break;
                    case Utility.FieldName.Synonyms:
                        page.SynonymsTitle = val;
                        break;
                    case Utility.FieldName.Japanese:
                        page.JapaneseTitle = val;
                        break;
                    case Utility.FieldName.Type:
                        page.Type = val;
                        break;
                    case Utility.FieldName.Episodes:
                        page.Episodes = val;
                        break;
                    case Utility.FieldName.Status:
                        page.Status = val;
                        break;
                    case Utility.FieldName.Aired:
                        page.Aired = val;
                        break;
                    case Utility.FieldName.Premiered:
                        page.Premiered = val;
                        break;
                    case Utility.FieldName.Broadcast:
                        page.Broadcast = val;
                        break;
                    case Utility.FieldName.Producers:
                        foreach(var item in node.Descendants().Where(x => x.Name == "a"))
                            page.Producers.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                        break;
                    case Utility.FieldName.Licensors:
                        foreach (var item in node.Descendants().Where(x => x.Name == "a"))
                            page.Licensors.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                        break;
                    case Utility.FieldName.Studios:
                        foreach (var item in node.Descendants().Where(x => x.Name == "a"))
                            page.Studios.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                        break;
                    case Utility.FieldName.Source:
                        page.Source = (SourceType)Enum.Parse(typeof(SourceType), val);
                        break;
                    case Utility.FieldName.Genres:
                        foreach (var item in node.Descendants().Where(x => x.Name == "a"))
                            page.Genres.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                        break;
                    case Utility.FieldName.Duration:
                        page.Duration = val;
                        break;
                    case Utility.FieldName.Rating:
                        page.AdultyRating = val;
                        break;
                    case Utility.FieldName.Score:
                        page.Score = Utility.GetIntFromString(val);
                        break;
                    case Utility.FieldName.Ranked:
                        page.Ranked = Utility.GetIntFromString(val);
                        break;
                    case Utility.FieldName.Popularity:
                        page.Popularity = Utility.GetIntFromString(val);
                        break;
                    case Utility.FieldName.Members:
                        page.Members = Utility.GetIntFromString(val);
                        break;
                    case Utility.FieldName.Favorites:
                        page.Favorites = Utility.GetIntFromString(val);
                        break;
                }

                if (classified == Utility.FieldName.Favorites)
                    break;
            }
#if tests
            Console.WriteLine(page.Title);
            Console.WriteLine(page.ImageLink.Path);
            Console.WriteLine(page.Description);
            //Console.WriteLine(page.Score);
            //Console.WriteLine(page.UsersVoted);
            //Console.WriteLine(page.Ranked);
            //Console.WriteLine(page.Popularity);
            //Console.WriteLine(page.Members);
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
