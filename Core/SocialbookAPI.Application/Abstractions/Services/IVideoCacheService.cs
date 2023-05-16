using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Abstractions.Services
{
    public interface IVideoCacheService
    {
        Task<List<string>> GetVideoIdsAsync();

    }
}
