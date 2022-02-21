using System;
using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Api.Dtos.LancamentoDto
{
    public class LancamentoCreateDto
    {

        public long Valor { get; set; } = 0;
        public DateTime Data { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Campo IdSubCategoria é obrigatorio. #CampoObrigatorio")]
        public long IdSubCategoria { get; set; } = -1;

        [StringLength(300, ErrorMessage = "Quantidade maxima de caracteres para comentario é 1000. #MaximoCaracteres")]
        public string Comentario{ get; set; }

        
    }
}