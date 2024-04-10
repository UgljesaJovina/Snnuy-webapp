using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Repositories.Models;
using WebApi.Utils.Attributes;
using Services.DTOs;
using Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService service) {
        userService = service;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
    public async Task<ActionResult<RegistrationResponse>> Register(AuthenticationRequest request) {
        try {
            return Ok(await userService.Register(request));
        } catch (ArgumentException ex) {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(object))]
    public async Task<ActionResult<LoginResponse>> Login(AuthenticationRequest request) {
        try {
            return Ok(await userService.Authenticate(request));
        } catch (KeyNotFoundException ex) {
            return NotFound(new { message = ex.Message });
        } catch (ArgumentException ex) {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("get-my-info")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(object))]
    public async Task<ActionResult<DetailedUserDTO>> GetMyInfo() {
        UserAccount? ua = HttpContext.Items["User"] as UserAccount;
        if (ua is null) return Unauthorized(new { message = "The token you sent is either broken or doesn't exist!" });
        return Ok(await userService.GetById(ua.Id));
    }

    [HttpGet("get-all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ICollection<UserShortObject>> GetAll() {
        return await userService.GetAll();
    }

    [HttpGet("get-by-id/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
    public async Task<ActionResult<DetailedUserDTO>> GetById(Guid userId) {
        try {
            return Ok(await userService.GetById(userId));
        } catch (KeyNotFoundException ex) {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> Update(UpdateAccountRequest request) {
        try {
            request.Id = ((UserAccount)HttpContext.Items["User"]!).Id;
            await userService.Update(request);
            return NoContent();
        } catch (KeyNotFoundException ex) {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpDelete("delete/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize]
    public async Task<IActionResult> Delete(Guid userId) {
        if (((UserAccount)HttpContext.Items["User"]!).Id != userId) return Unauthorized("You are trying to change the account that doesn't belong to you!");
        try {
            await userService.Delete(userId);
            return NoContent();
        } catch (KeyNotFoundException ex) {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpPatch("change-user-permissions/{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(Repositories.Enums.UserPermissions.ChangePermissions)]
    public async Task<IActionResult> ChangePermissions(Guid userId, [FromQuery] int permissions) {
        try {
            await userService.ChangeUserPermissions(userId, permissions);
            return NoContent();
        } catch (KeyNotFoundException ex) {
            return NotFound(new { message = ex.Message });
        }
    }
}
