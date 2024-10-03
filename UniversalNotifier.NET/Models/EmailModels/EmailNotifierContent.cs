using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UniversalNotifier.NET.Models.EmailModels
{
    public abstract class EmailNotifierContent : NotifierContent
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
    }
}
