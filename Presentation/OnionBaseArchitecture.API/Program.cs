using OnionBaseArchitecture.Application;
using OnionBaseArchitecture.Caching;
using OnionBaseArchitecture.Domain.Entities.Common;
using OnionBaseArchitecture.Infrastructure;
using OnionBaseArchitecture.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();//Client'tan gelen request neticvesinde oluþturulan HttpContext nesnesine katmanlardaki class'lar üzerinden(busineess logic) eriþebilmemizi saðlayan bir servistir.
builder.Services.AddApplicationServices();
builder.Services.AddCachingServices();
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();

ConnectionConfigs connectionConfigs = new ConnectionConfigs();
builder.Configuration.GetSection("ConnectionConfigs").Bind(connectionConfigs);
builder.Services.Add(new ServiceDescriptor(typeof(ConnectionConfigs), connectionConfigs));

CacheConfigs cacheConfigs = new CacheConfigs();
builder.Configuration.GetSection("CacheConfigs").Bind(cacheConfigs);
builder.Services.Add(new ServiceDescriptor(typeof(CacheConfigs), cacheConfigs));

// Add services to the container.

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

app.UseAuthorization();

app.MapControllers();

app.Run();
