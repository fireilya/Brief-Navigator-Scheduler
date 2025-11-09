using Microsoft.EntityFrameworkCore;

namespace Core.EFCore.Configuration;

public interface IDbContextOptionsConfigurator
{
    void Configure(DbContextOptionsBuilder optionsBuilder);
}