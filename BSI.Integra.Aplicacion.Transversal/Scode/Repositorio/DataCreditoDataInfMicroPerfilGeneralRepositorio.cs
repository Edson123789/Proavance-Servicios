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
    public class DataCreditoDataInfMicroPerfilGeneralRepositorio : BaseRepository<TDataCreditoDataInfMicroPerfilGeneral, DataCreditoDataInfMicroPerfilGeneralBO>
	{
		#region Metodos Base
		public DataCreditoDataInfMicroPerfilGeneralRepositorio() : base()
		{
		}
		public DataCreditoDataInfMicroPerfilGeneralRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataInfMicroPerfilGeneralBO> GetBy(Expression<Func<TDataCreditoDataInfMicroPerfilGeneral, bool>> filter)
		{
			IEnumerable<TDataCreditoDataInfMicroPerfilGeneral> listado = base.GetBy(filter);
			List<DataCreditoDataInfMicroPerfilGeneralBO> listadoBO = new List<DataCreditoDataInfMicroPerfilGeneralBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataInfMicroPerfilGeneralBO objetoBO = Mapper.Map<TDataCreditoDataInfMicroPerfilGeneral, DataCreditoDataInfMicroPerfilGeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataInfMicroPerfilGeneralBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataInfMicroPerfilGeneral entidad = base.FirstById(id);
				DataCreditoDataInfMicroPerfilGeneralBO objetoBO = new DataCreditoDataInfMicroPerfilGeneralBO();
				Mapper.Map<TDataCreditoDataInfMicroPerfilGeneral, DataCreditoDataInfMicroPerfilGeneralBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataInfMicroPerfilGeneralBO FirstBy(Expression<Func<TDataCreditoDataInfMicroPerfilGeneral, bool>> filter)
		{
			try
			{
				TDataCreditoDataInfMicroPerfilGeneral entidad = base.FirstBy(filter);
				DataCreditoDataInfMicroPerfilGeneralBO objetoBO = Mapper.Map<TDataCreditoDataInfMicroPerfilGeneral, DataCreditoDataInfMicroPerfilGeneralBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataInfMicroPerfilGeneralBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataInfMicroPerfilGeneral entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataInfMicroPerfilGeneralBO> listadoBO)
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

		public bool Update(DataCreditoDataInfMicroPerfilGeneralBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataInfMicroPerfilGeneral entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataInfMicroPerfilGeneralBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataInfMicroPerfilGeneral entidad, DataCreditoDataInfMicroPerfilGeneralBO objetoBO)
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

		private TDataCreditoDataInfMicroPerfilGeneral MapeoEntidad(DataCreditoDataInfMicroPerfilGeneralBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataInfMicroPerfilGeneral entidad = new TDataCreditoDataInfMicroPerfilGeneral();
				entidad = Mapper.Map<DataCreditoDataInfMicroPerfilGeneralBO, TDataCreditoDataInfMicroPerfilGeneral>(objetoBO,
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
