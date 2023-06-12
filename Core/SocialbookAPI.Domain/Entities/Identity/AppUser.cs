using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string? NameSurname { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public int? Level { get; set; }
        public int? VoteCount { get; set; }
        public int? Exp { get; set; }
        public string? Title { get; set; }


    }
}
