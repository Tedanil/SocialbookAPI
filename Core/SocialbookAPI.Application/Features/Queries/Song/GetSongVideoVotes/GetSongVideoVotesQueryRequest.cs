using MediatR;

namespace SocialbookAPI.Application.Features.Queries.Song.GetSongVideoVotes
{
    public class GetSongVideoVotesQueryRequest : IRequest<GetSongVideoVotesQueryResponse>
    {
        public string VideoId { get; set; }
    }
}