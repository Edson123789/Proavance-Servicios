using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class EnlaceMailChimpRepositorio : BaseRepository<TEnlaceMailChimp, EnlaceMailChimpBO>
    {
        #region Metodos Base
        public EnlaceMailChimpRepositorio() : base()
        {
        }
        public EnlaceMailChimpRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EnlaceMailChimpBO> GetBy(Expression<Func<TEnlaceMailChimp, bool>> filter)
        {
            IEnumerable<TEnlaceMailChimp> listado = base.GetBy(filter);
            List<EnlaceMailChimpBO> listadoBO = new List<EnlaceMailChimpBO>();
            foreach (var itemEntidad in listado)
            {
                EnlaceMailChimpBO objetoBO = Mapper.Map<TEnlaceMailChimp, EnlaceMailChimpBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EnlaceMailChimpBO FirstById(int id)
        {
            try
            {
                TEnlaceMailChimp entidad = base.FirstById(id);
                EnlaceMailChimpBO objetoBO = new EnlaceMailChimpBO();
                Mapper.Map<TEnlaceMailChimp, EnlaceMailChimpBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EnlaceMailChimpBO FirstBy(Expression<Func<TEnlaceMailChimp, bool>> filter)
        {
            try
            {
                TEnlaceMailChimp entidad = base.FirstBy(filter);
                EnlaceMailChimpBO objetoBO = Mapper.Map<TEnlaceMailChimp, EnlaceMailChimpBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EnlaceMailChimpBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEnlaceMailChimp entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EnlaceMailChimpBO> listadoBO)
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

        public bool Update(EnlaceMailChimpBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEnlaceMailChimp entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EnlaceMailChimpBO> listadoBO)
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
        private void AsignacionId(TEnlaceMailChimp entidad, EnlaceMailChimpBO objetoBO)
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

        private TEnlaceMailChimp MapeoEntidad(EnlaceMailChimpBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEnlaceMailChimp entidad = new TEnlaceMailChimp();
                entidad = Mapper.Map<EnlaceMailChimpBO, TEnlaceMailChimp>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ListaInteraccionEnlaceMailChimp != null && objetoBO.ListaInteraccionEnlaceMailChimp.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaInteraccionEnlaceMailChimp)
                    {
                        TInteraccionEnlaceMailchimp entidadHijo = new TInteraccionEnlaceMailchimp();
                        entidadHijo = Mapper.Map<InteraccionEnlaceMailchimpBO, TInteraccionEnlaceMailchimp>(hijo, opt => opt.ConfigureMap(MemberList.None));
                        entidad.TInteraccionEnlaceMailchimp.Add(entidadHijo);

                        if (hijo.ListaInteraccionEnlaceDetalleMailChimp != null && hijo.ListaInteraccionEnlaceDetalleMailChimp.Count > 0)
                        {
                            foreach (var hijo1 in hijo.ListaInteraccionEnlaceDetalleMailChimp)
                            {
                                TInteraccionEnlaceDetalleMailChimp entidadHijo1 = new TInteraccionEnlaceDetalleMailChimp();
                                entidadHijo1 = Mapper.Map<InteraccionEnlaceDetalleMailChimpBO, TInteraccionEnlaceDetalleMailChimp>(hijo1, opt => opt.ConfigureMap(MemberList.None));
                                entidadHijo.TInteraccionEnlaceDetalleMailChimp.Add(entidadHijo1);
                            }
                        }
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<EnlaceMailChimpBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TEnlaceMailChimp, bool>>> filters, Expression<Func<TEnlaceMailChimp, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TEnlaceMailChimp> listado = base.GetFiltered(filters, orderBy, ascending);
            List<EnlaceMailChimpBO> listadoBO = new List<EnlaceMailChimpBO>();

            foreach (var itemEntidad in listado)
            {
                EnlaceMailChimpBO objetoBO = Mapper.Map<TEnlaceMailChimp, EnlaceMailChimpBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
