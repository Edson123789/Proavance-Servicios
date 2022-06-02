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
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CajaPorRendir")]
    public class CajaPorRendirController : Controller
    {
        public CajaPorRendirController()
        {
        }

        #region MetodosAdicionales
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaMoneda()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MonedaRepositorio _repoMoneda = new MonedaRepositorio();
                var listaMoneda = _repoMoneda.ObtenerFiltroMoneda();
                return Json(new { Result = "OK", Records = listaMoneda });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaResponsableCaja()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CajaRepositorio _repoCaja = new CajaRepositorio();
                var lista = _repoCaja.ObtenerListaCajaResponsable();
                return Json(new { Result = "OK", Records = lista });
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
                    return Json(new { Result = "OK", Records = ListaVacia });
                }

                FurRepositorio _repoFur = new FurRepositorio();
                var lista = _repoFur.ObtenerDatosFur(NombreParcial);
                return Json(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerIdentidadUsuario(string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                IntegraAspNetUsersRepositorio _repoIntegraAspNetUsers = new IntegraAspNetUsersRepositorio();
                var UsuarioEncontrado = _repoIntegraAspNetUsers.ObtenerIdentidadUsusario(Usuario);
                return Json(new { Result = "OK", Id=UsuarioEncontrado.Id, Nombre=UsuarioEncontrado.NombreCompleto });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
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
                FurRepositorio _repoFur = new FurRepositorio();
                FurPagoRepositorio _repoFurPago = new FurPagoRepositorio();

                FurBO Fur = _repoFur.GetBy(x=>x.Id==IdFur).FirstOrDefault();

                List<FurPagoBO>  FurPagos = _repoFurPago.GetBy(x=>x.IdFur==IdFur).ToList();

                if (Fur==null) throw new Exception("Error No se encontro el FUR");

                decimal PagosAcumulado = 0;
                for (int i = 0; i < FurPagos.Count; ++i)
                    PagosAcumulado += FurPagos[i].PrecioTotalMonedaOrigen; 

                return Json(new { Result = "OK", MontoLimite=(Fur.PrecioTotalMonedaOrigen-PagosAcumulado) }); ;
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        #endregion

        #region CRUD
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarCajaPorRendirPonerEnviado([FromBody]  CajaPorRendirCompuestoDTO CajasPorRendir)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                for (int i = 0; i < CajasPorRendir.ListaCajaPorRendir.Count; ++i) {
                    CajaPorRendirRepositorio _repoCajaPorRendir = new CajaPorRendirRepositorio();
                    CajaPorRendirBO CajaPorRendir = _repoCajaPorRendir.GetBy(x => x.Id == CajasPorRendir.ListaCajaPorRendir[i].Id).FirstOrDefault();
                    if (CajaPorRendir == null) throw new Exception("No se encontro el registro de 'CajaPorRendir' que se quiere actualizar");


                    CajaPorRendir.EsEnviado = true;
                    CajaPorRendir.FechaEnvio = DateTime.Now;
                    CajaPorRendir.Estado = true;
                    CajaPorRendir.FechaModificacion = DateTime.Now;
                    CajaPorRendir.UsuarioModificacion = CajasPorRendir.ListaCajaPorRendir[i].UsuarioModificacion;

                    _repoCajaPorRendir.Update(CajaPorRendir);
                }


                
                return Json(new { Result = "OK" });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult VisualizarCajaPorRendir(int Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IntegraAspNetUsersRepositorio _repoIntegraAspNetUsers = new IntegraAspNetUsersRepositorio();
                CajaPorRendirRepositorio _repoCajaPorRendir = new CajaPorRendirRepositorio();
                var lista = _repoCajaPorRendir.ObtenerCajasPorRendirFinanzas(Usuario);
                return Json(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarCajaPorRendir([FromBody] CajaPorRendirDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CajaPorRendirRepositorio _repoCajaPorRendir = new CajaPorRendirRepositorio();
                CajaPorRendirBO NuevoCajaPorRendir = new CajaPorRendirBO();

                NuevoCajaPorRendir.IdCaja = null;
                NuevoCajaPorRendir.FechaEnvio = null;
                NuevoCajaPorRendir.IdCajaPorRendirCabecera = null;
                NuevoCajaPorRendir.FechaAprobacion = null;
                NuevoCajaPorRendir.IdPersonalSolicitante = ObjetoDTO.IdPersonalSolicitante  ;
                NuevoCajaPorRendir.IdFur = ObjetoDTO.IdFur;
                NuevoCajaPorRendir.IdPersonalResponsableCaja = ObjetoDTO.IdPersonalResponsable;
                NuevoCajaPorRendir.Descripcion = ObjetoDTO.Descripcion;
                NuevoCajaPorRendir.IdMoneda = ObjetoDTO.IdMoneda;
                NuevoCajaPorRendir.TotalEfectivo = ObjetoDTO.TotalEfectivo;
                NuevoCajaPorRendir.FechaEntregaEfectivo = ObjetoDTO.FechaEntregaEfectivo;
                NuevoCajaPorRendir.EsEnviado = false;
                NuevoCajaPorRendir.Estado = true  ;
                NuevoCajaPorRendir.FechaCreacion = DateTime.Now  ;
                NuevoCajaPorRendir.FechaModificacion = DateTime.Now;
                NuevoCajaPorRendir.UsuarioCreacion = ObjetoDTO.UsuarioModificacion  ;
                NuevoCajaPorRendir.UsuarioModificacion = ObjetoDTO.UsuarioModificacion  ;

                _repoCajaPorRendir.Insert(NuevoCajaPorRendir);


                FurRepositorio _repoFur = new FurRepositorio();
                FurBO Fur = _repoFur.GetBy(x => x.Id == ObjetoDTO.IdFur).FirstOrDefault();
                if (Fur == null) 
                    throw new Exception("No se encontro el FUR a actualizar");

                Fur.OcupadoSolicitud = true;
                _repoFur.Update(Fur);

                var CajasPorRendir = _repoCajaPorRendir.ObtenerCajasPorRendirSolicitudFinanzas(ObjetoDTO.IdPersonalSolicitante, NuevoCajaPorRendir.Id);
                if (CajasPorRendir.Count > 1) throw new Exception("Error: Multipes registros encontrados");
                if (CajasPorRendir.Count == 0) throw new Exception("Error: Ningun registro encontrado");
                return Json(new { Result = "OK", Records = CajasPorRendir[0] });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarCajaPorRendir([FromBody] CajaPorRendirDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CajaPorRendirRepositorio _repoCajaPorRendir = new CajaPorRendirRepositorio();
                CajaPorRendirBO CajaPorRendir = _repoCajaPorRendir.GetBy(x=>x.Id==ObjetoDTO.Id).FirstOrDefault();
                if (CajaPorRendir == null) throw new Exception("No se encontro el registro de 'CajaPorRendir' que se quiere actualizar");

                CajaPorRendir.IdCaja = null;
                CajaPorRendir.FechaEnvio = null;
                CajaPorRendir.IdCajaPorRendirCabecera = null;
                CajaPorRendir.FechaAprobacion = null;
                CajaPorRendir.IdPersonalSolicitante = ObjetoDTO.IdPersonalSolicitante;
                CajaPorRendir.IdFur = ObjetoDTO.IdFur;
                CajaPorRendir.IdPersonalResponsableCaja = ObjetoDTO.IdPersonalResponsable;
                CajaPorRendir.Descripcion = ObjetoDTO.Descripcion;
                CajaPorRendir.IdMoneda = ObjetoDTO.IdMoneda;
                CajaPorRendir.TotalEfectivo = ObjetoDTO.TotalEfectivo;
                CajaPorRendir.FechaEntregaEfectivo = ObjetoDTO.FechaEntregaEfectivo;
                CajaPorRendir.EsEnviado = false;
                CajaPorRendir.Estado = true;
                CajaPorRendir.FechaModificacion = DateTime.Now;
                CajaPorRendir.UsuarioModificacion = ObjetoDTO.UsuarioModificacion;

                _repoCajaPorRendir.Update(CajaPorRendir);


                FurRepositorio _repoFur = new FurRepositorio();
                FurBO Fur = _repoFur.GetBy(x => x.Id == ObjetoDTO.IdFur).FirstOrDefault();
                if (Fur == null) 
                    throw new Exception("No se encontro el FUR a actualizar");

                Fur.OcupadoSolicitud = true;
                _repoFur.Update(Fur);

                var CajasPorRendir = _repoCajaPorRendir.ObtenerCajasPorRendirSolicitudFinanzas(ObjetoDTO.IdPersonalSolicitante, CajaPorRendir.Id);
                if (CajasPorRendir.Count > 1) throw new Exception("Error: Multipes registros encontrados");
                if (CajasPorRendir.Count == 0) throw new Exception("Error: Ningun registro encontrado");
                return Json(new { Result = "OK", Records = CajasPorRendir[0] });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        #endregion

        [Route("[action]")]
        [HttpPost]
        public ActionResult DevolverSolicitudPorRendir([FromBody] FiltroDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //integraDBContext contexto = new integraDBContext();
                CajaPorRendirRepositorio repCajaPRrep = new CajaPorRendirRepositorio();
                CajaPorRendirBO cajaPR = new CajaPorRendirBO();
                cajaPR = repCajaPRrep.FirstById(Json.Id);
                using (TransactionScope scope = new TransactionScope())
                {
                    cajaPR.EsEnviado = false;
                    cajaPR.FechaEnvio = null;
                    cajaPR.FechaModificacion = DateTime.Now;
                    cajaPR.UsuarioModificacion = Json.Nombre;
                    repCajaPRrep.Update(cajaPR);
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
        public ActionResult EliminarCajaPorRendirSolicitudEnviada([FromBody] EliminarCajaPorRendirDTO eliminarDTO)
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
                    CajaPorRendirRepositorio  repCajaPRrep = new CajaPorRendirRepositorio(_integraDBContext);
                    FurRepositorio repFurRep = new FurRepositorio(_integraDBContext);
                    if (repCajaPRrep.Exist(eliminarDTO.Id))
                    {
                        repCajaPRrep.Delete(eliminarDTO.Id, eliminarDTO.NombreUsuario);
                        repFurRep.CambiarEstadoFurSolicitudCajaPR(eliminarDTO.IdFur);
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
        [HttpGet]
        public ActionResult ObtenerCajaPorRendir(int IdPersonal,int? idCaja, int? idPersonalSolicitante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CajaPorRendirRepositorio repCajaPR = new CajaPorRendirRepositorio();
                CajaRepositorio repCajaRep = new CajaRepositorio();
                int? IdMonedaCaja = null;
                if (idCaja != null)
                {
                    IdMonedaCaja = repCajaRep.GetBy(w => w.Id == idCaja).FirstOrDefault().IdMoneda;
                }

                var listadoPR = repCajaPR.ObtenerCajaPorRendirEnviada(IdPersonal, IdMonedaCaja, idPersonalSolicitante);
                if (listadoPR != null)
                {
                }
                return Ok(listadoPR);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerSolicitante(int IdPersonal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CajaPorRendirRepositorio repCajaPR = new CajaPorRendirRepositorio();
                var listaSolicitante = repCajaPR.ObtenerSolicitante(IdPersonal);
                return Ok(listaSolicitante);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]/{IdCaja}")]
        [HttpGet]
        public ActionResult ObtenerMontoCaja(int IdCaja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CajaPorRendirRepositorio repCajaPR = new CajaPorRendirRepositorio();
                var montoCaja=repCajaPR.ObtenerMontoTotalCaja(IdCaja);
                //var listadoPR = repCajaPR.ObtenerCajaPR(IdPersonal, idSolicitante);
                //if (listadoPR != null)
                //{
                //}
                return Ok(montoCaja);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult GenerarPorRendir([FromBody] GenerarPorRendirDTO generacionPorRendirDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string result = "SUCCESS";
                integraDBContext integraDBContext = new integraDBContext();
                CajaPorRendirCabeceraRepositorio _repPRCabeceraRep = new CajaPorRendirCabeceraRepositorio(integraDBContext);

                int correlativo = 0;
                var listCodigos = _repPRCabeceraRep.GetBy(x => x.Estado == true && x.Codigo.Contains(generacionPorRendirDTO.CajaPRCabecera.Codigo), x => new { x.Codigo }).ToList();
                if (listCodigos != null && listCodigos.Count()!=0)
                {
                    foreach (var Codigos in listCodigos)
                    {
                        if (Int32.Parse(Codigos.Codigo.Substring(Codigos.Codigo.LastIndexOf(".") + 1).Trim()) > correlativo)
                        {
                            correlativo = Int32.Parse(Codigos.Codigo.Substring(Codigos.Codigo.LastIndexOf(".") + 1).Trim());
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
                    
                    generacionPorRendirDTO.CajaPRCabecera.Codigo += correlativo;
                    generacionPorRendirDTO.CajaPRCabecera.Id = _repPRCabeceraRep.InsertarPorRendirCabecera(generacionPorRendirDTO.CajaPRCabecera, integraDBContext);

                    CajaPorRendirRepositorio _repCajaPorRendirRep = new CajaPorRendirRepositorio(integraDBContext);
                    foreach (var listaId in generacionPorRendirDTO.ListaIdPorRendir) {
                        _repCajaPorRendirRep.ActualizarCajaPorRendirAprobacion(generacionPorRendirDTO.CajaPRCabecera, listaId, integraDBContext);
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
        public ActionResult GenerarPorRendirInmediato([FromBody] GenerarPorRendirInmediatoDTO PorRendirInmediatoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string result = "SUCCESS";
                integraDBContext integraDBContext = new integraDBContext();
                CajaPorRendirCabeceraRepositorio _repPRCabeceraRep = new CajaPorRendirCabeceraRepositorio(integraDBContext);

                int correlativo = 0;
                var listCodigos = _repPRCabeceraRep.GetBy(x => x.Estado == true && x.Codigo.Contains(PorRendirInmediatoDTO.CajaPRCabecera.Codigo), x => new { x.Codigo }).ToList();
                if (listCodigos != null && listCodigos.Count() != 0)
                {
                    foreach (var Codigos in listCodigos)
                    {
                        if (Int32.Parse(Codigos.Codigo.Substring(Codigos.Codigo.LastIndexOf(".") + 1).Trim()) > correlativo)
                        {
                            correlativo = Int32.Parse(Codigos.Codigo.Substring(Codigos.Codigo.LastIndexOf(".") + 1).Trim());
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

                    PorRendirInmediatoDTO.CajaPRCabecera.Codigo += correlativo;
                    int IdPorRendirCabecera = _repPRCabeceraRep.InsertarPorRendirCabecera(PorRendirInmediatoDTO.CajaPRCabecera, integraDBContext);

                    CajaPorRendirRepositorio _repCajaPorRendirRep = new CajaPorRendirRepositorio(integraDBContext);
                    FurRepositorio _repFurRep = new FurRepositorio(integraDBContext);
                    foreach (var listaPorRendir in PorRendirInmediatoDTO.ListaPorRendir)
                    {
                        _repCajaPorRendirRep.InsertarRegistroPorRendirInmediato(listaPorRendir, IdPorRendirCabecera,integraDBContext);
                        _repFurRep.ActualizarEstadoOcupadoFur(listaPorRendir.IdFur.Value, integraDBContext);
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

    }
}
