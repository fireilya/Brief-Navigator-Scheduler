using System.Reflection;
using Core.EFCore.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Dao.Configuration;

public class DbContextModelConfigurator : IDbContextModelConfigurator
{
    public void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.UseAssemblyEntities(Assembly.GetExecutingAssembly());
    }
}