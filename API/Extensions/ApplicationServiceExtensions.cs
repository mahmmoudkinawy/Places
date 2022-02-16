namespace API.Extensions;
public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddScoped<IParkRepository, ParkRepository>();

        services.AddScoped<ITrailRepository, TrailRepository>();

        services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

        services.AddDbContext<DataContext>(options =>
            options.UseSqlite(config.GetConnectionString("DefaultConnection")));

        return services;
    }
}

