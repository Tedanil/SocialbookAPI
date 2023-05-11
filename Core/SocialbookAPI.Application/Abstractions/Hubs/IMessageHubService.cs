using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Abstractions.Hubs
{
    public interface IMessageHubService
    {
        Task MessageSentAsync(string message);
    }
}
