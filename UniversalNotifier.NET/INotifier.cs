using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalNotifier.NET.Models;

namespace UniversalNotifier.NET
{
    public interface INotifier<T, C> where T : INotifierReceiver where C : INotifierContent
    {
        NotifierResponse Send(C notificationContent, T receiver);
        Task<NotifierResponse> SendAsync(C notificationContent, T receiver);

        NotifierResponse SendMany(C notifierContent, IEnumerable<T> receivers);
        Task<NotifierResponse> SendManyAsync(C notifierContent, IEnumerable<T> receivers);

    }
}
