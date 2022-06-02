using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    /// Repositorio: Planificacion/TipoVista
    /// Autor: Gian Miranda
    /// Fecha: 04/03/2021
    /// <summary>
    /// Repositorio para consultas de pla.T_TipoVita
    /// </summary>
    public class TipoVistaRepositorio : BaseRepository<TTipoVista, TipoVistaBO>
    {
        #region Metodos Base
        public TipoVistaRepositorio() : base()
        {
        }
        public TipoVistaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoVistaBO> GetBy(Expression<Func<TTipoVista, bool>> filter)
        {
            IEnumerable<TTipoVista> listado = base.GetBy(filter);
            List<TipoVistaBO> listadoBO = new List<TipoVistaBO>();
            foreach (var itemEntidad in listado)
            {
                TipoVistaBO objetoBO = Mapper.Map<TTipoVista, TipoVistaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoVistaBO FirstById(int id)
        {
            try
            {
                TTipoVista entidad = base.FirstById(id);
                TipoVistaBO objetoBO = new TipoVistaBO();
                Mapper.Map<TTipoVista, TipoVistaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoVistaBO FirstBy(Expression<Func<TTipoVista, bool>> filter)
        {
            try
            {
                TTipoVista entidad = base.FirstBy(filter);
                TipoVistaBO objetoBO = Mapper.Map<TTipoVista, TipoVistaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoVistaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoVista entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoVistaBO> listadoBO)
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

        public bool Update(TipoVistaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoVista entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoVistaBO> listadoBO)
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
        private void AsignacionId(TTipoVista entidad, TipoVistaBO objetoBO)
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

        private TTipoVista MapeoEntidad(TipoVistaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoVista entidad = new TTipoVista();
                entidad = Mapper.Map<TipoVistaBO, TTipoVista>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<TipoVistaBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TTipoVista, bool>>> filters, Expression<Func<TTipoVista, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TTipoVista> listado = base.GetFiltered(filters, orderBy, ascending);
            List<TipoVistaBO> listadoBO = new List<TipoVistaBO>();

            foreach (var itemEntidad in listado)
            {
                TipoVistaBO objetoBO = Mapper.Map<TTipoVista, TipoVistaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion
    }
}
