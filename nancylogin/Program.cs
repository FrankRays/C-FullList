using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Nancylogin
{
    public class Program
    {
        public static void Main(string[] args)
        {
             IWebHost host = new WebHostBuilder()
                .UseIISIntegration()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();
            host.Run(); 
        }
    }
}
