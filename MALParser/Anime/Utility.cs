using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MALParser.AnimePage
{
    //TODO: make it internal.
    public class Utility
    {
        internal static int GetIntFromString(string text)
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

        internal static FieldName Classify(string text)
        {
            if (!text.Contains(":"))
                return FieldName.None;
            else
                return (FieldName)Enum.Parse(typeof(FieldName), text.Split(':')[0]);
        }
    }
}
