using Core.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.EFCore;

public class DataContext(
    IDbContextModelConfigurator modelConfigurator,
    ILogger<DataContext> logger,
    DbContextOptions dbContextOptions
) : DbContext(dbContextOptions)
{
    private const string CounterKey = "DataContextCounter";
    private readonly int currentContextNumber = GlobalCounter.GetCountWithIncrement(CounterKey);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        logger.LogInformation("Context: {contextNumber} Data context model creating", currentContextNumber);
        modelConfigurator.Configure(modelBuilder);
    }
}