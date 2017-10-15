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

            Dto.AnimePage page = Header.Parse(HTMLCode);

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
                        case Utility.FieldName.Adaptation:
                            foreach (var item in node.Descendants("td").ElementAt(1).Descendants("a"))
                                page.Adaptation.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                            break;
                        case Utility.FieldName.Sequel:
                            foreach (var item in node.Descendants("td").ElementAt(1).Descendants("a"))
                                page.Sequel.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                            break;
                        case Utility.FieldName.Prequel:
                            foreach (var item in node.Descendants("td").ElementAt(1).Descendants("a"))
                                page.Prequel.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                            break;
                        case Utility.FieldName.Alternativeversion:
                            foreach (var item in node.Descendants("td").ElementAt(1).Descendants("a"))
                                page.AlternativeVersion.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                            break;
                        case Utility.FieldName.Sidestory:
                            foreach (var item in node.Descendants("td").ElementAt(1).Descendants("a"))
                                page.SideStory.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                            break;
                        case Utility.FieldName.Spinoff:
                            foreach (var item in node.Descendants("td").ElementAt(1).Descendants("a"))
                                page.SpinOff.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
                            break;
                        case Utility.FieldName.Otherlinks:
                            foreach (var item in node.Descendants("td").ElementAt(1).Descendants("a"))
                                page.OtherLinks.Add(new LinkInfo(item.Attributes["href"].Value, HtmlEntity.DeEntitize(item.InnerText)));
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
                        Dto.CharacterInfo charInfo = new Dto.CharacterInfo();
                        var charLink = table.Descendants("td").ElementAt(1).Descendants("a").First().GetAttributeValue("href", "");
                        charInfo.CharacterName = table.Descendants("td").ElementAt(1).Descendants("a").First().InnerText.Trim();
                        charInfo.CharacterLink = new LinkInfo(charLink, charInfo.CharacterName);
                        charInfo.CharacterRole = table.Descendants("td").ElementAt(1).Descendants("small").First().InnerText.Trim();
                        charInfo.ImageLink = new LinkInfo(table.Descendants("img").First().GetAttributeValue("src", ""));
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
                    Console.WriteLine(person.Name + " -> " + person.Role);
                    page.PresentedStaff.Add(person);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }

#if tests
            //Console.WriteLine(page.Title);
            //Console.WriteLine(page.ImageLink.Path);
            //Console.WriteLine(page.Description);
            //Console.WriteLine(page.PromotionalVideo.Path);
            //Console.WriteLine(page.EnglishTitle);
            //Console.WriteLine(page.SynonymsTitle);
            //Console.WriteLine(page.JapaneseTitle);
            //Console.WriteLine(page.Type);
            //Console.WriteLine(page.Episodes);
            //Console.WriteLine(page.Status);
            //Console.WriteLine(page.Aired);
            //Console.WriteLine(page.Premiered);
            //Console.WriteLine(page.Broadcast);
            //Console.WriteLine(page.Source);
            //Console.WriteLine(page.Duration);
            //Console.WriteLine(page.AdultyRating);
            //Console.WriteLine(page.Score);
            //Console.WriteLine(page.UsersVoted);
            //Console.WriteLine(page.Ranked);
            //Console.WriteLine(page.Popularity);
            //Console.WriteLine(page.Members);
            //Console.WriteLine(page.Favorites);
            //page.Producers.ForEach(x => Console.Write(x.Name + ", "));
            //Console.WriteLine();
            //page.Licensors.ForEach(x => Console.Write(x.Name + ", "));
            //Console.WriteLine();
            //page.Genres.ForEach(x => Console.Write(x.Name + ", "));
            //Console.WriteLine();
            //page.Studios.ForEach(x => Console.Write(x.Name + ", "));
            //Console.WriteLine();
            //page.Adaptation.ForEach(x => Console.Write(x.Name + ", "));
            //Console.WriteLine("#########");
            //page.AlternativeVersion.ForEach(x => Console.Write(x.Name + ", "));
            //Console.WriteLine("#########");
            //page.SideStory.ForEach(x => Console.Write(x.Name + ", "));
            //Console.WriteLine("#########");
            //page.SpinOff.ForEach(x => Console.Write(x.Name + ", "));
            //Console.WriteLine("#########");
#endif
            return page;
        }

    }
}
