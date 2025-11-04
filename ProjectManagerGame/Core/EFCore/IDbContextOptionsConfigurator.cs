using Microsoft.EntityFrameworkCore;

namespace Core.EFCore;

public interface IDbContextOptionsConfigurator
{
    void Configure(DbContextOptionsBuilder optionsBuilder);
}