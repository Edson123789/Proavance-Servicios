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
    [Route("api/ComprobantePago")]
    public class ComprobantePagoController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public ComprobantePagoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult AsociarComprobante([FromBody] CompuestoDatosComprobantePagoDTO Comprobante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ComprobantePagoPorFurRepositorio _repComprobantePagoPorFur = new ComprobantePagoPorFurRepositorio(_integraDBContext);
                ComprobantePagoPorFurBO comprobantePagoPorFurBO = new ComprobantePagoPorFurBO();

                comprobantePagoPorFurBO.IdComprobantePago = Comprobante.IdComprobantePago;
                comprobantePagoPorFurBO.IdFur = Comprobante.IdFur;
                comprobantePagoPorFurBO.Monto = Comprobante.Monto;
                comprobantePagoPorFurBO.Estado = true;
                comprobantePagoPorFurBO.UsuarioCreacion = Comprobante.Usuario;
                comprobantePagoPorFurBO.UsuarioModificacion = Comprobante.Usuario;
                comprobantePagoPorFurBO.FechaCreacion = DateTime.Now;
                comprobantePagoPorFurBO.FechaModificacion = DateTime.Now;

                return Ok(_repComprobantePagoPorFur.Insert(comprobantePagoPorFurBO));

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] CompuestoComprobantePagoDTO Comprobante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ComprobantePagoRepositorio _repComprobantePago = new ComprobantePagoRepositorio(_integraDBContext);
                ComprobantePagoBO comprobantePagoBO = new ComprobantePagoBO();

                var ComprobanteExistente = _repComprobantePago.GetBy(x => x.IdProveedor == Comprobante.ComprobantePago.IdProveedor
                                                                && x.SerieComprobante == Comprobante.ComprobantePago.SerieComprobante
                                                                && x.NumeroComprobante == Comprobante.ComprobantePago.NumeroComprobante).FirstOrDefault();

                if (ComprobanteExistente != null)
                {
                    return Json(new { Result = "OK", Mensaje = "COMPROBANTE-EXISTENTE" });
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    comprobantePagoBO.IdSunatDocumento = Comprobante.ComprobantePago.IdSunatDocumento;
                    comprobantePagoBO.IdPais = Comprobante.ComprobantePago.IdPais;
                    comprobantePagoBO.SerieComprobante = Comprobante.ComprobantePago.SerieComprobante;
                    comprobantePagoBO.NumeroComprobante = Comprobante.ComprobantePago.NumeroComprobante;
                    comprobantePagoBO.IdMoneda = Comprobante.ComprobantePago.IdMoneda;
                    comprobantePagoBO.MontoNeto = Comprobante.ComprobantePago.MontoNeto;
                    comprobantePagoBO.MontoBruto = Comprobante.ComprobantePago.MontoBruto;
                    comprobantePagoBO.MontoInafecto = Comprobante.ComprobantePago.MontoInafecto;
                    comprobantePagoBO.PorcentajeIgv = Comprobante.ComprobantePago.PorcentajeIgv;
                    comprobantePagoBO.MontoIgv = Comprobante.ComprobantePago.MontoIgv;
                    comprobantePagoBO.AjusteMontoBruto = Comprobante.ComprobantePago.AjusteMontoBruto;
                    comprobantePagoBO.FechaEmision = Comprobante.ComprobantePago.FechaEmision;
                    comprobantePagoBO.FechaProgramacion = Comprobante.ComprobantePago.FechaProgramacion;
                    comprobantePagoBO.IdTipoImpuesto = Comprobante.ComprobantePago.IdTipoImpuesto;
                    comprobantePagoBO.IdRetencion = Comprobante.ComprobantePago.IdRetencion;
                    comprobantePagoBO.IdDetraccion = Comprobante.ComprobantePago.IdDetraccion;
                    comprobantePagoBO.OtraTazaContribucion = Comprobante.ComprobantePago.OtraTazaContribucion;
                    comprobantePagoBO.IdProveedor = Comprobante.ComprobantePago.IdProveedor;
                    comprobantePagoBO.Estado = true;
                    comprobantePagoBO.UsuarioCreacion = Comprobante.Usuario;
                    comprobantePagoBO.UsuarioModificacion = Comprobante.Usuario;
                    comprobantePagoBO.FechaCreacion = DateTime.Now;
                    comprobantePagoBO.FechaModificacion = DateTime.Now;

                    comprobantePagoBO.ComprobantePagoPorFur = new List<ComprobantePagoPorFurBO>();
                    
                    foreach (var item in Comprobante.ComprobantePagoPorFur)
                    {

                        ComprobantePagoPorFurBO comprobantePagoPorFur = new ComprobantePagoPorFurBO();
                        //comprobantePagoPorFur.IdComprobantePago = Comprobante.ComprobantePago.Id;
                        comprobantePagoPorFur.IdFur = item.Id;
                        comprobantePagoPorFur.Monto = item.MontoFur;
                        comprobantePagoPorFur.UsuarioCreacion = Comprobante.Usuario;
                        comprobantePagoPorFur.UsuarioModificacion = Comprobante.Usuario;
                        comprobantePagoPorFur.FechaCreacion = DateTime.Now;
                        comprobantePagoPorFur.FechaModificacion = DateTime.Now;
                        comprobantePagoPorFur.Estado = true;

                        comprobantePagoBO.ComprobantePagoPorFur.Add(comprobantePagoPorFur);
                    }
                    
                    _repComprobantePago.Insert(comprobantePagoBO);
                    scope.Complete();
                }

                return Ok(comprobantePagoBO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] CompuestoComprobantePagoDTO Comprobante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ComprobantePagoRepositorio _repComprobantePago = new ComprobantePagoRepositorio(_integraDBContext);
                ComprobantePagoBO comprobantePagoBO = new ComprobantePagoBO();
                ComprobantePagoPorFurRepositorio _repComprobantePagoPorFur = new ComprobantePagoPorFurRepositorio(_integraDBContext);

                using (TransactionScope scope = new TransactionScope())
                {
                    if (_repComprobantePago.Exist(Comprobante.ComprobantePago.Id))
                    {
                        //_repComprobantePagoPorFur.EliminacionLogicoPorComprobante(Comprobante.ComprobantePago.Id, Comprobante.Usuario, Comprobante.ComprobantePagoPorFur);

                        comprobantePagoBO = _repComprobantePago.FirstById(Comprobante.ComprobantePago.Id);
                        comprobantePagoBO.IdSunatDocumento = Comprobante.ComprobantePago.IdSunatDocumento;
                        comprobantePagoBO.IdPais = Comprobante.ComprobantePago.IdPais;
                        comprobantePagoBO.SerieComprobante = Comprobante.ComprobantePago.SerieComprobante;
                        comprobantePagoBO.NumeroComprobante = Comprobante.ComprobantePago.NumeroComprobante;
                        comprobantePagoBO.IdMoneda = Comprobante.ComprobantePago.IdMoneda;
                        comprobantePagoBO.MontoNeto = Comprobante.ComprobantePago.MontoNeto;
                        comprobantePagoBO.MontoBruto = Comprobante.ComprobantePago.MontoBruto;
                        comprobantePagoBO.MontoInafecto = Comprobante.ComprobantePago.MontoInafecto;
                        comprobantePagoBO.PorcentajeIgv = Comprobante.ComprobantePago.PorcentajeIgv;
                        comprobantePagoBO.MontoIgv = Comprobante.ComprobantePago.MontoIgv;
                        comprobantePagoBO.AjusteMontoBruto = Comprobante.ComprobantePago.AjusteMontoBruto;
                        comprobantePagoBO.FechaEmision = Comprobante.ComprobantePago.FechaEmision;
                        comprobantePagoBO.FechaProgramacion = Comprobante.ComprobantePago.FechaProgramacion;
                        comprobantePagoBO.IdTipoImpuesto = Comprobante.ComprobantePago.IdTipoImpuesto;
                        comprobantePagoBO.IdRetencion = Comprobante.ComprobantePago.IdRetencion;
                        comprobantePagoBO.IdDetraccion = Comprobante.ComprobantePago.IdDetraccion;
                        comprobantePagoBO.OtraTazaContribucion = Comprobante.ComprobantePago.OtraTazaContribucion;
                        comprobantePagoBO.IdProveedor = Comprobante.ComprobantePago.IdProveedor;
                        comprobantePagoBO.Estado = true;
                        comprobantePagoBO.UsuarioModificacion = Comprobante.Usuario;
                        comprobantePagoBO.FechaModificacion = DateTime.Now;

                        comprobantePagoBO.ComprobantePagoPorFur = new List<ComprobantePagoPorFurBO>();
                        foreach (var item in Comprobante.ComprobantePagoPorFur)
                        {
                            ComprobantePagoPorFurBO comprobantePagoPorFur;
                            if (_repComprobantePagoPorFur.Exist(x => x.Id==item.IdComprobantePagoPorFur && x.IdFur == item.Id && x.IdComprobantePago == Comprobante.ComprobantePago.Id))
                            {
                                comprobantePagoPorFur = _repComprobantePagoPorFur.FirstBy(x => x.Id == item.IdComprobantePagoPorFur);
                                comprobantePagoPorFur.IdFur = item.Id;
                                comprobantePagoPorFur.Monto = item.MontoFur;        
                                comprobantePagoPorFur.UsuarioModificacion = Comprobante.Usuario;
                                comprobantePagoPorFur.FechaModificacion = DateTime.Now;

                            }
                            else
                            {
                                comprobantePagoPorFur = new ComprobantePagoPorFurBO();
                                comprobantePagoPorFur.IdFur = item.Id;
                                comprobantePagoPorFur.Monto = item.MontoFur;
                                comprobantePagoPorFur.IdComprobantePago = Comprobante.ComprobantePago.Id;
                                comprobantePagoPorFur.UsuarioCreacion = Comprobante.Usuario;
                                comprobantePagoPorFur.UsuarioModificacion = Comprobante.Usuario;
                                comprobantePagoPorFur.FechaCreacion = DateTime.Now;
                                comprobantePagoPorFur.FechaModificacion = DateTime.Now;
                                comprobantePagoPorFur.Estado = true;
                            }

                            comprobantePagoBO.ComprobantePagoPorFur.Add(comprobantePagoPorFur);
                        }

                        _repComprobantePago.Update(comprobantePagoBO);
                        scope.Complete();
                    }
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
        public ActionResult Eliminar([FromBody]  CompuestoComprobantePagoDTO Comprobante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    ComprobantePagoRepositorio _repComprobantePago = new ComprobantePagoRepositorio(_integraDBContext);

                    if (_repComprobantePago.Exist(Comprobante.ComprobantePago.Id))
                    {
                        _repComprobantePago.Delete(Comprobante.ComprobantePago.Id, Comprobante.Usuario);
                        scope.Complete();
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest("Registro no existente");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarAsociacion([FromBody]  EliminarDTO eliminar)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    ComprobantePagoPorFurRepositorio _repComprobantePagoPorFur = new ComprobantePagoPorFurRepositorio(_integraDBContext);
                    FurPagoRepositorio _repFurPago = new FurPagoRepositorio(_integraDBContext);
                    CajaEgresoRepositorio _repCajaEgreso = new CajaEgresoRepositorio(_integraDBContext);

                    if (!_repFurPago.Exist(x => x.IdComprobantePagoPorFur == eliminar.Id) && !_repCajaEgreso.Exist(x => x.IdComprobantePagoPorFur == eliminar.Id))
                    {
                        if (_repComprobantePagoPorFur.Exist(eliminar.Id))
                        {
                            _repComprobantePagoPorFur.Delete(eliminar.Id, eliminar.NombreUsuario);
                            scope.Complete();
                            return Ok(true);
                        }
                        else
                        {
                            return BadRequest("Registro no existente");
                        }
                    }
                    else {
                        return BadRequest("Registro se esta usando en FursPago o CajaEgreso REC");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}