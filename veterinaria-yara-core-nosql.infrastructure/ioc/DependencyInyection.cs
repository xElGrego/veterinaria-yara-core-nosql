using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using veterinaria_yara_core_nosql.application.interfaces;
using veterinaria_yara_core_nosql.infrastructure.data.repositories.mascota;

namespace veterinaria_yara_core_nosql.infrastructure.ioc
{
    public static class DependencyInyection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoClient = new MongoClient(configuration.GetConnectionString("MongoDb"));
            services.AddSingleton<IMongoClient>(mongoClient);
            services.AddScoped<IMascota, MascotaRepository>();
            return services;
        }
    }
}
