using EmiSoft.CleanArchitecture.Application.Enums;
using EmiSoft.CleanArchitecture.Application.Extensions;

namespace EmiSoft.CleanArchitecture.Application.Exceptions;

public class ApiException : ApplicationException
{
    public int? Status { get; set; }

    public ApiException(HttpResponseStatusType type, string? message = null) : base(message ?? type.GetDescription())
    {
        Status = (int)type;
    }
}
