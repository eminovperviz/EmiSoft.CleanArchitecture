using EmiSoft.CleanArchitecture.Application.Interfaces;
using EmiSoft.CleanArchitecture.Domain.Interfaces;
using EmiSoft.CleanArchitecture.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace EmiSoft.CleanArchitecture.Infrastructure.Persistence.Data;

public class AppDbContext : DbContext
{
    private readonly ICurrentUserService _currentUserService;
    private static readonly MethodInfo _configureGlobalFiltersMethodInfo = typeof(AppDbContext).GetMethod(nameof(ConfigureGlobalFilters), BindingFlags.Instance | BindingFlags.NonPublic);

    public AppDbContext(DbContextOptions<AppDbContext> options, ICurrentUserService currentUserService) : base(options)
    {
        _currentUserService = currentUserService;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();
        ChangeDeleteBehaviorToRestrict(modelBuilder);
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            _configureGlobalFiltersMethodInfo
                .MakeGenericMethod(entityType.ClrType)
                .Invoke(this, new object[] { modelBuilder, entityType });
        }
        base.OnModelCreating(modelBuilder);

        modelBuilder.Seed();
    }

    //----------------Global Filter ------------
    protected void ConfigureGlobalFilters<TEntity>(ModelBuilder modelBuilder, IMutableEntityType entityType) where TEntity : class
    {
        if (entityType.BaseType != null || !ShouldFilterEntity<TEntity>(entityType))
            return;
        var filterExpression = CreateFilterExpression<TEntity>();
        if (filterExpression == null)
            return;
        modelBuilder.Entity<TEntity>().HasQueryFilter(filterExpression);

    }

    protected virtual bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType) where TEntity : class
    {
        return typeof(IDictionaryEntity).IsAssignableFrom(typeof(TEntity));
    }

    protected Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>() where TEntity : class
    {
        Expression<Func<TEntity, bool>> expression = null;

        if (typeof(IDictionaryEntity).IsAssignableFrom(typeof(TEntity)))
        {
            Expression<Func<TEntity, bool>> removedFilter = e => ((ISoftDelete)e).IsDeleted;
            expression = expression == null ? removedFilter : CombineExpressions(expression, removedFilter);
        }

        return expression;
    }

    protected Expression<Func<T, bool>> CombineExpressions<T>(Expression<Func<T, bool>> expression1, Expression<Func<T, bool>> expression2)
    {
        return ExpressionCombiner.Combine(expression1, expression2);
    }


    public new int SaveChanges(bool isCommit = true)
    {
        TrackChanges();
        if (isCommit)
            return base.SaveChanges();
        return 0;
    }

    public new async Task<int> SaveChangesAsync(bool isCommit = true, CancellationToken cancellationToken = new CancellationToken())
    {
        TrackChanges();
        if (isCommit)
            return await base.SaveChangesAsync(cancellationToken);
        return 0;
    }

    private void TrackChanges()
    {
        ChangeTracker.DetectChanges();
        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                entry.Entity.CreatedBy = _currentUserService.UserId;
                entry.Entity.CreatedDate = DateTime.Now;
                break;
                case EntityState.Modified:
                entry.Entity.LastModifiedBy = _currentUserService.UserId;
                entry.Entity.LastModifiedDate = DateTime.Now;
                break;
            }
        }
    }

    private static void ChangeDeleteBehaviorToRestrict(ModelBuilder modelBuilder)
    {
        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                     .SelectMany(t => t.GetForeignKeys())
                     .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;
    }
}
