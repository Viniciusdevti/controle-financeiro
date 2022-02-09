using ControleFinanceiro.Api.Helpers.Error;
using System.Collections.Generic;

namespace ControleFinanceiro.Service.Services
{
    public class ServiceMessage
    {

        public ServiceMessage()
        {
            Successfull = false;
            Message = "";

        }

        public bool Successfull { get; set; }
        public string Message { get; set; }
        public int CodeHttp { get; set; }
        public string CodeError { get; set; }

        public string Errors;

        public IEnumerable<object> ResultList { get; set; }
        public object Result { get; set; }

        public void AddReturnInternalError(string message)
        {
           CodeError = EnumErrors.InternalError.ToString();
           Message = message;
           CodeHttp = 500;
        }

        public void AddReturnBadRequest(string message, string codeErro)
        {
            CodeError = codeErro;
            Message = message;
            CodeHttp = 400;
        }
    }
}
