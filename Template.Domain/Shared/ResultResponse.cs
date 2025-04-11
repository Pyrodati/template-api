namespace Template.Domain.Shared;

public abstract class ResultResponse
{
}
public class SuccessResponse<TData> : ResultResponse
{
    public TData? Data { get; set; }
}

public class SuccessResponse<TData, TMeta> : SuccessResponse<TData>
{
    public TMeta? Meta { get; set; }
}
