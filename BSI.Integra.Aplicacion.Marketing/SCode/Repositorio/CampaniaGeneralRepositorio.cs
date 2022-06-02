using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.BO;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Marketing.Repositorio
{
    public class CampaniaGeneralRepositorio : BaseRepository<TCampaniaGeneral, CampaniaGeneralBO>
    {
        #region Metodos Base
        public CampaniaGeneralRepositorio() : base()
        {
        }
        public CampaniaGeneralRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<CampaniaGeneralBO> GetBy(Expression<Func<TCampaniaGeneral, bool>> filter)
        {
            IEnumerable<TCampaniaGeneral> listado = base.GetBy(filter);
            List<CampaniaGeneralBO> listadoBO = new List<CampaniaGeneralBO>();
            foreach (var itemEntidad in listado)
            {
                CampaniaGeneralBO objetoBO = Mapper.Map<TCampaniaGeneral, CampaniaGeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public CampaniaGeneralBO FirstById(int id)
        {
            try
            {
                TCampaniaGeneral entidad = base.FirstById(id);
                CampaniaGeneralBO objetoBO = new CampaniaGeneralBO();
                Mapper.Map<TCampaniaGeneral, CampaniaGeneralBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public CampaniaGeneralBO FirstBy(Expression<Func<TCampaniaGeneral, bool>> filter)
        {
            try
            {
                TCampaniaGeneral entidad = base.FirstBy(filter);
                CampaniaGeneralBO objetoBO = Mapper.Map<TCampaniaGeneral, CampaniaGeneralBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(CampaniaGeneralBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TCampaniaGeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<CampaniaGeneralBO> listadoBO)
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

        public bool Update(CampaniaGeneralBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TCampaniaGeneral entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<CampaniaGeneralBO> listadoBO)
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
        private void AsignacionId(TCampaniaGeneral entidad, CampaniaGeneralBO objetoBO)
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

        private TCampaniaGeneral MapeoEntidad(CampaniaGeneralBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TCampaniaGeneral entidad = new TCampaniaGeneral();
                entidad = Mapper.Map<CampaniaGeneralBO, TCampaniaGeneral>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.listaCampaniaGeneralDetalleBO != null && objetoBO.listaCampaniaGeneralDetalleBO.Count > 0)
                {

                    foreach (var hijo in objetoBO.listaCampaniaGeneralDetalleBO)
                    {
                        TCampaniaGeneralDetalle entidadHijo = new TCampaniaGeneralDetalle();
                        entidadHijo = Mapper.Map<CampaniaGeneralDetalleBO, TCampaniaGeneralDetalle>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TCampaniaGeneralDetalle.Add(entidadHijo);

                        //mapea al hijo interno
                        if (hijo.listaCampaniaGeneralDetalleProgramaBO != null && hijo.listaCampaniaGeneralDetalleProgramaBO.Count > 0)
                        {
                            foreach (var hijoPrograma in hijo.listaCampaniaGeneralDetalleProgramaBO)
                            {
                                TCampaniaGeneralDetallePrograma entidadHijoPrograma = new TCampaniaGeneralDetallePrograma();
                                entidadHijoPrograma = Mapper.Map<CampaniaGeneralDetalleProgramaBO, TCampaniaGeneralDetallePrograma>(hijoPrograma,
                                    opt => opt.ConfigureMap(MemberList.None));
                                entidadHijo.TCampaniaGeneralDetallePrograma.Add(entidadHijoPrograma);
                            }
                        }
                        if (hijo.AreaCampaniaGeneralDetalle != null && hijo.AreaCampaniaGeneralDetalle.Count > 0)
                        {
                            foreach (var subHijo in hijo.AreaCampaniaGeneralDetalle)
                            {
                                TCampaniaGeneralDetalleArea AreaCampaniaGeneralDetalle = new TCampaniaGeneralDetalleArea();
                                AreaCampaniaGeneralDetalle = Mapper.Map<CampaniaGeneralDetalleAreaBO, TCampaniaGeneralDetalleArea>(subHijo,
                                    opt => opt.ConfigureMap(MemberList.None));
                                entidadHijo.TCampaniaGeneralDetalleArea.Add(AreaCampaniaGeneralDetalle);
                            }
                        }
                        if (hijo.SubAreaCampaniaGeneralDetalle != null && hijo.SubAreaCampaniaGeneralDetalle.Count > 0)
                        {
                            foreach (var subHijo in hijo.SubAreaCampaniaGeneralDetalle)
                            {
                                TCampaniaGeneralDetalleSubArea AreaCampaniaGeneralDetalle = new TCampaniaGeneralDetalleSubArea();
                                AreaCampaniaGeneralDetalle = Mapper.Map<CampaniaGeneralDetalleSubAreaBO, TCampaniaGeneralDetalleSubArea>(subHijo,
                                    opt => opt.ConfigureMap(MemberList.None));
                                entidadHijo.TCampaniaGeneralDetalleSubArea.Add(AreaCampaniaGeneralDetalle);
                            }
                        }
                        if (hijo.ResponsableCampaniaGeneralDetalle != null && hijo.ResponsableCampaniaGeneralDetalle.Count > 0)
                        {
                            foreach (var subHijo in hijo.ResponsableCampaniaGeneralDetalle)
                            {
                                TCampaniaGeneralDetalleResponsable CampaniaGeneralDetalleResponsable = new TCampaniaGeneralDetalleResponsable();
                                CampaniaGeneralDetalleResponsable = Mapper.Map<CampaniaGeneralDetalleResponsableBO, TCampaniaGeneralDetalleResponsable>(subHijo,
                                    opt => opt.ConfigureMap(MemberList.None));
                                entidadHijo.TCampaniaGeneralDetalleResponsable.Add(CampaniaGeneralDetalleResponsable);
                            }
                        }
                    }
                }

                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<CampaniaGeneralBO> GetFiltered<KProperty>(IEnumerable<Expression<Func<TCampaniaGeneral, bool>>> filters, Expression<Func<TCampaniaGeneral, KProperty>> orderBy, bool ascending)
        {
            IEnumerable<TCampaniaGeneral> listado = base.GetFiltered(filters, orderBy, ascending);
            List<CampaniaGeneralBO> listadoBO = new List<CampaniaGeneralBO>();

            foreach (var itemEntidad in listado)
            {
                CampaniaGeneralBO objetoBO = Mapper.Map<TCampaniaGeneral, CampaniaGeneralBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }
            return listadoBO;
        }
        #endregion

        /// Autor: Carlos Crispin
        /// Fecha: 16/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene una lista con los registros de la tabla
        /// </summary>
        /// <returns>Lista de objetos de clase CampaniaGeneralDTO</returns>
        public List<CampaniaGeneralDTO> ObtenerListaCampaniaGeneral()
        {
            try
            {
                string query = @"SELECT Id, 
                                    Nombre, 
                                    IdCategoriaOrigen,
                                    FechaEnvio,
                                    IdNivelSegmentacion,
                                    NivelSegmentacion,
                                    IdHoraEnvio_Mailing,
                                    IdTipoAsociacion,
                                    IdProbabilidadRegistro_Nivel,
                                    NroMaximoSegmentos,
                                    CantidadPeriodoSinCorreo,
                                    IdTiempoFrecuencia,
                                    IdFiltroSegmento,
                                    IdPlantilla_Mailing,
                                    IdRemitenteMailing,
                                    IncluyeWhatsapp,
                                    FechaInicioEnvioWhatsapp,
                                    FechaFinEnvioWhatsapp,
                                    NumeroMinutosPrimerEnvio,
                                    IdHoraEnvio_Whatsapp,
                                    DiasSinWhatsapp,
                                    IdPlantilla_Whatsapp,
                                    FechaCreacion,
                                    FechaModificacion,
                                    UsuarioCreacion,
                                    UsuarioModificacion
                            FROM mkt.V_TCampaniaGeneral
                            WHERE Estado = 1; ";
                var respuestaQuery = _dapper.QueryDapper(query, null);
                List<CampaniaGeneralDTO> campaniaMailingGrid = JsonConvert.DeserializeObject<List<CampaniaGeneralDTO>>(respuestaQuery);
                return campaniaMailingGrid;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Carlos Crispin
        /// Fecha: 16/03/2021
        /// Versión: 1.0
        /// <summary>
        /// Obtiene una lista con los registros de la tabla para la gestion
        /// </summary>
        /// <returns>Lista de objetos de clase GestionCampaniaGeneralDTO</returns>
        public List<GestionCampaniaGeneralDTO> ObtenerListaCampaniaGeneralGestion()
        {
            try
            {
                string query = @"SELECT Id, 
                                    Nombre, 
                                    CantidadContactosMailing,
                                    FechaEnvio,
                                    HoraEnvio_Mailing,
                                    IncluirRebotes,
                                    CorreosEnviadosMailchimp,
                                    IdEstadoEnvio_Mailing,
                                    EstadoEnvio_Mailing,
                                    CantidadContactosWhatsapp,
                                    FechaInicioEnvioWhatsapp,
                                    FechaFinEnvioWhatsapp,
                                    HoraEnvio_Whatsapp,
                                    EstadoEnvio_Whatsapp,
                                    Enviados_Whatsapp,
                                    FechaCreacion,
                                    FechaModificacion,
                                    UsuarioCreacion,
                                    UsuarioModificacion
                            FROM mkt.V_TCampaniaGeneralGestion
                            WHERE Estado = 1;";
                var respuestaQuery = _dapper.QueryDapper(query, null);
                List<GestionCampaniaGeneralDTO> campaniaMailingGrid = JsonConvert.DeserializeObject<List<GestionCampaniaGeneralDTO>>(respuestaQuery);
                return campaniaMailingGrid;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Obtener por id
        /// </summary>
        /// <param name="id">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <returns>Objeto de clase CampaniaGeneralDTO</returns>
        public CampaniaGeneralDTO Obtener(int id)
        {
            try
            {
                var campaniaGeneral = new CampaniaGeneralDTO();
                var query = @"
                            SELECT Id, 
                                    Nombre, 
                                    IdCategoriaOrigen,
                                    FechaEnvio,
                                    IdNivelSegmentacion,
                                    NivelSegmentacion,
                                    IdHoraEnvio_Mailing,
                                    IdTipoAsociacion,
                                    IdProbabilidadRegistro_Nivel,
                                    NroMaximoSegmentos,
                                    CantidadPeriodoSinCorreo,
                                    IdTiempoFrecuencia,
                                    IdFiltroSegmento,
                                    IdPlantilla_Mailing,
                                    IdRemitenteMailing,
                                    IncluyeWhatsapp,
                                    FechaInicioEnvioWhatsapp,
                                    FechaFinEnvioWhatsapp,
                                    NumeroMinutosPrimerEnvio,
                                    IdHoraEnvio_Whatsapp,
                                    DiasSinWhatsapp,
                                    IdPlantilla_Whatsapp,
                                    FechaCreacion,
                                    FechaModificacion,
                                    UsuarioCreacion,
                                    UsuarioModificacion
                            FROM mkt.V_TCampaniaGeneral
                            WHERE Estado = 1
                            AND Id=@id;";
                var respuestaDB = _dapper.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(respuestaDB) && respuestaDB != "null")
                {
                    campaniaGeneral = JsonConvert.DeserializeObject<CampaniaGeneralDTO>(respuestaDB);
                }
                return campaniaGeneral;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene los registros de CampaniaGeneralDetalle con sus respectivos programas, filtrado por el IdCampaniaMailing
        /// </summary>
        /// <param name="idCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <returns>Lista de objetos de clase CampaniaGeneralDetalleEstadoEnEjecucionDTO</returns>
        public List<CampaniaGeneralDetalleEstadoEnEjecucionDTO> ObtenerEstadoEjecucionCampaniaGeneralDetalle(int idCampaniaGeneral)
        {
            try
            {
                var resultado = new List<CampaniaGeneralDetalleEstadoEnEjecucionDTO>();

                string query = @"SELECT IdCampaniaGeneral,
                                        IdCampaniaGeneralDetalle,
                                        EnEjecucion
                                FROM [mkt].[V_ObtenerEstadoEjecucionCampaniaGeneralDetalle]
                                WHERE IdCampaniaGeneral = @idCampaniaGeneral";

                var respuestaDB = _dapper.QueryDapper(query, new { idCampaniaGeneral });

                if (!string.IsNullOrEmpty(respuestaDB) && !respuestaDB.Contains("[]"))
                {
                    resultado = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleEstadoEnEjecucionDTO>>(respuestaDB);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Devuelve la lista de estados de recuperacion segun el modulo
        /// </summary>
        /// <param name="idModuloSistema">Id del modulo a considerar(PK de la tabla conf.T_ModuloSistema)</param>
        /// <returns>Lista de objetos de clase WhatsAppEstadoRecuperacionDTO</returns>
        public List<WhatsAppEstadoRecuperacionDTO> ObtenerEstadoRecuperacionWhatsApp(int idModuloSistema)
        {
            try
            {
                var listaEstadoWhatsApp = new List<WhatsAppEstadoRecuperacionDTO>();

                string query = "SELECT * FROM mkt.V_TRecuperacionAutomaticoModuloSistemaCampaniaGeneral WHERE IdModuloSistema = @IdModuloSistema";

                var respuestaQuery = _dapper.QueryDapper(query, new { IdModuloSistema = idModuloSistema });

                if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Contains("[]") && respuestaQuery != "null")
                    listaEstadoWhatsApp = JsonConvert.DeserializeObject<List<WhatsAppEstadoRecuperacionDTO>>(respuestaQuery);

                return listaEstadoWhatsApp;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene la campania general por el id
        /// </summary>
        /// <param name="idCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <returns>Objeto de clase CampaniaGeneralBO</returns>
        public CampaniaGeneralBO ObtenerCampaniaGeneral(int idCampaniaGeneral)
        {
            try
            {
                CampaniaGeneralBO campaniaGeneralObtenida = null;

                string query = "SELECT * FROM mkt.V_TCampaniaGeneral_Completo WHERE Id = @idCampaniaGeneral";

                var respuestaQuery = _dapper.FirstOrDefault(query, new { idCampaniaGeneral });

                if (!string.IsNullOrEmpty(respuestaQuery) && !respuestaQuery.Contains("[]"))
                    campaniaGeneralObtenida = JsonConvert.DeserializeObject<CampaniaGeneralBO>(respuestaQuery);

                return campaniaGeneralObtenida;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene los registros de CampaniaGeneralDetalle con sus respectivos programas, filtrado por el IdCampaniaMailing
        /// </summary>
        /// <param name="idCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <returns>Lista de objetos de clase PrioridadesCampaniaGeneralDetalleDTO</returns>
        public List<PrioridadesCampaniaGeneralDetalleDTO> ObtenerListaCampaniaGeneralDetalleConProgramas(int idCampaniaGeneral)
        {
            try
            {
                string query = @"
                  SELECT Id, 
                       Nombre,
                       Prioridad, 
                       Asunto,
                       IdPersonal, 
                       IdCentroCosto,
                       IdConjuntoAnuncio,
                       CantidadContactosMailing,
                       CantidadContactosWhatsapp, 
                       EnEjecucion,
                       NoIncluyeWhatsaap,
                       RowVersion, 
                       IdCampaniaGeneralDetallePrograma, 
                       IdPGeneral, 
                       NombreProgramaGeneral, 
                       IdArea, 
                       IdSubArea, 
                       CantidadSubidosMailChimp,
                        IdCampaniaGeneralDetalleResponsable,
                        IdResponsable,
                        Dia1,
                        Dia2,
                        Dia3,
                        Dia4,
                        Dia5,
                        Total
                FROM mkt.V_ObtenerCampaniaGeneralDetalle
                WHERE EstadoCampaniaGeneralDetalle = 1
                      AND IdCampaniaGeneral = @idCampaniaGeneral
                ORDER BY Id;
                ";
                var respuestaQuery = _dapper.QueryDapper(query, new { idCampaniaGeneral });
                IEnumerable<CampaniaGeneralDetalleConProgramasDTO> listaCampaniaGeneralDetalle = JsonConvert.DeserializeObject<List<CampaniaGeneralDetalleConProgramasDTO>>(respuestaQuery);

                var listaPrioridades = (from p in listaCampaniaGeneralDetalle
                                        group p by new
                                        {
                                            p.Id,
                                            p.Nombre,
                                            p.Prioridad,
                                            p.Asunto,
                                            p.IdPersonal,
                                            p.IdCentroCosto,
                                            p.IdConjuntoAnuncio,
                                            p.CantidadContactosMailing,
                                            p.CantidadContactosWhatsapp,
                                            p.CantidadSubidosMailChimp,
                                            p.EnEjecucion,
                                            p.NoIncluyeWhatsaap,

                                        } into g
                                        select new PrioridadesCampaniaGeneralDetalleDTO
                                        {
                                            Id = g.Key.Id,
                                            Nombre = g.Key.Nombre,
                                            Prioridad = g.Key.Prioridad,
                                            Asunto = g.Key.Asunto,
                                            IdPersonal = g.Key.IdPersonal,
                                            IdCentroCosto = g.Key.IdCentroCosto,
                                            IdConjuntoAnuncio = g.Key.IdConjuntoAnuncio,
                                            CantidadContactosMailing = g.Key.CantidadContactosMailing,
                                            CantidadContactosWhatsapp = g.Key.CantidadContactosWhatsapp,
                                            EnEjecucion = g.Key.EnEjecucion,
                                            NoIncluyeWhatsaap = g.Key.NoIncluyeWhatsaap,
                                            CantidadSubidosMailChimp = g.Key.CantidadSubidosMailChimp,
                                            ProgramasFiltro = g.Select(o => new CampaniaGeneralDetalleProgramaDTO
                                            {
                                                Id = o.IdCampaniaGeneralDetallePrograma,
                                                IdPgeneral = o.IdPGeneral,
                                                NombreProgramaGeneral = o.NombreProgramaGeneral
                                            }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),
                                            Areas = g.Where(x => x.IdArea != null).Select(o => o.IdArea.Value).GroupBy(i => i).Select(i => i.First()).ToList(),
                                            SubAreas = g.Where(x => x.IdSubArea != null).Select(o => o.IdSubArea.Value).GroupBy(i => i).Select(i => i.First()).ToList(),
                                            Responsables = g.Select(o => new CampaniaGeneralDetalleResponsableDTO
                                            {
                                                Id = o.IdCampaniaGeneralDetalleResponsable,
                                                IdResponsable = o.IdResponsable,
                                                Dia1 = o.Dia1,
                                                Dia2 = o.Dia2,
                                                Dia3 = o.Dia3,
                                                Dia4 = o.Dia4,
                                                Dia5 = o.Dia5,
                                                Total = o.Total
                                            }).GroupBy(i => i.Id).Select(i => i.First()).ToList().Where(i => i.Id != null).ToList(),

                                        }).ToList();

                return listaPrioridades;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los registros de CampaniaGeneralDetalleResponsables con sus respectivos dias, filtrado por el IdCampaniaMailingDetalle
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania general detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Lista de objetos de clase ResponsableDTO</returns>
        public List<ResponsablesDTO> ObtenerListaCampaniaGeneralDetalleResponsables(int idCampaniaGeneralDetalle)
        {
            try
            {
                var Responsables = new List<ResponsablesDTO>();
                var query = @"
                            SELECT Id, 
                                    IdPersonal,
                                    Dia1,
                                    Dia2,
                                    Dia3,
                                    Dia4,
                                    Dia5,
                                    Total
                            FROM mkt.T_CampaniaGeneralDetalleResponsable
                            WHERE Estado = 1
                            AND IdCampaniaGeneralDetalle=@idCampaniaGeneralDetalle;";

                var respuestaDB = _dapper.QueryDapper(query, new { idCampaniaGeneralDetalle });
                Responsables = JsonConvert.DeserializeObject<List<ResponsablesDTO>>(respuestaDB);

                return Responsables;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool CrearUrlFormularioPrioridad(int idCampaniaGeneralDetalle, string usuarioResponsable)
        {
            try
            {
                string spReporte = "[mkt].[SP_GenerarUrlFormularioPublicidadMailing]";
                string resultadoReporte = _dapper.QuerySPDapper(spReporte, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle, UsuarioResponsable = usuarioResponsable });

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ObtenerUrlFormularioPrioridad(int idCampaniaGeneralDetalle)
        {
            try
            {
                ValorStringDTO resultadoUrl = null;

                string spReporte = "[mkt].[SP_ObtenerUrlFormularioPublicidadMailing]";
                string resultadoReporte = _dapper.QuerySPFirstOrDefault(spReporte, new { IdCampaniaGeneralDetalle = idCampaniaGeneralDetalle });

                if (!string.IsNullOrEmpty(resultadoReporte) && !resultadoReporte.Contains("[]") && resultadoReporte != "null")
                {
                    resultadoUrl = JsonConvert.DeserializeObject<ValorStringDTO>(resultadoReporte);
                }

                return resultadoUrl != null ? resultadoUrl.Valor : string.Empty;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene Las Campanias generales a ejecutar
        /// </summary>
        /// <returns>Lista de objetos de clase ActividadCampaniaGeneralParaEjecutarDTO</returns>
        public List<ActividadCampaniaGeneralWhatsappParaEjecutarDTO> ObtenerActividadCampaniaGeneralParaEjecutar()
        {
            try
            {
                #region Captura del tiempo
                var horaActual = DateTime.Now;

                string fechaEnvio = horaActual.ToString("dd/MM/yyyy");
                string minutoActual = string.Empty;

                minutoActual = horaActual.Minute.ToString().Length == 1 ? minutoActual = "0" + horaActual.Minute : minutoActual = horaActual.Minute.ToString();
                string horaEnvio = horaActual.Hour + ":" + minutoActual + ":00";
                #endregion

                var listaCampaniaGeneralWhatsapp = new List<ActividadCampaniaGeneralWhatsappParaEjecutarDTO>();
                string query = "com.SP_CampaniaGeneral_ParaEjecutarWhatsapp";
                var resultadoListaWhatsApp = _dapper.QuerySPDapper(query, new { fechaEnvio, horaEnvio });
                if (!string.IsNullOrEmpty(resultadoListaWhatsApp) && !resultadoListaWhatsApp.Contains("[]"))
                    listaCampaniaGeneralWhatsapp = JsonConvert.DeserializeObject<List<ActividadCampaniaGeneralWhatsappParaEjecutarDTO>>(resultadoListaWhatsApp);

                return listaCampaniaGeneralWhatsapp;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Elimina los correos que han sido rebotados y de la lista de WhatsApp
        /// </summary>
        /// <param name="idCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        /// <returns>Lista de objetos de clase CampaniaGeneralDetalleBO</returns>
        public void EliminarRebotesPorIdCampaniaGeneral(int idCampaniaGeneral)
        {
            try
            {
                var spEliminarRebote = "[mkt].[SP_EliminarRebotesPorIdCampaniaGeneral]";
                var resultadoSp = _dapper.QuerySPFirstOrDefault(spEliminarRebote, new { IdCampaniaGeneral = idCampaniaGeneral });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Actualiza el estado de WhatsApp
        /// </summary>
        /// <param name="idCampaniaGeneral">Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)</param>
        public void ActualizarEstadoEnvioWhatsApp(int idCampaniaGeneral)
        {
            try
            {
                var spActualizarEstadoWhatsApp = "[mkt].[SP_ActualizarEstadoEnvioWhatsApp]";
                var resultadoSp = _dapper.QuerySPFirstOrDefault(spActualizarEstadoWhatsApp, new { IdCampaniaGeneral = idCampaniaGeneral });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
