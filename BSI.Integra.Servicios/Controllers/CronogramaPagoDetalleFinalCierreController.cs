using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using static BSI.Integra.Servicios.Controllers.GastoFinancieroCronogramaController;
using static BSI.Integra.Servicios.Controllers.PanelControlMetaController;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CronogramaPagoDetalleFinalCierre")]
    public class CronogramaPagoDetalleFinalCierreController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public CronogramaPagoDetalleFinalCierreController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
      
        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] CompuestoReporteResumenMontosDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CronogramaPagoDetalleFinalCierreRepositorio cronogramaPagoDetalleFinalCierreRepositorio = new CronogramaPagoDetalleFinalCierreRepositorio(_integraDBContext);
                CronogramaPagoDetalleFinalCierreBO cronogramaPagoDetalleFinalCierreBO = new CronogramaPagoDetalleFinalCierreBO();
                PeriodoRepositorio periodoRepositorio = new PeriodoRepositorio(_integraDBContext);
                var periodo = periodoRepositorio.ObtenerIdPeriodoFechaActual();

                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var itemMonto in Json.ListaPeriodoActual)
                    {
                        foreach (var itemMontoPeriodo in itemMonto.l)
                        {
                            if (itemMontoPeriodo.TipoMonto.Equals("Proyectado Actual($)"))
                            {
                                cronogramaPagoDetalleFinalCierreBO.IdPeriodoCorte = periodo.FirstOrDefault().Id;
                                cronogramaPagoDetalleFinalCierreBO.PeriodoNombre = itemMonto.g;
                                if (itemMontoPeriodo.TipoMonto.Equals("Proyectado Actual($)"))
                                {
                                    cronogramaPagoDetalleFinalCierreBO.MontoProyectado = Convert.ToDecimal(itemMontoPeriodo.Monto);
                                }
                                if (itemMontoPeriodo.TipoMonto.Equals("Real($)"))
                                {
                                    cronogramaPagoDetalleFinalCierreBO.MontoPagado = Convert.ToDecimal(itemMontoPeriodo.Monto);
                                }
                                cronogramaPagoDetalleFinalCierreBO.Estado = true;
                                cronogramaPagoDetalleFinalCierreBO.UsuarioCreacion = Json.Usuario;
                                cronogramaPagoDetalleFinalCierreBO.UsuarioModificacion = Json.Usuario;
                                cronogramaPagoDetalleFinalCierreBO.FechaCreacion = DateTime.Now;
                                cronogramaPagoDetalleFinalCierreBO.FechaModificacion = DateTime.Now;

                                cronogramaPagoDetalleFinalCierreRepositorio.Insert(cronogramaPagoDetalleFinalCierreBO);
                            }
                        }
                    }
                    
                    scope.Complete();
                }
                return Ok(cronogramaPagoDetalleFinalCierreBO);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] CompuestoGastoFinancieroCronogramaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GastoFinancieroCronogramaRepositorio gastoFinancieroCronogramaRepositorio = new GastoFinancieroCronogramaRepositorio(_integraDBContext);
                GastoFinancieroCronogramaDetalleRepositorio gastoFinancieroCronogramaDetalleRepositorio = new GastoFinancieroCronogramaDetalleRepositorio(_integraDBContext);
                GastoFinancieroCronogramaBO gastoFinancieroCronogramaBO = new GastoFinancieroCronogramaBO();

                using (TransactionScope scope = new TransactionScope())
                {
                    if (gastoFinancieroCronogramaRepositorio.Exist(Json.GastoFinancieroCronograma.Id))
                    {
                        gastoFinancieroCronogramaDetalleRepositorio.EliminacionLogicoPorCronograma(Json.GastoFinancieroCronograma.Id, Json.Usuario, Json.GastoFinancieroCronogramaDetalle);

                        gastoFinancieroCronogramaBO = gastoFinancieroCronogramaRepositorio.FirstById(Json.GastoFinancieroCronograma.Id);
                        gastoFinancieroCronogramaBO.Nombre = Json.GastoFinancieroCronograma.Nombre;
                        gastoFinancieroCronogramaBO.IdEntidadFinanciera = Json.GastoFinancieroCronograma.IdEntidadFinanciera;
                        gastoFinancieroCronogramaBO.IdMoneda = Json.GastoFinancieroCronograma.IdMoneda;
                        gastoFinancieroCronogramaBO.CapitalTotal = Json.GastoFinancieroCronograma.CapitalTotal;
                        gastoFinancieroCronogramaBO.InteresTotal = Json.GastoFinancieroCronograma.InteresTotal;
                        gastoFinancieroCronogramaBO.FechaInicio = Json.GastoFinancieroCronograma.FechaInicio;
                        gastoFinancieroCronogramaBO.Estado = true;
                        gastoFinancieroCronogramaBO.UsuarioModificacion = Json.Usuario;
                        gastoFinancieroCronogramaBO.FechaModificacion = DateTime.Now;

                        gastoFinancieroCronogramaBO.GastoFinancieroCronogramaDetalle = new List<GastoFinancieroCronogramaDetalleBO>();

                        foreach (var item in Json.GastoFinancieroCronogramaDetalle)
                        {
                            GastoFinancieroCronogramaDetalleBO gastoFinancieroCronogramaDetalleBO;
                            if (gastoFinancieroCronogramaDetalleRepositorio.Exist(x => x.Id == item.Id))
                            {
                                gastoFinancieroCronogramaDetalleBO = gastoFinancieroCronogramaDetalleRepositorio.FirstBy(x => x.Id == item.Id && x.IdGastoFinancieroCronograma == Json.GastoFinancieroCronograma.Id);
                                gastoFinancieroCronogramaDetalleBO.NumeroCuota = item.NumeroCuota;
                                gastoFinancieroCronogramaDetalleBO.CapitalCuota = item.CapitalCuota;
                                gastoFinancieroCronogramaDetalleBO.InteresCuota = item.InteresCuota;
                                gastoFinancieroCronogramaDetalleBO.FechaVencimientoCuota = item.FechaVencimientoCuota;
                                gastoFinancieroCronogramaDetalleBO.UsuarioModificacion = Json.Usuario;
                                gastoFinancieroCronogramaDetalleBO.FechaModificacion = DateTime.Now;
                                gastoFinancieroCronogramaDetalleBO.Estado = true;

                            }
                            else
                            {
                                gastoFinancieroCronogramaDetalleBO = new GastoFinancieroCronogramaDetalleBO();
                                gastoFinancieroCronogramaDetalleBO.NumeroCuota = item.NumeroCuota;
                                gastoFinancieroCronogramaDetalleBO.CapitalCuota = item.CapitalCuota;
                                gastoFinancieroCronogramaDetalleBO.InteresCuota = item.InteresCuota;
                                gastoFinancieroCronogramaDetalleBO.FechaVencimientoCuota = item.FechaVencimientoCuota;
                                gastoFinancieroCronogramaDetalleBO.UsuarioCreacion = Json.Usuario;
                                gastoFinancieroCronogramaDetalleBO.UsuarioModificacion = Json.Usuario;
                                gastoFinancieroCronogramaDetalleBO.FechaCreacion = DateTime.Now;
                                gastoFinancieroCronogramaDetalleBO.FechaModificacion = DateTime.Now;
                                gastoFinancieroCronogramaDetalleBO.Estado = true;
                            }
                            gastoFinancieroCronogramaBO.GastoFinancieroCronogramaDetalle.Add(gastoFinancieroCronogramaDetalleBO);
                        }

                        gastoFinancieroCronogramaRepositorio.Update(gastoFinancieroCronogramaBO);
                        scope.Complete();
                    }
                }
                return Ok(gastoFinancieroCronogramaBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Eliminar([FromBody] EliminarDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GastoFinancieroCronogramaRepositorio gastoFinancieroCronogramaRepositorio = new GastoFinancieroCronogramaRepositorio(_integraDBContext);
                GastoFinancieroCronogramaDetalleRepositorio gastoFinancieroCronogramaDetalleRepositorio = new GastoFinancieroCronogramaDetalleRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    if (gastoFinancieroCronogramaRepositorio.Exist(Json.Id))
                    {
                        gastoFinancieroCronogramaRepositorio.Delete(Json.Id, Json.NombreUsuario);

                        var hijosGastoFinancieroCronograma = gastoFinancieroCronogramaDetalleRepositorio.GetBy(x => x.IdGastoFinancieroCronograma == Json.Id);
                        foreach (var hijo in hijosGastoFinancieroCronograma)
                        {
                            gastoFinancieroCronogramaDetalleRepositorio.Delete(hijo.Id, Json.NombreUsuario);
                        }
                    }

                    scope.Complete();
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReportePrestamos([FromBody] FiltroReportePrestamoDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                GastoFinancieroCronogramaRepositorio _repoGastoFinancieroCronograma = new GastoFinancieroCronogramaRepositorio(_integraDBContext);

                var Lista = _repoGastoFinancieroCronograma.ObtenerReportePrestamos(Filtro.IdEntidadFinanciera, Filtro.IdGastoFinancieroCronograma);

                return Ok(Lista);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        public class ValidadorGastoFinancieroCronogramaDTO : AbstractValidator<TGastoFinancieroCronograma>
        {
            public static ValidadorGastoFinancieroCronogramaDTO Current = new ValidadorGastoFinancieroCronogramaDTO();
            public ValidadorGastoFinancieroCronogramaDTO()
            {
                RuleFor(objeto => objeto.Nombre).NotEmpty().WithMessage("Nombre es Obligatorio");

            }
        }
    }
}
