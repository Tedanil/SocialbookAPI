﻿using SocialbookAPI.Application.DTOs.VideoCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Application.Abstractions.Services
{
    public interface IYouTubeService
    {
        Task<VideoIdAndTime> GetMaxVotedVideoWithContentDetails();
    }
}
