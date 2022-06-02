using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
	public class FacebookAudienciaCuentaPublicitariaRepositorio : BaseRepository<TFacebookAudienciaCuentaPublicitaria, FacebookAudienciaCuentaPublicitariaBO>
	{
		#region Metodos Base
		public FacebookAudienciaCuentaPublicitariaRepositorio() : base()
		{
		}
		public FacebookAudienciaCuentaPublicitariaRepositorio(integraDBContext contexto) : base(contexto)
		{
		}
		public IEnumerable<FacebookAudienciaCuentaPublicitariaBO> GetBy(Expression<Func<TFacebookAudienciaCuentaPublicitaria, bool>> filter)
		{
			IEnumerable<TFacebookAudienciaCuentaPublicitaria> listado = base.GetBy(filter);
			List<FacebookAudienciaCuentaPublicitariaBO> listadoBO = new List<FacebookAudienciaCuentaPublicitariaBO>();
			foreach (var itemEntidad in listado)
			{
				FacebookAudienciaCuentaPublicitariaBO objetoBO = Mapper.Map<TFacebookAudienciaCuentaPublicitaria, FacebookAudienciaCuentaPublicitariaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
				listadoBO.Add(objetoBO);
			}

			return listadoBO;
		}
		public FacebookAudienciaCuentaPublicitariaBO FirstById(int id)
		{
			try
			{
				TFacebookAudienciaCuentaPublicitaria entidad = base.FirstById(id);
				FacebookAudienciaCuentaPublicitariaBO objetoBO = new FacebookAudienciaCuentaPublicitariaBO();
				Mapper.Map<TFacebookAudienciaCuentaPublicitaria, FacebookAudienciaCuentaPublicitariaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}
		public FacebookAudienciaCuentaPublicitariaBO FirstBy(Expression<Func<TFacebookAudienciaCuentaPublicitaria, bool>> filter)
		{
			try
			{
				TFacebookAudienciaCuentaPublicitaria entidad = base.FirstBy(filter);
				FacebookAudienciaCuentaPublicitariaBO objetoBO = Mapper.Map<TFacebookAudienciaCuentaPublicitaria, FacebookAudienciaCuentaPublicitariaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

				return objetoBO;
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}

		public bool Insert(FacebookAudienciaCuentaPublicitariaBO objetoBO)
		{
			try
			{
				//mapeo de la entidad
				TFacebookAudienciaCuentaPublicitaria entidad = MapeoEntidad(objetoBO);

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

		public bool Insert(IEnumerable<FacebookAudienciaCuentaPublicitariaBO> listadoBO)
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

		public bool Update(FacebookAudienciaCuentaPublicitariaBO objetoBO)
		{
			try
			{
				if (objetoBO == null)
				{
					throw new ArgumentNullException("Entidad nula");
				}

				//mapeo de la entidad
				TFacebookAudienciaCuentaPublicitaria entidad = MapeoEntidad(objetoBO);

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

		public bool Update(IEnumerable<FacebookAudienciaCuentaPublicitariaBO> listadoBO)
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
		private void AsignacionId(TFacebookAudienciaCuentaPublicitaria entidad, FacebookAudienciaCuentaPublicitariaBO objetoBO)
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

		private TFacebookAudienciaCuentaPublicitaria MapeoEntidad(FacebookAudienciaCuentaPublicitariaBO objetoBO)
		{
			try
			{
				//crea la entidad padre
				TFacebookAudienciaCuentaPublicitaria entidad = new TFacebookAudienciaCuentaPublicitaria();
				entidad = Mapper.Map<FacebookAudienciaCuentaPublicitariaBO, TFacebookAudienciaCuentaPublicitaria>(objetoBO,
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

        public List<FacebookAudienciaCuentaPublicitariaBO> ObtenerFacebookAudienciaCuentaPublicitaria(int IdConjuntoListaDetalle)
        {
            try
            {
                List<FacebookAudienciaCuentaPublicitariaBO> conjuntoListaDetalle = new List<FacebookAudienciaCuentaPublicitariaBO>();

                conjuntoListaDetalle = GetBy(w => w.Estado == true && w.IdConjuntoListaDetalle == IdConjuntoListaDetalle).ToList();

                return conjuntoListaDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }
    }
}
