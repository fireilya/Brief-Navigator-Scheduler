using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Core.EFCore.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Dao.DataBase;

public class DbContextModelConfigurator : IDbContextModelConfigurator
{
    public void Configure(ModelBuilder modelBuilder)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var entityTypes = assembly.GetExportedTypes()
           .Where(x => x.GetCustomAttribute<TableAttribute>() is not null)
           .ToArray();

        foreach (var entityType in entityTypes)
        {
            modelBuilder.Entity(entityType);
        }
    }
}