using SocialbookAPI.Application.DTOs.Song;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Abstractions.Services
{
    public interface ISongService
    {
        public Task CreateSongAsync(CreateSong createSong);
    }
}
