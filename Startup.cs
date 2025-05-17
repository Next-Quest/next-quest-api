using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NextQuest.Data;
using NextQuest.Interfaces;
using NextQuest.Models;
using NextQuest.Services;

namespace NextQuest;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Database
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        services.Configure<MongoDBSettings>(Configuration.GetSection("MongoDB"));
        
        // MongoDB Services
        services.AddSingleton<PostService>();
        
        // Dependency Injection
        services.AddScoped<IUserInterface, UserService>();
        services.AddScoped<IAuthInterface, AuthService>();
        services.AddScoped<IPostInterface, PostService>();

        // Authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    ClockSkew = TimeSpan.Zero,
                };
            });
        services.AddAuthorization();
        
        // Controllers
        services.AddControllers();

        // Swagger
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new() { Title = "NextQuest.Api", Version = "v1" }); });
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "NextQuest.Api v1"); });

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();
    }
}
