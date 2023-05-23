using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.DTOs.Facebook
{
    public class FacebookUserAccessTokenValidation_DTO
    {
        [JsonPropertyName("data")]
        public FacebookUserAccessTokenValidation_DTOData Data { get; set; }

    }

    public class FacebookUserAccessTokenValidation_DTOData
    {
        [JsonPropertyName("is_valid")]
        public bool IsValid { get; set; }

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }
    }
}
