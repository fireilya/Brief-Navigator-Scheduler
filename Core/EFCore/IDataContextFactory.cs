using Microsoft.EntityFrameworkCore;

namespace Core.EFCore;

public interface IDataContextFactory
{
    IDataContext Create();
}

public class DataContextFactory(
    IDbContextFactory<DataContext> efCoreFactory
) : IDataContextFactory
{
    public IDataContext Create() => efCoreFactory.CreateDbContext();
}