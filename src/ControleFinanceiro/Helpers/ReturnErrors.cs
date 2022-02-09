using ControleFinanceiro.Api.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ControleFinanceiro.Api.Helpers
{
    public static class  ReturnErrors 
    {
        public static ErrorDto Return(dynamic message)
        {
            var error = new ErrorDto { Codigo = message.ErrorCode,  Mensagem = message.Message };
            return error;
        }
    }
}
