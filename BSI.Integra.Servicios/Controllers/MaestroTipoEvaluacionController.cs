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
    /// Controlador: MaestroTipoEvaluacionController
    /// Autor: Britsel Calluchi
    /// Fecha: 07/09/2021
    /// <summary>
    /// Gestiona información Interfaz (M) Tipo de Evaluación
    /// </summary>
    [Route("api/MaestroTipoEvaluacion")]
    public class MaestroTipoEvaluacionController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public MaestroTipoEvaluacionController()
        {
            _integraDBContext = new integraDBContext();
        }
        /// TipoFuncion: GET
        /// Autor: Britsel Calluchi
        /// Fecha: 08/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Registro de Tipos de Evaluación
        /// </summary>
        /// <returns>List de Objeto(int, string, string, string, DateTime, DateTime)</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTipoEvaluacion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EvaluacionTipoRepositorio _repEvaluacionTipo = new EvaluacionTipoRepositorio();
                return Ok(_repEvaluacionTipo.GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre, x.UsuarioCreacion, x.UsuarioModificacion, x.FechaCreacion, x.FechaModificacion }).ToList());
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
        /// Eliminar Registro de Tipo de Evaluación
        /// </summary>
        /// <param name="Objeto">Información de Id, Usuario</param>
        /// <returns>Retorna StatusCodes, 200 si la eliminación es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarEvaluacionTipo([FromBody] EliminarDTO Objeto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    EvaluacionTipoRepositorio _repEvaluacionTipo = new EvaluacionTipoRepositorio(_integraDBContext);
                    if (_repEvaluacionTipo.Exist(Objeto.Id))
                    {
                        _repEvaluacionTipo.Delete(Objeto.Id, Objeto.NombreUsuario);
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
        /// Fecha: 08/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Insertar Registro de Tipo de Evaluación
        /// </summary>
        /// <param name="Json">Información compuesta de Tipo de Evaluación</param>
        /// <returns>Retorna StatusCodes, 200 si la inserción es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarEvaluacionTipo([FromBody] EvaluacionTipoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EvaluacionTipoRepositorio _repEvaluacionTipo = new EvaluacionTipoRepositorio();
                EvaluacionTipoBO evaluacionTipo = new EvaluacionTipoBO();
                using (TransactionScope scope = new TransactionScope())
                {
                    evaluacionTipo.Nombre = Json.Nombre;
                    evaluacionTipo.Estado = true;
                    evaluacionTipo.UsuarioCreacion = Json.UsuarioModificacion;
                    evaluacionTipo.FechaCreacion = DateTime.Now;
                    evaluacionTipo.UsuarioModificacion = Json.UsuarioModificacion;
                    evaluacionTipo.FechaModificacion = DateTime.Now;
                    _repEvaluacionTipo.Insert(evaluacionTipo);
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
        /// Fecha: 08/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualizar Registro de Tipo de Evaluación
        /// </summary>
        /// <param name="Json">Información compuesta de Tipo de Evaluación</param>
        /// <returns>Retorna StatusCodes, 200 si la actualización es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarEvaluacionTipo([FromBody] EvaluacionTipoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EvaluacionTipoRepositorio _repEvaluacionTipo = new EvaluacionTipoRepositorio();
                EvaluacionTipoBO evaluacionTipo = new EvaluacionTipoBO();
                evaluacionTipo = _repEvaluacionTipo.FirstById(Json.Id);
                using (TransactionScope scope = new TransactionScope())
                {
                    evaluacionTipo.Nombre = Json.Nombre;
                    evaluacionTipo.UsuarioModificacion = Json.UsuarioModificacion;
                    evaluacionTipo.FechaModificacion = DateTime.Now;
                    _repEvaluacionTipo.Update(evaluacionTipo);
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