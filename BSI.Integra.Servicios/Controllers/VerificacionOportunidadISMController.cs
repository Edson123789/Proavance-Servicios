using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Finanzas.BO;
using BSI.Integra.Aplicacion.Finanzas.Repositorio;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Servicios;
using BSI.Integra.Aplicacion.Servicios.BO;
using BSI.Integra.Aplicacion.Servicios.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Models.AulaVirtual;

using Microsoft.AspNetCore.Mvc;


namespace BSI.Integra.Servicios.Controllers
{
    [Route("api/VerificacionOportunidadIS")]
    [ApiController]
    public class VerificacionOportunidadISMController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        public VerificacionOportunidadISMController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerCombosVerificacionOportunidadISM()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                PeriodoRepositorio _repPeriodo = new PeriodoRepositorio();
                var periodos = _repPeriodo.ObtenerPeriodos();
                return Ok(periodos);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{Periodo}")]
        [HttpGet]
        public ActionResult ObtenerOportunidadesISM(string Periodo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var oportunidadVerificacion = new List<OportunidadIsVerificadaDTO>();
                OportunidadIsVerificadaRepositorio _repOportunidadIsVerificada = new OportunidadIsVerificadaRepositorio();
                if (string.IsNullOrEmpty(Periodo) || Periodo.Equals("null"))
                {
                    oportunidadVerificacion = _repOportunidadIsVerificada.ObtenerOportunidadIsVerificadaSinPeriodo();
                }
                else
                {
                    PeriodoRepositorio _repPeriodo = new PeriodoRepositorio();
                    var periodo = _repPeriodo.FirstById(Convert.ToInt32(Periodo));
                    oportunidadVerificacion = _repOportunidadIsVerificada.ObtenerOportunidadIsVerificadaConPeriodo(periodo.FechaInicialFinanzas, periodo.FechaFinFinanzas);
                }

                return Ok(oportunidadVerificacion);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarOportunidadVerificada([FromBody] OportunidadVerificadaDTO OportunidadVerificada)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //Validacion UsuarioMoodle
                AulaVirtualContext moodleContext = new AulaVirtualContext();
                MdlUser usuarioMoodle = new MdlUser();
                MoodleWebService moodleWebService = new MoodleWebService();
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
                PaisRepositorio _repPais = new PaisRepositorio(_integraDBContext);
                CiudadRepositorio _repCiudad = new CiudadRepositorio(_integraDBContext);
                PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio(_integraDBContext);
                PespecificoPadrePespecificoHijoRepositorio _repPespecificoPadreHijo = new PespecificoPadrePespecificoHijoRepositorio(_integraDBContext);
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                MoodleCursoRepositorio moodleCursoRepositorio = new MoodleCursoRepositorio(_integraDBContext);
                PEspecificoMatriculaAlumnoRepositorio pEspecificoMatriculaAlumnoRepositorio = new PEspecificoMatriculaAlumnoRepositorio(_integraDBContext);
                var matricula = _repMatriculaCabecera.FirstById(OportunidadVerificada.IdMatriculaCabecera);
                var pespecifico = _repPEspecifico.FirstById(matricula.IdPespecifico);
                var pEspecificoNuevaAulaVirtual = _repPEspecifico.ObtenerPEspecificoNuevaAulaVirtual();
                if (pEspecificoNuevaAulaVirtual.Exists(x => x.Id == pespecifico.Id))
                {
                    var oportunidad = _repOportunidad.FirstById(OportunidadVerificada.IdOportunidad);
                    var alumno = _repAlumno.FirstById(oportunidad.IdAlumno);

                    //Si se matriculo correctamente se hace la asignacion de coordinadora
                    ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO configuracionCoordinador = new ConfiguracionAsignacionCoordinadorOportunidadOperacionesBO();
                    PersonalRepositorio _repPersonal = new PersonalRepositorio(_integraDBContext);
                    var configuracion = configuracionCoordinador.ObtenerCoordinadorAsignacion(matricula.IdPespecifico, matricula.IdEstadoMatricula, matricula.IdSubEstadoMatricula, matricula.Id);
                    var personal = _repPersonal.FirstById(configuracion.IdPersonal);
                    matricula.UsuarioCoordinadorAcademico = configuracion.UsuarioPersonal;
                    matricula.UsuarioModificacion = OportunidadVerificada.Usuario;
                    matricula.FechaModificacion = DateTime.Now;

                    _repMatriculaCabecera.Update(matricula);

                    try
                    {
                        _repMatriculaCabecera.ActualizarTmatriculaCabecera(matricula.CodigoMatricula, configuracion.UsuarioPersonal);
                    }
                    catch (Exception e)
                    {
                    }

                    //Crear Oportunidad
                    OportunidadBO oportunidadBO = new OportunidadBO();
                    int idOportunidadOpe;
                    var oportunidadOperacionesExiste = _repOportunidad.ObtenerOportunidadOperacionesPorParametros(matricula.Id);

                    if (oportunidadOperacionesExiste == null)
                    {
                        var oportunidadOperaciones = oportunidadBO.GenerarOportunidadOperacionesConParametros(oportunidad.Id, OportunidadVerificada.Usuario, oportunidad.IdCentroCosto.Value, 47, configuracion.IdPersonal, matricula.Id);
                        _repOportunidad.InsertarOportunidadClasificacionOperaciones(oportunidadOperaciones.Id);
                        idOportunidadOpe = oportunidadOperaciones.Id;
                    }
                    else
                    {
                        if (oportunidadOperacionesExiste.IdOportunidadClasificacionOperaciones == null)
                        {
                            _repOportunidad.InsertarOportunidadClasificacionOperaciones(oportunidadOperacionesExiste.Id);
                        }
                        idOportunidadOpe = oportunidadOperacionesExiste.Id;
                    }

                    //Envia Correo
                    PlantillaOperacionesController plantillaController = new PlantillaOperacionesController(_integraDBContext);
                    PlantillaRepositorio _repPlantilla = new PlantillaRepositorio(_integraDBContext);
                    PlantillaBaseRepositorio _repPlantillaBase = new PlantillaBaseRepositorio(_integraDBContext);
                    PlantillaBO plantilla;
                    EnvioMasivoPlantillaBO _envioMasivoPlantilla = new EnvioMasivoPlantillaBO(_integraDBContext);

                    plantilla = _repPlantilla.FirstBy(x => x.Nombre.Contains("Bienvenida") && x.Nombre.Contains("Presencial"));

                    if (plantilla != null)
                    {
                        //var envioCorreo = plantillaController.EnvioC(coordinador.Email, matricula.CodigoMatricula, alumno.Email1, plantilla.Id);
                        if (!_repPlantillaBase.Exist(plantilla.IdPlantillaBase))
                        {
                            return BadRequest(ModelState);
                        }

                        var plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(plantilla.Id);
                        List<string> correosPersonalizadosCopiaOculta = new List<string>
                        {
                            "lhuallpa@bsginstitute.com",
                            "controldeaccesos@bsginstitute.com",
                            "bamontoya@bsginstitute.com",
                            personal.Email
                        };

                        ReemplazoEtiquetaPlantillaBO _reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext)
                        {
                            IdOportunidad = idOportunidadOpe,
                            IdPlantilla = plantilla.Id
                        };
                        _reemplazoEtiquetaPlantilla.ReemplazarEtiquetas();

                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            var emailCalculado = _reemplazoEtiquetaPlantilla.EmailReemplazado;
                            var archivosAdjuntos = _envioMasivoPlantilla.ObtenerArchivosAdjuntos(emailCalculado.CuerpoHTML);
                            TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                            {
                                Sender = personal.Email,
                                Recipient = alumno.Email1,
                                Subject = emailCalculado.Asunto,
                                Message = _envioMasivoPlantilla.QuitarEtiquetasArchivosAdjuntos(emailCalculado.CuerpoHTML),
                                Cc = "",
                                Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                                AttachedFiles = archivosAdjuntos
                            };
                            var mailServie = new TMK_MailServiceImpl();

                            mailServie.SetData(mailDataPersonalizado);
                            mailServie.SendMessageTask();
                        }
                    }

                    //Inserta en la tabla de verificacion
                    OportunidadIsVerificadaRepositorio _repOportunidadIsVerificada = new OportunidadIsVerificadaRepositorio();
                    var oportunidadVerificada = _repOportunidadIsVerificada.FirstBy(x => x.IdOportunidad == OportunidadVerificada.IdOportunidad || x.IdMatriculaCabecera == OportunidadVerificada.IdMatriculaCabecera);
                    if (oportunidadVerificada == null)
                    {
                        OportunidadIsVerificadaBO oportunidadIsVerificada = new OportunidadIsVerificadaBO();
                        oportunidadIsVerificada.IdOportunidad = OportunidadVerificada.IdOportunidad;
                        oportunidadIsVerificada.IdMatriculaCabecera = OportunidadVerificada.IdMatriculaCabecera;
                        oportunidadIsVerificada.Verificado = OportunidadVerificada.Verificado;
                        oportunidadIsVerificada.Estado = true;
                        oportunidadIsVerificada.UsuarioCreacion = OportunidadVerificada.Usuario;
                        oportunidadIsVerificada.UsuarioModificacion = OportunidadVerificada.Usuario;
                        oportunidadIsVerificada.FechaCreacion = DateTime.Now;
                        oportunidadIsVerificada.FechaModificacion = DateTime.Now;
                        _repOportunidadIsVerificada.Insert(oportunidadIsVerificada);

                        return Ok(oportunidadIsVerificada);
                    }
                    else
                    {
                        return Ok(oportunidadVerificada);
                    }
                }
                else
                {
                    return BadRequest("El Programa seleccionado no tiene una relación con el Aula Virtual.");
                }
            }
            catch (Exception e)
            {
                //SE REVIERTE LA ASIGNACION EN MATRICULA
                MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
                OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
                var matricula = _repMatriculaCabecera.FirstById(OportunidadVerificada.IdMatriculaCabecera);
                matricula.UsuarioCoordinadorAcademico = "0";
                matricula.UsuarioModificacion = OportunidadVerificada.Usuario;
                matricula.FechaModificacion = DateTime.Now;

                _repMatriculaCabecera.Update(matricula);
                _repMatriculaCabecera.ActualizarTmatriculaCabecera(matricula.CodigoMatricula, "0");

                //Si se creo la oportunidad de operaciones se revierte
                var oportunidadOperacionesExiste = _repOportunidad.ObtenerOportunidadOperacionesPorParametros(matricula.Id);
                if (oportunidadOperacionesExiste != null)
                {
                    if (oportunidadOperacionesExiste.IdOportunidadClasificacionOperaciones != null)
                    {
                        OportunidadClasificacionOperacionesRepositorio _repOportunidadClasificacionOperaciones = new OportunidadClasificacionOperacionesRepositorio(_integraDBContext);
                        var opClasificacion = _repOportunidadClasificacionOperaciones.FirstById(oportunidadOperacionesExiste.IdOportunidadClasificacionOperaciones.Value);
                        if (opClasificacion != null)
                        {
                            _repOportunidad.EliminarOportunidadFisicaOperacionesV3V4(opClasificacion.IdOportunidad);
                        }
                    }
                }
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public ActionResult ObtenerOportunidadesVerificadas()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                OportunidadIsVerificadaRepositorio _repOportunidadIsVerificada = new OportunidadIsVerificadaRepositorio(_integraDBContext);
                var oportunidadesVerificadas = _repOportunidadIsVerificada.ObtenerOportunidadesVerificadas();
                return Ok(oportunidadesVerificadas);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        private string QuitarAcentos(string inputString)
        {
            Regex a = new Regex("[á|à|ä|â|Á|À|Ä|Â]", RegexOptions.Compiled);
            Regex e = new Regex("[é|è|ë|ê|É|È|Ë|Ê]", RegexOptions.Compiled);
            Regex i = new Regex("[í|ì|ï|î|Í|Ì|Ï|Î]", RegexOptions.Compiled);
            Regex o = new Regex("[ó|ò|ö|ô|Ó|Ò|Ö|Ô]", RegexOptions.Compiled);
            Regex u = new Regex("[ú|ù|ü|û|Ú|Ù|Ü|Û]", RegexOptions.Compiled);
            Regex n = new Regex("[ñ|Ñ]", RegexOptions.Compiled);
            inputString = a.Replace(inputString, "a");
            inputString = e.Replace(inputString, "e");
            inputString = i.Replace(inputString, "i");
            inputString = o.Replace(inputString, "o");
            inputString = u.Replace(inputString, "u");
            inputString = n.Replace(inputString, "n");
            return inputString;
        }
    }
}
