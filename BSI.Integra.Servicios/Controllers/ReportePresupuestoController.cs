using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Reportes.Comercial.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ReportePresupuesto")]
    public class ReportePresupuestoController : ControllerBase
    {

        private readonly integraDBContext _integraDBContext;
        public ReportePresupuestoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosPresupuesto()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CiudadRepositorio repCiudad = new CiudadRepositorio();
                FurTipoPedidoRepositorio repFurTipoPedido = new FurTipoPedidoRepositorio();
                FurTipoSolicitudRepositorio repFurTipoSolicitud = new FurTipoSolicitudRepositorio();
                FurSubFaseAprobacionRepositorio repFursubFaseAprobacion = new FurSubFaseAprobacionRepositorio();
                ComprobantePagoEstadoRepositorio repComprobantePagoEstado = new ComprobantePagoEstadoRepositorio();
                FurFaseAprobacionRepositorio repFurFaseAprobacion = new FurFaseAprobacionRepositorio();
                PeriodoRepositorio repPeriodo = new PeriodoRepositorio();

                CombosPresupuestoDTO comboFurPago = new CombosPresupuestoDTO();

                comboFurPago.ListaCiudades = repCiudad.ObtenerCiudadesDeSedesExistentes();
                comboFurPago.ListaTipoPedido = repFurTipoPedido.ObtenerFurTiposPedidos();
                comboFurPago.ListaRubro = repFurTipoSolicitud.ObtenerFurTipoSolicitud();
                comboFurPago.ListaEstadoFaseFur = repFurFaseAprobacion.ObtenerListaFurFaseEstado();
                comboFurPago.ListaEstadoSubFaseFur = repFursubFaseAprobacion.ObtenerListaFurSubFaseEstado();
                comboFurPago.ListaEstadoComprobante = repComprobantePagoEstado.ObtenerListaComprobantePagoEstado();

                return Ok(comboFurPago);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPeriodos()
        {
            try
            {
                PeriodoRepositorio periodos = new PeriodoRepositorio();
                List<PeriodoFiltroDTO> listaPeriodos = new List<PeriodoFiltroDTO>();
                listaPeriodos = periodos.ObtenerPeriodosPresupuesto();

                return Ok(listaPeriodos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerRucNombreProveedorAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                ProveedorRepositorio repProveedorRep = new ProveedorRepositorio();
                if (Filtros != null && Filtros["Valor"] != null)
                {
                    return Ok(repProveedorRep.ObtenerProveedorRucAutocomplete(Filtros["Valor"].ToString()));
                }
                return Ok(repProveedorRep.ObtenerNombreProveedorAutocomplete(""));
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerIdNombreCentroCostoAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                CentroCostoRepositorio repCentroCostoRep = new CentroCostoRepositorio();
                if (Filtros != null && Filtros["Valor"] != null)
                {
                    return Ok(repCentroCostoRep.ObtenerTodoFiltroAutoComplete(Filtros["Valor"].ToString()));
                }
                return Ok(repCentroCostoRep.ObtenerTodoFiltroAutoComplete(""));
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerPlanContableAutoComplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                PlanContableRepositorio repPlanContableRep = new PlanContableRepositorio();
                if (Filtros != null && Filtros["Valor"] != null)
                {
                    return Ok(repPlanContableRep.ObtenerPlanContableAutoComplete(Filtros["Valor"].ToString()));
                }
                return Ok(repPlanContableRep.ObtenerPlanContableAutoComplete(""));
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerNombresFiltroAutoComplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
               PersonalRepositorio repPersonalRep = new PersonalRepositorio();
                if (Filtros != null && Filtros["Valor"] != null)
                {
                    return Ok(repPersonalRep.ObtenerNombresFiltroAutoComplete(Filtros["Valor"].ToString()));
                }
                return Ok(repPersonalRep.ObtenerNombresFiltroAutoComplete(""));
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaFurAutocomplete(string NombreParcial)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // evita que se devuelva todos los nombre (  todos encajan con string vacio ("")  )
                if (NombreParcial == null || NombreParcial.Trim().Equals(""))
                {
                    Object[] ListaVacia = new object[0];
                    return Ok(new { Result = "OK", Records = ListaVacia });
                }

                FurRepositorio _repoFur = new FurRepositorio();
                var lista = _repoFur.ObtenerDatosFur(NombreParcial);
                return Ok(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCodigoFurAutoComplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                FurRepositorio repFurRep = new FurRepositorio();
                if (Filtros != null && Filtros["Valor"] != null)
                {
                    return Ok(repFurRep.ObtenerFursBusquedaCodigoAutoComplete(Filtros["Valor"].ToString()));
                }
                return Ok(repFurRep.ObtenerFursBusquedaCodigoAutoComplete(""));
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarReporte([FromBody] ReportePresupuestoFiltroDTO Filtros)
        {
            try
            {
                ReportesRepositorio reportesRepositorio = new ReportesRepositorio();
                var presupuesto = reportesRepositorio.ObtenerReportePresupuestoFinanzas(Filtros);
                if (Filtros.periodoProgramacionActual.Count() > 0) {
                    var PresupuestoModificado = (
                      from p in presupuesto
                      group p by new
                      { p.IdFur
                        ,p.Empresa
                        ,p.Sede
                        ,p.Area
                        ,p.TipoPedido
                        ,p.CodigoFur
                        ,p.ProductoServicio
                        ,p.Descripcion
                        ,p.CentroCosto
                        ,p.Curso
                        ,p.Programa
                        ,p.MonedaProyectada
                        ,p.CantidadProyectada
                        ,p.PresentacionProyectada
                        ,p.PrecioUnitarioProyectado
                        ,p.PrecioTotalOrigenProyectado
                        ,p.PrecioTotalDolaresProyectado
                        ,p.MonedaProveedor
                        ,p.Cantidad
                        ,p.Unidad
                        ,p.PrecioUnitarioJF
                        ,p.PrecioTotalOrigenJF
                        ,p.PrecioTotalDolaresJF
                        ,p.Atipico
                        ,p.Rubro
                        ,p.NroCuenta
                        ,p.Cuenta
                        ,p.UsuarioCreacion
                        ,p.UsuarioAprobacion
                        ,p.UsuarioModificacion
                        ,p.SubFaseFur
                        ,p.FaseAprobacion
                        ,p.FechaLimiteFur
                        ,p.MesLimiteFur
                        ,p.EsDiferido

                      } into g
                      select new ReportePresupuestoDTO
                      {
                         IdFur = g.Key.IdFur
                        ,Empresa=g.Key.Empresa
                        ,Sede=g.Key.Sede
                        ,Area=g.Key.Area
                        ,TipoPedido=g.Key.TipoPedido
                        ,CodigoFur=g.Key.CodigoFur
                        ,ProductoServicio=g.Key.ProductoServicio
                        ,Descripcion=g.Key.Descripcion
                        ,CentroCosto=g.Key.CentroCosto
                        ,Curso=g.Key.Curso
                        ,Programa=g.Key.Programa
                        ,MonedaProyectada=g.Key.MonedaProyectada
                        ,CantidadProyectada=g.Key.CantidadProyectada
                        ,PresentacionProyectada=g.Key.PresentacionProyectada
                        ,PrecioUnitarioProyectado=g.Key.PrecioUnitarioProyectado
                        ,PrecioTotalOrigenProyectado=g.Key.PrecioTotalOrigenProyectado
                        ,PrecioTotalDolaresProyectado=g.Key.PrecioTotalDolaresProyectado
                        ,MonedaProveedor=g.Key.MonedaProveedor
                        ,Cantidad=g.Key.Cantidad
                        ,Unidad=g.Key.Unidad
                        ,PrecioUnitarioJF=g.Key.PrecioUnitarioJF
                        ,PrecioTotalOrigenJF=g.Key.PrecioTotalOrigenJF
                        ,PrecioTotalDolaresJF=g.Key.PrecioTotalDolaresJF
                        ,Atipico=g.Key.Atipico
                        ,Rubro=g.Key.Rubro
                        ,NroCuenta=g.Key.NroCuenta
                        ,Cuenta=g.Key.Cuenta
                        ,UsuarioCreacion=g.Key.UsuarioCreacion
                        ,UsuarioAprobacion=g.Key.UsuarioAprobacion
                        ,UsuarioModificacion=g.Key.UsuarioModificacion
                        ,FaseAprobacion=g.Key.FaseAprobacion
                        ,SubFaseFur=g.Key.SubFaseFur
                        ,FechaLimiteFur=g.Key.FechaLimiteFur
                        ,MesLimiteFur=g.Key.MesLimiteFur
                        ,EsDiferido=g.Key.EsDiferido
                        ,RUC=string.Join("/", g.Select(x => x.RUC).Distinct().ToList())
                        ,Proveedor=string.Join("/", g.Select(x => x.Proveedor).Distinct().ToList())
                        ,TipoComprobante=string.Join("/", g.Select(x => x.TipoComprobante).Distinct().ToList()).Replace("/SIN COMPROBANTE", "").Replace("SIN COMPROBANTE/", "")
                        ,NumeroComprobante=string.Join("/", g.Select(x => x.NumeroComprobante).Distinct().ToList()).Replace("/SIN COMPROBANTE", "").Replace("SIN COMPROBANTE/", "")
                        ,MonedaComprobante= g.Select(x => x.MonedaComprobante).FirstOrDefault()
                        ,MontoPorPagar=g.Select(x => x.MontoPorPagar).Sum()
                        ,MontoPorPagarDolares=g.Select(x => x.MontoPorPagarDolares).Sum()
                        ,FechaEmisionComprobante=g.Where(x => x.FechaEmisionComprobante != null).Select(x => x.FechaEmisionComprobante).FirstOrDefault()
                        ,MesEmision=string.Join("/", g.Select(x => x.MesEmision).Distinct().ToList())
                        ,FechaVencimientoComprobante= g.Where(x => x.FechaVencimientoComprobante != null).Select(x => x.FechaVencimientoComprobante).FirstOrDefault()
                        ,MesVencimiento=string.Join("/", g.Select(x => x.MesVencimiento).Distinct().ToList())
                        ,MonedaPago=string.Join("/", g.Select(x => x.MonedaPago).Distinct().ToList()).Replace("/NO PAGADO", "").Replace("NO PAGADO/", "")
                        ,FormaPago=string.Join("/", g.Select(x => x.FormaPago).Distinct().ToList()).Replace("/NO PAGADO", "").Replace("NO PAGADO/", "")
                        ,MontoRealPagado=g.Select(x => x.MontoRealPagado).Sum()
                        ,MontoRealPagadoDolares=g.Select(x => x.MontoRealPagadoDolares).Sum()
                        ,NumeroReciboBanco=string.Join("/", g.Select(x => x.NumeroReciboBanco).Distinct().ToList()).Replace("/NO PAGADO", "").Replace("NO PAGADO/", "")
                        ,EntidadFinanciera=string.Join("/", g.Select(x => x.EntidadFinanciera).Distinct().ToList()).Replace("/NO PAGADO", "").Replace("NO PAGADO/", "")
                        ,CuentaCorriente=string.Join("/", g.Select(x => x.CuentaCorriente).Distinct().ToList()).Replace("/NO PAGADO", "").Replace("NO PAGADO/", "")
                        ,FechaPagoBanco=g.Where(x => x.FechaPagoBanco!=null).Select(x => x.FechaPagoBanco).FirstOrDefault()
                        ,MesPagoBanco=string.Join("/", g.Select(x => x.MesPagoBanco).Distinct().ToList())
                        ,FechaProgramacionOriginal=g.Select(x => x.FechaProgramacionOriginal).FirstOrDefault()
                        ,MesProgramacionOriginal=string.Join("/", g.Select(x => x.MesProgramacionOriginal).Distinct().ToList())
                        ,FechaProgramacionActual=g.Select(x => x.FechaProgramacionActual).FirstOrDefault()
                        ,MesProgramacionActual=string.Join("/", g.Select(x => x.MesProgramacionActual).Distinct().ToList())
                        ,EstadoComprobante=string.Join("/", g.Select(x => x.EstadoComprobante).Distinct().ToList())
                      }
               );
                    return Ok(PresupuestoModificado);
                }
                else { 
                return Ok(presupuesto);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarFechaReprogramacion([FromBody] FechaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (Json.idComprobantePago == null || Json.idComprobantePago == 0)
                {
                    FurRepositorio repFurRep = new FurRepositorio(_integraDBContext);
                    var fur = repFurRep.FirstById(Json.idFur.Value);
                    using (TransactionScope scope = new TransactionScope())
                    {
                        //fur.fec = Json.Observacion;
                        fur.FechaLimiteReprogramacion = Json.FechaReprogramacion;
                        fur.UsuarioModificacion = Json.UsuarioModificacion;
                        fur.FechaModificacion = DateTime.Now;

                        repFurRep.Update(fur);

                        scope.Complete();
                    }
                }
                else {
                    ComprobantePagoRepositorio repComprobanteRep = new ComprobantePagoRepositorio(_integraDBContext);
                    var comprobante = repComprobanteRep.FirstById(Json.idComprobantePago.Value);

                    using (TransactionScope scope = new TransactionScope())
                    {
                        //fur.fec = Json.Observacion;
                        comprobante.FechaVencimientoReprogramacion = Json.FechaReprogramacion;
                        comprobante.UsuarioModificacion = Json.UsuarioModificacion;
                        comprobante.FechaModificacion = DateTime.Now;

                        repComprobanteRep.Update(comprobante);

                        scope.Complete();
                    }
                }                return Ok(Json);

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult DiferirFur([FromBody] ListaEnterosDTO listaEnteros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FurRepositorio repFurRep = new FurRepositorio(_integraDBContext);
                ComprobantePagoRepositorio repComprobantePagoRep = new ComprobantePagoRepositorio(_integraDBContext);



                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var IdFur in listaEnteros.ListaEnteros)
                    {
                        var FurItem = repFurRep.FirstById(IdFur);
                        if (FurItem.EsDiferido != true)
                        {
                            FurItem.EsDiferido = true;
                            FurItem.FechaLimiteReprogramacion = listaEnteros.FechaDiferida;
                            FurItem.UsuarioModificacion = listaEnteros.UsuarioModificacion;
                            FurItem.FechaModificacion = DateTime.Now;

                            repFurRep.Update(FurItem);
                        }
                    }                    
                    scope.Complete();
                }

                return Ok();

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult DesDiferirFur([FromBody] ListaEnterosDTO listaEnteros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FurRepositorio repFurRep = new FurRepositorio(_integraDBContext);
                ComprobantePagoRepositorio repComprobantePagoRep = new ComprobantePagoRepositorio(_integraDBContext);



                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var IdFur in listaEnteros.ListaEnteros)
                    {
                        var FurItem = repFurRep.FirstById(IdFur);
                        if (FurItem.EsDiferido == true)
                        {
                            FurItem.EsDiferido = false;
                            FurItem.UsuarioModificacion = listaEnteros.UsuarioModificacion;
                            FurItem.FechaModificacion = DateTime.Now;

                            repFurRep.Update(FurItem);
                        }
                    }
                    scope.Complete();
                }

                return Ok();

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
    }
}