using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
	/// Repositorio: Gestion de Personas/Postulante
	/// Autor: Luis Huallpa - Britsel C - Jose V.
	/// Fecha: 28/04/2021
	/// <summary>
	/// Repositorio para consultas de gp.T_Postulante
	/// </summary>
	public class PostulanteRepositorio : BaseRepository<TPostulante, PostulanteBO>
	{
		#region Metodos Base
		public PostulanteRepositorio() : base()
		{
		}
		public PostulanteRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<PostulanteBO> GetBy(Expression<Func<TPostulante, bool>> filter)
		{
			IEnumerable<TPostulante> listado = base.GetBy(filter);
			List<PostulanteBO> listadoBO = new List<PostulanteBO>();
			foreach (var itemEntidad in listado)
			{
				PostulanteBO objetoBO = Mapper.Map<TPostulante, PostulanteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public PostulanteBO FirstById(int id)
		{
			try
			{
				TPostulante entidad = base.FirstById(id);
				PostulanteBO objetoBO = new PostulanteBO();
				Mapper.Map<TPostulante, PostulanteBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public PostulanteBO FirstBy(Expression<Func<TPostulante, bool>> filter)
		{
			try
			{
				TPostulante entidad = base.FirstBy(filter);
				PostulanteBO objetoBO = Mapper.Map<TPostulante, PostulanteBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(PostulanteBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TPostulante entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<PostulanteBO> listadoBO)
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

		public bool Update(PostulanteBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TPostulante entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<PostulanteBO> listadoBO)
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
		private void AsignacionId(TPostulante entidad, PostulanteBO objetoBO)
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

		private TPostulante MapeoEntidad(PostulanteBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TPostulante entidad = new TPostulante();
				entidad = Mapper.Map<PostulanteBO, TPostulante>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public IEnumerable<PostulanteBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TPostulante, bool>>> filters, Expression<Func<TPostulante, KProperty>> orderBy, bool ascending)
		{
			IEnumerable<TPostulante> listado = base.GetFiltered(filters, orderBy, ascending);
			List<PostulanteBO> listadoBO = new List<PostulanteBO>();

			foreach (var itemEntidad in listado)
			{
				PostulanteBO objetoBO = Mapper.Map<TPostulante, PostulanteBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}
			return listadoBO;
		}
		#endregion

		/// <summary>
		/// Obtiene el Id y Nombre para ComboBox
		/// </summary>
		/// <returns></returns>
		public List<FiltroDTO> GetFiltroIdNombre()
		{
			var lista = GetBy(x => x.Estado == true, y => new FiltroDTO
			{
				Id = y.Id,
				Nombre = string.Concat(y.Nombre, " ", y.ApellidoPaterno, " ", y.ApellidoMaterno).Trim()
			}).ToList();
			return lista;
		}

		///Repositorio: PostulanteRepositorio
		///Autor: Jose Villena.
		///Fecha: 28/04/2021
		/// <summary>
		/// Obtiene el email1 del Postulante
		/// </summary>
		/// <param name="id"> Id Postulante </param>
		/// <returns>Email 1 del Postulante</returns>
		public string ObtenerEmail(int id)
		{
			try
			{
				return this.GetBy(x => x.Id == id).FirstOrDefault().Email;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene informacion de los postulantes inscritos
		/// </summary>
		/// <returns></returns>
		public List<PostulanteInformacionDTO> ObtenerPostulantesInscritos()
		{
			try
			{
				var query = "SELECT Id, Nombre, ApellidoPaterno, ApellidoMaterno, NroDocumento, Celular, Email, IdTipoDocumento, IdPais, IdCiudad, FechaModificacion, IdPostulanteProcesoSeleccion, IdEstadoProcesoSeleccion, IdProcesoSeleccion, ProcesoSeleccion, FechaRendicionExamen, FechaEnvioAccesos,IdPostulanteNivelPotencial,IdProveedor,IdPersonal_OperadorProceso,IdConvocatoriaPersonal,IdProcesoSeleccionEtapa,IdEstadoEtapaProcesoSeleccion,IdRespuestas,IdSexo,FechaNacimiento,CantidadHijo,UrlPerfilFacebook,UrlPerfilLinkedin FROM [gp].[V_TPostulante_ObtenerDatosPostulante2] WHERE Estado = 1 AND RowNumber = 1";
				var res = _dapper.QueryDapper(query, null);
				return JsonConvert.DeserializeObject<List<PostulanteInformacionDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene numero de telefono del postulante para el whatsapp
		/// </summary>
		/// <param name="idPostulante"></param>
		/// <returns></returns>
		public WhatsAppResultadoPostulanteDTO ObtenerNumeroConCodigoPaisWhatsApp(int idPostulante)
		{
			try
			{
				WhatsAppResultadoPostulanteDTO resultado = new WhatsAppResultadoPostulanteDTO();
				string _queryResultado = "SELECT IdPostulante, Celular , IdCodigoPais FROM [gp].[V_TPostulante_NumeroWhatsApp] WHERE IdPostulante=@IdPostulante AND Estado = 1";
				var queryResultado = _dapper.FirstOrDefault(_queryResultado, new { IdPostulante = idPostulante });
				if (queryResultado != "[]" && queryResultado != "null")
				{
					resultado = JsonConvert.DeserializeObject<WhatsAppResultadoPostulanteDTO>(queryResultado);
					return resultado;
				}
				return resultado;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene el Id,Nombre de los postulantes inscritos
		/// </summary>
		/// <param name="valor"></param>
		/// <returns></returns>
		public List<FiltroDTO> ObtenerTodoFiltroAutoComplete(string valor)
		{
			try
			{
				List<FiltroDTO> postulanteAutocompleteFiltro = new List<FiltroDTO>();
				string _queryPostulanteFiltro = string.Empty;
				_queryPostulanteFiltro = "SELECT Id, Nombre from gp.V_TPostulante_ObtenerPostulanteParaFiltro WHERE Nombre LIKE CONCAT('%',@valor,'%') AND Estado = 1 ORDER By Nombre ASC";
				var PostulanteDB = _dapper.QueryDapper(_queryPostulanteFiltro, new { valor });
				if (!string.IsNullOrEmpty(PostulanteDB) && !PostulanteDB.Contains("[]"))
				{
					postulanteAutocompleteFiltro = JsonConvert.DeserializeObject<List<FiltroDTO>>(PostulanteDB);
				}
				return postulanteAutocompleteFiltro;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene informacion de postulante por filtro
		/// </summary>
		/// <param name="filtro"></param>
		/// <returns></returns>
		public List<PostulanteInformacionDatosDTO> ObtenerPostulanteInformacion(PostulanteReporteFiltroDTO filtro)
		{
			try
			{

				var filtros = new
				{
					ListaPostulante = filtro.ListaPostulante == null ? "" : string.Join(",", filtro.ListaPostulante.Select(x => x)),
					ListaPuestoTrabajo = filtro.ListaPuestoTrabajo == null ? "" : string.Join(",", filtro.ListaPuestoTrabajo.Select(x => x)),
					ListaSede = filtro.ListaSede == null ? "" : string.Join(",", filtro.ListaSede.Select(x => x)),
					FechaInicio = filtro.FechaInicio,
					FechaFin = filtro.FechaFin,
				};

				List<PostulanteInformacionDatosDTO> postulanteAutocompleteFiltro = new List<PostulanteInformacionDatosDTO>();
				string query = string.Empty;
				query = "gp.SP_ObtenerInformacionPostulantes";
				var PostulanteDB = _dapper.QuerySPDapper(query, filtros);
				if (!string.IsNullOrEmpty(PostulanteDB) && !PostulanteDB.Contains("[]"))
				{
					postulanteAutocompleteFiltro = JsonConvert.DeserializeObject<List<PostulanteInformacionDatosDTO>>(PostulanteDB);
				}
				return postulanteAutocompleteFiltro;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene informacion de postulante por filtro
		/// </summary>
		/// <param name="filtro"></param>
		/// <returns> Lista de Objeto DTO : List<PostulanteInformacionDatosDTO> </returns>
		public List<PostulanteInformacionDatosDTO> ObtenerPostulanteInformacion_V2(ReportePostulanteDTO filtro)
		{
			try
			{

				var filtros = new
				{
					ListaPostulante = filtro.ListaPostulante == null ? "" : string.Join(",", filtro.ListaPostulante.Select(x => x)),
					ListaProcesoSeleccion = filtro.ListaProcesoSeleccion == null ? "" : string.Join(",", filtro.ListaProcesoSeleccion),
					ListaEstado = filtro.ListaEstadoProceso == null ? "" : string.Join(",", filtro.ListaEstadoProceso.Select(x => x)),
					FechaInicio = filtro.FechaInicio,
					FechaFin = filtro.FechaFin,
				};

				List<PostulanteInformacionDatosDTO> postulanteAutocompleteFiltro = new List<PostulanteInformacionDatosDTO>();
				string query = string.Empty;
				query = "gp.SP_ObtenerInformacionPostulantes_V2";
				var PostulanteDB = _dapper.QuerySPDapper(query, filtros);
				if (!string.IsNullOrEmpty(PostulanteDB) && !PostulanteDB.Contains("[]"))
				{
					postulanteAutocompleteFiltro = JsonConvert.DeserializeObject<List<PostulanteInformacionDatosDTO>>(PostulanteDB);
				}
				return postulanteAutocompleteFiltro;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		//PARA PLANTILLAS

		/// <summary>
		/// Obtiene lista de procesos de seleccion a lso que el postulante se inscribio mediante el idPostulante
		/// </summary>
		/// <param name="idPostulante"></param>
		/// <returns></returns>
		public ProcesoSeleccionInscritoDTO ObtenerProcesoSeleccionInscrito(int idPostulanteProcesoSeleccion)
		{
			try
			{
				var query = "SELECT Id, IdPostulante, Postulante, IdProcesoSeleccion, ProcesoSeleccion, IdPuestoTrabajo, PuestoTrabajo, IdSede, Sede, FechaRegistro FROM [gp].[V_TPostulanteProcesoSeleccion_ObtenerProcesoSeleccionados] WHERE Id = @IdPostulanteProcesoSeleccion AND Estado = 1 AND Activo = 1 ORDER BY FechaRegistro DESC";
				var res = _dapper.FirstOrDefault(query, new { IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion });
				return JsonConvert.DeserializeObject<ProcesoSeleccionInscritoDTO>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}


		/// <summary>
		/// Obtiene datos del postulante junto a su token
		/// </summary>
		/// <param name="idPostulanteProcesoSeleccion"></param>
		/// <returns></returns>
		public PostulanteAccesoProcesoSeleccionDTO ObtenerPostulanteProcesoSeleccion(int idPostulanteProcesoSeleccion)
		{
			try
			{
				var query = "SELECT Id, IdPostulante, Postulante, Dni, Email, ProcesoSeleccion, Token, GuidAccess FROM [gp].[V_TPostulanteProcesoSeleccion_ObtenerPostulanteProceso] WHERE Id = @IdPostulanteProcesoSeleccion AND Estado = 1";
				var res = _dapper.FirstOrDefault(query, new { IdPostulanteProcesoSeleccion = idPostulanteProcesoSeleccion });
				return JsonConvert.DeserializeObject<PostulanteAccesoProcesoSeleccionDTO>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}


		/// <summary>
		/// Obtiene informacion de postulante por filtro
		/// </summary>
		/// <param name="filtro"></param>
		/// <returns></returns>
		public List<PostulanteUltimoProcesoSeleccionDTO> ObtenerPostulantesUltimoProcesoSeleccion(ReportePostulanteDTO filtro)
		{
			try
			{

				var filtros = new
				{
					ListaPostulante = filtro.ListaPostulante == null ? "" : string.Join(",", filtro.ListaPostulante.Select(x => x)),
					ListaProcesoSeleccion = filtro.ListaProcesoSeleccion == null ? "" : string.Join(",", filtro.ListaProcesoSeleccion),
					ListaEstado = filtro.ListaEstadoProceso == null ? "" : string.Join(",", filtro.ListaEstadoProceso.Select(x => x)),
					FechaInicio = filtro.FechaInicio,
					FechaFin = filtro.FechaFin,
				};

				List<PostulanteUltimoProcesoSeleccionDTO> postulanteAutocompleteFiltro = new List<PostulanteUltimoProcesoSeleccionDTO>();
				string query = string.Empty;
				if (filtro.Check == true)
				{
					if (filtros.ListaPostulante.Length > 0)
					{
						query = "Select IdPostulante,Postulante,IdProcesoSeleccion,ProcesoSeleccion FROM gp.V_PostulanteUltimoProcesoSeleccion where IdPostulante in(" + filtros.ListaPostulante + ")";
					}
					else
					{
						query = "Select IdPostulante,Postulante,IdProcesoSeleccion,ProcesoSeleccion FROM gp.V_PostulanteUltimoProcesoSeleccion";
					}
				}
				else {

					if (filtro.FechaInicio == null || filtro.FechaFin == null)
					{

						filtro.FechaFin = DateTime.Now;
						filtro.FechaInicio = new DateTime(1900, 12, 31);
					}

					if (filtros.ListaProcesoSeleccion.Length > 0)
					{
						query = "Select IdPostulante,Postulante,IdProcesoSeleccion,ProcesoSeleccion FROM gp.V_PostulanteUltimoProcesoSeleccion where FechaExamen>=@fechaInicio and FechaExamen<=@fechaFin and IdProcesoSeleccion in(" + filtros.ListaProcesoSeleccion + ")";
					}
					else
					{
						query = "Select IdPostulante,Postulante,IdProcesoSeleccion,ProcesoSeleccion FROM gp.V_PostulanteUltimoProcesoSeleccion  where FechaExamen>=@fechaInicio and FechaExamen<=@fechaFin";
					}
				}

				var PostulanteDB = _dapper.QueryDapper(query, new { fechaInicio = filtro.FechaInicio,fechaFin = filtro.FechaFin });
				if (!string.IsNullOrEmpty(PostulanteDB) && !PostulanteDB.Contains("[]"))
				{
					postulanteAutocompleteFiltro = JsonConvert.DeserializeObject<List<PostulanteUltimoProcesoSeleccionDTO>>(PostulanteDB);
				}
				return postulanteAutocompleteFiltro;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}


		///Repositorio: PostulanteRepositorio
		///Autor: Edgar S.
		///Fecha: 18/03/2021
		/// <summary>
		/// Obtiene información de Postulantes por Id
		/// </summary>
		/// <param name="idPostulante"> Id de Postulante </param>
		/// <returns> PostulanteInformacionVisualDTO </returns>
		public PostulanteInformacionVisualDTO ObtenerInformacionPostulanteVisual(int idPostulante)
		{
			try
			{
				PostulanteInformacionVisualDTO lista = new PostulanteInformacionVisualDTO();
				var query = "SELECT Id, Nombre, ApellidoPaterno, ApellidoMaterno ,Edad, Celular, Email, Ciudad, UrlPerfilFacebook, UrlPerfilLinkedin, TieneHijo, CantidadHijo FROM [gp].[V_TPostulante_ObtenerInformacionPostulante] WHERE Id = @idPostulante AND Estado = 1";
				var res = _dapper.FirstOrDefault(query, new { idPostulante });

				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<PostulanteInformacionVisualDTO>(res);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		///Repositorio: PostulanteRepositorio
		///Autor: Edgar S.
		///Fecha: 29/03/2021
		/// <summary>
		/// Obtiene el Id,Nombre de los postulantes inscritos AutoComplete
		/// </summary>
		/// <param name="valor"> Valor de Búsqueda por nombre </param>
		/// <returns> List<FiltroDTO> </returns>
		public List<FiltroDTO> ObtenerPersonalComoPostulanteAutocomplete(string valor)
		{
			try
			{
				List<FiltroDTO> postulanteAutocompleteFiltro = new List<FiltroDTO>();
				string queryPostulanteFiltro = string.Empty;
				queryPostulanteFiltro = "SELECT Id, Nombre from gp.V_TPostulante_ObtenerPersonalComoPostulanteParaFiltro WHERE Nombre LIKE CONCAT('%',@valor,'%') AND Estado = 1 ORDER By Nombre ASC";
				var PostulanteDB = _dapper.QueryDapper(queryPostulanteFiltro, new { valor });
				if (!string.IsNullOrEmpty(PostulanteDB) && !PostulanteDB.Contains("[]"))
				{
					postulanteAutocompleteFiltro = JsonConvert.DeserializeObject<List<FiltroDTO>>(PostulanteDB);
				}
				return postulanteAutocompleteFiltro;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Autor: Jashin Salazar
		/// Fecha: 29/01/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene lista de elementos registrados para combo
		/// </summary>
		/// <returns> List<FiltroIdNombreDTO> </returns>
		public List<FiltroIdNombreDTO> ObtenerListaEstadoEstudioParaFiltro()
		{
			try
			{
				var query = "SELECT Id, Nombre FROM gp.T_EstadoEstudio WHERE Estado = 1";
				var resultado = _dapper.QueryDapper(query, null);
				return JsonConvert.DeserializeObject<List<FiltroIdNombreDTO>>(resultado);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Autor: Jashin Salazar
		/// Fecha: 29/01/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene lista de elementos registrados para combo
		/// </summary>
		/// <returns> List<FiltroIdNombreDTO> </returns>
		public List<PostulanteLogDTO> ObtenerHistorialPostulante(int IdPostulante, string Clave)
		{
			try
			{
				List<PostulanteLogDTO> lista = new List<PostulanteLogDTO>();
				string query = "gp.SP_ObtenerHistorialPostulante";
				var res = _dapper.QuerySPDapper(query, new { IdPostulante = IdPostulante, Clave = Clave});
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<PostulanteLogDTO>>(res);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Autor: Jashin Salazar
		/// Fecha: 29/01/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene lista de elementos registrados para combo
		/// </summary>
		/// <returns> List<FiltroIdNombreDTO> </returns>
		public List<ResultadoFinaltextoDTO> ValidarCorreoPostulante(int IdPostulante)
		{
			try
			{
				List<ResultadoFinaltextoDTO> lista = new List<ResultadoFinaltextoDTO>();
				string query = "gp.SP_ValidarCorreoPostulante";
				var res = _dapper.QuerySPDapper(query, new { IdPostulante = IdPostulante});
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<ResultadoFinaltextoDTO>>(res);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Autor: Jashin Salazar
		/// Fecha: 29/11/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene lista de elementos registrados para combo
		/// </summary>
		/// <returns> List<FiltroIdNombreDTO> </returns>
		public List<ComparacionProcesosSeleccionDTO> CompararProcesosSeleccion(int IdPostulante, int ProcesoOrigen, int ProcesoDestino)
		{
			try
			{
				List<ComparacionProcesosSeleccionDTO> lista = new List<ComparacionProcesosSeleccionDTO>();
				string query = "gp.SP_CompararProcesosSeleccion";
				var res = _dapper.QuerySPDapper(query, new { IdPostulante = IdPostulante, IdProcesoSeleccionOrigen = ProcesoOrigen , IdProcesoSeleccionDestino = ProcesoDestino });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<ComparacionProcesosSeleccionDTO>>(res);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Autor: Jashin Salazar
		/// Fecha: 01/12/2021
		/// Version: 1.0
		/// <summary>
		/// Actualiza el cambio de proceso para un postulante
		/// </summary>
		/// <returns> List<FiltroIdNombreDTO> </returns>
		public List<ComparacionProcesosSeleccionDTO> ActualizarProcesoPostulante(PostulanteProcesoNuevoDTO Informacion)
		{
			try
			{
				List<ComparacionProcesosSeleccionDTO> lista = new List<ComparacionProcesosSeleccionDTO>();
				var procesoSeleccionEtapa = string.Join(",", Informacion.IdsProcesoSeleccionEtapa.Select(x => x));
				string query = "gp.SP_CambiarProcesoSeleccionPostulante";
				var res = _dapper.QuerySPDapper(query, new { IdPostulante = Informacion.IdPostulante, IdProcesoSeleccionOrigen = Informacion.IdProcesoSeleccionOrigen, IdProcesoSeleccionDestino = Informacion.IdProcesoSeleccionDestino, IdsProcesoSeleccionEtapa= procesoSeleccionEtapa });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<ComparacionProcesosSeleccionDTO>>(res);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Autor: Jashin Salazar
		/// Fecha: 01/12/2021
		/// Version: 1.0
		/// <summary>
		/// Actualiza el cambio de proceso para un postulante
		/// </summary>
		/// <returns> List<FiltroIdNombreDTO> </returns>
		public List<ComparacionProcesosSeleccionDTO> ActualizarProcesoPostulanteSinNota(PostulanteProcesoNuevoDTO Informacion)
		{
			try
			{
				List<ComparacionProcesosSeleccionDTO> lista = new List<ComparacionProcesosSeleccionDTO>();
				string query = "gp.SP_CambiarProcesoSeleccionPostulanteSinNota";
				var res = _dapper.QuerySPDapper(query, new { IdPostulante = Informacion.IdPostulante, IdProcesoSeleccionDestino = Informacion.IdProcesoSeleccionDestino });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<List<ComparacionProcesosSeleccionDTO>>(res);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// Autor: Jashin Salazar
		/// Fecha: 01/12/2021
		/// Version: 1.0
		/// <summary>
		/// Actualiza el cambio de proceso para un postulante
		/// </summary>
		/// <returns> List<FiltroIdNombreDTO> </returns>
		public ResultadoFinaltextoV2DTO ObtenerEtapaProceso(int id)
		{
			try
			{
				ResultadoFinaltextoV2DTO lista = new ResultadoFinaltextoV2DTO();
				string query = "SELECT Nombre FROM gp.T_EstadoProcesoSeleccion WHERE Id=@Id";
				var res = _dapper.FirstOrDefault(query, new { Id = id });
				if (!string.IsNullOrEmpty(res) && !res.Contains("[]"))
				{
					lista = JsonConvert.DeserializeObject<ResultadoFinaltextoV2DTO>(res);
				}
				return lista;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
