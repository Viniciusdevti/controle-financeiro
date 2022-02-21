using ControleFinanceiro.Model.Models;
using ControleFinanceiro.Service.Services;
using System.Collections.Generic;

namespace ControleFinanceiro.Service.Interfaces
{
    public interface ICategoriaService
    {
        ServiceMessage<Categoria> GetAll();
        ServiceMessage<Categoria> Get(long id);
        ServiceMessage<Categoria> Post(Categoria categoria);
        ServiceMessage<Categoria> Put(Categoria categoria);
        ServiceMessage<Categoria> Delete(long id);
    }
}
