using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class CampaniaMailchimpRepositorio : BaseRepository<TCampaniaMailchimp, CampaniaMailchimpBO>
    {
        #region Metodos Base
        public CampaniaMailchimpRepositorio() : base()
        {
        }
        public CampaniaMailchimpRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CampaniaMailchimpBO> GetBy(Expression<Func<TCampaniaMailchimp, bool>> filter)
        {
            IEnumerable<TCampaniaMailchimp> listado = base.GetBy(filter);
            List<CampaniaMailchimpBO> listadoBO = new List<CampaniaMailchimpBO>();
            foreach (var itemEntidad in listado)
            {
                CampaniaMailchimpBO objetoBO = Mapper.Map<TCampaniaMailchimp, CampaniaMailchimpBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CampaniaMailchimpBO FirstById(int id)
        {
            try
            {
                TCampaniaMailchimp entidad = base.FirstById(id);
                CampaniaMailchimpBO objetoBO = new CampaniaMailchimpBO();
                Mapper.Map<TCampaniaMailchimp, CampaniaMailchimpBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CampaniaMailchimpBO FirstBy(Expression<Func<TCampaniaMailchimp, bool>> filter)
        {
            try
            {
                TCampaniaMailchimp entidad = base.FirstBy(filter);
                CampaniaMailchimpBO objetoBO = Mapper.Map<TCampaniaMailchimp, CampaniaMailchimpBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CampaniaMailchimpBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCampaniaMailchimp entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CampaniaMailchimpBO> listadoBO)
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

        public bool Update(CampaniaMailchimpBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCampaniaMailchimp entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CampaniaMailchimpBO> listadoBO)
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
        private void AsignacionId(TCampaniaMailchimp entidad, CampaniaMailchimpBO objetoBO)
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

        private TCampaniaMailchimp MapeoEntidad(CampaniaMailchimpBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCampaniaMailchimp entidad = new TCampaniaMailchimp();
                entidad = Mapper.Map<CampaniaMailchimpBO, TCampaniaMailchimp>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<CampaniaMailchimpBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TCampaniaMailchimp, bool>>> filters, Expression<Func<TCampaniaMailchimp, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TCampaniaMailchimp> listado = base.GetFiltered(filters, orderBy, ascending);
            List<CampaniaMailchimpBO> listadoBO = new List<CampaniaMailchimpBO>();

            foreach (var itemEntidad in listado)
            {
                CampaniaMailchimpBO objetoBO = Mapper.Map<TCampaniaMailchimp, CampaniaMailchimpBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
