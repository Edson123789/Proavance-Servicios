using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
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


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BSI.Integra.Servicios.Controllers.Operaciones
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculaCabeceraDatosCertificadoController : ControllerBase
    {
        private readonly integraDBContext _integraDBContext;
        public MatriculaCabeceraDatosCertificadoController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
        }
        /// TipoFuncion: GET
        /// Autor: Miguel Mora
        /// Fecha: 30/09/2021
        /// Versión: 1.0
        /// <summary>
        /// obtiene la cantidad de registros pendientes de certificados que existen
        /// </summary>
        /// <returns>MatriculaCabeceraDatosCertificadoBO<returns>
        [Route("[Action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerCambiosPendientes(int IdMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraDatosCertificadoRepositorio repMatriculaCertificado = new MatriculaCabeceraDatosCertificadoRepositorio(_integraDBContext);
                var EstadoCambioDatos = repMatriculaCertificado.GetBy(w => w.EstadoCambioDatos == true && w.IdMatriculaCabecera == IdMatriculaCabecera).Count();
                return Ok(new{EstadoCambioDatos} );
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
        /// Obtiene el registro valido de certificados
        /// </summary>
        /// <returns>MatriculaCabeceraDatosCertificadoBO<returns>
        [Route("[Action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ObtenerMatriculaCertificado(int IdMatriculaCabecera)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraDatosCertificadoRepositorio repMatriculaCertificado = new MatriculaCabeceraDatosCertificadoRepositorio(_integraDBContext);
                PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(_integraDBContext);
                var listado = repMatriculaCertificado.GetBy(w => w.Estado == true && w.EstadoCambioDatos==false && w.IdMatriculaCabecera==IdMatriculaCabecera);
                if (listado.Count() == 0) {
                    MatriculaCabeceraRepositorio _repMatriculaCabecera=new MatriculaCabeceraRepositorio(_integraDBContext);
                    AlumnoRepositorio _repAlumno=new AlumnoRepositorio(_integraDBContext); 
                    OportunidadRepositorio _repOportunidad= new OportunidadRepositorio(_integraDBContext);
                    var matriculaCabecera = _repMatriculaCabecera.FirstBy(x => x.Id == IdMatriculaCabecera);
                    var detalleMatriculaCabecera = matriculaCabecera.ObtenerDetalleMatricula();
                    var alumno = _repAlumno.FirstById(matriculaCabecera.IdAlumno);

                    var oportunidad = _repOportunidad.FirstById(detalleMatriculaCabecera.IdOportunidad);
                    var DatosCompuestosOportunidad = _repOportunidad.ObtenerDatosCompuestosPorIdOportunidad(oportunidad.Id);
                    DateTime fechaInicioCapacitacion = new DateTime();
                    DateTime fechaFinCapacitacion = new DateTime();
                    PEspecificoMatriculaAlumnoRepositorio reppEspecifico = new PEspecificoMatriculaAlumnoRepositorio();
                    bool existe = reppEspecifico.ExisteNuevaAulaVirtual(matriculaCabecera.IdPespecifico);
                    if (existe == false)
                    {
                        fechaInicioCapacitacion = repMatriculaCertificado.TranformarCadenaEnFecha(_repAlumno.ObtenerFechaInicioCapacitacion(matriculaCabecera.Id));
                        fechaFinCapacitacion = repMatriculaCertificado.TranformarCadenaEnFecha(_repAlumno.ObtenerFechaFinCapacitacion(matriculaCabecera.Id));
                    }
                    else
                    {
                        fechaInicioCapacitacion = repMatriculaCertificado.TranformarCadenaEnFecha(_repAlumno.ObtenerFechaInicioCapacitacionPortalWeb(matriculaCabecera.Id));
                        fechaFinCapacitacion = repMatriculaCertificado.TranformarCadenaEnFecha(_repAlumno.ObtenerFechaFinCapacitacionPortalWeb(matriculaCabecera.Id));

                    }
                    MatriculaCabeceraDatosCertificadoBO nuevocertificado = new MatriculaCabeceraDatosCertificadoBO
                    {
                        IdMatriculaCabecera = IdMatriculaCabecera,
                        Duracion = _repPespecifico.ObtenerDuracionProgramaEspecifico(matriculaCabecera.IdPespecifico, IdMatriculaCabecera),
                        FechaInicio = fechaInicioCapacitacion,
                        FechaFinal = fechaFinCapacitacion,
                        NombreCurso = detalleMatriculaCabecera.NombreProgramaGeneral.ToUpper(),
                        EstadoCambioDatos = false,
                        Estado = true,
                        UsuarioCreacion = "SYSTEM",
                        UsuarioModificacion = "SYSTEM",
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    repMatriculaCertificado.Insert(nuevocertificado);
                    listado = repMatriculaCertificado.GetBy(w => w.Estado == true && w.EstadoCambioDatos == false && w.IdMatriculaCabecera == IdMatriculaCabecera);
                }
                return Ok(listado);
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
        /// En caso que  ya existan 3 o mas versiones de cambio de datos guardara un registro de certificado donde su aprovacion dependera a la confirmacion de su supervisor, caso contrario
        /// Guarda un registro nuevo de certificados aprovado
        /// </summary>
        /// <param name=”ObjetoDTO”>DTO de la tabla retenciones</param>
        /// <returns>bool<returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarCertificadoDatos([FromBody] MatriculaCabeceraDatosCertificadoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraDatosCertificadoRepositorio repMatriculaCertificado = new MatriculaCabeceraDatosCertificadoRepositorio(_integraDBContext);
                MatriculaCabeceraDatosCertificadoDTO certificadoActual = repMatriculaCertificado.ObtenerDatosCertificadoPorMatricula(ObjetoDTO.IdMatriculaCabecera).First();

                MatriculaCabeceraDatosCertificadoBO nuevocertificado = new MatriculaCabeceraDatosCertificadoBO
                {
                    IdMatriculaCabecera = certificadoActual.IdMatriculaCabecera,
                    Duracion = ObjetoDTO.Duracion,
                    FechaInicio = (DateTime)ObjetoDTO.FechaInicio,
                    FechaFinal = (DateTime)ObjetoDTO.FechaFinal,
                    NombreCurso = ObjetoDTO.NombreCurso,
                    EstadoCambioDatos = true,
                    Estado = true,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                IntegraAspNetUsersRepositorio _repAspNetUsers = new IntegraAspNetUsersRepositorio();
                PersonalRepositorio repPersonal = new PersonalRepositorio();
                IntegraAspNetUsersBO user= _repAspNetUsers.FirstBy(w => w.Estado == true && w.UserName == ObjetoDTO.Usuario);
                PersonalBO personal = repPersonal.FirstById(user.PerId);
                MatriculaCabeceraDatosCertificadoMensajesBO nuevocertificadoMensaje = new MatriculaCabeceraDatosCertificadoMensajesBO
                {
                    IdMatriculaCabecera = certificadoActual.IdMatriculaCabecera,
                    IdPersonalRemitente = personal.Id,
                    IdPersonalReceptor = (int)personal.IdJefe,
                    Mensaje = ObjetoDTO.Mensaje,
                    ValorAntiguo = JsonConvert.SerializeObject(certificadoActual),
                    ValorNuevo = JsonConvert.SerializeObject(nuevocertificado),
                    EstadoMensaje=true,
                    Estado = true,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                MatriculaCabeceraDatosCertificadoMensajesRepositorio repMatriculaCertificadoMensaje = new MatriculaCabeceraDatosCertificadoMensajesRepositorio();
                repMatriculaCertificadoMensaje.Insert(nuevocertificadoMensaje);

                repMatriculaCertificado.Insert(nuevocertificado);

               
                return Ok(true);
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
        /// Guarda un registro de certificado donde su aprovacion dependera a la confirmacion de su supervisor
        /// </summary>
        /// <param name=”ObjetoDTO”>DTO de la tabla retenciones</param>
        /// <returns>bool<returns>
        [Route("[Action]")]
        [HttpPost]
        public ActionResult InsertarCertificadoNombreCurso([FromBody] MatriculaCabeceraDatosCertificadoDTO ObjetoDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MatriculaCabeceraDatosCertificadoRepositorio repMatriculaCertificado = new MatriculaCabeceraDatosCertificadoRepositorio(_integraDBContext);
                MatriculaCabeceraDatosCertificadoDTO certificadoActual = repMatriculaCertificado.ObtenerDatosCertificadoPorMatricula(ObjetoDTO.IdMatriculaCabecera).First();
                MatriculaCabeceraDatosCertificadoMensajesRepositorio repMatriculaCertificadoMensaje = new MatriculaCabeceraDatosCertificadoMensajesRepositorio();
                IntegraAspNetUsersRepositorio _repAspNetUsers = new IntegraAspNetUsersRepositorio();
                PersonalRepositorio repPersonal = new PersonalRepositorio();

                IntegraAspNetUsersBO user = _repAspNetUsers.FirstBy(w => w.Estado == true && w.UserName == ObjetoDTO.Usuario);
                PersonalBO personal = repPersonal.FirstById(user.PerId);

                MatriculaCabeceraDatosCertificadoBO nuevocertificado = new MatriculaCabeceraDatosCertificadoBO
                {
                    IdMatriculaCabecera = certificadoActual.IdMatriculaCabecera,
                    Duracion = certificadoActual.Duracion,
                    FechaInicio = (DateTime)certificadoActual.FechaInicio,
                    FechaFinal = (DateTime)certificadoActual.FechaFinal,
                    NombreCurso = ObjetoDTO.NombreCurso,
                    EstadoCambioDatos = false,
                    Estado = true,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
              
                MatriculaCabeceraDatosCertificadoMensajesBO nuevocertificadoMensaje = new MatriculaCabeceraDatosCertificadoMensajesBO
                {
                    IdMatriculaCabecera = certificadoActual.IdMatriculaCabecera,
                    IdPersonalRemitente = personal.Id,
                    IdPersonalReceptor = (int)personal.IdJefe,
                    Mensaje = ObjetoDTO.Mensaje,
                    ValorAntiguo = JsonConvert.SerializeObject(certificadoActual),
                    ValorNuevo = JsonConvert.SerializeObject(nuevocertificado),
                    EstadoMensaje = true,
                    Estado = true,
                    UsuarioCreacion = ObjetoDTO.Usuario,
                    UsuarioModificacion = ObjetoDTO.Usuario,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };
                repMatriculaCertificadoMensaje.Insert(nuevocertificadoMensaje);
                repMatriculaCertificado.Insert(nuevocertificado);
                return Ok(true);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
