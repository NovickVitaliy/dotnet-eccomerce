using OrderManagement.Domain.Models;

namespace OrderManagement.DataAccess.Repositories.Contracts;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<int> CreateAsync(TEntity entity);
    Task<TEntity?> GetByIdAsync(int id, bool trackingEnabled = false);
    Task<IReadOnlyCollection<TEntity>> GetAsync(int pageNumber, int pageSize, bool trackingEnabled = false);
    Task<bool> UpdateAsync(TEntity entity);
    Task<bool> DeleteAsync(TEntity entity);
    Task<bool> DeleteByIdAsync(int id);
}