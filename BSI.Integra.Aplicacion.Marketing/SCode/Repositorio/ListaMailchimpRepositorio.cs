using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class ListaMailchimpRepositorio : BaseRepository<TListaMailchimp, ListaMailchimpBO>
    {
        #region Metodos Base
        public ListaMailchimpRepositorio() : base()
        {
        }
        public ListaMailchimpRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ListaMailchimpBO> GetBy(Expression<Func<TListaMailchimp, bool>> filter)
        {
            IEnumerable<TListaMailchimp> listado = base.GetBy(filter);
            List<ListaMailchimpBO> listadoBO = new List<ListaMailchimpBO>();
            foreach (var itemEntidad in listado)
            {
                ListaMailchimpBO objetoBO = Mapper.Map<TListaMailchimp, ListaMailchimpBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ListaMailchimpBO FirstById(int id)
        {
            try
            {
                TListaMailchimp entidad = base.FirstById(id);
                ListaMailchimpBO objetoBO = new ListaMailchimpBO();
                Mapper.Map<TListaMailchimp, ListaMailchimpBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ListaMailchimpBO FirstBy(Expression<Func<TListaMailchimp, bool>> filter)
        {
            try
            {
                TListaMailchimp entidad = base.FirstBy(filter);
                ListaMailchimpBO objetoBO = Mapper.Map<TListaMailchimp, ListaMailchimpBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ListaMailchimpBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TListaMailchimp entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ListaMailchimpBO> listadoBO)
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

        public bool Update(ListaMailchimpBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TListaMailchimp entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ListaMailchimpBO> listadoBO)
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
        private void AsignacionId(TListaMailchimp entidad, ListaMailchimpBO objetoBO)
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

        private TListaMailchimp MapeoEntidad(ListaMailchimpBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TListaMailchimp entidad = new TListaMailchimp();
                entidad = Mapper.Map<ListaMailchimpBO, TListaMailchimp>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ListaMailchimpBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TListaMailchimp, bool>>> filters, Expression<Func<TListaMailchimp, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TListaMailchimp> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ListaMailchimpBO> listadoBO = new List<ListaMailchimpBO>();

            foreach (var itemEntidad in listado)
            {
                ListaMailchimpBO objetoBO = Mapper.Map<TListaMailchimp, ListaMailchimpBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
