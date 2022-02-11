using ControleFinanceiro.Model.Models;
using ControleFinanceiro.Service.Services;

namespace ControleFinanceiro.Service.Interfaces
{
        public interface ISubCategoriaService
    {
            ServiceMessage<SubCategoria> GetAll();
            ServiceMessage<SubCategoria> Get(long id);
            ServiceMessage<SubCategoria> Post(SubCategoria SubCategoria);
            ServiceMessage<SubCategoria> Put(SubCategoria SubCategoria);
            ServiceMessage<SubCategoria> Delete(long id);
        }
    }

