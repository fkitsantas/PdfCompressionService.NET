using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PdfCompressionService.Services.Interfaces;

namespace PdfCompressionService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Use the fully qualified name for PdfCompressionService
            services.AddScoped<IPdfCompressionService, Services.PdfCompressionService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}