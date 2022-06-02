using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReportePagosIngresos")]
    public class ReportePagosIngresosController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public ReportePagosIngresosController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerIdNombreCentroCostoAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
           
            try
            {
                if (Filtros != null)
                {
                    CentroCostoRepositorio repCentroCostoRep = new CentroCostoRepositorio();
                    if (Filtros != null && Filtros["Valor"] != null)
                    {
                        return Ok(repCentroCostoRep.ObtenerTodoFiltroAutoComplete(Filtros["Valor"].ToString()));
                    }
                    return Ok(repCentroCostoRep.ObtenerTodoFiltroAutoComplete(""));
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerAlumnoAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            
            try
            {
                if (Filtros != null)
                {
                    AlumnoRepositorio _repAlumno = new AlumnoRepositorio();
                    return Ok(_repAlumno.ObtenerTodoFiltroAutoComplete(Filtros["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCodigoMatriculaAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            
            try
            {
                if (Filtros != null)
                {
                    MatriculaCabeceraRepositorio repCentroCostoRep = new MatriculaCabeceraRepositorio();
                    if (Filtros != null && Filtros["Valor"] != null)
                    {
                        return Ok(repCentroCostoRep.ObtenerCodigoMatriculaAutocompleto(Filtros["Valor"].ToString()));
                    }
                    return Ok(repCentroCostoRep.ObtenerCodigoMatriculaAutocompleto(""));
                }
                else
                {
                    return Ok();
                }
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaFormaPago()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                FormaPagoRepositorio _repoFormaPago = new FormaPagoRepositorio();
                var lista = _repoFormaPago.ObtenerListaFormaPago();
                return Ok(lista);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerModalidadCurso()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                ModalidadCursoRepositorio _repoFormaPago = new ModalidadCursoRepositorio();
                var lista = _repoFormaPago.ObtenerModalidadCursoFiltro();
                return Ok(lista);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCiudadSedes()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CiudadRepositorio _repCiudad = new CiudadRepositorio(_integraDBContext);
                return Ok(_repCiudad.ObtenerCiudadesDeSedesExistentes());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReportePagoFiltroDTO FiltroIngresos)
        {
            try
            {
                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();
                FeriadoRepositorio repFeriadoRep = new FeriadoRepositorio();
                reportesRepositorio.ActualizarCronogramaVersionFinal();
                var result = reportesRepositorio.ObtenerReportePagosIngresos(FiltroIngresos).ToList();
                var ListFeriados = repFeriadoRep.GetBy(x => x.Estado == true).ToList();

                foreach (var item in result)
                {

                    //Aqui les calculo a todos el porcentaje de cobro 
                    item.TotalPagadoDisponible = item.TotalPagado * (item.PorcentajeCobro / 100);
                    //item.Cuota = item.Cuota * (item.PorcentajeCobro / 100);
                    //Fin Aqui les calculo a todos el porcentaje de cobro
                    item.Numero = item.Numero.Replace("-", "");
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

                    if (DateTime.Now.Date >= item.FechaDisponible.Value.Date)
                        item.EstadoEfectivo = "Disponible";
                    else
                        if (DateTime.Now.Date >= item.FechaDepositaron.Value.Date)
                        item.EstadoEfectivo = "Depositado";
                }             

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[Action]/{Fecha}/{Usuario}")]
        [HttpGet]
        /// Tipo Función: GET/
        /// Autor: Miguel
        /// Fecha: 12/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Congela los datos del reporte de pagos por dia en base a la fecha 
        /// </summary>
        /// <returns>Objeto</returns>
        public ActionResult GenerarCongelamientoReporteDePagosPorDia(string Fecha, string Usuario)
        {
            try
            {

                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();

                var congelamientoReportePagosPorDia = reportesRepositorio.CongelarReporteDePagosPorDia(Fecha, Usuario);

                return Ok(congelamientoReportePagosPorDia);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[Action]/{Fecha}/{Usuario}/{Periodo}")]
        [HttpGet]
        /// Tipo Función: GET/
        /// Autor: Miguel
        /// Fecha: 12/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Congela los datos del reporte de pagos por periodo en base a la fecha 
        /// </summary>
        /// <returns>Objeto</returns>
        public ActionResult GenerarCongelamientoReporteDePagosPorPeriodo(string Fecha, string Usuario,int Periodo)
        {
            try
            {

                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();

                var congelamientoReportePagosPorPeriodo = reportesRepositorio.CongelarReporteDePagosPorPeriodo(Fecha, Usuario,Periodo);

                return Ok(congelamientoReportePagosPorPeriodo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}