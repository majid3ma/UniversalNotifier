using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalNotifier.NET.Models.EmailModels
{
    public abstract class EmailNotifierContent : INotifierContent
    {
        public virtual string Subject { get; set; }
        public virtual bool IsHtml { get; set; }
        public virtual string Content { get; set; }

        public abstract string ParseContent();
    }
}
