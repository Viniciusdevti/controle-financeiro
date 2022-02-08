using ControleFinanceiro.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleFinanceiro.Data.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<ICollection<Categoria>> GetAll();
    }
}
