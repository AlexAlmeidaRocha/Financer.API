using FinancerAPI.Data.Dtos;
using FinancerAPI.Domain.Entities;

namespace FinancerAPI.Abstractions.Interface.Repositories
{
    public interface IExtratoRepository
    {
        Task Add(Extrato extrato);
        Task<Extrato?> GetById(int id);
        Task<List<Extrato>> GetAll(DateTime dtInitial, DateTime dtEnd);
        Task Update(Extrato extrato);
        Task<Dashboard> GetChart();
    }
}
