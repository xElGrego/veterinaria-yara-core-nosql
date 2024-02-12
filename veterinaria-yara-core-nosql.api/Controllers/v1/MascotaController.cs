using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using veterinaria_yara_core_nosql.application.interfaces;
using veterinaria_yara_core_nosql.application.models.dtos;
using veterinaria_yara_core_nosql.domain.DTOs;
using veterinaria_yara_core_nosql.domain.entities;
using veterinaria_yara_core_nosql.infrastructure.data.repositories.mascota;

namespace veterinaria_yara_core_nosql.api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class MascotaController : ControllerBase
    {
        private readonly IMascota _mascota;
        public MascotaController(IMascota mascotas)
        {
            _mascota = mascotas ?? throw new ArgumentNullException(nameof(mascotas));
        }

        /// <summary>
        /// Genera la lista de mascotas
        /// </summary>
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<Mascotas>), 200)]
        [ProducesResponseType(typeof(DtoResponseError), 400)]
        [ProducesResponseType(typeof(DtoResponseError), 500)]
        [Route("/v1/veterinaria-yara-nosql/consulta-mascotas")]
        public async Task<ActionResult<List<Mascotas>>> ConsultarMascotas()
        {
            var response = await _mascota.Consultar();
            return Ok(response);
        }


        /// <summary>
        /// Genera la lista de mascotas por fecha
        /// </summary>
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<Mascotas>), 200)]
        [ProducesResponseType(typeof(DtoResponseError), 400)]
        [ProducesResponseType(typeof(DtoResponseError), 500)]
        /// <param name="fechaInicio"> Fecha inicial para realizar la consulta </param
        /// <param name="fechaFin"> Fecha final para realizar la consulta  </param>
        [Route("/v1/veterinaria-yara-nosql/consulta-fecha-mascotas")]
        public async Task<ActionResult<List<Mascotas>>> ConsultarMascotasFecha(DateTime fechaInicio, DateTime fechaFin)
        {
            var response = await _mascota.ConsultarPorFecha(fechaInicio, fechaFin);
            return Ok(response);
        }

        /// <summary>
        /// Método para crea la mascota
        /// </summary>
        /// <param name="mascota"> Objeto para crear una mascota </param>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CrearResponse), 200)]
        [ProducesResponseType(typeof(DtoResponseError), 400)]
        [ProducesResponseType(typeof(DtoResponseError), 500)]
        [Route("/v1/veterinaria-yara-nosql/crear-mascota")]
        public async Task<ActionResult<CrearResponse>> CrearMascota([FromBody][Required] Mascotas mascota)
        {
            var response = await _mascota.Crear(mascota);
            return Ok(response);
        }

        /// <summary>
        /// Método para crea la mascota
        /// </summary>
        /// <param name="mascota"> Objeto para crear una mascota </param>
        [HttpPut]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CrearResponse), 200)]
        [ProducesResponseType(typeof(DtoResponseError), 400)]
        [ProducesResponseType(typeof(DtoResponseError), 500)]
        [Route("/v1/veterinaria-yara-nosql/editar-mascota")]
        public async Task<ActionResult<CrearResponse>> EditarMascota([FromBody][Required] Mascotas mascota)
        {
            var response = await _mascota.Editar(mascota.Id, mascota);
            return Ok(response);
        }


        /// <summary>
        /// Método para crea la mascota
        /// </summary>
        /// <param name="mascota"> Objeto para eliminar una mascota </param>
        [HttpDelete]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CrearResponse), 200)]
        [ProducesResponseType(typeof(DtoResponseError), 400)]
        [ProducesResponseType(typeof(DtoResponseError), 500)]
        [Route("/v1/veterinaria-yara-nosql/eliminar-mascota")]
        public async Task<ActionResult<CrearResponse>> EliminarMascota([FromHeader][Required] string Id)
        {
            var response = await _mascota.Eliminar(ObjectId.Parse(Id));
            return Ok(response);
        }
    }
}
