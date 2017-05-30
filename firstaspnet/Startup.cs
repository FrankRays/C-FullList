using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace firstaspnet{
    
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
                
            services.AddMvc();
        }
        public void Configure(IApplicationBuilder App, ILoggerFactory LoggerFactory){
            App.UseStaticFiles();
            App.UseMvc();   
            LoggerFactory.AddConsole();
        }
    }
    }
    

