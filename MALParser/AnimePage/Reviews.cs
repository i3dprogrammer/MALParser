using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MALParser.Dto.Utility;
using MALParser.Dto;
using System.Net.Http;
using HtmlAgilityPack;

namespace MALParser.AnimePage
{
    public class Reviews
    {
        private static HttpClient client = new HttpClient();

        public static async Task<ReviewsPageData> ParseAsync(string link)
        {
            return AnalyzeDocument(await client.GetStringAsync(link), link);
        }

        public static ReviewsPageData Parse(string link)
        {
            return AnalyzeDocument(client.GetStringAsync(link).Result, link);
        }

        private static ReviewsPageData AnalyzeDocument(string HTMLCode, string link)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTMLCode);

            ReviewsPageData page = new ReviewsPageData(Header.AnalyzeHeader(HTMLCode));

            //Parse presented reviews
            try
            {
                var reviewNodes = doc.DocumentNode.Descendants("div").Where(x => x.GetAttributeValue("class", "") == "borderDark");
                foreach (var review in reviewNodes)
                {
                    ReviewInfo reviewInfo = new ReviewInfo();
                    //Left part
                    var leftNode = review.Descendants("div").First(x => x.GetAttributeValue("class", "") == "spaceit").Descendants("tr").First();
                    reviewInfo.ImageLink = new LinkInfo(leftNode.Descendants("img").First().GetAttributeValue("src", ""));
                    var reviewerLink = new LinkInfo(leftNode.Descendants("a").First().GetAttributeValue("href", ""));
                    reviewerLink.Name = reviewerLink.Path.Split('/').Last();
                    reviewInfo.AllReviewsByThisGuy = new LinkInfo(leftNode.Descendants("a").Last().GetAttributeValue("href", ""));
                    reviewInfo.PeopleFoundHelpful = Utility.GetIntFromString(leftNode.Descendants("div").First(x => x.GetAttributeValue("class", "")== "lightLink spaceit").InnerText);

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
                    reviewInfo.Reviewer = reviewerLink;
                    reviewInfo.ReviewDescription = description;
                    page.Reviews.Add(reviewInfo);
                }

                if (doc.DocumentNode.Descendants("div").Any(x => x.GetAttributeValue("class", "") == "ml4") && doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "ml4").InnerText != "")
                {
                    string mainLink = link.Split('/').Take(link.Split('/').Length - 1).Aggregate((x, y) => x + "/" + y) + "/";
                    page.PreviousPageLink = new LinkInfo(mainLink + doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "ml4").Descendants("a").First().GetAttributeValue("href", ""));
                    page.NextPageLink = new LinkInfo(mainLink + doc.DocumentNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "ml4").Descendants("a").Last().GetAttributeValue("href", ""));

                    if (page.PreviousPageLink.Path == page.NextPageLink.Path)
                        page.PreviousPageLink = null;
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
