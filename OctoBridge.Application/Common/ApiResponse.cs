namespace OctoBridge.Application.Common;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public List<string>? Messages { get; set; }
    public List<ApiError>? Errors { get; set; }
    public ApiResponse()
    {
        Messages = new List<string>();
        Errors = new List<ApiError>();
    }
    public ApiResponse(bool success, List<string> message, T data, List<ApiError> errors)
    {
        Success = success;
        Messages = message;
        Data = data;
        Errors = errors ?? new List<ApiError>();
    }
}