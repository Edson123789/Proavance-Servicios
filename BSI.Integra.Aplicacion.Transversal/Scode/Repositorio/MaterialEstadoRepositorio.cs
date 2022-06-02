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
    public class MaterialEstadoRepositorio : BaseRepository<TMaterialEstado, MaterialEstadoBO>
    {
        #region Metodos Base
        public MaterialEstadoRepositorio() : base()
        {
        }
        public MaterialEstadoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<MaterialEstadoBO> GetBy(Expression<Func<TMaterialEstado, bool>> filter)
        {
            IEnumerable<TMaterialEstado> listado = base.GetBy(filter);
            List<MaterialEstadoBO> listadoBO = new List<MaterialEstadoBO>();
            foreach (var itemEntidad in listado)
            {
                MaterialEstadoBO objetoBO = Mapper.Map<TMaterialEstado, MaterialEstadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public MaterialEstadoBO FirstById(int id)
        {
            try
            {
                TMaterialEstado entidad = base.FirstById(id);
                MaterialEstadoBO objetoBO = new MaterialEstadoBO();
                Mapper.Map<TMaterialEstado, MaterialEstadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public MaterialEstadoBO FirstBy(Expression<Func<TMaterialEstado, bool>> filter)
        {
            try
            {
                TMaterialEstado entidad = base.FirstBy(filter);
                MaterialEstadoBO objetoBO = Mapper.Map<TMaterialEstado, MaterialEstadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(MaterialEstadoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TMaterialEstado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<MaterialEstadoBO> listadoBO)
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

        public bool Update(MaterialEstadoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TMaterialEstado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<MaterialEstadoBO> listadoBO)
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
        private void AsignacionId(TMaterialEstado entidad, MaterialEstadoBO objetoBO)
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

        private TMaterialEstado MapeoEntidad(MaterialEstadoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TMaterialEstado entidad = new TMaterialEstado();
                entidad = Mapper.Map<MaterialEstadoBO, TMaterialEstado>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<MaterialEstadoBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TMaterialEstado, bool>>> filters, Expression<Func<TMaterialEstado, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TMaterialEstado> listado = base.GetFiltered(filters, orderBy, ascending);
            List<MaterialEstadoBO> listadoBO = new List<MaterialEstadoBO>();

            foreach (var itemEntidad in listado)
            {
                MaterialEstadoBO objetoBO = Mapper.Map<TMaterialEstado, MaterialEstadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
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
        /// Obtiene la lista de estado de materiales
        /// </summary>
        /// <returns></returns>
        public List<MaterialEstadoBO> Obtener()
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
