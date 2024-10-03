using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UniversalNotifier.NET.Models
{
    public abstract class NotifierContent : INotifierContent
    {
        public string Content { get; set; }

        public string ParseContent(string template, Dictionary<string, string> templateValues)
        {
            var regexPattern = new Regex("\\%(?<key>\\w+)\\%|\\{\\{(?<key>\\w+)\\}\\}|\\{(?<key>\\w+)\\}");
            var result = regexPattern.Replace(template, match =>
            {
                var key = match.Groups["key"].Value;
                if (templateValues.ContainsKey(key))
                {
                    template = template.Replace(match.Value, templateValues[key]);
                }
                return match.Value;
            });
            return template;
        }
    }
}
