using Template.Application.Features.User.GetUserById;
using Template.Domain.Entities;

namespace Template.Application.Mappers;

public static class UserMapper
{
    public static GetUserByIdResponse ToGetUserByIdResponse(this User user)
    {
        return new GetUserByIdResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Password = user.Password,
            IsActive = user.IsActive,
            Roles = user.Roles.Select(role => new GetUserByIdResponse.GetUserByIdRole
            {
                Id = role.Id,
                Name = role.Name
            }).ToList()
        };
    }
}
