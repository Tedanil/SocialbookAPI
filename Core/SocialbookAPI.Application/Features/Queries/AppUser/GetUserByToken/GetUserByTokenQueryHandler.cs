using MediatR;
using SocialbookAPI.Application.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Features.Queries.AppUser.GetUserByToken
{
    public class GetUserByTokenQueryHandler : IRequestHandler<GetUserByTokenQueryRequest, GetUserByTokenQueryResponse>
    {
        readonly IUserService _userService;

        public GetUserByTokenQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetUserByTokenQueryResponse> Handle(GetUserByTokenQueryRequest request, CancellationToken cancellationToken)
        {
            var data = await _userService.GetUserAsync(request.RefreshToken);

            return new()
            {
                NameSurname = data.NameSurname,
                Username = data.Username,
                Email = data.Email,
                Level = data.Level,
                Exp = data.Exp,
                VoteCount = data.VoteCount,
                Title = data.Title,
                UserId = data.UserId,
                //Role = data.Role
            };

        }
    }
}
