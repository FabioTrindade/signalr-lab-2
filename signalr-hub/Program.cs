using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using signalr_hub.Configurations;
using signalr_hub.Context;
using signalr_hub.Hubs;
using signalr_hub.Providers;
using signalr_hub.Repositories;
using signalr_hub.Services;

var builder = WebApplication.CreateBuilder(args);

var machine = System.Environment.MachineName;
var redisSettings = builder.Configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>();
var databaseSettings = builder.Configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();

builder.Services.AddDbContext<SignalRDataContext>(options =>
{
    options.UseSqlServer(databaseSettings?.ConnectionString,
        migrations => migrations.MigrationsHistoryTable(string.Concat(databaseSettings.Database, "Migrations")));
});

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
}).AddStackExchangeRedis(redisSettings?.ConnectionString, options =>
{
    options.Configuration.ChannelPrefix = "lab_2_hub";
});

builder.Services.AddCors(options =>
{
    options
        .AddPolicy("CORSPolicy", builder
            => builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .SetIsOriginAllowed((hosts) => true));
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisSettings?.ConnectionString;
});

builder.Services.AddScoped<ICacheProvider, RedisCacheProvider>();
builder.Services.AddScoped<ICaseAlertRepository, CaseAlertRepository>();
builder.Services.AddScoped<IConnectionRepository, ConnectionRepository>();

builder.Services.AddScoped<ICaseAlertService, CaseAlertService>();
builder.Services.AddScoped<IConnectionService, ConnectionService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = machine
    });
});

var app = builder.Build();

var scopeMigrations = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

scopeMigrations.ServiceProvider.GetRequiredService<SignalRDataContext>().Database.Migrate();

app.UseWebSockets();

app.UseSwagger();

app.UseSwaggerUI();

app.UseCors("CORSPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<CaseAlertHub>("/case-alerts-hub");

app.Run();
