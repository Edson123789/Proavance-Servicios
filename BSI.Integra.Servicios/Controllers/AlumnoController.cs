using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using Mandrill.Models;
using Nancy.Json;
using BSI.Integra.Aplicacion.DTOs;
using System.IO;
using System.Net;
using BSI.Integra.Aplicacion.Base.BO;
using System.Linq;
using BSI.Integra.Aplicacion.Transversal.Scode.Helper;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using RestSharp;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Servicios.DTOs;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;

using RestSharp.Newtonsoft.Json.NetCore;
using BSI.Integra.Aplicacion.Transversal.Helper;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/Alumno")]
    public class AlumnoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly AlumnoRepositorio _repAlumno;
        private readonly PersonalRepositorio _repPersonal;

        private readonly OcurrenciaRepositorio _repOcurrencia;

        public AlumnoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repAlumno = new AlumnoRepositorio(_integraDBContext);
            _repPersonal = new PersonalRepositorio(_integraDBContext);
            _repOcurrencia = new OcurrenciaRepositorio(_integraDBContext);

        }
        /// Tipo Función: POST
        /// Autor: Lisbeth Ortogorin
        /// Fecha: 02/09/2021
        /// Versión: 1.0
        /// <summary>
        /// retorna el nombre completo del alumno y su id , por el valor del parametro
        /// </summary>
        /// <returns>Json/returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerAlumnoPorValor([FromBody] FiltroAutocompleteDTO Filtro)
        {
            if (!ModelState.IsValid)
            {
                return Ok();
            }
            try
            {
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
                var alumnosTemp = _repAlumno.ObtenerTodoFiltroAutoComplete(Filtro.Valor);
                foreach (var item in alumnosTemp)
                {
                    item.NombreCompleto = string.Concat(item.NombreCompleto, " (", item.Id, ") ");
                }
                return Ok(alumnosTemp);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public ActionResult ObtenerAlumnoChatPortalWebPorId(int id)
        {
            try
            {
                //AlumnoBO alumno = new AlumnoBO();
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
                var res = _repAlumno.ObtenerAlumnoChatPortalWebPorId(id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerAlumnoPorEmail(string Email1, string Email2)
        {
            try
            {
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
                return Ok(_repAlumno.ObtenerAlumnosPorEmail(Email1, Email2));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult CalcularNumeroOportunidadesPorContacto()
        {
            try
            {
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
                _repAlumno.CalcularNumeroOportunidadPorAlumno();
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{Email}/{NombreUsuario}")]
        [HttpGet]
        public ActionResult DesuscribirPorEmail(string Email, string NombreUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
                var listaAlumnosADesuscribir = _repAlumno.ObtenerSuscritosPorEmail(Email);
                foreach (var item in listaAlumnosADesuscribir)
                {
                    item.DeSuscrito = true;
                    item.UsuarioModificacion = NombreUsuario;
                    item.FechaModificacion = DateTime.Now;
                }
                _repAlumno.Update(listaAlumnosADesuscribir);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdAlumno}/{NombreUsuario}")]
        [HttpGet]
        public ActionResult Desuscribir(int IdAlumno, string NombreUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
                if (!_repAlumno.Exist(IdAlumno))
                {
                    return BadRequest("Alumno no existente!");
                }
                var alumno = _repAlumno.FirstById(IdAlumno);
                var listaAlumnosADesuscribir = _repAlumno.ObtenerSuscritosPorEmail(alumno.Email1);
                foreach (var item in listaAlumnosADesuscribir)
                {
                    item.DeSuscrito = true;
                    item.UsuarioModificacion = NombreUsuario;
                    item.FechaModificacion = DateTime.Now;
                }
                _repAlumno.Update(listaAlumnosADesuscribir);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]/{IdAlumno}/{NombreUsuario}")]
        [HttpGet]
        public ActionResult CalcularEstadoContactoWhatsApp(int IdAlumno, string NombreUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var listaAlumnosValidarEstadoContactoWhatsApp = new List<AlumnoBO>();
                if (IdAlumno == 0)
                {
                    listaAlumnosValidarEstadoContactoWhatsApp = _repAlumno.ObtenerContactosValidarEstadoContactoWhatsApp();
                }
                else
                {
                    if (!_repAlumno.Exist(IdAlumno))
                    {
                        return BadRequest("Alumno no existente!");
                    }
                    listaAlumnosValidarEstadoContactoWhatsApp.Add(_repAlumno.FirstById(IdAlumno));
                }

                foreach (var item in listaAlumnosValidarEstadoContactoWhatsApp)
                {
                    try
                    {
                        try
                        {
                            item.ValidarEstadoContactoWhatsApp();
                            //item.UsuarioModificacion = NombreUsuario;
                            //item.FechaModificacion = DateTime.Now;
                        }
                        catch (Exception e)
                        {
                            item.IdEstadoContactoWhatsApp = ValorEstatico.IdEstadoContactoWhatsAppSinValidar;
                        }
                        _repAlumno.Update(item);
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }
                //_repAlumno.Update(listaAlumnosValidarEstadoContactoWhatsApp);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[action]")]
        [HttpGet]
        public ActionResult CalcularEstadoContactoWhatsAppv3()
        {
            try
            {
                var listaAlumnosValidarEstadoContactoWhatsApp = _repAlumno.ObtenerContactosValidarEstadoContactoWhatsAppv2();
                foreach (var item in listaAlumnosValidarEstadoContactoWhatsApp)
                {
                    var url = "https://integrav4-servicios.bsginstitute.com/api/alumno/CalcularEstadoContactoWhatsApp/" + item.Valor + "/wchoque";
                    WebClient webClient = new WebClient();
                    var dta = webClient.DownloadString(url);

                }
                return Ok(true);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [Route("[action]/{IdInicial}/{Cantidad}/{EjecutarUnaVez}")]
        [HttpGet]
        public ActionResult CalcularEstadoContactoWhatsAppv2(int IdInicial, int Cantidad, bool EjecutarUnaVez)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var maxValue = IdInicial + 100000;
                var listaAlumnosValidarEstadoContactoWhatsApp = new List<AlumnoBO>();


                if (EjecutarUnaVez)
                {
                    while (maxValue >= IdInicial)
                    {
                        listaAlumnosValidarEstadoContactoWhatsApp = _repAlumno.GetBy(x => x.Estado == true && x.IdEstadoContactoWhatsApp == ValorEstatico.IdEstadoContactoWhatsAppSinValidar, IdInicial, Cantidad).ToList();

                        foreach (var item in listaAlumnosValidarEstadoContactoWhatsApp)
                        {
                            try
                            {
                                try
                                {
                                    item.ValidarEstadoContactoWhatsApp();
                                    //item.UsuarioModificacion = NombreUsuario;
                                    //item.FechaModificacion = DateTime.Now;
                                }
                                catch (Exception e)
                                {
                                    item.IdEstadoContactoWhatsApp = ValorEstatico.IdEstadoContactoWhatsAppSinValidar;
                                }
                                _repAlumno.Update(item);
                            }
                            catch (Exception e)
                            {
                                continue;
                            }
                        }
                        IdInicial += Cantidad;
                    }

                }
                else
                {
                    listaAlumnosValidarEstadoContactoWhatsApp = _repAlumno.GetBy(x => x.Estado == true && x.IdEstadoContactoWhatsApp == ValorEstatico.IdEstadoContactoWhatsAppSinValidar, IdInicial, Cantidad).ToList();
                    foreach (var item in listaAlumnosValidarEstadoContactoWhatsApp)
                    {
                        try
                        {
                            try
                            {
                                item.ValidarEstadoContactoWhatsApp();
                                //item.UsuarioModificacion = NombreUsuario;
                                //item.FechaModificacion = DateTime.Now;
                            }
                            catch (Exception e)
                            {
                                item.IdEstadoContactoWhatsApp = ValorEstatico.IdEstadoContactoWhatsAppSinValidar;
                            }
                            _repAlumno.Update(item);
                        }
                        catch (Exception e)
                        {
                            continue;
                        }
                    }
                }

                //_repAlumno.Update(listaAlumnosValidarEstadoContactoWhatsApp);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdOportunidad}/{IdPlantilla}")]
        [HttpGet]
        public ActionResult EnvioSmsOportunidadPlantilla (int IdOportunidad, int IdPlantilla)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int seccionMensaje = 1;
                string mensajeFinal = string.Empty;
                List<string> listaSubMensajeFinal = new List<string>();

                /*Buscar configuracion para el envio de SMS individual*/
                var _repSmsConfiguracionEnvio = new SmsConfiguracionEnvioRepositorio();
                var configuracionEstablecida = _repSmsConfiguracionEnvio.ConfiguracionSmsOportunidad(IdOportunidad);

                if (configuracionEstablecida != null)
                {
                    string urlBase = $"http://{configuracionEstablecida.Servidor}:80/sendsms?username=smsuser&password=smspwd&phonenumber=";

                    #region Cambio de etiqueta
                    var reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                    {
                        IdOportunidad = IdOportunidad,
                        IdPlantilla = IdPlantilla
                    };

                    reemplazoEtiquetaPlantilla.ReemplazarEtiquetasNuevasOportunidades();
                    #endregion

                    string[] palabras = reemplazoEtiquetaPlantilla.SmsReemplazado.Cuerpo.Split(' ');

                    foreach (string palabra in palabras)
                    {
                        if ((mensajeFinal + " " + palabra).Length <= 128)
                            mensajeFinal += " " + palabra;
                        else
                        {
                            listaSubMensajeFinal.Add(mensajeFinal.Trim());
                            mensajeFinal = palabra;
                        }
                    }

                    listaSubMensajeFinal.Add(mensajeFinal.Trim());
                    mensajeFinal = string.Empty;

                    foreach (string mensajeAEnviar in listaSubMensajeFinal)
                    {
                        string url = $"{urlBase}{configuracionEstablecida.Celular}&message={mensajeAEnviar.Replace(" ", "%20")}&[port={configuracionEstablecida.Tipo}-{configuracionEstablecida.Puerto}&][report=String&][timeout=5&][id=1]";

                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            wc.DownloadString(url);
                        }

                        _repAlumno.InsertaSMSOportunidadUsuario(configuracionEstablecida.Celular, configuracionEstablecida.IdPersonal, configuracionEstablecida.IdAlumno, mensajeAEnviar, seccionMensaje, configuracionEstablecida.IdCodigoPais.GetValueOrDefault(), "EnvioAutomatico");

                        seccionMensaje++;
                    }

                    var insertado = _repAlumno.InsertaSMSOportunidad(IdOportunidad, DateTime.Now);
                }

                return Ok(true);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin
        /// Fecha: 20/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Hace el envio de sms a los contactos por reprogramacion automatica por dia y ocurrencia
        /// </summary>
        /// 
        /// <returns>Json/returns>
        [Route("[Action]/{IdOportunidad}/{IdOcurrencia}")]
        [HttpGet]
        public ActionResult EnviarIndividualSMSPorOcurrencia(int IdOportunidad, int IdOcurrencia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int seccionMensaje = 1;
                string mensajeFinal = string.Empty;
                List<string> listaSubMensajeFinal = new List<string>();
                int idPlantilla = 0;

                var _repSmsConfiguracionEnvio = new SmsConfiguracionEnvioRepositorio();
                var configuracionEstablecida = _repSmsConfiguracionEnvio.ConfiguracionSmsOportunidad(IdOportunidad);

                if (configuracionEstablecida == null) return Ok(false);

                string urlBase = $"http://{configuracionEstablecida.Servidor}:80/sendsms?username=smsuser&password=smspwd&phonenumber=";

                // Validacion de mensaje si ya se le envio un sms el dia de hoy a esa oportunidad
                var envio = _repAlumno.Obtener_EnvioSMSPorDiaOportunidad(IdOportunidad, DateTime.Now);

                var ocurrencia = _repOcurrencia.FirstBy(x => x.Id == IdOcurrencia);

                if (envio == null && ocurrencia.IdEstadoOcurrencia == 2)
                {
                    var diasSinContacto = _repSmsConfiguracionEnvio.ObtenerDiasSinContacto(IdOportunidad);

                    /*Definicion de plantilla*/
                    switch (diasSinContacto.DiasSinContacto)
                    {
                        case 1:
                            idPlantilla = ValorEstatico.IdRecordatorioSms02;
                            break;
                        case 2:
                            idPlantilla = ValorEstatico.IdRecordatorioSms03;
                            break;
                        case 3:
                            idPlantilla = ValorEstatico.IdRecordatorioSms04;
                            break;
                        default:
                            idPlantilla = 0;
                            break;
                    }
                }

                if (idPlantilla > 0)
                {
                    #region Cambio de etiqueta
                    var reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                    {
                        IdOportunidad = IdOportunidad,
                        IdPlantilla = idPlantilla
                    };

                    reemplazoEtiquetaPlantilla.ReemplazarEtiquetasNuevasOportunidades();
                    #endregion

                    string[] palabras = reemplazoEtiquetaPlantilla.SmsReemplazado.Cuerpo.Split(' ');

                    foreach (string palabra in palabras)
                    {
                        if ((mensajeFinal + " " + palabra).Length <= 128)
                            mensajeFinal += " " + palabra;
                        else {
                            listaSubMensajeFinal.Add(mensajeFinal.Trim());
                            mensajeFinal = palabra;
                        }
                    }

                    listaSubMensajeFinal.Add(mensajeFinal.Trim());
                    mensajeFinal = string.Empty;

                    foreach (string mensajeAEnviar in listaSubMensajeFinal)
                    {
                        string url = $"{urlBase}{configuracionEstablecida.Celular}&message={mensajeAEnviar.Replace(" ", "%20")}&[port={configuracionEstablecida.Tipo}-{configuracionEstablecida.Puerto}&][report=String&][timeout=5&][id=1]";

                        using (WebClient wc = new WebClient())
                        {
                            wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            wc.DownloadString(url);
                        }

                        _repAlumno.InsertaSMSOportunidadUsuario(configuracionEstablecida.Celular, configuracionEstablecida.IdPersonal, configuracionEstablecida.IdAlumno, mensajeAEnviar, seccionMensaje, configuracionEstablecida.IdCodigoPais.GetValueOrDefault(), "EnvioOcurrencia");

                        seccionMensaje++;
                    }
                        
                    var insertado = _repAlumno.InsertaSMSOportunidad(IdOportunidad, DateTime.Now);
                }

                return Ok(true);
            }
            catch (Exception)
            {
                return Ok(false);
            }
        }

        /// Tipo Función: GET
        /// Autor: Carlos Crispin
        /// Fecha: 20/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Hace el envio de sms a los contactos por reprogramacion automatica por dia
        /// </summary>
        /// <returns>Json/returns>
        [Route("[action]/{IdOportunidad}/{IdOcurrencia}/{IdAlumno}/{IdPais}/{IdAsesor}/{Mensaje}")]
        [HttpGet]
        public ActionResult EnviarSMS(int IdOportunidad, int IdOcurrencia, int IdAlumno, int IdPais, int IdAsesor, string Mensaje)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // Valido si ya se le envio un sms el dia de hoy a esa oportunidad
                var envio = _repAlumno.Obtener_EnvioSMSPorDiaOportunidad(IdOportunidad, DateTime.Now);
                var ocurrencia = _repOcurrencia.FirstById(IdOcurrencia);
                if (envio == null && ocurrencia.IdEstadoOcurrencia == 2)
                {
                    string url = String.Empty;
                    var alumno = _repAlumno.FirstById(IdAlumno);

                    string MensajeMnn = "BSG INSTITUTE | Solicitud de información | Hola, soy Milagros Landeo de BSG Institute. Me estoy intentado comunicar contigo para brindarte asesoría personalizada sobre el programa de capacitación del que solicitaste información. Te pido por favor que puedas registrar mi numero ya que me intentare comunicar contigo en las próximas horas.También me puedes indicar por este medio la hora en la que te puedo llamar.";
                    string MensajeTarde = "BSG INSTITUTE | Solicitud de información |Hola, soy Milagros Landeo de BSG Institute.Me estoy intentado comunicar contigo para brindarte asesoría personalizada sobre el programa de capacitación del que solicitaste información.Te pido por favor que puedas registrar mi numero ya que me intentare comunicar contigo en las próximas horas o el día de mañana.También me puedes indicar por este medio la hora en la que te puedo llamar.";

                    if (DateTime.Now.Hour < 12)
                    {
                        Mensaje = MensajeMnn;
                    }
                    else
                    {
                        Mensaje = MensajeTarde;
                    }

                    if (alumno.Celular == null || string.IsNullOrEmpty(alumno.Celular))
                    {
                        throw new Exception("El numero del celular es nullo o vacio");
                    }
                    switch (IdPais)
                    {
                        case 51:// Peru
                            if (alumno.Celular.Length != 9)
                            {
                                throw new Exception("El numero de Peru no tiene los digitos para el pais");
                            }
                            url = "http://192.168.3.26:80/sendsms?username=smsuser&password=smspwd&phonenumber=" + alumno.Celular + "&message=" + Mensaje + "&[port=gsm-1.1&][report=String&][timeout=5&][id=1]";
                            break;
                        case 57:// Colombia
                            if (alumno.Celular.Length == 14)
                            {
                                alumno.Celular = alumno.Celular.Replace("0057", "", StringComparison.OrdinalIgnoreCase);
                            }
                            if (alumno.Celular.Length != 10)
                            {
                                throw new Exception("El numero de Colombia no tiene los digitos para el pais");
                            }
                            url = "http://192.168.6.28:80/sendsms?username=smsuser&password=smspwd&phonenumber=" + alumno.Celular + "&message=" + Mensaje + "&[port=gsm-4.16&][report=String&][timeout=5&][id=1]";
                            break;
                        case 591:// Bolivia
                            if (alumno.Celular.Length == 13)
                            {
                                alumno.Celular = alumno.Celular.Replace("00591", "", StringComparison.OrdinalIgnoreCase);
                            }
                            if (alumno.Celular.Length != 8)
                            {
                                throw new Exception("El numero de Bolivia no tiene los digitos para el pais");
                            }
                            url = "http://192.168.7.26:80/sendsms?username=smsuser&password=smspwd&phonenumber=" + alumno.Celular + "&message=" + Mensaje + "&[port=gsm-3.4&][report=String&][timeout=5&][id=1]";
                            break;
                        case 52:// Mexico
                            if (alumno.Celular.Length == 14)
                            {
                                alumno.Celular = alumno.Celular.Replace("0052", "", StringComparison.OrdinalIgnoreCase);
                            }
                            if (alumno.Celular.Length != 10)
                            {
                                throw new Exception("El numero de Mexico no tiene los digitos para el pais");
                            }
                            url = "http://192.168.8.30:80/sendsms?username=smsuser&password=smspwd&phonenumber=" + alumno.Celular + "&message=" + Mensaje + "&[port=lte-1.1&][report=String&][timeout=5&][id=1]";
                            break;
                    }



                    using (WebClient wc = new WebClient())
                    {
                        wc.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                        wc.DownloadString(url);
                    }

                    var insertado = _repAlumno.InsertaSMSOportunidad(IdOportunidad, DateTime.Now);


                    return Ok(true);
                }
                else
                {
                    return BadRequest("Ya se envio un sms para el cliente , solo se puede enviar 1 por dia");
                }




            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
