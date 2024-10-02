using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UniversalNotifier.NET.Models.EmailModels
{
    public abstract class EmailNotifierContent : INotifierContent
    {
        public EmailNotifierContent() { }
        public EmailNotifierContent(string subject, bool isHtml, string content)
        {
            Subject = subject;
            IsHtml = isHtml;
            Content = content;
        }
        public EmailNotifierContent(string subject, bool isHtml)
        {
            Subject = subject;
            IsHtml = isHtml;
        }
        public virtual string Subject { get; set; }
        public virtual bool IsHtml { get; set; }
        public virtual string Content { get; set; }
        public virtual string ParseContent(string template, Dictionary<string, string> templateValues)
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
            return result;
        }
    }
}
