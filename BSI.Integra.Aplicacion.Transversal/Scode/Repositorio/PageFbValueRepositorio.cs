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
    public class PageFbValueRepositorio : BaseRepository<TPageFbValue, PageFbValueBO>
    {
        #region Metodos Base
        public PageFbValueRepositorio() : base()
        {
        }
        public PageFbValueRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<PageFbValueBO> GetBy(Expression<Func<TPageFbValue, bool>> filter)
        {
            IEnumerable<TPageFbValue> listado = base.GetBy(filter);
            List<PageFbValueBO> listadoBO = new List<PageFbValueBO>();
            foreach (var itemEntidad in listado)
            {
                PageFbValueBO objetoBO = Mapper.Map<TPageFbValue, PageFbValueBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public PageFbValueBO FirstById(int id)
        {
            try
            {
                TPageFbValue entidad = base.FirstById(id);
                PageFbValueBO objetoBO = new PageFbValueBO();
                Mapper.Map<TPageFbValue, PageFbValueBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public PageFbValueBO FirstBy(Expression<Func<TPageFbValue, bool>> filter)
        {
            try
            {
                TPageFbValue entidad = base.FirstBy(filter);
                PageFbValueBO objetoBO = Mapper.Map<TPageFbValue, PageFbValueBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(PageFbValueBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TPageFbValue entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<PageFbValueBO> listadoBO)
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

        public bool Update(PageFbValueBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TPageFbValue entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<PageFbValueBO> listadoBO)
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
        private void AsignacionId(TPageFbValue entidad, PageFbValueBO objetoBO)
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

        private TPageFbValue MapeoEntidad(PageFbValueBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TPageFbValue entidad = new TPageFbValue();
                entidad = Mapper.Map<PageFbValueBO, TPageFbValue>(objetoBO,
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
