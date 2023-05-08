using Microsoft.EntityFrameworkCore;
using SocialbookAPI.Domain.Entities.Common;

namespace SocialbookAPI.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
