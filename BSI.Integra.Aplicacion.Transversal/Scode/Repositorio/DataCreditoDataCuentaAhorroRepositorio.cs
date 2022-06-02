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
    public class DataCreditoDataCuentaAhorroRepositorio : BaseRepository<TDataCreditoDataCuentaAhorro, DataCreditoDataCuentaAhorroBO>
    {
		#region Metodos Base
		public DataCreditoDataCuentaAhorroRepositorio() : base()
		{
		}
		public DataCreditoDataCuentaAhorroRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataCuentaAhorroBO> GetBy(Expression<Func<TDataCreditoDataCuentaAhorro, bool>> filter)
		{
			IEnumerable<TDataCreditoDataCuentaAhorro> listado = base.GetBy(filter);
			List<DataCreditoDataCuentaAhorroBO> listadoBO = new List<DataCreditoDataCuentaAhorroBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataCuentaAhorroBO objetoBO = Mapper.Map<TDataCreditoDataCuentaAhorro, DataCreditoDataCuentaAhorroBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataCuentaAhorroBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataCuentaAhorro entidad = base.FirstById(id);
				DataCreditoDataCuentaAhorroBO objetoBO = new DataCreditoDataCuentaAhorroBO();
				Mapper.Map<TDataCreditoDataCuentaAhorro, DataCreditoDataCuentaAhorroBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataCuentaAhorroBO FirstBy(Expression<Func<TDataCreditoDataCuentaAhorro, bool>> filter)
		{
			try
			{
				TDataCreditoDataCuentaAhorro entidad = base.FirstBy(filter);
				DataCreditoDataCuentaAhorroBO objetoBO = Mapper.Map<TDataCreditoDataCuentaAhorro, DataCreditoDataCuentaAhorroBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataCuentaAhorroBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataCuentaAhorro entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataCuentaAhorroBO> listadoBO)
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

		public bool Update(DataCreditoDataCuentaAhorroBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataCuentaAhorro entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataCuentaAhorroBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataCuentaAhorro entidad, DataCreditoDataCuentaAhorroBO objetoBO)
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

		private TDataCreditoDataCuentaAhorro MapeoEntidad(DataCreditoDataCuentaAhorroBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataCuentaAhorro entidad = new TDataCreditoDataCuentaAhorro();
				entidad = Mapper.Map<DataCreditoDataCuentaAhorroBO, TDataCreditoDataCuentaAhorro>(objetoBO,
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
