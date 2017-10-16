using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.Anime
{
    //TODO: make it internal.
    public class Utility
    {
        public static int GetIntFromString(string text)
        {
            int number = 0;
            for(int i = 0; i < text.Length; i++)
            {
                if (char.IsDigit(text[i]))
                {
                    number *= 10;
                    number += text[i] - '0';
                }
            }
            return number;
        }

        public static FieldName Classify(string text)
        {
            if (!text.Contains(":"))
                return FieldName.None;
            else
                return (FieldName)Enum.Parse(typeof(FieldName), text.Split(':')[0]);
        }

        public enum FieldName
        {
            English,
            Synonyms,
            Japanese,
            Type,
            Episodes,
            Status,
            Aired,
            Premiered,
            Broadcast,
            Producers,
            Licensors,
            Studios,
            Source,
            Genres,
            Duration,
            Rating,
            Score,
            Ranked,
            Popularity,
            Members,
            Favorites,
            Adaptation,
            Alternativeversion,
            Sidestory,
            Spinoff,
            Otherlinks,
            Other,
            Sequel,
            Prequel,
            Summary,
            Parentstory,
            None,
        }
    }
}
