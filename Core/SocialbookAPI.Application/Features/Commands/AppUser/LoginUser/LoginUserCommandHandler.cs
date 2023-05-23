using MediatR;
using Microsoft.AspNetCore.Identity;
using SocialbookAPI.Application.Abstractions.Services;
using SocialbookAPI.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password, 3600);
                return new LoginUserSuccessCommandResponse()
                {
                    Token = token
                };
            }
            catch (UserNameOrPasswordFailedException ex)
            {
                return new LoginUserErrorCommandResponse()
                {
                    Message = ex.Message
                };
            }

        }

    }
}
