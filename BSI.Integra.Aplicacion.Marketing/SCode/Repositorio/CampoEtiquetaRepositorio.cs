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
	public class CampoEtiquetaRepositorio : BaseRepository<TCampoEtiqueta, CampoEtiquetaBO>
	{
		#region Metodos Base
		public CampoEtiquetaRepositorio() : base()
		{
		}
		public CampoEtiquetaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<CampoEtiquetaBO> GetBy(Expression<Func<TCampoEtiqueta, bool>> filter)
		{
			IEnumerable<TCampoEtiqueta> listado = base.GetBy(filter);
			List<CampoEtiquetaBO> listadoBO = new List<CampoEtiquetaBO>();
			foreach (var itemEntidad in listado)
			{
				CampoEtiquetaBO objetoBO = Mapper.Map<TCampoEtiqueta, CampoEtiquetaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public CampoEtiquetaBO FirstById(int id)
		{
			try
			{
				TCampoEtiqueta entidad = base.FirstById(id);
				CampoEtiquetaBO objetoBO = new CampoEtiquetaBO();
				Mapper.Map<TCampoEtiqueta, CampoEtiquetaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public CampoEtiquetaBO FirstBy(Expression<Func<TCampoEtiqueta, bool>> filter)
		{
			try
			{
				TCampoEtiqueta entidad = base.FirstBy(filter);
				CampoEtiquetaBO objetoBO = Mapper.Map<TCampoEtiqueta, CampoEtiquetaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(CampoEtiquetaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TCampoEtiqueta entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<CampoEtiquetaBO> listadoBO)
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

		public bool Update(CampoEtiquetaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TCampoEtiqueta entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<CampoEtiquetaBO> listadoBO)
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
		private void AsignacionId(TCampoEtiqueta entidad, CampoEtiquetaBO objetoBO)
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

		private TCampoEtiqueta MapeoEntidad(CampoEtiquetaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TCampoEtiqueta entidad = new TCampoEtiqueta();
				entidad = Mapper.Map<CampoEtiquetaBO, TCampoEtiqueta>(objetoBO,
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
        /// Obtiene los registros por el IdSubArea.
        /// </summary>
        /// <param name="idSubArea"></param>
        /// <returns></returns>
        public List<CampoEtiquetaDTO> ObtenerCampoEtiquetaPorSubArea(int idSubArea)
        {
            try
            {
                return GetBy(x => x.IdSubAreaCampoEtiqueta == idSubArea, y => new CampoEtiquetaDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                    Campo = y.Campo, 
                    IdSubAreaEtiqueta = y.IdSubAreaCampoEtiqueta
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
