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
    /// Controlador: CriterioEvaluacionProcesoController
    /// Autor: Luis Huallpa - Edgar Serruto
    /// Fecha: 07/09/2021
    /// <summary>
    /// Gestiona información Interfaz (M) Criterio Evaluación
    /// </summary>
    [Route("api/CriterioEvaluacionProceso")]
    public class CriterioEvaluacionProcesoController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public CriterioEvaluacionProcesoController()
        {
            _integraDBContext = new integraDBContext();
        }
        /// TipoFuncion: GET
        /// Autor: Luis Huallpa - Edgar Serruto
        /// Fecha: 07/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Registro de Criterios de Proceso de Evaluación
        /// </summary>
        /// <returns>List<Object></returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCriterioEvaluacionProceso()
        {
            try
            {
                CriterioEvaluacionProcesoRepositorio _repCriterioEvaluacionProcesoRep = new CriterioEvaluacionProcesoRepositorio();
                ExamenRepositorio _repExamenRep = new ExamenRepositorio();
                var listaRespuesta = _repCriterioEvaluacionProcesoRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, Nombre = x.Nombre}).OrderByDescending(w => w.Id).ToList();
                List<Object> listaCompleta= new List<Object>();
                foreach (var item in listaRespuesta) {
                    var count = _repExamenRep.GetBy(x => x.IdCriterioEvaluacionProceso == item.Id).ToList().Count();
                    listaCompleta.Add(new {Id=item.Id,Nombre=item.Nombre,Relacionado= count >0?true:false});
                }
                return Ok(listaCompleta);
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
        /// Eliminar Criterio de Evaluación de Proceso
        /// </summary>
        /// <param name="Objeto">Información de Criterio de Evaluación de Proceso</param>
        /// <returns>Retorna StatusCodes, 200 si la eliminación es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarCriterioEvaluacionProceso([FromBody] EliminarDTO Objeto)
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
                    CriterioEvaluacionProcesoRepositorio _repCriterioEvaluacionProcesoRep = new CriterioEvaluacionProcesoRepositorio(_integraDBContext);
                    if (_repCriterioEvaluacionProcesoRep.Exist(Objeto.Id))
                    {
                        _repCriterioEvaluacionProcesoRep.Delete(Objeto.Id, Objeto.NombreUsuario);
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
        /// Autor: Luis Huallpa - Edgar Serruto
        /// Fecha: 07/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta Criterio de Evaluación de Proceso
        /// </summary>
        /// <param name="Json">Información Compuesta de Criterio de Evaluación de Proceso</param>
        /// <returns>Retorna StatusCodes, 200 si la inserción es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarCriterioEvaluacionProceso([FromBody] CriterioEvaluacionProcesoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CriterioEvaluacionProcesoRepositorio _repCriterioEvaluacionProcesoRep = new CriterioEvaluacionProcesoRepositorio();
                CriterioEvaluacionProcesoBO criterioEvaluacion = new CriterioEvaluacionProcesoBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    criterioEvaluacion.Nombre = Json.Nombre;
                    criterioEvaluacion.Estado = true;
                    criterioEvaluacion.UsuarioCreacion = Json.Usuario;
                    criterioEvaluacion.FechaCreacion = DateTime.Now;
                    criterioEvaluacion.UsuarioModificacion = Json.Usuario;
                    criterioEvaluacion.FechaModificacion = DateTime.Now;
                    _repCriterioEvaluacionProcesoRep.Insert(criterioEvaluacion);
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
        /// Autor: Luis Huallpa - Edgar Serruto
        /// Fecha: 07/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Actualiza Criterio de Evaluación de Proceso
        /// </summary>
        /// <param name="Json">Información Compuesta de Criterio de Evaluación de Proceso</param>
        /// <returns>Retorna StatusCodes, 200 si la actualización es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarCriterioEvaluacionProceso([FromBody] CriterioEvaluacionProcesoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CriterioEvaluacionProcesoRepositorio _repCriterioEvaluacionProcesoRep = new CriterioEvaluacionProcesoRepositorio();
                CriterioEvaluacionProcesoBO criterioEvaluacion = new CriterioEvaluacionProcesoBO();
                criterioEvaluacion = _repCriterioEvaluacionProcesoRep.FirstById(Json.Id);

                using (TransactionScope scope = new TransactionScope())
                {
                    criterioEvaluacion.Nombre = Json.Nombre;
                    criterioEvaluacion.UsuarioModificacion = Json.Usuario;
                    criterioEvaluacion.FechaModificacion = DateTime.Now;
                    _repCriterioEvaluacionProcesoRep.Update(criterioEvaluacion);
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