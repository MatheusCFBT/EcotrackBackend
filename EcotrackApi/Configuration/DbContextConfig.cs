using Ecotrack.Context;
using Microsoft.EntityFrameworkCore;

namespace EcotrackApi.Configuration
{
    public static class DbContextConfig
    {
        public static WebApplicationBuilder AddDbContextConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<EcotrackDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            return builder;
        }
    }
}