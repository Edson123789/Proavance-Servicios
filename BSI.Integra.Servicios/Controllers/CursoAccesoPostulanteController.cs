using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas.PostulanteAccesoTemporalAulaVirtualDTO;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CursoAccesoPostulanteController
    /// Autor: Edgar Serruto
    /// Fecha: 17/06/2021
    /// <summary>
    /// Gestiona la funcionalidad del modulo (C) Cursos para accesos a postulantes
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CursoAccesoPostulanteController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        private readonly ExamenTestRepositorio _repExamenTest;
        private readonly ExamenRepositorio _repExamen;
        private readonly CentroCostoRepositorio _repCentroCosto;
        private readonly PostulanteAccesoTemporalAulaVirtualRepositorio _repPostulanteAccesoTemporalAulaVirtual;
        private readonly IntegraAspNetUsersRepositorio _repIntegraAspNetUsers;
        private readonly PespecificoRepositorio _repPespecifico;
        private readonly AlumnoRepositorio _repAlumno;
        private readonly ExamenAsignadoRepositorio _repExamenAsignado;

        public CursoAccesoPostulanteController(integraDBContext IntegraDBContext)
        {
            _integraDBContext = IntegraDBContext;
            _repExamenTest = new ExamenTestRepositorio(_integraDBContext);
            _repExamen = new ExamenRepositorio(_integraDBContext);
            _repCentroCosto = new CentroCostoRepositorio(_integraDBContext);
            _repPostulanteAccesoTemporalAulaVirtual = new PostulanteAccesoTemporalAulaVirtualRepositorio(_integraDBContext);
            _repIntegraAspNetUsers = new IntegraAspNetUsersRepositorio(_integraDBContext);
            _repPespecifico = new PespecificoRepositorio(_integraDBContext);
            _repAlumno = new AlumnoRepositorio(_integraDBContext);
            _repExamenAsignado = new ExamenAsignadoRepositorio(_integraDBContext);
        }

        /// TipoFuncion: POST
        /// Autor: Edgar Serruto
        /// Fecha: 17/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene combos para interfaz
        /// </summary>
        /// <returns> Objeto Agrupado </returns>
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
                var combos = new
                {
                    ListaCurso = _repCentroCosto.GetBy(x => x.Estado == true).Select(x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre).ToList(),
                    ListaEvaluacion = _repExamenTest.GetBy(x => x.EsCalificadoPorPostulante == true).Select(x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).OrderBy(x => x.Nombre).ToList(),
                    ListaComponenteRelacionado = _repExamen.ObtenerExamenPostulanteFiltro(),
                };
                return Ok(combos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Edgar Serruto
        /// Fecha: 17/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene convocatorias Registradas
        /// </summary>
        /// <returns> List<ConvocatoriaPersonalDTO> </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult ObtenerCursoAccesoPostulanteRegistrado()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var listaConvocatoriasRegistradas = _repExamen.ObtenerCursoAsociadoComponente();
                return Ok(listaConvocatoriasRegistradas);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Edgar Serruto.
        /// Fecha: 06/07/2021
        /// Versión: 1.0
        /// <summary>
        /// Recibe información de interfaz y Crea asociación de Curso con Componente
        /// </summary>
        /// <param name="CursoAccesoPostulante"> Información de Asociación de Curso y Componente </param>
        /// <returns> Retorna StatusCodes, 200 si la inserción es exitosa con Bool de confirmación de inserción y string mensaje </returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult InsertarCursoAccesoPostulante([FromBody] AsociacionCursoComponenteDTO CursoAccesoPostulante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var mensaje = "";
                if (CursoAccesoPostulante.IdExamenTest > 0
                    && CursoAccesoPostulante.IdExamen > 0
                    && CursoAccesoPostulante.IdCentroCosto > 0
                    && CursoAccesoPostulante.CantidadDiasAcceso > 0)
                {
                    var asociacionCursoComponente = _repExamen.GetBy(x => x.Id == CursoAccesoPostulante.IdExamen && x.IdExamenTest == CursoAccesoPostulante.IdExamenTest).FirstOrDefault();
                    if (asociacionCursoComponente != null && asociacionCursoComponente.IdCentroCosto == null)
                    {
                        asociacionCursoComponente.IdCentroCosto = CursoAccesoPostulante.IdCentroCosto;
                        asociacionCursoComponente.CantidadDiasAcceso = CursoAccesoPostulante.CantidadDiasAcceso;
                        _repExamen.Update(asociacionCursoComponente);

                        mensaje = "Se actualizó la asociación correctamente";
                        return Ok(new { Respuesta = true, Mensaje = mensaje });
                    }
                    else
                    {
                        mensaje = "Ya existe un curso asociado a dicho componente";
                        return Ok(new { Respuesta = false, Mensaje = mensaje });
                    }
                }
                else
                {
                    mensaje = "Falta información obligatoria";
                    return Ok(new { Respuesta = false, Mensaje = mensaje });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Respuesta = false, Mensaje = e.Message });
            }
        }
        /// TipoFuncion: POST
        /// Autor: Edgar Serruto.
        /// Fecha: 17/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Recibe información de interfaz y Actualiza asociación de Curso con Componente
        /// </summary>
        /// <param name="CursoAccesoPostulante"> Información de Asociación de Curso y Componente </param>
        /// <returns> Retorna StatusCodes, 200 si la inserción es exitosa con Bool de confirmación de inserción </returns>
        [HttpPost]
        [Route("[action]")]
        public ActionResult ActualizarCursoAccesoPostulante([FromBody] AsociacionCursoComponenteDTO CursoAccesoPostulante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var mensaje = "";
                if (CursoAccesoPostulante.IdExamenTest > 0 
                    && CursoAccesoPostulante.IdExamen > 0 
                    && CursoAccesoPostulante.IdCentroCosto > 0
                    && CursoAccesoPostulante.CantidadDiasAcceso > 0)
                {
                    var asociacionCursoComponente = _repExamen.GetBy(x => x.Id == CursoAccesoPostulante.IdExamen && x.IdExamenTest == CursoAccesoPostulante.IdExamenTest).FirstOrDefault();
                    if(asociacionCursoComponente != null)
                    {
                        asociacionCursoComponente.IdCentroCosto = CursoAccesoPostulante.IdCentroCosto;
                        asociacionCursoComponente.CantidadDiasAcceso = CursoAccesoPostulante.CantidadDiasAcceso;
                        _repExamen.Update(asociacionCursoComponente);

                        mensaje = "Se actualizó la asociación correctamente";
                        return Ok(new { Respuesta = true, Mensaje = mensaje });
                    }
                    else
                    {
                        mensaje = "La evaluación y componente no corresponden";
                        return Ok(new { Respuesta = false, Mensaje = mensaje });
                    }
                }
                else
                {
                    mensaje = "Falta información obligatoria";
                    return Ok(new { Respuesta = false, Mensaje = mensaje });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new { Respuesta = false, Mensaje = e.Message });
            }
        }
        /// TipoFuncion: POST
        /// Autor: Edgar Serruto.
        /// Fecha: 24/05/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina Asociación de componente y curso
        /// </summary>
        /// <param name="AsociacionCursoComponente"> Id de convocatoria y usuario </param>
        /// <returns> Retorna StatusCodes, 200 si se eliminó existasamente con Bool de confirmación</returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EliminarAsociacionCursoAccesoPostulante([FromBody] EliminarDTO AsociacionCursoComponente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string mensaje = "";
                if (AsociacionCursoComponente.Id > 0 && (AsociacionCursoComponente.NombreUsuario).Trim().Length > 0)
                {
                    var asociacionCursoComponente = _repExamen.FirstById(AsociacionCursoComponente.Id);
                    if (asociacionCursoComponente != null)
                    {
                        asociacionCursoComponente.IdCentroCosto = null;
                        asociacionCursoComponente.CantidadDiasAcceso = null;
                        _repExamen.Update(asociacionCursoComponente);

                        mensaje = "Se quitó la asociación correctamente";
                        return Ok(new { Respuesta = true, Mensaje = mensaje });
                    }
                    else
                    {
                        mensaje = "No se pudo encontrar el componente";
                        return Ok(new { Respuesta = false, Mensaje = mensaje });
                    }
                }
                else
                {
                    mensaje = "Falta Id de Registro o Nombre de Usuario de Módulo";
                    return Ok(new { Respuesta = false, Mensaje = mensaje });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Edgar Serruto.
        /// Fecha: 21/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Registra Accesos, envia  correo a postulante para inserción en aula virtual
        /// </summary>
        /// <param name="InformacionPostulanteExamen"> Id de Postulante, Id de Examen y Usuario </param>
        /// <returns> Retorna StatusCodes, 200 si se eliminó existasamente con Bool de confirmación y Mensaje a Interfaz </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult EnviarAccesoAulaVirtualPostulante([FromBody] EnviarAccesoPostulanteDTO InformacionPostulanteExamen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string mensaje = "";

                var examenAsignado = _repExamenAsignado.GetBy(x => x.IdPostulante == InformacionPostulanteExamen.IdPostulante && x.IdExamen == InformacionPostulanteExamen.IdExamen).OrderByDescending(x => x.Id).FirstOrDefault();
                if(examenAsignado.EstadoAcceso == true)
                {
                    mensaje = "El postulante ya cuenta o contó con accesos para este componente";
                    return Ok(new { Respuesta = false, Mensaje = mensaje });
                }
                if (InformacionPostulanteExamen.IdPostulante > 0 && InformacionPostulanteExamen.IdExamen > 0 && (InformacionPostulanteExamen.Usuario).Trim().Length > 0 && InformacionPostulanteExamen.IdPlantilla > 0)
                {
                    PostulanteAccesoTemporalAulaVirtualBO postulanteAccesoTemporalAulaVirtual = new PostulanteAccesoTemporalAulaVirtualBO();
                    var validacionAcceso = postulanteAccesoTemporalAulaVirtual.CrearAccesosTemporalesPostulante(InformacionPostulanteExamen);
                    if (!validacionAcceso.ValidacionRespuesta)
                    {
                        return BadRequest("Hubo un fallo en la actualizacion de los accesos temporales");
                    }
                    else
                    {
                        var examen = _repExamen.GetBy(x => x.Id == InformacionPostulanteExamen.IdExamen).FirstOrDefault();
                        var pEspecifico = _repPespecifico.GetBy(x => x.IdCentroCosto == examen.IdCentroCosto).OrderByDescending(x => x.Id).FirstOrDefault();
                        var accesoTemporal = _repPostulanteAccesoTemporalAulaVirtual.GetBy(x => x.IdPostulante == InformacionPostulanteExamen.IdPostulante && x.IdPespecificoHijo == pEspecifico.Id).OrderByDescending(x => x.Id).FirstOrDefault();
                        var alumno = _repAlumno.FirstById(validacionAcceso.IdAlumno.GetValueOrDefault());
                        if (pEspecifico != null && examen!= null && accesoTemporal != null && alumno != null)
                        {
                            var reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                            {
                                IdPlantilla = InformacionPostulanteExamen.IdPlantilla,
                            };
                            var emailPersonal = _repIntegraAspNetUsers.GetBy(x => x.UserName == InformacionPostulanteExamen.Usuario).OrderByDescending(x => x.Id).FirstOrDefault();
                            reemplazoEtiquetaPlantilla.ReemplazarEtiquetasAccesosTemporalesPostulante(validacionAcceso, pEspecifico.Id, accesoTemporal.FechaInicio, accesoTemporal.FechaFin, emailPersonal.Email);
                            /*Prepara datos para envío de correo*/
                            List<string> correosPersonalizadosCopiaOculta = new List<string>
                            {
                                emailPersonal.Email,
                            };
                            List<string> correosPersonalizados = new List<string>
                            {
                                alumno.Email1,
                            };
                            var datosMensaje = reemplazoEtiquetaPlantilla.EmailReemplazado;
                            TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                            {
                                Sender = emailPersonal.Email,
                                Recipient = string.Join(",", correosPersonalizados.Distinct()),
                                Subject = datosMensaje.Asunto,
                                Message = datosMensaje.CuerpoHTML,
                                Cc = "",
                                Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                            };
                            var mailServie = new TMK_MailServiceImpl();
                            mailServie.SetData(mailDataPersonalizado);
                            mailServie.SendMessageTask();
                            /*Se actualiza la información de Postulante*/
                            var examenAsignadoActualizar = _repExamenAsignado.GetBy(x => x.IdPostulante == InformacionPostulanteExamen.IdPostulante && x.IdExamen == InformacionPostulanteExamen.IdExamen).OrderByDescending(x => x.Id).FirstOrDefault();
                            if (examenAsignadoActualizar != null)
                            {
                                examenAsignadoActualizar.EstadoAcceso = true;
                                examenAsignadoActualizar.UsuarioModificacion = InformacionPostulanteExamen.Usuario;
                                examenAsignadoActualizar.FechaModificacion = DateTime.Now;
                                var actualizarEstadoAcceso = _repExamenAsignado.Update(examenAsignadoActualizar);
                                if (actualizarEstadoAcceso)
                                {
                                    mensaje = "Se enviaron accesos y correo a postulante";
                                    return Ok(new { Respuesta = true, Mensaje = mensaje });
                                }
                                else
                                {
                                    mensaje = "No se pudo actualizar la información de Acceso de Postulante";
                                    return Ok(new { Respuesta = false, Mensaje = mensaje });
                                }                                
                            }
                            else
                            {
                                mensaje = "No se pudo actualizar la información de Acceso de Postulante";
                                return Ok(new { Respuesta = false, Mensaje = mensaje });
                            }                           
                        }
                        else
                        {
                            mensaje = "Falta configuración de curso o accesos de postulante";
                            return Ok(new { Respuesta = false, Mensaje = mensaje });
                        }
                    }                    
                }
                else
                {
                    mensaje = "Falta Información para continuar con la función";
                    return Ok(new { Respuesta = false, Mensaje = mensaje });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
