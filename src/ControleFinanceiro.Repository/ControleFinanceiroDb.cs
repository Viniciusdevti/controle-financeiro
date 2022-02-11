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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(myStringConnection);
        }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<SubCategoria> SubCategoria { get; set; }
        public DbSet<Lancamento> Lancamento { get; set; }
    }
}
