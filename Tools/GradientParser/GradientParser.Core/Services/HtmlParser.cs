using System;
using System.Text;
using System.Text.RegularExpressions;


namespace GradientParser.Services
{
   public class HtmlParser
    {
        public string Parse(string html, string tag)
        {
            var regex = new Regex("<div class=\"body\" style=\"(background-image: *(.+?);){1} *(background-size: *(.+?);)?\">");
            
            var matches = regex.Matches(html);
            var gradients = new StringBuilder();

            gradients.AppendLine("[");

            foreach (Match match in matches)
            {
                gradients.AppendLine(FormatGradientLine(match.Groups[1].Value, tag)+",");
            }

            var gradientsString = gradients.ToString();

            return gradientsString.Remove(gradientsString.Length - 3) + Environment.NewLine + "]";
        }

        private string FormatGradientLine(string gradient, string tag) => Newtonsoft.Json.JsonConvert.SerializeObject(new
        {
            slug = Guid.NewGuid(),
            stylesheet = gradient,
            tags = new[]
            {
                tag
            }
        });

    }
}
