using Microsoft.EntityFrameworkCore;
using FinancerAPI.Abstractions.Interface.Repositories;
using FinancerAPI.Domain.Entities;
using FinancerAPI.Data.Dtos;

namespace FinancerAPI.Data.EF.Repositories
{
    public class ExtratoRepository : IExtratoRepository
    {

        private readonly ApplicationDbContext _dbContext;

        public ExtratoRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(Extrato extract)
        {
            await _dbContext.Extratos.AddAsync(extract);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Extrato?> GetById(int id)
        {
            return await _dbContext.Extratos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Extrato>> GetAll(DateTime dtInitial, DateTime dtEnd)
        {
            return await _dbContext.Extratos.Where(x => x.Date >= dtInitial && x.Date <= dtEnd).ToListAsync();
        }

        public async Task<Dashboard> GetChart()
        {
            var resultPositive = await _dbContext.Extratos
                .Where(x => x.Value >= 0 && x.Date.Year == DateTime.Now.Year)
                .GroupBy(g => g.Date.Month)
                .ToListAsync();

            var chartDtosPositive = resultPositive.Select(x => new ChartDto()
            {
                Month = x.First().Date.Month,
                Value = x.Sum(y => y.Value)
            }).ToList();

            var resultNegative = await _dbContext.Extratos
                .Where(x => x.Value < 0 && x.Date.Year == DateTime.Now.Year)
                .GroupBy(g => g.Date.Month)
                .ToListAsync();

            var chartDtosNegative = resultNegative.Select(x => new ChartDto()
            {
                Month = x.First().Date.Month,
                Value = x.Sum(y => y.Value)
            }).ToList();

            return new Dashboard() { Positive = chartDtosPositive, Negative = chartDtosNegative };
        }

        public async Task Update(Extrato extract)
        {
            _dbContext.Extratos.Attach(extract);
            await _dbContext.SaveChangesAsync();
        }

    }
}
