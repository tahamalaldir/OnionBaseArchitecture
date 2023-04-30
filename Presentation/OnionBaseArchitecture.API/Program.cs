using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OnionBaseArchitecture.Application;
using OnionBaseArchitecture.Application.Common;
using OnionBaseArchitecture.Caching;
using OnionBaseArchitecture.Infrastructure;
using OnionBaseArchitecture.Persistence;
using static Dapper.SqlMapper;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();//Client'tan gelen request neticvesinde oluþturulan HttpContext nesnesine katmanlardaki class'lar üzerinden(busineess logic) eriþebilmemizi saðlayan bir servistir.
builder.Services.AddApplicationServices();
builder.Services.AddCachingServices();
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();

AppSettings appSettings = new AppSettings();
builder.Configuration.GetSection("AppSettings").Bind(appSettings);
builder.Services.Add(new ServiceDescriptor(typeof(AppSettings), appSettings));

ConnectionConfigs connectionConfigs = new ConnectionConfigs();
builder.Configuration.GetSection("ConnectionConfigs").Bind(connectionConfigs);
builder.Services.Add(new ServiceDescriptor(typeof(ConnectionConfigs), connectionConfigs));

CacheConfigs cacheConfigs = new CacheConfigs();
builder.Configuration.GetSection("CacheConfigs").Bind(cacheConfigs);
builder.Services.Add(new ServiceDescriptor(typeof(CacheConfigs), cacheConfigs));

JwtConfigs jwtConfigs = new JwtConfigs();
builder.Configuration.GetSection("JwtConfigs").Bind(jwtConfigs);
builder.Services.Add(new ServiceDescriptor(typeof(JwtConfigs), jwtConfigs));

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(
options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
}).ConfigureApiBehaviorOptions(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});

// JWT authentication Aayarlamasý
var key = Encoding.ASCII.GetBytes(jwtConfigs.TokenKey);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false, //Olu�turulacak token de�erini kimlerin/hangi originlerin/sitelerin kullan�c� belirledi�imiz de�erdir. -> www.bilmemne.com
        ValidateIssuer = false, //Olu�turulacak token de�erini kimin da��tt�n� ifade edece�imiz aland�r. -> www.myapi.com
        ValidateLifetime = true, //Olu�turulan token de�erinin s�resini kontrol edecek olan do�rulamad�r.
        ValidateIssuerSigningKey = true, //�retilecek token de�erinin uygulamam�za ait bir de�er oldu�unu ifade eden suciry key verisinin do�rulanmas�d�r.
        IssuerSigningKey = new SymmetricSecurityKey(key),
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
        ValidAudience = jwtConfigs.Audience,
        ValidIssuer = jwtConfigs.Issuer,
    };

    x.Events = new JwtBearerEvents
    {
        OnChallenge = async context =>
        {
            // Call this to skip the default logic and avoid using the default response
            context.HandleResponse();

            // Write to the response in any way you wish
            context.Response.StatusCode = 401;
            context.Response.Headers.Append("my-custom-header", "custom-value");
            await context.Response.WriteAsync("Yetkisiz erişim!");
        }
    };
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
