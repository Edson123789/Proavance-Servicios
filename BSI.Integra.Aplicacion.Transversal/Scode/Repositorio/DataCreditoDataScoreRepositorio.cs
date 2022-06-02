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
    public class DataCreditoDataScoreRepositorio : BaseRepository<TDataCreditoDataScore, DataCreditoDataScoreBO>
    {
		#region Metodos Base
		public DataCreditoDataScoreRepositorio() : base()
		{
		}
		public DataCreditoDataScoreRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataScoreBO> GetBy(Expression<Func<TDataCreditoDataScore, bool>> filter)
		{
			IEnumerable<TDataCreditoDataScore> listado = base.GetBy(filter);
			List<DataCreditoDataScoreBO> listadoBO = new List<DataCreditoDataScoreBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataScoreBO objetoBO = Mapper.Map<TDataCreditoDataScore, DataCreditoDataScoreBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataScoreBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataScore entidad = base.FirstById(id);
				DataCreditoDataScoreBO objetoBO = new DataCreditoDataScoreBO();
				Mapper.Map<TDataCreditoDataScore, DataCreditoDataScoreBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataScoreBO FirstBy(Expression<Func<TDataCreditoDataScore, bool>> filter)
		{
			try
			{
				TDataCreditoDataScore entidad = base.FirstBy(filter);
				DataCreditoDataScoreBO objetoBO = Mapper.Map<TDataCreditoDataScore, DataCreditoDataScoreBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataScoreBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataScore entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataScoreBO> listadoBO)
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

		public bool Update(DataCreditoDataScoreBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataScore entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataScoreBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataScore entidad, DataCreditoDataScoreBO objetoBO)
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

		private TDataCreditoDataScore MapeoEntidad(DataCreditoDataScoreBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataScore entidad = new TDataCreditoDataScore();
				entidad = Mapper.Map<DataCreditoDataScoreBO, TDataCreditoDataScore>(objetoBO,
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
