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
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
	public class InstagramComentarioRepositorio : BaseRepository<TInstagramComentario, InstagramComentarioBO>
	{
		#region Metodos Base
		public InstagramComentarioRepositorio() : base()
		{
		}
		public InstagramComentarioRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<InstagramComentarioBO> GetBy(Expression<Func<TInstagramComentario, bool>> filter)
		{
			IEnumerable<TInstagramComentario> listado = base.GetBy(filter);
			List<InstagramComentarioBO> listadoBO = new List<InstagramComentarioBO>();
			foreach (var itemEntidad in listado)
			{
				InstagramComentarioBO objetoBO = Mapper.Map<TInstagramComentario, InstagramComentarioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public InstagramComentarioBO FirstById(int id)
		{
			try
			{
				TInstagramComentario entidad = base.FirstById(id);
				InstagramComentarioBO objetoBO = new InstagramComentarioBO();
				Mapper.Map<TInstagramComentario, InstagramComentarioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public InstagramComentarioBO FirstBy(Expression<Func<TInstagramComentario, bool>> filter)
		{
			try
			{
				TInstagramComentario entidad = base.FirstBy(filter);
				InstagramComentarioBO objetoBO = Mapper.Map<TInstagramComentario, InstagramComentarioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(InstagramComentarioBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TInstagramComentario entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<InstagramComentarioBO> listadoBO)
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

		public bool Update(InstagramComentarioBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TInstagramComentario entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<InstagramComentarioBO> listadoBO)
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
		private void AsignacionId(TInstagramComentario entidad, InstagramComentarioBO objetoBO)
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

		private TInstagramComentario MapeoEntidad(InstagramComentarioBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TInstagramComentario entidad = new TInstagramComentario();
				entidad = Mapper.Map<InstagramComentarioBO, TInstagramComentario>(objetoBO,
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
        /// Se obtiene los comentarios asociados a un asesor
        /// </summary>
        /// <param name="IdPersonal"></param>
        /// <returns></returns>
        public List<InstagramComentarioPersonalDTO> ObtenerComentariosPorIdPersonal(int IdPersonal)
        {
            try
            {
                string datos = _dapper.QuerySPDapper("mkt.SP_ObtenerComentarioInstagramPorPersonal", new { IdPersonal });
                return JsonConvert.DeserializeObject<List<InstagramComentarioPersonalDTO>>(datos);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// SE obtiene el detalle de los comentarios de un Usuario
        /// </summary>
        /// <param name="IdInstagramUsuario"></param>
        /// <param name="IdInstagramPublicacion"></param>
        /// <returns></returns>
        public List<InstagramComentarioUsuarioPublicacionDTO> ObtenerComentariosPorUsuarioPublicacion(int IdInstagramUsuario, int IdInstagramPublicacion)
        {
            try
            {
                string datos = _dapper.QuerySPDapper("mkt.SP_ObtenerComentarioInstagramPorUsuario", new { IdInstagramUsuario, IdInstagramPublicacion});
                return JsonConvert.DeserializeObject<List<InstagramComentarioUsuarioPublicacionDTO>>(datos);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
