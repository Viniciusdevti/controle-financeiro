using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Api.Dtos.SubCategoriaDto
{
    public class SubCategoriaCreateDto
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public long IdCategoria { get; set; }
    }
}
