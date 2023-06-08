using Microsoft.OpenApi.Models;

namespace Calendario.Config
{
    public static class ApiDocumentation
    {
        public static IServiceCollection ConfigureApiDocumentationUI(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Calendario API", Version = "v1" });
            });
            return services;
        }
        public static IApplicationBuilder ConfigureApiDocumentationUI(this IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calendario API v1"));
            return app;
        }
    }
}
