using ControleFinanceiro.DataContext.Models.ModelBase;
using ControleFinanceiro.Service.Services;
using System;

namespace ControleFinanceiro.Service.Interfaces
{
    public interface IBalancoService
    {
        ServiceMessage<Balanco> Get(DateTime dataInicio, DateTime dataFim, long id);
    }
}
