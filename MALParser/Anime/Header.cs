using HtmlAgilityPack;
using MALParser.Dto.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.Anime
{
    public class Header
    {
        internal static Dto.AnimePage Parse(string HTMLCode)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTMLCode);

            var page = new Dto.AnimePage();

            //Parse links
            try
            {
                var navbarNode = doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("id", "") == "horiznav_nav");
                foreach(var node in navbarNode.Descendants("li"))
                {
                    string str = node.Descendants("a").First().InnerText;
                    string link = node.Descendants("a").First().GetAttributeValue("a", "");
                    Console.WriteLine(str);
                    switch (str)
                    {
                        case "Details":
                            page.DetailsLink = new LinkInfo(link, str);
                            break;
                        case "Videos":
                            page.VideosLink = new LinkInfo(link, str);
                            break;
                        case "Episodes":
                            page.EpisodesLink = new LinkInfo(link, str);
                            break;
                        case "Reviews":
                            page.ReviewsLink = new LinkInfo(link, str);
                            break;
                        case "Recommendations":
                            page.RecommendationsLink = new LinkInfo(link, str);
                            break;
                        case "Stats":
                            page.StatsLink = new LinkInfo(link, str);
                            break;
                        case "Characters & Staff":
                            page.CharactersAndStaffLink = new LinkInfo(link, str);
                            break;
                        case "News":
                            page.NewsLink = new LinkInfo(link, str);
                            break;
                        case "Forum":
                            page.ForumLink = new LinkInfo(link, str);
                            break;
                        case "Featured":
                            page.FeaturedLink = new LinkInfo(link, str);
                            break;
                        case "Clubs":
                            page.ClubsLink = new LinkInfo(link, str);
                            break;
                        case "Pictures":
                            page.PicturesLink = new LinkInfo(link, str);
                            break;
                        case "More info":
                            page.MoreInfoLink = new LinkInfo(link, str);
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
                page.Score = float.Parse(doc.DocumentNode.Descendants("span").First(x => x.GetAttributeValue("itemprop", "") == "ratingValue").InnerText);
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
                            foreach (var item in node.Descendants().Where(x => x.Name == "a"))
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
                            page.Source = (SourceType)Enum.Parse(typeof(SourceType), val.Replace(" ", ""));
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
            return page;
        }
    }
}
