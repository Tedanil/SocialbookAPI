﻿using MediatR;

namespace SocialbookAPI.Application.Features.Queries.AppUser.GetAllUsers
{
    public class GetAllUsersQueryRequest : IRequest<GetAllUsersQueryResponse>
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }
}