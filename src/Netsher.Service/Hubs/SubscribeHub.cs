using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Netsher.Service.Hubs
{
    public class SubscribeHub : Hub, ISubscribeHub
    {
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();

            if (!httpContext.Request.Query.ContainsKey("subscribeTo"))
            {
                Context.Abort();
                return;
            }

            var subscribeTo = httpContext.Request.Query["subscribeTo"];
            Context.Items.Add("subscribeTo", subscribeTo);

            await Groups.AddToGroupAsync(Context.ConnectionId, subscribeTo);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Context.Items.TryGetValue("subscribeTo", out object subscribeTo);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, subscribeTo.ToString());
            await base.OnDisconnectedAsync(exception);
        }

        public async Task SentToGroup(string group, string data)
        {
            await Clients.Group(group).SendAsync("publish", data);
        }
    }
}
