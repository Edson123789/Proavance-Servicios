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
    public class DataCreditoDataInfAgrResumenSaldoSectorRepositorio : BaseRepository<TDataCreditoDataInfAgrResumenSaldoSector, DataCreditoDataInfAgrResumenSaldoSectorBO>
	{
		#region Metodos Base
		public DataCreditoDataInfAgrResumenSaldoSectorRepositorio() : base()
		{
		}
		public DataCreditoDataInfAgrResumenSaldoSectorRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataInfAgrResumenSaldoSectorBO> GetBy(Expression<Func<TDataCreditoDataInfAgrResumenSaldoSector, bool>> filter)
		{
			IEnumerable<TDataCreditoDataInfAgrResumenSaldoSector> listado = base.GetBy(filter);
			List<DataCreditoDataInfAgrResumenSaldoSectorBO> listadoBO = new List<DataCreditoDataInfAgrResumenSaldoSectorBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataInfAgrResumenSaldoSectorBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrResumenSaldoSector, DataCreditoDataInfAgrResumenSaldoSectorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataInfAgrResumenSaldoSectorBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataInfAgrResumenSaldoSector entidad = base.FirstById(id);
				DataCreditoDataInfAgrResumenSaldoSectorBO objetoBO = new DataCreditoDataInfAgrResumenSaldoSectorBO();
				Mapper.Map<TDataCreditoDataInfAgrResumenSaldoSector, DataCreditoDataInfAgrResumenSaldoSectorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataInfAgrResumenSaldoSectorBO FirstBy(Expression<Func<TDataCreditoDataInfAgrResumenSaldoSector, bool>> filter)
		{
			try
			{
				TDataCreditoDataInfAgrResumenSaldoSector entidad = base.FirstBy(filter);
				DataCreditoDataInfAgrResumenSaldoSectorBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrResumenSaldoSector, DataCreditoDataInfAgrResumenSaldoSectorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataInfAgrResumenSaldoSectorBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataInfAgrResumenSaldoSector entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataInfAgrResumenSaldoSectorBO> listadoBO)
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

		public bool Update(DataCreditoDataInfAgrResumenSaldoSectorBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataInfAgrResumenSaldoSector entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataInfAgrResumenSaldoSectorBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataInfAgrResumenSaldoSector entidad, DataCreditoDataInfAgrResumenSaldoSectorBO objetoBO)
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

		private TDataCreditoDataInfAgrResumenSaldoSector MapeoEntidad(DataCreditoDataInfAgrResumenSaldoSectorBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataInfAgrResumenSaldoSector entidad = new TDataCreditoDataInfAgrResumenSaldoSector();
				entidad = Mapper.Map<DataCreditoDataInfAgrResumenSaldoSectorBO, TDataCreditoDataInfAgrResumenSaldoSector>(objetoBO,
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
