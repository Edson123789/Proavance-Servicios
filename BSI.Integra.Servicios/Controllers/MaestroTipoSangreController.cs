using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: MaestroTipoSangreController
    /// Autor: Luis Huallpa - Edgar Serruto
    /// Fecha: 07/09/2021
    /// <summary>
    /// Gestiona información Interfaz (M) Tipo Sangre
    /// </summary>
    [Route("api/MaestroTipoSangre")]
    [ApiController]
    public class MaestroTipoSangreController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly TipoSangreRepositorio _repTipoSangre;

        public MaestroTipoSangreController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            _repTipoSangre = new TipoSangreRepositorio(_integraDBContext);
        }
        /// TipoFuncion: POST
        /// Autor: Luis Huallpa - Edgar Serruto
        /// Fecha: 07/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Registros de Tipo de Sangre
        /// </summary>
        /// <returns>List<TipoSangreDTO></returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerTipoSangreRegistrados()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var listaTipoSangre = _repTipoSangre.ObtenerTipoSangreRegistrado();
                return Ok(listaTipoSangre);
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
        /// Inserta Registros de Tipos de Sangre
        /// </summary>
        /// <param name="TipoSangre">Información Compuesta de Tipos de Sangre</param>
        /// <returns>Retorna StatusCodes, 200 si la inserción es exitosa con Bool de confirmación</returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult InsertarTipoSangre([FromBody] TipoSangreDTO TipoSangre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
               TipoSangreBO tipoSangre = new TipoSangreBO()
               {
                    TipoSangre = TipoSangre.TipoSangre,
                    Comentario = TipoSangre.Comentario,                    
                    Estado = true,
                    UsuarioCreacion = TipoSangre.Usuario,
                    UsuarioModificacion = TipoSangre.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                var resultado = _repTipoSangre.Insert(tipoSangre);
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
        /// Actualiza Registros de Tipos de Sangre
        /// </summary>
        /// <param name="TipoSangre">Información Compuesta de Tipos de Sangre</param>
        /// <returns>Retorna StatusCodes, 200 si la actualización es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarTipoSangre([FromBody] TipoSangreDTO TipoSangre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var tipoSangre = _repTipoSangre.FirstById(TipoSangre.Id);
                tipoSangre.TipoSangre = TipoSangre.TipoSangre;
                tipoSangre.Comentario = TipoSangre.Comentario;
                tipoSangre.UsuarioModificacion = TipoSangre.Usuario;
                tipoSangre.FechaModificacion = DateTime.Now;
                var resultado = _repTipoSangre.Update(tipoSangre);
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Edgar Serruto
        /// Fecha: 07/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina Registros de Tipo de Sangre
        /// </summary>
        /// <param name="TipoSangre">Información Id, Usuario de registro</param>
        /// <returns>Retorna StatusCodes, 200 si la eliminación es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarTipoSangre([FromBody] EliminarDTO TipoSangre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (_repTipoSangre.Exist(TipoSangre.Id))
                {
                    var resultado = _repTipoSangre.Delete(TipoSangre.Id, TipoSangre.NombreUsuario);
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
