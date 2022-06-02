using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
	/// Repositorio: CentroCostoRepositorio
	/// Autor: Edgar S.
	/// Fecha: 08/02/2021
	/// <summary>
	/// Gestión de Centro de Costo
	/// </summary>
	public class CentroCostoRepositorio : BaseRepository<TCentroCosto, CentroCostoBO>
	{
		#region Metodos Base
		public CentroCostoRepositorio() : base()
		{
		}
		public CentroCostoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<CentroCostoBO> GetBy(Expression<Func<TCentroCosto, bool>> filter)
		{
			IEnumerable<TCentroCosto> listado = base.GetBy(filter);
			List<CentroCostoBO> listadoBO = new List<CentroCostoBO>();
			foreach (var itemEntidad in listado)
			{
				CentroCostoBO objetoBO = Mapper.Map<TCentroCosto, CentroCostoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public CentroCostoBO FirstById(int id)
		{
			try
			{
				TCentroCosto entidad = base.FirstById(id);
				//CentroCostoBO objetoBO = new CentroCostoBO();
				CentroCostoBO objetoBO=Mapper.Map<TCentroCosto, CentroCostoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public CentroCostoBO FirstBy(Expression<Func<TCentroCosto, bool>> filter)
		{
			try
			{
				TCentroCosto entidad = base.FirstBy(filter);
				//CentroCostoBO objetoBO = new CentroCostoBO();
				CentroCostoBO objetoBO=Mapper.Map<TCentroCosto, CentroCostoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(CentroCostoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TCentroCosto entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<CentroCostoBO> listadoBO)
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

		public bool Update(CentroCostoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TCentroCosto entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<CentroCostoBO> listadoBO)
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
		private void AsignacionId(TCentroCosto entidad, CentroCostoBO objetoBO)
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

		private TCentroCosto MapeoEntidad(CentroCostoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TCentroCosto entidad = new TCentroCosto();
				entidad = Mapper.Map<CentroCostoBO, TCentroCosto>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				if (objetoBO.ProgramaEspecifico != null)
				{
					TPespecifico entidadHijo = new TPespecifico();
					entidadHijo = Mapper.Map<PespecificoBO, TPespecifico>(objetoBO.ProgramaEspecifico,
						opt => opt.ConfigureMap(MemberList.None));
					entidad.TPespecifico.Add(entidadHijo);

					//mapea al hijo interno
					if (objetoBO.ProgramaEspecifico.CursoPespecifico != null)
					{
						TCursoPespecifico entidadHijo2 = new TCursoPespecifico();
						entidadHijo2 = Mapper.Map<CursoPespecificoBO, TCursoPespecifico>(objetoBO.ProgramaEspecifico.CursoPespecifico,
							opt => opt.ConfigureMap(MemberList.None));
						entidadHijo.TCursoPespecifico.Add(entidadHijo2);
					}
				}

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		#endregion

		/// <summary>
		/// Obtiene lista de centro de costos para tabla
		/// </summary>
		/// <param name="filtro"></param>
		/// <returns></returns>
		public List<CentroCostoCompuestoDTO> ObtenerCentroCostoParaTabla(FiltroCentroCostoDTO filtro)
		{
			try
			{
				string _queryCentroCosto = "pla.SP_TCentroCosto_ContieneNombre";
				var queryCentroCosto = _dapper.QuerySPDapper(_queryCentroCosto, filtro);
				return JsonConvert.DeserializeObject<List<CentroCostoCompuestoDTO>>(queryCentroCosto);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message); 
			}
		}

		/// Repositorio: CentroCostoRepositorio
		/// Autor: ,Edgar S.
		/// Fecha: 22/02/2021
		/// Version: 1.1
		/// <summary>
		/// Obtener Centro Costo Para Filtro    
		/// </summary>
		/// <param></param>
		/// <returns>Objeto(Lista):List<FiltroDTO></returns>		
		public List<FiltroDTO> ObtenerCentroCostoParaFiltro()
		{
			try
			{ 
				//SELECT Id, Nombre FROM pla.V_TPGeneral_Nombre WHERE Estado = 1				
				string queryCentroCosto = "SELECT Id, Nombre FROM pla.V_TCentroCosto_ParaFiltro WHERE Estado = 1";
				var reultadoCentroCosto = _dapper.QueryDapper(queryCentroCosto, null );
				return JsonConvert.DeserializeObject<List<FiltroDTO>>(reultadoCentroCosto);                
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
			
		}
		/// <summary>
		/// Obtiene lista de centro de costos para filtro de formulario
		/// </summary>
		/// <returns>Lista de objetos de clase FiltroDTO</returns>
		public List<FiltroDTO> ObtenerParaFiltro()
		{
			try
			{
				string _queryCentroCosto = "SELECT Id, Nombre FROM pla.V_ObtenerCentroCostoParaFiltro WHERE Estado = 1";
				var queryCentroCosto = _dapper.QueryDapper(_queryCentroCosto, null);
				return JsonConvert.DeserializeObject<List<FiltroDTO>>(queryCentroCosto);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

		}

		///Repositorio: CentroCostoRepositorio
		///Autor: Edgar S.
		///Fecha: 08/02/2021
		/// <summary>
		/// Obtener información de centro de Costo AutoComplete
		/// </summary>
		/// <param name="valor"> valores de búsqueda </param>
		/// <returns> Lista de registros de Centros de Costo Registrados </returns>
		/// <returns> Objeto DTO: List<CentroCostoFiltroAutocompleteDTO> </returns>		
		public List<CentroCostoFiltroAutocompleteDTO> ObtenerTodoFiltroAutoComplete(string valor)
		{
			try
			{
				List<CentroCostoFiltroAutocompleteDTO> centrosCostoAutocompleteFiltro = new List<CentroCostoFiltroAutocompleteDTO>();
				string queryCentroCostoFiltro = string.Empty;
				queryCentroCostoFiltro = "SELECT Id, Nombre from pla.V_TCentroCosto_ParaFiltro WHERE Nombre LIKE CONCAT('%',@valor,'%') AND Estado = 1 ORDER By Nombre ASC";
				var CentroCostoDB = _dapper.QueryDapper(queryCentroCostoFiltro, new { valor });
				if (!string.IsNullOrEmpty(CentroCostoDB) && !CentroCostoDB.Contains("[]"))
				{
					centrosCostoAutocompleteFiltro = JsonConvert.DeserializeObject<List<CentroCostoFiltroAutocompleteDTO>>(CentroCostoDB);
				}
				return centrosCostoAutocompleteFiltro;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		///Repositorio: CentroCostoRepositorio
		///Autor: Edgar S.
		///Fecha: 08/02/2021
		/// <summary>
		/// Obtener información de centro de Costo AutoComplete
		/// </summary>
		/// <param name="valor"> valores de búsqueda </param>
		/// <returns> Lista de registros de Centros de Costo Registrados </returns>
		/// <returns> Objeto DTO: List<CentroCostoFiltroAutocompleteDTO> </returns>		
		public List<CentroCostoFiltroAutocompleteDTO> ObtenerTodoFiltroAutoCompleteV2(string valor)
		{
			try
			{
				List<CentroCostoFiltroAutocompleteDTO> centrosCostoAutocompleteFiltro = new List<CentroCostoFiltroAutocompleteDTO>();
				string queryCentroCostoFiltro = string.Empty;
				queryCentroCostoFiltro = "SELECT Id, Nombre from pla.V_TCentroCosto_ParaFiltroV2 WHERE Nombre LIKE CONCAT('%',@valor,'%') AND Estado = 1 ORDER By Nombre ASC";
				var CentroCostoDB = _dapper.QueryDapper(queryCentroCostoFiltro, new { valor });
				if (!string.IsNullOrEmpty(CentroCostoDB) && !CentroCostoDB.Contains("[]"))
				{
					centrosCostoAutocompleteFiltro = JsonConvert.DeserializeObject<List<CentroCostoFiltroAutocompleteDTO>>(CentroCostoDB);
				}
				return centrosCostoAutocompleteFiltro;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene todos los centro de costo por area y subarea
		/// </summary>
		/// <param name="listaArea"></param>
		/// <param name="listaSubArea"></param>
		/// <returns></returns>
		public List<FiltroDTO> ObtenerCentroCostoPorAreaSubArea(int[] listaArea, int[] listaSubArea) {
			try
			{
				List<FiltroDTO> centroCosto = new List<FiltroDTO>();
				var _query = "SELECT Id, Nombre FROM pla.V_ObtenerCentroCostoPorAreaSubArea WHERE IdAreaCapacitacion IN @listaArea AND IdSubAreaCapacitacion IN @listaSubArea";
				var CentroCostoDB = _dapper.QueryDapper(_query, new { listaArea, listaSubArea });
				if (!string.IsNullOrEmpty(CentroCostoDB) && !CentroCostoDB.Contains("[]"))
				{
					centroCosto = JsonConvert.DeserializeObject<List<FiltroDTO>>(CentroCostoDB);
				}
				return centroCosto;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene el Id, Nombre de los centros de costo por parametro Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public CentroCostoFiltroAutocompleteDTO ObtenerCentroCostoNombrePorId(int id)
		{
			try
			{
				string _query = "SELECT Id, Nombre FROM pla.V_TCentroCosto_ParaFiltro WHERE Id = @id and Estado = 1";
				var CentroCosto = _dapper.FirstOrDefault(_query, new { id });
				return JsonConvert.DeserializeObject<CentroCostoFiltroAutocompleteDTO>(CentroCosto);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
		}

		/// <summary>
		/// Retorna datos de centro de costo para programa especifico
		/// </summary>
		/// <param name="Codigo"></param>
		/// <param name="Condicion"></param>
		/// <param name="Anio"></param>
		/// <param name="NombreCiudad"></param>
		/// <returns></returns>
		public List<CentroCostoDTO> ObtenerCentroCostoParaPEspecifico(string codigo, string condicion, string anio, string nombreCiudad)
		{
			try
			{
				var query = "Select Id,	IdArea, IdSubArea, IdPgeneral, Nombre, Codigo, IdAreaCc, Ismtotales, Icpftotales From pla.V_TCentroCosto_ObtenerDatos Where Estado=1 and Nombre Like''+@codigo+'%' and " + condicion + "and Nombre Like '%'+@anio+'%' and Nombre Like'%'+@nombreCiudad+'%'";
				var CentroCosto = _dapper.QueryDapper(query, new { codigo, anio, nombreCiudad });
				return JsonConvert.DeserializeObject<List<CentroCostoDTO>>(CentroCosto);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene Valor para etiquetas po centro costo
		/// </summary>
		/// <param name="idCentroCosto"></param>
		/// <returns></returns>
		public PlantillaCentroCostoDTO ObtenerCentroCostoParaPEspecifico(int idCentroCosto)
		{
			try
			{
				var query = "Select IdCentroCosto,	NombrePartner, NombrePEspecifico from pla.T_CentroCosto_OptenerPlantillaPatner Where IdCentroCosto=@IdCentroCosto";
				var CentroCosto = _dapper.FirstOrDefault(query, new { IdCentroCosto=idCentroCosto});
				return JsonConvert.DeserializeObject<PlantillaCentroCostoDTO>(CentroCosto);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene Valor para etiquetas por programa general
		/// </summary>
		/// <param name="IdPGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
		/// <returns>Objeto de clase PlantillaCentroCostoDTO</returns>
		public PlantillaCentroCostoDTO ObtenerRemplazoPlantilla(int IdPgeneral)
		{
			try
			{
				var query = "SELECT NombrePartner, NombrePgeneral FROM pla.V_Pgeneral_OptenerPlantillaWhatsapp Where IdPgeneral = @IdPgeneral";
				var CentroCosto = _dapper.FirstOrDefault(query, new { IdPgeneral });
				return JsonConvert.DeserializeObject<PlantillaCentroCostoDTO>(CentroCosto);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		
		/// <summary>
		/// Obtiene Valor para etiquetas po centro costo
		/// </summary>
		/// <param name="idCentroCosto"></param>
		/// <returns></returns>
		public PlantillaCentroCostoDTO ObtenerCentroCostoParaPlantillaWhatsApp(int idCentroCosto)
		{
			try
			{
				var query = "Select IdCentroCosto,	NombrePartner, NombrePEspecifico,NombrePgeneral from pla.T_CentroCosto_OptenerPlantillaWhatsapp Where IdCentroCosto=@IdCentroCosto";
				var CentroCosto = _dapper.FirstOrDefault(query, new { IdCentroCosto=idCentroCosto});
				return JsonConvert.DeserializeObject<PlantillaCentroCostoDTO>(CentroCosto);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Repositorio: CentroCostoRepositorio
		/// Autor: Edgar S.
		/// Fecha: 22/02/2021
		/// Version: 1.1
		/// <summary>
		/// Obtiene lista de centro de costos para filtro por Asesores
		/// </summary>
		/// <returns>Lista de objeto DTO: List(FiltroDTO)</returns>
		public List<FiltroDTO> ObtenerCentroCostoPorAsesores(List<int> listaAsesores)
		{
			try
			{
				List<FiltroDTO> centroCosto = new List<FiltroDTO>();
				string _query = "SELECT Id,Nombre FROM pla.V_ObtenerCentroCostoPorOportunidades WHERE IdPersonal IN @listaAsesores AND id IS NOT NULL AND Estado=1";
				var respuestaQuery = _dapper.QueryDapper(_query, new { listaAsesores = listaAsesores });
				if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Contains("[]"))
				{ 
					centroCosto = JsonConvert.DeserializeObject<List<FiltroDTO>>(respuestaQuery);
				}
				return centroCosto;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}

		}

		public string ObtenerUltimoCentroCostoPorCodigo(string codigo)
	{
		try
		{
			string _queryCentroCostoUltimo = "Select Codigo from pla.V_TCentroCosto_ObtenerDatos where Estado=1 and Codigo like''+@Codigo+'%' Order by Codigo desc";
			var queryCentroCostoUltimo = _dapper.FirstOrDefault(_queryCentroCostoUltimo, new { Codigo = codigo });
			return JsonConvert.DeserializeObject<CentroCostoDTO>(queryCentroCostoUltimo).Codigo;
		}
		catch (Exception e)
		{
			throw new Exception(e.Message);
		}
	}
		/// <summary>
		/// Obtiene datos del centro de costo por IdProgramaEspecifico
		/// </summary>
		/// <param name="idPEspecifico"></param>
		/// <returns></returns>
		public List<DatosCentroCostoDTO> ObtenerDatosCentroCostos(int idPEspecifico)
		{
			try
			{
				List<DatosCentroCostoDTO> datosCentroCostos = new List<DatosCentroCostoDTO>();
				var _query = "SELECT CodigoBanco, CentroCosto, Tipo, Categoria FROM pla.V_DatosCentroCostoPorIdPEspecifico WHERE IdPEspecifico = @idPEspecifico AND EstadoProgramaEspecifico = 1 AND EstadoCentroCosto = 1";
				var datosCentroCostoDB = _dapper.QueryDapper(_query, new { idPEspecifico});
				if (!string.IsNullOrEmpty(datosCentroCostoDB) && !datosCentroCostoDB.Contains("[]"))
				{
					datosCentroCostos = JsonConvert.DeserializeObject<List<DatosCentroCostoDTO>>(datosCentroCostoDB);
				}
				return datosCentroCostos;                
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene un listado de centro costo filtrado por nombre
		/// </summary>
		/// <param name="nombreCentroCosto"></param>
		/// <returns></returns>
		public List<CentroCostoPEspecificoDTO> ObtenerDatosCentroCostoPorNombre(string nombreCentroCosto)
		{
			try
			{
				List<CentroCostoPEspecificoDTO> centroCosto = new List<CentroCostoPEspecificoDTO>();
				var _query = "SELECT IdCentroCosto, NombreCentroCosto, IdPEspecifico, NombrePEspecifico FROM ope.V_ObtenerCentroCostoPorNombre WHERE NombreCentroCosto LIKE CONCAT('%',@nombreCentroCosto,'%')";
				var centroCostoDB = _dapper.QueryDapper(_query, new { nombreCentroCosto });
				if (!string.IsNullOrEmpty(centroCostoDB) && !centroCostoDB.Contains("[]"))
				{
					centroCosto = JsonConvert.DeserializeObject<List<CentroCostoPEspecificoDTO>>(centroCostoDB);
				}
				return centroCosto;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Se obtiene los registros de centro de costo filtrado por el Nombre del centro de costo
		/// </summary>
		/// <param name="filtro"></param>
		/// <returns></returns>
		public List<CentroCostoDTO> ObtenerTodoCentroCostoFiltro(string filtro)
		{
			try
			{
				List<CentroCostoDTO> centroCosto = new List<CentroCostoDTO>();
				var _query = "SELECT Id, IdArea, IdSubArea, IdPGeneral, Nombre, Codigo, IdAreaCC, ISMTotales, ICPFTotales  FROM pla.V_ObtenerTodoCentroCostoFiltro  WHERE Nombre LIKE CONCAT('%',@filtro,'%') AND Estado = 1";
				var centroCostoDB = _dapper.QueryDapper(_query, new { filtro });
				if (!string.IsNullOrEmpty(centroCostoDB) && !centroCostoDB.Contains("[]"))
				{
					centroCosto = JsonConvert.DeserializeObject<List<CentroCostoDTO>>(centroCostoDB);
				}
				return centroCosto;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene una lista de CentroCosto dado un NombreParcial con Estado=1
		/// </summary>
		/// <param name="nombreCentroCosto"></param>
		/// <returns></returns>
		public List<CentroCostoPEspecificoDTO> ObtenerListaCentrosCostoPorNombre(string nombreCentroCosto)
		{
			try
			{
				List<CentroCostoPEspecificoDTO> centroCosto = new List<CentroCostoPEspecificoDTO>();
				var _query = "SELECT Id as IdCentroCosto, Nombre as NombreCentroCosto FROM pla.T_CentroCosto WHERE Estado=1 AND Nombre LIKE '%"+nombreCentroCosto+"%'";
				var centroCostoDB = _dapper.QueryDapper(_query, new { nombreCentroCosto });
				if (!string.IsNullOrEmpty(centroCostoDB) && !centroCostoDB.Contains("[]"))
				{
					centroCosto = JsonConvert.DeserializeObject<List<CentroCostoPEspecificoDTO>>(centroCostoDB);
				}
				return centroCosto;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene el Id,Nombre de los centros de costo por un parametro y con antiguedad maximo de un año
		/// </summary>
		/// <param name="valor"></param>
		/// <returns></returns>
		public List<CentroCostoFiltroAutocompleteDTO> ObtenerTodoFiltroAutoCompletePorFecha(string valor)
		{
			try
			{
				List<CentroCostoFiltroAutocompleteDTO> centrosCostoAutocompleteFiltro = new List<CentroCostoFiltroAutocompleteDTO>();
				string _queryCentroCostoFiltro = string.Empty;
				_queryCentroCostoFiltro = "SELECT Id, Nombre from pla.V_TCentroCosto_ParaFiltroPorFecha WHERE Nombre LIKE CONCAT('%',@valor,'%') AND Estado = 1 AND FechaCreacion > @FechaCreacion ORDER By Nombre ASC";
				DateTime fechaActual = (DateTime.Now.AddYears(-1));
				var CentroCostoDB = _dapper.QueryDapper(_queryCentroCostoFiltro, new { valor, @FechaCreacion= fechaActual });
				if (!string.IsNullOrEmpty(CentroCostoDB) && !CentroCostoDB.Contains("[]"))
				{
					centrosCostoAutocompleteFiltro = JsonConvert.DeserializeObject<List<CentroCostoFiltroAutocompleteDTO>>(CentroCostoDB);
				}
				return centrosCostoAutocompleteFiltro;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene todos los centros de costo creados hace un año.
		/// </summary>
		/// <returns>Lista de objetos de clase CentroCostoFiltroAutocompleteDTO</returns>
		public List<CentroCostoFiltroAutocompleteDTO> ObtenerTodoFiltroAntiguedadUnAño()
        {
            try
            {
                List<CentroCostoFiltroAutocompleteDTO> centrosCostoAutocompleteFiltro = new List<CentroCostoFiltroAutocompleteDTO>();
                string queryCentroCostoFiltro = string.Empty;
                queryCentroCostoFiltro = "SELECT Id, Nombre from pla.V_TCentroCosto_ParaFiltroPorFecha WHERE Estado = 1 AND FechaCreacion > @FechaCreacion ORDER By Nombre ASC";
                DateTime fechaActual = (DateTime.Now.AddYears(-2));
                var CentroCostoDB = _dapper.QueryDapper(queryCentroCostoFiltro, new { @FechaCreacion = fechaActual });
                if (!string.IsNullOrEmpty(CentroCostoDB) && !CentroCostoDB.Contains("[]"))
                {
                    centrosCostoAutocompleteFiltro = JsonConvert.DeserializeObject<List<CentroCostoFiltroAutocompleteDTO>>(CentroCostoDB);
                }
                return centrosCostoAutocompleteFiltro;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		/// <summary>
		/// Se obtienen los centros de costos que no esten asociados a un GrupoFiltroProgramaCritico
		/// </summary>
		/// <param name="idGrupo">Id del grupo de programa critico (PK de la tabla pla.T_GrupoFiltroProgramaCritico)</param>
		/// <returns>Lista de objetos de clase CentroCostoSubAreaDTO</returns>
		public List<CentroCostoSubAreaDTO> ObtenerCentroCostoPorSubArea(int idGrupo)
		{
			try
			{
				List<CentroCostoSubAreaDTO> listaCentroCosto = new List<CentroCostoSubAreaDTO>();
				var registrosBO = _dapper.QuerySPDapper("com.SP_ObtenerCentroCostoPorSubArea", new { idGrupo });
				if (!string.IsNullOrEmpty(registrosBO) && !registrosBO.Contains("[]"))
				{
					listaCentroCosto = JsonConvert.DeserializeObject<List<CentroCostoSubAreaDTO>>(registrosBO);
				}
				return listaCentroCosto;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		//public List<FiltroDTO> ObtenerCentroCostoFiltro()
		//{
		//	try
		//	{
		//		GetBy(x => x.Estado == true && x.FechaCreacion.Year >= DateTime.Now.Year, x=> new FiltroDTO { Id = x.Id, Nombre = x.Nombre });
		//	}
		//	catch (Exception e)
		//	{
		//		throw new Exception(e.Message);
		//	}
		//}

		/// <summary>
		/// Obtiene Lista de Programas Especificos para filtro para un combobox(PEspecifico) que depende 
		/// de otro combobox(PGeneral)
		/// </summary>
		/// <returns></returns>
		public List<DatosListaPespecificoDePgeneralDTO> ObtenerListaCentroCostoParaFiltroDeProgramaGeneral()
		{
			try
			{
				var query = "SELECT Id, Nombre, IdProgramaGeneral FROM pla.V_ListaCentroCostoParaTabla where Estado=1  ORDER BY Id DESC";
				var PEspecifico = _dapper.QueryDapper(query, null);
				return JsonConvert.DeserializeObject<List<DatosListaPespecificoDePgeneralDTO>>(PEspecifico);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}


		/// Autor: Jose Villena
		/// Fecha: 28/05/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene centro de costo padre y centro de costo individual
		/// </summary>
		/// <param></param>
		/// <returns>List<CentroCostoPadreCentroCostoIndividualDTO></returns>
		public List<CentroCostoPadreCentroCostoIndividualDTO> ObtenerCentroCostoPadreCentroCostoIndividual()
		{
			try
			{
				var query = "SELECT IdCentroCosto, IdProgramaEspecifico, CentroCosto, ProgramaEspecifico, EstadoProgramaEspecifico, Tipo FROM [pla].[V_TPEspecifico_ObtenerPEspecificoIndividualPadre] WHERE Estado = 1";
				var res = _dapper.QueryDapper(query, null);
				return JsonConvert.DeserializeObject<List<CentroCostoPadreCentroCostoIndividualDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}	

        public List<CentroCostoPartnerDTO> ObtenerTodoCentroCostoPartner()
        {
            try
            {
                var query = "SELECT Id, IdCentroCosto, NombreCentroCosto,IdTroncalPartner,NombreTroncalPartner FROM pla.V_CentroCostoPartner where Estado=1  ORDER BY Id DESC";
                var PEspecifico = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<CentroCostoPartnerDTO>>(PEspecifico);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

		/// <summary>
		/// Obtiene lista de centro de costos padre
		/// </summary>
		/// <returns></returns>
		public List<CentroCostoProgramaEspecificoFiltroDTO> ObtenerCentroCostoPadres(int? tipo)
		{
			try
			{
				var query = "";
				if (tipo.HasValue)
				{
					query = $@"
					SELECT DISTINCT 
						   IdCentroCosto AS Id, 
						   CentroCosto AS Nombre, 
						   IdPEspecifico
					FROM pla.V_TPEspecifico_ObtenerProgramasParaFiltro
					WHERE Estado = 1
						  AND RowNumber = 1
						  AND Tipo = 1;
					";
				}
				else
				{
					query = $@"
					SELECT DISTINCT 
						   IdCentroCosto AS Id, 
						   CentroCosto AS Nombre, 
						   IdPEspecifico
					FROM pla.V_TPEspecifico_ObtenerProgramasParaFiltro
					WHERE Estado = 1
						  AND RowNumber = 1
					";
				}
				
				var res = _dapper.QueryDapper(query, null);
				return JsonConvert.DeserializeObject<List<CentroCostoProgramaEspecificoFiltroDTO>>(res);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Se obtiene para filtro
		/// </summary>
		/// <returns></returns>
		public List<FiltroDTO> ObtenerTodoFiltro()
		{
			try
			{
				return this.GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		/// Autor: Fischer V
		/// Fecha: 22/03/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene CentroCosto dado un nombre
		/// </summary>
		/// <param name="nombreCentroCosto">Nombre del Centro de costo</param>
		/// <returns>Retorna Objeto CentroCostoPEspecificoDTO con Informacion de Centro de costo</returns>
		public CentroCostoPEspecificoDTO ObtenerCentrosCostoPorNombre(string nombreCentroCosto)
		{
			try
			{
				CentroCostoPEspecificoDTO centroCosto = new CentroCostoPEspecificoDTO();
				var query = "SELECT Id as IdCentroCosto, Nombre as NombreCentroCosto FROM pla.T_CentroCosto WHERE Estado=1 AND Nombre = @nombreCentroCosto";
				var centroCostoDB = _dapper.FirstOrDefault(query, new { nombreCentroCosto });
				centroCosto = JsonConvert.DeserializeObject<CentroCostoPEspecificoDTO>(centroCostoDB);
				
				return centroCosto;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// <summary>
		/// Obtiene la lista de Id de centro de costo excluido
		/// </summary>
		/// <param name="tipoExclusion">Entero con el tipo de exclusion Hardcodeado</param>
		/// <returns>Lista de objetos de tipo ValorIntDTO</returns>
		public List<ValorIntDTO> ObtenerCentroCostoExcluidoEnvioAutomatico(int tipoExclusion)
        {
			/*
			 * tipoExclusion: 1: ProgramasGenerales
			 */
			try
			{
				List<ValorIntDTO> listaCentroCosto = new List<ValorIntDTO>();
				var registrosBO = _dapper.QuerySPDapper("pla.SP_ObtenerCentroCostoExcluidoEnvioAutomatico", new { TipoExclusion = tipoExclusion });
				if (!string.IsNullOrEmpty(registrosBO) && !registrosBO.Contains("[]"))
				{
					listaCentroCosto = JsonConvert.DeserializeObject<List<ValorIntDTO>>(registrosBO);
				}
				return listaCentroCosto;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}
