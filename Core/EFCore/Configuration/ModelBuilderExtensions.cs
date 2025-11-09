using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Core.EFCore.Configuration;

public static class ModelBuilderExtensions
{
    public static void UseAssemblyEntities(this ModelBuilder modelBuilder, Assembly assembly)
    {
        var entityTypes = assembly.GetExportedTypes()
           .Where(x => x.GetCustomAttribute<TableAttribute>() is not null)
           .ToArray();

        foreach (var entityType in entityTypes)
        {
            modelBuilder.Entity(entityType);
        }
    }
}