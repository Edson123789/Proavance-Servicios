using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
	public class SubAreaCampoEtiquetaRepositorio : BaseRepository<TSubAreaCampoEtiqueta, SubAreaCampoEtiquetaBO>
	{
		#region Metodos Base
		public SubAreaCampoEtiquetaRepositorio() : base()
		{
		}
		public SubAreaCampoEtiquetaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<SubAreaCampoEtiquetaBO> GetBy(Expression<Func<TSubAreaCampoEtiqueta, bool>> filter)
		{
			IEnumerable<TSubAreaCampoEtiqueta> listado = base.GetBy(filter);
			List<SubAreaCampoEtiquetaBO> listadoBO = new List<SubAreaCampoEtiquetaBO>();
			foreach (var itemEntidad in listado)
			{
				SubAreaCampoEtiquetaBO objetoBO = Mapper.Map<TSubAreaCampoEtiqueta, SubAreaCampoEtiquetaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public SubAreaCampoEtiquetaBO FirstById(int id)
		{
			try
			{
				TSubAreaCampoEtiqueta entidad = base.FirstById(id);
				SubAreaCampoEtiquetaBO objetoBO = new SubAreaCampoEtiquetaBO();
				Mapper.Map<TSubAreaCampoEtiqueta, SubAreaCampoEtiquetaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public SubAreaCampoEtiquetaBO FirstBy(Expression<Func<TSubAreaCampoEtiqueta, bool>> filter)
		{
			try
			{
				TSubAreaCampoEtiqueta entidad = base.FirstBy(filter);
				SubAreaCampoEtiquetaBO objetoBO = Mapper.Map<TSubAreaCampoEtiqueta, SubAreaCampoEtiquetaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(SubAreaCampoEtiquetaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TSubAreaCampoEtiqueta entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<SubAreaCampoEtiquetaBO> listadoBO)
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

		public bool Update(SubAreaCampoEtiquetaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TSubAreaCampoEtiqueta entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<SubAreaCampoEtiquetaBO> listadoBO)
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
		private void AsignacionId(TSubAreaCampoEtiqueta entidad, SubAreaCampoEtiquetaBO objetoBO)
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

		private TSubAreaCampoEtiqueta MapeoEntidad(SubAreaCampoEtiquetaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TSubAreaCampoEtiqueta entidad = new TSubAreaCampoEtiqueta();
				entidad = Mapper.Map<SubAreaCampoEtiquetaBO, TSubAreaCampoEtiqueta>(objetoBO,
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

        /// <summary>
        /// Obtiene las subareas de una Area
        /// </summary>
        /// <returns></returns>
        public List<SubAreaCampoEtiquetaDTO> ObtenerSubAreaPorArea(int idArea)
        {
            try
            {
                return GetBy(x => x.IdAreaCampoEtiqueta == idArea, y => new SubAreaCampoEtiquetaDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    IdAreaEtiqueta = y.IdAreaCampoEtiqueta
                }
                ).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
