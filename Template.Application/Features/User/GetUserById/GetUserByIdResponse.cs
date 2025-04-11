namespace Template.Application.Features.User.GetUserById;

public record GetUserByIdResponse
{
    public int Id { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public bool IsActive { get; init; } = false;
    public List<GetUserByIdRole> Roles { get; init; } = [];
    public record GetUserByIdRole
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}
