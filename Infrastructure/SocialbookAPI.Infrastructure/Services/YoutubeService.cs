using Microsoft.Extensions.Configuration;
using SocialbookAPI.Application.Abstractions.Services;
using SocialbookAPI.Application.DTOs.VideoCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SocialbookAPI.Infrastructure.Services
{
    public class YouTubeService : IYouTubeService
    {
        private readonly HttpClient _httpClient;
        private readonly IVoteService _voteService;
        private readonly IConfiguration _configuration;

        public YouTubeService(HttpClient httpClient, IVoteService voteService, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _voteService = voteService;
            _configuration = configuration;
        }

        public async Task<VideoIdAndTime> GetMaxVotedVideoWithContentDetails()
        {
            // Get the video with the most votes
            var maxVotedVideo = _voteService.GetMaxVotedVideo();

            var apiKey = _configuration["YoutubeAPIKey"];

            // Get video details from the YouTube API
            var response = await _httpClient.GetAsync($"https://www.googleapis.com/youtube/v3/videos?part=snippet,contentDetails&id={maxVotedVideo.Key}&key={apiKey}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var videoDetails = JsonSerializer.Deserialize<YouTubeVideoDetails>(content);

            // Calculate video duration in seconds
            var duration = videoDetails.Items[0].ContentDetails.Duration;
            var durationInSeconds = ConvertDurationToSeconds(duration);
            Console.WriteLine($"saniye {duration}");
            Console.WriteLine($"çevrilniş saniye {durationInSeconds}");



            // Return video id and duration
            return new VideoIdAndTime
            {
                VideoId = maxVotedVideo.Key,
                VideoTime = durationInSeconds.ToString()
            };
        }

        private int ConvertDurationToSeconds(string duration)
        {
            var match = Regex.Match(duration, @"PT(\d+H)?(\d+M)?(\d+S)?");

            var hours = string.IsNullOrEmpty(match.Groups[1].Value) ? 0 : int.Parse(match.Groups[1].Value.Replace("H", ""));
            var minutes = string.IsNullOrEmpty(match.Groups[2].Value) ? 0 : int.Parse(match.Groups[2].Value.Replace("M", ""));
            var seconds = string.IsNullOrEmpty(match.Groups[3].Value) ? 0 : int.Parse(match.Groups[3].Value.Replace("S", ""));

            return hours * 3600 + minutes * 60 + seconds;
        }
    }

    public class YouTubeVideoDetails
    {
        [JsonPropertyName("items")]
        public List<YouTubeVideoItem> Items { get; set; }
    }

    public class YouTubeVideoItem
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("snippet")]
        public Snippet Snippet { get; set; }

        [JsonPropertyName("contentDetails")]
        public ContentDetails ContentDetails { get; set; }
    }

    public class Snippet
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
    }

    public class ContentDetails
    {
        [JsonPropertyName("duration")]
        public string Duration { get; set; }
    }


}
