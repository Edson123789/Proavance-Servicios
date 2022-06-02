using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ActualizarFechasCronogramaPagoFinal")]
    [ApiController]
    public class ActualizarFechasCronogramaPagoFinalController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public ActualizarFechasCronogramaPagoFinalController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerPeriodo()
        {
            try
            {
                PeriodoRepositorio periodos = new PeriodoRepositorio();
                List<FiltroDTO> listaPeriodos = new List<FiltroDTO>();
                listaPeriodos = periodos.ObtenerPeriodosPendiente();

                return Ok(listaPeriodos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpGet]
        public ActionResult GenerarReporte(DateTime FechaInicioFiltro,DateTime FechaFinFiltro, int? IdCambio)
        {
            try
            {
                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();
                FeriadoRepositorio repFeriadoRep = new FeriadoRepositorio();
                PeriodoRepositorio repPeriodo = new PeriodoRepositorio();
                ReportePagoFiltroDTO Filtro = new ReportePagoFiltroDTO();
                Filtro.IdCambio = IdCambio;
                Filtro.FechaInicio = FechaInicioFiltro;
                Filtro.FechaFin = FechaFinFiltro;
                var result = reportesRepositorio.ObtenerReportePagoAlumnos(Filtro).ToList();
                var ListFeriados = repFeriadoRep.GetBy(x => x.Estado == true).ToList();

                DateTime? FechaDisponibleOriginal = null;
                DateTime? FechaIngresoEnCuentaOriginal =null;
                foreach (var item in result)
                {
                    FechaDisponibleOriginal = item.FechaDisponible;
                    FechaIngresoEnCuentaOriginal = item.FechaDepositaron;

                    if (item.CuentaFeriados == false && item.ConsiderarDiasHabilesLV == false && item.ConsiderarDiasHabilesLS == false)
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
                                    if ((item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" && item.ConsiderarDiasHabilesLS == true) || ((item.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday" || item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday") && item.ConsiderarDiasHabilesLV == true))
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
                                        if ((item.ConsiderarDiasHabilesLV == true && (item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDepositaron.Value.DayOfWeek.ToString() == "Saturday")) || (item.ConsiderarDiasHabilesLS == true && item.FechaDepositaron.Value.DayOfWeek.ToString() == "Sunday"))
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
                                    if ((item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" && item.ConsiderarDiasHabilesLS == true) || ((item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday") && item.ConsiderarDiasHabilesLV == true))
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
                                        if ((item.ConsiderarDiasHabilesLV == true && (item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday" || item.FechaDisponible.Value.DayOfWeek.ToString() == "Saturday")) || (item.ConsiderarDiasHabilesLS == true && item.FechaDisponible.Value.DayOfWeek.ToString() == "Sunday"))
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

                    if (DateTime.Now.Date >= item.FechaDisponible.Value.Date)
                        item.EstadoEfectivo = "Disponible";
                    else
                        if (DateTime.Now.Date >= item.FechaDepositaron.Value.Date)
                        item.EstadoEfectivo = "Depositado";
                    item.FechaDisponible = FechaDisponibleOriginal == null ? item.FechaDisponible : FechaDisponibleOriginal;
                    item.FechaDepositaron = FechaIngresoEnCuentaOriginal == null ? item.FechaDepositaron : FechaIngresoEnCuentaOriginal;
                }

                return Ok(result.OrderByDescending(x=> x.FechaProcesoPago));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult CambiarFechaProcesoPago([FromBody] ListaEnterosDTO listaEnteros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CronogramaPagoDetalleFinalRepositorio repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var IdCronograma in listaEnteros.ListaEnteros)
                    {
                        var CronogramaPagoDetalleFinalItem = repCronogramaPagoDetalleFinal.FirstById(IdCronograma);

                        CronogramaPagoDetalleFinalItem.FechaProcesoPagoReal = listaEnteros.FechaDiferida;
                        CronogramaPagoDetalleFinalItem.UsuarioModificacion = listaEnteros.UsuarioModificacion;
                        CronogramaPagoDetalleFinalItem.FechaModificacion = DateTime.Now;

                        repCronogramaPagoDetalleFinal.Update(CronogramaPagoDetalleFinalItem);
                        
                    }
                    scope.Complete();
                }

                return Ok(true);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult CambiarFechaIngresoCuenta([FromBody] ListaEnterosDTO listaEnteros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CronogramaPagoDetalleFinalRepositorio repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var IdCronograma in listaEnteros.ListaEnteros)
                    {
                        var CronogramaPagoDetalleFinalItem = repCronogramaPagoDetalleFinal.FirstById(IdCronograma);

                        CronogramaPagoDetalleFinalItem.FechaIngresoEnCuenta = listaEnteros.FechaDiferida;
                        CronogramaPagoDetalleFinalItem.UsuarioModificacion = listaEnteros.UsuarioModificacion;
                        CronogramaPagoDetalleFinalItem.FechaModificacion = DateTime.Now;

                        repCronogramaPagoDetalleFinal.Update(CronogramaPagoDetalleFinalItem);

                    }
                    scope.Complete();
                }

                return Ok(true);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult CambiarFechaEfectivoDisponible([FromBody] ListaEnterosDTO listaEnteros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CronogramaPagoDetalleFinalRepositorio repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var IdCronograma in listaEnteros.ListaEnteros)
                    {
                        var CronogramaPagoDetalleFinalItem = repCronogramaPagoDetalleFinal.FirstById(IdCronograma);

                        CronogramaPagoDetalleFinalItem.FechaEfectivoDisponible = listaEnteros.FechaDiferida;
                        CronogramaPagoDetalleFinalItem.UsuarioModificacion = listaEnteros.UsuarioModificacion;
                        CronogramaPagoDetalleFinalItem.FechaModificacion = DateTime.Now;

                        repCronogramaPagoDetalleFinal.Update(CronogramaPagoDetalleFinalItem);

                    }
                    scope.Complete();
                }

                return Ok(true);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarFechaProcesoPago([FromBody] FechaCronogramaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                    CronogramaPagoDetalleFinalRepositorio repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio(_integraDBContext);
                    var datosCuota = repCronogramaPagoDetalleFinal.FirstById(Json.idCronogramaPagoDetalleFinal);
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //fur.fec = Json.Observacion;
                        datosCuota.FechaProcesoPagoReal = Json.FechaReprogramacion;
                        datosCuota.UsuarioModificacion = Json.UsuarioModificacion;
                        datosCuota.FechaModificacion = DateTime.Now;

                    repCronogramaPagoDetalleFinal.Update(datosCuota);

                        scope.Complete();
                    }
                return Ok(Json);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarFechaIngresoCuenta([FromBody] FechaCronogramaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CronogramaPagoDetalleFinalRepositorio repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio(_integraDBContext);
                var datosCuota = repCronogramaPagoDetalleFinal.FirstById(Json.idCronogramaPagoDetalleFinal);
                using (TransactionScope scope = new TransactionScope())
                {
                    //fur.fec = Json.Observacion;
                    datosCuota.FechaIngresoEnCuenta = Json.FechaReprogramacion;
                    datosCuota.UsuarioModificacion = Json.UsuarioModificacion;
                    datosCuota.FechaModificacion = DateTime.Now;

                    repCronogramaPagoDetalleFinal.Update(datosCuota);

                    scope.Complete();
                }
                return Ok(Json);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarFechaEfectivoDisponible([FromBody] FechaCronogramaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CronogramaPagoDetalleFinalRepositorio repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio(_integraDBContext);
                var datosCuota = repCronogramaPagoDetalleFinal.FirstById(Json.idCronogramaPagoDetalleFinal);
                using (TransactionScope scope = new TransactionScope())
                {
                    //fur.fec = Json.Observacion;
                    datosCuota.FechaEfectivoDisponible = Json.FechaReprogramacion;
                    datosCuota.UsuarioModificacion = Json.UsuarioModificacion;
                    datosCuota.FechaModificacion = DateTime.Now;

                    repCronogramaPagoDetalleFinal.Update(datosCuota);

                    scope.Complete();
                }
                return Ok(Json);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
}
