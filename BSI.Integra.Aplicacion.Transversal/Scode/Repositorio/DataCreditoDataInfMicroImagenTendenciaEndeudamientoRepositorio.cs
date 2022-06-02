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
    public class DataCreditoDataInfMicroImagenTendenciaEndeudamientoRepositorio : BaseRepository<TDataCreditoDataInfMicroImagenTendenciaEndeudamiento, DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO>
	{
		#region Metodos Base
		public DataCreditoDataInfMicroImagenTendenciaEndeudamientoRepositorio() : base()
		{
		}
		public DataCreditoDataInfMicroImagenTendenciaEndeudamientoRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO> GetBy(Expression<Func<TDataCreditoDataInfMicroImagenTendenciaEndeudamiento, bool>> filter)
		{
			IEnumerable<TDataCreditoDataInfMicroImagenTendenciaEndeudamiento> listado = base.GetBy(filter);
			List<DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO> listadoBO = new List<DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO>();
			foreach (var itemEntidad in listado)
			{
				DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO objetoBO = Mapper.Map<TDataCreditoDataInfMicroImagenTendenciaEndeudamiento, DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO FirstById(int id)
		{
			try
			{
				TDataCreditoDataInfMicroImagenTendenciaEndeudamiento entidad = base.FirstById(id);
				DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO objetoBO = new DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO();
				Mapper.Map<TDataCreditoDataInfMicroImagenTendenciaEndeudamiento, DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO FirstBy(Expression<Func<TDataCreditoDataInfMicroImagenTendenciaEndeudamiento, bool>> filter)
		{
			try
			{
				TDataCreditoDataInfMicroImagenTendenciaEndeudamiento entidad = base.FirstBy(filter);
				DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO objetoBO = Mapper.Map<TDataCreditoDataInfMicroImagenTendenciaEndeudamiento, DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TDataCreditoDataInfMicroImagenTendenciaEndeudamiento entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO> listadoBO)
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

		public bool Update(DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TDataCreditoDataInfMicroImagenTendenciaEndeudamiento entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO> listadoBO)
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
		private void AsignacionId(TDataCreditoDataInfMicroImagenTendenciaEndeudamiento entidad, DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO objetoBO)
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

		private TDataCreditoDataInfMicroImagenTendenciaEndeudamiento MapeoEntidad(DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TDataCreditoDataInfMicroImagenTendenciaEndeudamiento entidad = new TDataCreditoDataInfMicroImagenTendenciaEndeudamiento();
				entidad = Mapper.Map<DataCreditoDataInfMicroImagenTendenciaEndeudamientoBO, TDataCreditoDataInfMicroImagenTendenciaEndeudamiento>(objetoBO,
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
