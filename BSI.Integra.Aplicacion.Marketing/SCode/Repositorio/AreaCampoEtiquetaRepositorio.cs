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
	public class AreaCampoEtiquetaRepositorio : BaseRepository<TAreaCampoEtiqueta, AreaCampoEtiquetaBO>
	{
		#region Metodos Base
		public AreaCampoEtiquetaRepositorio() : base()
		{
		}
		public AreaCampoEtiquetaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<AreaCampoEtiquetaBO> GetBy(Expression<Func<TAreaCampoEtiqueta, bool>> filter)
		{
			IEnumerable<TAreaCampoEtiqueta> listado = base.GetBy(filter);
			List<AreaCampoEtiquetaBO> listadoBO = new List<AreaCampoEtiquetaBO>();
			foreach (var itemEntidad in listado)
			{
				AreaCampoEtiquetaBO objetoBO = Mapper.Map<TAreaCampoEtiqueta, AreaCampoEtiquetaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public AreaCampoEtiquetaBO FirstById(int id)
		{
			try
			{
				TAreaCampoEtiqueta entidad = base.FirstById(id);
				AreaCampoEtiquetaBO objetoBO = new AreaCampoEtiquetaBO();
				Mapper.Map<TAreaCampoEtiqueta, AreaCampoEtiquetaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public AreaCampoEtiquetaBO FirstBy(Expression<Func<TAreaCampoEtiqueta, bool>> filter)
		{
			try
			{
				TAreaCampoEtiqueta entidad = base.FirstBy(filter);
				AreaCampoEtiquetaBO objetoBO = Mapper.Map<TAreaCampoEtiqueta, AreaCampoEtiquetaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(AreaCampoEtiquetaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TAreaCampoEtiqueta entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<AreaCampoEtiquetaBO> listadoBO)
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

		public bool Update(AreaCampoEtiquetaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TAreaCampoEtiqueta entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<AreaCampoEtiquetaBO> listadoBO)
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
		private void AsignacionId(TAreaCampoEtiqueta entidad, AreaCampoEtiquetaBO objetoBO)
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

		private TAreaCampoEtiqueta MapeoEntidad(AreaCampoEtiquetaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TAreaCampoEtiqueta entidad = new TAreaCampoEtiqueta();
				entidad = Mapper.Map<AreaCampoEtiquetaBO, TAreaCampoEtiqueta>(objetoBO,
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
        /// Obtiene el Id y nombre de todos los registros
        /// </summary>
        /// <returns></returns>
		public List<AreaCampoEtiquetaDTO> ObtenerAreas()
        {
            try
            {
                return GetBy(x => true, y => new AreaCampoEtiquetaDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre
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
