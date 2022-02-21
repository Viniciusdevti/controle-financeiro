using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Api.Dtos.CategoriaDto
{
    public class CategoriaCreateDto
    {

        [Required(ErrorMessage = "Campo Nome é obrigatorio. #CampoObrigatorio")]
        [StringLength(300, ErrorMessage = "Quantidade maxima de caracteres para nome é 300. #MaximoCaracteres")]
        public string Nome { get; set; }
    }
}
