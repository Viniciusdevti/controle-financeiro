using System;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Api.Dtos.LancamentoDto
{
    public class LancamentoUpdateDto
    {
        [Range(0, int.MaxValue, ErrorMessage = "Campo IdSubCategoria é obrigatorio. #CampoObrigatorio")]
        public long IdLancamento { get; set; } = -1;
        public long Valor { get; set; }
        public DateTime Data { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Campo IdSubCategoria é obrigatorio. #CampoObrigatorio")]
        public long IdSubCategoria { get; set; } = -1;

        [StringLength(1000, ErrorMessage = "Quantidade maxima de caracteres para comentario é 1000. #MaximoCaracteres")]
        public string Comentario { get; set; }
    }
}
