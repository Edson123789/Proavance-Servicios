using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/CajaNotaIngreso")]
    public class CajaNotaIngresoController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public CajaNotaIngresoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerNotaIngresoCaja(int? IdCaja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                NotaIngresoCajaRepositorio repCajaNIC = new NotaIngresoCajaRepositorio(_integraDBContext);

                var listado = repCajaNIC.ObtenerCajaNIC(IdCaja);
                if (listado != null)
                {
                }
                return Ok(listado);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCajaResponsable()
        {
            try
            {
                CajaRepositorio repCajaRep = new CajaRepositorio(_integraDBContext);
                return Ok(repCajaRep.ObtenerCajaResponsable());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerOrigenIngreso()
        {
            try
            {
                OrigenIngresoCajaRepositorio repOrigenIngresoRep = new OrigenIngresoCajaRepositorio(_integraDBContext);
                return Ok(repOrigenIngresoRep.ObtenerOrigen());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarNotaIngresoCaja([FromBody] NotaIngresoCajaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                NotaIngresoCajaRepositorio repNotaIngresoCajaRep = new NotaIngresoCajaRepositorio(_integraDBContext);
                NotaIngresoCajaBO cajaNic = new NotaIngresoCajaBO();
                //int correlativo = 0;
                //var countNics = repNotaIngresoCajaRep.GetBy(x => x.Estado == true && x.IdCaja==Json.IdCaja && x.Anho==DateTime.Now.Year).Count();
                int correlativo = 0;
                var listCodigos = repNotaIngresoCajaRep.GetBy(x => x.Estado == true && x.CodigoNic.Contains(Json.CodigoNic), x => new { x.CodigoNic }).ToList();
                if (listCodigos != null || listCodigos.Count() != 0)
                {
                    foreach (var Codigos in listCodigos)
                    {
                        if (Int32.Parse(Codigos.CodigoNic.Substring(Codigos.CodigoNic.LastIndexOf(".") + 1).Trim()) > correlativo)
                        {
                            correlativo = Int32.Parse(Codigos.CodigoNic.Substring(Codigos.CodigoNic.LastIndexOf(".") + 1).Trim());
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
                    cajaNic.IdCaja = Json.IdCaja;
                    //cajaNic.CodigoNic = Json.CodigoNic + (countNics+1);
                    cajaNic.CodigoNic = Json.CodigoNic + (correlativo);
                    cajaNic.IdOrigenIngresoCaja = Json.IdOrigenIngresoCaja;
                    cajaNic.IdPersonalEmitido = Json.IdPersonalEmitido;
                    cajaNic.Monto = Json.Monto;
                    cajaNic.FechaGiro= DateTime.Parse(Json.FechaGiro);
                    cajaNic.Concepto = Json.Concepto;
                    cajaNic.FechaCobro = DateTime.Parse(Json.FechaCobro);
                    cajaNic.Anho = DateTime.Now.Year;
                    cajaNic.UsuarioCreacion = Json.UsuarioModificacion;
                    cajaNic.FechaCreacion = DateTime.Now;
                    cajaNic.UsuarioModificacion = Json.UsuarioModificacion;
                    cajaNic.FechaModificacion = DateTime.Now;
                    cajaNic.Estado = true;

                    repNotaIngresoCajaRep.Insert(cajaNic);
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
        public ActionResult ActualizarNotaIngresoCaja([FromBody] NotaIngresoCajaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //integraDBContext contexto = new integraDBContext();
                NotaIngresoCajaRepositorio repNotaIngresoCajaRep = new NotaIngresoCajaRepositorio(_integraDBContext);
                NotaIngresoCajaBO cajaNic = new NotaIngresoCajaBO();
                cajaNic = repNotaIngresoCajaRep.FirstById(Json.Id);
                int correlativo = 0;
                var listCodigos = repNotaIngresoCajaRep.GetBy(x => x.Estado == true && x.CodigoNic.Contains(Json.CodigoNic), x => new { x.CodigoNic }).ToList();
                if (listCodigos != null || listCodigos.Count() != 0)
                {
                    foreach (var Codigos in listCodigos)
                    {
                        if (Int32.Parse(Codigos.CodigoNic.Substring(Codigos.CodigoNic.LastIndexOf(".") + 1).Trim()) > correlativo)
                        {
                            correlativo = Int32.Parse(Codigos.CodigoNic.Substring(Codigos.CodigoNic.LastIndexOf(".") + 1).Trim());
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
                    if (cajaNic.IdCaja != Json.IdCaja)
                    {
                        cajaNic.IdCaja = Json.IdCaja;
                        cajaNic.CodigoNic = Json.CodigoNic + (correlativo);
                    }
                    cajaNic.IdOrigenIngresoCaja = Json.IdOrigenIngresoCaja;
                    cajaNic.IdPersonalEmitido = Json.IdPersonalEmitido;
                    cajaNic.Monto = Json.Monto;
                    cajaNic.FechaGiro = DateTime.Parse(Json.FechaGiro);
                    cajaNic.Concepto = Json.Concepto;
                    cajaNic.FechaCobro = DateTime.Parse(Json.FechaCobro);
                    cajaNic.Anho = DateTime.Now.Year;
                    cajaNic.UsuarioModificacion = Json.UsuarioModificacion;
                    cajaNic.FechaModificacion = DateTime.Now;
                    cajaNic.Estado = true;

                    repNotaIngresoCajaRep.Update(cajaNic);
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
        public ActionResult EliminarNotaIngresoCaja([FromBody] EliminarDTO EliminarDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    NotaIngresoCajaRepositorio repNotaIngresoCajaRep = new NotaIngresoCajaRepositorio(_integraDBContext);
                    if (repNotaIngresoCajaRep.Exist((int)EliminarDTO.Id))
                    {
                        repNotaIngresoCajaRep.Delete((int)EliminarDTO.Id, (string)EliminarDTO.NombreUsuario);
                        scope.Complete();
                        return base.Ok(true);
                    }
                    else
                    {
                        return base.BadRequest("Registro no existente");
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