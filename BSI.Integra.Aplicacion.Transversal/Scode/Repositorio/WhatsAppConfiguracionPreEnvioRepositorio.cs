using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class WhatsAppConfiguracionPreEnvioRepositorio : BaseRepository<TWhatsAppConfiguracionPreEnvio, WhatsAppConfiguracionPreEnvioBO>
    {
        #region Metodos Base
        public WhatsAppConfiguracionPreEnvioRepositorio() : base()
        {
        }
        public WhatsAppConfiguracionPreEnvioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WhatsAppConfiguracionPreEnvioBO> GetBy(Expression<Func<TWhatsAppConfiguracionPreEnvio, bool>> filter)
        {
            IEnumerable<TWhatsAppConfiguracionPreEnvio> listado = base.GetBy(filter);
            List<WhatsAppConfiguracionPreEnvioBO> listadoBO = new List<WhatsAppConfiguracionPreEnvioBO>();
            foreach (var itemEntidad in listado)
            {
                WhatsAppConfiguracionPreEnvioBO objetoBO = Mapper.Map<TWhatsAppConfiguracionPreEnvio, WhatsAppConfiguracionPreEnvioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WhatsAppConfiguracionPreEnvioBO FirstById(int id)
        {
            try
            {
                TWhatsAppConfiguracionPreEnvio entidad = base.FirstById(id);
                WhatsAppConfiguracionPreEnvioBO objetoBO = new WhatsAppConfiguracionPreEnvioBO();
                Mapper.Map<TWhatsAppConfiguracionPreEnvio, WhatsAppConfiguracionPreEnvioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WhatsAppConfiguracionPreEnvioBO FirstBy(Expression<Func<TWhatsAppConfiguracionPreEnvio, bool>> filter)
        {
            try
            {
                TWhatsAppConfiguracionPreEnvio entidad = base.FirstBy(filter);
                WhatsAppConfiguracionPreEnvioBO objetoBO = Mapper.Map<TWhatsAppConfiguracionPreEnvio, WhatsAppConfiguracionPreEnvioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WhatsAppConfiguracionPreEnvioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWhatsAppConfiguracionPreEnvio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WhatsAppConfiguracionPreEnvioBO> listadoBO)
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

        public bool Update(WhatsAppConfiguracionPreEnvioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWhatsAppConfiguracionPreEnvio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WhatsAppConfiguracionPreEnvioBO> listadoBO)
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
        private void AsignacionId(TWhatsAppConfiguracionPreEnvio entidad, WhatsAppConfiguracionPreEnvioBO objetoBO)
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

        private TWhatsAppConfiguracionPreEnvio MapeoEntidad(WhatsAppConfiguracionPreEnvioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppConfiguracionPreEnvio entidad = new TWhatsAppConfiguracionPreEnvio();
                entidad = Mapper.Map<WhatsAppConfiguracionPreEnvioBO, TWhatsAppConfiguracionPreEnvio>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<WhatsAppConfiguracionPreEnvioBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TWhatsAppConfiguracionPreEnvio, bool>>> filters, Expression<Func<TWhatsAppConfiguracionPreEnvio, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TWhatsAppConfiguracionPreEnvio> listado = base.GetFiltered(filters, orderBy, ascending);
            List<WhatsAppConfiguracionPreEnvioBO> listadoBO = new List<WhatsAppConfiguracionPreEnvioBO>();

            foreach (var itemEntidad in listado)
            {
                WhatsAppConfiguracionPreEnvioBO objetoBO = Mapper.Map<TWhatsAppConfiguracionPreEnvio, WhatsAppConfiguracionPreEnvioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// <summary>
        /// Autor: Jorge Rivera Tito
        /// Descripción: Funcion que permite consumir de la tabla T_WhatsAppConfiguracionPreEnvio los datos pre-procesados del conjunto de lista
        /// </summary>
        /// <param name="IdConjuntoListaDetalle">Identificador del conjunto de lista</param>
        /// <returns>Retorna el conjunto de datso pre-procesados</returns>
        public List<WhatsAppConfiguracionPreEnvioDTO> ListasWhatsAppEnvioAutomaticoMasivoPreProcesada(int IdConjuntoListaDetalle)
        {
            try
            {
                List<WhatsAppConfiguracionPreEnvioDTO> ConfiguracionPre = new List<WhatsAppConfiguracionPreEnvioDTO>();
                string Query = string.Empty;
                Query = "SELECT Id,IdWhatsappMensajePublicidad,IdConjuntoListaResultado,IdAlumno,Celular,IdPais AS IdCodigoPais,NroEjecucion,Validado,Plantilla,IdPersonal,IdPlantilla,IdWhatsAppEstadoValidacion,objetoplantilla,Procesado FROM mkt.T_WhatsAppConfiguracionPreEnvio WHERE Estado=1 and Validado=1 and Procesado=0 and MensajeProceso='No Porcesado' and IdConjuntoListaDetalle=@IdConjuntoListaDetalle";
                var QueryRespuesta = _dapper.QueryDapper(Query, new { IdConjuntoListaDetalle });
                if (!string.IsNullOrEmpty(QueryRespuesta) && !QueryRespuesta.Contains("[]"))
                {
                    ConfiguracionPre = JsonConvert.DeserializeObject<List<WhatsAppConfiguracionPreEnvioDTO>>(QueryRespuesta);
                }
                return ConfiguracionPre;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Autor: Carlos Crispin Riquelme
        /// Descripción: Funcion que permite consumir de la tabla T_WhatsAppConfiguracionPreEnvio los datos pre-procesados del conjunto de lista de las campanias generales
        /// </summary>
        /// <param name="IdCampaniaGeneralDetalle">Id de la campania general detalle</param>
        /// <param name="cantidad">Cantidad de contactos a enviar</param>
        /// <returns>Retorna el conjunto de datso pre-procesados</returns>
        public List<WhatsAppConfiguracionPreEnvioDTO> ListasWhatsAppEnvioAutomaticoMasivoPreProcesadaCampaniaGeneral(int cantidad, int idCampaniaGeneralDetalle, int idPersonal)
        {
            try
            {
                List<WhatsAppConfiguracionPreEnvioDTO> ConfiguracionPre = new List<WhatsAppConfiguracionPreEnvioDTO>();

                string querySP = "mkt.SP_ObtenerListaWhatsAppCampaniaGeneralPreEnvio";

                var respuestaConsulta = _dapper.QuerySPDapper(querySP, new { CantidadAEnviar = cantidad, IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle, IdPersonal = idPersonal });

                if (!string.IsNullOrEmpty(respuestaConsulta) && !respuestaConsulta.Contains("[]") && respuestaConsulta != "null")
                    ConfiguracionPre = JsonConvert.DeserializeObject<List<WhatsAppConfiguracionPreEnvioDTO>>(respuestaConsulta);

                return ConfiguracionPre;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Autor: Jorge Rivera Tito
        /// Descripción: Funcion que lee los datos de una vista para tener los registros WhatsApp Configurados del PreEnvio
        /// </summary>
        /// <param name="IdConjuntoListaDetalle">Identificador del conjunto de lista</param>
        /// <returns>Lista de registros WhatsApp Configurados del PreEnvio</returns>
        public List<VistaWhatsAppConfiguracionPreEnvioDTO> ListasVisualizarWhatsAppEnvioAutomaticoMasivoPreProcesada(int IdConjuntoListaDetalle)
        {
            try
            {
                List<VistaWhatsAppConfiguracionPreEnvioDTO> ConfiguracionPre = new List<VistaWhatsAppConfiguracionPreEnvioDTO>();
                string Query = string.Empty;
                Query = "SELECT Id, IdWhatsappMensajePublicidad, IdConjuntoListaResultado, IdAlumno, Alumno, Celular, IdPais, Pais, NroEjecucion, Validado, Plantilla, IdPersonal, Personal, IdPlantilla, IdWhatsAppEstadoValidacion, WhatsAppEstadoValidacion, objetoplantilla FROM mkt.V_RegistroWhatsAppConfiguracionPreEnvio WHERE IdConjuntoListaDetalle=@IdConjuntoListaDetalle";
                var QueryRespuesta = _dapper.QueryDapper(Query, new { IdConjuntoListaDetalle });
                if (!string.IsNullOrEmpty(QueryRespuesta) && !QueryRespuesta.Contains("[]"))
                {
                    ConfiguracionPre = JsonConvert.DeserializeObject<List<VistaWhatsAppConfiguracionPreEnvioDTO>>(QueryRespuesta);
                }
                return ConfiguracionPre;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Autor: Jorge Rivera Tito
        /// Descripción: Permite obtener los registros consultando de forma directa a la tabla segun el Id del conjunto de lista
        /// </summary>
        /// <param name="IdConjuntoLista"></param>
        /// <returns>Objeto de clase RegistroSeguimientoPreProcesoListaWhatsAppDTO</returns>
        public RegistroSeguimientoPreProcesoListaWhatsAppDTO RegistroSeguimientoPreProcesoListaWhatsApp(int IdConjuntoLista)
        {
            try
            {
                RegistroSeguimientoPreProcesoListaWhatsAppDTO Registro = new RegistroSeguimientoPreProcesoListaWhatsAppDTO();
                string QueryPersona = string.Empty;
                QueryPersona = "SELECT Id, IdEstadoSeguimientoPreProcesoListaWhatsApp, IdConjuntoLista FROM mkt.T_SeguimientoPreProcesoListaWhatsApp WHERE Estado=1 AND IdConjuntoLista=@IdConjuntoLista";
                var queryModalidad = _dapper.FirstOrDefault(QueryPersona, new { IdConjuntoLista });
                if (!string.IsNullOrEmpty(queryModalidad) && !queryModalidad.Contains("[]"))
                {
                    Registro = JsonConvert.DeserializeObject<RegistroSeguimientoPreProcesoListaWhatsAppDTO>(queryModalidad);
                }

                return Registro;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Inserta una nueva configuracion de WhatsAppConfiguracionPreEnvioBO
        /// </summary>
        /// <param name="listaNuevoWhatsAppConfiguracionPreEnvio">Objeto de tipo WhatsAppConfiguracionPreEnvioBO</param>
        /// <returns>Entero con el Id del scope</returns>
        public bool InsertarConfiguracionPreEnvioRepositorioMailingGeneral(List<WhatsAppConfiguracionPreEnvioBO> listaNuevoWhatsAppConfiguracionPreEnvio)
        {
            try
            {
                int contador = 0;
                string spQuery = "[mkt].[SP_InsertarWhatsAppConfiguracionPreEnvioMailingGeneral]";

                foreach (var nuevoWhatsAppConfiguracionPreEnvio in listaNuevoWhatsAppConfiguracionPreEnvio)
                {
                    try
                    {
                        if (contador >= 2500)
                        {
                            contador = 0;
                            Thread.Sleep(1000);
                        }

                        _dapper.QuerySPFirstOrDefault(spQuery, new
                        {
                            nuevoWhatsAppConfiguracionPreEnvio.IdWhatsappMensajePublicidad,
                            nuevoWhatsAppConfiguracionPreEnvio.IdConjuntoListaResultado,
                            nuevoWhatsAppConfiguracionPreEnvio.IdAlumno,
                            nuevoWhatsAppConfiguracionPreEnvio.Celular,
                            nuevoWhatsAppConfiguracionPreEnvio.IdPais,
                            nuevoWhatsAppConfiguracionPreEnvio.NroEjecucion,
                            nuevoWhatsAppConfiguracionPreEnvio.Validado,
                            nuevoWhatsAppConfiguracionPreEnvio.Plantilla,
                            nuevoWhatsAppConfiguracionPreEnvio.IdPersonal,
                            nuevoWhatsAppConfiguracionPreEnvio.IdPGeneral,
                            nuevoWhatsAppConfiguracionPreEnvio.IdPlantilla,
                            nuevoWhatsAppConfiguracionPreEnvio.IdWhatsAppEstadoValidacion,
                            ObjetoPlantilla = nuevoWhatsAppConfiguracionPreEnvio.objetoplantilla,
                            nuevoWhatsAppConfiguracionPreEnvio.IdConjuntoListaDetalle,
                            nuevoWhatsAppConfiguracionPreEnvio.IdPrioridadMailChimpListaCorreo,
                            nuevoWhatsAppConfiguracionPreEnvio.Procesado,
                            nuevoWhatsAppConfiguracionPreEnvio.MensajeProceso,
                            nuevoWhatsAppConfiguracionPreEnvio.UsuarioCreacion,
                            nuevoWhatsAppConfiguracionPreEnvio.UsuarioModificacion
                        });

                        contador++;
                    }
                    catch (Exception)
                    {
                        _dapper.QuerySPFirstOrDefault(spQuery, new
                        {
                            nuevoWhatsAppConfiguracionPreEnvio.IdWhatsappMensajePublicidad,
                            nuevoWhatsAppConfiguracionPreEnvio.IdConjuntoListaResultado,
                            nuevoWhatsAppConfiguracionPreEnvio.IdAlumno,
                            nuevoWhatsAppConfiguracionPreEnvio.Celular,
                            nuevoWhatsAppConfiguracionPreEnvio.IdPais,
                            nuevoWhatsAppConfiguracionPreEnvio.NroEjecucion,
                            nuevoWhatsAppConfiguracionPreEnvio.Validado,
                            nuevoWhatsAppConfiguracionPreEnvio.Plantilla,
                            nuevoWhatsAppConfiguracionPreEnvio.IdPersonal,
                            nuevoWhatsAppConfiguracionPreEnvio.IdPGeneral,
                            nuevoWhatsAppConfiguracionPreEnvio.IdPlantilla,
                            nuevoWhatsAppConfiguracionPreEnvio.IdWhatsAppEstadoValidacion,
                            ObjetoPlantilla = nuevoWhatsAppConfiguracionPreEnvio.objetoplantilla,
                            nuevoWhatsAppConfiguracionPreEnvio.IdConjuntoListaDetalle,
                            nuevoWhatsAppConfiguracionPreEnvio.IdPrioridadMailChimpListaCorreo,
                            nuevoWhatsAppConfiguracionPreEnvio.Procesado,
                            nuevoWhatsAppConfiguracionPreEnvio.MensajeProceso,
                            nuevoWhatsAppConfiguracionPreEnvio.UsuarioCreacion,
                            nuevoWhatsAppConfiguracionPreEnvio.UsuarioModificacion
                        });

                        contador++;
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
