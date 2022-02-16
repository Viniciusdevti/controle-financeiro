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

        public string Codigo { get; set; }

        public string Mensagem { get; }

        public ValidationError(string message, string code)
        {
            Codigo = code;
            Mensagem = message;
        }
    }

    public class ValidationResultModel
    {
        public List<ValidationError> Mensagens { get; }

        public ValidationResultModel(ModelStateDictionary modelState)
        {
            Mensagens = modelState.Keys
                    .SelectMany(key => modelState[key].Errors.Select(x => new ValidationError(
                        x.ErrorMessage.Contains("#") ? x.ErrorMessage.Split('#')[0].Trim() : "NI/NA",
                        x.ErrorMessage.Contains("#") ? x.ErrorMessage.Split('#')[1] : "NI/NA")))
                    .ToList();
        }
    }

   

 
}