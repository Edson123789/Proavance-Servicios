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
    /// Controlador: PreguntaTipoController
    /// Autor: Britsel Calluchi
    /// Fecha: 07/09/2021
    /// <summary>
    /// Gestiona información Interfaz (M) Tipo Pregunta
    /// </summary>
    [Route("api/PreguntaTipo")]
    public class PreguntaTipoController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public PreguntaTipoController()
        {
            _integraDBContext = new integraDBContext();
        }
        /// TipoFuncion: GET
        /// Autor: Britsel Calluchi
        /// Fecha: 07/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Registro de Tipos de Preguntas
        /// </summary>
        /// <returns>List<PreguntaTipoDTO></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTipoPregunta()
        {
            try
            {
                PreguntaTipoRepositorio _repPreguntaTipoRep = new PreguntaTipoRepositorio();
                var listaRespuesta = _repPreguntaTipoRep.ObtenerPreguntaTipo();
                return Ok(listaRespuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Britsel Calluchi
        /// Fecha: 07/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Registros para Combo
        /// </summary>
        /// <returns>Lista de Objeto(int, string)</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombo()
        {
            try
            {
                TipoRespuestaRepositorio _repTipoRespuestaRep = new TipoRespuestaRepositorio();
                var listaRespuesta = _repTipoRespuestaRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, Nombre = x.Nombre }).OrderByDescending(w => w.Id).ToList();
                return Ok(listaRespuesta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Britsel Calluchi
        /// Fecha: 08/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Eliminar Registro de Tipo de Pregunta
        /// </summary>
        /// <param name="Objeto">Información de Id, Usuario</param>
        /// <returns>Retorna StatusCodes, 200 si la eliminación es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarTipoPregunta([FromBody] EliminarDTO Objeto)
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
                    PreguntaTipoRepositorio _repPreguntaTipoRep = new PreguntaTipoRepositorio(_integraDBContext);
                    if (_repPreguntaTipoRep.Exist(Objeto.Id))
                    {
                        _repPreguntaTipoRep.Delete(Objeto.Id, Objeto.NombreUsuario);
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
        /// Insertar Registro de Tipo de Pregunta
        /// </summary>
        /// <param name="Json">Información compuesta de Tipo de Pregunta</param>
        /// <returns>Retorna StatusCodes, 200 si la inserción es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarTipoPregunta([FromBody] PreguntaTipoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PreguntaTipoRepositorio _repPreguntaTipoRep = new PreguntaTipoRepositorio();
                PreguntaTipoBO tipoPregunta = new PreguntaTipoBO();
                using (TransactionScope scope = new TransactionScope())
                {
                    tipoPregunta.Nombre = Json.Nombre;
                    tipoPregunta.IdTipoRespuesta = Json.IdTipoRespuesta;
                    tipoPregunta.Estado = true;
                    tipoPregunta.UsuarioCreacion = Json.Usuario;
                    tipoPregunta.FechaCreacion = DateTime.Now;
                    tipoPregunta.UsuarioModificacion = Json.Usuario;
                    tipoPregunta.FechaModificacion = DateTime.Now;
                    _repPreguntaTipoRep.Insert(tipoPregunta);
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
        /// Actualizar Registro de Tipo Pregunta
        /// </summary>
        /// <param name="Json">Información compuesta de Tipo de Pregunta</param>
        /// <returns>Retorna StatusCodes, 200 si la actualización es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarTipoPregunta([FromBody] PreguntaTipoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PreguntaTipoRepositorio _repPreguntaTipoRep = new PreguntaTipoRepositorio();
                PreguntaTipoBO tipoPregunta = new PreguntaTipoBO();
                tipoPregunta = _repPreguntaTipoRep.FirstById(Json.Id);
                using (TransactionScope scope = new TransactionScope())
                {
                    tipoPregunta.Nombre = Json.Nombre;
                    tipoPregunta.IdTipoRespuesta = Json.IdTipoRespuesta;
                    tipoPregunta.UsuarioModificacion = Json.Usuario;
                    tipoPregunta.FechaModificacion = DateTime.Now;
                    _repPreguntaTipoRep.Update(tipoPregunta);
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