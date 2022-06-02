using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: TipoRespuestaController
    /// Autor: Britsel Calluchi
    /// Fecha: 07/09/2021
    /// <summary>
    /// Gestiona información Interfaz (M) Tipo Respuesta
    /// </summary>
    [Route("api/TipoRespuesta")]
    public class TipoRespuestaController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public TipoRespuestaController()
        {
            _integraDBContext = new integraDBContext();
        }
        /// TipoFuncion: GET
        /// Autor: Britsel Calluchi
        /// Fecha: 07/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Registro de Tipos de Respuesta
        /// </summary>
        /// <returns>Lista de Objeto(int, string, string)</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTipoRespuesta()
        {
            try
            {
                TipoRespuestaRepositorio _repTipoRespuesta = new TipoRespuestaRepositorio();
                var listaRespuesta = _repTipoRespuesta.GetBy(x => x.Estado == true, x => new { Id = x.Id, Nombre = x.Nombre,Descripcion=x.Descripcion }).OrderByDescending(w => w.Id).ToList();
                return Ok(listaRespuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Britsel Calluchi
        /// Fecha: 07/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Eliminar Tipo de Respuesta
        /// </summary>
        /// <param name="TipoRespuesta">Información de Id, Usuario</param>
        /// <returns>Retorna StatusCodes, 200 si la eliminación es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarTipoRespuesta([FromBody] EliminarDTO TipoRespuesta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext _integraDBContext = new integraDBContext();
                using (TransactionScope scope = new TransactionScope())
                {
                    TipoRespuestaRepositorio _repTipoRespuesta = new TipoRespuestaRepositorio(_integraDBContext);
                    if (_repTipoRespuesta.Exist(TipoRespuesta.Id))
                    {
                        _repTipoRespuesta.Delete(TipoRespuesta.Id, TipoRespuesta.NombreUsuario);
                        scope.Complete();
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("Registro no existente");
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Britsel Calluchi
        /// Fecha: 07/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Insertar Tipo de Respuesta
        /// </summary>
        /// <param name="Json">Información compuesta de Tipo de Respuesta</param>
        /// <returns>Retorna StatusCodes, 200 si la inserción es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarTipoRespuesta([FromBody] TipoRespuestaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoRespuestaRepositorio _repTipoRespuesta = new TipoRespuestaRepositorio();
                TipoRespuestaBO tipoRespuesta = new TipoRespuestaBO();
                using (TransactionScope scope = new TransactionScope())
                {
                    tipoRespuesta.Nombre = Json.Nombre;
                    tipoRespuesta.Descripcion = Json.Descripcion;
                    tipoRespuesta.Estado = true;
                    tipoRespuesta.UsuarioCreacion = Json.Usuario;
                    tipoRespuesta.FechaCreacion = DateTime.Now;
                    tipoRespuesta.UsuarioModificacion = Json.Usuario;
                    tipoRespuesta.FechaModificacion = DateTime.Now;
                    _repTipoRespuesta.Insert(tipoRespuesta);
                    scope.Complete();
                }
                string rpta = "INSERTADO CORRECTAMENTE";
                return Ok(new { rpta });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Britsel Calluchi
        /// Fecha: 07/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualizar Tipo de Respuesta
        /// </summary>
        /// <param name="Json">Información compuesta de Tipo de Respuesta</param>
        /// <returns>Retorna StatusCodes, 200 si la actualización es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarTipoRespuesta([FromBody] TipoRespuestaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                TipoRespuestaRepositorio _repTipoRespuesta = new TipoRespuestaRepositorio();
                TipoRespuestaBO tipoRespuesta = new TipoRespuestaBO();
                tipoRespuesta = _repTipoRespuesta.FirstById(Json.Id);
                using (TransactionScope scope = new TransactionScope())
                {
                    tipoRespuesta.Nombre = Json.Nombre;
                    tipoRespuesta.Descripcion = Json.Descripcion;
                    tipoRespuesta.UsuarioModificacion = Json.Usuario;
                    tipoRespuesta.FechaModificacion = DateTime.Now;
                    _repTipoRespuesta.Update(tipoRespuesta);
                    scope.Complete();
                }
                string rpta = "ACTUALIZADO CORRECTAMENTE";
                return Ok(new { rpta });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
}