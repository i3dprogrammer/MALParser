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

            Dto.AnimePage page = new Dto.AnimePage(Header.Parse(HTMLCode));

            //Statistics PARSED from header, not needed.
            //Score = float.Parse(doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "fl-l score").InnerText),
            //UsersVoted = Utility.GetIntFromString(doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "fl-l score").GetAttributeValue("data-user", "0")),
            //Ranked = Utility.GetIntFromString(doc.DocumentNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "numbers ranked").InnerText),
            //Popularity = Utility.GetIntFromString(doc.DocumentNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "numbers popularity").InnerText),
            //Members = Utility.GetIntFromString(doc.DocumentNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "numbers members").InnerText)

            try
            {
                page.Description = HtmlEntity.DeEntitize(doc.DocumentNode.Descendants("span").First(x => x.GetAttributeValue("itemprop", "") == "description").InnerText);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            try
            {
                page.PromotionalVideo = new LinkInfo(doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "video-promotion").Descendants("a").First().Attributes["href"].Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            try
            {
                //TODO: parse background.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }


            //Parse related anime details
            try
            {
                var tableNode = doc.DocumentNode.Descendants("table").First(x => x.GetAttributeValue("class", "") == "anime_detail_related_anime");
                foreach(var node in tableNode.Descendants("tr"))
                {
                    switch (Utility.Classify(node.InnerText.Replace(" ", "").Replace("-", "")))
                    {
                        case FieldName.Adaptation:
                            foreach (var item in node.Descendants("td").ElementAt(1).Descendants("a"))
                                page.Adaptation.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                            break;
                        case FieldName.Sequel:
                            foreach (var item in node.Descendants("td").ElementAt(1).Descendants("a"))
                                page.Sequel.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                            break;
                        case FieldName.Prequel:
                            foreach (var item in node.Descendants("td").ElementAt(1).Descendants("a"))
                                page.Prequel.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                            break;
                        case FieldName.Alternativeversion:
                            foreach (var item in node.Descendants("td").ElementAt(1).Descendants("a"))
                                page.AlternativeVersion.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                            break;
                        case FieldName.Sidestory:
                            foreach (var item in node.Descendants("td").ElementAt(1).Descendants("a"))
                                page.SideStory.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                            break;
                        case FieldName.Spinoff:
                            foreach (var item in node.Descendants("td").ElementAt(1).Descendants("a"))
                                page.SpinOff.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                            break;
                        case FieldName.Other:
                        case FieldName.Otherlinks:
                            foreach (var item in node.Descendants("td").ElementAt(1).Descendants("a"))
                                page.OtherLinks.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                            break;
                        case FieldName.Parentstory:
                            foreach (var item in node.Descendants("td").ElementAt(1).Descendants("a"))
                                page.ParentStory.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                            break;
                        case FieldName.Summary:
                            foreach (var item in node.Descendants("td").ElementAt(1).Descendants("a"))
                                page.Summary.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                            break;
                        case FieldName.Characters:
                            foreach (var item in node.Descendants("td").ElementAt(1).Descendants("a"))
                                page.Characters.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                            break;
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            //Parse presented characters & voice overs
            try
            {
                var charactersNode = doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "detail-characters-list clearfix");
                foreach(var div in charactersNode.ChildNodes)
                {
                    foreach (var table in div.ChildNodes.Where(x => x.Name == "table"))
                    {
                        //Left part
                        Dto.CharacterInfo charInfo = new Dto.CharacterInfo();
                        var charLink = table.Descendants("td").ElementAt(1).Descendants("a").First().GetAttributeValue("href", "");
                        charInfo.CharacterName = table.Descendants("td").ElementAt(1).Descendants("a").First().InnerText.Trim();
                        charInfo.CharacterLink = new LinkInfo(charLink, charInfo.CharacterName);
                        charInfo.CharacterRole = table.Descendants("td").ElementAt(1).Descendants("small").First().InnerText.Trim();
                        charInfo.ImageLink = new LinkInfo(table.Descendants("img").First().GetAttributeValue("src", ""));

                        //Right part
                        charInfo.VoiceOvers = new List<Dto.PersonInfo>();
                        foreach(var vo in table.Descendants("tr").First().ChildNodes.Where(x => x.Name == "td").Last().Descendants("tr"))
                        {
                            var person = new Dto.PersonInfo();
                            person.Name = vo.Descendants("td").First().Descendants("a").First().InnerText.Trim();
                            person.Link = new LinkInfo(vo.Descendants("td").First().Descendants("a").First().GetAttributeValue("href", ""), person.Name);
                            person.Role = vo.Descendants("td").First().Descendants("small").First().InnerText.Trim();
                            person.ImageLink = new LinkInfo(vo.Descendants("img").First().GetAttributeValue("src", ""));
                            charInfo.VoiceOvers.Add(person);
                        }
                        page.PresentedCharacters.Add(charInfo);
                    }
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            //Parse presented staff
            try
            {
                var staffNode = doc.DocumentNode.Descendants("div").Last(x => x.GetAttributeValue("class", "") == "detail-characters-list clearfix");
                foreach(var table in staffNode.Descendants("table"))
                {
                    Dto.PersonInfo person = new Dto.PersonInfo();
                    person.ImageLink = new LinkInfo(table.Descendants("img").First().GetAttributeValue("src", ""));
                    person.Name = table.Descendants("td").ElementAt(1).Descendants("a").First().InnerText.Trim();
                    person.Link = new LinkInfo(table.Descendants("td").ElementAt(1).Descendants("a").First().GetAttributeValue("href", ""), person.Name);
                    person.Role = table.Descendants("td").ElementAt(1).Descendants("small").First().InnerText.Trim();
                    page.PresentedStaff.Add(person);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            //Parse opening/ending themes
            try
            {
                //Opening
                var openingNode = doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "theme-songs js-theme-songs opnening");
                foreach (var node in openingNode.Descendants("span"))
                    page.OpeningTheme.Add(HtmlEntity.DeEntitize(node.InnerText));

                //Ending
                var endingNode = doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "theme-songs js-theme-songs ending");
                foreach (var node in endingNode.Descendants("span"))
                    page.EndingTheme.Add(HtmlEntity.DeEntitize(node.InnerText));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            //Parse presented reviews
            try
            {
                var reviewNodes = doc.DocumentNode.Descendants("div").Where(x => x.GetAttributeValue("class", "") == "borderDark");
                foreach(var review in reviewNodes)
                {
                    Dto.ReviewInfo reviewInfo = new Dto.ReviewInfo();
                    //Left part
                    var leftNode = review.Descendants("div").First(x => x.GetAttributeValue("class", "") == "spaceit").Descendants("tr").First();
                    reviewInfo.ImageLink = new LinkInfo(leftNode.Descendants("img").First().GetAttributeValue("src", ""));
                    var reviewerLink = new LinkInfo(leftNode.Descendants("a").First().GetAttributeValue("href", ""));
                    reviewerLink.Name = reviewerLink.Path.Split('/').Last();
                    reviewInfo.AllReviewsByThisGuy = new LinkInfo(leftNode.Descendants("a").Last().GetAttributeValue("href", ""));
                    reviewInfo.PeopleFoundHelpful = Utility.GetIntFromString(leftNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "lightLink spaceit").InnerText);

                    //Right part
                    var mb8Node = review.Descendants("div").First(x => x.GetAttributeValue("class", "") == "mb8");
                    reviewInfo.Date = DateTime.Parse(HtmlEntity.DeEntitize(mb8Node.Descendants("div").First().InnerText));
                    reviewInfo.EpisodesSeen = HtmlEntity.DeEntitize(mb8Node.Descendants("div").ElementAt(1).InnerText);

                    //Description and Rating
                    var descriptionNode = review.Descendants("div").First(x => x.GetAttributeValue("class", "") == "spaceit textReadability word-break pt8 mt8");
                    var ratings = descriptionNode.Descendants("tr");
                    reviewInfo.OverallRating = Utility.GetIntFromString(ratings.ElementAt(0).Descendants("td").ElementAt(1).InnerText);
                    reviewInfo.StoryRating = Utility.GetIntFromString(ratings.ElementAt(1).Descendants("td").ElementAt(1).InnerText);
                    reviewInfo.AnimationRating = Utility.GetIntFromString(ratings.ElementAt(2).Descendants("td").ElementAt(1).InnerText);
                    reviewInfo.SoundRating = Utility.GetIntFromString(ratings.ElementAt(3).Descendants("td").ElementAt(1).InnerText);
                    reviewInfo.CharacterRating = Utility.GetIntFromString(ratings.ElementAt(4).Descendants("td").ElementAt(1).InnerText);
                    reviewInfo.EnjoymentRating = Utility.GetIntFromString(ratings.ElementAt(5).Descendants("td").ElementAt(1).InnerText);
                    var description = HtmlEntity.DeEntitize(descriptionNode.InnerText.Replace(descriptionNode.Descendants("div").First().InnerText, "")).Trim();
                    int error = 50;
                    description = description.Remove(description.Length - error, error);
                    reviewInfo.ReviewDescription = description;
                    reviewInfo.Reviewer = reviewerLink;

                    page.PresentedReviews.Add(reviewInfo);
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            //Parse anime recommendations
            try
            {
                var recommendations = doc.DocumentNode.Descendants("ul").First(x => x.GetAttributeValue("class", "") == "anime-slide js-anime-slide");
                foreach(var recommend in recommendations.ChildNodes)
                {
                    string name = HtmlEntity.DeEntitize(recommend.Descendants("span").First(x => x.GetAttributeValue("class", "") == "title fs10").InnerText).Trim();
                    int users = Utility.GetIntFromString(recommend.Descendants("span").First(x => x.GetAttributeValue("class", "") == "users").InnerText);
                    string animeLink = recommend.Descendants("a").First().GetAttributeValue("href", "");
                    string imageLink = recommend.Descendants("img").First().GetAttributeValue("src", "");
                    page.PresentedRecommendations.Add(new Dto.RecommendationInfo()
                    {
                        RecommendationLink = new LinkInfo(animeLink, name),
                        AnimeImageLink = new LinkInfo(imageLink),
                        RecommendedUsers = users,
                    });
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

            return page;
        }

    }
}
