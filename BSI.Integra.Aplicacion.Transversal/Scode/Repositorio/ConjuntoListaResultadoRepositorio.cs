using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Marketing/ConjuntoListaResultado
    /// Autor: Fischer Valdez - Wilber Choque - Joao Benavente - Gian Miranda
    /// Fecha: 05/02/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_ConjuntoListaResultado
    /// </summary>
    public class ConjuntoListaResultadoRepositorio : BaseRepository<TConjuntoListaResultado, ConjuntoListaResultadoBO>
    {
        #region Metodos Base
        public ConjuntoListaResultadoRepositorio() : base()
        {
        }
        public ConjuntoListaResultadoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<ConjuntoListaResultadoBO> GetBy(Expression<Func<TConjuntoListaResultado, bool>> filter)
        {
            IEnumerable<TConjuntoListaResultado> listado = base.GetBy(filter);
            List<ConjuntoListaResultadoBO> listadoBO = new List<ConjuntoListaResultadoBO>();
            foreach (var itemEntidad in listado)
            {
                ConjuntoListaResultadoBO objetoBO = Mapper.Map<TConjuntoListaResultado, ConjuntoListaResultadoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public ConjuntoListaResultadoBO FirstById(int id)
        {
            try
            {
                TConjuntoListaResultado entidad = base.FirstById(id);
                ConjuntoListaResultadoBO objetoBO = new ConjuntoListaResultadoBO();
                Mapper.Map<TConjuntoListaResultado, ConjuntoListaResultadoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public ConjuntoListaResultadoBO FirstBy(Expression<Func<TConjuntoListaResultado, bool>> filter)
        {
            try
            {
                TConjuntoListaResultado entidad = base.FirstBy(filter);
                ConjuntoListaResultadoBO objetoBO = Mapper.Map<TConjuntoListaResultado, ConjuntoListaResultadoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(ConjuntoListaResultadoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TConjuntoListaResultado entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<ConjuntoListaResultadoBO> listadoBO)
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

        public bool Update(ConjuntoListaResultadoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TConjuntoListaResultado entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<ConjuntoListaResultadoBO> listadoBO)
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
        private void AsignacionId(TConjuntoListaResultado entidad, ConjuntoListaResultadoBO objetoBO)
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

        private TConjuntoListaResultado MapeoEntidad(ConjuntoListaResultadoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TConjuntoListaResultado entidad = new TConjuntoListaResultado();
                entidad = Mapper.Map<ConjuntoListaResultadoBO, TConjuntoListaResultado>(objetoBO,
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
        /// Elimina los registros que pertenecen a un conjunto lista detalle
        /// </summary>
        /// <param name="idConjuntoListaDetalle"></param>
        /// <returns></returns>
        public bool Eliminar(int idConjuntoListaDetalle)
        {
            try
            {
                this._dapper.QuerySPDapper("mkt.SP_EliminarConjuntoListaResultadoPorConjuntoListaDetalle", new { idConjuntoListaDetalle });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina los registros que pertenecen a un conjunto lista
        /// </summary>
        /// <param name="idConjuntoLista"></param>
        /// <returns></returns>
        public bool EliminarPorConjuntoLista(int idConjuntoLista, string nombreUsuario)
        {
            try
            {
                this._dapper.QuerySPDapper("mkt.SP_EliminarConjuntoListaResultadoPorConjuntoLista", new { idConjuntoLista, nombreUsuario });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene El resultado por conjuntoListadetalle
        /// </summary>
        /// <param name="idConjuntoLista"></param>
        /// <returns></returns>
        public List<WhatsAppResultadoConjuntoListaDTO> ObtenerConjuntoListaResultado(int IdConjuntoListaDetalle)
        {
            try
            {
                List<WhatsAppResultadoConjuntoListaDTO> resultado = new List<WhatsAppResultadoConjuntoListaDTO>();
                string _queryResultado = "Select  IdConjuntoListaResultado,IdAlumno,Celular,IdCodigoPais,NroEjecucion From mkt.V_WhatsAppConjuntoListadetalleResultado Where IdConjuntoListaDetalle=@IdConjuntoListaDetalle and Activo=1 ";
                var queryResultado = _dapper.QueryDapper(_queryResultado, new { IdConjuntoListaDetalle });
                if (queryResultado != "[]" && queryResultado != "null")
                {
                    resultado = JsonConvert.DeserializeObject<List<WhatsAppResultadoConjuntoListaDTO>>(queryResultado);
                    return resultado;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Autor: Gian Miranda
        /// Descripción: Obtiene El resultado por conjuntoListadetalle para pre validar
        /// </summary>
        /// <param name="idConjuntoListaDetalle">Identificador del conjunto lista detalle</param>
        /// <returns>Retorna la lista de datos de cada usuario del conjunto de lista (PreWhatsAppResultadoConjuntoListaDTO)</returns>
        public List<PreWhatsAppResultadoConjuntoListaDTO> ObtenerListaPreparadaProcesamiento(int idConjuntoListaDetalle)
        {
            try
            {
                List<PreWhatsAppResultadoConjuntoListaDTO> resultadoLista = new List<PreWhatsAppResultadoConjuntoListaDTO>();

                string querySp = "mkt.SP_ObtenerConjuntoListaResultadoWhatsApp";
                var queryResultado = _dapper.QuerySPDapper(querySp, new { IdConjuntoListaDetalle = idConjuntoListaDetalle });

                if (queryResultado != "[]" && queryResultado != "null")
                    resultadoLista = JsonConvert.DeserializeObject<List<PreWhatsAppResultadoConjuntoListaDTO>>(queryResultado);

                return resultadoLista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Autor: Jorge Rivera Tito
        /// Descripción: Obtiene El resultado por conjuntoListadetalle para pre validar
        /// </summary>
        /// <param name="idConjuntoLista">Identificador del conjunto de anuncios</param>
        /// <returns>Retorna ls lista de datos de cada usuario del conjunto de lista (PreWhatsAppResultadoConjuntoListaDTO)</returns>
        public List<PreWhatsAppResultadoConjuntoListaDTO> PreObtenerConjuntoListaResultado(int IdConjuntoListaDetalle)
        {
            try
            {
                List<PreWhatsAppResultadoConjuntoListaDTO> Resultado = new List<PreWhatsAppResultadoConjuntoListaDTO>();
                string Query = "Select  IdConjuntoListaResultado,IdAlumno,Celular,IdCodigoPais,NroEjecucion From mkt.V_WhatsAppConjuntoListadetalleResultado Where IdConjuntoListaDetalle=@IdConjuntoListaDetalle and Activo=1 ";
                var QueryResultado = _dapper.QueryDapper(Query, new { IdConjuntoListaDetalle });
                if (QueryResultado != "[]" && QueryResultado != "null")
                {
                    Resultado = JsonConvert.DeserializeObject<List<PreWhatsAppResultadoConjuntoListaDTO>>(QueryResultado);
                    return Resultado;
                }
                return Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el resultado de un conjunto lista detalle
        /// </summary>
        /// <param name="IdConjuntoListaDetalle"></param>
        /// <returns></returns>
        public List<WhatsAppResultadoConjuntoListaDTO> ObtenerConjuntoListaResultadoWhatsAppMasivoOperaciones(int idConjuntoListaDetalle)
        {
            try
            {
                List<WhatsAppResultadoConjuntoListaDTO> resultado = new List<WhatsAppResultadoConjuntoListaDTO>();
                string _queryResultado = $@"
                                    SELECT 
                                            IdConjuntoListaResultado,
                                            IdAlumno,
                                            Celular,
                                            IdCodigoPais,
                                            IdPersonal,
                                            NroEjecucion
                                    FROM mkt.V_WhatsAppConjuntoListadetalleResultadoMasivoOperaciones
                                    WHERE IdConjuntoListaDetalle = @idConjuntoListaDetalle
                                    AND Activo = 1;
                                    ";
                var queryResultado = _dapper.QueryDapper(_queryResultado, new { idConjuntoListaDetalle });
                if (!string.IsNullOrEmpty(queryResultado) && queryResultado != "[]")
                {
                    resultado = JsonConvert.DeserializeObject<List<WhatsAppResultadoConjuntoListaDTO>>(queryResultado);
                    return resultado;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<WhatsAppResultadoConjuntoListaDTO> ObtenerOportunidadesReasignadasOperaciones()
        {
            try
            {
                List<WhatsAppResultadoConjuntoListaDTO> resultado = new List<WhatsAppResultadoConjuntoListaDTO>();
                string _queryResultado = "Select  IdConjuntoListaResultado,IdAlumno,Celular,IdCodigoPais,IdPersonal,IdPgeneral,IdPlantilla From ope.V_ObtenerOportunidadesReasignadas Order by IdPersonal desc";
                var queryResultado = _dapper.QueryDapper(_queryResultado, null);
                if (queryResultado != "[]" && queryResultado != "null")
                {
                    resultado = JsonConvert.DeserializeObject<List<WhatsAppResultadoConjuntoListaDTO>>(queryResultado);
                    return resultado;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<RegularizarMensajeWhatsAppEnvioDTO> ObtenerEnvioSinMensaje()
        {
            try
            {
                List<RegularizarMensajeWhatsAppEnvioDTO> resultado = new List<RegularizarMensajeWhatsAppEnvioDTO>();
                string _queryResultado = "Select  Id,IdConjuntoListaResultado,IdAlumno,Celular,IdCodigoPais,IdPersonal,IdPgeneral,IdPlantilla From com.V_ObtenerRegistrosSinMensaje Order by IdWhatsAppConfiguracionEnvio desc";
                var queryResultado = _dapper.QueryDapper(_queryResultado, null);
                if (queryResultado != "[]" && queryResultado != "null")
                {
                    resultado = JsonConvert.DeserializeObject<List<RegularizarMensajeWhatsAppEnvioDTO>>(queryResultado);
                    return resultado;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene El resultado por conjuntoListadetalle
        /// </summary>
        /// <param name="idConjuntoLista"></param>
        /// <returns></returns>
        public List<FacebookAudienciaDatosAlumnoDTO> ObtenerConjuntoListaResultadoFacebook(int IdConjuntoListaDetalle)
        {
            try
            {
                List<FacebookAudienciaDatosAlumnoDTO> resultado = new List<FacebookAudienciaDatosAlumnoDTO>();
                string _queryResultado = "Select IdAlumno,Email1 From mkt.V_FacebookConjuntoListadetalleResultado Where IdConjuntoListaDetalle=@IdConjuntoListaDetalle and Activo=1 ";
                var queryResultado = _dapper.QueryDapper(_queryResultado, new { IdConjuntoListaDetalle });
                if (queryResultado != "[]" && queryResultado != "null")
                {
                    resultado = JsonConvert.DeserializeObject<List<FacebookAudienciaDatosAlumnoDTO>>(queryResultado);
                    return resultado;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los conjunto lista resultado
        /// </summary>
        /// <param name="idConjuntoListaDetalle">Id del conjunto lista detalle que se va a ejecutar</param>
        /// <returns>Lista con BO del conjunto Lista Resultado</returns>
        public List<ConjuntoListaResultadoBO> ObtenerPorConjuntoListaDetalle(int idConjuntoListaDetalle)
        {
            try
            {
                return this.GetBy(x => x.IdConjuntoListaDetalle == idConjuntoListaDetalle && x.Activo == true).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene todos los conjunto lista resultado
        /// </summary>
        /// <param name="idConjuntoListaDetalle">Id del conjunto lista detalle que se va a ejecutar</param>
        /// <returns>Lista con BO del conjunto Lista Resultado</returns>
        public List<ConjuntoListaResultadoBO> ObtenerPorConjuntoListaDetalleRedireccion(int idConjuntoListaDetalle)
        {
            try
            {
                List<ConjuntoListaResultadoBO> resultadoFinal = new List<ConjuntoListaResultadoBO>();
                var solicitudesCambiosDB = _dapper.QuerySPDapper("mkt.SP_ObtenerConjuntoListaResultadoPorIdConjuntoListaDetalle", new { IdConjuntoListaDetalle = idConjuntoListaDetalle });
                if (!string.IsNullOrEmpty(solicitudesCambiosDB) && !solicitudesCambiosDB.Contains("[]"))
                {
                    resultadoFinal = JsonConvert.DeserializeObject<List<ConjuntoListaResultadoBO>>(solicitudesCambiosDB);
                }
                return resultadoFinal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene todos los conjunto lista resultado Dapper
        /// </summary>
        /// <param name="idConjuntoListaDetalle"></param>
        /// <returns></returns>
        public List<ConjuntoListaResultadoBO> ObtenerPorConjuntoListaDetalleDapper(int idConjuntoListaDetalle)
        {
            try
            {
                List<ConjuntoListaResultadoBO> resultado = new List<ConjuntoListaResultadoBO>();
                string _queryResultado = $@"
                                           SELECT Id, IdAlumno,
		                                            IdConjuntoListaDetalle,
		                                            EsVentaCruzada,
		                                            NroEjecucion,
		                                            Activo,
		                                            IdMigracion,
		                                            IdOportunidad
                                            FROM mkt.T_ConjuntoListaResultado
                                            WHERE IdConjuntoListaDetalle = @idConjuntoListaDetalle
                                                AND Activo = 1
                                        ";
                var queryResultado = _dapper.QueryDapper(_queryResultado, new { idConjuntoListaDetalle });
                if (!string.IsNullOrEmpty(queryResultado) && queryResultado != "[]")
                {
                    resultado = JsonConvert.DeserializeObject<List<ConjuntoListaResultadoBO>>(queryResultado);
                    return resultado;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
		/// Obtiene Hora Minima y las fases de oportunidad en una cadena
		/// </summary>
		/// <param name="filtros"></param>
		/// <returns></returns>
		public FiltroFasesOportunidadAlumnoDTO ObtenerHoraMinimaFasesCadena(FiltroHoraMinimaFasesCadenaDTO filtros)
        {
            try
            {
                FiltroFasesOportunidadAlumnoDTO FiltroFasesOportunidadAlumno = new FiltroFasesOportunidadAlumnoDTO();

                var FiltroFasesOportunidadAlumnoDB = _dapper.QuerySPFirstOrDefault("[mkt].[SP_HoraYFasesFiltro]", filtros);

                if (!FiltroFasesOportunidadAlumnoDB.Contains("[]") && !string.IsNullOrEmpty(FiltroFasesOportunidadAlumnoDB))
                {
                    FiltroFasesOportunidadAlumno = JsonConvert.DeserializeObject<FiltroFasesOportunidadAlumnoDTO>(FiltroFasesOportunidadAlumnoDB);
                }
                return FiltroFasesOportunidadAlumno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
		/// Obtiene los alumnos y oportunidad respectivas para el envio automatico
		/// </summary>
		/// <param name="filtros"></param>
		/// <returns></returns>
		public List<AlumnoOportunidadFiltroDTO> ObtenerAlumnoOportunidadEnvioAutomatico(AlumnoOportunidadEnvioAutomaticoDTO filtros)
        {
            try
            {
                List<AlumnoOportunidadFiltroDTO> ConjuntoListaOportunidadAlumno = new List<AlumnoOportunidadFiltroDTO>();

                var ConjuntoListaOportunidadAlumnoDB = _dapper.QuerySPDapper("[mkt].[SP_ObtenerAlumnoOportunidadEnvioAutomatico]", filtros);

                if (!ConjuntoListaOportunidadAlumnoDB.Contains("[]") && !string.IsNullOrEmpty(ConjuntoListaOportunidadAlumnoDB))
                {
                    ConjuntoListaOportunidadAlumno = JsonConvert.DeserializeObject<List<AlumnoOportunidadFiltroDTO>>(ConjuntoListaOportunidadAlumnoDB);
                }

                return ConjuntoListaOportunidadAlumno;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// SE obtiene los Psid(Usuarios de Facebook) de un ConjuntoListaResultado y página
        /// </summary>
        /// <param name="IdConjuntoListaDetalle"></param>
        /// <param name="idFacebookPagina"></param>
        /// <returns></returns>
        public string[][] ObtenerMessengerUsuarioPorConjuntoListaResultado(int IdConjuntoListaDetalle, int idFacebookPagina)
        {
            try
            {
                List<MessengerUsuarioPsidDTO> resultado = new List<MessengerUsuarioPsidDTO>();
                string _queryResultado = "Select PSID From mkt.V_ObtenerMessengerUsuario_ConjuntoListaResultado Where IdConjuntoListaDetalle=@IdConjuntoListaDetalle and IdFacebookPagina=@idFacebookPagina GROUP BY PSID";
                var queryResultado = _dapper.QueryDapper(_queryResultado, new { IdConjuntoListaDetalle, idFacebookPagina });
                if (queryResultado != "[]" && queryResultado != "null")
                {
                    resultado = JsonConvert.DeserializeObject<List<MessengerUsuarioPsidDTO>>(queryResultado);

                    string[][] array = new string[resultado.Count][];
                    for (int i = 0; i < resultado.Count; i++)
                    {
                        array[i] = new string[1] { resultado[i].PSID };
                    }
                    return array;
                }
                else
                {
                    string[][] array = { };
                    return array;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Verifica existencia de mkt.T_ConjuntoListaResultado
        /// </summary>
        /// <param name="idConjuntoListaResultado">Id de ConjuntoListaResultado (PK de la tabla mkt.T_ConjuntoListaResultado)</param>
        /// <returns>Booleano para determinar si existe o no un ConjuntoListaResultado</returns>
        public bool ExisteConjuntoListaResultado(int idConjuntoListaResultado)
        {
            try
            {
                string spQuery = "[mkt].[SP_ExisteConjuntoListaResultado]";

                var query = _dapper.QuerySPFirstOrDefault(spQuery, new
                {
                    Id = idConjuntoListaResultado
                });

                return !string.IsNullOrEmpty(query) && !query.Contains("[]");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Busca un registro en mkt.T_ConjuntoListaResultado
        /// </summary>
        /// <param name="idConjuntoListaResultado">Id de ConjuntoListaResultado (PK de la tabla mkt.T_ConjuntoListaResultado)</param>
        /// <returns>Busca registro para determinar si existe o no un ConjuntoListaResultado</returns>
        public ConjuntoListaResultadoBO BuscaConjuntoListaResultado(int idConjuntoListaResultado)
        {
            try
            {
                var conjuntoListaResultado = new ConjuntoListaResultadoBO();

                string spQuery = "[mkt].[SP_BuscaConjuntoListaResultado]";

                var query = _dapper.QuerySPFirstOrDefault(spQuery, new
                {
                    Id = idConjuntoListaResultado
                });

                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    conjuntoListaResultado = JsonConvert.DeserializeObject<ConjuntoListaResultadoBO>(query);
                }

                return conjuntoListaResultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
