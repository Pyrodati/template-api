using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Template.Application.Abstractions;
using Template.Application.Features.Role.CreateRole;
using Template.Application.Features.Role.GetPermissionsByRoleId;
using Template.Application.Features.Role.GetRoleById;
using Template.Application.Features.Role.UpdateRole;
using Template.Application.Features.Role.UpdateRolePermissions;
using Template.Domain.Shared;
using Template.Infrastructure.Authentification;

namespace Template.Presentation.Controllers;

[Route("roles")]
[ApiController]
public sealed class RoleController(IRoleService roleService) : ControllerBase
{
    IRoleService _roleService = roleService;

    [HasPermission(Domain.Enums.RightPermission.ROLE_READ)]
    [HttpGet("{id}")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SuccessResponse<GetRoleByIdResponse>))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetRoleById(int id)
    {
        var result = await _roleService.GetRoleByIdAsync(id);
        return Ok(result.Value);
    }

    [HasPermission(Domain.Enums.RightPermission.ROLE_READ)]
    [HttpGet("{id}/permissions")]
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(SuccessResponse<GetPermissionsByRoleIdResponse>))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetPermissionsByRoleId(int id)
    {
        var result = await _roleService.GetPermissionsByRoleIdAsync(id);
        return Ok(result.Value);
    }

    [HasPermission(Domain.Enums.RightPermission.ROLE_CREATE)]
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status409Conflict)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateRole(CreateRoleRequest request)
    {
        await _roleService.CreateRoleAsync(request);
        return NoContent();
    }

    [HasPermission(Domain.Enums.RightPermission.ROLE_UPDATE)]
    [HttpPut("{id}")]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status409Conflict)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateRole(int id, UpdateRoleRequest request)
    {
        await _roleService.UpdateRoleAsync(id, request);
        return NoContent();
    }

    [HasPermission(Domain.Enums.RightPermission.ROLE_UPDATE)]
    [HttpPut("{id}/permissions")]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateRolePermissions(int id, UpdateRolePermissionsRequest request)
    {
        await _roleService.UpdateRolePermissionsAsync(id, request);
        return NoContent();
    }

    [HasPermission(Domain.Enums.RightPermission.ROLE_DELETE)]
    [HttpDelete("{id}")]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status401Unauthorized)]
    [SwaggerResponse(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteRole(int id)
    {
        await _roleService.DeleteRoleAsync(id);
        return NoContent();
    }
}
