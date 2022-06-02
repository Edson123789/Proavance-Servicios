using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    public class ConjuntoListaDetalleRepositorio : BaseRepository<TConjuntoListaDetalle, ConjuntoListaDetalleBO>
    {
        #region Metodos Base
        public ConjuntoListaDetalleRepositorio() : base()
        {
        }
        public ConjuntoListaDetalleRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConjuntoListaDetalleBO> GetBy(Expression<Func<TConjuntoListaDetalle, bool>> filter)
        {
            IEnumerable<TConjuntoListaDetalle> listado = base.GetBy(filter);
            List<ConjuntoListaDetalleBO> listadoBO = new List<ConjuntoListaDetalleBO>();
            foreach (var itemEntidad in listado)
            {
                ConjuntoListaDetalleBO objetoBO = Mapper.Map<TConjuntoListaDetalle, ConjuntoListaDetalleBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConjuntoListaDetalleBO FirstById(int id)
        {
            try
            {
                TConjuntoListaDetalle entidad = base.FirstById(id);
                ConjuntoListaDetalleBO objetoBO = new ConjuntoListaDetalleBO();
                Mapper.Map<TConjuntoListaDetalle, ConjuntoListaDetalleBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConjuntoListaDetalleBO FirstBy(Expression<Func<TConjuntoListaDetalle, bool>> filter)
        {
            try
            {
                TConjuntoListaDetalle entidad = base.FirstBy(filter);
                ConjuntoListaDetalleBO objetoBO = Mapper.Map<TConjuntoListaDetalle, ConjuntoListaDetalleBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConjuntoListaDetalleBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConjuntoListaDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConjuntoListaDetalleBO> listadoBO)
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

        public bool Update(ConjuntoListaDetalleBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConjuntoListaDetalle entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConjuntoListaDetalleBO> listadoBO)
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
        private void AsignacionId(TConjuntoListaDetalle entidad, ConjuntoListaDetalleBO objetoBO)
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

        private TConjuntoListaDetalle MapeoEntidad(ConjuntoListaDetalleBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConjuntoListaDetalle entidad = new TConjuntoListaDetalle();
                entidad = Mapper.Map<ConjuntoListaDetalleBO, TConjuntoListaDetalle>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Obtiene los detalles del conjunto lista
        /// </summary>
        /// <param name="idConjuntoLista"></param>
        /// <returns></returns>
        public List<ConjuntoListaDetalleDTO> Obtener(int idConjuntoLista)
        {
            try
            {
                List<ConjuntoListaDetalleDTO> conjuntoListaDetalle = new List<ConjuntoListaDetalleDTO>();

                var _query = @"
                            SELECT IdConjuntoLista, 
                                    Id, 
                                    Nombre, 
                                    Descripcion, 
                                    Prioridad, 
                                    UsuarioCreacion, 
                                    UsuarioModificacion, 
                                    FechaCreacion, 
                                    FechaModificacion
                            FROM mkt.V_ObtenerConjuntoListaDetalle
                            WHERE EstadoConjuntoLista = 1
                                    AND EstadoConjuntoListaDetalle = 1
                                    AND IdConjuntoLista = @idConjuntoLista;
                            ";
                var query = _dapper.QueryDapper(_query, new { idConjuntoLista });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    conjuntoListaDetalle = JsonConvert.DeserializeObject<List<ConjuntoListaDetalleDTO>>(query);
                }
                return conjuntoListaDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        /// <summary>
        /// Obtiene los detalles del conjunto lista para  Actividades
        /// </summary>
        /// <param name="idConjuntoLista"></param>
        /// <returns></returns>
        public List<ConjuntoListaDetalleMailingDTO> ObtenerListasMailing(int idConjuntoLista)
        {
            try
            {
                List<ConjuntoListaDetalleMailingDTO> conjuntoListaDetalle = new List<ConjuntoListaDetalleMailingDTO>();

                var _query = @"
                            SELECT   
                                    M.Id, 
                                    ISNULL(M.Campania,L.Nombre) AS Campania, 
                                    M.CodMailing, 
                                    L.Id AS IdConjuntoListaDetalle,
                                    M.IdPlantilla, 
                                    M.IdRemitenteMailing, 
                                    M.IdPersonal, 
                                    M.Tipo, 
                                    M.Subject
                            FROM mkt.V_ObtenerConjuntoListaDetalle L
                            LEFT JOIN mkt.T_CampaniaMailingDetalle M on M.IdConjuntoListaDetalle=L.Id
                            WHERE EstadoConjuntoLista = 1
                                    AND EstadoConjuntoListaDetalle = 1
                                    AND IdConjuntoLista = @idConjuntoLista
                                    AND (M.Estado=1 OR M.Estado is null);
                            ";
                var query = _dapper.QueryDapper(_query, new { idConjuntoLista });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    conjuntoListaDetalle = JsonConvert.DeserializeObject<List<ConjuntoListaDetalleMailingDTO>>(query);
                }
                return conjuntoListaDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los conjunto lista detalle asociados a un conjunto lista
        /// </summary>
        /// <param name="idConjuntoLista"></param>
        /// <returns></returns>
        public List<ConjuntoListaDetalleMailingMasivoDTO> ObtenerListasMailingMasivo(int idConjuntoLista)
        {
            try
            {
                List<ConjuntoListaDetalleMailingMasivoDTO> conjuntoListaDetalleMasivo = new List<ConjuntoListaDetalleMailingMasivoDTO>();
                var _query = @"
                            SELECT ConfiguracionEnvioMailig.Id AS Id, 
                                   ISNULL(ConfiguracionEnvioMailig.Nombre, ConjuntoListaDetalle.Nombre) AS Nombre, 
                                   ConfiguracionEnvioMailig.Descripcion AS Descripcion, 
                                   ConjuntoListaDetalle.Id AS IdConjuntoListaDetalle, 
                                   ConfiguracionEnvioMailig.IdPlantilla AS IdPlantilla
                            FROM mkt.T_ConjuntoLista AS ConjuntoLista
                                 INNER JOIN mkt.T_ConjuntoListaDetalle AS ConjuntoListaDetalle ON ConjuntoLista.Id = ConjuntoListaDetalle.IdConjuntoLista
                                 LEFT JOIN mkt.T_ConfiguracionEnvioMailing AS ConfiguracionEnvioMailig ON ConfiguracionEnvioMailig.IdConjuntoListaDetalle = ConjuntoListaDetalle.Id
                            WHERE ConjuntoLista.Estado = 1
                                  AND ConjuntoListaDetalle.Estado = 1
                                  AND ConjuntoLista.Id = @idConjuntoLista
                                  AND (ConfiguracionEnvioMailig.Activo = 1
                                       OR ConfiguracionEnvioMailig.Activo IS NULL);
                            ";

                var query = _dapper.QueryDapper(_query, new { idConjuntoLista });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    conjuntoListaDetalleMasivo = JsonConvert.DeserializeObject<List<ConjuntoListaDetalleMailingMasivoDTO>>(query);
                }
                return conjuntoListaDetalleMasivo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los conjunto lista detalle asociados a un conjunto lista
        /// </summary>
        /// <param name="idConjuntoLista"></param>
        /// <returns></returns>
        public List<ConjuntoListaDetalleMailingDinamicoDTO> ObtenerListasMailingDinamico(int idConjuntoLista)
        {
            try
            {
                List<ConjuntoListaDetalleMailingDinamicoDTO> conjuntoListaDetalleMasivo = new List<ConjuntoListaDetalleMailingDinamicoDTO>();
                var _query = @"
                            SELECT ConfiguracionEnvioMailig.Id AS Id, 
                                   ISNULL(ConfiguracionEnvioMailig.Nombre, ConjuntoListaDetalle.Nombre) AS Nombre, 
                                   ConfiguracionEnvioMailig.Descripcion AS Descripcion, 
                                   ConjuntoListaDetalle.Id AS IdConjuntoListaDetalle, 
                                   ConfiguracionEnvioMailig.IdPlantilla AS IdPlantilla
                            FROM mkt.T_ConjuntoLista AS ConjuntoLista
                                 INNER JOIN mkt.T_ConjuntoListaDetalle AS ConjuntoListaDetalle ON ConjuntoLista.Id = ConjuntoListaDetalle.IdConjuntoLista
                                 LEFT JOIN mkt.T_ConfiguracionEnvioMailing AS ConfiguracionEnvioMailig ON ConfiguracionEnvioMailig.IdConjuntoListaDetalle = ConjuntoListaDetalle.Id
                            WHERE ConjuntoLista.Estado = 1
                                  AND ConjuntoListaDetalle.Estado = 1
                                  AND ConjuntoLista.Id = @idConjuntoLista
                                  AND (ConfiguracionEnvioMailig.Activo = 1
                                       OR ConfiguracionEnvioMailig.Activo IS NULL);
                            ";

                var query = _dapper.QueryDapper(_query, new { idConjuntoLista });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    conjuntoListaDetalleMasivo = JsonConvert.DeserializeObject<List<ConjuntoListaDetalleMailingDinamicoDTO>>(query);
                }
                return conjuntoListaDetalleMasivo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los detalles del conjunto lista para  Actividades
        /// </summary>
        /// <param name="idConjuntoLista"></param>
        /// <returns></returns>
        public List<ConjuntoListaDetalleAudienciaDTO> ObtenerListasAudiencia(int idConjuntoLista)
        {
            try
            {
                List<ConjuntoListaDetalleAudienciaDTO> conjuntoListaDetalle = new List<ConjuntoListaDetalleAudienciaDTO>();

                var _query = @"
                            SELECT  
                                    A.Id,
                                    L.Id as IdConjuntoListaDetalle,
                                    ISNULL(A.Nombre,L.Nombre) AS Nombre, 
                                    A.Descripcion,
                                    A.IdFiltroSegmento,
                                    A.FacebookIdAudiencia
                            FROM mkt.V_ObtenerConjuntoListaDetalle L
                            LEFT JOIN mkt.T_FacebookAudienciaCuentaPublicitaria FC on FC.IdConjuntoListaDetalle =L.Id and FC.Origen='Propio'
                            LEFT JOIN mkt.T_FacebookAudiencia A on A.Id=FC.IdFacebookAudiencia
                            WHERE EstadoConjuntoLista = 1
                                    AND EstadoConjuntoListaDetalle = 1
                                    AND IdConjuntoLista = @idConjuntoLista;
                            ";
                var query = _dapper.QueryDapper(_query, new { idConjuntoLista });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    conjuntoListaDetalle = JsonConvert.DeserializeObject<List<ConjuntoListaDetalleAudienciaDTO>>(query);
                }
                return conjuntoListaDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los detalles del conjunto lista para  Actividades
        /// </summary>
        /// <param name="idConjuntoLista">Id del conjunto lista (PK de la tabla mkt.T_ConjuntoLista)</param>
        /// <returns>Lista de objetos de clase ConjuntoListaDetalleWhatsAppDTO</returns>
        public List<ConjuntoListaDetalleWhatsAppDTO> ObtenerListasWhatsApp(int idConjuntoLista)
        {
            try
            {
                List<ConjuntoListaDetalleWhatsAppDTO> conjuntoListaDetalle = new List<ConjuntoListaDetalleWhatsAppDTO>();

                var _query =     @"
                            SELECT  W.Id ,
                                    ISNULL(W.Nombre,L.Nombre) AS Nombre, 
                                    W.Descripcion, 
                                    L.Id AS IdConjuntoListaDetalle, 
                                    W.IdPlantilla ,
                                    W.IdPersonal 
                            FROM mkt.V_ObtenerConjuntoListaDetalle L
                            LEFT JOIN mkt.T_WhatsAppConfiguracionEnvio W on W.IdConjuntoListaDetalle=L.Id
                            WHERE EstadoConjuntoLista = 1
                                    AND EstadoConjuntoListaDetalle = 1
                                    AND IdConjuntoLista = @idConjuntoLista
                                    AND (w.Activo=1 or Activo is null);
                            ";
                var query = _dapper.QueryDapper(_query, new { idConjuntoLista });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    conjuntoListaDetalle = JsonConvert.DeserializeObject<List<ConjuntoListaDetalleWhatsAppDTO>>(query);
                }
                return conjuntoListaDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los detalles del conjunto lista para Actividades
        /// </summary>
        /// <param name="idConjuntoListaDetalle">Id del conjunto lista (PK de la tabla mkt.T_ConjuntoLista)</param>
        /// <returns>Lista de objetos de clase ConjuntoListaDetalleSmsDTO</returns>
        public List<ConjuntoListaDetalleSmsDTO> ObtenerListasSms(int idConjuntoLista)
        {
            try
            {
                List<ConjuntoListaDetalleSmsDTO> conjuntoListaDetalle = new List<ConjuntoListaDetalleSmsDTO>();

                var spQuery = "mkt.SP_ObtenerConfiguracionSmsConjuntoListaSeleccionado";
                var resultadoCrudo = _dapper.QuerySPDapper(spQuery, new { IdConjuntoLista = idConjuntoLista });

                if (!string.IsNullOrEmpty(spQuery) && !spQuery.Contains("[]") && spQuery != "null")
                    conjuntoListaDetalle = JsonConvert.DeserializeObject<List<ConjuntoListaDetalleSmsDTO>>(resultadoCrudo);

                return conjuntoListaDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los detalles del conjunto lista para  Actividades
        /// </summary>
        /// <param name="idConjuntoLista"></param>
        /// <returns></returns>
        public List<IdConjuntoListaDetalleDTO> ObtenerConjuntoListaDetalleId(int IdConjuntoLista)
        {
            try
            {
                List<IdConjuntoListaDetalleDTO> conjuntoListaDetalle = new List<IdConjuntoListaDetalleDTO>();

                var _query = @"
                            SELECT  IdConjuntoListaDetalle                                  
                            FROM mkt.V_ObtenerIdConjuntoListaDetalle
                            WHERE Estado = 1 AND IdConjuntoLista = @IdConjuntoLista";
                var query = _dapper.QueryDapper(_query, new { IdConjuntoLista });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    conjuntoListaDetalle = JsonConvert.DeserializeObject<List<IdConjuntoListaDetalleDTO>>(query);
                }
                return conjuntoListaDetalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<MessengerEnvioMasivoDTO> ObtenerConjuntoListaDetalleParaMessengerEnvioMasivo(int idConjuntoLista)
        {
            try
            {
                List<MessengerEnvioMasivoDTO> items = new List<MessengerEnvioMasivoDTO>();

                var query = _dapper.QuerySPDapper("mkt.SP_ObtenerConjuntoListaDetalleEnvioMasivoMessenger", new
                {
                    idConjuntoLista
                });


                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<MessengerEnvioMasivoDTO>>(query);
                }
                return items;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }

        }
    }
}
