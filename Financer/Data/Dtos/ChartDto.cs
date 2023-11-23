namespace FinancerAPI.Data.Dtos
{
    public class Dashboard
    {
        public List<ChartDto> Positive { get; set; }
        public List<ChartDto> Negative { get; set; }
    }

    public class ChartDto
    {
        public int Month { get; set; }
        public decimal Value { get; set; }
    }
}
