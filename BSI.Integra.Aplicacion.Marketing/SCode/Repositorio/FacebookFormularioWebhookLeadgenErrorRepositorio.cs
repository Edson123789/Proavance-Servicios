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
    public class FacebookFormularioWebhookLeadgenErrorRepositorio : BaseRepository<TFacebookFormularioWebhookLeadgenError, FacebookFormularioWebhookLeadgenErrorBO>
    {
        #region Metodos Base
        public FacebookFormularioWebhookLeadgenErrorRepositorio() : base()
        {
        }
        public FacebookFormularioWebhookLeadgenErrorRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FacebookFormularioWebhookLeadgenErrorBO> GetBy(Expression<Func<TFacebookFormularioWebhookLeadgenError, bool>> filter)
        {
            IEnumerable<TFacebookFormularioWebhookLeadgenError> listado = base.GetBy(filter);
            List<FacebookFormularioWebhookLeadgenErrorBO> listadoBO = new List<FacebookFormularioWebhookLeadgenErrorBO>();
            foreach (var itemEntidad in listado)
            {
                FacebookFormularioWebhookLeadgenErrorBO objetoBO = Mapper.Map<TFacebookFormularioWebhookLeadgenError, FacebookFormularioWebhookLeadgenErrorBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FacebookFormularioWebhookLeadgenErrorBO FirstById(int id)
        {
            try
            {
                TFacebookFormularioWebhookLeadgenError entidad = base.FirstById(id);
                FacebookFormularioWebhookLeadgenErrorBO objetoBO = new FacebookFormularioWebhookLeadgenErrorBO();
                Mapper.Map<TFacebookFormularioWebhookLeadgenError, FacebookFormularioWebhookLeadgenErrorBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FacebookFormularioWebhookLeadgenErrorBO FirstBy(Expression<Func<TFacebookFormularioWebhookLeadgenError, bool>> filter)
        {
            try
            {
                TFacebookFormularioWebhookLeadgenError entidad = base.FirstBy(filter);
                FacebookFormularioWebhookLeadgenErrorBO objetoBO = Mapper.Map<TFacebookFormularioWebhookLeadgenError, FacebookFormularioWebhookLeadgenErrorBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FacebookFormularioWebhookLeadgenErrorBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFacebookFormularioWebhookLeadgenError entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FacebookFormularioWebhookLeadgenErrorBO> listadoBO)
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

        public bool Update(FacebookFormularioWebhookLeadgenErrorBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFacebookFormularioWebhookLeadgenError entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FacebookFormularioWebhookLeadgenErrorBO> listadoBO)
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
        private void AsignacionId(TFacebookFormularioWebhookLeadgenError entidad, FacebookFormularioWebhookLeadgenErrorBO objetoBO)
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

        private TFacebookFormularioWebhookLeadgenError MapeoEntidad(FacebookFormularioWebhookLeadgenErrorBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFacebookFormularioWebhookLeadgenError entidad = new TFacebookFormularioWebhookLeadgenError();
                entidad = Mapper.Map<FacebookFormularioWebhookLeadgenErrorBO, TFacebookFormularioWebhookLeadgenError>(objetoBO,
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
