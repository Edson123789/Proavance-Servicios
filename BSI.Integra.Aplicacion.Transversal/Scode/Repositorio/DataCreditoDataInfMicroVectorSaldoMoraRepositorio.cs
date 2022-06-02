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
    public class DataCreditoDataInfMicroVectorSaldoMoraRepositorio : BaseRepository<TDataCreditoDataInfMicroVectorSaldoMora, DataCreditoDataInfMicroVectorSaldoMoraBO>
	{
		#region Metodos Base
		public DataCreditoDataInfMicroVectorSaldoMoraRepositorio() : base()
		{
		}
		public DataCreditoDataInfMicroVectorSaldoMoraRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataInfMicroVectorSaldoMoraBO> GetBy(Expression<Func<TDataCreditoDataInfMicroVectorSaldoMora, bool>> filter)
		{
			IEnumerable<TDataCreditoDataInfMicroVectorSaldoMora> listado = base.GetBy(filter);
			List<DataCreditoDataInfMicroVectorSaldoMoraBO> listadoBO = new List<DataCreditoDataInfMicroVectorSaldoMoraBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataInfMicroVectorSaldoMoraBO objetoBO = Mapper.Map<TDataCreditoDataInfMicroVectorSaldoMora, DataCreditoDataInfMicroVectorSaldoMoraBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataInfMicroVectorSaldoMoraBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataInfMicroVectorSaldoMora entidad = base.FirstById(id);
				DataCreditoDataInfMicroVectorSaldoMoraBO objetoBO = new DataCreditoDataInfMicroVectorSaldoMoraBO();
				Mapper.Map<TDataCreditoDataInfMicroVectorSaldoMora, DataCreditoDataInfMicroVectorSaldoMoraBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataInfMicroVectorSaldoMoraBO FirstBy(Expression<Func<TDataCreditoDataInfMicroVectorSaldoMora, bool>> filter)
		{
			try
			{
				TDataCreditoDataInfMicroVectorSaldoMora entidad = base.FirstBy(filter);
				DataCreditoDataInfMicroVectorSaldoMoraBO objetoBO = Mapper.Map<TDataCreditoDataInfMicroVectorSaldoMora, DataCreditoDataInfMicroVectorSaldoMoraBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataInfMicroVectorSaldoMoraBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataInfMicroVectorSaldoMora entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataInfMicroVectorSaldoMoraBO> listadoBO)
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

		public bool Update(DataCreditoDataInfMicroVectorSaldoMoraBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataInfMicroVectorSaldoMora entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataInfMicroVectorSaldoMoraBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataInfMicroVectorSaldoMora entidad, DataCreditoDataInfMicroVectorSaldoMoraBO objetoBO)
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

		private TDataCreditoDataInfMicroVectorSaldoMora MapeoEntidad(DataCreditoDataInfMicroVectorSaldoMoraBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataInfMicroVectorSaldoMora entidad = new TDataCreditoDataInfMicroVectorSaldoMora();
				entidad = Mapper.Map<DataCreditoDataInfMicroVectorSaldoMoraBO, TDataCreditoDataInfMicroVectorSaldoMora>(objetoBO,
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
