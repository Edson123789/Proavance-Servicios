using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/HistoricoProductoProveedor")]
    public class HistoricoProductoProveedorController : Controller
    {
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerTodoHistoricoUltimaVersion(int? IdHistorico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                HistoricoProductoProveedorRepositorio _repHistoricoRep = new HistoricoProductoProveedorRepositorio();
                //var Data = _repHistoricoRep.ObtenerHistoricoUltimaVersion(IdHistorico);
                //var Total = _repHistoricoRep.GetBy(x => x.Estado == true).ToList().Count();
                return Ok(_repHistoricoRep.ObtenerHistoricoUltimaVersion(IdHistorico));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerNombreHistoricoAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                HistoricoProductoProveedorRepositorio repHistoricoRep = new HistoricoProductoProveedorRepositorio();
                CiudadRepositorio _repCiudadRep = new CiudadRepositorio();
                if (Filtros != null && Filtros["Valor"] != null)
                {
                    return Ok(repHistoricoRep.ObtenerNombreHistoricoAutocomplete(Filtros["Valor"].ToString()));
                }
                return Ok(repHistoricoRep.ObtenerNombreHistoricoAutocomplete(""));
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCondicionPago()
        {
            try
            {
                CondicionPagoRepositorio repCondicionPagoRep = new CondicionPagoRepositorio();

                return Ok(repCondicionPagoRep.GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre }).ToList());
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCondicionTipoPago()
        {
            try
            {
                CondicionTipoPagoRepositorio repTipoPagoRep = new CondicionTipoPagoRepositorio();

                return Ok(repTipoPagoRep.GetBy(x => x.Estado == true, x => new { x.Id, x.Nombre }).ToList());
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarProveedorAProductoEnHistorico([FromBody] ProductoHistoricoDTO objetoProductoHistorico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    integraDBContext integraDB = new integraDBContext();
                    string result = "Se Asocio el Proveedor Correctamente";

                    HistoricoProductoProveedorVersionDTO ObjetoHistorico = new HistoricoProductoProveedorVersionDTO();
                    ProductoRepositorio _repProductoRep = new ProductoRepositorio(integraDB);
                    if (objetoProductoHistorico.Producto.Id == 0)
                    {
                        objetoProductoHistorico.Producto = _repProductoRep.InsertarProducto(objetoProductoHistorico.Producto, integraDB);
                        objetoProductoHistorico.Historico.IdProducto = objetoProductoHistorico.Producto.Id;
                        this.insertarHistoricoProducto(objetoProductoHistorico.Historico, integraDB);
                        result = "Se Actualizo el Producto y se Asocio el Proveedor Correctamente";

                    }
                    else
                    {
                        _repProductoRep.ActualizarProducto(objetoProductoHistorico.Producto, integraDB);
                        this.insertarHistoricoProducto(objetoProductoHistorico.Historico, integraDB);
                        result = "Se Inserto un Nuevo Producto y se Asocio el Proveedor Correctamente";
                    }

                    scope.Complete();


                    return Ok(new { result });
                }

            }
            catch (Exception ex)
            {
                string result = "ERROR,NO SE INSERTO EL TIPO DE CAMBIO PARA LA MONEDA SELECCIONADA";
                return BadRequest(result );
            }
        }

        private bool insertarHistoricoProducto(HistoricoProductoProveedorVersionDTO objetoHistorico,integraDBContext context)
        {

            try
            {
                HistoricoProductoProveedorRepositorio _repHistoricoRep = new HistoricoProductoProveedorRepositorio(context);
                HistoricoProductoProveedorBO historico = new HistoricoProductoProveedorBO();

                TipoCambioMonedaRepositorio repTipoCambioMonedaRep = new TipoCambioMonedaRepositorio(context);
                                
                decimal DolarASoles = 0;
                decimal monedaAdolar = 0;
                if (objetoHistorico.IdMoneda == 19)
                {
                    DolarASoles = 1;
                }
                else {
                    var tipoCambioMonedaDia = repTipoCambioMonedaRep.GetBy(x => x.Estado == true && x.Fecha.ToString("yyyy/MM/dd").Equals(DateTime.Now.ToString("yyyy/MM/dd")) && x.IdMoneda == objetoHistorico.IdMoneda, x => new { x.Id, x.MonedaAdolar, x.DolarAmoneda }).FirstOrDefault();
                    monedaAdolar = (decimal)tipoCambioMonedaDia.MonedaAdolar;
                }
                
                var objetoVersion = _repHistoricoRep.GetBy(x => x.IdProducto == objetoHistorico.IdProducto && x.IdProveedor == objetoHistorico.IdProveedor, x => new { version = x.Version }).OrderByDescending(y => y.version).FirstOrDefault();
                int version = 0;
                if (objetoVersion == null)
                {
                    version = 0;
                }
                else
                {
                    version = objetoVersion.version + 1;
                }
                var existe = 0;//_repHistoricoRep.GetBy(x => x.IdProducto == objetoHistorico.IdProducto && x.IdProveedor == objetoHistorico.IdProveedor && x.Precio == objetoHistorico.Precio && x.IdMoneda == objetoHistorico.IdMoneda && x.Estado == true).ToList();

                if (existe == 0)
                {
                        historico.IdProducto = objetoHistorico.IdProducto;
                        historico.IdProveedor = objetoHistorico.IdProveedor;
                    //Si la moneda es igual a dolares se tomara el tipo de cambio a soles, para poder pagar en soles y dolares,
                    //Si la moneda es diferente a dolares se podra pagar en la moneda origen y en dolare
                        historico.CostoMonedaOrigen =objetoHistorico.Precio;
                        historico.CostoDolares = objetoHistorico.IdMoneda == 19 ? objetoHistorico.Precio : (objetoHistorico.Precio / monedaAdolar);
                        historico.IdMoneda = objetoHistorico.IdMoneda;
                        historico.Precio = objetoHistorico.Precio;
                        historico.TipoCambio = objetoHistorico.IdMoneda == 19 ? 1: monedaAdolar;
                        historico.IdCondicionPago = objetoHistorico.IdCondicionPago;
                        historico.IdCondicionTipoPago = objetoHistorico.IdTipoPago;
                        historico.Version = version;
                        historico.Observaciones = objetoHistorico.Observaciones;
                        historico.Estado = true;
                        historico.UsuarioCreacion = objetoHistorico.UsuarioModificacion;
                        historico.UsuarioModificacion = objetoHistorico.UsuarioModificacion;
                        historico.FechaModificacion = DateTime.Now;
                        historico.FechaCreacion = DateTime.Now;
                        _repHistoricoRep.Insert(historico);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarHistoricoProductoProveedor([FromBody] ActualizarHistoricoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //integraDBContext contexto = new integraDBContext();
                HistoricoProductoProveedorRepositorio _repHistoricoRep = new HistoricoProductoProveedorRepositorio();
                HistoricoProductoProveedorBO historico = new HistoricoProductoProveedorBO();
                historico = _repHistoricoRep.FirstById(Json.Id);
                using (TransactionScope scope = new TransactionScope())
                {
                    historico.IdCondicionPago = Json.IdCondicionPago;
                    historico.IdCondicionTipoPago = Json.IdTipoPago;
                    historico.Observaciones = Json.Observaciones;
                    historico.UsuarioModificacion = Json.UsuarioModificacion;
                    historico.FechaModificacion = DateTime.Now;

                    _repHistoricoRep.Update(historico);
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
        public ActionResult EliminarHistoricoProductoProveedor([FromBody] EliminarDTO HistoricoPPDTO)
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
                    HistoricoProductoProveedorRepositorio _repHistoricoRep = new HistoricoProductoProveedorRepositorio(_integraDBContext);
                    if (_repHistoricoRep.Exist(HistoricoPPDTO.Id))
                    {
                        _repHistoricoRep.Delete(HistoricoPPDTO.Id, HistoricoPPDTO.NombreUsuario);
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
    }
}