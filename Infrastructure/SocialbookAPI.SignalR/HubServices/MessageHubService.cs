using Microsoft.AspNetCore.SignalR;
using SocialbookAPI.Application.Abstractions.Hubs;
using SocialbookAPI.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.SignalR.HubServices
{
    public class MessageHubService : IMessageHubService
    {
        readonly IHubContext<MessageHub> _hubContext;

        public MessageHubService(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task MessageSentAsync(string message)
        {
            await _hubContext.Clients.All.SendAsync(ReceiveFunctionNames.MessageSent, message);
        }
    }
}
