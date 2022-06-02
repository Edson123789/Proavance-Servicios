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
    public class DataCreditoDataInfAgrResumenSaldoRepositorio : BaseRepository<TDataCreditoDataInfAgrResumenSaldo, DataCreditoDataInfAgrResumenSaldoBO>
	{
		#region Metodos Base
		public DataCreditoDataInfAgrResumenSaldoRepositorio() : base()
		{
		}
		public DataCreditoDataInfAgrResumenSaldoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataInfAgrResumenSaldoBO> GetBy(Expression<Func<TDataCreditoDataInfAgrResumenSaldo, bool>> filter)
		{
			IEnumerable<TDataCreditoDataInfAgrResumenSaldo> listado = base.GetBy(filter);
			List<DataCreditoDataInfAgrResumenSaldoBO> listadoBO = new List<DataCreditoDataInfAgrResumenSaldoBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataInfAgrResumenSaldoBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrResumenSaldo, DataCreditoDataInfAgrResumenSaldoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataInfAgrResumenSaldoBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataInfAgrResumenSaldo entidad = base.FirstById(id);
				DataCreditoDataInfAgrResumenSaldoBO objetoBO = new DataCreditoDataInfAgrResumenSaldoBO();
				Mapper.Map<TDataCreditoDataInfAgrResumenSaldo, DataCreditoDataInfAgrResumenSaldoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataInfAgrResumenSaldoBO FirstBy(Expression<Func<TDataCreditoDataInfAgrResumenSaldo, bool>> filter)
		{
			try
			{
				TDataCreditoDataInfAgrResumenSaldo entidad = base.FirstBy(filter);
				DataCreditoDataInfAgrResumenSaldoBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrResumenSaldo, DataCreditoDataInfAgrResumenSaldoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataInfAgrResumenSaldoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataInfAgrResumenSaldo entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataInfAgrResumenSaldoBO> listadoBO)
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

		public bool Update(DataCreditoDataInfAgrResumenSaldoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataInfAgrResumenSaldo entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataInfAgrResumenSaldoBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataInfAgrResumenSaldo entidad, DataCreditoDataInfAgrResumenSaldoBO objetoBO)
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

		private TDataCreditoDataInfAgrResumenSaldo MapeoEntidad(DataCreditoDataInfAgrResumenSaldoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataInfAgrResumenSaldo entidad = new TDataCreditoDataInfAgrResumenSaldo();
				entidad = Mapper.Map<DataCreditoDataInfAgrResumenSaldoBO, TDataCreditoDataInfAgrResumenSaldo>(objetoBO,
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
