using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialbookAPI.Application.Features.Commands.AppUser.CreateUser;
using SocialbookAPI.Application.Features.Commands.AppUser.UpdateAllUsersVoteCounts;
using SocialbookAPI.Application.Features.Commands.AppUser.UpdatePassword;
using SocialbookAPI.Application.Features.Commands.AppUser.UpdateUserInfos;
using SocialbookAPI.Application.Features.Queries.AppUser.GetUserByToken;

namespace SocialbookAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetUser([FromBody] GetUserByTokenQueryRequest getUserByTokenQueryRequest)
        {
            GetUserByTokenQueryResponse response = await _mediator.Send(getUserByTokenQueryRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateUserInfos([FromBody] UpdateUserInfosCommandRequest updateUserInfosCommandRequest )
        {
            UpdateUserInfosCommandResponse response = await _mediator.Send(updateUserInfosCommandRequest);
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> UpdateAllVoteCounts([FromBody] UpdateAllUsersVoteCountsCommandRequest updateAllUsersVoteCountsCommandRequest)
        {
            UpdateAllUsersVoteCountsCommandResponse response = await _mediator.Send(updateAllUsersVoteCountsCommandRequest);
            return Ok(response);
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest updatePasswordCommandRequest)
        {
            UpdatePasswordCommandResponse response = await _mediator.Send(updatePasswordCommandRequest);
            return Ok(response);
        }


    }
}
