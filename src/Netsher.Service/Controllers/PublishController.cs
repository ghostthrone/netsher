using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Netsher.Service.Hubs;
using Netsher.Service.Model;

namespace Netsher.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublishController : ControllerBase
    {

        private readonly IHubContext<SubscribeHub> _subscribeHub;

        public PublishController(IHubContext<SubscribeHub> hubContext) =>
            _subscribeHub = hubContext;

        public async Task<IActionResult> Post(PublishData publishData)
        {
            await _subscribeHub.Clients.
                Group(publishData.PublishTo).
                SendAsync("publish", publishData.Data);

            return Ok();
        }
    }
}