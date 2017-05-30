using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

namespace YourNamespace
{
    public class Startup
    {
        public void Configure(IApplicationBuilder App)
        {
            App.UseMvc();
            App.UseStaticFiles();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }
    }
}