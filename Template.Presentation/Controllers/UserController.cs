using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;
using Template.Application.Abstractions;
using Template.Application.Features.User.CreateUser;
using Template.Application.Features.User.GetUserById;
using Template.Application.Features.User.Login;
using Template.Application.Features.User.UpdateUser;
using Template.Application.Features.User.UpdateUserPassword;
using Template.Domain.Shared;
using Template.Infrastructure.Authentification;

namespace Template.Presentation.Controllers;

[Route("users")]
[ApiController]
public sealed class UserController(IUserService userService) : ControllerBase
{
    IUserService _userService = userService;

    [HttpPost("login")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SuccessResponse<LoginResponse>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        BaseResult result = await _userService.LoginAsync(request, cancellationToken);
        return Ok(result.Value);
    }

    [HasPermission(Domain.Enums.RightPermission.USER_READ)]
    [HttpGet("{id}")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SuccessResponse<GetUserByIdResponse>))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserById(int id)
    {
        var result = await _userService.GetUserByIdAsync(id);
        return Ok(result.Value);
    }

    [HasPermission(Domain.Enums.RightPermission.USER_CREATE)]
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {
        await _userService.CreateUserAsync(request);
        return Created();
    }

    [HasPermission(Domain.Enums.RightPermission.USER_UPDATE)]
    [HttpPut("{id}")]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateUser(int id, UpdateUserRequest request)
    {
        await _userService.UpdateUserAsync(id, request);
        return NoContent();
    }

    [HasPermission(Domain.Enums.RightPermission.USER_UPDATE_PASSWORD)]
    [HttpPatch("{id}/password")]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUserPassword(int id, UpdateUserPasswordRequest request)
    {
        await _userService.UpdateUserPasswordAsync(id, request);
        return NoContent();
    }

    [HasPermission(Domain.Enums.RightPermission.USER_DELETE)]
    [HttpDelete("{id}")]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
}
