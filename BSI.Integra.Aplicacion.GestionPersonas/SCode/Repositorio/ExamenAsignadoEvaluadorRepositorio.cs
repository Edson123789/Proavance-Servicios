using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.GestionPersonas.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.GestionPersonas.Repositorio
{
	/// Repositorio: ExamenAsignadoEvaluadorRepositorio
	/// Autor: Britsel C., Luis H., Edgar S.
	/// Fecha: 29/01/2021
	/// <summary>
	/// Gestión de Examenes asignados al evaluador por Proceso de Seleccion
	/// </summary>
	public class ExamenAsignadoEvaluadorRepositorio : BaseRepository<TExamenAsignadoEvaluador, ExamenAsignadoEvaluadorBO>
	{
		#region Metodos Base
		public ExamenAsignadoEvaluadorRepositorio() : base()
		{
		}
		public ExamenAsignadoEvaluadorRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ExamenAsignadoEvaluadorBO> GetBy(Expression<Func<TExamenAsignadoEvaluador, bool>> filter)
		{
			IEnumerable<TExamenAsignadoEvaluador> listado = base.GetBy(filter);
			List<ExamenAsignadoEvaluadorBO> listadoBO = new List<ExamenAsignadoEvaluadorBO>();
			foreach (var itemEntidad in listado)
			{
				ExamenAsignadoEvaluadorBO objetoBO = Mapper.Map<TExamenAsignadoEvaluador, ExamenAsignadoEvaluadorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ExamenAsignadoEvaluadorBO FirstById(int id)
		{
			try
			{
				TExamenAsignadoEvaluador entidad = base.FirstById(id);
				ExamenAsignadoEvaluadorBO objetoBO = new ExamenAsignadoEvaluadorBO();
				Mapper.Map<TExamenAsignadoEvaluador, ExamenAsignadoEvaluadorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ExamenAsignadoEvaluadorBO FirstBy(Expression<Func<TExamenAsignadoEvaluador, bool>> filter)
		{
			try
			{
				TExamenAsignadoEvaluador entidad = base.FirstBy(filter);
				ExamenAsignadoEvaluadorBO objetoBO = Mapper.Map<TExamenAsignadoEvaluador, ExamenAsignadoEvaluadorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ExamenAsignadoEvaluadorBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TExamenAsignadoEvaluador entidad = MapeoEntidad(objetoBO);

				bool resultado = base.Insert(entidad);
				if (resultado)
					AsignacionId(entidad, objetoBO);

				return resultado;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(IEnumerable<ExamenAsignadoEvaluadorBO> listadoBO)
		{
			try
			{
				foreach (var objetoBO in listadoBO)
				{
					bool resultado = Insert(objetoBO);
					if (resultado == false)
						return false;
				}

				return true;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public bool Update(ExamenAsignadoEvaluadorBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TExamenAsignadoEvaluador entidad = MapeoEntidad(objetoBO);

				bool resultado = base.Update(entidad);
				if (resultado)
					AsignacionId(entidad, objetoBO);

				return resultado;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public bool Update(IEnumerable<ExamenAsignadoEvaluadorBO> listadoBO)
		{
			try
			{
				foreach (var objetoBO in listadoBO)
				{
					bool resultado = Update(objetoBO);
					if (resultado == false)
						return false;
				}

				return true;
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		private void AsignacionId(TExamenAsignadoEvaluador entidad, ExamenAsignadoEvaluadorBO objetoBO)
		{
			try
			{
				if (entidad != null && objetoBO != null)
				{
					objetoBO.Id = entidad.Id;
				}
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		private TExamenAsignadoEvaluador MapeoEntidad(ExamenAsignadoEvaluadorBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TExamenAsignadoEvaluador entidad = new TExamenAsignadoEvaluador();
				entidad = Mapper.Map<ExamenAsignadoEvaluadorBO, TExamenAsignadoEvaluador>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<ExamenAsignadoEvaluadorBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TExamenAsignadoEvaluador, bool>>> filters, Expression<Func<TExamenAsignadoEvaluador, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TExamenAsignadoEvaluador> listado = base.GetFiltered(filters, orderBy, ascending);
			List<ExamenAsignadoEvaluadorBO> listadoBO = new List<ExamenAsignadoEvaluadorBO>();

			foreach (var itemEntidad in listado)
			{
				ExamenAsignadoEvaluadorBO objetoBO = Mapper.Map<TExamenAsignadoEvaluador, ExamenAsignadoEvaluadorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		/// Repositorio: ExamenAsignadoEvaluadorRepositorio
		/// Autor: Britsel C., Luis H.
		/// Fecha: 29/01/2021
		/// <summary>
		/// Obtiene examenes asignados a evaluador
		/// </summary>
		/// <param name="idProcesoSeleccion"> Id de Proceso de Selección </param>
		/// <returns> Lista de Objeto DTO : List<ConfiguracionAsignacionExamenV2DTO> </returns>
		public List<ConfiguracionAsignacionExamenV2DTO> ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccionV2(int idProcesoSeleccion)
		{
			try
			{
				var query = "SELECT Id, IdProcesoSeleccion, IdEvaluacion, IdExamen, NroOrden FROM [gp].[V_TConfiguracionAsignacionExamenV2] WHERE IdProcesoSeleccion = @IdProcesoSeleccion AND Estado = 1 AND EsCalificadoPorPostulante = 0";
				var res = _dapper.QueryDapper(query, new { IdProcesoSeleccion = idProcesoSeleccion });
				return JsonConvert.DeserializeObject<List<ConfiguracionAsignacionExamenV2DTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// Repositorio: ExamenAsignadoEvaluadorRepositorio
		/// Autor: Britsel C., Luis H., Edgar S.
		/// Fecha: 29/01/2021
		/// <summary>
		/// Obtiene lista de evaluacion de evaluadores
		/// </summary>
		/// <param name="filtro"> filtro de búsqueda </param>
		/// <returns> Lista de Objeto DTO : List<EvaluadorEvaluacionDTO></returns>
		public List<EvaluadorEvaluacionDTO> ObtenerListaEvaluacionEvaluador(EvaluadorEvaluacionFiltroDTO filtro)
		{
			try
			{

				var filtros = new
				{
					ListaPostulante = filtro.ListaPostulante == null ? "" : string.Join(",", filtro.ListaPostulante.Select(x => x)),
					ListaProcesoSeleccion = filtro.ListaProcesoSeleccion == null ? "" : string.Join(",", filtro.ListaProcesoSeleccion.Select(x => x)),
					ListaPersonal = filtro.ListaPersonal == null ? "" : string.Join(",", filtro.ListaPersonal.Select(x => x)),
					ListaPuestoTrabajo = filtro.ListaPuestoTrabajo == null ? "" : string.Join(",", filtro.ListaPuestoTrabajo.Select(x => x)),
					ListaSede = filtro.ListaSede == null ? "" : string.Join(",", filtro.ListaSede.Select(x => x))
				};

				List<EvaluadorEvaluacionDTO> listaEvaluacionEvaluador = new List<EvaluadorEvaluacionDTO>();
				string query = string.Empty;
				query = "gp.SP_EvaluacionEvaluador";
				var evaluacionEvaluador = _dapper.QuerySPDapper(query, filtros);
				if (!string.IsNullOrEmpty(evaluacionEvaluador) && !evaluacionEvaluador.Contains("[]"))
				{
					listaEvaluacionEvaluador = JsonConvert.DeserializeObject<List<EvaluadorEvaluacionDTO>>(evaluacionEvaluador);
				}
				return listaEvaluacionEvaluador;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// Repositorio: ExamenAsignadoEvaluadorRepositorio
		/// Autor: Britsel C., Luis H., Edgar S.
		/// Fecha: 29/01/2021
		/// <summary>
		/// Obtiene lista de preguntas asignadas con a un test
		/// </summary>
		/// <param name="Test"> Información de evaluación </param>
		/// <returns> Lista de objeto DTO: List<PreguntaTestDTO> </returns>
		public List<PreguntaTestDTO> ObtenerPreguntasTest(TestInformacionDTO Test)
		{
			try
			{
				var query = string.Empty;
				List<PreguntaTestDTO> listaTest = new List<PreguntaTestDTO>();

				if (Test.MostrarEvaluacionAgrupado)
				{
					query = @"
						SELECT IdEvaluacion, 
							   IdGrupoComponenteEvaluacion, 
							   IdExamenAsignado, 
							   IdExamen, 
							   IdPostulante, 
							   IdProcesoSeleccion, 
							   IdPregunta, 
							   EnunciadoPregunta, 
							   NroOrdenPregunta, 
							   IdPreguntaTipo, 
							   PreguntaTipo, 
							   IdTipoRespuesta, 
							   TipoRespuesta
						FROM [gp].[V_TExamenAsignadoEvaluador_ObtenerPreguntas]
						WHERE IdProcesoSeleccion = @IdProcesoSeleccion
						AND IdEvaluacion = @IdTest AND Estado = 1
						AND IdPostulante = @IdPostulante
								";
				}
				else if (Test.MostrarEvaluacionPorGrupo)
				{
					query = @"
						SELECT IdEvaluacion, 
							   IdGrupoComponenteEvaluacion, 
							   IdExamenAsignado, 
							   IdExamen, 
							   IdPostulante, 
							   IdProcesoSeleccion, 
							   IdPregunta, 
							   EnunciadoPregunta, 
							   NroOrdenPregunta, 
							   IdPreguntaTipo, 
							   PreguntaTipo, 
							   IdTipoRespuesta, 
							   TipoRespuesta
						FROM [gp].[V_TExamenAsignadoEvaluador_ObtenerPreguntas]
						WHERE IdProcesoSeleccion = @IdProcesoSeleccion
						AND IdGrupoComponenteEvaluacion = @IdTest AND Estado = 1
						AND IdPostulante = @IdPostulante
								";
				}
				else if (Test.MostrarEvaluacionPorComponente)
				{
					query = @"
						SELECT IdEvaluacion, 
							   IdGrupoComponenteEvaluacion, 
							   IdExamenAsignado, 
							   IdExamen, 
							   IdPostulante, 
							   IdProcesoSeleccion, 
							   IdPregunta, 
							   EnunciadoPregunta, 
							   NroOrdenPregunta, 
							   IdPreguntaTipo, 
							   PreguntaTipo, 
							   IdTipoRespuesta, 
							   TipoRespuesta
						FROM [gp].[V_TExamenAsignadoEvaluador_ObtenerPreguntas]
						WHERE IdProcesoSeleccion = @IdProcesoSeleccion
						AND IdExamen = @IdTest AND Estado = 1
						AND IdPostulante = @IdPostulante
								";
				}
				var repuesta = _dapper.QueryDapper(query, new { Test.IdProcesoSeleccion, Test.IdTest, Test.IdPostulante });

				if (!string.IsNullOrEmpty(repuesta) && !repuesta.Contains("[]"))
				{
					listaTest = JsonConvert.DeserializeObject<List<PreguntaTestDTO>>(repuesta);
				}

				return listaTest;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// Repositorio: ExamenAsignadoEvaluadorRepositorio
		/// Autor: Britsel C., Luis H., Edgar S.
		/// Fecha: 29/01/2021
		/// <summary>
		/// obtiene lista de respuesta de preguntas asignadas a un test
		/// </summary>
		/// <param name="IdExamen"> Id de Examen </param>
		/// <param name="IdPregunta"> Id de Pregunta</param>
		/// <returns> Lista de objeto DTO : List<RespuestasTestDTO> </returns>
		public List<RespuestasTestDTO> ObtenerListaPreguntasRespuestaTest(int IdExamen, int IdPregunta)
		{
			var query = string.Empty;
			List<RespuestasTestDTO> listaExamenPreguntas = new List<RespuestasTestDTO>();

			query = "SELECT IdPregunta, IdRespuesta, NroOrden, EnunciadoRespuesta FROM gp.V_TAsignacionPreguntaExamen_ObtenerRespuestasPreguntas WHERE IdExamen = @IdExamen AND IdPregunta = @IdPregunta AND Estado = 1";
			var repuesta = _dapper.QueryDapper(query, new { IdExamen, IdPregunta });

			if (!string.IsNullOrEmpty(repuesta) && !repuesta.Contains("[]"))
			{
				listaExamenPreguntas = JsonConvert.DeserializeObject<List<RespuestasTestDTO>>(repuesta);
			}

			return listaExamenPreguntas;
		}

		/// Repositorio: ExamenAsignadoEvaluadorRepositorio
		/// Autor: Britsel C., Luis H., Edgar S.
		/// Fecha: 29/01/2021
		/// <summary>
		/// Obtiene lista de evaluaciones de evaluador asignadas
		/// </summary>
		/// <param name="filtro"> Filtro de búsqueda </param>
		/// <returns> Lista de objeto DTO:  List<EvaluacionesAsignadasEvaluador> </returns>
		public List<EvaluacionesAsignadasEvaluador> ObtenerListaEvaluacionEvaluador(FiltroEvaluacionEvaluador filtro)
		{
			try
			{
				List<EvaluacionesAsignadasEvaluador> listaEvaluacionAsignadaEvaluador = new List<EvaluacionesAsignadasEvaluador>();
				string query = string.Empty;
				query = "SELECT Id, IdPostulante, IdProcesoSeleccion, IdExamen, IdGrupoComponenteEvaluacion, IdEvaluacion, Evaluacion, MostrarEvaluacionAgrupado, MostrarEvaluacionPorGrupo, MostrarEvaluacionPorComponente, EstadoExamen, RequiereTiempo, DuracionMinutos, Instrucciones FROM [gp].[V_TExamenAsignadoEvaluador_ObtenerExamenAsignado] WHERE Estado = 1 AND IdPostulante = @IdPostulante AND IdProcesoSeleccion = @IdProcesoSeleccion";
				var evaluacionEvaluador = _dapper.QueryDapper(query, new { IdPostulante = filtro.IdPostulante, IdProcesoSeleccion = filtro.IdProcesoSeleccion });
				if (!string.IsNullOrEmpty(evaluacionEvaluador) && !evaluacionEvaluador.Contains("[]"))
				{
					listaEvaluacionAsignadaEvaluador = JsonConvert.DeserializeObject<List<EvaluacionesAsignadasEvaluador>>(evaluacionEvaluador);
				}
				return listaEvaluacionAsignadaEvaluador;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// Repositorio: ExamenAsignadoEvaluadorRepositorio
		/// Autor: Jashin Salazar
		/// Fecha: 03/11/2021
		/// <summary>
		/// Obtiene lista de evaluaciones del portal por proceso
		/// </summary>
		/// <param name="filtro"> Filtro de búsqueda </param>
		/// <returns> Lista de objeto DTO:  List<EvaluacionPortalPostulante> </returns>
		public List<EvaluacionPortalPostulante> ObtenerEvaluacionesPortalPostulante(ReportePostulanteDTO filtro)
		{
			try
			{
				List<EvaluacionPortalPostulante> listaEvaluacionesPortalPostulante = new List<EvaluacionPortalPostulante>();
				string query = string.Empty;
				string evaluacionPortal = string.Empty;
				if (filtro.Check)
				{
					var IdP = filtro.ListaPostulante.ElementAt(0);
					query = "SELECT IdProcesoSeleccion,IdPostulante,IdExamen,IdPespecifico,IdProgramaGeneral,IdAlumno,CantidadConfigurado,CantidadResuelto,PuntajeCurso FROM gp.V_ObtenerPuntajeCursoPostulante WHERE IdPostulante = @IdPostulante AND IdExamen IS NOT NULL";
					evaluacionPortal = _dapper.QueryDapper(query, new { IdPostulante = IdP });
				}
				else
				{
					query = "SELECT IdProcesoSeleccion,IdPostulante,IdExamen,IdPespecifico,IdProgramaGeneral,IdAlumno,CantidadConfigurado,CantidadResuelto,PuntajeCurso FROM gp.V_ObtenerPuntajeCursoPostulante WHERE IdProcesoSeleccion = @IdProcesoSeleccion AND IdExamen IS NOT NULL";
					evaluacionPortal = _dapper.QueryDapper(query, new { IdProcesoSeleccion = filtro.ListaProcesoSeleccion });
				}

				if (!string.IsNullOrEmpty(evaluacionPortal) && !evaluacionPortal.Contains("[]"))
				{
					listaEvaluacionesPortalPostulante = JsonConvert.DeserializeObject<List<EvaluacionPortalPostulante>>(evaluacionPortal);
				}
				return listaEvaluacionesPortalPostulante;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
