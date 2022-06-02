using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: MaestroContratoEstado
    /// Autor: Luis Huallpa - Edgar Serruto
    /// Fecha: 07/09/2021
    /// <summary>
    /// Gestiona información Interfaz (M) Estado de Contrato
    /// </summary>
    [Route("api/MaestroContratoEstado")]
    [ApiController]
    public class MaestroContratoEstadoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly ContratoEstadoRepositorio _repContratoEstado;

        public MaestroContratoEstadoController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            _repContratoEstado = new ContratoEstadoRepositorio(_integraDBContext);
        }
        /// TipoFuncion: POST
        /// Autor: Luis Huallpa - Edgar Serruto
        /// Fecha: 07/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Perfiles de  Estado de Contrato Registrados
        /// </summary>
        /// <returns>List<ContratoEstadoDTO></returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerContratoEstadoRegistrados()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var listaContratoEstado = _repContratoEstado.ObtenerContratoEstadoRegistrado();
                return Ok(listaContratoEstado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Luis Huallpa - Edgar Serruto
        /// Fecha: 07/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta Registros de Estado de Contrato
        /// </summary>
        /// <param name="ContratoEstado">Información Compuesta de Estado de Contrato</param>
        /// <returns>Retorna StatusCodes, 200 si la inserción es exitosa con Bool de confirmación</returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult InsertarContratoEstado([FromBody] ContratoEstadoDTO ContratoEstado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ContratoEstadoBO contratoEstado = new ContratoEstadoBO()
                {
                    Nombre = ContratoEstado.Nombre,
                    Estado = true,
                    UsuarioCreacion = ContratoEstado.UsuarioCreacion,
                    UsuarioModificacion = ContratoEstado.UsuarioModificacion,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                var resultado = _repContratoEstado.Insert(contratoEstado);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Luis Huallpa - Edgar Serruto
        /// Fecha: 07/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Registros de Estado de Contrato
        /// </summary>
        /// <param name="ContratoEstado">Información Compuesta de Estado de Contrato</param>
        /// <returns>Retorna StatusCodes, 200 si la actualización es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarContratoEstado([FromBody] ContratoEstadoDTO ContratoEstado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var contratoEstado = _repContratoEstado.FirstById(ContratoEstado.Id);
                contratoEstado.Nombre = ContratoEstado.Nombre;
                contratoEstado.UsuarioModificacion = ContratoEstado.UsuarioModificacion;
                contratoEstado.FechaModificacion = DateTime.Now;
                var resultado = _repContratoEstado.Update(contratoEstado);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Luis Huallpa - Edgar Serruto
        /// Fecha: 07/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina Registros de Estado de Contrato
        /// </summary>
        /// <param name="ContratoEstado">Información Id, Usuario de registro</param>
        /// <returns>Retorna StatusCodes, 200 si la eliminación es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarContratoEstado([FromBody] EliminarDTO ContratoEstado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (_repContratoEstado.Exist(ContratoEstado.Id))
                {
                    var resultado = _repContratoEstado.Delete(ContratoEstado.Id, ContratoEstado.NombreUsuario);
                    return Ok(resultado);
                }
                else
                {
                    return BadRequest("La categoria a eliminar no existe o ya fue eliminada.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
