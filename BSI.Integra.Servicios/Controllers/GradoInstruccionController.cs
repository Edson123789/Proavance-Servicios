using System;
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
    /// Controlador: GradoInstruccionController
    /// Autor: Britsel Calluchi
    /// Fecha: 08/09/2021
    /// <summary>
    /// Gestiona información Interfaz (M) Estado Formación
    /// </summary>
    [Route("api/GradoInstruccion")]
    public class GradoInstruccionController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private readonly GradoEstudioRepositorio _repGradoEstudio;
        public GradoInstruccionController()
        {
            _integraDBContext = new integraDBContext();
            _repGradoEstudio = new GradoEstudioRepositorio(_integraDBContext);
        }
        /// TipoFuncion: GET
        /// Autor: Britsel Calluchi
        /// Fecha: 08/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Registros de Grados de Formación
        /// </summary>
        /// <returns>Lista de Objetos(int,string)</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerGradoInstruccion()
        {
            try
            {
                var listaGradoInstruccion=_repGradoEstudio.GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre }).ToList();
                return Ok(listaGradoInstruccion);
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
        /// Elimina Registro de Grado de Formación
        /// </summary>
        /// <param name="Objeto"></param>
        /// <returns>Retorna StatusCodes, 200 si la eliminación es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarGradoInstruccion([FromBody] EliminarDTO Objeto)
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
                    if (_repGradoEstudio.Exist(Objeto.Id))
                    {
                        _repGradoEstudio.Delete(Objeto.Id, Objeto.NombreUsuario);
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
        /// Insertar Registro de Grado de Formación
        /// </summary>
        /// <param name="Json">Información compuesta de Grado de Formación</param>
        /// <returns>Retorna StatusCodes, 200 si la inserción es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarGradoInstruccion([FromBody] GenericoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GradoEstudioBO gradoInstruccion = new GradoEstudioBO();
                using (TransactionScope scope = new TransactionScope())
                {
                    gradoInstruccion.Nombre = Json.Nombre;
                    gradoInstruccion.Estado = true;
                    gradoInstruccion.UsuarioCreacion = Json.Usuario;
                    gradoInstruccion.FechaCreacion = DateTime.Now;
                    gradoInstruccion.UsuarioModificacion = Json.Usuario;
                    gradoInstruccion.FechaModificacion = DateTime.Now;

                    _repGradoEstudio.Insert(gradoInstruccion);
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
        /// Actualizar Registro de Grado de Formación
        /// </summary>
        /// <param name="Json">Información compuesta de Grado de Formación</param>
        /// <returns>Retorna StatusCodes, 200 si la actualización es exitosa con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarGradoInstruccion([FromBody] GenericoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GradoEstudioBO gradoInstruccion = new GradoEstudioBO();
                gradoInstruccion = _repGradoEstudio.FirstById(Json.Id);
                using (TransactionScope scope = new TransactionScope())
                {
                    gradoInstruccion.Nombre = Json.Nombre;
                    gradoInstruccion.UsuarioModificacion = Json.Usuario;
                    gradoInstruccion.FechaModificacion = DateTime.Now;
                    _repGradoEstudio.Update(gradoInstruccion);
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