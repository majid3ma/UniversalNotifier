using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalNotifier.NET.Models
{
    public interface INotifierContent
    {
        string ParseContent(string template, Dictionary<string, string> templateValues);
        string Content { get; set; }

    }
}
