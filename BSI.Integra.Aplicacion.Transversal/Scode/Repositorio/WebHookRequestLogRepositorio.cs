using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class WebhookRequestLogRepositorio : BaseRepository<TWebhookRequestLog, WebHookRequestLogBO>
    {
        #region Metodos Base
        public WebhookRequestLogRepositorio() : base()
        {
        }
        public WebhookRequestLogRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WebHookRequestLogBO> GetBy(Expression<Func<TWebhookRequestLog, bool>> filter)
        {
            IEnumerable<TWebhookRequestLog> listado = base.GetBy(filter);
            List<WebHookRequestLogBO> listadoBO = new List<WebHookRequestLogBO>();
            foreach (var itemEntidad in listado)
            {
                WebHookRequestLogBO objetoBO = Mapper.Map<TWebhookRequestLog, WebHookRequestLogBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WebHookRequestLogBO FirstById(int id)
        {
            try
            {
                TWebhookRequestLog entidad = base.FirstById(id);
                WebHookRequestLogBO objetoBO = new WebHookRequestLogBO();
                Mapper.Map<TWebhookRequestLog, WebHookRequestLogBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WebHookRequestLogBO FirstBy(Expression<Func<TWebhookRequestLog, bool>> filter)
        {
            try
            {
                TWebhookRequestLog entidad = base.FirstBy(filter);
                WebHookRequestLogBO objetoBO = Mapper.Map<TWebhookRequestLog, WebHookRequestLogBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WebHookRequestLogBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWebhookRequestLog entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WebHookRequestLogBO> listadoBO)
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

        public bool Update(WebHookRequestLogBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWebhookRequestLog entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WebHookRequestLogBO> listadoBO)
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
        private void AsignacionId(TWebhookRequestLog entidad, WebHookRequestLogBO objetoBO)
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

        private TWebhookRequestLog MapeoEntidad(WebHookRequestLogBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWebhookRequestLog entidad = new TWebhookRequestLog();
                entidad = Mapper.Map<WebHookRequestLogBO, TWebhookRequestLog>(objetoBO,
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
