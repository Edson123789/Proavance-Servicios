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
    public class SedeRepositorio : BaseRepository<TSede, SedeBO>
    {
        #region Metodos Base
        public SedeRepositorio() : base()
        {
        }
        public SedeRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<SedeBO> GetBy(Expression<Func<TSede, bool>> filter)
        {
            IEnumerable<TSede> listado = base.GetBy(filter);
            List<SedeBO> listadoBO = new List<SedeBO>();
            foreach (var itemEntidad in listado)
            {
                SedeBO objetoBO = Mapper.Map<TSede, SedeBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public SedeBO FirstById(int id)
        {
            try
            {
                TSede entidad = base.FirstById(id);
                SedeBO objetoBO = new SedeBO();
                Mapper.Map<TSede, SedeBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public SedeBO FirstBy(Expression<Func<TSede, bool>> filter)
        {
            try
            {
                TSede entidad = base.FirstBy(filter);
                SedeBO objetoBO = Mapper.Map<TSede, SedeBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(SedeBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TSede entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<SedeBO> listadoBO)
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

        public bool Update(SedeBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TSede entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<SedeBO> listadoBO)
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
        private void AsignacionId(TSede entidad, SedeBO objetoBO)
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

        private TSede MapeoEntidad(SedeBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TSede entidad = new TSede();
                entidad = Mapper.Map<SedeBO, TSede>(objetoBO,
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
        /// Obtiene la lista de nombres de las ciudades de  sedes
        /// </summary>
        /// <returns></returns>
        public List<SedeFiltroCiudadDTO> ObtenerTodoSedeCiudad()
        {
            try
            {
                List<SedeFiltroCiudadDTO> obtenerTodoSedeCiudad = new List<SedeFiltroCiudadDTO>();
                var _query = "SELECT Id, Nombre FROM pla.V_ObtenerTodoSede WHERE EstadoSede = 1 AND EstadoCiudad = 1";
                var obtenerTodoSedeCiudadDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(obtenerTodoSedeCiudadDB) && !obtenerTodoSedeCiudadDB.Contains("[]"))
                {
                    obtenerTodoSedeCiudad = JsonConvert.DeserializeObject<List<SedeFiltroCiudadDTO>>(obtenerTodoSedeCiudadDB);
                }
                return obtenerTodoSedeCiudad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene la lista de nombres de las ciudades de  registradas en pla.T_Cede (Usado para ComboBox)
        /// </summary>
        /// <returns></returns>
        public List<SedeFiltroCiudadDTO> ObtenerNombreCiudadPorCede()
        {
            try
            {
                List<SedeFiltroCiudadDTO> obtenerTodoSedeCiudad = new List<SedeFiltroCiudadDTO>();
                var _query = "SELECT Id, Nombre FROM fin.V_ObtenerAreaNegocio ";
                var obtenerTodoSedeCiudadDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(obtenerTodoSedeCiudadDB) && !obtenerTodoSedeCiudadDB.Contains("[]"))
                {
                    obtenerTodoSedeCiudad = JsonConvert.DeserializeObject<List<SedeFiltroCiudadDTO>>(obtenerTodoSedeCiudadDB);
                }
                return obtenerTodoSedeCiudad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de nombres de las sedes segun lo registrado en los FURS
        /// </summary>
        /// <returns></returns>
        public List<SedeFiltroCiudadDTO> ObtenerListaSedesSegunFur()
        {
            try
            {
                List<SedeFiltroCiudadDTO> obtenerTodoSedeCiudad = new List<SedeFiltroCiudadDTO>();
                var _query = "SELECT Id, Nombre FROM fin.V_ObtenerSedesSegunFurs";
                var obtenerTodoSedeCiudadDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(obtenerTodoSedeCiudadDB) && !obtenerTodoSedeCiudadDB.Contains("[]"))
                {
                    obtenerTodoSedeCiudad = JsonConvert.DeserializeObject<List<SedeFiltroCiudadDTO>>(obtenerTodoSedeCiudadDB);
                }
                return obtenerTodoSedeCiudad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la lIsta de Sedes segun los fur, y que esten asociados a un comprobante con detraccion (utilizado para llenar combobox)
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerListaSedesConComprobanteDetraccion()
        {
            try
            {
                List<FiltroDTO> obtenerTodoSedeCiudad = new List<FiltroDTO>();
                var _query = "SELECT Id, Nombre FROM fin.V_PaisComprobanteDetraccion";
                var obtenerTodoSedeCiudadDB = _dapper.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(obtenerTodoSedeCiudadDB) && !obtenerTodoSedeCiudadDB.Contains("[]"))
                {
                    obtenerTodoSedeCiudad = JsonConvert.DeserializeObject<List<FiltroDTO>>(obtenerTodoSedeCiudadDB);
                }
                return obtenerTodoSedeCiudad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
