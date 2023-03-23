using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RRHH.API;
using RRHH.API.Data;
using RRHH.API.Data.Entities;
using RRHH.API.UserManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(opt =>
{
    var cnstring = $@"Filename=Data\DB\{builder.Configuration.GetConnectionString("LocalDB")}";
    opt.UseSqlite(cnstring);
    //opt.UseSqlServer(builder.Configuration.GetConnectionString("SQLRRHHContext"));
});

//Add services
builder.Services.AddIdentity<AplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders()
                .AddRoleManager<RoleManager<IdentityRole>>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireNonAlphanumeric=false;
    options.Password.RequireDigit=false;
    options.Password.RequireLowercase=false;
    options.Password.RequireUppercase=false;
});

builder.Services.AddScoped<UsersService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//Create DB
using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetService<DataContext>();
    ctx.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
});

app.MapPost("/Login", async (string username, string password, UsersService usersService) =>
{
    var user = await usersService.Login(username, password);

    if (user is null)
        return new ApiResult(false, Errors: new List<string> { "Usuario o password incorrecto" });
 
    return new ApiResult(true,
                         Data: user);

});

app.MapGet("/allemployees", () =>
{
    return "Hello All Employees";
});

app.SeedIdentityData();

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}