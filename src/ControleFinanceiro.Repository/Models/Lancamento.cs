using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ControleFinanceiro.Model.Models

{
    [Table("Lancamento")]
    public class Lancamento
    {
        [Key]
        public long IdLancamento{ get; set; }
        [Required]
        public long Valor { get; set; }
        [Required]
        public DateTime Data { get; set; }
        public string Comentario { get; set; }
        [Required]
        [ForeignKey("SubCategoria")]
        public long IdSubCategoria { get; set; }

        public virtual SubCategoria SubCategoria { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
