using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

[AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAttribute : Attribute, IAsyncActionFilter
{
    private const string ApiKeyName = "api-key";
    private const string ApiKey = "aXRhw7o=.";

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var apiKeyHeader = context.HttpContext.Request.Headers[ApiKeyName];
        if (apiKeyHeader != ApiKey)
        {
            context.Result = new ContentResult()
            {
                StatusCode = 401,
                Content =  $"api-key '{apiKeyHeader}' não é valida." 
            };
            return;
        }
         await next();
    }

}
