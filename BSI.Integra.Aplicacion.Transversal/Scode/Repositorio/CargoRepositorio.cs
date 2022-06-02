using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Maestros.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: CargoRepositorio
    /// Autor: Johan Cayo - Luis Huallpa - Wilber Choque - Ansoli Espinoza - Richard Zenteno.
    /// Fecha: 16/06/2021
    /// <summary>
    /// Repositorio para de tabla T_Cargo
    /// </summary>
    public class CargoRepositorio : BaseRepository<TCargo, CargoBO>
    {
        #region Metodos Base
        public CargoRepositorio() : base()
        {
        }
        public CargoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CargoBO> GetBy(Expression<Func<TCargo, bool>> filter)
        {
            IEnumerable<TCargo> listado = base.GetBy(filter);
            List<CargoBO> listadoBO = new List<CargoBO>();
            foreach (var itemEntidad in listado)
            {
                CargoBO objetoBO = Mapper.Map<TCargo, CargoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CargoBO FirstById(int id)
        {
            try
            {
                TCargo entidad = base.FirstById(id);
                CargoBO objetoBO = new CargoBO();
                Mapper.Map<TCargo, CargoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CargoBO FirstBy(Expression<Func<TCargo, bool>> filter)
        {
            try
            {
                TCargo entidad = base.FirstBy(filter);
                CargoBO objetoBO = Mapper.Map<TCargo, CargoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CargoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCargo entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CargoBO> listadoBO)
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

        public bool Update(CargoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCargo entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CargoBO> listadoBO)
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
        private void AsignacionId(TCargo entidad, CargoBO objetoBO)
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

        private TCargo MapeoEntidad(CargoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCargo entidad = new TCargo();
                entidad = Mapper.Map<CargoBO, TCargo>(objetoBO,
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
        /// Repositorio: CargoRepositorio
        /// Autor: 
        /// Fecha: 16/06/2021
        /// Version: 1.0
        /// <summary>
        /// Retorna una lista de los cargos para ser usados en filtros  
        /// </summary>
        /// <returns> List<CargoFiltroDTO> </returns>        
        public List<CargoFiltroDTO> ObtenerCargoFiltro()
        {
            try
            {
                List<CargoFiltroDTO> cargos = new List<CargoFiltroDTO>();
                var query = string.Empty;
                query = "SELECT Id, Nombre FROM pla.V_TCargo_ObtenerIdNombre WHERE  Estado = 1 ";
                var cargosDB = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(cargosDB) && !cargosDB.Contains("[]"))
                {
                    cargos = JsonConvert.DeserializeObject<List<CargoFiltroDTO>>(cargosDB);
                }
                return cargos;              
            }
            catch (Exception e) 
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Retorna una lista de los cargos para ser usados en grilla de su propio CRUD 
        /// </summary>
        /// <returns></returns>
        public List<CargoDTO> ObtenerTodoCargo()
        {
            try
            {
                List<CargoDTO> cargos = new List<CargoDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre, Orden FROM pla.T_Cargo WHERE  Estado = 1 ORDER BY Orden DESC";
                var cargosDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(cargosDB) && !cargosDB.Contains("[]"))
                {
                    cargos = JsonConvert.DeserializeObject<List<CargoDTO>>(cargosDB);
                }
                return cargos;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}
