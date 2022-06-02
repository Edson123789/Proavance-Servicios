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
	public class FacebookPaginaRepositorio : BaseRepository<TFacebookPagina, FacebookPaginaBO>
	{
		#region Metodos Base
		public FacebookPaginaRepositorio() : base()
		{
		}
		public FacebookPaginaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<FacebookPaginaBO> GetBy(Expression<Func<TFacebookPagina, bool>> filter)
		{
			IEnumerable<TFacebookPagina> listado = base.GetBy(filter);
			List<FacebookPaginaBO> listadoBO = new List<FacebookPaginaBO>();
			foreach (var itemEntidad in listado)
			{
				FacebookPaginaBO objetoBO = Mapper.Map<TFacebookPagina, FacebookPaginaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public FacebookPaginaBO FirstById(int id)
		{
			try
			{
				TFacebookPagina entidad = base.FirstById(id);
				FacebookPaginaBO objetoBO = new FacebookPaginaBO();
				Mapper.Map<TFacebookPagina, FacebookPaginaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public FacebookPaginaBO FirstBy(Expression<Func<TFacebookPagina, bool>> filter)
		{
			try
			{
				TFacebookPagina entidad = base.FirstBy(filter);
				FacebookPaginaBO objetoBO = Mapper.Map<TFacebookPagina, FacebookPaginaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(FacebookPaginaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TFacebookPagina entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<FacebookPaginaBO> listadoBO)
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

		public bool Update(FacebookPaginaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TFacebookPagina entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<FacebookPaginaBO> listadoBO)
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
		private void AsignacionId(TFacebookPagina entidad, FacebookPaginaBO objetoBO)
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

		private TFacebookPagina MapeoEntidad(FacebookPaginaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TFacebookPagina entidad = new TFacebookPagina();
				entidad = Mapper.Map<FacebookPaginaBO, TFacebookPagina>(objetoBO,
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


        public FacebookAplicacionPaginaTokenDTO ObtenerTokenAcceso(string facebookPAginaId, int idFacebookAplicacion)
        {
            try
            {
                FacebookAplicacionPaginaTokenDTO facebookAplicacionPaginaTokenDTO = new FacebookAplicacionPaginaTokenDTO();
                var _query = "SELECT IdFacebookPagina, Token FROM mkt.V_ObtenerToken_FacebookAplicacionPagina WHERE FacebookPaginaId = @facebookPAginaId AND IdFacebookAplicacion = @idFacebookAplicacion";
                var respuesta = _dapper.FirstOrDefault(_query, new { facebookPAginaId, idFacebookAplicacion });
                if (!respuesta.Contains("[]") || !respuesta.Contains("null") || !respuesta.Contains(""))
                {
                    facebookAplicacionPaginaTokenDTO = JsonConvert.DeserializeObject<FacebookAplicacionPaginaTokenDTO>(respuesta);
                }
                return facebookAplicacionPaginaTokenDTO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
