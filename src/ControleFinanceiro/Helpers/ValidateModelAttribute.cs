using ControleFinanceiro.Api.Helpers.ValidationModelState;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ControleFinanceiro.Api.Helpers
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new ValidationFailedResult(context.ModelState);
            }
        }
    }
}
