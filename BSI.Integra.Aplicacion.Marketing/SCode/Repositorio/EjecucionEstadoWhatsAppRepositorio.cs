using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.DTOs.Marketing;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class EjecucionEstadoWhatsAppRepositorio : BaseRepository<TEjecucionEstadoWhatsApp, EjecucionEstadoWhatsAppBO>
    {
        #region Metodos Base
        public EjecucionEstadoWhatsAppRepositorio() : base()
        {
        }
        public EjecucionEstadoWhatsAppRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<EjecucionEstadoWhatsAppBO> GetBy(Expression<Func<TEjecucionEstadoWhatsApp, bool>> filter)
        {
            IEnumerable<TEjecucionEstadoWhatsApp> listado = base.GetBy(filter);
            List<EjecucionEstadoWhatsAppBO> listadoBO = new List<EjecucionEstadoWhatsAppBO>();
            foreach (var itemEntidad in listado)
            {
                EjecucionEstadoWhatsAppBO objetoBO = Mapper.Map<TEjecucionEstadoWhatsApp, EjecucionEstadoWhatsAppBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public EjecucionEstadoWhatsAppBO FirstById(int id)
        {
            try
            {
                TEjecucionEstadoWhatsApp entidad = base.FirstById(id);
                EjecucionEstadoWhatsAppBO objetoBO = new EjecucionEstadoWhatsAppBO();
                Mapper.Map<TEjecucionEstadoWhatsApp, EjecucionEstadoWhatsAppBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public EjecucionEstadoWhatsAppBO FirstBy(Expression<Func<TEjecucionEstadoWhatsApp, bool>> filter)
        {
            try
            {
                TEjecucionEstadoWhatsApp entidad = base.FirstBy(filter);
                EjecucionEstadoWhatsAppBO objetoBO = Mapper.Map<TEjecucionEstadoWhatsApp, EjecucionEstadoWhatsAppBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(EjecucionEstadoWhatsAppBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TEjecucionEstadoWhatsApp entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<EjecucionEstadoWhatsAppBO> listadoBO)
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

        public bool Update(EjecucionEstadoWhatsAppBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TEjecucionEstadoWhatsApp entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<EjecucionEstadoWhatsAppBO> listadoBO)
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
        private void AsignacionId(TEjecucionEstadoWhatsApp entidad, EjecucionEstadoWhatsAppBO objetoBO)
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

        private TEjecucionEstadoWhatsApp MapeoEntidad(EjecucionEstadoWhatsAppBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TEjecucionEstadoWhatsApp entidad = new TEjecucionEstadoWhatsApp();
                entidad = Mapper.Map<EjecucionEstadoWhatsAppBO, TEjecucionEstadoWhatsApp>(objetoBO,
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

        /// Repositorio: EjecucionEstadoWhatsAppRepositorio
        /// Autor: Jashin Salazar.
        /// Fecha: 12/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener combos de periodos para interfaz EjecutarFiltroSegmento
        /// </summary>
        /// <returns> List<TiempoFrecuenciaDTO> </returns>
        public List<TiempoFrecuenciaDTO> ObtenerListaPeriodo()
        {
            try
            {
                List<TiempoFrecuenciaDTO> registros = new List<TiempoFrecuenciaDTO>();
                var query = "SELECT Id, Nombre FROM mkt.T_TiempoFrecuencia WHERE Id IN (2,3,4)";
                var resultado = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    registros = JsonConvert.DeserializeObject<List<TiempoFrecuenciaDTO>>(resultado);
                }
                return registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Repositorio: EjecucionEstadoWhatsAppRepositorio
        /// Autor: Jashin Salazar.
        /// Fecha: 12/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener combos de periodos para interfaz EjecutarFiltroSegmento
        /// </summary>
        /// <returns> ConfiguracionEjecucionEstadoWhatsAppDTO </returns>
        public ConfiguracionEjecucionEstadoWhatsAppDTO ObtenerConfiguracionActual()
        {
            try
            {
                ConfiguracionEjecucionEstadoWhatsAppDTO registros = new ConfiguracionEjecucionEstadoWhatsAppDTO();
                var query = "SELECT Id,FechaEjecucion,FechaProximaEjecucion,CantidadTiempoFrecuencia,IdTiempoFrecuencia FROM mkt.V_ObtenerConfiguracionEjecucionEstadoWhatsApp";
                var resultado = _dapper.FirstOrDefault(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    registros = JsonConvert.DeserializeObject<ConfiguracionEjecucionEstadoWhatsAppDTO>(resultado);
                }
                return registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jashin Salazar.
        /// Fecha: 12/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener combos de periodos para interfaz EjecutarFiltroSegmento
        /// </summary>
        /// <returns> ConfiguracionEjecucionEstadoWhatsAppDTO </returns>
        public ConfiguracionEjecucionEstadoWhatsAppDTO VerificarFecha(DateTime fecha)
        {
            try
            {
                ConfiguracionEjecucionEstadoWhatsAppDTO registros = new ConfiguracionEjecucionEstadoWhatsAppDTO();
                var query = "SELECT Id FROM mkt.T_EjecucionEstadoWhatsApp WHERE CAST(@Fecha AS DATE) BETWEEN FechaInicio AND FechaFin AND Estado=1";
                var resultado = _dapper.FirstOrDefault(query, new { Fecha=fecha});
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    registros = JsonConvert.DeserializeObject<ConfiguracionEjecucionEstadoWhatsAppDTO>(resultado);
                }
                return registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jashin Salazar.
        /// Fecha: 12/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener combos de periodos para interfaz EjecutarFiltroSegmento
        /// </summary>
        /// <returns> ConfiguracionEjecucionEstadoWhatsAppDTO </returns>
        public ConfiguracionEjecucionEstadoWhatsAppDTO VerificarFechaModificacion(DateTime fecha, int id)
        {
            try
            {
                ConfiguracionEjecucionEstadoWhatsAppDTO registros = new ConfiguracionEjecucionEstadoWhatsAppDTO();
                var query = "SELECT Id FROM mkt.T_EjecucionEstadoWhatsApp WHERE CAST(@Fecha AS DATE) BETWEEN FechaInicio AND FechaFin AND Estado=1 AND Id != @Id";
                var resultado = _dapper.FirstOrDefault(query, new { Fecha = fecha, Id=id});
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    registros = JsonConvert.DeserializeObject<ConfiguracionEjecucionEstadoWhatsAppDTO>(resultado);
                }
                return registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Repositorio: EjecucionEstadoWhatsAppRepositorio
        /// Autor: Jashin Salazar.
        /// Fecha: 12/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtener combos de periodos para interfaz EjecutarFiltroSegmento
        /// </summary>
        /// <returns> List<GrillaEjecucionEstadoWhatsAppDTO> </returns>
        public List<GrillaEjecucionEstadoWhatsAppDTO> ObtenerTodoEjecucionEstadoWhatsApp()
        {
            try
            {
                List<GrillaEjecucionEstadoWhatsAppDTO> registros = new List<GrillaEjecucionEstadoWhatsAppDTO>();
                var query = "SELECT * FROM mkt.V_ObtenerEjecucionEstadoWhatsApp";
                var resultado = _dapper.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    registros = JsonConvert.DeserializeObject<List<GrillaEjecucionEstadoWhatsAppDTO>>(resultado);
                }
                return registros;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
