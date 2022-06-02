using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Transversal;
using BSI.Integra.Aplicacion.DTOs.DTOs.Comercial;
using System.Text.RegularExpressions;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Comercial/Oportunidad
    /// Autor: Jose Villena - Carlos Crispin - Luis Huallpa - Jorge Rivera - Gian Miranda - Edgar S.
    /// Fecha: 09/02/2021
    /// <summary>
    /// Repositorio para consultas de com.T_Oportunidad
    /// </summary>
    public class OportunidadRepositorio : BaseRepository<TOportunidad, OportunidadBO>
    {
        #region Metodos Base
        public OportunidadRepositorio() : base()
        {
        }
        public OportunidadRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<OportunidadBO> GetBy(Expression<Func<TOportunidad, bool>> filter)
        {
            IEnumerable<TOportunidad> listado = base.GetBy(filter).ToList();
            List<OportunidadBO> listadoBO = new List<OportunidadBO>();
            foreach (var itemEntidad in listado)
            {
                OportunidadBO objetoBO = Mapper.Map<TOportunidad, OportunidadBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public OportunidadBO FirstById(int id)
        {
            try
            {
                TOportunidad entidad = base.FirstById(id);
                OportunidadBO objetoBO = Mapper.Map<TOportunidad, OportunidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public OportunidadBO FirstBy(Expression<Func<TOportunidad, bool>> filter)
        {
            try
            {
                TOportunidad entidad = base.FirstBy(filter);
                OportunidadBO objetoBO = Mapper.Map<TOportunidad, OportunidadBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(OportunidadBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TOportunidad entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<OportunidadBO> listadoBO)
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

        public bool Update(OportunidadBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TOportunidad entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<OportunidadBO> listadoBO)
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
        private void AsignacionId(TOportunidad entidad, OportunidadBO objetoBO)
        {
            try
            {
                if (entidad != null && objetoBO != null)
                {
                    objetoBO.Id = entidad.Id;
                    objetoBO.RowVersion = entidad.RowVersion;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private TOportunidad MapeoEntidad(OportunidadBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TOportunidad entidad = new TOportunidad();
                entidad = Mapper.Map<OportunidadBO, TOportunidad>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ActividadAntigua != null)
                {
                    TActividadDetalle entidadHijo = new TActividadDetalle();
                    entidadHijo = Mapper.Map<ActividadDetalleBO, TActividadDetalle>(objetoBO.ActividadAntigua,
                        opt => opt.ConfigureMap(MemberList.None));
                    entidad.TActividadDetalle.Add(entidadHijo);

                    //mapea al hijo interno
                    if (objetoBO.ActividadAntigua.LlamadaActividad != null)
                    {
                        TLlamadaActividad entidadHijo2 = new TLlamadaActividad();
                        entidadHijo2 = Mapper.Map<LlamadaActividadBO, TLlamadaActividad>(objetoBO.ActividadAntigua.LlamadaActividad,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidadHijo.TLlamadaActividad.Add(entidadHijo2);
                    }
                }
                if (objetoBO.ActividadNueva != null)
                {
                    TActividadDetalle entidadHijo = new TActividadDetalle();
                    entidadHijo = Mapper.Map<ActividadDetalleBO, TActividadDetalle>(objetoBO.ActividadNueva,
                        opt => opt.ConfigureMap(MemberList.None));
                    entidad.TActividadDetalle.Add(entidadHijo);

                    //mapea al hijo interno
                    if (objetoBO.ActividadNueva.LlamadaActividad != null)
                    {
                        TLlamadaActividad entidadHijo2 = new TLlamadaActividad();
                        entidadHijo2 = Mapper.Map<LlamadaActividadBO, TLlamadaActividad>(objetoBO.ActividadNueva.LlamadaActividad,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidadHijo.TLlamadaActividad.Add(entidadHijo2);
                    }
                }
                if (objetoBO.ActividadNuevaProgramarActividad != null)
                {
                    TActividadDetalle entidadHijo = new TActividadDetalle();
                    entidadHijo = Mapper.Map<ActividadDetalleBO, TActividadDetalle>(objetoBO.ActividadNuevaProgramarActividad,
                        opt => opt.ConfigureMap(MemberList.None));
                    entidad.TActividadDetalle.Add(entidadHijo);

                    //mapea al hijo interno
                    if (objetoBO.ActividadNuevaProgramarActividad.LlamadaActividad != null)
                    {
                        TLlamadaActividad entidadHijo2 = new TLlamadaActividad();
                        entidadHijo2 = Mapper.Map<LlamadaActividadBO, TLlamadaActividad>(objetoBO.ActividadNuevaProgramarActividad.LlamadaActividad,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidadHijo.TLlamadaActividad.Add(entidadHijo2);
                    }
                }
                if (objetoBO.Actividades != null && objetoBO.Actividades.Count > 0)
                {
                    foreach (var hijo in objetoBO.Actividades)
                    {
                        TActividadDetalle entidadHijo = new TActividadDetalle();
                        entidadHijo = Mapper.Map<ActividadDetalleBO, TActividadDetalle>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TActividadDetalle.Add(entidadHijo);

                        //mapea al hijo interno
                        if (hijo.LlamadaActividad != null)
                        {
                            TLlamadaActividad entidadHijo2 = new TLlamadaActividad();
                            entidadHijo2 = Mapper.Map<LlamadaActividadBO, TLlamadaActividad>(hijo.LlamadaActividad,
                                opt => opt.ConfigureMap(MemberList.None));
                            entidadHijo.TLlamadaActividad.Add(entidadHijo2);
                        }
                    }
                }
                if (objetoBO.OportunidadLogAntigua != null)
                {
                    TOportunidadLog entidadHijo = new TOportunidadLog();
                    entidadHijo = Mapper.Map<OportunidadLogBO, TOportunidadLog>(objetoBO.OportunidadLogAntigua,
                        opt => opt.ConfigureMap(MemberList.None));
                    entidad.TOportunidadLog.Add(entidadHijo);
                }
                if (objetoBO.OportunidadLogNueva != null)
                {
                    TOportunidadLog entidadHijo = new TOportunidadLog();
                    entidadHijo = Mapper.Map<OportunidadLogBO, TOportunidadLog>(objetoBO.OportunidadLogNueva,
                        opt => opt.ConfigureMap(MemberList.None));
                    entidad.TOportunidadLog.Add(entidadHijo);
                }
                if (objetoBO.ComprobantePago != null)
                {
                    TComprobantePagoOportunidad entidadHijo = new TComprobantePagoOportunidad();
                    entidadHijo = Mapper.Map<ComprobantePagoOportunidadBO, TComprobantePagoOportunidad>(objetoBO.ComprobantePago,
                        opt => opt.ConfigureMap(MemberList.None));
                    entidad.TComprobantePagoOportunidad.Add(entidadHijo);
                }
                if (objetoBO.OportunidadCompetidor != null)
                {
                    TOportunidadCompetidor entidadHijo = new TOportunidadCompetidor();
                    entidadHijo = Mapper.Map<OportunidadCompetidorBO, TOportunidadCompetidor>(objetoBO.OportunidadCompetidor,
                        opt => opt.ConfigureMap(MemberList.None));
                    entidad.TOportunidadCompetidor.Add(entidadHijo);

                    //mapea a los hijos del nivel
                    if (objetoBO.OportunidadCompetidor.ListaPrerequisitoGeneral != null && objetoBO.OportunidadCompetidor.ListaPrerequisitoGeneral.Count > 0)
                    {
                        foreach (var hijo in objetoBO.OportunidadCompetidor.ListaPrerequisitoGeneral)
                        {
                            TOportunidadPrerequisitoGeneral entidadHijo2 = new TOportunidadPrerequisitoGeneral();
                            entidadHijo2 = Mapper.Map<OportunidadPrerequisitoGeneralBO, TOportunidadPrerequisitoGeneral>(
                                hijo,
                                opt => opt.ConfigureMap(MemberList.None)
                                    );
                            entidadHijo.TOportunidadPrerequisitoGeneral.Add(entidadHijo2);
                        }
                    }
                    if (objetoBO.OportunidadCompetidor.ListaPrerequisitoEspecifico != null && objetoBO.OportunidadCompetidor.ListaPrerequisitoEspecifico.Count > 0)
                    {
                        foreach (var hijo in objetoBO.OportunidadCompetidor.ListaPrerequisitoEspecifico)
                        {
                            TOportunidadPrerequisitoEspecifico entidadHijo2 = new TOportunidadPrerequisitoEspecifico();
                            entidadHijo2 = Mapper.Map<OportunidadPrerequisitoEspecificoBO, TOportunidadPrerequisitoEspecifico>(
                                hijo,
                                opt => opt.ConfigureMap(MemberList.None)
                                    );
                            entidadHijo.TOportunidadPrerequisitoEspecifico.Add(entidadHijo2);
                        }
                    }
                    if (objetoBO.OportunidadCompetidor.ListaBeneficio != null && objetoBO.OportunidadCompetidor.ListaBeneficio.Count > 0)
                    {
                        foreach (var hijo in objetoBO.OportunidadCompetidor.ListaBeneficio)
                        {
                            TOportunidadBeneficio entidadHijo2 = new TOportunidadBeneficio();
                            entidadHijo2 = Mapper.Map<OportunidadBeneficioBO, TOportunidadBeneficio>(
                                hijo,
                                opt => opt.ConfigureMap(MemberList.None)
                                    );
                            entidadHijo.TOportunidadBeneficio.Add(entidadHijo2);
                        }
                    }
                    if (objetoBO.OportunidadCompetidor.ListaCompetidor != null && objetoBO.OportunidadCompetidor.ListaCompetidor.Count > 0)
                    {
                        foreach (var hijo in objetoBO.OportunidadCompetidor.ListaCompetidor)
                        {
                            TDetalleOportunidadCompetidor entidadHijo2 = new TDetalleOportunidadCompetidor();
                            entidadHijo2 = Mapper.Map<DetalleOportunidadCompetidorBO, TDetalleOportunidadCompetidor>(
                                hijo,
                                opt => opt.ConfigureMap(MemberList.None)
                                    );
                            entidadHijo.TDetalleOportunidadCompetidor.Add(entidadHijo2);
                        }
                    }
                }
                if (objetoBO.CalidadProcesamiento != null)
                {
                    TCalidadProcesamiento entidadHijo = new TCalidadProcesamiento();
                    entidadHijo = Mapper.Map<CalidadProcesamientoBO, TCalidadProcesamiento>(objetoBO.CalidadProcesamiento,
                        opt => opt.ConfigureMap(MemberList.None));
                    entidad.TCalidadProcesamiento.Add(entidadHijo);
                }
                if (objetoBO.ListaSoluciones != null && objetoBO.ListaSoluciones.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaSoluciones)
                    {
                        TSolucionClienteByActividad entidadHijo = new TSolucionClienteByActividad();
                        entidadHijo = Mapper.Map<SolucionClienteByActividadBO, TSolucionClienteByActividad>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TSolucionClienteByActividad.Add(entidadHijo);
                    }
                }

                //Creacion de oportunidades
                if (objetoBO.ModeloDataMining != null)
                {
                    TModeloDataMining entidadHijo = new TModeloDataMining();
                    entidadHijo = Mapper.Map<ModeloDataMiningBO, TModeloDataMining>(objetoBO.ModeloDataMining,
                        opt => opt.ConfigureMap(MemberList.None));
                    entidad.TModeloDataMining.Add(entidadHijo);
                }
                if (objetoBO.AsignacionOportunidad != null)
                {
                    TAsignacionOportunidad entidadHijo = new TAsignacionOportunidad();
                    entidadHijo = Mapper.Map<AsignacionOportunidadBO, TAsignacionOportunidad>(objetoBO.AsignacionOportunidad,
                        opt => opt.ConfigureMap(MemberList.None));
                    entidad.TAsignacionOportunidad.Add(entidadHijo);

                    if (objetoBO.AsignacionOportunidad.AsignacionOportunidadLog != null)
                    {
                        TAsignacionOportunidadLog entidadHijo2 = new TAsignacionOportunidadLog();
                        entidadHijo2 = Mapper.Map<AsignacionOportunidadLogBO, TAsignacionOportunidadLog>(objetoBO.AsignacionOportunidad.AsignacionOportunidadLog,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidadHijo.TAsignacionOportunidadLog.Add(entidadHijo2);
                    }
                }
                //if (objetoBO.AsignacionOportunidadLog != nullz)
                //{
                //    TAsignacionOportunidadLog entidadHijo = new TAsignacionOportunidadLog();
                //    entidadHijo2 = Mapper.Map<AsignacionOportunidadLogBO, TAsignacionOportunidadLog>(objetoBO.AsignacionOportunidadLog,
                //        opt => opt.ConfigureMap(MemberList.None));
                //    entidad.TAsignacionOportunidad.TAsignacionOportunidadLog.Add(entidadHijo);
                //}

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Obtiene Valores de Oportunidad PEspecifico y Pgeneral Por la ActividadDetalle
        /// </summary>
        /// <param name="idActividadDetalle"></param>
        /// <returns></returns>
        public DatosOportunidadDocumentosCompuestoDTO ObtenerDatosCompuestosPorActividades(int idActividadDetalle)
        {
            try
            {
                DatosOportunidadDocumentosCompuestoDTO datoOportunidad = new DatosOportunidadDocumentosCompuestoDTO();
                string _queryOportunidad = "Select * From com.V_OportunidadCompuesto Where IdActividadDetalle =@IdActividadDetalle";
                var _oportunidad = _dapper.FirstOrDefault(_queryOportunidad, new { IdActividadDetalle = idActividadDetalle });
                if (!string.IsNullOrEmpty(_oportunidad) && !_oportunidad.Equals("null"))
                {
                    datoOportunidad = JsonConvert.DeserializeObject<DatosOportunidadDocumentosCompuestoDTO>(_oportunidad);
                }
                return datoOportunidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene Valores de Oportunidad PEspecifico y Pgeneral Por Id de la Oportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns>Objeto del tipo DatosOportunidadDocumentosCompuestoDTO</returns>
        public DatosOportunidadDocumentosCompuestoDTO ObtenerDatosCompuestosPorIdOportunidad(int idOportunidad)
        {
            try
            {
                DatosOportunidadDocumentosCompuestoDTO datoOportunidad = new DatosOportunidadDocumentosCompuestoDTO();
                string _queryOportunidad = "Select * From com.V_OportunidadCompuesto Where Id=@Id";
                var _oportunidad = _dapper.FirstOrDefault(_queryOportunidad, new { Id = idOportunidad });
                if (!string.IsNullOrEmpty(_oportunidad) && !_oportunidad.Equals("null"))
                {
                    datoOportunidad = JsonConvert.DeserializeObject<DatosOportunidadDocumentosCompuestoDTO>(_oportunidad);
                }
                return datoOportunidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Carga una lista de oportunidades por id alumno
        /// </summary>
        public List<OportunidadVentaCruzadaDTO> ObtenerHistorialOportunidades(int idAlumno, int idClasificacionPersona)
        {
            try
            {
                List<OportunidadVentaCruzadaDTO> oportunidadesVentaCruzada = new List<OportunidadVentaCruzadaDTO>();
                var oportunidadesVentaCruzadaDB = _dapper.QuerySPDapper("com.SP_Oportunidad_VentaCruzadaTop5", new { idClasificacionPersona });

                if (!string.IsNullOrEmpty(oportunidadesVentaCruzadaDB) && !oportunidadesVentaCruzadaDB.Contains("[]"))
                {
                    oportunidadesVentaCruzada = JsonConvert.DeserializeObject<List<OportunidadVentaCruzadaDTO>>(oportunidadesVentaCruzadaDB);
                    if (oportunidadesVentaCruzada != null)
                    {
                        oportunidadesVentaCruzada = oportunidadesVentaCruzada.Where(x => x.Orden == 1).OrderByDescending(w => w.Costo).ToList();
                    }
                }
                return oportunidadesVentaCruzada;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<OportunidadVentaCruzadaDTO> ObtenerHistorialOportunidadesReporte(int idAlumno)
        {
            try
            {
                List<OportunidadVentaCruzadaDTO> oportunidadesVentaCruzada = new List<OportunidadVentaCruzadaDTO>();
                var oportunidadesVentaCruzadaDB = _dapper.QuerySPDapper("com.SP_Oportunidad_VentaCruzadaTop5Reporte", new { idAlumno });

                if (!string.IsNullOrEmpty(oportunidadesVentaCruzadaDB) && !oportunidadesVentaCruzadaDB.Contains("[]"))
                {
                    oportunidadesVentaCruzada = JsonConvert.DeserializeObject<List<OportunidadVentaCruzadaDTO>>(oportunidadesVentaCruzadaDB);
                    if (oportunidadesVentaCruzada != null)
                    {
                        oportunidadesVentaCruzada = oportunidadesVentaCruzada.Where(x => x.Orden == 1).ToList();
                    }
                }
                return oportunidadesVentaCruzada;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el historial de interaccion por oportunidad y faseOportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="fase"></param>
        public List<ActividadOportunidadDTO> CargarHistorialInteraccionesOportunidad(int idOportunidad)
        {
            try
            {
                List<ActividadOportunidadDTO> actividadesOportunidad = new List<ActividadOportunidadDTO>();
                string _query = "SELECT CodigoFaseOportunidad FROM com.V_ObtenerCodigoFaseOportunidad WHERE IdOportunidad = @idOportunidad";
                var _registro = _dapper.FirstOrDefault(_query, new { idOportunidad });
                var faseOportunidad = JsonConvert.DeserializeObject<CodigoFaseOportunidadDTO>(_registro);
                if (faseOportunidad.CodigoFaseOportunidad == "IS" || faseOportunidad.CodigoFaseOportunidad == "M")
                {
                    string registrosDB = _dapper.QuerySPDapper("com.SP_ObtenerHistorialOcurrenciasPorOportunidad", new { idOportunidad });
                    if (!string.IsNullOrEmpty(registrosDB))
                    {
                        actividadesOportunidad = JsonConvert.DeserializeObject<List<ActividadOportunidadDTO>>(registrosDB);
                    }
                }
                else
                {
                    string registrosDB = _dapper.QuerySPDapper("com.SP_HistorialInteraccionOportunidad", new { idOportunidad });
                    if (!string.IsNullOrEmpty(registrosDB))
                    {
                        actividadesOportunidad = JsonConvert.DeserializeObject<List<ActividadOportunidadDTO>>(registrosDB);
                    }
                }
                return actividadesOportunidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Carga un historial de oportunidades por idAlumno
        /// </summary>
        public List<OportunidadHistorialDTO> CargarOportunidadHistorial(int idAlumno, int idClasificacionPersona)
        {
            try
            {
                List<OportunidadHistorialDTO> oportunidadesHistorial = new List<OportunidadHistorialDTO>();
                var registrosBO = _dapper.QuerySPDapper("com.SP_Oportunidad_HistorialOportunidades", new { idClasificacionPersona });
                if (!string.IsNullOrEmpty(registrosBO) && !registrosBO.Contains("[]"))
                {
                    oportunidadesHistorial = JsonConvert.DeserializeObject<List<OportunidadHistorialDTO>>(registrosBO);
                }
                return oportunidadesHistorial;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public DatosEnvioCorreoOportunidadDTO ObtenerDatosEnvioCorreoOportunidad(int tipoOportunidad, int idOportunidad)
        {
            try
            {
                DatosEnvioCorreoOportunidadDTO datosEnvioCorreo = new DatosEnvioCorreoOportunidadDTO();
                var registrosBO = _dapper.QuerySPFirstOrDefault("com.SP_ObtenerDatosEnvioCorreoOportunidadById", new { tipoOportunidad, idOportunidad });
                if (!string.IsNullOrEmpty(registrosBO) && !registrosBO.Contains("[]"))
                {
                    datosEnvioCorreo = JsonConvert.DeserializeObject<DatosEnvioCorreoOportunidadDTO>(registrosBO);
                }
                return datosEnvioCorreo;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Valida si existe una oportunidad en seguimiento para el mismo centro costo
        /// </summary>
        /// <param name="idContacto"></param>
        /// <param name="idCentroCosto"></param>
        /// <param name="idOcurrencia"></param>
        /// <returns></returns>
        public bool ValidarRN2(int idContacto, int idCentroCosto, int idOcurrencia)
        {
            try
            {
                var registroDB = _dapper.QuerySPFirstOrDefault("com.SP_ValidarRN2Agenda", new { idContacto, idCentroCosto, idOcurrencia });
                var _bool = JsonConvert.DeserializeObject<BoolRN2DTO>(registroDB);
                return _bool.Estado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Retorna tiempos capacitacion de un oportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns>id,idtiempoCapacitacion,IdTiempoCapacitacionValidacion</returns>
        public OportunidadTiempoCapacitacionDTO ObtenerOportunidadTiempoCapacitacion(int idOportunidad)
        {
            try
            {
                OportunidadTiempoCapacitacionDTO oportunidadTiempoCapacitacion = new OportunidadTiempoCapacitacionDTO();
                string _queryOportunidad = string.Empty;
                _queryOportunidad = "SELECT Id,IdTiempoCapacitacion, IdTiempoCapacitacionValidacion FROM com.V_TOportunidad_ObtenerTiempoCapacitacion WHERE Id = @idOportunidad";
                var oportunidadDB = _dapper.FirstOrDefault(_queryOportunidad, new { idOportunidad });
                if (!string.IsNullOrEmpty(oportunidadDB) && !oportunidadDB.Contains("[]"))
                {
                    oportunidadTiempoCapacitacion = JsonConvert.DeserializeObject<OportunidadTiempoCapacitacionDTO>(oportunidadDB);
                }
                return oportunidadTiempoCapacitacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        /// <summary>
        /// Carga una lista de problemas cliente por idOportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public List<OportunidadProblemaClienteDTO> ObtenerOportunidadProblemasCliente(int idOportunidad)
        {
            try
            {
                List<OportunidadProblemaClienteDTO> listaOportunidadProblemasCliente = new List<OportunidadProblemaClienteDTO>();
                var problemasClienteDB = _dapper.QuerySPDapper("com.SP_Oportunidad_ProblemasCliente", new { idOportunidad });
                listaOportunidadProblemasCliente = JsonConvert.DeserializeObject<List<OportunidadProblemaClienteDTO>>(problemasClienteDB);
                return listaOportunidadProblemasCliente;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene una lista de oportunidades a cerrar por mismo programa general y fase prelanzamiento
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idCentroCosto"></param>
        /// <param name="idAsignado"></param>
        /// <param name="idCategoriaOrigen"></param>
        /// <param name="id"></param>
        /// <returns>Lista de objetos de clase OportunidadCerrarDTO</returns>
        public List<OportunidadCerrarDTO> CrearCerrarOportunidadesMismoPGPrelanzamiento(int idAlumno, int idCentroCosto, int idAsignado, int idCategoriaOrigen, int id)
        {
            List<OportunidadCerrarDTO> oportunidadCerrar = new List<OportunidadCerrarDTO>();
            var registrosBD = _dapper.QuerySPDapper("com.Sp_CrearCerrarOportunidadMismoPGPrelanzamiento", new { idAlumno, idCentroCosto, idAsignado, idCategoriaOrigen, id });
            oportunidadCerrar = JsonConvert.DeserializeObject<List<OportunidadCerrarDTO>>(registrosBD);
            return oportunidadCerrar;
        }

        /// <summary>
        /// Trae las oportunidades en IS o M del mismo programa y del mismo alumno pero que no sea la misma oportunidad
        /// </summary>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <param name="idProgramaGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="celular">Celular del alumno</param>
        /// <returns>Lista de objetos de clase OportunidadISMDTO</returns>
        public List<OportunidadISMDTO> ValidarOportunidadesISM(int idAlumno, int idProgramaGeneral, int idOportunidad, string celular)
        {
            try
            {
                List<OportunidadISMDTO> oportunidadesISM = new List<OportunidadISMDTO>();
                var queryAsesorById = "SELECT IdOportunidad, IdProgramaGeneral, IdAlumno FROM com.V_OportunidadesISM WHERE (IdAlumno = @idAlumno OR Celular = @celular) AND IdProgramaGeneral=@idProgramaGeneral AND IdOportunidad <> @idOportunidad ";
                var registrosBD = _dapper.QueryDapper(queryAsesorById, new { idAlumno, idProgramaGeneral, idOportunidad, celular });
                oportunidadesISM = JsonConvert.DeserializeObject<List<OportunidadISMDTO>>(registrosBD) ?? new List<OportunidadISMDTO>();

                return oportunidadesISM;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Trae las oportunidades con sus probabilidades del mismo programa y del mismo alumno que que no sea la misma oportunidad
        /// </summary>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <param name="idProgramaGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="celular">Celular del alumno</param>
        /// <returns>Lista de objetos de clase OportunidadProbabilidadDTO</returns>
        public List<OportunidadProbabilidadDTO> ValidarOportunidadesProbabilidad(int idAlumno, int idProgramaGeneral, int idOportunidad, string celular)
        {
            try
            {
                List<OportunidadProbabilidadDTO> oportunidadesProbabilidad = new List<OportunidadProbabilidadDTO>();
                var queryAsesorById = "SELECT IdOportunidad, IdProgramaGeneral, IdAlumno, PesoProbabilidad FROM com.V_OportunidadesProbabilidades WHERE (IdAlumno = @idAlumno OR Celular = @celular) AND IdOportunidad <> @idOportunidad";
                var registrosBD = _dapper.QueryDapper(queryAsesorById, new { idAlumno, idProgramaGeneral, idOportunidad, celular });
                oportunidadesProbabilidad = JsonConvert.DeserializeObject<List<OportunidadProbabilidadDTO>>(registrosBD);
                if (oportunidadesProbabilidad == null)
                {
                    oportunidadesProbabilidad = new List<OportunidadProbabilidadDTO>();
                }
                return oportunidadesProbabilidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Trae las oportunidades con los pesos de sus categorias del mismo alumno que que no sea la misma oportunidad
        /// </summary>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="celular">Celular del alumno</param>
        /// <returns>Lista de objetos de clase OportunidadCategoriaDTO</returns>
        public List<OportunidadCategoriaDTO> ValidarOportunidadesCategoria(int idAlumno, int idOportunidad, string celular)
        {
            try
            {
                List<OportunidadCategoriaDTO> oportunidadesCategoria = new List<OportunidadCategoriaDTO>();
                var queryAsesorById = "SELECT IdOportunidad, IdFaseOportunidad, IdPersonalAsignado, IdProgramaGeneral, IdAlumno, PesoCategoria FROM com.V_OportunidadesMismoPGCategorias WHERE (IdAlumno = @idAlumno OR Celular = @celular) AND IdOportunidad <> @idOportunidad ";
                var registrosBD = _dapper.QueryDapper(queryAsesorById, new { idAlumno, idOportunidad, celular });
                oportunidadesCategoria = JsonConvert.DeserializeObject<List<OportunidadCategoriaDTO>>(registrosBD);
                if (oportunidadesCategoria == null)
                {
                    oportunidadesCategoria = new List<OportunidadCategoriaDTO>();
                }
                return oportunidadesCategoria;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Trae las oportunidades con los pesos de sus categorias del mismo alumno que que no sea la misma oportunidad en fase IP
        /// </summary>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <param name="idProgramaGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="celular">Celular del alumno</param>
        /// <returns>Lista de objetos de clase OportunidadCategoriaDTO</returns>
        public List<OportunidadCategoriaDTO> ValidarOportunidadesCategoriaIPMismoPG(int idAlumno, int idProgramaGeneral, int idOportunidad, string celular)
        {
            try
            {
                List<OportunidadCategoriaDTO> oportunidadesCategoria = new List<OportunidadCategoriaDTO>();
                var queryAsesorById = "SELECT IdOportunidad, IdFaseOportunidad, IdPersonalAsignado, IdProgramaGeneral, IdAlumno, PesoCategoria FROM com.V_OportunidadesCategoriasIP WHERE (IdAlumno = @idAlumno OR Celular = @celular) AND IdProgramaGeneral=@idProgramaGeneral AND IdOportunidad <> @idOportunidad ";
                var registrosBD = _dapper.QueryDapper(queryAsesorById, new { idAlumno, idProgramaGeneral, idOportunidad, celular });
                oportunidadesCategoria = JsonConvert.DeserializeObject<List<OportunidadCategoriaDTO>>(registrosBD);
                if (oportunidadesCategoria == null)
                {
                    oportunidadesCategoria = new List<OportunidadCategoriaDTO>();
                }
                return oportunidadesCategoria;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Trae las oportunidades con los pesos de sus categorias del mismo alumno pero de diferente programa que no sea la misma oportunidad en fase IP
        /// </summary>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <param name="idProgramaGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="celular">Celular del alumno</param>
        /// <returns>Lista de objetos OportunidadCategoriaDTO</returns>
        public List<OportunidadCategoriaDTO> ValidarOportunidadesCategoriaIPDiferentePG(int idAlumno, int idProgramaGeneral, int idOportunidad, string celular)
        {
            try
            {
                List<OportunidadCategoriaDTO> oportunidadesCategoria = new List<OportunidadCategoriaDTO>();
                var queryAsesorById = "SELECT IdOportunidad, IdFaseOportunidad, IdPersonalAsignado, IdProgramaGeneral, IdAlumno, PesoCategoria FROM com.V_OportunidadesCategoriasIP WHERE (IdAlumno = @idAlumno OR Celular = @celular) AND IdProgramaGeneral<>@idProgramaGeneral AND IdOportunidad <> @idOportunidad ";
                var registrosBD = _dapper.QueryDapper(queryAsesorById, new { idAlumno, idProgramaGeneral, idOportunidad, celular });
                oportunidadesCategoria = JsonConvert.DeserializeObject<List<OportunidadCategoriaDTO>>(registrosBD);
                if (oportunidadesCategoria == null)
                {
                    oportunidadesCategoria = new List<OportunidadCategoriaDTO>();
                }
                return oportunidadesCategoria;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Trae las oportunidades con los pesos de sus categorias del mismo alumno pero de diferente programa que no sea la misma oportunidad en fase BNC IT RN
        /// </summary>
        /// <param name="idAlumno">Id del alumno (PK de la tabla mkt.T_Alumno)</param>
        /// <param name="idProgramaGeneral">Id del programa general (PK de la tabla pla.T_PGeneral)</param>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="celular">Celular del alumno</param>
        /// <returns>Lista de objetos OportunidadCategoriaDTO</returns>
        public List<OportunidadCategoriaDTO> ValidarOportunidadesCategoriaBNCITRNDiferentePG(int idAlumno, int idProgramaGeneral, int idOportunidad, string celular)
        {
            try
            {
                List<OportunidadCategoriaDTO> oportunidadesCategoria = new List<OportunidadCategoriaDTO>();
                var _queryAsesorById = "SELECT IdOportunidad, IdFaseOportunidad, IdPersonalAsignado, IdProgramaGeneral, IdAlumno, PesoCategoria FROM com.V_OportunidadesCategoriasBNCITRN WHERE (IdAlumno = @idAlumno OR Celular = @celular) AND IdProgramaGeneral<>@idProgramaGeneral AND IdOportunidad <> @idOportunidad ";
                var registrosBD = _dapper.QueryDapper(_queryAsesorById, new { idAlumno, idProgramaGeneral, idOportunidad, celular });
                oportunidadesCategoria = JsonConvert.DeserializeObject<List<OportunidadCategoriaDTO>>(registrosBD);
                if (oportunidadesCategoria == null)
                {
                    oportunidadesCategoria = new List<OportunidadCategoriaDTO>();
                }
                return oportunidadesCategoria;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Repositorio: Oportunidad/ObtenerAsesorParaCentroCosto
        /// Autor: Carlos Crispin Riquelme
        /// Fecha: 25/01/2021
        /// <summary>
        /// Obtiene la lista de asesores candidatos para asignarles la oportunidad
        /// </summary>
        /// <param name="idCentroCosto">Id del centro de costo a analizar de la oportunidad entrante (PK de la tabla pla.T_CentroCosto)</param>
        /// <param name="idSubCategoriaDato">Id de la subcategoria dato de la oportunidad entrante(PK de la tabla mkt.T_SubCategoriaDato)</param>
        /// <param name="idPais">Id del pais de la oportunidad entrante (PK de la tabla conf.T_Pais)</param>
        /// <param name="probabilidad">Id de la probabilidad de la oportunidad entrante(PK de la tabla mkt.T_ProbabilidadRegistro_PW)</param>
        /// <param name="prioridad">Prioridad a analizar en el flujo (Configuracion de la interfaz de configuracion de asignacion automatica)</param>
        /// <returns>Lista de objetos de clase AsignacionAutomaticaAsesorPosibilidadDTO</returns>
        public List<AsignacionAutomaticaAsesorPosibilidadDTO> ObtenerAsesorParaCentroCosto(int idCentroCosto, int idSubCategoriaDato, int idPais, int probabilidad, int prioridad)
        {
            try
            {
                List<AsignacionAutomaticaAsesorPosibilidadDTO> oportunidadesHistorial = new List<AsignacionAutomaticaAsesorPosibilidadDTO>();
                var registrosBO = _dapper.QuerySPDapper("com.SP_ObtenerAsesorAsignacionAutomaticaAsesorCentroCosto", new { idCentroCosto, idSubCategoriaDato, idPais, probabilidad, prioridad });
                if (!string.IsNullOrEmpty(registrosBO) && !registrosBO.Contains("[]"))
                {
                    oportunidadesHistorial = JsonConvert.DeserializeObject<List<AsignacionAutomaticaAsesorPosibilidadDTO>>(registrosBO);
                }
                return oportunidadesHistorial;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la ultima oportunidad insertada con usuario 
        /// </summary>
        public EstadoOportunidadFiltroDTO ObtenerUltimaOportunidad()
        {
            try
            {
                EstadoOportunidadFiltroDTO obtenerUltima = new EstadoOportunidadFiltroDTO();
                var _query = "SELECT Id FROM com.T_EstadoOportunidad WHERE Nombre = 'Reasignada Venta Cruzada'";
                var registrosBD = _dapper.FirstOrDefault(_query, null);
                obtenerUltima = JsonConvert.DeserializeObject<EstadoOportunidadFiltroDTO>(registrosBD);
                return obtenerUltima;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los datos de un personal y su jefe 
        /// </summary>
        /// <param name="idAsesor"></param>
        /// <returns></returns>
        public PersonalReasignacionDTO ObtenerPersonalJefeReasignacion(int idAsesor)
        {
            try
            {
                PersonalReasignacionDTO personalReasignacion = new PersonalReasignacionDTO();
                var _queryAsesorById = "SELECT IdAsesor, NombreCompletoAsesor, EmailAsesor, IdJefe, NombreCompletoJefe, EmailJefe FROM gp.V_TPersonal_ObtenerPersonalJefe WHERE  IdAsesor = @idAsesor";
                var registrosBD = _dapper.FirstOrDefault(_queryAsesorById, new { idAsesor });
                personalReasignacion = JsonConvert.DeserializeObject<PersonalReasignacionDTO>(registrosBD);
                return personalReasignacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ProgramaGeneralPrerequisitoOportunidadDTO> ObtenerPrerequisitosPorOportunidad(int idOportunidad)
        {
            try
            {
                List<ProgramaGeneralPrerequisitoOportunidadDTO> programaGeneralPrerequisitosOportunidad = new List<ProgramaGeneralPrerequisitoOportunidadDTO>();
                var prerequisitosBD = _dapper.QuerySPDapper("pla.SP_ObtenerPrerequisitosPorOportunidad", new { idOportunidad });
                programaGeneralPrerequisitosOportunidad = JsonConvert.DeserializeObject<List<ProgramaGeneralPrerequisitoOportunidadDTO>>(prerequisitosBD);
                return programaGeneralPrerequisitosOportunidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ProgramaGeneralPrerequisitoOportunidadDTO> ObtenerPrerequisitosEspecificoPorOportunidad(int idOportunidad)
        {
            try
            {
                List<ProgramaGeneralPrerequisitoOportunidadDTO> programaGeneralPrerequisitosOportunidad = new List<ProgramaGeneralPrerequisitoOportunidadDTO>();
                var prerequisitosBD = _dapper.QuerySPDapper("pla.SP_ObtenerPrerequisitoEspecificoPorOportunidad", new { idOportunidad });
                programaGeneralPrerequisitosOportunidad = JsonConvert.DeserializeObject<List<ProgramaGeneralPrerequisitoOportunidadDTO>>(prerequisitosBD);
                return programaGeneralPrerequisitosOportunidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ProgramaGeneralBeneficioOportunidadDTO> ObtenerBeneficiosPorOportunidad(int idOportunidad)
        {
            try
            {
                List<ProgramaGeneralBeneficioOportunidadDTO> programaGeneralBeneficiosOportunidad = new List<ProgramaGeneralBeneficioOportunidadDTO>();
                var beneficiosDB = _dapper.QuerySPDapper("pla.SP_ObtenerBeneficiosPorOportunidad", new { idOportunidad });
                programaGeneralBeneficiosOportunidad = JsonConvert.DeserializeObject<List<ProgramaGeneralBeneficioOportunidadDTO>>(beneficiosDB);
                return programaGeneralBeneficiosOportunidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<OportunidadCompetidorDTO> ObtenerCompetidoresPorOportunidad(int idOportunidad)
        {
            try
            {
                List<OportunidadCompetidorDTO> oportunidadesCompetidor = new List<OportunidadCompetidorDTO>();
                var _query = "SELECT * FROM com.T_OportunidadCompetidor WHERE IdOportunidad = @idOportunidad";
                var prerequisitosBD = _dapper.QueryDapper(_query, new { idOportunidad });
                oportunidadesCompetidor = JsonConvert.DeserializeObject<List<OportunidadCompetidorDTO>>(prerequisitosBD);
                return oportunidadesCompetidor;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene los datos necesarios de la oportunidad para calcualr la fecha de programacion Automatica
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public DatosOportunidadReprogramacionAutomaticaDTO ObtenerDatosParaReprogramcionAutomatica(int idOportunidad)
        {
            try
            {
                string _queryOportunidad = "Select  IdPersonalAsignado,IdActividadCabeceraUltima,IdTipoDato,IdCategoriaOrigen from com.V_TOportunidad_FechaProgramacionAutomatica where Id=@IdOportunidad";
                var queryOportunidad = _dapper.FirstOrDefault(_queryOportunidad, new { idOportunidad });
                if (!queryOportunidad.Equals("null"))
                {
                    DatosOportunidadReprogramacionAutomaticaDTO Oportunidad = JsonConvert.DeserializeObject<DatosOportunidadReprogramacionAutomaticaDTO>(queryOportunidad);
                    return Oportunidad;

                }
                else
                {
                    throw new Exception("No Existe Oportunidad con Identificador " + idOportunidad);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene los datos necesarios de la oportunidad para calcualr la fecha de programacion Automatica
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public DatosOportunidadReprogramacionManualOperacioneDTO ObtenerCasosReprogramacionManualOperaciones(int idOportunidad)
        {
            try
            {
                DatosOportunidadReprogramacionManualOperacioneDTO Resultado = new DatosOportunidadReprogramacionManualOperacioneDTO();
                var registrosBD = _dapper.QuerySPFirstOrDefault("com.SP_ObtenerCasosReprogramacionManualOperaciones", new { idOportunidad });

                Resultado = JsonConvert.DeserializeObject<DatosOportunidadReprogramacionManualOperacioneDTO>(registrosBD);
                return Resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene una actividad registrada, es consultada cuando se asignan oportunidades a asesores
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="idEstadoOportunidad"></param>
        /// <returns></returns>
        public ActividadProgramadaAgendaDTO ObtenerAgendaNoProgramada(int idOportunidad, int idEstadoOportunidad)
        {
            try
            {
                ActividadProgramadaAgendaDTO actividadProgramada = new ActividadProgramadaAgendaDTO();

                var _query = "SELECT RowIndex,Id,TipoActividad,EstadoHoja,CentroCosto,Contacto,IdCargo,IdAreaFormacion,ContactoIdATrabajo,ContactoIdIndustria,CodigoFase,TipoDato,Origen,FechaActividad,HoraProgramada,IdContacto,IdOportunidad,UltimoComentario,IdActividadCabecera,ActividadesVencidas,ReprogramacionManual,ReprogramacionAutomatica,ActividadCabecera,Asesor,IdAsesor,IdCentroCosto,IdFaseOportunidad,IdTipoDato,ValidaLlamada,ProbInicial,ProbActual,CategoriaNombre,IdCategoriaOrigen FROM com.V_ObtenerActividadAgenda WHERE IdFaseOportunidad NOT IN @idsFaseOportunidad AND (IdEstadoOportunidad = @idEstadoOportunidad or IdEstadoOportunidad = 3) AND IdOportunidad = @idOportunidad";
                //var actividadProgramadaDB = _dapper.FirstOrDefault(_query, new { idsFaseOportunidad = new[] { ValorEstatico.IdFaseOportunidadRN3, ValorEstatico.IdFaseOportunidadE }, idEstadoOportunidad, idOportunidad });
                var actividadProgramadaDB = _dapper.FirstOrDefault(_query, new { idsFaseOportunidad = new[] { ValorEstatico.IdFaseOportunidadRN3, ValorEstatico.IdFaseOportunidadE }, idEstadoOportunidad, idOportunidad });
                actividadProgramada = JsonConvert.DeserializeObject<ActividadProgramadaAgendaDTO>(actividadProgramadaDB);

                if (actividadProgramada != null)
                {
                    actividadProgramada.TieneMultipleSolicitud = this.TieneMultipleSolicitud(idOportunidad);
                }
                return actividadProgramada;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene un boolean que indica si tiene mas de una oportunidad
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool TieneMultipleSolicitud(int id)
        {
            try
            {
                var Alumno = this.GetBy(x => x.Estado == true && x.Id == id, x => new { x.IdAlumno }).FirstOrDefault();
                if (Alumno != null)
                {
                    var cantidadOportunidades = this.GetBy(x => x.IdAlumno == Alumno.IdAlumno, x => new { x.Id }).Count();
                    if (cantidadOportunidades == 1)//Tiene solo una oportunidad
                    {
                        return false; ;
                    }
                    else if (cantidadOportunidades > 1)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Valida si cumple las reglas de validacion por probabilidad
        /// </summary>
        /// <param name="idProbabilidadRegistroPwActual">Id de la probabilidad de registro actual</param>
        /// <returns>Booleano</returns>
        public bool ValidarProbabilidadVentaCruzada(int? idProbabilidadRegistroPwActual)
        {
            try
            {
                idProbabilidadRegistroPwActual = idProbabilidadRegistroPwActual == null ? 0 : idProbabilidadRegistroPwActual;
                var validacionProbabilidadesVentaCruzada = this.ObtenerValidacionesProbabilidadVentaCruzada();
                return validacionProbabilidadesVentaCruzada.Any(x => x.IdProbabilidadRegistroPW == idProbabilidadRegistroPwActual);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las validades de probabilidad para venta cruzada
        /// </summary>
        /// <returns></returns>
        private List<VentaCruzadaValidacionDTO> ObtenerValidacionesProbabilidadVentaCruzada()
        {
            try
            {
                List<VentaCruzadaValidacionDTO> ventaCruzadaValidacion = new List<VentaCruzadaValidacionDTO>();
                var _query = "SELECT IdProbabilidadRegistroPW FROM mkt.V_TVentaCruzadaValidacionProbabilidad_ObtenerProbabilidades Where Estado = 1";
                var probabilidadesValidacionDB = _dapper.QueryDapper(_query, null);
                ventaCruzadaValidacion = JsonConvert.DeserializeObject<List<VentaCruzadaValidacionDTO>>(probabilidadesValidacionDB);
                return ventaCruzadaValidacion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<CentroCostoProbableDTO> ObtenerCentroCostoProbable(int idContacto, DateTime fechaActual)
        {
            try
            {
                List<CentroCostoProbableDTO> centroCostoProbables = new List<CentroCostoProbableDTO>();
                var _query = "SELECT DISTINCT IdPEspecifico, Tipo, IdPersonal, Precio FROM com.V_ObtenerCentroCostoProbable WHERE IdAlumno = @idContacto AND estadop LIKE '%Lanzamiento%' AND probabilidadActualDesc = 'Muy Alta' AND activo = 1 AND EstadoPersonal = 1 AND EstadoPeriodo = 1 AND EstadoPeriodoMeta = 1 AND EstadoPEspecifico = 1 AND EstadoProbabilidadContactoPrograma = 1 AND @fechaActual BETWEEN FechaInicial AND FechaFin";
                var centroCostoProbablesDB = _dapper.QueryDapper(_query, new { idContacto, fechaActual });
                centroCostoProbables = JsonConvert.DeserializeObject<List<CentroCostoProbableDTO>>(centroCostoProbablesDB);
                return centroCostoProbables;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Retorna si el alumno tiene oportunidades con fase en seguimiento
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <returns></returns>
        public OportunidadFaseSeguimientoDTO OportunidadesFaseSeguimiento(int idAlumno)
        {
            try
            {
                OportunidadFaseSeguimientoDTO oportunidadFaseSeguimiento = new OportunidadFaseSeguimientoDTO();
                var _query = "SELECT ID, NombreCentroCosto,NombreCompletoPersonal,CodigoFaseOportunidad FROM com.V_ObtenerOportunidadesFaseSeguimiento WHERE IdAlumno = @idAlumno  AND EstadoFaseOportunidad = 1 AND EnSeguimiento = 1 AND EstadoOportunidad = 1 ORDER BY FechaCreacion DESC";
                var oportunidadFaseSeguimientoDB = _dapper.FirstOrDefault(_query, new { idAlumno });
                oportunidadFaseSeguimiento = JsonConvert.DeserializeObject<OportunidadFaseSeguimientoDTO>(oportunidadFaseSeguimientoDB);
                return oportunidadFaseSeguimiento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el asesor asignado segun el Programa General
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="idCentroCosto"></param>
        /// <returns></returns>
        public ResultadoDTO ObtenerIdPersonalAsignadoChat(int idAlumno, int idCentroCosto)
        {
            ResultadoDTO asesorChat = new ResultadoDTO();
            var registrosBD = _dapper.QuerySPFirstOrDefault("com.SP_ObtenerIdAsignadoChat", new { idAlumno, idCentroCosto });

            asesorChat = JsonConvert.DeserializeObject<ResultadoDTO>(registrosBD);
            return asesorChat;
        }
        public ResultadoDTO GetAsesorAsociado(int idAlumno)
        {
            ResultadoDTO asesor = new ResultadoDTO();
            var registrosBD = _dapper.QuerySPFirstOrDefault("com.SP_GetAsesorAsociado", new { idAlumno });
            asesor = JsonConvert.DeserializeObject<ResultadoDTO>(registrosBD);
            return asesor;
        }

        public OportunidadReasignarDTO ObtenerDatosOportunidadReasignacion(int idOportunidad)
        {
            try
            {
                OportunidadReasignarDTO oportunidadReasignar = new OportunidadReasignarDTO();
                var _query = "SELECT IdAsesor, NombreCompletoAsesor, EmailAsesor, IdJefe, NombreCompletoJefe, EmailJefe, IdOportunidad, CodigoFaseOportunidad, Nombre1, Nombre2, ApellidoPaterno, ApellidoMaterno FROM   com.V_ObtenerDatosOportunidadAReasignar WHERE EstadoPersonal = 1 AND EstadoOportunidad = 1 AND EstadoFaseOportunidad = 1 AND IdOportunidad IN ( @idOportunidad)";
                var datosOportunidadDB = _dapper.FirstOrDefault(_query, new { idOportunidad });
                if (!string.IsNullOrEmpty(datosOportunidadDB) && !datosOportunidadDB.Contains("[]") && !datosOportunidadDB.Contains("null"))
                {
                    oportunidadReasignar = JsonConvert.DeserializeObject<OportunidadReasignarDTO>(datosOportunidadDB);
                }
                return oportunidadReasignar;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las oportunidades de un asesor inasistente
        /// </summary>
        /// <param name="idAsesor"></param>
        /// <returns></returns>
        public List<AsignacionAutomaticaAsesorCentroCostoProbabilidadDTO> ObtenerOportunidadesAsesoresInasistentes(int idAsesor)
        {
            try
            {
                List<AsignacionAutomaticaAsesorCentroCostoProbabilidadDTO> asignacionAutomaticaAsesorCentroCostoProbabilidad = new List<AsignacionAutomaticaAsesorCentroCostoProbabilidadDTO>();
                var datosOportunidadAsesorInasistenteDB = _dapper.QuerySPDapper("mkt.sp_obtenerOportunidadesAsesorInasistenteNuevoModelo", new { idAsesor });
                if (!string.IsNullOrEmpty(datosOportunidadAsesorInasistenteDB) && !datosOportunidadAsesorInasistenteDB.Contains("[]") && !datosOportunidadAsesorInasistenteDB.Contains("null"))
                {
                    asignacionAutomaticaAsesorCentroCostoProbabilidad = JsonConvert.DeserializeObject<List<AsignacionAutomaticaAsesorCentroCostoProbabilidadDTO>>(datosOportunidadAsesorInasistenteDB);
                }
                return asignacionAutomaticaAsesorCentroCostoProbabilidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: OportunidadRepositorio
        ///Autor: Edgar S.
        ///Fecha: 08/02/2021
        /// <summary>
        /// Obtener información por filtro de Oportunidades
        /// </summary>
        /// <param name="obj"> filtro de búsqueda </param>
        /// <param name="paginador"> paginador </param>
        /// <param name="filtroGrilla"> filtros de grilla </param>
        /// <param name="operadorComparacion"> Operadores de comparación </param>
        /// <returns> Lista de registros filtrados : ResultadoAsignacionManualFiltroTotalDTO </returns>
        public ResultadoAsignacionManualFiltroTotalDTO ObtenerPorFiltroPaginaManual(AsignacionManualOportunidadFiltroDTO obj, Paginador paginador, GridFilters filtroGrilla, List<OperadorComparacionDTO> operadorComparacion)
        {
            try
            {
                var filtros = new object();
                string queryCondicion = string.Empty;
                string contacto = string.Empty;
                List<string> palabraContacto = new List<string>();
                string email1 = string.Empty;
                int flagVentaCruzada = 0;
                int usuarioModificacion = 0;
                string[] idFaseoportunidad = new string[6];
                string[] idArea = new string[6];
                string[] idSubArea = new string[6];
                string[] idPersonal = new string[6];
                string[] idCategoriaOrigen = new string[6];
                string[] idCentroCosto = new string[6];
                string[] idProbabilidad = new string[6];
                string[] idTipoDato = new string[6];
                string[] idPais = new string[6];
                string[] idPrograma = new string[6];
                string[] idTipoCategoriaOrigen = new string[6];

                // Filtros Grilla 
                string condicion = string.Empty;
                string centroCosto = string.Empty;
                string asesor = string.Empty;
                string tipoDato = string.Empty;
                string nombreFase = string.Empty;
                string email = string.Empty;
                string categoria = string.Empty;
                string nombreCampania = string.Empty;
                string contacto1 = string.Empty;
                string estadoOportunidad = string.Empty;
                string probabilidadActual = string.Empty;
                string nombreGrupo = string.Empty;
                string areaCapacitacion = string.Empty;
                string subAreaCapacitacion = string.Empty;

                if (filtroGrilla != null)
                {

                    foreach (var item in filtroGrilla.Filters)
                    {
                        if (item.Field == "CentroCosto" && item.Value.Contains(""))
                        {
                            condicion += " AND CentroCosto LIKE @CentroCosto ";
                            centroCosto = item.Value;
                        }
                        else if (item.Field == "Asesor" && item.Value.Contains(""))
                        {
                            condicion += " AND Asesor LIKE @Asesor ";
                            asesor = item.Value;
                        }
                        else if (item.Field == "TipoDato" && item.Value.Contains(""))
                        {
                            condicion += " AND TipoDato LIKE @TipoDato ";
                            tipoDato = item.Value;
                        }
                        else if (item.Field == "NombreFase" && item.Value.Contains(""))
                        {
                            condicion += " AND NombreFase LIKE @NombreFase ";
                            nombreFase = item.Value;
                        }
                        else if (item.Field == "Contacto" && item.Value.Contains(""))
                        {
                            condicion += " AND Contacto LIKE @Contacto1 ";
                            contacto1 = item.Value;
                        }
                        else if (item.Field == "Email" && item.Value.Contains(""))
                        {
                            condicion += " AND Email LIKE @Email ";
                            email = item.Value;
                        }
                        else if (item.Field == "Categoria" && item.Value.Contains(""))
                        {
                            condicion += " AND Categoria LIKE @Categoria ";
                            categoria = item.Value;
                        }
                        else if (item.Field == "NombreCampania" && item.Value.Contains(""))
                        {
                            condicion += " AND NombreCampania LIKE @NombreCampania ";
                            nombreCampania = item.Value;
                        }
                        else if (item.Field == "EstadoOportunidad" && item.Value.Contains(""))
                        {
                            condicion += " AND EstadoOportunidad LIKE @EstadoOportunidad ";
                            estadoOportunidad = item.Value;
                        }
                        else if (item.Field == "ProbabilidadActual" && item.Value.Contains(""))
                        {
                            condicion += " AND ProbabilidadActual LIKE @ProbabilidadActual ";
                            probabilidadActual = item.Value;
                        }
                        else if (item.Field == "NombreGrupo" && item.Value.Contains(""))
                        {
                            condicion += " AND NombreGrupo LIKE @NombreGrupo ";
                            nombreGrupo = item.Value;
                        }
                        else if (item.Field == "AreaCapacitacion" && item.Value.Contains(""))
                        {
                            condicion += " AND AreaCapacitacion LIKE @AreaCapacitacion ";
                            areaCapacitacion = item.Value;
                        }
                        else if (item.Field == "SubAreaCapacitacion" && item.Value.Contains(""))
                        {
                            condicion += " AND SubAreaCapacitacion LIKE @SubAreaCapacitacion ";
                            subAreaCapacitacion = item.Value;
                        }
                    }
                }

                //Filtros Combos
                if (obj.Area != string.Empty)
                {
                    queryCondicion += "AND IdArea IN @IdArea ";
                    idArea = obj.Area.Split(",");
                }
                if (obj.subArea != string.Empty)
                {
                    queryCondicion += "AND IdSubArea IN @IdSubArea ";
                    idSubArea = obj.subArea.Split(",");
                }
                if (obj.FasesOportunidad != string.Empty)
                {
                    queryCondicion += "AND IdFaseOportunidad IN @IdFaseOportunidad ";
                    idFaseoportunidad = obj.FasesOportunidad.Split(",");
                }
                if (obj.CentrosCosto != string.Empty)
                {
                    queryCondicion += "AND IdCentroCosto IN @IdCentroCosto ";
                    idCentroCosto = obj.CentrosCosto.Split(",");
                }
                if (obj.Asesores != string.Empty)
                {
                    queryCondicion += "AND IdPersonal IN @IdPersonal ";
                    idPersonal = obj.Asesores.Split(",");
                }
                if (obj.Probabilidad != string.Empty)
                {
                    queryCondicion += "AND IdProbabilidad IN @IdProbabilidad ";
                    idProbabilidad = obj.Probabilidad.Split(",");
                }
                if (obj.Programa != string.Empty)
                {
                    queryCondicion += "AND IdPrograma IN @IdPrograma ";
                    idPrograma = obj.Programa.Split(",");
                }
                if (obj.Categorias != string.Empty)
                {
                    queryCondicion += "AND IdCategoriaOrigen IN @IdCategoriaOrigen ";
                    idCategoriaOrigen = obj.Categorias.Split(",");
                }
                if (obj.TiposDato != string.Empty)
                {
                    queryCondicion += "AND IdTipoDato IN @IdTipoDato ";
                    idTipoDato = obj.TiposDato.Split(",");
                }
                if (obj.Pais != string.Empty)
                {
                    queryCondicion += "AND IdPais IN @IdPais ";
                    idPais = obj.Pais.Split(",");
                }
                if (obj.TipoCategoriaOrigen != string.Empty)
                {
                    queryCondicion += "AND IdTipoCategoriaOrigen IN @IdTipoCategoriaOrigen ";
                    idTipoCategoriaOrigen = obj.TipoCategoriaOrigen.Split(",");
                }
                if (obj.contacto != string.Empty && obj.email == string.Empty)
                {
                    queryCondicion += "AND IdAlumno IN (SELECT Id FROM (SELECT ALU.Id, CONCAT(ALU.Nombre1, ' ', ALU.Nombre2, ' ', ALU.ApellidoPaterno, ' ', ALU.ApellidoMaterno) AS Contacto FROM mkt.T_Alumno AS ALU WHERE ALU.Estado = 1) AS T0 WHERE T0.Contacto LIKE CONCAT('%',@Contacto,'%')) ";
                    contacto = Regex.Replace(obj.contacto.Trim(), @"\s+|\s", "%");
                }

                if (obj.email != string.Empty && obj.contacto == string.Empty)
                {
                    queryCondicion += "AND IdAlumno IN (SELECT Id FROM (SELECT ALU.Id, ALU.Email1 FROM mkt.T_Alumno AS ALU WHERE ALU.Estado = 1) AS T0 WHERE T0.Email1 LIKE CONCAT('%',@Email1,'%')) ";
                    email1 = Regex.Replace(obj.email.Trim(), @"\s+|\s", "%");
                }

                if (obj.contacto != string.Empty && obj.email != string.Empty)
                {
                    queryCondicion += "AND IdAlumno IN (SELECT Id FROM (SELECT ALU.Id, CONCAT(ALU.Nombre1, ' ', ALU.Nombre2, ' ', ALU.ApellidoPaterno, ' ', ALU.ApellidoMaterno) AS Contacto, ALU.Email1 FROM mkt.T_Alumno AS ALU WHERE ALU.Estado = 1) AS T0 WHERE T0.Contacto LIKE CONCAT('%',@Contacto,'%') AND T0.Email1 LIKE CONCAT('%',@Email1,'%')) ";
                    contacto = Regex.Replace(obj.contacto.Trim(), @"\s+|\s", "%");
                    email1 = Regex.Replace(obj.email.Trim(), @"\s+|\s", "%");
                }


                if (obj.UsuarioModificacion != string.Empty)
                {
                    queryCondicion += "AND PersonalModificacion = @UsuarioModificacion ";
                    usuarioModificacion = Int32.Parse(obj.UsuarioModificacion);
                }
                if (obj.ventaCruzada != string.Empty)
                {
                    queryCondicion += "AND FlagVentaCruzada = @FlagVentaCruzada ";
                    flagVentaCruzada = Int32.Parse(obj.ventaCruzada);
                }
                if (obj.FechaProgramacionInicio != null && obj.FechaProgramacionFin != null)
                {
                    queryCondicion += "AND UltimaFechaProgramada BETWEEN @FechaProgramacionInicio AND @FechaProgramacionFin ";
                    obj.FechaProgramacionInicio = new DateTime(obj.FechaProgramacionInicio.Value.Year, obj.FechaProgramacionInicio.Value.Month, obj.FechaProgramacionInicio.Value.Day, 0, 0, 0);
                    obj.FechaProgramacionFin = new DateTime(obj.FechaProgramacionFin.Value.Year, obj.FechaProgramacionFin.Value.Month, obj.FechaProgramacionFin.Value.Day, 23, 59, 59);
                }

                // Filtros por cantidad de oportunidades
                if (obj.NroOportunidades != null && obj.IdOperadorComparacionNroOportunidades != null)
                {
                    var simbolo = operadorComparacion.Where(x => x.Id == obj.IdOperadorComparacionNroOportunidades.Value).FirstOrDefault().Simbolo;
                    queryCondicion += @" AND NroOportunidades " + simbolo + " " + obj.NroOportunidades;
                }
                if (obj.NroSolicitud != null && obj.IdOperadorComparacionNroSolicitud != null)
                {
                    var simbolo = operadorComparacion.Where(x => x.Id == obj.IdOperadorComparacionNroSolicitud.Value).FirstOrDefault().Simbolo;
                    queryCondicion += @" AND NroSolicitud " + simbolo + " " + obj.NroSolicitud;
                }
                if (obj.NroSolicitudPorArea != null && obj.IdOperadorComparacionNroSolicitudPorArea != null)
                {
                    var simbolo = operadorComparacion.Where(x => x.Id == obj.IdOperadorComparacionNroSolicitudPorArea.Value).FirstOrDefault().Simbolo;
                    queryCondicion += @" AND NroSolicitudPorArea " + simbolo + " " + obj.NroSolicitudPorArea;
                }
                if (obj.NroSolicitudPorSubArea != null && obj.IdOperadorComparacionNroSolicitudPorSubArea != null)
                {
                    var simbolo = operadorComparacion.Where(x => x.Id == obj.IdOperadorComparacionNroSolicitudPorSubArea.Value).FirstOrDefault().Simbolo;
                    queryCondicion += @" AND NroSolicitudPorSubArea " + simbolo + " " + obj.NroSolicitudPorSubArea;
                }
                if (obj.NroSolicitudPorProgramaGeneral != null && obj.IdOperadorComparacionNroSolicitudPorProgramaGeneral != null)
                {
                    var simbolo = operadorComparacion.Where(x => x.Id == obj.IdOperadorComparacionNroSolicitudPorProgramaGeneral.Value).FirstOrDefault().Simbolo;
                    queryCondicion += @" AND NroSolicitudPorProgramaGeneral " + simbolo + " " + obj.NroSolicitudPorProgramaGeneral;
                }
                if (obj.NroSolicitudPorProgramaEspecifico != null && obj.IdOperadorComparacionNroSolicitudPorProgramaEspecifico != null)
                {
                    var simbolo = operadorComparacion.Where(x => x.Id == obj.IdOperadorComparacionNroSolicitudPorProgramaEspecifico.Value).FirstOrDefault().Simbolo;
                    queryCondicion += @" AND NroSolicitudPorProgramaEspecifico " + simbolo + " " + obj.NroSolicitudPorProgramaEspecifico;
                }

                if (obj.FechaFin == null || obj.FechaInicio == null)
                {
                    obj.FechaFin = DateTime.Now;
                    obj.FechaInicio = DateTime.Now;
                }
                obj.FechaFin = obj.FechaFin.Value.AddDays(1);
                string queryCampos = "IdAlumno,Id,IdCentroCosto,CentroCosto,IdPersonal,Asesor,IdTipoDato,IdFaseOportunidad,IdOrigen,Contacto,Email,UsuarioModificacion,FechaRegistroCampania,Categoria,NombreGrupo,AreaCapacitacion,SubAreaCapacitacion,NombreCampania,FechaCreacion,FechaModificacion,UltimaFechaProgramada,IdEstadoOportunidad,EstadoOportunidad,ProbabilidadActual, NroOportunidades, NroSolicitud, NroSolicitudPorArea, NroSolicitudPorSubArea, NroSolicitudPorProgramaGeneral, NroSolicitudPorProgramaEspecifico, DiasSinContactoManhana, DiasSinContactoTarde, NombrePais";

                ResultadoAsignacionManualFiltroTotalDTO resultado = new ResultadoAsignacionManualFiltroTotalDTO();

                if (paginador != null && paginador.take != 0)
                {
                    string queryOportunidadConsulta = "SELECT " + queryCampos + " FROM com.V_TOportunidad_ObtenerPorFiltroPaginaNuevoModelo WHERE FechaCreacion BETWEEN @FechaInicio AND @FechaFin " + condicion + " " + queryCondicion + " ORDER BY FechaCreacion DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
                    var queryOportunidad = _dapper.QueryDapper(queryOportunidadConsulta, new { obj.FechaInicio, obj.FechaFin, obj.FechaProgramacionInicio, obj.FechaProgramacionFin, CentroCosto = "%" + centroCosto + "%", Asesor = "%" + asesor + "%", TipoDato = "%" + tipoDato + "%", NombreFase = "%" + nombreFase + "%", Contacto1 = "%" + contacto1 + "%", Email = "%" + email + "%", Categoria = "%" + categoria + "%", NombreCampania = "%" + nombreCampania + "%", EstadoOportunidad = "%" + estadoOportunidad + "%", ProbabilidadActual = "%" + probabilidadActual + "%", NombreGrupo = "%" + nombreGrupo + "%", AreaCapacitacion = "%" + areaCapacitacion + "%", SubAreaCapacitacion = "%" + subAreaCapacitacion + "%", idFaseoportunidad, idArea, idSubArea, idCentroCosto, idCategoriaOrigen, idPrograma, idProbabilidad, idPersonal, contacto, Email1 = email1, usuarioModificacion, idTipoDato, idPais, idTipoCategoriaOrigen, flagVentaCruzada, Skip = paginador.skip, Take = paginador.take });
                    var rpta = JsonConvert.DeserializeObject<List<ResultadoAsignacionManualFiltroDTO>>(queryOportunidad);
                    var cantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapper.FirstOrDefault("Select Count(*) From com.V_TOportunidad_ObtenerPorFiltroPaginaNuevoModelo where FechaCreacion Between @FechaInicio AND @FechaFin " + condicion + " " + queryCondicion + "", new { obj.FechaInicio, obj.FechaFin, obj.FechaProgramacionInicio, obj.FechaProgramacionFin, CentroCosto = "%" + centroCosto + "%", Asesor = "%" + asesor + "%", TipoDato = "%" + tipoDato + "%", NombreFase = "%" + nombreFase + "%", Contacto1 = "%" + contacto1 + "%", Email = "%" + email + "%", Categoria = "%" + categoria + "%", NombreCampania = "%" + nombreCampania + "%", EstadoOportunidad = "%" + estadoOportunidad + "%", ProbabilidadActual = "%" + probabilidadActual + "%", NombreGrupo = "%" + nombreGrupo + "%", AreaCapacitacion = "%" + areaCapacitacion + "%", SubAreaCapacitacion = "%" + subAreaCapacitacion + "%", idFaseoportunidad, idArea, idSubArea, idCentroCosto, idCategoriaOrigen, idPrograma, idProbabilidad, idPersonal, contacto, Email1 = email1, usuarioModificacion, idTipoDato, idPais, idTipoCategoriaOrigen, flagVentaCruzada }));


                    resultado.data = rpta;
                    resultado.Total = cantidadRegistros.Select(w => w.Value).FirstOrDefault();
                }
                else
                {
                    string queryOportunidadConsulta = "SELECT " + queryCampos + " FROM com.V_TOportunidad_ObtenerPorFiltroPaginaNuevoModelo WHERE FechaCreacion BETWEEN @FechaInicio AND @FechaFin " + queryCondicion + " ORDER BY FechaCreacion DESC";
                    var queryOportunidad = _dapper.QueryDapper(queryOportunidadConsulta, new { obj.FechaInicio, obj.FechaFin, obj.FechaProgramacionInicio, obj.FechaProgramacionFin, CentroCosto = "%" + centroCosto + "%", Asesor = "%" + asesor + "%", TipoDato = "%" + tipoDato + "%", NombreFase = "%" + nombreFase + "%", Contacto1 = "%" + contacto1 + "%", Email = "%" + email + "%", Categoria = "%" + categoria + "%", NombreCampania = "%" + nombreCampania + "%", EstadoOportunidad = "%" + estadoOportunidad + "%", ProbabilidadActual = "%" + probabilidadActual + "%", idFaseoportunidad, idArea, idSubArea, idCentroCosto, idCategoriaOrigen, idPrograma, idProbabilidad, idPersonal, contacto, email1, usuarioModificacion, idTipoDato, idPais, idTipoCategoriaOrigen, flagVentaCruzada });
                    var rpta = JsonConvert.DeserializeObject<List<ResultadoAsignacionManualFiltroDTO>>(queryOportunidad);
                    var cantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapper.FirstOrDefault("Select Count (*) From com.V_TOportunidad_ObtenerPorFiltroPaginaNuevoModelo where FechaCreacion Between @FechaInicio AND @FechaFin " + queryCondicion + "", new { obj.FechaInicio, obj.FechaFin, obj.FechaProgramacionInicio, obj.FechaProgramacionFin, CentroCosto = "%" + centroCosto + "%", Asesor = "%" + asesor + "%", TipoDato = "%" + tipoDato + "%", NombreFase = "%" + nombreFase + "%", Contacto1 = "%" + contacto1 + "%", Email = "%" + email + "%", Categoria = "%" + categoria + "%", NombreCampania = "%" + nombreCampania + "%", EstadoOportunidad = "%" + estadoOportunidad + "%", ProbabilidadActual = "%" + probabilidadActual + "%", NombreGrupo = "%" + nombreGrupo + "%", AreaCapacitacion = "%" + areaCapacitacion + "%", SubAreaCapacitacion = "%" + subAreaCapacitacion + "%", idFaseoportunidad, idArea, idSubArea, idCentroCosto, idCategoriaOrigen, idPrograma, idProbabilidad, idPersonal, contacto, email1, usuarioModificacion, idTipoDato, idPais, idTipoCategoriaOrigen, flagVentaCruzada }));

                    resultado.data = rpta;
                    resultado.Total = cantidadRegistros.Select(w => w.Value).FirstOrDefault();
                }

                return resultado;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public object ObtenerPorFiltroRevertirFase(RevertirFaseFiltroDTO obj, Paginador paginador)
        {
            try
            {
                var total = 0;
                var filtros = new object();
                string _queryCondicion = "";

                string Contacto = "";
                string[] IdFaseoportunidad = new string[6];
                string[] IdPersonal = new string[6];
                string[] IdOrigen = new string[6];
                string[] IdCentroCosto = new string[6];
                string[] IdTipoDato = new string[6];


                if (obj.FaseOportunidad != "")
                {
                    _queryCondicion = _queryCondicion + "AND IdFaseOportunidad in @IdFaseOportunidad ";
                    IdFaseoportunidad = obj.FaseOportunidad.Split(",");
                }
                if (obj.Oportunidad != "")
                {
                    _queryCondicion = _queryCondicion + "AND IdCentroCosto in @IdCentroCosto ";
                    IdCentroCosto = obj.Oportunidad.Split(",");
                }
                if (obj.Asesor != "")
                {
                    _queryCondicion = _queryCondicion + "AND IdPersonal in @IdPersonal ";
                    IdPersonal = obj.Asesor.Split(",");
                }
                if (obj.Origen != "")
                {
                    _queryCondicion = _queryCondicion + "AND IdOrigen in @IdOrigen ";
                    IdOrigen = obj.Origen.Split(",");
                }
                if (obj.TipoDato != "")
                {
                    _queryCondicion = _queryCondicion + "AND IdTipoDato in @IdTipoDato ";
                    IdTipoDato = obj.TipoDato.Split(",");
                }

                if (obj.Alumno != "")
                {
                    _queryCondicion = _queryCondicion + "AND Alumno like CONCAT('%',@Contacto,'%')";
                    Contacto = obj.Alumno;
                }

                string _queryCampos = " Id,Oportunidad,Asesor,TipoDato,FaseOportunidad,Origen,Alumno, IdFaseOportunidad, IdCentroCosto, IdPersonal, IdOrigen, IdTipoDato, IdAlumno";

                if (paginador != null && paginador.take != 0)
                {
                    string _queryOportunidad = "Select " + _queryCampos + " from com.V_ObtenerOportunidad_RevertirFase where Estado = 1 " + _queryCondicion + " order by FechaCreacion desc OFFSET  @Skip ROWS FETCH NEXT @Take ROWS ONLY";
                    var queryOportunidad = _dapper.QueryDapper(_queryOportunidad, new { IdFaseoportunidad, IdCentroCosto, IdOrigen, IdPersonal, Contacto, IdTipoDato, Skip = paginador.skip, Take = paginador.take });
                    var rpta = JsonConvert.DeserializeObject<List<RevertirFaseFiltroDTO>>(queryOportunidad);
                    var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapper.FirstOrDefault("Select Count(*) From com.V_ObtenerOportunidad_RevertirFase where Estado = 1 " + _queryCondicion + "", new { IdFaseoportunidad, IdCentroCosto, IdOrigen, IdPersonal, Contacto, IdTipoDato }));

                    return new { data = rpta, Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault() };
                }
                else
                {
                    string _queryOportunidad = "Select " + _queryCampos + " from com.V_ObtenerOportunidad_RevertirFase where Estado = 1 " + _queryCondicion + " order by FechaCreacion desc";
                    var queryOportunidad = _dapper.QueryDapper(_queryOportunidad, new { IdFaseoportunidad, IdCentroCosto, IdOrigen, IdPersonal, Contacto, IdTipoDato });
                    var rpta = JsonConvert.DeserializeObject<List<RevertirFaseFiltroDTO>>(queryOportunidad);
                    var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapper.FirstOrDefault("Select Count (*) From com.V_ObtenerOportunidad_RevertirFase where Estado = 1" + _queryCondicion + "", new { IdFaseoportunidad, IdCentroCosto, IdOrigen, IdPersonal, Contacto, IdTipoDato }));

                    return new { data = rpta, Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault() };
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public object ObtenerPorFiltroRegistrarOportunidad(FiltrosRegistrarOportunidadDTO obj, Paginador paginador)
        {
            try
            {

                var filtros = new object();
                string Alumno = "";
                string _queryCondicion = "";
                string[] IdFaseoportunidad = new string[6];
                string[] IdPersonal = new string[6];
                string[] IdOrigen = new string[6];
                string[] IdCentroCosto = new string[6];
                string[] IdTipoDato = new string[6];
                string Paginacion = "";

                if (obj.FasesOportunidad != "")
                {
                    _queryCondicion = _queryCondicion + "AND IdFaseOportunidad in @IdFaseOportunidad ";
                    IdFaseoportunidad = obj.FasesOportunidad.Split(",");
                }
                if (obj.Origenes != "")
                {
                    _queryCondicion = _queryCondicion + "AND IdOrigen in @IdOrigen ";
                    IdOrigen = obj.Origenes.Split(",");
                }
                if (obj.CentrosCosto != "")
                {
                    _queryCondicion = _queryCondicion + "AND IdCentroCosto in @IdCentroCosto ";
                    IdCentroCosto = obj.CentrosCosto.Split(",");
                }
                if (obj.Asesores != "")
                {
                    _queryCondicion = _queryCondicion + "AND IdPersonal in @IdPersonal ";
                    IdPersonal = obj.Asesores.Split(",");
                }
                if (obj.TiposDato != "")
                {
                    _queryCondicion = _queryCondicion + "AND IdTipoDato in @IdTipoDato ";
                    IdTipoDato = obj.TiposDato.Split(",");
                }
                if (obj.contacto != "")
                {
                    _queryCondicion = _queryCondicion + "AND ((@Alumno = '' AND (ApellidoPaterno + ' ' + ApellidoMaterno + ' ' + Nombre1 + ' ' + Nombre2 ) <> '')" +
                                                        " OR(@Alumno <> '' AND(ApellidoPaterno + ' ' + ApellidoMaterno + (case ApellidoMaterno when '' then '' else ' ' end) +Nombre1 + ' ' + Nombre2) like '%' + @Alumno + '%')) ";
                    Alumno = obj.contacto;
                }
                if (obj.FechaFin == null || obj.FechaInicio == null)
                {
                    obj.FechaFin = DateTime.Now;
                    obj.FechaInicio = DateTime.Now;
                }

                if (paginador.take != 0)
                {
                    Paginacion = " OFFSET  @Skip ROWS FETCH NEXT @Take ROWS ONLY";
                }
                obj.FechaFin = obj.FechaFin.Value.AddDays(1);
                string _queryCampos = " Id,Nombre1,Nombre2,ApellidoPaterno,ApellidoMaterno,Email1,Email2,IdCentroCosto,NombreCentroCosto,IdPersonal,NombrePersonal,IdTipoDato,NombreTipoDato,IdFaseOportunidad,CodigoFase,CodigoFaseMaxima,IdOrigen,NombreOrigen,NombrePais,CodigoPais,NombreCiudad,CodigoCiudad,HoraPeru,HoraContacto,Celular,Telefono,Direccion,Dni,IdEmpresa,IdCargo,IdFormacion,IdTrabajo,IdIndustria,IdOportunidad,FechaCreacionOportunidad,IdReferido,Asociado,NombreGrupo,CodigoMailing";


                string _queryOportunidad = "Select " + _queryCampos + " from [com].[V_ObtenerOportunidadesContactos2] where FechaCreacionOportunidad Between @FechaInicio AND @FechaFin " + _queryCondicion + " order by FechaCreacionOportunidad desc" + Paginacion;
                var queryOportunidad = _dapper.QueryDapper(_queryOportunidad, new { obj.FechaInicio, obj.FechaFin, IdOrigen, IdFaseoportunidad, IdCentroCosto, IdPersonal, Alumno, IdTipoDato, Skip = paginador.skip, Take = paginador.take });
                var rpta = JsonConvert.DeserializeObject<List<ResultadoRegistrarOportunidadFiltroDTO>>(queryOportunidad);
                var CantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapper.FirstOrDefault("Select Count(*) From [com].[V_ObtenerOportunidadesContactos2] where FechaCreacionOportunidad Between @FechaInicio AND @FechaFin " + _queryCondicion + "", new { obj.FechaInicio, obj.FechaFin, IdFaseoportunidad, IdCentroCosto, IdOrigen, IdPersonal, Alumno, IdTipoDato }));

                foreach (var item in rpta)
                {
                    item.Email1 = EncriptarStringCorreo(item.Email1);
                    item.Email2 = EncriptarStringCorreo(item.Email2);
                    item.Celular = EncriptarStringNumero(item.Celular);
                }

                return new { data = rpta, Total = CantidadRegistros.Select(w => w.Value).FirstOrDefault() };


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string EncriptarStringCorreo(string parametro)
        {
            string respuesta = parametro;
            if (parametro != null)
            {
                int posicion = parametro.IndexOf("@");

                if (posicion > 0)
                {
                    respuesta = new string('x', posicion) + parametro.Remove(0, posicion);
                }
            }
            return respuesta;
        }
        public string EncriptarStringNumero(string parametro)
        {
            string respuesta = parametro;
            if (parametro != null)
            {
                int longitud = parametro.Length;
                if (longitud > 4)
                {
                    int posicion = longitud - 4;
                    //respuesta = parametro.Remove(0, posicion) + new string('x', 4);
                    respuesta = parametro.Remove(posicion, 4) + new string('x', 4);
                }
            }
            return respuesta;
        }
        /// <summary>
        /// Obtiene las oportunidades anteriores de un Alumno
        /// </summary>
        /// <param name="idContacto"></param>
        /// <param name="fechaActual"></param>
        /// <returns></returns>
        public List<OportunidadAnteriorDTO> ObtenerOportunidadesAnterioresAlumno(int idAlumno)
        {
            try
            {
                List<OportunidadAnteriorDTO> oportunidadesAnteriores = new List<OportunidadAnteriorDTO>();
                var _query = "SELECT IdOportunidad, FaseOportunidad, FaseMaxima, FechaCreacion, CentroCosto , Programa, Probabilidad, Grupo, FechaSolicitud, Asesor, TipoDato, CategoriaOrigen FROM com.V_ObtenerOportunidadesReporteSeguimiento WHERE IdAlumno = @idAlumno AND EstadoOportunidad = 1" +
                    "ORDER BY FechaCreacion DESC";
                var respuestaQuery = _dapper.QueryDapper(_query, new { idAlumno });
                oportunidadesAnteriores = JsonConvert.DeserializeObject<List<OportunidadAnteriorDTO>>(respuestaQuery);
                return oportunidadesAnteriores;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las llamadas 3cx
        /// </summary>
        /// <param name="idActividadDetalle"></param>
        /// <returns></returns>
        public List<ActividadesEjecutadasTresCXDTO> GetLlamadasTresCXPorActividadDetalle(int idActividadDetalle)
        {
            List<ActividadesEjecutadasTresCXDTO> actividadesEjecutadas = new List<ActividadesEjecutadasTresCXDTO>();
            try
            {
                var registrosBD = _dapper.QuerySPDapper("com.SP_ObtenerLlamadasTRESCXPorActividadDetalle", new { IdActividadDetalle = idActividadDetalle });
                actividadesEjecutadas = JsonConvert.DeserializeObject<List<ActividadesEjecutadasTresCXDTO>>(registrosBD);
                return actividadesEjecutadas;
            }
            catch (Exception)
            {
                return actividadesEjecutadas;
            }

        }

        /// <summary>
        /// Obtiene informacion complementaria para el Reporte de Seguimiento
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public ReporteSeguimientoOportunidadComplementosDTO ObtenerInformacionComplementariaReporteSeguimiento(int idOportunidad)
        {
            try
            {
                ReporteSeguimientoOportunidadComplementosDTO complementosDTO = new ReporteSeguimientoOportunidadComplementosDTO();
                var _query = "SELECT ProbabilidadActual, CodigoFase, CategoriaOrigen, CentroCosto,EstadoMatricula,SubEstadoMatricula FROM com.V_ObtenerComplementosReporteSeguimientoOportundidad WHERE IdOportunidad = @idOportunidad";
                var queryRespuesta = _dapper.FirstOrDefault(_query, new { idOportunidad });
                if (!string.IsNullOrEmpty(queryRespuesta) && !queryRespuesta.Contains("[]"))
                {
                    complementosDTO = JsonConvert.DeserializeObject<ReporteSeguimientoOportunidadComplementosDTO>(queryRespuesta);
                }
                return complementosDTO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene historial oportunidad mediante idAlumno
        /// </summary>
        /// <param name="idContacto"></param>
        /// <returns></returns>
        public List<HistorialOportunidadDTO> ObtenerHistorialOportunidadPorIdContacto(int idAlumno)
        {
            try
            {
                List<HistorialOportunidadDTO> historialOportunidad = new List<HistorialOportunidadDTO>();
                var _query = string.Empty;
                _query = "SELECT IdOportunidad, CentroCosto, Precio, FaseActual, FechaCierre, Personal, Descripcion, TipoPago FROM [com].[V_TOportunidad_HistorialOportunidadAlumno] where IdContacto = @idAlumno AND Estado = 1 AND RowNumber = 1";
                var historialOportunidadDB = _dapper.QueryDapper(_query, new { idAlumno });
                historialOportunidad = JsonConvert.DeserializeObject<List<HistorialOportunidadDTO>>(historialOportunidadDB);
                return historialOportunidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las Fechas de las Actividades que no hubo contacto con el cliente
        /// </summary>
        /// <returns></returns>
        public List<OportunidadesNoEjecutadasDTO> ObtenerOportunidadesNoEjecutadas()
        {
            try
            {
                List<OportunidadesNoEjecutadasDTO> oportunidades = new List<OportunidadesNoEjecutadasDTO>();
                var registrosBO = _dapper.QuerySPDapper("com.SP_ObtenerOportunidadesNoEjecutadas", new { });
                if (!string.IsNullOrEmpty(registrosBO) && !registrosBO.Contains("[]"))
                {
                    oportunidades = JsonConvert.DeserializeObject<List<OportunidadesNoEjecutadasDTO>>(registrosBO);
                }
                return oportunidades;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene Informacion de usuario por numero de alumno
        /// </summary>
        /// <param name="Numero"></param>
        /// <returns></returns>
        public PersonalAlumnoDTO ObtenerOportunidadPorNumero(string Numero)
        {
            try
            {
                string _queryOportunidad = "Select IdPersonal,IdAlumno From [com].[V_TOportunidadPorNumero] Where EstadoAlumno=1 AND (Celular =@Numero) AND FaseOportunidad in('BNC','IT','IP','PF','IC','IS','M','ISM') AND IdPersonal!=125 Order By FechaCreacion Desc";
                var queryOportunidad = _dapper.FirstOrDefault(_queryOportunidad, new { Numero });
                var Oportunidad = JsonConvert.DeserializeObject<PersonalAlumnoDTO>(queryOportunidad);
                return Oportunidad;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// Obtiene el Centro de Costo por numero de Alumno y id de personal
        /// </summary>
        /// <param name="Numero"></param>
        /// <param name="IdPersonal"></param>
        /// <returns></returns>
        public int ObtenerCentroCostoPorCelularAlumno(string Numero, int IdPersonal)
        {
            try
            {
                string _queryOportunidad = "Select IdCentroCosto as Id From [com].[V_TOportunidadPorNumero] Where EstadoAlumno=1 AND (Celular =@Numero) AND IdPersonal=@IdPersonal AND FaseOportunidad in('BNC','IT','IP','PF','IC','IS','M') AND IdPersonal!=125 Order By FechaCreacion Desc";
                var queryOportunidad = _dapper.FirstOrDefault(_queryOportunidad, new { Numero, IdPersonal });
                if (queryOportunidad != "null" && queryOportunidad != "")
                {
                    var Oportunidad = JsonConvert.DeserializeObject<FiltroDTO>(queryOportunidad);
                    return Oportunidad.Id;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Obtiene el Centro de Costo por numero de Alumno y id de personal
        /// </summary>
        /// <param name="Numero"></param>
        /// <param name="IdPersonal"></param>
        /// <returns></returns>
        public OportunidadDatosChatWhatsAppDTO ObtenerOportunidadPorAsesoryAlumno(int IdAlumno, int IdPersonal, string Numero)
        {
            try
            {
                if (Numero.StartsWith("51"))
                {
                    Numero = Numero.Substring(2, 9);
                }
                if (IdAlumno != 0)
                {
                    OportunidadDatosChatWhatsAppDTO Oportunidad = new OportunidadDatosChatWhatsAppDTO();
                    string _queryOportunidad = "com.SP_OportunidadPorAlumnoyAsesor";
                    var queryOportunidad = _dapper.QuerySPFirstOrDefault(_queryOportunidad, new { IdAlumno, IdPersonal });
                    if (queryOportunidad != "null" && queryOportunidad != "")
                    {
                        Oportunidad = JsonConvert.DeserializeObject<OportunidadDatosChatWhatsAppDTO>(queryOportunidad);

                        return Oportunidad;
                    }
                    return null;
                }
                else
                {
                    OportunidadDatosChatWhatsAppDTO Oportunidad = new OportunidadDatosChatWhatsAppDTO();
                    string _queryOportunidad = "com.SP_OportunidadPorNumeroyAsesor";
                    var queryOportunidad = _dapper.QuerySPFirstOrDefault(_queryOportunidad, new { Numero, IdPersonal });
                    if (queryOportunidad != "null" && queryOportunidad != "")
                    {
                        Oportunidad = JsonConvert.DeserializeObject<OportunidadDatosChatWhatsAppDTO>(queryOportunidad);

                        return Oportunidad;
                    }
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// Obtiene el Centro de Costo por numero de Alumno 
        /// </summary>
        /// <param name="IdAlumno"></param>
        /// <param name="Numero"></param>
        /// <returns></returns>
        public OportunidadDatosChatWhatsAppDTO ObtenerOportunidadPorAlumno(int IdAlumno, string Numero)
        {
            try
            {
                if (Numero.StartsWith("51"))
                {
                    Numero = Numero.Substring(2, 9);
                }
                if (IdAlumno != 0)
                {
                    OportunidadDatosChatWhatsAppDTO Oportunidad = new OportunidadDatosChatWhatsAppDTO();
                    string _queryOportunidad = "com.SP_OportunidadPorAlumno";
                    var queryOportunidad = _dapper.QuerySPFirstOrDefault(_queryOportunidad, new { IdAlumno });
                    if (queryOportunidad != "null" && queryOportunidad != "")
                    {
                        Oportunidad = JsonConvert.DeserializeObject<OportunidadDatosChatWhatsAppDTO>(queryOportunidad);

                        return Oportunidad;
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// Obtiene oportunidades filtrado por numero de alumno
        /// </summary>
        /// <param name="Numero"></param>
        /// <param name="IdPgeneral"></param>
        /// <returns></returns>
        public List<ValidarOportunidadWhatsAppDTO> ValidarOportunidadesWhatsApp(string Numero, int IdPgeneral)
        {
            try
            {
                List<ValidarOportunidadWhatsAppDTO> opo = new List<ValidarOportunidadWhatsAppDTO>();
                string _queryOportunidad = "Select FaseOportunidad,IdPgeneral,IdPersonal From [com].[V_TOportunidadValidarPorNumero] Where EstadoAlumno=1 AND Celular =@Numero AND FaseOportunidad in('BNC','IT','IP','PF','IC','IS','M','RN2') AND EstadoPespecifico=1 Order By FechaCreacion Desc";
                var queryOportunidad = _dapper.QueryDapper(_queryOportunidad, new { Numero });
                var Oportunidad = JsonConvert.DeserializeObject<List<ValidarOportunidadWhatsAppDTO>>(queryOportunidad);

                return Oportunidad;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        /// <summary>
        /// Calcula el número de solicitudes de informacion por contacto,
        /// modifica el numero de solicitud por Area, SubArea, ProgramaGeneral y ProgramaEspecifico
        /// </summary>
        /// <param name="CalcularTodo"></param>
        public void CalcularSolicitudes(bool CalcularTodo, bool CalcularPorCentroCostoModificado, bool CalcularOportunidadesNuevasSinCalculo)
        {
            try
            {
                _dapper.QuerySPDapper("mkt.SP_CalcularNroSolicitudesPorContacto", new { CalcularTodo, CalcularPorCentroCostoModificado, CalcularOportunidadesNuevasSinCalculo });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public List<FechaSesionProximaDTO> ObtenerProximaSesionPorOportunidad(int idOportunidad)
        {
            try
            {
                List<FechaSesionProximaDTO> fechaSesionProximaDTOs = new List<FechaSesionProximaDTO>();
                var query = "SELECT Fecha FROM com.V_CronogramaFechaSesion WHERE Fecha > GETDATE() AND IdOportunidad = @idOportunidad ORDER BY Fecha ASC";
                var dapper = _dapper.QueryDapper(query, new { IdOportunidad = idOportunidad });

                if (!string.IsNullOrEmpty(dapper) && !dapper.Contains("[]"))
                {
                    fechaSesionProximaDTOs = JsonConvert.DeserializeObject<List<FechaSesionProximaDTO>>(dapper);
                }
                return fechaSesionProximaDTOs;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public List<int> ObtenerOportunidades()
        {
            try
            {
                string _queryOportunidad = $@"
                   SELECT t1.Id
            FROM com.T_Oportunidad AS t1
                 LEFT JOIN com.T_ModeloDataMining AS t2 ON t1.id = t2.idOportunidad
            WHERE t2.IdProbabilidadRegistroPW_Actual = 1
                  OR t2.IdProbabilidadRegistroPW_Actual IS NULL
                  AND CONVERT(DATE, t1.FechaCreacion) >= '2020-01-01'
                        ";
                var queryOportunidad = _dapper.QueryDapper(_queryOportunidad, new { });
                var Oportunidad = JsonConvert.DeserializeObject<List<IdDTO>>(queryOportunidad);
                return Oportunidad.Select(x => x.Id).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// Obtiene los comentarios de operaciones por tipo
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="idTipoSeguimientoAlumnoCategoria"></param>
        /// <returns></returns>
        public List<ObtenerSeguimientoAlunoComentarioDTO> ObtenerComentariosOperaciones(int idOportunidad, int idTipoSeguimientoAlumnoCategoria)
        {
            try
            {
                List<ObtenerSeguimientoAlunoComentarioDTO> Comentario = new List<ObtenerSeguimientoAlunoComentarioDTO>();
                string _queryOportunidad = $@"
                        SELECT Id, 
                               IdSeguimientoAlumnoCategoria, 
                               SeguimientoAlumnoCategoria, 
                               Comentario, 
                               FechaCompromiso, 
                               NroCuota, 
                               NroSubCuota,
                               FechaCreacion,
                               UsuarioCreacion
                        FROM ope.V_ObtenerComentarioOperaciones
                        WHERE Estado = 1
                              AND IdOportunidad = @idOportunidad
                              AND IdTipoSeguimientoAlumnoCategoria = @idTipoSeguimientoAlumnoCategoria
                        ORDER BY FechaCompromiso DESC
                        ";
                var queryOportunidad = _dapper.QueryDapper(_queryOportunidad, new { idOportunidad, idTipoSeguimientoAlumnoCategoria });
                if (!queryOportunidad.Contains("[]"))
                {
                    Comentario = JsonConvert.DeserializeObject<List<ObtenerSeguimientoAlunoComentarioDTO>>(queryOportunidad);
                }

                return Comentario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene el idPersonal por oportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public IdDTO ObtenerIdPersonalOperaciones(int idOportunidad)
        {
            try
            {
                List<IdDTO> listaId = new List<IdDTO>();
                string _query = $@"
                            SELECT Personal.Id AS Id
                            FROM fin.T_MatriculaCabecera AS MatriculaCabecera
                                 INNER JOIN com.T_MontoPagoCronograma AS MontoPagoCronograma ON MatriculaCabecera.IdCronograma = MontoPagoCronograma.Id
                                 INNER JOIN com.T_Oportunidad AS Oportunidad ON MontoPagoCronograma.IdOportunidad = Oportunidad.Id
                                 INNER JOIN conf.T_Integra_AspNetUsers AS AspNetUsers ON AspNetUsers.UserName = MatriculaCabecera.UsuarioCoordinadorAcademico
                                 INNER JOIN gp.T_Personal AS Personal ON Personal.Id = AspNetUsers.PerId
                            WHERE MatriculaCabecera.Estado = 1
                                  AND MontoPagoCronograma.Estado = 1
                                  AND Oportunidad.Estado = 1
                                  AND MatriculaCabecera.UsuarioCoordinadorAcademico <> '0'
                                  AND MontoPagoCronograma.IdOportunidad = @idOportunidad
                                  AND Personal.Activo = 1
                        ";
                var resultado = _dapper.QueryDapper(_query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaId = JsonConvert.DeserializeObject<List<IdDTO>>(resultado);
                }
                if (listaId.Count() < 1)
                {
                    throw new Exception("No existe id");
                }
                return listaId.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IdDTO ObtenerIdPersonalOperacionesv2(string CodigoAlumno)
        {
            try
            {
                List<IdDTO> listaId = new List<IdDTO>();
                string _query = $@"SELECT Personal.Id AS Id
								FROM bsadmin_prod.ope.AlumnosMatriculados AS MatriculaCabecera
								INNER JOIN conf.T_Integra_AspNetUsers AS AspNetUsers ON AspNetUsers.UserName = MatriculaCabecera.Usuario_Coordinador_Academico
								INNER JOIN gp.T_Personal AS Personal ON Personal.Id = AspNetUsers.PerId
								WHERE MatriculaCabecera.Usuario_Coordinador_Academico <> '0'
								AND MatriculaCabecera.CodigoAlumno = @CodigoAlumno
								AND Personal.Activo = 1
							";
                var resultado = _dapper.QueryDapper(_query, new { CodigoAlumno });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaId = JsonConvert.DeserializeObject<List<IdDTO>>(resultado);
                }
                if (listaId.Count() < 1)
                {
                    throw new Exception("No existe id");
                }
                return listaId.FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene el idPersonal por oportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public bool TienePersonalOperaciones(int idOportunidad)
        {
            try
            {
                List<IdDTO> listaId = new List<IdDTO>();
                string _query = $@"
                            SELECT Personal.Id AS Id
                            FROM fin.T_MatriculaCabecera AS MatriculaCabecera
                                 INNER JOIN com.T_MontoPagoCronograma AS MontoPagoCronograma ON MatriculaCabecera.IdCronograma = MontoPagoCronograma.Id
                                 INNER JOIN com.T_Oportunidad AS Oportunidad ON MontoPagoCronograma.IdOportunidad = Oportunidad.Id
                                 INNER JOIN conf.T_Integra_AspNetUsers AS AspNetUsers ON AspNetUsers.UserName = MatriculaCabecera.UsuarioCoordinadorAcademico
                                 INNER JOIN gp.T_Personal AS Personal ON Personal.Id = AspNetUsers.PerId
                            WHERE MatriculaCabecera.Estado = 1
                                  AND MontoPagoCronograma.Estado = 1
                                  AND Oportunidad.Estado = 1
                                  AND MatriculaCabecera.UsuarioCoordinadorAcademico <> '0'
                                  AND MontoPagoCronograma.IdOportunidad = @idOportunidad
                                  AND Personal.Activo = 1
                        ";
                var resultado = _dapper.QueryDapper(_query, new { idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaId = JsonConvert.DeserializeObject<List<IdDTO>>(resultado);
                }
                if (listaId.Count() == 0)
                {
                    return false;
                }
                else if (listaId.Count() >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //return listaId.FirstOrDefault();
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Obtiene los ids de las oportunidades de ventas en IS - M que aun no tienen su oportunidad en operaciones
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public List<IdDTOV2> ObtenerOportunidadesOperacionesSinCrear(int idOportunidad, string usuarioCoordinadorAcademico, string estados)
        {
            try
            {
                List<IdDTOV2> listaId = new List<IdDTOV2>();
                if (idOportunidad == 0)
                {
                    string _query = $@"Com.SP_GenerarOportunidadOperaciones_v2";
                    var resultado = _dapper.QuerySPDapper(_query, new { UsuarioCoodinadorAcademico = usuarioCoordinadorAcademico, Estados = estados });
                    if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    {
                        listaId = JsonConvert.DeserializeObject<List<IdDTOV2>>(resultado);
                    }
                }
                else
                {
                    string _query = $@"
                        ";
                    //Select Id, IdCentroCosto From Com.V_GenerarOportunidadOperaciones_v2 Where Id=@IdOportunidad;

                    var resultado = _dapper.QueryDapper(_query, new { IdOportunidad = idOportunidad });
                    if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                    {
                        listaId = JsonConvert.DeserializeObject<List<IdDTOV2>>(resultado);
                    }
                }


                return listaId;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ProgramaEspecificoOportunidadDTO ObtenerDatosOportunidad(int IdOportunidad)
        {
            try
            {
                string _query = "Select IdPespecifico,IdAlumno From com.V_PespecificoOportunidad Where Id= @IdOportunidad";
                string _queryoportunidad = _dapper.FirstOrDefault(_query, new { IdOportunidad });
                if (!_queryoportunidad.ToUpper().Contains("NULL"))
                {
                    return JsonConvert.DeserializeObject<ProgramaEspecificoOportunidadDTO>(_queryoportunidad);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Obtiene los ids de las oportunidades de ventas en IS - M que aun no tienen su oportunidad en operaciones- logica  2
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public List<IdDTOV3> ObtenerOportunidadesOperacionesSinCrear_logica2(string usuarioCoordinadorAcademico, string estados)
        {
            try
            {
                List<IdDTOV3> listaId = new List<IdDTOV3>();

                string _query = $@"Com.SP_GenerarOportunidadOperaciones_logica2";
                var resultado = _dapper.QuerySPDapper(_query, new { UsuarioCoodinadorAcademico = usuarioCoordinadorAcademico, Estados = estados });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaId = JsonConvert.DeserializeObject<List<IdDTOV3>>(resultado);
                }

                return listaId;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<IdOportunidadDTO> ObtenerOportunidadesOperacionesSinCrear_logica3(string usuarioCoordinadorAcademico, string estados)
        {
            try
            {
                List<IdOportunidadDTO> listaId = new List<IdOportunidadDTO>();

                string _query = $@"Com.sp_GenerarOportunidadOperaciones_logica3";
                var resultado = _dapper.QuerySPDapper(_query, new { UsuarioCoodinadorAcademico = usuarioCoordinadorAcademico, Estados = estados });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    listaId = JsonConvert.DeserializeObject<List<IdOportunidadDTO>>(resultado);
                }

                return listaId;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Obtiene por CodigoMatricula a que Tab Pertenece
        /// </summary>
        /// <param name="usuarioCoordinadorAcademico"></param>
        /// <param name="estados"></param>
        /// <returns></returns>
        public OportunidadTabAgenda ObtenerClasificacionTabAgenda(int IdPersonal, string TextoaBuscar, int Tipo)
        {
            try
            {
                OportunidadTabAgenda clasificacion = new OportunidadTabAgenda();
                if (Tipo == 1)
                {
                    string _query = $@"Ope.SP_ObtenerClasificacionTabAgenda";
                    var resultado = _dapper.QuerySPFirstOrDefault(_query, new { IdPersonal, TextoaBuscar });
                    if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                    {
                        clasificacion = JsonConvert.DeserializeObject<OportunidadTabAgenda>(resultado);
                    }

                }
                else
                {
                    string _query = $@"Ope.SP_ObtenerClasificacionTabAgendaporDNI";
                    var resultado = _dapper.QuerySPFirstOrDefault(_query, new { IdPersonal, TextoaBuscar });
                    if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                    {
                        clasificacion = JsonConvert.DeserializeObject<OportunidadTabAgenda>(resultado);
                    }
                }


                return clasificacion;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Obtiene el objeto oportunidadBO a partir de un string serializado
        /// </summary>
        /// <param name="oportunidadSerializada"></param>
        /// <returns></returns>
        public OportunidadBO ObtenerOportunidadOperaciones(string oportunidadSerializada)
        {
            try
            {
                return JsonConvert.DeserializeObject<OportunidadBO>(oportunidadSerializada);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Inserta la oportunidad generada en la tabla ope.T_OportunidadClasificacionOperaciones
        /// </summary>
        /// <param name="idOportunidad"></param>
        public void InsertarOportunidadClasificacionOperaciones(int idOportunidad)
        {
            try
            {
                var query = "ope.SP_CalcularOportunidadClasificacionOperaciones_PorIdOportunidad";
                var res = _dapper.QuerySPFirstOrDefault(query, new { idOportunidad = idOportunidad.ToString() });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtiene oportunidad de operaciones mediante los siguientes parametros
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public OportunidadOperacionesFiltroDTO ObtenerOportunidadOperacionesPorParametros(int idMatriculaCabecera)
        {
            try
            {
                var query = "SELECT Id, IdPersonal_Asignado, IdAlumno, IdCentroCosto, IdPadre, IdFaseOportunidad, IdPersonalAreaTrabajo, IdOportunidadClasificacionOperaciones FROM [com].[V_TOportunidad_ObtenerOportunidadOperaciones] WHERE IdMatriculaCabecera = @IdMatriculaCabecera";
                var res = _dapper.FirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                return JsonConvert.DeserializeObject<OportunidadOperacionesFiltroDTO>(res);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Elimina la oportunidad de operaciones en v3 y v4 fisicamente
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public bool EliminarOportunidadFisicaOperacionesV3V4(int idOportunidad)
        {
            try
            {
                var resultado = new Dictionary<string, bool>();

                string query = _dapper.QuerySPFirstOrDefault("ope.SP_EliminarOportunidadOperacionesFisicamenteNuevoWebPhoneNuevoModelo", new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(query))
                {
                    resultado = JsonConvert.DeserializeObject<Dictionary<string, bool>>(query);
                }
                return resultado.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        /// <summary>
		/// Obtiene Email de Personal y alumno por oportunidad
		/// </summary>
		/// <param name="idOportunidad"></param>
		/// <returns></returns>
		public EmailPersonalAlumnoDTO ObtenerEmailPorOportunidad(int idOportunidad)
        {
            try
            {
                EmailPersonalAlumnoDTO _emails = new EmailPersonalAlumnoDTO();
                var query = "SELECT EmailPersonal,EmailAlumno FROM [com].[V_TOportunidad_EmailPersonalAlumno] WHERE Id = @IdOportunidad";
                var res = _dapper.FirstOrDefault(query, new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(res) && !res.ToUpper().Contains("NULL"))
                {
                    _emails = JsonConvert.DeserializeObject<EmailPersonalAlumnoDTO>(res);
                }
                return _emails;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el cronograma de pago completo en html
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public string ObtenerCronogramaPagoCompleto(int id)
        {
            try
            {
                var _resultado = new List<CuotaCronogramaDetalleDTO>();
                var query = $@"com.SP_ObtenerMontoPagoCronogramaPagoCuotasCompleto";
                var resultado = _dapper.QuerySPDapper(query, new { IdOportunidad = id });

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<List<CuotaCronogramaDetalleDTO>>(resultado);
                }
                var _htmlFinal = "";

                if (_resultado.Count() > 0 && _resultado != null)//tabla
                {
                    var totalCuotas = _resultado.Max(x => x.NroCuota);
                    var ultimo = _resultado.Last();
                    _htmlFinal += $@"
                                    <table style='width: 390px;text-align: center;'>
                                        <tr>
                                            <th style='width:103px;height:28px;text-align: center;'> Nro. Cuota </th>
                                            <th style='width:140px;height:28px;text-align: center;'> Monto </th>
                                            <th style='width:193px;height:28px;text-align: center;'> Fecha de vencimiento </th>
                                        </tr>
                                        ";
                    foreach (var item in _resultado)
                    {
                        _htmlFinal += $@"
                                        <tr>
                                            <td style='width:103px;height:23px;text-align:center;'> { item.NroCuota }</td>
                                            <td style='width:140px;height:23px;text-align:center;'> { item.SimboloMoneda } { item.Cuota } </td>
                                            <td style='width:193px;height:23px;text-align:center;'> { item.FechaVencimiento.ToString("dd/MM/yyyy") } </td>
                                        </tr>
                                        ";
                    }
                    _htmlFinal += $@"</table>";
                }
                return _htmlFinal.Replace("dolares", "dólares");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el monto total del programa en el formato Simbolo +" " + MontoTotal +" "+ NombrePlural
        /// </summary>
        /// <param name="id"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public string ObtenerMontoTotal(int id)
        {
            try
            {
                var _resultado = new ResumenCronogramaDTO();
                var query = $@"com.SP_ObtenerMontoTotal";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdOportunidad = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    _resultado = JsonConvert.DeserializeObject<ResumenCronogramaDTO>(resultado);
                }
                return string.Concat(_resultado.SimboloMoneda, " ", _resultado.MontoTotal, " ", _resultado.NombrePluralMoneda);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene la version por matricula
        /// </summary>
        /// <param name="IdMatriculaCabecera"></param>
        /// <returns></returns>
        public string ObtenerVersion(int idOportunidad)
        {
            try
            {
                var _resultado = new ValorStringDTO();
                var query = $@"com.SP_ObtenerVersionAlumno";
                var resultado = _dapper.QuerySPFirstOrDefault(query, new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    _resultado = JsonConvert.DeserializeObject<ValorStringDTO>(resultado);
                }
                return _resultado.Valor;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
		/// Obtiene Oportunidad para certificado
		/// </summary>
		/// <param name="idPgeneral"></param>
		/// <returns></returns>
		public int ObtenerOportunidadPrograma(int idPgeneral)
        {
            try
            {
                ValorIntDTO _valor = new ValorIntDTO();
                var query = "SELECT IdOportunidad as Valor FROM ope.V_OportunidadesCertificado WHERE IdProgramaGeneral = @idPgeneral";
                var res = _dapper.FirstOrDefault(query, new { idPgeneral });
                if (!string.IsNullOrEmpty(res) && !res.ToUpper().Contains("NULL"))
                {
                    _valor = JsonConvert.DeserializeObject<ValorIntDTO>(res);
                    return _valor.Valor;
                }
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 01/03/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de Oportunidades de Operaciones
        /// </summary>
        /// <param name="Paginador">paginador</param>
        /// <param name="Filtro"> Filtro Modulo </param>
        /// <param name="FilterGrid"> filtros de grilla </param>        
        /// <returns>Lista</returns>
        public ResultadoFiltroAsignacionOportunidadDTO ObtenerPorFiltroPaginaManualOperaciones(Paginador Paginador, AsignacionManualOportunidadOperacionesFiltroDTO Filtro, GridFilters FilterGrid, List<string> listaCodigoMatricula)
        {
            try
            {
                if (FilterGrid != null)
                {

                    foreach (var item in FilterGrid.Filters)
                    {
                        if (item.Field == "Email" && item.Value.Contains(""))
                        {
                            Filtro.Email = item.Value.Trim();
                        }
                        else if (item.Field == "CodigoMatricula" && item.Value.Contains(""))
                        {
                            Filtro.CodigoMatricula = item.Value.Trim();
                        }

                    }
                }

                ResultadoFiltroAsignacionOportunidadDTO obj = new ResultadoFiltroAsignacionOportunidadDTO();

                if (Filtro.CodigoMatricula != "" && Filtro.CodigoMatricula != null)
                {
                    listaCodigoMatricula.Add(Filtro.CodigoMatricula);
                }


                var filtrosV2 = new
                {
                    ListaPersonal = Filtro.ListaPersonal == null ? "" : string.Join(",", Filtro.ListaPersonal.Select(x => x)),
                    ListaCentroCosto = Filtro.ListaCentroCosto == null ? "" : string.Join(",", Filtro.ListaCentroCosto.Select(x => x)),
                    ListaEstados = Filtro.ListaEstados == null ? "" : string.Join(",", Filtro.ListaEstados.Select(x => x)),
                    ListaSubEstados = Filtro.ListaSubestados == null ? "" : string.Join(",", Filtro.ListaSubestados.Select(x => x)),
                    Email = Filtro.Email,
                    //CodigoMatricula = Filtro.CodigoMatricula,
                    CodigoMatricula = listaCodigoMatricula == null ? "" : string.Join(",", listaCodigoMatricula.Select(x => x)),

                    Skip = Paginador.skip,
                    Take = Paginador.take,
                    Cantidad = false,

                };
                var filtros2V2 = new
                {
                    ListaPersonal = Filtro.ListaPersonal == null ? "" : string.Join(",", Filtro.ListaPersonal.Select(x => x)),
                    ListaCentroCosto = Filtro.ListaCentroCosto == null ? "" : string.Join(",", Filtro.ListaCentroCosto.Select(x => x)),
                    ListaEstados = Filtro.ListaEstados == null ? "" : string.Join(",", Filtro.ListaEstados.Select(x => x)),
                    ListaSubEstados = Filtro.ListaSubestados == null ? "" : string.Join(",", Filtro.ListaSubestados.Select(x => x)),
                    Email = Filtro.Email,
                    //CodigoMatricula = Filtro.CodigoMatricula,
                    CodigoMatricula = listaCodigoMatricula == null ? "" : string.Join(",", listaCodigoMatricula.Select(x => x)),
                    Skip = Paginador.skip,
                    Take = Paginador.take,
                    Cantidad = true,

                };

                string queryOportunidad = "com.SP_ObtenerOportunidadesOperaciones_AsignacionManual";
                var resQueryOportunidad = _dapper.QuerySPDapper(queryOportunidad, filtrosV2);
                var queryCantidad = _dapper.QuerySPFirstOrDefault(queryOportunidad, filtros2V2);
                var rpta = JsonConvert.DeserializeObject<List<AsignacionManualOportunidadOperacionesDTO>>(resQueryOportunidad);
                var total = JsonConvert.DeserializeObject<TotalOportunidadAsignacionManualOperacionesDTO>(queryCantidad);

                obj.Lista = rpta;
                obj.Total = total.Cantidad;
                return obj;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene las oportunidades recientes dependiendo del tiempo (5 minutos)
        /// </summary>
        /// <returns>Lista de DTO con las oportunidades recientes para Whatsapp</returns>
        public List<OportunidadWhatsappDTO> ObtenerOportunidadesRecientesWhatsapp()
        {
            try
            {
                var queryDapper = "SELECT Id, IdAlumno, Celular, IdPersonal FROM mkt.V_ObtenerUltimasOportunidades";

                var dataDB = _dapper.QueryDapper(queryDapper, null);
                var data = !string.IsNullOrEmpty(dataDB) && !dataDB.Contains("[]") ? JsonConvert.DeserializeObject<List<OportunidadWhatsappDTO>>(dataDB) : null;

                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las oportunidades recientes dependiendo del tiempo (3 dias)
        /// </summary>
        /// <returns>Lista de DTO con las oportunidades de los ultimos tres dias para Whatsapp</returns>
        public List<OportunidadWhatsappDTO> ObtenerOportunidadesUltimoRecordatorioWhatsapp()
        {
            try
            {
                var queryDapper = "SELECT Id, IdAlumno, Celular, IdPersonal FROM mkt.V_ObtenerOportunidadesUltimoRecordatorio";

                var dataDB = _dapper.QueryDapper(queryDapper, null);
                var data = !string.IsNullOrEmpty(dataDB) && !dataDB.Contains("[]") ? JsonConvert.DeserializeObject<List<OportunidadWhatsappDTO>>(dataDB) : null;

                return data;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        ///Repositorio: OportunidadRepositorio
        ///Autor: Edgar S.
        ///Fecha: 08/02/2021
        /// <summary>
        /// Obtener información por filtro de Oportunidades
        /// </summary>
        /// <param name="obj"> filtro de búsqueda </param>
        /// <param name="paginador"> paginador </param>
        /// <param name="filtroGrilla"> filtros de grilla </param>
        /// <param name="operadorComparacion"> Operadores de comparación </param>
        /// <returns> Lista de registros filtrados : ResultadoAsignacionManualFiltroTotalDTO </returns>
        public ResultadoAsignacionManualFiltroTotalDTO ObtenerPorFiltroPaginaManualV2(AsignacionManualOportunidadFiltroDTO obj, Paginador paginador, GridFilters filtroGrilla, List<OperadorComparacionDTO> operadorComparacion)
        {
            try
            {
                var total = 0;
                var filtros = new object();
                string queryCondicion = "";
                string contacto = "";
                string[] email1 = new string[6];
                int flagVentaCruzada = 0;
                int usuarioModificacion = 0;
                string[] idFaseoportunidad = new string[6];
                string[] idArea = new string[6];
                string[] idSubArea = new string[6];
                string[] idPersonal = new string[6];
                string[] idCategoriaOrigen = new string[6];
                string[] idCentroCosto = new string[6];
                string[] idProbabilidad = new string[6];
                string[] idTipoDato = new string[6];
                string[] idPais = new string[6];
                string[] idPrograma = new string[6];
                string[] idTipoCategoriaOrigen = new string[6];

                // Filtros Grilla 
                string condicion = string.Empty;
                string centroCosto = string.Empty;
                string asesor = string.Empty;
                string tipoDato = string.Empty;
                string nombreFase = string.Empty;
                string email = string.Empty;
                string categoria = string.Empty;
                string nombreCampania = string.Empty;
                string contacto1 = string.Empty;
                string estadoOportunidad = string.Empty;
                string probabilidadActual = string.Empty;
                string nombreGrupo = string.Empty;
                string areaCapacitacion = string.Empty;
                string subAreaCapacitacion = string.Empty;

                if (filtroGrilla != null)
                {

                    foreach (var item in filtroGrilla.Filters)
                    {
                        if (item.Field == "CentroCosto" && item.Value.Contains(""))
                        {
                            condicion += " AND CentroCosto LIKE @CentroCosto ";
                            centroCosto = item.Value;
                        }
                        else if (item.Field == "Asesor" && item.Value.Contains(""))
                        {
                            condicion += " AND Asesor LIKE @Asesor ";
                            asesor = item.Value;
                        }
                        else if (item.Field == "TipoDato" && item.Value.Contains(""))
                        {
                            condicion += " AND TipoDato LIKE @TipoDato ";
                            tipoDato = item.Value;
                        }
                        else if (item.Field == "NombreFase" && item.Value.Contains(""))
                        {
                            condicion += " AND NombreFase LIKE @NombreFase ";
                            nombreFase = item.Value;
                        }
                        else if (item.Field == "Contacto" && item.Value.Contains(""))
                        {
                            condicion = condicion + " and Contacto like @Contacto1 ";
                            contacto1 = item.Value;
                        }
                        else if (item.Field == "Email" && item.Value.Contains(""))
                        {
                            condicion += " AND IdAlumno LIKE @Email ";
                            email = item.Value;
                        }
                        else if (item.Field == "Categoria" && item.Value.Contains(""))
                        {
                            condicion += " AND Categoria LIKE @Categoria ";
                            categoria = item.Value;
                        }
                        else if (item.Field == "NombreCampania" && item.Value.Contains(""))
                        {
                            condicion += " AND NombreCampania LIKE @NombreCampania ";
                            nombreCampania = item.Value;
                        }
                        else if (item.Field == "EstadoOportunidad" && item.Value.Contains(""))
                        {
                            condicion += " AND EstadoOportunidad LIKE @EstadoOportunidad ";
                            estadoOportunidad = item.Value;
                        }
                        else if (item.Field == "ProbabilidadActual" && item.Value.Contains(""))
                        {
                            condicion += " AND ProbabilidadActual LIKE @ProbabilidadActual ";
                            probabilidadActual = item.Value;
                        }
                        else if (item.Field == "NombreGrupo" && item.Value.Contains(""))
                        {
                            condicion += " AND NombreGrupo LIKE @NombreGrupo ";
                            nombreGrupo = item.Value;
                        }
                        else if (item.Field == "AreaCapacitacion" && item.Value.Contains(""))
                        {
                            condicion += " AND AreaCapacitacion LIKE @AreaCapacitacion ";
                            areaCapacitacion = item.Value;
                        }
                        else if (item.Field == "SubAreaCapacitacion" && item.Value.Contains(""))
                        {
                            condicion += " AND SubAreaCapacitacion LIKE @SubAreaCapacitacion ";
                            subAreaCapacitacion = item.Value;
                        }
                    }
                }

                // Filtros Combos
                if (obj.Area != "")
                {
                    queryCondicion = queryCondicion + "AND IdArea IN @IdArea ";
                    idArea = obj.Area.Split(",");
                }
                if (obj.subArea != "")
                {
                    queryCondicion = queryCondicion + "AND IdSubArea IN @IdSubArea ";
                    idSubArea = obj.subArea.Split(",");
                }
                if (obj.FasesOportunidad != "")
                {
                    queryCondicion = queryCondicion + "AND IdFaseOportunidad IN @IdFaseOportunidad ";
                    idFaseoportunidad = obj.FasesOportunidad.Split(",");
                }
                if (obj.CentrosCosto != "")
                {
                    queryCondicion = queryCondicion + "AND IdCentroCosto IN @IdCentroCosto ";
                    idCentroCosto = obj.CentrosCosto.Split(",");
                }
                if (obj.Asesores != "")
                {
                    queryCondicion = queryCondicion + "AND IdPersonal IN @IdPersonal ";
                    idPersonal = obj.Asesores.Split(",");
                }
                if (obj.Probabilidad != "")
                {
                    queryCondicion = queryCondicion + "AND IdProbabilidad IN @IdProbabilidad ";
                    idProbabilidad = obj.Probabilidad.Split(",");
                }
                if (obj.Programa != "")
                {
                    queryCondicion = queryCondicion + "AND IdPrograma IN @IdPrograma ";
                    idPrograma = obj.Programa.Split(",");
                }
                if (obj.Categorias != "")
                {
                    queryCondicion = queryCondicion + "AND IdCategoriaOrigen IN @IdCategoriaOrigen ";
                    idCategoriaOrigen = obj.Categorias.Split(",");
                }
                if (obj.TiposDato != "")
                {
                    queryCondicion = queryCondicion + "AND IdTipoDato IN @IdTipoDato ";
                    idTipoDato = obj.TiposDato.Split(",");
                }
                if (obj.Pais != "")
                {
                    queryCondicion = queryCondicion + "AND IdPais IN @IdPais ";
                    idPais = obj.Pais.Split(",");
                }
                if (obj.TipoCategoriaOrigen != "")
                {
                    queryCondicion = queryCondicion + "AND IdTipoCategoriaOrigen IN @IdTipoCategoriaOrigen ";
                    idTipoCategoriaOrigen = obj.TipoCategoriaOrigen.Split(",");
                }
                if (obj.contacto != "")
                {
                    queryCondicion = queryCondicion + "And Contacto like CONCAT('%',@Contacto,'%') ";
                    contacto = obj.contacto;
                }
                if (obj.email != "")
                {
                    queryCondicion = queryCondicion + "AND IdAlumno IN @Email1 ";
                    email1 = obj.email.Split(",");
                }
                if (obj.UsuarioModificacion != "")
                {
                    queryCondicion = queryCondicion + "AND PersonalModificacion = @UsuarioModificacion ";
                    usuarioModificacion = Int32.Parse(obj.UsuarioModificacion);
                }
                if (obj.ventaCruzada != "")
                {
                    queryCondicion = queryCondicion + "AND FlagVentaCruzada = @FlagVentaCruzada ";
                    flagVentaCruzada = Int32.Parse(obj.ventaCruzada);
                }
                if (obj.FechaProgramacionInicio != null && obj.FechaProgramacionFin != null)
                {
                    queryCondicion = queryCondicion + "AND UltimaFechaProgramada BETWEEN @FechaProgramacionInicio AND @FechaProgramacionFin ";
                    obj.FechaProgramacionInicio = new DateTime(obj.FechaProgramacionInicio.Value.Year, obj.FechaProgramacionInicio.Value.Month, obj.FechaProgramacionInicio.Value.Day, 0, 0, 0);
                    obj.FechaProgramacionFin = new DateTime(obj.FechaProgramacionFin.Value.Year, obj.FechaProgramacionFin.Value.Month, obj.FechaProgramacionFin.Value.Day, 23, 59, 59);
                }

                // Filtros por cantidad de oportunidades
                if (obj.NroOportunidades != null && obj.IdOperadorComparacionNroOportunidades != null)
                {
                    var simbolo = operadorComparacion.Where(x => x.Id == obj.IdOperadorComparacionNroOportunidades.Value).FirstOrDefault().Simbolo;
                    queryCondicion += @" AND NroOportunidades " + simbolo + " " + obj.NroOportunidades;
                }
                if (obj.NroSolicitud != null && obj.IdOperadorComparacionNroSolicitud != null)
                {
                    var simbolo = operadorComparacion.Where(x => x.Id == obj.IdOperadorComparacionNroSolicitud.Value).FirstOrDefault().Simbolo;
                    queryCondicion += @" AND NroSolicitud " + simbolo + " " + obj.NroSolicitud;
                }
                if (obj.NroSolicitudPorArea != null && obj.IdOperadorComparacionNroSolicitudPorArea != null)
                {
                    var simbolo = operadorComparacion.Where(x => x.Id == obj.IdOperadorComparacionNroSolicitudPorArea.Value).FirstOrDefault().Simbolo;
                    queryCondicion += @" AND NroSolicitudPorArea " + simbolo + " " + obj.NroSolicitudPorArea;
                }
                if (obj.NroSolicitudPorSubArea != null && obj.IdOperadorComparacionNroSolicitudPorSubArea != null)
                {
                    var simbolo = operadorComparacion.Where(x => x.Id == obj.IdOperadorComparacionNroSolicitudPorSubArea.Value).FirstOrDefault().Simbolo;
                    queryCondicion += @" AND NroSolicitudPorSubArea " + simbolo + " " + obj.NroSolicitudPorSubArea;
                }
                if (obj.NroSolicitudPorProgramaGeneral != null && obj.IdOperadorComparacionNroSolicitudPorProgramaGeneral != null)
                {
                    var simbolo = operadorComparacion.Where(x => x.Id == obj.IdOperadorComparacionNroSolicitudPorProgramaGeneral.Value).FirstOrDefault().Simbolo;
                    queryCondicion += @" AND NroSolicitudPorProgramaGeneral " + simbolo + " " + obj.NroSolicitudPorProgramaGeneral;
                }
                if (obj.NroSolicitudPorProgramaEspecifico != null && obj.IdOperadorComparacionNroSolicitudPorProgramaEspecifico != null)
                {
                    var simbolo = operadorComparacion.Where(x => x.Id == obj.IdOperadorComparacionNroSolicitudPorProgramaEspecifico.Value).FirstOrDefault().Simbolo;
                    queryCondicion += @" AND NroSolicitudPorProgramaEspecifico " + simbolo + " " + obj.NroSolicitudPorProgramaEspecifico;
                }

                if (obj.FechaFin == null || obj.FechaInicio == null)
                {
                    obj.FechaFin = DateTime.Now;
                    obj.FechaInicio = DateTime.Now;
                }
                obj.FechaFin = obj.FechaFin.Value.AddDays(1);
                string queryCampos = "IdAlumno,Id,IdCentroCosto,CentroCosto,IdPersonal,Asesor,IdTipoDato,IdFaseOportunidad,IdOrigen,Contacto,Email,UsuarioModificacion,FechaRegistroCampania,Categoria,NombreGrupo,AreaCapacitacion,SubAreaCapacitacion,NombreCampania,FechaCreacion,FechaModificacion,UltimaFechaProgramada,IdEstadoOportunidad,EstadoOportunidad,ProbabilidadActual, NroOportunidades, NroSolicitud, NroSolicitudPorArea, NroSolicitudPorSubArea, NroSolicitudPorProgramaGeneral, NroSolicitudPorProgramaEspecifico, DiasSinContactoManhana, DiasSinContactoTarde, NombrePais";

                ResultadoAsignacionManualFiltroTotalDTO resultado = new ResultadoAsignacionManualFiltroTotalDTO();

                if (paginador != null && paginador.take != 0)
                {
                    string queryOportunidadConsulta = "SELECT " + queryCampos + " FROM com.V_TOportunidad_ObtenerPorFiltroPaginaNuevoModelo WHERE FechaCreacion BETWEEN @FechaInicio AND @FechaFin " + condicion + " " + queryCondicion + " ORDER BY FechaCreacion desc OFFSET  @Skip ROWS FETCH NEXT @Take ROWS ONLY";
                    var queryOportunidad = _dapper.QueryDapper(queryOportunidadConsulta, new { obj.FechaInicio, obj.FechaFin, obj.FechaProgramacionInicio, obj.FechaProgramacionFin, CentroCosto = "%" + centroCosto + "%", Asesor = "%" + asesor + "%", TipoDato = "%" + tipoDato + "%", NombreFase = "%" + nombreFase + "%", Contacto1 = "%" + contacto1 + "%", Email = "%" + email + "%", Categoria = "%" + categoria + "%", NombreCampania = "%" + nombreCampania + "%", EstadoOportunidad = "%" + estadoOportunidad + "%", ProbabilidadActual = "%" + probabilidadActual + "%", NombreGrupo = "%" + nombreGrupo + "%", AreaCapacitacion = "%" + areaCapacitacion + "%", SubAreaCapacitacion = "%" + subAreaCapacitacion + "%", idFaseoportunidad, idArea, idSubArea, idCentroCosto, idCategoriaOrigen, idPrograma, idProbabilidad, idPersonal, contacto, email1, usuarioModificacion, idTipoDato, idPais, idTipoCategoriaOrigen, flagVentaCruzada, Skip = paginador.skip, Take = paginador.take });
                    var rpta = JsonConvert.DeserializeObject<List<ResultadoAsignacionManualFiltroDTO>>(queryOportunidad);
                    var cantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapper.FirstOrDefault("Select Count(*) From com.V_TOportunidad_ObtenerPorFiltroPaginaNuevoModelo where FechaCreacion Between @FechaInicio AND @FechaFin " + condicion + " " + queryCondicion + "", new { obj.FechaInicio, obj.FechaFin, obj.FechaProgramacionInicio, obj.FechaProgramacionFin, CentroCosto = "%" + centroCosto + "%", Asesor = "%" + asesor + "%", TipoDato = "%" + tipoDato + "%", NombreFase = "%" + nombreFase + "%", Contacto1 = "%" + contacto1 + "%", Email = "%" + email + "%", Categoria = "%" + categoria + "%", NombreCampania = "%" + nombreCampania + "%", EstadoOportunidad = "%" + estadoOportunidad + "%", ProbabilidadActual = "%" + probabilidadActual + "%", NombreGrupo = "%" + nombreGrupo + "%", AreaCapacitacion = "%" + areaCapacitacion + "%", SubAreaCapacitacion = "%" + subAreaCapacitacion + "%", idFaseoportunidad, idArea, idSubArea, idCentroCosto, idCategoriaOrigen, idPrograma, idProbabilidad, idPersonal, contacto, email1, usuarioModificacion, idTipoDato, idPais, idTipoCategoriaOrigen, flagVentaCruzada }));

                    resultado.data = rpta;
                    resultado.Total = cantidadRegistros.Select(w => w.Value).FirstOrDefault();
                }
                else
                {
                    string queryOportunidadConsulta = "SELECT " + queryCampos + " from com.V_TOportunidad_ObtenerPorFiltroPaginaNuevoModelo WHERE FechaCreacion BETWEEN @FechaInicio AND @FechaFin " + queryCondicion + " ORDER BY FechaCreacion DESC";
                    var queryOportunidad = _dapper.QueryDapper(queryOportunidadConsulta, new { obj.FechaInicio, obj.FechaFin, obj.FechaProgramacionInicio, obj.FechaProgramacionFin, CentroCosto = "%" + centroCosto + "%", Asesor = "%" + asesor + "%", TipoDato = "%" + tipoDato + "%", NombreFase = "%" + nombreFase + "%", Contacto1 = "%" + contacto1 + "%", Email = "%" + email + "%", Categoria = "%" + categoria + "%", NombreCampania = "%" + nombreCampania + "%", EstadoOportunidad = "%" + estadoOportunidad + "%", ProbabilidadActual = "%" + probabilidadActual + "%", idFaseoportunidad, idArea, idSubArea, idCentroCosto, idCategoriaOrigen, idPrograma, idProbabilidad, idPersonal, contacto, email1, usuarioModificacion, idTipoDato, idPais, idTipoCategoriaOrigen, flagVentaCruzada });
                    var rpta = JsonConvert.DeserializeObject<List<ResultadoAsignacionManualFiltroDTO>>(queryOportunidad);
                    var cantidadRegistros = JsonConvert.DeserializeObject<Dictionary<string, int>>(_dapper.FirstOrDefault("Select Count (*) From com.V_TOportunidad_ObtenerPorFiltroPaginaNuevoModelo where FechaCreacion Between @FechaInicio AND @FechaFin " + queryCondicion + "", new { obj.FechaInicio, obj.FechaFin, obj.FechaProgramacionInicio, obj.FechaProgramacionFin, CentroCosto = "%" + centroCosto + "%", Asesor = "%" + asesor + "%", TipoDato = "%" + tipoDato + "%", NombreFase = "%" + nombreFase + "%", Contacto1 = "%" + contacto1 + "%", Email = "%" + email + "%", Categoria = "%" + categoria + "%", NombreCampania = "%" + nombreCampania + "%", EstadoOportunidad = "%" + estadoOportunidad + "%", ProbabilidadActual = "%" + probabilidadActual + "%", NombreGrupo = "%" + nombreGrupo + "%", AreaCapacitacion = "%" + areaCapacitacion + "%", SubAreaCapacitacion = "%" + subAreaCapacitacion + "%", idFaseoportunidad, idArea, idSubArea, idCentroCosto, idCategoriaOrigen, idPrograma, idProbabilidad, idPersonal, contacto, email1, usuarioModificacion, idTipoDato, idPais, idTipoCategoriaOrigen, flagVentaCruzada }));

                    resultado.data = rpta;
                    resultado.Total = cantidadRegistros.Select(w => w.Value).FirstOrDefault();
                }

                return resultado;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 01/11/2021
        /// Version: 1.0
        /// <summary>
        /// Actualiza el flag de la validacion de la oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad a actualizar el estado de la validacion (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="validacionCorrecta">Flag que determina si la validacion se completo exitosamente</param>
        /// <returns>Bool</returns>
        public bool ActualizarValidacionOportunidad(int idOportunidad, bool validacionCorrecta)
        {
            try
            {
                string spPeticion = "[com].[SP_ActualizarValidacionOportunidad]";
                string resultadoPeticion = _dapper.QuerySPFirstOrDefault(spPeticion, new { IdOportunidad = idOportunidad, ValidacionCorrecta = validacionCorrecta });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 01/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene las oportunidades sin probabilidad
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio para obtener las oportunidades sin probabilidad</param>
        /// <param name="fechaFin">Fecha de fin para obtener las oportunidades sin probabilidad</param>
        /// <returns>Lista de objetos de clase ValorIntDTO</returns>
        public List<ValorIntDTO> ObtenerListaIdOportunidadSinProbabilidad(DateTime fechaInicio, DateTime fechaFin)
        {
            List<ValorIntDTO> listaIdOportunidad = new List<ValorIntDTO>();

            string spPeticion = "[mkt].[SP_ObtenerListaOportunidadSinProbabilidad]";
            string resultadoPeticion = _dapper.QuerySPDapper(spPeticion, new { FechaInicio = fechaInicio, FechaFin = fechaFin });

            if (!string.IsNullOrEmpty(resultadoPeticion) && !resultadoPeticion.Contains("[]") && resultadoPeticion != "null")
            {
                listaIdOportunidad = JsonConvert.DeserializeObject<List<ValorIntDTO>>(resultadoPeticion);
            }                

            return listaIdOportunidad;
        }

        /// Autor: Gian Miranda
        /// Fecha: 01/11/2021
        /// Version: 1.0
        /// <summary>
        /// Actualiza el flag de la validacion de la oportunidad
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio actualizar correctamente el flag</param>
        /// <param name="fechaFin">Fecha de fin actualizar correctamente el flag</param>
        /// <returns>Bool</returns>
        public bool RegularizarAsignacionAutomaticaTemporalFlagErroneo(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                string spPeticion = "[mkt].[SP_RegularizarAsignacionAutomaticaTemporalFlagErroneo]";
                string resultadoPeticion = _dapper.QuerySPFirstOrDefault(spPeticion, new { FechaInicio = fechaInicio, FechaFin = fechaFin });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Gian Miranda
        /// Fecha: 01/11/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene las oportunidades que no llegaron a completar su validacion
        /// </summary>
        /// <param name="fechaInicio">Fecha de inicio actualizar correctamente el flag</param>
        /// <param name="fechaFin">Fecha de fin actualizar correctamente el flag</param>
        /// <returns>Bool</returns>
        public List<OportunidadAsignacionAutomaticaDTO> ObtenerOportunidadSinValidacionCompleta(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                List<OportunidadAsignacionAutomaticaDTO> resultadoOportunidadAsignacionAutomatica = new List<OportunidadAsignacionAutomaticaDTO>();

                string spPeticion = "[com].[SP_ObtenerOportunidadSinValidacionCompleta]";
                string resultadoPeticion = _dapper.QuerySPDapper(spPeticion, new { FechaInicio = fechaInicio, FechaFin = fechaFin });

                if (!string.IsNullOrEmpty(resultadoPeticion) && !resultadoPeticion.Contains("[]") && resultadoPeticion != "null")
                    resultadoOportunidadAsignacionAutomatica = JsonConvert.DeserializeObject<List<OportunidadAsignacionAutomaticaDTO>>(resultadoPeticion);

                return resultadoOportunidadAsignacionAutomatica;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
