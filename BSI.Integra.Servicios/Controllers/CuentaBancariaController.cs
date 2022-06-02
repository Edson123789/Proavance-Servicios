using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    [Route("api/CuentaBancaria")]
    public class CuentaBancariaController : Controller
    {
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCuentasBancarias()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CuentaCorrienteRepositorio _repCuentaCorriente = new CuentaCorrienteRepositorio();
                var Data = _repCuentaCorriente.ObtenerCuentasBancarias();
                var Total = Data.Count();
                return Ok(new { Data, Total });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCuentasCorriente()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CuentaCorrienteRepositorio _repCuentaCorriente = new CuentaCorrienteRepositorio();
                return Ok(_repCuentaCorriente.GetBy(x => x.Estado == true, x => new { x.Id, Cuenta=x.NumeroCuenta, x.IdBanco,x.IdMoneda }).ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerEntidadFinanciera()
        {
            try
            {
                EntidadFinancieraRepositorio _repEntidadFinancieraRep = new EntidadFinancieraRepositorio();
                return Ok(_repEntidadFinancieraRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, NombreBanco = x.Nombre.ToUpper() }).ToList());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerMoneda()
        {
            try
            {
                MonedaRepositorio _repMonedaRep = new MonedaRepositorio();
                return Ok(_repMonedaRep.GetBy(x => x.Estado == true, x => new { Id = x.Id, NombreMoneda = x.NombrePlural.ToUpper() }).ToList());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCiudadAutocomplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                CiudadRepositorio _repCiudadRep=new CiudadRepositorio();
                IntegraAspNetUsersRepositorio _repRegistrosIntegra = new IntegraAspNetUsersRepositorio();
                if (Filtros != null && Filtros["Valor"] != null)
                {
                    var y = _repCiudadRep.ObtenerCiudadFiltroAutocomplete(Filtros["Valor"].ToString());
                    return Ok(y);
                }
                return Ok(new { });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult InsertarCuentaBancaria([FromBody] CuentaBancariaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CuentaCorrienteRepositorio _repCuentaCorrienteRep = new CuentaCorrienteRepositorio();
                CuentaCorrienteBO cuenta = new CuentaCorrienteBO();
                string[] numeroCuentaArray = Json.NumeroCuenta.Split("-");

                using (TransactionScope scope = new TransactionScope())
                {
                    cuenta.NumeroCuenta = Json.NumeroCuenta;
                    cuenta.IdCiudad = Json.IdCiudad;
                    cuenta.Sucursal = numeroCuentaArray[0];
                    cuenta.IdMoneda = Json.IdMoneda;
                    cuenta.Cuenta = numeroCuentaArray[1];
                    cuenta.IdBanco = Json.IdBanco;
                    cuenta.Estado = true;
                    cuenta.UsuarioCreacion = Json.UsuarioModificacion;
                    cuenta.FechaCreacion = DateTime.Now;
                    cuenta.UsuarioModificacion = Json.UsuarioModificacion;
                    cuenta.FechaModificacion = DateTime.Now;
                    Json.FechaModificacion = cuenta.FechaModificacion;

                    _repCuentaCorrienteRep.Insert(cuenta);
                    scope.Complete();
                }
                Json.EstadoCuenta = cuenta.Estado;
                Json.Id = cuenta.Id;

                return Ok(Json);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarCuentaBancaria([FromBody] CuentaBancariaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //integraDBContext contexto = new integraDBContext();
                CuentaCorrienteRepositorio _repCuentaCorrienteRep = new CuentaCorrienteRepositorio();
                CuentaCorrienteBO cuenta = new CuentaCorrienteBO();
                cuenta = _repCuentaCorrienteRep.FirstById(Json.Id);
                string[] numeroCuentaArray = Json.NumeroCuenta.Split("-");
                using (TransactionScope scope = new TransactionScope())
                {
                    cuenta.NumeroCuenta = Json.NumeroCuenta;
                    cuenta.IdCiudad = Json.IdCiudad;
                    cuenta.Sucursal = numeroCuentaArray[0];
                    cuenta.IdMoneda = Json.IdMoneda;
                    cuenta.Cuenta = numeroCuentaArray[1];
                    cuenta.IdBanco = Json.IdBanco;
                    cuenta.Estado = Json.EstadoCuenta;
                    cuenta.UsuarioModificacion = Json.UsuarioModificacion;
                    cuenta.FechaModificacion = DateTime.Now;
                    Json.FechaModificacion = cuenta.FechaModificacion;
                    _repCuentaCorrienteRep.Update(cuenta);
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
        public ActionResult EliminarCuentaBancaria([FromBody] EliminarDTO CuentaBancariaDTO)
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
                    CuentaCorrienteRepositorio _repCuentaCorrienteRep = new CuentaCorrienteRepositorio(_integraDBContext);
                    if (_repCuentaCorrienteRep.Exist(CuentaBancariaDTO.Id))
                    {
                        _repCuentaCorrienteRep.Delete(CuentaBancariaDTO.Id, CuentaBancariaDTO.NombreUsuario);
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