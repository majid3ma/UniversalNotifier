using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalNotifier.NET.Models
{
    public class NotifierResponse
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public bool Success => !Errors.Any();
        public IList<string> Errors { get; set; }
        public NotifierResponse() => Errors = new List<string>();



    }
}
