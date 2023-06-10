using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MutliUserBooking.Application.Interfaces;
using MutliUserBooking.Application.Interfaces.Repositories;
using MutliUserBooking.Infrastructure.Persistence.Contexts;
using MutliUserBooking.Infrastructure.Persistence.Repositories;
using MutliUserBooking.Infrastructure.Persistence.Repository;

namespace MutliUserBooking.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                   b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            }

            #region Repositories

            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IPositionRepositoryAsync, PositionRepositoryAsync>();
            services.AddTransient<IEmployeeRepositoryAsync, EmployeeRepositoryAsync>();
            services.AddTransient<IUserRepositoryAsync, UserRepositoryAsync>();

            #endregion Repositories
        }
    }
}