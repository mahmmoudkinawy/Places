namespace API.Extensions;
public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration config)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

        services.AddDbContext<DataContext>(options =>
            options.UseSqlite(config.GetConnectionString("DefaultConnection")));

        return services;
    }
}

