using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: AreaTrabajoRepositorio
    /// Autor: Fischer Valdez - Johan Cayo - Luis Huallpa - Wilber Choque - Richard Zenteno.
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_AreaTrabajo
    /// </summary>
    public class AreaTrabajoRepositorio : BaseRepository<TAreaTrabajo, AreaTrabajoBO>
    {
        #region Metodos Base
        public AreaTrabajoRepositorio() : base()
        {
        }
        public AreaTrabajoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AreaTrabajoBO> GetBy(Expression<Func<TAreaTrabajo, bool>> filter)
        {
            IEnumerable<TAreaTrabajo> listado = base.GetBy(filter);
            List<AreaTrabajoBO> listadoBO = new List<AreaTrabajoBO>();
            foreach (var itemEntidad in listado)
            {
                AreaTrabajoBO objetoBO = Mapper.Map<TAreaTrabajo, AreaTrabajoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AreaTrabajoBO FirstById(int id)
        {
            try
            {
                TAreaTrabajo entidad = base.FirstById(id);
                AreaTrabajoBO objetoBO = new AreaTrabajoBO();
                Mapper.Map<TAreaTrabajo, AreaTrabajoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AreaTrabajoBO FirstBy(Expression<Func<TAreaTrabajo, bool>> filter)
        {
            try
            {
                TAreaTrabajo entidad = base.FirstBy(filter);
                AreaTrabajoBO objetoBO = Mapper.Map<TAreaTrabajo, AreaTrabajoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AreaTrabajoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAreaTrabajo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AreaTrabajoBO> listadoBO)
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

        public bool Update(AreaTrabajoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAreaTrabajo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AreaTrabajoBO> listadoBO)
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
        private void AsignacionId(TAreaTrabajo entidad, AreaTrabajoBO objetoBO)
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

        private TAreaTrabajo MapeoEntidad(AreaTrabajoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAreaTrabajo entidad = new TAreaTrabajo();
                entidad = Mapper.Map<AreaTrabajoBO, TAreaTrabajo>(objetoBO,
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
        /// Obtiene la lista de nombres de las Areas de trabajo  registradas en el sistema 
        /// y sus IDs, (Usado para el llenado de combobox).
        /// </summary>
        /// <returns></returns>
        public List<AreaTrabajoFiltroDTO> ObtenerAreasTrabajo()
        {
            try
            {
                List<AreaTrabajoFiltroDTO> areasTrabajo = new List<AreaTrabajoFiltroDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre FROM pla.V_TAreaTrabajo_Filtro WHERE  Estado = 1";
                var _areasTrabajo = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(_areasTrabajo) && !_areasTrabajo.Contains("[]"))
                {
                    areasTrabajo = JsonConvert.DeserializeObject<List<AreaTrabajoFiltroDTO>>(_areasTrabajo);
                }
                return areasTrabajo;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Repositorio: AreaTrabajoRepositorio
        /// Autor: 
        /// Fecha: 04/03/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de las areas de trabajo registradas para filtros de fromularios    
        /// </summary>
        /// <returns> List<AreaTrabajoDTO> </returns>
        public List<AreaTrabajoDTO> ObtenerTodoAreaTrabajoFiltro()
        {
            try
            {
                var query = "SELECT Id,Nombre FROM pla.V_TAreaTrabajo_Filtro Where Estado = 1";
                var areaTrabajoDB = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<AreaTrabajoDTO>>(areaTrabajoDB);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
