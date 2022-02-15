using ControleFinanceiro.Api.Helpers.Error;
using System.Collections.Generic;

namespace ControleFinanceiro.Service.Services
{
   public class ServiceMessage<T>
    {

        public ServiceMessage()
        {
            Successfull = false;
            Mensagem = new ErrorDto();
            Mensagens = new List<ErrorDto>();
        }

        public bool Successfull { get; set; }
        
        public int CodeHttp { get; set; }
        public string CodeError { get; set; }

        public string Errors;

        public ErrorDto Mensagem;
        public List<ErrorDto> Mensagens;
        public List<T> ResultList { get; set; }
        public T Result { get; set; }

        public void AddReturnInternalError(string message)
        {
            Mensagem.Codigo = EnumErrors.ErroInterno.ToString(); ;
            Mensagem.Mensagem = message;
        }

        public void AddReturnBadRequest(string message, string codeErro)
        {
            Mensagens.Add(new ErrorDto
            {
                Codigo = codeErro,
                Mensagem = message
            });
        }
        }
    }

    public class ErrorDto
    {
        public string Codigo { get; set; }
        public string Mensagem { get; set; }
    }
