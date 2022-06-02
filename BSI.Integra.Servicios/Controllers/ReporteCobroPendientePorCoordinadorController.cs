using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteCobroPendientePorCoordinador")]
    public class ReporteCobroPendientePorCoordinadorController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public ReporteCobroPendientePorCoordinadorController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosPendientes()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PeriodoRepositorio _repPeriodo = new PeriodoRepositorio(_integraDBContext);
                PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
                AreaCapacitacionRepositorio _repAreaCapacitacion = new AreaCapacitacionRepositorio(_integraDBContext);
                SubAreaCapacitacionRepositorio _repSubAreaCapacitacion = new SubAreaCapacitacionRepositorio(_integraDBContext);
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio(_integraDBContext);
                //PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
                ModalidadCursoRepositorio _repModalidadCurso = new ModalidadCursoRepositorio(_integraDBContext);
                TroncalCiudadRepositorio _repTroncalCiudad = new TroncalCiudadRepositorio(_integraDBContext);
				CentroCostoRepositorio _repCentroCosto = new CentroCostoRepositorio();

                CombosCobroPendientePorCoordinadorDTO comboPendiente = new CombosCobroPendientePorCoordinadorDTO();

                comboPendiente.ListaPeriodo = _repPeriodo.ObtenerPeriodosPendiente();
                comboPendiente.ListaCoordinador = _repPersonal.ObtenerCoordinadoresOperaciones();
                comboPendiente.ListaAreaCapacitacion = _repAreaCapacitacion.ObtenerTodoFiltro();
                comboPendiente.ListaSubAreaCapacitacion = _repSubAreaCapacitacion.ObtenerSubAreasParaFiltro();
                comboPendiente.ListaProgramaGeneral = _repPGeneral.ObtenerProgramasFiltroDeSubAreasCapacitacion();
				//comboPendiente.ListaProgramaEspecifico = _repPEspecifico.ObtenerListaProgramaEspecificoParaFiltroDeProgramaGeneral();
				comboPendiente.ListaCentroCosto = _repCentroCosto.ObtenerListaCentroCostoParaFiltroDeProgramaGeneral();
				comboPendiente.ListaModalidades = _repModalidadCurso.ObtenerModalidadCursoFiltro();
                comboPendiente.ListaSede = _repTroncalCiudad.ObtenerTroncalCiudad();

                return Ok(comboPendiente);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReportePendienteCobroPorCoordinadorDTO FiltroPendientePorCobro)
        {
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
                TroncalCiudadRepositorio _repTroncalCiudad = new TroncalCiudadRepositorio(_integraDBContext);

                if (FiltroPendientePorCobro.Coordinadora.Count() == 0)
                {
                    var sincoordinador = "0";
                    FiltroPendientePorCobro.Coordinadora = _repPersonal.ObtenerCoordinadoresOperaciones().Select(w => w.Usuario).ToList();
                    FiltroPendientePorCobro.Coordinadora.Add(sincoordinador);
                }
                if (FiltroPendientePorCobro.Modalidad.Count == 0)
                {
                    FiltroPendientePorCobro.Modalidad = "Online Asincronica,Presencial,Online Sincronica".Split(",").ToList();
                }
                if (FiltroPendientePorCobro.Sede.Count == 0)
                {
                    FiltroPendientePorCobro.Sede = _repTroncalCiudad.ObtenerTroncalCiudad().Select(w => w.Id).ToList();
                }

                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();
                PersonalRepositorio repPersonal = new PersonalRepositorio();
                var pendienteCobro=(IEnumerable<ReportePendienteCobroCoordinadorDTO>)null;
                
                if (FiltroPendientePorCobro.ValorTipoSaldo.Equals("Saldo Pendiente"))
                {
                     pendienteCobro = reportesRepositorio.ObtenerReportePendienteCobroPorCoordinadora(FiltroPendientePorCobro).Where(x=> x.SaldoPendienteDolares>0).OrderBy(x => x.FechaVencimiento);
                }
                else {
                     pendienteCobro = reportesRepositorio.ObtenerReportePendienteCobroPorCoordinadora(FiltroPendientePorCobro).OrderBy(x => x.FechaVencimiento);
                }
                IEnumerable<ReportePendienteCobroAgrupadoDTO> agrupado = null;


                agrupado = pendienteCobro.GroupBy(x => x.Periodo)
                .Select(g => new ReportePendienteCobroAgrupadoDTO
                {
                    Periodo = g.Key,
                    DetalleFecha = g.Select(y => new ReportePendienteCobroAgrupadoDetalleFechaDTO
                    {
                        CentroCosto = y.CentroCosto,
                        CoordinadoraCobranza = y.CoordinadoraCobranza,
                        Alumno = y.Alumno,
                        CodigoMatricula = y.CodigoMatricula,
                        Dni = y.Dni,
                        SaldoPendienteDolares = y.SaldoPendienteDolares,
                        CodigoSemaforoActual = y.CodigoSemaforoActual,
						ColorSemaforo = reportesRepositorio.obtenerColorSemaforo(y.CodigoSemaforoActual),
                        IdAlumno = y.IdAlumno,
						IdSentinel = y.IdSentinel,
						TiempoSentinel = Convert.ToInt32(DateTime.Now.Subtract(y.FechaConsultaSentinel == null ? DateTime.Now : y.FechaConsultaSentinel.Value).TotalDays),
						Documentos = y.Documentacion,
						CartaAutorizacion = y.CartaAutorizacion.HasValue ? Convert.ToInt32(y.CartaAutorizacion.Value) : 0,
						CartaCompromiso = y.CartaCompromiso.HasValue ? Convert.ToInt32(y.CartaCompromiso) : 0,
						Convenio = y.Convenio.HasValue ? Convert.ToInt32(y.Convenio) : 0,
						Cronograma = y.Cronograma.HasValue ? Convert.ToInt32(y.Cronograma) : 0,
						DocDNI = y.DocDNI.HasValue ? Convert.ToInt32(y.DocDNI) : 0,
						HojaRequisitos = y.HojaRequisitos.HasValue ? Convert.ToInt32(y.HojaRequisitos) : 0,
						OrdenCompra = y.OrdenCompra.HasValue ? Convert.ToInt32(y.OrdenCompra) : 0,
						Pagare = y.Pagare.HasValue ? Convert.ToInt32(y.Pagare) : 0,
						Observaciones = y.Observaciones

					}).ToList()
                });
                return Ok(agrupado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}