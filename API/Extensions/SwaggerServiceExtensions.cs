namespace API.Extensions;
public static class SwaggerServiceExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Places API",
                Version = "v1",
                Description = "It's a very simple API for implementing CRUD for Places",
                Contact = new OpenApiContact
                {
                    Name = "Mahmmoud Kinawy",
                    Email = "mahmmoudkinawy@gmail.com"
                }
            });
        });

        return services;
    }
}
