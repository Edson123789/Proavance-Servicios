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
	public class DataCreditoLogRepositorio : BaseRepository<TDataCreditoLog, DataCreditoLogBO>
	{
		#region Metodos Base
		public DataCreditoLogRepositorio() : base()
		{
		}
		public DataCreditoLogRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoLogBO> GetBy(Expression<Func<TDataCreditoLog, bool>> filter)
		{
			IEnumerable<TDataCreditoLog> listado = base.GetBy(filter);
			List<DataCreditoLogBO> listadoBO = new List<DataCreditoLogBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoLogBO objetoBO = Mapper.Map<TDataCreditoLog, DataCreditoLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoLogBO FirstById(int id)
		{
			try
			{
				TDataCreditoLog entidad = base.FirstById(id);
				DataCreditoLogBO objetoBO = new DataCreditoLogBO();
				Mapper.Map<TDataCreditoLog, DataCreditoLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoLogBO FirstBy(Expression<Func<TDataCreditoLog, bool>> filter)
		{
			try
			{
				TDataCreditoLog entidad = base.FirstBy(filter);
				DataCreditoLogBO objetoBO = Mapper.Map<TDataCreditoLog, DataCreditoLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoLogBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoLog entidad = MapeoEntidad(objetoBO);

				bool resultado = base.Insert(entidad);
				if (resultado)
					AsignacionId(entidad, objetoBO);

				return resultado;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public bool Insert(IEnumerable<DataCreditoLogBO> listadoBO)
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

		public bool Update(DataCreditoLogBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoLog entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoLogBO> listadoBO)
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
		private void AsignacionId(TDataCreditoLog entidad, DataCreditoLogBO objetoBO)
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

		private TDataCreditoLog MapeoEntidad(DataCreditoLogBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoLog entidad = new TDataCreditoLog();
				entidad = Mapper.Map<DataCreditoLogBO, TDataCreditoLog>(objetoBO,
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
