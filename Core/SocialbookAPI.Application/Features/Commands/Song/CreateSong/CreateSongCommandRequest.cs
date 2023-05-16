using MediatR;

namespace SocialbookAPI.Application.Features.Commands.Song.CreateSong
{
    public class CreateSongCommandRequest : IRequest<CreateSongCommandResponse>
    {
        public string VideoId { get; set; }
        public string Genre { get; set; }
    }
}