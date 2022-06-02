using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.DTOs.Transversal;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: CertificadoGeneracionAutomatica
    /// Autor: Fischer Valdez - Priscila Pacsi - Miguel Mora
    /// Fecha: 01/10/2021        
    /// <summary>
    /// Controlador de Monto Pago Cronograma
    /// </summary>
    [Route("api/CertificadoGeneracionAutomatica")]
    public class CertificadoGeneracionAutomaticaController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public CertificadoGeneracionAutomaticaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        // GET: api/<controller>
        [Route("[Action]")]
        [HttpGet]
        public IActionResult ProcesoCertificadosAutomaticos()
        {
            PgeneralConfiguracionPlantillaRepositorio _repPgeneralConfiguracionPlantilla = new PgeneralConfiguracionPlantillaRepositorio(_integraDBContext);
            CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoRepositorio _repCertificadoGeneradoAutomatico = new CertificadoGeneradoAutomaticoRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoContenidoRepositorio _repCertificadoGeneradoAutomaticoContenido = new CertificadoGeneradoAutomaticoContenidoRepositorio(_integraDBContext);
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
            //var datosGenerarNuevamente;//=Falta implementar

            var dato = _repPgeneralConfiguracionPlantilla.ObtenerDatosParaCertificados();

            foreach (var item in dato)
            {
                try
                {
                    DocumentosBO documentosBO = new DocumentosBO();
                    int Idplantillabase = 0;
                    string codigoCertificado = "";
                    var documentoByte = documentosBO.GenerarVistaPreviaCertificado(item.IdPlantillaFrontal, item.IdPlantillaPosterior ?? default(int), item.IdOportunidad,ref Idplantillabase , ref codigoCertificado);                    

                    CertificadoGeneradoAutomaticoBO certificadoGenerado = new CertificadoGeneradoAutomaticoBO();
                    certificadoGenerado.IdMatriculaCabecera = item.IdMatriculaCabecera;
                    certificadoGenerado.IdPgeneral = item.IdPgeneral;
                    certificadoGenerado.IdPgeneralConfiguracionPlantilla = item.IdPgeneralConfiguracionPlantilla;
                    certificadoGenerado.IdUrlBlockStorage = 1;
                    certificadoGenerado.ContentType = "application/pdf";
                    certificadoGenerado.NombreArchivo = Idplantillabase == 12 ? codigoCertificado : documentosBO.ContenidoCertificado.CorrelativoConstancia.ToString();
                    certificadoGenerado.IdPespecifico = item.IdPespecifico;
                    certificadoGenerado.IdPlantilla = item.IdPlantillaFrontal;
                    certificadoGenerado.FechaEmision = DateTime.Now;
                    certificadoGenerado.FechaCreacion = DateTime.Now;
                    certificadoGenerado.FechaModificacion = DateTime.Now;
                    certificadoGenerado.UsuarioCreacion = "SYSTEM";
                    certificadoGenerado.UsuarioModificacion = "SYSTEM";
                    certificadoGenerado.Estado = true;
                    _repCertificadoGeneradoAutomatico.Insert(certificadoGenerado);

                    CertificadoGeneradoAutomaticoContenidoBO contenidoCertificadoBO = documentosBO.ContenidoCertificado;
                    contenidoCertificadoBO.IdCertificadoGeneradoAutomatico = certificadoGenerado.Id;
                    contenidoCertificadoBO.Estado = true;
                    contenidoCertificadoBO.FechaCreacion = DateTime.Now;
                    contenidoCertificadoBO.FechaModificacion = DateTime.Now;
                    contenidoCertificadoBO.UsuarioCreacion = "SYSTEM";
                    contenidoCertificadoBO.UsuarioModificacion = "SYSTEM";

                    _repCertificadoGeneradoAutomaticoContenido.Insert(contenidoCertificadoBO);                    

                    var Url = _repCertificadoDetalle.guardarArchivoCertificado(documentoByte, "application/pdf", certificadoGenerado.NombreArchivo);


                    if (Idplantillabase == 12 )/*12:Certificado*/
                    {
                        var estados = _repMatriculaCabecera.ObtenerEstadosPorPlantillas(Idplantillabase, item.IdPlantillaFrontal, item.IdPlantillaPosterior ?? default(int), item.IdEstadoMatricula, item.IdSubEstadoMatricula);

                        var matriculaBO = _repMatriculaCabecera.FirstById(item.IdMatriculaCabecera);
                        matriculaBO.IdEstadoMatricula = estados.IdEstadoMatricula;
                        matriculaBO.IdSubEstadoMatricula = estados.IdSubEstadoMatricula;
                        matriculaBO.UsuarioModificacion = "Gen-Certi";
                        matriculaBO.FechaModificacion = DateTime.Now;
                        matriculaBO.IdEstadoMatriculaCertificado = matriculaBO.IdEstadoMatricula;
                        matriculaBO.IdSubEstadoMatriculaCertificado = matriculaBO.IdSubEstadoMatricula;
                        _repMatriculaCabecera.Update(matriculaBO);
                    }
                }
                catch (Exception ex)
                {
                    List<string> correos = new List<string>
                    {
                        "fvaldez@bsginstitute.com",
                        "lpacsi@bsginstitute.com"
                    };
                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "fvaldez@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "Error Proceso Certificados",
                        Message = "IdMatricula: " + item.IdMatriculaCabecera.ToString() + ", IdPgeneralConfiguracionPlantilla: " + item.IdPgeneralConfiguracionPlantilla.ToString() + " < br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString(),
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }


            }
            return Ok();
        }
        /// TipoFuncion: GET
        /// Autor: Fischer Valdez - Priscila Pacsi
        /// Fecha: 01/10/2021
        /// Versión: 1.5
        /// <summary>
        /// Genera el certificado digital del alumno
        /// </summary>
        /// <returns> Retorna un objeto Booleano</returns>
        [Route("[Action]/{idAlumno}")]
        [HttpGet]
        public ActionResult GenerarCertificadoPorAlumno(int idAlumno)
        {
            PgeneralConfiguracionPlantillaRepositorio _repPgeneralConfiguracionPlantilla = new PgeneralConfiguracionPlantillaRepositorio(_integraDBContext);
            CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoRepositorio _repCertificadoGeneradoAutomatico = new CertificadoGeneradoAutomaticoRepositorio(_integraDBContext);
            IntegraAspNetUsersRepositorio _IntegraAspNetUsersRepositorio = new IntegraAspNetUsersRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoContenidoRepositorio _repCertificadoGeneradoAutomaticoContenido = new CertificadoGeneradoAutomaticoContenidoRepositorio(_integraDBContext);
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
            List<string> correos = new List<string>();
                   
            List<string> palabra = new List<string>();
            palabra.Add("esquema");
            //var listaCorreos = _repMatriculaCabecera.ConseguirCorreos(palabra);

            //correos.AddRange(listaCorreos);

            var dato = _repPgeneralConfiguracionPlantilla.ObtenerDatosParaCertificadosporAlumno(idAlumno);

            if(dato.Count == 0)
            {
                
                var listaCursoMatriculado = _repMatriculaCabecera.ObtenerListaCursosCulminadosPorAlumno(idAlumno);
                if(listaCursoMatriculado!= null)
                {
                    foreach(var curso in listaCursoMatriculado)
                    {
                        var datosMatricula = _repMatriculaCabecera.ObtenerIdAlumnoCoordinadorAcademico(curso.Id);
                        var correoCoordinadorAcademico = _IntegraAspNetUsersRepositorio.ObtenerEmailPorNombreUsuario(datosMatricula.UsuarioCoordinadorAcademico);

                        var listaCertificado = _repCertificadoGeneradoAutomatico.ObtenerCertificados(curso.Id);
                        bool flag=false;
                        if(listaCertificado.Count >0)
                        {
                            foreach (var nombre in listaCertificado)
                            {
                                if(!nombre.NombreArchivo.Contains("P"))
                                {
                                    flag = true;
                                    break;
                                }
                            };
                            if(!flag)
                            {
                                correos.Add(correoCoordinadorAcademico);
                                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                                TMKMailDataDTO mailData = new TMKMailDataDTO
                                {
                                    Sender = "fvaldez@bsginstitute.com",
                                    Recipient = string.Join(",", correos),
                                    Subject = "Error Proceso Certificados",
                                    Message = "IdMatricula: " + curso.Id.ToString() + "< br/>El alumno no se encuentra en estado o subEstado correcto",
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
                            correos.Add(correoCoordinadorAcademico);
                            TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                            TMKMailDataDTO mailData = new TMKMailDataDTO
                            {
                                Sender = "fvaldez@bsginstitute.com",
                                Recipient = string.Join(",", correos),
                                Subject = "Error Proceso Certificados",
                                Message = "IdMatricula: " + curso.Id.ToString() + "<br/>El alumno no se encuentra en estado o subEstado correcto",
                                Cc = "",
                                Bcc = "",
                                AttachedFiles = null
                            };
                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                        }
                    }
                }
            }
            foreach (var item in dato)
            {
                try
                {
                    var datosMatricula = _repMatriculaCabecera.ObtenerIdAlumnoCoordinadorAcademico(item.IdMatriculaCabecera);
                    var correoCoordinadorAcademico = _IntegraAspNetUsersRepositorio.ObtenerEmailPorNombreUsuario(datosMatricula.UsuarioCoordinadorAcademico);
                    correos.Add(correoCoordinadorAcademico);
                    DocumentosBO documentosBO = new DocumentosBO();
                    int Idplantillabase = 0;
                    string codigoCertificado = "";
                    
                    var documentoByte = documentosBO.GenerarVistaPreviaCertificado(item.IdPlantillaFrontal, item.IdPlantillaPosterior ?? default(int), item.IdOportunidad, ref Idplantillabase, ref codigoCertificado);

                    CertificadoGeneradoAutomaticoBO certificadoGenerado = new CertificadoGeneradoAutomaticoBO();
                    certificadoGenerado.IdMatriculaCabecera = item.IdMatriculaCabecera;
                    certificadoGenerado.IdPgeneral = item.IdPgeneral;
                    certificadoGenerado.IdPgeneralConfiguracionPlantilla = item.IdPgeneralConfiguracionPlantilla;
                    certificadoGenerado.IdUrlBlockStorage = 1;
                    certificadoGenerado.ContentType = "application/pdf";
                    certificadoGenerado.NombreArchivo = Idplantillabase == 12 ? codigoCertificado : documentosBO.ContenidoCertificado.CorrelativoConstancia.ToString();
                    certificadoGenerado.IdPespecifico = item.IdPespecifico;
                    certificadoGenerado.IdPlantilla = item.IdPlantillaFrontal;
                    certificadoGenerado.FechaEmision = DateTime.Now;
                    certificadoGenerado.FechaCreacion = DateTime.Now;
                    certificadoGenerado.FechaModificacion = DateTime.Now;
                    certificadoGenerado.UsuarioCreacion = "SYSTEM";
                    certificadoGenerado.UsuarioModificacion = "SYSTEM";
                    certificadoGenerado.Estado = true;
                    _repCertificadoGeneradoAutomatico.Insert(certificadoGenerado);

                    CertificadoGeneradoAutomaticoContenidoBO contenidoCertificadoBO = documentosBO.ContenidoCertificado;

                    contenidoCertificadoBO.IdCertificadoGeneradoAutomatico = certificadoGenerado.Id;
                    contenidoCertificadoBO.FechaFinCapacitacion = contenidoCertificadoBO.FechaFinCapacitacion == null ? "": contenidoCertificadoBO.FechaFinCapacitacion;
                    contenidoCertificadoBO.Estado = true;
                    contenidoCertificadoBO.FechaCreacion = DateTime.Now;
                    contenidoCertificadoBO.FechaModificacion = DateTime.Now;
                    contenidoCertificadoBO.UsuarioCreacion = "SYSTEM";
                    contenidoCertificadoBO.UsuarioModificacion = "SYSTEM";

                    _repCertificadoGeneradoAutomaticoContenido.Insert(contenidoCertificadoBO);

                    var Url = _repCertificadoDetalle.guardarArchivoCertificado(documentoByte, "application/pdf", certificadoGenerado.NombreArchivo);


                    if (Idplantillabase == 12)/*12:Certificado*/
                    {
                        var estados = _repMatriculaCabecera.ObtenerEstadosPorPlantillas(Idplantillabase, item.IdPlantillaFrontal, item.IdPlantillaPosterior ?? default(int), item.IdEstadoMatricula, item.IdSubEstadoMatricula);

                        var matriculaBO = _repMatriculaCabecera.FirstById(item.IdMatriculaCabecera);
                        matriculaBO.IdEstadoMatricula = estados.IdEstadoMatricula;
                        matriculaBO.IdSubEstadoMatricula = estados.IdSubEstadoMatricula;
                        matriculaBO.UsuarioModificacion = "Gen-Certi";
                        matriculaBO.FechaModificacion = DateTime.Now;
                        matriculaBO.IdEstadoMatriculaCertificado = matriculaBO.IdEstadoMatricula;
                        matriculaBO.IdSubEstadoMatriculaCertificado = matriculaBO.IdSubEstadoMatricula;
                        _repMatriculaCabecera.Update(matriculaBO);
                    }
                }
                catch (Exception ex)
                {
                   
                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "fvaldez@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "Error Proceso Certificados",
                        Message = "IdMatricula: " + item.IdMatriculaCabecera.ToString() + ", IdPgeneralConfiguracionPlantilla: " + item.IdPgeneralConfiguracionPlantilla.ToString() + " < br/>" + ex.Message,
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };
                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }


            }
            return Ok(true);
        }
        [Route("[Action]")]
        [HttpGet]
        public ActionResult GenerarCertificadoModularporProgramaEspecifico(int idMatricula,string listaPespecifico)
        {
            PgeneralConfiguracionPlantillaRepositorio _repPgeneralConfiguracionPlantilla = new PgeneralConfiguracionPlantillaRepositorio(_integraDBContext);
            CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoRepositorio _repCertificadoGeneradoAutomatico = new CertificadoGeneradoAutomaticoRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoContenidoRepositorio _repCertificadoGeneradoAutomaticoContenido = new CertificadoGeneradoAutomaticoContenidoRepositorio(_integraDBContext);
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
            PlantillaRepositorio _repPlantilla = new PlantillaRepositorio(_integraDBContext);

            //var dato = _repPgeneralConfiguracionPlantilla.ObtenerDatosParaCertificadosporAlumno(idAlumno);
            var ListaPespecifo = listaPespecifico.Split(",");
            var plantillaModular = _repPlantilla.ObtenerPlantillaCertificadoModular();
            foreach (var item in ListaPespecifo)
            {
                try
                {
                    DocumentosBO documentosBO = new DocumentosBO();
                    int Idplantillabase = 0;
                    string codigoCertificado = "";
                    int IdPgeneral = 0;
                    var documentoByte = documentosBO.GenerarCertificadoModular(plantillaModular, 1235, idMatricula,0, Convert.ToInt32(item), ref codigoCertificado,ref IdPgeneral, 0);

                    CertificadoGeneradoAutomaticoBO certificadoGenerado = new CertificadoGeneradoAutomaticoBO();
                    certificadoGenerado.IdMatriculaCabecera = idMatricula;
                    certificadoGenerado.IdPgeneral = IdPgeneral;
                    certificadoGenerado.IdPgeneralConfiguracionPlantilla = 1;/*Certificado Modular no tiene Configuracion*/
                    certificadoGenerado.IdUrlBlockStorage = 1;
                    certificadoGenerado.ContentType = "application/pdf";
                    certificadoGenerado.NombreArchivo = codigoCertificado;
                    certificadoGenerado.IdPespecifico = Convert.ToInt32(item);
                    certificadoGenerado.IdPlantilla = plantillaModular;
                    certificadoGenerado.FechaEmision = DateTime.Now;
                    certificadoGenerado.FechaCreacion = DateTime.Now;
                    certificadoGenerado.FechaModificacion = DateTime.Now;
                    certificadoGenerado.UsuarioCreacion = "SYSTEM";
                    certificadoGenerado.UsuarioModificacion = "SYSTEM";
                    certificadoGenerado.Estado = true;
                    _repCertificadoGeneradoAutomatico.Insert(certificadoGenerado);

                    CertificadoGeneradoAutomaticoContenidoBO contenidoCertificadoBO = documentosBO.ContenidoCertificado;
                    contenidoCertificadoBO.IdCertificadoGeneradoAutomatico = certificadoGenerado.Id;
                    contenidoCertificadoBO.FechaFinCapacitacion = contenidoCertificadoBO.FechaFinCapacitacion == null ? "": contenidoCertificadoBO.FechaFinCapacitacion;
                    contenidoCertificadoBO.Estado = true;
                    contenidoCertificadoBO.FechaCreacion = DateTime.Now;
                    contenidoCertificadoBO.FechaModificacion = DateTime.Now;
                    contenidoCertificadoBO.UsuarioCreacion = "SYSTEM";
                    contenidoCertificadoBO.UsuarioModificacion = "SYSTEM";

                    _repCertificadoGeneradoAutomaticoContenido.Insert(contenidoCertificadoBO);

                    var Url = _repCertificadoDetalle.guardarArchivoCertificado(documentoByte, "application/pdf", certificadoGenerado.NombreArchivo);


                    //if (Idplantillabase == 12)/*12:Certificado*/
                    //{
                    //    var estados = _repMatriculaCabecera.ObtenerEstadosPorPlantillas(Idplantillabase, item.IdPlantillaFrontal, item.IdPlantillaPosterior ?? default(int), item.IdEstadoMatricula, item.IdSubEstadoMatricula);

                    //    var matriculaBO = _repMatriculaCabecera.FirstById(item.IdMatriculaCabecera);
                    //    matriculaBO.IdEstadoMatricula = estados.IdEstadoMatricula;
                    //    matriculaBO.IdSubEstadoMatricula = estados.IdSubEstadoMatricula;
                    //    matriculaBO.UsuarioModificacion = "Gen-Certi";
                    //    matriculaBO.FechaModificacion = DateTime.Now;
                    //    _repMatriculaCabecera.Update(matriculaBO);
                    //}
                }
                catch (Exception ex)
                {
                    List<string> correos = new List<string>
                    {
                        "fvaldez@bsginstitute.com",
                        "lpacsi@bsginstitute.com"
                    };
                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "fvaldez@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "Error Proceso Certificado modular por pespecifico",
                        Message = "IdMatricula: " + idMatricula.ToString() + ", plantilla: " + plantillaModular.ToString() + " < br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString(),
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }


            }
            return Ok(true);
        }
        [Route("[Action]")]
        [HttpGet]
        public ActionResult GenerarCertificadoModularporCursoMoodle(int idMatricula,string listaCursoMoodle,int IdProgramaGeneral)
        {
            PgeneralConfiguracionPlantillaRepositorio _repPgeneralConfiguracionPlantilla = new PgeneralConfiguracionPlantillaRepositorio(_integraDBContext);
            CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoRepositorio _repCertificadoGeneradoAutomatico = new CertificadoGeneradoAutomaticoRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoContenidoRepositorio _repCertificadoGeneradoAutomaticoContenido = new CertificadoGeneradoAutomaticoContenidoRepositorio(_integraDBContext);
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
            PlantillaRepositorio _repPlantilla = new PlantillaRepositorio(_integraDBContext);

            //var dato = _repPgeneralConfiguracionPlantilla.ObtenerDatosParaCertificadosporAlumno(idAlumno);
            var ListaCursoMoodle = listaCursoMoodle.Split(",");
            var plantillaModular = _repPlantilla.ObtenerPlantillaCertificadoModular();
            foreach (var item in ListaCursoMoodle)
            {
                try
                {
                    DocumentosBO documentosBO = new DocumentosBO();
                    int Idplantillabase = 0;
                    string codigoCertificado = "";
                    int IdPgeneral = 0;
                    var documentoByte = documentosBO.GenerarCertificadoModular(plantillaModular, 1235, idMatricula, 1, Convert.ToInt32(item), ref codigoCertificado, ref IdPgeneral, IdProgramaGeneral);

                    CertificadoGeneradoAutomaticoBO certificadoGenerado = new CertificadoGeneradoAutomaticoBO();
                    certificadoGenerado.IdMatriculaCabecera = idMatricula;
                    certificadoGenerado.IdPgeneral = IdPgeneral;
                    certificadoGenerado.IdPgeneralConfiguracionPlantilla = 0;/*Certificado Modular no tiene Configuracion*/
                    certificadoGenerado.IdUrlBlockStorage = 1;
                    certificadoGenerado.ContentType = "application/pdf";
                    certificadoGenerado.NombreArchivo = codigoCertificado;
                    certificadoGenerado.IdPespecifico = Convert.ToInt32(item);
                    certificadoGenerado.IdPlantilla = plantillaModular;
                    certificadoGenerado.FechaEmision = DateTime.Now;
                    certificadoGenerado.FechaCreacion = DateTime.Now;
                    certificadoGenerado.FechaModificacion = DateTime.Now;
                    certificadoGenerado.UsuarioCreacion = "SYSTEM";
                    certificadoGenerado.UsuarioModificacion = "SYSTEM";
                    certificadoGenerado.Estado = true;
                    _repCertificadoGeneradoAutomatico.Insert(certificadoGenerado);

                    CertificadoGeneradoAutomaticoContenidoBO contenidoCertificadoBO = documentosBO.ContenidoCertificado;
                    contenidoCertificadoBO.IdCertificadoGeneradoAutomatico = certificadoGenerado.Id;
                    contenidoCertificadoBO.FechaFinCapacitacion = contenidoCertificadoBO.FechaFinCapacitacion == null ? "": contenidoCertificadoBO.FechaFinCapacitacion;
                    contenidoCertificadoBO.Estado = true;
                    contenidoCertificadoBO.FechaCreacion = DateTime.Now;
                    contenidoCertificadoBO.FechaModificacion = DateTime.Now;
                    contenidoCertificadoBO.UsuarioCreacion = "SYSTEM";
                    contenidoCertificadoBO.UsuarioModificacion = "SYSTEM";

                    _repCertificadoGeneradoAutomaticoContenido.Insert(contenidoCertificadoBO);

                    var Url = _repCertificadoDetalle.guardarArchivoCertificado(documentoByte, "application/pdf", certificadoGenerado.NombreArchivo);                    
                }
                catch (Exception ex)
                {
                    List<string> correos = new List<string>
                    {
                        "fvaldez@bsginstitute.com",
                        "lpacsi@bsginstitute.com"
                    };
                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "fvaldez@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "Error Proceso Certificados modular por cursomodle",
                        Message = "IdMatricula: " + idMatricula.ToString() + ", plantilla: " + plantillaModular.ToString() + " < br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString(),
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }


            }
            return Ok(true);
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult GenerarCertificadoModularporCursoMoodleList([FromBody] CertificadoModularDTO ObjetoDTO)
        {
            PgeneralConfiguracionPlantillaRepositorio _repPgeneralConfiguracionPlantilla = new PgeneralConfiguracionPlantillaRepositorio(_integraDBContext);
            CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoRepositorio _repCertificadoGeneradoAutomatico = new CertificadoGeneradoAutomaticoRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoContenidoRepositorio _repCertificadoGeneradoAutomaticoContenido = new CertificadoGeneradoAutomaticoContenidoRepositorio(_integraDBContext);
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
            PlantillaRepositorio _repPlantilla = new PlantillaRepositorio(_integraDBContext);

            //var dato = _repPgeneralConfiguracionPlantilla.ObtenerDatosParaCertificadosporAlumno(idAlumno);
            var ListaCursoMoodle = ObjetoDTO.Cursos;
            var plantillaModular = _repPlantilla.ObtenerPlantillaCertificadoModular();
            foreach (var item in ListaCursoMoodle)
            {
                try
                {
                    DocumentosBO documentosBO = new DocumentosBO();
                    int Idplantillabase = 0;
                    string codigoCertificado = "";
                    int IdPgeneral = 0;
                    var documentoByte = documentosBO.GenerarCertificadoModular(plantillaModular, 1235, ObjetoDTO.IdMatriculaCabacera, 1, Convert.ToInt32(item.IdCursoMoodle), ref codigoCertificado, ref IdPgeneral, item.IdPeneral);

                    CertificadoGeneradoAutomaticoBO certificadoGenerado = new CertificadoGeneradoAutomaticoBO();
                    certificadoGenerado.IdMatriculaCabecera = ObjetoDTO.IdMatriculaCabacera;
                    certificadoGenerado.IdPgeneral = IdPgeneral;
                    certificadoGenerado.IdPgeneralConfiguracionPlantilla = 0;/*Certificado Modular no tiene Configuracion*/
                    certificadoGenerado.IdUrlBlockStorage = 1;
                    certificadoGenerado.ContentType = "application/pdf";
                    certificadoGenerado.NombreArchivo = codigoCertificado;
                    certificadoGenerado.IdPespecifico = Convert.ToInt32(item.IdCursoMoodle);
                    certificadoGenerado.IdPlantilla = plantillaModular;
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
                }
                catch (Exception ex)
                {
                    List<string> correos = new List<string>
                    {
                        "fvaldez@bsginstitute.com",
                        "lpacsi@bsginstitute.com"
                    };
                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "fvaldez@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "Error Proceso Certificados modular por cursomodle",
                        Message = "IdMatricula: " + ObjetoDTO.IdMatriculaCabacera.ToString() + ", plantilla: " + plantillaModular.ToString() + " < br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString(),
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };

                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }


            }
            return Ok(true);
        }
        [Route("[Action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public IActionResult ObtenerCertificadosPorMatricula(int IdMatriculaCabecera)
        {
            CertificadoGeneradoAutomaticoRepositorio _repCertificadoGeneradoAutomatico = new CertificadoGeneradoAutomaticoRepositorio();

            var rpta = _repCertificadoGeneradoAutomatico.ObtenerCertificadosporMatricula(IdMatriculaCabecera);

            return Ok(rpta);
        }
        [Route("[action]/{idMatricula}/{codigoCertificado}")]
        [HttpGet]
        public ActionResult ObtenerInformacionCertificado(int idMatricula, string codigoCertificado)
        {
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio();

                var DatosCertificado = _repMatriculaCabecera.ObtenerInformacionAlumnoCertificado(idMatricula, codigoCertificado);
                

                return Ok(DatosCertificado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]/{idMatricula}/{idDocumento}")]
        [HttpGet]
        public ActionResult ObtenerInformacionConstancia(int idMatricula, string idDocumento)
        {
            try
            {
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio();

                var DatosCertificado = _repMatriculaCabecera.ObtenerInformacionAlumnoConstancia(idMatricula , idDocumento);
                return Ok(DatosCertificado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult GenerarCertificadoIrca(int IdMatriculaCabecera)
        {
            try
            {
                ContenidoCertificadoIrcaRepositorio _repContenidoCertificadoIrca = new ContenidoCertificadoIrcaRepositorio(_integraDBContext);
                PgeneralConfiguracionPlantillaRepositorio _repPgeneralConfiguracionPlantilla = new PgeneralConfiguracionPlantillaRepositorio(_integraDBContext);
                CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
                CertificadoGeneradoAutomaticoRepositorio _repCertificadoGeneradoAutomatico = new CertificadoGeneradoAutomaticoRepositorio(_integraDBContext);
                CertificadoGeneradoAutomaticoContenidoRepositorio _repCertificadoGeneradoAutomaticoContenido = new CertificadoGeneradoAutomaticoContenidoRepositorio(_integraDBContext);
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);

                var ObtenerCertificadosIrca = _repContenidoCertificadoIrca.ObtenerDatosParaCertificadosIrca(IdMatriculaCabecera);
                foreach (var item in ObtenerCertificadosIrca)
                {
                    try
                    {
                        DocumentosBO documentosBO = new DocumentosBO();
                        int Idplantillabase = 0;
                        int IdPgeneral = 0;
                        string codigoCertificado = "";

                        var contenidoCertificadoIrcaBO = _repContenidoCertificadoIrca.FirstById(item.Id);

                        var documentoByte = documentosBO.GenerarCertificadoIrca(item.IdPlantillaFrontal,item.Id, item.IdPespecifico, ref codigoCertificado, ref IdPgeneral);

                        CertificadoGeneradoAutomaticoBO certificadoGenerado = new CertificadoGeneradoAutomaticoBO();
                        certificadoGenerado.IdMatriculaCabecera = item.IdMatriculaCabecera;
                        certificadoGenerado.IdPgeneral = item.IdPgeneral;
                        certificadoGenerado.IdPgeneralConfiguracionPlantilla = item.IdPgeneralConfiguracionPlantilla;
                        certificadoGenerado.IdUrlBlockStorage = 1;
                        certificadoGenerado.ContentType = "application/pdf";
                        certificadoGenerado.NombreArchivo = codigoCertificado;
                        certificadoGenerado.IdPespecifico = item.IdPespecifico;
                        certificadoGenerado.IdPlantilla = item.IdPlantillaFrontal;
                        certificadoGenerado.FechaEmision = DateTime.Now;
                        certificadoGenerado.FechaCreacion = DateTime.Now;
                        certificadoGenerado.FechaModificacion = DateTime.Now;
                        certificadoGenerado.UsuarioCreacion = "SYSTEM";
                        certificadoGenerado.UsuarioModificacion = "SYSTEM";
                        certificadoGenerado.Estado = true;
                        _repCertificadoGeneradoAutomatico.Insert(certificadoGenerado);

                        CertificadoGeneradoAutomaticoContenidoBO contenidoCertificadoBO = documentosBO.ContenidoCertificado;
                        contenidoCertificadoBO.IdCertificadoGeneradoAutomatico = certificadoGenerado.Id;
                        contenidoCertificadoBO.Ciudad = contenidoCertificadoBO.Ciudad == null? "Lima": contenidoCertificadoBO.Ciudad;
                        contenidoCertificadoBO.Estado = true;
                        contenidoCertificadoBO.FechaCreacion = DateTime.Now;
                        contenidoCertificadoBO.FechaModificacion = DateTime.Now;
                        contenidoCertificadoBO.UsuarioCreacion = "SYSTEM";
                        contenidoCertificadoBO.UsuarioModificacion = "SYSTEM";

                        _repCertificadoGeneradoAutomaticoContenido.Insert(contenidoCertificadoBO);

                        
                        contenidoCertificadoIrcaBO.Procesado = true;
                        contenidoCertificadoIrcaBO.UsuarioModificacion = "Gen-Auto";
                        contenidoCertificadoIrcaBO.FechaModificacion = DateTime.Now;

                        _repContenidoCertificadoIrca.Update(contenidoCertificadoIrcaBO);

                        var Url = _repCertificadoDetalle.guardarArchivoCertificado(documentoByte, "application/pdf", certificadoGenerado.NombreArchivo);                        
                    }
                    catch (Exception ex)
                    {
                        List<string> correos = new List<string>
                        {
                            "fvaldez@bsginstitute.com",
                            "lpacsi@bsginstitute.com"
                        };
                        TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                        TMKMailDataDTO mailData = new TMKMailDataDTO
                        {
                            Sender = "fvaldez@bsginstitute.com",
                            Recipient = string.Join(",", correos),
                            Subject = "Error Proceso Certificados Irca",
                            Message = "IdMatricula: " + item.IdMatriculaCabecera.ToString() + ", IdPgeneralConfiguracionPlantilla: " + item.IdPgeneralConfiguracionPlantilla.ToString() + " < br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString(),
                            Cc = "",
                            Bcc = "",
                            AttachedFiles = null
                        };

                        Mailservice.SetData(mailData);
                        Mailservice.SendMessageTask();
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]/{IdMatriculaCabecera}/{IdPlantilla}/{id}")]
        [HttpGet]
        public ActionResult GenerarConstanciaPorMatricula(int IdMatriculaCabecera, int IdPlantilla, int id)
        {
            PgeneralConfiguracionPlantillaRepositorio _repPgeneralConfiguracionPlantilla = new PgeneralConfiguracionPlantillaRepositorio(_integraDBContext);
            CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoRepositorio _repCertificadoGeneradoAutomatico = new CertificadoGeneradoAutomaticoRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoContenidoRepositorio _repCertificadoGeneradoAutomaticoContenido = new CertificadoGeneradoAutomaticoContenidoRepositorio(_integraDBContext);
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
            TarifarioRepositorio _repTarifarioRepositorio = new TarifarioRepositorio(_integraDBContext);

            var dato = _repPgeneralConfiguracionPlantilla.ObtenerDatosParaConstanciasPorMatricula(IdMatriculaCabecera);
            
            try
            {
                DocumentosBO documentosBO = new DocumentosBO();
                int Idplantillabase = 0;
                string codigoCertificado = "";
                var documentoByte = documentosBO.GenerarVistaPreviaCertificado(IdPlantilla, 0, dato.IdOportunidad, ref Idplantillabase, ref codigoCertificado);

                CertificadoGeneradoAutomaticoBO certificadoGenerado = new CertificadoGeneradoAutomaticoBO();
                certificadoGenerado.IdMatriculaCabecera = dato.IdMatriculaCabecera;
                certificadoGenerado.IdPgeneral = dato.IdPgeneral;
                certificadoGenerado.IdPgeneralConfiguracionPlantilla = dato.IdPgeneralConfiguracionPlantilla;
                certificadoGenerado.IdUrlBlockStorage = 1;
                certificadoGenerado.ContentType = "application/pdf";
                certificadoGenerado.NombreArchivo = Idplantillabase == 12 ? codigoCertificado : documentosBO.ContenidoCertificado.CorrelativoConstancia.ToString();
                certificadoGenerado.IdPespecifico = dato.IdPespecifico;
                certificadoGenerado.IdPlantilla = IdPlantilla;
                certificadoGenerado.FechaEmision = DateTime.Now;
                certificadoGenerado.FechaCreacion = DateTime.Now;
                certificadoGenerado.FechaModificacion = DateTime.Now;
                certificadoGenerado.UsuarioCreacion = "SYSTEM";
                certificadoGenerado.UsuarioModificacion = "SYSTEM";
                certificadoGenerado.Estado = true;
                certificadoGenerado.IdCronogramaPagoTarifario = id;
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

                //Consulta update -> id 
                var actualizar = _repTarifarioRepositorio.ActualizarGestionadoCronogramaPagoTarifario(id);
                if (!actualizar)
                {
                    return BadRequest();
                }
                //[fin].[SP_ActualizarGestionadoCronogramaPagoTarifario]
                

            }
            catch (Exception ex)
            {
                List<string> correos = new List<string>
                    {
                        "fvaldez@bsginstitute.com",
                        "lpacsi@bsginstitute.com"
                    };
                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "fvaldez@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = "Error Proceso Constancia por matricula",
                    Message = "IdMatricula: " + dato.IdMatriculaCabecera.ToString() + ", IdPgeneralConfiguracionPlantilla: " + dato.IdPgeneralConfiguracionPlantilla.ToString() + " < br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString(),
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };

                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();
            }
            return Ok(true);


        }
        [Route("[Action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult GenerarCertificadoSinFondo(int IdMatriculaCabecera)
        {
            PgeneralConfiguracionPlantillaRepositorio _repPgeneralConfiguracionPlantilla = new PgeneralConfiguracionPlantillaRepositorio(_integraDBContext);
            CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoRepositorio _repCertificadoGeneradoAutomatico = new CertificadoGeneradoAutomaticoRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoContenidoRepositorio _repCertificadoGeneradoAutomaticoContenido = new CertificadoGeneradoAutomaticoContenidoRepositorio(_integraDBContext);
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);

            var dato = _repCertificadoGeneradoAutomaticoContenido.ObtenerDatosParaCertificadoSinFondo(IdMatriculaCabecera);

            foreach (var item in dato)
            {
                try
                {
                    DocumentosBO documentosBO = new DocumentosBO();
                    int Idplantillabase = 0;
                    string codigoCertificado = "";
                    var documentoByte = documentosBO.GenerarCertificadoSinFondo(item);     

                    var Url = _repCertificadoDetalle.guardarArchivoCertificado(documentoByte, "application/pdf", item.CodigoCertificado+"IMP");
                }
                catch (Exception ex)
                {
                    List<string> correos = new List<string>
                    {
                        "fvaldez@bsginstitute.com",
                        "lpacsi@bsginstitute.com"
                    };
                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "fvaldez@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "Error Proceso Certificados",
                        Message = "IdMatricula: " + item.IdMatriculaCabecera.ToString()  + " < br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString(),
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };
                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }


            }
            return Ok(true);
        }
        /// TipoFuncion: GET
        /// Autor: Abelson Quiñones - Priscila Pacsi
        /// Fecha: 01/10/2021
        /// Versión: 1.5
        /// <summary>
        /// Genera el certificado digital del alumno del nuevo aula virtual
        /// </summary>
        /// <returns> Retorna un objeto Booleano</returns>
        [Route("[Action]/{idAlumno}")]
        [HttpGet]
        public ActionResult GenerarCertificadoPorAlumnoPortalWeb(int idAlumno)
        {
            PgeneralConfiguracionPlantillaRepositorio _repPgeneralConfiguracionPlantilla = new PgeneralConfiguracionPlantillaRepositorio(_integraDBContext);
            CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoRepositorio _repCertificadoGeneradoAutomatico = new CertificadoGeneradoAutomaticoRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoContenidoRepositorio _repCertificadoGeneradoAutomaticoContenido = new CertificadoGeneradoAutomaticoContenidoRepositorio(_integraDBContext);
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
            IntegraAspNetUsersRepositorio _IntegraAspNetUsersRepositorio = new IntegraAspNetUsersRepositorio(_integraDBContext);
            List<string> correos = new List<string>();
            List<string> palabra = new List<string>();
            palabra.Add("esquema");
            var listaCorreos = _repMatriculaCabecera.ConseguirCorreos(palabra);
            correos.AddRange(listaCorreos);

            var dato = _repPgeneralConfiguracionPlantilla.ObtenerDatosParaCertificadosporAlumnoPortalWeb(idAlumno);
            if (dato.Count == 0)
            {

                var listaCursoMatriculado = _repMatriculaCabecera.ObtenerListaCursosCulminadosPorAlumno(idAlumno);
                if (listaCursoMatriculado != null)
                {
                    foreach (var curso in listaCursoMatriculado)
                    {
                        var datosMatricula = _repMatriculaCabecera.ObtenerIdAlumnoCoordinadorAcademico(curso.Id);
                        var correoCoordinadorAcademico = _IntegraAspNetUsersRepositorio.ObtenerEmailPorNombreUsuario(datosMatricula.UsuarioCoordinadorAcademico);

                        var listaCertificado = _repCertificadoGeneradoAutomatico.ObtenerCertificados(curso.Id);
                        bool flag = false;
                        if (listaCertificado.Count > 0)
                        {
                            foreach (var nombre in listaCertificado)
                            {
                                if (!nombre.NombreArchivo.Contains("P"))
                                {
                                    flag = true;
                                    break;
                                }
                            };
                            if (!flag)
                            {
                                correos.Add(correoCoordinadorAcademico);
                                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                                TMKMailDataDTO mailData = new TMKMailDataDTO
                                {
                                    Sender = "fvaldez@bsginstitute.com",
                                    Recipient = string.Join(",", correos),
                                    Subject = "Error Proceso Certificados",
                                    Message = "IdMatricula: " + curso.Id.ToString() + "< br/>El alumno no se encuentra en estado o subEstado correcto",
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
                            correos.Add(correoCoordinadorAcademico);
                            TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                            TMKMailDataDTO mailData = new TMKMailDataDTO
                            {
                                Sender = "fvaldez@bsginstitute.com",
                                Recipient = string.Join(",", correos),
                                Subject = "Error Proceso Certificados",
                                Message = "IdMatricula: " + curso.Id.ToString() + "<br/>El alumno no se encuentra en estado o subEstado correcto",
                                Cc = "",
                                Bcc = "",
                                AttachedFiles = null
                            };
                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                        }
                    }
                }
            }
            foreach (var item in dato)
            {
                try
                {
                    var datosMatricula = _repMatriculaCabecera.ObtenerIdAlumnoCoordinadorAcademico(item.IdMatriculaCabecera);
                    var correoCoordinadorAcademico = _IntegraAspNetUsersRepositorio.ObtenerEmailPorNombreUsuario(datosMatricula.UsuarioCoordinadorAcademico);
                    correos.Add(correoCoordinadorAcademico);

                    DocumentosBO documentosBO = new DocumentosBO();
                    int Idplantillabase = 0;
                    string codigoCertificado = "";

                    var documentoByte = documentosBO.GenerarVistaPreviaCertificadoPortalWeb(item.IdPlantillaFrontal, item.IdPlantillaPosterior ?? default(int), item.IdOportunidad, ref Idplantillabase, ref codigoCertificado);

                    CertificadoGeneradoAutomaticoBO certificadoGenerado = new CertificadoGeneradoAutomaticoBO();
                    certificadoGenerado.IdMatriculaCabecera = item.IdMatriculaCabecera;
                    certificadoGenerado.IdPgeneral = item.IdPgeneral;
                    certificadoGenerado.IdPgeneralConfiguracionPlantilla = item.IdPgeneralConfiguracionPlantilla;
                    certificadoGenerado.IdUrlBlockStorage = 1;
                    certificadoGenerado.ContentType = "application/pdf";
                    certificadoGenerado.NombreArchivo = Idplantillabase == 12 ? codigoCertificado : documentosBO.ContenidoCertificado.CorrelativoConstancia.ToString();
                    certificadoGenerado.IdPespecifico = item.IdPespecifico;
                    certificadoGenerado.IdPlantilla = item.IdPlantillaFrontal;
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
                        var estados = _repMatriculaCabecera.ObtenerEstadosPorPlantillas(Idplantillabase, item.IdPlantillaFrontal, item.IdPlantillaPosterior ?? default(int), item.IdEstadoMatricula, item.IdSubEstadoMatricula);

                        var matriculaBO = _repMatriculaCabecera.FirstById(item.IdMatriculaCabecera);
                        matriculaBO.IdEstadoMatricula = estados.IdEstadoMatricula;
                        matriculaBO.IdSubEstadoMatricula = estados.IdSubEstadoMatricula;
                        matriculaBO.UsuarioModificacion = "Gen-Certi";
                        matriculaBO.FechaModificacion = DateTime.Now;
                        matriculaBO.IdEstadoMatriculaCertificado = matriculaBO.IdEstadoMatricula;
                        matriculaBO.IdSubEstadoMatriculaCertificado = matriculaBO.IdSubEstadoMatricula;
                        //_repMatriculaCabecera.Update(matriculaBO);
                    }
                }
                catch (Exception ex)
                {
                    
                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "fvaldez@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "Error Proceso Certificados",
                        Message = "IdMatricula: " + item.IdMatriculaCabecera.ToString() + ", IdPgeneralConfiguracionPlantilla: " + item.IdPgeneralConfiguracionPlantilla.ToString() + " < br/>" + ex.Message + " <br/> Mensaje toString <br/> " + ex.ToString(),
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };
                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }


            }
            return Ok(true);
        }
        /// TipoFuncion: GET
        /// Autor: Priscila Pacsi-Miguel Mora
        /// Fecha: 01/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Genera el certificado digital del alumno del nuevo aula virtual por id matricula cabecera
        /// </summary>
        /// <returns> Retorna un objeto Booleano</returns>
        [Route("[Action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult GenerarCertificadoPorAlumnoPortalWebPorIdMatricula(int IdMatriculaCabecera)//Idmatriculacabecera
        {
            PgeneralConfiguracionPlantillaRepositorio _repPgeneralConfiguracionPlantilla = new PgeneralConfiguracionPlantillaRepositorio(_integraDBContext);
            CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoRepositorio _repCertificadoGeneradoAutomatico = new CertificadoGeneradoAutomaticoRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoContenidoRepositorio _repCertificadoGeneradoAutomaticoContenido = new CertificadoGeneradoAutomaticoContenidoRepositorio(_integraDBContext);
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
            IntegraAspNetUsersRepositorio _IntegraAspNetUsersRepositorio = new IntegraAspNetUsersRepositorio(_integraDBContext);
            List<string> correos = new List<string>();
            List<string> palabra = new List<string>();
            palabra.Add("esquema");
            var listaCorreos = _repMatriculaCabecera.ConseguirCorreos(palabra);
            if (listaCorreos != null || listaCorreos.Count > 0)
            {
                correos.AddRange(listaCorreos);
            }
            correos.Add("lpacsi@bsginstitute.com");
            var dato = _repPgeneralConfiguracionPlantilla.ObtenerDatosParaCertificadosporAlumnoPortalWebPorIdMatriculaCabecera(IdMatriculaCabecera);
            if(dato != null)
            {
                try
                {
                    var datosMatricula = _repMatriculaCabecera.ObtenerIdAlumnoCoordinadorAcademico(dato.IdMatriculaCabecera);
                    var correoCoordinadorAcademico = _IntegraAspNetUsersRepositorio.ObtenerEmailPorNombreUsuario(datosMatricula.UsuarioCoordinadorAcademico);
                    correos.Add(correoCoordinadorAcademico);
                    DocumentosBO documentosBO = new DocumentosBO();
                    int Idplantillabase = 0;
                    string codigoCertificado = "";

                    var documentoByte = documentosBO.GenerarVistaPreviaCertificadoPortalWeb(dato.IdPlantillaFrontal, dato.IdPlantillaPosterior ?? default(int), dato.IdOportunidad, ref Idplantillabase, ref codigoCertificado);

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
                        var estados = _repMatriculaCabecera.ObtenerEstadosPorPlantillas(Idplantillabase, dato.IdPlantillaFrontal, dato.IdPlantillaPosterior ?? default(int), dato.IdEstadoMatricula, dato.IdSubEstadoMatricula);

                        var matriculaBO = _repMatriculaCabecera.FirstById(dato.IdMatriculaCabecera);
                        matriculaBO.IdEstadoMatricula = estados.IdEstadoMatricula;
                        matriculaBO.IdSubEstadoMatricula = estados.IdSubEstadoMatricula;
                        matriculaBO.UsuarioModificacion = "Gen-Certi";
                        matriculaBO.FechaModificacion = DateTime.Now;
                        matriculaBO.IdEstadoMatriculaCertificado = matriculaBO.IdEstadoMatricula;
                        matriculaBO.IdSubEstadoMatriculaCertificado = matriculaBO.IdSubEstadoMatricula;
                        _repMatriculaCabecera.Update(matriculaBO);
                    }
                }
                catch (Exception ex)
                {
                   
                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "fvaldez@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "Error Proceso Certificados",
                        Message = "IdMatricula: " + dato.IdMatriculaCabecera.ToString() + ", IdPgeneralConfiguracionPlantilla: " + dato.IdPgeneralConfiguracionPlantilla.ToString() + " \n" + ex.Message,
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };
                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
                return Ok(true);
            }
            else
            {
                var datosMatricula = _repMatriculaCabecera.ObtenerIdAlumnoCoordinadorAcademico(IdMatriculaCabecera);
                var correoCoordinadorAcademico = _IntegraAspNetUsersRepositorio.ObtenerEmailPorNombreUsuario(datosMatricula.UsuarioCoordinadorAcademico);

                var listaCertificado = _repCertificadoGeneradoAutomatico.ObtenerCertificados(IdMatriculaCabecera);
                bool flag = false;
                if (listaCertificado.Count > 0)
                {
                    foreach (var nombre in listaCertificado)
                    {
                        if (nombre.NombreArchivo.Contains("P"))
                        {
                            flag = true;
                            correos.Add(correoCoordinadorAcademico);
                            TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();
                            TMKMailDataDTO mailData = new TMKMailDataDTO
                            {
                                Sender = "fvaldez@bsginstitute.com",
                                Recipient = string.Join(",", correos),
                                Subject = "Error Proceso Certificados",
                                Message = "IdMatricula: " + IdMatriculaCabecera.ToString() + "\nYa se emitio el certificado del alumno",
                                Cc = "",
                                Bcc = "",
                                AttachedFiles = null
                            };
                            Mailservice.SetData(mailData);
                            Mailservice.SendMessageTask();
                            break;
                        }
                    };
                    if (!flag)
                    {
                        correos.Add(correoCoordinadorAcademico);
                        TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                        TMKMailDataDTO mailData = new TMKMailDataDTO
                        {
                            Sender = "fvaldez@bsginstitute.com",
                            Recipient = string.Join(",", correos),
                            Subject = "Error Proceso Certificados",
                            Message = "IdMatricula: " + IdMatriculaCabecera.ToString() + "\nEl alumno no se encuentra en estado o subEstado correcto",
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
                    correos.Add(correoCoordinadorAcademico);
                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "fvaldez@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "Error Proceso Certificados",
                        Message = "IdMatricula: " + IdMatriculaCabecera.ToString() + "\nEl alumno no se encuentra en estado o subEstado correcto",
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };
                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
                return Ok(false);
            }
        }

        /// TipoFuncion: GET
        /// Autor: Priscila Pacsi - Miguel Mora
        /// Fecha: 07/10/2021
        /// Versión: 1.5
        /// <summary>
        /// Genera el certificado digital del alumno por idMatriculaCabecera
        /// </summary>
        /// <returns> Retorna un objeto Booleano</returns>
        [Route("[Action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult GenerarCertificadoPorAlumnoIdMatriculaCabecera(int idMatriculaCabecera)
        {
            PgeneralConfiguracionPlantillaRepositorio _repPgeneralConfiguracionPlantilla = new PgeneralConfiguracionPlantillaRepositorio(_integraDBContext);
            CertificadoDetalleRepositorio _repCertificadoDetalle = new CertificadoDetalleRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoRepositorio _repCertificadoGeneradoAutomatico = new CertificadoGeneradoAutomaticoRepositorio(_integraDBContext);
            IntegraAspNetUsersRepositorio _IntegraAspNetUsersRepositorio = new IntegraAspNetUsersRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoContenidoRepositorio _repCertificadoGeneradoAutomaticoContenido = new CertificadoGeneradoAutomaticoContenidoRepositorio(_integraDBContext);
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
            List<string> correos = new List<string>();

            List<string> palabra = new List<string>();
            palabra.Add("esquema");
            //var listaCorreos = _repMatriculaCabecera.ConseguirCorreos(palabra);

            //correos.AddRange(listaCorreos);

            var dato = _repPgeneralConfiguracionPlantilla.ObtenerDatosParaCertificadosporAlumnoIdMatriculaCabecera(idMatriculaCabecera);
            var datosMatricula = _repMatriculaCabecera.ObtenerIdAlumnoCoordinadorAcademico(idMatriculaCabecera);
            var correoCoordinadorAcademico = _IntegraAspNetUsersRepositorio.ObtenerEmailPorNombreUsuario(datosMatricula.UsuarioCoordinadorAcademico);
            correos.Add(correoCoordinadorAcademico);
            if (dato != null)
            {
                try
                {
                    
                    DocumentosBO documentosBO = new DocumentosBO();
                    int Idplantillabase = 0;
                    string codigoCertificado = "";

                    var documentoByte = documentosBO.GenerarVistaPreviaCertificado(dato.IdPlantillaFrontal, dato.IdPlantillaPosterior ?? default(int), dato.IdOportunidad, ref Idplantillabase, ref codigoCertificado);

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
                        var estados = _repMatriculaCabecera.ObtenerEstadosPorPlantillas(Idplantillabase, dato.IdPlantillaFrontal, dato.IdPlantillaPosterior ?? default(int), dato.IdEstadoMatricula, dato.IdSubEstadoMatricula);

                        var matriculaBO = _repMatriculaCabecera.FirstById(dato.IdMatriculaCabecera);
                        matriculaBO.IdEstadoMatricula = estados.IdEstadoMatricula;
                        matriculaBO.IdSubEstadoMatricula = estados.IdSubEstadoMatricula;
                        matriculaBO.UsuarioModificacion = "Gen-Certi";
                        matriculaBO.FechaModificacion = DateTime.Now;
                        matriculaBO.IdEstadoMatriculaCertificado = matriculaBO.IdEstadoMatricula;
                        matriculaBO.IdSubEstadoMatriculaCertificado = matriculaBO.IdSubEstadoMatricula;
                        _repMatriculaCabecera.Update(matriculaBO);
                    }
                }
                catch (Exception ex)
                {

                    TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                    TMKMailDataDTO mailData = new TMKMailDataDTO
                    {
                        Sender = "fvaldez@bsginstitute.com",
                        Recipient = string.Join(",", correos),
                        Subject = "Error Proceso Certificados",
                        Message = "IdMatricula: " + idMatriculaCabecera.ToString() + ", IdPgeneralConfiguracionPlantilla: " + dato.IdPgeneralConfiguracionPlantilla.ToString() + " < br/>" + ex.Message,
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };
                    Mailservice.SetData(mailData);
                    Mailservice.SendMessageTask();
                }
                return Ok(true);
            }
            else
            {
                correos.Add(correoCoordinadorAcademico);
                TMK_MailServiceImpl Mailservice = new TMK_MailServiceImpl();

                TMKMailDataDTO mailData = new TMKMailDataDTO
                {
                    Sender = "fvaldez@bsginstitute.com",
                    Recipient = string.Join(",", correos),
                    Subject = "Error Proceso Certificados",
                    Message = "IdMatricula: " + idMatriculaCabecera.ToString() + "\nEl alumno no se encuentra en estado o subEstado correcto",
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };
                Mailservice.SetData(mailData);
                Mailservice.SendMessageTask();
            }
            return Ok(false);

        }
        /// TipoFuncion: GET
        /// Autor: MiguelMora
        /// Fecha: 11/10/2021
        /// Versión: 1.0
        /// <summary>
        /// Elimina los registros de los certificados de un alumno
        /// </summary>
        /// <returns> Retorna un objeto Booleano</returns>
        [Route("[Action]/{idAlumno}")]
        [HttpGet]
        public ActionResult EliminarCertificadosGenerados(int idAlumno)
        {
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
            CertificadoGeneradoAutomaticoRepositorio _repCertificadoGeneradoAutomatico = new CertificadoGeneradoAutomaticoRepositorio(_integraDBContext);

            var dato = _repMatriculaCabecera.ObtenerIdentificadoresMatriculaComboPorAlumno(idAlumno);
            
            foreach (var item in dato)
            {
                try
                {
                    int idCertificadoGeneradoAutomatico = _repCertificadoGeneradoAutomatico.ObtenerRegistrosProgramaPorMatricula(item.IdMatriculaCabecera);
                    if (idCertificadoGeneradoAutomatico > 0)
                    {
                        _repCertificadoGeneradoAutomatico.EliminarRegistrosPorId(idCertificadoGeneradoAutomatico);
                    }
                   
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }


            }
            return Ok(true);
        }
    }
}
