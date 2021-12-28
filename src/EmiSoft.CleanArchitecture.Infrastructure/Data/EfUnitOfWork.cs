using EmiSoft.CleanArchitecture.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmiSoft.CleanArchitecture.Infrastructure.Data;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    public EfUnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int ExecuteSqlRaw(string sqlQuery) => _dbContext.Database.ExecuteSqlRaw(sqlQuery);

    public async Task<int> ExecuteSqlRawAsync(string sqlQuery) => await _dbContext.Database.ExecuteSqlRawAsync(sqlQuery);

    public int Commit() => _dbContext.SaveChanges(true);

    public async Task<int> CommitAsync() => await _dbContext.SaveChangesAsync(true);

    public void Rollback() => _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());

    public async Task RollbackAsync()
    {
        foreach (var entity in _dbContext.ChangeTracker.Entries())
        {
            await entity.ReloadAsync();
        }
    }
}
