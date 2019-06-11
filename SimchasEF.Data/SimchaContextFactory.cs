using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SimchasEF.Data
{
    public class SimchaContextFactory : IDesignTimeDbContextFactory<SimchaContext>
    {
        public SimchaContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}SimchasEF.Web"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new SimchaContext(config.GetConnectionString("ConStr"));
        }
    }

}
