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
    public class DataCreditoDataInfAgrResumenPrincipalRepositorio : BaseRepository<TDataCreditoDataInfAgrResumenPrincipal, DataCreditoDataInfAgrResumenPrincipalBO>
	{
		#region Metodos Base
		public DataCreditoDataInfAgrResumenPrincipalRepositorio() : base()
		{
		}
		public DataCreditoDataInfAgrResumenPrincipalRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataInfAgrResumenPrincipalBO> GetBy(Expression<Func<TDataCreditoDataInfAgrResumenPrincipal, bool>> filter)
		{
			IEnumerable<TDataCreditoDataInfAgrResumenPrincipal> listado = base.GetBy(filter);
			List<DataCreditoDataInfAgrResumenPrincipalBO> listadoBO = new List<DataCreditoDataInfAgrResumenPrincipalBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataInfAgrResumenPrincipalBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrResumenPrincipal, DataCreditoDataInfAgrResumenPrincipalBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataInfAgrResumenPrincipalBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataInfAgrResumenPrincipal entidad = base.FirstById(id);
				DataCreditoDataInfAgrResumenPrincipalBO objetoBO = new DataCreditoDataInfAgrResumenPrincipalBO();
				Mapper.Map<TDataCreditoDataInfAgrResumenPrincipal, DataCreditoDataInfAgrResumenPrincipalBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataInfAgrResumenPrincipalBO FirstBy(Expression<Func<TDataCreditoDataInfAgrResumenPrincipal, bool>> filter)
		{
			try
			{
				TDataCreditoDataInfAgrResumenPrincipal entidad = base.FirstBy(filter);
				DataCreditoDataInfAgrResumenPrincipalBO objetoBO = Mapper.Map<TDataCreditoDataInfAgrResumenPrincipal, DataCreditoDataInfAgrResumenPrincipalBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataInfAgrResumenPrincipalBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataInfAgrResumenPrincipal entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataInfAgrResumenPrincipalBO> listadoBO)
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

		public bool Update(DataCreditoDataInfAgrResumenPrincipalBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataInfAgrResumenPrincipal entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataInfAgrResumenPrincipalBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataInfAgrResumenPrincipal entidad, DataCreditoDataInfAgrResumenPrincipalBO objetoBO)
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

		private TDataCreditoDataInfAgrResumenPrincipal MapeoEntidad(DataCreditoDataInfAgrResumenPrincipalBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataInfAgrResumenPrincipal entidad = new TDataCreditoDataInfAgrResumenPrincipal();
				entidad = Mapper.Map<DataCreditoDataInfAgrResumenPrincipalBO, TDataCreditoDataInfAgrResumenPrincipal>(objetoBO,
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
