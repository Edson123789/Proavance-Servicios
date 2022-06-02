using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Operaciones;
using BSI.Integra.Aplicacion.DTOs.Transversal;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.SCode.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoreLinq;

namespace BSI.Integra.Servicios.Controllers.Operaciones
{
    [Route("api/Operaciones/Nota")]
    public class NotaController : Controller
    {
        private readonly integraDBContext _integraDBContext;
        private NotaRepositorio _repoNota;

        public NotaController(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repoNota = new NotaRepositorio(integraDBContext);
        }


        [Route("[Action]")]
        [HttpGet]
        public ActionResult Punto()
        {
            return Ok("Test");
        }

        [Route("[Action]/{idDocente}/{filtro}")]
        [HttpGet]
        public ActionResult ListadoCcPorDocente(int idDocente, string filtro)
        {
            try
            {
                var listado = _repoNota.ListadoCcPorDocente(idDocente, filtro);

                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[action]")]
        [HttpPost]
        public ActionResult ListadoCcPorDocenteFiltrado([FromBody]CursoPorDocenteFiltroDTO filtro)
        {
            try
            {
                var listado = _repoNota.ListadoCcPorDocenteFiltrado(filtro);

                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{idPespecifico}")]
        [HttpGet]
        public ActionResult ListadoAsistenciaPorPrograma(int idPespecifico)
        {
            try
            {
                var listado = _repoNota.ListadoAsistenciaPorPrograma(idPespecifico);

                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{idPespecifico}")]
        [HttpGet]
        public ActionResult ListadoNotaPorPrograma(int idPespecifico)
        {
            try
            {
                var listado = _repoNota.ListadoAsistenciaPorPrograma(idPespecifico);

                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{idPespecifico}")]
        [HttpGet]
        public ActionResult ListadoNotaProcesar(int idPespecifico)
        {
            try
            {
                PespecificoRepositorio _repoPespecifico = new PespecificoRepositorio();
                var pespecifico = _repoPespecifico.FirstById(idPespecifico);
                
                EvaluacionRepositorio _repoEvaluacion = new EvaluacionRepositorio();
                IEnumerable<EvaluacionBO> listadoEvaluacion;

                listadoEvaluacion = _repoEvaluacion.GetBy(w => w.IdPespecifico == idPespecifico && w.Aprobado == true);

                var listadoIdEvaluacion = listadoEvaluacion.Select(s => s.Id);
                var listadoNota = _repoNota.GetBy(w => listadoIdEvaluacion.Contains(w.IdEvaluacion));

                var listadoMatricula = _repoNota.ListadoMatriculaPorPrograma(idPespecifico);

                return Ok(new { ListadoEvaluaciones = listadoEvaluacion, ListadoNotas = listadoNota, ListadoMatriculas = listadoMatricula });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{idPespecifico}/{grupo}")]
        [HttpGet]
        public ActionResult ListadoNotaProcesar(int idPespecifico, int grupo)
        {
            try
            {
                PespecificoRepositorio _repoPespecifico = new PespecificoRepositorio();
                var pespecifico = _repoPespecifico.FirstById(idPespecifico);

                EvaluacionRepositorio _repoEvaluacion = new EvaluacionRepositorio();
                IEnumerable<EvaluacionBO> listadoEvaluacion;

                listadoEvaluacion = _repoEvaluacion.GetBy(w => w.IdPespecifico == idPespecifico && w.Grupo == grupo && w.Aprobado == true);

                var listadoIdEvaluacion = listadoEvaluacion.Select(s => s.Id);
                var listadoNota = _repoNota.GetBy(w => listadoIdEvaluacion.Contains(w.IdEvaluacion));

                var listadoMatricula = _repoNota.ListadoMatriculaPorProgramaPorGrupo(idPespecifico, grupo);

                //calculo de la asistencia
                PespecificoSesionRepositorio _repoSesiones = new PespecificoSesionRepositorio();
                var listadoSesiones = _repoSesiones.GetBy(w => w.IdPespecifico == idPespecifico && w.Grupo == grupo);

                AsistenciaRepositorio _reposAsistencia = new AsistenciaRepositorio();
                var listadoIdSesion = listadoSesiones.Select(s => s.Id);
                var listadoAsistencia = _reposAsistencia.GetBy(w => listadoIdSesion.Contains(w.IdPespecificoSesion));

                EvaluacionEscalaCalificacionRepositorio _repoEscala = new EvaluacionEscalaCalificacionRepositorio();
                var escalaCalificacion = _repoEscala.ObtenerEscalaPorPEspecifico_Presencial(idPespecifico);

                return Ok(new
                {
                    ListadoEvaluaciones = listadoEvaluacion, ListadoNotas = listadoNota,
                    ListadoMatriculas = listadoMatricula,
                    EscalaCalificacion = Convert.ToInt32(escalaCalificacion?.EscalaCalificacion ?? 0),
                    ListadoSesiones = listadoSesiones, ListadoAsistencias = listadoAsistencia
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ListadoNotaProcesarPorIdMatricula(int IdMatriculaCabecera)
        {
            try
            {
                MatriculaCabeceraRepositorio repMatriculaCabeceraRep = new MatriculaCabeceraRepositorio();
                var MatriculaCabecera = repMatriculaCabeceraRep.GetBy(x => x.Id == IdMatriculaCabecera).FirstOrDefault();
                if (MatriculaCabecera != null)
                {
                    int idPespecifico = MatriculaCabecera.IdPespecifico;
                    int grupo = 1;

                    PespecificoRepositorio _repoPespecifico = new PespecificoRepositorio();
                    var pespecifico = _repoPespecifico.FirstById(idPespecifico);

                    EvaluacionRepositorio _repoEvaluacion = new EvaluacionRepositorio();
                    IEnumerable<EvaluacionBO> listadoEvaluacion;

                    listadoEvaluacion = _repoEvaluacion.GetBy(w => w.IdPespecifico == idPespecifico && w.Aprobado == true );

                    var listadoIdEvaluacion = listadoEvaluacion.Select(s => s.Id);
                    var listadoNota = _repoNota.GetBy(w => listadoIdEvaluacion.Contains(w.IdEvaluacion) && w.IdMatriculaCabecera==IdMatriculaCabecera);

                    var listadoMatricula = _repoNota.ListadoMatriculaPorProgramaPorGrupo(idPespecifico, grupo,IdMatriculaCabecera);

                    //calculo de la asistencia
                    PespecificoSesionRepositorio _repoSesiones = new PespecificoSesionRepositorio();
                    var listadoSesiones = _repoSesiones.GetBy(w => w.IdPespecifico == idPespecifico && w.Grupo == grupo);

                    AsistenciaRepositorio _reposAsistencia = new AsistenciaRepositorio();
                    var listadoIdSesion = listadoSesiones.Select(s => s.Id);
                    var listadoAsistencia = _reposAsistencia.GetBy(w => listadoIdSesion.Contains(w.IdPespecificoSesion) && w.IdMatriculaCabecera==IdMatriculaCabecera);

                    return Ok(new
                    {
                        ListadoEvaluaciones = listadoEvaluacion,
                        ListadoNotas = listadoNota,
                        ListadoMatriculas = listadoMatricula,
                        EscalaCalificacion = 20,
                        ListadoSesiones = listadoSesiones,
                        ListadoAsistencias = listadoAsistencia
                    });
                }
                else {
                    return Ok();
                }                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{IdMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ListadoNotaPorIdMatricula(int IdMatriculaCabecera)
        {
            try
            {                
                    
                var listadoNotasPorMatricula = _repoNota.ListadoNotaPorMatriculaCabecera( IdMatriculaCabecera);

                EvaluacionEscalaCalificacionRepositorio _repoEscala = new EvaluacionEscalaCalificacionRepositorio();

                foreach (var item in listadoNotasPorMatricula)
                {
                    item.NombreEvaluacion = item.NombreEvaluacion == null ? "" : item.NombreEvaluacion;
                    if (item.NombreEvaluacion.ToUpper().Contains("ASISTENCIA")) {
                        var escalaCalificacion = _repoEscala.ObtenerEscalaPorPEspecifico_Presencial(item.IdPEspecifico);
                        item.Nota = item.Nota * Convert.ToInt32(escalaCalificacion?.EscalaCalificacion ?? 0);
                    }
                    if (item.IdEvaluacion==null) {
                        item.Nota = null;                        
                    }
                }
                return Ok(listadoNotasPorMatricula);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{idPespecifico}/{grupo}")]
        [HttpGet]
        public ActionResult ListadoAsistenciaProcesar(int idPespecifico, int grupo)
        {
            try
            {
                PespecificoSesionRepositorio _repoSesiones = new PespecificoSesionRepositorio();                
                var listadoSesiones = _repoSesiones.GetBy(w => w.IdPespecifico == idPespecifico && w.Grupo == grupo);

                AsistenciaRepositorio _reposAsistencia = new AsistenciaRepositorio();
                var listadoIdSesion = listadoSesiones.Select(s => s.Id);
                var listadoAsistencia = _reposAsistencia.GetBy(w => listadoIdSesion.Contains(w.IdPespecificoSesion));

                var listadoMatricula = _repoNota.ListadoMatriculaPorProgramaPorGrupo(idPespecifico, grupo);

                return Ok(new { ListadoSesiones = listadoSesiones, ListadoAsistencias = listadoAsistencia, ListadoMatriculas = listadoMatricula });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [Route("[Action]")]
        [HttpPost]
        public ActionResult Registrar([FromForm]List<NotaRegistrarDTO> notas, [FromForm]int idPEspecifico, [FromForm]string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<NotaBO> listado = new List<NotaBO>();
                List<NotaBO> listadoNotaExistente = new List<NotaBO>();
                if (notas != null && notas.Count > 0)
                {
                    listadoNotaExistente = _repoNota.GetBy(w => notas.Select(s => s.Id).Contains(w.Id)).ToList();
                }
                foreach (var nota in notas)
                {
                    if (listadoNotaExistente.Any(w => w.Id == nota.Id))
                    {
                        NotaBO notaBo = listadoNotaExistente.FirstOrDefault(w => w.Id == nota.Id);

                        notaBo.IdEvaluacion = nota.IdEvaluacion;
                        notaBo.IdMatriculaCabecera = nota.IdMatriculaCabecera;
                        notaBo.Nota = nota.Nota != null ? nota.Nota.Value : 0;

                        notaBo.Estado = true;
                        notaBo.FechaModificacion = DateTime.Now;
                        notaBo.UsuarioModificacion = usuario;

                        listado.Add(notaBo);
                    }
                    else
                    {
                        NotaBO notaBo = new NotaBO();
                        notaBo.Id = nota.Id;
                        notaBo.IdEvaluacion = nota.IdEvaluacion;
                        notaBo.IdMatriculaCabecera = nota.IdMatriculaCabecera;
                        notaBo.Nota = nota.Nota != null ? nota.Nota.Value : 0;

                        notaBo.Estado = true;
                        notaBo.FechaCreacion = DateTime.Now;
                        notaBo.FechaModificacion = DateTime.Now;
                        notaBo.UsuarioCreacion = usuario;
                        notaBo.UsuarioModificacion = usuario;
                        
                        listado.Add(notaBo);
                    }                    
                }

                bool resultado;
                using (TransactionScope scope = new TransactionScope())
                {
                    resultado = _repoNota.Update(listado);
                    scope.Complete();
                }

                return Ok(resultado);               
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + (e.InnerException != null ? ("- " + e.InnerException.Message) : ""));
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult Aprobar([FromForm]List<NotaRegistrarDTO> notas, [FromForm]int idPEspecifico, [FromForm] int grupo, [FromForm]string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                List<NotaBO> listado = new List<NotaBO>();
                List<NotaBO> listadoNotaExistente = new List<NotaBO>();
                if (notas != null && notas.Count > 0)
                {
                    listadoNotaExistente = _repoNota.GetBy(w => notas.Select(s => s.Id).Contains(w.Id)).ToList();
                }
                foreach (var nota in notas)
                {
                    if (listadoNotaExistente.Any(w => w.Id == nota.Id))
                    {
                        NotaBO notaBo = listadoNotaExistente.FirstOrDefault(w => w.Id == nota.Id);

                        notaBo.IdEvaluacion = nota.IdEvaluacion;
                        notaBo.IdMatriculaCabecera = nota.IdMatriculaCabecera;
                        notaBo.Nota = nota.Nota != null ? nota.Nota.Value : 0;

                        notaBo.Estado = true;
                        notaBo.FechaModificacion = DateTime.Now;
                        notaBo.UsuarioModificacion = usuario;

                        listado.Add(notaBo);
                    }
                    else
                    {
                        NotaBO notaBo = new NotaBO();
                        notaBo.Id = nota.Id;
                        notaBo.IdEvaluacion = nota.IdEvaluacion;
                        notaBo.IdMatriculaCabecera = nota.IdMatriculaCabecera;
                        notaBo.Nota = nota.Nota != null ? nota.Nota.Value : 0;

                        notaBo.Estado = true;
                        notaBo.FechaCreacion = DateTime.Now;
                        notaBo.FechaModificacion = DateTime.Now;
                        notaBo.UsuarioCreacion = usuario;
                        notaBo.UsuarioModificacion = usuario;

                        listado.Add(notaBo);
                    }
                }

                //adicion de la aprobacion
                PEspecificoAprobacionCalificacionRepositorio aprobacionRepositorio = new PEspecificoAprobacionCalificacionRepositorio(_integraDBContext);
                PEspecificoAprobacionCalificacionBO aprobacionBO;
                if (aprobacionRepositorio.Exist(w => w.IdPespecifico == idPEspecifico && w.Grupo == grupo))
                {
                    aprobacionBO =
                        aprobacionRepositorio.FirstBy(w => w.IdPespecifico == idPEspecifico && w.Grupo == grupo);
                    aprobacionBO.EsNotaAprobada = true;
                    aprobacionBO.FechaAprobacionNota = DateTime.Now;
                    aprobacionBO.UsuarioModificacion = usuario;
                    aprobacionBO.FechaModificacion = DateTime.Now;
                }
                else
                {
                    aprobacionBO = new PEspecificoAprobacionCalificacionBO()
                    {
                        IdPespecifico = idPEspecifico,
                        Grupo = grupo,
                        EsNotaAprobada = true,
                        FechaAprobacionNota = DateTime.Now,

                        Estado = true,
                        UsuarioCreacion = usuario,
                        UsuarioModificacion = usuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                }

                bool resultado;
                using (TransactionScope scope = new TransactionScope())
                {
                    resultado = _repoNota.Update(listado);
                    aprobacionRepositorio.Update(aprobacionBO);
                    scope.Complete();
                }

                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + (e.InnerException != null ? ("- " + e.InnerException.Message) : ""));
            }
        }

        [Route("[Action]/{idPespecifico}")]
        [HttpGet]
        public ActionResult ListadoAlumnosPendienteCalificarProyecto(int idPespecifico)
        {
            try
            {
                PgeneralProyectoAplicacionRepositorio _repoProyecto = new PgeneralProyectoAplicacionRepositorio();
                var listadoAlumno = _repoProyecto.ListadoAlumnoPendienteCalificacion(idPespecifico);

                return Ok(listadoAlumno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]/{idMatriculaCabecera}")]
        [HttpGet]
        public ActionResult ListadoAlumnoHistoricoProyecto(int idMatriculaCabecera)
        {
            try
            {
                PgeneralProyectoAplicacionRepositorio _repoProyecto = new PgeneralProyectoAplicacionRepositorio();
                var listadoAlumno = _repoProyecto.ListadoAlumnoHistorico(idMatriculaCabecera);

                return Ok(listadoAlumno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult CalificarProyectoAplicacion([FromBody] ProyectoAplicacionCalificarDTO calificacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                PgeneralProyectoAplicacionEnvioRepositorio _repoProyecto =
                    new PgeneralProyectoAplicacionEnvioRepositorio();
                var bo = _repoProyecto.FirstById(calificacion.IdPgeneralProyectoAplicacion);
                bo.IdPgeneralProyectoAplicacionEstado = calificacion.IdPgeneralProyectoAplicacionEstado;
                bo.Nota = calificacion.Nota;
                bo.Comentarios = calificacion.Comentarios;
                bo.FechaCalificacion = DateTime.Now;

                bo.UsuarioModificacion = calificacion.Usuario;
                bo.FechaModificacion = DateTime.Now;

                bool resultado;
                using (TransactionScope scope = new TransactionScope())
                {
                    resultado = _repoProyecto.Update(bo);
                    scope.Complete();
                }

                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + (e.InnerException != null ? ("- " + e.InnerException.Message) : ""));
            }
        }

        /// <summary>
        /// Método responsable del registro de los archivos de proyecto/entregables enviados por alumnos
        /// </summary>
        /// <param name="proyecto">DTO con información del proyecto/entregable</param>
        /// <param name="Files">Colección de archivos</param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        [RequestSizeLimit(600000000)]
        public ActionResult SubirDocumentosProyecto(ProyectoAplicacionEntregaVersionPwDTOV2 proyecto, [FromForm] IList<IFormFile> Files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                MaterialVersionRepositorio _repMaterialVersion = new MaterialVersionRepositorio(_integraDBContext);

                string NombreArchivo = "";
                string NombreArchivotemp = "";
                string ContentType = "";
                var urlArchivoRepositorio = "";
                PgeneralProyectoAplicacionEnvioRepositorio _repoProyecto =
                    new PgeneralProyectoAplicacionEnvioRepositorio();

                var listadoProyectosEnviados =
                    _repoProyecto.GetBy(w => w.IdMatriculaCabecera == proyecto.IdMatriculaCabecera);
                //validacion de envío de proyectos
                if (proyecto.EsEntregable == false && listadoProyectosEnviados != null &&
                    listadoProyectosEnviados.Any())
                {
                    if (listadoProyectosEnviados.Count() >= 2)
                        return BadRequest("No se puede registrar el envío, el límite de envíos es de 2");
                    if (listadoProyectosEnviados.Any(w => w.IdEscalaCalificacionDetalle == null))
                        return BadRequest("No se puede registrar el envío, tiene un envío pendiente de calificación");
                }

                if (Files != null && Files.Count > 0)
                {
                    UtilBO utilBO = new UtilBO();
                    foreach (var file in Files)
                    {
                        ContentType = file.ContentType;
                        NombreArchivo = file.FileName;
                        NombreArchivotemp = file.FileName;
                        NombreArchivotemp =
                            DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-" +
                            utilBO.SlugNombreArchivo(NombreArchivotemp);
                        urlArchivoRepositorio = _repMaterialVersion.SubirDocumentosProyectoAplicacionRepositorio(file.ConvertToByte(), file.ContentType, NombreArchivotemp);
                    }
                }
                else
                {
                    return BadRequest("No se subió ningún archivo.");
                }

                if (string.IsNullOrEmpty(urlArchivoRepositorio))
                {
                    return BadRequest("Ocurrió un problema al subir el archivo.");
                }

                var bo = new PgeneralProyectoAplicacionEnvioBO
                {
                    IdPgeneralProyectoAplicacionEstado = 1,
                    IdMatriculaCabecera = proyecto.IdMatriculaCabecera,
                    //FechaEnvio = proyecto.FechaEnvioProyecto,
                    FechaEnvio = DateTime.Now,
                    NombreArchivo = NombreArchivo,
                    RutaArchivo = urlArchivoRepositorio,
                    EsEntregable = proyecto.EsEntregable,

                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = proyecto.Usuario,
                    UsuarioModificacion = proyecto.Usuario
                };
                
                var result = _repoProyecto.Insert(bo);

                return Ok(urlArchivoRepositorio);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("[Action]/{idPespecifico}/{grupo}")]
        [HttpGet]
        public ActionResult ListadoNotaProcesarV2(int idPespecifico, int grupo)
        {
            try
            {
                PespecificoRepositorio _repoPespecifico = new PespecificoRepositorio();
                var pespecifico = _repoPespecifico.FirstById(idPespecifico);

                var listadoMatricula = _repoNota.ListadoMatriculaPorProgramaPorGrupo(idPespecifico, grupo);

                EsquemaEvaluacionRepositorio _repoEsquema = new EsquemaEvaluacionRepositorio();
                EsquemaEvaluacionPgeneralRepositorio _repoEsquemaAsignacion = new EsquemaEvaluacionPgeneralRepositorio();
                EsquemaEvaluacionPgeneralDetalleRepositorio _repoEsquemaAsignacionDetalle = new EsquemaEvaluacionPgeneralDetalleRepositorio();
                EscalaCalificacionRepositorio _repoEscala = new EscalaCalificacionRepositorio();
                ParametroEvaluacionNotaRepositorio _repoParametroNota = new ParametroEvaluacionNotaRepositorio();

                var listadoNota = _repoParametroNota.GetBy(w => w.IdPespecifico == idPespecifico && w.Grupo == w.Grupo);

                var escalas = _repoEscala.GetBy(w => w.Estado == true,
                    s => new {s.Id, s.Nombre, ListadoDetalle = s.TEscalaCalificacionDetalle});

                var esquemaAsignado = _repoEsquemaAsignacion.GetBy(w => w.IdPgeneral == pespecifico.IdProgramaGeneral);
                var esquemaAsignadoDetalle = _repoEsquemaAsignacionDetalle.GetBy(
                    w => esquemaAsignado.Select(s => s.Id).Contains(w.IdEsquemaEvaluacionPgeneral),
                    s => new
                    {
                        s.Id, s.Nombre, s.IdCriterioEvaluacion,
                        NombreCriterioEvaluacion = s.IdCriterioEvaluacionNavigation.Nombre, s.UrlArchivoInstrucciones,
                        PonderacionParametroEvaluacion = 0, PonderacionCriterioEvaluacion = 0
                    });

                return Ok(new
                {
                    ListadoMatriculas = listadoMatricula,
                    ListadoEscala = escalas,
                    ListadoParametroEvaluacion = esquemaAsignadoDetalle,
                    ListadoNota = listadoNota
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult RegistrarV2([FromForm] List<ParametroNotaRegistrarV2DTO> notas,
            [FromForm] int idPEspecifico, [FromForm] string usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ParametroEvaluacionNotaRepositorio _repoNotaParametro = new ParametroEvaluacionNotaRepositorio();
                List<ParametroEvaluacionNotaBO> listado = new List<ParametroEvaluacionNotaBO>();

                foreach (var nota in notas)
                {
                    if (_repoNota.Exist(w => w.Id == nota.Id))
                    {
                        ParametroEvaluacionNotaBO notaBo = _repoNotaParametro.FirstById(nota.Id);

                        notaBo.IdPespecifico = nota.IdPespecifico;
                        notaBo.Grupo = nota.Grupo;
                        notaBo.IdMatriculaCabecera = nota.IdMatriculaCabecera;
                        notaBo.IdParametroEvaluacion = nota.IdParametroEvaluacion;
                        notaBo.IdEscalaCalificacionDetalle = nota.IdEscalaCalificacionDetalle;

                        notaBo.Estado = true;
                        notaBo.FechaModificacion = DateTime.Now;
                        notaBo.UsuarioModificacion = usuario;

                        listado.Add(notaBo);
                    }
                    else
                    {
                        ParametroEvaluacionNotaBO notaBo = new ParametroEvaluacionNotaBO();
                        notaBo.Id = nota.Id;
                        notaBo.IdPespecifico = nota.IdPespecifico;
                        notaBo.Grupo = nota.Grupo;
                        notaBo.IdMatriculaCabecera = nota.IdMatriculaCabecera;
                        notaBo.IdParametroEvaluacion = nota.IdParametroEvaluacion;
                        notaBo.IdEscalaCalificacionDetalle = nota.IdEscalaCalificacionDetalle;

                        notaBo.Estado = true;
                        notaBo.FechaCreacion = DateTime.Now;
                        notaBo.FechaModificacion = DateTime.Now;
                        notaBo.UsuarioCreacion = usuario;
                        notaBo.UsuarioModificacion = usuario;

                        listado.Add(notaBo);
                    }
                }

                bool resultado;
                using (TransactionScope scope = new TransactionScope())
                {
                    resultado = _repoNotaParametro.Update(listado);
                    scope.Complete();
                }

                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + (e.InnerException != null ? ("- " + e.InnerException.Message) : ""));
            }
        }

        [Route("[Action]/{idMatriculaCabecera}/{idPEspecifico}/{grupo}")]
        [HttpGet]
        public ActionResult ListadoActividadesCalificablesDocentePorCurso(int idMatriculaCabecera, int idPEspecifico, int grupo)
        {
            try
            {
                PespecificoRepositorio _repoPespecifico = new PespecificoRepositorio(_integraDBContext);
                PgeneralRepositorio _repoPGeneral = new PgeneralRepositorio(_integraDBContext);

                var pespecifico = _repoPespecifico.FirstById(idPEspecifico);
                if (pespecifico == null)
                {
                    return BadRequest("El programa no existe");
                }
                if (pespecifico.TipoId == null)
                {
                    return BadRequest("No se pudo determinar la modalidad del programa");
                }

                int idModalidad = pespecifico.TipoId.Value;
                
                var listado = _repoNota.ListadoActividadesCalificablesDocentePorCurso(idMatriculaCabecera, idPEspecifico, grupo, idModalidad);
                EsquemaEvaluacionBO esquemaBO = new EsquemaEvaluacionBO(_integraDBContext);
                List<ParametroEvaluacionNotaBO> listadoEvaluar = new List<ParametroEvaluacionNotaBO>();
                foreach (var grupoActividad in listado.Where(w => w.IdEscalaCalificacionDetalle != null)
                    .GroupBy(g => g.IdEsquemaEvaluacionPGeneralDetalle))
                {
                    listadoEvaluar = new List<ParametroEvaluacionNotaBO>();
                    listadoEvaluar = grupoActividad.Select(s => new ParametroEvaluacionNotaBO()
                    {
                        IdParametroEvaluacion = s.IdParametroEvaluacion,
                        IdEscalaCalificacionDetalle = s.IdEscalaCalificacionDetalle
                    }).ToList();

                    decimal notaCriterio = esquemaBO.ObtenerNotaCriterioEvaluacion(listadoEvaluar);

                    grupoActividad.ForEach(f => f.NotaCriterioPGeneralDetalle = notaCriterio);
                }

                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        [RequestSizeLimit(200000000)]
        public ActionResult RegistrarV3([FromForm] List<ParametroNotaRegistrarV3DTO> notas,
            [FromForm] int idPEspecifico, [FromForm] string retroalimentacion, [FromForm] string usuario, [FromForm] IList<IFormFile> archivos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //validaciones
                if (notas == null || notas.Count == 0)
                    return BadRequest("No se enviaron parámetros a evaluar");
                if (notas.Any(w => w.IdEscalaCalificacionDetalle == null || w.IdEscalaCalificacionDetalle == 0))
                    return BadRequest("Debe de seleccionar un valor para todos los parámetros de evaluación");
                if (string.IsNullOrEmpty(retroalimentacion))
                    return BadRequest("Debe de ingresar una retroalimentación");

                ParametroEvaluacionNotaRepositorio _repoNotaParametro = new ParametroEvaluacionNotaRepositorio();
                List<ParametroEvaluacionNotaBO> listado = new List<ParametroEvaluacionNotaBO>();

                ParametroEvaluacionNotaBO bo = new ParametroEvaluacionNotaBO();
                string varUrl = null;
                string nombreArchivo = null;
                UtilBO utilBO = new UtilBO();
                foreach (var archivo in archivos)
                {
                    nombreArchivo = archivo.FileName;
                    string nombreArchivoBlob = string.Concat(DateTime.Now.ToString("yyyyMMdd-HHmmss"), "-",
                        utilBO.SlugNombreArchivo(archivo.FileName));
                    varUrl = bo.SubirArchivo(archivo.ConvertToByte(), archivo.ContentType, nombreArchivoBlob);
                }

                foreach (var nota in notas)
                {
                    if (_repoNota.Exist(w => w.Id == nota.Id))
                    {
                        ParametroEvaluacionNotaBO notaBo = _repoNotaParametro.FirstById(nota.Id);

                        notaBo.IdPespecifico = nota.IdPespecifico;
                        notaBo.Grupo = nota.Grupo;
                        notaBo.IdMatriculaCabecera = nota.IdMatriculaCabecera;
                        notaBo.IdEsquemaEvaluacionPGeneralDetalle = nota.IdEsquemaEvaluacionPGeneralDetalle;
                        notaBo.IdParametroEvaluacion = nota.IdParametroEvaluacion;
                        notaBo.IdEscalaCalificacionDetalle = nota.IdEscalaCalificacionDetalle;
                        notaBo.Retroalimentacion = retroalimentacion;
                        notaBo.NombreArchivoRetroalimentacion = string.IsNullOrEmpty(nombreArchivo)
                            ? notaBo.NombreArchivoRetroalimentacion
                            : nombreArchivo;
                        notaBo.UrlArchivoSubidoRetroalimentacion = string.IsNullOrEmpty(nombreArchivo)
                            ? notaBo.UrlArchivoSubidoRetroalimentacion
                            : varUrl;

                        notaBo.Estado = true;
                        notaBo.FechaModificacion = DateTime.Now;
                        notaBo.UsuarioModificacion = usuario;

                        listado.Add(notaBo);
                    }
                    else
                    {
                        ParametroEvaluacionNotaBO notaBo = new ParametroEvaluacionNotaBO();
                        notaBo.Id = nota.Id;
                        notaBo.IdPespecifico = nota.IdPespecifico;
                        notaBo.Grupo = nota.Grupo;
                        notaBo.IdMatriculaCabecera = nota.IdMatriculaCabecera;
                        notaBo.IdEsquemaEvaluacionPGeneralDetalle = nota.IdEsquemaEvaluacionPGeneralDetalle;
                        notaBo.IdParametroEvaluacion = nota.IdParametroEvaluacion;
                        notaBo.IdEscalaCalificacionDetalle = nota.IdEscalaCalificacionDetalle;
                        notaBo.Retroalimentacion = retroalimentacion;
                        notaBo.NombreArchivoRetroalimentacion = nombreArchivo;
                        notaBo.UrlArchivoSubidoRetroalimentacion = varUrl;

                        notaBo.Estado = true;
                        notaBo.FechaCreacion = DateTime.Now;
                        notaBo.FechaModificacion = DateTime.Now;
                        notaBo.UsuarioCreacion = usuario;
                        notaBo.UsuarioModificacion = usuario;

                        listado.Add(notaBo);
                    }
                }

                bool resultado;
                EsquemaEvaluacionBO esquemaBO = new EsquemaEvaluacionBO(_integraDBContext);
                //evalua si es un proyecto anterior
                if (notas.FirstOrDefault().EsProyectoAnterior)
                {
                    decimal notaCriterio = esquemaBO.ObtenerNotaCriterioEvaluacion(listado);

                    PgeneralProyectoAplicacionEnvioRepositorio _repoProyecto =
                        new PgeneralProyectoAplicacionEnvioRepositorio(_integraDBContext);
                    var idProyectoAnterior = notas.FirstOrDefault().IdProyectoAplicacionEnvioAnterior;
                    var boProyectoExistente = _repoProyecto.FirstById(idProyectoAnterior.Value);

                    boProyectoExistente.Comentarios = retroalimentacion;
                    boProyectoExistente.FechaCalificacion = DateTime.Now;
                    boProyectoExistente.Nota = notaCriterio;
                    boProyectoExistente.NombreArchivoRetroalimentacion = nombreArchivo;
                    boProyectoExistente.RutaArchivoRetroalimentacion = varUrl;
                    boProyectoExistente.IdEscalaCalificacionDetalle =
                        notas.FirstOrDefault().IdEscalaCalificacionDetalle;

                    boProyectoExistente.UsuarioModificacion = usuario;
                    boProyectoExistente.FechaModificacion = DateTime.Now;
                    resultado = _repoProyecto.Update(boProyectoExistente);

                    //registra el envio al alumno
                    PgeneralProyectoAplicacionEnvioBO boEnvio = new PgeneralProyectoAplicacionEnvioBO(_integraDBContext);
                    var envio = boEnvio.EnviarCorreoAlumnoProyectoCalificadoAulaAnterior(boProyectoExistente.Id, ValorEstatico.IdPlantillaAlumnoProyectoCalificado);

                    if (envio != null)
                    {
                        GmailCorreoRepositorio _repoCorreo = new GmailCorreoRepositorio();
                        var resultadoEnvio = _repoCorreo.Insert(new GmailCorreoBO()
                        {
                            IdEtiqueta = 1, //sent:1 , inbox:2
                            Asunto = envio.Asunto,
                            Fecha = DateTime.Now,
                            EmailBody = envio.EmailBody,
                            Seen = false,
                            Remitente = envio.Remitente,
                            Cc = envio.Cc,
                            Bcc = envio.Bcc,
                            Destinatarios = envio.Destinatarios,
                            IdPersonal = envio.IdPersonal,
                            Estado = true,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = "SYSTEM",
                            UsuarioModificacion = "SYSTEM",
                            //IdClasificacionPersona = oportunidad.IdClasificacionPersona
                        });
                    }
                }
                else
                {
                    //actualizar portal
                    int? PortalTareaEvaluacionTareaId = notas.FirstOrDefault().PortalTareaEvaluacionTareaId;
                    if (!PortalTareaEvaluacionTareaId.HasValue)
                        return BadRequest("No se pudo identificar el envio en el portal");

                    decimal notaCriterio = esquemaBO.ObtenerNotaCriterioEvaluacion(listado);
                    bool actualizacionPortal = _repoNotaParametro.RegistrarNotaPortal(PortalTareaEvaluacionTareaId.Value, notaCriterio);
                    if (!actualizacionPortal)
                        return BadRequest("No se pudo registrar la nota");

                    using (TransactionScope scope = new TransactionScope())
                    {
                        resultado = _repoNotaParametro.Update(listado);
                        scope.Complete();
                    }
                }
                
                return Ok(resultado);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + (e.InnerException != null ? ("- " + e.InnerException.Message) : ""));
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public ActionResult ObtenerDetalleCalificacionPorCurso(int idMatriculaCabecera, int idPEspecifico, int grupo)
        {
            try
            {
                EsquemaEvaluacionBO esquemaBO = new EsquemaEvaluacionBO(_integraDBContext);
                EsquemaEvaluacion_NotaCursoDTO detalleNota = esquemaBO.ObtenerDetalleCalificacionPorCurso(idMatriculaCabecera, idPEspecifico, grupo);
                
                return Ok(detalleNota);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + (e.InnerException != null ? ("- " + e.InnerException.Message) : ""));
            }
        }

        /// Tipo Función: GET
        /// Autor: Ansoli Espinoza
        /// Fecha: 25-01-2021
        /// Versión: 1.0
        /// <summary>
        /// EndPoint para el envio de los correos a docente de proyectos sin calificar del aula anterior
        /// </summary>
        /// <returns>Html de la vista</returns>
        [Route("[Action]")]
        [HttpGet]
        public ActionResult EnviarCorreoProyectoPendienteDocentesAulaAnterior()
        {
            try
            {
                PgeneralProyectoAplicacionEnvioBO bo = new PgeneralProyectoAplicacionEnvioBO(_integraDBContext);
                PgeneralProyectoAplicacionEnvioRepositorio _repo = new PgeneralProyectoAplicacionEnvioRepositorio(_integraDBContext);
                var listado = _repo.ListadoProyectoAplicacionAulaAnterior_SinCalificar();
                if (listado == null || listado.Count == 0)
                    return Ok(new {mensaje = "No hay proyectos con docente asignado pendientes de calificación"});
                var listadoEnviado = bo.EnviarCorreoProyectoPendienteDocentesAulaAnterior(listado, ValorEstatico.IdPlantillaDocenteProyectoPendienteCalificar);

                GmailCorreoRepositorio _repoCorreo = new GmailCorreoRepositorio();
                var resultadoEnvio = _repoCorreo.Insert(listadoEnviado.Select(s => new GmailCorreoBO()
                {
                    IdEtiqueta = 1,//sent:1 , inbox:2
                    Asunto = s.Asunto,
                    Fecha = DateTime.Now,
                    EmailBody = s.EmailBody,
                    Seen = false,
                    Remitente = s.Remitente,
                    Cc = s.Cc,
                    Bcc = s.Bcc,
                    Destinatarios = s.Destinatarios,
                    IdPersonal = s.IdPersonal,
                    Estado = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = "SYSTEM",
                    UsuarioModificacion = "SYSTEM",
                    //IdClasificacionPersona = oportunidad.IdClasificacionPersona
                }).ToList());
                return Ok(new {mensaje = "Correos enviados satisfactoriamente"});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Endpoint para regularizar el correo de confirmacion de calificacion del proyecto de aplicacion del aula anterior
        /// </summary>
        /// <param name="idEnvio">Id del envio - tabla: T_PgeneralProyectoAplicacionEnvio</param>
        /// <returns></returns>
        [Route("[Action]/{idEnvio}")]
        [HttpGet]
        public ActionResult EnviarCorreoAlumnoProyectoCalificadoAulaAnterior(int idEnvio)
        {
            try
            {
                PgeneralProyectoAplicacionEnvioBO bo = new PgeneralProyectoAplicacionEnvioBO(_integraDBContext);
                var envio = bo.EnviarCorreoAlumnoProyectoCalificadoAulaAnterior(idEnvio, ValorEstatico.IdPlantillaAlumnoProyectoCalificado);

                if (envio != null)
                {
                    GmailCorreoRepositorio _repoCorreo = new GmailCorreoRepositorio();
                    var resultadoEnvio = _repoCorreo.Insert(new GmailCorreoBO()
                    {
                        IdEtiqueta = 1, //sent:1 , inbox:2
                        Asunto = envio.Asunto,
                        Fecha = DateTime.Now,
                        EmailBody = envio.EmailBody,
                        Seen = false,
                        Remitente = envio.Remitente,
                        Cc = envio.Cc,
                        Bcc = envio.Bcc,
                        Destinatarios = envio.Destinatarios,
                        IdPersonal = envio.IdPersonal,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = "SYSTEM",
                        UsuarioModificacion = "SYSTEM",
                        //IdClasificacionPersona = oportunidad.IdClasificacionPersona
                    });
                }
                else
                {
                    return BadRequest("Ocurrió un error al enviar el correo al alumno");
                }

                return Ok("Correo enviado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
         
        }
    }
}