using ChatApp.Hubs;
using ChatApp.Services;
using ChatApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials();
    });
});
var redisConnectionString = builder.Configuration.GetSection("Redis")["ConnectionString"];
builder.Services.AddSingleton<IRedisService>(new RedisService(redisConnectionString));
builder.Services.AddTransient<IChatService, ChatService>();

var app = builder.Build();

app.UseCors("AllowAll");

app.MapHub<ChatHub>("/chatHub");

app.Run();
