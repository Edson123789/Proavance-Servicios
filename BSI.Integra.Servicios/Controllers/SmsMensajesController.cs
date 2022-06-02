using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Persistencia.Models;
using FluentValidation;
using BSI.Integra.Aplicacion.Marketing.BO;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Persistencia.SCode.IRepository;
using Microsoft.Extensions.Logging;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/SmsMensajes")]
    public class SmsMensajesController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;

        private readonly SmsMensajeRecibidoRepositorio _repSmsMensajeRecibido;
        private readonly LogRepositorio _repLog;

        public SmsMensajesController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;

            _repSmsMensajeRecibido = new SmsMensajeRecibidoRepositorio(_integraDBContext);
            _repLog = new LogRepositorio(_integraDBContext);
        }

        /// <summary>
        /// Inserta el mensaje en la base de datos correspondiente
        /// </summary>
        /// <param name="PhoneNumber">Numero del telefono del mensaje entrante</param>
        /// <param name="Port">Puerto en donde ingresa el mensaje</param>
        /// <param name="PortName">Nombre o alias del puerto donde ingresa el mensaje</param>
        /// <param name="Message">Mensaje entrante</param>
        /// <param name="Time">Fecha de recepcion del mensaje</param>
        /// <param name="Imsi">Imsi del mensaje</param>
        /// <param name="Status">Estado del mensaje recibido</param>
        /// <returns>Response 200 con booleano true, caso contrario response 400 con booleano true</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult RecibirMensaje(string PhoneNumber, string Port, string PortName, string Message, string Time, string Imsi, string Status)
        {
            try
            {
                SmsMensajeRecibidoBO SmsMensajeRecibido = new SmsMensajeRecibidoBO(_integraDBContext)
                {
                    NumeroTelefono = PhoneNumber,
                    Puerto = Port,
                    NombrePuerto = PortName,
                    Mensaje = Message,
                    Imsi = Imsi,
                    EstadoMensaje = Status,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = "SystemSms",
                    UsuarioModificacion = "SystemSms",
                    Estado = true
                };

                if (!string.IsNullOrEmpty(Time) && DateTime.TryParse(Time, out DateTime fechaRecepcionConversion))
                    SmsMensajeRecibido.FechaRecepcion = fechaRecepcionConversion;

                _repSmsMensajeRecibido.Insert(SmsMensajeRecibido);

                return Ok(true);
            }
            catch (Exception ex)
            {
                _repLog.Insert(new TLog { Ip = "-", Usuario = "-", Maquina = "-", Ruta = "RecibirMensaje", Parametros = $"PhoneNumber={PhoneNumber}, Port={Port}, PortName={PortName}, Time={Time}", Mensaje = $"{ex.Message}-{(ex.InnerException != null ? ex.InnerException.Message : "No contiene InnerException")}", Excepcion = $"{ex}", Tipo = "RECEIVE-SMS", IdPadre = 0, UsuarioCreacion = string.Empty, UsuarioModificacion = string.Empty, FechaCreacion = DateTime.Now, FechaModificacion = DateTime.Now, Estado = true });

                return Ok(true);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 31/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Ejecuta las actividades automaticas de SMS Masivos
        /// </summary>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult EjecutarActividadesAutomaticasSmsMasivo([FromBody] List<ActividadParaEjecutarDTO> ListaActividadesAutomaticasSmsMasivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Gian Miranda
        /// Fecha: 31/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los ultimos mensajes recibidos por el chat
        /// </summary>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
        [Route("[action]/{IdPersonal}/{IdAlumno}")]
        [HttpGet]
        public ActionResult SmsUltimoMensajeRecibidoChat(int IdPersonal, int IdAlumno)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SmsMensajeEnviadoRepositorio _repSmsMensajeEnviado = new SmsMensajeEnviadoRepositorio(_integraDBContext);

                var resultado = _repSmsMensajeEnviado.ListaUltimoMensajeSmsRecibido(IdPersonal, IdAlumno);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// Tipo Función: POST
        /// Autor: Gian Miranda
        /// Fecha: 31/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Ejecuta las actividades automaticas de SMS Masivos
        /// </summary>
        /// <returns>Booleano con respuesta 200 o 400 con el mensaje de error</returns>
        [Route("[Action]/{Celular}")]
        [HttpGet]
        public ActionResult ObtenerHistorialSmsPorCelular(string Celular)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SmsMensajeEnviadoRepositorio _repSmsMensajeEnviado = new SmsMensajeEnviadoRepositorio(_integraDBContext);
                
                var resultado = _repSmsMensajeEnviado.ObtenerHistorialSmsPorCelular(Celular);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
