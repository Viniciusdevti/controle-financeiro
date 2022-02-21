using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.Model.Models
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
