using BSI.Integra.Aplicacion.DTOs.Marketing;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: DocumentoLegalController
    /// Autor: Jashin Salazar
    /// Fecha: 12/1/2021
    /// <summary>
    /// Controlador que contiene los endpoints para la interfaz Ejecucion de estado de WhatsApp
    /// </summary>
    [Route("api/EjecucionEstadoWhatsApp")]
    public class EjecucionEstadoWhatsAppController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly EjecucionEstadoWhatsAppRepositorio _repEjecucionEstadoWhatsApp;
        private readonly EjecucionEstadoWhatsAppLogRepositorio _repEjecucionEstadoWhatsAppLog;


        public EjecucionEstadoWhatsAppController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            _repEjecucionEstadoWhatsApp = new EjecucionEstadoWhatsAppRepositorio(_integraDBContext);
            _repEjecucionEstadoWhatsAppLog = new EjecucionEstadoWhatsAppLogRepositorio(_integraDBContext);
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 22/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos para los combos.
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerCombos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var ListaPeriodo = _repEjecucionEstadoWhatsApp.ObtenerListaPeriodo();
                return Ok(ListaPeriodo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 22/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos para la grilla de la interfaz.
        /// </summary>
        /// <returns>List<FiltroDTO></returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ObtenerTodoEjecucionEstadoWhatsApp()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var ListaPeriodo = _repEjecucionEstadoWhatsApp.ObtenerTodoEjecucionEstadoWhatsApp();
                return Ok(ListaPeriodo);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jashin Salazar
        /// Fecha: 12/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Ejecuta el calculo de estado de WhatsApp
        /// </summary>
        [HttpGet]
        [Route("[action]")]
        public ActionResult EjecutarEstadoWhatsApp()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var configuracion = _repEjecucionEstadoWhatsApp.ObtenerConfiguracionActual();
                var fechaActual = DateTime.Today;
                if (configuracion.FechaProximaEjecucion == fechaActual)
                {
                    EjecucionEstadoWhatsAppLogBO configuracionEjecucionLog = new EjecucionEstadoWhatsAppLogBO();
                    configuracionEjecucionLog = _repEjecucionEstadoWhatsAppLog.GetBy(x => x.IdEjecucionEstadoWhatsApp == configuracion.Id).FirstOrDefault();
                    configuracionEjecucionLog.FechaEjecucion = DateTime.Today;
                    configuracionEjecucionLog.UsuarioModificacion = "WhatsApp Masivo";
                    configuracionEjecucionLog.FechaModificacion = DateTime.Now;
                    if (!configuracionEjecucionLog.HasErrors)
                        _repEjecucionEstadoWhatsAppLog.Update(configuracionEjecucionLog);
                    else
                        return BadRequest(configuracionEjecucionLog.GetErrors(null));
                    return Ok(true);
                }
                else
                {
                    return Ok(false);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 12/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Insersion de configuracion para ejecucion de estado de whatsapp
        /// </summary>
        /// <returns> true o false </returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult InsertarEjecucionEstadoWhatsApp([FromBody] EjecucionEstadoWhatsAppDTO configuracion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var validacion = _repEjecucionEstadoWhatsApp.VerificarFecha(configuracion.FechaInicio);
                var validacionFinal = _repEjecucionEstadoWhatsApp.VerificarFecha(configuracion.FechaFin);
                if (validacion == null && validacionFinal == null)
                {
                    EjecucionEstadoWhatsAppBO configuracionEjecucion = new EjecucionEstadoWhatsAppBO();
                    configuracionEjecucion.FechaInicio = configuracion.FechaInicio;
                    configuracionEjecucion.FechaFin = configuracion.FechaFin;
                    configuracionEjecucion.CantidadTiempoFrecuencia = configuracion.CantidadTiempoFrecuencia;
                    configuracionEjecucion.IdTiempoFrecuencia = configuracion.IdTiempoFrecuencia;
                    configuracionEjecucion.Estado = true;
                    configuracionEjecucion.UsuarioCreacion = configuracion.Usuario;
                    configuracionEjecucion.UsuarioModificacion = configuracion.Usuario;
                    configuracionEjecucion.FechaCreacion = DateTime.Now;
                    configuracionEjecucion.FechaModificacion = DateTime.Now;
                    if (!configuracionEjecucion.HasErrors)
                        _repEjecucionEstadoWhatsApp.Insert(configuracionEjecucion);
                    else
                        return BadRequest(configuracionEjecucion.GetErrors(null));

                    EjecucionEstadoWhatsAppLogBO configuracionEjecucionLog = new EjecucionEstadoWhatsAppLogBO();
                    configuracionEjecucionLog.IdEjecucionEstadoWhatsApp = configuracionEjecucion.Id;
                    configuracionEjecucionLog.FechaEjecucion = DateTime.Today;
                    configuracionEjecucionLog.Estado = true;
                    configuracionEjecucionLog.UsuarioCreacion = configuracion.Usuario;
                    configuracionEjecucionLog.UsuarioModificacion = configuracion.Usuario;
                    configuracionEjecucionLog.FechaCreacion = DateTime.Now;
                    configuracionEjecucionLog.FechaModificacion = DateTime.Now;
                    if (!configuracionEjecucionLog.HasErrors)
                        _repEjecucionEstadoWhatsAppLog.Insert(configuracionEjecucionLog);
                    else
                        return BadRequest(configuracionEjecucionLog.GetErrors(null));

                    return Ok(true);
                }
                else
                {
                    return BadRequest("La fecha ingresada se encuentra en el rango de otra fecha");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 12/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Modificacion de configuracion para ejecucion de estado de whatsapp
        /// </summary>
        /// <returns> true o false </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ActualizarEjecucionEstadoWhatsApp([FromBody] EjecucionEstadoWhatsAppDTO configuracion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var validacion = _repEjecucionEstadoWhatsApp.VerificarFechaModificacion(configuracion.FechaInicio, configuracion.Id);
                var validacionFinal = _repEjecucionEstadoWhatsApp.VerificarFechaModificacion(configuracion.FechaFin, configuracion.Id);
                if (validacion == null && validacionFinal == null)
                {
                    EjecucionEstadoWhatsAppBO configuracionEjecucion = new EjecucionEstadoWhatsAppBO();
                    configuracionEjecucion = _repEjecucionEstadoWhatsApp.FirstById(configuracion.Id);
                    configuracionEjecucion.FechaInicio = configuracion.FechaInicio;
                    configuracionEjecucion.FechaFin = configuracion.FechaFin;
                    configuracionEjecucion.CantidadTiempoFrecuencia = configuracion.CantidadTiempoFrecuencia;
                    configuracionEjecucion.IdTiempoFrecuencia = configuracion.IdTiempoFrecuencia;
                    configuracionEjecucion.UsuarioModificacion = configuracion.Usuario;
                    configuracionEjecucion.FechaModificacion = DateTime.Now;
                    if (!configuracionEjecucion.HasErrors)
                        _repEjecucionEstadoWhatsApp.Update(configuracionEjecucion);
                    else
                        return BadRequest(configuracionEjecucion.GetErrors(null));

                    EjecucionEstadoWhatsAppLogBO configuracionEjecucionLog = new EjecucionEstadoWhatsAppLogBO();
                    configuracionEjecucionLog = _repEjecucionEstadoWhatsAppLog.GetBy(x => x.IdEjecucionEstadoWhatsApp == configuracion.Id).FirstOrDefault();
                    configuracionEjecucionLog.FechaEjecucion = DateTime.Today;
                    configuracionEjecucionLog.UsuarioModificacion = configuracion.Usuario;
                    configuracionEjecucionLog.FechaModificacion = DateTime.Now;
                    if (!configuracionEjecucionLog.HasErrors)
                        _repEjecucionEstadoWhatsAppLog.Update(configuracionEjecucionLog);
                    else
                        return BadRequest(configuracionEjecucionLog.GetErrors(null));
                    return Ok(true);
                }
                else
                {
                    return BadRequest("La fecha ingresada se encuentra en el rango de otra fecha");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// Tipo Función: POST
        /// Autor: Jashin Salazar
        /// Fecha: 12/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Eliminacion de configuracion para ejecucion de estado de whatsapp
        /// </summary>
        /// <returns> true o false </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarEjecucionEstadoWhatsApp([FromBody] EjecucionEstadoWhatsAppDTO configuracion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (_repEjecucionEstadoWhatsApp.Exist(configuracion.Id))
                {
                    _repEjecucionEstadoWhatsApp.Delete(configuracion.Id, configuracion.Usuario);
                    var configuracionEjecucionLog = _repEjecucionEstadoWhatsAppLog.GetBy(x => x.IdEjecucionEstadoWhatsApp == configuracion.Id).FirstOrDefault();
                    if (configuracionEjecucionLog != null)
                    {
                        _repEjecucionEstadoWhatsAppLog.Delete(configuracionEjecucionLog.Id, configuracion.Usuario);
                    }
                    return Ok(true);
                }
                else
                {
                    return BadRequest("La configuracion a eliminar no existe o ya fue eliminada.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
