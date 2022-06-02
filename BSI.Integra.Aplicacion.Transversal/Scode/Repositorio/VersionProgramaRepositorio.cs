using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class VersionProgramaRepositorio : BaseRepository<TVersionPrograma, VersionProgramaBO>
    {
        #region Metodos Base
        public VersionProgramaRepositorio() : base()
        {
        }
        public VersionProgramaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<VersionProgramaBO> GetBy(Expression<Func<TVersionPrograma, bool>> filter)
        {
            IEnumerable<TVersionPrograma> listado = base.GetBy(filter);
            List<VersionProgramaBO> listadoBO = new List<VersionProgramaBO>();
            foreach (var itemEntidad in listado)
            {
                VersionProgramaBO objetoBO = Mapper.Map<TVersionPrograma, VersionProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public IEnumerable<VersionProgramaBO> GetBy(Expression<Func<TVersionPrograma, bool>> filter, int skip, int take)
        {
            IEnumerable<TVersionPrograma> listado = base.GetBy(filter).Skip(skip).Take(take);
            List<VersionProgramaBO> listadoBO = new List<VersionProgramaBO>();
            foreach (var itemEntidad in listado)
            {
                VersionProgramaBO objetoBO = Mapper.Map<TVersionPrograma, VersionProgramaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }

        public VersionProgramaBO FirstById(int id)
        {
            try
            {
                TVersionPrograma entidad = base.FirstById(id);
                VersionProgramaBO objetoBO = new VersionProgramaBO();
                Mapper.Map<TVersionPrograma, VersionProgramaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public VersionProgramaBO FirstBy(Expression<Func<TVersionPrograma, bool>> filter)
        {
            try
            {
                TVersionPrograma entidad = base.FirstBy(filter);
                VersionProgramaBO objetoBO = Mapper.Map<TVersionPrograma, VersionProgramaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(VersionProgramaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TVersionPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<VersionProgramaBO> listadoBO)
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

        public bool Update(VersionProgramaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TVersionPrograma entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<VersionProgramaBO> listadoBO)
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
        private void AsignacionId(TVersionPrograma entidad, VersionProgramaBO objetoBO)
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

        private TVersionPrograma MapeoEntidad(VersionProgramaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TVersionPrograma entidad = new TVersionPrograma();
                entidad = Mapper.Map<VersionProgramaBO, TVersionPrograma>(objetoBO,
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
        /// Obtiene Lista de Tipo de Datos con estado activo
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerFiltro()
        {
            try
            {
                return GetBy(x => x.Estado == true, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<VersionProgramaDTO> ObtenerTodo()
        {
            try
            {
                return GetBy(x => x.Estado == true, x => new VersionProgramaDTO { Id = x.Id, Nombre = x.Nombre,Usuario =x.UsuarioModificacion }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
