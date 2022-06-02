using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Marketing/WhatsAppConfiguracionEnvio
    /// Autor: Fischer Valdez - Gian Miranda
    /// Fecha: 05/02/2021
    /// <summary>
    /// Repositorio para consultas de la configuracion para el envio de WhatsApp
    /// </summary>
    public class WhatsAppConfiguracionEnvioRepositorio : BaseRepository<TWhatsAppConfiguracionEnvio, WhatsAppConfiguracionEnvioBO>
    {
        #region Metodos Base
        public WhatsAppConfiguracionEnvioRepositorio() : base()
        {
        }
        public WhatsAppConfiguracionEnvioRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<WhatsAppConfiguracionEnvioBO> GetBy(Expression<Func<TWhatsAppConfiguracionEnvio, bool>> filter)
        {
            IEnumerable<TWhatsAppConfiguracionEnvio> listado = base.GetBy(filter);
            List<WhatsAppConfiguracionEnvioBO> listadoBO = new List<WhatsAppConfiguracionEnvioBO>();
            foreach (var itemEntidad in listado)
            {
                WhatsAppConfiguracionEnvioBO objetoBO = Mapper.Map<TWhatsAppConfiguracionEnvio, WhatsAppConfiguracionEnvioBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public WhatsAppConfiguracionEnvioBO FirstById(int id)
        {
            try
            {
                TWhatsAppConfiguracionEnvio entidad = base.FirstById(id);
                WhatsAppConfiguracionEnvioBO objetoBO = new WhatsAppConfiguracionEnvioBO();
                Mapper.Map<TWhatsAppConfiguracionEnvio, WhatsAppConfiguracionEnvioBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public WhatsAppConfiguracionEnvioBO FirstBy(Expression<Func<TWhatsAppConfiguracionEnvio, bool>> filter)
        {
            try
            {
                TWhatsAppConfiguracionEnvio entidad = base.FirstBy(filter);
                WhatsAppConfiguracionEnvioBO objetoBO = Mapper.Map<TWhatsAppConfiguracionEnvio, WhatsAppConfiguracionEnvioBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(WhatsAppConfiguracionEnvioBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TWhatsAppConfiguracionEnvio entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<WhatsAppConfiguracionEnvioBO> listadoBO)
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

        public bool Update(WhatsAppConfiguracionEnvioBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TWhatsAppConfiguracionEnvio entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<WhatsAppConfiguracionEnvioBO> listadoBO)
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
        private void AsignacionId(TWhatsAppConfiguracionEnvio entidad, WhatsAppConfiguracionEnvioBO objetoBO)
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

        private TWhatsAppConfiguracionEnvio MapeoEntidad(WhatsAppConfiguracionEnvioBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TWhatsAppConfiguracionEnvio entidad = new TWhatsAppConfiguracionEnvio();
                entidad = Mapper.Map<WhatsAppConfiguracionEnvioBO, TWhatsAppConfiguracionEnvio>(objetoBO,
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

        /// Autor: Fischer Valdez - Gian Miranda
        /// Fecha: 05/02/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene Las configuracionActivas por IdConjuntoLista
        /// </summary>
        /// <param name="idConjuntoLista">Id del ConjuntoLista a ejecutar (PK de la tabla mkt.T_ConjuntoLista)</param>
        /// <returns>Lista de objetos(ConjuntoListaDetalleWhatsAppDTO)</returns>
        public List<ConjuntoListaDetalleWhatsAppDTO> ObtenerConfiguracionPorIdConjuntoLista(int idConjuntoLista)
        {
            try
            {
                List<ConjuntoListaDetalleWhatsAppDTO> configuracion = new List<ConjuntoListaDetalleWhatsAppDTO>();
                string consultaConfiguracion = @"SELECT Id, IdConjuntoListaDetalle, Nombre, Descripcion, IdPlantilla, IdPersonal, IdConjuntoLista
                                                FROM mkt.V_WhatsAppConfiguracionLista
                                                WHERE IdConjuntoLista = @IdConjuntoLista and EstadoConjuntoListaDetalle = 1 AND Activo = 1";
                var resultadoConfiguracion = _dapper.QueryDapper(consultaConfiguracion, new { idConjuntoLista });

                if (resultadoConfiguracion != "[]" && resultadoConfiguracion != "null")
                {
                    configuracion = JsonConvert.DeserializeObject<List<ConjuntoListaDetalleWhatsAppDTO>>(resultadoConfiguracion);

                    return configuracion;
                }
                return configuracion;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Elimina Registros Procesados sin enviar por conjuntoLista
        /// </summary>
        /// <returns></returns>
        public int EliminarEnviosProcesados(int idConjuntoLista)
        {
            try
            {
                ValorIntDTO respuesta = new ValorIntDTO();
                string _query = "mkt.SP_WhatsAppMensajePublicidad_Eliminar";
                string query = _dapper.QuerySPFirstOrDefault(_query, new { IdConjuntoLista = idConjuntoLista });
                if (!string.IsNullOrEmpty(query) && !query.Contains("null"))
                {
                    respuesta = JsonConvert.DeserializeObject<ValorIntDTO>(query);
                    return respuesta.Valor;
                }
                return respuesta.Valor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina SeguimientoPreProcesoListaWhatsAppBO mediante un SP para llamarlo desde replica
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id del detalle de la campania general (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        public void EliminarWhatsAppConfiguracionMailingGeneral(int idCampaniaGeneralDetalle)
        {
            try
            {
                string spQuery = "[mkt].[SP_EliminarWhatsAppConfiguracionMailingGeneral]";
                var query = _dapper.QuerySPDapper(spQuery, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Inserta SeguimientoPreProcesoListaWhatsAppBO mediante un SP para llamarlo desde replica
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id del detalle de la campania general (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Objeto de tipo WhatsAppConfiguracionEnvioBO</returns>
        public WhatsAppConfiguracionEnvioBO InsertarWhatsAppConfiguracionGeneralMailing(int idCampaniaGeneralDetalle)
        {
            try
            {
                WhatsAppConfiguracionEnvioBO objResultado = new WhatsAppConfiguracionEnvioBO();

                string spQuery = "mkt.SP_InsertarWhatsAppConfiguracionGeneralMailing";
                var query = _dapper.QuerySPFirstOrDefault(spQuery, new
                {
                    IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle
                });
                if (!string.IsNullOrEmpty(query))
                {
                    objResultado = JsonConvert.DeserializeObject<WhatsAppConfiguracionEnvioBO>(query);
                }

                return objResultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Inserta el registro de la caida del servidor
        /// </summary>
        /// <param name="usuarioResponsable">Usuario responsable del cambio del flag</param>
        /// <param name="estadoHabilitado">Flag para determinar si está habilitado o deshabilitado la recuperacion del modulo</param>
        /// <returns>Boolean</returns>
        public bool ActualizarEstadoWhatsAppRecuperacion(string tipo, string usuarioResponsable, bool estadoHabilitado)
        {
            try
            {
                string spQuery = "mkt.SP_ActualizarEstadoWhatsAppRecuperacion";

                var query = _dapper.QuerySPFirstOrDefault(spQuery, new
                {
                    IdModuloSistema = ValorEstatico.IdModuloSistemaWhatsAppMailing,
                    Tipo = tipo,
                    UsuarioResponsable = usuarioResponsable,
                    EstadoHabilitado = estadoHabilitado
                });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Inserta el registro de la caida del servidor
        /// </summary>
        /// <param name="servidor">Servidor detectado</param>
        /// <returns>Boolean</returns>
        public bool InsertarRegistroCaidaServidor(string servidor)
        {
            try
            {
                string spQuery = "conf.SP_InsertarRegistroCaidaServidor";
                var query = _dapper.QuerySPFirstOrDefault(spQuery, new
                {
                    Servidor = servidor
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
