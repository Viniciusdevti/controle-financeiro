using ControleFinanceiro.Model.Models;

namespace ControleFinanceiro.DataContext.BaseQuery
{
    public class LancamentoQuery
    {
        public long Valor{ get; set; }
        public Categoria Categoria{ get; set; }
    }
}
