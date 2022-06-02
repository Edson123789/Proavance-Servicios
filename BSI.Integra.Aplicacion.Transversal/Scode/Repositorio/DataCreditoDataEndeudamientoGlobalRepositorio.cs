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
    public class DataCreditoDataEndeudamientoGlobalRepositorio : BaseRepository<TDataCreditoDataEndeudamientoGlobal, DataCreditoDataEndeudamientoGlobalBO>
    {
		#region Metodos Base
		public DataCreditoDataEndeudamientoGlobalRepositorio() : base()
		{
		}
		public DataCreditoDataEndeudamientoGlobalRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataEndeudamientoGlobalBO> GetBy(Expression<Func<TDataCreditoDataEndeudamientoGlobal, bool>> filter)
		{
			IEnumerable<TDataCreditoDataEndeudamientoGlobal> listado = base.GetBy(filter);
			List<DataCreditoDataEndeudamientoGlobalBO> listadoBO = new List<DataCreditoDataEndeudamientoGlobalBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataEndeudamientoGlobalBO objetoBO = Mapper.Map<TDataCreditoDataEndeudamientoGlobal, DataCreditoDataEndeudamientoGlobalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataEndeudamientoGlobalBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataEndeudamientoGlobal entidad = base.FirstById(id);
				DataCreditoDataEndeudamientoGlobalBO objetoBO = new DataCreditoDataEndeudamientoGlobalBO();
				Mapper.Map<TDataCreditoDataEndeudamientoGlobal, DataCreditoDataEndeudamientoGlobalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataEndeudamientoGlobalBO FirstBy(Expression<Func<TDataCreditoDataEndeudamientoGlobal, bool>> filter)
		{
			try
			{
				TDataCreditoDataEndeudamientoGlobal entidad = base.FirstBy(filter);
				DataCreditoDataEndeudamientoGlobalBO objetoBO = Mapper.Map<TDataCreditoDataEndeudamientoGlobal, DataCreditoDataEndeudamientoGlobalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataEndeudamientoGlobalBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataEndeudamientoGlobal entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataEndeudamientoGlobalBO> listadoBO)
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

		public bool Update(DataCreditoDataEndeudamientoGlobalBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataEndeudamientoGlobal entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataEndeudamientoGlobalBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataEndeudamientoGlobal entidad, DataCreditoDataEndeudamientoGlobalBO objetoBO)
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

		private TDataCreditoDataEndeudamientoGlobal MapeoEntidad(DataCreditoDataEndeudamientoGlobalBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataEndeudamientoGlobal entidad = new TDataCreditoDataEndeudamientoGlobal();
				entidad = Mapper.Map<DataCreditoDataEndeudamientoGlobalBO, TDataCreditoDataEndeudamientoGlobal>(objetoBO,
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
