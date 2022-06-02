using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ExamenFeedbackController
    /// Autor: Britsel Calluchi
    /// Fecha: 07/09/2021
    /// <summary>
    /// Gestiona información Interfaz (M) Feedback URL
    /// </summary>
    [Route("api/ExamenFeedback")]
    public class ExamenFeedbackController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public ExamenFeedbackController()
        {
            _integraDBContext = new integraDBContext();
        }
        /// TipoFuncion: GET
        /// Autor: Britsel Calluchi
        /// Fecha: 07/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Registro Feedback para examenes
        /// </summary>
        /// <returns>Lista de Objeto(int, string, string)</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerExamenFeedback()
        {
            try
            {
                ExamenFeedbackRepositorio _repExamenFeedbackRep = new ExamenFeedbackRepositorio();
                var listaRespuesta = _repExamenFeedbackRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, Nombre = x.Nombre, Url = x.Url }).OrderByDescending(w => w.Id).ToList();
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
        /// Eliminar FeedBack de Examen
        /// </summary>
        /// <param name="ExamenFeedback">Información de Id, Usuario</param>
        /// <returns>Retorna StatusCodes, 200 si la eliminación es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarExamenFeedback([FromBody] EliminarDTO ExamenFeedback)
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
                    ExamenFeedbackRepositorio _repExamenFeedbackRep = new ExamenFeedbackRepositorio(_integraDBContext);
                    if (_repExamenFeedbackRep.Exist(ExamenFeedback.Id))
                    {
                        _repExamenFeedbackRep.Delete(ExamenFeedback.Id, ExamenFeedback.NombreUsuario);
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
        /// Insertar FeedBack de Examen
        /// </summary>
        /// <param name="Json">Información compuesta de FeedBack de Examen</param>
        /// <returns>Retorna StatusCodes, 200 si la inserción es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarExamenFeedback([FromBody] ExamenFeedbackDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExamenFeedbackRepositorio _repExamenFeedbackRep = new ExamenFeedbackRepositorio();
                ExamenFeedbackBO examenFeedback = new ExamenFeedbackBO();
                using (TransactionScope scope = new TransactionScope())
                {
                    examenFeedback.Nombre = Json.Nombre;
                    examenFeedback.Url = Json.Url;
                    examenFeedback.Estado = true;
                    examenFeedback.UsuarioCreacion = Json.Usuario;
                    examenFeedback.FechaCreacion = DateTime.Now;
                    examenFeedback.UsuarioModificacion = Json.Usuario;
                    examenFeedback.FechaModificacion = DateTime.Now;
                    _repExamenFeedbackRep.Insert(examenFeedback);
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
        /// Actualizar FeedBack de Examen
        /// </summary>
        /// <param name="Json">Información compuesta de FeedBack de Examen</param>
        /// <returns>Retorna StatusCodes, 200 si la actualización es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarExamenFeedback([FromBody] ExamenFeedbackDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ExamenFeedbackRepositorio _repExamenFeedbackRep = new ExamenFeedbackRepositorio();
                ExamenFeedbackBO examenFeedback = new ExamenFeedbackBO();
                examenFeedback = _repExamenFeedbackRep.FirstById(Json.Id);
                using (TransactionScope scope = new TransactionScope())
                {
                    examenFeedback.Nombre = Json.Nombre;
                    examenFeedback.Url = Json.Url;
                    examenFeedback.UsuarioModificacion = Json.Usuario;
                    examenFeedback.FechaModificacion = DateTime.Now;
                    _repExamenFeedbackRep.Update(examenFeedback);
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