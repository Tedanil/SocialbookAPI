using SocialbookAPI.Application.Abstractions.Services;
using SocialbookAPI.Application.DTOs.Song;
using SocialbookAPI.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Persistence.Services
{
    public class SongService : ISongService
    {
        readonly ISongReadRepository _songReadRepository;
        readonly ISongWriteRepository _songWriteRepository;

        public SongService(ISongReadRepository songReadRepository, ISongWriteRepository songWriteRepository)
        {
            _songReadRepository = songReadRepository;
            _songWriteRepository = songWriteRepository;
        }

        public async Task CreateSongAsync(CreateSong createSong)
        {
            await _songWriteRepository.AddAsync(new()
            {

                VideoId = createSong.VideoId,
                Genre = createSong.Genre
            });

            await _songWriteRepository.SaveAsync();
            
        }
    }
}
