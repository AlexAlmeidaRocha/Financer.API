using Microsoft.EntityFrameworkCore;
using FinancerAPI.Abstractions.Interface.Repositories;
using FinancerAPI.Data.EF;
using FinancerAPI.Data.EF.Repositories;

namespace FinancerAPI.Config
{
    public static class Ioc
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection services)
        {
            string patchDb = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory.Split("bin")[0], "Data\\DB\\SQLITEDB1.db");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(@$"Data Source={patchDb}"));
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IExtratoRepository, ExtratoRepository>();
            return services;
        }
    }
}
