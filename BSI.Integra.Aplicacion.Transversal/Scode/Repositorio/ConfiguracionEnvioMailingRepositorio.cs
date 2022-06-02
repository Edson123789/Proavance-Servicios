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
    /// Repositorio: Marketing/ConfioguracionEnvioMailing
    /// Autor: Wilber Choque - Gian Miranda
    /// Fecha: 05/02/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_ConfiguracionEnvioMailing
    /// </summary>
    public class ConfiguracionEnvioMailingRepositorio : BaseRepository<TConfiguracionEnvioMailing, ConfiguracionEnvioMailingBO>
    {
        #region Metodos Base
        public ConfiguracionEnvioMailingRepositorio() : base()
        {
        }
        public ConfiguracionEnvioMailingRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConfiguracionEnvioMailingBO> GetBy(Expression<Func<TConfiguracionEnvioMailing, bool>> filter)
        {
            IEnumerable<TConfiguracionEnvioMailing> listado = base.GetBy(filter);
            List<ConfiguracionEnvioMailingBO> listadoBO = new List<ConfiguracionEnvioMailingBO>();
            foreach (var itemEntidad in listado)
            {
                ConfiguracionEnvioMailingBO objetoBO = Mapper.Map<TConfiguracionEnvioMailing, ConfiguracionEnvioMailingBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConfiguracionEnvioMailingBO FirstById(int id)
        {
            try
            {
                TConfiguracionEnvioMailing entidad = base.FirstById(id);
                ConfiguracionEnvioMailingBO objetoBO = new ConfiguracionEnvioMailingBO();
                Mapper.Map<TConfiguracionEnvioMailing, ConfiguracionEnvioMailingBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConfiguracionEnvioMailingBO FirstBy(Expression<Func<TConfiguracionEnvioMailing, bool>> filter)
        {
            try
            {
                TConfiguracionEnvioMailing entidad = base.FirstBy(filter);
                ConfiguracionEnvioMailingBO objetoBO = Mapper.Map<TConfiguracionEnvioMailing, ConfiguracionEnvioMailingBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConfiguracionEnvioMailingBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConfiguracionEnvioMailing entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConfiguracionEnvioMailingBO> listadoBO)
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

        public bool Update(ConfiguracionEnvioMailingBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConfiguracionEnvioMailing entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConfiguracionEnvioMailingBO> listadoBO)
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
        private void AsignacionId(TConfiguracionEnvioMailing entidad, ConfiguracionEnvioMailingBO objetoBO)
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

        private TConfiguracionEnvioMailing MapeoEntidad(ConfiguracionEnvioMailingBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionEnvioMailing entidad = new TConfiguracionEnvioMailing();
                entidad = Mapper.Map<ConfiguracionEnvioMailingBO, TConfiguracionEnvioMailing>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<ConfiguracionEnvioMailingBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TConfiguracionEnvioMailing, bool>>> filters, Expression<Func<TConfiguracionEnvioMailing, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TConfiguracionEnvioMailing> listado = base.GetFiltered(filters, orderBy, ascending);
            List<ConfiguracionEnvioMailingBO> listadoBO = new List<ConfiguracionEnvioMailingBO>();

            foreach (var itemEntidad in listado)
            {
                ConfiguracionEnvioMailingBO objetoBO = Mapper.Map<TConfiguracionEnvioMailing, ConfiguracionEnvioMailingBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// Autor: Wilber Choque - Gian Miranda
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene la configuracion por un conjunto lista
        /// </summary>
        /// <param name="idConjuntoLista">Id del conjunto lista (PK de la tabla mkt.T_ConjuntoLista)</param>
        /// <returns>Lista de objetos (ConjuntoListaDetalleMailingMasivoDTO)</returns>
        public List<ConjuntoListaDetalleMailingMasivoDTO> ObtenerConfiguracionPorConjuntoLista(int idConjuntoLista)
        {
            try
            {
                List<ConjuntoListaDetalleMailingMasivoDTO> Configuracion = new List<ConjuntoListaDetalleMailingMasivoDTO>();
                string queryConfiguraciones = $@"
                                                SELECT Id, 
                                                       Nombre, 
                                                       Descripcion, 
                                                       IdConjuntoListaDetalle, 
                                                       IdPlantilla
                                                FROM mkt.V_MailingConfiguracionLista
                                                WHERE Activo = 1
                                                      AND EstadoConjuntoListaDetalle = 1
                                                      AND IdConjuntoLista = @idConjuntoLista";

                var queryConfiguracionesFinal = _dapper.QueryDapper(queryConfiguraciones, new { idConjuntoLista });

                if (queryConfiguracionesFinal != "[]" && queryConfiguracionesFinal != "null")
                {
                    Configuracion = JsonConvert.DeserializeObject<List<ConjuntoListaDetalleMailingMasivoDTO>>(queryConfiguracionesFinal);
                    return Configuracion;
                }
                return Configuracion;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Wilber Choque - Gian Miranda
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene la configuracion por un conjunto lista dinamico
        /// </summary>
        /// <param name="idConjuntoLista">Id del conjunto lista (PK de la tabla mkt.T_ConjuntoLista)</param>
        /// <returns>Lista de objetos (ConjuntoListaDetalleMailingDinamicoDTO)</returns>
        public List<ConjuntoListaDetalleMailingDinamicoDTO> ObtenerConfiguracionPorConjuntoListaDinamica(int idConjuntoLista)
        {
            try
            {
                List<ConjuntoListaDetalleMailingDinamicoDTO> Configuracion = new List<ConjuntoListaDetalleMailingDinamicoDTO>();
                string _queryConfiguraciones = $@"
                                                SELECT Id, 
                                                       Nombre, 
                                                       Descripcion, 
                                                       IdConjuntoListaDetalle, 
                                                       IdPlantilla
                                                FROM mkt.V_MailingConfiguracionLista
                                                WHERE Activo = 1
                                                      AND EstadoConjuntoListaDetalle = 1
                                                      AND IdConjuntoLista = @idConjuntoLista";

                var queryConfiguraciones = _dapper.QueryDapper(_queryConfiguraciones, new { idConjuntoLista });

                if (queryConfiguraciones != "[]" && queryConfiguraciones != "null")
                {
                    Configuracion = JsonConvert.DeserializeObject<List<ConjuntoListaDetalleMailingDinamicoDTO>>(queryConfiguraciones);
                    return Configuracion;
                }
                return Configuracion;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Wilber Choque - Gian Miranda
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene mailing masivo para subir listas automaticas.
        /// </summary>
        /// <returns>Lista de objetos (MailingMasivoAutomaticoServiceDTO)</returns>
        public List<MailingMasivoAutomaticoServiceDTO> ObtenerMailingMasivoParaSubirListasAutomaticas()
        {
            try
            {
                List<MailingMasivoAutomaticoServiceDTO> lista = new List<MailingMasivoAutomaticoServiceDTO>();
                DateTime HoraActual = DateTime.Now;
                string FechaInicioActividad = HoraActual.ToString("dd/MM/yyyy");


                var _query = "";
                var query = _dapper.QueryDapper(_query, new { FechaInicioActividad = FechaInicioActividad });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    lista = JsonConvert.DeserializeObject<List<MailingMasivoAutomaticoServiceDTO>>(query);
                }
                return lista;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Wilber Choque - Gian Miranda
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene los envios masivos
        /// </summary>
        /// <param name="id"> Id de correo</param>
        /// <returns>ObjetoDTO: CorreoDTO</returns>
        public CorreoDTO ObtenerEnvioMasivo(int id)
        {
            try
            {
                CorreoDTO Configuracion = new CorreoDTO();
                string _queryConfiguraciones = $@"
                                             SELECT Id, 
                                                   Asunto, 
                                                   EmailBody, 
                                                   Fecha, 
                                                   Remitente, 
                                                   Destinatarios, 
                                                   Seen, 
                                                   TotalCorreos, 
                                                   IdPersonal, 
                                                   IdAlumno, 
                                                   ConCopia,    
                                                   EnvioMasivoMandrill
                                            FROM mkt.V_ObtenerEnviosMasivos
                                            WHERE Id = @id;";

                var queryConfiguraciones = _dapper.FirstOrDefault(_queryConfiguraciones, new { id });

                if (!string.IsNullOrEmpty(queryConfiguraciones) && queryConfiguraciones != "null")
                {
                    Configuracion = JsonConvert.DeserializeObject<CorreoDTO>(queryConfiguraciones);
                    return Configuracion;
                }
                return Configuracion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Wilber Choque - Gian Miranda
        /// Fecha: 30/04/2021
        /// <summary>
        /// Obtiene los envios masivos
        /// </summary>
        /// <param name="email"> Correo. </param>
        /// <returns>Lista de objetos (CorreoDTO)</returns>
        public List<CorreoDTO> ObtenerEnviosMasivos(string email)
        {
            try
            {
                List<CorreoDTO> Configuracion = new List<CorreoDTO>();
                string _queryConfiguraciones = $@"
                                                    SELECT Id, 
                                                           Asunto,
                                                           EmailBody,
                                                           Fecha, 
                                                           Remitente, 
                                                           Destinatarios, 
                                                           Seen, 
                                                           TotalCorreos, 
                                                           IdPersonal, 
                                                           IdAlumno, 
                                                           ConCopia,
                                                           EnvioMasivoMandrill
                                                    FROM mkt.V_ObtenerEnviosMasivos
                                                    WHERE Destinatarios = @email;";

                var queryConfiguraciones = _dapper.QueryDapper(_queryConfiguraciones, new { email });

                if (queryConfiguraciones != "[]" && queryConfiguraciones != "null")
                {
                    Configuracion = JsonConvert.DeserializeObject<List<CorreoDTO>>(queryConfiguraciones);
                    return Configuracion;
                }
                return Configuracion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}

