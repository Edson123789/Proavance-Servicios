using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ControlCursosFinalizados
    /// <summary>
    /// Autor: Jose Villena
    /// Fecha: 16/12/2021
    /// <summary>
    /// Gestion de endpoints para el control de cursos Finalizados Online y Presencial
    /// </summary>
    [Route("api/ControlCursosFinalizados")]    
    public class ControlCursosFinalizadosController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PgeneralRepositorio _repPgeneral;
        private readonly PespecificoRepositorio _repPEspecifico;
        private readonly CentroCostoRepositorio _repCentroCosto;
        private readonly EstadoPespecificoRepositorio _repEstadoPespecifico;         
        private readonly PespecificoSesionRepositorio _repPEspecificoSesion;         
        private readonly ObservacionesCursosFinalizadosLogRepositorio _repObservacionesCursosFinalizadosLog;         
        private readonly PEspecificoAprobacionCalificacionRepositorio _repPEspecificoAprobacionCalificacion;         

        public ControlCursosFinalizadosController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repPgeneral = new PgeneralRepositorio(_integraDBContext);
            _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
            _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
            _repEstadoPespecifico = new EstadoPespecificoRepositorio(_integraDBContext);
            _repPEspecificoSesion = new PespecificoSesionRepositorio(_integraDBContext);
            _repObservacionesCursosFinalizadosLog = new ObservacionesCursosFinalizadosLogRepositorio(_integraDBContext);
            _repPEspecificoAprobacionCalificacion = new PEspecificoAprobacionCalificacionRepositorio(_integraDBContext);
        }

        /// Tipo Función: POST
        /// Autor: Jose Villena
        /// Fecha: 16/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los combos para el modulo de Control Cursos Finalizados
        /// </summary>
        /// <returns>Response 200 con objeto anonimo (Lista de objetos de clase PGeneralFiltroDTO, lista de objetos de clase PEspecificoProgramaGeneralFiltroDTO), caso contrario response 400 con el mensaje de error</returns>
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
                var comboPgeneral = _repPgeneral.ObtenerProgramasGeneralesFiltro();
                var comboPespecifico = _repPEspecifico.ObtenerProgramasEspecificosPadres(null);
                var comboCentroCosto = _repCentroCosto.ObtenerCentroCostoPadres(null);                
                var comboEstadoPespecifico = _repEstadoPespecifico.ObtenerEstadoPespecificoParaCombo();

                return Ok(new {
                    ComboPgeneral = comboPgeneral,
                    ComboPespecifico = comboPespecifico,
                    ComboCentroCosto = comboCentroCosto,
                    ComboEstadoPespecifico = comboEstadoPespecifico                    
                });

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
        public ActionResult GenerarReporteControlCursos([FromBody] ControlCursosFiltroDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var controlCursos = _repPEspecificoSesion.GenerarReporteControlCursos(Filtro);
                return Ok(controlCursos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        //[Route("[Action]/{Observacion}/{IdPEspecifico}/{Usuario}")]
        [Route("[Action]")]
        //public ActionResult ActualizarObservaciones(string Observacion, int IdPEspecifico, string Usuario)
        public ActionResult ActualizarObservaciones([FromBody] ObservacionControlCursosDTO json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var pespecifico = _repPEspecifico.FirstById(json.IdPEspecifico);
                if (pespecifico.ObservacionCursoFinalizado == null)
                {
                    PespecificoBO PEspecifico = _repPEspecifico.FirstById(json.IdPEspecifico);
                    PEspecifico.ObservacionCursoFinalizado = json.Observacion;
                    _repPEspecifico.Update(PEspecifico);
                }
                else
                {
                    ObservacionesCursosFinalizadosLogBO observacionCurso = new ObservacionesCursosFinalizadosLogBO();
                    observacionCurso.IdPespecifico = json.IdPEspecifico;
                    observacionCurso.ObservacionCursoFinalizado = json.Observacion;
                    observacionCurso.Estado = true;
                    observacionCurso.UsuarioCreacion = json.Usuario;
                    observacionCurso.UsuarioModificacion = json.Usuario;
                    observacionCurso.FechaCreacion = DateTime.Now;
                    observacionCurso.FechaModificacion = DateTime.Now;
                    _repObservacionesCursosFinalizadosLog.Insert(observacionCurso);

                    PespecificoBO PEspecifico = _repPEspecifico.FirstById(json.IdPEspecifico);
                    PEspecifico.ObservacionCursoFinalizado = json.Observacion;
                    _repPEspecifico.Update(PEspecifico);

                }
                if (json.FechaFinalizacion != null)
                {
                    
                    var pespecificoAprobacion = _repPEspecificoAprobacionCalificacion.GetBy(x => x.IdPespecifico == json.IdPEspecifico);
                    if (pespecificoAprobacion.Count() == 0)
                    {
                        throw new Exception("Error nose tiene al menos una confirmacion del Docente");
                    }
                    else
                    {
                        PEspecificoAprobacionCalificacionBO fechaFinalizacion = new PEspecificoAprobacionCalificacionBO();
                        var idAprobacion = pespecificoAprobacion.First();
                        fechaFinalizacion = _repPEspecificoAprobacionCalificacion.FirstById(idAprobacion.Id);
                        var fecha = Convert.ToDateTime(json.FechaFinalizacion);
                        var fechaActualizada = fecha.AddHours(-5);
                        fechaFinalizacion.FechaCreacion = fechaActualizada;                        
                        _repPEspecificoAprobacionCalificacion.Update(fechaFinalizacion);
                    }
                    
                }

                return Ok(json);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
