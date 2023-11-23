using System.ComponentModel.DataAnnotations;
using FinancerAPI.Domain.Enums;

namespace FinancerAPI.Models
{
    public class ExtratoCreateModel
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
    }
}
