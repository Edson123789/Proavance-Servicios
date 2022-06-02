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
    public class DataCreditoDataProductoValorRepositorio : BaseRepository<TDataCreditoDataProductoValor, DataCreditoDataProductoValorBO>
    {
		#region Metodos Base
		public DataCreditoDataProductoValorRepositorio() : base()
		{
		}
		public DataCreditoDataProductoValorRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataProductoValorBO> GetBy(Expression<Func<TDataCreditoDataProductoValor, bool>> filter)
		{
			IEnumerable<TDataCreditoDataProductoValor> listado = base.GetBy(filter);
			List<DataCreditoDataProductoValorBO> listadoBO = new List<DataCreditoDataProductoValorBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataProductoValorBO objetoBO = Mapper.Map<TDataCreditoDataProductoValor, DataCreditoDataProductoValorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataProductoValorBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataProductoValor entidad = base.FirstById(id);
				DataCreditoDataProductoValorBO objetoBO = new DataCreditoDataProductoValorBO();
				Mapper.Map<TDataCreditoDataProductoValor, DataCreditoDataProductoValorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataProductoValorBO FirstBy(Expression<Func<TDataCreditoDataProductoValor, bool>> filter)
		{
			try
			{
				TDataCreditoDataProductoValor entidad = base.FirstBy(filter);
				DataCreditoDataProductoValorBO objetoBO = Mapper.Map<TDataCreditoDataProductoValor, DataCreditoDataProductoValorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataProductoValorBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataProductoValor entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataProductoValorBO> listadoBO)
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

		public bool Update(DataCreditoDataProductoValorBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataProductoValor entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataProductoValorBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataProductoValor entidad, DataCreditoDataProductoValorBO objetoBO)
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

		private TDataCreditoDataProductoValor MapeoEntidad(DataCreditoDataProductoValorBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataProductoValor entidad = new TDataCreditoDataProductoValor();
				entidad = Mapper.Map<DataCreditoDataProductoValorBO, TDataCreditoDataProductoValor>(objetoBO,
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
