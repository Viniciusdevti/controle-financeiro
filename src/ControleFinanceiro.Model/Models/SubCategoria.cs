using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ControleFinanceiro.Model.Models

{
    [Table("SubCategoria")]
    public class SubCategoria
    {
        [Key]
        public long IdSubCategoria { get; set; }
        [Required]
        public int Nome { get; set; }
        [Required]
        public long IdCategoria { get; set; }
    }
}
