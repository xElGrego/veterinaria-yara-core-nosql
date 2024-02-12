using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using veterinaria_yara_core_nosql.application.interfaces;
using veterinaria_yara_core_nosql.application.models.exceptions;
using veterinaria_yara_core_nosql.domain.DTOs;
using veterinaria_yara_core_nosql.domain.entities;

namespace veterinaria_yara_core_nosql.infrastructure.data.repositories.mascota
{
    public class MascotaRepository : IMascota
    {
        private readonly IMongoCollection<Mascotas> _mascotas;
        private ILogger<MascotaRepository> _logger;

        public MascotaRepository(ILogger<MascotaRepository> logger, IMongoClient client)
        {
            var database = client.GetDatabase("VeterinariaYara");
            var collection = database.GetCollection<Mascotas>(nameof(Mascotas));
            _mascotas = collection;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<Mascotas>> Consultar()
        {
            try
            {
                var mascotas = await _mascotas.Find(_ => true).ToListAsync();
                return mascotas;
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar mascotas", ex.Message);
                throw new VeterinariaYaraNoSqlException(ex.Message);
            }
        }

        public async Task<List<Mascotas>> ConsultarPorFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                var filter = Builders<Mascotas>.Filter.And(
                    Builders<Mascotas>.Filter.Gte(m => m.Fecha, fechaInicio),
                    Builders<Mascotas>.Filter.Lte(m => m.Fecha, fechaFin)
                );
                var mascotas = await _mascotas.Find(filter).ToListAsync();
                return mascotas;
            }
            catch (Exception ex)
            {
                _logger.LogError("Consultar mascotas por rango de fechas", ex.Message);
                throw new VeterinariaYaraNoSqlException(ex.Message);
            }
        }

        public async Task<CrearResponse> Crear(Mascotas mascota)
        {
            try
            {
                mascota.Fecha.ToString("yyyy-MM-dd");
                await _mascotas.InsertOneAsync(mascota);

                var response = new CrearResponse
                {
                    Response = "La mascota fue creada con éxito"
                };
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Crear mascotas", ex.Message);
                throw new VeterinariaYaraNoSqlException(ex.Message);
            }
        }

        public async Task<CrearResponse> Editar(ObjectId id, Mascotas mascota)
        {
            try
            {
                var filter = Builders<Mascotas>.Filter.Eq(x => x.Id, id);
                var updatet = Builders<Mascotas>.Update
                    .Set(x => x.Nombre, mascota.Nombre)
                    .Set(x => x.Peso, mascota.Peso)
                    .Set(x => x.Raza, mascota.Raza)
                    .Set(x => x.Edad, mascota.Edad);

                await _mascotas.UpdateOneAsync(filter, updatet);

                var response = new CrearResponse
                {
                    Response = "La mascota fue editada con éxito"
                };
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Editar mascotas", ex.Message);
                throw new VeterinariaYaraNoSqlException(ex.Message);
            }
        }

        public async Task<CrearResponse> Eliminar(ObjectId id)
        {
            try
            {
                var filter = Builders<Mascotas>.Filter.Eq(x => x.Id, id);
                await _mascotas.DeleteOneAsync(filter);

                var response = new CrearResponse
                {
                    Response = "La mascota fue eliminada con éxito"
                };
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError("Eliminar mascotas", ex.Message);
                throw new VeterinariaYaraNoSqlException(ex.Message);
            }
        }
    }
}
