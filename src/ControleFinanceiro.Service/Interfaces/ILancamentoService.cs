using ControleFinanceiro.Model.Models;
using ControleFinanceiro.Service.Services;

namespace ControleFinanceiro.Service.Interfaces
{
    public interface ILancamentoService
    {

        ServiceMessage<Categoria> GetAll();
        ServiceMessage<Categoria> Get(long id);
        ServiceMessage<Categoria> Post(Categoria categoria);
        ServiceMessage<Categoria> Put(Categoria categoria);
        ServiceMessage<Categoria> Delete(long id);
    }
}
