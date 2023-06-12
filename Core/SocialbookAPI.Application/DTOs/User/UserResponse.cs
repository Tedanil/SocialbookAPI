using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.DTOs.User
{
    public class UserResponse
    {
        public string? NameSurname { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? UserId { get; set; }
        public int? Level { get; set; }
        public int? Exp { get; set; }
        public string? Title { get; set; }
        public int? VoteCount { get; set; }
        //public string? Role { get; set; }
    }
}
