using System;
using System.Linq;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: InformacionWebinar
    /// <summary>
    /// Autor: Jose Villena - Gian Miranda
    /// Fecha: 12/07/2021
    /// <summary>
    /// Gestion de endpoints para la informacion de webinar y los alumnos
    /// </summary>
    [Route("api/InformacionWebinar")]
    public class InformacionWebinarController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PgeneralRepositorio _repPgeneral;
        private readonly PespecificoRepositorio _repPEspecifico;
        private readonly PespecificoSesionRepositorio _repPEspecificoSesion;

        public InformacionWebinarController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repPgeneral = new PgeneralRepositorio(_integraDBContext);
            _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
            _repPEspecificoSesion = new PespecificoSesionRepositorio(_integraDBContext);
        }

        /// Tipo Función: POST
        /// Autor: Jose Villena
        /// Fecha: 12/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos para el modulo de informacion webinar
        /// </summary>
        /// <returns>Response 200 con objeto anonimo (Lista de objetos de clase PgeneralWebinarDTO, lista de objetos de clase DatosListaPespecificoDTO), caso contrario response 400 con el mensaje de error</returns>
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
                var comboPgeneral = _repPgeneral.ListarProgramasPanel();
                var comboPespecifico = _repPEspecifico.ObtenerListaProgramaEspecificoParaFiltro();

                return Ok(new { comboPgeneral, comboPespecifico });

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Jose Villena
        /// Fecha: 12/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los webinars por filtro
        /// </summary>
        /// <param name="Filtro">Objeto de clase WebinarReporteFiltroDTO</param>
        /// <returns>Response 200 con objeto anonimo (Lista de objetos de clase WebinarDDetalleSesionDTO ordenados, caso contrario response 400 con el mensaje de error</returns>
        [HttpPost]
        [Route("[Action]")]
        public ActionResult ObtenerWebinarPorFiltro([FromBody] WebinarReporteFiltroDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var webinar = _repPEspecificoSesion.ObtenerInformacionSesionesWebinarGrid(Filtro);
                return Ok(webinar.OrderBy(w => w.Fecha));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Villena
        /// Fecha: 12/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los webinars por filtro
        /// </summary>
        /// <param name="IdPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <returns>Response 200 con objeto anonimo (Lista de objetos de clase ComboGenericoDTO, caso contrario response 400 con el mensaje de error</returns>
        [Route("[Action]/{idPGeneral}")]
        [HttpGet]
        public ActionResult ObtenerPEspecificoWebinar(int IdPGeneral)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PespecificoRepositorio pespecificoRepositorio = new PespecificoRepositorio(_integraDBContext);
                return Ok(pespecificoRepositorio.ObtenerPEspecificoWebinar(IdPGeneral));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
