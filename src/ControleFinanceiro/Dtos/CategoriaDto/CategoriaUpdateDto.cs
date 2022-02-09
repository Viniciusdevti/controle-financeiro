using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Api.Dtos.CategoriaDto
{
    public class CategoriaUpdateDto
    {
        [Required]
        public long IdCategoria { get; set; }

        [Required]
        public string Nome { get; set; }
    }
}
