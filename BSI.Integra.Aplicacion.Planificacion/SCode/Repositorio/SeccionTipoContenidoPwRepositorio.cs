using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Planificacion.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;

namespace BSI.Integra.Aplicacion.Planificacion.Repositorio
{
    public class SeccionTipoContenidoPwRepositorio : BaseRepository<TSeccionTipoContenidoPw, SeccionTipoContenidoPwBO>
    {
        #region Metodos Base
        public SeccionTipoContenidoPwRepositorio() : base()
        {
        }
        public SeccionTipoContenidoPwRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SeccionTipoContenidoPwBO> GetBy(Expression<Func<TSeccionTipoContenidoPw, bool>> filter)
        {
            IEnumerable<TSeccionTipoContenidoPw> listado = base.GetBy(filter);
            List<SeccionTipoContenidoPwBO> listadoBO = new List<SeccionTipoContenidoPwBO>();
            foreach (var itemEntidad in listado)
            {
                SeccionTipoContenidoPwBO objetoBO = Mapper.Map<TSeccionTipoContenidoPw, SeccionTipoContenidoPwBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SeccionTipoContenidoPwBO FirstById(int id)
        {
            try
            {
                TSeccionTipoContenidoPw entidad = base.FirstById(id);
                SeccionTipoContenidoPwBO objetoBO = new SeccionTipoContenidoPwBO();
                Mapper.Map<TSeccionTipoContenidoPw, SeccionTipoContenidoPwBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SeccionTipoContenidoPwBO FirstBy(Expression<Func<TSeccionTipoContenidoPw, bool>> filter)
        {
            try
            {
                TSeccionTipoContenidoPw entidad = base.FirstBy(filter);
                SeccionTipoContenidoPwBO objetoBO = Mapper.Map<TSeccionTipoContenidoPw, SeccionTipoContenidoPwBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SeccionTipoContenidoPwBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSeccionTipoContenidoPw entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SeccionTipoContenidoPwBO> listadoBO)
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

        public bool Update(SeccionTipoContenidoPwBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSeccionTipoContenidoPw entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SeccionTipoContenidoPwBO> listadoBO)
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
        private void AsignacionId(TSeccionTipoContenidoPw entidad, SeccionTipoContenidoPwBO objetoBO)
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

        private TSeccionTipoContenidoPw MapeoEntidad(SeccionTipoContenidoPwBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSeccionTipoContenidoPw entidad = new TSeccionTipoContenidoPw();
                entidad = Mapper.Map<SeccionTipoContenidoPwBO, TSeccionTipoContenidoPw>(objetoBO,
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
        /// Obtiene lista de SeccionTipoContenidoPws
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoGrid()
        {
            try
            {
                var lista = GetBy(x => true, y => new FiltroDTO
                {
                    Id = y.Id,
                    Nombre = y.Nombre,
                }).OrderByDescending(x => x.Id).ToList();

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        
    }
}
