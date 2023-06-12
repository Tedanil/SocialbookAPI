using MediatR;
using SocialbookAPI.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Features.Commands.AppUser.UpdateAllUsersVoteCounts
{
    public class UpdateAllUsersVoteCountsCommandHandler : IRequestHandler<UpdateAllUsersVoteCountsCommandRequest, UpdateAllUsersVoteCountsCommandResponse>
    {
        readonly IUserService _userService;

        public UpdateAllUsersVoteCountsCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UpdateAllUsersVoteCountsCommandResponse> Handle(UpdateAllUsersVoteCountsCommandRequest request, CancellationToken cancellationToken)
        {
            await _userService.UpdateUserVoteCountsBasedOnLevels();
            return new();
        }
    }
}
