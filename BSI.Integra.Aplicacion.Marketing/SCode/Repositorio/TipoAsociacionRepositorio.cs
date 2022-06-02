using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class TipoAsociacionRepositorio : BaseRepository<TTipoAsociacion, TipoAsociacionBO>
    {
        #region Metodos Base
        public TipoAsociacionRepositorio() : base()
        {
        }
        public TipoAsociacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoAsociacionBO> GetBy(Expression<Func<TTipoAsociacion, bool>> filter)
        {
            IEnumerable<TTipoAsociacion> listado = base.GetBy(filter);
            List<TipoAsociacionBO> listadoBO = new List<TipoAsociacionBO>();
            foreach (var itemEntidad in listado)
            {
                TipoAsociacionBO objetoBO = Mapper.Map<TTipoAsociacion, TipoAsociacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoAsociacionBO FirstById(int id)
        {
            try
            {
                TTipoAsociacion entidad = base.FirstById(id);
                TipoAsociacionBO objetoBO = new TipoAsociacionBO();
                Mapper.Map<TTipoAsociacion, TipoAsociacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoAsociacionBO FirstBy(Expression<Func<TTipoAsociacion, bool>> filter)
        {
            try
            {
                TTipoAsociacion entidad = base.FirstBy(filter);
                TipoAsociacionBO objetoBO = Mapper.Map<TTipoAsociacion, TipoAsociacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoAsociacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoAsociacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoAsociacionBO> listadoBO)
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

        public bool Update(TipoAsociacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoAsociacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoAsociacionBO> listadoBO)
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
        private void AsignacionId(TTipoAsociacion entidad, TipoAsociacionBO objetoBO)
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

        private TTipoAsociacion MapeoEntidad(TipoAsociacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoAsociacion entidad = new TTipoAsociacion();
                entidad = Mapper.Map<TipoAsociacionBO, TTipoAsociacion>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<TipoAsociacionBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TTipoAsociacion, bool>>> filters, Expression<Func<TTipoAsociacion, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TTipoAsociacion> listado = base.GetFiltered(filters, orderBy, ascending);
            List<TipoAsociacionBO> listadoBO = new List<TipoAsociacionBO>();

            foreach (var itemEntidad in listado)
            {
                TipoAsociacionBO objetoBO = Mapper.Map<TTipoAsociacion, TipoAsociacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
