using SocialbookAPI.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Domain.Entities
{
    public class Song : BaseEntity

    {
        public string? Name { get; set; }
        public string? VideoId { get; set; }
        public SongImage? Image { get; set; }
        public string? Genre { get; set; }




    }
}
