using Microsoft.EntityFrameworkCore;

namespace Core.EFCore.Configuration;

public interface IDbContextModelConfigurator
{
    void Configure(ModelBuilder modelBuilder);
}