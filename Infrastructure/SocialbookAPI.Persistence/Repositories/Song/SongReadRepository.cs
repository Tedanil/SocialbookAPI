using SocialbookAPI.Application.Repositories;
using SocialbookAPI.Domain.Entities;
using SocialbookAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Persistence.Repositories
{
    public class SongReadRepository : ReadRepository<Song>, ISongReadRepository
    {
        public SongReadRepository(SocialbookDbContext context) : base(context)
        {
        }
    }
}
