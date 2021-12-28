using EmiSoft.CleanArchitecture.Application.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EmiSoft.CleanArchitecture.Application.Filters;

public class GlobalValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            throw new ValidationException(context.ModelState);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }

}
