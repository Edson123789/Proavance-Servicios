using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class MiembroMailchimpRepositorio : BaseRepository<TMiembroMailchimp, MiembroMailchimpBO>
    {
        #region Metodos Base
        public MiembroMailchimpRepositorio() : base()
        {
        }
        public MiembroMailchimpRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MiembroMailchimpBO> GetBy(Expression<Func<TMiembroMailchimp, bool>> filter)
        {
            IEnumerable<TMiembroMailchimp> listado = base.GetBy(filter);
            List<MiembroMailchimpBO> listadoBO = new List<MiembroMailchimpBO>();
            foreach (var itemEntidad in listado)
            {
                MiembroMailchimpBO objetoBO = Mapper.Map<TMiembroMailchimp, MiembroMailchimpBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MiembroMailchimpBO FirstById(int id)
        {
            try
            {
                TMiembroMailchimp entidad = base.FirstById(id);
                MiembroMailchimpBO objetoBO = new MiembroMailchimpBO();
                Mapper.Map<TMiembroMailchimp, MiembroMailchimpBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MiembroMailchimpBO FirstBy(Expression<Func<TMiembroMailchimp, bool>> filter)
        {
            try
            {
                TMiembroMailchimp entidad = base.FirstBy(filter);
                MiembroMailchimpBO objetoBO = Mapper.Map<TMiembroMailchimp, MiembroMailchimpBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MiembroMailchimpBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMiembroMailchimp entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MiembroMailchimpBO> listadoBO)
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

        public bool Update(MiembroMailchimpBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMiembroMailchimp entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MiembroMailchimpBO> listadoBO)
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
        private void AsignacionId(TMiembroMailchimp entidad, MiembroMailchimpBO objetoBO)
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

        private TMiembroMailchimp MapeoEntidad(MiembroMailchimpBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMiembroMailchimp entidad = new TMiembroMailchimp();
                entidad = Mapper.Map<MiembroMailchimpBO, TMiembroMailchimp>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<MiembroMailchimpBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TMiembroMailchimp, bool>>> filters, Expression<Func<TMiembroMailchimp, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TMiembroMailchimp> listado = base.GetFiltered(filters, orderBy, ascending);
            List<MiembroMailchimpBO> listadoBO = new List<MiembroMailchimpBO>();

            foreach (var itemEntidad in listado)
            {
                MiembroMailchimpBO objetoBO = Mapper.Map<TMiembroMailchimp, MiembroMailchimpBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
