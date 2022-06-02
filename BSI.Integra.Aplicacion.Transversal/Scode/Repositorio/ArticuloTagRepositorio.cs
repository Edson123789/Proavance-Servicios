using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ArticuloTagRepositorio : BaseRepository<TArticuloTag, ArticuloTagBO>
    {
        #region Metodos Base
        public ArticuloTagRepositorio() : base()
        {
        }
        public ArticuloTagRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ArticuloTagBO> GetBy(Expression<Func<TArticuloTag, bool>> filter)
        {
            IEnumerable<TArticuloTag> listado = base.GetBy(filter);
            List<ArticuloTagBO> listadoBO = new List<ArticuloTagBO>();
            foreach (var itemEntidad in listado)
            {
                ArticuloTagBO objetoBO = Mapper.Map<TArticuloTag, ArticuloTagBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ArticuloTagBO FirstById(int id)
        {
            try
            {
                TArticuloTag entidad = base.FirstById(id);
                ArticuloTagBO objetoBO = new ArticuloTagBO();
                Mapper.Map<TArticuloTag, ArticuloTagBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ArticuloTagBO FirstBy(Expression<Func<TArticuloTag, bool>> filter)
        {
            try
            {
                TArticuloTag entidad = base.FirstBy(filter);
                ArticuloTagBO objetoBO = Mapper.Map<TArticuloTag, ArticuloTagBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ArticuloTagBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TArticuloTag entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ArticuloTagBO> listadoBO)
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

        public bool Update(ArticuloTagBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TArticuloTag entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ArticuloTagBO> listadoBO)
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
        private void AsignacionId(TArticuloTag entidad, ArticuloTagBO objetoBO)
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

        private TArticuloTag MapeoEntidad(ArticuloTagBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TArticuloTag entidad = new TArticuloTag();
                entidad = Mapper.Map<ArticuloTagBO, TArticuloTag>(objetoBO,
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
