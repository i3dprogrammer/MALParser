﻿using HtmlAgilityPack;
using MALParser.Dto.AnimePageModels;
using MALParser.Dto.Utility;
using MALParser.Dto.ListModels;
using MALParser.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.Lists
{
    internal static class AnimeListParser
    {
        private static HttpClient client = new HttpClient();
       

        public static async Task<AnimeListData> ParseAsync(string link)
        {
            return AnalyzeDocument(await client.GetStringAsync(link), link);
        }

        public static AnimeListData Parse(string link)
        {
            return AnalyzeDocument(client.GetStringAsync(link).Result, link);
        }

        private static AnimeListData AnalyzeDocument(string HTMLCode, string link)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(HTMLCode);

            AnimeListData list = new AnimeListData();

            try
            {
                foreach (var animeNode in doc.DocumentNode.Descendants("div").Where(x => x.GetAttributeValue("class", "") == "seasonal-anime js-seasonal-anime"))
                {
                    CoreAnimeEntry page = new CoreAnimeEntry()
                    {
                        Title = Utility.FixString(HtmlEntity.DeEntitize(animeNode.Descendants("p").First(x => x.GetAttributeValue("class", "") == "title-text").InnerText)),
                        AnimeLink = new LinkInfo(animeNode.Descendants("p").First(x => x.GetAttributeValue("class", "") == "title-text").Descendants("a").First().GetAttributeValue("href", "")),
                        Episodes = Utility.FixString(HtmlEntity.DeEntitize(animeNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "eps").InnerText)),
                        ImageLink = new LinkInfo(animeNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "image").Descendants().First().GetAttributeValue("src", "")),
                        Synopsis = HtmlEntity.DeEntitize(animeNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "synopsis js-synopsis").InnerText),
                    };

                    if (animeNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "source").InnerText.Length > 1)
                        page.Source = (AnimeSourceType)Enum.Parse(typeof(AnimeSourceType), Utility.FixEnum(animeNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "source").InnerText));
                    LinkInfo prod = new LinkInfo()
                    {
                        Name = Utility.FixString(HtmlEntity.DeEntitize(animeNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "producer").InnerText))
                    };

                    if (animeNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "producer").Descendants("a").Any())
                        prod.Path = Utility.GetCorrectLinkFormat(animeNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "producer").Descendants("a").First().GetAttributeValue("href", ""));
                    page.Studios.Add(prod);
                    animeNode.Descendants("span").Where(x => x.GetAttributeValue("span", "") == "genre").ToList().ForEach(x =>
                    {
                        page.Genres.Add(Utility.FixString(x.InnerText));
                    });
                    
                    float.TryParse(animeNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "score").InnerText, out float score);
                    page.Members = Utility.GetIntFromString(animeNode.Descendants("span").First(x => x.GetAttributeValue("class", "") == "member fl-r").InnerText);

                    string[] str = HtmlEntity.DeEntitize(animeNode.Descendants("div").First(x => x.GetAttributeValue("class", "") == "info").InnerText).Split('-');
                    page.Type = Utility.FixString(str[0].Replace(" ", ""));
                    page.Aired = Utility.FixString(str[1].Split(',').Take(2).Aggregate((x, y) => x + ", " + y));
                    if (str[1].Split(',').Length > 2)
                        page.Broadcast = Utility.FixString(str[1].Split(',')[2]);
                    
                    list.Animes.Add(page);
                }

                if (doc.DocumentNode.Descendants().Any(x => x.GetAttributeValue("class", "") == "mt12 mb12"))
                {
                    int currOffset = Utility.GetIntFromString(doc.DocumentNode.Descendants().First(x => x.GetAttributeValue("class", "") == "link current").GetAttributeValue("href", "").Split('=').Last());

                    var links = doc.DocumentNode.Descendants().Where(x => x.GetAttributeValue("class", "") == "link");
                    int minOffset = Utility.GetIntFromString(links.First().GetAttributeValue("href", "").Split('=').Last());
                    int maxOffset = Utility.GetIntFromString(links.Last().GetAttributeValue("href", "").Split('=').Last());

                    string newLink = "";
                    if (link.Contains("?page="))
                        newLink = link.Split('=')[0] + "=";
                    else
                        newLink = link + "?page=";

                    if (currOffset > minOffset)
                        list.PreviousPageLink = new LinkInfo(newLink + (currOffset - 1));
                    if (currOffset < maxOffset)
                        list.NextPageLink = new LinkInfo(newLink + (currOffset + 1));

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace + link);
            }

            return list;
        }
    }
}
