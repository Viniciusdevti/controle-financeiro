using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Api.Dtos.CategoriaDto
{
    public class CategoriaUpdateDto
    {
        [Range(0, int.MaxValue, ErrorMessage = "Campo IdCategoria é obrigatorio . #RequiredField")]
        public long IdCategoria { get; set; } = -1;

        [Required(ErrorMessage = "Campo Nome é obrigatorio . #RequiredField")]
        [StringLength(300, ErrorMessage = "Quantidade maxima de caracteres para nome é 300. #MaximoCaracteres")]
        public string Nome { get; set; }
    }
}
