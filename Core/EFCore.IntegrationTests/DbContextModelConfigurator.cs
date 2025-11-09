using Core.EFCore.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Core.EFCore.IntegrationTests;

public class DbContextModelConfigurator : IDbContextModelConfigurator
{
    public void Configure(ModelBuilder modelBuilder)
    {
        throw new System.NotImplementedException();
    }
}