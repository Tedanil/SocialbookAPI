using MediatR;
using SocialbookAPI.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Features.Commands.AppUser.UpdateUserInfos
{
    public class UpdateUserInfosCommandHandler : IRequestHandler<UpdateUserInfosCommandRequest, UpdateUserInfosCommandResponse>
    {
        readonly IUserService _userService;

        public UpdateUserInfosCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UpdateUserInfosCommandResponse> Handle(UpdateUserInfosCommandRequest request, CancellationToken cancellationToken)
        {
            await _userService.UpdateUserInfos(request.UserId);
            return new();
        }
    }
}
