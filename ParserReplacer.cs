using HtmlAgilityPack;
using System.Collections.Generic;

namespace MyExamApp
{
    class ParserReplacer
    {
        public static string HTMLtoText(string textHTML, List<HtmlNode> images)
        {

            if (images.Count > 0)
                foreach (var image in images)
                    textHTML = textHTML.Replace(image.OuterHtml, "<img>");

            return Replace(textHTML);
        }
        public static string Replace(string text)
        {
            var dev_stupid_comm = "<!--<p class=\"left_margin\">Пробовал вот такие варианты<p><img src=\"https://ege.sdamgia.ru/formula/svg/16/16159b458f0a3a6676908523d8a1efeb.svg\" class=\"tex\" style=\"vertical-align:-2pt\" /><p><img src=\"https://ege.sdamgia.ru/formula/svg/46/468dd08e461ce832c1b9500e069b5a73.svg\" class=\"tex\" style=\"vertical-align:-2pt\" /><p><img src=\"https://ege.sdamgia.ru/formula/svg/42/42e43b5bea121befe61d4c41758086c4.svg\" class=\"tex\" style=\"vertical-align:-2pt\" /><p>последний, мне кажется, лучший.<p>&nbsp;</p><p class=\"left_margin\">Интресно, что если написать \\cdot и потом ^\\circ rm{C}, то TeX будет отбивать градус и букву С: <img src=\"https://ege.sdamgia.ru/formula/svg/d9/d96d45d8a3aa3ee4db24af7f16a74cde.svg\" class=\"tex\" style=\"vertical-align:-2pt\" /><p>-->";
            text = text
                 .Replace("&nbsp;", " ")
                .Replace("&shy;", "")
                .Replace("<p class=\"left_margin\">", "</new>")
                .Replace("<center>", "")
                .Replace("<p>", "</new>") // НЕ ТРОГАТЬ!!!!!!!
                .Replace("</p>", " </new>")
                .Replace("<br>", "")
                .Replace("</center>", "")
                .Replace("<span style=\"letter-spacing:2px \">", "")
                .Replace("span style=\"letter-spacing: 2px; \"", "")
                .Replace("</span>", " </new>")
                .Replace("<p align=\"center\">", "")
                .Replace("<span style=\"letter-spaceing:2px\">", "")
                .Replace("<p class=\"left_margin\">", "")
                .Replace("</td></tr></tbody></table>", "</table>")
                .Replace("<table style=\"margin:auto;border:1px\">", "<table>")
                .Replace("<tbody>", "")
                .Replace("</tbody>", "")
                .Replace("<!--auto generated from answers-->", "")
                .Replace("<!--np-->", "")
                .Replace("<!--rule_info-->", "")
                .Replace("<table border=\"0\" width=\"680\" align=\"center\"><tbody><tr><td style=\"border:0\">", "<table>")
                .Replace("<center>", "")
                .Replace("border=\"0", "")
                .Replace("style =\"border:0\"", "")
                .Replace(" style=\"text - align:center\"", "")
                .Replace(" style=\"margin: auto\"", "")
                .Replace(" align =\"center\"", "")
                .Replace(" style=\"letter-spaceing:2px\"", "")
                .Replace("&perp;", "⊥")
                .Replace("&ang;", "∠")
                .Replace("&le;", "≤")
                .Replace("&ge;", "≥")
                .Replace("&#769;", "´")
                .Replace("&mdash;", "-")
                .Replace("<tbody>", "")
                .Replace("</tbody", "")
                .Replace(dev_stupid_comm, "")
                .Replace("&#8211;", "-");
                
            
            return text;
        }
    }
}
