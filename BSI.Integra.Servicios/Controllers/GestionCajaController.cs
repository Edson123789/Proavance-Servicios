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
    [Route("api/GestionCaja")]
    public class GestionCajaController : Controller
    {
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCajaFinanzas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CajaRepositorio repCajaRep = new CajaRepositorio();
                var Data = repCajaRep.ObtenerCajasFinanzas();
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
        public ActionResult ObtenerEmpresasAutorizadas()
        {
            try
            {
                EmpresaAutorizadaRepositorio repEmpresaRep = new EmpresaAutorizadaRepositorio();                
                return Ok(repEmpresaRep.GetBy(x => x.Estado == true && x.Activo == true, x => new { x.Id, Empresa = x.RazonSocial, x.Direccion, x.Ruc, x.Central }).ToList());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarCajaFinanzas([FromBody] EliminarDTO CajaFinanzasDTO)
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
                    CajaRepositorio repCajaRep = new CajaRepositorio(_integraDBContext);
                    if (repCajaRep.Exist(CajaFinanzasDTO.Id))
                    {
                        repCajaRep.Delete(CajaFinanzasDTO.Id, CajaFinanzasDTO.NombreUsuario);
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
        public ActionResult InsertarCajaFinanzas([FromBody] CajaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CajaRepositorio repCajaRep = new CajaRepositorio();
                CajaBO cajaFinanzas = new CajaBO();

                using (TransactionScope scope = new TransactionScope())
                {

                    cajaFinanzas.CodigoCaja = Json.CodigoCaja;
                    cajaFinanzas.IdMoneda = Json.IdMoneda;
                    cajaFinanzas.IdEmpresaAutorizada = Json.IdEmpresa;
                    cajaFinanzas.IdEntidadFinanciera = Json.IdBanco;
                    cajaFinanzas.IdCuentaCorriente = Json.IdCuenta;
                    cajaFinanzas.IdCiudad = Json.IdCiudad;
                    cajaFinanzas.IdPersonalResponsable = Json.IdPersonal;
                    cajaFinanzas.Activo = Json.Activo;
                    cajaFinanzas.Estado = true;
                    cajaFinanzas.UsuarioCreacion = Json.UsuarioModificacion;
                    cajaFinanzas.FechaCreacion = DateTime.Now;
                    cajaFinanzas.UsuarioModificacion = Json.UsuarioModificacion;
                    cajaFinanzas.FechaModificacion = DateTime.Now;

                    repCajaRep.Insert(cajaFinanzas);
                    scope.Complete();
                }
                string rpta = "INSERTADO CORRECTAMENTE";
                return Ok(new { rpta});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult ActualizarCajaFinanzas([FromBody] CajaDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CajaRepositorio repCajaRep = new CajaRepositorio();
                CajaBO cajaFinanzas = new CajaBO();
                cajaFinanzas = repCajaRep.FirstById(Json.Id);

                using (TransactionScope scope = new TransactionScope())
                {
                    cajaFinanzas.CodigoCaja = Json.CodigoCaja;
                    cajaFinanzas.IdMoneda = Json.IdMoneda;
                    cajaFinanzas.IdEmpresaAutorizada = Json.IdEmpresa;
                    cajaFinanzas.IdEntidadFinanciera = Json.IdBanco;
                    cajaFinanzas.IdCuentaCorriente = Json.IdCuenta;
                    cajaFinanzas.IdCiudad = Json.IdCiudad;
                    cajaFinanzas.IdPersonalResponsable = Json.IdPersonal;
                    cajaFinanzas.Activo = Json.Activo;
                    cajaFinanzas.UsuarioModificacion = Json.UsuarioModificacion;
                    cajaFinanzas.FechaModificacion = DateTime.Now;
                    repCajaRep.Update(cajaFinanzas);
                    scope.Complete();
                }
                string rpta = "ACTUALIZADO CORRECTAMENTE";
                return Ok(new { rpta });

            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

    }
}