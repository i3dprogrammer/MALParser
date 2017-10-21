using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MALParser
{
    internal class Utility
    {
        internal static int GetIntFromString(string text)
        {
            int number = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsDigit(text[i]))
                {
                    number *= 10;
                    number += text[i] - '0';
                }
            }
            return number;
        }

        internal static FieldName ClassifyFieldName(string text)
        {
            if (!text.Contains(":"))
                return FieldName.None;
            else
                return (FieldName)Enum.Parse(typeof(FieldName), text.Split(':')[0]);
        }

        internal static string GetCorrectLinkFormat(string wrongFormat)
        {
            if (wrongFormat.StartsWith("http"))
                return wrongFormat;
            else if (wrongFormat.StartsWith("/"))
            {
                return "https://myanimelist.net" + wrongFormat;
            }
            else
            {
                return ""; //Should get the main link in here, and return the extra, but w/e
            }
        }

        internal static string FixEnum(string text)
        {
            if (text.Contains(" "))
            {
                int captIndex = text.IndexOf(' ') + 1;
                StringBuilder builder = new StringBuilder(text);
                builder[captIndex] = (char)((builder[captIndex] - 'a') + 'A');
                text = builder.ToString();
            }
            return new String(text.Where(char.IsLetter).ToArray());
        }

        internal static string FixString(string text)
        {
            return text.Replace("\n", "").Replace("\r", "").Trim();
        }
    }
}
