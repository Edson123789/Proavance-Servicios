using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class ConfiguracionEnvioAutomaticoDetalleRepositorio : BaseRepository<TConfiguracionEnvioAutomaticoDetalle, ConfiguracionEnvioAutomaticoDetalleBO>
    {
        #region Metodos Base
        public ConfiguracionEnvioAutomaticoDetalleRepositorio() : base()
        {
        }
        public ConfiguracionEnvioAutomaticoDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionEnvioAutomaticoDetalleBO> GetBy(Expression<Func<TConfiguracionEnvioAutomaticoDetalle, bool>> filter)
        {
            IEnumerable<TConfiguracionEnvioAutomaticoDetalle> listado = base.GetBy(filter);
            List<ConfiguracionEnvioAutomaticoDetalleBO> listadoBO = new List<ConfiguracionEnvioAutomaticoDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionEnvioAutomaticoDetalleBO objetoBO = Mapper.Map<TConfiguracionEnvioAutomaticoDetalle, ConfiguracionEnvioAutomaticoDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionEnvioAutomaticoDetalleBO FirstById(int id)
        {
            try
            {
                TConfiguracionEnvioAutomaticoDetalle entidad = base.FirstById(id);
                ConfiguracionEnvioAutomaticoDetalleBO objetoBO = new ConfiguracionEnvioAutomaticoDetalleBO();
                Mapper.Map<TConfiguracionEnvioAutomaticoDetalle, ConfiguracionEnvioAutomaticoDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionEnvioAutomaticoDetalleBO FirstBy(Expression<Func<TConfiguracionEnvioAutomaticoDetalle, bool>> filter)
        {
            try
            {
                TConfiguracionEnvioAutomaticoDetalle entidad = base.FirstBy(filter);
                ConfiguracionEnvioAutomaticoDetalleBO objetoBO = Mapper.Map<TConfiguracionEnvioAutomaticoDetalle, ConfiguracionEnvioAutomaticoDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionEnvioAutomaticoDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionEnvioAutomaticoDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionEnvioAutomaticoDetalleBO> listadoBO)
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

        public bool Update(ConfiguracionEnvioAutomaticoDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionEnvioAutomaticoDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionEnvioAutomaticoDetalleBO> listadoBO)
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
        private void AsignacionId(TConfiguracionEnvioAutomaticoDetalle entidad, ConfiguracionEnvioAutomaticoDetalleBO objetoBO)
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

        private TConfiguracionEnvioAutomaticoDetalle MapeoEntidad(ConfiguracionEnvioAutomaticoDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionEnvioAutomaticoDetalle entidad = new TConfiguracionEnvioAutomaticoDetalle();
                entidad = Mapper.Map<ConfiguracionEnvioAutomaticoDetalleBO, TConfiguracionEnvioAutomaticoDetalle>(objetoBO,
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
        /// Obtiene centiles asignados a una evaluacion
        /// </summary>
        /// <param name="idExamenTest"></param>
        /// <returns></returns>
        public List<ConfiguracionEnvioAutomaticoDetalleDTO> ObtenerConfiguracionEnvioAutomaticoDetalle(int idConfiguracionEnvioAutomatico)
        {
            try
            {
                List<ConfiguracionEnvioAutomaticoDetalleDTO> Configuracion = new List<ConfiguracionEnvioAutomaticoDetalleDTO>();
                var campos = "Id,IdConfiguracionEnvioAutomatico,IdTipoEnvioAutomatico,IdTiempoFrecuencia,IdPlantilla,Valor,HoraEnvioAutomatico";

                var _query = "SELECT " + campos + " FROM  mkt.T_ConfiguracionEnvioAutomaticoDetalle WHERE Estado=1 AND IdConfiguracionEnvioAutomatico=@idConfiguracionEnvioAutomatico";
                var listaConfiguracionDB = _dapper.QueryDapper(_query, new { IdConfiguracionEnvioAutomatico = idConfiguracionEnvioAutomatico });
                if (!listaConfiguracionDB.Contains("[]") && !string.IsNullOrEmpty(listaConfiguracionDB))
                {
                    Configuracion = JsonConvert.DeserializeObject<List<ConfiguracionEnvioAutomaticoDetalleDTO>>(listaConfiguracionDB);
                }
                return Configuracion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<DatosConfiguracionEnvioAutomaticoOperacionesDTO> ObtenerConfiguracionEnvioAutomatico()
        {
            try
            {
                var query = "Select IdConfiguracionEnvioAutomatico,IdEstadoInicial,IdSubEstadoInicial,IdEstadoDestino,IdSubEstadoDestino,EnvioWhatsApp,EnvioCorreo,EnvioMensajeTexto,IdTipoEnvioAutomatico,IdTiempoFrecuencia,IdPlantilla,Valor,Hora from ope.V_ObtenerConfiguracionEnvioAutomatico where Estado = 1";
                
                var Lista = _dapper.QueryDapper(query, null);
                return JsonConvert.DeserializeObject<List<DatosConfiguracionEnvioAutomaticoOperacionesDTO>>(Lista);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
