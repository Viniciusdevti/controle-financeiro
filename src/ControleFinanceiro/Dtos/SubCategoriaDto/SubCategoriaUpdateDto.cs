using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Api.Dtos.SubCategoriaDto
{
    public class SubCategoriaUpdateDto
    {
        [Range(0, int.MaxValue, ErrorMessage = "Campo IdCategoria é obrigatorio. #CampoObrigatorio")]
        public long IdSubCategoria { get; set; } = -1;

        [Required(ErrorMessage = "Campo Nome é obrigatorio. #CampoObrigatorio")]
        [StringLength(300, ErrorMessage = "Quantidade maxima de caracteres para nome é 300. #MaximoCaracteres")]
        public string Nome { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Campo IdCategoria é obrigatorio. #CampoObrigatorio")]
        public long IdCategoria { get; set; } = -1;
    }
}
