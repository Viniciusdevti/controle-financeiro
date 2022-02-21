
using System;

namespace ControleFinanceiro.Api.Dtos.Balanco
{
    public class BalancoGetDto
    {
        public long Id { get; set; } = -1;
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
    }
}
