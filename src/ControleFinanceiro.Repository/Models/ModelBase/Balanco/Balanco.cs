namespace ControleFinanceiro.DataContext.Models.ModelBase
{
    public class Balanco
    {
        public CategoriaBalanco Categoria { get; set; }
        public string Receita { get; set; }
        public string Despesa { get; set; }
        public string Saldo { get; set; }
    }
}
