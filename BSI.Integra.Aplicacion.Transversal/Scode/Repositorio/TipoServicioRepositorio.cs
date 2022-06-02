using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class TipoServicioRepositorio : BaseRepository<TTipoServicio, TipoServicioBO>
    {
        #region Metodos Base
        public TipoServicioRepositorio() : base()
        {
        }
        public TipoServicioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<TipoServicioBO> GetBy(Expression<Func<TTipoServicio, bool>> filter)
        {
            IEnumerable<TTipoServicio> listado = base.GetBy(filter);
            List<TipoServicioBO> listadoBO = new List<TipoServicioBO>();
            foreach (var itemEntidad in listado)
            {
                TipoServicioBO objetoBO = Mapper.Map<TTipoServicio, TipoServicioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public TipoServicioBO FirstById(int id)
        {
            try
            {
                TTipoServicio entidad = base.FirstById(id);
                TipoServicioBO objetoBO = new TipoServicioBO();
                Mapper.Map<TTipoServicio, TipoServicioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public TipoServicioBO FirstBy(Expression<Func<TTipoServicio, bool>> filter)
        {
            try
            {
                TTipoServicio entidad = base.FirstBy(filter);
                TipoServicioBO objetoBO = Mapper.Map<TTipoServicio, TipoServicioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(TipoServicioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TTipoServicio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<TipoServicioBO> listadoBO)
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

        public bool Update(TipoServicioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TTipoServicio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<TipoServicioBO> listadoBO)
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
        private void AsignacionId(TTipoServicio entidad, TipoServicioBO objetoBO)
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

        private TTipoServicio MapeoEntidad(TipoServicioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TTipoServicio entidad = new TTipoServicio();
                entidad = Mapper.Map<TipoServicioBO, TTipoServicio>(objetoBO,
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

        /// <summary>
        /// Obtiene todos los registros para filtro
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene la lista de tipos de servicio
        /// </summary>
        /// <returns></returns>
        public List<TipoServicioBO> Obtener()
        {
            try
            {
                return this.GetBy(x => x.Estado).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
