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
    public class DataCreditoDataInfAgrHistoricoSaldoTotalRepositorio : BaseRepository<TDataCreditoDataInfAgrHistoricoSaldoTotal, DataCreditoDataInfAgrHistoricoSaldoTotalBO>
	{
		#region Metodos Base
		public DataCreditoDataInfAgrHistoricoSaldoTotalRepositorio() : base()
		{
		}
		public DataCreditoDataInfAgrHistoricoSaldoTotalRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataInfAgrHistoricoSaldoTotalBO> GetBy(Expression<Func<TDataCreditoDataInfAgrHistoricoSaldoTotal, bool>> filter)
		{
			IEnumerable<TDataCreditoDataInfAgrHistoricoSaldoTotal> listado = base.GetBy(filter);
			List<DataCreditoDataInfAgrHistoricoSaldoTotalBO> listadoBO = new List<DataCreditoDataInfAgrHistoricoSaldoTotalBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataInfAgrHistoricoSaldoTotalBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrHistoricoSaldoTotal, DataCreditoDataInfAgrHistoricoSaldoTotalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataInfAgrHistoricoSaldoTotalBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataInfAgrHistoricoSaldoTotal entidad = base.FirstById(id);
				DataCreditoDataInfAgrHistoricoSaldoTotalBO objetoBO = new DataCreditoDataInfAgrHistoricoSaldoTotalBO();
				Mapper.Map<TDataCreditoDataInfAgrHistoricoSaldoTotal, DataCreditoDataInfAgrHistoricoSaldoTotalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataInfAgrHistoricoSaldoTotalBO FirstBy(Expression<Func<TDataCreditoDataInfAgrHistoricoSaldoTotal, bool>> filter)
		{
			try
			{
				TDataCreditoDataInfAgrHistoricoSaldoTotal entidad = base.FirstBy(filter);
				DataCreditoDataInfAgrHistoricoSaldoTotalBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrHistoricoSaldoTotal, DataCreditoDataInfAgrHistoricoSaldoTotalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataInfAgrHistoricoSaldoTotalBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataInfAgrHistoricoSaldoTotal entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataInfAgrHistoricoSaldoTotalBO> listadoBO)
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

		public bool Update(DataCreditoDataInfAgrHistoricoSaldoTotalBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataInfAgrHistoricoSaldoTotal entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataInfAgrHistoricoSaldoTotalBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataInfAgrHistoricoSaldoTotal entidad, DataCreditoDataInfAgrHistoricoSaldoTotalBO objetoBO)
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

		private TDataCreditoDataInfAgrHistoricoSaldoTotal MapeoEntidad(DataCreditoDataInfAgrHistoricoSaldoTotalBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataInfAgrHistoricoSaldoTotal entidad = new TDataCreditoDataInfAgrHistoricoSaldoTotal();
				entidad = Mapper.Map<DataCreditoDataInfAgrHistoricoSaldoTotalBO, TDataCreditoDataInfAgrHistoricoSaldoTotal>(objetoBO,
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
