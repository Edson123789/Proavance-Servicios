using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/SolicitudCertificadoFisico")]
    public class SolicitudCertificadoFisicoController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public SolicitudCertificadoFisicoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        [Route("[action]/{IdPersonal}")]
        [HttpGet]
        public ActionResult ObtenerCombos(int IdPersonal)
        {
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
                EstadoCertificadoFisicoRepositorio _repEstadoCertificadoFisico = new EstadoCertificadoFisicoRepositorio(_integraDBContext);
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                

                var filtro = new
                {
                    //Coordinador = _repPersonal.ObtenerCoordinadorasOperaciones(),
                    Coordinador = _repPersonal.ObtenerPersonalAsignadoOperacionesTotalV2(IdPersonal),
                    EstadoCertificadoFisico = _repEstadoCertificadoFisico.ObtenerEstadParaFiltro(),
                    MatriculaCabecera = _repMatriculaCabecera.ObtenerCodigoMatriculaParaCombo()
                };

                return Ok(filtro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);   
            }            
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerEstadoCertificadoFisico()
        {
            try
            {
                EstadoCertificadoFisicoRepositorio _repEstadoCertificadoFisico = new EstadoCertificadoFisicoRepositorio(_integraDBContext);                

                var filtro = new
                {
                    EstadoCertificadoFisico = _repEstadoCertificadoFisico.ObtenerEstadParaFiltro()
                };

                return Ok(filtro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);   
            }            
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerMatriculaFiltroAutoComplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                    return Ok(_repMatriculaCabecera.ObtenerCodigoMatriculaAutocompleto(Filtros["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCodigoSeguimientoEnvioAutoComplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    SolicitudCertificadoFisicoRepositorio _repSolicitudCertificadoFisico = new SolicitudCertificadoFisicoRepositorio(_integraDBContext);
                    return Ok(_repSolicitudCertificadoFisico.ObtenerCodigoSeguimientoFiltro(Filtros["valor"].ToString()));
                }
                else
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerFURAutoComplete([FromBody] Dictionary<string, string> Filtros)
        {
            try
            {
                if (Filtros != null)
                {
                    FurRepositorio _repFur = new FurRepositorio(_integraDBContext);

                    return Ok(_repFur.ObtenerFurAutoComplete(Filtros["Valor"].ToString()));
                }
                else
                {
                    return Ok();
                }

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerFUR()
        {
            try
            {
                FurRepositorio _repFur = new FurRepositorio(_integraDBContext);

                return Ok(_repFur.ObtenerFur());
                

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObternerSolicitudCertificadoFisico([FromBody] filtroSolicitudCertificadoFisicoDTO json)
        {
            try
            {
                PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
                SolicitudCertificadoFisicoRepositorio _repSolicitudCertificadoFisico = new SolicitudCertificadoFisicoRepositorio(_integraDBContext);
                ContenidoSolicitudCertificadoFisicoDTO rpta = new ContenidoSolicitudCertificadoFisicoDTO();
                if (json.ListaCoordinador.Count() == 0)
                {
                    var asistentesCargo = _repPersonal.ObtenerPersonalAsignadoOperacionesTotalV2(json.Personal);
                    List<int> ListaCoordinadortmp = new List<int>();
                    foreach (var item in asistentesCargo)
                    {
                        ListaCoordinadortmp.Add(item.Id);
                    }
                    json.ListaCoordinador = ListaCoordinadortmp;
                    rpta = _repSolicitudCertificadoFisico.ObtenerSolicitudesCertificadoFisico(json);
                }
                else
                {
                    rpta = _repSolicitudCertificadoFisico.ObtenerSolicitudesCertificadoFisico(json);
                }                    
                return Ok(rpta);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarCertificadoFisico([FromBody]SolicitudCertificadoFisicoDTO json)
        {
            try
            {
                SolicitudCertificadoFisicoRepositorio _repSolicitudCertificadoFisico = new SolicitudCertificadoFisicoRepositorio(_integraDBContext);

                SolicitudCertificadoFisicoBO solicitudCertificadoFisicoBO = new SolicitudCertificadoFisicoBO();
                solicitudCertificadoFisicoBO.IdMatriculaCabecera = json.IdMatriculaCabecera;
                solicitudCertificadoFisicoBO.IdPersonal = json.IdPersonal;
                solicitudCertificadoFisicoBO.FechaSolicitud = DateTime.Now;
                solicitudCertificadoFisicoBO.IdEstadoCertificadoFisico = 1; /*1= Pendiente de Envio*/
                //solicitudCertificadoFisicoBO.IdCertificadoGeneradoAutomatico = 1; 
                solicitudCertificadoFisicoBO.Estado = true;
                solicitudCertificadoFisicoBO.FechaCreacion = DateTime.Now;
                solicitudCertificadoFisicoBO.FechaModificacion = DateTime.Now;
                solicitudCertificadoFisicoBO.UsuarioCreacion = json.Usuario;
                solicitudCertificadoFisicoBO.UsuarioModificacion = json.Usuario;

                var rpta = _repSolicitudCertificadoFisico.Insert(solicitudCertificadoFisicoBO);
                return Ok(rpta);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Insertar([FromBody]SolicitudCertificadoFisicoDTO json)
        {
            try
            {
                SolicitudCertificadoFisicoRepositorio _repSolicitudCertificadoFisico = new SolicitudCertificadoFisicoRepositorio(_integraDBContext);
                var solicitudCertificadoFisicoBO = _repSolicitudCertificadoFisico.FirstBy(x => x.IdMatriculaCabecera == json.IdMatriculaCabecera && x.IdCertificadoGeneradoAutomatico==json.IdCertificadoGeneradoAutomatico);
                if (solicitudCertificadoFisicoBO != null)
                {
                    //PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
                    //var personal = _repPersonal.FirstById(solicitudCertificadoFisicoBO.IdPersonal);
                    return BadRequest("El certificado físico ya fue solicitado el día " + solicitudCertificadoFisicoBO.FechaCreacion.ToString("dd/MM/yyyy") + " por el usuario: " + solicitudCertificadoFisicoBO.UsuarioCreacion);
                }
                solicitudCertificadoFisicoBO = new SolicitudCertificadoFisicoBO();
                solicitudCertificadoFisicoBO.IdMatriculaCabecera = json.IdMatriculaCabecera;
                solicitudCertificadoFisicoBO.IdPersonal = json.IdPersonal;
                solicitudCertificadoFisicoBO.FechaSolicitud = DateTime.Now;
                solicitudCertificadoFisicoBO.IdEstadoCertificadoFisico = 1; /*1= Pendiente de Envio*/
                solicitudCertificadoFisicoBO.IdCertificadoGeneradoAutomatico = json.IdCertificadoGeneradoAutomatico;
                solicitudCertificadoFisicoBO.Estado = true;
                solicitudCertificadoFisicoBO.FechaCreacion = DateTime.Now;
                solicitudCertificadoFisicoBO.FechaModificacion = DateTime.Now;
                solicitudCertificadoFisicoBO.UsuarioCreacion = json.Usuario;
                solicitudCertificadoFisicoBO.UsuarioModificacion = json.Usuario;

                var rpta = _repSolicitudCertificadoFisico.Insert(solicitudCertificadoFisicoBO);
                return Ok(rpta);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Actualizar([FromBody]SolicitudCertificadoFisicoDTO json)
        {
            try
            {
                SolicitudCertificadoFisicoRepositorio _repSolicitudCertificadoFisico = new SolicitudCertificadoFisicoRepositorio(_integraDBContext);

                SolicitudCertificadoFisicoBO solicitudCertificadoFisicoBO = _repSolicitudCertificadoFisico.FirstById(json.Id);

                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                FurRepositorio _repFurRepositorio = new FurRepositorio(_integraDBContext);
                CourierRepositorio _repCourierRepositorio = new CourierRepositorio(_integraDBContext);
                CourierDetalleRepositorio _repCourierDetalleRepositorio = new CourierDetalleRepositorio(_integraDBContext);
                IntegraAspNetUsersRepositorio _IntegraAspNetUsersRepositorio = new IntegraAspNetUsersRepositorio(_integraDBContext);
                UsuarioRepositorio _UsuarioRepositorio = new UsuarioRepositorio(_integraDBContext);
                AlumnoRepositorio _AlumnoRepositorio = new AlumnoRepositorio(_integraDBContext);
                var _PlantillaOperacionesController = new PlantillaOperacionesController(_integraDBContext);

                if (json.IdFur !=null)
                {
                    solicitudCertificadoFisicoBO.IdFur = json.IdFur;
                    solicitudCertificadoFisicoBO.IdProveedor = json.IdProveedor;
                }
                if(json.IdEstadoCertificadoFisico != 0)
                {
                    solicitudCertificadoFisicoBO.IdEstadoCertificadoFisico = json.IdEstadoCertificadoFisico;
                    if (solicitudCertificadoFisicoBO.IdEstadoCertificadoFisico == 4) {
                        DatosRegistroEnvioFisico datos = new DatosRegistroEnvioFisico();
                        var matricula = _repMatriculaCabecera.FirstById(solicitudCertificadoFisicoBO.IdMatriculaCabecera);
                        var courier = _repCourierRepositorio.FirstById((int)solicitudCertificadoFisicoBO.IdCourier);
                        datos.IdMatriculaCabecera = matricula.Id;
                        datos.IdAlumno = matricula.IdAlumno;
                        datos.Courier = courier.Nombre;
                        datos.CodigoSeguimiento = solicitudCertificadoFisicoBO.CodigoSeguimiento;
                        var correoAlumno = _AlumnoRepositorio.ObtenerDatosAlumnoPorId(datos.IdAlumno);
                        var usuarioCoordinadora = _repMatriculaCabecera.ObtenerIdAlumnoCoordinadorAcademico(datos.IdMatriculaCabecera);
                        var correoCoordinadora = _IntegraAspNetUsersRepositorio.ObtenerEmailPorNombreUsuario(usuarioCoordinadora.UsuarioCoordinadorAcademico);
                        var user = _UsuarioRepositorio.FirstBy(x => x.NombreUsuario == usuarioCoordinadora.UsuarioCoordinadorAcademico);
                        if (user != null)
                        {
                            datos.IdPersonal = user.IdPersonal;
                        }

                        var idPlantilla = 1455;
                        var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                        {
                            IdPlantilla = idPlantilla
                        };
                        _reemplazoEtiquetaPlantilla.ReemplazarEtiquetasEnvioCorreoSolicitudEnvioFiscio(datos);

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
                        var alumno = _AlumnoRepositorio.FirstById(datos.IdAlumno);
                        var alumnoNumero = _AlumnoRepositorio.ObtenerNumeroWhatsApp(alumno.IdCodigoPais.Value, alumno.Celular);
                        contact.contacts.Add("+" + alumnoNumero);

                        var respuestaw = _PlantillaOperacionesController.Envio(correoAlumno.Email1, matricula.CodigoMatricula, alumnoNumero, 1463);
                    }
                }
                if(json.CodigoSeguimientoEnvio !=null)
                {
                    solicitudCertificadoFisicoBO.CodigoSeguimientoEnvio = json.CodigoSeguimientoEnvio;
                }
                if(json.FechaEntregaReal !=null)
                {
                    solicitudCertificadoFisicoBO.FechaEntregaReal = json.FechaEntregaReal;
                }
                if (json.FechaEntregaEstimada != null)
                {

                    solicitudCertificadoFisicoBO.FechaEntregaEstimada = json.FechaEntregaEstimada;
                }
                if (json.FechaEntregaCourier != null)
                {

                    solicitudCertificadoFisicoBO.FechaEntregaCourier = json.FechaEntregaCourier;

                    if (solicitudCertificadoFisicoBO.IdCourier != null && solicitudCertificadoFisicoBO.IdPaisConsignado != null && solicitudCertificadoFisicoBO.IdCiudadConsignada != null) {
                        var CourierDetalle = _repCourierDetalleRepositorio.FirstBy(
                            x=>x.IdCourier== solicitudCertificadoFisicoBO.IdCourier && 
                            x.IdCiudad==solicitudCertificadoFisicoBO.IdCiudadConsignada &&
                            x.Estado==true);
                        if (CourierDetalle!=null) {
                            solicitudCertificadoFisicoBO.FechaEntregaEstimada= ((DateTime)solicitudCertificadoFisicoBO.FechaEntregaCourier).AddDays(CourierDetalle.TiempoEnvio);
                        }
                    }
                }
                if (json.EstadoCourier != null)
                {
                    solicitudCertificadoFisicoBO.EstadoCourier = json.EstadoCourier;
                }
                if (json.IdPaisConsignado != null)
                {
                    if (solicitudCertificadoFisicoBO.IdPaisConsignado != json.IdPaisConsignado)
                    {
                        solicitudCertificadoFisicoBO.IdCiudadConsignada = null;

                    }
                    solicitudCertificadoFisicoBO.IdPaisConsignado = json.IdPaisConsignado;
                }
                if (json.IdCiudadConsignada != null)
                {
                    solicitudCertificadoFisicoBO.IdCiudadConsignada = json.IdCiudadConsignada;
                }
                if (json.CodigoSeguimiento != null)
                {
                    solicitudCertificadoFisicoBO.CodigoSeguimiento = json.CodigoSeguimiento;
                }
                if (json.IdCourier != null)
                {
                    if (solicitudCertificadoFisicoBO.IdCourier != json.IdCourier)
                    {
                        solicitudCertificadoFisicoBO.IdPaisConsignado = null;
                        solicitudCertificadoFisicoBO.IdCiudadConsignada = null;
                    }
                    solicitudCertificadoFisicoBO.IdCourier = json.IdCourier;
                }

                solicitudCertificadoFisicoBO.FechaModificacion = DateTime.Now;
                solicitudCertificadoFisicoBO.UsuarioModificacion = json.Usuario;

                var rpta = _repSolicitudCertificadoFisico.Update(solicitudCertificadoFisicoBO);

                if ((json.IdCourier != null || json.CodigoSeguimiento != null) && solicitudCertificadoFisicoBO.IdCourier!=null && solicitudCertificadoFisicoBO.CodigoSeguimiento!=null) {
                    DatosRegistroEnvioFisico datos = new DatosRegistroEnvioFisico();
                    var matricula = _repMatriculaCabecera.FirstById(solicitudCertificadoFisicoBO.IdMatriculaCabecera);
                    var courier= _repCourierRepositorio.FirstById((int)solicitudCertificadoFisicoBO.IdCourier);
                    datos.IdMatriculaCabecera = matricula.Id;
                    datos.IdAlumno = matricula.IdAlumno;
                    datos.Courier = courier.Nombre;
                    datos.CodigoSeguimiento = solicitudCertificadoFisicoBO.CodigoSeguimiento;
                    var correoAlumno = _AlumnoRepositorio.ObtenerDatosAlumnoPorId(datos.IdAlumno);
                    var usuarioCoordinadora = _repMatriculaCabecera.ObtenerIdAlumnoCoordinadorAcademico(datos.IdMatriculaCabecera);
                    var correoCoordinadora = _IntegraAspNetUsersRepositorio.ObtenerEmailPorNombreUsuario(usuarioCoordinadora.UsuarioCoordinadorAcademico);
                    var user = _UsuarioRepositorio.FirstBy(x => x.NombreUsuario == usuarioCoordinadora.UsuarioCoordinadorAcademico);
                    if (user != null)
                    {
                        datos.IdPersonal = user.IdPersonal;
                    }

                    var idPlantilla = 1454;
                    var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                    {
                        IdPlantilla = idPlantilla
                    };
                    _reemplazoEtiquetaPlantilla.ReemplazarEtiquetasEnvioCorreoSolicitudEnvioFiscio(datos);

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
                    var alumno = _AlumnoRepositorio.FirstById(datos.IdAlumno);
                    var alumnoNumero = _AlumnoRepositorio.ObtenerNumeroWhatsApp(alumno.IdCodigoPais.Value, alumno.Celular);
                    contact.contacts.Add("+" + alumnoNumero);

                    var respuestaw = _PlantillaOperacionesController.Envio(correoAlumno.Email1, matricula.CodigoMatricula, alumnoNumero, 1462);
                }

                
                return Ok(solicitudCertificadoFisicoBO);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{IdCertificadoGeneradoAutomatico}/{IdSolicitudCertificadoFisico}/{Usuario}")]
        [HttpGet]
        public ActionResult GenerarCertificadoFisico(int IdCertificadoGeneradoAutomatico,int IdSolicitudCertificadoFisico, string Usuario)
        {
            PgeneralConfiguracionPlantillaRepositorio _repPgeneralConfiguracionPlantilla = new PgeneralConfiguracionPlantillaRepositorio(_integraDBContext);
            CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoRepositorio _repCertificadoGeneradoAutomatico = new CertificadoGeneradoAutomaticoRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoContenidoRepositorio _repCertificadoGeneradoAutomaticoContenido = new CertificadoGeneradoAutomaticoContenidoRepositorio(_integraDBContext);
            RegistroCertificadoFisicoGeneradoRepositorio _repregistroCertificadoFisicoGenerado = new RegistroCertificadoFisicoGeneradoRepositorio(_integraDBContext);
            SolicitudCertificadoFisicoRepositorio _repSolicitudCertificadoFisico = new SolicitudCertificadoFisicoRepositorio(_integraDBContext);

            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);

            var dato = _repCertificadoGeneradoAutomaticoContenido.ObtenerDatosParaCertificadoFisico(IdCertificadoGeneradoAutomatico);
            string urlDocumento ="";
            foreach (var item in dato)
            {
                try
                {
                    RegistroCertificadoFisicoGeneradoBO registroCertificadoFisicoGeneradoBO = new RegistroCertificadoFisicoGeneradoBO();
                    DocumentosBO documentosBO = new DocumentosBO();

                    var documentoByte = documentosBO.GenerarCertificadoSinFondo(item);

                    urlDocumento = _repCertificadoDetalle.guardarArchivoCertificadoFisico(documentoByte, "application/pdf", item.CodigoCertificado + "IMP");

                    registroCertificadoFisicoGeneradoBO.IdSolicitudCertificadoFisico = IdSolicitudCertificadoFisico;
                    registroCertificadoFisicoGeneradoBO.IdUrlBlockStorage = 11;/*Certificado Fisico*/
                    registroCertificadoFisicoGeneradoBO.FormatoArchivo = "application/pdf";
                    registroCertificadoFisicoGeneradoBO.NombreArchivo = item.CodigoCertificado + "IMP";
                    registroCertificadoFisicoGeneradoBO.UltimaFechaGeneracion = DateTime.Now;
                    registroCertificadoFisicoGeneradoBO.Estado = true;
                    registroCertificadoFisicoGeneradoBO.FechaCreacion = DateTime.Now;
                    registroCertificadoFisicoGeneradoBO.FechaModificacion = DateTime.Now;
                    registroCertificadoFisicoGeneradoBO.UsuarioCreacion = Usuario;
                    registroCertificadoFisicoGeneradoBO.UsuarioModificacion = Usuario;

                    _repregistroCertificadoFisicoGenerado.Insert(registroCertificadoFisicoGeneradoBO);

                    var solicitudCertificadoBO = _repSolicitudCertificadoFisico.FirstById(IdSolicitudCertificadoFisico);

                    if (solicitudCertificadoBO.IdEstadoCertificadoFisico == 1)/*1:Pendiente de Envio*/
                    {
                        solicitudCertificadoBO.IdEstadoCertificadoFisico = 2; /*2:Impreso Pendiente de envio*/
                        solicitudCertificadoBO.FechaModificacion = DateTime.Now;
                        solicitudCertificadoBO.UsuarioModificacion = Usuario;

                        _repSolicitudCertificadoFisico.Update(solicitudCertificadoBO);
                    }
                    

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }


            }
            return Ok(new{ urlDocumento});
        }
        /// Tipo Función: GET
        /// Autor: Lourdes Priscila Pacsi Gamboa
        /// Fecha: 25/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Funcion que envia correos para la solicitud de envio de certificados fisicos
        /// </summary>
        /// <param name="Id">Id de un registro de la tabla mkt.T_SolicitudCertificadoFisico</param>
        /// <returns>Objeto del tipo DatosRegistroEnvioFisico</returns>
        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult ObtenerDatosRegistroEnvioFisico(int Id)
        {
            try
            {
                SolicitudCertificadoFisicoRepositorio _repSolicitudCertificadoFisico = new SolicitudCertificadoFisicoRepositorio(_integraDBContext);            

                var rpta = _repSolicitudCertificadoFisico.DatosRegistroEnvioFisico(Id);
                return Ok(rpta);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Lourdes Priscila Pacsi Gamboa
        /// Fecha: 25/06/2021
        /// Versión: 1.0
        /// <summary>
        /// Funcion que envia correos para la solicitud de envio de certificados fisicos
        /// </summary>
        /// <param name="Id">Id de un registro de la tabla mkt.T_SolicitudCertificadoFisico</param>
        /// <returns>true</returns>
        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult EnvioCorreoSolicitudCertificadoFisico(int Id)
        {

            SolicitudCertificadoFisicoRepositorio _repSolicitudCertificadoFisico = new SolicitudCertificadoFisicoRepositorio(_integraDBContext);
            AlumnoRepositorio _AlumnoRepositorio = new AlumnoRepositorio(_integraDBContext);
            MatriculaCabeceraRepositorio _MatriculaCabeceraRepositorio = new MatriculaCabeceraRepositorio(_integraDBContext);
            IntegraAspNetUsersRepositorio _IntegraAspNetUsersRepositorio = new IntegraAspNetUsersRepositorio(_integraDBContext);

            var rpta = _repSolicitudCertificadoFisico.DatosRegistroEnvioFisico(Id);
            var correoAlumno = _AlumnoRepositorio.ObtenerDatosAlumnoPorId(rpta.IdAlumno);
            var usuarioCoordinadora = _MatriculaCabeceraRepositorio.ObtenerIdAlumnoCoordinadorAcademico(rpta.IdMatriculaCabecera);
            var correoCoordinadora= _IntegraAspNetUsersRepositorio.ObtenerEmailPorNombreUsuario(usuarioCoordinadora.UsuarioCoordinadorAcademico);
            //var idPlantilla = 1391;
            var idPlantilla = 1392;
            var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
            {
                IdPlantilla = idPlantilla
            };
            _reemplazoEtiquetaPlantilla.ReemplazarEtiquetasEnvioCorreoSolicitudEnvioFiscio(rpta);

            var emailCalculado = _reemplazoEtiquetaPlantilla.EmailReemplazado;
            List<string> correosPersonalizadosCopiaOculta = new List<string>
                {
                    "lhuallpa@bsginstitute.com",
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

            return Ok(true);
        }

        /// Tipo Función: GET
        /// Autor: Max Mantilla
        /// Fecha: 02/12/2021
        /// Versión: 1.0
        /// <summary>
        /// Funcion que obtiene el registro de certificados físicos
        /// </summary>
        /// <param name="Id">Id de un registro de la tabla mkt.T_SolicitudCertificadoFisico</param>
        /// <returns>Objeto del tipo DatosRegistroEnvioFisico</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerDatosReporteCertificadoEnvioFisico()
        {
            try
            {
                SolicitudCertificadoFisicoRepositorio _repSolicitudCertificadoFisico = new SolicitudCertificadoFisicoRepositorio(_integraDBContext);

                var rpta = _repSolicitudCertificadoFisico.DatosReporteCertificadoEnvioFisico();
                return Ok(rpta);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        
        /// TipoFuncion: GET
        /// Autor: Max Mantilla
        /// Fecha: 03/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene datos del reporte de certificados físicos, según el CodigoMatricula
        /// </summary>
        /// <returns>List<CourierDetalleDTO><returns>
        [Route("[Action]/{CodigoMatricula}")]
        [HttpGet]
        public ActionResult DatosReporteCertificadoEnvioFisicoPorId(string CodigoMatricula)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                SolicitudCertificadoFisicoRepositorio _repReporteCertificadoFisico = new SolicitudCertificadoFisicoRepositorio(_integraDBContext);
                return Ok(_repReporteCertificadoFisico.DatosReporteCertificadoEnvioFisicoPorId(CodigoMatricula));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        /// TipoFuncion: POST
        /// Autor: Miguel Mora
        /// Fecha: 26/11/2021
        /// Versión: 1.0
        /// <summary>
        /// Edita los valores de la tabla de solicitudes de certofocados fisicos por un archivo excel
		/// </summary>
		/// <returns>Objeto<returns>
		[Route("[Action]")]
        [HttpPost]
        public ActionResult ImportarDatos([FromForm] IFormFile ArchivoExcel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                PlantillaOperacionesController _PlantillaOperacionesController = new PlantillaOperacionesController(_integraDBContext);
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                SolicitudCertificadoFisicoRepositorio _repSolicitudCertificadoFisico = new SolicitudCertificadoFisicoRepositorio(_integraDBContext);
                CertificadoGeneradoAutomaticoRepositorio _repCertificadoGeneradoAutomaticoRepositorio = new CertificadoGeneradoAutomaticoRepositorio(_integraDBContext);
                FurRepositorio _repFurRepositorio = new FurRepositorio(_integraDBContext);
                CourierRepositorio _repCourierRepositorio = new CourierRepositorio(_integraDBContext);
                CourierDetalleRepositorio _repCourierDetalleRepositorio = new CourierDetalleRepositorio(_integraDBContext);
                IntegraAspNetUsersRepositorio _IntegraAspNetUsersRepositorio = new IntegraAspNetUsersRepositorio(_integraDBContext);
                UsuarioRepositorio _UsuarioRepositorio = new UsuarioRepositorio(_integraDBContext);
                AlumnoRepositorio _AlumnoRepositorio = new AlumnoRepositorio(_integraDBContext);
                var ListaSolicitudes = new List<SolicitudCertificadoFisicoBO>();
                int errores = 0, correctos = 0;
                int index = 0;
                using (var cvs = new CsvReader(new StreamReader(ArchivoExcel.OpenReadStream())))
                {
                    cvs.Configuration.Delimiter = ";";
                    cvs.Configuration.MissingFieldFound = null;
                    cvs.Configuration.BadDataFound = null;
                    cvs.Read();
                    cvs.ReadHeader();
                    while (cvs.Read())
                    {
                        index++;
                        //var certificadoDigital= _repCertificadoGeneradoAutomaticoRepositorio.FirstBy(x=>x.NombreArchivo==cvs.GetField<string>("Nombre Archivo") && x.Estado==true);
                        var codigomatricula = _repMatriculaCabecera.FirstBy(x => x.CodigoMatricula == cvs.GetField<string>("Codigo Matricula") && x.Estado == true);
                        /*if (certificadoDigital == null)
                        {
                            errores++;
                        }*/
                        if (codigomatricula == null)
                        {
                            errores++;
                        }
                        else
                        {

                            //SolicitudCertificadoFisicoBO solicitudCertificadoFisicoBO = _repSolicitudCertificadoFisico.FirstBy(x => x.IdCertificadoGeneradoAutomatico == certificadoDigital.Id && x.Estado==true);
                            SolicitudCertificadoFisicoBO solicitudCertificadoFisicoBO = _repSolicitudCertificadoFisico.FirstBy(x => x.IdMatriculaCabecera == codigomatricula.Id && x.Estado == true);

                            if (solicitudCertificadoFisicoBO == null)
                            {
                                errores++;
                            }
                            else
                            {
                                var matricula = _repMatriculaCabecera.FirstById(solicitudCertificadoFisicoBO.IdMatriculaCabecera);
                                var fur = _repFurRepositorio.FirstBy(x => x.Codigo == cvs.GetField<string>("FUR"));
                                if (fur == null)
                                {
                                    errores++;
                                }
                                else
                                {
                                    solicitudCertificadoFisicoBO.IdFur = fur.Id;
                                    var couriernombre = cvs.GetField<string>("Courier").Trim();
                                    var pais = cvs.GetField<string>("Pais remitente").Trim();
                                    var ciudad = cvs.GetField<string>("Ciudad remitente").Trim();
                                    var courier = _repCourierRepositorio.ObtenerCourierPorNombre(couriernombre, pais, ciudad);
                                    if (courier == null)
                                    {
                                        errores++;
                                    }
                                    else
                                    {
                                        solicitudCertificadoFisicoBO.IdCourier = courier.Id;
                                        var courierDetalle = _repCourierDetalleRepositorio.ObtenerCourierDetallePorNombre((int)courier.Id, cvs.GetField<string>("Pais consignado").Trim(), cvs.GetField<string>("Ciudad consignada").Trim());
                                        if (courierDetalle == null)
                                        {
                                            errores++;
                                        }
                                        else
                                        {

                                            solicitudCertificadoFisicoBO.IdPaisConsignado = courierDetalle.IdPais;
                                            solicitudCertificadoFisicoBO.IdCiudadConsignada = courierDetalle.IdCiudad;
                                            solicitudCertificadoFisicoBO.CodigoSeguimiento = cvs.GetField<string>("Codigo de seguimiento");
                                            solicitudCertificadoFisicoBO.EstadoCourier = cvs.GetField<string>("Estado de courier");
                                            solicitudCertificadoFisicoBO.FechaEntregaCourier = cvs.GetField<string>("Fecha de entrega courier") == "" ? DateTime.Today : cvs.GetField<DateTime>("Fecha de entrega courier");
                                            solicitudCertificadoFisicoBO.UsuarioModificacion = "IMPORT";
                                            solicitudCertificadoFisicoBO.FechaModificacion = DateTime.Now;
                                            _repSolicitudCertificadoFisico.Update(solicitudCertificadoFisicoBO);
                                            if (!string.IsNullOrEmpty(solicitudCertificadoFisicoBO.CodigoSeguimiento.Trim()))
                                            {
                                                DatosRegistroEnvioFisico datos = new DatosRegistroEnvioFisico();
                                                var datosMatricula = _repMatriculaCabecera.FirstById(matricula.Id);
                                                datos.IdMatriculaCabecera = matricula.Id;
                                                datos.IdAlumno = datosMatricula.IdAlumno;
                                                datos.Courier = cvs.GetField<string>("Courier").Trim();
                                                datos.CodigoSeguimiento = solicitudCertificadoFisicoBO.CodigoSeguimiento;
                                                var correoAlumno = _AlumnoRepositorio.ObtenerDatosAlumnoPorId(datos.IdAlumno);
                                                var usuarioCoordinadora = _repMatriculaCabecera.ObtenerIdAlumnoCoordinadorAcademico(datos.IdMatriculaCabecera);
                                                var correoCoordinadora = _IntegraAspNetUsersRepositorio.ObtenerEmailPorNombreUsuario(usuarioCoordinadora.UsuarioCoordinadorAcademico);
                                                var user = _UsuarioRepositorio.FirstBy(x => x.NombreUsuario == usuarioCoordinadora.UsuarioCoordinadorAcademico);
                                                if (user != null)
                                                {
                                                    datos.IdPersonal = user.IdPersonal;
                                                }

                                                var idPlantilla = 1454;
                                                var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                                                {
                                                    IdPlantilla = idPlantilla
                                                };
                                                _reemplazoEtiquetaPlantilla.ReemplazarEtiquetasEnvioCorreoSolicitudEnvioFiscio(datos);

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
                                                var alumno = _AlumnoRepositorio.FirstById(datos.IdAlumno);
                                                var alumnoNumero = _AlumnoRepositorio.ObtenerNumeroWhatsApp(alumno.IdCodigoPais.Value, alumno.Celular);
                                                contact.contacts.Add("+" + alumnoNumero);

                                                var respuestaw = _PlantillaOperacionesController.Envio(correoAlumno.Email1, matricula.CodigoMatricula, alumnoNumero, 1462);
                                            };

                                            correctos++;

                                        }

                                    }

                                }
                            }
                        }

                    }
                }

                return Ok(new { correctos = correctos, errores = errores });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("[Action]/{Id}")]
        [HttpGet]
        public ActionResult EnvioCorreoPlantillaSolicitudCertificadoFisico(int Id)
        {

            SolicitudCertificadoFisicoRepositorio _repSolicitudCertificadoFisico = new SolicitudCertificadoFisicoRepositorio(_integraDBContext);
            AlumnoRepositorio _AlumnoRepositorio = new AlumnoRepositorio(_integraDBContext);
            MatriculaCabeceraRepositorio _MatriculaCabeceraRepositorio = new MatriculaCabeceraRepositorio(_integraDBContext);
            IntegraAspNetUsersRepositorio _IntegraAspNetUsersRepositorio = new IntegraAspNetUsersRepositorio(_integraDBContext);
            var _PlantillaOperacionesController = new PlantillaOperacionesController(_integraDBContext);


            var rpta = _repSolicitudCertificadoFisico.DatosRegistroEnvioFisico(Id);
            var correoAlumno = _AlumnoRepositorio.ObtenerDatosAlumnoPorId(rpta.IdAlumno);
            var usuarioCoordinadora = _MatriculaCabeceraRepositorio.ObtenerIdAlumnoCoordinadorAcademico(rpta.IdMatriculaCabecera);
            var correoCoordinadora = _IntegraAspNetUsersRepositorio.ObtenerEmailPorNombreUsuario(usuarioCoordinadora.UsuarioCoordinadorAcademico);
            //var idPlantilla = 1391;
            var idPlantilla = 1455;
            var _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
            {
                IdPlantilla = idPlantilla
            };
            rpta.IdPersonal = 4819;

            AlumnoBO alumnoBO = new AlumnoBO();
            ValidarNumerosWhatsAppAsyncDTO contact = new ValidarNumerosWhatsAppAsyncDTO();
            contact.contacts = new List<string>();
            var alumno = _AlumnoRepositorio.FirstById(rpta.IdAlumno);
            var alumnoNumero = _AlumnoRepositorio.ObtenerNumeroWhatsApp(alumno.IdCodigoPais.Value, alumno.Celular);
            contact.contacts.Add("+" + "51940802005");
            //var validacion = alumnoBO.ValidarNumeroEnvioWhatsApp((int)rpta.IdPersonal, alumno.IdCodigoPais.Value, contact);

            var respuestaw = _PlantillaOperacionesController.Envio("mmora@bsginstitute.com", "10053168A18387", alumnoNumero, 935);
            _reemplazoEtiquetaPlantilla.ReemplazarEtiquetasEnvioCorreoSolicitudEnvioFiscio(rpta);

            var emailCalculado = _reemplazoEtiquetaPlantilla.EmailReemplazado;
            List<string> correosPersonalizadosCopiaOculta = new List<string>
                {
                    "lhuallpa@bsginstitute.com",
                };


            TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
            {
                Sender = correoCoordinadora,
                //Sender = personal.Email,
                Recipient = "mmora@bsginstitute.com",
                Subject = emailCalculado.Asunto,
                Message = emailCalculado.CuerpoHTML,
                Cc = "",
                Bcc = string.Join(",", "mmora@bsginstitute.com"),
                AttachedFiles = emailCalculado.ListaArchivosAdjuntos
            };
            var mailServie = new TMK_MailServiceImpl();

            mailServie.SetData(mailDataPersonalizado);
            mailServie.SendMessageTask();

            return Ok(true);
        }
    }
}
