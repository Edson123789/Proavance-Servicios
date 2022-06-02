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
    public class ConfiguracionEnvioAutomaticoRepositorio : BaseRepository<TConfiguracionEnvioAutomatico, ConfiguracionEnvioAutomaticoBO>
    {
        #region Metodos Base
        public ConfiguracionEnvioAutomaticoRepositorio() : base()
        {
        }
        public ConfiguracionEnvioAutomaticoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionEnvioAutomaticoBO> GetBy(Expression<Func<TConfiguracionEnvioAutomatico, bool>> filter)
        {
            IEnumerable<TConfiguracionEnvioAutomatico> listado = base.GetBy(filter);
            List<ConfiguracionEnvioAutomaticoBO> listadoBO = new List<ConfiguracionEnvioAutomaticoBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionEnvioAutomaticoBO objetoBO = Mapper.Map<TConfiguracionEnvioAutomatico, ConfiguracionEnvioAutomaticoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionEnvioAutomaticoBO FirstById(int id)
        {
            try
            {
                TConfiguracionEnvioAutomatico entidad = base.FirstById(id);
                ConfiguracionEnvioAutomaticoBO objetoBO = new ConfiguracionEnvioAutomaticoBO();
                Mapper.Map<TConfiguracionEnvioAutomatico, ConfiguracionEnvioAutomaticoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionEnvioAutomaticoBO FirstBy(Expression<Func<TConfiguracionEnvioAutomatico, bool>> filter)
        {
            try
            {
                TConfiguracionEnvioAutomatico entidad = base.FirstBy(filter);
                ConfiguracionEnvioAutomaticoBO objetoBO = Mapper.Map<TConfiguracionEnvioAutomatico, ConfiguracionEnvioAutomaticoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionEnvioAutomaticoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionEnvioAutomatico entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionEnvioAutomaticoBO> listadoBO)
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

        public bool Update(ConfiguracionEnvioAutomaticoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionEnvioAutomatico entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionEnvioAutomaticoBO> listadoBO)
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
        private void AsignacionId(TConfiguracionEnvioAutomatico entidad, ConfiguracionEnvioAutomaticoBO objetoBO)
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

        private TConfiguracionEnvioAutomatico MapeoEntidad(ConfiguracionEnvioAutomaticoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionEnvioAutomatico entidad = new TConfiguracionEnvioAutomatico();
                entidad = Mapper.Map<ConfiguracionEnvioAutomaticoBO, TConfiguracionEnvioAutomatico>(objetoBO,
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

        public List<ObtenerConfiguracionEnvioDTO> ObtenerConfiguracionEnvioAutomatico()
        {
            try
            {
                var data = new List<ObtenerConfiguracionEnvioDTO>();
                var _query = "SELECT Id,IdEstado_Inicial AS IdEstadoInicial ,IdEstado_Destino AS IdEstadoDestino,IdSubEstado_Inicial AS IdSubEstadoInicial,IdSubEstado_Destino AS IdSubEstadoDestino,EnvioWhatsApp,EnvioCorreo,EnvioMensajeTexto,UsuarioModificacion,FechaModificacion,Estado  FROM mkt.T_ConfiguracionEnvioAutomatico WHERE Estado = 1";
                var respuesta = _dapper.QueryDapper(_query, null);
                if (!respuesta.Contains("[]") || !respuesta.Contains("null") || !respuesta.Contains(""))
                {
                    data = JsonConvert.DeserializeObject<List<ObtenerConfiguracionEnvioDTO>>(respuesta);
                }
                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<ConfiguracionEnvioDTO> InsertarConfiguracion(ConfiguracionEnvioAutomaticoDTO objeto)
        {
            try
            {
                string _queryInsertar = "mkt.SP_InsertarConfiguracionEnvioAutomatico";
                var queryInsert = _dapper.QuerySPDapper(_queryInsertar, new
                {
                    IdEstadoInicial = objeto.IdEstadoInicial,
                    IdEstadoDestino = objeto.IdEstadoDestino,
                    IdSubEstadoInicial = objeto.IdSubEstadoInicial,
                    IdSubEstadoDestino = objeto.IdSubEstadoDestino,
                    AplicaWhatsApp = objeto.AplicaWhatsApp,
                    AplicaSMS = objeto.AplicaSMS,
                    AplicaCorreo = objeto.AplicaCorreo,
                    UsuarioConfiguracion = objeto.Usuario
                });
                return JsonConvert.DeserializeObject<List<ConfiguracionEnvioDTO>>(queryInsert);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<ConfiguracionEnvioDTO> ActualizarConfiguracion(ConfiguracionEnvioAutomaticoDTO objeto)
        {
            try
            {
                string _queryActualizar = "mkt.SP_ActualizarConfiguracionEnvioAutomatico";
                var queryActualizar = _dapper.QuerySPDapper(_queryActualizar, new
                {
                    IdConfiguracion = objeto.Id,
                    IdEstadoInicial = objeto.IdEstadoInicial,
                    IdEstadoDestino = objeto.IdEstadoDestino,
                    IdSubEstadoInicial = objeto.IdSubEstadoInicial,
                    IdSubEstadoDestino = objeto.IdSubEstadoDestino,
                    AplicaWhatsApp = objeto.AplicaWhatsApp,
                    AplicaSMS = objeto.AplicaSMS,
                    AplicaCorreo = objeto.AplicaCorreo,
                    UsuarioConfiguracion = objeto.Usuario
                });
                return JsonConvert.DeserializeObject<List<ConfiguracionEnvioDTO>>(queryActualizar);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ConfiguracionEnvioDTO> EliminarConfiguracion(int IdConfiguracion, string Usuario)
        {
            try
            {
                string _queryEliminar = "mkt.SP_EliminarConfiguracionEnvioAutomatico";
                var queryEliminar = _dapper.QuerySPDapper(_queryEliminar, new
                {
                    IdConfiguracion = IdConfiguracion,
                    Usuario = Usuario
                });
                return JsonConvert.DeserializeObject<List<ConfiguracionEnvioDTO>>(queryEliminar);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<DatosConfiguracionEnvioAutomaticoOperacionesDTO> ObtenerConfiguracionEnvioAutomaticoOperaciones()
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
