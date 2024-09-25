using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalNotifier.NET.Models;
using UniversalNotifier.NET.Models.EmailModels;

namespace UniversalNotifier.NET
{
    public abstract class EmailNotifier : INotifier<EmailNotifierReceiver, EmailNotifierContent>
    {
        protected abstract string FromAccount { get; set; }
        public virtual NotifierResponse Send(EmailNotifierContent notificationContent, EmailNotifierReceiver receiver) => SendAsync(notificationContent, receiver).Result;
        public abstract Task<NotifierResponse> SendAsync(EmailNotifierContent notificationContent, EmailNotifierReceiver receiver);

        public virtual NotifierResponse SendMany(EmailNotifierContent notifierContent, IEnumerable<EmailNotifierReceiver> receivers) => SendManyAsync(notifierContent, receivers).Result;

        public abstract Task<NotifierResponse> SendManyAsync(EmailNotifierContent notifierContent, IEnumerable<EmailNotifierReceiver> receivers);
    }
}
