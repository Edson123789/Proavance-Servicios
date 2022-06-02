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
    public class DataCreditoDataInfAgrHistoricoSaldoTipoCuentaRepositorio : BaseRepository<TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta, DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO>
	{
		#region Metodos Base
		public DataCreditoDataInfAgrHistoricoSaldoTipoCuentaRepositorio() : base()
		{
		}
		public DataCreditoDataInfAgrHistoricoSaldoTipoCuentaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO> GetBy(Expression<Func<TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta, bool>> filter)
		{
			IEnumerable<TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta> listado = base.GetBy(filter);
			List<DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO> listadoBO = new List<DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta, DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta entidad = base.FirstById(id);
				DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO objetoBO = new DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO();
				Mapper.Map<TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta, DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO FirstBy(Expression<Func<TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta, bool>> filter)
		{
			try
			{
				TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta entidad = base.FirstBy(filter);
				DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta, DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO> listadoBO)
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

		public bool Update(DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta entidad, DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO objetoBO)
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

		private TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta MapeoEntidad(DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta entidad = new TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta();
				entidad = Mapper.Map<DataCreditoDataInfAgrHistoricoSaldoTipoCuentaBO, TDataCreditoDataInfAgrHistoricoSaldoTipoCuenta>(objetoBO,
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
