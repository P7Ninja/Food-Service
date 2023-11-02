using FoodService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextPool<FoodServiceContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("FoodServiceDb") 
        ?? throw new InvalidOperationException("Connection string 'FoodServiceDb' not found")));

// allows browsers to access the api. Can be deleted later when api gateway is set up
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "All",
        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

builder.Services.AddOutputCache();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(settings =>
    {
        var gatewayUrl = Environment.GetEnvironmentVariable("GATEWAY_URL");
        if (gatewayUrl != null)
        {
            settings.PreSerializeFilters.Add((swagger, httpReq) =>
            {
                swagger.Servers.Clear();
                var serverUrl = $"{httpReq.Scheme}://{gatewayUrl}";
                swagger.Servers.Add(new OpenApiServer { Url = serverUrl });
            });
        }
    });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// allows browsers to access the api. Can be deleted later when api gateway is set up
app.UseCors("All");

app.UseOutputCache();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<FoodServiceContext>();
    await db.Database.EnsureCreatedAsync();
}

app.Run();
