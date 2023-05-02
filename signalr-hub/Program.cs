using signalr_hub.Configurations;
using signalr_hub.Hubs;
using signalr_hub.Providers;
using signalr_hub.Repositories;
using signalr_hub.Services;

var builder = WebApplication.CreateBuilder(args);

var redisSettings = builder.Configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>();

//builder.Services.AddSignalR().AddStackExchangeRedis(redisSettings?.ConnectionString);

builder.Services.AddSignalR(o => { o.EnableDetailedErrors = true; })
    .AddStackExchangeRedis(redisSettings?.ConnectionString, options =>
    {
        options.Configuration.ChannelPrefix = "dsihub";
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
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseWebSockets();

app.UseSwagger();

app.UseSwaggerUI();

app.UseCors("CORSPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<CaseAlertHub>("/case-alerts-hub");

app.Run();
