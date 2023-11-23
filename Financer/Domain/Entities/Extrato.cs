using System.ComponentModel.DataAnnotations;
using FinancerAPI.Domain.Enums;

namespace FinancerAPI.Domain.Entities
{
    public class Extrato
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(80)]
        public string Description { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public bool Avulso { get; set; }
        public ExtratoStatus Status { get; set; }
    }
}
