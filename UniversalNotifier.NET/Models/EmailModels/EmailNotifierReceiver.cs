using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UniversalNotifier.NET.Models.EmailModels
{
    public class EmailNotifierReceiver : INotifierReceiver
    {
        public string Email { get; set; }
        public string Name { get; set; }

        public EmailNotifierReceiver(string email, string name)
        {
            Email = email;
            Name = name;
        }

        public virtual string ParseReceiver()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return Email;
            }
            return $"{Name} <{Email}>";
        }

        public virtual bool IsValidEmail()
        {
            var pattern = "^([^\x00-\x20\x22\x28\x29\x2c\x2e\x3a-\x3c\x3e\x40\x5b-\x5d\x7f-\xff]+|\x22([^\x0d\x22\x5c\x80-\xff]|\x5c[\x00-\x7f])*\x22)(\x2e([^\x00-\x20\x22\x28\x29\x2c\x2e\x3a-\x3c\x3e\x40\x5b-\x5d\x7f-\xff]+|\x22([^\x0d\x22\x5c\x80-\xff]|\x5c[\x00-\x7f])*\x22))*\x40([^\x00-\x20\x22\x28\x29\x2c\x2e\x3a-\x3c\x3e\x40\x5b-\x5d\x7f-\xff]+|\x5b([^\x0d\x5b-\x5d\x80-\xff]|\x5c[\x00-\x7f])*\x5d)(\x2e([^\x00-\x20\x22\x28\x29\x2c\x2e\x3a-\x3c\x3e\x40\x5b-\x5d\x7f-\xff]+|\x5b([^\x0d\x5b-\x5d\x80-\xff]|\x5c[\x00-\x7f])*\x5d))*$";
            return !string.IsNullOrEmpty(Email) && Regex.IsMatch(Email, pattern);
        }
    }
}
