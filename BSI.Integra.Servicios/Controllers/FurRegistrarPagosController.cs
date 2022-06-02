using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/FurRegistrarPagos")]
    public class FurRegistrarPagosController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public FurRegistrarPagosController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosFurPago()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PersonalAreaTrabajoRepositorio repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio();
                CiudadRepositorio repCiudad = new CiudadRepositorio();
                FurPagoRepositorio repFurPago = new FurPagoRepositorio();
                SunatDocumentoRepositorio repSunatDocumento = new SunatDocumentoRepositorio();
                TipoImpuestoRepositorio repTipoImpuesto = new TipoImpuestoRepositorio();
                RetencionRepositorio repRetencion = new RetencionRepositorio();
                DetraccionRepositorio repDetraccion = new DetraccionRepositorio();
                PaisRepositorio repPais = new PaisRepositorio();
                ComprobantePagoRepositorio repComprobantePago = new ComprobantePagoRepositorio();
                CuentaCorrienteRepositorio repCuentaCorriente = new CuentaCorrienteRepositorio();
                FormaPagoRepositorio repFormaPago = new FormaPagoRepositorio();
                MonedaRepositorio repMoneda = new MonedaRepositorio();

                CombosFurPagoDTO comboFurPago = new CombosFurPagoDTO();

                comboFurPago.ListaAreaTrabajo = repPersonalAreaTrabajo.ObtenerAreaTrabajoPersonal();
                comboFurPago.ListaCiudades = repCiudad.ObtenerCiudadesDeSedesExistentes();
                comboFurPago.ListaMoneda = repMoneda.ObtenerFiltroMoneda();
                comboFurPago.ListaEstado = repFurPago.ObtenerEstadoFurPago();
                comboFurPago.ListaSunatDocumento = repSunatDocumento.ObtenerElementosSunatDocumento();
                comboFurPago.ListaIgv = repTipoImpuesto.ObtenerValorIgv();
                comboFurPago.ListaRetencion = repRetencion.ObtenerListaRetencion();
                comboFurPago.ListaDetraccion = repDetraccion.ObtenerListaDetraccion();
                comboFurPago.ListaPais = repPais.ObtenerListaPais();
                //comboFurPago.ListaComprobantePago = repComprobantePago.ObtenerComprobantePago();
                comboFurPago.ListaCuentaCorriente = repCuentaCorriente.ObtenerCuentaCorrienteIdNombre();
                comboFurPago.ListaFormaPago = repFormaPago.ObtenerListaFormaPago();

                return Ok(comboFurPago);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComprobantePago()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ComprobantePagoRepositorio repComprobantePago = new ComprobantePagoRepositorio();
                var ListaComprobantePago = repComprobantePago.ObtenerComprobantePago();

                return Ok(ListaComprobantePago);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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

        [Route("[action]/{IdProveedor}")]
        [HttpGet]
        public ActionResult ObtenerProductoFur(int IdProveedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (IdProveedor == 0)
                {
                    return BadRequest(" ");
                }
                else
                {
                    FurRepositorio repFurRep = new FurRepositorio();
                    return Ok(repFurRep.ObtenerProductoFur(IdProveedor));
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarFur([FromBody] EliminarDTO FurEliminarDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext _integraDBContext = new integraDBContext();
                using (TransactionScope scope = new TransactionScope())
                {
                    FurRepositorio repFurRep = new FurRepositorio(_integraDBContext);
                    if (repFurRep.Exist(FurEliminarDTO.Id))
                    {
                        repFurRep.Delete(FurEliminarDTO.Id, FurEliminarDTO.NombreUsuario);
                        scope.Complete();
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("Registro no existente");
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarListaFur([FromBody] ListaEliminarDTO ListadoEliminarDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                integraDBContext _integraDBContext = new integraDBContext();
                using (TransactionScope scope = new TransactionScope())
                {
                    FurRepositorio repFurRep = new FurRepositorio(_integraDBContext);
                    foreach (var listado in ListadoEliminarDTO.ListaEliminar) {
                        repFurRep.EliminarFur(listado, _integraDBContext);
                    }
                        scope.Complete();
                        return Ok(true);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarFurPago([FromBody] FurPagoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FurPagoRepositorio repFurPagoRep = new FurPagoRepositorio(_integraDBContext);
                ComprobantePagoPorFurRepositorio repComPagoPorFur = new ComprobantePagoPorFurRepositorio(_integraDBContext);
                FurPagoBO furPago = new FurPagoBO();
                FurRepositorio repFurRep = new FurRepositorio(_integraDBContext);
                FurBO furBO;
                var correlativo = repFurPagoRep.ObtenerFurPago(Json.IdFur);
                using (TransactionScope scope = new TransactionScope())
                {
                    furBO = repFurRep.GetBy(x => x.Id == Json.IdFur).FirstOrDefault();

                    furPago.IdFur = Json.IdFur;
                    if (Json.IdComprobantePagoPorFur == null)
                    {
                        furPago.IdComprobantePago = null;
                    }
                    else
                    {
                        furPago.IdComprobantePago = repComPagoPorFur.ObtenerIdComprobante(Json.IdComprobantePagoPorFur);
                    }                    
                    furPago.NumeroPago = correlativo;                    
                    furPago.IdMoneda = Json.IdMoneda;
                    furPago.IdCuentaCorriente = System.Convert.ToInt32(Json.NumeroCuenta);
                    // furPago.NumeroCuenta = Json.NumeroCuenta;
                    furPago.NumeroRecibo = Json.NumeroRecibo;
                    furPago.IdFormaPago = Json.IdFormaPago;
                    furPago.FechaCobroBanco = Json.FechaCobroBanco;
                    if (Json.IdMoneda == 19)
                    {
                        furPago.PrecioTotalMonedaDolares = Json.PrecioTotalMonedaDolares;
                        furPago.PrecioTotalMonedaOrigen = Json.PrecioTotalMonedaOrigen;
                    }
                    else
                    {
                        furPago.PrecioTotalMonedaDolares = Json.PrecioTotalMonedaDolares;
                        furPago.PrecioTotalMonedaOrigen = Json.PrecioTotalMonedaOrigen;
                    }
                    furPago.IdComprobantePagoPorFur = Json.IdComprobantePagoPorFur;
                    furPago.UsuarioCreacion = Json.Usuario;
                    furPago.FechaCreacion = DateTime.Now;
                    furPago.UsuarioModificacion = Json.Usuario;
                    furPago.FechaModificacion = DateTime.Now;
                    furPago.Estado = true;

                    if (furBO != null)
                    {
                        furBO.IdFurSubFaseAprobacion = 2;
                        furBO.Cancelado = Json.IdCancelado;
                        furBO.UsuarioModificacion = Json.Usuario;
                        furBO.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        throw new Exception("No existe FUR ");
                    }

                    repFurRep.Update(furBO);
                    repFurPagoRep.Insert(furPago);
                    scope.Complete();
                }
                return Ok(Json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarFurPago([FromBody] FurPagoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //integraDBContext contexto = new integraDBContext();
                FurPagoRepositorio repFurPagoRep = new FurPagoRepositorio(_integraDBContext);
                ComprobantePagoPorFurRepositorio repComPagoPorFur = new ComprobantePagoPorFurRepositorio(_integraDBContext);
                FurPagoBO furPago = new FurPagoBO();
                FurRepositorio repFurRep = new FurRepositorio(_integraDBContext);
                FurBO furBO = new FurBO();

                var correlativo = repFurPagoRep.ObtenerFurPago(Json.IdFur);

                using (TransactionScope scope = new TransactionScope())
                {
                    if (Json.NumeroPago != 0)
                    {
                        furBO = repFurRep.GetBy(x => x.Id == Json.IdFur).FirstOrDefault();

                        furPago = repFurPagoRep.FirstById(Json.Id);
                        furPago.IdFur = Json.IdFur;
                        if (Json.IdComprobantePagoPorFur == null)
                        {
                            furPago.IdComprobantePago = null;
                        }
                        else
                        {
                            furPago.IdComprobantePago = repComPagoPorFur.ObtenerIdComprobante(Json.IdComprobantePagoPorFur);
                        }
                        furPago.NumeroPago = Json.NumeroPago;
                        furPago.IdMoneda = Json.IdMoneda;
                        furPago.IdCuentaCorriente = System.Convert.ToInt32(Json.NumeroCuenta);
                        //furPago.NumeroCuenta = Json.NumeroCuenta;
                        furPago.NumeroRecibo = Json.NumeroRecibo;
                        furPago.IdFormaPago = Json.IdFormaPago;
                        furPago.FechaCobroBanco = Json.FechaCobroBanco;
                        if (Json.IdMoneda == 19)
                        {
                            furPago.PrecioTotalMonedaDolares = Json.PrecioTotalMonedaDolares;
                            furPago.PrecioTotalMonedaOrigen = Json.PrecioTotalMonedaOrigen;
                        }
                        else
                        {
                            furPago.PrecioTotalMonedaDolares = Json.PrecioTotalMonedaDolares;
                            furPago.PrecioTotalMonedaOrigen = Json.PrecioTotalMonedaOrigen;
                        }
                        furPago.IdComprobantePagoPorFur = Json.IdComprobantePagoPorFur;
                        //furPago.UsuarioCreacion = Json.Usuario;
                        furPago.FechaCreacion = DateTime.Now;
                        furPago.UsuarioModificacion = Json.Usuario;
                        furPago.FechaModificacion = DateTime.Now;
                        furPago.Estado = true;
                        if (furBO != null)
                        {
                            furBO.IdFurSubFaseAprobacion = 2;
                            furBO.Cancelado = Json.IdCancelado;
                            furBO.UsuarioModificacion = Json.Usuario;
                            furBO.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            throw new Exception("No existe FUR ");
                        }

                        repFurRep.Update(furBO);
                        repFurPagoRep.Update(furPago);
                    }
                    else
                    {
                        furBO = repFurRep.GetBy(x => x.Id == Json.IdFur).FirstOrDefault();
                        furPago.IdFur = Json.IdFur;
                        if (Json.IdComprobantePagoPorFur == null)
                        {
                            furPago.IdComprobantePago = Json.IdComprobantePago;
                        }
                        else
                        {
                            furPago.IdComprobantePago = repComPagoPorFur.ObtenerIdComprobante(Json.IdComprobantePagoPorFur);
                        }
                        furPago.NumeroPago = correlativo;
                        furPago.IdMoneda = Json.IdMoneda;
                        furPago.IdCuentaCorriente = System.Convert.ToInt32(Json.NumeroCuenta);
                        //furPago.NumeroCuenta = Json.NumeroCuenta;
                        furPago.NumeroRecibo = Json.NumeroRecibo;
                        furPago.IdFormaPago = Json.IdFormaPago;
                        furPago.FechaCobroBanco = Json.FechaCobroBanco;
                        if (Json.IdMoneda == 19)
                        {
                            furPago.PrecioTotalMonedaDolares = Json.PrecioTotalMonedaDolares;
                            furPago.PrecioTotalMonedaOrigen = Json.PrecioTotalMonedaOrigen;
                        }
                        else
                        {
                            furPago.PrecioTotalMonedaDolares = Json.PrecioTotalMonedaDolares;
                            furPago.PrecioTotalMonedaOrigen = Json.PrecioTotalMonedaOrigen;
                        }
                        furPago.IdComprobantePagoPorFur = Json.IdComprobantePagoPorFur;
                        furPago.UsuarioCreacion = Json.Usuario;
                        furPago.FechaCreacion = DateTime.Now;
                        furPago.UsuarioModificacion = Json.Usuario;
                        furPago.FechaModificacion = DateTime.Now;
                        furPago.Estado = true;
                        if (furBO != null)
                        {
                            furBO.IdFurSubFaseAprobacion = 2;
                            furBO.Cancelado = Json.IdCancelado;
                            furBO.UsuarioModificacion = Json.Usuario;
                            furBO.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            throw new Exception("No existe FUR ");
                        }

                        repFurRep.Update(furBO);
                        repFurPagoRep.Insert(furPago);
                    }
                    
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
        public ActionResult EliminarFurPago([FromBody] FurPagoDTO FurPagoEliminarDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                
                using (TransactionScope scope = new TransactionScope())
                {
                    FurPagoRepositorio repFurPagoRep = new FurPagoRepositorio(_integraDBContext);
                    if (repFurPagoRep.Exist(FurPagoEliminarDTO.Id))
                    {
                        repFurPagoRep.Delete(FurPagoEliminarDTO.Id, FurPagoEliminarDTO.Usuario);
                        scope.Complete();
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("Registro no existente");
                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerListaPagos(int IdFur)
        {
            try
            {
                
                    FurPagoRepositorio repFurPagoRep = new FurPagoRepositorio();
                    var listaFurPagos = repFurPagoRep.ObtenerListaFurPagos(IdFur);
                    return Ok(listaFurPagos);
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult BuscarListaPagos(int? IdArea, int? IdCiudad, int? Anio, int? Semana, int? IdMoneda, bool? Estado)
        {
            try
            {

                FurPagoRepositorio repFurPagoRep = new FurPagoRepositorio();
                var listaObtenerFurPagos = repFurPagoRep.BuscarListaFurPagos(IdArea, IdCiudad, Anio, Semana, IdMoneda, Estado);


                var listaObtenerFurPagosAgrupado = (
                      from p in listaObtenerFurPagos
                      group p by new {
                          p.IdFur,
                          p.Codigo,
                          p.IdProveedor,
                          p.NombreProveedor,
                          p.IdProducto,
                          p.NombreProducto,
                          p.IdCC,
                          p.Cantidad,
                          p.NombreCentroCosto,
                          p.NumeroCuenta,
                          p.DescripcionCuenta,
                          p.MonedaFur,
                          p.NombreMonedaFur,
                          p.PrecioUnitarioDolares,
                          p.PrecioUnitarioSoles,
                          p.PrecioTotalDolares,
                          p.PrecioTotalSoles,
                          //p.IdDocumentoPago,
                          //p.NombreDocumento,
                          p.Descripcion,
                          //p.FechaEfectivo,
                          p.FaseAprobacion,
                          p.Antiguo,
                          p.MonedaPagoRealizado,
                          p.NombreMonedaPagoRealizado,
                          p.Usuario,
                          p.EstadoCancelado,
                          p.FechaModificacion

                      } into g
                      select new ListaFurPagoDTO
                      {
                          IdFur = g.Key.IdFur,
                          Codigo = g.Key.Codigo,
                          IdProveedor = g.Key.IdProveedor,
                          NombreProveedor = g.Key.NombreProveedor,
                          IdProducto = g.Key.IdProducto,
                          NombreProducto = g.Key.NombreProducto,
                          IdCC = g.Key.IdCC,
                          Cantidad = g.Key.Cantidad,
                          NombreCentroCosto = g.Key.NombreCentroCosto,
                          NumeroCuenta = g.Key.NumeroCuenta,
                          DescripcionCuenta = g.Key.DescripcionCuenta,
                          MonedaFur = g.Key.MonedaFur,
                          NombreMonedaFur = g.Key.NombreMonedaFur,
                          //IdDocumentoPago = g.Key.IdDocumentoPago,
                          Descripcion = g.Key.Descripcion,
                          FaseAprobacion = g.Key.FaseAprobacion,
                          Antiguo = g.Key.Antiguo,
                          MonedaPagoRealizado = g.Key.MonedaPagoRealizado,
                          NombreMonedaPagoRealizado = g.Key.NombreMonedaPagoRealizado,
                          Usuario = g.Key.Usuario,
                          EstadoCancelado = g.Key.EstadoCancelado,
                          FechaModificacion = g.Key.FechaModificacion,
                          PrecioUnitarioDolares = g.Key.PrecioUnitarioDolares,
                          PrecioUnitarioSoles = g.Key.PrecioUnitarioSoles,
                          PrecioTotalDolares = g.Key.PrecioTotalDolares,
                          PrecioTotalSoles = g.Key.PrecioTotalSoles,

                          NombreDocumento = string.Join("/", g.Select(x => x.NombreDocumento).ToList()),
                          //FechaEfectivo = string.Join("/", g.Select(x => x.FechaEfectivo).ToList()),
                          //PrecioUnitarioDolares = g.Select(x => x.PrecioUnitarioDolares).Sum(),
                          //PrecioUnitarioSoles = g.Select(x => x.PrecioUnitarioSoles).Sum(),
                          //PrecioTotalDolares = g.Select(x => x.PrecioTotalDolares).Sum(),
                          //PrecioTotalSoles = g.Select(x => x.PrecioTotalSoles).Sum(),
                          PagoMonedaOrigen = g.Select(x => x.PagoMonedaOrigen).Sum(),
                          PagoDolares = g.Select(x => x.PagoDolares).Sum(),
                          NumeroRecibo = string.Join("/",g.Select(x => x.NumeroRecibo).ToList()),
                          NumeroComprobante = string.Join("/",g.Select(x => x.NumeroComprobante).ToList()),
                      }
               );
                return Ok(listaObtenerFurPagosAgrupado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
                    
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerComprobantesPorFur(int idFur)
        {
            try
            {

                ComprobantePagoRepositorio repComprobantePagoRep = new ComprobantePagoRepositorio();
                var listaObtenerComprobantes = repComprobantePagoRep.ObtenerComprobantePagoPorFur(idFur);
                return Ok(listaObtenerComprobantes);


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult PagoMasivo([FromBody] CompuestoFurRegistrarPagosDTO Json)
        {
            try
            {
                FurPagoRepositorio repFurPagoRep = new FurPagoRepositorio(_integraDBContext);                
                //List<PagoMasivoDTO> listaPago = Json.PagoMasivo;
                FurRepositorio repFur = new FurRepositorio(_integraDBContext);
                FurBO furBO;

                using (TransactionScope scope = new TransactionScope())
                {

                    //foreach (PagoMasivoDTO pago in listaPago)
                    //{
                    //    if (pago.MontoAmortizar <= 0)
                    //    {
                    //        throw new Exception("No se puede amortizar Montos Negativos FUR: " + pago.Codigo);
                    //    }
                    //}
                    foreach (var pago in Json.FurSeleccionados)
                    {
                        var correlativo = repFurPagoRep.ObtenerFurPago(pago);
                        furBO = repFur.GetBy(x => x.Id == pago).FirstOrDefault();

                        FurPagoBO furPago = new FurPagoBO();
                        //furPago.IdFur = pago.Id;
                        furPago.NumeroPago = correlativo;
                        //furPago.IdMoneda = pago.IdMoneda;
                        //furPago.NumeroCuenta = Json.FurPago.NumeroCuenta;
                        furPago.IdCuentaCorriente =System.Convert.ToInt32(Json.FurPago.NumeroCuenta);
                        furPago.NumeroRecibo = Json.FurPago.NumeroRecibo;
                        furPago.IdFormaPago = Json.FurPago.IdFormaPago;
                        furPago.FechaCobroBanco = Convert.ToDateTime(Json.FurPago.FechaCobroBanco);
                        if (Json.FurPago.IdMoneda == 19)
                        {
                            furPago.PrecioTotalMonedaOrigen = 0;
                            furPago.PrecioTotalMonedaDolares = 0;
                        }
                        else
                        {
                            furPago.PrecioTotalMonedaOrigen = 0;
                            furPago.PrecioTotalMonedaDolares = 0;
                        }
                        if (furBO != null)
                        {
                            furBO.Cancelado = true;
                            furBO.UsuarioModificacion = Json.Usuario;
                            furBO.FechaModificacion = DateTime.Now;
                        }
                        else
                        {
                            throw new Exception("No existe FUR ");
                        }
                        furPago.Estado = true;
                        furPago.UsuarioCreacion = Json.Usuario;
                        furPago.UsuarioModificacion = Json.Usuario;
                        furPago.FechaCreacion = DateTime.Now;
                        furPago.FechaModificacion = DateTime.Now;

                        repFur.Update(furBO);
                        repFurPagoRep.Insert(furPago);
                    }
                    scope.Complete();
                }
                return Ok(Json);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerPagosBloquePorFur([FromBody] List<int> idFurs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                FurRepositorio repFurRep = new FurRepositorio();
                List<PagoMasivoDatosDTO> pagosMasivos = new List<PagoMasivoDatosDTO>();
                foreach (int IdFur in idFurs)
                {
                    var valor = repFurRep.ObtenerPagosBloquePorFurs(IdFur);
                    if (valor.MontoAmortizar > 0)
                    {
                        pagosMasivos.Add(valor);
                    }
                    
                }
                return Ok(pagosMasivos);


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPagosRealizadosPorFur(int idFur)
        {
            try
            {

                FurPagoRepositorio repFurPagoRep = new FurPagoRepositorio();
                
                var furPagoRealizado = repFurPagoRep.ObtenerPagosRealizadosPorFur(idFur);
                
                return Ok(furPagoRealizado);


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerProveedorComprobanteMontoMoneda(int idFur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ComprobantePagoPorFurRepositorio repComprobantePagoPorFur = new ComprobantePagoPorFurRepositorio();
                var ListaComprobantePagoPorFur = repComprobantePagoPorFur.ObtenerComprobantePorFur(idFur);

                return Ok(ListaComprobantePagoPorFur);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}