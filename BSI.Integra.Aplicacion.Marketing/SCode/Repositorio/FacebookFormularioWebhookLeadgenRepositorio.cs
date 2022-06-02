using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class FacebookFormularioWebhookLeadgenRepositorio : BaseRepository<TFacebookFormularioWebhookLeadgen, FacebookFormularioWebhookLeadgenBO>
    {
        #region Metodos Base
        public FacebookFormularioWebhookLeadgenRepositorio() : base()
        {
        }
        public FacebookFormularioWebhookLeadgenRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FacebookFormularioWebhookLeadgenBO> GetBy(Expression<Func<TFacebookFormularioWebhookLeadgen, bool>> filter)
        {
            IEnumerable<TFacebookFormularioWebhookLeadgen> listado = base.GetBy(filter);
            List<FacebookFormularioWebhookLeadgenBO> listadoBO = new List<FacebookFormularioWebhookLeadgenBO>();
            foreach (var itemEntidad in listado)
            {
                FacebookFormularioWebhookLeadgenBO objetoBO = Mapper.Map<TFacebookFormularioWebhookLeadgen, FacebookFormularioWebhookLeadgenBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FacebookFormularioWebhookLeadgenBO FirstById(int id)
        {
            try
            {
                TFacebookFormularioWebhookLeadgen entidad = base.FirstById(id);
                FacebookFormularioWebhookLeadgenBO objetoBO = new FacebookFormularioWebhookLeadgenBO();
                Mapper.Map<TFacebookFormularioWebhookLeadgen, FacebookFormularioWebhookLeadgenBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FacebookFormularioWebhookLeadgenBO FirstBy(Expression<Func<TFacebookFormularioWebhookLeadgen, bool>> filter)
        {
            try
            {
                TFacebookFormularioWebhookLeadgen entidad = base.FirstBy(filter);
                FacebookFormularioWebhookLeadgenBO objetoBO = Mapper.Map<TFacebookFormularioWebhookLeadgen, FacebookFormularioWebhookLeadgenBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FacebookFormularioWebhookLeadgenBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFacebookFormularioWebhookLeadgen entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FacebookFormularioWebhookLeadgenBO> listadoBO)
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

        public bool Update(FacebookFormularioWebhookLeadgenBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFacebookFormularioWebhookLeadgen entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FacebookFormularioWebhookLeadgenBO> listadoBO)
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
        private void AsignacionId(TFacebookFormularioWebhookLeadgen entidad, FacebookFormularioWebhookLeadgenBO objetoBO)
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

        private TFacebookFormularioWebhookLeadgen MapeoEntidad(FacebookFormularioWebhookLeadgenBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFacebookFormularioWebhookLeadgen entidad = new TFacebookFormularioWebhookLeadgen();
                entidad = Mapper.Map<FacebookFormularioWebhookLeadgenBO, TFacebookFormularioWebhookLeadgen>(objetoBO,
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
