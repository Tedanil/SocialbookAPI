using Microsoft.AspNetCore.SignalR;
using SocialbookAPI.Application.Abstractions.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.SignalR.Hubs
{
    public  class MessageHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync(ReceiveFunctionNames.MessageSent, message);
        }
    }
}
