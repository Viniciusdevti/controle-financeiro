using ControleFinanceiro.Model.Models;
using ControleFinanceiro.Service.Services;
using System.Collections.Generic;

namespace ControleFinanceiro.Service.Interfaces
{
    public interface ICategoriaService
    {
        ServiceMessage GetAll();
    }
}
