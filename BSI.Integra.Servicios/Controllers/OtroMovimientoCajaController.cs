using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Aplicacion.Maestros.Repositorio;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.IRepository;
using BSI.Integra.Servicios.Helpers;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/OtroMovimientoCaja")]
    public class OtroMovimientoCajaController : Controller
    {
        public OtroMovimientoCajaController()
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
        public ActionResult ObtenerListaTipoMovimientoCaja()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

               TipoMovimientoCajaRepositorio _repoTipoMovimientoCaja = new TipoMovimientoCajaRepositorio();
                var lista = _repoTipoMovimientoCaja.ObtenerListaTipoMovimientoCaja();
                return Json(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaSubTipoMovimientoCaja()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                SubTipoMovimientoCajaRepositorio _repoSubTipoMovimientoCaja = new SubTipoMovimientoCajaRepositorio();
                var lista = _repoSubTipoMovimientoCaja.ObtenerListaSubTipoMovimientoCaja();
                return Json(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaFormaPago()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                FormaPagoRepositorio _repoFormaPago = new FormaPagoRepositorio();
                var lista = _repoFormaPago.ObtenerListaFormaPago();
                return Json(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaCuentaCorriente()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                CuentaCorrienteRepositorio _repoCuentaCorriente = new CuentaCorrienteRepositorio();
                var lista = _repoCuentaCorriente.ObtenerCuentaCorrienteConEntidad();
                return Json(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaAlumnoAutocomplete(string NombreParcial)
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

                AlumnoRepositorio _repoAlumno = new AlumnoRepositorio();
                var lista = _repoAlumno.ObtenerTodoFiltroAutoComplete(NombreParcial);
                return Json(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaCentroCosto(string NombreParcial)
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
                CentroCostoRepositorio _repoCentroCosto = new CentroCostoRepositorio();
                var lista = _repoCentroCosto.ObtenerListaCentrosCostoPorNombre(NombreParcial);
                return Json(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerListaPlanContableAutoComplete(string NombreParcial)
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
                PlanContableRepositorio _repoPlanContable = new PlanContableRepositorio();
                var lista = _repoPlanContable.ObtenerPlanContableAutoComplete(NombreParcial);
                return Json(new { Result = "OK", Records = lista });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        #endregion

        #region CRUD
        [Route("[Action]")]
        [HttpGet]
        public ActionResult VisualizarOtroMovimientoCaja()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OtroMovimientoCajaRepositorio _repoOtroMovimientoCaja = new OtroMovimientoCajaRepositorio();
                var OtroMovimientoCajaes = _repoOtroMovimientoCaja.ObtenerListaOtroMovimientoCaja();
                return Json(new { Result = "OK", Records = OtroMovimientoCajaes });
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarOtroMovimientoCaja([FromBody] OtroMovimientoCajaDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OtroMovimientoCajaRepositorio _repoOtroMovimientoCaja = new OtroMovimientoCajaRepositorio();
                
                OtroMovimientoCajaBO NuevoOtroMovimientoCaja = new OtroMovimientoCajaBO();

                NuevoOtroMovimientoCaja.IdSubTipoMovimientoCaja = ObjetoDTO.IdSubTipoMovimientoCaja;
                NuevoOtroMovimientoCaja.Precio = ObjetoDTO.Precio;
                NuevoOtroMovimientoCaja.IdMoneda = ObjetoDTO.IdMoneda;
                NuevoOtroMovimientoCaja.FechaPago = ObjetoDTO.FechaPago;
                NuevoOtroMovimientoCaja.IdCentroCosto = ObjetoDTO.IdCentroCosto;
                NuevoOtroMovimientoCaja.IdPlanContable = ObjetoDTO.IdPlanContable;
                NuevoOtroMovimientoCaja.IdCuentaCorriente = ObjetoDTO.IdCuentaCorriente;
                NuevoOtroMovimientoCaja.Observaciones = ObjetoDTO.Observaciones;
                NuevoOtroMovimientoCaja.IdAlumno = ObjetoDTO.IdAlumno;
                NuevoOtroMovimientoCaja.IdFormaPago = ObjetoDTO.IdFormaPago;
                NuevoOtroMovimientoCaja.Estado = true;
                NuevoOtroMovimientoCaja.UsuarioCreacion = ObjetoDTO.Usuario;
                NuevoOtroMovimientoCaja.UsuarioModificacion = ObjetoDTO.Usuario;
                NuevoOtroMovimientoCaja.FechaCreacion = DateTime.Now;
                NuevoOtroMovimientoCaja.FechaModificacion = DateTime.Now;

                _repoOtroMovimientoCaja.Insert(NuevoOtroMovimientoCaja);

                var Resultados = _repoOtroMovimientoCaja.ObtenerOtroMovimientoCajaPorID(NuevoOtroMovimientoCaja.Id);
                if (Resultados == null || Resultados.Count < 1) 
                    throw new Exception("No se pudo recuperar el registro 'Id' inexistente");

                return Ok(Resultados[0]);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult ActualizarOtroMovimientoCaja([FromBody] OtroMovimientoCajaDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OtroMovimientoCajaRepositorio _repoOtroMovimientoCaja = new OtroMovimientoCajaRepositorio();

                var OtroMovimientoCaja = _repoOtroMovimientoCaja.GetBy(x=>x.Id==ObjetoDTO.Id).FirstOrDefault();

                OtroMovimientoCaja.Precio = ObjetoDTO.Precio;
                OtroMovimientoCaja.IdMoneda = ObjetoDTO.IdMoneda;
                OtroMovimientoCaja.FechaPago = ObjetoDTO.FechaPago;
                OtroMovimientoCaja.IdCentroCosto = ObjetoDTO.IdCentroCosto;
                OtroMovimientoCaja.IdPlanContable = ObjetoDTO.IdPlanContable;
                OtroMovimientoCaja.IdCuentaCorriente = ObjetoDTO.IdCuentaCorriente;
                OtroMovimientoCaja.Observaciones = ObjetoDTO.Observaciones;
                OtroMovimientoCaja.IdAlumno = ObjetoDTO.IdAlumno;
                OtroMovimientoCaja.IdFormaPago = ObjetoDTO.IdFormaPago;
                OtroMovimientoCaja.Estado = true;
                OtroMovimientoCaja.UsuarioModificacion = ObjetoDTO.Usuario;
                OtroMovimientoCaja.FechaModificacion = DateTime.Now;

                _repoOtroMovimientoCaja.Update(OtroMovimientoCaja);

                var Resultados = _repoOtroMovimientoCaja.ObtenerOtroMovimientoCajaPorID(OtroMovimientoCaja.Id);
                if (Resultados == null || Resultados.Count < 1)
                    throw new Exception("No se pudo recuperar el registro 'Id' inexistente");

                return Ok(Resultados[0]);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarOtroMovimientoCaja([FromBody] EliminarDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OtroMovimientoCajaRepositorio _repoOtroMovimientoCaja = new OtroMovimientoCajaRepositorio();
                
                _repoOtroMovimientoCaja.Delete(ObjetoDTO.Id, ObjetoDTO.NombreUsuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
#endregion

    
    }
}
