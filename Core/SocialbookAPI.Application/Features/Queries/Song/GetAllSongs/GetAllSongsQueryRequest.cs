using MediatR;

namespace SocialbookAPI.Application.Features.Queries.Song.GetAllSongs
{
    public class GetAllSongsQueryRequest : IRequest<GetAllSongsQueryResponse>
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}