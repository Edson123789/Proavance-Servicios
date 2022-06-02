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
    /// Repositorio: EstadoOcurrenciaRepositorio
    /// Autor: Edgar S.
    /// Fecha: 08/03/2021
    /// <summary>
    /// Gestión de Estado de Ocurrencia
    /// </summary>
    public class EstadoOcurrenciaRepositorio : BaseRepository<TEstadoOcurrencia, EstadoOcurrenciaBO>
    {
        #region Metodos Base
        public EstadoOcurrenciaRepositorio() : base()
        {
        }
     
        public EstadoOcurrenciaRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EstadoOcurrenciaBO> GetBy(Expression<Func<TEstadoOcurrencia, bool>> filter)
        {
            IEnumerable<TEstadoOcurrencia> listado = base.GetBy(filter);
            List<EstadoOcurrenciaBO> listadoBO = new List<EstadoOcurrenciaBO>();
            foreach (var itemEntidad in listado)
            {
                EstadoOcurrenciaBO objetoBO = Mapper.Map<TEstadoOcurrencia, EstadoOcurrenciaBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EstadoOcurrenciaBO FirstById(int id)
        {
            try
            {
                TEstadoOcurrencia entidad = base.FirstById(id);
                EstadoOcurrenciaBO objetoBO = new EstadoOcurrenciaBO();
                Mapper.Map<TEstadoOcurrencia, EstadoOcurrenciaBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EstadoOcurrenciaBO FirstBy(Expression<Func<TEstadoOcurrencia, bool>> filter)
        {
            try
            {
                TEstadoOcurrencia entidad = base.FirstBy(filter);
                EstadoOcurrenciaBO objetoBO = Mapper.Map<TEstadoOcurrencia, EstadoOcurrenciaBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EstadoOcurrenciaBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEstadoOcurrencia entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EstadoOcurrenciaBO> listadoBO)
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

        public bool Update(EstadoOcurrenciaBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEstadoOcurrencia entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EstadoOcurrenciaBO> listadoBO)
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
        private void AsignacionId(TEstadoOcurrencia entidad, EstadoOcurrenciaBO objetoBO)
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

        private TEstadoOcurrencia MapeoEntidad(EstadoOcurrenciaBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEstadoOcurrencia entidad = new TEstadoOcurrencia();
                entidad = Mapper.Map<EstadoOcurrenciaBO, TEstadoOcurrencia>(objetoBO,
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


        /// Repositorio: EstadoOcurrenciaRepositorio
        /// Autor: Edgar S.
        /// Fecha: 08/03/2021
        /// <summary>
        /// Obtiene las lista de estados de ocurrencias para combo box
        /// </summary>
        /// <param></param>
        /// <returns> Lista de ObjetosDTO: List<EstadoOcurrenciaFiltroDTO> </returns>
        public List<EstadoOcurrenciaFiltroDTO> ObtenerEstadoOcurrenciasParaFiltro()
        {
            try
            {
                List<EstadoOcurrenciaFiltroDTO> estados = new List<EstadoOcurrenciaFiltroDTO>();
                string _query = "SELECT Id, Nombre FROM com.T_EstadoOcurrencia WHERE Estado=1";
                var estadosDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(estadosDB) && !estadosDB.Contains("[]"))
                {
                    estados = JsonConvert.DeserializeObject<List<EstadoOcurrenciaFiltroDTO>>(estadosDB);
                }
                return estados;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
