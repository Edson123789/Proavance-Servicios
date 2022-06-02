using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/BICOperacionesAutomatico")]
    public class BICOperacionesAutomaticoController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public BICOperacionesAutomaticoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ActualizarEstadoAutomaticoPorAbandonar()
        {
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
            MandrilEnvioCorreoRepositorio _repMandrilEnvioCorreo = new MandrilEnvioCorreoRepositorio(_integraDBContext);
            EnvioCorreoBicOperacionesRepositorio _repEnvioCorreoBicOperaciones = new EnvioCorreoBicOperacionesRepositorio(_integraDBContext);
            try
            {
                var primerBloqueAProcesar = _repMatriculaCabecera.MatriculasCulminadosSinCambiodeEstado();
                List<MandrilEnvioCorreoBO> listaMandrilEnvioCorreoBO = new List<MandrilEnvioCorreoBO>();
                foreach (var matriculaAActualizar in primerBloqueAProcesar)
                {
                    listaMandrilEnvioCorreoBO = new List<MandrilEnvioCorreoBO>();
                    MatriculaCabeceraBO matriculaCabeceraBO = _repMatriculaCabecera.FirstById(matriculaAActualizar.IdMatriculacabecera);

                    matriculaCabeceraBO.IdEstadoMatricula = 20;/*20: Por Abandonar*/
                    matriculaCabeceraBO.IdSubEstadoMatricula = 44; /*44: Por Abandonar*/
                    matriculaCabeceraBO.UsuarioModificacion = "UsuarioAutomatico";
                    matriculaCabeceraBO.FechaModificacion = DateTime.Now;

                    _repMatriculaCabecera.Update(matriculaCabeceraBO);

                    List<string> correosPersonalizados = new List<string>
                    {
                        matriculaAActualizar.Email1
                    };

                    var reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                    {
                        IdOportunidad = matriculaAActualizar.IdOportunidad,
                        IdPlantilla = matriculaAActualizar.IdPlantilla
                    };
                    reemplazoEtiquetaPlantilla.ReemplazarEtiquetas();
                    var emailCalculado = reemplazoEtiquetaPlantilla.EmailReemplazado;

                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = matriculaAActualizar.EmailCoordinador,
                        //Sender = "w.choque.itusaca@isur.edu.pe",
                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                        Subject = emailCalculado.Asunto,
                        Message = emailCalculado.CuerpoHTML,
                        Cc = "",
                        Bcc = "",
                        AttachedFiles = null
                    };
                    var mailServie = new TMK_MailServiceImpl();

                    mailServie.SetData(mailDataPersonalizado);
                    var listaIdsMailChimp = mailServie.SendMessageTask();

                    foreach (var mensaje in listaIdsMailChimp)
                    {
                        var mandrilEnvioCorreoBO = new MandrilEnvioCorreoBO
                        {
                            IdOportunidad = matriculaAActualizar.IdOportunidad,
                            IdPersonal = matriculaAActualizar.IdPersonal,
                            IdAlumno = matriculaAActualizar.IdAlumno,
                            IdCentroCosto = matriculaAActualizar.IdCentroCosto,
                            IdMandrilTipoAsignacion = 6, //Envio masivo operaciones
                            EstadoEnvio = 1,
                            IdMandrilTipoEnvio = 2, //Manual = 2
                            FechaEnvio = DateTime.Now,
                            Asunto = emailCalculado.Asunto,
                            FkMandril = mensaje.MensajeId,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = "EnvioAutomatico",
                            UsuarioModificacion = "EnvioAutomatico",
                            EsEnvioMasivo = true
                        };

                        if (!mandrilEnvioCorreoBO.HasErrors)
                        {
                            listaMandrilEnvioCorreoBO.Add(mandrilEnvioCorreoBO);

                        }
                        else
                        {
                            continue;
                        }
                    }
                    _repMandrilEnvioCorreo.Insert(listaMandrilEnvioCorreoBO);

                    EnvioCorreoBicOperacionesBO envioCorreoBicOperacionesBO = new EnvioCorreoBicOperacionesBO();

                    envioCorreoBicOperacionesBO.IdMatriculaCabecera = matriculaAActualizar.IdMatriculacabecera;
                    envioCorreoBicOperacionesBO.Asunto = emailCalculado.Asunto;
                    envioCorreoBicOperacionesBO.Mensaje = emailCalculado.CuerpoHTML;
                    envioCorreoBicOperacionesBO.Remitente = matriculaAActualizar.EmailCoordinador;
                    envioCorreoBicOperacionesBO.Destiantario = matriculaAActualizar.Email1;
                    envioCorreoBicOperacionesBO.EnviadoCorrectamente = true;
                    envioCorreoBicOperacionesBO.IdMandrilEnvioCorreo = listaMandrilEnvioCorreoBO.FirstOrDefault().Id;
                    envioCorreoBicOperacionesBO.Estado = true;
                    envioCorreoBicOperacionesBO.UsuarioCreacion = "EnvioAutomatico";
                    envioCorreoBicOperacionesBO.UsuarioModificacion = "EnvioAutomatico";
                    envioCorreoBicOperacionesBO.FechaCreacion = DateTime.Now;
                    envioCorreoBicOperacionesBO.FechaModificacion = DateTime.Now;

                    _repEnvioCorreoBicOperaciones.Insert(envioCorreoBicOperacionesBO);
                }

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("[action]")]
        [HttpGet]
        public ActionResult ActualizarEstadoAutomaticoAbandonado()
        {
            MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
            try
            {
                var datosAProcesar = _repMatriculaCabecera.MatriculasParaPaseAAbandonado();

                foreach (var itemPorAbandonar in datosAProcesar)
                {
                    try {
                        var matriculaCabeceraBO = _repMatriculaCabecera.FirstById(itemPorAbandonar.IdMatriculacabecera);

                        matriculaCabeceraBO.IdEstadoMatricula = 8;/*8: Abandonado*/
                        matriculaCabeceraBO.IdSubEstadoMatricula = 20; /*20: Abandonado*/
                        matriculaCabeceraBO.UsuarioModificacion = "UsuarioAutomatico";
                        matriculaCabeceraBO.FechaModificacion = DateTime.Now;

                        _repMatriculaCabecera.Update(matriculaCabeceraBO);
                    }
                    catch (Exception e)
                    { 

                    }                    
                }

                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
