using MongoDB.Bson;
using veterinaria_yara_core_nosql.domain.DTOs;
using veterinaria_yara_core_nosql.domain.entities;

namespace veterinaria_yara_core_nosql.application.interfaces
{
    public interface IMascota
    {
        Task<CrearResponse> Crear(Mascotas mascota);
        Task<List<Mascotas>> Consultar();
        Task<List<Mascotas>> ConsultarPorFecha(DateTime fechaInicio, DateTime fechaFin);
        Task<CrearResponse> Editar(ObjectId id, Mascotas mascota);
        Task<CrearResponse> Eliminar(ObjectId id);
    }
}