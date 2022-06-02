using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: ReporteCompromisoPagoAlumno
    /// Autor: Jose Villena
    /// Fecha: 07/05/2021        
    /// <summary>
    /// Controlador de Reporte de Compromiso Pago Alumno
    /// </summary>
    [Route("api/ReporteCompromisoPagoAlumno")]
    [ApiController]
    public class ReporteCompromisoPagoAlumnoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;        
        private readonly CentroCostoRepositorio _repCentroCosto;
        private readonly PersonalRepositorio _repPersonal;
        private readonly MontoPagoCronogramaRepositorio _repMontoPagoCronograma;

        public ReporteCompromisoPagoAlumnoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;          
            _repPersonal = new PersonalRepositorio(_integraDBContext);
            _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
            _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio(_integraDBContext);
        }

        /// TipoFuncion: POST
        /// Autor: Jose Villena
        /// Fecha: 07/05/2021   
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combos para el Reporte Compromiso Pago Alumno
        /// </summary>
        /// <returns> Lista de Objetos : comboPersonal,comboCentroCosto </returns>
        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombos(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //var comboPersonal = _repPersonal.ObtenerCoordinadoresParaFiltro();
                var comboPersonal = _repPersonal.ObtenerPersonalAsignadoOperacionesTotalV2(IdPersonal);                
                var comboCentroCosto = _repCentroCosto.ObtenerCentroCostoParaFiltro();

                return Ok(new { comboPersonal, comboCentroCosto });

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// TipoFuncion: POST
        /// Autor: Jose Villena
        /// Fecha: 07/05/2021   
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combos para el Reporte Compromiso Pago Alumno
        /// </summary>
        /// <param name="Obj"> Objetos Filtro Reporte </param>
        /// <returns> Objeto resultado filtro : ResultadoFiltroReporteCompromisoDTO </returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerReporteCompromiso(GenerarReporteCompromisoPagoFiltroGrillaDTO Obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ResultadoFiltroReporteCompromisoDTO compromiso = new ResultadoFiltroReporteCompromisoDTO();
                if (Obj.Filtro.ListaCoordinador.Count() == 0)
                {
                    var asistentesCargo = _repPersonal.ObtenerPersonalAsignadoOperacionesTotalV2(Obj.Filtro.Personal);
                    List<int> ListaCoordinadortmp = new List<int>();
                    foreach (var item in asistentesCargo)
                    {
                        ListaCoordinadortmp.Add(item.Id);
                    }
                    Obj.Filtro.ListaCoordinador = ListaCoordinadortmp;
                    compromiso = _repMontoPagoCronograma.ObtenerReporteCompromisoPagoFiltrado(Obj.Paginador, Obj.Filtro, Obj.Filter);
                }
                else
                {
                    compromiso = _repMontoPagoCronograma.ObtenerReporteCompromisoPagoFiltrado(Obj.Paginador, Obj.Filtro, Obj.Filter);
                }
                return Ok(compromiso);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
