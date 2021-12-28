using EmiSoft.CleanArchitecture.Application.Exceptions;
using EmiSoft.CleanArchitecture.SharedKernel.Resources;

namespace EmiSoft.CleanArchitecture.Application.Extensions;

public static class AppGuard
{
    public static void ThrowIfNull<T>(T value, Exception? exception = null)
    {
        ArgumentNullException.ThrowIfNull(nameof(value));
        if (exception is not null)
        {
            throw exception;
        }
    }

    public static void ThrowIfNotNull<T>(T value, Exception? exception = null)
    {
        if (value is not null)
            throw new ApiException(Enums.HttpResponseStatusType.ValidationError, string.Format(SharedResources.ObjectNotFound, typeof(T).Name));

        if (exception != null)
        {
            throw exception;
        }
    }

    public static void NotFound<T>(T value, Exception? exception = null)
    {
        if (value is null)
        {
            throw new ApiException(Enums.HttpResponseStatusType.ValidationError, string.Format(SharedResources.ObjectNotFound, typeof(T).Name));
        }
        if (exception is not null)
        {
            throw exception;
        }
    }
}
