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
        public string Nome { get; set; }

        [Required]
        [ForeignKey("Categoria")]
        public long IdCategoria { get; set; }

        public virtual  Categoria Categoria { get; set; }

        public bool Ativo { get; set; } = true;
    }
}
