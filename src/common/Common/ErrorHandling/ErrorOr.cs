namespace Common.ErrorHandling;

public class ErrorOr<TData>
{
    public bool Success { get; private set; }
    public string Description { get; private set; }
    public TData Data { get; private set; }

    private ErrorOr(bool success, string description, TData data)
    {
        Success = success;
        Description = description;
        Data = data;
    }

    public static ErrorOr<TData> Ok(TData data, string description = "200 OK")
    {
        return new ErrorOr<TData>(true, description, data);
    }

    public static ErrorOr<TData> BadRequest(string description = "400 Bad Request")
    {
        return new ErrorOr<TData>(false, description, default(TData));
    }

    public static ErrorOr<TData> NotFound(string description = "404 Not Found")
    {
        return new ErrorOr<TData>(false, description, default(TData));
    }

    public static ErrorOr<TData> InternalServerError(string description = "500 Internal Server Error")
    {
        return new ErrorOr<TData>(false, description, default(TData));
    }

    public static implicit operator ErrorOr<TData>(TData data) => new ErrorOr<TData>(true, string.Empty, data);
}
