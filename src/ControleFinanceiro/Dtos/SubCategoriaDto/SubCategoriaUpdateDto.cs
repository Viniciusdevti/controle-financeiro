using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Api.Dtos.SubCategoriaDto
{
    public class SubCategoriaUpdateDto
    {
        [Required]
        public long IdSubCategoria { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public long IdCategoria { get; set; }
    }
}
