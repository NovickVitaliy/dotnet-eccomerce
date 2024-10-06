using Microsoft.EntityFrameworkCore;
using OrderManagement.DataAccess.Persistence;
using OrderManagement.DataAccess.Repositories.Contracts;
using OrderManagement.Domain.Models;

namespace OrderManagement.DataAccess.Repositories.Implementations;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext _appDbContext;
    
    protected GenericRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    
    public async Task<int> CreateAsync(TEntity entity)
    {
        await _appDbContext.Set<TEntity>().AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
        return entity.Id;
    }
    
    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await _appDbContext.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<IReadOnlyCollection<TEntity>> GetAsync(int pageNumber, int pageSize)
    {
        var skip = (pageNumber - 1) * pageSize;

        return await _appDbContext.Set<TEntity>().Skip(skip).Take(pageSize).ToListAsync();
    }
    
    public async Task<bool> UpdateAsync(TEntity entity)
    {
        _appDbContext.Set<TEntity>().Update(entity);
        var rowsChanged = await _appDbContext.SaveChangesAsync();
        return rowsChanged == 1;
    }
    
    public async Task<bool> DeleteAsync(TEntity entity)
    {
        _appDbContext.Set<TEntity>().Remove(entity);
        var rowsChanged = await _appDbContext.SaveChangesAsync();
        return rowsChanged == 1;
    }
    
    public async Task<bool> DeleteByIdAsync(int id)
    {
        var entity = await _appDbContext.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);
        if (entity is null)
        {
            return false;
        }
        _appDbContext.Set<TEntity>().Remove(entity);
        await _appDbContext.SaveChangesAsync();
        return true;
    }
}