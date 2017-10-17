using HtmlAgilityPack;
using MALParser.Dto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.AnimePage
{
    internal class Header
    {
        private static HttpClient client = new HttpClient();

        public static async Task<BaseAnimeInfo> ParseAsync(string link)
        {
            return AnalyzeHeader(await client.GetStringAsync(link));
        }

        public static BaseAnimeInfo Parse(string link)
        {
            return AnalyzeHeader(client.GetStringAsync(link).Result);
        }

        public static BaseAnimeInfo AnalyzeHeader(string HTMLCode)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTMLCode);

            var page = new BaseAnimeInfo();

            //Parse links
            try
            {
                var navbarNode = doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("id", "") == "horiznav_nav");
                foreach(var node in navbarNode.Descendants("li"))
                {
                    string str = node.Descendants("a").First().InnerText.Trim();
                    string link = node.Descendants("a").First().GetAttributeValue("href", "");
                    link = Utility.GetCorrectLinkFormat(link);
                    
                    switch (str)
                    {
                        case "Details":
                            page.Link_Details = new LinkInfo(link, str);
                            break;
                        case "Videos":
                            page.Link_Videos = new LinkInfo(link, str);
                            break;
                        case "Episodes":
                            page.Link_Episodes = new LinkInfo(link, str);
                            break;
                        case "Reviews":
                            page.Link_Reviews = new LinkInfo(link, str);
                            break;
                        case "Recommendations":
                            page.Link_Recommendations = new LinkInfo(link, str);
                            break;
                        case "Stats":
                            page.Link_Stats = new LinkInfo(link, str);
                            break;
                        case "Characters & Staff":
                            page.Link_CharactersAndStaff = new LinkInfo(link, str);
                            break;
                        case "News":
                            page.Link_News = new LinkInfo(link, str);
                            break;
                        case "Forum":
                            page.Link_Forum = new LinkInfo(link, str);
                            break;
                        case "Featured":
                            page.Link_Featured = new LinkInfo(link, str);
                            break;
                        case "Clubs":
                            page.Link_Clubs = new LinkInfo(link, str);
                            break;
                        case "Pictures":
                            page.Link_Pictures = new LinkInfo(link, str);
                            break;
                        case "More info":
                            page.Link_MoreInfo = new LinkInfo(link, str);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            try
            {
                page.Title = HtmlEntity.DeEntitize(doc.DocumentNode.Descendants("span").First(x => x.GetAttributeValue("itemprop", "") == "name").InnerText);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            try
            {
                page.ImageLink = new LinkInfo(doc.DocumentNode.Descendants("img").First(x => x.GetAttributeValue("itemprop", "") == "image").Attributes["src"].Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            try
            {
                page.UsersVoted = Utility.GetIntFromString(doc.DocumentNode.Descendants("span").First(x => x.GetAttributeValue("itemprop", "") == "ratingCount").InnerText);
                float score;
                float.TryParse(doc.DocumentNode.Descendants("span").First(x => x.GetAttributeValue("itemprop", "") == "ratingValue").InnerText, out score);
                page.Score = score;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            try
            {
                var node = doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("data-id", "") == "info2").InnerText; //TODO get page.Ranked
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            try
            {
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
                        case FieldName.English:
                            page.EnglishTitle = val;
                            break;
                        case FieldName.Synonyms:
                            page.SynonymsTitle = val;
                            break;
                        case FieldName.Japanese:
                            page.JapaneseTitle = val;
                            break;
                        case FieldName.Type:
                            page.Type = val;
                            break;
                        case FieldName.Episodes:
                            page.Episodes = val;
                            break;
                        case FieldName.Status:
                            page.Status = val;
                            break;
                        case FieldName.Aired:
                            page.Aired = val;
                            break;
                        case FieldName.Premiered:
                            page.Premiered = val;
                            break;
                        case FieldName.Broadcast:
                            page.Broadcast = val;
                            break;
                        case FieldName.Producers:
                            foreach (var item in node.Descendants().Where(x => x.Name == "a"))
                                page.Producers.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                            break;
                        case FieldName.Licensors:
                            foreach (var item in node.Descendants().Where(x => x.Name == "a"))
                                page.Licensors.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                            break;
                        case FieldName.Studios:
                            foreach (var item in node.Descendants().Where(x => x.Name == "a"))
                                page.Studios.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                            break;
                        case FieldName.Source:
                            page.Source = (SourceType)Enum.Parse(typeof(SourceType), val.Replace(" ", ""));
                            break;
                        case FieldName.Genres:
                            foreach (var item in node.Descendants().Where(x => x.Name == "a"))
                                page.Genres.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                            break;
                        case FieldName.Duration:
                            page.Duration = val;
                            break;
                        case FieldName.Rating:
                            page.AgeRating = val;
                            break;
                        case FieldName.Popularity:
                            page.Popularity = Utility.GetIntFromString(val);
                            break;
                        case FieldName.Members:
                            page.Members = Utility.GetIntFromString(val);
                            break;
                        case FieldName.Favorites:
                            page.Favorites = Utility.GetIntFromString(val);
                            break;
                    }

                    if (classified == FieldName.Favorites || classified == FieldName.None)
                        break;
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
