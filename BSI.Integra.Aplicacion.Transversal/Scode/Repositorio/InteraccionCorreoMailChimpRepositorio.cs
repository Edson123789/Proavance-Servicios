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
    /// Repositorio: Marketing/InteraccionCorreoMailChimp
    /// Autor: Gian Miranda
    /// Fecha: 27/01/2021
    /// <summary>
    /// Repositorio para la interaccion con la DB mkt.T_InteraccionCorreoMailChimp
    /// </summary>
    public class InteraccionCorreoMailChimpRepositorio : BaseRepository<TInteraccionCorreoMailChimp, InteraccionCorreoMailChimpBO>
    {
        #region Metodos Base
        public InteraccionCorreoMailChimpRepositorio() : base()
        {
        }
        public InteraccionCorreoMailChimpRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<InteraccionCorreoMailChimpBO> GetBy(Expression<Func<TInteraccionCorreoMailChimp, bool>> filter)
        {
            IEnumerable<TInteraccionCorreoMailChimp> listado = base.GetBy(filter);
            List<InteraccionCorreoMailChimpBO> listadoBO = new List<InteraccionCorreoMailChimpBO>();
            foreach (var itemEntidad in listado)
            {
                InteraccionCorreoMailChimpBO objetoBO = Mapper.Map<TInteraccionCorreoMailChimp, InteraccionCorreoMailChimpBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public InteraccionCorreoMailChimpBO FirstById(int id)
        {
            try
            {
                TInteraccionCorreoMailChimp entidad = base.FirstById(id);
                InteraccionCorreoMailChimpBO objetoBO = new InteraccionCorreoMailChimpBO();
                Mapper.Map<TInteraccionCorreoMailChimp, InteraccionCorreoMailChimpBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public InteraccionCorreoMailChimpBO FirstBy(Expression<Func<TInteraccionCorreoMailChimp, bool>> filter)
        {
            try
            {
                TInteraccionCorreoMailChimp entidad = base.FirstBy(filter);
                InteraccionCorreoMailChimpBO objetoBO = Mapper.Map<TInteraccionCorreoMailChimp, InteraccionCorreoMailChimpBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(InteraccionCorreoMailChimpBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TInteraccionCorreoMailChimp entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<InteraccionCorreoMailChimpBO> listadoBO)
        {
            try
            {
                foreach (var objetoBO in listadoBO)
                {
                    bool resultado;
                    if (objetoBO.Id == 0)
                    {
                        resultado = Insert(objetoBO);
                    }
                    else {
                        resultado = Update(objetoBO);
                    }
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

        public bool Update(InteraccionCorreoMailChimpBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TInteraccionCorreoMailChimp entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<InteraccionCorreoMailChimpBO> listadoBO)
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
        private void AsignacionId(TInteraccionCorreoMailChimp entidad, InteraccionCorreoMailChimpBO objetoBO)
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

        private TInteraccionCorreoMailChimp MapeoEntidad(InteraccionCorreoMailChimpBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TInteraccionCorreoMailChimp entidad = new TInteraccionCorreoMailChimp();
                entidad = Mapper.Map<InteraccionCorreoMailChimpBO, TInteraccionCorreoMailChimp>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ListaInteraccionCorreoDetalleMailChimp != null && objetoBO.ListaInteraccionCorreoDetalleMailChimp.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaInteraccionCorreoDetalleMailChimp)
                    {
                        TInteraccionCorreoDetalleMailChimp entidadHijo = new TInteraccionCorreoDetalleMailChimp();
                        entidadHijo = Mapper.Map<InteraccionCorreoDetalleMailChimpBO, TInteraccionCorreoDetalleMailChimp>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TInteraccionCorreoDetalleMailChimp.Add(entidadHijo);
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<InteraccionCorreoMailChimpBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TInteraccionCorreoMailChimp, bool>>> filters, Expression<Func<TInteraccionCorreoMailChimp, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TInteraccionCorreoMailChimp> listado = base.GetFiltered(filters, orderBy, ascending);
            List<InteraccionCorreoMailChimpBO> listadoBO = new List<InteraccionCorreoMailChimpBO>();

            foreach (var itemEntidad in listado)
            {
                InteraccionCorreoMailChimpBO objetoBO = Mapper.Map<TInteraccionCorreoMailChimp, InteraccionCorreoMailChimpBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
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
        public List<InteraccionCorreoMailChimpBO> InteraccionCorreoCompletoPorCampaniaMailChimpId(string campaniaMailChimpId)
        {
            try
            {
                List<InteraccionCorreoMailChimpBO> listaInteraccionCorreoMailChimp = null;

                var query = "SELECT * FROM mkt.V_TInteraccionCorreoMailChimp_PorCampaniaMailChimpId WHERE IdCampaniaMailchimp = @campaniaMailChimpId";
                var queryRespuesta = _dapper.QueryDapper(query, new { campaniaMailChimpId });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    listaInteraccionCorreoMailChimp = JsonConvert.DeserializeObject<List<InteraccionCorreoMailChimpBO>>(queryRespuesta);
                }
                return listaInteraccionCorreoMailChimp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
