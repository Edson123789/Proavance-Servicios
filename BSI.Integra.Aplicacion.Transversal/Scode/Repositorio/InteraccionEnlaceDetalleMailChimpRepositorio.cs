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
    /// Repositorio: InteraccionEnlaceDetalleMailChimp
    /// Autor: Wilber Choque - Gian Miranda
    /// Fecha: 05/07/2021
    /// <summary>
    /// Interaccion con la tabla mkt.T_InteraccionEnlaceDetalleMailChimp
    /// </summary>
    public class InteraccionEnlaceDetalleMailChimpRepositorio : BaseRepository<TInteraccionEnlaceDetalleMailChimp, InteraccionEnlaceDetalleMailChimpBO>
    {
        #region Metodos Base
        public InteraccionEnlaceDetalleMailChimpRepositorio() : base()
        {
        }
        public InteraccionEnlaceDetalleMailChimpRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<InteraccionEnlaceDetalleMailChimpBO> GetBy(Expression<Func<TInteraccionEnlaceDetalleMailChimp, bool>> filter)
        {
            IEnumerable<TInteraccionEnlaceDetalleMailChimp> listado = base.GetBy(filter);
            List<InteraccionEnlaceDetalleMailChimpBO> listadoBO = new List<InteraccionEnlaceDetalleMailChimpBO>();
            foreach (var itemEntidad in listado)
            {
                InteraccionEnlaceDetalleMailChimpBO objetoBO = Mapper.Map<TInteraccionEnlaceDetalleMailChimp, InteraccionEnlaceDetalleMailChimpBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public InteraccionEnlaceDetalleMailChimpBO FirstById(int id)
        {
            try
            {
                TInteraccionEnlaceDetalleMailChimp entidad = base.FirstById(id);
                InteraccionEnlaceDetalleMailChimpBO objetoBO = new InteraccionEnlaceDetalleMailChimpBO();
                Mapper.Map<TInteraccionEnlaceDetalleMailChimp, InteraccionEnlaceDetalleMailChimpBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public InteraccionEnlaceDetalleMailChimpBO FirstBy(Expression<Func<TInteraccionEnlaceDetalleMailChimp, bool>> filter)
        {
            try
            {
                TInteraccionEnlaceDetalleMailChimp entidad = base.FirstBy(filter);
                InteraccionEnlaceDetalleMailChimpBO objetoBO = Mapper.Map<TInteraccionEnlaceDetalleMailChimp, InteraccionEnlaceDetalleMailChimpBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(InteraccionEnlaceDetalleMailChimpBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TInteraccionEnlaceDetalleMailChimp entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<InteraccionEnlaceDetalleMailChimpBO> listadoBO)
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

        public bool Update(InteraccionEnlaceDetalleMailChimpBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TInteraccionEnlaceDetalleMailChimp entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<InteraccionEnlaceDetalleMailChimpBO> listadoBO)
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
        private void AsignacionId(TInteraccionEnlaceDetalleMailChimp entidad, InteraccionEnlaceDetalleMailChimpBO objetoBO)
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

        private TInteraccionEnlaceDetalleMailChimp MapeoEntidad(InteraccionEnlaceDetalleMailChimpBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TInteraccionEnlaceDetalleMailChimp entidad = new TInteraccionEnlaceDetalleMailChimp();
                entidad = Mapper.Map<InteraccionEnlaceDetalleMailChimpBO, TInteraccionEnlaceDetalleMailChimp>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<InteraccionEnlaceDetalleMailChimpBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TInteraccionEnlaceDetalleMailChimp, bool>>> filters, Expression<Func<TInteraccionEnlaceDetalleMailChimp, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TInteraccionEnlaceDetalleMailChimp> listado = base.GetFiltered(filters, orderBy, ascending);
            List<InteraccionEnlaceDetalleMailChimpBO> listadoBO = new List<InteraccionEnlaceDetalleMailChimpBO>();

            foreach (var itemEntidad in listado)
            {
                InteraccionEnlaceDetalleMailChimpBO objetoBO = Mapper.Map<TInteraccionEnlaceDetalleMailChimp, InteraccionEnlaceDetalleMailChimpBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
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
        public List<InteraccionEnlaceDetalleMailChimpBO> InteraccionEnlaceDetalleMailchimpCompletoPorCampaniaMailChimpId(string campaniaMailChimpId)
        {
            try
            {
                List<InteraccionEnlaceDetalleMailChimpBO> listaInteraccionEnlaceDetalleMailchimp = null;

                var query = "SELECT * FROM mkt.V_TInteraccionEnlaceDetalleMailchimpCompleto_PorCampaniaMailChimpId WHERE CampaniaMailChimpId = @campaniaMailChimpId";
                var queryRespuesta = _dapper.QueryDapper(query, new { campaniaMailChimpId });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    listaInteraccionEnlaceDetalleMailchimp = JsonConvert.DeserializeObject<List<InteraccionEnlaceDetalleMailChimpBO>>(queryRespuesta);
                }
                return listaInteraccionEnlaceDetalleMailchimp;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
