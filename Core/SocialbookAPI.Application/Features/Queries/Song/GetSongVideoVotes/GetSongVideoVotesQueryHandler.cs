using MediatR;
using SocialbookAPI.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Features.Queries.Song.GetSongVideoVotes
{
    public class GetSongVideoVotesQueryHandler : IRequestHandler<GetSongVideoVotesQueryRequest, GetSongVideoVotesQueryResponse>
    {
        readonly IVoteService _voteService;

        public GetSongVideoVotesQueryHandler(IVoteService voteService)
        {
            _voteService = voteService;
        }

        public  Task<GetSongVideoVotesQueryResponse> Handle(GetSongVideoVotesQueryRequest request, CancellationToken cancellationToken)
        {
            var votes = _voteService.GetVotes(request.VideoId);
            var response = new GetSongVideoVotesQueryResponse
            {
                Votes = votes,
            };

            return Task.FromResult(response);
        }
    }
}
