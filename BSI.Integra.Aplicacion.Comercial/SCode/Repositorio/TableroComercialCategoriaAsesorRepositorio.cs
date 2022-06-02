using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Comercial.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;

namespace BSI.Integra.Aplicacion.Comercial.Repositorio
{
	public class TableroComercialCategoriaAsesorRepositorio : BaseRepository<TTableroComercialCategoriaAsesor, TableroComercialCategoriaAsesorBO>
	{
		#region Metodos Base
		public TableroComercialCategoriaAsesorRepositorio() : base()
		{
		}
		public TableroComercialCategoriaAsesorRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<TableroComercialCategoriaAsesorBO> GetBy(Expression<Func<TTableroComercialCategoriaAsesor, bool>> filter)
		{
			IEnumerable<TTableroComercialCategoriaAsesor> listado = base.GetBy(filter);
			List<TableroComercialCategoriaAsesorBO> listadoBO = new List<TableroComercialCategoriaAsesorBO>();
			foreach (var itemEntidad in listado)
			{
				TableroComercialCategoriaAsesorBO objetoBO = Mapper.Map<TTableroComercialCategoriaAsesor, TableroComercialCategoriaAsesorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public TableroComercialCategoriaAsesorBO FirstById(int id)
		{
			try
			{
				TTableroComercialCategoriaAsesor entidad = base.FirstById(id);
				//TableroComercialCategoriaAsesorBO objetoBO = new TableroComercialCategoriaAsesorBO();
				TableroComercialCategoriaAsesorBO objetoBO = Mapper.Map<TTableroComercialCategoriaAsesor, TableroComercialCategoriaAsesorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public TableroComercialCategoriaAsesorBO FirstBy(Expression<Func<TTableroComercialCategoriaAsesor, bool>> filter)
		{
			try
			{
				TTableroComercialCategoriaAsesor entidad = base.FirstBy(filter);
				//TableroComercialCategoriaAsesorBO objetoBO = new TableroComercialCategoriaAsesorBO();
				TableroComercialCategoriaAsesorBO objetoBO = Mapper.Map<TTableroComercialCategoriaAsesor, TableroComercialCategoriaAsesorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(TableroComercialCategoriaAsesorBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TTableroComercialCategoriaAsesor entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<TableroComercialCategoriaAsesorBO> listadoBO)
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

		public bool Update(TableroComercialCategoriaAsesorBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TTableroComercialCategoriaAsesor entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<TableroComercialCategoriaAsesorBO> listadoBO)
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
		private void AsignacionId(TTableroComercialCategoriaAsesor entidad, TableroComercialCategoriaAsesorBO objetoBO)
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

		private TTableroComercialCategoriaAsesor MapeoEntidad(TableroComercialCategoriaAsesorBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TTableroComercialCategoriaAsesor entidad = new TTableroComercialCategoriaAsesor();
				entidad = Mapper.Map<TableroComercialCategoriaAsesorBO, TTableroComercialCategoriaAsesor>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		#endregion

		/// Repositorio: TableroComercialCategoriaAsesorRepositorio
		/// Autor: Jashin Salazar
		/// Fecha: 31/05/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene la lista de datos para (M)Categoria de asesores
		/// </summary>
		/// <returns>List<MonedaCodigoFiltroDTO></returns>
		public List<TableroComercialCategoriaAsesorCompuestoDTO> ObtenerCategoriaAsesorParaTabla()
		{
			try
			{
				string queryCategoriaAsesor = "SELECT Id, Nombre, MontoVenta, IdMonedaVenta, CodigoMonedaVenta, IdVisualizacionMonedaVenta, VisualizacionMonedaVenta, MontoPremio, IdMonedaPremio, CodigoMonedaPremio, VisualizarMonedaLocal FROM com.V_TTableroComercialCategoriaAsesor_DatosTablero";
				var respuestaCategoriaAsesor = _dapper.QueryDapper(queryCategoriaAsesor, new { });
				return JsonConvert.DeserializeObject<List<TableroComercialCategoriaAsesorCompuestoDTO>>(respuestaCategoriaAsesor);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// Repositorio: TableroComercialCategoriaAsesorRepositorio
		/// Autor: Jashin Salazar
		/// Fecha: 02/06/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene datos para comboBox para (O) Ficha de Datos del Personal
		/// </summary>
		/// <returns>List<MonedaCodigoFiltroDTO></returns>
		public List<TableroComercialCategoriaAsesorCompuestoDTO> ObtenerComboBoxCategoriaAsesor()
		{
			try
			{
				string queryCategoriaAsesor = "SELECT Id, Nombre FROM com.T_TableroComercialCategoriaAsesor WHERE Estado=1";
				var respuestaCategoriaAsesor = _dapper.QueryDapper(queryCategoriaAsesor, new { });
				return JsonConvert.DeserializeObject<List<TableroComercialCategoriaAsesorCompuestoDTO>>(respuestaCategoriaAsesor);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// Repositorio: TableroComercialCategoriaAsesorRepositorio
		/// Autor: Jashin Salazar
		/// Fecha: 02/06/2021
		/// Version: 1.0
		/// <summary>
		/// Obtiene datos de categoria para el leaderboard
		/// </summary>
		/// <returns>List<MonedaCodigoFiltroDTO></returns>
		public List<TableroComercialCategoriaAsesorCompuestoDTO> ObtenerListaCategorias()
		{
			try
			{
				string queryCategoriaAsesor = "SELECT Nombre, MontoVenta,MontoPremio,VisualizarMonedaLocal,IdMoneda_Premio AS IdMonedaPremio,IdMoneda_Venta AS IdMonedaVenta FROM com.T_TableroComercialCategoriaAsesor WHERE Estado=1 ORDER BY MontoPremio DESC";
				var respuestaCategoriaAsesor = _dapper.QueryDapper(queryCategoriaAsesor, new { });
				return JsonConvert.DeserializeObject<List<TableroComercialCategoriaAsesorCompuestoDTO>>(respuestaCategoriaAsesor);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}


		/// Repositorio: TableroComercialCategoriaAsesorRepositorio
		/// Autor: Jashin Salazar.
		/// Fecha: 04/06/2021
		/// <summary>
		/// Se obtiene Asesor para leaderboard
		/// </summary>
		/// <returns> PersonalTableroComercialDTO </returns>
		public AsesorTableroComercialDTO ObtenerAsesorLeaderBoard(int idPersonal)
		{
			try
			{
				AsesorTableroComercialDTO personal = new AsesorTableroComercialDTO();
				var query = @"
					SELECT Id, 
						   Nombre, 
						   Apellido, 
                           Categoria
					FROM [gp].[V_TPersonal_TableroComercial]
					WHERE Id = @Id";
				var res = _dapper.FirstOrDefault(query, new { Id = idPersonal });
				if (!string.IsNullOrEmpty(res))
				{
					personal = JsonConvert.DeserializeObject<AsesorTableroComercialDTO>(res);
				}
				return personal;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// Repositorio: TableroComercialCategoriaAsesorRepositorio
		/// Autor: Jashin Salazar.
		/// Fecha: 08/06/2021
		/// <summary>
		/// Se obtiene ventas por asesor para leaderboard
		/// </summary>
		/// <returns> List<VentaAsesorTableroComercialDTO> </returns>
		public List<VentaAsesorTableroComercialDTO> ObtenerVentasPorAsesor(int idPersonal)
		{
			try
			{
				DateTime fechaActual = DateTime.Today;
				int prevMonth = fechaActual.AddMonths(-1).Month;
				int year = fechaActual.AddMonths(-1).Year;
				int daysInPrevMonth = DateTime.DaysInMonth(year, prevMonth);
				DateTime mesPasado = new DateTime(year, prevMonth, 1);
				List<VentaAsesorTableroComercialDTO> personal = new List<VentaAsesorTableroComercialDTO>();
				string query = "com.SP_ObtenerVentasMensuales";
				string respuestaQuery = _dapper.QuerySPDapper(query, new { fecha = mesPasado, esHistorico = 1, numeroMeses = 3, asesores = idPersonal });
				personal = JsonConvert.DeserializeObject<List<VentaAsesorTableroComercialDTO>>(respuestaQuery);
				//if (!string.IsNullOrEmpty(res))
				//{
				//    personal = JsonConvert.DeserializeObject<VentaAsesorTableroComercialDTO>(res);
				//}
				return personal;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// Repositorio: TableroComercialCategoriaAsesorRepositorio
		/// Autor: Jashin Salazar.
		/// Fecha: 08/06/2021
		/// <summary>
		/// Se obtiene sueldo por asesor para leaderboard
		/// </summary>
		/// <returns> List<SueldoAsesorTableroComercialDTO>  </returns>
		public List<SueldoAsesorTableroComercialDTO> ObtenerSueldoPorAsesor(int idPersonal)
		{
			try
			{
				List<SueldoAsesorTableroComercialDTO> personal = new List<SueldoAsesorTableroComercialDTO>();
				string query = "com.SP_ObtenerSueldoPersonal";
				string respuestaQuery = _dapper.QuerySPDapper(query, new { asesor = idPersonal });

				if (!string.IsNullOrEmpty(respuestaQuery))
				{
					personal = JsonConvert.DeserializeObject<List<SueldoAsesorTableroComercialDTO>>(respuestaQuery);
				}
				return personal;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// Repositorio: TableroComercialCategoriaAsesorRepositorio
		/// Autor: Jashin Salazar.
		/// Fecha: 08/06/2021
		/// <summary>
		/// Se obtiene sueldo por asesor para leaderboard
		/// </summary>
		/// <returns> List<SueldoAsesorTableroComercialDTO>  </returns>
		public List<SueldoAsesorTableroComercialDTO> ObtenerBonoPorAsesor(int idPersonal, float? venta)
		{
			try
			{
				List<SueldoAsesorTableroComercialDTO> personal = new List<SueldoAsesorTableroComercialDTO>();
				string query = "com.SP_ObtenerBonoPersonal";
				string respuestaQuery = _dapper.QuerySPDapper(query, new { asesor = idPersonal, montoVenta = venta });

				if (!string.IsNullOrEmpty(respuestaQuery))
				{
					personal = JsonConvert.DeserializeObject<List<SueldoAsesorTableroComercialDTO>>(respuestaQuery);
				}
				return personal;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// Repositorio: TableroComercialCategoriaAsesorRepositorio
		/// Autor: Jashin Salazar.
		/// Fecha: 08/06/2021
		/// <summary>
		/// Se obtiene ventas semanal por asesor para leaderboard
		/// </summary>
		/// <returns> List<SueldoSemanalAsesorDTO> </returns>
		public List<SueldoSemanalAsesorDTO> ObtenerVentaSemanalPorAsesor(int idPersonal)
		{
			try
			{
				DateTime fechaActual = DateTime.Today;
				int prevMonth = fechaActual.AddMonths(-1).Month;
				int year = fechaActual.AddMonths(-1).Year;
				int daysInPrevMonth = DateTime.DaysInMonth(year, prevMonth);
				DateTime mesPasado = new DateTime(year, prevMonth, 1);
				List<SueldoSemanalAsesorDTO> personal = new List<SueldoSemanalAsesorDTO>();
				string query = "com.SP_ObtenerVentasMensualesPorSemana";
				string respuestaQuery = _dapper.QuerySPDapper(query, new { fecha = mesPasado, asesor = idPersonal });

				if (!string.IsNullOrEmpty(respuestaQuery))
				{
					personal = JsonConvert.DeserializeObject<List<SueldoSemanalAsesorDTO>>(respuestaQuery);
				}
				return personal;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// Repositorio: TableroComercialCategoriaAsesorRepositorio
		/// Autor: Jashin Salazar.
		/// Fecha: 08/06/2021
		/// <summary>
		/// Se obtiene ranking para leaderboard
		/// </summary>
		/// <returns>  </returns>
		public List<RankingAsesorDTO> ObtenerRanking(int idPersonal)
		{
			try
			{
				DateTime fechaActual = DateTime.Today;
				int prevMonth = fechaActual.AddMonths(-1).Month;
				int year = fechaActual.AddMonths(-1).Year;
				int daysInPrevMonth = DateTime.DaysInMonth(year, prevMonth);
				DateTime mesPasado = new DateTime(year, prevMonth, 1);
				List<RankingAsesorDTO> personal = new List<RankingAsesorDTO>();
				string query = "com.SP_TableroComercialRanking";
				string respuestaQuery = _dapper.QuerySPDapper(query, new { fecha = mesPasado });
				var topRanking = new List<RankingAsesorDTO>();
				if (!string.IsNullOrEmpty(respuestaQuery))
				{
					personal = JsonConvert.DeserializeObject<List<RankingAsesorDTO>>(respuestaQuery);

					var puestoAsesor = (from t in personal
										where t.IdPersonal == idPersonal
										select t.Orden).FirstOrDefault();

					if (puestoAsesor < 5)
					{
						topRanking = (from t in personal
									  select t).Take(5).ToList();
					}
					else
					{
						topRanking = (from t in personal
									  select t).Take(4).ToList();
						var asesor = (from t in personal
									  where t.IdPersonal == idPersonal
									  select t).ToList();
						topRanking.AddRange(asesor);
					}
				}
				return topRanking;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// Repositorio: TableroComercialCategoriaAsesorRepositorio
		/// Autor: Jashin Salazar.
		/// Fecha: 08/06/2021
		/// <summary>
		/// Se obtiene mejor mes para leaderboard
		/// </summary>
		/// <returns>  </returns>
		public List<MejorMesAsesorDTO> ObtenerMejorMes(int idPersonal)
		{
			try
			{
				List<MejorMesAsesorDTO> personal = new List<MejorMesAsesorDTO>();
				string query = "SELECT IdPersonal, Monto FROM com.T_TableroComercialMejorMes WHERE IdPersonal=@asesor";
				string respuestaQuery = _dapper.QueryDapper(query, new { asesor = idPersonal });
				var topRanking = new List<RankingAsesorDTO>();
				if (!string.IsNullOrEmpty(respuestaQuery))
				{
					personal = JsonConvert.DeserializeObject<List<MejorMesAsesorDTO>>(respuestaQuery);
				}
				return personal;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// Repositorio: TableroComercialCategoriaAsesorRepositorio
		/// Autor: Jashin Salazar.
		/// Fecha: 09/06/2021
		/// <summary>
		/// Se obtiene comisiones pasadas para leaderboard
		/// </summary>
		/// <returns>  </returns>
		public List<ComisionesPasadasDTO> ObtenerComisionPasadaPorAsesor(int idPersonal)
		{
			try
			{
				List<ComisionesPasadasDTO> personal = new List<ComisionesPasadasDTO>();
				string query = "SELECT IdPersonal, MontoSoles, MontoDolares FROM com.V_ObtenerComisionPasada WHERE IdPersonal=@asesor";
				string respuestaQuery = _dapper.QueryDapper(query, new { asesor = idPersonal });
				var topRanking = new List<RankingAsesorDTO>();
				if (!string.IsNullOrEmpty(respuestaQuery))
				{
					personal = JsonConvert.DeserializeObject<List<ComisionesPasadasDTO>>(respuestaQuery);
				}
				return personal;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		/// Repositorio: TableroComercialCategoriaAsesorRepositorio
		/// Autor: Jashin Salazar.
		/// Fecha: 09/06/2021
		/// <summary>
		/// Se obtiene comisiones actuales para leaderboard
		/// </summary>
		/// <returns>  </returns>
		public List<ComisionActualDTO> ObtenerComisionActualPorAsesor(int idPersonal, float? venta)
		{
			try
			{
				List<ComisionActualDTO> personal = new List<ComisionActualDTO>();
				string query = "com.SP_ObtenerComisionActual";
				string respuestaQuery = _dapper.QuerySPDapper(query, new { asesor = idPersonal, montoVenta = venta });
				var topRanking = new List<RankingAsesorDTO>();
				if (!string.IsNullOrEmpty(respuestaQuery))
				{
					personal = JsonConvert.DeserializeObject<List<ComisionActualDTO>>(respuestaQuery);
				}
				return personal;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}
}