using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class DataCreditoDataCuentaCarteraRepositorio : BaseRepository<TDataCreditoDataCuentaCartera, DataCreditoDataCuentaCarteraBO>
    {
		#region Metodos Base
		public DataCreditoDataCuentaCarteraRepositorio() : base()
		{
		}
		public DataCreditoDataCuentaCarteraRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataCuentaCarteraBO> GetBy(Expression<Func<TDataCreditoDataCuentaCartera, bool>> filter)
		{
			IEnumerable<TDataCreditoDataCuentaCartera> listado = base.GetBy(filter);
			List<DataCreditoDataCuentaCarteraBO> listadoBO = new List<DataCreditoDataCuentaCarteraBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataCuentaCarteraBO objetoBO = Mapper.Map<TDataCreditoDataCuentaCartera, DataCreditoDataCuentaCarteraBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataCuentaCarteraBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataCuentaCartera entidad = base.FirstById(id);
				DataCreditoDataCuentaCarteraBO objetoBO = new DataCreditoDataCuentaCarteraBO();
				Mapper.Map<TDataCreditoDataCuentaCartera, DataCreditoDataCuentaCarteraBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataCuentaCarteraBO FirstBy(Expression<Func<TDataCreditoDataCuentaCartera, bool>> filter)
		{
			try
			{
				TDataCreditoDataCuentaCartera entidad = base.FirstBy(filter);
				DataCreditoDataCuentaCarteraBO objetoBO = Mapper.Map<TDataCreditoDataCuentaCartera, DataCreditoDataCuentaCarteraBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataCuentaCarteraBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataCuentaCartera entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataCuentaCarteraBO> listadoBO)
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

		public bool Update(DataCreditoDataCuentaCarteraBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataCuentaCartera entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataCuentaCarteraBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataCuentaCartera entidad, DataCreditoDataCuentaCarteraBO objetoBO)
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

		private TDataCreditoDataCuentaCartera MapeoEntidad(DataCreditoDataCuentaCarteraBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataCuentaCartera entidad = new TDataCreditoDataCuentaCartera();
				entidad = Mapper.Map<DataCreditoDataCuentaCarteraBO, TDataCreditoDataCuentaCartera>(objetoBO,
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
	}
}
