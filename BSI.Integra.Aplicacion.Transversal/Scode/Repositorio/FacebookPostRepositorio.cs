using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class FacebookPostRepositorio : BaseRepository<TFacebookPost, FacebookPostBO>
    {
        #region Metodos Base
        public FacebookPostRepositorio() : base()
        {
        }
        public FacebookPostRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FacebookPostBO> GetBy(Expression<Func<TFacebookPost, bool>> filter)
        {
            IEnumerable<TFacebookPost> listado = base.GetBy(filter);
            List<FacebookPostBO> listadoBO = new List<FacebookPostBO>();
            foreach (var itemEntidad in listado)
            {
                FacebookPostBO objetoBO = Mapper.Map<TFacebookPost, FacebookPostBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FacebookPostBO FirstById(int id)
        {
            try
            {
                TFacebookPost entidad = base.FirstById(id);
                FacebookPostBO objetoBO = new FacebookPostBO();
                Mapper.Map<TFacebookPost, FacebookPostBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FacebookPostBO FirstBy(Expression<Func<TFacebookPost, bool>> filter)
        {
            try
            {
                TFacebookPost entidad = base.FirstBy(filter);
                FacebookPostBO objetoBO = Mapper.Map<TFacebookPost, FacebookPostBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FacebookPostBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFacebookPost entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FacebookPostBO> listadoBO)
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

        public bool Update(FacebookPostBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFacebookPost entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FacebookPostBO> listadoBO)
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
        private void AsignacionId(TFacebookPost entidad, FacebookPostBO objetoBO)
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

        private TFacebookPost MapeoEntidad(FacebookPostBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFacebookPost entidad = new TFacebookPost();
                entidad = Mapper.Map<FacebookPostBO, TFacebookPost>(objetoBO,
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

        public PostAreaFacebookDTO ObtenerArea(string idPost)
        {
			try
			{
				string _queryArea = "SELECT IdArea FROM com.V_TFacebookPost_ObtenerArea WHERE Estado=1";
				var queryArea = _dapper.FirstOrDefault(_queryArea, new { Id = idPost });
				return JsonConvert.DeserializeObject<PostAreaFacebookDTO>(queryArea);
			}
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
            
        }
    }
}
