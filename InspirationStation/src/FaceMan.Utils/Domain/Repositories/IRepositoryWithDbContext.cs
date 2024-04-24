using Microsoft.EntityFrameworkCore;

namespace FaceMan.Utils.Domain.Repositories;

public interface IRepositoryWithDbContext
{
    DbContext GetDbContext();

    Task<DbContext> GetDbContextAsync();
}