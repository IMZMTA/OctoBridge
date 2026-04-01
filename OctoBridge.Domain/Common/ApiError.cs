using OctoBridge.Domain.Enums;
using OctoBridge.Domain.Constants.Messages;

namespace OctoBridge.Domain.Common;

public class ApiError
{
    public string Field { get; set; } = Messages.General;
    public string Message { get; set; } = string.Empty;
    public string Code { get; set; } = ErrorCode.InternalServerError.ToString();
    public string Source { get; set; } = ErrorSource.Internal.ToString();
}