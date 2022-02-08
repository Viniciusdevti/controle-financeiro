using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ControleFinanceiro.Model.Models

{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        public long IdCategoria { get; set; }
        [Required]
        public string Nome { get; set; }
    }
}
