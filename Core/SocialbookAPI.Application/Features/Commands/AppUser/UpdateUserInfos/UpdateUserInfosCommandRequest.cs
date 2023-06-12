using MediatR;

namespace SocialbookAPI.Application.Features.Commands.AppUser.UpdateUserInfos
{
    public class UpdateUserInfosCommandRequest :IRequest<UpdateUserInfosCommandResponse>
    {
        public string UserId { get; set; }
    }
}