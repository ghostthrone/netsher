using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Netsher.Service.Hubs
{
    public interface ISubscribeHub
    {
        Task SentToGroup(string group, string data);
    }
}
