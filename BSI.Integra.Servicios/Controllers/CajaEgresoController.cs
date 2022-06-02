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
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CajaEgreso")]
    public class CajaEgresoController : Controller
    {
        private readonly integraDBContext _integraDBContext ;
        public CajaEgresoController() {
            _integraDBContext = new integraDBContext();
        }

        #region ServicosAdicionales
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerImpuestoRetencionDetraccion(int idPais)
        {
            try
            {
                TipoImpuestoRepositorio _repoTipoImpuesto = new TipoImpuestoRepositorio(_integraDBContext);
                RetencionRepositorio _repoRetencion = new RetencionRepositorio(_integraDBContext);
                DetraccionRepositorio _repoDetraccion = new DetraccionRepositorio(_integraDBContext);

                var ListaImpuestos = _repoTipoImpuesto.ObtenerValorIgvPorPais(idPais);
                var ListaRetenciones = _repoRetencion.ObtenerValorRetencionPorPais(idPais);
                var ListaDetracciones = _repoDetraccion.ObtenerValorDetraccionPorPais(idPais);
                

               

                return Ok(new { Result = "OK", ListaImpuesto = ListaImpuestos, ListaRetencion = ListaRetenciones, ListaDetraccion = ListaDetracciones });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaElementosSunatDocumento()
        {
            try
            {
                SunatDocumentoRepositorio _repoSunatDocumento = new SunatDocumentoRepositorio(_integraDBContext);
                var ListaElementosSunatDocumento = _repoSunatDocumento.ObtenerElementosSunatDocumento();

                return Ok(new { Result = "OK", Records = ListaElementosSunatDocumento });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaElementosMoneda()
        {
            try
            {
                MonedaRepositorio _repoMoneda = new MonedaRepositorio(_integraDBContext);
                var ListaElementosMoneda = _repoMoneda.ObtenerFiltroMoneda();

                return Ok(new { Result = "OK", Records = ListaElementosMoneda });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaElementosFur(string NombreParcial, int IdCajaPorRendirCabecera)
        {
            try
            {
                // evita que se devuelva todos los nombre (  todos encajan con string vacio ("")  )
                if (NombreParcial == null || NombreParcial.Trim().Equals(""))
                {
                    Object[] ListaVacia = new object[0];
                    return Json(new { Result = "OK", Records = ListaVacia });
                }

                FurRepositorio _repoFur = new FurRepositorio(_integraDBContext);
                var lista = _repoFur.ObtenerDatosFurcajaEgreso(NombreParcial, IdCajaPorRendirCabecera);
                return Json(new { Result = "OK", Records = lista });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaElementosProveedor(string NombreParcial)
        {
            try
            {
                // evita que se devuelva todos los nombre (  todos encajan con string vacio ("")  )
                if (NombreParcial == null || NombreParcial.Trim().Equals(""))
                {
                    Object[] ListaVacia = new object[0];
                    return Json(new { Result = "OK", Records = ListaVacia });
                }

                ProveedorRepositorio _repoProveedor = new ProveedorRepositorio(_integraDBContext);
                var lista = _repoProveedor.ObtenerProveedorPorRuc(NombreParcial);
                return Json(new { Result = "OK", Records = lista });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaElementosComprobantes(string NombreParcial)
        {
            try
            {
                // evita que se devuelva todos los nombre (  todos encajan con string vacio ("")  )
                if (NombreParcial == null || NombreParcial.Trim().Equals(""))
                {
                    Object[] ListaVacia = new object[0];
                    return Json(new { Result = "OK", Records = ListaVacia });
                }

                ComprobantePagoRepositorio _repoComprobantePago = new ComprobantePagoRepositorio(_integraDBContext);
                var lista = _repoComprobantePago.ObtenerComprobantePorRuc(NombreParcial);
                return Json(new { Result = "OK", Records = lista });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerTodoComprobante([FromBody] Dictionary<string, string> Valor)
        {
            try
            {
                
                if (Valor != null && Valor.Count > 0)
                {
                    ComprobantePagoRepositorio _repoComprobantePago = new ComprobantePagoRepositorio(_integraDBContext);
                    var listaComprobante = _repoComprobantePago.ObtenerComprobanteAutocomplete(Valor["filtro"]);
                    return Ok(listaComprobante);
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

        
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerMontoUtilizadoDeComprobante(int IdComprobante)
        {
            try
            {
                ComprobantePagoPorFurRepositorio _repoComprobantePago = new ComprobantePagoPorFurRepositorio(_integraDBContext);
                var MontoUtilizado = _repoComprobantePago.ObtenerMontoUtilizadoComprobante(IdComprobante);
                return Json(new { Result = "OK", Monto = MontoUtilizado });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdFur}")]
        [HttpGet]
        public ActionResult ObtenerMontoLimite(int IdFur)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FurRepositorio _repoFur = new FurRepositorio(_integraDBContext);

                FurBO Fur = _repoFur.GetBy(x => x.Id == IdFur).FirstOrDefault();
                if (Fur == null) throw new Exception("Error No se encontro el FUR");

                ComprobantePagoPorFurRepositorio _repoComprobantePagoPorFur = new ComprobantePagoPorFurRepositorio(_integraDBContext);
                List<ComprobantePagoPorFurBO> RegistrosDelFur = _repoComprobantePagoPorFur.GetBy(x => x.IdFur == IdFur).ToList();

                decimal PagosAcumulado = 0;
                for (int i = 0; i < RegistrosDelFur.Count; ++i)
                    PagosAcumulado += RegistrosDelFur[i].Monto;

                return Json(new { Result = "OK", MontoLimite = (Fur.PrecioTotalMonedaOrigen - PagosAcumulado) }); ;
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCajasPorRendirParaRendicion(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
               
                CajaPorRendirCabeceraRepositorio _repoCajaEgreso = new CajaPorRendirCabeceraRepositorio(_integraDBContext);
                var RegistrosCajaEgreso = _repoCajaEgreso.ObtenerCajasPorRendirParaRendicion(IdPersonal);

                return Ok(new { Result = "OK", Records = RegistrosCajaEgreso });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerDetalleCajasPorRendir(int IdCajaPorRendirCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CajaPorRendirRepositorio _repoCajaPorRendir = new CajaPorRendirRepositorio(_integraDBContext);
                var RegistrosDetalleCajaPorRendir = _repoCajaPorRendir.ObtenerCajasPorRendirPorIdPorRendirCabecera(IdCajaPorRendirCabecera);

                return Ok(new { Result = "OK", Records = RegistrosDetalleCajaPorRendir });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaDetraccion()
        {
            try
            {
                DetraccionRepositorio _repoDetraccion = new DetraccionRepositorio(_integraDBContext);
                var ListaElementosDetraccion = _repoDetraccion.ObtenerListaDetraccion();

                return Ok(new { Result = "OK", Records = ListaElementosDetraccion });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerListaRetencion()
        {
            try
            {
                RetencionRepositorio _repoRetencion = new RetencionRepositorio(_integraDBContext);
                var ListaElementosRetencion = _repoRetencion.ObtenerListaRetencion();

                return Ok(new { Result = "OK", Records = ListaElementosRetencion });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerIgv()
        {
            try
            {
                TipoImpuestoRepositorio _repoTipoImpuesto = new TipoImpuestoRepositorio(_integraDBContext);
                var Impuesto = _repoTipoImpuesto.ObtenerValorIgv();

                if (Impuesto.Count == 0) throw new Exception("El IGV para este modulo no se encontro en la base de datos");

                return Ok(new { Result = "OK", Igv = Impuesto[0].Nombre, IdTipoImpuesto=Impuesto[0].Id });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion



        #region CRUD
        [Route("[action]")]
        [HttpGet]
        public ActionResult VisualizarCajaEgreso(int IdCajaPorRendirCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
               
                CajaEgresoRepositorio _repoCajaEgreso = new CajaEgresoRepositorio(_integraDBContext);
                var RegistrosCajaEgreso = _repoCajaEgreso.ObtenerRegistrosCajaEgreso(IdCajaPorRendirCabecera);

                return Ok(new { Result = "OK", Records = RegistrosCajaEgreso });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarCajaEgreso([FromBody] CajaEgresoDTO RequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CajaEgresoRepositorio _repoCajaEgreso = new CajaEgresoRepositorio(_integraDBContext);
                ComprobantePagoPorFurRepositorio _repoComprobantePagoPorFurRepositorio = new ComprobantePagoPorFurRepositorio(_integraDBContext);


                ComprobantePagoPorFurBO ComprobantePagoPorFur = new ComprobantePagoPorFurBO();
                ComprobantePagoPorFur.IdComprobantePago = RequestDTO.IdComprobantePago;
                ComprobantePagoPorFur.IdFur = (RequestDTO.IdFur == null ? 0 : RequestDTO.IdFur.Value);
                ComprobantePagoPorFur.Monto = RequestDTO.TotalEfectivo;
                ComprobantePagoPorFur.Estado = true;
                ComprobantePagoPorFur.UsuarioCreacion = RequestDTO.Usuario;
                ComprobantePagoPorFur.UsuarioModificacion = RequestDTO.Usuario;
                ComprobantePagoPorFur.FechaCreacion = DateTime.Now;
                ComprobantePagoPorFur.FechaModificacion = DateTime.Now;
                _repoComprobantePagoPorFurRepositorio.Insert(ComprobantePagoPorFur);

                CajaEgresoBO CajaEgreso = new CajaEgresoBO();
                CajaEgreso.IdCajaPorRendirCabecera = RequestDTO.IdCajaPorRendirCabecera;
                CajaEgreso.IdCaja = RequestDTO.IdCaja;
                CajaEgreso.IdComprobantePago = RequestDTO.IdComprobantePago;

                CajaEgreso.IdComprobantePagoPorFur = ComprobantePagoPorFur.Id;

                CajaEgreso.IdFur = RequestDTO.IdFur;
                CajaEgreso.Descripcion = RequestDTO.Descripcion;
                CajaEgreso.IdMoneda = RequestDTO.IdMoneda; 
                CajaEgreso.TotalEfectivo = RequestDTO.TotalEfectivo;
                CajaEgreso.IdCajaEgresoAprobado = null;
                CajaEgreso.EsEnviado = false;
                CajaEgreso.IdPersonalSolicitante = RequestDTO.IdPersonalSolicitante;
                CajaEgreso.Estado = true;
                CajaEgreso.UsuarioCreacion = RequestDTO.Usuario;
                CajaEgreso.UsuarioModificacion = RequestDTO.Usuario;
                CajaEgreso.FechaCreacion = DateTime.Now;
                CajaEgreso.FechaModificacion = DateTime.Now;
                _repoCajaEgreso.Insert(CajaEgreso);



                var CajasEgreso = _repoCajaEgreso.ObtenerRegistroCajaEgreso(CajaEgreso.Id);
                if (CajasEgreso.Count > 1) throw new Exception("Error: Multiples registros encontrados");
                if (CajasEgreso.Count == 0) throw new Exception("Error: Ningun registro encontrado");
                return Json(new { Result = "OK", Records = CajasEgreso[0] });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarComprobante([FromBody] ComprobantePagoInsercionDTO RequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    ComprobantePagoRepositorio _repoComprobantePago = new ComprobantePagoRepositorio();
                    ComprobantePagoBO ComprobantePago = new ComprobantePagoBO();

                    var Comprobante = _repoComprobantePago.GetBy(x=>x.IdProveedor == RequestDTO.IdProveedor 
                                                                && x.SerieComprobante == RequestDTO.SerieComprobante 
                                                                && x.NumeroComprobante == RequestDTO.NumeroComprobante).FirstOrDefault();

                    if (Comprobante != null)
                    {
                        return Json(new { Result = "OK", Mensaje = "COMPROBANTE-EXISTENTE" });
                    }

                    ComprobantePago.IdSunatDocumento = RequestDTO.IdSunatDocumento;
                    ComprobantePago.IdPais = RequestDTO.IdPais;
                    ComprobantePago.SerieComprobante = RequestDTO.SerieComprobante;
                    ComprobantePago.NumeroComprobante = RequestDTO.NumeroComprobante;
                    ComprobantePago.IdMoneda = RequestDTO.IdMoneda;
                    ComprobantePago.MontoBruto = RequestDTO.MontoBruto;
                    ComprobantePago.MontoInafecto = RequestDTO.MontoInafecto;
                    ComprobantePago.AjusteMontoBruto = RequestDTO.AjusteMontoBruto;
                    ComprobantePago.MontoNeto = RequestDTO.MontoNeto;
                    ComprobantePago.FechaEmision = RequestDTO.FechaEmision;
                    ComprobantePago.FechaProgramacion = RequestDTO.FechaProgramacion;
                    if (RequestDTO.IdTipoImpuesto != null)
                    {
                        ComprobantePago.IdTipoImpuesto = RequestDTO.IdTipoImpuesto;
                        ComprobantePago.PorcentajeIgv = RequestDTO.PorcentajeIgv;
                        ComprobantePago.MontoIgv = RequestDTO.MontoIgv;

                    } else
                    {
                        ComprobantePago.IdTipoImpuesto = null;
                        ComprobantePago.PorcentajeIgv = null;
                        ComprobantePago.MontoIgv = null;
                    }
                    ComprobantePago.IdRetencion = RequestDTO.IdRetencion;
                    ComprobantePago.IdDetraccion = RequestDTO.IdDetraccion;
                    ComprobantePago.OtraTazaContribucion = RequestDTO.OtraTazaContribucion;
                    ComprobantePago.IdProveedor = RequestDTO.IdProveedor;
                    ComprobantePago.Estado = true;
                    ComprobantePago.UsuarioCreacion = RequestDTO.Usuario;
                    ComprobantePago.UsuarioModificacion = RequestDTO.Usuario;
                    ComprobantePago.FechaCreacion = DateTime.Now;
                    ComprobantePago.FechaModificacion = DateTime.Now;

                    _repoComprobantePago.Insert(ComprobantePago);
                    scope.Complete();
                    return Ok(ComprobantePago);
                }


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }




        //[Route("[action]")]
        //[HttpPost]
        //public ActionResult ActualizarCajaEgreso([FromBody] CajaEgresoDTO RequestDTO)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    try
        //    {
        //        CajaEgresoRepositorio _repoCajaEgreso = new CajaEgresoRepositorio(_integraDBContext);
        //        ComprobantePagoPorFurRepositorio _repoComprobantePagoPorFurRepositorio = new ComprobantePagoPorFurRepositorio(_integraDBContext);

        //        CajaEgresoBO CajaEgreso = _repoCajaEgreso.GetBy(x => x.Id == RequestDTO.Id).FirstOrDefault();
        //        if (CajaEgreso == null) throw new Exception("El registro que se desea actualizar no existe ¿Id correcto?");

        //        bool CambioComprobante = (CajaEgreso.IdComprobantePago != RequestDTO.IdComprobantePago ? true : false);
        //        bool CambioFur = (CajaEgreso.IdFur != RequestDTO.IdFur ? true : false);

        //        ComprobantePagoPorFurBO ComprobantePagoPorFur = _repoComprobantePagoPorFurRepositorio.GetBy(x => x.IdFur == CajaEgreso.IdFur && x.IdComprobantePago == CajaEgreso.IdComprobantePago && x.Monto == CajaEgreso.TotalEfectivo && x.Estado == true).FirstOrDefault();
        //        if (ComprobantePagoPorFur == null)
        //            throw new Exception("No se encontro registro de 'ComprobantePagoPorFur' para el IdComprobante=" + CajaEgreso.IdComprobantePago + " e IdFur=" + CajaEgreso.IdFur + " indicados");
                


        //        CajaEgreso.IdCajaPorRendirCabecera = RequestDTO.IdCajaPorRendirCabecera;
        //        CajaEgreso.IdCaja = RequestDTO.IdCaja;
        //        CajaEgreso.IdComprobantePago = RequestDTO.IdComprobantePago;
        //        CajaEgreso.IdFur = RequestDTO.IdFur;
        //        CajaEgreso.Descripcion = RequestDTO.Descripcion;
        //        CajaEgreso.IdMoneda = RequestDTO.IdMoneda; 
        //        CajaEgreso.TotalEfectivo = RequestDTO.TotalEfectivo;
        //        CajaEgreso.IdCajaEgresoAprobado = null;
        //        CajaEgreso.EsEnviado = false;
        //        CajaEgreso.IdPersonalSolicitante = RequestDTO.IdPersonalSolicitante;
        //        CajaEgreso.Estado = true;
        //        CajaEgreso.UsuarioModificacion = RequestDTO.Usuario;
        //        CajaEgreso.FechaModificacion = DateTime.Now;

        //        _repoCajaEgreso.Update(CajaEgreso);

        //        if (CambioComprobante || CambioFur)
        //        {
        //            ComprobantePagoPorFur.IdComprobantePago = RequestDTO.IdComprobantePago;
        //            ComprobantePagoPorFur.IdFur = (RequestDTO.IdFur == null ? 0 : RequestDTO.IdFur.Value);
        //            ComprobantePagoPorFur.UsuarioModificacion = RequestDTO.Usuario;
        //            ComprobantePagoPorFur.FechaModificacion = DateTime.Now;
        //        }
               

        //        ComprobantePagoPorFur.Monto = RequestDTO.TotalEfectivo;
        //        _repoComprobantePagoPorFurRepositorio.Update(ComprobantePagoPorFur);


        //        var CajasEgreso = _repoCajaEgreso.ObtenerRegistroCajaEgreso(CajaEgreso.Id);
        //        if (CajasEgreso.Count > 1) throw new Exception("Error: Multiples registros encontrados");
        //        if (CajasEgreso.Count == 0) throw new Exception("Error: Ningun registro encontrado");
        //        return Json(new { Result = "OK", Records = CajasEgreso[0] });
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarCajaEgresoEstablecerRendido([FromBody] IdUsuarioIdPorRendirCabeceraDTO RequestDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CajaEgresoRepositorio _repoCajaEgreso = new CajaEgresoRepositorio(_integraDBContext);
                List<CajaEgresoBO> CajasEgreso = _repoCajaEgreso.GetBy(x => x.IdCajaPorRendirCabecera == RequestDTO.IdCajaPorRendirCabecera && x.Estado==true && x.EsEnviado==false).ToList();
                if (CajasEgreso == null) throw new Exception("El registro que se desea actualizar no existe ¿Id correcto?");
                else if (CajasEgreso.Count==0) throw new Exception("No se detecto ningun Registro para Rendirlo");


                for (int i =0; i<CajasEgreso.Count; ++i)
                {
                    CajasEgreso[i].EsEnviado = true;
                    CajasEgreso[i].FechaEnvio = DateTime.Now;
                    _repoCajaEgreso.Update(CajasEgreso[i]);
                }

                CajaPorRendirCabeceraRepositorio _repoCajaPorRendirCabecera = new CajaPorRendirCabeceraRepositorio(_integraDBContext);
                var CajaPorRendirCabecera = _repoCajaPorRendirCabecera.GetBy(x => x.Id == RequestDTO.IdCajaPorRendirCabecera).FirstOrDefault();
                if (CajaPorRendirCabecera == null) throw new Exception("El registro que se desea actualizar no existe ¿Id correcto?");

                CajaPorRendirCabecera.EsRendido = true;

                _repoCajaPorRendirCabecera.Update(CajaPorRendirCabecera);

                
                var RegistrosPorRendirCabecera = _repoCajaPorRendirCabecera.ObtenerCajasPorRendirParaRendicion(RequestDTO.IdPersonal);
                return Ok(new { Result = "OK", Records = RegistrosPorRendirCabecera });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarCajaEgreso([FromBody] EliminarCajaEgresoDTO eliminarDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CajaEgresoRepositorio _repoCajaEgreso = new CajaEgresoRepositorio(_integraDBContext);
                ComprobantePagoPorFurRepositorio _repoComprobantePagoPorFurRepositorio = new ComprobantePagoPorFurRepositorio(_integraDBContext);

                FurRepositorio _repoFur = new FurRepositorio(_integraDBContext);
                
                CajaEgresoBO CajaEgreso = _repoCajaEgreso.GetBy(x => x.Id == eliminarDTO.Id).FirstOrDefault();
                if (CajaEgreso == null) throw new Exception("El registro que se desea eliminar no existe ¿Id correcto?");


               
               ComprobantePagoPorFurBO ComprobantePagoPorFur = _repoComprobantePagoPorFurRepositorio.GetBy(x => x.IdFur == CajaEgreso.IdFur && x.IdComprobantePago == CajaEgreso.IdComprobantePago && x.Monto == CajaEgreso.TotalEfectivo && x.Estado == true).FirstOrDefault();
               if (ComprobantePagoPorFur == null)
                   throw new Exception("No se encontro registro de 'ComprobantePagoPorFur' para el IdComprobante=" + CajaEgreso.IdComprobantePago + " e IdFur=" + CajaEgreso.IdFur + " indicados");

               ComprobantePagoPorFur.Estado = false;
               ComprobantePagoPorFur.UsuarioModificacion = eliminarDTO.NombreUsuario;
               ComprobantePagoPorFur.FechaModificacion = DateTime.Now;
               _repoComprobantePagoPorFurRepositorio.Update(ComprobantePagoPorFur);



                var Fur = _repoFur.GetBy(x => x.Id == CajaEgreso.IdFur).FirstOrDefault();
                Fur.OcupadoRendicion = false;
                Fur.UsuarioModificacion = eliminarDTO.NombreUsuario;
                Fur.FechaModificacion = DateTime.Now;
                _repoFur.Update(Fur);

                _repoCajaEgreso.Delete(eliminarDTO.Id, eliminarDTO.NombreUsuario);
                return Ok(CajaEgreso);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerPRCabeceraPorValor([FromBody] Dictionary<string, string> Valor)
        {
            try
            {
                if (Valor != null && Valor.Count > 0)
                {
                    CajaPorRendirCabeceraRepositorio repPorRendirCabRep = new CajaPorRendirCabeceraRepositorio(_integraDBContext);
                    var listaPorRendir = repPorRendirCabRep.ObtenerCajaPorRendirAutocomplete(Valor["filtro"]);
                    return Ok(listaPorRendir);
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

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerFurAutocompleteREC([FromBody] Dictionary<string, string> Valor)
        {
            try
            {
                if (Valor != null && Valor.Count > 0)
                {
                    FurRepositorio repFurRep = new FurRepositorio(_integraDBContext);
                    var listaFur = repFurRep.ObtenerFursAutocompleteREC(Valor["filtro"]);
                    return Ok(listaFur);
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


        [Route("[action]")]
        [HttpPost]
        public ActionResult DevolverSolicitudEgresoCaja([FromBody] FiltroDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //integraDBContext contexto = new integraDBContext();
                CajaEgresoRepositorio repCajaRECrep = new CajaEgresoRepositorio(_integraDBContext);
                CajaEgresoBO cajaREC = new CajaEgresoBO();
                cajaREC = repCajaRECrep.FirstById(Json.Id);
                using (TransactionScope scope = new TransactionScope())
                {
                    cajaREC.EsEnviado = false;
                    cajaREC.FechaEnvio = null;
                    cajaREC.FechaModificacion = DateTime.Now;
                    cajaREC.UsuarioModificacion = Json.Nombre;
                    repCajaRECrep.Update(cajaREC);
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
        [HttpGet]
        public ActionResult ObtenerCajaRegistroEgreso(int IdPersonal, int? idCaja,int ? idsolicitante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CajaEgresoRepositorio repCajaEgresoRep = new CajaEgresoRepositorio(_integraDBContext);

                var listadoREC = repCajaEgresoRep.ObtenerCajaEgresoEnviado(IdPersonal, idCaja, idsolicitante);
                var listadoSolicitante = listadoREC.Select(x => new { Id =x.IdPersonalSolicitante,Nombre=x.PersonalSolicitante }).Distinct().ToList();
                if (listadoREC != null)
                {
                }
                return Ok(new{ listadoREC, listadoSolicitante ,Total= listadoREC.Count()});
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarRegistroEgresoCaja([FromBody] GenerarRegistroEgresoDTO generacionRegEgresoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string result = "SUCCESS";
                integraDBContext integraDBContext = new integraDBContext();
                CajaEgresoAprobadoRepositorio _repEgresoAprobadoRep = new CajaEgresoAprobadoRepositorio(integraDBContext);

                int correlativo = 0;
                var listCodigos = _repEgresoAprobadoRep.GetBy(x => x.Estado == true && x.CodigoRec.Contains(generacionRegEgresoDTO.CajaRECAprobado.CodigoRec), x => new { x.CodigoRec }).ToList();
                if (listCodigos != null && listCodigos.Count() != 0)
                {
                    foreach (var Codigos in listCodigos)
                    {
                        if (Int32.Parse(Codigos.CodigoRec.Substring(Codigos.CodigoRec.LastIndexOf(".") + 1).Trim()) > correlativo)
                        {
                            correlativo = Int32.Parse(Codigos.CodigoRec.Substring(Codigos.CodigoRec.LastIndexOf(".") + 1).Trim());
                        }
                    }
                    correlativo++;
                }
                else
                {
                    correlativo = 1;
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    CajaEgresoBO registroEgreso = new CajaEgresoBO(integraDBContext);
                    generacionRegEgresoDTO.CajaRECAprobado.CodigoRec += correlativo;

                    CajaEgresoAprobadoBO egresoAprobado = new CajaEgresoAprobadoBO();
                    egresoAprobado.CodigoRec = generacionRegEgresoDTO.CajaRECAprobado.CodigoRec;
                    egresoAprobado.IdCaja = generacionRegEgresoDTO.CajaRECAprobado.IdCaja;
                    egresoAprobado.Anho = generacionRegEgresoDTO.CajaRECAprobado.Anho;
                    egresoAprobado.FechaCreacionRegistro = generacionRegEgresoDTO.CajaRECAprobado.FechaCreacionRegistro;
                    egresoAprobado.Detalle = generacionRegEgresoDTO.CajaRECAprobado.Detalle;
                    egresoAprobado.Observacion = generacionRegEgresoDTO.CajaRECAprobado.Observacion;
                    egresoAprobado.Origen = generacionRegEgresoDTO.CajaRECAprobado.Origen;
                    egresoAprobado.Estado = true;
                    egresoAprobado.UsuarioCreacion = generacionRegEgresoDTO.CajaRECAprobado.UsuarioModificacion;
                    egresoAprobado.UsuarioModificacion = generacionRegEgresoDTO.CajaRECAprobado.UsuarioModificacion;
                    egresoAprobado.FechaModificacion = DateTime.Now;
                    egresoAprobado.FechaCreacion = DateTime.Now;
                    _repEgresoAprobadoRep.Insert(egresoAprobado);

                    generacionRegEgresoDTO.CajaRECAprobado.Id = egresoAprobado.Id; //_repEgresoAprobadoRep.InsertarRegistroEgresoAprobado(generacionRegEgresoDTO.CajaRECAprobado, integraDBContext);

                    CajaEgresoRepositorio _repCajaEgresoRep = new CajaEgresoRepositorio(integraDBContext);
                    foreach (var listaId in generacionRegEgresoDTO.ListaEgresoCancelado)
                    {
                        registroEgreso.ActualizarAprobacionCajaEgreso(generacionRegEgresoDTO.CajaRECAprobado, listaId);
                        //_repCajaEgresoRep.ActualizarAprobacionCajaEgreso(generacionRegEgresoDTO.CajaRECAprobado, listaId, generacionRegEgresoDTO.EsCancelado[listaId], integraDBContext);
                       // _repCajaEgresoRep.ActualizarFurCancelar(generacionRegEgresoDTO.EsCancelado[listaId], listaId, integraDBContext);

                    }
                    scope.Complete();
                }
                return Ok(new { result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarRegistroEgresoCajaInmediato([FromBody] GenerarRegistroEgresoInmediatoDTO generacionEgresoDirecto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string result = "SUCCESS";
                integraDBContext integraDBContext = new integraDBContext();
                CajaEgresoAprobadoRepositorio _repEgresoAprobadoRep = new CajaEgresoAprobadoRepositorio(integraDBContext);

                int correlativo = 0;
                var listCodigos = _repEgresoAprobadoRep.GetBy(x => x.Estado == true && x.CodigoRec.Contains(generacionEgresoDirecto.CajaEgresoAprobado.CodigoRec) && x.IdCaja== generacionEgresoDirecto.CajaEgresoAprobado.IdCaja, x => new { x.CodigoRec }).ToList();
                if (listCodigos != null && listCodigos.Count() != 0)
                {
                    foreach (var Codigos in listCodigos)
                    {
                        if (Int32.Parse(Codigos.CodigoRec.Substring(Codigos.CodigoRec.LastIndexOf(".") + 1).Trim()) > correlativo)
                        {
                            correlativo = Int32.Parse(Codigos.CodigoRec.Substring(Codigos.CodigoRec.LastIndexOf(".") + 1).Trim());
                        }
                    }
                    correlativo++;
                }
                else
                {
                    correlativo = 1;
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    CajaEgresoBO registroEgreso = new CajaEgresoBO(integraDBContext);
                    generacionEgresoDirecto.CajaEgresoAprobado.CodigoRec += correlativo;

                    CajaEgresoAprobadoBO egresoAprobado = new CajaEgresoAprobadoBO();
                    egresoAprobado.CodigoRec = generacionEgresoDirecto.CajaEgresoAprobado.CodigoRec;
                    egresoAprobado.IdCaja = generacionEgresoDirecto.CajaEgresoAprobado.IdCaja;
                    egresoAprobado.Anho = generacionEgresoDirecto.CajaEgresoAprobado.Anho;
                    egresoAprobado.FechaCreacionRegistro = generacionEgresoDirecto.CajaEgresoAprobado.FechaCreacionRegistro;
                    egresoAprobado.Detalle = generacionEgresoDirecto.CajaEgresoAprobado.Detalle;
                    egresoAprobado.Observacion = generacionEgresoDirecto.CajaEgresoAprobado.Observacion;
                    egresoAprobado.Origen = generacionEgresoDirecto.CajaEgresoAprobado.Origen;
                    egresoAprobado.Estado = true;
                    egresoAprobado.UsuarioCreacion = generacionEgresoDirecto.CajaEgresoAprobado.UsuarioModificacion;
                    egresoAprobado.UsuarioModificacion = generacionEgresoDirecto.CajaEgresoAprobado.UsuarioModificacion;
                    egresoAprobado.FechaModificacion = DateTime.Now;
                    egresoAprobado.FechaCreacion = DateTime.Now;
                    _repEgresoAprobadoRep.Insert(egresoAprobado);

                    generacionEgresoDirecto.CajaEgresoAprobado.Id = egresoAprobado.Id; //_repEgresoAprobadoRep.InsertarRegistroEgresoAprobado(generacionRegEgresoDTO.CajaRECAprobado, integraDBContext);

                    CajaEgresoRepositorio _repCajaEgresoRep = new CajaEgresoRepositorio(integraDBContext);
                    foreach (var listaCajaEgreso in generacionEgresoDirecto.ListaRegistroEgreso)
                    {
                        registroEgreso.InsertarAprobacionCajaEgreso(generacionEgresoDirecto.CajaEgresoAprobado, listaCajaEgreso);
                        //_repCajaEgresoRep.ActualizarAprobacionCajaEgreso(generacionRegEgresoDTO.CajaRECAprobado, listaId, generacionRegEgresoDTO.EsCancelado[listaId], integraDBContext);
                        // _repCajaEgresoRep.ActualizarFurCancelar(generacionRegEgresoDTO.EsCancelado[listaId], listaId, integraDBContext);

                    }
                    scope.Complete();
                }
                return Ok(new { result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarRegistroEgresoCajaEnviado([FromBody] RegistroEgresoCajaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //integraDBContext contexto = new integraDBContext();
                integraDBContext _context = new integraDBContext();
                CajaEgresoRepositorio repCajaEgresoRep = new CajaEgresoRepositorio(_context);
                ComprobantePagoPorFurRepositorio repComprobantePagoPorFurRep = new ComprobantePagoPorFurRepositorio(_context);
                ComprobantePagoPorFurBO comprobanteAsociado = new ComprobantePagoPorFurBO();
                var cajaRec = repCajaEgresoRep.FirstById(Json.Id);
                
                using (TransactionScope scope = new TransactionScope())
                {
                    if ((cajaRec.IdFur != Json.IdFur || cajaRec.TotalEfectivo != Json.TotalEfectivo || cajaRec.IdComprobantePago != Json.IdComprobantePago) &&
                        (Json.IdComprobantePago != null && Json.IdFur != null && Json.IdComprobantePago != 0 && Json.IdFur != 0))
                    {

                        if (cajaRec.IdComprobantePagoPorFur != null && cajaRec.IdComprobantePagoPorFur != 0)
                        {
                            repComprobantePagoPorFurRep.Delete(cajaRec.IdComprobantePagoPorFur.Value, Json.UsuarioModificacion);
                        }
                        comprobanteAsociado.IdComprobantePago = Json.IdComprobantePago.Value;
                        comprobanteAsociado.IdFur = Json.IdFur.Value;
                        comprobanteAsociado.Monto = Json.TotalEfectivo;
                        comprobanteAsociado.Estado = true;
                        comprobanteAsociado.UsuarioCreacion = Json.UsuarioModificacion;
                        comprobanteAsociado.UsuarioModificacion = Json.UsuarioModificacion;
                        comprobanteAsociado.FechaModificacion = DateTime.Now;
                        comprobanteAsociado.FechaCreacion = DateTime.Now;
                        repComprobantePagoPorFurRep.Insert(comprobanteAsociado);
                    }
                    else { comprobanteAsociado = null; }

                    cajaRec.IdComprobantePago = Json.IdComprobantePago;
                    cajaRec.IdComprobantePagoPorFur = comprobanteAsociado == null ? cajaRec.IdComprobantePagoPorFur : comprobanteAsociado.Id;
                    cajaRec.IdFur = Json.IdFur;
                    cajaRec.Descripcion = Json.Descripcion;
                    cajaRec.IdMoneda = Json.IdMoneda;
                    cajaRec.TotalEfectivo = Json.TotalEfectivo;
                    cajaRec.UsuarioModificacion = Json.UsuarioModificacion;
                    cajaRec.FechaModificacion = DateTime.Now;

                    repCajaEgresoRep.Update(cajaRec);
                    

                    if (Json.IdFur != Json.IdFurAnterior)
                    {
                        FurRepositorio repFurRep = new FurRepositorio(_context);
                        if (Json.IdFur != null && Json.IdFur !=0) {
                            var fur = repFurRep.FirstById(Json.IdFur.Value);
                            fur.OcupadoRendicion = true;
                            fur.OcupadoSolicitud = true;
                            repFurRep.Update(fur);
                        }
                        if (Json.IdFurAnterior != null && Json.IdFurAnterior != 0)
                        {
                            var fur = repFurRep.FirstById(Json.IdFurAnterior.Value);
                            fur.OcupadoRendicion = false;
                            fur.OcupadoSolicitud = false;
                            repFurRep.Update(fur);
                        }
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

    }
    
}
