using Microsoft.EntityFrameworkCore;

namespace Core.EFCore;

public interface IDbContextModelConfigurator
{
    void Configure(ModelBuilder modelBuilder);
}