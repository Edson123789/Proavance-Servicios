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
    /// Repositorio: AreaFormacionRepositorio
    /// Autor: Fischer Valdez - Luis Huallpa - Wilber Choque Ansoli Espinoza - Richard Zenteno.
    /// Fecha: 29/01/2021
    /// <summary>
    /// Gestión de Area de Formación registrados
    /// </summary>
    public class AreaFormacionRepositorio : BaseRepository<TAreaFormacion, AreaFormacionBO>
    {
        #region Metodos Base
        public AreaFormacionRepositorio() : base()
        {
        }
        public AreaFormacionRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<AreaFormacionBO> GetBy(Expression<Func<TAreaFormacion, bool>> filter)
        {
            IEnumerable<TAreaFormacion> listado = base.GetBy(filter);
            List<AreaFormacionBO> listadoBO = new List<AreaFormacionBO>();
            foreach (var itemEntidad in listado)
            {
                AreaFormacionBO objetoBO = Mapper.Map<TAreaFormacion, AreaFormacionBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public AreaFormacionBO FirstById(int id)
        {
            try
            {
                TAreaFormacion entidad = base.FirstById(id);
                AreaFormacionBO objetoBO = new AreaFormacionBO();
                Mapper.Map<TAreaFormacion, AreaFormacionBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public AreaFormacionBO FirstBy(Expression<Func<TAreaFormacion, bool>> filter)
        {
            try
            {
                TAreaFormacion entidad = base.FirstBy(filter);
                AreaFormacionBO objetoBO = Mapper.Map<TAreaFormacion, AreaFormacionBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(AreaFormacionBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TAreaFormacion entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<AreaFormacionBO> listadoBO)
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

        public bool Update(AreaFormacionBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TAreaFormacion entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<AreaFormacionBO> listadoBO)
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
        private void AsignacionId(TAreaFormacion entidad, AreaFormacionBO objetoBO)
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

        private TAreaFormacion MapeoEntidad(AreaFormacionBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TAreaFormacion entidad = new TAreaFormacion();
                entidad = Mapper.Map<AreaFormacionBO, TAreaFormacion>(objetoBO,
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

        /// Repositorio: AreaFormacionRepositorio
        /// Autor: 
        /// Fecha: 29/01/2021
        /// <summary>
        /// Obtiene la lista de nombres de Area formacion registradas en el sistema, 
        /// y sus IDs, (Usado para el llenado de combobox).
        /// </summary> 
        /// <returns> List<AreaFormacionFiltroDTO> </returns>
        public List<AreaFormacionFiltroDTO> ObtenerAreaFormacionFiltro()
        {
            try
            {
                List<AreaFormacionFiltroDTO> areasFormacion = new List<AreaFormacionFiltroDTO>();
                var query = string.Empty;
                query = "SELECT Id, Nombre FROM pla.V_TAreaFormacion_ObtenerIdNombre WHERE  Estado = 1";
                var areasFormacionDB = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(areasFormacionDB) && !areasFormacionDB.Contains("[]"))
                {
                    areasFormacion = JsonConvert.DeserializeObject<List<AreaFormacionFiltroDTO>>(areasFormacionDB);
                }
                return areasFormacion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
