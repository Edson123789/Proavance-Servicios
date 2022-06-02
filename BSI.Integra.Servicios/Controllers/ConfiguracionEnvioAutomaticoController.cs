using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Servicios.DTOs;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Newtonsoft.Json.NetCore;
using Method = RestSharp.Method;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfiguracionEnvioAutomaticoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly EstadoMatriculaRepositorio _repEstadoMatricula;
        private readonly MatriculaCabeceraRepositorio _repMatriculaCabecera;
        private readonly PlantillaRepositorio _repPlantilla;
        private readonly TipoEnvioAutomaticoRepositorio _repTipoEnvioAutomatico;
        private readonly ConfiguracionEnvioAutomaticoRepositorio _repConfiguracionEnvioAutomatico;
        private readonly ConfiguracionEnvioAutomaticoDetalleRepositorio _repConfiguracionEnvioAutomaticoDetalle;
        private readonly SolicitudOperacionesRepositorio _repSolicitudOperaciones;
        private readonly WhatsAppUsuarioCredencialRepositorio _repTokenUsuario;
        private readonly WhatsAppConfiguracionRepositorio _repCredenciales;
        private readonly PersonalRepositorio _repPersonal;
        private readonly AlumnoRepositorio _repAlumno;
        private readonly OportunidadRepositorio _repOportunidad;
        private readonly PlantillaBaseRepositorio _repPlantillaBase;
        private ReemplazoEtiquetaPlantillaBO _reemplazoEtiquetaPlantilla;
        public ConfiguracionEnvioAutomaticoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repEstadoMatricula = new EstadoMatriculaRepositorio(_integraDBContext);
            _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
            _repPlantilla = new PlantillaRepositorio(_integraDBContext);
            _repTipoEnvioAutomatico = new TipoEnvioAutomaticoRepositorio(_integraDBContext);
            _repConfiguracionEnvioAutomatico = new ConfiguracionEnvioAutomaticoRepositorio(_integraDBContext);
            _repConfiguracionEnvioAutomaticoDetalle = new ConfiguracionEnvioAutomaticoDetalleRepositorio(_integraDBContext);
            _repSolicitudOperaciones = new SolicitudOperacionesRepositorio(_integraDBContext);
            _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);
            _repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
            _repPersonal = new PersonalRepositorio(_integraDBContext);
            _repAlumno = new AlumnoRepositorio(_integraDBContext);
            _repOportunidad = new OportunidadRepositorio(_integraDBContext);
            _repPlantillaBase = new PlantillaBaseRepositorio(_integraDBContext);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerCombos()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }try
            {
                var detalles = new
                {

                    filtroEstadoMatricula = _repEstadoMatricula.ObtenerTodoFiltro(),
                    filtroSubEstadoMatricula = _repMatriculaCabecera.ObtenerSubEstadoMatricula(),
                    filtroPlantillas = _repPlantilla.ObtenerListaPlantillasConfiguracionEnvio(),
                    filtroTipoEnvioAutomatico = _repTipoEnvioAutomatico.ObtenerTodoFiltro()
                };
                
                return Ok(detalles);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ObtenerConfiguracionEnvioAutomatico()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {                
                var lista = _repConfiguracionEnvioAutomatico.ObtenerConfiguracionEnvioAutomatico();


                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerConfiguracionAsociada([FromBody] int IdConfiguracionEnvioAutomatico)
        {
            try
            {               

                var listaConfiguracion = _repConfiguracionEnvioAutomaticoDetalle.ObtenerConfiguracionEnvioAutomaticoDetalle(IdConfiguracionEnvioAutomatico);

                return Ok(listaConfiguracion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody] ConfiguracionEnvioAutomaticoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {                
                var lista = _repConfiguracionEnvioAutomatico.ActualizarConfiguracion(Json);
                var resetConfiguracionDetalle = _repConfiguracionEnvioAutomaticoDetalle.GetBy(w=>w.IdConfiguracionEnvioAutomatico==Json.Id);

                if (resetConfiguracionDetalle.Count() > 0)
                {
                    foreach(var tmp in resetConfiguracionDetalle)
                    {
                        var configuracionDetalleEstado = _repConfiguracionEnvioAutomaticoDetalle.FirstById(tmp.Id);
                        configuracionDetalleEstado.Estado = false;
                        _repConfiguracionEnvioAutomaticoDetalle.Update(configuracionDetalleEstado);
                    }
                }

                foreach (var item in Json.ListaConfiguracionEnvioAutomatico) 
                {
                    ConfiguracionEnvioAutomaticoDetalleBO configuracionDetalle = new ConfiguracionEnvioAutomaticoDetalleBO();

                    item.IdConfiguracionEnvioAutomatico = lista.FirstOrDefault().Id;
                    configuracionDetalle.IdConfiguracionEnvioAutomatico = item.IdConfiguracionEnvioAutomatico;
                    configuracionDetalle.IdTipoEnvioAutomatico = item.IdTipoEnvioAutomatico;
                    configuracionDetalle.IdTiempoFrecuencia = item.IdTiempoFrecuencia;
                    configuracionDetalle.IdPlantilla = item.IdPlantilla;
                    configuracionDetalle.Valor = item.Valor;
                    if (configuracionDetalle.IdTipoEnvioAutomatico == 1)
                    {
                        configuracionDetalle.EnvioWhatsApp = true;
                        configuracionDetalle.EnvioCorreo = false;
                        configuracionDetalle.EnvioMensajeTexto = false;
                    }
                    else if (configuracionDetalle.IdTipoEnvioAutomatico == 2)
                    {
                        configuracionDetalle.EnvioWhatsApp = false;
                        configuracionDetalle.EnvioCorreo = false;
                        configuracionDetalle.EnvioMensajeTexto = true;
                    }
                    else if (configuracionDetalle.IdTipoEnvioAutomatico == 3)
                    {
                        configuracionDetalle.EnvioWhatsApp = false;
                        configuracionDetalle.EnvioCorreo = true;
                        configuracionDetalle.EnvioMensajeTexto = false;
                    }
                    configuracionDetalle.HoraEnvioAutomatico = item.HoraEnvioAutomatico;
                    configuracionDetalle.Estado = true;
                    configuracionDetalle.UsuarioCreacion = Json.Usuario;
                    configuracionDetalle.UsuarioModificacion = Json.Usuario;
                    configuracionDetalle.FechaCreacion = DateTime.Now;
                    configuracionDetalle.FechaModificacion = DateTime.Now;
                    _repConfiguracionEnvioAutomaticoDetalle.Insert(configuracionDetalle);
                }
                return Ok(Json);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody] ConfiguracionEnvioAutomaticoDTO Json)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var lista = _repConfiguracionEnvioAutomatico.InsertarConfiguracion(Json);

                foreach (var item in Json.ListaConfiguracionEnvioAutomatico)
                {
                    ConfiguracionEnvioAutomaticoDetalleBO configuracionDetalle = new ConfiguracionEnvioAutomaticoDetalleBO();

                    item.IdConfiguracionEnvioAutomatico = lista.FirstOrDefault().Id;
                    configuracionDetalle.IdConfiguracionEnvioAutomatico = item.IdConfiguracionEnvioAutomatico;
                    configuracionDetalle.IdTipoEnvioAutomatico = item.IdTipoEnvioAutomatico;
                    configuracionDetalle.IdTiempoFrecuencia = item.IdTiempoFrecuencia;
                    configuracionDetalle.IdPlantilla = item.IdPlantilla;
                    configuracionDetalle.Valor = item.Valor;
                    if (configuracionDetalle.IdTipoEnvioAutomatico == 1)
                    {
                        configuracionDetalle.EnvioWhatsApp = true;
                        configuracionDetalle.EnvioCorreo = false;
                        configuracionDetalle.EnvioMensajeTexto = false;
                    }
                    else if (configuracionDetalle.IdTipoEnvioAutomatico == 2)
                    {
                        configuracionDetalle.EnvioWhatsApp = false;
                        configuracionDetalle.EnvioCorreo = false;
                        configuracionDetalle.EnvioMensajeTexto = true;
                    }
                    else if (configuracionDetalle.IdTipoEnvioAutomatico == 3)
                    {
                        configuracionDetalle.EnvioWhatsApp = false;
                        configuracionDetalle.EnvioCorreo = true;
                        configuracionDetalle.EnvioMensajeTexto = false;
                    }
                    configuracionDetalle.HoraEnvioAutomatico = item.HoraEnvioAutomatico;
                    configuracionDetalle.Estado = true;
                    configuracionDetalle.UsuarioCreacion = Json.Usuario;
                    configuracionDetalle.UsuarioModificacion = Json.Usuario;
                    configuracionDetalle.FechaCreacion = DateTime.Now;
                    configuracionDetalle.FechaModificacion = DateTime.Now;
                    _repConfiguracionEnvioAutomaticoDetalle.Insert(configuracionDetalle);



                }
                return Ok(Json);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("[action]/{IdConfiguracion}/{Usuario}")]
        [HttpPost]
        public IActionResult Eliminar(int IdConfiguracion, string Usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {                
                var lista = _repConfiguracionEnvioAutomatico.EliminarConfiguracion(IdConfiguracion, Usuario);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult EjecutarEnvioAutomaticoOperaciones([FromBody] DatosEnvioAutomaticoOperacionesDTO DatosEnvioAutomatico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (DatosEnvioAutomatico == null)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var Fechaactual = DatosEnvioAutomatico.FechaEnvio;
                var ListaCambioEstadoSubEstado = _repSolicitudOperaciones.ObtenerDatosEnvioAutomaticoOperaciones();
                var ConfiguracionEnvioAutomatico = _repConfiguracionEnvioAutomatico.ObtenerConfiguracionEnvioAutomaticoOperaciones();
                foreach (var alumno in ListaCambioEstadoSubEstado)
                {                    
                    foreach (var conf in ConfiguracionEnvioAutomatico)
                    {
                        var ValorEstadoAnteriorConf = _repEstadoMatricula.ObtenerEstadoMatriculaEnvioAutomatico(conf.IdEstadoInicial);
                        var ValorEstadoNuevoConf = _repEstadoMatricula.ObtenerEstadoMatriculaEnvioAutomatico(conf.IdEstadoDestino);
                        var ValorSubEstadoAnteriorConf = _repEstadoMatricula.ObtenerSubEstadoMatriculaEnvioAutomatico(conf.IdSubEstadoInicial);
                        var ValorSubEstadoNuevoConf = _repEstadoMatricula.ObtenerSubEstadoMatriculaEnvioAutomatico(conf.IdSubEstadoDestino);
                        
                        //Si es cambio de estado y subestado
                        if ((alumno.ValorAnteriorEstado == (ValorEstadoAnteriorConf == null ? "" : ValorEstadoAnteriorConf.Nombre) || conf.IdEstadoInicial == -1) && (alumno.ValorNuevoEstado == (ValorEstadoNuevoConf == null ? "" : ValorEstadoNuevoConf.Nombre) || conf.IdEstadoDestino == -1) && (alumno.ValorAnteriorSubEstado == (ValorSubEstadoAnteriorConf == null ? "" : ValorSubEstadoAnteriorConf.Nombre) || conf.IdSubEstadoInicial == -1) && (alumno.ValorNuevoSubEstado == (ValorSubEstadoNuevoConf == null ? "" : ValorSubEstadoNuevoConf.Nombre) || conf.IdSubEstadoDestino == -1))
                        {
                            if (conf.IdTipoEnvioAutomatico==1)//WhatsApp
                            {                    
                                if (conf.IdTiempoFrecuencia == 6)//horas
                                {
                                    var fechaComparar = Fechaactual.AddHours(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && alumno.FechaAprobacion.Hour == fechaComparar.Hour && alumno.FechaAprobacion.Minute == fechaComparar.Minute)
                                    {
                                        EnvioWebexWhatsapp(alumno.CodigoMatricula, conf.IdPlantilla, alumno.Email);                                            
                                    }
                                }
                                else if (conf.IdTiempoFrecuencia == 2 )//dias
                                {
                                    var horaEnvio = Fechaactual.Add(conf.Hora.Value);
                                    var fechaComparar = Fechaactual.AddDays(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && horaEnvio.Hour == Fechaactual.Hour && horaEnvio.Minute == Fechaactual.Minute)
                                    {
                                        EnvioWebexWhatsapp(alumno.CodigoMatricula, conf.IdPlantilla, alumno.Email);                                            
                                    }
                                }
                                else if (conf.IdTiempoFrecuencia == 3)//semanas
                                {
                                    var horaEnvio = Fechaactual.Add(conf.Hora.Value);
                                    var valorDias = conf.Valor * 7;
                                    var fechaComparar = alumno.FechaAprobacion.AddDays(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && horaEnvio.Hour == Fechaactual.Hour && horaEnvio.Minute == Fechaactual.Minute)
                                    {
                                        EnvioWebexWhatsapp(alumno.CodigoMatricula, conf.IdPlantilla, alumno.Email);                                            
                                    }
                                }
                            }
                            else if (conf.IdTipoEnvioAutomatico == 2)//Mensaje de Texto
                            {
                                if (conf.IdTiempoFrecuencia == 6)//horas
                                {
                                    var fechaComparar = Fechaactual.AddHours(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && alumno.FechaAprobacion.Hour == fechaComparar.Hour && alumno.FechaAprobacion.Minute == fechaComparar.Minute)
                                    {
                                        EnviarSMS(alumno.IdMatriculaCabecera, conf.IdPlantilla,alumno.IdPersonalSolicitante);
                                    }
                                }
                                else if (conf.IdTiempoFrecuencia == 2)//dias
                                {
                                    var horaEnvio = Fechaactual.Add(conf.Hora.Value);
                                    var fechaComparar = Fechaactual.AddDays(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && horaEnvio.Hour == Fechaactual.Hour && horaEnvio.Minute == Fechaactual.Minute)
                                    {
                                        EnviarSMS(alumno.IdMatriculaCabecera, conf.IdPlantilla, alumno.IdPersonalSolicitante);
                                    }
                                }
                                else if (conf.IdTiempoFrecuencia == 3)//semanas
                                {
                                    var horaEnvio = Fechaactual.Add(conf.Hora.Value);
                                    var valorDias = conf.Valor * 7;
                                    var fechaComparar = alumno.FechaAprobacion.AddDays(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && horaEnvio.Hour == Fechaactual.Hour && horaEnvio.Minute == Fechaactual.Minute)
                                    {
                                        EnviarSMS(alumno.IdMatriculaCabecera, conf.IdPlantilla, alumno.IdPersonalSolicitante);
                                    }
                                }                                
                            }
                            else if (conf.IdTipoEnvioAutomatico == 3)//Correo electrónico
                            {
                                if (conf.IdTiempoFrecuencia == 6)//horas
                                {
                                    var fechaComparar = Fechaactual.AddHours(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && alumno.FechaAprobacion.Hour == fechaComparar.Hour && alumno.FechaAprobacion.Minute == fechaComparar.Minute)
                                    {
                                        EnvioWebexCorreo(alumno.CodigoMatricula, conf.IdPlantilla, alumno.Email, alumno.ZonaHoraria == (int?)null ? 0 : alumno.ZonaHoraria.Value, string.IsNullOrEmpty(alumno.NombrePais) ? "Perú" : alumno.NombrePais, alumno.IdMatriculaCabecera);
                                    }
                                }
                                else if (conf.IdTiempoFrecuencia == 2)//dias
                                {
                                    var horaEnvio = Fechaactual.Add(conf.Hora.Value);
                                    var fechaComparar = Fechaactual.AddDays(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && horaEnvio.Hour == Fechaactual.Hour && horaEnvio.Minute == Fechaactual.Minute)
                                    {
                                        EnvioWebexCorreo(alumno.CodigoMatricula, conf.IdPlantilla, alumno.Email, alumno.ZonaHoraria == (int?)null ? 0 : alumno.ZonaHoraria.Value, string.IsNullOrEmpty(alumno.NombrePais) ? "Perú" : alumno.NombrePais, alumno.IdMatriculaCabecera);
                                    }
                                }
                                else if (conf.IdTiempoFrecuencia == 3)//semanas
                                {
                                    var horaEnvio = Fechaactual.Add(conf.Hora.Value);
                                    var valorDias = conf.Valor * 7;
                                    var fechaComparar = alumno.FechaAprobacion.AddDays(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && horaEnvio.Hour == Fechaactual.Hour && horaEnvio.Minute == Fechaactual.Minute)
                                    {
                                        EnvioWebexCorreo(alumno.CodigoMatricula, conf.IdPlantilla, alumno.Email, alumno.ZonaHoraria == (int?)null ? 0 : alumno.ZonaHoraria.Value, string.IsNullOrEmpty(alumno.NombrePais) ? "Perú" : alumno.NombrePais, alumno.IdMatriculaCabecera);
                                    }
                                }
                            }
                        }
                        //Si es cambio de Estado
                        if ((alumno.ValorAnteriorEstado == (ValorEstadoAnteriorConf == null ? "" : ValorEstadoAnteriorConf.Nombre) || conf.IdEstadoInicial == -1) && (alumno.ValorNuevoEstado == (ValorEstadoNuevoConf == null ? "" : ValorEstadoNuevoConf.Nombre) || conf.IdEstadoDestino == -1))
                        {
                            if (conf.IdTipoEnvioAutomatico == 1)//WhatsApp
                            {
                                if (conf.IdTiempoFrecuencia == 6)//horas
                                {
                                    var fechaComparar = Fechaactual.AddHours(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && alumno.FechaAprobacion.Hour == fechaComparar.Hour && alumno.FechaAprobacion.Minute == fechaComparar.Minute)
                                    {
                                        EnvioWebexWhatsapp(alumno.CodigoMatricula, conf.IdPlantilla, alumno.Email);
                                    }
                                }
                                else if (conf.IdTiempoFrecuencia == 2)//dias
                                {
                                    var horaEnvio = Fechaactual.Add(conf.Hora.Value);
                                    var fechaComparar = Fechaactual.AddDays(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && horaEnvio.Hour == Fechaactual.Hour && horaEnvio.Minute == Fechaactual.Minute)
                                    {
                                        EnvioWebexWhatsapp(alumno.CodigoMatricula, conf.IdPlantilla, alumno.Email);
                                    }
                                }
                                else if (conf.IdTiempoFrecuencia == 3)//semanas
                                {
                                    var horaEnvio = Fechaactual.Add(conf.Hora.Value);
                                    var valorDias = conf.Valor * 7;
                                    var fechaComparar = alumno.FechaAprobacion.AddDays(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && horaEnvio.Hour == Fechaactual.Hour && horaEnvio.Minute == Fechaactual.Minute)
                                    {
                                        EnvioWebexWhatsapp(alumno.CodigoMatricula, conf.IdPlantilla, alumno.Email);
                                    }
                                }
                            }
                            else if (conf.IdTipoEnvioAutomatico == 2)//Mensaje de Texto
                            {
                                if (conf.IdTiempoFrecuencia == 6)//horas
                                {
                                    var fechaComparar = Fechaactual.AddHours(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && alumno.FechaAprobacion.Hour == fechaComparar.Hour && alumno.FechaAprobacion.Minute == fechaComparar.Minute)
                                    {
                                        EnviarSMS(alumno.IdMatriculaCabecera, conf.IdPlantilla, alumno.IdPersonalSolicitante);
                                    }
                                }
                                else if (conf.IdTiempoFrecuencia == 2)//dias
                                {
                                    var horaEnvio = Fechaactual.Add(conf.Hora.Value);
                                    var fechaComparar = Fechaactual.AddDays(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && horaEnvio.Hour == Fechaactual.Hour && horaEnvio.Minute == Fechaactual.Minute)
                                    {
                                        EnviarSMS(alumno.IdMatriculaCabecera, conf.IdPlantilla, alumno.IdPersonalSolicitante);
                                    }
                                }
                                else if (conf.IdTiempoFrecuencia == 3)//semanas
                                {
                                    var horaEnvio = Fechaactual.Add(conf.Hora.Value);
                                    var valorDias = conf.Valor * 7;
                                    var fechaComparar = alumno.FechaAprobacion.AddDays(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && horaEnvio.Hour == Fechaactual.Hour && horaEnvio.Minute == Fechaactual.Minute)
                                    {
                                        EnviarSMS(alumno.IdMatriculaCabecera, conf.IdPlantilla, alumno.IdPersonalSolicitante);
                                    }
                                }
                            }
                            else if (conf.IdTipoEnvioAutomatico == 3)//Correo electrónico
                            {
                                if (conf.IdTiempoFrecuencia == 6)//horas
                                {
                                    var fechaComparar = Fechaactual.AddHours(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && alumno.FechaAprobacion.Hour == fechaComparar.Hour && alumno.FechaAprobacion.Minute == fechaComparar.Minute)
                                    {
                                        EnvioWebexCorreo(alumno.CodigoMatricula, conf.IdPlantilla, alumno.Email, alumno.ZonaHoraria == (int?)null ? 0 : alumno.ZonaHoraria.Value, string.IsNullOrEmpty(alumno.NombrePais) ? "Perú" : alumno.NombrePais, alumno.IdMatriculaCabecera);
                                    }
                                }
                                else if (conf.IdTiempoFrecuencia == 2)//dias
                                {
                                    var horaEnvio = Fechaactual.Add(conf.Hora.Value);
                                    var fechaComparar = Fechaactual.AddDays(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && horaEnvio.Hour == Fechaactual.Hour && horaEnvio.Minute == Fechaactual.Minute)
                                    {
                                        EnvioWebexCorreo(alumno.CodigoMatricula, conf.IdPlantilla, alumno.Email, alumno.ZonaHoraria == (int?)null ? 0 : alumno.ZonaHoraria.Value, string.IsNullOrEmpty(alumno.NombrePais) ? "Perú" : alumno.NombrePais, alumno.IdMatriculaCabecera);
                                    }
                                }
                                else if (conf.IdTiempoFrecuencia == 3)//semanas
                                {
                                    var horaEnvio = Fechaactual.Add(conf.Hora.Value);
                                    var valorDias = conf.Valor * 7;
                                    var fechaComparar = alumno.FechaAprobacion.AddDays(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && horaEnvio.Hour == Fechaactual.Hour && horaEnvio.Minute == Fechaactual.Minute)
                                    {
                                        EnvioWebexCorreo(alumno.CodigoMatricula, conf.IdPlantilla, alumno.Email, alumno.ZonaHoraria == (int?)null ? 0 : alumno.ZonaHoraria.Value, string.IsNullOrEmpty(alumno.NombrePais) ? "Perú" : alumno.NombrePais, alumno.IdMatriculaCabecera);
                                    }
                                }
                            }
                        }
                        //Si es cambio de SubEstado
                        if ((alumno.ValorAnteriorSubEstado == (ValorSubEstadoAnteriorConf == null ? "" : ValorSubEstadoAnteriorConf.Nombre)  || conf.IdSubEstadoInicial == -1) && (alumno.ValorNuevoSubEstado == (ValorSubEstadoNuevoConf == null ? "" : ValorSubEstadoNuevoConf.Nombre) || conf.IdSubEstadoDestino == -1))
                        {
                            if (conf.IdTipoEnvioAutomatico == 1)//WhatsApp
                            {
                                if (conf.IdTiempoFrecuencia == 6)//horas
                                {
                                    var fechaComparar = Fechaactual.AddHours(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && alumno.FechaAprobacion.Hour == fechaComparar.Hour && alumno.FechaAprobacion.Minute == fechaComparar.Minute)
                                    {
                                        EnvioWebexWhatsapp(alumno.CodigoMatricula, conf.IdPlantilla, alumno.Email);
                                    }
                                }
                                else if (conf.IdTiempoFrecuencia == 2)//dias
                                {
                                    var horaEnvio = Convert.ToDateTime(conf.Hora.ToString());
                                    var fechaComparar = Fechaactual.AddDays(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && horaEnvio.Hour == Fechaactual.Hour && horaEnvio.Minute == Fechaactual.Minute)
                                    {
                                        EnvioWebexWhatsapp(alumno.CodigoMatricula, conf.IdPlantilla, alumno.Email);
                                    }
                                }
                                else if (conf.IdTiempoFrecuencia == 3)//semanas
                                {
                                    var horaEnvio = Convert.ToDateTime(conf.Hora.ToString());
                                    var valorDias = conf.Valor * 7;
                                    var fechaComparar = alumno.FechaAprobacion.AddDays(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && horaEnvio.Hour == Fechaactual.Hour && horaEnvio.Minute == Fechaactual.Minute)
                                    {
                                        EnvioWebexWhatsapp(alumno.CodigoMatricula, conf.IdPlantilla, alumno.Email);
                                    }
                                }
                            }
                            else if (conf.IdTipoEnvioAutomatico == 2)//Mensaje de Texto
                            {
                                if (conf.IdTiempoFrecuencia == 6)//horas
                                {
                                    var fechaComparar = Fechaactual.AddHours(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && alumno.FechaAprobacion.Hour == fechaComparar.Hour && alumno.FechaAprobacion.Minute == fechaComparar.Minute)
                                    {
                                        EnviarSMS(alumno.IdMatriculaCabecera, conf.IdPlantilla, alumno.IdPersonalSolicitante);
                                    }
                                }
                                else if (conf.IdTiempoFrecuencia == 2)//dias
                                {
                                    var horaEnvio = Convert.ToDateTime(conf.Hora.ToString());
                                    var fechaComparar = Fechaactual.AddDays(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && horaEnvio.Hour == Fechaactual.Hour && horaEnvio.Minute == Fechaactual.Minute)
                                    {
                                        EnviarSMS(alumno.IdMatriculaCabecera, conf.IdPlantilla, alumno.IdPersonalSolicitante);
                                    }
                                }
                                else if (conf.IdTiempoFrecuencia == 3)//semanas
                                {
                                    var horaEnvio = Convert.ToDateTime(conf.Hora.ToString());
                                    var valorDias = conf.Valor * 7;
                                    var fechaComparar = alumno.FechaAprobacion.AddDays(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && horaEnvio.Hour == Fechaactual.Hour && horaEnvio.Minute == Fechaactual.Minute)
                                    {
                                        EnviarSMS(alumno.IdMatriculaCabecera, conf.IdPlantilla, alumno.IdPersonalSolicitante);
                                    }
                                }
                            }
                            else if (conf.IdTipoEnvioAutomatico == 3)//Correo electrónico
                            {
                                if (conf.IdTiempoFrecuencia == 6)//horas
                                {
                                    var fechaComparar = Fechaactual.AddHours(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && alumno.FechaAprobacion.Hour == fechaComparar.Hour && alumno.FechaAprobacion.Minute == fechaComparar.Minute)
                                    {
                                        EnvioWebexCorreo(alumno.CodigoMatricula, conf.IdPlantilla, alumno.Email, alumno.ZonaHoraria == (int?)null ? 0 : alumno.ZonaHoraria.Value, string.IsNullOrEmpty(alumno.NombrePais) ? "Perú" : alumno.NombrePais, alumno.IdMatriculaCabecera);
                                    }
                                }
                                else if (conf.IdTiempoFrecuencia == 2)//dias
                                {
                                    
                                    var horaEnvio = Convert.ToDateTime(conf.Hora.ToString());
                                    var fechaComparar = Fechaactual.AddDays(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && horaEnvio.Hour == Fechaactual.Hour && horaEnvio.Minute == Fechaactual.Minute)
                                    {
                                        EnvioWebexCorreo(alumno.CodigoMatricula, conf.IdPlantilla, alumno.Email, alumno.ZonaHoraria == (int?)null ? 0 : alumno.ZonaHoraria.Value, string.IsNullOrEmpty(alumno.NombrePais) ? "Perú" : alumno.NombrePais, alumno.IdMatriculaCabecera);
                                    }
                                }
                                else if (conf.IdTiempoFrecuencia == 3)//semanas
                                {
                                    var horaEnvio = Convert.ToDateTime(conf.Hora.ToString());
                                    var valorDias = conf.Valor * 7;
                                    var fechaComparar = alumno.FechaAprobacion.AddDays(-conf.Valor);
                                    if (alumno.FechaAprobacion.Date == fechaComparar.Date && horaEnvio.Hour == Fechaactual.Hour && horaEnvio.Minute == Fechaactual.Minute)
                                    {
                                        EnvioWebexCorreo(alumno.CodigoMatricula, conf.IdPlantilla, alumno.Email, alumno.ZonaHoraria == (int?)null ? 0 : alumno.ZonaHoraria.Value, string.IsNullOrEmpty(alumno.NombrePais) ? "Perú" : alumno.NombrePais, alumno.IdMatriculaCabecera);
                                    }
                                }
                            }
                        }             

                    }
                }
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public bool EnvioWebexCorreo(string CodigoAlumno, int IdPlantilla, string Destinatario, int IncrementoZonaHoraria, string NombrePais, int? idMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }
            try
            {
                if (!_repMatriculaCabecera.Exist(x => x.CodigoMatricula == CodigoAlumno))
                {
                    return false;
                }

                var matriculaCabecera = _repMatriculaCabecera.FirstBy(x => x.CodigoMatricula == CodigoAlumno);
                var detalleMatriculaCabecera = matriculaCabecera.ObtenerDetalleMatricula();

                if (!_repAlumno.Exist(matriculaCabecera.IdAlumno))
                {
                    return false;
                }

                if (!_repPlantilla.Exist(IdPlantilla))
                {
                    return false;
                }

                var plantilla = _repPlantilla.FirstById(IdPlantilla);
                if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                {
                    return false;
                }

                var plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantilla);
                var alumno = _repAlumno.FirstById(matriculaCabecera.IdAlumno);

                var oportunidad = _repOportunidad.FirstById(detalleMatriculaCabecera.IdOportunidad);
                var personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);

                _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                {
                    IdOportunidad = oportunidad.Id,
                    IdPlantilla = IdPlantilla,
                    IncrementoZonaHoraria = IncrementoZonaHoraria,
                    NombrePais = NombrePais,                    
                    IdMatriculaCabecera = idMatriculaCabecera
                };
                _reemplazoEtiquetaPlantilla.ReemplazarEtiquetas();

                var destinatarios = "jvillena@bsginstitute.com";
                //var destinatarios = alumno.Email1;

                var Remitente = string.IsNullOrEmpty(personal.Email) == true ? "matriculas@bsginstitute.com" : personal.Email;

                if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {

                    var emailCalculado = _reemplazoEtiquetaPlantilla.EmailReemplazado;
                    List<string> correosPersonalizadosCopia = new List<string>();
                    //cuando la plantilla es condiciones y caracteristicas
                    //1227	Condiciones y Características - PERÚ OPERACIONES
                    //1245	Condiciones y Características - COLOMBIA OPERACIONES
                    if (Remitente == "matriculas@bsginstitute.com" && (IdPlantilla == 1227 || IdPlantilla == 1245))
                    {
                        correosPersonalizadosCopia.Add("grabaciones@bsginstitute.com");
                    }
                    List<string> correosPersonalizadosCopiaOculta = new List<string>
                    {
                        "jvillena@bsginstitute.com",
                        "modpru@bsginstitute.com",
                        //"ccrispin@bsginstitute.com",
                        "wruiz@bsginstitute.com"
                    };

                    List<string> correosPersonalizados = new List<string>
                    {
                    };
                    correosPersonalizados.Add(destinatarios);

                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = Remitente,
                        //Sender = personal.Email,
                        //Sender = "w.choque.itusaca@isur.edu.pe",
                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                        Subject = emailCalculado.Asunto,
                        Message = emailCalculado.CuerpoHTML,
                        Cc = string.Join(",", correosPersonalizadosCopia.Distinct()),
                        Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                        AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                    };
                    var mailServie = new TMK_MailServiceImpl();

                    mailServie.SetData(mailDataPersonalizado);
                    mailServie.SendMessageTask();

                    //logica guardar envio
                    var gmailCorreo = new GmailCorreoBO
                    {
                        IdEtiqueta = 1,//sent:1 , inbox:2
                        Asunto = emailCalculado.Asunto,
                        Fecha = DateTime.Now,
                        EmailBody = emailCalculado.CuerpoHTML,
                        Seen = false,
                        Remitente = Remitente,
                        Cc = "",
                        Bcc = "modpru@bsginstitute.com",
                        Destinatarios = string.Join(",", correosPersonalizados.Distinct()),
                        IdPersonal = personal.Id,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = "SYSTEM",
                        UsuarioModificacion = "SYSTEM",
                        IdClasificacionPersona = oportunidad.IdClasificacionPersona
                    };
                    var _repGmailCorreo = new GmailCorreoRepositorio(_integraDBContext);
                    _repGmailCorreo.Insert(gmailCorreo);
                    return true;
                }
                else
                {
                    return false;
                }                
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool EnvioWebexWhatsapp(string CodigoAlumno, int IdPlantilla, string Destinatario)
        {
            if (!ModelState.IsValid)
            {
                return false;
            }
            try
            {                
                var matriculaCabecera = _repMatriculaCabecera.FirstBy(x => x.CodigoMatricula == CodigoAlumno);
                var detalleMatriculaCabecera = matriculaCabecera.ObtenerDetalleMatricula();

                var plantilla = _repPlantilla.FirstById(IdPlantilla);
                var alumno = _repAlumno.FirstById(matriculaCabecera.IdAlumno);
                var oportunidad = _repOportunidad.FirstById(detalleMatriculaCabecera.IdOportunidad);
                var personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);

                alumno.Celular = alumno.Celular.Replace("+", "");

                if (alumno.IdPais == 51)
                {
                    alumno.Celular = "51" + alumno.Celular;
                }
                else if (alumno.IdPais == 57)
                {
                    if (alumno.Celular.Length == 12 && alumno.Celular.StartsWith("57"))
                    {
                        alumno.Celular = alumno.Celular;
                    }
                    else
                    {
                        alumno.Celular = alumno.Celular.Remove(0, 2);
                    }


                }
                else if (alumno.IdPais == 591)
                {
                    if (alumno.Celular.Length == 11 && alumno.Celular.StartsWith("591"))
                    {
                        alumno.Celular = alumno.Celular;
                    }
                    else
                    {
                        alumno.Celular = alumno.Celular.Remove(0, 2);
                    }
                }
                else
                {
                    alumno.Celular = "1";
                }

                if (alumno.IdPais == 51)
                {
                    alumno.Celular = alumno.Celular + "51934129449";
                }

                //var Destinatarios = alumno.Celular;
                var Destinatarios = "51934129449";
                var destinatarios = Destinatarios.Split(";");

                if (plantilla.IdPlantillaBase == 8)
                {//logica whatsapp
                    _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                    {
                        IdOportunidad = oportunidad.Id,
                        IdPlantilla = IdPlantilla
                    };
                    _reemplazoEtiquetaPlantilla.ReemplazarEtiquetas();

                    var whatsAppCalculado = _reemplazoEtiquetaPlantilla.WhatsAppReemplazado;

                    var listaWhatsappConjuntoListaResultado = new List<WhatsAppResultadoConjuntoListaDTO>();

                    foreach (var destinatario in destinatarios)
                    {
                        listaWhatsappConjuntoListaResultado.Add(new WhatsAppResultadoConjuntoListaDTO()
                        {
                            IdAlumno = alumno.Id,
                            Celular = destinatario,
                            IdPersonal = personal.Id,
                            IdCodigoPais = alumno.IdCodigoPais ?? default,
                            IdConjuntoListaResultado = 0,
                            IdPgeneral = null,
                            IdPlantilla = IdPlantilla,
                            NroEjecucion = 1,
                            objetoplantilla = whatsAppCalculado.ListaEtiquetas,
                            Plantilla = whatsAppCalculado.Plantilla,
                            Validado = false
                        });
                    }

                    this.ValidarNumeroConjuntoLista(ref listaWhatsappConjuntoListaResultado);
                    listaWhatsappConjuntoListaResultado = listaWhatsappConjuntoListaResultado.Where(w => w.Validado == true).ToList();
                    listaWhatsappConjuntoListaResultado = listaWhatsappConjuntoListaResultado.Where(w => w.Plantilla != null && w.objetoplantilla.Count != 0).ToList();
                    this.EnvioAutomaticoPlantilla(listaWhatsappConjuntoListaResultado);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool EnviarSMS(int IdMatriculaCabecera, int IdPlantilla, int IdAsesor)
        {

            if (!ModelState.IsValid)
            {
                return false;
            }
            try
            {           
                string url = String.Empty;
                var matricula = _repMatriculaCabecera.FirstById(IdMatriculaCabecera);
                var alumno = _repAlumno.FirstById(matricula.IdAlumno);
                alumno.Celular= "51934129449";
                string Mensaje = "BSG INSTITUTE | Solicitud de información | Hola, soy Milagros Landeo de BSG Institute. Me estoy intentado comunicar contigo para brindarte asesoría personalizada sobre el programa de capacitación del que solicitaste información. Te pido por favor que puedas registrar mi numero ya que me intentare comunicar contigo en las próximas horas.También me puedes indicar por este medio la hora en la que te puedo llamar.";
                //string MensajeTarde = "BSG INSTITUTE | Solicitud de información |Hola, soy Milagros Landeo de BSG Institute.Me estoy intentado comunicar contigo para brindarte asesoría personalizada sobre el programa de capacitación del que solicitaste información.Te pido por favor que puedas registrar mi numero ya que me intentare comunicar contigo en las próximas horas o el día de mañana.También me puedes indicar por este medio la hora en la que te puedo llamar.";                

                if (alumno.Celular == null || String.IsNullOrEmpty(alumno.Celular))
                {
                    throw new Exception("El numero del celular es nullo o vacio");
                }
                switch (alumno.IdCodigoPais)
                {
                    case 51://Peru
                        if (alumno.Celular.Length != 9)
                        {
                            throw new Exception("El numero de Peru no tiene los digitos para el pais");
                        }
                        url = "http://192.168.3.24:80/sendsms?username=smsuser&password=smspwd&phonenumber=" + alumno.Celular + "&message=" + Mensaje + "&[port=gsm-3.4&][report=String&][timeout=5&][id=1]";
                        break;
                    case 57://Colombia
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
                    case 591://Bolivia
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
                    case 52://Mexico
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
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void ValidarNumeroConjuntoLista(ref List<WhatsAppResultadoConjuntoListaDTO> NumerosValidados)
        {
            string urlToPost;
            bool banderaLogin = false;
            string _tokenComunicacion = string.Empty;
            foreach (var Alumno in NumerosValidados)
            {
                WhatsAppMensajePublicidadBO whatsAppMensajePublicidad = new WhatsAppMensajePublicidadBO();

                ValidarNumerosWhatsAppDTO DTO = new ValidarNumerosWhatsAppDTO();
                DTO.contacts = new List<string>();
                DTO.blocking = "wait";
                DTO.contacts.Add("+" + Alumno.Celular);
                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    var _credencialesHost = _repCredenciales.ObtenerCredencialHost(Alumno.IdCodigoPais);
                    //calcula el valor que tiene
                    var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(Alumno.IdPersonal.Value, Alumno.IdCodigoPais);

                    var mensajeJSON = JsonConvert.SerializeObject(DTO);

                    string resultado = string.Empty;

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _repTokenUsuario.CredencialUsuarioLogin(Alumno.IdPersonal.Value);

                        var client = new RestClient(urlToPostUsuario);
                        var request = new RestSharp.RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Content-Length", "");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", _credencialesHost.IpHost);
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                        request.AddHeader("Content-Type", "application/json");
                        IRestResponse response = client.Execute(request);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var datos = JsonConvert.DeserializeObject<userLogeo>(response.Content);

                            foreach (var item in datos.users)
                            {
                                TWhatsAppUsuarioCredencial modelCredencial = new TWhatsAppUsuarioCredencial();

                                modelCredencial.IdWhatsAppUsuario = userLogin.IdWhatsAppUsuario;
                                modelCredencial.IdWhatsAppConfiguracion = _credencialesHost.Id;
                                modelCredencial.UserAuthToken = item.token;
                                modelCredencial.ExpiresAfter = Convert.ToDateTime(item.expires_after);
                                modelCredencial.EsMigracion = true;
                                modelCredencial.Estado = true;
                                modelCredencial.FechaCreacion = DateTime.Now;
                                modelCredencial.FechaModificacion = DateTime.Now;
                                modelCredencial.UsuarioCreacion = "whatsapp";
                                modelCredencial.UsuarioModificacion = "whatsapp";

                                var rpta = _repTokenUsuario.Insert(modelCredencial);

                                _tokenComunicacion = item.token;
                            }

                            banderaLogin = true;

                        }
                        else
                        {
                            banderaLogin = false;
                        }

                    }
                    else
                    {
                        _tokenComunicacion = tokenValida.UserAuthToken;
                        banderaLogin = true;
                    }

                    urlToPost = _credencialesHost.UrlWhatsApp + "/v1/contacts";

                    if (banderaLogin)
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.Encoding = Encoding.UTF8;

                            var serializer = new JavaScriptSerializer();

                            var serializedResult = serializer.Serialize(DTO);
                            string myParameters = serializedResult;
                            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                            client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                            client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                            client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                            resultado = client.UploadString(urlToPost, myParameters);


                        }

                        var datoRespuesta = JsonConvert.DeserializeObject<numerosValidos>(resultado);

                        foreach (var item in datoRespuesta.contacts)
                        {
                            if (item.status == "invalid")
                            {
                                Alumno.Validado = false;
                            }
                            else
                            {
                                Alumno.Validado = true;
                            }
                        }
                        
                    }
                    else
                    {
                        Alumno.Validado = false;                        
                    }

                }
                catch (Exception ex)
                {
                    List<string> correos = new List<string>();
                    correos.Add("ccrispin@bsginstitute.com");
                    correos.Add("jvillena@bsginstitute.com");

                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO();
                    mailData.Sender = "fvaldez@bsginstitute.com";
                    mailData.Recipient = string.Join(",", correos);
                    mailData.Subject = "Validacion Numero WhatsApp";
                    mailData.Message = "Alumno: " + Alumno.IdAlumno.ToString() + ", Numero: " + Alumno.Celular.ToString() + "<br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString();
                    mailData.Cc = "";
                    mailData.Bcc = "";
                    mailData.AttachedFiles = null;

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
            }


        }

        private void EnvioAutomaticoPlantilla(List<WhatsAppResultadoConjuntoListaDTO> MensajeAlumno)
        {

            bool banderaLogin = false;
            string _tokenComunicacion = string.Empty;
            var IdPlantilla = MensajeAlumno.FirstOrDefault().IdPlantilla.Value;
            var Plantilla = _repPlantilla.ObtenerPlantillaPorId(IdPlantilla);
            foreach (var AlumnoMensaje in MensajeAlumno)
            {
                WhatsAppMensajeEnviadoAutomaticoDTO DTO = new WhatsAppMensajeEnviadoAutomaticoDTO()
                {
                    Id = 0,
                    WaTo = AlumnoMensaje.Celular,
                    WaType = "hsm",
                    WaTypeMensaje = 8,
                    WaRecipientType = "hsm",
                    WaBody = Plantilla.Descripcion,
                    WaCaption = AlumnoMensaje.Plantilla,
                    datosPlantillaWhatsApp = AlumnoMensaje.objetoplantilla
                };

                try
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                    delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };

                    WhatsAppConfiguracionRepositorio _repCredenciales = new WhatsAppConfiguracionRepositorio(_integraDBContext);
                    WhatsAppUsuarioCredencialRepositorio _repTokenUsuario = new WhatsAppUsuarioCredencialRepositorio(_integraDBContext);

                    var _credencialesHost = _repCredenciales.ObtenerCredencialHost(AlumnoMensaje.IdCodigoPais);
                    //personal debe tener accesos
                    var tokenValida = _repTokenUsuario.ValidarCredencialesUsuario(AlumnoMensaje.IdPersonal.Value, AlumnoMensaje.IdCodigoPais);

                    string urlToPost = _credencialesHost.UrlWhatsApp;

                    string resultado = string.Empty, _waType = string.Empty;

                    //TWhatsAppMensajeEnviado mensajeEnviado = new TWhatsAppMensajeEnviado();

                    if (tokenValida == null || DateTime.Now >= tokenValida.ExpiresAfter)
                    {
                        string urlToPostUsuario = _credencialesHost.UrlWhatsApp + "/v1/users/login";

                        var userLogin = _repTokenUsuario.CredencialUsuarioLogin(AlumnoMensaje.IdPersonal.Value);

                        var client = new RestClient(urlToPostUsuario);
                        var request = new RestSharp.RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");
                        request.AddHeader("Content-Length", "");
                        request.AddHeader("Accept-Encoding", "gzip, deflate");
                        request.AddHeader("Host", _credencialesHost.IpHost);
                        request.AddHeader("Cache-Control", "no-cache");
                        request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(userLogin.UserUsername + ":" + userLogin.UserPassword)));
                        request.AddHeader("Content-Type", "application/json");
                        
                    }
                    else
                    {
                        _tokenComunicacion = tokenValida.UserAuthToken;
                        banderaLogin = true;
                    }

                    if (banderaLogin)
                    {
                        switch (DTO.WaType.ToLower())
                        {
                            case "text":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages";
                                _waType = "text";

                                MensajeTextoEnvio _mensajeTexto = new MensajeTextoEnvio();

                                _mensajeTexto.to = DTO.WaTo;
                                _mensajeTexto.type = DTO.WaType;
                                _mensajeTexto.recipient_type = DTO.WaRecipientType;
                                _mensajeTexto.text = new text();

                                _mensajeTexto.text.body = DTO.WaBody;

                                using (WebClient client = new WebClient())
                                {
                                    //client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajeTexto);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajeTexto);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                            case "hsm":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                _waType = "hsm";

                                MensajePlantillaWhatsAppEnvio _mensajePlantilla = new MensajePlantillaWhatsAppEnvio();

                                _mensajePlantilla.to = DTO.WaTo;
                                _mensajePlantilla.type = DTO.WaType;
                                _mensajePlantilla.hsm = new hsm();

                                _mensajePlantilla.hsm.@namespace = "fc4f8077_6093_d099_e65a_6545de12f96b";
                                _mensajePlantilla.hsm.element_name = DTO.WaBody;

                                _mensajePlantilla.hsm.language = new language();
                                _mensajePlantilla.hsm.language.policy = "deterministic";
                                _mensajePlantilla.hsm.language.code = "es";

                                if (DTO.datosPlantillaWhatsApp != null)
                                {
                                    _mensajePlantilla.hsm.localizable_params = new List<localizable_params>();
                                    foreach (var listaDatos in DTO.datosPlantillaWhatsApp)
                                    {
                                        localizable_params _dato = new localizable_params();
                                        _dato.@default = listaDatos.texto;

                                        _mensajePlantilla.hsm.localizable_params.Add(_dato);
                                    }
                                }

                                using (WebClient client = new WebClient())
                                {
                                    client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajePlantilla);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajePlantilla);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                            case "image":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                _waType = "image";

                                MensajeImagenEnvio _mensajeImagen = new MensajeImagenEnvio();
                                _mensajeImagen.to = DTO.WaTo;
                                _mensajeImagen.type = DTO.WaType;
                                _mensajeImagen.recipient_type = DTO.WaRecipientType;

                                _mensajeImagen.image = new image();

                                _mensajeImagen.image.caption = DTO.WaCaption;
                                _mensajeImagen.image.link = DTO.WaLink;

                                using (WebClient client = new WebClient())
                                {
                                    client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajeImagen);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajeImagen);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                            case "document":
                                urlToPost = _credencialesHost.UrlWhatsApp + "/v1/messages/";
                                _waType = "document";

                                MensajeDocumentoEnvio _mensajeDocumento = new MensajeDocumentoEnvio();
                                _mensajeDocumento.to = DTO.WaTo;
                                _mensajeDocumento.type = DTO.WaType;
                                _mensajeDocumento.recipient_type = DTO.WaRecipientType;

                                _mensajeDocumento.document = new document();

                                _mensajeDocumento.document.caption = DTO.WaCaption;
                                _mensajeDocumento.document.link = DTO.WaLink;
                                _mensajeDocumento.document.filename = DTO.WaFileName;

                                using (WebClient client = new WebClient())
                                {
                                    client.Encoding = Encoding.UTF8;
                                    var mensajeJSON = JsonConvert.SerializeObject(_mensajeDocumento);
                                    var serializer = new JavaScriptSerializer();

                                    var serializedResult = serializer.Serialize(_mensajeDocumento);
                                    string myParameters = serializedResult;
                                    client.Headers[HttpRequestHeader.Authorization] = "Bearer " + _tokenComunicacion;
                                    client.Headers[HttpRequestHeader.ContentLength] = mensajeJSON.Length.ToString();
                                    client.Headers[HttpRequestHeader.Host] = _credencialesHost.IpHost;
                                    client.Headers[HttpRequestHeader.ContentType] = "application/json; charset=utf-8";
                                    resultado = client.UploadString(urlToPost, myParameters);
                                }

                                break;
                        }
                        
                    }
                    else
                    {                        
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                System.Threading.Thread.Sleep(5000);
            }

        }
    }
}
