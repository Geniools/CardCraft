using CardCraftServer.Hubs;
using CardCraftServer.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
}); 

builder.Services.AddSingleton<OnlineGameManagerDatabase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.MapHub<GameHub>("/gameHub");

app.Run();