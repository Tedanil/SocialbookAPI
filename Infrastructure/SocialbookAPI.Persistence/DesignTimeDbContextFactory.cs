using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SocialbookAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SocialbookDbContext>
    {
        public SocialbookDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<SocialbookDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseNpgsql(Configuration.ConnectionString);
            return new(dbContextOptionsBuilder.Options);
        }
    }
}
