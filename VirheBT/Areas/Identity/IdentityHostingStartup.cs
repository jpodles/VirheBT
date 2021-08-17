using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(VirheBT.Areas.Identity.IdentityHostingStartup))]

namespace VirheBT.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}