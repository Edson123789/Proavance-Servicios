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
    public class DataCreditoDataTarjetaCreditoRepositorio : BaseRepository<TDataCreditoDataTarjetaCredito, DataCreditoDataTarjetaCreditoBO>
    {
		#region Metodos Base
		public DataCreditoDataTarjetaCreditoRepositorio() : base()
		{
		}
		public DataCreditoDataTarjetaCreditoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataTarjetaCreditoBO> GetBy(Expression<Func<TDataCreditoDataTarjetaCredito, bool>> filter)
		{
			IEnumerable<TDataCreditoDataTarjetaCredito> listado = base.GetBy(filter);
			List<DataCreditoDataTarjetaCreditoBO> listadoBO = new List<DataCreditoDataTarjetaCreditoBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataTarjetaCreditoBO objetoBO = Mapper.Map<TDataCreditoDataTarjetaCredito, DataCreditoDataTarjetaCreditoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataTarjetaCreditoBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataTarjetaCredito entidad = base.FirstById(id);
				DataCreditoDataTarjetaCreditoBO objetoBO = new DataCreditoDataTarjetaCreditoBO();
				Mapper.Map<TDataCreditoDataTarjetaCredito, DataCreditoDataTarjetaCreditoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataTarjetaCreditoBO FirstBy(Expression<Func<TDataCreditoDataTarjetaCredito, bool>> filter)
		{
			try
			{
				TDataCreditoDataTarjetaCredito entidad = base.FirstBy(filter);
				DataCreditoDataTarjetaCreditoBO objetoBO = Mapper.Map<TDataCreditoDataTarjetaCredito, DataCreditoDataTarjetaCreditoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataTarjetaCreditoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataTarjetaCredito entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataTarjetaCreditoBO> listadoBO)
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

		public bool Update(DataCreditoDataTarjetaCreditoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataTarjetaCredito entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataTarjetaCreditoBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataTarjetaCredito entidad, DataCreditoDataTarjetaCreditoBO objetoBO)
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

		private TDataCreditoDataTarjetaCredito MapeoEntidad(DataCreditoDataTarjetaCreditoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataTarjetaCredito entidad = new TDataCreditoDataTarjetaCredito();
				entidad = Mapper.Map<DataCreditoDataTarjetaCreditoBO, TDataCreditoDataTarjetaCredito>(objetoBO,
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
