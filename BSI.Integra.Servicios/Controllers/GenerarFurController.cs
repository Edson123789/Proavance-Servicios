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

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/GenerarFur")]
    public class GenerarFurController : Controller
    {
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerTipoPedidoFur()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FurTipoPedidoRepositorio repTipoPedidoRep = new FurTipoPedidoRepositorio();
                return Ok(repTipoPedidoRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, x.Nombre }));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
        [Route("[action]/{IdCiudad}")]
        [HttpGet]
        public ActionResult ObtenerEmpresasFur(int IdCiudad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EmpresaAutorizadaRepositorio repositorioEmpresaAutorizada = new EmpresaAutorizadaRepositorio();
                return Ok(repositorioEmpresaAutorizada.ObetenerEmpresasAutirizadasPorCiudad(IdCiudad));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerEstadoFaseAprobacion()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FurFaseAprobacionRepositorio repFurFaseRep = new FurFaseAprobacionRepositorio();
                return Ok(repFurFaseRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, x.Nombre }));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerFurGrid([FromBody] ParametrosFurDTO json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FurBO furBO = new FurBO();
                var Records=furBO.ObtenerFursParaGrid(json);
                var Total = Records.Count;
                return Ok(new { Records, Total });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]/{codigo}")]
        [HttpGet]
        public ActionResult ObtenerFurBusquedaCodigo(string codigo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (codigo.Equals(""))
                {
                    return BadRequest(" ");
                }
                else
                {
                    FurRepositorio repFurRep = new FurRepositorio();
                    return Ok(repFurRep.ObtenerFursBusquedaCodigo(codigo));
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerFurPorAprobar([FromBody] ParametroFurPorAprobarDTO json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FurBO furBO = new FurBO();
                return Ok(furBO.ObtenerFursPorAprobar(json));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult AprobarFurProyectado([FromBody] ListaFiltroDTO json)
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
                    FurRepositorio repFurRep = new FurRepositorio(integraDB);
                    PersonalAreaTrabajoRepositorio repAreaTrabajoRep = new PersonalAreaTrabajoRepositorio(integraDB);
                    CiudadRepositorio repCiudadRep = new CiudadRepositorio(integraDB);
                    FurBO fur = new FurBO();
                    int idAreaTrabajo=0;
                    int idCiudad = 0;
                    string AreaTrabajo="";
                    string Ciudad = "";
                    AreaTrabajo = repAreaTrabajoRep.FirstById(3).Codigo.Trim();
                    foreach (var objIdUsuario in json.ListaFiltro)
                    {
                        idAreaTrabajo = repFurRep.FirstById(objIdUsuario.Id).IdPersonalAreaTrabajo.Value;
                        idCiudad = repFurRep.FirstById(objIdUsuario.Id).IdCiudad;
                        AreaTrabajo = repAreaTrabajoRep.FirstById(idAreaTrabajo).Codigo.Trim();
                        Ciudad = repCiudadRep.FirstById(idCiudad).Nombre.Trim();
                        repFurRep.AprobarFurProyectado(objIdUsuario,AreaTrabajo,Ciudad, integraDB);
                    }
                    scope.Complete();
                }
                string result = "APROBADOS CORRECTAMENTE";
                return Ok(new {result});
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarFurMasivamente([FromBody] ListaFiltroDTO json)
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
                    integraDBContext integraDB = new integraDBContext();
                    FurRepositorio repFurRep = new FurRepositorio(_integraDBContext);


                    foreach (var objIdUsuario in json.ListaFiltro)
                    {
                        if (repFurRep.Exist(objIdUsuario.Id))
                        {
                            repFurRep.Delete(objIdUsuario.Id, objIdUsuario.Nombre);
                        }
                        else
                        {
                            return BadRequest("Registro no existente");
                        }
                    }
                    scope.Complete();
                }
                string result = "ELIMINADOS CORRECTAMENTE";
                return Ok(new { result });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult AprobarObservarFur([FromBody] ListaFiltroDTO json)
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
                    FurBO fur = new FurBO();
                    foreach (var objIdUsuario in json.ListaFiltro)
                    {
                        fur.AprobarObservarFur(objIdUsuario,json.IdRol,json.CheckedIsFurGeneral, json.isAprobar, json.Observacion, integraDB);
                    }
                    scope.Complete();
                }
                string result = "APROBADOS CORRECTAMENTE";
                if (!json.isAprobar) {
                    result = "OBSERVADOS CORRECTAMENTE";
                }
                return Ok(new { result });
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
        public ActionResult InsertarFur([FromBody] FurDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                FurRepositorio repFurRep = new FurRepositorio();
                FurBO fur = new FurBO();
                int correlativo = 0;
                var listCodigos=repFurRep.GetBy(x => x.Estado == true && x.Codigo.Contains(Json.Codigo), x => new { x.Codigo }).ToList();
                if (listCodigos != null || listCodigos.Count()!=0)
                {
                    foreach (var Codigos in listCodigos)
                    {
                        if (Int32.Parse(Codigos.Codigo.Substring(Codigos.Codigo.LastIndexOf("-") + 1).Trim()) > correlativo)
                        {
                            correlativo = Int32.Parse(Codigos.Codigo.Substring(Codigos.Codigo.LastIndexOf("-") + 1).Trim());
                        }
                    }
                    correlativo++;
                }
                else {
                    correlativo = 1;
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    fur.Codigo = Json.Codigo+correlativo;
                    fur.IdPersonalAreaTrabajo = Json.IdPersonalAreaTrabajo;
                    fur.IdCiudad = Json.IdCiudad;
                    fur.IdFurTipoPedido = Json.IdFurTipoPedido;
                    fur.NumeroSemana = Json.NumeroSemana.Value;
                    fur.NumeroFur = Json.Codigo + correlativo;
                    fur.UsuarioSolicitud = Json.UsuarioModificacion;
                    fur.IdCentroCosto = Json.IdCentroCosto.Value;
                    fur.NumeroCuenta = Json.NumeroCuenta;
                    fur.Cuenta = Json.Cuenta;
                    fur.IdProveedor = Json.IdProveedor.Value;
                    fur.IdProducto = Json.IdProducto.Value;
                    fur.Cantidad = Json.Cantidad;
                    fur.IdProductoPresentacion = Json.IdProductoPresentacion.Value;
                    fur.Descripcion = Json.Descripcion;
                    fur.FechaLimite = DateTime.Parse(Json.FechaLimite);
                    fur.PrecioUnitarioMonedaOrigen = Json.PrecioUnitarioMonedaOrigen;
                    fur.PrecioUnitarioDolares = Json.PrecioUnitarioDolares;
                    fur.IdMonedaProveedor = Json.IdMoneda_Proveedor;
                    fur.IdFurFaseAprobacion1 = ValorEstatico.IdFurEstadoPorAprobar;
                    fur.PrecioTotalMonedaOrigen = Json.PrecioUnitarioMonedaOrigen * Json.Cantidad;
                    fur.PrecioTotalDolares = Json.PrecioUnitarioDolares * Json.Cantidad;
                    fur.PagoMonedaOrigen= Json.PrecioUnitarioMonedaOrigen * Json.Cantidad;
                    fur.PagoDolares = Json.PrecioUnitarioDolares * Json.Cantidad;
                    fur.Cancelado = false;
                    fur.Antiguo = 0;
                    fur.IdMonedaPagoReal = Json.IdMonedaPagoReal;
                    fur.IdMonedaPagoRealizado = Json.IdMoneda_Proveedor;
                    fur.EstadoAprobadoObservado = false;
                    fur.OcupadoSolicitud = false;
                    fur.OcupadoRendicion = false;
                    fur.UsuarioCreacion = Json.UsuarioModificacion;
                    fur.FechaCreacion = DateTime.Now;
                    fur.UsuarioModificacion = Json.UsuarioModificacion;
                    fur.FechaModificacion = DateTime.Now;
                    fur.Estado = true;
                    fur.IdEmpresa = Json.IdEmpresa;                   
                    repFurRep.Insert(fur);
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
        public ActionResult ActualizarFur([FromBody] FurDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //integraDBContext contexto = new integraDBContext();
                FurRepositorio repFurRep = new FurRepositorio();
                FurBO fur = new FurBO();
                fur = repFurRep.FirstById(Json.Id);
                
                using (TransactionScope scope = new TransactionScope())
                {                                     
                    //fur.IdPersonalAreaTrabajo = Json.IdPersonalAreaTrabajo;
                    fur.IdCiudad = Json.IdCiudad;
                    fur.IdFurTipoPedido = Json.IdFurTipoPedido;
                    fur.NumeroSemana = Json.NumeroSemana.Value;
                    //fur.UsuarioSolicitud = Json.UsuarioModificacion;
                    fur.IdCentroCosto = Json.IdCentroCosto.Value;
                    fur.NumeroCuenta = Json.NumeroCuenta;
                    fur.Cuenta = Json.Cuenta;
                    fur.IdProveedor = Json.IdProveedor;
                    fur.IdProducto = Json.IdProducto;
                    fur.Cantidad = Json.Cantidad;
                    fur.IdProductoPresentacion = Json.IdProductoPresentacion.Value;
                    fur.Descripcion = Json.Descripcion;
                    fur.FechaLimite = DateTime.Parse(Json.FechaLimite);
                    fur.PrecioUnitarioMonedaOrigen = Json.PrecioUnitarioMonedaOrigen;
                    fur.PrecioUnitarioDolares = Json.PrecioUnitarioDolares;
                    fur.IdMonedaProveedor = Json.IdMoneda_Proveedor;
                    fur.PrecioTotalMonedaOrigen = Json.PrecioUnitarioMonedaOrigen * Json.Cantidad;
                    fur.PrecioTotalDolares = Json.PrecioUnitarioDolares * Json.Cantidad;
                    //fur.PagoSoles = Json.PrecioUnitarioSoles * Json.Cantidad;
                    //fur.PagoDolares = Json.PrecioUnitarioDolares * Json.Cantidad;
                    fur.IdMonedaPagoReal = Json.IdMonedaPagoReal;
                    fur.UsuarioModificacion = Json.UsuarioModificacion;
                    fur.FechaModificacion = DateTime.Now;
                    fur.IdEmpresa = Json.IdEmpresa;
                    repFurRep.Update(fur);
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
        public ActionResult ObtenerFurPorValor([FromBody] Dictionary<string, string> Valor)
        {
            try
            {
                if (Valor != null && Valor.Count > 0)
                {
                    FurRepositorio repFurRep = new FurRepositorio();
                    var listaFur = repFurRep.ObtenerDatosFur(Valor["filtro"]);
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
        public ActionResult EliminacionAutomaticaFur()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required,
                                   new System.TimeSpan(0, 20, 0)) )
                {

                    integraDBContext _integraDBContext = new integraDBContext();
                    FurRepositorio repFurRep = new FurRepositorio(_integraDBContext);
                    var ListadoEliminar = repFurRep.ObtenerIdFurAprobadoPorEliminar();
                    var listadoActualizarEstado = repFurRep.ObtenerIdFurAprobadoNoEjecutado();
                    foreach (var listado in ListadoEliminar)
                    {
                        repFurRep.EliminarFur(listado, _integraDBContext);
                    }
                    foreach (var listado in listadoActualizarEstado)
                    {
                        repFurRep.ActualizarFurEstadoAprobadoNoEjecutado(listado, _integraDBContext);
                    }
                    scope.Complete();
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}