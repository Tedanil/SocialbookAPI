﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SocialbookAPI.Domain.Entities.Identity;
using SocialbookAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialbookAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<SocialbookDbContext>(options => options.UseNpgsql(Configuration.ConnectionString));
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<SocialbookDbContext>()
            .AddDefaultTokenProviders();

        }
    }
}