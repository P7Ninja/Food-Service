using FoodService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FoodServiceContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("FoodServiceContextSQLServer") 
        ?? throw new InvalidOperationException("Connection string 'FoodServiceContext' not found.")));

// allows browsers to access the api. Can be deleted later when api gateway is set up
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "All",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

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

app.UseHttpsRedirection();

// allows browsers to access the api. Can be deleted later when api gateway is set up
app.UseCors("All");

app.UseAuthorization();

app.MapControllers();

// update/create database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<FoodServiceContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
        Console.WriteLine("Updated db");
    }
}

app.Run();
