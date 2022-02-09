using ControleFinanceiro.Model.Models;
using ControleFinanceiro.Service.Services;
using System.Collections.Generic;

namespace ControleFinanceiro.Service.Interfaces
{
    public interface ICategoriaService
    {
        ServiceMessage GetAll();
        ServiceMessage Get(long id);
        ServiceMessage Post(Categoria categoria);
        ServiceMessage Put(Categoria categoria);
        ServiceMessage Delete();
    }
}
