using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Aplicacion.Comercial.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Operaciones.BO;
using BSI.Integra.Aplicacion.Operaciones.Repositorio;
using BSI.Integra.Aplicacion.Planificacion.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
	/// Controlador: MatriculaMoodleSolicitud
	/// Autor: Jose Villena
	/// Fecha: 01/01/2021        
	/// <summary>
	/// Controlador de Matricula Moodle Solicitud
	/// </summary>
	[Route("api/MatriculaMoodleSolicitud")]
	[ApiController]
	public class MatriculaMoodleSolicitudController : Controller
	{
		private readonly integraDBContext _integraDBContext;
		public MatriculaMoodleSolicitudController(integraDBContext integraDBContext)
		{
			_integraDBContext = integraDBContext;
		}

		/// TipoFuncion: GET
		/// Autor: Jose Villena
		/// Fecha: 03/05/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene cursos moodle de la matrícula
		/// </summary>
		/// <returns> Cursos de la matrícula: List<MatriculaCursosMoodleAlumnoDTO></returns>
		[Route("[action]/{CodigoMatricula}/{IdOportunidad}")]
		[HttpGet]
		public ActionResult ObtenerCursosMoodleMatricula(string CodigoMatricula, int IdOportunidad)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				MatriculaCabeceraRepositorio _repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
				PespecificoRepositorio _repPespecifico = new PespecificoRepositorio(_integraDBContext);
				MatriculaMoodleSolicitudRepositorio _repMatriculaMoodleSolicitud = new MatriculaMoodleSolicitudRepositorio(_integraDBContext);
				List<MatriculaCursosMoodleAlumnoDTO> listaMatriculaAlumnosMoodle = new List<MatriculaCursosMoodleAlumnoDTO>();
				var matricula = _repMatriculaCabecera.FirstBy(x => x.CodigoMatricula.Equals(CodigoMatricula));
				var pespecifico = _repPespecifico.ObtenerDatosCompletosPespecificoPorId(matricula.IdPespecifico);
				var solicitudMatricula = _repMatriculaMoodleSolicitud.ObtenerSolicitudesMatriculaMoodlePorIdOportunidad(IdOportunidad);
				if (pespecifico.IdCursoMoodle == null)
				{
					return Ok(new { Estado = false });
				}
				else
				{
					MoodleCursoRepositorio _moodleCursoRepositorio = new MoodleCursoRepositorio(_integraDBContext);
					AlumnoRepositorio _repAlumno = new AlumnoRepositorio(_integraDBContext);
					List<MoodleCursoDTO> moodleCursos = new List<MoodleCursoDTO>();
					var moodleCurso = _moodleCursoRepositorio.ObtenerMoodleCurso(pespecifico.IdCursoMoodle.Value);
					var alumnoMoodle = _repAlumno.ObtenerAccesosInicialesMoodle(matricula.IdAlumno);
					if (moodleCurso.IdCategoria == 5) // Categoria especial para los presenciales
					{
						moodleCursos.Add(moodleCurso);
					}
					else
					{
						var categoria = _moodleCursoRepositorio.ObtenerCategoriaMoodleCurso(moodleCurso.IdCategoria);

						if (categoria == null)
						{
							throw new Exception("El curso no tiene categoria correcta.");
						}
						if (categoria.TipoCategoria.Equals("Diplomado")) //si es diplomado
						{
							moodleCursos = _moodleCursoRepositorio.ObtenerMoodleCursosPorCategoria(categoria.IdCategoria);
						}
						else
						{
							moodleCursos.Add(moodleCurso);
						}
					}

					foreach (var item in moodleCursos)
					{
						var matriculaMoodle = _moodleCursoRepositorio.ObtenerCursoMatriculadoPorCursoAlumnoMoodle(Convert.ToInt32(alumnoMoodle.IdMoodle), item.IdCursoMoodle).FirstOrDefault();
						
						MatriculaCursosMoodleAlumnoDTO curso = new MatriculaCursosMoodleAlumnoDTO();
						curso.IdCursoMoodle = item.IdCursoMoodle;
						curso.IdAlumnoMoodle = Convert.ToInt32(alumnoMoodle.IdMoodle);
						curso.IdAlumnoIntegra = matricula.IdAlumno;
						curso.CodigoMatricula = matricula.CodigoMatricula;
						curso.NombreCursoMoodle = item.NombreCursoMoodle;
						if (matriculaMoodle != null)
						{
							curso.IdMatriculaMoodle = matriculaMoodle.IdMatriculaMoodle;
							curso.FechaInicioMatricula = matriculaMoodle.FechaInicioMatricula;
							curso.FechaFinMatricula = matriculaMoodle.FechaFinMatricula;
							if (matriculaMoodle.FechaFinMatricula >= DateTime.Now.Date)
							{
								curso.Habilitado = 1;
							}
							else
							{
								curso.Habilitado = 0;
							}
						}
						else
						{
							curso.Habilitado = 0;
						}
						if (solicitudMatricula.Count > 0)
						{
							var solicitud = solicitudMatricula.Where(x => x.IdCursoMoodle == item.IdCursoMoodle).FirstOrDefault();
							if(solicitud != null)
							{
								curso.IdMatriculaMoodleSolicitud = solicitud.Id;
								curso.IdMatriculaMoodleSolicitudEstado = solicitud.IdMatriculaMoodleSolicitudEstado;
								curso.MatriculaMoodleSolicitudEstado = solicitud.MatriculaMoodleSolicitudEstado;
							}
						}
						listaMatriculaAlumnosMoodle.Add(curso);
					}
					return Ok(new { Estado = true, moodleCursos = listaMatriculaAlumnosMoodle });
				}

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: , Jose Villena.
		/// Fecha: 22/03/2021
		/// Versión: 1.0
		/// <summary>
		/// Modifica Fecha Inicio, Fecha Fin Integra y Moodle - Cronograma de Evaluaciones y sus Versiones
		/// </summary>
		/// <returns> Solicitud Fechas modificadas </returns>
		/// <returns> Nuevo Cronograma de Evaluaciones  </returns>
		/// <returns> Versiones  </returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult RealizarSolicitud([FromBody]RealizarSolicitudMatriculaMoodleDTO Solicitud)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				List<CronogramaAutoEvaluacionV2DTO> cronogramaUltimo = new List<CronogramaAutoEvaluacionV2DTO>();
				List<VersionCronogramaAutoEvaluacionDTO> versiones = new List<VersionCronogramaAutoEvaluacionDTO>();
				MatriculaMoodleSolicitudRepositorio repMatriculaMoodleSolicitud = new MatriculaMoodleSolicitudRepositorio(_integraDBContext);
				MatriculaMoodleSolicitudBO matriculaMoodleSolicitud = new MatriculaMoodleSolicitudBO();
				OportunidadRepositorio repOportunidad = new OportunidadRepositorio(_integraDBContext);
				MoodleCronogramaEvaluacionRepositorio repCronograma = new MoodleCronogramaEvaluacionRepositorio(_integraDBContext);
				MatriculasMoodleBO matriculasMoodle = new MatriculasMoodleBO(_integraDBContext);
				MatriculaCabeceraRepositorio repMatriculaCabecera = new MatriculaCabeceraRepositorio(_integraDBContext);
				PespecificoRepositorio repPespecifico = new PespecificoRepositorio(_integraDBContext);

				matriculaMoodleSolicitud.IdOportunidad = Solicitud.IdOportunidad;
				matriculaMoodleSolicitud.CodigoMatricula = Solicitud.CodigoMatricula;
				matriculaMoodleSolicitud.IdUsuarioMoodle = Solicitud.IdUsuarioMoodle;
				matriculaMoodleSolicitud.IdCursoMoodle = Solicitud.IdCursoMoodle;
				matriculaMoodleSolicitud.UsuarioSolicitud = Solicitud.Usuario;
				matriculaMoodleSolicitud.FechaSolicitud = DateTime.Now;
				matriculaMoodleSolicitud.IdMatriculaMoodleSolicitudEstado = Solicitud.IdMatriculaMoodleSolicitudEstado;
				matriculaMoodleSolicitud.FechaInicioMatricula = Solicitud.FechaInicioMatricula.Date;
				matriculaMoodleSolicitud.FechaFinMatricula = Solicitud.FechaFinMatricula.Date;
				matriculaMoodleSolicitud.Estado = true;
				matriculaMoodleSolicitud.UsuarioCreacion = Solicitud.Usuario;
				matriculaMoodleSolicitud.UsuarioModificacion = Solicitud.Usuario;
				matriculaMoodleSolicitud.FechaCreacion = DateTime.Now;
				matriculaMoodleSolicitud.FechaModificacion = DateTime.Now;
				var oportunidad = repOportunidad.FirstById(Solicitud.IdOportunidad);
				if (Solicitud.Habilitado == 0)
				{
					matriculaMoodleSolicitud.FechaFinMatricula = DateTime.Now.AddDays(-1).Date;
				}
				if (Solicitud.TipoPersonal.Equals("Coordinador"))
				{
					matriculaMoodleSolicitud.UsuarioAprobacion = Solicitud.Usuario;
					matriculaMoodleSolicitud.FechaAprobacion = DateTime.Now;
					if (Solicitud.Habilitado == 1)//Habilitado
					{
						var matriculaCorrecta = matriculasMoodle.RegistrarHabilitarMatriculaMoodleConFechas(Solicitud.IdUsuarioMoodle, Solicitud.IdCursoMoodle, Solicitud.FechaInicioMatricula, Solicitud.FechaFinMatricula, Solicitud.CodigoMatricula, oportunidad.IdAlumno, Solicitud.Usuario);
						if (matriculaCorrecta)
						{
							var regularizar = matriculasMoodle.RegularizarTmpMatriculasMoodle(Solicitud.IdUsuarioMoodle, Solicitud.IdCursoMoodle, Solicitud.CodigoMatricula, oportunidad.IdAlumno, Solicitud.Usuario);
							if (!regularizar)
							{
								return BadRequest(ModelState);
							}
							int matriculaAlumno = repMatriculaCabecera.ObtenerMatriculaAlumno(Solicitud.CodigoMatricula);
							var programaCursoAlumno = repPespecifico.ObtenerPespecificoTipoAlumno(matriculaAlumno);

							if (Solicitud.Flag == true && programaCursoAlumno.Tipo == "Online Asincronica")
                            {
								
								var respuesta = repCronograma.CongelarCrongrogramaFechaEspecifica(matriculaAlumno , Solicitud.FechaInicioMatricula);
								cronogramaUltimo = repCronograma.ObtenerCronogramaAutoEvaluacion_UltimaVersion(matriculaAlumno);
								versiones = repCronograma.ObtenerVersionesCronograma(matriculaAlumno);

							}
						}
						else
						{
							return BadRequest(ModelState);
						}
					}
					else // Deshabilitado
					{
						matriculaMoodleSolicitud.FechaFinMatricula = DateTime.Now.AddDays(-1).Date;
						if (Solicitud.IdMatriculaMoodle.HasValue)
						{
							var deshabilitar = matriculasMoodle.DeshabilitarMatriculaPorIdMatriculaMoodle(Solicitud.IdMatriculaMoodle.Value, Solicitud.IdUsuarioMoodle, Solicitud.IdCursoMoodle);
							if (deshabilitar)
							{
								var regularizar = matriculasMoodle.RegularizarTmpMatriculasMoodle(Solicitud.IdUsuarioMoodle, Solicitud.IdCursoMoodle, Solicitud.CodigoMatricula, oportunidad.IdAlumno, Solicitud.Usuario);
								if (!regularizar)
								{
									return BadRequest(ModelState);
								}
							}
							else
							{
								return BadRequest(ModelState);
							}
						}
						else
							return BadRequest(ModelState);
					}
				}
				repMatriculaMoodleSolicitud.Insert(matriculaMoodleSolicitud);

				return Ok(new { matriculaMoodleSolicitud = matriculaMoodleSolicitud, CronogramaUltimaVersion = cronogramaUltimo, Versiones = versiones });
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}


		[Route("[Action]")]
		[HttpPost]
		public ActionResult AprobarRechazarSolicitud([FromBody]AprobarRechazarMatriculaMoodleSolicitudDTO AprobarRechazarSolicitud)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				MatriculaMoodleSolicitudRepositorio _repMatriculaMoodleSolicitud = new MatriculaMoodleSolicitudRepositorio(_integraDBContext);
				MatriculasMoodleBO matriculasMoodle = new MatriculasMoodleBO(_integraDBContext);
				OportunidadRepositorio _repOportunidad = new OportunidadRepositorio(_integraDBContext);
				var solicitud = _repMatriculaMoodleSolicitud.FirstById(AprobarRechazarSolicitud.IdMatriculaMoodleSolicitud);
				var oportunidad = _repOportunidad.FirstById(solicitud.IdOportunidad);
				if (AprobarRechazarSolicitud.IdMatriculaMoodleSolicitudEstado == 2) //Aprobado
				{
					if (solicitud.FechaFinMatricula.Date >= DateTime.Now.Date)//Habilitado
					{
						var matriculaCorrecta = matriculasMoodle.RegistrarHabilitarMatriculaMoodleConFechas(solicitud.IdUsuarioMoodle, solicitud.IdCursoMoodle, solicitud.FechaInicioMatricula, solicitud.FechaFinMatricula, solicitud.CodigoMatricula, oportunidad.IdAlumno, AprobarRechazarSolicitud.Usuario);
						if (matriculaCorrecta)
						{
							var regularizar = matriculasMoodle.RegularizarTmpMatriculasMoodle(solicitud.IdUsuarioMoodle, solicitud.IdCursoMoodle, solicitud.CodigoMatricula, oportunidad.IdAlumno, AprobarRechazarSolicitud.Usuario);
							if (!regularizar)
							{
								return BadRequest(ModelState);
							}
						}
						else
						{
							return BadRequest(ModelState);
						}
					}
					else // Deshabilitado
					{
						solicitud.FechaFinMatricula = DateTime.Now.AddDays(-1).Date;
						if (AprobarRechazarSolicitud.IdMatriculaMoodle.HasValue)
						{
							var deshabilitar = matriculasMoodle.DeshabilitarMatriculaPorIdMatriculaMoodle(AprobarRechazarSolicitud.IdMatriculaMoodle.Value, solicitud.IdUsuarioMoodle, solicitud.IdCursoMoodle);
							if (deshabilitar)
							{
								var regularizar = matriculasMoodle.RegularizarTmpMatriculasMoodle(solicitud.IdUsuarioMoodle, solicitud.IdCursoMoodle, solicitud.CodigoMatricula, oportunidad.IdAlumno, AprobarRechazarSolicitud.Usuario);
								if (!regularizar)
								{
									return BadRequest(ModelState);
								}
							}
							else
							{
								return BadRequest(ModelState);
							}
						}
						else
							return BadRequest(ModelState);
					}
				}

				solicitud.IdMatriculaMoodleSolicitudEstado = AprobarRechazarSolicitud.IdMatriculaMoodleSolicitudEstado;
				solicitud.UsuarioAprobacion = AprobarRechazarSolicitud.Usuario;
				solicitud.FechaAprobacion = DateTime.Now;
				solicitud.UsuarioModificacion = AprobarRechazarSolicitud.Usuario;
				solicitud.FechaModificacion = DateTime.Now;
				_repMatriculaMoodleSolicitud.Update(solicitud);
				return Ok(solicitud);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Jose Villena
		/// Fecha: 03/05/2021
		/// Versión: 1.0
		/// <summary>
		/// Listar Solicitudes Matricula Moodle
		/// </summary>
		/// <returns> Retorna lista solicitudes:  List<MatriculaMoodleSolicitudDTO> </returns>
		[Route("[Action]")]
		[HttpPost]
		public ActionResult ListaSolicitudesMatriculaMoodle([FromBody]FiltroListaSolicitudMatriculaMoodleDTO Filtro)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				MatriculaMoodleSolicitudRepositorio _repMatriculaMoodleSolicitud = new MatriculaMoodleSolicitudRepositorio(_integraDBContext);
				var listaSolicitudes = _repMatriculaMoodleSolicitud.ObtenerSolicitudesMatriculaMoodlePorIdOportunidadIdCursoMoodle(Filtro.IdOportunidad,Filtro.IdCursoMoodle);
				return Ok(listaSolicitudes);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
