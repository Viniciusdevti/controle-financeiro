using ControleFinanceiro.Api.Helpers.Error;
using ControleFinanceiro.DataContext;
using ControleFinanceiro.DataContext.BaseQuery;
using ControleFinanceiro.DataContext.Models.ModelBase;
using ControleFinanceiro.Model.Models;
using ControleFinanceiro.Repository.Repository;
using ControleFinanceiro.Service.Interfaces;
using System;
using System.Collections.Generic;

namespace ControleFinanceiro.Service.Services
{
    public class BalancoService : IBalancoService
    {
        private BaseRepository<ControleFinanceiroDb, Lancamento> myRepository;
        public void Dispose() => myRepository = null;

        public BalancoService(string stringConnection)
        {
            myRepository = new BaseRepository<ControleFinanceiroDb, Lancamento>(stringConnection);
        }
        public ServiceMessage<Balanco> Get(DateTime dataInicio, DateTime dataFim, long id)
        {

            ServiceMessage<Balanco> serviceMessage = new();
            serviceMessage.Result = new();
            try
            {
                (DateTime, DateTime) date = (dataInicio, dataFim);
                ValidationRules(serviceMessage, date);

                if (serviceMessage.Mensagens.Count > 0)
                {
                    serviceMessage.CodeHttp = 400;
                    return serviceMessage;
                }

                var lancamentoEntity = myRepository.ListLancamento(dataInicio, dataFim, id);


                if (lancamentoEntity.Count == 0)
                {
                    serviceMessage.CodeHttp = 400;
                    serviceMessage.AddReturnBadRequest("Não foram encontrados lançamentos para o periodo selecionado", EnumErrors.BalancoNaoEncontrado.ToString());
                    return serviceMessage;
                }


                CalculateValues(lancamentoEntity, serviceMessage);

                if (id != -1)
                {
                    serviceMessage.Result.Categoria = new();
                    serviceMessage.Result.Categoria.IdCategoria = lancamentoEntity[0].Categoria.IdCategoria;
                    serviceMessage.Result.Categoria.Nome = lancamentoEntity[0].Categoria.Nome;
                }


                serviceMessage.Successfull = true;
                return serviceMessage;
            }
            catch (Exception ex)
            {
                serviceMessage.AddReturnInternalError(ex.Message);
                return serviceMessage;
            }

        }

        private void ValidationRules(ServiceMessage<Balanco> serviceMessage, (DateTime, DateTime) date)
        {

            if (date.Item1.Date > date.Item2.Date)
                serviceMessage.AddReturnBadRequest("A data inicial não pode ser maior que a final", EnumErrors.DataInvalida.ToString());

            if (date.Item1 > DateTime.Now.Date || date.Item2 > DateTime.Now.Date)
                serviceMessage.AddReturnBadRequest("A data não pode ser maior que a data atual", EnumErrors.DataInvalida.ToString());

        }

        public void CalculateValues(List<LancamentoQuery> lancamento, ServiceMessage<Balanco> serviceMessage)
        {
            long positiveNumber = 0;
            long negativeNumber = 0;

            foreach (var item in lancamento)
            {
                if (item.Valor > 0)
                    positiveNumber += item.Valor;
                else
                    negativeNumber = (negativeNumber) + (item.Valor);
            }
            serviceMessage.Result.Receita = positiveNumber.ToString("C");
            serviceMessage.Result.Despesa = negativeNumber.ToString("C");
            serviceMessage.Result.Saldo = (negativeNumber + positiveNumber).ToString("C");
        }
    }
}
