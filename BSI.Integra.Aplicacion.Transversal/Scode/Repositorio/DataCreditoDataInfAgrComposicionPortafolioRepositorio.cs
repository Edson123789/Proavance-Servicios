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
    public class DataCreditoDataInfAgrComposicionPortafolioRepositorio : BaseRepository<TDataCreditoDataInfAgrComposicionPortafolio, DataCreditoDataInfAgrComposicionPortafolioBO>
	{
		#region Metodos Base
		public DataCreditoDataInfAgrComposicionPortafolioRepositorio() : base()
		{
		}
		public DataCreditoDataInfAgrComposicionPortafolioRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataInfAgrComposicionPortafolioBO> GetBy(Expression<Func<TDataCreditoDataInfAgrComposicionPortafolio, bool>> filter)
		{
			IEnumerable<TDataCreditoDataInfAgrComposicionPortafolio> listado = base.GetBy(filter);
			List<DataCreditoDataInfAgrComposicionPortafolioBO> listadoBO = new List<DataCreditoDataInfAgrComposicionPortafolioBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataInfAgrComposicionPortafolioBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrComposicionPortafolio, DataCreditoDataInfAgrComposicionPortafolioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataInfAgrComposicionPortafolioBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataInfAgrComposicionPortafolio entidad = base.FirstById(id);
				DataCreditoDataInfAgrComposicionPortafolioBO objetoBO = new DataCreditoDataInfAgrComposicionPortafolioBO();
				Mapper.Map<TDataCreditoDataInfAgrComposicionPortafolio, DataCreditoDataInfAgrComposicionPortafolioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataInfAgrComposicionPortafolioBO FirstBy(Expression<Func<TDataCreditoDataInfAgrComposicionPortafolio, bool>> filter)
		{
			try
			{
				TDataCreditoDataInfAgrComposicionPortafolio entidad = base.FirstBy(filter);
				DataCreditoDataInfAgrComposicionPortafolioBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrComposicionPortafolio, DataCreditoDataInfAgrComposicionPortafolioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataInfAgrComposicionPortafolioBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataInfAgrComposicionPortafolio entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataInfAgrComposicionPortafolioBO> listadoBO)
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

		public bool Update(DataCreditoDataInfAgrComposicionPortafolioBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataInfAgrComposicionPortafolio entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataInfAgrComposicionPortafolioBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataInfAgrComposicionPortafolio entidad, DataCreditoDataInfAgrComposicionPortafolioBO objetoBO)
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

		private TDataCreditoDataInfAgrComposicionPortafolio MapeoEntidad(DataCreditoDataInfAgrComposicionPortafolioBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataInfAgrComposicionPortafolio entidad = new TDataCreditoDataInfAgrComposicionPortafolio();
				entidad = Mapper.Map<DataCreditoDataInfAgrComposicionPortafolioBO, TDataCreditoDataInfAgrComposicionPortafolio>(objetoBO,
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
