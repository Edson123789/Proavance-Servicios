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
    public class CargoMoraRepositorio : BaseRepository<TCargo, CargoMoraBO>
    {
        #region Metodos Base
        public CargoMoraRepositorio() : base()
        {
        }
        public CargoMoraRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CargoMoraBO> GetBy(Expression<Func<TCargo, bool>> filter)
        {
            IEnumerable<TCargo> listado = base.GetBy(filter);
            List<CargoMoraBO> listadoBO = new List<CargoMoraBO>();
            foreach (var itemEntidad in listado)
            {
                CargoMoraBO objetoBO = Mapper.Map<TCargo, CargoMoraBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CargoMoraBO FirstById(int id)
        {
            try
            {
                TCargo entidad = base.FirstById(id);
                CargoMoraBO objetoBO = new CargoMoraBO();
                Mapper.Map<TCargo, CargoMoraBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CargoMoraBO FirstBy(Expression<Func<TCargo, bool>> filter)
        {
            try
            {
                TCargo entidad = base.FirstBy(filter);
                CargoMoraBO objetoBO = Mapper.Map<TCargo, CargoMoraBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CargoMoraBO objetoBO)
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

        public bool Insert(IEnumerable<CargoMoraBO> listadoBO)
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

        public bool Update(CargoMoraBO objetoBO)
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

        public bool Update(IEnumerable<CargoMoraBO> listadoBO)
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
        private void AsignacionId(TCargo entidad, CargoMoraBO objetoBO)
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

        private TCargo MapeoEntidad(CargoMoraBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCargo entidad = new TCargo();
                entidad = Mapper.Map<CargoMoraBO, TCargo>(objetoBO,
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


        public List<CargoDTO> ObtenerTodoCargoMora()
        {
            try
            {
                List<CargoDTO> cargos = new List<CargoDTO>();
                var _query = string.Empty;
                _query = "SELECT Id, Nombre,Descripcion, Orden FROM pla.T_Cargo WHERE  Estado = 1 ORDER BY Orden DESC";
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
        /// Autor: ----------
        /// Fecha: 04/03/2021
        /// Version: 1.0
        /// <summary>
        /// Retorna una lista de los cargos para ser usados en filtros  
        /// </summary>
        /// <param></param>
        /// <returns>Objeto(Id,Nombre)</returns>        
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
    }
}
