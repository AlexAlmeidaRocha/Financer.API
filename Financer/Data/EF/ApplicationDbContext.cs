using Microsoft.EntityFrameworkCore;
using FinancerAPI.Domain.Entities;

namespace FinancerAPI.Data.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Extrato> Extratos { get; set; } = default!;
    }
}
