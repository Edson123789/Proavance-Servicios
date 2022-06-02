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
    public class DataCreditoDataInfAgrResumenSaldoMesRepositorio : BaseRepository<TDataCreditoDataInfAgrResumenSaldoMes, DataCreditoDataInfAgrResumenSaldoMesBO>
	{
		#region Metodos Base
		public DataCreditoDataInfAgrResumenSaldoMesRepositorio() : base()
		{
		}
		public DataCreditoDataInfAgrResumenSaldoMesRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataInfAgrResumenSaldoMesBO> GetBy(Expression<Func<TDataCreditoDataInfAgrResumenSaldoMes, bool>> filter)
		{
			IEnumerable<TDataCreditoDataInfAgrResumenSaldoMes> listado = base.GetBy(filter);
			List<DataCreditoDataInfAgrResumenSaldoMesBO> listadoBO = new List<DataCreditoDataInfAgrResumenSaldoMesBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataInfAgrResumenSaldoMesBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrResumenSaldoMes, DataCreditoDataInfAgrResumenSaldoMesBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataInfAgrResumenSaldoMesBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataInfAgrResumenSaldoMes entidad = base.FirstById(id);
				DataCreditoDataInfAgrResumenSaldoMesBO objetoBO = new DataCreditoDataInfAgrResumenSaldoMesBO();
				Mapper.Map<TDataCreditoDataInfAgrResumenSaldoMes, DataCreditoDataInfAgrResumenSaldoMesBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataInfAgrResumenSaldoMesBO FirstBy(Expression<Func<TDataCreditoDataInfAgrResumenSaldoMes, bool>> filter)
		{
			try
			{
				TDataCreditoDataInfAgrResumenSaldoMes entidad = base.FirstBy(filter);
				DataCreditoDataInfAgrResumenSaldoMesBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrResumenSaldoMes, DataCreditoDataInfAgrResumenSaldoMesBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataInfAgrResumenSaldoMesBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataInfAgrResumenSaldoMes entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataInfAgrResumenSaldoMesBO> listadoBO)
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

		public bool Update(DataCreditoDataInfAgrResumenSaldoMesBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataInfAgrResumenSaldoMes entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataInfAgrResumenSaldoMesBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataInfAgrResumenSaldoMes entidad, DataCreditoDataInfAgrResumenSaldoMesBO objetoBO)
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

		private TDataCreditoDataInfAgrResumenSaldoMes MapeoEntidad(DataCreditoDataInfAgrResumenSaldoMesBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataInfAgrResumenSaldoMes entidad = new TDataCreditoDataInfAgrResumenSaldoMes();
				entidad = Mapper.Map<DataCreditoDataInfAgrResumenSaldoMesBO, TDataCreditoDataInfAgrResumenSaldoMes>(objetoBO,
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
