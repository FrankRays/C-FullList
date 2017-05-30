using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Nancy.Owin;

namespace GreatNumberGame{
    public class Startup{
        public void Configure(IApplicationBuilder app, ILoggerFactory LoggerFactory){
            LoggerFactory.AddConsole();
            app.UseOwin(x => x.UseNancy());
        }
    }
}