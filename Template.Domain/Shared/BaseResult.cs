namespace Template.Domain.Shared;

public abstract class BaseResult
{
    public ResultResponse? Value { get; set; }
}

public class Result : BaseResult
{
    public static Result Success()
    {
        return new Result();
    }

}

public class Result<TData> : BaseResult
{
    public static Result<TData> Success(TData data)
    {
        return new Result<TData>()
        {
            Value = new SuccessResponse<TData>
            {
                Data = data
            }
        };
    }
}

public class Result<TData, TMeta> : BaseResult
{
    public static Result<TData, TMeta> Success(TData data, TMeta meta)
    {
        return new Result<TData, TMeta>()
        {
            Value = new SuccessResponse<TData, TMeta>
            {
                Data = data,
                Meta = meta,
            }
        };
    }
}