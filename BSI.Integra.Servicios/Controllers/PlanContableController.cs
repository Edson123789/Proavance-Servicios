using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/PlanContable")]
    public class PlanContableController : Controller
    {
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCuentasContables()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlanContableRepositorio _repPlanContable = new PlanContableRepositorio();
                var Data = _repPlanContable.ObtenerPlanContable();
                var Total = Data.Count();
                return Ok(new { Data, Total });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCuentasAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                PlanContableRepositorio repPlnaContableRep = new PlanContableRepositorio();
                if (Filtros != null && Filtros["Valor"] != null)
                {
                    return Ok(repPlnaContableRep.GetBy(x => x.Cuenta.ToString().Contains(Filtros["Valor"].ToString()) || x.Descripcion.Contains(Filtros["Valor"].ToString()), x => new { Cuenta = x.Cuenta, Nombre = x.Cuenta+" - "+x.Descripcion.ToUpper() }).ToList());
                }
                return Ok(repPlnaContableRep.GetBy(x => x.Estado, x => new { Cuenta = x.Cuenta, Nombre = x.Cuenta + " - " + x.Descripcion.ToUpper() }).ToList());
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCuentasContableAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                PlanContableRepositorio repPlnaContableRep = new PlanContableRepositorio();
                if (Filtros != null && Filtros["Valor"] != null)
                {
                    return Ok(repPlnaContableRep.GetBy(x => x.Cuenta.ToString().Contains(Filtros["Valor"].ToString()) || x.Descripcion.Contains(Filtros["Valor"].ToString()), x => new { Id = x.Id, Nombre = x.Cuenta + " - " + x.Descripcion.ToUpper() }).ToList());
                }
                return Ok(repPlnaContableRep.GetBy(x => x.Estado, x => new { Id = x.Id, Nombre = x.Cuenta + " - " + x.Descripcion.ToUpper() }).ToList());
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerTipoCuentasContables()
        {
            try
            {
                PlanContableTipoCuentaRepositorio _repTipoCuentaRep = new PlanContableTipoCuentaRepositorio();
                var y = _repTipoCuentaRep.GetBy(x => x.Estado == true, x => new { x.Id, Nombre = x.Nombre.ToUpper()}).ToList();
                return Ok(y);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCuentaContablePadre()
        {
            try
            {
                PlanContableRepositorio _repPlanContableRep = new PlanContableRepositorio();
                return Ok(_repPlanContableRep.GetBy(x => x.Estado == true && x.IdPlanContableTipoCuenta !=null, x=>new {x.Cuenta}).ToList().OrderBy(x=>x.Cuenta));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("[action]/{Cuenta}")]
        [HttpGet]
        public ActionResult ObtenerCuentasHijoPlanContable(long Cuenta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlanContableRepositorio _repPlanContableRep = new PlanContableRepositorio();
                return Ok(_repPlanContableRep.ObteneCuentasHijo(Cuenta));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarCuentaContable([FromBody] PlanContableDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PlanContableRepositorio _repPlanContableRep = new PlanContableRepositorio();
                PlanContableBO cuentaContable = new PlanContableBO();
                bool escuentahijo=_repPlanContableRep.ObteneCuentasHijo(Json.Cuenta).Count()==0?true:false;

                using (TransactionScope scope = new TransactionScope())
                {
                    cuentaContable.Cuenta = Json.Cuenta;
                    cuentaContable.Descripcion = Json.Descripcion;
                    cuentaContable.Padre = Json.Padre;
                    cuentaContable.Cbal = Json.Cbal;
                    cuentaContable.Univel = escuentahijo;
                    cuentaContable.Debe = Json.Debe;
                    cuentaContable.Haber = Json.Haber;
                    cuentaContable.IdPlanContableTipoCuenta = Json.IdTipoCuenta;
                    cuentaContable.Analisis = Json.Analisis;
                    cuentaContable.CentroCosto = Json.CentroCosto;
                    cuentaContable.Estado =true;
                    cuentaContable.UsuarioModificacion = Json.UsuarioModificacion;
                    cuentaContable.UsuarioCreacion = Json.UsuarioModificacion;
                    cuentaContable.FechaCreacion = DateTime.Now;
                    cuentaContable.FechaModificacion = DateTime.Now;

                    _repPlanContableRep.Insert(cuentaContable);
                    scope.Complete();
                }
                Json.Estado = cuentaContable.Estado;
                Json.Id = cuentaContable.Id;

                if (Json.Padre != 0 ) {
                    int idPadre = _repPlanContableRep.FirstBy(w=>w.Cuenta==Json.Padre).Id;
                    cuentaContable = _repPlanContableRep.FirstById(idPadre);
                    escuentahijo = _repPlanContableRep.ObteneCuentasHijo(Json.Padre).Count() == 0 ? true : false;
                    using (TransactionScope scope = new TransactionScope()){
                        cuentaContable.Univel = escuentahijo;
                        _repPlanContableRep.Update(cuentaContable);
                        scope.Complete();
                    }
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
        public IActionResult ActualizarCuentaContable([FromBody] PlanContableDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //integraDBContext contexto = new integraDBContext();
                PlanContableRepositorio _repPlanContableRep = new PlanContableRepositorio();
                PlanContableBO cuentaContable = new PlanContableBO();
                cuentaContable = _repPlanContableRep.FirstById(Json.Id);
                var escuentahijo = _repPlanContableRep.ObteneCuentasHijo(Json.Cuenta).Count() == 0 ? true : false;
                int padreAnterior = cuentaContable.Padre;
                using (TransactionScope scope = new TransactionScope())
                {
                    cuentaContable.Cuenta = Json.Cuenta;
                    cuentaContable.Descripcion = Json.Descripcion;
                    cuentaContable.Padre = Json.Padre;
                    cuentaContable.Cbal = Json.Cbal;
                    cuentaContable.Univel = escuentahijo;
                    cuentaContable.Debe = Json.Debe;
                    cuentaContable.Haber = Json.Haber;
                    cuentaContable.IdPlanContableTipoCuenta = Json.IdTipoCuenta;
                    cuentaContable.Analisis = Json.Analisis;
                    cuentaContable.CentroCosto = Json.CentroCosto;
                    cuentaContable.Estado = Json.Estado;
                    cuentaContable.UsuarioModificacion = Json.UsuarioModificacion;
                    cuentaContable.FechaModificacion = DateTime.Now;
                    Json.FechaModificacion = cuentaContable.FechaModificacion;
                    _repPlanContableRep.Update(cuentaContable);
                    scope.Complete();
                }

                if (padreAnterior != cuentaContable.Padre && cuentaContable.Padre!=0) {
                    int idPadre = _repPlanContableRep.FirstBy(w => w.Cuenta == Json.Padre).Id;
                    cuentaContable = _repPlanContableRep.FirstById(idPadre);
                    escuentahijo = _repPlanContableRep.ObteneCuentasHijo(Json.Padre).Count() == 0 ? true : false;
                    using (TransactionScope scope = new TransactionScope())
                    {
                        cuentaContable.Univel = escuentahijo;
                        _repPlanContableRep.Update(cuentaContable);
                        scope.Complete();
                    }
                    int idPadreAnterior = _repPlanContableRep.FirstBy(w => w.Cuenta == padreAnterior).Id;
                    cuentaContable = _repPlanContableRep.FirstById(idPadreAnterior);
                    escuentahijo = _repPlanContableRep.ObteneCuentasHijo(padreAnterior).Count() == 0 ? true : false;
                    using (TransactionScope scope = new TransactionScope())
                    {
                        cuentaContable.Univel = escuentahijo;
                        _repPlanContableRep.Update(cuentaContable);
                        scope.Complete();
                    }

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
        public ActionResult EliminarCuentaContable([FromBody] EliminarDTO CuentaContableDTO)
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
                    PlanContableRepositorio _repPlanContableRep = new PlanContableRepositorio(_integraDBContext);
                    PlanContableBO cuentaContable = new PlanContableBO();
                    if (_repPlanContableRep.Exist(CuentaContableDTO.Id))
                    {
                        long cuentaPadre = _repPlanContableRep.FirstBy(w => w.Id == CuentaContableDTO.Id).Padre;
                        _repPlanContableRep.Delete(CuentaContableDTO.Id, CuentaContableDTO.NombreUsuario);
                        
                        if (cuentaPadre != 0)
                        {
                            int idPadre = _repPlanContableRep.FirstBy(w => w.Cuenta == cuentaPadre).Id;
                            cuentaContable = _repPlanContableRep.FirstById(idPadre);
                            var escuentahijo = _repPlanContableRep.ObteneCuentasHijo(cuentaPadre).Count() == 0 ? true : false;
                            cuentaContable.Univel = escuentahijo;
                            _repPlanContableRep.Update(cuentaContable);                              
                        }

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