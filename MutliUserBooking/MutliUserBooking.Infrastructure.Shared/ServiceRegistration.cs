using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MutliUserBooking.Application.Interfaces;
using MutliUserBooking.Domain.Settings;
using MutliUserBooking.Infrastructure.Shared.Services;

namespace MutliUserBooking.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings"));
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IMockService, MockService>();
        }
    }
}