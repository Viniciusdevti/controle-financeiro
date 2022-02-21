namespace ControleFinanceiro.Api.Dtos.SubCategoriaDto
{
    public class SubCategoriaGetDto
    {
        public long IdSubCategoria { get; set; }
        public string Nome { get; set; }
        public long IdCategoria { get; set; }
    }
}
