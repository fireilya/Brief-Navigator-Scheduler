using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Core.EFCore.Configuration;

public class NpgDbContextOptionsConfigurator(
    IOptions<DataContextOptions> options
) : IDbContextOptionsConfigurator
{
    public void Configure(DbContextOptionsBuilder optionsBuilder)
    {
        var dataContextOptions = options.Value;
        if (dataContextOptions.EnableSensitiveDataLogging)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        optionsBuilder.UseNpgsql(dataContextOptions.ConnectionString);
    }
}