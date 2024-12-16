using Application.Contracts.Services;
using Application.Models.Dtos.Token;
using Application.Models.Dtos.User.Authenticate;
using Application.Models.Dtos.User.Create;
using Application.Models.Dtos.User.Get;
using Application.Models.Dtos.User.Update;
using Api.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<TokenOutputDto>> Authenticate(UserAuthenticateInputDto input)
        {
            var result = await _userService.Authenticate(input);
            return Ok(result);
        }

        [HttpPost("create")]
        [AllowAnonymous]
        public async Task<ActionResult<UserCreateOutputDto>> CreateUser(UserCreateInputDto input)
        {
            var result = await _userService.CreateUserAsync(input);
            return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
        }

        [HttpPatch("role/{id}")]
        [PermissionsAuthorize("CAN_USER_CHANGE_ROLE")]
        public async Task<ActionResult> ChangeRole(string id, UserChangeRoleInputDto input)
        {
            await _userService.ChangeUserRole(id, input);
            return NoContent();
        }

        [HttpPatch("disable/{id}")]
        [PermissionsAuthorize("CAN_USER_DISABLE")]
        public async Task<ActionResult> DisableUser(string id, string callerEmail)
        {
            await _userService.DisableUser(id, callerEmail);
            return NoContent();
        }

        [HttpPatch("enable/{id}")]
        [PermissionsAuthorize("CAN_USER_ENABLE")]
        public async Task<ActionResult> EnableUser(string id, string callerEmail)
        {
            await _userService.EnableUser(id, callerEmail);
            return NoContent();
        }

        [HttpGet("get/{id}")]
        [PermissionsAuthorize("CAN_USER_GET_BY_ID")]
        public async Task<ActionResult<UserGetOutputDto>> GetUserById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }

        [HttpGet("getByEmail/{email}")]
        [PermissionsAuthorize("CAN_USER_GET_BY_EMAIL")]
        public async Task<ActionResult<UserGetOutputDto>> GetByEmail(string email)
        {
            var user = await _userService.GetByEmail(email);
            return Ok(user);
        }

        [HttpGet("all")]
        [PermissionsAuthorize("CAN_USER_GET_ALL")]
        public async Task<ActionResult<IEnumerable<UserGetOutputDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

    }
}
