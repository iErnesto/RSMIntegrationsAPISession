using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RSMEnterpriseIntegrationsAPI.Application.Services;
using RSMEnterpriseIntegrationsAPI.Domain.Interfaces;
using RSMEnterpriseIntegrationsAPI.Domain.Models;
using RSMEnterpriseIntegrationsAPI.Infrastructure;
using RSMEnterpriseIntegrationsAPI.Infrastructure.Repositories;
using RSMEnterpriseIntegrationsAPI.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AdvWorksDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyLocalDBConnection"),
        opt => opt.MigrationsAssembly(typeof(AdvWorksDbContext).Assembly.FullName));
});

builder.Services.AddTransient<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddTransient<IDepartmentService, DepartmentService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddTransient<IProductCategoryService, ProductCategoryService>();
builder.Services.AddTransient<ISalesOrderHeaderRepository, SalesOrderHeaderRepository>();
builder.Services.AddTransient<ISalesOrderHeaderService, SalesOrderHeaderService>();
builder.Services.AddTransient<IUserLoginRepository, UserLoginRepository>();
builder.Services.AddTransient<IUserLoginService, UserLoginService>();
builder.Services.AddTransient<IPasswordHasher, BCryptPasswordHasher>();

// Configure JWT settings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
if (jwtSettings != null)
{
    var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
        });
}
else
{
    
    throw new ApplicationException("JwtSettings is not configured correctly.");
}

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
