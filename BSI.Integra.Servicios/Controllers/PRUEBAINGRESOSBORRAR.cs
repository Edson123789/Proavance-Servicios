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
    [Route("api/ReporteIngresosFinalPRUEBA")]
    public class PRUEBAINGRESOSBORRAR : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public PRUEBAINGRESOSBORRAR(integraDBContext integraDBContext)
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
                ReportePagoFiltroDTO Filtro = new ReportePagoFiltroDTO();
                DateTime FechaInicial = FiltroIngresos.FechaInicioFiltro.Value.Date.AddHours(00).AddMinutes(00).AddSeconds(00);
                DateTime FechaFinal = FiltroIngresos.FechaFinFiltro.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                Filtro.FechaInicio = FechaInicial.AddMonths(-2);
                Filtro.FechaFin = FechaFinal.AddMonths(2);
                IEnumerable<PagoAlumnoIngresosDTO> result = reportesRepositorio.ObtenerReportePagoAlumnosIngresos(Filtro).ToList();
                IEnumerable<PagoAlumnoIngresosDTO> resultOtrosIngresosEgresos = reportesRepositorio.ObtenerReporteIngresosOtrosIngresos(Filtro).ToList();
                var ListFeriados = repFeriadoRep.GetBy(x => x.Estado == true).ToList();

                /*Halla la FechaIngresoEnCuenta y EfectoDisponible de ReportePagoAlumnosIngresos*/
                //DateTime? FechaDisponibleOriginal = null;
                DateTime? FechaIngresoEnCuentaOriginal = null;
                foreach (var item in result)
                {
                    //FechaDisponibleOriginal = item.FechaDisponible;
                    FechaIngresoEnCuentaOriginal = item.FechaIngresoEnCuenta;

                    if (item.CuentaFeriados == false && item.ConsiderarDiasHabilesLV == false && item.ConsiderarDiasHabilesLS == false)
                    {
                        item.FechaIngresoEnCuenta = item.FechaPagoReal;
                        //item.FechaDisponible = item.FechaPagoReal;
                        item.FechaIngresoEnCuenta = item.FechaIngresoEnCuenta.Value.AddDays(item.DiasDeposito);
                        //item.FechaDisponible = item.FechaDisponible.Value.AddDays(item.DiasDisponible);
                    }
                    else
                    {
                        //deposito
                        if (item.DiasDeposito == 0)
                        {
                            bool validadorferiado = true;
                            bool validadorhabiles = true;
                            item.FechaIngresoEnCuenta = item.FechaPago;
                            while (validadorferiado || validadorhabiles)
                            {
                                if ((ListFeriados.Where(w => w.Dia == item.FechaIngresoEnCuenta.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null) && item.CuentaFeriados == true)
                                {
                                    item.FechaIngresoEnCuenta = item.FechaIngresoEnCuenta.Value.AddDays(1);
                                    validadorferiado = true;
                                }
                                else
                                {
                                    validadorferiado = false;
                                    if ((item.FechaIngresoEnCuenta.Value.DayOfWeek.ToString() == "Sunday" && item.ConsiderarDiasHabilesLS == true) || ((item.FechaIngresoEnCuenta.Value.DayOfWeek.ToString() == "Saturday" || item.FechaIngresoEnCuenta.Value.DayOfWeek.ToString() == "Sunday") && item.ConsiderarDiasHabilesLV == true))
                                    {
                                        item.FechaIngresoEnCuenta = item.FechaIngresoEnCuenta.Value.AddDays(1);
                                        validadorhabiles = true;
                                    }
                                    else
                                        validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                }
                            }
                        }
                        else
                        {
                            item.FechaIngresoEnCuenta = item.FechaPagoReal;
                            while (item.DiasDeposito > 0)
                            {
                                item.FechaIngresoEnCuenta = item.FechaIngresoEnCuenta.Value.AddDays(1);

                                if ((ListFeriados.Where(w => w.Dia == item.FechaIngresoEnCuenta.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                {
                                    if (item.CuentaFeriados == true)
                                        item.DiasDeposito = item.DiasDeposito;//sigue igual los dias deposito porque me salto el feriado
                                    else
                                        item.DiasDeposito = item.DiasDeposito - 1;
                                }
                                else
                                {
                                    if (item.FechaIngresoEnCuenta.Value.DayOfWeek.ToString() == "Sunday" || item.FechaIngresoEnCuenta.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                    {
                                        if ((item.ConsiderarDiasHabilesLV == true && (item.FechaIngresoEnCuenta.Value.DayOfWeek.ToString() == "Sunday" || item.FechaIngresoEnCuenta.Value.DayOfWeek.ToString() == "Saturday")) || (item.ConsiderarDiasHabilesLS == true && item.FechaIngresoEnCuenta.Value.DayOfWeek.ToString() == "Sunday"))
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
                        //    if (item.DiasDisponible == 0)
                        //    {
                        //        bool validadorferiado = true;
                        //        bool validadorhabiles = true;
                        //        item.FechaDisponible = item.FechaPago;
                        //        while (validadorferiado || validadorhabiles)
                        //        {
                        //            if ((ListFeriados.Where(w => w.Dia == item.FechaDisponible.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null) && item.CuentaFeriados == true)
                        //            {
                        //                item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);
                        //                validadorferiado = true;
                        //            }
                        //            else
                        //            {
                        //                validadorferiado = false;
                        //                if ((item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" && item.ConsiderarDiasHabilesLS == true) || ((item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday") && item.ConsiderarDiasHabilesLV == true))
                        //                {
                        //                    item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);
                        //                    validadorhabiles = true;
                        //                }
                        //                else
                        //                    validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        item.FechaDisponible = item.FechaPagoReal;
                        //        while (item.DiasDisponible > 0)
                        //        {
                        //            item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);

                        //            if ((ListFeriados.Where(w => w.Dia == item.FechaDisponible.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                        //            {
                        //                if (item.CuentaFeriados == true)
                        //                    item.DiasDisponible = item.DiasDisponible;//sigue igual los dias deposito porque me salto el feriado
                        //                else
                        //                    item.DiasDisponible = item.DiasDisponible - 1;
                        //            }
                        //            else
                        //            {
                        //                if (item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                        //                {
                        //                    if ((item.ConsiderarDiasHabilesLV == true && (item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")) || (item.ConsiderarDiasHabilesLS == true && item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday"))
                        //                        item.DiasDisponible = item.DiasDisponible;//sigue igual los dias deposito porque me salto el domingo
                        //                    else
                        //                        item.DiasDisponible = item.DiasDisponible - 1;
                        //                }
                        //                else
                        //                    item.DiasDisponible = item.DiasDisponible - 1;
                        //            }

                        //        }
                        //    }

                    }

                    //if (DateTime.Now.Date >= item.FechaDisponible.Value.Date)
                    //    item.EstadoEfectivo = "Disponible";
                    //else
                    if (DateTime.Now.Date >= item.FechaIngresoEnCuenta.Value.Date)
                        item.EstadoEfectivo = "Depositado";
                    //item.FechaDisponible = FechaDisponibleOriginal == null ? item.FechaDisponible : FechaDisponibleOriginal;
                    item.FechaIngresoEnCuenta = FechaIngresoEnCuentaOriginal == null ? item.FechaIngresoEnCuenta : FechaIngresoEnCuentaOriginal;
                }

                /*Halla la FechaIngresoEnCuenta y EfectoDisponible de ReporteIngresosOtrosIngresos*/
                //DateTime? FechaDisponibleOriginal = null;

                foreach (var item in resultOtrosIngresosEgresos)
                {
                    FechaIngresoEnCuentaOriginal = item.FechaIngresoEnCuenta;

                    if (item.CuentaFeriados == false && item.ConsiderarDiasHabilesLV == false && item.ConsiderarDiasHabilesLS == false)
                    {
                        item.FechaIngresoEnCuenta = item.FechaPagoReal;
                        //item.FechaDisponible = item.FechaPagoReal;
                        item.FechaIngresoEnCuenta = item.FechaIngresoEnCuenta.Value.AddDays(item.DiasDeposito);
                        //item.FechaDisponible = item.FechaDisponible.Value.AddDays(item.DiasDisponible);
                    }
                    else
                    {
                        //deposito
                        if (item.DiasDeposito == 0)
                        {
                            bool validadorferiado = true;
                            bool validadorhabiles = true;
                            item.FechaIngresoEnCuenta = item.FechaPago;
                            while (validadorferiado || validadorhabiles)
                            {
                                if ((ListFeriados.Where(w => w.Dia == item.FechaIngresoEnCuenta.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null) && item.CuentaFeriados == true)
                                {
                                    item.FechaIngresoEnCuenta = item.FechaIngresoEnCuenta.Value.AddDays(1);
                                    validadorferiado = true;
                                }
                                else
                                {
                                    validadorferiado = false;
                                    if ((item.FechaIngresoEnCuenta.Value.DayOfWeek.ToString() == "Sunday" && item.ConsiderarDiasHabilesLS == true) || ((item.FechaIngresoEnCuenta.Value.DayOfWeek.ToString() == "Saturday" || item.FechaIngresoEnCuenta.Value.DayOfWeek.ToString() == "Sunday") && item.ConsiderarDiasHabilesLV == true))
                                    {
                                        item.FechaIngresoEnCuenta = item.FechaIngresoEnCuenta.Value.AddDays(1);
                                        validadorhabiles = true;
                                    }
                                    else
                                        validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                                }
                            }
                        }
                        else
                        {
                            item.FechaIngresoEnCuenta = item.FechaPagoReal;
                            while (item.DiasDeposito > 0)
                            {
                                item.FechaIngresoEnCuenta = item.FechaIngresoEnCuenta.Value.AddDays(1);

                                if ((ListFeriados.Where(w => w.Dia == item.FechaIngresoEnCuenta.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                                {
                                    if (item.CuentaFeriados == true)
                                        item.DiasDeposito = item.DiasDeposito;//sigue igual los dias deposito porque me salto el feriado
                                    else
                                        item.DiasDeposito = item.DiasDeposito - 1;
                                }
                                else
                                {
                                    if (item.FechaIngresoEnCuenta.Value.DayOfWeek.ToString() == "Sunday" || item.FechaIngresoEnCuenta.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                                    {
                                        if ((item.ConsiderarDiasHabilesLV == true && (item.FechaIngresoEnCuenta.Value.DayOfWeek.ToString() == "Sunday" || item.FechaIngresoEnCuenta.Value.DayOfWeek.ToString() == "Saturday")) || (item.ConsiderarDiasHabilesLS == true && item.FechaIngresoEnCuenta.Value.DayOfWeek.ToString() == "Sunday"))
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
                        //    if (item.DiasDisponible == 0)
                        //    {
                        //        bool validadorferiado = true;
                        //        bool validadorhabiles = true;
                        //        item.FechaDisponible = item.FechaPago;
                        //        while (validadorferiado || validadorhabiles)
                        //        {
                        //            if ((ListFeriados.Where(w => w.Dia == item.FechaDisponible.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null) && item.CuentaFeriados == true)
                        //            {
                        //                item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);
                        //                validadorferiado = true;
                        //            }
                        //            else
                        //            {
                        //                validadorferiado = false;
                        //                if ((item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" && item.ConsiderarDiasHabilesLS == true) || ((item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday") && item.ConsiderarDiasHabilesLV == true))
                        //                {
                        //                    item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);
                        //                    validadorhabiles = true;
                        //                }
                        //                else
                        //                    validadorhabiles = false;//lo mando a falso porque no le importa que caiga feriado
                        //            }
                        //        }
                        //    }
                        //    else
                        //    {
                        //        item.FechaDisponible = item.FechaPagoReal;
                        //        while (item.DiasDisponible > 0)
                        //        {
                        //            item.FechaDisponible = item.FechaDisponible.Value.AddDays(1);

                        //            if ((ListFeriados.Where(w => w.Dia == item.FechaDisponible.Value.Date && w.IdTroncalCiudad == item.IdCiudad).FirstOrDefault() != null))//significa que es feriado entonces ahora valido si considera feriados
                        //            {
                        //                if (item.CuentaFeriados == true)
                        //                    item.DiasDisponible = item.DiasDisponible;//sigue igual los dias deposito porque me salto el feriado
                        //                else
                        //                    item.DiasDisponible = item.DiasDisponible - 1;
                        //            }
                        //            else
                        //            {
                        //                if (item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")//significa que es domingo entonces ahora valido si considera dias habiles
                        //                {
                        //                    if ((item.ConsiderarDiasHabilesLV == true && (item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")) || (item.ConsiderarDiasHabilesLS == true && item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday"))
                        //                        item.DiasDisponible = item.DiasDisponible;//sigue igual los dias deposito porque me salto el domingo
                        //                    else
                        //                        item.DiasDisponible = item.DiasDisponible - 1;
                        //                }
                        //                else
                        //                    item.DiasDisponible = item.DiasDisponible - 1;
                        //            }

                        //        }
                        //    }

                    }

                    //if (DateTime.Now.Date >= item.FechaDisponible.Value.Date)
                    //    item.EstadoEfectivo = "Disponible";
                    //else
                    if (DateTime.Now.Date >= item.FechaIngresoEnCuenta.Value.Date)
                        item.EstadoEfectivo = "Depositado";
                    //item.FechaDisponible = FechaDisponibleOriginal == null ? item.FechaDisponible : FechaDisponibleOriginal;
                    item.FechaIngresoEnCuenta = FechaIngresoEnCuentaOriginal == null ? item.FechaIngresoEnCuenta : FechaIngresoEnCuentaOriginal;
                }

                var resultOtrosIngresos = resultOtrosIngresosEgresos.Where(x => x.IdTipoMovimientoCaja == 1).ToList(); //Extrae Solo Los INGRESOS
                var resultOtrosEgresos = resultOtrosIngresosEgresos.Where(x => x.IdTipoMovimientoCaja == 2).ToList();//Extrae Solo Los EGRESOS

                var PagosTotalParaPeriodoSeleccionado = result.Where(x => x.FechaPagoOriginal.Value >= FechaInicial && x.FechaPagoOriginal.Value <= FechaFinal).ToList();
                var ItemSinInHouse = result.Where(x => x.FechaPagoOriginal.Value >= FechaInicial && x.FechaPagoOriginal.Value <= FechaFinal && x.IdCategoriaOrigen != 460).ToList(); // Extrae los pagos que se dieron en el periodo de tiempo seleccionado que NO sean INHOUSE

                var IngresoRealVentasSinInHouse = ItemSinInHouse.Where(x => new DateTime(x.FechaPagoOriginal.Value.Year, x.FechaPagoOriginal.Value.Month, 1) <= new DateTime(x.FechaMatricula.Value.Year, x.FechaMatricula.Value.Month, 1)).ToList();
                var IngresoRealNOVentasSinInhouse = ItemSinInHouse.Where(x => new DateTime(x.FechaPagoOriginal.Value.Year, x.FechaPagoOriginal.Value.Month, 1) > new DateTime(x.FechaMatricula.Value.Year, x.FechaMatricula.Value.Month, 1)).ToList();//BORRAR: SOLO PARA VALIDARE QUE SUME EL TOTAL DE PERSONAS QUE NO SON INHOUSE
                DateTime fdss = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                var PagosOperaciones = ItemSinInHouse.Where(x => new DateTime(x.FechaPagoOriginal.Value.Year, x.FechaPagoOriginal.Value.Month, 1) > new DateTime(x.FechaMatricula.Value.Year, x.FechaMatricula.Value.Month, 1)).ToList();

                var IngresoRecuperado = PagosOperaciones.Where(x => new DateTime(x.FechaPagoOriginal.Value.Year, x.FechaPagoOriginal.Value.Month, 1) > new DateTime(x.FechaCuota.Value.Year, x.FechaCuota.Value.Month, 1)).ToList();
                var IngresoAdelanto = PagosOperaciones.Where(x => new DateTime(x.FechaPagoOriginal.Value.Year, x.FechaPagoOriginal.Value.Month, 1) < new DateTime(x.FechaCuota.Value.Year, x.FechaCuota.Value.Month, 1)).ToList();
                var IngresoRealMes = PagosOperaciones.Where(x => new DateTime(x.FechaPagoOriginal.Value.Year, x.FechaPagoOriginal.Value.Month, 1) == new DateTime(x.FechaCuota.Value.Year, x.FechaCuota.Value.Month, 1)).ToList();
                var IngresoRealInHouseConCuota1 = result.Where(x => x.FechaPagoOriginal.Value >= FechaInicial && x.FechaPagoOriginal.Value <= FechaFinal && x.IdCategoriaOrigen == 460).ToList();

                //  var PagosVentas= result.Where()
                var resultOtrosIngresosPorFechaPago = resultOtrosIngresos.Where(x => x.FechaPagoOriginal.Value >= FechaInicial && x.FechaPagoOriginal.Value <= FechaFinal).ToList();
                var resultOtrosEgresosPorFechaPago = resultOtrosEgresos.Where(x => x.FechaPagoOriginal.Value >= FechaInicial && x.FechaPagoOriginal.Value <= FechaFinal).ToList();

                // Todos los pagos que hayan Ingresado a Cuenta en el PeriodoSeleccionado
                var IngresoRealPagosPeriodo = result.Where(x => x.FechaIngresoEnCuenta.Value.Date >= FechaInicial.Date && x.FechaIngresoEnCuenta.Value.Date <= FechaFinal.Date).ToList();

                //Todos los pagos que tengan Ingresen A Cuenta Despues del PeriodoSeleccionado Ingreso en el Periodo Seleccionado + 2 Meses
                var IngresoRealMesSiguientePagosPeriodo = result.Where(x => x.FechaIngresoEnCuenta.Value.Date > FechaFinal).ToList();

                //Se obtiene La Suma Total de Los Pagos que Ingresan a Cuenta en el PeriodoSeleccionado Pero que se hayan pagado 2 Meses Antes y se disminuye lo que se pago de comision al banco
                var SumaIngresosMesConFechaPagoMesAnterior = IngresoRealPagosPeriodo.Where(x => x.FechaPagoOriginal.Value.Date < FechaInicial.Date).ToList().Sum(x => x.MontoPagado)- IngresoRealPagosPeriodo.Where(x => x.FechaPagoOriginal.Value.Date < FechaInicial.Date).ToList().Sum(x => x.CobroComisionMontoPagado);

                var PRUEBABORRARSumaIngresosMesConFechaPagoMesAnterior = IngresoRealPagosPeriodo.Where(x => x.FechaPagoOriginal.Value.Date < FechaInicial.Date).ToList();
                var PRUEBABORRARSumaIngresosMesSiguienteConFechaPagoMesActual = IngresoRealMesSiguientePagosPeriodo.Where(x => x.FechaPagoOriginal.Value.Date >= FechaInicial.Date && x.FechaPagoOriginal.Value.Date <= FechaFinal.Date).ToList();

                //Se obtiene La Suma Total de Los Pagos que Ingresan a Cuenta despues del PeriodoSeleccionado Pero que se hayan pagado en el PeriodoSeleccionado
                var SumaIngresosMesSiguienteConFechaPagoMesActual = IngresoRealMesSiguientePagosPeriodo.Where(x => x.FechaPagoOriginal.Value.Date>=FechaInicial.Date && x.FechaPagoOriginal.Value.Date <= FechaFinal.Date).ToList().Sum(x => x.MontoPagado)- IngresoRealMesSiguientePagosPeriodo.Where(x => x.FechaPagoOriginal.Value.Date >= FechaInicial.Date && x.FechaPagoOriginal.Value.Date <= FechaFinal.Date).ToList().Sum(x => x.CobroComisionMontoPagado);                 

                var SumaItemInHouse = ItemSinInHouse.Sum(x => x.MontoPagado);
                var SumaTotalOperaciones = PagosOperaciones.Sum(x => x.MontoPagado);
                var SumaIngresoRecuperado = IngresoRecuperado.Sum(x => x.MontoPagado);
                var SumaIngresoAdelanto = IngresoAdelanto.Sum(x => x.MontoPagado);
                var SumaIngresoRealMes = IngresoRealMes.Sum(x => x.MontoPagado);
                var SumaIngresoRealVentas = IngresoRealVentasSinInHouse.Sum(x => x.MontoPagado);
                var SumaTotalInHouse = IngresoRealInHouseConCuota1.Sum(x => x.MontoPagado);
                var SumaTotalOtrosIngresos = resultOtrosIngresosPorFechaPago.Sum(x => x.MontoPagado);
                var SumaTotalOtrosEgresos = resultOtrosEgresosPorFechaPago.Sum(x => x.MontoPagado);

                var SumaTotalComisiones = PagosTotalParaPeriodoSeleccionado.Sum(x => x.CobroComisionMontoPagado);

                var SumaTotalComisionesBORRAR = IngresoRecuperado.Sum(x => x.CobroComisionMontoPagado)
                    + IngresoAdelanto.Sum(x => x.CobroComisionMontoPagado) + IngresoRealMes.Sum(x => x.CobroComisionMontoPagado) + IngresoRealVentasSinInHouse.Sum(x => x.CobroComisionMontoPagado)
                    + IngresoRealInHouseConCuota1.Sum(x => x.CobroComisionMontoPagado) + resultOtrosIngresosPorFechaPago.Sum(x => x.CobroComisionMontoPagado);


                // Es la diferencia del monto pagado Con el tipo de Cambio de Su Fecha de Pago menos el montoPagado con el tipo de Cambio de su Fecha de Matricula
                var DiferenciaPorTipoCambio = (PagosOperaciones.Sum(x => x.MontoPagadoTipoCambioFechaPago) + IngresoRealVentasSinInHouse.Sum(x => x.MontoPagadoTipoCambioFechaPago) +
                    IngresoRealInHouseConCuota1.Sum(x => x.MontoPagadoTipoCambioFechaPago) + resultOtrosIngresosPorFechaPago.Sum(x => x.MontoPagadoTipoCambioFechaPago)) -
                    (ItemSinInHouse.Sum(x => x.MontoPagado) + IngresoRealInHouseConCuota1.Sum(x => x.MontoPagado) + resultOtrosIngresosPorFechaPago.Sum(x => x.MontoPagado));
                               

                var IngresoDolaresTotalTipoCambioFechaMatricula = ItemSinInHouse.Sum(x => x.MontoPagado) + IngresoRealInHouseConCuota1.Sum(x => x.MontoPagado) + resultOtrosIngresos.Sum(x => x.MontoPagado);
                var IngresoDolaresTotalTipoCambioFechaPago = ItemSinInHouse.Sum(x => x.MontoPagadoTipoCambioFechaPago) + IngresoRealInHouseConCuota1.Sum(x => x.MontoPagadoTipoCambioFechaPago) + resultOtrosIngresos.Sum(x => x.MontoPagadoTipoCambioFechaPago);

                /////////////CONVIERTO EL FORMATO A EL FORMATO NECESITADO/////////////////////////

                List<ReporteIngresosDetalleDTO> detalles = new List<ReporteIngresosDetalleDTO>();

                ReporteIngresosDetalleDTO detalle1 = new ReporteIngresosDetalleDTO();
                detalle1.Tipo = "Ingreso Real de Operaciones por Adelantos($)";
                detalle1.Valor = SumaIngresoAdelanto;
                detalles.Add(detalle1);

                ReporteIngresosDetalleDTO detalle2 = new ReporteIngresosDetalleDTO();
                detalle2.Tipo = "Ingreso Real de Operaciones($)";
                detalle2.Valor = SumaIngresoRealMes;
                detalles.Add(detalle2);

                ReporteIngresosDetalleDTO detalle3 = new ReporteIngresosDetalleDTO();
                detalle3.Tipo = "Ingreso Real de Operaciones por Recuperaciones($)";
                detalle3.Valor = SumaIngresoRecuperado;
                detalles.Add(detalle3);

                ReporteIngresosDetalleDTO detalle4 = new ReporteIngresosDetalleDTO();
                detalle4.Tipo = "Ingreso Real de Ventas($)";
                detalle4.Valor = SumaIngresoRealVentas;
                detalles.Add(detalle4);

                ReporteIngresosDetalleDTO detalle5 = new ReporteIngresosDetalleDTO();
                detalle5.Tipo = "Inhouse($)";
                detalle5.Valor = SumaTotalInHouse;
                detalles.Add(detalle5);
                //////////////////////////////////inicio
                ReporteIngresosDetalleDTO detalle6 = new ReporteIngresosDetalleDTO();
                detalle6.Tipo = "Otros Ingresos($)";
                detalle6.Valor = SumaTotalOtrosIngresos;
                detalles.Add(detalle6);


                ReporteIngresosDetalleDTO detalle12 = new ReporteIngresosDetalleDTO();
                detalle12.Tipo = "Otros Egresos($)";
                detalle12.Valor = (-1) * SumaTotalOtrosEgresos;
                detalles.Add(detalle12);

                ReporteIngresosDetalleDTO detalle7 = new ReporteIngresosDetalleDTO();
                detalle7.Tipo = "Diferencia por Tipo Cambio($)";
                detalle7.Valor = DiferenciaPorTipoCambio;
                detalles.Add(detalle7);

                ReporteIngresosDetalleDTO detalle8 = new ReporteIngresosDetalleDTO();
                detalle8.Tipo = "Monto en Flujo($)";
                detalle8.Valor = SumaIngresoAdelanto + SumaIngresoRealMes + SumaIngresoRecuperado + SumaIngresoRealVentas + SumaTotalInHouse + SumaTotalOtrosIngresos + DiferenciaPorTipoCambio;
                detalles.Add(detalle8);

                ReporteIngresosDetalleDTO detalle9 = new ReporteIngresosDetalleDTO();
                detalle9.Tipo = "Ingresos Registrados Anteriores pero Ingresados en el Periodo Seleccionado($)";
                detalle9.Valor = SumaIngresosMesConFechaPagoMesAnterior;
                detalles.Add(detalle9);

                ReporteIngresosDetalleDTO detalle10 = new ReporteIngresosDetalleDTO();
                detalle10.Tipo = "Ingresos Registrados en el Periodo Seleccionado pero Registrados despues($)";
                detalle10.Valor = (-1) * SumaIngresosMesSiguienteConFechaPagoMesActual;
                detalles.Add(detalle10);

                ReporteIngresosDetalleDTO detalle11 = new ReporteIngresosDetalleDTO();
                detalle11.Tipo = "Comisiones por Comercio Electronico($)";
                detalle11.Valor = (-1) * SumaTotalComisiones;
                detalles.Add(detalle11);

                return Ok(PRUEBABORRARSumaIngresosMesSiguienteConFechaPagoMesActual);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }
}