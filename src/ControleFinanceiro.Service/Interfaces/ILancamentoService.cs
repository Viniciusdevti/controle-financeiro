using ControleFinanceiro.Model.Models;
using ControleFinanceiro.Service.Services;

namespace ControleFinanceiro.Service.Interfaces
{
    public interface ILancamentoService
    {

        ServiceMessage<Lancamento> GetAll();
        ServiceMessage<Lancamento> Get(long id);
        ServiceMessage<Lancamento> Post(Lancamento Lancamento);
        ServiceMessage<Lancamento> Put(Lancamento Lancamento);
        ServiceMessage<Lancamento> Delete(long id);
    }
}
