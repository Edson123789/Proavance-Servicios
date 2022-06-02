using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: InteraccionEnlaceMailchimp
    /// Autor: Wilber Choque - Gian Miranda
    /// Fecha: 05/07/2021
    /// <summary>
    /// Interaccion con la tabla mkt.T_InteraccionEnlaceMailchimp
    /// </summary>
    public class InteraccionEnlaceMailchimpRepositorio : BaseRepository<TInteraccionEnlaceMailchimp, InteraccionEnlaceMailchimpBO>
    {
        #region Metodos Base
        public InteraccionEnlaceMailchimpRepositorio() : base()
        {
        }
        public InteraccionEnlaceMailchimpRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<InteraccionEnlaceMailchimpBO> GetBy(Expression<Func<TInteraccionEnlaceMailchimp, bool>> filter)
        {
            IEnumerable<TInteraccionEnlaceMailchimp> listado = base.GetBy(filter);
            List<InteraccionEnlaceMailchimpBO> listadoBO = new List<InteraccionEnlaceMailchimpBO>();
            foreach (var itemEntidad in listado)
            {
                InteraccionEnlaceMailchimpBO objetoBO = Mapper.Map<TInteraccionEnlaceMailchimp, InteraccionEnlaceMailchimpBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public InteraccionEnlaceMailchimpBO FirstById(int id)
        {
            try
            {
                TInteraccionEnlaceMailchimp entidad = base.FirstById(id);
                InteraccionEnlaceMailchimpBO objetoBO = new InteraccionEnlaceMailchimpBO();
                Mapper.Map<TInteraccionEnlaceMailchimp, InteraccionEnlaceMailchimpBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public InteraccionEnlaceMailchimpBO FirstBy(Expression<Func<TInteraccionEnlaceMailchimp, bool>> filter)
        {
            try
            {
                TInteraccionEnlaceMailchimp entidad = base.FirstBy(filter);
                InteraccionEnlaceMailchimpBO objetoBO = Mapper.Map<TInteraccionEnlaceMailchimp, InteraccionEnlaceMailchimpBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(InteraccionEnlaceMailchimpBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TInteraccionEnlaceMailchimp entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<InteraccionEnlaceMailchimpBO> listadoBO)
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

        public bool Update(InteraccionEnlaceMailchimpBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TInteraccionEnlaceMailchimp entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<InteraccionEnlaceMailchimpBO> listadoBO)
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
        private void AsignacionId(TInteraccionEnlaceMailchimp entidad, InteraccionEnlaceMailchimpBO objetoBO)
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

        private TInteraccionEnlaceMailchimp MapeoEntidad(InteraccionEnlaceMailchimpBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TInteraccionEnlaceMailchimp entidad = new TInteraccionEnlaceMailchimp();
                entidad = Mapper.Map<InteraccionEnlaceMailchimpBO, TInteraccionEnlaceMailchimp>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<InteraccionEnlaceMailchimpBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TInteraccionEnlaceMailchimp, bool>>> filters, Expression<Func<TInteraccionEnlaceMailchimp, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TInteraccionEnlaceMailchimp> listado = base.GetFiltered(filters, orderBy, ascending);
            List<InteraccionEnlaceMailchimpBO> listadoBO = new List<InteraccionEnlaceMailchimpBO>();

            foreach (var itemEntidad in listado)
            {
                InteraccionEnlaceMailchimpBO objetoBO = Mapper.Map<TInteraccionEnlaceMailchimp, InteraccionEnlaceMailchimpBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Obtiene los datos completo para obtener la interaccion enlace mailchimp
        /// </summary>
        /// <param name="campaniaMailChimpId">Id original de la campania de Mailchimp</param>
        /// <returns>Lista de objetos de clase InteraccionEnlaceMailchimpBO</returns>
        public List<InteraccionEnlaceMailchimpBO> InteraccionEnlaceMailchimpCompletoPorCampaniaMailChimpId(string campaniaMailChimpId)
        {
            try
            {
                List<InteraccionEnlaceMailchimpBO> listaInteraccionEnlaceMailchimp = null;
                
                var query = "SELECT * FROM mkt.V_TInteraccionEnlaceMailchimpCompleto_PorCampaniaMailChimpId WHERE CampaniaMailChimpId = @campaniaMailChimpId";
                var queryRespuesta = _dapper.QueryDapper(query, new { campaniaMailChimpId });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    listaInteraccionEnlaceMailchimp = JsonConvert.DeserializeObject<List<InteraccionEnlaceMailchimpBO>>(queryRespuesta);
                }
                return listaInteraccionEnlaceMailchimp;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
