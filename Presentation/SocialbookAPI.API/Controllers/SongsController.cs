using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialbookAPI.Application.Repositories;
using SocialbookAPI.Application.ViewModels.Songs;

namespace SocialbookAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        readonly private ISongWriteRepository _songWriteRepository;
        readonly private ISongReadRepository _songReadRepository;
        public SongsController(ISongWriteRepository songWriteRepository, ISongReadRepository songReadRepository)
        {
            _songWriteRepository = songWriteRepository;
            _songReadRepository = songReadRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_CreateSong Model)
        {
            await _songWriteRepository.AddAsync(new()
            {
                
                VideoId = Model.VideoId,
                Genre = Model.Genre
            });

            await _songWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetVideoIds()
        {
            var songs = await _songReadRepository.GetAll()
                                                  .Select(s => s.VideoId)
                                                  .Take(14)
                                                  .ToListAsync();

            return Ok(songs);
        }

    }
}
