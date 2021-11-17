using MagicCrawler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MagicCrawler.Services
{
    public class HtmlParser
    {
        private readonly Regex _regex = new Regex("<div class=\"body\" style=\"(background-image: *(.+?);){1} *(background-size: *(.+?);)?\">");

        public List<Gradient> Parse(string html, string tag)
        {
            var matches = _regex.Matches(html);
            return matches.Select(x => CreateGradient(x, tag)).ToList();
        }

        private Gradient CreateGradient(Match match, string tag)
        {
            var stylesheet = match.Groups[2].Value;
            var size = match.Groups[4].Value;

            var gradient = new Gradient
            {
                Slug = Guid.NewGuid().ToString(),
                Stylesheet = stylesheet,
                Size = !string.IsNullOrWhiteSpace(size) ? size : null,
                Tags = new List<string> { tag }
            };

            return gradient;
        }
    }
}
