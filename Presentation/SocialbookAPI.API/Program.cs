using SocialbookAPI.Application;
using SocialbookAPI.Infrastructure;
using SocialbookAPI.Infrastructure.Services;
using SocialbookAPI.Persistence;
using SocialbookAPI.SignalR;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHubs();


app.Run();
