using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReporteIngresosFinal")]
    public class ReporteIngresosFinalController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public ReporteIngresosFinalController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
            
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerPeriodos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PeriodoRepositorio _repPeriodo = new PeriodoRepositorio();
                return Ok(_repPeriodo.ObtenerPeriodos());               
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporteIngresos([FromBody] ReporteIngresosFiltroDTO FiltroIngresos)
        {
            try
            {
                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();
                FeriadoRepositorio repFeriadoRep = new FeriadoRepositorio();
                PeriodoRepositorio repPeriodo = new PeriodoRepositorio();
                ReportePagoFiltroDTO FiltroIngreso = new ReportePagoFiltroDTO();

                DateTime FechaInicial = FiltroIngresos.FechaInicioFiltro.Value.Date.AddHours(00).AddMinutes(00).AddSeconds(00);
                DateTime FechaFinal = FiltroIngresos.FechaFinFiltro.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                DateTime FechaInicialAnterior = FiltroIngresos.FechaInicioFiltro.Value.Date.AddMonths(-2);

                FiltroIngreso.FechaInicio = FechaInicial;
                FiltroIngreso.FechaFin = FechaFinal;
                IEnumerable<PagoAlumnoIngresosDTO> resultVentas = reportesRepositorio.ObtenerReporteIngresosVentas(FiltroIngreso).ToList();
                IEnumerable<PagoAlumnoIngresosDTO> resultOperaciones = reportesRepositorio.ObtenerReporteIngresosOperaciones(FiltroIngreso).ToList();
                IEnumerable<PagosIngresosDTO> resultOperacionesTipoCambio = reportesRepositorio.ObtenerReporteIngresosOperacionesTipoCambio(FiltroIngreso).ToList();
                IEnumerable<PagoAlumnoIngresosDTO> resultOtrosIngresosEgresos = reportesRepositorio.ObtenerReporteIngresosOtrosIngresos(FiltroIngreso).ToList();
                IEnumerable<PagosIngresosDTO> resultReportePagos = reportesRepositorio.ObtenerPagosIngresos(FechaInicial,FechaFinal).ToList();
                IEnumerable<PagosIngresosDTO> resultReportePagosPosterior = reportesRepositorio.ObtenerPagosIngresosPosterior(FechaInicial, FechaFinal).ToList();
                IEnumerable<PagosIngresosDTO> resultReportePagosAnterior = reportesRepositorio.ObtenerPagosIngresosAnterior(FechaInicialAnterior, FechaInicial).ToList();
                IEnumerable<PagosIngresosDTO> resultReporteGestionCobranza = reportesRepositorio.ObtenerPagosIngresosGestionCobranza(FechaInicial, FechaFinal).ToList();
                IEnumerable<PagosIngresosDTO> resultReporteTasasAcademicas= reportesRepositorio.ObtenerPagosTasasAcademicas(FechaInicial, FechaFinal).ToList();
                IEnumerable<PagosIngresosDTO> resultReportePagosAnteriorDeposito = reportesRepositorio.ObtenerPagosIngresosAnteriorConDeposito(FechaInicialAnterior, FechaInicial).ToList();
                IEnumerable<PagosIngresosDTO> resultReportePagosPosteriorDeposito = reportesRepositorio.ObtenerPagosIngresosPosteriorConDeposito(FechaInicial, FechaFinal).ToList();

                //IEnumerable<PagoAlumnoIngresosDTO> result = reportesRepositorio.ObtenerReportePagoAlumnosIngresos(Filtro).ToList();

                var ItemVentas = resultVentas.Where(x => x.FechaPago.Value >= FechaInicial && x.FechaPago.Value <= FechaFinal).ToList(); // Extrae los pagos que se dieron en el periodo de tiempo seleccionado que NO sean INHOUSE
                var ItemOperacionesAdelanto = resultOperaciones.Where(x => x.FechaCuota.Value >= FechaFinal).ToList();
                var ItemOperacionesReal = resultOperaciones.Where(x => x.FechaCuota.Value >= FechaInicial && x.FechaCuota.Value <= FechaFinal).ToList();
                var ItemOperacionesRecuperaciones = resultOperaciones.Where(x => x.FechaCuota.Value < FechaInicial).ToList();
                var ItemOperacionesInHouse = resultOperaciones.Where(x => x.IdCategoriaOrigen == 1).ToList();

                var resultOtrosIngresos = resultOtrosIngresosEgresos.Where(x => x.IdTipoMovimientoCaja == 1).ToList(); //Extrae Solo Los INGRESOS
                var resultOtrosEgresos = resultOtrosIngresosEgresos.Where(x => x.IdTipoMovimientoCaja == 2).ToList();//Extrae Solo Los EGRESOS
                var resultTipoCambio = resultOperacionesTipoCambio.Where(x => x.FechaPagoOriginal.Value >= FechaInicial && x.FechaPagoOriginal.Value <= FechaFinal).ToList(); //pagos con nuevos

                var ItemReportePagosPagos = resultReportePagos.Where(x => x.FechaPagoOriginal.Value >= FechaInicial && x.FechaPagoOriginal.Value <= FechaFinal).ToList();

                var resultOtrosIngresosPorFechaPago = resultOtrosIngresos.Where(x => x.FechaPago.Value >= FechaInicial && x.FechaPagoOriginal.Value <= FechaFinal).ToList();
                var resultOtrosEgresosPorFechaPago = resultOtrosEgresos.Where(x => x.FechaPago.Value >= FechaInicial && x.FechaPagoOriginal.Value <= FechaFinal).ToList();

                var ListFeriados = repFeriadoRep.GetBy(x => x.Estado == true).ToList();

                //---------------------------------------------------------------

                DateTime? FechaDisponibleOriginal = null;
                DateTime? FechaIngresoEnCuentaOriginal = null;
                foreach (var item in resultReportePagos)
                {

                    //Aqui les calculo a todos el porcentaje de cobro 
                    item.TotalPagadoDisponible = item.TotalPagado * (item.PorcentajeCobro / 100);
                    //item.Cuota = item.Cuota * (item.PorcentajeCobro / 100);
                    //Fin Aqui les calculo a todos el porcentaje de cobro

                    FechaDisponibleOriginal = item.FechaDisponible;
                    FechaIngresoEnCuentaOriginal = item.FechaDepositaron;

                    if (item.CuentaFeriados == false && item.ConsideraDiasHabilesLV == false && item.ConsideraDiasHabilesLS == false)
                    {
                        item.FechaDepositaron = item.FechaPagoReal;
                        item.FechaDisponible = item.FechaPagoReal;
                        item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(item.DiasDeposito);
                        item.FechaDisponible = item.FechaDisponible.Value.AddDays(item.DiasDisponible);
                    }
                    else
                    {
                        //deposito
                        if (item.DiasDeposito == 0)
                        {
                            bool validadorferiado = true;
                            bool validadorhabiles = true;
                            item.FechaDepositaron = item.FechaPago;
                            while (validadorferiado || validadorhabiles)
                            {
                                if ((ListFeriados.Where(w => w.Dia == item.FechaDepositaron.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null) && item.CuentaFeriados == true)
                                {
                                    item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(1);
                                    validadorferiado = true;
                                }
                                else
                                {
                                    validadorferiado = false;
                                    if ((item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" && item.ConsideraDiasHabilesLS == true) || ((item.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday" || item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday") && item.ConsideraDiasHabilesLV == true))
                                    {
                                        item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(1);
                                        validadorhabiles = true;
                                    }
                                    else
                                        validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                }
                            }
                        }
                        else
                        {
                            item.FechaDepositaron = item.FechaPagoReal;
                            while (item.DiasDeposito > 0)
                            {
                                item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(1);

                                if ((ListFeriados.Where(w => w.Dia == item.FechaDepositaron.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                {
                                    if (item.CuentaFeriados == true)
                                        item.DiasDeposito = item.DiasDeposito;//sigue igual los dias deposito porque me salto el feriado
                                    else
                                        item.DiasDeposito = item.DiasDeposito - 1;
                                }
                                else
                                {
                                    if (item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                    {
                                        if ((item.ConsideraDiasHabilesLV == true && (item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")) || (item.ConsideraDiasHabilesLS == true && item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday"))
                                            item.DiasDeposito = item.DiasDeposito;//sigue igual los dias deposito porque me salto el domingo o sabado
                                        else
                                            item.DiasDeposito = item.DiasDeposito - 1;
                                    }
                                    else
                                        item.DiasDeposito = item.DiasDeposito - 1;
                                }

                            }
                        }

                        //disponible
                        if (item.DiasDisponible == 0)
                        {
                            bool validadorferiado = true;
                            bool validadorhabiles = true;
                            item.FechaDisponible = item.FechaPago;
                            while (validadorferiado || validadorhabiles)
                            {
                                if ((ListFeriados.Where(w => w.Dia == item.FechaDisponible.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null) && item.CuentaFeriados == true)
                                {
                                    item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);
                                    validadorferiado = true;
                                }
                                else
                                {
                                    validadorferiado = false;
                                    if ((item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" && item.ConsideraDiasHabilesLS == true) || ((item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday") && item.ConsideraDiasHabilesLV == true))
                                    {
                                        item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);
                                        validadorhabiles = true;
                                    }
                                    else
                                        validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                }
                            }
                        }
                        else
                        {
                            item.FechaDisponible = item.FechaPagoReal;
                            while (item.DiasDisponible > 0)
                            {
                                item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);

                                if ((ListFeriados.Where(w => w.Dia == item.FechaDisponible.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                {
                                    if (item.CuentaFeriados == true)
                                        item.DiasDisponible = item.DiasDisponible;//sigue igual los dias deposito porque me salto el feriado
                                    else
                                        item.DiasDisponible = item.DiasDisponible - 1;
                                }
                                else
                                {
                                    if (item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                    {
                                        if ((item.ConsideraDiasHabilesLV == true && (item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")) || (item.ConsideraDiasHabilesLS == true && item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday"))
                                            item.DiasDisponible = item.DiasDisponible;//sigue igual los dias deposito porque me salto el domingo
                                        else
                                            item.DiasDisponible = item.DiasDisponible - 1;
                                    }
                                    else
                                        item.DiasDisponible = item.DiasDisponible - 1;
                                }

                            }
                        }

                    }

                }
                foreach (var item in resultReportePagosPosterior)
                {

                    //Aqui les calculo a todos el porcentaje de cobro 
                    item.TotalPagadoDisponible = item.TotalPagado * (item.PorcentajeCobro / 100);
                    //item.Cuota = item.Cuota * (item.PorcentajeCobro / 100);
                    //Fin Aqui les calculo a todos el porcentaje de cobro

                    FechaDisponibleOriginal = item.FechaDisponible;
                    FechaIngresoEnCuentaOriginal = item.FechaDepositaron;

                    if (item.CuentaFeriados == false && item.ConsideraDiasHabilesLV == false && item.ConsideraDiasHabilesLS == false)
                    {
                        item.FechaDepositaron = item.FechaPagoReal;
                        item.FechaDisponible = item.FechaPagoReal;
                        item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(item.DiasDeposito);
                        item.FechaDisponible = item.FechaDisponible.Value.AddDays(item.DiasDisponible);
                    }
                    else
                    {
                        //deposito
                        if (item.DiasDeposito == 0)
                        {
                            bool validadorferiado = true;
                            bool validadorhabiles = true;
                            item.FechaDepositaron = item.FechaPago;
                            while (validadorferiado || validadorhabiles)
                            {
                                if ((ListFeriados.Where(w => w.Dia == item.FechaDepositaron.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null) && item.CuentaFeriados == true)
                                {
                                    item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(1);
                                    validadorferiado = true;
                                }
                                else
                                {
                                    validadorferiado = false;
                                    if ((item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" && item.ConsideraDiasHabilesLS == true) || ((item.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday" || item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday") && item.ConsideraDiasHabilesLV == true))
                                    {
                                        item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(1);
                                        validadorhabiles = true;
                                    }
                                    else
                                        validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                }
                            }
                        }
                        else
                        {
                            item.FechaDepositaron = item.FechaPagoReal;
                            while (item.DiasDeposito > 0)
                            {
                                item.FechaDepositaron = item.FechaDepositaron.Value.AddDays(1);

                                if ((ListFeriados.Where(w => w.Dia == item.FechaDepositaron.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                {
                                    if (item.CuentaFeriados == true)
                                        item.DiasDeposito = item.DiasDeposito;//sigue igual los dias deposito porque me salto el feriado
                                    else
                                        item.DiasDeposito = item.DiasDeposito - 1;
                                }
                                else
                                {
                                    if (item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                    {
                                        if ((item.ConsideraDiasHabilesLV == true && (item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")) || (item.ConsideraDiasHabilesLS == true && item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday"))
                                            item.DiasDeposito = item.DiasDeposito;//sigue igual los dias deposito porque me salto el domingo o sabado
                                        else
                                            item.DiasDeposito = item.DiasDeposito - 1;
                                    }
                                    else
                                        item.DiasDeposito = item.DiasDeposito - 1;
                                }

                            }
                        }

                        //disponible
                        if (item.DiasDisponible == 0)
                        {
                            bool validadorferiado = true;
                            bool validadorhabiles = true;
                            item.FechaDisponible = item.FechaPago;
                            while (validadorferiado || validadorhabiles)
                            {
                                if ((ListFeriados.Where(w => w.Dia == item.FechaDisponible.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null) && item.CuentaFeriados == true)
                                {
                                    item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);
                                    validadorferiado = true;
                                }
                                else
                                {
                                    validadorferiado = false;
                                    if ((item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" && item.ConsideraDiasHabilesLS == true) || ((item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday") && item.ConsideraDiasHabilesLV == true))
                                    {
                                        item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);
                                        validadorhabiles = true;
                                    }
                                    else
                                        validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                }
                            }
                        }
                        else
                        {
                            item.FechaDisponible = item.FechaPagoReal;
                            while (item.DiasDisponible > 0)
                            {
                                item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);

                                if ((ListFeriados.Where(w => w.Dia == item.FechaDisponible.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                {
                                    if (item.CuentaFeriados == true)
                                        item.DiasDisponible = item.DiasDisponible;//sigue igual los dias deposito porque me salto el feriado
                                    else
                                        item.DiasDisponible = item.DiasDisponible - 1;
                                }
                                else
                                {
                                    if (item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                    {
                                        if ((item.ConsideraDiasHabilesLV == true && (item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")) || (item.ConsideraDiasHabilesLS == true && item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday"))
                                            item.DiasDisponible = item.DiasDisponible;//sigue igual los dias deposito porque me salto el domingo
                                        else
                                            item.DiasDisponible = item.DiasDisponible - 1;
                                    }
                                    else
                                        item.DiasDisponible = item.DiasDisponible - 1;
                                }

                            }
                        }

                    }
                }
                
                foreach (var itemanterior in resultReportePagosAnterior)
                {

                    //Aqui les calculo a todos el porcentaje de cobro 
                    itemanterior.TotalPagadoDisponible = itemanterior.TotalPagado * (itemanterior.PorcentajeCobro / 100);
                    //item.Cuota = item.Cuota * (item.PorcentajeCobro / 100);
                    //Fin Aqui les calculo a todos el porcentaje de cobro

                    FechaDisponibleOriginal = itemanterior.FechaDisponible;
                    FechaIngresoEnCuentaOriginal = itemanterior.FechaDepositaron;

                    if (itemanterior.CuentaFeriados == false && itemanterior.ConsideraDiasHabilesLV == false && itemanterior.ConsideraDiasHabilesLS == false)
                    {
                        itemanterior.FechaDepositaron = itemanterior.FechaPagoReal;
                        itemanterior.FechaDisponible = itemanterior.FechaPagoReal;
                        itemanterior.FechaDepositaron = itemanterior.FechaDepositaron.Value.AddDays(itemanterior.DiasDeposito);
                        itemanterior.FechaDisponible = itemanterior.FechaDisponible.Value.AddDays(itemanterior.DiasDisponible);
                    }
                    else
                    {
                        //deposito
                        if (itemanterior.DiasDeposito == 0)
                        {
                            bool validadorferiado = true;
                            bool validadorhabiles = true;
                            itemanterior.FechaDepositaron = itemanterior.FechaPago;
                            while (validadorferiado || validadorhabiles)
                            {
                                if ((ListFeriados.Where(w => w.Dia == itemanterior.FechaDepositaron.Value.Date && w.IdTroncalCiudad == itemanterior.IdCiudad).FirstOrDefault() != null) && itemanterior.CuentaFeriados == true)
                                {
                                    itemanterior.FechaDepositaron = itemanterior.FechaDepositaron.Value.AddDays(1);
                                    validadorferiado = true;
                                }
                                else
                                {
                                    validadorferiado = false;
                                    if ((itemanterior.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" && itemanterior.ConsideraDiasHabilesLS == true) || ((itemanterior.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday" || itemanterior.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday") && itemanterior.ConsideraDiasHabilesLV == true))
                                    {
                                        itemanterior.FechaDepositaron = itemanterior.FechaDepositaron.Value.AddDays(1);
                                        validadorhabiles = true;
                                    }
                                    else
                                        validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                }
                            }
                        }
                        else
                        {
                            itemanterior.FechaDepositaron = itemanterior.FechaPagoReal;
                            while (itemanterior.DiasDeposito > 0)
                            {
                                itemanterior.FechaDepositaron = itemanterior.FechaDepositaron.Value.AddDays(1);

                                if ((ListFeriados.Where(w => w.Dia == itemanterior.FechaDepositaron.Value.Date && w.IdTroncalCiudad == itemanterior.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                {
                                    if (itemanterior.CuentaFeriados == true)
                                        itemanterior.DiasDeposito = itemanterior.DiasDeposito;//sigue igual los dias deposito porque me salto el feriado
                                    else
                                        itemanterior.DiasDeposito = itemanterior.DiasDeposito - 1;
                                }
                                else
                                {
                                    if (itemanterior.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || itemanterior.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                    {
                                        if ((itemanterior.ConsideraDiasHabilesLV == true && (itemanterior.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || itemanterior.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")) || (itemanterior.ConsideraDiasHabilesLS == true && itemanterior.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday"))
                                            itemanterior.DiasDeposito = itemanterior.DiasDeposito;//sigue igual los dias deposito porque me salto el domingo o sabado
                                        else
                                            itemanterior.DiasDeposito = itemanterior.DiasDeposito - 1;
                                    }
                                    else
                                        itemanterior.DiasDeposito = itemanterior.DiasDeposito - 1;
                                }

                            }
                        }

                        //disponible
                        if (itemanterior.DiasDisponible == 0)
                        {
                            bool validadorferiado = true;
                            bool validadorhabiles = true;
                            itemanterior.FechaDisponible = itemanterior.FechaPago;
                            while (validadorferiado || validadorhabiles)
                            {
                                if ((ListFeriados.Where(w => w.Dia == itemanterior.FechaDisponible.Value.Date && w.IdTroncalCiudad == itemanterior.IdCiudad).FirstOrDefault() != null) && itemanterior.CuentaFeriados == true)
                                {
                                    itemanterior.FechaDisponible = itemanterior.FechaDisponible.Value.AddDays(1);
                                    validadorferiado = true;
                                }
                                else
                                {
                                    validadorferiado = false;
                                    if ((itemanterior.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" && itemanterior.ConsideraDiasHabilesLS == true) || ((itemanterior.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday" || itemanterior.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday") && itemanterior.ConsideraDiasHabilesLV == true))
                                    {
                                        itemanterior.FechaDisponible = itemanterior.FechaDisponible.Value.AddDays(1);
                                        validadorhabiles = true;
                                    }
                                    else
                                        validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                }
                            }
                        }
                        else
                        {
                            itemanterior.FechaDisponible = itemanterior.FechaPagoReal;
                            while (itemanterior.DiasDisponible > 0)
                            {
                                itemanterior.FechaDisponible = itemanterior.FechaDisponible.Value.AddDays(1);

                                if ((ListFeriados.Where(w => w.Dia == itemanterior.FechaDisponible.Value.Date && w.IdTroncalCiudad == itemanterior.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                {
                                    if (itemanterior.CuentaFeriados == true)
                                        itemanterior.DiasDisponible = itemanterior.DiasDisponible;//sigue igual los dias deposito porque me salto el feriado
                                    else
                                        itemanterior.DiasDisponible = itemanterior.DiasDisponible - 1;
                                }
                                else
                                {
                                    if (itemanterior.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || itemanterior.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                    {
                                        if ((itemanterior.ConsideraDiasHabilesLV == true && (itemanterior.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || itemanterior.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")) || (itemanterior.ConsideraDiasHabilesLS == true && itemanterior.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday"))
                                            itemanterior.DiasDisponible = itemanterior.DiasDisponible;//sigue igual los dias deposito porque me salto el domingo
                                        else
                                            itemanterior.DiasDisponible = itemanterior.DiasDisponible - 1;
                                    }
                                    else
                                        itemanterior.DiasDisponible = itemanterior.DiasDisponible - 1;
                                }

                            }
                        }

                    }

                }
                    
                    
                
                DateTime fdss = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var resultOtrosIngresosPorFechaPagoAnterior = resultReportePagosAnterior.Where(x => x.FechaDepositaron.Value >= FechaInicial && x.FechaDepositaron.Value <= FechaFinal).ToList();
                var resultOtrosEgresosPorFechaPagoPosterior = resultReportePagosPosterior.Where(x => x.FechaDepositaron.Value > FechaFinal).ToList();
                var resultOtrosIngresosPorFechaPagoAnteriorDeposito = resultReportePagosAnteriorDeposito.Where(x => x.FechaDepositaron.Value >= FechaInicial && x.FechaDepositaron.Value <= FechaFinal).ToList();
                var resultOtrosEgresosPorFechaPagoPosteriorDeposito = resultReportePagosPosteriorDeposito.Where(x => x.FechaDepositaron.Value > FechaFinal).ToList();
                var resultComisiones = resultReportePagos.Where(x => x.FechaPagoReal.Value >= FechaInicial && x.FechaPagoReal.Value <= FechaFinal).ToList();
                var resultGestionCobranza = resultReporteGestionCobranza.Where(x => x.FechaPagoReal.Value >= FechaInicial && x.FechaPagoReal.Value <= FechaFinal).ToList();
                var resultTasasAcademicas = resultReporteTasasAcademicas.Where(x => x.FechaPagoReal.Value >= FechaInicial && x.FechaPagoReal.Value <= FechaFinal).ToList();

                //Sumamos las listas finales
                var SumaIngresoRealVentas = ItemVentas.Sum(x => x.MontoPagado);//Suma de cuotas
                var SumaIngresoAdelanto = ItemOperacionesAdelanto.Sum(x => x.MontoPagado);//Suma de cuotas
                var SumaIngresoRealMes = ItemOperacionesReal.Sum(x => x.MontoPagado);//Suma de cuotas
                var SumaIngresoRecuperado = ItemOperacionesRecuperaciones.Sum(x => x.MontoPagado);// suma de cuotas
                var SumaIngresoInHouse = ItemOperacionesInHouse.Sum(x => x.MontoPagado);// suma cuotas de inhouse
                var SumaTotalOtrosIngresos = resultOtrosIngresosPorFechaPago.Sum(x => x.MontoPagado);
                var SumaTotalOtrosEgresos = resultOtrosEgresosPorFechaPago.Sum(x => x.MontoPagado);
                var SumaTotalPagos = resultReportePagos.Sum(x=> x.TotalPagado);
                var SumaTotalTipoCambio = resultTipoCambio.Sum(x => x.TotalPagado); // suma monto total pagado cuota + mora
                var SumaTotalPagosAnteriores = resultOtrosIngresosPorFechaPagoAnterior.Sum(x => x.TotalPagado);
                var SumaTotalPagosPosteriores = resultOtrosEgresosPorFechaPagoPosterior.Sum(x => x.TotalPagado);
                var SumaTotalPagosAnterioresDeposito = resultOtrosIngresosPorFechaPagoAnteriorDeposito.Sum(x => x.TotalPagado);
                var SumaTotalPagosPosterioresDeposito = resultOtrosEgresosPorFechaPagoPosteriorDeposito.Sum(x => x.TotalPagado);
                var SumaTotalPagado = resultComisiones.Sum(x => x.TotalPagado);
                var SumaTotalPagadoDisponible = resultComisiones.Sum(x => x.TotalPagadoDisponible);
                var SumaTotalGestionCobranza = resultGestionCobranza.Sum(x => x.TotalPagado);
                var SumaTotalTasasAcademicas = resultTasasAcademicas.Sum(x => x.TotalPagado);
                //--------------------------------------------------------------------------------

                /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

                List<ReporteIngresosDetalleDTO> detalles = new List<ReporteIngresosDetalleDTO>();

                ReporteIngresosDetalleDTO detalle1 = new ReporteIngresosDetalleDTO();
                detalle1.Tipo = "Ingreso Real de Operaciones por Adelantos($)";
                detalle1.Valor = Decimal.Round(SumaIngresoAdelanto);
                detalles.Add(detalle1);

                ReporteIngresosDetalleDTO detalle2 = new ReporteIngresosDetalleDTO();
                detalle2.Tipo = "Ingreso Real de Operaciones($)";
                detalle2.Valor = Decimal.Round(SumaIngresoRealMes-SumaIngresoInHouse);
                detalles.Add(detalle2);

                ReporteIngresosDetalleDTO detalle3 = new ReporteIngresosDetalleDTO();
                detalle3.Tipo = "Ingreso Real de Operaciones por Recuperaciones($)";
                detalle3.Valor = Decimal.Round(SumaIngresoRecuperado);
                detalles.Add(detalle3);

                ReporteIngresosDetalleDTO detalle4 = new ReporteIngresosDetalleDTO();
                detalle4.Tipo = "Ingreso Real de Ventas($)";
                detalle4.Valor = Decimal.Round(SumaIngresoRealVentas);
                detalles.Add(detalle4);

                ReporteIngresosDetalleDTO detalle5 = new ReporteIngresosDetalleDTO();
                detalle5.Tipo = "Inhouse($)";
                detalle5.Valor = Decimal.Round(SumaIngresoInHouse);
                detalles.Add(detalle5);
                //////////////////////////////////inicio
                ReporteIngresosDetalleDTO detalle6 = new ReporteIngresosDetalleDTO();
                detalle6.Tipo = "Otros Ingresos($)";
                detalle6.Valor = Decimal.Round(SumaTotalOtrosIngresos + SumaTotalGestionCobranza + SumaTotalTasasAcademicas);
                detalles.Add(detalle6);

                ReporteIngresosDetalleDTO detalle12 = new ReporteIngresosDetalleDTO();
                detalle12.Tipo = "Otros Egresos($)";
                detalle12.Valor = (-1) * Decimal.Round(SumaTotalOtrosEgresos);
                detalles.Add(detalle12);

                ReporteIngresosDetalleDTO detalle7 = new ReporteIngresosDetalleDTO();
                detalle7.Tipo = "Diferencia por Tipo Cambio($)";
                detalle7.Valor = Decimal.Round(SumaTotalTipoCambio - (SumaIngresoAdelanto + SumaIngresoRealMes + SumaIngresoRecuperado + SumaIngresoRealVentas));
                detalles.Add(detalle7);

                ReporteIngresosDetalleDTO detalle8 = new ReporteIngresosDetalleDTO();
                detalle8.Tipo = "Monto en Flujo($)";
                detalle8.Valor = Decimal.Round(SumaIngresoAdelanto + SumaIngresoRealMes + SumaIngresoRecuperado + SumaIngresoRealVentas + SumaIngresoInHouse + detalle6.Valor + detalle7.Valor + detalle12.Valor);
                detalles.Add(detalle8);

                ReporteIngresosDetalleDTO detalle9 = new ReporteIngresosDetalleDTO();
                detalle9.Tipo = "Ingresos Registrados Anteriores pero Ingresados en el Periodo Seleccionado($)";
                detalle9.Valor = Decimal.Round(SumaTotalPagosAnteriores + SumaTotalPagosAnterioresDeposito);
                detalles.Add(detalle9);

                ReporteIngresosDetalleDTO detalle10 = new ReporteIngresosDetalleDTO();
                detalle10.Tipo = "Ingresos Registrados en el Periodo Seleccionado pero Registrados despues($)";
                detalle10.Valor = (-1) * Decimal.Round(SumaTotalPagosPosteriores + SumaTotalPagosPosterioresDeposito);
                detalles.Add(detalle10);

                ReporteIngresosDetalleDTO detalle11 = new ReporteIngresosDetalleDTO();
                detalle11.Tipo = "Comisiones por Comercio Electronico($)";
                detalle11.Valor = (-1) * Decimal.Round(SumaTotalPagado-SumaTotalPagadoDisponible);
                detalles.Add(detalle11);

                return Ok(new { detalles });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }
}