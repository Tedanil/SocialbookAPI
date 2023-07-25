using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialbookAPI.Application.Consts;
using SocialbookAPI.Application.CustomAttributes;
using SocialbookAPI.Application.Enums;
using SocialbookAPI.Application.Features.Commands.AppUser.AssignRoleToUser;
using SocialbookAPI.Application.Features.Commands.AppUser.CreateUser;
using SocialbookAPI.Application.Features.Commands.AppUser.UpdateAllUsersVoteCounts;
using SocialbookAPI.Application.Features.Commands.AppUser.UpdatePassword;
using SocialbookAPI.Application.Features.Commands.AppUser.UpdateUserInfos;
using SocialbookAPI.Application.Features.Queries.AppUser.GetAllUsers;
using SocialbookAPI.Application.Features.Queries.AppUser.GetRolesToUser;
using SocialbookAPI.Application.Features.Queries.AppUser.GetUserByToken;
using System.Net;

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

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(Menu = AuthorizeDefinitionConstants.Users,
            ActionType = ActionType.Reading, Definition = "Get All Users")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest getAllUsersQueryRequest)
        {
            GetAllUsersQueryResponse response = await _mediator.Send(getAllUsersQueryRequest);
            return Ok(response);
        }

        [HttpGet("get-roles-to-user/{UserId}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Roles To Users", Menu = AuthorizeDefinitionConstants.Users)]
        public async Task<IActionResult> GetRolesToUser([FromRoute] GetRolesToUserQueryRequest getRolesToUserQueryRequest)
        {
            GetRolesToUserQueryResponse response = await _mediator.Send(getRolesToUserQueryRequest);
            return Ok(response);
        }

        [HttpPost("assign-role-to-user")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Assign Role To User", Menu = AuthorizeDefinitionConstants.Users)]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommandRequest assignRoleToUserCommandRequest)
        {
            AssignRoleToUserCommandResponse response = await _mediator.Send(assignRoleToUserCommandRequest);
            return Ok(response);
        }


    }
}
