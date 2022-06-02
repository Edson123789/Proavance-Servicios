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
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/ComprobantePagoAlumno")]
    public class ComprobantePagoAlumnoController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public ComprobantePagoAlumnoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        /// Tipo Función: GET
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 02/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Retorna los datos de comprobante pago por alumno
        /// </summary>
        /// <returns>Tipo de objeto que retorna la función</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerReporteComprobante([FromBody] filtroReporteComprobanteDTO Comprobante)
        {
            ComprobantePagoOportunidadRepositorio _repComprobantePago = new ComprobantePagoOportunidadRepositorio();

            var comprobante = _repComprobantePago.ObtenerReporteComprobanteAlumno(Comprobante);

            return Ok(comprobante);
        }
        /// Tipo Función: GET
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 12/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la data para los combos de ComprobantePagoReporte
        /// </summary>
        /// <returns>Tipo de objeto que retorna la función</returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerCombosReporte()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PeriodoRepositorio _repPeriodo = new PeriodoRepositorio(_integraDBContext);
                FormaPagoRepositorio _repFormaPago = new FormaPagoRepositorio(_integraDBContext);
                CombosComprobanteDTO comboComprobante = new CombosComprobanteDTO();
                comboComprobante.ListaPeriodo = _repPeriodo.ObtenerPeriodosPendiente();
                comboComprobante.ListaFormaPago = _repFormaPago.ObtenerListaFormaPago();
                return Ok(comboComprobante);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Lisbeth Ortogorin Condori
        /// Fecha: 02/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta o actualiza los comprobantes
        /// </summary>
        /// <returns>Tipo de objeto que retorna la función</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult InsertarEditar([FromBody] ComprobantePagoOportunidadDTO comprobante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var _alumno = new AlumnoRepositorio();
                var alumno = _alumno.ObtenerDatosAlumnoPorId(comprobante.IdContacto);
                var _repComprobantePagoOportunidad = new ComprobantePagoOportunidadRepositorio(_integraDBContext);
                var comprobantePago = new ComprobantePagoOportunidadBO();
                var existeComrobantePagoOportundiad = _repComprobantePagoOportunidad.FirstBy(x => x.IdContacto == comprobante.IdContacto);
                if (existeComrobantePagoOportundiad != null){
                    using (TransactionScope scope = new TransactionScope())
                    {

                        existeComrobantePagoOportundiad.IdContacto = comprobante.IdContacto;
                        existeComrobantePagoOportundiad.Nombres = existeComrobantePagoOportundiad.Nombres;
                        existeComrobantePagoOportundiad.Apellidos = existeComrobantePagoOportundiad.Apellidos;
                        existeComrobantePagoOportundiad.Celular = existeComrobantePagoOportundiad.Celular;
                        existeComrobantePagoOportundiad.Dni = existeComrobantePagoOportundiad.Dni;
                        existeComrobantePagoOportundiad.Correo = existeComrobantePagoOportundiad.Correo;
                        existeComrobantePagoOportundiad.NombrePais = existeComrobantePagoOportundiad.NombrePais;
                        existeComrobantePagoOportundiad.IdPais = existeComrobantePagoOportundiad.IdPais;
                        existeComrobantePagoOportundiad.NombreCiudad = existeComrobantePagoOportundiad.NombreCiudad;
                        existeComrobantePagoOportundiad.TipoComprobante = comprobante.TipoComprobante;
                        existeComrobantePagoOportundiad.NroDocumento = comprobante.NroDocumento != null ? comprobante.NroDocumento : "";
                        existeComrobantePagoOportundiad.NombreRazonSocial = comprobante.NombreRazonSocial != null ? comprobante.NombreRazonSocial : "";
                        existeComrobantePagoOportundiad.Direccion = existeComrobantePagoOportundiad.Direccion;
                        if (comprobante.TipoComprobante == "Boleta") { existeComrobantePagoOportundiad.BitComprobante = 0; }
                        else { existeComrobantePagoOportundiad.BitComprobante = 1; }
                        existeComrobantePagoOportundiad.IdOcurrencia = existeComrobantePagoOportundiad.IdOcurrencia;
                        existeComrobantePagoOportundiad.IdAsesor = existeComrobantePagoOportundiad.IdAsesor;
                        existeComrobantePagoOportundiad.IdOportunidad = existeComrobantePagoOportundiad.IdOportunidad;
                        existeComrobantePagoOportundiad.Comentario = comprobante.Comentario;
                        existeComrobantePagoOportundiad.FechaCreacion = existeComrobantePagoOportundiad.FechaCreacion;
                        existeComrobantePagoOportundiad.FechaModificacion = DateTime.Now;
                        existeComrobantePagoOportundiad.UsuarioCreacion = existeComrobantePagoOportundiad.UsuarioCreacion;
                        existeComrobantePagoOportundiad.UsuarioModificacion = comprobante.UsuarioModificacion;
                        existeComrobantePagoOportundiad.Estado = true;

                        _repComprobantePagoOportunidad.Update(existeComrobantePagoOportundiad);
                        scope.Complete();
                    };
                }
                else
                {
                    using (TransactionScope scope = new TransactionScope())
                    {

                        comprobantePago.IdContacto = comprobante.IdContacto;
                        comprobantePago.Nombres = alumno.Nombre1 + " " + alumno.Nombre2;
                        comprobantePago.Apellidos = alumno.ApellidoPaterno + " " + alumno.ApellidoMaterno;
                        comprobantePago.Celular = alumno.Celular;
                        comprobantePago.Dni = alumno.Dni;
                        comprobantePago.Correo = alumno.Email1;
                        comprobantePago.NombrePais = alumno.NombrePais != null ? alumno.NombrePais : "";
                        comprobantePago.IdPais = alumno.IdCodigoPais;
                        comprobantePago.NombreCiudad = alumno.NombreCiudad != null ? alumno.NombreCiudad : "";
                        comprobantePago.TipoComprobante = comprobante.TipoComprobante;
                        comprobantePago.NroDocumento = comprobante.NroDocumento != null ? comprobante.NroDocumento : "";
                        comprobantePago.NombreRazonSocial = comprobante.NombreRazonSocial != null ? comprobante.NombreRazonSocial : "";
                        comprobantePago.Direccion = alumno.Direccion;
                        if (comprobante.TipoComprobante == "Boleta") { comprobantePago.BitComprobante = 0; }
                        else { comprobantePago.BitComprobante = 1; }
                        comprobantePago.IdOcurrencia = comprobante.IdOcurrencia;
                        comprobantePago.IdAsesor = comprobante.IdAsesor;
                        comprobantePago.IdOportunidad = comprobante.IdOportunidad;
                        comprobantePago.Comentario = comprobante.Comentario;
                        comprobantePago.FechaCreacion = DateTime.Now;
                        comprobantePago.FechaModificacion = DateTime.Now;
                        comprobantePago.UsuarioCreacion = comprobante.UsuarioCreacion;
                        comprobantePago.UsuarioModificacion = comprobante.UsuarioModificacion;
                        comprobantePago.Estado = true;

                        _repComprobantePagoOportunidad.Insert(comprobantePago);
                        scope.Complete();
                    };
                }
                          
                return Ok(comprobantePago);
            }            
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

    }
}