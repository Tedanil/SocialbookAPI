using MediatR;

namespace SocialbookAPI.Application.Features.Queries.AppUser.GetUserByToken
{
    public class GetUserByTokenQueryRequest : IRequest<GetUserByTokenQueryResponse>
    {
        public string RefreshToken { get; set; }

    }
}