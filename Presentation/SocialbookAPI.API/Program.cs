using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SocialbookAPI.API.Filters;
using SocialbookAPI.Application;
using SocialbookAPI.Application.Validators.Users;
using SocialbookAPI.Infrastructure;
using SocialbookAPI.Infrastructure.Filters;
using SocialbookAPI.Infrastructure.Services;
using SocialbookAPI.Persistence;
using SocialbookAPI.SignalR;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();


builder.Services.AddAplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices();
builder.Services.AddSignalRServices();
builder.Services.AddStackExchangeRedisCache(options => options.Configuration = "localhost:1559");
builder.Services.AddHostedService<RedisStartupService>();
// Add services to the container.

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
            policy.WithOrigins("https://localhost:4200", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod()
            .AllowCredentials()
            ));

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationFilter>();
    options.Filters.Add<RolePermissionFilter>();
})
               .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateUserValidator>())
               .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer("Admin", options =>
       {
           options.TokenValidationParameters = new()
           {
               ValidateAudience = true, //Oluþturulacak token deðerini kimlerin/hangi originlerin/sitelerin kullanýcý belirlediðimiz deðerdir. -> www.bilmemne.com
               ValidateIssuer = true, //Oluþturulacak token deðerini kimin daðýttýný ifade edeceðimiz alandýr. -> www.myapi.com
               ValidateLifetime = true, //Oluþturulan token deðerinin süresini kontrol edecek olan doðrulamadýr.
               ValidateIssuerSigningKey = true, //Üretilecek token deðerinin uygulamamýza ait bir deðer olduðunu ifade eden suciry key verisinin doðrulanmasýdýr.

               ValidAudience = builder.Configuration["Token:Audience"],
               ValidIssuer = builder.Configuration["Token:Issuer"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
               LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,

               NameClaimType = ClaimTypes.Name //JWT üzerinde Name claimne karþýlýk gelen deðeri User.Identity.Name propertysinden elde edebiliriz.
           };
       });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseCors();
app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.MapHubs();


app.Run();
