using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOsComercial;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class DataCreditoBusquedaRepositorio : BaseRepository<TDataCreditoBusqueda, DataCreditoBusquedaBO>
    {
		#region Metodos Base
		public DataCreditoBusquedaRepositorio() : base()
		{
		}
		public DataCreditoBusquedaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoBusquedaBO> GetBy(Expression<Func<TDataCreditoBusqueda, bool>> filter)
		{
			IEnumerable<TDataCreditoBusqueda> listado = base.GetBy(filter);
			List<DataCreditoBusquedaBO> listadoBO = new List<DataCreditoBusquedaBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoBusquedaBO objetoBO = Mapper.Map<TDataCreditoBusqueda, DataCreditoBusquedaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoBusquedaBO FirstById(int id)
		{
			try
			{
				TDataCreditoBusqueda entidad = base.FirstById(id);
				DataCreditoBusquedaBO objetoBO = new DataCreditoBusquedaBO();
				Mapper.Map<TDataCreditoBusqueda, DataCreditoBusquedaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoBusquedaBO FirstBy(Expression<Func<TDataCreditoBusqueda, bool>> filter)
		{
			try
			{
				TDataCreditoBusqueda entidad = base.FirstBy(filter);
				DataCreditoBusquedaBO objetoBO = Mapper.Map<TDataCreditoBusqueda, DataCreditoBusquedaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoBusquedaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoBusqueda entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoBusquedaBO> listadoBO)
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

		public bool Update(DataCreditoBusquedaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoBusqueda entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoBusquedaBO> listadoBO)
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
		private void AsignacionId(TDataCreditoBusqueda entidad, DataCreditoBusquedaBO objetoBO)
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

		private TDataCreditoBusqueda MapeoEntidad(DataCreditoBusquedaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoBusqueda entidad = new TDataCreditoBusqueda();
				entidad = Mapper.Map<DataCreditoBusquedaBO, TDataCreditoBusqueda>(objetoBO,
					opt => opt.ConfigureMap(MemberList.None));

				//mapea los hijos

				return entidad;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		#endregion

		/// Repositorio: DataCreditoBusquedaRepositorio
		/// Autor: Jashin Salazar.
		/// Fecha: 18/02/2022
		/// Version: 1.0
		/// <summary>
		/// Obtiene el IdDataCredito de un alumno.
		/// </summary>
		/// <param name="nroDocumento"> Numero de documento del alumno </param>
		/// <returns> ObjetoDTO: DataCreditoDTO </returns>
		public DataCreditoDTO ObtenerIDDataCredito(int IdAlumno)
		{
			try
			{
				string query = "com.SP_DataCreditoObtenerIdPorAlumno";
				string respuestaQuery = _dapper.QuerySPFirstOrDefault(query, new { IdAlumno = IdAlumno });
				return JsonConvert.DeserializeObject<DataCreditoDTO>(respuestaQuery);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Repositorio: PersonalRepositorio
		/// Autor: Jashin Salazar.
		/// Fecha: 18/02/2022
		/// <summary>
		/// Obtiene el historial de tarjetas de credito de DataCredito
		/// </summary>
		/// <param name="idDataCredito"> Id de data credito en la BD </param>
		/// <returns>Lista de ObjetosDTO: List(DataCreditoTarjetaCreditoDTO)</returns>
		public List<DataCreditoTarjetaCreditoDTO> ObtenerHistorialTarjetasDataCredito(int idDataCredito)
		{
			try
			{
				string query = "com.SP_DataCreditoObtenerTarjetasCredito";
				string respuestaQuery = _dapper.QuerySPDapper(query, new { IdDataCredito = idDataCredito });
				return JsonConvert.DeserializeObject<List<DataCreditoTarjetaCreditoDTO>>(respuestaQuery);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Repositorio: PersonalRepositorio
		/// Autor: Jashin Salazar.
		/// Fecha: 18/02/2022
		/// <summary>
		/// Obtiene el historial de Deudas vigentes de credito de DataCredito
		/// </summary>
		/// <param name="idDataCredito"> Id de data credito en la BD </param>
		/// <returns>Lista de ObjetosDTO: List(DataCreditoTarjetaCreditoDTO)</returns>
		public List<DataCreditoCreditoVigenteDTO> ObtenerHistorialDeudasDataCredito(int idDataCredito)
		{
			try
			{
				string query = "com.SP_DataCreditoObtenerCreditoVigente";
				string respuestaQuery = _dapper.QuerySPDapper(query, new { IdDataCredito = idDataCredito });
				return JsonConvert.DeserializeObject<List<DataCreditoCreditoVigenteDTO>>(respuestaQuery);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		/// Repositorio: PersonalRepositorio
		/// Autor: Jashin Salazar.
		/// Fecha: 19/02/2022
		/// <summary>
		/// Obtiene el historial de Deudas vigentes de credito de DataCredito
		/// </summary>
		/// <param name="idDataCredito"> Id de data credito en la BD </param>
		/// <returns>Lista de ObjetosDTO: List(DataCreditoTarjetaCreditoDTO)</returns>
		public DataCreditoInformacionDTO ObtenerInformacionDataCredito(int idDataCredito)
		{
			try
			{
				string query = "com.SP_DataCreditoObtenerInformacionGeneral";
				string respuestaQuery = _dapper.QuerySPFirstOrDefault(query, new { IdDataCredito = idDataCredito });
				return JsonConvert.DeserializeObject<DataCreditoInformacionDTO>(respuestaQuery);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
	}


}
