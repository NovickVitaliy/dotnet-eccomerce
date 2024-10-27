using CommunityService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunityService.Application.Data;

public interface IAppDbContext
{
    DbSet<Review> Reviews { get; }
    DbSet<Question> Questions { get; }

    Task<int> SaveChangesAsync();
}