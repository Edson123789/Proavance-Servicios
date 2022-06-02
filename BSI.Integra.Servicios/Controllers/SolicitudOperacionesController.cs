using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.SCode.BO;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Socket;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Models.AulaVirtual;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: SolicitudOperaciones
    /// Autor: Jose Villena
    /// Fecha: 03/05/2021
    /// <summary>
    /// Gestiona todas la propiedades de la tabla t_SolicitudOperaciones
    /// </summary>
    [Route("api/SolicitudOperaciones")]
    public class SolicitudOperacionesController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public SolicitudOperacionesController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        /// Tipo Función: GET
        /// Autor: Jose Villena  
        /// Fecha: 03/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las solicitudes de Operaciones
        /// </summary>
        /// <returns>retorna lista de solicitudes: List<DatosSolicitudOperacionesDTO></returns>
        [Route("[Action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerSolicitudOperaciones(int idOportunidad)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                SolicitudOperacionesRepositorio _repSolicitudOperaciones = new SolicitudOperacionesRepositorio(_integraDBContext);

                var resultado = _repSolicitudOperaciones.ObtenerSolicitudOperaciones(idOportunidad);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerHistorialAccesoTemporal(int IdOportunidad)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                SolicitudOperacionesRepositorio _repSolicitudOperaciones = new SolicitudOperacionesRepositorio(_integraDBContext);

                var resultado = _repSolicitudOperaciones.ObtenerHistorialAccesoTemporal(IdOportunidad);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Villena  
        /// Fecha: 03/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las solicitudes de Operaciones Realizadas
        /// </summary>
        /// <returns>retorna lista de solicitudes realizadas: List<DatosSolicitudOperacionesDTO></returns>
        [Route("[Action]/{IdOportunidad}")]
        [HttpGet]
        public ActionResult ObtenerSolicitudOperacionesRealizadas(int idOportunidad)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                SolicitudOperacionesRepositorio _repSolicitudOperaciones = new SolicitudOperacionesRepositorio(_integraDBContext);

                var resultado = _repSolicitudOperaciones.ObtenerSolicitudOperacionesRealizadas(idOportunidad);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Jose Villena  
        /// Fecha: 03/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las solicitudes de Operaciones
        /// </summary>
        /// <returns>retorna lista de solicitudes: List<DatosSolicitudOperacionesDTO></returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerSolicitudOperaciones()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                SolicitudOperacionesRepositorio _repSolicitudOperaciones = new SolicitudOperacionesRepositorio(_integraDBContext);

                var resultado = _repSolicitudOperaciones.ObtenerTodoSolicitudOperaciones();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Insertar(SolicitudOperacionesDTO obj, [FromForm] IList<IFormFile> Files)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                string nombreArchivo = string.Empty;
                string contentType = string.Empty;

                SolicitudOperacionesRepositorio _repSolicitudOperaciones = new SolicitudOperacionesRepositorio(_integraDBContext);
                if (Files != null)
                {
                    foreach (var file in Files)
                    {
                        contentType = file.ContentType;
                        nombreArchivo = file.FileName;
                        _repSolicitudOperaciones.guardarArchivos(file.ConvertToByte(), file.ContentType, file.FileName);
                    }
                }



                SolicitudOperacionesBO solicitudOperaciones = new SolicitudOperacionesBO();

                solicitudOperaciones.IdOportunidad = obj.IdOportunidad;
                solicitudOperaciones.IdTipoSolicitudOperaciones = obj.IdTipoSolicitudOperaciones;
                solicitudOperaciones.FechaSolicitud = DateTime.Now;
                solicitudOperaciones.IdPersonalSolicitante = obj.IdPersonalSolicitante;
                solicitudOperaciones.IdPersonalAprobacion = obj.IdPersonalAprobacion;
                solicitudOperaciones.ValorAnterior = obj.ValorAnterior;
                solicitudOperaciones.ValorNuevo = obj.ValorNuevo;
                solicitudOperaciones.Aprobado = obj.Aprobado;
                solicitudOperaciones.EsCancelado = false;
                solicitudOperaciones.ComentarioSolicitante = obj.ComentarioSolicitante;
                solicitudOperaciones.Observacion = obj.Observacion;
                solicitudOperaciones.IdUrlBlockStorage = 1;
                solicitudOperaciones.NombreArchivo = nombreArchivo;
                solicitudOperaciones.ContentType = contentType;
                solicitudOperaciones.Realizado = false;
                solicitudOperaciones.Estado = true;
                solicitudOperaciones.UsuarioCreacion = obj.Usuario;
                solicitudOperaciones.UsuarioModificacion = obj.Usuario;
                solicitudOperaciones.FechaCreacion = DateTime.Now;
                solicitudOperaciones.FechaModificacion = DateTime.Now;

                _repSolicitudOperaciones.Insert(solicitudOperaciones);

                return Ok(solicitudOperaciones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Carlos Crispin - Fischer Valdez - Edgar Serruto - Jose Villena
        /// Fecha: 18/01/2021
        /// Versión: 1.0
        /// <summary>
        /// Inserta diversas solicitudes de operaciones
        /// </summary>
        /// <returns></returns>

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarSolicitudOperaciones([FromBody] SolicitudOperacionesDTO Obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                string nombreArchivo = string.Empty;
                string contentType = string.Empty;

                SolicitudOperacionesRepositorio _repSolicitudOperaciones = new SolicitudOperacionesRepositorio(_integraDBContext);

                if (_repSolicitudOperaciones.Exist(w => w.IdOportunidad == Obj.IdOportunidad && w.IdTipoSolicitudOperaciones == Obj.IdTipoSolicitudOperaciones && w.Aprobado == false && w.EsCancelado == false))
                {
                    return BadRequest("La oportunidad ya tiene una solicitud de este tipo");
                }

                if (Obj.IdTipoSolicitudOperaciones == 4 && Obj.ValorNuevoSubestado != null)
                {
                    var cumplecriterio = _repSolicitudOperaciones.ValidarCambioSubEstado(Obj.IdOportunidad, Obj.ValorNuevoSubestado); //0 :no cumple con los requisitos , 1 :cumple pero no requiere validar la informacion portal web, 2 :cumple pero si requiere validar la informacion portal web
                    if (cumplecriterio.Valor == 0)
                    {
                        return BadRequest("No cumple con los criterios para pasar al nuevo subestado");
                    }
                }

                // Valido cambio de sbetsado para aplicar sus reglas y definir si pasa
                if (Obj.IdTipoSolicitudOperaciones == 5)
                {
                    var cumplecriterio = _repSolicitudOperaciones.ValidarCambioSubEstado(Obj.IdOportunidad, Obj.ValorNuevo); //0 :no cumple con los requisitos , 1 :cumple pero no requiere validar la informacion portal web, 2 :cumple pero si requiere validar la informacion portal web
                    if (cumplecriterio.Valor == 0)
                    {
                        return BadRequest("No cumple con los criterios para pasar al nuevo subestado");
                    }
                    else if (cumplecriterio.Valor == 1)// No valida informacion portal web
                    {
                        // Pasa noma // No se hace nada
                    }
                    else if (cumplecriterio.Valor == 2)// Valida informacion portal web
                    {
                        // Actualiza campo terminos a false
                        var acualizado = _repSolicitudOperaciones.ActualizarTerminosPortalWeb(Obj.IdOportunidad);

                        // Aqui ira el correo con la plantilla para el alumno
                    }
                }

                if (Obj.IdTipoSolicitudOperaciones == 7)
                {
                    string fecha = Obj.ValorNuevo;
                    string format = "dd/MM/yyyy";
                    DateTime fechaDia = DateTime.ParseExact(fecha, format, CultureInfo.InvariantCulture);
                    Obj.ValorNuevo = fechaDia.ToString("yyyy/MM/dd");
                }

                SolicitudOperacionesBO solicitudOperaciones = new SolicitudOperacionesBO
                {
                    IdOportunidad = Obj.IdOportunidad,
                    IdTipoSolicitudOperaciones = Obj.IdTipoSolicitudOperaciones,
                    FechaSolicitud = DateTime.Now,
                    IdPersonalSolicitante = Obj.IdPersonalSolicitante,
                    IdPersonalAprobacion = Obj.IdPersonalAprobacion,
                    ValorAnterior = Obj.ValorAnterior,
                    ValorNuevo = Obj.ValorNuevo,
                    Aprobado = Obj.Aprobado,
                    EsCancelado = false,
                    ComentarioSolicitante = Obj.ComentarioSolicitante,
                    Observacion = Obj.Observacion,
                    IdUrlBlockStorage = 1,
                    NombreArchivo = nombreArchivo,
                    ContentType = contentType,
                    Realizado = false,
                    ObservacionEncargado = Obj.ObservacionEncargado,
                    Estado = true,
                    UsuarioCreacion = Obj.Usuario,
                    UsuarioModificacion = Obj.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    RelacionEstadoSubEstado = Obj.RelacionEstadoSubEstado
                };

                if (!solicitudOperaciones.HasErrors)
                {
                    _repSolicitudOperaciones.Insert(solicitudOperaciones);
                    if (Obj.IdTipoSolicitudOperaciones == 8) /*Accesos temporales*/
                    {
                        SolicitudOperacionesAccesoTemporalDetalleRepositorio _repSolicitudOperacionesAccesoTemporalDetalle = new SolicitudOperacionesAccesoTemporalDetalleRepositorio(_integraDBContext);

                        foreach (var idPEspecifico in Obj.ListaIdPEspecificos)
                        {
                            SolicitudOperacionesAccesoTemporalDetalleBO solicitudOperacionesAccesoTemporal = new SolicitudOperacionesAccesoTemporalDetalleBO
                            {
                                IdSolicitudOperaciones = solicitudOperaciones.Id,
                                IdPEspecifico = idPEspecifico,
                                Estado = true,
                                UsuarioCreacion = Obj.Usuario,
                                UsuarioModificacion = Obj.Usuario,
                                FechaCreacion = DateTime.Now,
                                FechaModificacion = DateTime.Now
                            };

                            if (!solicitudOperacionesAccesoTemporal.HasErrors)
                                _repSolicitudOperacionesAccesoTemporalDetalle.Insert(solicitudOperacionesAccesoTemporal);
                        }
                    }
                }
                else
                {
                    return BadRequest(solicitudOperaciones.ActualesErrores);
                }


                return Ok(solicitudOperaciones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerConfirmacionSolicitudes(int Id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                SolicitudOperacionesRepositorio _repSolicitudOperaciones = new SolicitudOperacionesRepositorio(_integraDBContext);
                List<DatosSolicitudOperacionesDTO> rpta = new List<DatosSolicitudOperacionesDTO>();

                var resultado = _repSolicitudOperaciones.GetBy(x => x.Id == Id && x.Aprobado == false).FirstOrDefault();
                if (resultado != null)
                {
                    var llamarDatos = _repSolicitudOperaciones.ObtenerSolicitudOperacionesEnBloque(resultado.IdOportunidad);
                    var resultadoEstado = new DatosSolicitudOperacionesDTO();
                    var resultadoSubEstado = new List<DatosSolicitudOperacionesDTO>();

                    if (resultado.IdTipoSolicitudOperaciones == 4)
                    {
                        resultadoEstado = llamarDatos.Where(x => x.Id == Id && x.Aprobado == false).FirstOrDefault();
                        resultadoSubEstado = llamarDatos.Where(x => x.RelacionEstadoSubEstado == Id && x.Aprobado == false).ToList();
                    }
                    else if (resultado.IdTipoSolicitudOperaciones == 5)
                    {
                        resultadoSubEstado = llamarDatos.Where(x => x.Id == Id && x.Aprobado == false).ToList();
                        resultadoEstado = llamarDatos.Where(x => x.Id == resultado.RelacionEstadoSubEstado && x.Aprobado == false).FirstOrDefault();
                    }

                    if (resultadoEstado != null)
                    {
                        rpta.Add(resultadoEstado);
                    }
                    if (resultadoSubEstado != null && resultadoSubEstado.Count > 0)
                    {
                        rpta.AddRange(resultadoSubEstado);
                    }

                }

                return Ok(rpta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [Route("[Action]/{IdMatriculaCabecera}/{IdOportunidad}/{IdTipoSolicitudOperaciones}/{IdPersonalSolicitante}/{IdPersonalAprobacion}/{ValorAnterior}/{ValorNuevo}/{Aprobado}/{ComentarioSolicitante}/{Observacion}/{ObservacionEncargado}/{Usuario}")]
        [HttpGet]
        public ActionResult InsertarSolicitudOperacionesPortal(int IdMatriculaCabecera, int IdOportunidad, int IdTipoSolicitudOperaciones, int IdPersonalSolicitante, int IdPersonalAprobacion, string ValorAnterior, string ValorNuevo, bool Aprobado, string ComentarioSolicitante, string Observacion, string ObservacionEncargado, string Usuario)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                string NombreArchivo = "";
                string ContentType = "";
                SolicitudOperacionesRepositorio _repSolicitudOperaciones = new SolicitudOperacionesRepositorio(_integraDBContext);

                var idOportunidadPorMatricula = _repSolicitudOperaciones.ObtenerIdoportunidad(IdMatriculaCabecera);
                if (_repSolicitudOperaciones.Exist(w => w.IdOportunidad == idOportunidadPorMatricula && w.IdTipoSolicitudOperaciones == IdTipoSolicitudOperaciones && w.Aprobado == false && w.EsCancelado == false))
                {
                    return BadRequest("La oportunidad ya tiene una solicitud de este tipo");
                }
                SolicitudOperacionesBO solicitudOperaciones = new SolicitudOperacionesBO
                {
                    IdOportunidad = idOportunidadPorMatricula,
                    IdTipoSolicitudOperaciones = IdTipoSolicitudOperaciones,
                    FechaSolicitud = DateTime.Now,
                    IdPersonalSolicitante = IdPersonalSolicitante,
                    IdPersonalAprobacion = IdPersonalAprobacion,
                    ValorAnterior = ValorAnterior,
                    ValorNuevo = ValorNuevo,
                    Aprobado = Aprobado,
                    EsCancelado = false,
                    ComentarioSolicitante = ComentarioSolicitante,
                    IdUrlBlockStorage = 1,
                    NombreArchivo = NombreArchivo,
                    ContentType = ContentType,
                    Observacion = Observacion,
                    Realizado = true,
                    ObservacionEncargado = ObservacionEncargado,
                    Estado = true,
                    UsuarioCreacion = Usuario,
                    UsuarioModificacion = Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                if (!solicitudOperaciones.HasErrors)
                {
                    _repSolicitudOperaciones.Insert(solicitudOperaciones);
                }
                else
                {
                    return BadRequest(solicitudOperaciones.ActualesErrores);
                }


                return Ok(solicitudOperaciones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarSolicitudOperacionesPortalV2([FromBody] SolicitudOperacionesPortalDTO obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                string NombreArchivo = "";
                string ContentType = "";

                SolicitudOperacionesRepositorio _repSolicitudOperaciones = new SolicitudOperacionesRepositorio(_integraDBContext);
                var idOportunidadPorMatricula = _repSolicitudOperaciones.ObtenerIdoportunidad(obj.IdMatriculaCabecera);
                //var idPersonalAsignado = _repSolicitudOperaciones.ObtenerIdPersonalAsignado(idOportunidadPorMatricula);
                var Resultado = _repSolicitudOperaciones.ObtenerDiasPendientesRecorrer(idOportunidadPorMatricula);
                int diasPendientesRecorrer = 0;
                foreach (var item in Resultado)
                {
                    var x = item.ObservacionEncargado;
                    var arr = x.Split(',');
                    int numero = Int32.Parse(arr[2]);

                    diasPendientesRecorrer += numero;
                }
                var diasPendientesPorRecorrer = 90 - diasPendientesRecorrer;
                int diasRecorridos = obj.DiasRecorrer;
                if (diasRecorridos > diasPendientesPorRecorrer)
                {
                    return BadRequest("Los dias por recorrer sobrepasan a los dias disponibles por recorrer");
                }
                //if (_repSolicitudOperaciones.Exist(w => w.IdOportunidad == idOportunidadPorMatricula && w.IdTipoSolicitudOperaciones == obj.IdTipoSolicitudOperaciones && w.Aprobado == false && w.EsCancelado == false))
                //{
                //    return BadRequest("La oportunidad ya tiene una solicitud de este tipo");
                //}
                SolicitudOperacionesBO solicitudOperaciones = new SolicitudOperacionesBO
                {
                    IdOportunidad = idOportunidadPorMatricula,
                    IdTipoSolicitudOperaciones = obj.IdTipoSolicitudOperaciones,
                    FechaSolicitud = DateTime.Now,
                    IdPersonalSolicitante = 4777,
                    IdPersonalAprobacion = 4777,
                    ValorAnterior = obj.ValorAnterior,
                    ValorNuevo = obj.ValorNuevo,
                    Aprobado = obj.Aprobado,
                    EsCancelado = false,
                    ComentarioSolicitante = obj.ComentarioSolicitante,
                    Observacion = obj.Observacion,
                    IdUrlBlockStorage = 1,
                    NombreArchivo = NombreArchivo,
                    ContentType = ContentType,
                    Realizado = false,
                    ObservacionEncargado = obj.ObservacionEncargado,
                    Estado = true,
                    UsuarioCreacion = obj.Usuario,
                    UsuarioModificacion = obj.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                if (!solicitudOperaciones.HasErrors)
                {
                    _repSolicitudOperaciones.Insert(solicitudOperaciones);
                }
                else
                {
                    return BadRequest(solicitudOperaciones.ActualesErrores);
                }


                return Ok(solicitudOperaciones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{IdSolicitudOperaciones}/{Usuario}/{IdPersonal}")]
        [HttpGet]
        public ActionResult AprobarSolicitudOperaciones(int IdSolicitudOperaciones, string Usuario, int IdPersonal)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                SolicitudOperacionesRepositorio _repSolicitudOperaciones = new SolicitudOperacionesRepositorio(_integraDBContext);
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                MoodleCronogramaEvaluacionBO moodleCronogramaEvaluacionBO = new MoodleCronogramaEvaluacionBO(_integraDBContext);
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                AlumnoRepositorio _AlumnoRepositorio = new AlumnoRepositorio(_integraDBContext);
                var _PlantillaOperacionesController = new PlantillaOperacionesController(_integraDBContext);


                var solicitudOperaciones = _repSolicitudOperaciones.FirstById(IdSolicitudOperaciones);

                solicitudOperaciones.Aprobado = true;
                solicitudOperaciones.FechaAprobacion = DateTime.Now;
                solicitudOperaciones.UsuarioModificacion = Usuario;
                solicitudOperaciones.FechaModificacion = DateTime.Now;

                if (solicitudOperaciones.IdTipoSolicitudOperaciones == 4)/*Estado*/
                {
                    solicitudOperaciones.IdPersonalAprobacion = IdPersonal;
                    solicitudOperaciones.Realizado = true;


                    _repSolicitudOperaciones.Update(solicitudOperaciones);
                    _repMatriculaCabecera.ActualizarEstadoMatriculaPorSolicitud(IdSolicitudOperaciones, solicitudOperaciones.ValorNuevo);
                }
                else if (solicitudOperaciones.IdTipoSolicitudOperaciones == 5)/*SubEstado*/
                {
                    solicitudOperaciones.IdPersonalAprobacion = IdPersonal;
                    solicitudOperaciones.Realizado = true;
                    _repSolicitudOperaciones.Update(solicitudOperaciones);
                    _repMatriculaCabecera.ActualizarSubEstadoMatriculaPorSolicitud(IdSolicitudOperaciones, solicitudOperaciones.ValorNuevo);
                }
                else if (solicitudOperaciones.IdTipoSolicitudOperaciones == 6)/*6:Autoevaluaciones*/
                {
                    solicitudOperaciones.IdPersonalAprobacion = IdPersonal;
                    solicitudOperaciones.Realizado = true;

                    var compuesto = solicitudOperaciones.ObservacionEncargado.Split(",");
                    var respuesta = moodleCronogramaEvaluacionBO.ReprogramarCronograma(Convert.ToInt32(compuesto[0]), Convert.ToInt32(compuesto[1]), Convert.ToInt32(compuesto[2]), Convert.ToBoolean(compuesto[3]), Usuario);
                    if (respuesta.Estado == false)
                    {
                        return BadRequest(respuesta.Mensaje);
                    }
                    else
                    {
                        try
                        {
                            //var idPlantilla = 0;

                            var reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                            {
                                IdOportunidad = solicitudOperaciones.IdOportunidad,
                                IdPlantilla = 1226 //Información Cronograma de Autoevaluación - Aonline
                            };
                            reemplazoEtiquetaPlantilla.ReemplazarEtiquetas();
                            //envio correo
                            var oportunidad = _repOportunidad.ObtenerEmailPorOportunidad(solicitudOperaciones.IdOportunidad);

                            List<string> correosPersonalizados = new List<string>
                            {
                                oportunidad.EmailAlumno
                            };

                            TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                            {
                                Sender = oportunidad.EmailPersonal,
                                Recipient = string.Join(",", correosPersonalizados.Distinct()),
                                Subject = reemplazoEtiquetaPlantilla.EmailReemplazado.Asunto,
                                Message = reemplazoEtiquetaPlantilla.EmailReemplazado.CuerpoHTML,
                                Cc = "",
                                Bcc = "",//"fvaldez@bsginstitute.com",//string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                                AttachedFiles = reemplazoEtiquetaPlantilla.EmailReemplazado.ListaArchivosAdjuntos,
                            };
                            var mailServie = new TMK_MailServiceImpl();
                            mailServie.SetData(mailDataPersonalizado);
                            mailServie.SendMessageTask();

                        }
                        catch (Exception e)
                        {
                        }
                    }
                    _repSolicitudOperaciones.Update(solicitudOperaciones);
                }
                else if (solicitudOperaciones.IdTipoSolicitudOperaciones == 7)/*7: FechaFinalizacion*/
                {
                    var IdMatriculaCabecera = _repSolicitudOperaciones.ObtenerMatriculaPorOportunidad(solicitudOperaciones.IdOportunidad);
                    var matriculaCabeceraBO = _repMatriculaCabecera.FirstById(IdMatriculaCabecera);

                    solicitudOperaciones.IdPersonalAprobacion = IdPersonal;
                    solicitudOperaciones.Realizado = true;

                    matriculaCabeceraBO.FechaFinalizacion = Convert.ToDateTime(solicitudOperaciones.ValorNuevo);
                    matriculaCabeceraBO.UsuarioModificacion = Usuario;
                    matriculaCabeceraBO.FechaModificacion = DateTime.Now;


                    _repMatriculaCabecera.Update(matriculaCabeceraBO);


                    _repSolicitudOperaciones.Update(solicitudOperaciones);
                }
                else if (solicitudOperaciones.IdTipoSolicitudOperaciones == 8)/*8: Acceso Temporal*/
                {
                    try
                    {
                        _repSolicitudOperaciones.RegistrarCursoPrueba(IdSolicitudOperaciones);

                        solicitudOperaciones.IdPersonalAprobacion = IdPersonal;
                        solicitudOperaciones.FechaAprobacion = DateTime.Now;
                        solicitudOperaciones.Observacion = "Aprobado";
                        solicitudOperaciones.Aprobado = true;
                        solicitudOperaciones.Realizado = true;

                        _repSolicitudOperaciones.Update(solicitudOperaciones);

                        try
                        {
                            // Mailing
                            var reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                            {
                                IdOportunidad = solicitudOperaciones.IdOportunidad,
                                IdPlantilla = ValorEstatico.IdPlantillaAccesoTemporalMailing
                            };
                            reemplazoEtiquetaPlantilla.ReemplazarEtiquetas();
                            //Envio correo
                            var oportunidad = _repOportunidad.ObtenerEmailPorOportunidad(solicitudOperaciones.IdOportunidad);

                            List<string> correosPersonalizados = new List<string>
                            {
                                oportunidad.EmailAlumno
                            };

                            TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                            {
                                Sender = oportunidad.EmailPersonal,
                                Recipient = string.Join(",", correosPersonalizados.Distinct()),
                                Subject = reemplazoEtiquetaPlantilla.EmailReemplazado.Asunto,
                                Message = reemplazoEtiquetaPlantilla.EmailReemplazado.CuerpoHTML,
                                Cc = "",
                                Bcc = "gmiranda@bsginstitute.com",
                                AttachedFiles = reemplazoEtiquetaPlantilla.EmailReemplazado.ListaArchivosAdjuntos,
                            };
                            var mailServie = new TMK_MailServiceImpl();
                            mailServie.SetData(mailDataPersonalizado);
                            mailServie.SendMessageTask();
                        }
                        catch (Exception e)
                        {
                            List<string> correos = new List<string>
                            {
                                "gmiranda@bsginstitute.com"
                            };
                            TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();
                            TMKMailDataDTO mailData = new TMKMailDataDTO
                            {
                                Sender = "gmiranda@bsginstitute.com",
                                Recipient = string.Join(",", correos),
                                Subject = string.Concat("ERROR: Solicitud Operaciones Acceso Temporal"),
                                Message = string.Concat("Message: ", JsonConvert.SerializeObject(e)),
                                Cc = "",
                                Bcc = "",
                                AttachedFiles = null
                            };
                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                        }

                        try
                        {
                            PlantillaOperacionesController plantillaOperacionesController = new PlantillaOperacionesController(_integraDBContext);

                            plantillaOperacionesController.EnvioWhatsappNumeroIndividual(solicitudOperaciones.IdOportunidad, ValorEstatico.IdPlantillaAccesoTemporalWhatsApp, 1/*Reemplazo de operaciones*/);
                        }
                        catch (Exception e)
                        {
                            List<string> correos = new List<string>
                            {
                                "gmiranda@bsginstitute.com"
                            };
                            TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();
                            TMKMailDataDTO mailData = new TMKMailDataDTO
                            {
                                Sender = "gmiranda@bsginstitute.com",
                                Recipient = string.Join(",", correos),
                                Subject = string.Concat("ERROR: Solicitud Operaciones Acceso Temporal"),
                                Message = string.Concat("Message: ", JsonConvert.SerializeObject(e)),
                                Cc = "",
                                Bcc = "",
                                AttachedFiles = null
                            };
                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                        }
                    }
                    catch (Exception e)
                    {
                        List<string> correos = new List<string>
                        {
                            "gmiranda@bsginstitute.com"
                        };
                        TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();
                        TMKMailDataDTO mailData = new TMKMailDataDTO
                        {
                            Sender = "gmiranda@bsginstitute.com",
                            Recipient = string.Join(",", correos),
                            Subject = string.Concat("ERROR: Solicitud Operaciones Acceso Temporal"),
                            Message = string.Concat("Message: ", JsonConvert.SerializeObject(e)),
                            Cc = "",
                            Bcc = "",
                            AttachedFiles = null
                        };
                        Mailservice.SetData(mailData);
                        Mailservice.SendMessageTask();
                    }
                }
                else
                {
                    _repSolicitudOperaciones.Update(solicitudOperaciones);
                }
                var IdMatricula = _repSolicitudOperaciones.ObtenerMatriculaPorOportunidad(solicitudOperaciones.IdOportunidad);
                var matriculaCabecera = _repMatriculaCabecera.FirstById(IdMatricula);

                if (solicitudOperaciones.IdTipoSolicitudOperaciones == 5 || solicitudOperaciones.IdTipoSolicitudOperaciones == 4)
                {
                    if (matriculaCabecera.IdEstadoMatricula == 5 &&
                        (
                        matriculaCabecera.IdSubEstadoMatricula == 1 ||
                        matriculaCabecera.IdSubEstadoMatricula == 5 ||
                        matriculaCabecera.IdSubEstadoMatricula == 6 ||
                        matriculaCabecera.IdSubEstadoMatricula == 43)
                        )
                    {
                        IntegraAspNetUsersRepositorio _IntegraAspNetUsersRepositorio = new IntegraAspNetUsersRepositorio(_integraDBContext);
                        UsuarioRepositorio _UsuarioRepositorio = new UsuarioRepositorio(_integraDBContext);

                        DatosRegistroEnvioFisico rpta = new DatosRegistroEnvioFisico();
                        rpta.IdMatriculaCabecera = matriculaCabecera.Id;
                        rpta.IdAlumno = matriculaCabecera.IdAlumno;
                        var datosalumno = _AlumnoRepositorio.ObtenerDatosAlumnoPorId(rpta.IdAlumno);
                        rpta.Nombre = datosalumno.Nombre1 + " " + datosalumno.Nombre2;
                        var correoAlumno = _AlumnoRepositorio.ObtenerDatosAlumnoPorId(rpta.IdAlumno);
                        var usuarioCoordinadora = _repMatriculaCabecera.ObtenerIdAlumnoCoordinadorAcademico(rpta.IdMatriculaCabecera);
                        var correoCoordinadora = _IntegraAspNetUsersRepositorio.ObtenerEmailPorNombreUsuario(usuarioCoordinadora.UsuarioCoordinadorAcademico);
                        //var idPlantilla = 1391;
                        var idPlantilla = 1453;
                        var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                        {
                            IdPlantilla = idPlantilla
                        };
                        var user = _UsuarioRepositorio.FirstBy(x => x.NombreUsuario == usuarioCoordinadora.UsuarioCoordinadorAcademico);
                        if (user != null)
                        {
                            rpta.IdPersonal = user.IdPersonal;
                        }
                        _reemplazoEtiquetaPlantilla.ReemplazarEtiquetasEnvioCorreoSolicitudEnvioFiscio(rpta);

                        var emailCalculado = _reemplazoEtiquetaPlantilla.EmailReemplazado;
                        List<string> correosPersonalizadosCopiaOculta = new List<string>
                        {
                            "lhuallpa@bsginstitute.com",
                            "mmora@bsginstitute.com"
                        };


                        TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                        {
                            Sender = correoCoordinadora,
                            //Sender = personal.Email,
                            Recipient = correoAlumno.Email1,
                            Subject = emailCalculado.Asunto,
                            Message = emailCalculado.CuerpoHTML,
                            Cc = "",
                            Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                            AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                        };
                        var mailServie = new TMK_MailServiceImpl();

                        mailServie.SetData(mailDataPersonalizado);
                        mailServie.SendMessageTask();


                        AlumnoBO alumnoBO = new AlumnoBO();
                        ValidarNumerosWhatsAppAsyncDTO contact = new ValidarNumerosWhatsAppAsyncDTO();
                        contact.contacts = new List<string>();
                        var alumno = _AlumnoRepositorio.FirstById(rpta.IdAlumno);
                        var alumnoNumero = _AlumnoRepositorio.ObtenerNumeroWhatsApp(alumno.IdCodigoPais.Value, alumno.Celular);
                        contact.contacts.Add("+" + alumnoNumero);

                        var respuestaw = _PlantillaOperacionesController.Envio(correoAlumno.Email1, matriculaCabecera.CodigoMatricula, alumnoNumero, 1461);

                    };
                }
                return Ok(solicitudOperaciones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[Action]/{IdSolicitudOperaciones}/{Usuario}/{Observacion}")]
        [HttpGet]
        public ActionResult CancelarSolicitudOperaciones(int IdSolicitudOperaciones, string Usuario, string Observacion)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                SolicitudOperacionesRepositorio _repSolicitudOperaciones = new SolicitudOperacionesRepositorio(_integraDBContext);
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);

                var solicitudOperaciones = _repSolicitudOperaciones.FirstById(IdSolicitudOperaciones);

                solicitudOperaciones.EsCancelado = true;
                solicitudOperaciones.Observacion = Observacion;
                solicitudOperaciones.UsuarioModificacion = Usuario;
                solicitudOperaciones.FechaModificacion = DateTime.Now;

                _repSolicitudOperaciones.Update(solicitudOperaciones);
                try
                {
                    if (solicitudOperaciones.IdPersonalSolicitante != 0)
                    {
                        AgendaSocket.getInstance().SolicitudOperacionesRealizadaCancelada(solicitudOperaciones.IdOportunidad, solicitudOperaciones.IdPersonalSolicitante, 0);
                    }

                }
                catch (Exception)
                {
                }
                return Ok(solicitudOperaciones);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[Action]/{IdSolicitudOperaciones}/{Usuario}/{Observacion}")]
        [HttpGet]
        public ActionResult realizadoSolicitudOperaciones(int IdSolicitudOperaciones, string Usuario, string Observacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SolicitudOperacionesRepositorio _repSolicitudOperaciones = new SolicitudOperacionesRepositorio(_integraDBContext);
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                MatriculasMoodleBO matriculaMoodle = new MatriculasMoodleBO(_integraDBContext);
                var _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
                var pEspecificoNuevaAulaVirtual = _repPEspecifico.ObtenerPEspecificoNuevaAulaVirtual();

                var _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                var _repAlumno = new AlumnoRepositorio(_integraDBContext);
                var _repPersonal = new PersonalRepositorio(_integraDBContext);
                var solicitudOperaciones = _repSolicitudOperaciones.FirstById(IdSolicitudOperaciones);

                solicitudOperaciones.Realizado = true;
                solicitudOperaciones.Observacion = Observacion;
                solicitudOperaciones.UsuarioModificacion = Usuario;
                solicitudOperaciones.FechaModificacion = DateTime.Now;
                int IdMatriculaCabecera = 0;
                string CodigoMatricula = "";
                if (solicitudOperaciones.IdTipoSolicitudOperaciones == 1)//centrocosto
                {
                    var Registros = _repMatriculaCabecera.ObtenerRegistrosParaActualizar(solicitudOperaciones.Id);
                    IdMatriculaCabecera = Registros.IdMatriculaCabeceraV4;
                    CodigoMatricula = Registros.IdMatriculaCabeceraV3;
                    if (matriculaMoodle.QuitarMatricula(IdMatriculaCabecera, Registros.IdPespecificoV4, Usuario))
                    {
                        var esCorrecto = _repMatriculaCabecera.ActualizarCentroCosto(Registros);
                        if (esCorrecto)
                        {
                            RespuestaWebDTO cronograma = new RespuestaWebDTO();
                            MoodleCronogramaEvaluacionBO objetoCongelarCronograma = new MoodleCronogramaEvaluacionBO();
                            MdlUser moodleUser = new MdlUser();
                            try
                            {
                                var idPlantilla = 0;
                                if (_repPEspecifico.Exist(Registros.IdPespecificoV4))
                                {
                                    var pEspecifico = _repPEspecifico.FirstById(Registros.IdPespecificoV4);
                                    if (pEspecifico.TipoId == ValorEstatico.IdModalidadCursoOnlineAsincronica
                                        && !pEspecificoNuevaAulaVirtual.Exists(x => x.Id == Registros.IdPespecificoV4))
                                    {
                                        idPlantilla = ValorEstatico.IdPlantillaBienvenidaAlumnoAOnline;
                                    }
                                    else
                                    {
                                        idPlantilla = ValorEstatico.IdPlantillaBienvenidaAlumnoPresencialOnline;
                                    }
                                    var reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                                    {
                                        IdOportunidad = Registros.IdOportunidadV4,
                                        IdPlantilla = idPlantilla
                                    };
                                    reemplazoEtiquetaPlantilla.ReemplazarEtiquetas();
                                    //envio correo
                                    var oportunidad = _repOportunidad.FirstById(Registros.IdOportunidadV4);
                                    var personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);
                                    var alumno = _repAlumno.FirstById(oportunidad.IdAlumno);

                                    List<string> correosPersonalizados = new List<string>
                                    {
                                        alumno.Email1
                                    };
                                    List<string> correosPersonalizadosCopiaOculta = new List<string>
                                    {
                                        //"fvaldez@bsginstitute.com",
                                        "lhuallpa@bsginstitute.com",
                                        "controldeaccesos@bsginstitute.com",
                                        "bamontoya@bsginstitute.com",
                                        personal.Email
                                    };
                                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                                    {
                                        Sender = personal.Email,
                                        //Sender = "w.choque.itusaca@isur.edu.pe",
                                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                                        Subject = reemplazoEtiquetaPlantilla.EmailReemplazado.Asunto,
                                        Message = reemplazoEtiquetaPlantilla.EmailReemplazado.CuerpoHTML,
                                        Cc = "",
                                        Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                                        AttachedFiles = reemplazoEtiquetaPlantilla.EmailReemplazado.ListaArchivosAdjuntos,
                                    };
                                    var mailServie = new TMK_MailServiceImpl();
                                    mailServie.SetData(mailDataPersonalizado);
                                    mailServie.SendMessageTask();
                                }
                            }
                            catch (Exception e)
                            {
                                if (cronograma.Estado == true) objetoCongelarCronograma.EliminarUltimaVersionCongelada(IdMatriculaCabecera, moodleUser.Username);
                            }
                        }
                    }
                    else
                    {
                        solicitudOperaciones.Realizado = false;
                    }
                }
                else if (solicitudOperaciones.IdTipoSolicitudOperaciones == 3)//Version
                {
                    try
                    {
                        var Registros = _repMatriculaCabecera.ObtenerRegistrosParaActualizarVersion(solicitudOperaciones.Id);
                        IdMatriculaCabecera = Registros.IdMatriculaCabeceraV4;
                        CodigoMatricula = Registros.IdMatriculaCabeceraV3;

                        var matriculacabecera = _repMatriculaCabecera.FirstById(IdMatriculaCabecera);

                        int valorNuevo = 0;
                        switch (solicitudOperaciones.ValorNuevo)
                        {
                            case "Básica":
                                valorNuevo = 1;
                                break;
                            case "Profesional":
                                valorNuevo = 2;
                                break;
                            case "Gerencial":
                                valorNuevo = 3;
                                break;
                        }
                        matriculacabecera.IdPaquete = valorNuevo;
                        matriculacabecera.FechaModificacion = DateTime.Now;
                        matriculacabecera.UsuarioModificacion = Usuario;

                        //if (matriculacabecera.IdCronograma == 0 || matriculacabecera.IdCronograma == null)
                        //{
                        //    throw new Exception("Id Cronograma no tiene valor");
                        //}

                        var resultadoEliminacion = _repMatriculaCabecera.EliminarBeneficiosMatriculaCabeceraIdMatricula(IdMatriculaCabecera);
                        var listaNuevosBeneficios = _repMatriculaCabecera.InsertarBeneficiosMatriculaCabeceraIdMatricula(IdMatriculaCabecera, valorNuevo, matriculacabecera.IdCronograma);
                        _repMatriculaCabecera.Update(matriculacabecera);

                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }

                _repSolicitudOperaciones.Update(solicitudOperaciones);
                try
                {
                    if (solicitudOperaciones.IdPersonalSolicitante != 0)
                    {
                        AgendaSocket.getInstance().SolicitudOperacionesRealizadaCancelada(solicitudOperaciones.IdOportunidad, solicitudOperaciones.IdPersonalSolicitante, 1);
                    }
                }
                catch (Exception)
                {
                }
                return Ok(new { CodigoMatricula, IdMatriculaCabecera });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{IdMatriculaCabecera}/{IdPEspecifico}/{IdOportunidad}")]
        [HttpGet]
        public ActionResult EnviarMail(int IdMatriculaCabecera, int IdPEspecifico, int IdOportunidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MoodleCronogramaEvaluacionBO moodleCronogramaEvaluacion = new MoodleCronogramaEvaluacionBO();
                //moodleCronogramaEvaluacion.ObtenerCronogramaAutoEvaluacionUltimaVersion(IdMatriculaCabecera);
                try
                {
                    PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
                    OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                    AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
                    PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);

                    var idPlantilla = 0;
                    if (_repPEspecifico.Exist(IdPEspecifico))
                    {
                        var pEspecifico = _repPEspecifico.FirstById(IdPEspecifico);
                        if (pEspecifico.TipoId == ValorEstatico.IdModalidadCursoOnlineAsincronica)
                        {
                            idPlantilla = ValorEstatico.IdPlantillaBienvenidaAlumnoAOnline;
                        }
                        else
                        {
                            idPlantilla = ValorEstatico.IdPlantillaBienvenidaAlumnoPresencialOnline;
                        }
                        var reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                        {
                            IdOportunidad = IdOportunidad,
                            IdPlantilla = idPlantilla
                        };
                        reemplazoEtiquetaPlantilla.ReemplazarEtiquetas();
                        //envio correo
                        var oportunidad = _repOportunidad.FirstById(IdOportunidad);
                        var personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);
                        var alumno = _repAlumno.FirstById(oportunidad.IdAlumno);

                        List<string> correosPersonalizados = new List<string>
                                    {
                                        alumno.Email1
                                    };
                        List<string> correosPersonalizadosCopiaOculta = new List<string>
                                    {
										//"fvaldez@bsginstitute.com",
										"lhuallpa@bsginstitute.com",
                                        "controldeaccesos@bsginstitute.com",
                                        "bamontoya@bsginstitute.com",
                                        personal.Email
                                    };
                        TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                        {
                            Sender = personal.Email,
                            //Sender = "w.choque.itusaca@isur.edu.pe",
                            Recipient = string.Join(",", correosPersonalizados.Distinct()),
                            Subject = reemplazoEtiquetaPlantilla.EmailReemplazado.Asunto,
                            Message = reemplazoEtiquetaPlantilla.EmailReemplazado.CuerpoHTML,
                            Cc = "",
                            Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                            AttachedFiles = reemplazoEtiquetaPlantilla.EmailReemplazado.ListaArchivosAdjuntos,
                        };
                        var mailServie = new TMK_MailServiceImpl();
                        mailServie.SetData(mailDataPersonalizado);
                        mailServie.SendMessageTask();
                    }
                }
                catch (Exception e)
                {
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerDiasPendientesRecorrer(int IdMatriculaCabecera)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                SolicitudOperacionesRepositorio _repSolicitudOperaciones = new SolicitudOperacionesRepositorio(_integraDBContext);
                var idOportunidadPorMatricula = _repSolicitudOperaciones.ObtenerIdoportunidad(IdMatriculaCabecera);
                var Resultado = _repSolicitudOperaciones.ObtenerDiasPendientesRecorrer(idOportunidadPorMatricula);
                int diasPendientesRecorrer = 0;
                foreach (var item in Resultado)
                {
                    var x = item.ObservacionEncargado;
                    var arr = x.Split(',');
                    int numero = Int32.Parse(arr[2]);

                    diasPendientesRecorrer += numero;
                }
                return Ok(diasPendientesRecorrer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
