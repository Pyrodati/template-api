using Template.Domain.Entities;

namespace Template.Domain.Authentification;

public interface IJwtProvider
{
    string Generate(User user, HashSet<string> permissions);
}
