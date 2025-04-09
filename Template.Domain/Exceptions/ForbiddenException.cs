namespace Template.Domain.Exceptions;

public class ForbiddenException(string error) : Exception
{
    public string Error { get; } = error;
}

