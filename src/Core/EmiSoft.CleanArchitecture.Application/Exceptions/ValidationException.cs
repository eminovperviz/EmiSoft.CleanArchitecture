using EmiSoft.CleanArchitecture.Application.Enums;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EmiSoft.CleanArchitecture.Application.Exceptions;

public class ValidationException : ApiException
{
    public ValidationException() : base(HttpResponseStatusType.ValidationError)
    {
        Errors = new Dictionary<string, string[]>();
    }
    /// <summary>
    /// for CQRS pipeline 
    /// </summary>
    /// <param name="failures"></param>
    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    /// <summary>
    /// for service oriented architecture
    /// </summary>
    /// <param name="keyValues"></param>
    public ValidationException(ModelStateDictionary keyValues)
       : this()
    {
        Errors = keyValues.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }
}
