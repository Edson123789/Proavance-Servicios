using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Operaciones;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Scode.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace BSI.Integra.Servicios.Controllers.Operaciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculaCabeceraDatosCertificadoMensajesController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public MatriculaCabeceraDatosCertificadoMensajesController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }

        /// TipoFuncion: GET
        /// Autor: Miguel Mora
        /// Fecha: 30/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la cantidad de mensajes pendientes que tiene un usuario
        /// </summary>
        /// <returns>INT<returns>
        [Route("[Action]/{usuario}")]
        [HttpGet]
        public ActionResult ObtenerCantidadMensajes(string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IntegraAspNetUsersRepositorio _repAspNetUsers = new IntegraAspNetUsersRepositorio();
                PersonalRepositorio repPersonal = new PersonalRepositorio();
                IntegraAspNetUsersBO user = _repAspNetUsers.FirstBy(w => w.Estado == true && w.UserName == usuario);
                PersonalBO personal = repPersonal.FirstById(user.PerId);
                MatriculaCabeceraDatosCertificadoMensajesRepositorio repMatriculaCertificadoMensajes = new MatriculaCabeceraDatosCertificadoMensajesRepositorio(_integraDBContext);
                int listado = repMatriculaCertificadoMensajes.GetBy(w => w.Estado == true && w.IdPersonalReceptor == personal.Id && w.EstadoMensaje == true && w.ValorAntiguo!="-").Count();
                return Ok(listado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Miguel Mora
        /// Fecha: 30/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los mensjes que tiene un usuario
        /// </summary>
        /// <returns>INT<returns>
        [Route("[Action]/{usuario}")]
        [HttpGet]
        public ActionResult ObtenerMensajes(string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                IntegraAspNetUsersRepositorio _repAspNetUsers = new IntegraAspNetUsersRepositorio();
                PersonalRepositorio repPersonal = new PersonalRepositorio();
                IntegraAspNetUsersBO user = _repAspNetUsers.FirstBy(w => w.Estado == true && w.UserName == usuario);
                PersonalBO personal = repPersonal.FirstById(user.PerId);
                MatriculaCabeceraDatosCertificadoMensajesRepositorio repMatriculaCertificadoMensajes = new MatriculaCabeceraDatosCertificadoMensajesRepositorio(_integraDBContext);

                List<MatriculaCabeceraDatosCertificadoMensajesDTO> listadoPendientes = repMatriculaCertificadoMensajes.ObtenerMensajesPendientes(personal.Id);
                List<MatriculaCabeceraDatosCertificadoMensajesDTO> listadoLeidos = repMatriculaCertificadoMensajes.ObtenerMensajesLeidos(personal.Id);

                return Ok(new {pendientes= listadoPendientes,leidos=listadoLeidos });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Miguel Mora
        /// Fecha: 30/09/2021
        /// Versión: 1.0
        /// <summary>
        /// modifica un mensajes y lo apruba o desaprueba en base a sus respuesta
        /// </summary>
        /// <param name=”ObjetoDTO”>DTO de la tabla retenciones</param>
        /// <returns>bool<returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ModificarCertificadoMensaje([FromBody] MatriculaCabeceraDatosCertificadoMensajesDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraDatosCertificadoRepositorio repMatriculaCertificado = new MatriculaCabeceraDatosCertificadoRepositorio(_integraDBContext);
                MatriculaCabeceraDatosCertificadoMensajesRepositorio repMatriculaCertificadoMensaje = new MatriculaCabeceraDatosCertificadoMensajesRepositorio();
                MatriculaCabeceraDatosCertificadoMensajesBO mensaje = repMatriculaCertificadoMensaje.FirstBy(w => w.Id == ObjetoDTO.Id);
                MatriculaCabeceraDatosCertificadoDTO certificadoActual = repMatriculaCertificado.ObtenerDatosCertificadoPorMatricula(mensaje.IdMatriculaCabecera).First();
                MatriculaCabeceraDatosCertificadoBO solicitudCertificado = repMatriculaCertificado.FirstBy(w => 
                w.Estado == true && 
                w.IdMatriculaCabecera==mensaje.IdMatriculaCabecera &&
                w.EstadoCambioDatos == true);

                var valorMensaje = "Se aprobo tu solicitud -";
                if (ObjetoDTO.solicitud == false)
                {
                    valorMensaje = "NO se aprobo tu solicitud -";
                    solicitudCertificado.Estado = false;
                }
                else {

                    repMatriculaCertificado.Delete(certificadoActual.Id, ObjetoDTO.Usuario);

                }

                solicitudCertificado.EstadoCambioDatos = false;
                repMatriculaCertificado.Update(solicitudCertificado);

                if (ObjetoDTO.solicitud == true) {

                    PgeneralConfiguracionPlantillaRepositorio _repPgeneralConfiguracionPlantilla = new PgeneralConfiguracionPlantillaRepositorio(_integraDBContext);
                    CertificadoGeneradoAutomaticoRepositorio _repCertificadoGeneradoAutomatico = new CertificadoGeneradoAutomaticoRepositorio(_integraDBContext);
                    CertificadoGeneradoAutomaticoContenidoRepositorio _repCertificadoGeneradoAutomaticoContenido = new CertificadoGeneradoAutomaticoContenidoRepositorio(_integraDBContext);
                    CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
                    var dato = _repPgeneralConfiguracionPlantilla.ObtenerDatosParaCertificadosporAlumnoPortalWebPorIdMatriculaCabecera(certificadoActual.IdMatriculaCabecera);

                    MatriculaCabeceraRepositorio repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                    MatriculaCabeceraBO matricula = repMatriculaCabecera.FirstById(certificadoActual.IdMatriculaCabecera);
                    PEspecificoMatriculaAlumnoRepositorio reppEspecifico = new PEspecificoMatriculaAlumnoRepositorio();
                    bool existe = reppEspecifico.ExisteNuevaAulaVirtual(matricula.IdPespecifico);
                    DocumentosBO documentosBO = new DocumentosBO();
                    int Idplantillabase = 0;
                    string codigoCertificado = "";
                    byte[] documentoByte;

                    int idCertificadoGeneradoAutomatico = _repCertificadoGeneradoAutomatico.ObtenerRegistrosProgramaPorMatricula(dato.IdMatriculaCabecera);
                    if (idCertificadoGeneradoAutomatico > 0)
                    {
                        _repCertificadoGeneradoAutomatico.EliminarRegistrosPorId(idCertificadoGeneradoAutomatico);
                    }

                    if (existe == false)
                    {
                        documentoByte = documentosBO.GenerarVistaPreviaCertificado(dato.IdPlantillaFrontal, dato.IdPlantillaPosterior ?? default(int), dato.IdOportunidad, ref Idplantillabase, ref codigoCertificado);
                    }
                    else
                    {
                        documentoByte = documentosBO.GenerarVistaPreviaCertificadoPortalWeb(dato.IdPlantillaFrontal, dato.IdPlantillaPosterior ?? default(int), dato.IdOportunidad, ref Idplantillabase, ref codigoCertificado);

                    }

                    CertificadoGeneradoAutomaticoBO certificadoGenerado = new CertificadoGeneradoAutomaticoBO();
                    certificadoGenerado.IdMatriculaCabecera = dato.IdMatriculaCabecera;
                    certificadoGenerado.IdPgeneral = dato.IdPgeneral;
                    certificadoGenerado.IdPgeneralConfiguracionPlantilla = dato.IdPgeneralConfiguracionPlantilla;
                    certificadoGenerado.IdUrlBlockStorage = 1;
                    certificadoGenerado.ContentType = "application/pdf";
                    certificadoGenerado.NombreArchivo = Idplantillabase == 12 ? codigoCertificado : documentosBO.ContenidoCertificado.CorrelativoConstancia.ToString();
                    certificadoGenerado.IdPespecifico = dato.IdPespecifico;
                    certificadoGenerado.IdPlantilla = dato.IdPlantillaFrontal;
                    certificadoGenerado.FechaEmision = DateTime.Now;
                    certificadoGenerado.FechaCreacion = DateTime.Now;
                    certificadoGenerado.FechaModificacion = DateTime.Now;
                    certificadoGenerado.UsuarioCreacion = "SYSTEM";
                    certificadoGenerado.UsuarioModificacion = "SYSTEM";
                    certificadoGenerado.Estado = true;
                    _repCertificadoGeneradoAutomatico.Insert(certificadoGenerado);

                    CertificadoGeneradoAutomaticoContenidoBO contenidoCertificadoBO = documentosBO.ContenidoCertificado;

                    contenidoCertificadoBO.IdCertificadoGeneradoAutomatico = certificadoGenerado.Id;
                    contenidoCertificadoBO.FechaFinCapacitacion = contenidoCertificadoBO.FechaFinCapacitacion == null ? "" : contenidoCertificadoBO.FechaFinCapacitacion;
                    contenidoCertificadoBO.Estado = true;
                    contenidoCertificadoBO.FechaCreacion = DateTime.Now;
                    contenidoCertificadoBO.FechaModificacion = DateTime.Now;
                    contenidoCertificadoBO.UsuarioCreacion = "SYSTEM";
                    contenidoCertificadoBO.UsuarioModificacion = "SYSTEM";

                    _repCertificadoGeneradoAutomaticoContenido.Insert(contenidoCertificadoBO);

                    var Url = _repCertificadoDetalle.guardarArchivoCertificado(documentoByte, "application/pdf", certificadoGenerado.NombreArchivo);


                    if (Idplantillabase == 12)/*12:Certificado*/
                    {
                        var estados = repMatriculaCabecera.ObtenerEstadosPorPlantillas(Idplantillabase, dato.IdPlantillaFrontal, dato.IdPlantillaPosterior ?? default(int), dato.IdEstadoMatricula, dato.IdSubEstadoMatricula);

                        var matriculaBO = repMatriculaCabecera.FirstById(dato.IdMatriculaCabecera);
                        matriculaBO.IdEstadoMatricula = estados.IdEstadoMatricula;
                        matriculaBO.IdSubEstadoMatricula = estados.IdSubEstadoMatricula;
                        matriculaBO.UsuarioModificacion = "Gen-Certi";
                        matriculaBO.FechaModificacion = DateTime.Now;
                        matriculaBO.IdEstadoMatriculaCertificado = matriculaBO.IdEstadoMatricula;
                        matriculaBO.IdSubEstadoMatriculaCertificado = matriculaBO.IdSubEstadoMatricula;
                        repMatriculaCabecera.Update(matriculaBO);
                    }
                }

                mensaje.EstadoMensaje = false;
                repMatriculaCertificadoMensaje.Update(mensaje);

                valorMensaje += ObjetoDTO.MensajeRespuesta;

                MatriculaCabeceraDatosCertificadoMensajesBO nuevocertificadoMensaje = new MatriculaCabeceraDatosCertificadoMensajesBO
                {
                    IdMatriculaCabecera = mensaje.IdMatriculaCabecera,
                    IdPersonalRemitente = mensaje.IdPersonalReceptor,
                    IdPersonalReceptor = mensaje.IdPersonalRemitente,
                    Mensaje = valorMensaje,
                    ValorAntiguo ="-",
                    ValorNuevo = "-",
                    EstadoMensaje = true,
                    Estado = true,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                repMatriculaCertificadoMensaje.Insert(nuevocertificadoMensaje);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: GET
        /// Autor: Miguel Mora
        /// Fecha: 30/09/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los mensjes que tiene un usuario
        /// </summary>
        /// <returns>Bool<returns>
        [Route("[Action]/{IdMatriculaCabeceraDatosCertificadoMensajes}")]
        [HttpGet]
        public ActionResult VerMensaje(int IdMatriculaCabeceraDatosCertificadoMensajes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraDatosCertificadoMensajesRepositorio repMatriculaCertificadoMensaje = new MatriculaCabeceraDatosCertificadoMensajesRepositorio();
                MatriculaCabeceraDatosCertificadoMensajesBO mensaje = repMatriculaCertificadoMensaje.FirstBy(w => w.Id == IdMatriculaCabeceraDatosCertificadoMensajes);
                mensaje.EstadoMensaje = false;
                repMatriculaCertificadoMensaje.Update(mensaje);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
