namespace SocialbookAPI.Application.Features.Queries.AppUser.GetUserByToken
{
    public class GetUserByTokenQueryResponse
    {
        public string? NameSurname { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? UserId { get; set; }
        public int? Level { get; set; }
        public int? Exp { get; set; }
        public string? Title { get; set; }
        public int? VoteCount { get; set; }
        public string? Role { get; set; }
    }
}