using ControleFinanceiro.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.DataContext
{
    public class ControleFinanceiroDb : DbContext
    {
        private string myStringConnection;
        public ControleFinanceiroDb() { }
        public ControleFinanceiroDb(string stringConnection)
        {
            myStringConnection = stringConnection;
        }
        public ControleFinanceiroDb(DbContextOptions<ControleFinanceiroDb> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(myStringConnection))
                optionsBuilder.UseSqlServer(myStringConnection);
        }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<SubCategoria> SubCategoria { get; set; }
        public DbSet<Lancamento> Lancamento { get; set; }
    }
}
