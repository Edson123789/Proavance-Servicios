using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class UrlContenedorPermisoRepositorio : BaseRepository<TUrlContenedorPermisos, UrlContenedorPermisoBO>
    {
        #region Metodos Base
        public UrlContenedorPermisoRepositorio() : base()
        {
        }
        public UrlContenedorPermisoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<UrlContenedorPermisoBO> GetBy(Expression<Func<TUrlContenedorPermisos, bool>> filter)
        {
            IEnumerable<TUrlContenedorPermisos> listado = base.GetBy(filter);
            List<UrlContenedorPermisoBO> listadoBO = new List<UrlContenedorPermisoBO>();
            foreach (var itemEntidad in listado)
            {
                UrlContenedorPermisoBO objetoBO = Mapper.Map<TUrlContenedorPermisos, UrlContenedorPermisoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public UrlContenedorPermisoBO FirstById(int id)
        {
            try
            {
                TUrlContenedorPermisos entidad = base.FirstById(id);
                UrlContenedorPermisoBO objetoBO = new UrlContenedorPermisoBO();
                Mapper.Map<TUrlContenedorPermisos, UrlContenedorPermisoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public UrlContenedorPermisoBO FirstBy(Expression<Func<TUrlContenedorPermisos, bool>> filter)
        {
            try
            {
                TUrlContenedorPermisos entidad = base.FirstBy(filter);
                UrlContenedorPermisoBO objetoBO = Mapper.Map<TUrlContenedorPermisos, UrlContenedorPermisoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(UrlContenedorPermisoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TUrlContenedorPermisos entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<UrlContenedorPermisoBO> listadoBO)
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

        public bool Update(UrlContenedorPermisoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TUrlContenedorPermisos entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<UrlContenedorPermisoBO> listadoBO)
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
        private void AsignacionId(TUrlContenedorPermisos entidad, UrlContenedorPermisoBO objetoBO)
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

        private TUrlContenedorPermisos MapeoEntidad(UrlContenedorPermisoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TUrlContenedorPermisos entidad = new TUrlContenedorPermisos();
                entidad = Mapper.Map<UrlContenedorPermisoBO, TUrlContenedorPermisos>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<UrlContenedorPermisoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TUrlContenedorPermisos, bool>>> filters, Expression<Func<TUrlContenedorPermisos, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TUrlContenedorPermisos> listado = base.GetFiltered(filters, orderBy, ascending);
            List<UrlContenedorPermisoBO> listadoBO = new List<UrlContenedorPermisoBO>();

            foreach (var itemEntidad in listado)
            {
                UrlContenedorPermisoBO objetoBO = Mapper.Map<TUrlContenedorPermisos, UrlContenedorPermisoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
