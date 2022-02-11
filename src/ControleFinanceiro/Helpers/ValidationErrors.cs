using ControleFinanceiro.Api.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ControleFinanceiro.Api.Helpers
{
    public class ValidationError
    {
        public string Campo { get; }
        public string Mensagem { get; set; }
        public string Codigo { get; }
        public ValidationError(string campo, string mensagem, string codigo)
        {
            Campo = campo;
            Mensagem = mensagem;
            Codigo = codigo;
        }
    }
    public class ValidationResultModel
    {

        public List<object> Erros;

        public ValidationResultModel(ModelStateDictionary modelState)
        {

            var listErrors = modelState.Keys
                      .SelectMany(key => modelState[key].Errors.Select(x =>
                      new ValidationError(
                          key,
                           x.ErrorMessage.Split('#')[0].Trim(),
                           x.ErrorMessage.Split('#')[1]))
                        .ToList());

            Erros = new();
            foreach (var error in listErrors)
            {
                var Errors = new { error.Codigo, error.Mensagem };
                Erros.Add(Errors);
            }
        }
            
        }
    
    }
        

    
    public class ValidationFailedResult : ObjectResult
    {
        public ValidationFailedResult(ModelStateDictionary modelState)
            : base(new ValidationResultModel(modelState))
        {
            StatusCode = 400;
        }
    }

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



