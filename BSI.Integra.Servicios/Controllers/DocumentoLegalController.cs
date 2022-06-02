using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using System.Transactions;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: DocumentoLegalController
    /// Autor: Jashin Salazar
    /// Fecha: 22/07/2017
    /// <summary>
    /// Controlador que contiene los endpoints para la interfaz Docuemntos Legales
    /// </summary>

    [Route("api/DocumentoLegal")]
    public class DocumentoLegalController: ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PaisRepositorio _repPais;
        private readonly DocumentoLegalRepositorio _repDocumentoLegal;
        private readonly PersonalAreaTrabajoRepositorio _repAreaTrabajo;
        public DocumentoLegalController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            _repPais = new PaisRepositorio(_integraDBContext);
            _repDocumentoLegal = new DocumentoLegalRepositorio(_integraDBContext);
            _repAreaTrabajo = new PersonalAreaTrabajoRepositorio(_integraDBContext);
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 22/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos para los combos.
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerCombos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var ListaPais = _repPais.ObtenerListaPais();
                var Areas = _repAreaTrabajo.ObtenerAreaAgenda();
                var Combos = new
                {
                    ListaPais,
                    Areas
                };
                return Ok(Combos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 22/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de documentos legales.
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerTodoDocumentoLegal()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok(_repDocumentoLegal.ObtenerDocumentoLegal());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar
        /// Fecha: 22/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los registros de documentos legales.
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        [HttpGet]
        [Route("[action]/{Area}/{Rol}/{IdAlumno}")]
        public ActionResult ObtenerDocumentoLegalAgenda(int Area, string Rol, int IdAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
                var alumno = _repAlumno.FirstById(IdAlumno);
                return Ok(_repDocumentoLegal.ObtenerDocumentoLegalAgenda(Area,Rol,(int)alumno.IdCodigoPais));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //

        [HttpPost]
        [Route("[action]")]
        public ActionResult InsertarDocumentoLegal([FromBody] DocumentoLegalDTO DocumentoLegal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DocumentoLegalBO doc = new DocumentoLegalBO(_integraDBContext);
                    var res2 = doc.InsertarDocumentoLegal(DocumentoLegal);
                    scope.Complete();
                    return Ok(res2);
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarDocumentoLegal([FromBody] DocumentoLegalDTO DocumentoLegal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DocumentoLegalBO doc = new DocumentoLegalBO(_integraDBContext);
                    var res = doc.ActualizarDocumentoLegal(DocumentoLegal);
                    scope.Complete();
                    return Ok(res);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdDocumentoLegal}/{Usuario}")]
        [HttpGet]
        public ActionResult EliminarDocumentoLegal(int IdDocumentoLegal, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (_repDocumentoLegal.Exist(IdDocumentoLegal))
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        DocumentoLegalBO doc = new DocumentoLegalBO(_integraDBContext);
                        var res = doc.EliminarDocumentoLegal(IdDocumentoLegal,Usuario);
                        scope.Complete();
                        return Ok(res);
                    }
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
