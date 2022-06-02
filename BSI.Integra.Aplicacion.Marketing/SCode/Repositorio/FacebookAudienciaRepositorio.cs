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
	public class FacebookAudienciaRepositorio : BaseRepository<TFacebookAudiencia, FacebookAudienciaBO>
	{
		#region Metodos Base
		public FacebookAudienciaRepositorio() : base()
		{
		}
		public FacebookAudienciaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<FacebookAudienciaBO> GetBy(Expression<Func<TFacebookAudiencia, bool>> filter)
		{
			IEnumerable<TFacebookAudiencia> listado = base.GetBy(filter);
			List<FacebookAudienciaBO> listadoBO = new List<FacebookAudienciaBO>();
			foreach (var itemEntidad in listado)
			{
				FacebookAudienciaBO objetoBO = Mapper.Map<TFacebookAudiencia, FacebookAudienciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public FacebookAudienciaBO FirstById(int id)
		{
			try
			{
				TFacebookAudiencia entidad = base.FirstById(id);
				FacebookAudienciaBO objetoBO = new FacebookAudienciaBO();
				Mapper.Map<TFacebookAudiencia, FacebookAudienciaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public FacebookAudienciaBO FirstBy(Expression<Func<TFacebookAudiencia, bool>> filter)
		{
			try
			{
				TFacebookAudiencia entidad = base.FirstBy(filter);
				FacebookAudienciaBO objetoBO = Mapper.Map<TFacebookAudiencia, FacebookAudienciaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(FacebookAudienciaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TFacebookAudiencia entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<FacebookAudienciaBO> listadoBO)
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

		public bool Update(FacebookAudienciaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TFacebookAudiencia entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<FacebookAudienciaBO> listadoBO)
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
		private void AsignacionId(TFacebookAudiencia entidad, FacebookAudienciaBO objetoBO)
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

		private TFacebookAudiencia MapeoEntidad(FacebookAudienciaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TFacebookAudiencia entidad = new TFacebookAudiencia();
				entidad = Mapper.Map<FacebookAudienciaBO, TFacebookAudiencia>(objetoBO,
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
        /// Obtiene todos los registros para combos.
        /// </summary>
        /// <returns></returns>
        public List<FacebookAudienciaComboDTO> ObtenerCombo()
        {
            try
            {
                List<FacebookAudienciaComboDTO> listaFacebookAudiencia = new List<FacebookAudienciaComboDTO>();
                listaFacebookAudiencia = GetBy(x => true, y => new FacebookAudienciaComboDTO
                {
                    FacebookIdAudiencia = y.FacebookIdAudiencia,
                    Nombre = y.Nombre
                }).ToList();
                return listaFacebookAudiencia;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<FacebookAudienciaHistorialDTO> ObtenerHistorialPorIdFiltroSegmento(int idFiltroSegmento)
        {
            try
            {
                List<FacebookAudienciaHistorialDTO> listaFacebookAudienciaHistorial = new List<FacebookAudienciaHistorialDTO>();
                var _query = "SELECT NombreCuentaPublicitaria, FacebookIdCuentaPublicitaria, FacebookIdAudiencia, Nombre, FechaModificacion, Subtipo FROM mkt.V_ObtenerAudienciaCuentaPublicitaria WHERE IdFiltroSegmento = @idFiltroSegmento AND EstadoFacebookAudiencia = 1 AND EstadoFacebookAudienciaCuentaPublicitaria = 1 AND Origen = 'Propio'";
                var respuestaQuery = _dapper.QueryDapper(_query, new { idFiltroSegmento });
                if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Contains("[]") && !respuestaQuery.Contains("null"))
                {
                    listaFacebookAudienciaHistorial = JsonConvert.DeserializeObject<List<FacebookAudienciaHistorialDTO>>(respuestaQuery);
                }
                return listaFacebookAudienciaHistorial;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
