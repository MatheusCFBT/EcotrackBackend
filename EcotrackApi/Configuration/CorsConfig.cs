namespace EcotrackApi.Configuration
{
    public static class CorsConfig
    {
        public static WebApplicationBuilder AddCorsConfig(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Development", builder =>
                {
                    builder.WithOrigins("http://127.0.0.1:5500") 
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials(); 
                });
            });
            return builder;
        }
    }
}