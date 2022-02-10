using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ControleFinanceiro.Model.Models

{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        public long IdCategoria { get; set; }

        public string Nome { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
