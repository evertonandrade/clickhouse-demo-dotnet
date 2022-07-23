using ClickHouse.Demo.Data;
using ClickHouse.Demo.Models;
using ClickHouse.EntityFrameworkCore.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ClickHouse");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseClickHouse(connectionString)
);

var app = builder.Build();

app.MapGet("/", () => "ClickHouse Demo!");

app.MapGet("users", async (AppDbContext dbContext) =>
{
    var users = await dbContext.Users.ToListAsync();
    return Results.Ok(users);
});

app.MapPost("users", async (AppDbContext dbContext, User user) =>
{
    await dbContext.Users.AddAsync(user);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/users/{user.Id}", user);
});

app.Run();
