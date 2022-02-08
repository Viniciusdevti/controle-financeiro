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

        public IEnumerable<object> Result { get; set; }

    }
}
