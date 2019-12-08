using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace BackendCore.Helpers
{
    public class StringToHtmlHelper : IStringToHtmlHelper
    {
        public string GetHtml(string input)
        {
            var compiled =
                input.Replace("&lt;","&amp;&lt;")
                .Replace("&gt;","&amp;&gt;")
                .Replace("&quot;","&amp;&qout;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;");
            
            var regexInstagram = new Regex("(https?://www.)?instagram.com(/p/\\w+/?)");
            var regexYoutube = new Regex("http(?:s?)://(?:www.)?youtu(?:be.com/watch\\?v=|.be/)([\\w\\-_]*)(&(amp;)?‌​[\\w\\?‌​=]*)?(.*)");
            var regexLink = new Regex(@"(http[s]?://(www.)?|ftp://(www.)?|www.){1}([0-9A-Za-z-.@:%_\+~#=]+)+((\.[a-zA-Z]{2,3})+)(/(.)*)?(\?(.)*)?");
            compiled = regexLink.Replace(compiled, new MatchEvaluator((link) => (!regexYoutube.IsMatch(link.Value) && !regexInstagram.IsMatch(link.Value)) ? $"<a href=\"{link.Value}\" target=\"_blank\">{link.Value}</a>" : link.Value));
            var splitID = "assa%%$#34534@hx___4343534swx_s_a_345343texvg4444q_qw_s";
            compiled = regexYoutube.Replace(compiled, new MatchEvaluator((link) => $"<iframe src=\"https://www.youtube.com/embed/{link.Groups[1]}\" />${ splitID }"));
            compiled = regexInstagram.Replace(compiled, new MatchEvaluator((link) => $"<iframe class=\"iframe-instagram\" src=\"{ link}embed\" />${ splitID }"));
            compiled = $"<p>{compiled.Replace("\n", "</p><p>")}</p>";
            return compiled.Replace(splitID, "\n");
        }
    }
}
