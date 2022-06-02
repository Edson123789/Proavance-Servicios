using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Repositorio: Marketing/ConfiguracionEnvioMailingDetalle
    /// Autor: Fischer Valdez - Wilber Choque - Gian Miranda
    /// Fecha: 05/02/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_ConfiguracionEnvioMailingDetalle
    /// </summary>
    public class ConfiguracionEnvioMailingDetalleRepositorio : BaseRepository<TConfiguracionEnvioMailingDetalle, ConfiguracionEnvioMailingDetalleBO>
    {
        #region Metodos Base
        public ConfiguracionEnvioMailingDetalleRepositorio() : base()
        {
        }
        public ConfiguracionEnvioMailingDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionEnvioMailingDetalleBO> GetBy(Expression<Func<TConfiguracionEnvioMailingDetalle, bool>> filter)
        {
            IEnumerable<TConfiguracionEnvioMailingDetalle> listado = base.GetBy(filter);
            List<ConfiguracionEnvioMailingDetalleBO> listadoBO = new List<ConfiguracionEnvioMailingDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionEnvioMailingDetalleBO objetoBO = Mapper.Map<TConfiguracionEnvioMailingDetalle, ConfiguracionEnvioMailingDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionEnvioMailingDetalleBO FirstById(int id)
        {
            try
            {
                TConfiguracionEnvioMailingDetalle entidad = base.FirstById(id);
                ConfiguracionEnvioMailingDetalleBO objetoBO = new ConfiguracionEnvioMailingDetalleBO();
                Mapper.Map<TConfiguracionEnvioMailingDetalle, ConfiguracionEnvioMailingDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionEnvioMailingDetalleBO FirstBy(Expression<Func<TConfiguracionEnvioMailingDetalle, bool>> filter)
        {
            try
            {
                TConfiguracionEnvioMailingDetalle entidad = base.FirstBy(filter);
                ConfiguracionEnvioMailingDetalleBO objetoBO = Mapper.Map<TConfiguracionEnvioMailingDetalle, ConfiguracionEnvioMailingDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionEnvioMailingDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionEnvioMailingDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionEnvioMailingDetalleBO> listadoBO)
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

        public bool Update(ConfiguracionEnvioMailingDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionEnvioMailingDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionEnvioMailingDetalleBO> listadoBO)
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
        private void AsignacionId(TConfiguracionEnvioMailingDetalle entidad, ConfiguracionEnvioMailingDetalleBO objetoBO)
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

        private TConfiguracionEnvioMailingDetalle MapeoEntidad(ConfiguracionEnvioMailingDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionEnvioMailingDetalle entidad = new TConfiguracionEnvioMailingDetalle();
                entidad = Mapper.Map<ConfiguracionEnvioMailingDetalleBO, TConfiguracionEnvioMailingDetalle>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ConfiguracionEnvioMailingDetalleBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TConfiguracionEnvioMailingDetalle, bool>>> filters, Expression<Func<TConfiguracionEnvioMailingDetalle, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TConfiguracionEnvioMailingDetalle> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ConfiguracionEnvioMailingDetalleBO> listadoBO = new List<ConfiguracionEnvioMailingDetalleBO>();

            foreach (var itemEntidad in listado)
            {
                ConfiguracionEnvioMailingDetalleBO objetoBO = Mapper.Map<TConfiguracionEnvioMailingDetalle, ConfiguracionEnvioMailingDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        public List<ListaAlumnoMailingDTO> ObtenerRegistrosParaEnvioPersonalizado(int IdMatriculaCabecera)
        {
            try
            {
                List<ListaAlumnoMailingDTO> listaAlumnos = new List<ListaAlumnoMailingDTO>();
                string _query = "SP_ObtenerAlumnosParaEnvioPersonalizado";
                string query = _dapper.QuerySPDapper(_query, new { IdMatriculaCabecera });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    listaAlumnos = JsonConvert.DeserializeObject<List<ListaAlumnoMailingDTO>>(query);
                }
                return listaAlumnos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public string ObtenerContenidoPlantilla(int IdPlantilla)
        {
            try
            {
                ValorStringDTO plantilla = new ValorStringDTO();
                string _query = "Select contenido as Valor From mkt.V_ObtenerContenidoPlantilla where IdPlantilla = @IdPlantilla";
                string query = _dapper.QuerySPDapper(_query, new { IdPlantilla });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    plantilla = JsonConvert.DeserializeObject<ValorStringDTO>(query);
                    return plantilla.Valor;
                }
                return "";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Inserta un nuevo registro en la tabla mkt.T_ConfiguracionEnvioMailingDetalle
        /// </summary>
        /// <param name="listaConfiguracionEnvioMailingDetalle">Lista de objeto de tipo ConfiguracionEnvioMailingDetalleBO</param>
        /// <returns>Retorna booleano dependiendo del resultado final de la insercion</returns>
        public bool InsertarConfiguracionEnvioMailingDetalle(List<ConfiguracionEnvioMailingDetalleBO> listaConfiguracionEnvioMailingDetalle)
        {
            try
            {
                string spQuery = "[mkt].[SP_InsertarConfiguracionEnvioMailingDetalle]";

                foreach (var filtro in listaConfiguracionEnvioMailingDetalle)
                {
                    try
                    {
                        var query = _dapper.QuerySPFirstOrDefault(spQuery, new
                        {
                            filtro.Asunto,
                            filtro.CuerpoHtml,
                            filtro.EnviadoCorrectamente,
                            filtro.IdConfiguracionEnvioMailing,
                            filtro.IdConjuntoListaResultado,
                            filtro.UsuarioCreacion,
                            filtro.UsuarioModificacion,
                            filtro.MensajeError,
                            filtro.IdMandrilEnvioCorreo
                        });
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Verifica existencia de mkt.T_ConfiguracionEnvioMailingDetalle
        /// </summary>
        /// <param name="idConfiguracionEnvioMailingDetalle">Id de ConfiguracionEnvioMailingDetalle (PK de la tabla mkt.T_ConfiguracionEnvioMailingDetalle)</param>
        /// <returns>Booleano para determinar si existe o no una configuracion detalle para envio de mailing</returns>
        public bool ExisteConfiguracionEnvioMailingDetalle(int idConfiguracionEnvioMailingDetalle)
        {
            try
            {
                string spQuery = "[mkt].[SP_ExisteConfiguracionEnvioMailingDetalle]";

                var query = _dapper.QuerySPFirstOrDefault(spQuery, new
                {
                    Id = idConfiguracionEnvioMailingDetalle
                });

                return !string.IsNullOrEmpty(query) && !query.Contains("[]");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Busca un registro por el Id en mkt.T_ConfiguracionEnvioMailingDetalle
        /// </summary>
        /// <param name="idConfiguracionEnvioMailingDetalle">Id de ConfiguracionEnvioMailingDetalle (PK de la tabla mkt.T_ConfiguracionEnvioMailingDetalle)</param>
        /// <returns>Registro con todos los campos en la tabla mkt.T_ConfiguracionEnvioMailingDetalle</returns>
        public ConfiguracionEnvioMailingDetalleBO BuscaConfiguracionEnvioMailingDetallePorId(int idConfiguracionEnvioMailingDetalle)
        {
            try
            {
                var configuracionEnvioMailingDetalle = new ConfiguracionEnvioMailingDetalleBO();

                string spQuery = "[mkt].[SP_BuscaConfiguracionEnvioMailingDetallePorId]";

                var query = _dapper.QuerySPFirstOrDefault(spQuery, new
                {
                    Id = idConfiguracionEnvioMailingDetalle
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    configuracionEnvioMailingDetalle = JsonConvert.DeserializeObject<ConfiguracionEnvioMailingDetalleBO>(query);
                }

                return configuracionEnvioMailingDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Busca un registro por el IdConfiguracionEnvioMailing y el flag de EnviadoCorrectamente en mkt.T_ConfiguracionEnvioMailingDetalle
        /// </summary>
        /// <param name="idConfiguracionEnvioMailing">Id de la configuracion de envio mailing</param>
        /// <param name="enviadoCorrectamente">Flag para evaluar si se envio correctamente el mailing</param>
        /// <returns>Registro con todos los campos en la tabla mkt.T_ConfiguracionEnvioMailingDetalle</returns>
        public List<ConfiguracionEnvioMailingDetalleBO> BuscaConfiguracionEnvioMailingDetallePorIdConfiguracionEnvioMailing(int idConfiguracionEnvioMailing, bool enviadoCorrectamente)
        {
            try
            {
                var configuracionEnvioMailingDetalle = new List<ConfiguracionEnvioMailingDetalleBO>();

                string spQuery = "[mkt].[SP_BuscaConfiguracionEnvioMailingDetallePorIdConfiguracionEnvioMailing]";

                var query = _dapper.QuerySPDapper(spQuery, new
                {
                    IdConfiguracionEnvioMailing = idConfiguracionEnvioMailing,
                    EnviadoCorrectamente = enviadoCorrectamente
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    configuracionEnvioMailingDetalle = JsonConvert.DeserializeObject<List<ConfiguracionEnvioMailingDetalleBO>>(query);
                }

                return configuracionEnvioMailingDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Actualiza un registro en la tabla mkt.T_ConfiguracionEnvioMailingDetalle
        /// </summary>
        /// <param name="filtro">Objeto de tipo ConfiguracionEnvioMailingDetalleBO</param>
        /// <returns>Retorna booleano dependiendo del resultado final de la actualizacion</returns>
        public bool ActualizarConfiguracionEnvioMailingDetalle(ConfiguracionEnvioMailingDetalleBO filtro)
        {
            try
            {
                string spQuery = "[mkt].[SP_ActualizarConfiguracionEnvioMailingDetalle]";

                var query = _dapper.QuerySPFirstOrDefault(spQuery, new
                {
                    filtro.EnviadoCorrectamente,
                    filtro.MensajeError,
                    filtro.IdMandrilEnvioCorreo,
                    filtro.UsuarioModificacion,
                    filtro.FechaModificacion,
                    filtro.Id
                });

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}

