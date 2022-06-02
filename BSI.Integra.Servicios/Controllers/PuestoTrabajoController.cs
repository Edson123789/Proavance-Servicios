using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Aplicacion.GestionPersonas.Repositorio;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
	/// Controlador: PuestoTrabajoController
	/// Autor: Britsel C., Luis H., Edgar S.
	/// Fecha: 29/01/2021
	/// <summary>
	/// Gestión de Puestos de Trabajo y perfiles de puesto
	/// </summary>
	[Route("api/PuestoTrabajo")]

	public class PuestoTrabajoController : ControllerBase
	{
		private readonly integraDBContext _integraDBContext;
		private readonly PuestoTrabajoRepositorio _repPuestoTrabajo;
		private readonly PersonalAreaTrabajoRepositorio _repPersonalAreaTrabajo;
		private readonly PuestoTrabajoDependenciaRepositorio _repPuestoTrabajoDependencia;
		private readonly PuestoTrabajoRelacionInternaRepositorio _repPuestoTrabajoRelacionInterna;
		private readonly PuestoTrabajoPuestoACargoRepositorio _repPuestoTrabajoPuestoACargo;
		private readonly PuestoTrabajoFuncionRepositorio _repPuestoTrabajoFuncion;
		private readonly PuestoTrabajoReporteRepositorio _repPuestoTrabajoReporte;
		private readonly PuestoTrabajoCursoComplementarioRepositorio _repPuestoTrabajoCursoComplementario;
		private readonly PuestoTrabajoExperienciaRepositorio _repPuestoTrabajoExperiencia;
		private readonly PuestoTrabajoCaracteristicaPersonalRepositorio _repPuestoTrabajoCaracteristicaPersonal;
		private readonly PuestoTrabajoFormacionAcademicaRepositorio _repPuestoTrabajoFormacionAcademica;

		private readonly PersonalTipoFuncionRepositorio _repPersonalTipoFuncion;
		private readonly FrecuenciaPuestoTrabajoRepositorio _repFrecuenciaPuestoTrabajo;
		private readonly TipoCompetenciaTecnicaRepositorio _repTipoCompetenciaTecnica;
		private readonly CompetenciaTecnicaRepositorio _reoCompetenciaTecnica;
		private readonly NivelCompetenciaTecnicaRepositorio _repNivelCompetenciaTecnica;
		private readonly ExperienciaRepositorio _repExperiencia;
		private readonly TipoExperienciaRepositorio _repTipoExperiencia;
		private readonly SexoRepositorio _repSexo;
		private readonly EstadoCivilRepositorio _repEstadoCivil;
		private readonly TipoFormacionRepositorio _repTipoFormacion;
		private readonly NivelEstudioRepositorio _repNivelEstudio;
		private readonly AreaFormacionRepositorio _repAreaFormacion;
		private readonly CentroEstudioRepositorio _repCentroEstudio;
		private readonly GradoEstudioRepositorio _repGradoEstudio;

		private readonly PerfilPuestoTrabajoRepositorio _repPerfilPuestoTrabajo;
		private readonly PuestoTrabajoRelacionRepositorio _repPuestoTrabajoRelacion;
		private readonly PuestoTrabajoRelacionDetalleRepositorio _repPuestoTrabajoRelacionDetalle;

		private readonly ProcesoSeleccionRangoRepositorio _repProcesoSeleccionRango;
		private readonly PuestoTrabajoPuntajeCalificacionRepositorio _repPuestoTrabajoPuntajeCalificacion;

		private readonly PerfilPuestoTrabajoPersonalAprobacionRepositorio _repPerfilPuestoTrabajoPersonalAprobacion;
		private readonly PerfilPuestoTrabajoEstadoSolicitudRepositorio _repPerfilPuestoTrabajoEstadoSolicitud;

		public PuestoTrabajoController(integraDBContext IntegraDBContext)
		{
			_integraDBContext = IntegraDBContext;
			_repPuestoTrabajo = new PuestoTrabajoRepositorio(_integraDBContext);
			_repPersonalAreaTrabajo = new PersonalAreaTrabajoRepositorio(_integraDBContext);
			_repPuestoTrabajoDependencia = new PuestoTrabajoDependenciaRepositorio(_integraDBContext);
			_repPuestoTrabajoRelacionInterna = new PuestoTrabajoRelacionInternaRepositorio(_integraDBContext);
			_repPuestoTrabajoPuestoACargo = new PuestoTrabajoPuestoACargoRepositorio(_integraDBContext);
			_repPuestoTrabajoFuncion = new PuestoTrabajoFuncionRepositorio(_integraDBContext);
			_repPuestoTrabajoReporte = new PuestoTrabajoReporteRepositorio(_integraDBContext);
			_repPuestoTrabajoCursoComplementario = new PuestoTrabajoCursoComplementarioRepositorio(_integraDBContext);
			_repPuestoTrabajoExperiencia = new PuestoTrabajoExperienciaRepositorio(_integraDBContext);
			_repPuestoTrabajoCaracteristicaPersonal = new PuestoTrabajoCaracteristicaPersonalRepositorio(_integraDBContext);
			_repPuestoTrabajoFormacionAcademica = new PuestoTrabajoFormacionAcademicaRepositorio(_integraDBContext);

			_repPersonalTipoFuncion = new PersonalTipoFuncionRepositorio(_integraDBContext);
			_repFrecuenciaPuestoTrabajo = new FrecuenciaPuestoTrabajoRepositorio(_integraDBContext);
			_repTipoCompetenciaTecnica = new TipoCompetenciaTecnicaRepositorio(_integraDBContext);
			_reoCompetenciaTecnica = new CompetenciaTecnicaRepositorio(_integraDBContext);
			_repNivelCompetenciaTecnica = new NivelCompetenciaTecnicaRepositorio(_integraDBContext);
			_repTipoExperiencia = new TipoExperienciaRepositorio(_integraDBContext);
			_repExperiencia = new ExperienciaRepositorio(_integraDBContext);
			_repSexo = new SexoRepositorio(_integraDBContext);
			_repEstadoCivil = new EstadoCivilRepositorio(_integraDBContext);
			_repTipoFormacion = new TipoFormacionRepositorio(_integraDBContext);
			_repNivelEstudio = new NivelEstudioRepositorio(_integraDBContext);
			_repAreaFormacion = new AreaFormacionRepositorio(_integraDBContext);
			_repCentroEstudio = new CentroEstudioRepositorio(_integraDBContext);
			_repGradoEstudio = new GradoEstudioRepositorio(_integraDBContext);

			_repPerfilPuestoTrabajo = new PerfilPuestoTrabajoRepositorio(_integraDBContext);
			_repPuestoTrabajoRelacion = new PuestoTrabajoRelacionRepositorio(_integraDBContext);
			_repPuestoTrabajoRelacionDetalle = new PuestoTrabajoRelacionDetalleRepositorio(_integraDBContext);

			_repProcesoSeleccionRango = new ProcesoSeleccionRangoRepositorio(_integraDBContext);
			_repPuestoTrabajoPuntajeCalificacion = new PuestoTrabajoPuntajeCalificacionRepositorio(_integraDBContext);

			_repPerfilPuestoTrabajoPersonalAprobacion = new PerfilPuestoTrabajoPersonalAprobacionRepositorio(_integraDBContext);

			_repPerfilPuestoTrabajoEstadoSolicitud = new PerfilPuestoTrabajoEstadoSolicitudRepositorio(_integraDBContext);
		}

		/// TipoFuncion: GET
		/// Autor: Luis H, Edgar S.
		/// Fecha: 25/01/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene lista de personal autorizado para aprobación de versión
		/// </summary>
		/// <returns>Obtiene lista de personal autorizado : List<PuestoTrabajoPersonalAprobacionBO> </returns>
		[HttpGet]
		[Route("[Action]/{IdPersonal}")]
		public ActionResult EsPersonalAprobacionVersion(int IdPersonal)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaPersonalAprobacion = _repPerfilPuestoTrabajoPersonalAprobacion.GetBy(x => x.IdPersonal == IdPersonal).Select(x => x.IdPuestoTrabajo).ToList();

				return Ok(listaPersonalAprobacion);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Luis H, Edgar S.
		/// Fecha: 25/01/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene combos para módulo
		/// </summary>
		/// <returns>Objeto Agrupado</returns>
		[HttpPost]
		[Route("[Action]")]
		public ActionResult ObtenerCombos()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaPersonalAreaTrabajo = _repPersonalAreaTrabajo.ObtenerTodoFiltro();
				var listaPuestoTrabajo = _repPuestoTrabajo.GetFiltroIdNombre();
				var listaTipoFuncion = _repPersonalTipoFuncion.ObtenerPersonalTipoFuncion();
				var listaFrecuenciaPuestoTrabajo = _repFrecuenciaPuestoTrabajo.ObtenerFrecuenciaPuestoTrabajo();
				var listaTipoCompetenciaTecnica = _repTipoCompetenciaTecnica.ObtenerListaParaFiltro();
				var listaCompetenciaTecnica = _reoCompetenciaTecnica.ObtenerListaParaFiltro();
				var listaNivelCompetenciaTecnica = _repNivelCompetenciaTecnica.ObtenerListaParaFiltro();
				var listaExperiencia = _repExperiencia.ObtenerExperiencia();
				var listaTipoExperiencia = _repTipoExperiencia.ObtenerListaParaFiltro();
				var listaSexo = _repSexo.GetFiltroIdNombre();
				FiltroIdNombreDTO op = new FiltroIdNombreDTO
				{
					Id = 8,
					Nombre = "Indistinto"
				};
				listaSexo.Add(op);

				var listaEstadoCivil = _repEstadoCivil.GetFiltroIdNombre();
				listaEstadoCivil.Add(op);
				var listaTipoFormacion = _repTipoFormacion.ObtenerTipoFormacion();
				var listaNivelEstudio = _repNivelEstudio.ObtenerListaNivelEstudio();
				var listaAreaFormacion = _repAreaFormacion.ObtenerAreaFormacionFiltro();
				var listaCentroEstudio = _repCentroEstudio.ObtenerListaParaFiltro();
				var listaGradoEstudio = _repGradoEstudio.ObtenerListaParaFiltro();
				var listaRango = _repProcesoSeleccionRango.ObtenerListaParaCombo();

				return Ok(new
				{
					ListaPersonalAreaTrabajo = listaPersonalAreaTrabajo,
					ListaPuestoTrabajo = listaPuestoTrabajo,
					ListaTipoFuncion = listaTipoFuncion,
					ListaFrecuenciaPuestoTrabajo = listaFrecuenciaPuestoTrabajo,
					ListaTipoCompetenciaTecnica = listaTipoCompetenciaTecnica,
					ListaCompetenciaTecnica  = listaCompetenciaTecnica,
					ListaNivelCompetenciaTecnica = listaNivelCompetenciaTecnica,
					ListaExperiencia  = listaExperiencia,
					ListaTipoExperiencia  = listaTipoExperiencia,
					ListaSexo  = listaSexo,
					ListaEstadoCivil  = listaEstadoCivil,
					ListaTipoFormacion  = listaTipoFormacion,
					ListaNivelEstudio  = listaNivelEstudio,
					ListaAreaFormacion  = listaAreaFormacion,
					ListaCentroEstudio  = listaCentroEstudio,
					ListaGradoEstudio  = listaGradoEstudio,
					ListaRango = listaRango
				});
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}


		/// Tipo Función: POST 
		/// Autor: Edgar S.
		/// Fecha: 19/01/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene registros de Puestos de Trabajo y la última modificación realizada en el módulo
		/// </summary>
		/// <returns>Lista de Puestos de Trabajo : List<PuestoTrabajoEnviarDTO> </returns>
		[HttpPost]
		[Route("[Action]")]
		public ActionResult ObtenerPuestoTrabajoRegistrado()
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaPuestoTrabajoFecha = _repPuestoTrabajo.ObtenerPuestoTrabajoRegistradoFechaModificacion();
				List<PuestoTrabajoEnviarDTO> listaFiltrada = new List<PuestoTrabajoEnviarDTO>();
				if (listaPuestoTrabajoFecha.Count > 0)
				{
					var grupoPuestoTrabajo = listaPuestoTrabajoFecha.GroupBy(u => new { u.Id, u.Nombre, u.IdPersonalAreaTrabajo, u.IdPerfilPuestoTrabajo, u.PersonalAreaTrabajo })
						.Select(group => new PuestoFechaDTO
						{
							Id = group.Key.Id,
							Nombre = group.Key.Nombre,
							PersonalAreaTrabajo = group.Key.PersonalAreaTrabajo,
							IdPersonalAreaTrabajo = group.Key.IdPersonalAreaTrabajo,
							IdPerfilPuestoTrabajo = group.Key.IdPerfilPuestoTrabajo,
							ListaFechaModificacion = group.Select(x => new FechaModificacionDTO
							{
								PuestoTrabajoFechaModificacion = x.PuestoTrabajoFechaModificacion,
								PerfilPuestoTrabajoFechaModificacion = x.PerfilPuestoTrabajoFechaModificacion,
								PersonalAreaFechaModificacion = x.PersonalAreaFechaModificacion,
								PuestoTrabajoCaracteristicaPersonalFechaModificacion = x.PuestoTrabajoCaracteristicaPersonalFechaModificacion,
								PuestoTrabajoCursoComplementarioFechaModificacion = x.PuestoTrabajoCursoComplementarioFechaModificacion,
								PuestoTrabajoExperienciaFechaModificacion = x.PuestoTrabajoExperienciaFechaModificacion,
								PuestoTrabajoFormacionAcademicaFechaModificacion = x.PuestoTrabajoFormacionAcademicaFechaModificacion,
								PuestoTrabajoFuncionFechaModificacion = x.PuestoTrabajoFuncionFechaModificacion,
								PuestoTrabajoRelacionFechaModificacion = x.PuestoTrabajoRelacionFechaModificacion,
								PuestoTrabajoRelacionDetalleFechaModificacion = x.PuestoTrabajoRelacionDetalleFechaModificacion,
								PuestoTrabajoReporteFechaModificacion = x.PuestoTrabajoReporteFechaModificacion,
								PuestoTrabajoPuntajeCalificacionFechaModificacion = x.PuestoTrabajoPuntajeCalificacionFechaModificacion,
								ModuloSistemaPuestoTrabajoFechaModificacion = x.ModuloSistemaPuestoTrabajoFechaModificacion,
								PuestoTrabajoUsuarioModificacion = x.PuestoTrabajoUsuarioModificacion,
								PerfilPuestoTrabajoUsuarioModificacion = x.PerfilPuestoTrabajoUsuarioModificacion,
								PersonalAreaUsuarioModificacion = x.PersonalAreaUsuarioModificacion,
								PuestoTrabajoCaracteristicaPersonalUsuarioModificacion = x.PuestoTrabajoCaracteristicaPersonalUsuarioModificacion,
								PuestoTrabajoCursoComplementarioUsuarioModificacion = x.PuestoTrabajoCursoComplementarioUsuarioModificacion,
								PuestoTrabajoExperienciaUsuarioModificacion = x.PuestoTrabajoExperienciaUsuarioModificacion,
								PuestoTrabajoFormacionAcademicaUsuarioModificacion = x.PuestoTrabajoFormacionAcademicaUsuarioModificacion,
								PuestoTrabajoFuncionUsuarioModificacion = x.PuestoTrabajoFuncionUsuarioModificacion,
								PuestoTrabajoRelacionUsuarioModificacion = x.PuestoTrabajoRelacionUsuarioModificacion,
								PuestoTrabajoRelacionDetalleUsuarioModificacion = x.PuestoTrabajoRelacionDetalleUsuarioModificacion,
								PuestoTrabajoReporteUsuarioModificacion = x.PuestoTrabajoReporteUsuarioModificacion,
								PuestoTrabajoPuntajeCalificacionUsuarioModificacion = x.PuestoTrabajoPuntajeCalificacionUsuarioModificacion,
								ModuloSistemaPuestoTrabajoUsuarioModificacion = x.ModuloSistemaPuestoTrabajoUsuarioModificacion
							}).ToList()
						}
					).ToList();

					List<FechaUsuarioDTO> listaFechaModificacion;
					foreach (var item in grupoPuestoTrabajo)
					{
						listaFechaModificacion = new List<FechaUsuarioDTO>();
						List<FechaUsuarioDTO> listaPuestoTrabajoFechaModificacion = item.ListaFechaModificacion.Where(x => x.PuestoTrabajoFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PuestoTrabajoFechaModificacion.GetValueOrDefault(), Usuario = x.PuestoTrabajoUsuarioModificacion }).ToList();
						List<FechaUsuarioDTO> listaPerfilPuestoTrabajoFecha = item.ListaFechaModificacion.Where(x => x.PerfilPuestoTrabajoFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PerfilPuestoTrabajoFechaModificacion.GetValueOrDefault(), Usuario = x.PerfilPuestoTrabajoUsuarioModificacion }).ToList();
						List<FechaUsuarioDTO> listaPersonalAreaFechaModificacion = item.ListaFechaModificacion.Where(x => x.PersonalAreaFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PersonalAreaFechaModificacion.GetValueOrDefault(), Usuario = x.PersonalAreaUsuarioModificacion }).ToList();
						List<FechaUsuarioDTO> listaPuestoTrabajoCaracteristicaPersonalFechaModificacion = item.ListaFechaModificacion.Where(x => x.PuestoTrabajoCaracteristicaPersonalFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PuestoTrabajoCaracteristicaPersonalFechaModificacion.GetValueOrDefault(), Usuario = x.PuestoTrabajoCaracteristicaPersonalUsuarioModificacion }).ToList();
						List<FechaUsuarioDTO> listaPuestoTrabajoCursoComplementarioFechaModificacion = item.ListaFechaModificacion.Where(x => x.PuestoTrabajoCursoComplementarioFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PuestoTrabajoCursoComplementarioFechaModificacion.GetValueOrDefault(), Usuario = x.PuestoTrabajoCursoComplementarioUsuarioModificacion }).ToList();
						List<FechaUsuarioDTO> listaPuestoTrabajoExperienciaFechaModificacion = item.ListaFechaModificacion.Where(x => x.PuestoTrabajoExperienciaFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PuestoTrabajoExperienciaFechaModificacion.GetValueOrDefault(), Usuario = x.PuestoTrabajoExperienciaUsuarioModificacion }).ToList();
						List<FechaUsuarioDTO> listaPuestoTrabajoFormacionAcademicaFechaModificacion = item.ListaFechaModificacion.Where(x => x.PuestoTrabajoFormacionAcademicaFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PuestoTrabajoFormacionAcademicaFechaModificacion.GetValueOrDefault(), Usuario = x.PuestoTrabajoFormacionAcademicaUsuarioModificacion }).ToList();
						List<FechaUsuarioDTO> listaPuestoTrabajoFuncionFechaModificacion = item.ListaFechaModificacion.Where(x => x.PuestoTrabajoFuncionFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PuestoTrabajoFuncionFechaModificacion.GetValueOrDefault(), Usuario = x.PuestoTrabajoFuncionUsuarioModificacion }).ToList();
						List<FechaUsuarioDTO> listaPuestoTrabajoRelacionFechaModificacion = item.ListaFechaModificacion.Where(x => x.PuestoTrabajoRelacionFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PuestoTrabajoRelacionFechaModificacion.GetValueOrDefault(), Usuario = x.PuestoTrabajoRelacionUsuarioModificacion }).ToList();
						List<FechaUsuarioDTO> listaPuestoTrabajoRelacionDetalleFechaModificacion = item.ListaFechaModificacion.Where(x => x.PuestoTrabajoRelacionDetalleFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PuestoTrabajoRelacionDetalleFechaModificacion.GetValueOrDefault(), Usuario = x.PuestoTrabajoRelacionDetalleUsuarioModificacion }).ToList();
						List<FechaUsuarioDTO> listaPuestoTrabajoReporteFechaModificacion = item.ListaFechaModificacion.Where(x => x.PuestoTrabajoReporteFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PuestoTrabajoReporteFechaModificacion.GetValueOrDefault(), Usuario = x.PuestoTrabajoReporteUsuarioModificacion }).ToList();
						List<FechaUsuarioDTO> listaPuestoTrabajoPuntajeCalificacionFechaModificacion = item.ListaFechaModificacion.Where(x => x.PuestoTrabajoPuntajeCalificacionFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.PuestoTrabajoPuntajeCalificacionFechaModificacion.GetValueOrDefault(), Usuario = x.PuestoTrabajoPuntajeCalificacionUsuarioModificacion }).ToList();
						List<FechaUsuarioDTO> listaModuloSistemaPuestoTrabajoFechaModificacion = item.ListaFechaModificacion.Where(x => x.ModuloSistemaPuestoTrabajoFechaModificacion != null).Select(x => new FechaUsuarioDTO { Fecha = x.ModuloSistemaPuestoTrabajoFechaModificacion.GetValueOrDefault(), Usuario = x.ModuloSistemaPuestoTrabajoUsuarioModificacion }).ToList();
						listaFechaModificacion.AddRange(listaPuestoTrabajoFechaModificacion);
						listaFechaModificacion.AddRange(listaPerfilPuestoTrabajoFecha);
						listaFechaModificacion.AddRange(listaPersonalAreaFechaModificacion);
						listaFechaModificacion.AddRange(listaPuestoTrabajoCaracteristicaPersonalFechaModificacion);
						listaFechaModificacion.AddRange(listaPuestoTrabajoCursoComplementarioFechaModificacion);
						listaFechaModificacion.AddRange(listaPuestoTrabajoExperienciaFechaModificacion);
						listaFechaModificacion.AddRange(listaPuestoTrabajoFormacionAcademicaFechaModificacion);
						listaFechaModificacion.AddRange(listaPuestoTrabajoFuncionFechaModificacion);
						listaFechaModificacion.AddRange(listaPuestoTrabajoRelacionFechaModificacion);
						listaFechaModificacion.AddRange(listaPuestoTrabajoRelacionDetalleFechaModificacion);
						listaFechaModificacion.AddRange(listaPuestoTrabajoReporteFechaModificacion);
						listaFechaModificacion.AddRange(listaPuestoTrabajoPuntajeCalificacionFechaModificacion);
						listaFechaModificacion.AddRange(listaModuloSistemaPuestoTrabajoFechaModificacion);
						PuestoTrabajoEnviarDTO registroPuestoTrabajo;
						DateTime fechaReciente = listaFechaModificacion[0].Fecha;
						string usuarioReciente = listaFechaModificacion[0].Usuario;
						foreach (var item2 in listaFechaModificacion)
						{
							if (item2.Fecha > fechaReciente)
							{
								fechaReciente = item2.Fecha;
								usuarioReciente = item2.Usuario;
							}
						}
						var fechaRecienteLetras = fechaReciente.ToString("dd/M/yyy", CultureInfo.CreateSpecificCulture("es-PE"));

						registroPuestoTrabajo = new PuestoTrabajoEnviarDTO()
						{
							Id = item.Id,
							IdPersonalAreaTrabajo = item.IdPersonalAreaTrabajo,
							PersonalAreaTrabajo = item.PersonalAreaTrabajo,
							Nombre = item.Nombre,
							IdPerfilPuestoTrabajo = item.IdPerfilPuestoTrabajo,
							Objetivo = item.Objetivo,
							Descripcion = item.Descripcion,
							UsuarioModificacion = usuarioReciente,
							FechaModificacion = fechaRecienteLetras
						};
						listaFiltrada.Add(registroPuestoTrabajo);
					}
				}
				return Ok(listaFiltrada);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Luis H, Edgar S.
		/// Fecha: 25/01/2021
		/// Versión: 1.0
		/// <summary>
		/// Inserta registro de Puesto de Trabajo
		/// </summary>
		/// <returns> Obtiene confirmación de inserción : Bool</returns>
		[HttpPost]
		[Route("[Action]")]
		public ActionResult InsertarPuestoTrabajo([FromBody]CompuestoPuestoTrabajo Compuesto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				bool res = false;
				using (TransactionScope scope = new TransactionScope())
				{
					PuestoTrabajoBO puestoTrabajo = new PuestoTrabajoBO()
					{
						Nombre = Compuesto.PuestoTrabajo.Nombre,
						IdPersonalAreaTrabajo = Compuesto.PuestoTrabajo.IdPersonalAreaTrabajo,
						Estado = true,
						UsuarioCreacion = Compuesto.Usuario,
						UsuarioModificacion = Compuesto.Usuario,
						FechaCreacion = DateTime.Now,
						FechaModificacion = DateTime.Now
					};
					res = _repPuestoTrabajo.Insert(puestoTrabajo);
					scope.Complete();
				}
				return Ok(res);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Luis H, Edgar S.
		/// Fecha: 25/01/2021
		/// Versión: 1.0
		/// <summary>
		/// Actualiza Puesto de Trabajo
		/// </summary>
		/// <returns> Obtiene confirmación de actualización : Bool </returns>
		[HttpPost]
		[Route("[Action]")]
		public ActionResult ActualizarPuestoTrabajo([FromBody]CompuestoPuestoTrabajo Compuesto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				bool res = false;
				if (_repPuestoTrabajo.Exist(Compuesto.PuestoTrabajo.Id))
				{
					var puestoTrabajo = _repPuestoTrabajo.FirstById(Compuesto.PuestoTrabajo.Id);
					using (TransactionScope scope = new TransactionScope())
					{
						puestoTrabajo.Nombre = Compuesto.PuestoTrabajo.Nombre;
						puestoTrabajo.IdPersonalAreaTrabajo = Compuesto.PuestoTrabajo.IdPersonalAreaTrabajo;
						puestoTrabajo.UsuarioModificacion = Compuesto.Usuario;
						puestoTrabajo.FechaModificacion = DateTime.Now;
						res = _repPuestoTrabajo.Update(puestoTrabajo);
						scope.Complete();
					}
				}

				return Ok(res);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Luis H, Edgar S.
		/// Fecha: 25/01/2021
		/// Versión: 1.0
		/// <summary>
		/// Elimina puesto de trabajo
		/// </summary>
		/// <returns>Obtiene confirmación de eliminación : Bool </returns>
		[HttpPost]
		[Route("[Action]")]
		public ActionResult EliminarPuestoTrabajo([FromBody]EliminarDTO PuestoTrabajo)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				bool res = false;
				if (_repPuestoTrabajo.Exist(PuestoTrabajo.Id))
				{

					using (TransactionScope scope = new TransactionScope())
					{
						res = _repPuestoTrabajo.Delete(PuestoTrabajo.Id, PuestoTrabajo.NombreUsuario);
						scope.Complete();
					}
				}
				return Ok(res);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Luis H, Edgar S.
		/// Fecha: 25/01/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Perfil de Puesto de Trabajo
		/// </summary>
		/// <returns>
		///			 Obtiene Perfil de Puesto de Trabajo
		///			 ListaPuestoTrabajoRelacion, ListaPuestoTrabajoFuncion, ListaPuestoTrabajoReporte, ListaPuestoTrabajoCursoComplementario,
		///			 ListaPuestoTrabajoExperiencia, ListaPuestoTrabajoCaracteristicaPersonal, ListaPuestoTrabajoFormacionAcademica,
		///			 ListaEvaluacionesPuntajeCalificacion, ListaEvaluaciones
		///	</returns>		
		[HttpPost]
		[Route("[Action]")]
		public ActionResult ObtenerPerfilPuestoTrabajo([FromBody] int IdPerfilPuestoTrabajo)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				var listaPuestoTrabajoRelacion = _repPerfilPuestoTrabajo.ObtenerPuestoTrabajoRelacion(IdPerfilPuestoTrabajo);
				var listaPuestoTrabajoRelacionCompuesto = listaPuestoTrabajoRelacion.GroupBy(x => new { x.Id, x.IdPerfilPuestoTrabajo }).Select(x => new PuestoTrabajoRelacionCompuestoDTO
				{
					Id = x.Key.Id,
					IdPerfilPuestoTrabajo = x.Key.IdPerfilPuestoTrabajo,
					ListaPuestoDependencia = x.GroupBy(y => new { y.IdPuestoTrabajoRelacionDetalle, y.IdPuestoTrabajo_Dependencia, y.PuestoTrabajo_Dependencia }).Select(y => new FiltroIdNombrePKDTO
					{
						Id = y.Key.IdPuestoTrabajo_Dependencia == null ? 0 : y.Key.IdPuestoTrabajo_Dependencia.Value,
						Nombre = y.Key.PuestoTrabajo_Dependencia,
						PK = y.Key.IdPuestoTrabajoRelacionDetalle
					}).ToList(),
					ListaPuestoACargo = x.GroupBy(y => new { y.IdPuestoTrabajoRelacionDetalle, y.IdPuestoTrabajo_PuestoACargo, y.PuestoTrabajo_PuestoACargo }).Select(y => new FiltroIdNombrePKDTO
					{
						Id = y.Key.IdPuestoTrabajo_PuestoACargo == null ? 0 : y.Key.IdPuestoTrabajo_PuestoACargo.Value,
						Nombre = y.Key.PuestoTrabajo_PuestoACargo,
						PK = y.Key.IdPuestoTrabajoRelacionDetalle
					}).ToList(),
					ListaPuestoRelacionInterna = x.GroupBy(y => new { y.IdPuestoTrabajoRelacionDetalle, y.IdPersonalAreaTrabajo, y.PersonalAreaTrabajo }).Select(y => new FiltroIdNombrePKDTO
					{
						Id = y.Key.IdPersonalAreaTrabajo == null ? 0 : y.Key.IdPersonalAreaTrabajo.Value,
						Nombre = y.Key.PersonalAreaTrabajo,
						PK = y.Key.IdPuestoTrabajoRelacionDetalle
					}).ToList()
				}).ToList();

				foreach(var item in listaPuestoTrabajoRelacionCompuesto)
				{
					item.ListaPuestoACargo.RemoveAll(x => x.Id == 0);
					item.ListaPuestoDependencia.RemoveAll(x => x.Id == 0);
					item.ListaPuestoRelacionInterna.RemoveAll(x => x.Id == 0);
				}

				//FUNCIONES
				var listaPuestoTrabajoFuncion = _repPuestoTrabajoFuncion.ObtenerPuestoTrabajoFuncion(IdPerfilPuestoTrabajo);

				//REPORTE	
				var listaPuestoTrabajoReporte = _repPuestoTrabajoReporte.ObtenerPuestoTrabajoReporte(IdPerfilPuestoTrabajo);

				//CURSOS COMPLEMENTARIOS
				var listaPuestoTrabajoCursoComplementario = _repPuestoTrabajoCursoComplementario.ObtenerPuestoTrabajoCursoComplementario(IdPerfilPuestoTrabajo);

				//EXPERIENCIA
				var listaPuestoTrabajoExperiencia = _repPuestoTrabajoExperiencia.ObtenerPuestoTrabajoExperiencia(IdPerfilPuestoTrabajo);

				//CARACTERISTICAS PERSONALES
				var listaPuestoTrabajoCaracteristicaPersonal = _repPuestoTrabajoCaracteristicaPersonal.ObtenerPuestoTrabajoCaracteristicaPersonal(IdPerfilPuestoTrabajo);

				//FORMACION ACADEMICA
				var listaPuestoTrabajoFormacionAcademica = _repPuestoTrabajoFormacionAcademica.ObtenerPuestoTrabajoFormacionAcademica(IdPerfilPuestoTrabajo);
				var listaPuestoTrabajoFormacionAcademicaProcesada = listaPuestoTrabajoFormacionAcademica.Select(x => new PuestoTrabajoFormacionAcademicaDTO
				{
					Id = x.Id,
					IdPerfilPuestoTrabajo = x.IdPerfilPuestoTrabajo,
					IdAreaFormacion = x.IdAreaFormacion.Split(",").Select(y => Convert.ToInt32(y)).ToList(),
					IdCentroEstudio = x.IdCentroEstudio.Split(",").Select(y => Convert.ToInt32(y)).ToList(),
					IdGradoEstudio = x.IdGradoEstudio.Split(",").Select(y => Convert.ToInt32(y)).ToList(),
					IdNivelEstudio = x.IdNivelEstudio.Split(",").Select(y => Convert.ToInt32(y)).ToList(),
					IdTipoFormacion = x.IdTipoFormacion.Split(",").Select(y => Convert.ToInt32(y)).ToList()
				}).ToList();

				var listaEvaluacion = _repPuestoTrabajoPuntajeCalificacion.ObtenerNombreEvaluacionPuntaje();
				var ListaCalificacionTotal = listaEvaluacion.Where(x => x.CalificacionTotal == true).ToList();
				var ListaCalificacionAgrupadaIndependiente = listaEvaluacion.Where(x => x.CalificacionTotal == false).ToList();
				var ListaIndependiente = ListaCalificacionAgrupadaIndependiente.Where(x => x.IdGrupo == null || x.IdGrupo == 0).ToList();
				var ListaGrupo = ListaCalificacionAgrupadaIndependiente.Where(x => x.IdGrupo != null && x.IdGrupo != 0).ToList();
				List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO> nuevoCalificacionTotal = new List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO>();
				List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO> nuevoCalificacionAgrupada = new List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO>();
				List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO> nuevoCalificacionIndependiente = new List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO>();

				//Separa todas las Calificaciones Totales, se agrupa las evaluaciones y se colocan en nuevoCalificacionTotal
				if (ListaCalificacionTotal.Count() > 0)
				{
					foreach (var item in ListaCalificacionTotal)
					{
						item.IdComponente = null;
						item.IdGrupo = null;
						item.NombreComponente = null;
						item.NombreGrupo = null;
						item.CalificaAgrupadoNoIndependiente = false;
					}
					nuevoCalificacionTotal = ListaCalificacionTotal.GroupBy(u => (u.IdEvaluacion, u.NombreEvaluacion))
						.Select(group => new PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO
						{
							IdEvaluacion = group.Key.IdEvaluacion,
							IdGrupo = null,
							IdComponente = null,
							NombreComponente = null,
							NombreGrupo = null,
							NombreEvaluacion = group.Key.NombreEvaluacion,
							CalificacionTotal = true,
							Puntaje = null,
							CalificaPorCentil = false,
							IdProcesoSeleccionRango = 0,
							EsCalificable = false
						}).ToList();
				}

				//Separa todas las Calificaciones por Componente, se agrupa los Componentes y se colocan en nuevoCalificacionIndependiente
				if (ListaIndependiente.Count() > 0)
				{
					foreach (var item in ListaIndependiente)
					{
						item.IdGrupo = null;
						item.NombreGrupo = null;
					}
					nuevoCalificacionIndependiente = ListaIndependiente.GroupBy(u => (u.IdComponente, u.NombreComponente, u.IdEvaluacion, u.NombreEvaluacion))
						.Select(group => new PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO
						{
							IdEvaluacion = group.Key.IdEvaluacion,
							IdGrupo = null,
							IdComponente = group.Key.IdComponente,
							NombreComponente = group.Key.NombreComponente,
							NombreGrupo = null,
							NombreEvaluacion = group.Key.NombreEvaluacion,
							CalificacionTotal = false,
							Puntaje = null,
							CalificaPorCentil = false,
							CalificaAgrupadoNoIndependiente = false,
							IdProcesoSeleccionRango = 0,
							EsCalificable = false
						}).ToList();
				}

				//Separa todas las Calificaciones por Grupo, se agrupa los Grupos y se colocan en nuevoCalificacionAgrupada
				if (ListaGrupo.Count() > 0)
				{
					foreach (var item in ListaGrupo)
					{
						item.IdComponente = null;
						item.NombreComponente = null;
					}
					nuevoCalificacionAgrupada = ListaGrupo.GroupBy(u => (u.IdGrupo, u.NombreGrupo, u.IdEvaluacion, u.NombreEvaluacion))
						.Select(group => new PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO
						{
							IdEvaluacion = group.Key.IdEvaluacion,
							IdGrupo = group.Key.IdGrupo,
							IdComponente = null,
							NombreComponente = null,
							NombreGrupo = group.Key.NombreGrupo,
							NombreEvaluacion = group.Key.NombreEvaluacion,
							CalificacionTotal = false,
							Puntaje = null,
							CalificaPorCentil = false,
							CalificaAgrupadoNoIndependiente = true,
							IdProcesoSeleccionRango = 0,
							EsCalificable = false
						}).ToList();
				}
				List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO> listaPuntajeCalificacionTotal = new List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO>();
				listaPuntajeCalificacionTotal = nuevoCalificacionTotal.Concat(nuevoCalificacionAgrupada).Concat(nuevoCalificacionIndependiente).ToList();

				foreach (var item in listaPuntajeCalificacionTotal)
				{
					if (item.IdEvaluacion != null && item.IdGrupo == null && item.IdComponente == null)
					{

						PuestoTrabajoPuntajeCalificacionBO evaluacionPje = new PuestoTrabajoPuntajeCalificacionBO();
						evaluacionPje = _repPuestoTrabajoPuntajeCalificacion.FirstBy(x => x.IdPerfilPuestoTrabajo == IdPerfilPuestoTrabajo && x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == null && x.IdExamen == null);
						if (evaluacionPje != null && evaluacionPje.Id != 0)
						{
							item.Puntaje = evaluacionPje.PuntajeMinimo;
							item.CalificaPorCentil = evaluacionPje.CalificaPorCentil;
							item.IdProcesoSeleccionRango = evaluacionPje.IdProcesoSeleccionRango;
							item.EsCalificable = evaluacionPje.EsCalificable;
						}
					}
					if (item.IdGrupo != null)
					{

						PuestoTrabajoPuntajeCalificacionBO evaluacionPje = new PuestoTrabajoPuntajeCalificacionBO();
						evaluacionPje = _repPuestoTrabajoPuntajeCalificacion.FirstBy(x => x.IdPerfilPuestoTrabajo == IdPerfilPuestoTrabajo && x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdExamen == null);
						if (evaluacionPje != null && evaluacionPje.Id != 0)
						{
							item.Puntaje = evaluacionPje.PuntajeMinimo;
							item.CalificaPorCentil = evaluacionPje.CalificaPorCentil;
							item.IdProcesoSeleccionRango = evaluacionPje.IdProcesoSeleccionRango;
							item.EsCalificable = evaluacionPje.EsCalificable;
						}
					}
					if (item.IdComponente != null)
					{
						PuestoTrabajoPuntajeCalificacionBO evaluacionPje = new PuestoTrabajoPuntajeCalificacionBO();
						evaluacionPje = _repPuestoTrabajoPuntajeCalificacion.FirstBy(x => x.IdPerfilPuestoTrabajo == IdPerfilPuestoTrabajo && x.IdExamenTest == item.IdEvaluacion && x.IdExamen == item.IdComponente && x.IdGrupoComponenteEvaluacion == null);
						if (evaluacionPje != null && evaluacionPje.Id != 0)
						{
							item.Puntaje = evaluacionPje.PuntajeMinimo;
							item.CalificaPorCentil = evaluacionPje.CalificaPorCentil;
							item.IdProcesoSeleccionRango = evaluacionPje.IdProcesoSeleccionRango;
							item.EsCalificable = evaluacionPje.EsCalificable;
						}
					}
				}

				List<PuestoTrabajoNombreEvaluacionesAgrupadaIndependienteDTO> listaNombreEvaluacion = new List<PuestoTrabajoNombreEvaluacionesAgrupadaIndependienteDTO>();
				foreach (var item in listaPuntajeCalificacionTotal)
				{
					if (listaPuntajeCalificacionTotal.Count > 0)
					{
						listaNombreEvaluacion.Add(new PuestoTrabajoNombreEvaluacionesAgrupadaIndependienteDTO
						{
							Id = item.IdEvaluacion.Value,
							Nombre = item.NombreEvaluacion,
							CalificacionTotal = item.CalificacionTotal,
							CalificaAgrupadoNoIndependiente = item.CalificaAgrupadoNoIndependiente
						});
					}

				}
				var listaEvaluaciones = listaNombreEvaluacion.GroupBy(u => (u.Id, u.Nombre, u.CalificacionTotal, u.CalificaAgrupadoNoIndependiente))
						.Select(group => new PuestoTrabajoNombreEvaluacionesAgrupadaIndependienteDTO
						{
							Id = group.Key.Id,
							Nombre = group.Key.Nombre,
							CalificacionTotal = group.Key.CalificacionTotal,
							CalificaAgrupadoNoIndependiente = group.Key.CalificaAgrupadoNoIndependiente
						}).ToList();

				return Ok(new
				{
					ListaPuestoTrabajoRelacion = listaPuestoTrabajoRelacionCompuesto,
					ListaPuestoTrabajoFuncion = listaPuestoTrabajoFuncion,
					ListaPuestoTrabajoReporte = listaPuestoTrabajoReporte,
					ListaPuestoTrabajoCursoComplementario = listaPuestoTrabajoCursoComplementario,
					ListaPuestoTrabajoExperiencia = listaPuestoTrabajoExperiencia,
					ListaPuestoTrabajoCaracteristicaPersonal = listaPuestoTrabajoCaracteristicaPersonal,
					ListaPuestoTrabajoFormacionAcademica = listaPuestoTrabajoFormacionAcademicaProcesada,
					ListaEvaluacionesPuntajeCalificacion = listaPuntajeCalificacionTotal,
					ListaEvaluaciones = listaEvaluaciones
				});
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Luis H, Edgar S.
		/// Fecha: 25/01/2021
		/// Versión: 1.0
		/// <summary>
		/// Insertar o Actualizar Perfil de Puesto de Trabajo
		/// </summary>
		/// <returns> Obtiene confirmación de inserción o actualización : Bool </returns>
		[HttpPost]
		[Route("[action]")]
		public ActionResult InsertarActualizarPerfilPuestoTrabajo([FromBody]PerfilPuestoTrabajoInsertarActualizarDTO PerfilPuesto)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				if (_repPuestoTrabajo.Exist(PerfilPuesto.IdPuestoTrabajo))
				{
					using (TransactionScope scope = new TransactionScope())
					{
						var puestoTrabajo = _repPuestoTrabajo.FirstById(PerfilPuesto.IdPuestoTrabajo);
						PerfilPuestoTrabajoBO perfilPuestoTrabajo;
						//Si es Usuario Aprobado, Crear siempre una nueva versión
						if (PerfilPuesto.EsUsuarioAprobacion)
                        {
							PerfilPuesto.CrearNuevaVersion = true;
						}
						if (PerfilPuesto.CrearNuevaVersion)
						{
							int version = 1;
							if (PerfilPuesto.EsUsuarioAprobacion)
							{
								if (PerfilPuesto.IdPerfilPuestoTrabajo > 0)
								{
									var perfilPuestoTrabajoExistente = _repPerfilPuestoTrabajo.FirstById(PerfilPuesto.IdPerfilPuestoTrabajo);
									perfilPuestoTrabajoExistente.EsActual = false;
									perfilPuestoTrabajoExistente.UsuarioModificacion = PerfilPuesto.Usuario;
									perfilPuestoTrabajoExistente.FechaModificacion = DateTime.Now;
									_repPerfilPuestoTrabajo.Update(perfilPuestoTrabajoExistente);

									version = perfilPuestoTrabajoExistente.Version + 1;
									//Eliminar Solicitudes Anteriores si existieran
									var eliminarSolicitudesPendientes = _repPerfilPuestoTrabajo.GetBy(x => x.IdPuestoTrabajo == PerfilPuesto.IdPuestoTrabajo && x.IdPerfilPuestoTrabajoEstadoSolicitud == 5).FirstOrDefault();
									if (eliminarSolicitudesPendientes != null)
									{
										eliminarSolicitudesPendientes.IdPerfilPuestoTrabajoEstadoSolicitud = 2;
										eliminarSolicitudesPendientes.IdPersonalAprobacion = PerfilPuesto.IdPersonal;
										eliminarSolicitudesPendientes.UsuarioModificacion = PerfilPuesto.Usuario;
										eliminarSolicitudesPendientes.FechaModificacion = DateTime.Now;
										_repPerfilPuestoTrabajo.Update(eliminarSolicitudesPendientes);
									}
								}

								perfilPuestoTrabajo = new PerfilPuestoTrabajoBO()
								{
									IdPuestoTrabajo = puestoTrabajo.Id,
									Descripcion = PerfilPuesto.Descripcion,
									Objetivo = PerfilPuesto.Objetivo,
									EsActual = true,
									Version = version,
									Estado = true,
									UsuarioModificacion = PerfilPuesto.Usuario,
									UsuarioCreacion = PerfilPuesto.Usuario,
									FechaModificacion = DateTime.Now,
									FechaCreacion = DateTime.Now,
									IdPersonalSolicitud = PerfilPuesto.IdPersonal,
									FechaSolicitud = DateTime.Now,
									IdPersonalAprobacion = PerfilPuesto.IdPersonal,
									FechaAprobacion = DateTime.Now,
									IdPerfilPuestoTrabajoEstadoSolicitud = 1
								};
								_repPerfilPuestoTrabajo.Insert(perfilPuestoTrabajo);
							}
							else
							{
								var estado = _repPerfilPuestoTrabajoEstadoSolicitud.FirstBy(x => x.Nombre.Equals("Solicitado"));
								int? idEstado = null;
								if(estado != null)
								{
									idEstado = estado.Id;
								}
								if (PerfilPuesto.IdPerfilPuestoTrabajo > 0)
								{
									var perfilPuestoTrabajoExistente = _repPerfilPuestoTrabajo.FirstById(PerfilPuesto.IdPerfilPuestoTrabajo);
									version = perfilPuestoTrabajoExistente.Version + 1;
								}

								perfilPuestoTrabajo = new PerfilPuestoTrabajoBO()
								{
									IdPuestoTrabajo = puestoTrabajo.Id,
									Descripcion = PerfilPuesto.Descripcion,
									Objetivo = PerfilPuesto.Objetivo,
									EsActual = false,
									Version = version,
									Estado = true,
									UsuarioModificacion = PerfilPuesto.Usuario,
									UsuarioCreacion = PerfilPuesto.Usuario,
									FechaModificacion = DateTime.Now,
									FechaCreacion = DateTime.Now,
									IdPersonalSolicitud = PerfilPuesto.IdPersonal,
									FechaSolicitud = DateTime.Now,
									IdPerfilPuestoTrabajoEstadoSolicitud = idEstado
								};
								_repPerfilPuestoTrabajo.Insert(perfilPuestoTrabajo);
							}

							if (PerfilPuesto.EstadoPuestoTrabajoCaracteristicaPersonal && PerfilPuesto.PuestoTrabajoCaracteristicaPersonal.Count > 0)
							{
								foreach (var item in PerfilPuesto.PuestoTrabajoCaracteristicaPersonal)
								{
									PuestoTrabajoCaracteristicaPersonalBO puestoTrabajoCaracteristicaPersonal = new PuestoTrabajoCaracteristicaPersonalBO
									{
										IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
										EdadMinima = item.EdadMinima,
										EdadMaxima = item.EdadMaxima,
										IdEstadoCivil = item.IdEstadoCivil,
										IdSexo = item.IdSexo,
										Estado = true,
										FechaCreacion = DateTime.Now,
										FechaModificacion = DateTime.Now,
										UsuarioCreacion = PerfilPuesto.Usuario,
										UsuarioModificacion = PerfilPuesto.Usuario
									};
									_repPuestoTrabajoCaracteristicaPersonal.Insert(puestoTrabajoCaracteristicaPersonal);
								}
							}

							if (PerfilPuesto.EstadoPuestoTrabajoCursoComplementario && PerfilPuesto.PuestoTrabajoCursoComplementario.Count > 0)
							{
								foreach (var item in PerfilPuesto.PuestoTrabajoCursoComplementario)
								{
									PuestoTrabajoCursoComplementarioBO puestoTrabajoCursoComplementario = new PuestoTrabajoCursoComplementarioBO
									{
										IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
										IdCompetenciaTecnica = item.IdCompetenciaTecnica,
										IdNivelCompetenciaTecnica = item.IdNivelCompetenciaTecnica,
										IdTipoCompetenciaTecnica = item.IdTipoCompetenciaTecnica,
										Estado = true,
										FechaCreacion = DateTime.Now,
										FechaModificacion = DateTime.Now,
										UsuarioCreacion = PerfilPuesto.Usuario,
										UsuarioModificacion = PerfilPuesto.Usuario
									};
									_repPuestoTrabajoCursoComplementario.Insert(puestoTrabajoCursoComplementario);
								}
							}

							if (PerfilPuesto.EstadoPuestoTrabajoExperiencia && PerfilPuesto.PuestoTrabajoExperiencia.Count > 0)
							{
								foreach (var item in PerfilPuesto.PuestoTrabajoExperiencia)
								{
									PuestoTrabajoExperienciaBO puestoTrabajoExperiencia = new PuestoTrabajoExperienciaBO
									{
										IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
										IdExperiencia = item.IdExperiencia,
										IdTipoExperiencia = item.IdTipoExperiencia,
										NumeroMinimo = item.NumeroMinimo,
										Periodo = item.Periodo,
										Estado = true,
										FechaCreacion = DateTime.Now,
										FechaModificacion = DateTime.Now,
										UsuarioCreacion = PerfilPuesto.Usuario,
										UsuarioModificacion = PerfilPuesto.Usuario
									};
									_repPuestoTrabajoExperiencia.Insert(puestoTrabajoExperiencia);
								}
							}

							if (PerfilPuesto.EstadoPuestoTrabajoFormacionAcademica && PerfilPuesto.PuestoTrabajoFormacion.Count > 0)
							{
								foreach (var item in PerfilPuesto.PuestoTrabajoFormacion)
								{
									PuestoTrabajoFormacionAcademicaBO puestoTrabajoFormacionAcademica = new PuestoTrabajoFormacionAcademicaBO
									{
										IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
										IdAreaFormacion = item.IdAreaFormacion == null ? "" : string.Join(",", item.IdAreaFormacion.Select(x => x)),
										IdCentroEstudio = item.IdCentroEstudio == null ? "" : string.Join(",", item.IdCentroEstudio.Select(x => x)),
										IdGradoEstudio = item.IdGradoEstudio == null ? "" : string.Join(",", item.IdGradoEstudio.Select(x => x)),
										IdNivelEstudio = item.IdNivelEstudio == null ? "" : string.Join(",", item.IdNivelEstudio.Select(x => x)),
										IdTipoFormacion = item.IdTipoFormacion == null ? "" : string.Join(",", item.IdTipoFormacion.Select(x => x)),
										Estado = true,
										FechaCreacion = DateTime.Now,
										FechaModificacion = DateTime.Now,
										UsuarioCreacion = PerfilPuesto.Usuario,
										UsuarioModificacion = PerfilPuesto.Usuario
									};
									_repPuestoTrabajoFormacionAcademica.Insert(puestoTrabajoFormacionAcademica);
								}
							}

							if (PerfilPuesto.EstadoPuestoTrabajoFuncion && PerfilPuesto.PuestoTrabajoFuncion.Count > 0)
							{
								foreach (var item in PerfilPuesto.PuestoTrabajoFuncion)
								{
									PuestoTrabajoFuncionBO puestoTrabajoFuncion = new PuestoTrabajoFuncionBO
									{
										IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
										IdFrecuenciaPuestoTrabajo = item.IdFrecuenciaPuestoTrabajo,
										IdPersonalTipoFuncion = item.IdPersonalTipoFuncion,
										Nombre = item.Funcion,
										NroOrden = item.NroOrden,
										Estado = true,
										FechaCreacion = DateTime.Now,
										FechaModificacion = DateTime.Now,
										UsuarioCreacion = PerfilPuesto.Usuario,
										UsuarioModificacion = PerfilPuesto.Usuario
									};
									_repPuestoTrabajoFuncion.Insert(puestoTrabajoFuncion);
								}
							}

							if (PerfilPuesto.EstadoPuestoTrabajoRelacion && PerfilPuesto.PuestoTrabajoRelacion.Count > 0)
							{
								foreach (var item in PerfilPuesto.PuestoTrabajoRelacion)
								{
									PuestoTrabajoRelacionBO puestoTrabajoRelacion = new PuestoTrabajoRelacionBO()
									{
										IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
										Estado = true,
										FechaCreacion = DateTime.Now,
										FechaModificacion = DateTime.Now,
										UsuarioCreacion = PerfilPuesto.Usuario,
										UsuarioModificacion = PerfilPuesto.Usuario
									};
									_repPuestoTrabajoRelacion.Insert(puestoTrabajoRelacion);

									foreach (var rel in item.ListaPuestoRelacionInterna)
									{
										PuestoTrabajoRelacionDetalleBO puestoTrabajoRelacionDetalle = new PuestoTrabajoRelacionDetalleBO
										{
											IdPuestoTrabajoRelacion = puestoTrabajoRelacion.Id,
											IdPersonalAreaTrabajo = rel.Id,
											Estado = true,
											FechaCreacion = DateTime.Now,
											FechaModificacion = DateTime.Now,
											UsuarioCreacion = PerfilPuesto.Usuario,
											UsuarioModificacion = PerfilPuesto.Usuario
										};
										_repPuestoTrabajoRelacionDetalle.Insert(puestoTrabajoRelacionDetalle);
									}

									foreach (var rel in item.ListaPuestoACargo)
									{
										PuestoTrabajoRelacionDetalleBO puestoTrabajoRelacionDetalle = new PuestoTrabajoRelacionDetalleBO
										{
											IdPuestoTrabajoRelacion = puestoTrabajoRelacion.Id,
											IdPuestoTrabajoPuestoAcargo = rel.Id,
											Estado = true,
											FechaCreacion = DateTime.Now,
											FechaModificacion = DateTime.Now,
											UsuarioCreacion = PerfilPuesto.Usuario,
											UsuarioModificacion = PerfilPuesto.Usuario
										};
										_repPuestoTrabajoRelacionDetalle.Insert(puestoTrabajoRelacionDetalle);
									}

									foreach (var rel in item.ListaPuestoDependencia)
									{
										PuestoTrabajoRelacionDetalleBO puestoTrabajoRelacionDetalle = new PuestoTrabajoRelacionDetalleBO
										{
											IdPuestoTrabajoRelacion = puestoTrabajoRelacion.Id,
											IdPuestoTrabajoDependencia = rel.Id,
											Estado = true,
											FechaCreacion = DateTime.Now,
											FechaModificacion = DateTime.Now,
											UsuarioCreacion = PerfilPuesto.Usuario,
											UsuarioModificacion = PerfilPuesto.Usuario
										};
										_repPuestoTrabajoRelacionDetalle.Insert(puestoTrabajoRelacionDetalle);
									}
								}
							}

							if (PerfilPuesto.EstadoPuestoTrabajoReporte && PerfilPuesto.PuestoTrabajoReporte.Count > 0)
							{
								foreach (var item in PerfilPuesto.PuestoTrabajoReporte)
								{
									PuestoTrabajoReporteBO puestoTrabajoReporte = new PuestoTrabajoReporteBO
									{
										IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
										IdFrecuenciaPuestoTrabajo = item.IdFrecuenciaPuestoTrabajo,
										Nombre = item.Reporte,
										NroOrden = item.NroOrden,
										Estado = true,
										FechaCreacion = DateTime.Now,
										FechaModificacion = DateTime.Now,
										UsuarioCreacion = PerfilPuesto.Usuario,
										UsuarioModificacion = PerfilPuesto.Usuario
									};
									_repPuestoTrabajoReporte.Insert(puestoTrabajoReporte);
								}
							}

							var listaPuntajePrevio = _repPuestoTrabajoPuntajeCalificacion.GetBy(x => x.IdPerfilPuestoTrabajo == PerfilPuesto.IdPerfilPuestoTrabajo).ToList();

							foreach (var item in listaPuntajePrevio)
							{
								PuestoTrabajoPuntajeCalificacionBO puntajeAnt = new PuestoTrabajoPuntajeCalificacionBO()
								{
									IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
									IdExamen = item.IdExamen,
									IdExamenTest = item.IdExamenTest,
									IdGrupoComponenteEvaluacion = item.IdGrupoComponenteEvaluacion,
									CalificaPorCentil = item.CalificaPorCentil,
									PuntajeMinimo = item.PuntajeMinimo,
									IdProcesoSeleccionRango = item.IdProcesoSeleccionRango,
									EsCalificable = item.EsCalificable,
									Estado = true,
									UsuarioCreacion = PerfilPuesto.Usuario,
									FechaCreacion = DateTime.Now,
									UsuarioModificacion = PerfilPuesto.Usuario,
									FechaModificacion = DateTime.Now
								};
								_repPuestoTrabajoPuntajeCalificacion.Insert(puntajeAnt);
							}

							foreach (var item in PerfilPuesto.Puntaje.ListaPuntaje)
							{
								PuestoTrabajoPuntajeCalificacionBO puntaje = _repPuestoTrabajoPuntajeCalificacion.FirstBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id && x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdExamen == item.IdComponente);
								if (puntaje != null && puntaje.Id != null && puntaje.Id != 0)
								{

									puntaje.CalificaPorCentil = item.CalificaPorCentil;
									puntaje.PuntajeMinimo = item.Puntaje;
									puntaje.IdProcesoSeleccionRango = item.IdProcesoSeleccionRango;
									puntaje.EsCalificable = item.EsCalificable;

									puntaje.UsuarioModificacion = PerfilPuesto.Usuario;
									puntaje.FechaModificacion = DateTime.Now;
									_repPuestoTrabajoPuntajeCalificacion.Update(puntaje);
								}
								else
								{
									puntaje = new PuestoTrabajoPuntajeCalificacionBO()
									{
										IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
										IdExamen = item.IdComponente,
										IdExamenTest = item.IdEvaluacion,
										IdGrupoComponenteEvaluacion = item.IdGrupo,
										CalificaPorCentil = item.CalificaPorCentil,
										PuntajeMinimo = item.Puntaje,
										IdProcesoSeleccionRango = item.IdProcesoSeleccionRango,
										EsCalificable = item.EsCalificable,
										Estado = true,
										UsuarioCreacion = PerfilPuesto.Usuario,
										FechaCreacion = DateTime.Now,
										UsuarioModificacion = PerfilPuesto.Usuario,
										FechaModificacion = DateTime.Now
									};
									_repPuestoTrabajoPuntajeCalificacion.Insert(puntaje);
								}
							}
						}
						else
						{
							perfilPuestoTrabajo = _repPerfilPuestoTrabajo.GetBy(x => x.IdPuestoTrabajo == PerfilPuesto.IdPuestoTrabajo && x.IdPerfilPuestoTrabajoEstadoSolicitud == 5).FirstOrDefault();
							perfilPuestoTrabajo.Objetivo = PerfilPuesto.Objetivo;
							perfilPuestoTrabajo.Descripcion = PerfilPuesto.Descripcion;
							perfilPuestoTrabajo.UsuarioModificacion = PerfilPuesto.Usuario;
							perfilPuestoTrabajo.FechaModificacion = DateTime.Now;
							_repPerfilPuestoTrabajo.Update(perfilPuestoTrabajo);

							PerfilPuesto.IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id;

							if (PerfilPuesto.EstadoPuestoTrabajoCaracteristicaPersonal)
							{
								var listaPuestoTrabajoCaracteristicaPersonal = _repPuestoTrabajoCaracteristicaPersonal.GetBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id).ToList();
								foreach (var item in listaPuestoTrabajoCaracteristicaPersonal)
								{
									if (!PerfilPuesto.PuestoTrabajoCaracteristicaPersonal.Any(x => x.Id == item.Id))
									{
										_repPuestoTrabajoCaracteristicaPersonal.Delete(item.Id, PerfilPuesto.Usuario);
									}

								}
								foreach (var item in PerfilPuesto.PuestoTrabajoCaracteristicaPersonal)
								{
									PuestoTrabajoCaracteristicaPersonalBO puestoTrabajoCaracteristicaPersonal = new PuestoTrabajoCaracteristicaPersonalBO
									{
										IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
										EdadMinima = item.EdadMinima,
										EdadMaxima = item.EdadMaxima,
										IdEstadoCivil = item.IdEstadoCivil,
										IdSexo = item.IdSexo,
										Estado = true,
										FechaCreacion = DateTime.Now,
										FechaModificacion = DateTime.Now,
										UsuarioCreacion = PerfilPuesto.Usuario,
										UsuarioModificacion = PerfilPuesto.Usuario
									};
									_repPuestoTrabajoCaracteristicaPersonal.Insert(puestoTrabajoCaracteristicaPersonal);
								}
							}

							if (PerfilPuesto.EstadoPuestoTrabajoCursoComplementario)
							{
								var listaPuestoTrabajoCursoComplementario = _repPuestoTrabajoCursoComplementario.GetBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id).ToList();
								foreach (var item in listaPuestoTrabajoCursoComplementario)
								{
									if (!PerfilPuesto.PuestoTrabajoCursoComplementario.Any(x => x.Id == item.Id))
									{
										_repPuestoTrabajoCursoComplementario.Delete(item.Id, PerfilPuesto.Usuario);
									}
								}

								foreach (var item in PerfilPuesto.PuestoTrabajoCursoComplementario)
								{
									PuestoTrabajoCursoComplementarioBO puestoTrabajoCursoComplementario = new PuestoTrabajoCursoComplementarioBO
									{
										IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
										IdCompetenciaTecnica = item.IdCompetenciaTecnica,
										IdNivelCompetenciaTecnica = item.IdNivelCompetenciaTecnica,
										IdTipoCompetenciaTecnica = item.IdTipoCompetenciaTecnica,
										Estado = true,
										FechaCreacion = DateTime.Now,
										FechaModificacion = DateTime.Now,
										UsuarioCreacion = PerfilPuesto.Usuario,
										UsuarioModificacion = PerfilPuesto.Usuario
									};
									_repPuestoTrabajoCursoComplementario.Insert(puestoTrabajoCursoComplementario);
								}
							}

							if (PerfilPuesto.EstadoPuestoTrabajoExperiencia)
							{
								var listaPuestoTrabajoExperiencia = _repPuestoTrabajoExperiencia.GetBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id).ToList();
								foreach (var item in listaPuestoTrabajoExperiencia)
								{
									if (!PerfilPuesto.PuestoTrabajoExperiencia.Any(x => x.Id == item.Id))
									{
										_repPuestoTrabajoExperiencia.Delete(item.Id, PerfilPuesto.Usuario);
									}
								}
								foreach (var item in PerfilPuesto.PuestoTrabajoExperiencia)
								{
									PuestoTrabajoExperienciaBO puestoTrabajoExperiencia = new PuestoTrabajoExperienciaBO
									{
										IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
										IdExperiencia = item.IdExperiencia,
										IdTipoExperiencia = item.IdTipoExperiencia,
										NumeroMinimo = item.NumeroMinimo,
										Periodo = item.Periodo,
										Estado = true,
										FechaCreacion = DateTime.Now,
										FechaModificacion = DateTime.Now,
										UsuarioCreacion = PerfilPuesto.Usuario,
										UsuarioModificacion = PerfilPuesto.Usuario
									};
									_repPuestoTrabajoExperiencia.Insert(puestoTrabajoExperiencia);
								}
							}

							if (PerfilPuesto.EstadoPuestoTrabajoFormacionAcademica)
							{
								var listaPuestoTrabajoFormacion = _repPuestoTrabajoFormacionAcademica.GetBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id).ToList();
								foreach (var item in listaPuestoTrabajoFormacion)
								{
									if (!PerfilPuesto.PuestoTrabajoFormacion.Any(x => x.Id == item.Id))
									{
										_repPuestoTrabajoFormacionAcademica.Delete(item.Id, PerfilPuesto.Usuario);
									}
								}
								foreach (var item in PerfilPuesto.PuestoTrabajoFormacion)
								{
									PuestoTrabajoFormacionAcademicaBO puestoTrabajoFormacionAcademica = new PuestoTrabajoFormacionAcademicaBO
									{
										IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
										IdAreaFormacion = item.IdAreaFormacion == null ? "" : string.Join(",", item.IdAreaFormacion.Select(x => x)),
										IdCentroEstudio = item.IdCentroEstudio == null ? "" : string.Join(",", item.IdCentroEstudio.Select(x => x)),
										IdGradoEstudio = item.IdGradoEstudio == null ? "" : string.Join(",", item.IdGradoEstudio.Select(x => x)),
										IdNivelEstudio = item.IdNivelEstudio == null ? "" : string.Join(",", item.IdNivelEstudio.Select(x => x)),
										IdTipoFormacion = item.IdTipoFormacion == null ? "" : string.Join(",", item.IdTipoFormacion.Select(x => x)),
										Estado = true,
										FechaCreacion = DateTime.Now,
										FechaModificacion = DateTime.Now,
										UsuarioCreacion = PerfilPuesto.Usuario,
										UsuarioModificacion = PerfilPuesto.Usuario
									};
									_repPuestoTrabajoFormacionAcademica.Insert(puestoTrabajoFormacionAcademica);
								}
							}

							if (PerfilPuesto.EstadoPuestoTrabajoFuncion)
							{
								var listaPuestoTrabajoFuncion = _repPuestoTrabajoFuncion.GetBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id).ToList();
								foreach (var item in listaPuestoTrabajoFuncion)
								{
									if (!PerfilPuesto.PuestoTrabajoFuncion.Any(x => x.Id == item.Id))
									{
										_repPuestoTrabajoFuncion.Delete(item.Id, PerfilPuesto.Usuario);
									}
								}
								foreach (var item in PerfilPuesto.PuestoTrabajoFuncion)
								{
									PuestoTrabajoFuncionBO puestoTrabajoFuncion = new PuestoTrabajoFuncionBO
									{
										IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
										IdFrecuenciaPuestoTrabajo = item.IdFrecuenciaPuestoTrabajo,
										IdPersonalTipoFuncion = item.IdPersonalTipoFuncion,
										Nombre = item.Funcion,
										NroOrden = item.NroOrden,
										Estado = true,
										FechaCreacion = DateTime.Now,
										FechaModificacion = DateTime.Now,
										UsuarioCreacion = PerfilPuesto.Usuario,
										UsuarioModificacion = PerfilPuesto.Usuario
									};
									_repPuestoTrabajoFuncion.Insert(puestoTrabajoFuncion);
								}
							}

							if (PerfilPuesto.EstadoPuestoTrabajoRelacion)
							{
								var listaPuestoTrabajoRelacion = _repPuestoTrabajoRelacion.GetBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id).ToList();
								foreach (var item in listaPuestoTrabajoRelacion)
								{
									if (!PerfilPuesto.PuestoTrabajoRelacion.Any(x => x.Id == item.Id))
									{
										var list = _repPuestoTrabajoRelacionDetalle.GetBy(x => x.IdPuestoTrabajoRelacion == item.Id);
										foreach (var it in list)
										{
											_repPuestoTrabajoRelacionDetalle.Delete(it.Id, PerfilPuesto.Usuario);
										}
										_repPuestoTrabajoRelacion.Delete(item.Id, PerfilPuesto.Usuario);
									}
								}

								foreach (var item in PerfilPuesto.PuestoTrabajoRelacion)
								{
									PuestoTrabajoRelacionBO puestoTrabajoRelacion = new PuestoTrabajoRelacionBO()
									{
										IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
										Estado = true,
										FechaCreacion = DateTime.Now,
										FechaModificacion = DateTime.Now,
										UsuarioCreacion = PerfilPuesto.Usuario,
										UsuarioModificacion = PerfilPuesto.Usuario
									};
									_repPuestoTrabajoRelacion.Insert(puestoTrabajoRelacion);

									foreach (var rel in item.ListaPuestoRelacionInterna)
									{
										PuestoTrabajoRelacionDetalleBO puestoTrabajoRelacionDetalle = new PuestoTrabajoRelacionDetalleBO
										{
											IdPuestoTrabajoRelacion = puestoTrabajoRelacion.Id,
											IdPersonalAreaTrabajo = rel.Id,
											Estado = true,
											FechaCreacion = DateTime.Now,
											FechaModificacion = DateTime.Now,
											UsuarioCreacion = PerfilPuesto.Usuario,
											UsuarioModificacion = PerfilPuesto.Usuario
										};
										_repPuestoTrabajoRelacionDetalle.Insert(puestoTrabajoRelacionDetalle);
									}

									foreach (var rel in item.ListaPuestoACargo)
									{
										PuestoTrabajoRelacionDetalleBO puestoTrabajoRelacionDetalle = new PuestoTrabajoRelacionDetalleBO
										{
											IdPuestoTrabajoRelacion = puestoTrabajoRelacion.Id,
											IdPuestoTrabajoPuestoAcargo = rel.Id,
											Estado = true,
											FechaCreacion = DateTime.Now,
											FechaModificacion = DateTime.Now,
											UsuarioCreacion = PerfilPuesto.Usuario,
											UsuarioModificacion = PerfilPuesto.Usuario
										};
										_repPuestoTrabajoRelacionDetalle.Insert(puestoTrabajoRelacionDetalle);
									}

									foreach (var rel in item.ListaPuestoDependencia)
									{
										PuestoTrabajoRelacionDetalleBO puestoTrabajoRelacionDetalle = new PuestoTrabajoRelacionDetalleBO
										{
											IdPuestoTrabajoRelacion = puestoTrabajoRelacion.Id,
											IdPuestoTrabajoDependencia = rel.Id,
											Estado = true,
											FechaCreacion = DateTime.Now,
											FechaModificacion = DateTime.Now,
											UsuarioCreacion = PerfilPuesto.Usuario,
											UsuarioModificacion = PerfilPuesto.Usuario
										};
										_repPuestoTrabajoRelacionDetalle.Insert(puestoTrabajoRelacionDetalle);
									}
								}

							}

							if (PerfilPuesto.EstadoPuestoTrabajoReporte)
							{
								var listaPuestoTrabajoReporte = _repPuestoTrabajoReporte.GetBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id).ToList();
								foreach (var item in listaPuestoTrabajoReporte)
								{
									if (!PerfilPuesto.PuestoTrabajoReporte.Any(x => x.Id == item.Id))
									{
										_repPuestoTrabajoReporte.Delete(item.Id, PerfilPuesto.Usuario);
									}
								}
								foreach (var item in PerfilPuesto.PuestoTrabajoReporte)
								{
									PuestoTrabajoReporteBO puestoTrabajoReporte = new PuestoTrabajoReporteBO
									{
										IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id,
										IdFrecuenciaPuestoTrabajo = item.IdFrecuenciaPuestoTrabajo,
										Nombre = item.Reporte,
										NroOrden = item.NroOrden,
										Estado = true,
										FechaCreacion = DateTime.Now,
										FechaModificacion = DateTime.Now,
										UsuarioCreacion = PerfilPuesto.Usuario,
										UsuarioModificacion = PerfilPuesto.Usuario
									};
									_repPuestoTrabajoReporte.Insert(puestoTrabajoReporte);
								}
							}


							foreach (var item in PerfilPuesto.Puntaje.ListaPuntaje)
							{

								PuestoTrabajoPuntajeCalificacionBO puntaje = _repPuestoTrabajoPuntajeCalificacion.FirstBy(x => x.IdPerfilPuestoTrabajo == perfilPuestoTrabajo.Id && x.IdExamenTest == item.IdEvaluacion && x.IdGrupoComponenteEvaluacion == item.IdGrupo && x.IdExamen == item.IdComponente);
								if (puntaje != null && puntaje.Id != null && puntaje.Id != 0)
								{

									puntaje.CalificaPorCentil = item.CalificaPorCentil;
									puntaje.PuntajeMinimo = item.Puntaje;
									puntaje.IdProcesoSeleccionRango = item.IdProcesoSeleccionRango;
									puntaje.EsCalificable = item.EsCalificable;

									puntaje.UsuarioModificacion = PerfilPuesto.Usuario;
									puntaje.FechaModificacion = DateTime.Now;
									_repPuestoTrabajoPuntajeCalificacion.Update(puntaje);
								}
								else
								{
									puntaje = new PuestoTrabajoPuntajeCalificacionBO();
									puntaje.IdPerfilPuestoTrabajo = perfilPuestoTrabajo.Id;
									puntaje.IdExamen = item.IdComponente;
									puntaje.IdExamenTest = item.IdEvaluacion;
									puntaje.IdGrupoComponenteEvaluacion = item.IdGrupo;
									puntaje.CalificaPorCentil = item.CalificaPorCentil;
									puntaje.PuntajeMinimo = item.Puntaje;
									puntaje.IdProcesoSeleccionRango = item.IdProcesoSeleccionRango;
									puntaje.EsCalificable = item.EsCalificable;

									puntaje.Estado = true;
									puntaje.UsuarioCreacion = PerfilPuesto.Usuario;
									puntaje.FechaCreacion = DateTime.Now;
									puntaje.UsuarioModificacion = PerfilPuesto.Usuario;
									puntaje.FechaModificacion = DateTime.Now;
									_repPuestoTrabajoPuntajeCalificacion.Insert(puntaje);
								}
							}
						}

						scope.Complete();
						return Ok(true);
					}
				}
				else
				{
					return BadRequest("El puesto de trabajo no existe");
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Luis H, Edgar S.
		/// Fecha: 25/01/2021
		/// Versión: 1.0
		/// <summary>
		/// Aprueba o rechaza versiones de Perfil de Puesto de Trabajo
		/// </summary>
		/// <returns> Retorna confirmación de aprobación o rechazo de perfil de puesto de trabajo : Bool </returns>
		[HttpPost]
		[Route("[Action]")]
		public ActionResult AprobarRechazarVersionPerfilPuestoTrabajo([FromBody] AprobacionRechazoPerfilPuestoTrabajoDTO PerfilPuestoTrabajo)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				using (TransactionScope scope = new TransactionScope())
				{
					var perfilPuestoTrabajo = _repPerfilPuestoTrabajo.FirstById(PerfilPuestoTrabajo.IdPerfilPuestoTrabajo);
					if(perfilPuestoTrabajo != null)
					{						
						int? idEstado = null;
						if (PerfilPuestoTrabajo.TipoBoton) //Aprobado
						{
							var listaPerfilesAnteriores = _repPerfilPuestoTrabajo.GetBy(x => x.IdPuestoTrabajo == perfilPuestoTrabajo.IdPuestoTrabajo && x.EsActual == true).ToList();
							foreach (var item in listaPerfilesAnteriores)
							{
								item.EsActual = false;
								item.UsuarioModificacion = PerfilPuestoTrabajo.Usuario;
								item.FechaModificacion = DateTime.Now;
								_repPerfilPuestoTrabajo.Update(item);
							}

							perfilPuestoTrabajo.EsActual = true;
							var estado = _repPerfilPuestoTrabajoEstadoSolicitud.FirstBy(x => x.Nombre.Equals("Aprobado"));
							if (estado != null)
							{
								idEstado = estado.Id;
							}
							perfilPuestoTrabajo.IdPerfilPuestoTrabajoEstadoSolicitud = idEstado;
						}
						else //Rechazado
						{
							perfilPuestoTrabajo.EsActual = false;
							var estado = _repPerfilPuestoTrabajoEstadoSolicitud.FirstBy(x => x.Nombre.Equals("Rechazado"));
							if (estado != null)
							{
								idEstado = estado.Id;
							}
							perfilPuestoTrabajo.IdPerfilPuestoTrabajoEstadoSolicitud = idEstado;
						}
						perfilPuestoTrabajo.IdPersonalAprobacion = PerfilPuestoTrabajo.IdPersonal;
						perfilPuestoTrabajo.FechaAprobacion = DateTime.Now;
						perfilPuestoTrabajo.Observacion = PerfilPuestoTrabajo.Observacion;
						perfilPuestoTrabajo.UsuarioModificacion = PerfilPuestoTrabajo.Usuario;
						perfilPuestoTrabajo.FechaModificacion = DateTime.Now;
						_repPerfilPuestoTrabajo.Update(perfilPuestoTrabajo);
						scope.Complete();

						return Ok(true);
					}
					else
					{
						scope.Complete();
						return BadRequest("El perfil de puesto de trabajo no existe o fue eliminado");
					}

				}

			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// TipoFuncion: POST
		/// Autor: Luis H, Edgar S.
		/// Fecha: 25/01/2021
		/// Versión: 1.0
		/// <summary>
		/// Obtiene Perfil de Puesto de Trabajo
		/// </summary>
		/// <returns> Retorna Perfil de Puesto de Trabajo: : List<PerfilPuestoTrabajoDTO> </returns>
		[HttpPost]
		[Route("[Action]")]
		public ActionResult ObtenerListaHistoricoPerfilPuestoTrabajo([FromBody] int IdPuestoTrabajo)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				List<PerfilPuestoTrabajoDTO> listaPerfilPuestoTrabajoHistorico;
				if (IdPuestoTrabajo > 0)
				{
					listaPerfilPuestoTrabajoHistorico = _repPerfilPuestoTrabajo.ObtenerListaPerfilPuestoTrabajoHistorico(IdPuestoTrabajo);
				}
				else
				{
					listaPerfilPuestoTrabajoHistorico = new List<PerfilPuestoTrabajoDTO>();
				}

				return Ok(listaPerfilPuestoTrabajoHistorico);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}


		// Tipo Función: GET
		/// Autor: Edgar S.
		/// Fecha: 19/01/2021
		/// Versión: 1.0
		/// <summary>
		/// Retorna registros de módulos asociados a un puesto de trabajo
		/// </summary>
		/// <returns> Retorna una Lista módulos asociados a un puesto de trabajo : List<PuestoTrabajoModuloSistemaDTO>  </returns>
		[HttpGet]
		[Route("[Action]/{IdPuestoTrabajo}")]
		public ActionResult ObtenerGridAsignacionInterfaz(int IdPuestoTrabajo)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				PuestoTrabajoRepositorio _repPuestoTrabajo = new PuestoTrabajoRepositorio(_integraDBContext);
				ModuloSistemaRepositorio _repModuloSistema = new ModuloSistemaRepositorio(_integraDBContext);
				ModuloSistemaPuestoTrabajoRepositorio _repModuloSistemaPuestoTrabajo = new ModuloSistemaPuestoTrabajoRepositorio(_integraDBContext);

				var listaModuloSistema = _repModuloSistema.ObtenerModulosGrupoModulo();
				var listaPuestoTrabajo = _repPuestoTrabajo.GetAll();
				var listaModuloPuestoTrabajo = _repModuloSistemaPuestoTrabajo.GetBy(x => x.IdPuestoTrabajo == IdPuestoTrabajo).ToList();

				List<PuestoTrabajoModuloSistemaDTO> listaRegistroModuloAsignados = new List<PuestoTrabajoModuloSistemaDTO>();
				PuestoTrabajoModuloSistemaDTO registroModuloAsignados;
				if (listaPuestoTrabajo != null)
                {
					foreach (var modulo in listaModuloSistema)
					{
						bool estadoModuloSistemaPuestoTrabajo = false;
						var puestoTrabajoModulo = listaModuloPuestoTrabajo.Where(x => x.IdModuloSistema == modulo.Id).FirstOrDefault();
						if (puestoTrabajoModulo != null)
						{
							estadoModuloSistemaPuestoTrabajo = puestoTrabajoModulo.Estado;
						}

						registroModuloAsignados = new PuestoTrabajoModuloSistemaDTO()
						{
							IdModuloSistema = modulo.Id,
							ModuloSistema = modulo.Nombre,
							IdModuloSistemaGrupo = modulo.IdModuloSistemaGrupo,
							ModuloSistemaGrupo = modulo.ModuloSistemaGrupo,
							NombreTipo = modulo.NombreTipo,
							Url = modulo.Url,
							Estado = estadoModuloSistemaPuestoTrabajo
						};

						listaRegistroModuloAsignados.Add(registroModuloAsignados);
					}
				}
                if (listaRegistroModuloAsignados.Count > 0)
                {
					listaRegistroModuloAsignados = listaRegistroModuloAsignados.OrderBy(x => x.ModuloSistemaGrupo).ToList();
				}				
				return Ok(listaRegistroModuloAsignados);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		// Tipo Función: POST
		/// Autor: Edgar S.
		/// Fecha: 19/01/2021
		/// Versión: 1.0
		/// <summary>
		/// Actualiza la asociación de un módulo con un puesto de trabajo
		/// </summary>
		/// <returns> Confirmacion de Actualización de Asociciación Modulo-PuestoTrabajo : Bool </returns>
		[HttpPost]
		[Route("[Action]")]
		public ActionResult InsertarActualizarInterfaz([FromBody] AsignarInterfazDTO ListaAsignar)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
                PuestoTrabajoRepositorio _repPuestoTrabajo = new PuestoTrabajoRepositorio(_integraDBContext);
                ModuloSistemaRepositorio _repModuloSistema = new ModuloSistemaRepositorio(_integraDBContext);
                ModuloSistemaPuestoTrabajoRepositorio _repPuestoModulo = new ModuloSistemaPuestoTrabajoRepositorio(_integraDBContext);
				PersonalPuestoSedeHistoricoRepositorio _repPersonalPuestoSedeHistorico = new PersonalPuestoSedeHistoricoRepositorio(_integraDBContext);
				UsuarioRepositorio _usuarioModulo = new UsuarioRepositorio(_integraDBContext);
				ModuloSistemaAccesoRepositorio _moduloAcceso = new ModuloSistemaAccesoRepositorio(_integraDBContext);
				ModuloSistemaPuestoTrabajoRepositorio _moduloPuesto = new ModuloSistemaPuestoTrabajoRepositorio(_integraDBContext);

				var listaAsignados = _repPuestoModulo.GetBy(x => x.IdPuestoTrabajo == ListaAsignar.Id).ToList();
                //var listaModuloPuestoTrabajo = _repPuestoTrabajo.ObtenerModulosPorPuestoTrabajo(ListaAsignar.Id);

				ModuloSistemaPuestoTrabajoBO agregar;
				foreach (var item in listaAsignados)
				{
					var existeAsignacion = ListaAsignar.ListaAsignacion.Where(x => x.IdModuloSistema == item.IdModuloSistema).FirstOrDefault();
					if (existeAsignacion == null)
					{
						var eliminar = _repPuestoModulo.FirstById(item.Id);
						_repPuestoModulo.Delete(eliminar.Id, ListaAsignar.Usuario);
					}
					else
					{
						ListaAsignar.ListaAsignacion.Remove(existeAsignacion);
					}
				}
				foreach (var item in ListaAsignar.ListaAsignacion)
				{
					agregar = new ModuloSistemaPuestoTrabajoBO()
					{
						IdModuloSistema = item.IdModuloSistema,
						IdPuestoTrabajo = ListaAsignar.Id,
						Estado = true,
						UsuarioCreacion = ListaAsignar.Usuario,
						UsuarioModificacion = ListaAsignar.Usuario,
						FechaCreacion = DateTime.Now,
						FechaModificacion = DateTime.Now
					};
					_repPuestoModulo.Insert(agregar);
				}

				var listaPersonalCambio = _repPersonalPuestoSedeHistorico.GetBy(x => x.IdPuestoTrabajo == ListaAsignar.Id && x.Actual == true).ToList();
				foreach (var personal in listaPersonalCambio)
				{
					//Asignación de módulos
					var usuario = _usuarioModulo.GetBy(x => x.IdPersonal == personal.IdPersonal).FirstOrDefault();
					if (usuario != null)
					{
						var listaModuloAnterior = _moduloAcceso.GetBy(x => x.IdUsuario == usuario.Id).ToList();
						var listaModuloNuevo = _moduloPuesto.GetBy(x => x.IdPuestoTrabajo == personal.IdPuestoTrabajo).ToList();
						if (listaModuloAnterior.Count > 0)
						{
							foreach (var moduloAnterior in listaModuloAnterior)
							{
								_moduloAcceso.Delete(moduloAnterior.Id, ListaAsignar.Usuario);
							}
						}

						if (listaModuloNuevo.Count > 0)
						{
							ModuloSistemaAccesoBO agregarModulo;
							foreach (var moduloNuevo in listaModuloNuevo)
							{
								agregarModulo = new ModuloSistemaAccesoBO()
								{
									IdUsuarioRol = usuario.IdUsuarioRol,
									IdUsuario = usuario.Id,
									IdModuloSistema = moduloNuevo.IdModuloSistema,
									Estado = true,
									UsuarioCreacion = ListaAsignar.Usuario,
									UsuarioModificacion = ListaAsignar.Usuario,
									FechaCreacion = DateTime.Now,
									FechaModificacion = DateTime.Now
								};
								_moduloAcceso.Insert(agregarModulo);
							}
						}
					}
				}

				return Ok(true);
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}

		/// Autor: Edgar S.
		/// Fecha: 25/01/2021
		/// Versión: 1.0
		/// <summary>
		/// Valida la existencia de una Solicitud de Nueva versión de Puesto de Trabajo 
		/// </summary>
		/// <returns> Objeto DTO: ValidarAprobacionDTO </returns>
		[HttpPost]
		[Route("[Action]")]
		public ActionResult ObtenerExistenciaPerfilPuestoTrabajo([FromBody] int IdPuestoTrabajo)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			try
			{
				ValidarAprobacionDTO listaAprobacionExiste = new ValidarAprobacionDTO();
				List<int> listaPersonalAprobacion = new List<int>();
				List<PerfilPuestoTrabajoDTO> listaPerfilPuestoTrabajoHistorico = listaPerfilPuestoTrabajoHistorico = _repPerfilPuestoTrabajo.ObtenerListaPerfilPuestoTrabajoHistorico(IdPuestoTrabajo);
				var listaPersonal = _repPerfilPuestoTrabajoPersonalAprobacion.GetBy(x => x.IdPuestoTrabajo == IdPuestoTrabajo).ToList();
				foreach (var personalAprobacion in listaPersonal)
				{
					listaPersonalAprobacion.Add(personalAprobacion.IdPersonal);
				}
				listaAprobacionExiste.ListaUsuarioAprobracion = listaPersonalAprobacion;
				if (listaPerfilPuestoTrabajoHistorico.Count > 0)
				{
					var solicitudPendiente = listaPerfilPuestoTrabajoHistorico.Where(x => x.IdPerfilPuestoTrabajoEstadoSolicitud == 5).FirstOrDefault();
					if (solicitudPendiente != null)
					{
						listaAprobacionExiste.Existe = true;
						return Ok(listaAprobacionExiste);
					}
					else
					{
						listaAprobacionExiste.Existe = false;
						return Ok(listaAprobacionExiste);
					}
				}
                else
                {
					listaAprobacionExiste.Existe = false;
					return Ok(listaAprobacionExiste);
				}
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}
