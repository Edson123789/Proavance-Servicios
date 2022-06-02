using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Marketing/InteraccionCorreoDetalleMailChimp
    /// Autor: Gian Miranda
    /// Fecha: 27/01/2021
    /// <summary>
    /// Repositorio para la interaccion con la DB mkt.T_InteraccionCorreoDetalleMailChimp
    /// </summary>
    public class InteraccionCorreoDetalleMailChimpRepositorio : BaseRepository<TInteraccionCorreoDetalleMailChimp, InteraccionCorreoDetalleMailChimpBO>
    {
        #region Metodos Base
        public InteraccionCorreoDetalleMailChimpRepositorio() : base()
        {
        }
        public InteraccionCorreoDetalleMailChimpRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<InteraccionCorreoDetalleMailChimpBO> GetBy(Expression<Func<TInteraccionCorreoDetalleMailChimp, bool>> filter)
        {
            IEnumerable<TInteraccionCorreoDetalleMailChimp> listado = base.GetBy(filter);
            List<InteraccionCorreoDetalleMailChimpBO> listadoBO = new List<InteraccionCorreoDetalleMailChimpBO>();
            foreach (var itemEntidad in listado)
            {
                InteraccionCorreoDetalleMailChimpBO objetoBO = Mapper.Map<TInteraccionCorreoDetalleMailChimp, InteraccionCorreoDetalleMailChimpBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public InteraccionCorreoDetalleMailChimpBO FirstById(int id)
        {
            try
            {
                TInteraccionCorreoDetalleMailChimp entidad = base.FirstById(id);
                InteraccionCorreoDetalleMailChimpBO objetoBO = new InteraccionCorreoDetalleMailChimpBO();
                Mapper.Map<TInteraccionCorreoDetalleMailChimp, InteraccionCorreoDetalleMailChimpBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public InteraccionCorreoDetalleMailChimpBO FirstBy(Expression<Func<TInteraccionCorreoDetalleMailChimp, bool>> filter)
        {
            try
            {
                TInteraccionCorreoDetalleMailChimp entidad = base.FirstBy(filter);
                InteraccionCorreoDetalleMailChimpBO objetoBO = Mapper.Map<TInteraccionCorreoDetalleMailChimp, InteraccionCorreoDetalleMailChimpBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(InteraccionCorreoDetalleMailChimpBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TInteraccionCorreoDetalleMailChimp entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<InteraccionCorreoDetalleMailChimpBO> listadoBO)
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

        public bool Update(InteraccionCorreoDetalleMailChimpBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TInteraccionCorreoDetalleMailChimp entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<InteraccionCorreoDetalleMailChimpBO> listadoBO)
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
        private void AsignacionId(TInteraccionCorreoDetalleMailChimp entidad, InteraccionCorreoDetalleMailChimpBO objetoBO)
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

        private TInteraccionCorreoDetalleMailChimp MapeoEntidad(InteraccionCorreoDetalleMailChimpBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TInteraccionCorreoDetalleMailChimp entidad = new TInteraccionCorreoDetalleMailChimp();
                entidad = Mapper.Map<InteraccionCorreoDetalleMailChimpBO, TInteraccionCorreoDetalleMailChimp>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<InteraccionCorreoDetalleMailChimpBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TInteraccionCorreoDetalleMailChimp, bool>>> filters, Expression<Func<TInteraccionCorreoDetalleMailChimp, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TInteraccionCorreoDetalleMailChimp> listado = base.GetFiltered(filters, orderBy, ascending);
            List<InteraccionCorreoDetalleMailChimpBO> listadoBO = new List<InteraccionCorreoDetalleMailChimpBO>();

            foreach (var itemEntidad in listado)
            {
                InteraccionCorreoDetalleMailChimpBO objetoBO = Mapper.Map<TInteraccionCorreoDetalleMailChimp, InteraccionCorreoDetalleMailChimpBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Elimina logicamente por la campaniaMailChimpId los registros de la tabla mkt.T_InteraccionCorreoDetalleMailChimp
        /// </summary>
        /// <param name="idCampaniaMailchimp">Id campania Mailchimp original</param>
        public void EliminarInteraccionCorreoDetalleMailChimpPorCampaniaMailChimpId(string idCampaniaMailchimp)
        {
            try
            {
                var spEliminarInteraccionCorreoDetalleMailChimp = "[mkt].[SP_EliminarInteraccionCorreoDetalleMailChimp_CampaniaMailchimpId]";
                var resultadoSp = _dapper.QuerySPFirstOrDefault(spEliminarInteraccionCorreoDetalleMailChimp, new { CampaniaMailChimpId = idCampaniaMailchimp, UsuarioModificacion = "systemUpdate" });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
