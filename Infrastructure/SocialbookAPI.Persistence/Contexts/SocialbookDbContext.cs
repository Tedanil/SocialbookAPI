using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialbookAPI.Domain.Entities;
using SocialbookAPI.Domain.Entities.Common;
using SocialbookAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SocialbookAPI.Persistence.Contexts
{
    public class SocialbookDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public SocialbookDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<Endpoint> Endpoints { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow

                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
