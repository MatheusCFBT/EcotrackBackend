using Ecotrack.Context;
using EcotrackBusiness.Interfaces;
using EcotrackBusiness.Notifications;
using EcotrackBusiness.Services;
using EcotrackData.Repository;

namespace EcotrackApi.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static WebApplicationBuilder AddDependencyInjectionConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<EcotrackDbContext>();
            builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
            builder.Services.AddScoped<IClienteService, ClienteService>();
            builder.Services.AddScoped<INotificador, Notificador>();
            builder.Services.AddScoped<IEmailSender, EmailSenderService>();

            return builder;
        }
    }
}