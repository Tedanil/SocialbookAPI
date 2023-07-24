namespace SocialbookAPI.Application.Features.Queries.Song.GetAllSongs
{
    public class GetAllSongsQueryResponse
    {
        public int TotalSongCount { get; set; }
        public object Songs { get; set; }
    }
}