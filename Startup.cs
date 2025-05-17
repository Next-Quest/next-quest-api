using Microsoft.EntityFrameworkCore;
using NextQuest.Data;
using NextQuest.Interfaces;
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

        // Dependency Injection
        services.AddScoped<IUserInterface, UserService>();
        services.AddScoped<IAuthInterface, AuthService>();

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

        app.MapControllers();
    }
}
