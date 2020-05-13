using System;
using System.Text;
using System.Text.RegularExpressions;


namespace GradientParser.Services
{
   public class HtmlParser
    {
        public string Parse(string html,string tag)
        {
            var regex = new Regex("<div class=\"body\" style=\"background-image: *(.+?);\">");
            
            System.Threading.Tasks.Task.Delay(System.TimeSpan.FromSeconds(2)).Wait();
            var matches = regex.Matches(html);
            var gradients = new StringBuilder();

            foreach (Match match in matches)
            {
                gradients.AppendLine(FormatGradientLine(match.Groups[1].Value, tag));
            }

            return gradients.ToString();
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
