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
	/// Repositorio: ExamenRepositorio
	/// Autor: Britsel Calluchi - Luis Huallpa - Edgar Serruto
	/// Fecha: 29/01/2021
	/// <summary>
	/// Gestión de Examenes tabla T_Examen
	/// </summary>
	public class ExamenRepositorio : BaseRepository<TExamen, ExamenBO>
	{
		#region Metodos Base
		public ExamenRepositorio() : base()
		{
		}
		public ExamenRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<ExamenBO> GetBy(Expression<Func<TExamen, bool>> filter)
		{
			IEnumerable<TExamen> listado = base.GetBy(filter);
			List<ExamenBO> listadoBO = new List<ExamenBO>();
			foreach (var itemEntidad in listado)
			{
				ExamenBO objetoBO = Mapper.Map<TExamen, ExamenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public ExamenBO FirstById(int id)
		{
			try
			{
				TExamen entidad = base.FirstById(id);
				ExamenBO objetoBO = new ExamenBO();
				Mapper.Map<TExamen, ExamenBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public ExamenBO FirstBy(Expression<Func<TExamen, bool>> filter)
		{
			try
			{
				TExamen entidad = base.FirstBy(filter);
				ExamenBO objetoBO = Mapper.Map<TExamen, ExamenBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(ExamenBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TExamen entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<ExamenBO> listadoBO)
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

		public bool Update(ExamenBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TExamen entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<ExamenBO> listadoBO)
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
		private void AsignacionId(TExamen entidad, ExamenBO objetoBO)
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

		private TExamen MapeoEntidad(ExamenBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TExamen entidad = new TExamen();
				entidad = Mapper.Map<ExamenBO, TExamen>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<ExamenBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TExamen, bool>>> filters, Expression<Func<TExamen, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TExamen> listado = base.GetFiltered(filters, orderBy, ascending);
			List<ExamenBO> listadoBO = new List<ExamenBO>();

			foreach (var itemEntidad in listado)
			{
				ExamenBO objetoBO = Mapper.Map<TExamen, ExamenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		/// <summary>
		/// Obtiene todos los elementos para filtro
		/// </summary>
		/// <returns></returns>
		public List<FiltroDTO> ObtenerTodoFiltro()
		{
			try
			{
				return this.GetBy(x => x.Estado == true).Select(x => new FiltroDTO { Id = x.Id, Nombre = x.Titulo }).ToList();
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public List<ReporteGmatPmaDTO> ObtenerReporteGmat(ReporteProcesoSeleccionFiltroDTO filtro)
		{
			try
			{
				var query = "gp.SP_ReporteSelecionPersonal_GMAT_PMA";
				var res = _dapper.QuerySPDapper(query, new { filtro.IdSexo, filtro.IdPuesto, filtro.IdSede, filtro.FechaInicio, Secciones = filtro.Psicotecnico, filtro.FechaFin});
				var rpta = JsonConvert.DeserializeObject<List<ReporteGmatPmaDTO>>(res);
				return rpta;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

		}
		public List<ReporteGmatPmaDTO> ObtenerReporteGmatPmaPostulante(ReporteProcesoSeleccionFiltroDTO filtro)
		{
			try
			{
				var query = "gp.SP_ReporteSelecionPersonal_GMAT_PMAPostulantes";
				var res = _dapper.QuerySPDapper(query, new { filtro.Postulante });
				var rpta = JsonConvert.DeserializeObject<List<ReporteGmatPmaDTO>>(res);
				return rpta;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

		}

		public List<ProcesoSelecionPsicologicoDTO> ObtenerReporteIsraOptimismoNeopir(ReporteProcesoSeleccionFiltroDTO filtro)
		{
			try
			{
				var query = "gp.SP_ReporteSelecionPersonal_ISRA_OPT_NEO";
				var res = _dapper.QuerySPDapper(query, new { filtro.IdSexo, filtro.IdPuesto, filtro.IdSede, Secciones = filtro.Psicologico, filtro.FechaInicio, filtro.FechaFin });
				var rpta = JsonConvert.DeserializeObject<List<ProcesoSelecionPsicologicoDTO>>(res);
				return rpta;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

		}

		public List<ProcesoSelecionPsicologicoDTO> ObtenerReporteIsraOptimismoNeopirPostulante(ReporteProcesoSeleccionFiltroDTO filtro)
		{
			try
			{
				var query = "gp.SP_ReporteSelecionPersonal_ISRA_OPT_NEOPostulantes";
				var res = _dapper.QuerySPDapper(query, new { filtro.Postulante });
				var rpta = JsonConvert.DeserializeObject<List<ProcesoSelecionPsicologicoDTO>>(res);
				return rpta;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

		}

		public List<DatosExamenPostulanteDTO> ObtenerReporteExamenPostulante(ReporteExamenPostulanteDTO filtro)
		{
			try
			{
				var query = "gp.SP_ReporteExamenSelecionPersonal";
				var res = _dapper.QuerySPDapper(query, new { filtro.Postulante, filtro.IdPuesto,filtro.IdSede,filtro.FechaInicio,filtro.FechaFin });
				var rpta = JsonConvert.DeserializeObject<List<DatosExamenPostulanteDTO>>(res);
				return rpta;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

		}
		/// Repositorio: ExamenRepositorio
		/// Autor: Edgar S.
		/// Fecha: 27/03/2021
		/// <summary>
		///	Obtiene información de Notas de Postulantes
		/// </summary>
		/// <param name="filtro"> Filtros de Búsqueda </param>
		/// <returns> List<DatosExamenPostulanteDTO> </returns>
		public List<DatosExamenPostulanteDTO> ObtenerReporteExamenPostulante_V2(ReportePostulanteDTO filtro)
		{
			try
			{
				var filtros = new
				{
					Postulante = filtro.ListaPostulante.Count==0? null : string.Join(",", filtro.ListaPostulante.Select(x => x)),
					IdProcesoSeleccion = filtro.ListaProcesoSeleccion == null ? null : string.Join(",", filtro.ListaProcesoSeleccion),
					FechaInicio = filtro.FechaInicio,
					FechaFin = filtro.FechaFin,
				};
				var query = "gp.SP_ReporteExamenSelecionPersonal_V2";
				var res = _dapper.QuerySPDapper(query, filtros);
				var rpta = JsonConvert.DeserializeObject<List<DatosExamenPostulanteDTO>>(res);

				if(filtro.ListaPostulanteGrupoComparacion != null)
                {
					var filtrosGrupoComparacion = new
					{
						Postulante = filtro.ListaPostulante.Count == 0 ? null : string.Join(",", filtro.ListaPostulanteGrupoComparacion.Select(x => x)),
						IdProcesoSeleccion = filtro.ListaProcesoSeleccion == null ? null : string.Join(",", filtro.ListaProcesoSeleccionGrupoComparacion)
					};
					var queryGrupoComparacion = "gp.SP_ReporteExamenPersonalGrupoComparacion";
					var resGrupoComparacion = _dapper.QuerySPDapper(queryGrupoComparacion, filtrosGrupoComparacion);
					var rptaGrupoComparacion = JsonConvert.DeserializeObject<List<DatosExamenPostulanteDTO>>(resGrupoComparacion);

					rpta.AddRange(rptaGrupoComparacion);
				}

				return rpta;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

		}
		public List<EstructuraBasicaDTO> ObtenerExamenNoAsignadoProcesoSeleccion(int IdProcesoSeleccion)
		{
			try
			{
				List<EstructuraBasicaDTO> ProcesoSeleccion = new List<EstructuraBasicaDTO>();
				var listaProcesoDB = _dapper.QuerySPDapper("gp.SP_ExamenesNoAsociadosConfiguracion", new { IdProcesoSeleccion });
				if (!string.IsNullOrEmpty(listaProcesoDB) && !listaProcesoDB.Contains("[]"))
				{
					ProcesoSeleccion = JsonConvert.DeserializeObject<List<EstructuraBasicaDTO>>(listaProcesoDB);
				}
				return ProcesoSeleccion;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public List<ExamenAsignadoProcesoDTO> ObtenerExamenAsignadoProcesoSeleccion(int IdProcesoSeleccion)
		{
			try
			{
				List<ExamenAsignadoProcesoDTO> ProcesoSeleccion = new List<ExamenAsignadoProcesoDTO>();
				var listaProcesoDB = _dapper.QuerySPDapper("gp.SP_ExamenesAsociadosConfiguracion", new { IdProcesoSeleccion });
				if (!string.IsNullOrEmpty(listaProcesoDB) && !listaProcesoDB.Contains("[]"))
				{
					ProcesoSeleccion = JsonConvert.DeserializeObject<List<ExamenAsignadoProcesoDTO>>(listaProcesoDB);
				}
				return ProcesoSeleccion;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public List<EvaluacionPersonaCompletoDTO> ObtenerEvaluacionPersonaCompleto()
		{
			try
			{
				List<EvaluacionPersonaCompletoDTO> EvaluacionPersona = new List<EvaluacionPersonaCompletoDTO>();
				var campos = "IdExamen,NombreEvaluacion,TituloEvaluacion,Instrucciones,CronometrarExamen,TiempoLimiteExamen,IdExamenTest,ExamenTest,IdCriterioEvaluacionProceso,IdExamenConfiguracionFormato,ColorTextoTitulo,TamanioTextoTitulo,ColorFondoTitulo,TipoLetraTitulo,ColorTextoEnunciado" +
					",TamanioTextoEnunciado,ColorFondoEnunciado,TipoLetraEnunciado,ColorTextoRespuesta,TamanioTextoRespuesta,ColorFondoRespuesta,TipoLetraRespuesta,IdExamenComportamiento,ResponderTodasLasPreguntas,IdEvaluacionFeedBackAprobado" +
					",IdEvaluacionFeedBackDesaprobado,IdEvaluacionFeedBackCancelado,IdExamenConfigurarResultado,CalificarExamen,PuntajeExamen,PuntajeAprobacion,MostrarResultado,MostrarPuntajeTotal,MostrarPuntajePregunta,UsuarioModificacion,RequiereCentil,IdFormulaPuntaje, Factor";

				var _query = "SELECT " + campos + " FROM  gp.V_ObtenerEValuaciones WHERE Estado = 1";
				var dataDB = _dapper.QueryDapper(_query, null);
				if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
				{
					EvaluacionPersona = JsonConvert.DeserializeObject<List<EvaluacionPersonaCompletoDTO>>(dataDB);
				}
				return EvaluacionPersona;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// Repositorio: ExamenRepositorio
		/// Autor: Edgar S.
		/// Fecha: 11/05/2021
		/// <summary>
		/// Obtiene calificación máxima de componente según fórmula
		/// </summary>
		/// <param name="idExamen"> FK de T_Examen </param>
		/// <returns> List<PuntajeCalificacionComponenteDTO> </returns>
		public PuntajeCalificacionComponenteDTO ObtenerPuntajeCalificacion(int idExamen)
		{
			try
			{
				List<PuntajeCalificacionComponenteDTO> evaluacionPersona =new List<PuntajeCalificacionComponenteDTO>();
				var query = "SELECT PuntajeTipoRespuesta, CantidadPreguntas, SumaPuntaje FROM  gp.V_PuntajeCalificacionComponente WHERE IdExamen=" + idExamen;
				var dataDB = _dapper.QueryDapper(query, null);
				if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
				{
					evaluacionPersona = JsonConvert.DeserializeObject<List<PuntajeCalificacionComponenteDTO>>(dataDB);
				}

                if (evaluacionPersona.Count == 0)
                {
					PuntajeCalificacionComponenteDTO auxiliar = new PuntajeCalificacionComponenteDTO();
					evaluacionPersona.Add(auxiliar);
				}
				return evaluacionPersona[0];

			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool InsertarCentilExamen(CentilDTO item)
		{
			try
			{
				var resultado = new Dictionary<string, bool>();

				string query = _dapper.QuerySPFirstOrDefault("gp.SP_InsertarCentil", item);
				if (!string.IsNullOrEmpty(query))
				{
					resultado = JsonConvert.DeserializeObject<Dictionary<string, bool>>(query);
				}
				return resultado.Select(x => x.Value).FirstOrDefault();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}


		/// <summary>
		/// Obtiene lista de componentes asociados a una evaluacion
		/// </summary>
		/// <returns></returns>
		public List<ComponenteAsociadoEvaluacion> ObtenerComponentesAsociadosEvaluacion(int idEvaluacion)
		{
			try
			{
				List<ComponenteAsociadoEvaluacion> lista = new List<ComponenteAsociadoEvaluacion>();
				var _query = "SELECT IdExamen, NombreExamen, FactorComponente, IdEvaluacion FROM gp.V_TExamenTest_ObtenerExamenesAsociados WHERE Estado = 1 AND IdEvaluacion = @IdEvaluacion";
				var dataDB = _dapper.QueryDapper(_query, new { IdEvaluacion = idEvaluacion });
				if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
				{
					lista = JsonConvert.DeserializeObject<List<ComponenteAsociadoEvaluacion>>(dataDB);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}


		/// <summary>
		/// Obtiene lista de calificacion y centiles por IdProcesoSeleccion
		/// </summary>
		/// <returns></returns>
		public List<ObtenerCalificacionCentilDTO> ObtenerInformacionCentilPorProcesoSeleccion(string ListaProcesoSeleccion)
		{
			try
			{
				var query = "SELECT Id, CalificaPorCentil,EsCalificable,IdProcesoSeleccionRango,IdProcesoSeleccion,IdExamen,IdExamenTest,IdGrupoComponenteEvaluacion, PuntajeMinimo, IdCentil,Centil,IdSexoCentil,ValorMinimo,ValorMaximo from gp.V_ObtenerCalificacionCentiles WHERE IdProcesoSeleccion in ("+ ListaProcesoSeleccion +") AND Estado = 1";
				var res = _dapper.QueryDapper(query, new { IdProcesoSeleccion = ListaProcesoSeleccion });
				return JsonConvert.DeserializeObject<List<ObtenerCalificacionCentilDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene lista de Centiles por IdExamen
		/// </summary>
		/// <returns></returns>
		public List<CentilBO> ObtenerCentilesAsociados(string listaIdExamen)
		{
			try
			{
				var query = "SELECT Id, IdExamenTest,IdGrupoComponenteEvaluacion,IdExamen,IdSexo,ValorMinimo,ValorMaximo,Centil, CentilLetra, Estado from gp.V_Centil_ObtenerCalificacionCentilesPorExamen WHERE IdExamen in (" + listaIdExamen + ") AND Estado = 1";
				var res = _dapper.QueryDapper(query, new { IdProcesoSeleccion = listaIdExamen });
				return JsonConvert.DeserializeObject<List<CentilBO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// Repositorio: ExamenRepositorio
		/// Autor: Edgar S.
		/// Fecha: 25/03/2021
		/// <summary>
		/// Obtiene Puntaje de Por Examen Test
		/// </summary>
		/// <param name="filtro"> Valores de búsqueda de puntajes </param>
		/// <returns> Lista Objeto DTO: List<DatosExamenPostulanteDTO> </returns>
		public List<DatosExamenPostulanteDTO> ObtenerPuntajeExamenTest(CalificacionAutomaticaDTO filtro)
		{
			try
			{
				var filtros = new
				{
                    filtro.IdPostulante,
					filtro.IdProcesoSeleccion,
					filtro.IdExamenTest
				};
				var query = "gp.SP_ObtenerPuntajesExamenTest";
				var res = _dapper.QuerySPDapper(query, filtros);
				var rpta = JsonConvert.DeserializeObject<List<DatosExamenPostulanteDTO>>(res);
				return rpta;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

		}

		/// Repositorio: ExamenRepositorio
		/// Autor: Edgar S.
		/// Fecha: 25/03/2021
		/// <summary>
		/// Obtiene lista de configuración de cantidad de evaluaciones y componentes asignados
		/// </summary>
		/// <returns> List<ObtenerConfiguracionExamenTestDTO> </returns>
		public List<ObtenerConfiguracionExamenTestDTO> ObtenerConfiguracionPuntaje()
		{
			try
			{
				List<ObtenerConfiguracionExamenTestDTO> lista = new List<ObtenerConfiguracionExamenTestDTO>();
				var query = "SELECT IdExamenTest, IdExamen, IdGrupo, CantidadPreguntas FROM [gp].[V_ObtenerConfiguracionPorExamenTest] WHERE Estado = 1";
				var dataDB = _dapper.QueryDapper(query, null);
				if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
				{
					lista = JsonConvert.DeserializeObject<List<ObtenerConfiguracionExamenTestDTO>>(dataDB);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Repositorio: ExamenRepositorio
		/// Autor: Edgar Serruto.
		/// Fecha: 18/06/2021
		/// <summary>
		/// Obtiene lista de componentes asociados a centro de costo
		/// </summary>
		/// <returns> List<CentroCostoComponenteDTO> </returns>
		public List<FiltroExamenTestExamenDTO> ObtenerExamenPostulanteFiltro()
		{
			try
			{
				List<FiltroExamenTestExamenDTO> listaFiltroEvaluacionComponente = new List<FiltroExamenTestExamenDTO>();
				var query = "SELECT IdExamenTest, IdExamen, Examen FROM gp.V_TExamen_ObtenerExamenesPostulanteCombo ORDER BY Examen";
				var dataDB = _dapper.QueryDapper(query, null);
				if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
				{
					listaFiltroEvaluacionComponente = JsonConvert.DeserializeObject<List<FiltroExamenTestExamenDTO>>(dataDB);
				}
				return listaFiltroEvaluacionComponente;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Repositorio: ExamenRepositorio
		/// Autor: Edgar Serruto.
		/// Fecha: 18/06/2021
		/// <summary>
		/// Obtiene lista de componentes asociados a centro de costo
		/// </summary>
		/// <returns> List<CentroCostoComponenteDTO> </returns>
		public List<CentroCostoComponenteDTO> ObtenerCursoAsociadoComponente()
		{
			try
			{
				List<CentroCostoComponenteDTO> listaCentroCostoComponente = new List<CentroCostoComponenteDTO>();
				var query = "SELECT IdExamenTest, ExamenTest, IdExamen, Examen, IdCentroCosto, CentroCosto, CantidadDiasAcceso FROM gp.V_TExamen_ObtenerCursoPorComponente";
				var dataDB = _dapper.QueryDapper(query, null);
				if (!dataDB.Contains("[]") && !string.IsNullOrEmpty(dataDB))
				{
					listaCentroCostoComponente = JsonConvert.DeserializeObject<List<CentroCostoComponenteDTO>>(dataDB);
				}
				return listaCentroCostoComponente;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
