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
    public class DataCreditoDataInfAgrTotalRepositorio : BaseRepository<TDataCreditoDataInfAgrTotal, DataCreditoDataInfAgrTotalBO>
	{
		#region Metodos Base
		public DataCreditoDataInfAgrTotalRepositorio() : base()
		{
		}
		public DataCreditoDataInfAgrTotalRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataInfAgrTotalBO> GetBy(Expression<Func<TDataCreditoDataInfAgrTotal, bool>> filter)
		{
			IEnumerable<TDataCreditoDataInfAgrTotal> listado = base.GetBy(filter);
			List<DataCreditoDataInfAgrTotalBO> listadoBO = new List<DataCreditoDataInfAgrTotalBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataInfAgrTotalBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrTotal, DataCreditoDataInfAgrTotalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataInfAgrTotalBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataInfAgrTotal entidad = base.FirstById(id);
				DataCreditoDataInfAgrTotalBO objetoBO = new DataCreditoDataInfAgrTotalBO();
				Mapper.Map<TDataCreditoDataInfAgrTotal, DataCreditoDataInfAgrTotalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataInfAgrTotalBO FirstBy(Expression<Func<TDataCreditoDataInfAgrTotal, bool>> filter)
		{
			try
			{
				TDataCreditoDataInfAgrTotal entidad = base.FirstBy(filter);
				DataCreditoDataInfAgrTotalBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrTotal, DataCreditoDataInfAgrTotalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataInfAgrTotalBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataInfAgrTotal entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataInfAgrTotalBO> listadoBO)
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

		public bool Update(DataCreditoDataInfAgrTotalBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataInfAgrTotal entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataInfAgrTotalBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataInfAgrTotal entidad, DataCreditoDataInfAgrTotalBO objetoBO)
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

		private TDataCreditoDataInfAgrTotal MapeoEntidad(DataCreditoDataInfAgrTotalBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataInfAgrTotal entidad = new TDataCreditoDataInfAgrTotal();
				entidad = Mapper.Map<DataCreditoDataInfAgrTotalBO, TDataCreditoDataInfAgrTotal>(objetoBO,
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
