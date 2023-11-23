using FinancerAPI.Domain.Enums;

namespace FinancerAPI.Models
{
    public class ExtratoPatchModel
    {
        public required DateTime Date { get; set; }
        public required string Description { get; set; }
    }
}
