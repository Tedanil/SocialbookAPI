using Microsoft.EntityFrameworkCore;
using SocialbookAPI.Application.Abstractions.Services;
using SocialbookAPI.Application.DTOs.Song;
using SocialbookAPI.Application.Repositories;
using SocialbookAPI.Domain.Entities;
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

        public (object, int) GetAllSongs(int page, int size)
        {
            var query = _songReadRepository.GetAll(false);

            IQueryable<Song> songsQuery = null;

            if (page != -1 && size != -1)
                songsQuery = query.Skip(page * size).Take(size);
            else
                songsQuery = query;

            var selectedSongs = songsQuery
              .Select(s => new
              {
                 s.Id,
                 s.VideoId,
                 s.Genre
              })
              .ToList();

            int totalCount = query.Count();

            return (selectedSongs, totalCount);

        }
    }

    
}
