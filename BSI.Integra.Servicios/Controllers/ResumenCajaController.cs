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
    [Route("api/ResumenCaja")]
    public class ResumenCajaController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public ResumenCajaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerEmpresaAutorizada()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                EmpresaAutorizadaRepositorio repEmpresaRep = new EmpresaAutorizadaRepositorio();
                return Ok(repEmpresaRep.ObtenerEmpresas());
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCuentaCorriente()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CuentaCorrienteRepositorio repCuentaRep = new CuentaCorrienteRepositorio();
                return Ok(repCuentaRep.ObtenerCuentaCorrienteIdNombre());
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerResumenCaja()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CajaRepositorio repCajaRep = new CajaRepositorio();
                return Ok(repCajaRep.ObtenerResumenCaja());
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerNotaIngresoPorFecha(DateTime? FechaInicial, DateTime? FechaFinal , int IdCaja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _filtro = new FiltroLandingPagePortalDTO()
                {
                    FechaInicial = FechaInicial,
                    FechaFinal = FechaFinal
                };
                NotaIngresoCajaRepositorio _repNotaIngreso = new NotaIngresoCajaRepositorio();
                var listado = _repNotaIngreso.ObtenerCajaIngresoByFecha(FechaInicial, FechaFinal, IdCaja);
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

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerDocumentosNIC([FromBody] ListaEnterosDTO listaEnteros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                NotaIngresoCajaRepositorio repCajaIngresoRep = new NotaIngresoCajaRepositorio();
                NotaIngresoCajaBO cajaIngreso = new NotaIngresoCajaBO();
                List<byte[]> listaPDFbytes=new List<byte[]>();
                string listaEnterosString = String.Join(",", listaEnteros.ListaEnteros.Select(i => i.ToString()).ToArray());
                var listaIngresoCajaDTO=repCajaIngresoRep.ObtenerDatosCajaIngreso(listaEnteros.ListaEnteros.ToArray());

                foreach (var datosIngresoCaja in listaIngresoCajaDTO) {
                    var pdf= cajaIngreso.GenerarPDFNotaIngresoCaja(datosIngresoCaja);
                    listaPDFbytes.Add(pdf);
                }

                //listaPDFbytes.Add(3);
                //NotaIngresoCajaDTO datosCajaIngreso=
                
                return Ok(listaPDFbytes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerPorRendirPorFecha(DateTime? FechaInicial, DateTime? FechaFinal, int IdCaja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CajaPorRendirCabeceraRepositorio _repPorRendirCabeceraRep = new CajaPorRendirCabeceraRepositorio();
                var listadoPorRendir= _repPorRendirCabeceraRep.ObtenerCajaPorRendirByFecha(FechaInicial, FechaFinal, IdCaja);
                if (listadoPorRendir != null)
                {
                }
                return Ok(listadoPorRendir);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerDocumentosCajaPorRendir([FromBody] ListaEnterosDTO listaEnteros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CajaPorRendirCabeceraRepositorio repPorRendirCabRep = new CajaPorRendirCabeceraRepositorio();
                CajaPorRendirCabeceraBO cajaPorRendir = new CajaPorRendirCabeceraBO();
                List<byte[]> listaPDFbytes = new List<byte[]>();
                var listaCajaPorRendirPdf = repPorRendirCabRep.ObtenerDatosCajaPorRendir(listaEnteros.ListaEnteros);
                foreach (var datosCajaPorRendir in listaCajaPorRendirPdf)
                {
                    var pdf = cajaPorRendir.GenerarPDFReciboCajaPorRendir(datosCajaPorRendir);
                    listaPDFbytes.Add(pdf);
                }

                return Ok(listaPDFbytes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerReciboEgresoPorFecha(DateTime? FechaInicial, DateTime? FechaFinal, int IdCaja)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CajaEgresoAprobadoRepositorio _repEgresoAprobadoRep = new CajaEgresoAprobadoRepositorio();
                var listaEgreso=_repEgresoAprobadoRep.ObtenerCajaEgresoAprobadoByFecha(FechaInicial, FechaFinal, IdCaja);

                if (listaEgreso != null)
                {
                }
                return Ok(listaEgreso);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerDocumentosEgresoCaja([FromBody] ListaEnterosDTO listaEnteros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CajaEgresoAprobadoRepositorio repCajaEgresoRep = new CajaEgresoAprobadoRepositorio();
                CajaEgresoAprobadoBO cajaEgreso = new CajaEgresoAprobadoBO();
                List<byte[]> listaPDFbytes = new List<byte[]>();

                var listaCajaEgresoDatosPdf = repCajaEgresoRep.ObtenerDatosCajaEgreso(listaEnteros.ListaEnteros);
                foreach (var datosEgresoCaja in listaCajaEgresoDatosPdf)
                {
                    var pdf = cajaEgreso.GenerarPDFReciboEgresoCaja(datosEgresoCaja);
                    listaPDFbytes.Add(pdf);
                }

                return Ok(listaPDFbytes);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarComentariosCajaEgresoAprobado([FromBody] ComentariosCajaEgresoAprobadoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CajaEgresoAprobadoRepositorio repCajaEgresoAprobadoRep = new CajaEgresoAprobadoRepositorio();

                var cajaRecAprobado = repCajaEgresoAprobadoRep.FirstById(Json.Id);

                using (TransactionScope scope = new TransactionScope())
                {
                    cajaRecAprobado.Observacion = Json.Observacion;
                    cajaRecAprobado.Detalle = Json.Detalle;
                    cajaRecAprobado.Origen = Json.Origen;                    
                    cajaRecAprobado.UsuarioModificacion = Json.UsuarioModificacion;
                    cajaRecAprobado.FechaModificacion = DateTime.Now;

                    repCajaEgresoAprobadoRep.Update(cajaRecAprobado);

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
        public ActionResult ActualizarComentariosCajaPorRendirCabecera([FromBody] ComentariosCajaPorRendirDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                CajaPorRendirCabeceraRepositorio repCajaPorRendirCabReo = new CajaPorRendirCabeceraRepositorio();
                var cajaPRCabecera = repCajaPorRendirCabReo.FirstById(Json.Id);
                
                using (TransactionScope scope = new TransactionScope())
                {
                    cajaPRCabecera.Observacion = Json.Observacion;
                    cajaPRCabecera.Descripcion = Json.Detalle;
                    cajaPRCabecera.MontoDevolucion = Json.MontoDevolucion;
                    cajaPRCabecera.UsuarioModificacion = Json.UsuarioModificacion;
                    cajaPRCabecera.FechaModificacion = DateTime.Now;

                    repCajaPorRendirCabReo.Update(cajaPRCabecera);

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