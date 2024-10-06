using Microsoft.EntityFrameworkCore;
using OrderManagement.DataAccess.Persistence;
using OrderManagement.DataAccess.Repositories.Contracts;
using OrderManagement.Domain.Models;

namespace OrderManagement.DataAccess.Repositories.Implementations;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly AppDbContext AppDbContext;
    
    protected GenericRepository(AppDbContext appDbContext)
    {
        AppDbContext = appDbContext;
    }
    
    public async Task<int> CreateAsync(TEntity entity)
    {
        await AppDbContext.Set<TEntity>().AddAsync(entity);
        await AppDbContext.SaveChangesAsync();
        return entity.Id;
    }
    
    public async Task<TEntity?> GetByIdAsync(int id, bool trackingEnabled = false)
    {
        var query = AppDbContext.Set<TEntity>().Where(x => x.Id == id);
        return trackingEnabled
            ? await query.SingleOrDefaultAsync()
            : await query.AsNoTracking().SingleOrDefaultAsync();
    }
    
    public async Task<IReadOnlyCollection<TEntity>> GetAsync(int pageNumber, int pageSize, bool trackingEnabled = false)
    {
        var skip = (pageNumber - 1) * pageSize;
        var query = AppDbContext.Set<TEntity>().Skip(skip).Take(pageSize);
        return trackingEnabled
            ? await query.ToListAsync()
            : await query.AsNoTracking().ToListAsync();
    }
    
    public async Task<bool> UpdateAsync(TEntity entity)
    {
        AppDbContext.Set<TEntity>().Update(entity);
        var rowsChanged = await AppDbContext.SaveChangesAsync();
        return rowsChanged == 1;
    }
    
    public async Task<bool> DeleteAsync(TEntity entity)
    {
        AppDbContext.Set<TEntity>().Remove(entity);
        var rowsChanged = await AppDbContext.SaveChangesAsync();
        return rowsChanged == 1;
    }
    
    public async Task<bool> DeleteByIdAsync(int id)
    {
        var entity = await AppDbContext.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);
        if (entity is null)
        {
            return false;
        }
        AppDbContext.Set<TEntity>().Remove(entity);
        await AppDbContext.SaveChangesAsync();
        return true;
    }
}