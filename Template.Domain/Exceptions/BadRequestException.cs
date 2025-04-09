namespace Template.Domain.Exceptions;

public class BadRequestException(string error) : Exception
{
    public string Error { get; } = error;
}
