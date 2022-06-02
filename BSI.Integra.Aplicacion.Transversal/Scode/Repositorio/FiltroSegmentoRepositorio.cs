using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.Repository;
using BSI.Integra.Aplicacion.DTOs;
using Newtonsoft.Json;
using System.Linq;

namespace BSI.Integra.Aplicacion.Transversal.Repositorio
{
    /// Repositorio: Marketing/FiltroSegmento
    /// Autor: Fischer Valdez - Jose Villena - Wilber Choque - Ansoli Espinoza - Priscila Pacsi - Johan Cayo - Richard Zenteno - Gian Miranda
    /// Fecha: 09/02/2021
    /// <summary>
    /// Repositorio para consultas de mkt.T_FiltroSegmento
    /// </summary>
    public class FiltroSegmentoRepositorio : BaseRepository<TFiltroSegmento, FiltroSegmentoBO>
    {
        #region Metodos Base
        public FiltroSegmentoRepositorio() : base()
        {
        }
        public FiltroSegmentoRepositorio(integraDBContext contexto) : base(contexto)
        {
        }
        public IEnumerable<FiltroSegmentoBO> GetBy(Expression<Func<TFiltroSegmento, bool>> filter)
        {
            IEnumerable<TFiltroSegmento> listado = base.GetBy(filter);
            List<FiltroSegmentoBO> listadoBO = new List<FiltroSegmentoBO>();
            foreach (var itemEntidad in listado)
            {
                FiltroSegmentoBO objetoBO = Mapper.Map<TFiltroSegmento, FiltroSegmentoBO>(itemEntidad, opt => opt.ConfigureMap(MemberList.None));
                listadoBO.Add(objetoBO);
            }

            return listadoBO;
        }
        public FiltroSegmentoBO FirstById(int id)
        {
            try
            {
                TFiltroSegmento entidad = base.FirstById(id);
                FiltroSegmentoBO objetoBO = new FiltroSegmentoBO();
                Mapper.Map<TFiltroSegmento, FiltroSegmentoBO>(entidad, objetoBO, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public FiltroSegmentoBO FirstBy(Expression<Func<TFiltroSegmento, bool>> filter)
        {
            try
            {
                TFiltroSegmento entidad = base.FirstBy(filter);
                FiltroSegmentoBO objetoBO = Mapper.Map<TFiltroSegmento, FiltroSegmentoBO>(entidad, opt => opt.ConfigureMap(MemberList.None));

                return objetoBO;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Insert(FiltroSegmentoBO objetoBO)
        {
            try
            {
                //mapeo de la entidad
                TFiltroSegmento entidad = MapeoEntidad(objetoBO);

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

        public bool Insert(IEnumerable<FiltroSegmentoBO> listadoBO)
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

        public bool Update(FiltroSegmentoBO objetoBO)
        {
            try
            {
                if (objetoBO == null)
                {
                    throw new ArgumentNullException("Entidad nula");
                }

                //mapeo de la entidad
                TFiltroSegmento entidad = MapeoEntidad(objetoBO);

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

        public bool Update(IEnumerable<FiltroSegmentoBO> listadoBO)
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
        private void AsignacionId(TFiltroSegmento entidad, FiltroSegmentoBO objetoBO)
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

        private TFiltroSegmento MapeoEntidad(FiltroSegmentoBO objetoBO)
        {
            try
            {
                //crea la entidad padre
                TFiltroSegmento entidad = new TFiltroSegmento();
                entidad = Mapper.Map<FiltroSegmentoBO, TFiltroSegmento>(objetoBO,
                    opt => opt.ConfigureMap(MemberList.None));

                //mapea los hijos
                if (objetoBO.ListaFiltroSegmentoValorTipo != null && objetoBO.ListaFiltroSegmentoValorTipo.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaFiltroSegmentoValorTipo)
                    {
                        TFiltroSegmentoValorTipo entidadHijo = new TFiltroSegmentoValorTipo();
                        entidadHijo = Mapper.Map<FiltroSegmentoValorTipoBO, TFiltroSegmentoValorTipo>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TFiltroSegmentoValorTipo.Add(entidadHijo);
                    }
                }
                if (objetoBO.ListaFiltroSegmentoDetalle != null && objetoBO.ListaFiltroSegmentoDetalle.Count > 0)
                {
                    foreach (var hijo in objetoBO.ListaFiltroSegmentoDetalle)
                    {
                        TFiltroSegmentoDetalle entidadHijo = new TFiltroSegmentoDetalle();
                        entidadHijo = Mapper.Map<FiltroSegmentoDetalleBO, TFiltroSegmentoDetalle>(hijo,
                            opt => opt.ConfigureMap(MemberList.None));
                        entidad.TFiltroSegmentoDetalle.Add(entidadHijo);
                    }
                }
                return entidad;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion

        /// <summary>
        /// Obtiene filtro segmento para autocomplete
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerAutoComplete(string valor)
        {
            try
            {
                return this.GetBy(x => x.Nombre.Contains(valor), x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList(); ;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el Id y Nombre para ComboBox
        /// </summary>
        /// <returns>Lista de objetos de clase FiltroIdNombreDTO</returns>
        public List<FiltroIdNombreDTO> GetFiltroIdNombre()
        {
            return this.GetBy(x => true, x => new FiltroIdNombreDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
        }
        /// <summary>
        /// Obtiene todos los filtro Segmento (Activos) registrado en el sistema
        /// </summary>
        /// <returns></returns>
        public List<FiltroSegmentoPanelDTO> ObtenerFiltroSegmentoPanel()
        {
            try
            {
                List<FiltroSegmentoPanelDTO> items = new List<FiltroSegmentoPanelDTO>();
                var _query = "SELECT Id, Nombre, Descripcion, IdFiltroSegmentoTipoContacto, NombreFiltroSegmentoTipoContacto, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion, FiltroEjecutado FROM mkt.V_TFiltroSegmento_PanelDataBasica WHERE Estado = 1;";
                var respuestaDapper = _dapper.QueryDapper(_query, new { });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    items = JsonConvert.DeserializeObject<List<FiltroSegmentoPanelDTO>>(respuestaDapper);
                }
                return items;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene todos los filtro Segmento (Activos) registrado en el sistema
        /// </summary>
        /// <returns></returns>
        public FiltroSegmentoDTO ObtenerFiltroSegmentoDatosPorId(int id)
        {
            try
            {
                FiltroSegmentoDTO item = new FiltroSegmentoDTO();
                var _query =
                    @"
                    SELECT 
                       Id, 
                       Nombre,
                       Descripcion,
                       IdFiltroSegmentoTipoContacto, 
                       IdOperadorComparacionNroSolicitudInformacion, 
                       NroSolicitudInformacion, 
                       IdOperadorComparacionNroOportunidades, 
                       NroOportunidades, 
                       FechaInicioCreacionUltimaOportunidad, 
                       FechaFinCreacionUltimaOportunidad, 
                       FechaInicioModificacionUltimaActividadDetalle, 
                       FechaFinModificacionUltimaActividadDetalle, 
                       EsRn2, 
                       FechaInicioProgramacionUltimaActividadDetalleRn2, 
                       FechaFinProgramacionUltimaActividadDetalleRn2, 
                       FechaInicioFormulario, 
                       FechaFinFormulario, 
                       FechaInicioChatIntegra, 
                       FechaFinChatIntegra, 
                       IdOperadorComparacionTiempoMaximoRespuestaChatOnline, 
                       TiempoMaximoRespuestaChatOnline, 
                       IdOperadorComparacionNroPalabrasClienteChatOnline, 
                       NroPalabrasClienteChatOnline, 
                       IdOperadorComparacionTiempoPromedioRespuestaChatOnline, 
                       TiempoPromedioRespuestaChatOnline, 
                       IdOperadorComparacionNroPalabrasClienteChatOffline, 
                       NroPalabrasClienteChatOffline, 
                       FechaInicioCorreo, 
                       FechaFinCorreo, 
                       IdOperadorComparacionNroCorreosAbiertos, 
                       NroCorreosAbiertos, 
                       IdOperadorComparacionNroCorreosNoAbiertos, 
                       NroCorreosNoAbiertos, 
                       IdOperadorComparacionNroClicksEnlace, 
                       NroClicksEnlace, 
                       IdOperadorComparacionNroCorreosAbiertosMailchimp, 
                       NroCorreosAbiertosMailchimp, 
                       IdOperadorComparacionNroCorreosNoAbiertosMailchimp, 
                       NroCorreosNoAbiertosMailchimp, 
                       IdOperadorComparacionNroClicksEnlaceMailchimp, 
                       NroClicksEnlaceMailchimp, 
                       EsSuscribirme, 
                       EsDesuscribirme, 
                       ConsiderarFiltroGeneral, 
                       ConsiderarFiltroEspecifico, 
                       TieneVentaCruzada, 
                       IdOperadorComparacionNroTotalLineaCreditoVigente, 
                       NroTotalLineaCreditoVigente, 
                       IdOperadorComparacionMontoTotalLineaCreditoVigente, 
                       MontoTotalLineaCreditoVigente, 
                       IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente, 
                       MontoMaximoOtorgadoLineaCreditoVigente, 
                       IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente, 
                       MontoMinimoOtorgadoLineaCreditoVigente, 
                       IdOperadorComparacionNroTotalLineaCreditoVigenteVencida, 
                       NroTotalLineaCreditoVigenteVencida, 
                       IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida, 
                       MontoTotalLineaCreditoVigenteVencida, 
                       IdOperadorComparacion_NroTcOtorgada, 
                       NroTcOtorgada, 
                       IdOperadorComparacionMontoTotalOtorgadoEnTcs, 
                       MontoTotalOtorgadoEnTcs, 
                       IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc, 
                       MontoMaximoOtorgadoEnUnaTc, 
                       IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc, 
                       MontoMinimoOtorgadoEnUnaTc, 
                       IdOperadorComparacionMontoDisponibleTotalEnTcs, 
                       MontoDisponibleTotalEnTcs, 
                       FechaInicioLlamada, 
                       FechaFinLlamada, 
                       IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad, 
                       DuracionPromedioLlamadaPorOportunidad, 
                       IdOperadorComparacionDuracionTotalLlamadaPorOportunidad, 
                       DuracionTotalLlamadaPorOportunidad, 
                       IdOperadorComparacionNroLlamada, 
                       NroLlamada, 
                       IdOperadorComparacionDuracionLlamada, 
                       DuracionLlamada, 
                       IdOperadorComparacionTasaEjecucionLlamada, 
                       TasaEjecucionLlamada, 
                       FechaInicioInteraccionSitioWeb, 
                       FechaFinInteraccionSitioWeb, 
                       IdOperadorComparacionTiempoVisualizacionTotalSitioWeb, 
                       TiempoVisualizacionTotalSitioWeb, 
                       IdOperadorComparacionNroClickEnlaceTodoSitioWeb, 
                       NroClickEnlaceTodoSitioWeb, 
                       IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma, 
                       TiempoVisualizacionTotalPaginaPrograma, 
                       IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas, 
                       TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas, 
                       IdOperadorComparacionNroClickEnlacePaginaPrograma, 
                       NroClickEnlacePaginaPrograma, 
                       ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma, 
                       ConsiderarClickBotonMatricularmePaginaPrograma, 
                       ConsiderarClickBotonVersionPruebaPaginaPrograma, 
                       IdOperadorComparacionTiempoVisualizacionTotalPaginaBSCampus, 
                       TiempoVisualizacionTotalPaginaBSCampus, 
                       IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBSCampus, 
                       TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBSCampus, 
                       IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea, 
                       NroVisitasDirectorioTagAreaSubArea, 
                       IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea, 
                       TiempoVisualizacionTotalDirectorioTagAreaSubArea, 
                       IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea, 
                       NroClickEnlaceDirectorioTagAreaSubArea, 
                       IdOperadorComparacionNroVisitasPaginaMisCursos, 
                       NroVisitasPaginaMisCursos, 
                       IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos, 
                       TiempoVisualizacionTotalPaginaMisCursos, 
                       IdOperadorComparacionNroClickEnlacePaginaMisCursos, 
                       NroClickEnlacePaginaMisCursos, 
                       IdOperadorComparacionNroVisitaPaginaCursoDiplomado, 
                       NroVisitaPaginaCursoDiplomado, 
                       IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado, 
                       TiempoVisualizacionTotalPaginaCursoDiplomado, 
                       IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado, 
                       NroClicksEnlacePaginaCursoDiplomado, 
                       ConsiderarClickFiltroPaginaCursoDiplomado, 
                       IdOperadorComparacionNroSolicitudInformacionPg, 
                       NroSolicitudInformacionPg, 
                       IdOperadorComparacionNroSolicitudInformacionArea, 
                       NroSolicitudInformacionArea, 
                       IdOperadorComparacionNroSolicitudInformacionSubArea, 
                       NroSolicitudInformacionSubArea, 
                       ConsiderarOportunidadHistorica, 
                       ConsiderarCategoriaDato, 
                       ConsiderarInteraccionOfflineOnline, 
                       ConsiderarInteraccionSitioWeb, 
                       ConsiderarInteraccionFormularios, 
                       ConsiderarInteraccionChatPW, 
                       ConsiderarInteraccionCorreo, 
                       ConsiderarHistorialFinanciero, 
                       ConsiderarInteraccionWhatsApp, 
                       ConsiderarInteraccionChatMessenger, 
                       ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal, 
                       FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal, 
                       FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal, 
                       IdTiempoFrecuenciaMatriculaAlumno, 
                       CantidadTiempoMatriculaAlumno,
                       ConsiderarConMessengerValido,
                       ConsiderarConWhatsAppValido,
                       ConsiderarConEmailValido,
                       IdTiempoFrecuenciaCumpleaniosContactoDentroDe,
                       CantidadTiempoCumpleaniosContactoDentroDe,
                       FechaInicioMatriculaAlumno,
                       FechaFinMatriculaAlumno,
                       ConsiderarAlumnosAsignacionAutomaticaOperaciones,
                       ExcluirMatriculados,
                       IdOperadorMedidaTiempoCreacionOportunidad,
                       NroMedidaTiempoCreacionOportunidad,
                       IdOperadorMedidaTiempoUltimaActividadEjecutada,
                       NroMedidaTiempoUltimaActividadEjecutada,
                       EnvioAutomaticoEstadoActividadDetalle,
                       ConsiderarYaEnviados,
                       ConsiderarEnvioAutomatico,
                       AplicaSobreCreacionOportunidad,
                       AplicaSobreUltimaActividad
                FROM mkt.V_TFiltroSegmento_Panel
                WHERE Id = @id
                      AND Estado = 1;
                ";
                var respuestaDapper = _dapper.FirstOrDefault(_query, new { id });

                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Contains("[]"))
                {
                    item = JsonConvert.DeserializeObject<FiltroSegmentoDTO>(respuestaDapper);
                }
                return item;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los valores luego de ser aplicado el filtro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<FiltroSegmentoCompuestoDTO> ObtenerResultado(int id, int idFiltroSegmentoTipoContacto)
        {
            try
            {
                var listadoFiltroSegmentoCompuestos = new List<FiltroSegmentoCompuestoDTO>();

                var query = "";

                switch (idFiltroSegmentoTipoContacto)
                {
                    case 1:///alumno - exalumno
                        query = "mkt.SP_ObtenerResultadoFiltroTipoAlumno";
                        break;
                    case 2://docente
                        query = "";
                        break;
                    case 6:///prospecto
                        query = "mkt.SP_ObtenerResultadoFiltro";
                        break;
                    default:
                        break;
                }
                var listadoFiltroSegmentoDB = _dapper.QuerySPDapper(query, new { IdFiltroSegmento = id });

                if (!string.IsNullOrEmpty(listadoFiltroSegmentoDB) && !listadoFiltroSegmentoDB.Contains("[]"))
                {
                    listadoFiltroSegmentoCompuestos = JsonConvert.DeserializeObject<List<FiltroSegmentoCompuestoDTO>>(listadoFiltroSegmentoDB);
                }
                return listadoFiltroSegmentoCompuestos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Ejecuta el filtro segmento de tipo contacto alumno - ex alumno
        /// </summary>
        /// <param name="obj"></param>
        private void EjecutarFiltroTipoContactoAlumnoExAlumno(FiltroSegmentoDTO obj)
        {
            try
            {
                var IdFiltroSegmento = obj.Id;
                var ListaAreaCapacitacion = string.Join(",", obj.ListaArea.Select(x => x.Valor));
                var ListaSubAreaCapacitacion = string.Join(",", obj.ListaSubArea.Select(x => x.Valor));
                var ListaPGeneral = string.Join(",", obj.ListaProgramaGeneral.Select(x => x.Valor));
                var ListaPEspecifico = string.Join(",", obj.ListaProgramaEspecifico.Select(x => x.Valor));
                var ListaEstadoMatricula = string.Join(",", obj.ListaEstadoMatricula.Select(x => x.Valor));
                var ListaSubEstadoMatricula = string.Join(",", obj.ListaSubEstadoMatricula.Select(x => x.Valor));
                var ListaModalidadCurso = string.Join(",", obj.ListaModalidadCurso.Select(x => x.Valor));
                var ListaPais = string.Join(",", obj.ListaPais.Select(x => x.Valor));
                var ListaCiudad = string.Join(",", obj.ListaCiudad.Select(x => x.Valor));
                var ListaDocumentoAlumno = string.Join(",", obj.ListaDocumentoAlumno.Select(x => x.Valor));
                var ListaActividadCabecera = string.Join(",", obj.ListaActividadCabecera.Select(x => x.Valor));
                var ListaOcurrencia = string.Join(",", obj.ListaOcurrencia.Select(x => x.Valor));
                var CantidadTiempoMatriculaAlumno = obj.CantidadTiempoMatriculaAlumno;
                var IdTiempoFrecuenciaMatriculaAlumno = obj.IdTiempoFrecuenciaMatriculaAlumno;
                var FechaInicioMatriculaAlumno = obj.FechaInicioMatriculaAlumno;
                var FechaFinMatriculaAlumno = obj.FechaFinMatriculaAlumno;
                var CantidadTiempoCumpleaniosContactoDentroDe = obj.CantidadTiempoCumpleaniosContactoDentroDe;
                var IdTiempoFrecuenciaCumpleaniosContactoDentroDe = obj.IdTiempoFrecuenciaCumpleaniosContactoDentroDe;
                var NombreUsuario = obj.NombreUsuario;
                var ConsiderarTabEstadoSesion = obj.ListaSesion.Any();
                var ConsiderarTabEstadoAvanceAcademico = obj.ListaEstadoAcademico.Any();
                var ConsiderarTabEstadoPago = obj.ListaEstadoPago.Any();
                var ConsiderarTabPorcentajeAvance = obj.ListaPorcentajeAvance.Any();
                var ConsiderarTabEstadoLlamadaTelefonica = obj.ListaEstadoLlamada.Any();
                var ConsiderarTabEstadoSesionWebinar = obj.ListaSesionWebinar.Any();
                var ConsiderarTabEstadoTrabajoAlumno = (obj.ListaTrabajoAlumno.Any() || obj.ListaTrabajoAlumnoFinal.Any()) ? true : false;
                var ConsiderarConMessengerValido = obj.ConsiderarConMessengerValido;
                var ConsiderarConWhatsAppValido = obj.ConsiderarConWhatsAppValido;
                var ConsiderarConEmailValido = obj.ConsiderarConEmailValido;
                var ListaTarifario = string.Join(",", obj.ListaTarifario.Select(x => x.Valor));
                var ConsiderarAlumnosAsignacionAutomaticaOperaciones = obj.ConsiderarAlumnosAsignacionAutomaticaOperaciones;

                var parametros = new
                {
                    IdFiltroSegmento,
                    ListaAreaCapacitacion,
                    ListaSubAreaCapacitacion,
                    ListaPGeneral,
                    ListaPEspecifico,
                    ListaEstadoMatricula,
                    ListaSubEstadoMatricula,
                    ListaModalidadCurso,
                    ListaPais,
                    ListaCiudad,
                    ListaDocumentoAlumno,
                    ListaActividadCabecera,
                    ListaOcurrencia,
                    CantidadTiempoMatriculaAlumno,
                    IdTiempoFrecuenciaMatriculaAlumno,
                    FechaInicioMatriculaAlumno,
                    FechaFinMatriculaAlumno,
                    CantidadTiempoCumpleaniosContactoDentroDe,
                    IdTiempoFrecuenciaCumpleaniosContactoDentroDe,
                    NombreUsuario,
                    ConsiderarTabEstadoSesion,
                    ConsiderarTabEstadoAvanceAcademico,
                    ConsiderarTabEstadoPago,
                    ConsiderarTabPorcentajeAvance,
                    ConsiderarTabEstadoLlamadaTelefonica,
                    ConsiderarTabEstadoSesionWebinar,
                    ConsiderarTabEstadoTrabajoAlumno,
                    ConsiderarConMessengerValido,
                    ConsiderarConWhatsAppValido,
                    ConsiderarConEmailValido,
                    ListaTarifario,
                    ConsiderarAlumnosAsignacionAutomaticaOperaciones
                };

                _dapper.QuerySPDapper("mkt.SP_EjecutarFiltroSegmentoTipoAlumno", parametros);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Ejecuta el filtro segmento de tipo contacto docente
        /// </summary>
        /// <param name="obj"></param>
        private void EjecutarFiltroTipoContactoDocente(FiltroSegmentoDTO obj)
        {
            try
            {
                var Id = obj.Id;
                var ListaAreaCapacitacion = string.Join(",", obj.ListaArea.Select(x => x.Valor));
                var ListaSubAreaCapacitacion = string.Join(",", obj.ListaSubArea.Select(x => x.Valor));
                var ListaPGeneral = string.Join(",", obj.ListaProgramaGeneral.Select(x => x.Valor));
                var ListaPEspecifico = string.Join(",", obj.ListaProgramaEspecifico.Select(x => x.Valor));
                //var ListadoPais = string.Join(",", obj.ListaPais.Select(x => x.Valor));
                //var ListadoCiudad = string.Join(",", obj.ListaCiudad.Select(x => x.Valor));
                var FechaInicioFormulario = obj.FechaInicioFormulario;
                var FechaFinFormulario = obj.FechaFinFormulario;

                var parametros = new
                {
                    Id,
                    ListaAreaCapacitacion,
                    ListaSubAreaCapacitacion,
                    ListaPGeneral,
                    ListaPEspecifico,
                    FechaInicioFormulario,
                    FechaFinFormulario
                };

                var listadoFiltroSegmentoDB = _dapper.QuerySPDapper("mkt.SP_EjecutarFiltroSegmentoTipoDocente", parametros);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Ejecuta el filtro segmento de tipo contacto prospecto
        /// </summary>
        /// <param name="obj"></param>
        private void EjecutarFiltroTipoContactoProspecto(FiltroSegmentoDTO obj)
        {
            try
            {
                var IdFiltroSegmento = obj.Id;
                var ListaAreaCapacitacion = string.Join(",", obj.ListaArea.Select(x => x.Valor));
                var ListaSubAreaCapacitacion = string.Join(",", obj.ListaSubArea.Select(x => x.Valor));
                var ListaPGeneral = string.Join(",", obj.ListaProgramaGeneral.Select(x => x.Valor));
                var ListaPEspecifico = string.Join(",", obj.ListaProgramaEspecifico.Select(x => x.Valor));

                var IdOperadorComparacionNroSolicitudInformacion = obj.IdOperadorComparacionNroSolicitudInformacion;
                var NroSolicitudInformacion = obj.NroSolicitudInformacion;
                var IdOperadorComparacionNroOportunidades = obj.IdOperadorComparacionNroOportunidades;
                var NroOportunidades = obj.NroOportunidades;

                var EsRN2 = obj.EsRn2;
                DateTime? FechaInicioProgramacionUltimaActividadDetalleRn2 = null;
                DateTime? FechaFinProgramacionUltimaActividadDetalleRn2 = null;

                if (EsRN2)
                {
                    FechaInicioProgramacionUltimaActividadDetalleRn2 = obj.FechaInicioProgramacionUltimaActividadDetalleRn2.Value.Date;
                    FechaFinProgramacionUltimaActividadDetalleRn2 = obj.FechaFinProgramacionUltimaActividadDetalleRn2.Value.Date;
                }


                var IdOperadorComparacionNroSolicitudInformacionPg = obj.IdOperadorComparacionNroSolicitudInformacionPg;
                var NroSolicitudInformacionPg = obj.NroSolicitudInformacionPg;
                var IdOperadorComparacionNroSolicitudInformacionArea = obj.IdOperadorComparacionNroSolicitudInformacionArea;
                var NroSolicitudInformacionArea = obj.NroSolicitudInformacionArea;
                var IdOperadorComparacionNroSolicitudInformacionSubArea = obj.IdOperadorComparacionNroSolicitudInformacionSubArea;
                var NroSolicitudInformacionSubArea = obj.NroSolicitudInformacionSubArea;

                var ExcluirMatriculados = obj.ExcluirMatriculados;

                var FechaInicioCreacionUltimaOportunidad = obj.FechaInicioCreacionUltimaOportunidad;
                var FechaFinCreacionUltimaOportunidad = obj.FechaFinCreacionUltimaOportunidad;

                var FechaInicioModificacionUltimaActividadDetalle = obj.FechaInicioModificacionUltimaActividadDetalle;
                var FechaFinModificacionUltimaActividadDetalle = obj.FechaFinModificacionUltimaActividadDetalle;

                var ListaOportunidadInicialFaseMaxima = string.Join(",", obj.ListaOportunidadInicialFaseMaxima.Select(x => x.Valor));
                var ListaOportunidadInicialFaseActual = string.Join(",", obj.ListaOportunidadInicialFaseActual.Select(x => x.Valor));
                var ListaOportunidadActualFaseMaxima = string.Join(",", obj.ListaOportunidadActualFaseMaxima.Select(x => x.Valor));
                var ListaOportunidadActualFaseActual = string.Join(",", obj.ListaOportunidadActualFaseActual.Select(x => x.Valor));

                var ListaPais = string.Join(",", obj.ListaPais.Select(x => x.Valor));
                var ListaCiudad = string.Join(",", obj.ListaCiudad.Select(x => x.Valor));

                var ListaTipoCategoriaOrigen = string.Join(",", obj.ListaTipoCategoriaOrigen.Select(x => x.Valor));
                var ListaCategoriaOrigen = string.Join(",", obj.ListaCategoriaOrigen.Select(x => x.Valor));


                var ListaCargo = string.Join(",", obj.ListaCargo.Select(x => x.Valor));
                var ListaIndustria = string.Join(",", obj.ListaIndustria.Select(x => x.Valor));
                var ListaAreaFormacion = string.Join(",", obj.ListaAreaFormacion.Select(x => x.Valor));
                var ListaAreaTrabajo = string.Join(",", obj.ListaAreaTrabajo.Select(x => x.Valor));

                var FechaInicioFormulario = obj.FechaInicioFormulario;
                var FechaFinFormulario = obj.FechaFinFormulario;
                var ListaTipoFormulario = string.Join(",", obj.ListaTipoFormulario.Select(x => x.Valor));
                var ListaTipoInteraccionFormulario = string.Join(",", obj.ListaTipoInteraccionFormulario.Select(x => x.Valor));

                var ListaProbabilidadOportunidad = string.Join(",", obj.ListaProbabilidadOportunidad.Select(x => x.Valor));
                var ListaActividadLlamada = string.Join(",", obj.ListaActividadLlamada.Select(x => x.Valor));

                var FechaInicioChatIntegra = obj.FechaInicioChatIntegra;
                var FechaFinChatIntegra = obj.FechaFinChatIntegra;
                var IdOperadorComparacionTiempoMaximoRespuestaChatOnline = obj.IdOperadorComparacionTiempoMaximoRespuestaChatOnline;
                var TiempoMaximoRespuestaChatOnline = obj.TiempoMaximoRespuestaChatOnline;
                var IdOperadorComparacionNroPalabrasClienteChatOnline = obj.IdOperadorComparacionNroPalabrasClienteChatOnline;
                var NroPalabrasClienteChatOnline = obj.NroPalabrasClienteChatOnline;
                var IdOperadorComparacionTiempoPromedioRespuestaChatOnline = obj.IdOperadorComparacionTiempoPromedioRespuestaChatOnline;
                var TiempoPromedioRespuestaChatOnline = obj.TiempoPromedioRespuestaChatOnline;
                var IdOperadorComparacionNroPalabrasClienteChatOffline = obj.IdOperadorComparacionNroPalabrasClienteChatOffline;
                var NroPalabrasClienteChatOffline = obj.NroPalabrasClienteChatOffline;

                var FechaInicioCorreo = obj.FechaInicioCorreo;
                var FechaFinCorreo = obj.FechaFinCorreo;
                var IdOperadorComparacionNroCorreosAbiertos = obj.IdOperadorComparacionNroCorreosAbiertos;
                var NroCorreosAbiertos = obj.NroCorreosAbiertos;
                var IdOperadorComparacionNroCorreosNoAbiertos = obj.IdOperadorComparacionNroCorreosNoAbiertos;
                var NroCorreosNoAbiertos = obj.NroCorreosNoAbiertos;
                var IdOperadorComparacionNroClicksEnlace = obj.IdOperadorComparacionNroClicksEnlace;
                var NroClicksEnlace = obj.NroClicksEnlace;
                var EsSuscribirme = obj.EsSuscribirme;
                var EsDesuscribirme = obj.EsDesuscribirme;

                var IdOperadorComparacionNroCorreosAbiertosMailChimp = obj.IdOperadorComparacionNroCorreosAbiertosMailChimp;
                var NroCorreosAbiertosMailChimp = obj.NroCorreosAbiertosMailChimp;
                var IdOperadorComparacionNroCorreosNoAbiertosMailChimp = obj.IdOperadorComparacionNroCorreosNoAbiertosMailChimp;
                var NroCorreosNoAbiertosMailChimp = obj.NroCorreosNoAbiertosMailChimp;
                var IdOperadorComparacionNroClicksEnlaceMailChimp = obj.IdOperadorComparacionNroClicksEnlaceMailChimp;
                var NroClicksEnlaceMailChimp = obj.NroClicksEnlaceMailChimp;

                var ConsiderarFiltroGeneral = obj.ConsiderarFiltroGeneral;
                var ConsiderarFiltroEspecifico = obj.ConsiderarFiltroEspecifico;
                var TieneVentaCruzada = obj.TieneVentaCruzada;

                var FechaInicioLlamada = obj.FechaInicioLlamada;
                var FechaFinLlamada = obj.FechaFinLlamada;

                var IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad = obj.IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad;
                var DuracionPromedioLlamadaPorOportunidad = obj.DuracionPromedioLlamadaPorOportunidad;
                var IdOperadorComparacionDuracionTotalLlamadaPorOportunidad = obj.IdOperadorComparacionDuracionTotalLlamadaPorOportunidad;
                var DuracionTotalLlamadaPorOportunidad = obj.DuracionTotalLlamadaPorOportunidad;
                var IdOperadorComparacionNroLlamada = obj.IdOperadorComparacionNroLlamada;
                var NroLlamada = obj.NroLlamada;
                var IdOperadorComparacionDuracionLlamada = obj.IdOperadorComparacionDuracionLlamada;
                var DuracionLlamada = obj.DuracionLlamada;
                var IdOperadorComparacionTasaEjecucionLlamada = obj.IdOperadorComparacionTasaEjecucionLlamada;
                var TasaEjecucionLlamada = obj.TasaEjecucionLlamada;

                var IdOperadorComparacionNroTotalLineaCreditoVigente = obj.IdOperadorComparacionNroTotalLineaCreditoVigente;
                var NroTotalLineaCreditoVigente = obj.NroTotalLineaCreditoVigente;
                var IdOperadorComparacionMontoTotalLineaCreditoVigente = obj.IdOperadorComparacionMontoTotalLineaCreditoVigente;
                var MontoTotalLineaCreditoVigente = obj.MontoTotalLineaCreditoVigente;
                var IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente = obj.IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente;
                var MontoMaximoOtorgadoLineaCreditoVigente = obj.MontoMaximoOtorgadoLineaCreditoVigente;
                var IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente = obj.IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente;
                var MontoMinimoOtorgadoLineaCreditoVigente = obj.MontoMinimoOtorgadoLineaCreditoVigente;
                var IdOperadorComparacionNroTotalLineaCreditoVigenteVencida = obj.IdOperadorComparacionNroTotalLineaCreditoVigenteVencida;
                var NroTotalLineaCreditoVigenteVencida = obj.NroTotalLineaCreditoVigenteVencida;
                var IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida = obj.IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida;
                var MontoTotalLineaCreditoVigenteVencida = obj.MontoTotalLineaCreditoVigenteVencida;
                var IdOperadorComparacionNroTcOtorgada = obj.IdOperadorComparacionNroTcOtorgada;
                var NroTcOtorgada = obj.NroTcOtorgada;
                var IdOperadorComparacionMontoTotalOtorgadoEnTcs = obj.IdOperadorComparacionMontoTotalOtorgadoEnTcs;
                var MontoTotalOtorgadoEnTcs = obj.MontoTotalOtorgadoEnTcs;

                var IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc = obj.IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc;
                var MontoMaximoOtorgadoEnUnaTc = obj.MontoMaximoOtorgadoEnUnaTc;
                var IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc = obj.IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc;
                var MontoMinimoOtorgadoEnUnaTc = obj.MontoMinimoOtorgadoEnUnaTc;
                var IdOperadorComparacionMontoDisponibleTotalEnTcs = obj.IdOperadorComparacionMontoDisponibleTotalEnTcs;
                var MontoDisponibleTotalEnTcs = obj.MontoDisponibleTotalEnTcs;


                var FechaInicioInteraccionSitioWeb = obj.FechaInicioInteraccionSitioWeb;
                var FechaFinInteraccionSitioWeb = obj.FechaFinInteraccionSitioWeb;
                var IdOperadorComparacionTiempoVisualizacionTotalSitioWeb = obj.IdOperadorComparacionTiempoVisualizacionTotalSitioWeb;
                var TiempoVisualizacionTotalSitioWeb = obj.TiempoVisualizacionTotalSitioWeb;
                var IdOperadorComparacionNroClickEnlaceTodoSitioWeb = obj.IdOperadorComparacionNroClickEnlaceTodoSitioWeb;
                var NroClickEnlaceTodoSitioWeb = obj.NroClickEnlaceTodoSitioWeb;
                var IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma;
                var TiempoVisualizacionTotalPaginaPrograma = obj.TiempoVisualizacionTotalPaginaPrograma;
                var IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = obj.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas;
                var TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = obj.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas;
                var IdOperadorComparacionNroClickEnlacePaginaPrograma = obj.IdOperadorComparacionNroClickEnlacePaginaPrograma;
                var NroClickEnlacePaginaPrograma = obj.NroClickEnlacePaginaPrograma;
                var ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma = obj.ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma;
                var ConsiderarClickBotonMatricularmePaginaPrograma = obj.ConsiderarClickBotonMatricularmePaginaPrograma;
                var ConsiderarClickBotonVersionPruebaPaginaPrograma = obj.ConsiderarClickBotonVersionPruebaPaginaPrograma;
                var IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus;
                var TiempoVisualizacionTotalPaginaBscampus = obj.TiempoVisualizacionTotalPaginaBscampus;
                var IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = obj.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus;
                var TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = obj.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus;

                var IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea = obj.IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea;
                var NroVisitasDirectorioTagAreaSubArea = obj.NroVisitasDirectorioTagAreaSubArea;
                var IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea = obj.IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea;
                var TiempoVisualizacionTotalDirectorioTagAreaSubArea = obj.TiempoVisualizacionTotalDirectorioTagAreaSubArea;
                var IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea = obj.IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea;
                var NroClickEnlaceDirectorioTagAreaSubArea = obj.NroClickEnlaceDirectorioTagAreaSubArea;
                var IdOperadorComparacionNroVisitasPaginaMisCursos = obj.IdOperadorComparacionNroVisitasPaginaMisCursos;
                var NroVisitasPaginaMisCursos = obj.NroVisitasPaginaMisCursos;
                var IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos;
                var TiempoVisualizacionTotalPaginaMisCursos = obj.TiempoVisualizacionTotalPaginaMisCursos;
                var IdOperadorComparacionNroClickEnlacePaginaMisCursos = obj.IdOperadorComparacionNroClickEnlacePaginaMisCursos;
                var NroClickEnlacePaginaMisCursos = obj.NroClickEnlacePaginaMisCursos;
                var IdOperadorComparacionNroVisitaPaginaCursoDiplomado = obj.IdOperadorComparacionNroVisitaPaginaCursoDiplomado;
                var NroVisitaPaginaCursoDiplomado = obj.NroVisitaPaginaCursoDiplomado;
                var IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado;
                var TiempoVisualizacionTotalPaginaCursoDiplomado = obj.TiempoVisualizacionTotalPaginaCursoDiplomado;
                var IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado = obj.IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado;
                var NroClicksEnlacePaginaCursoDiplomado = obj.NroClicksEnlacePaginaCursoDiplomado;
                var ConsiderarClickFiltroPaginaCursoDiplomado = obj.ConsiderarClickFiltroPaginaCursoDiplomado;


                var ListaVCAreaCapacitacion = string.Join(",", obj.ListaVCArea.Select(x => x.Valor));
                var ListaVCSubAreaCapacitacion = string.Join(",", obj.ListaVCSubArea.Select(x => x.Valor));
                var ListaVCPGeneralCapacitacion = string.Join(",", obj.ListaVCPGeneral.Select(x => x.Valor));

                var ListaProbabilidadVentaCruzada = string.Join(",", obj.ListaProbabilidadVentaCruzada.Select(x => x.Valor));

                var ConsiderarTabOportunidadHistorica = obj.ConsiderarOportunidadHistorica;
                var ConsiderarTabCategoriaDato = obj.ConsiderarCategoriaDato;
                var ConsiderarTabInteraccionOfflineOnline = obj.ConsiderarInteraccionOfflineOnline;
                var ConsiderarTabInteraccionSitioWeb = obj.ConsiderarInteraccionSitioWeb;
                var ConsiderarTabInteraccionFormularios = obj.ConsiderarInteraccionFormularios;
                var ConsiderarTabInteraccionChatPw = obj.ConsiderarInteraccionChatPw;
                var ConsiderarTabInteraccionCorreo = obj.ConsiderarInteraccionCorreo;
                var ConsiderarTabHistorialFinanciero = obj.ConsiderarHistorialFinanciero;
                var ConsiderarTabInteraccionWhatsApp = obj.ConsiderarInteraccionWhatsApp;
                var ConsiderarTabInteraccionChatMessenger = obj.ConsiderarInteraccionChatMessenger;

                var ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = obj.ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;
                var FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = obj.FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;
                var FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = obj.FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;

                var ConsiderarConMessengerValido = obj.ConsiderarConMessengerValido;
                var ConsiderarConWhatsAppValido = obj.ConsiderarConWhatsAppValido;
                var ConsiderarConEmailValido = obj.ConsiderarConEmailValido;

                var NombreUsuario = obj.NombreUsuario;

                var parametros = new
                {
                    IdFiltroSegmento,
                    ListaAreaCapacitacion,
                    ListaSubAreaCapacitacion,
                    ListaPGeneral,
                    ListaPEspecifico,

                    IdOperadorComparacionNroSolicitudInformacion,
                    NroSolicitudInformacion,
                    IdOperadorComparacionNroOportunidades,
                    NroOportunidades,

                    EsRN2,
                    FechaInicioProgramacionUltimaActividadDetalleRn2,
                    FechaFinProgramacionUltimaActividadDetalleRn2,

                    FechaInicioCreacionUltimaOportunidad,
                    FechaFinCreacionUltimaOportunidad,

                    FechaInicioModificacionUltimaActividadDetalle,
                    FechaFinModificacionUltimaActividadDetalle,

                    ListaOportunidadInicialFaseMaxima,
                    ListaOportunidadInicialFaseActual,
                    ListaOportunidadActualFaseMaxima,
                    ListaOportunidadActualFaseActual,

                    ListaPais,
                    ListaCiudad,

                    ListaTipoCategoriaOrigen,
                    ListaCategoriaOrigen,

                    ListaCargo,
                    ListaIndustria,
                    ListaAreaFormacion,
                    ListaAreaTrabajo,

                    FechaInicioFormulario,
                    FechaFinFormulario,
                    ListaTipoFormulario,
                    ListaTipoInteraccionFormulario,

                    FechaInicioChatIntegra,
                    FechaFinChatIntegra,
                    IdOperadorComparacionTiempoMaximoRespuestaChatOnline,
                    TiempoMaximoRespuestaChatOnline,
                    IdOperadorComparacionNroPalabrasClienteChatOnline,
                    NroPalabrasClienteChatOnline,
                    IdOperadorComparacionTiempoPromedioRespuestaChatOnline,
                    TiempoPromedioRespuestaChatOnline,
                    IdOperadorComparacionNroPalabrasClienteChatOffline,
                    NroPalabrasClienteChatOffline,

                    FechaInicioCorreo,
                    FechaFinCorreo,
                    IdOperadorComparacionNroCorreosAbiertos,
                    NroCorreosAbiertos,
                    IdOperadorComparacionNroCorreosNoAbiertos,
                    NroCorreosNoAbiertos,
                    IdOperadorComparacionNroClicksEnlace,
                    NroClicksEnlace,
                    EsSuscribirme,
                    EsDesuscribirme,

                    IdOperadorComparacionNroCorreosAbiertosMailChimp,
                    NroCorreosAbiertosMailChimp,
                    IdOperadorComparacionNroCorreosNoAbiertosMailChimp,
                    NroCorreosNoAbiertosMailChimp,
                    IdOperadorComparacionNroClicksEnlaceMailChimp,
                    NroClicksEnlaceMailChimp,

                    ConsiderarFiltroGeneral,
                    ConsiderarFiltroEspecifico,
                    TieneVentaCruzada,

                    ListaProbabilidadOportunidad,
                    ListaActividadLlamada,

                    FechaInicioLlamada,
                    FechaFinLlamada,

                    IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad,
                    DuracionPromedioLlamadaPorOportunidad,
                    IdOperadorComparacionDuracionTotalLlamadaPorOportunidad,
                    DuracionTotalLlamadaPorOportunidad,
                    IdOperadorComparacionNroLlamada,
                    NroLlamada,
                    IdOperadorComparacionDuracionLlamada,
                    DuracionLlamada,
                    IdOperadorComparacionTasaEjecucionLlamada,
                    TasaEjecucionLlamada,

                    IdOperadorComparacionNroTotalLineaCreditoVigente,
                    NroTotalLineaCreditoVigente,
                    IdOperadorComparacionMontoTotalLineaCreditoVigente,
                    MontoTotalLineaCreditoVigente,
                    IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente,
                    MontoMaximoOtorgadoLineaCreditoVigente,
                    IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente,
                    MontoMinimoOtorgadoLineaCreditoVigente,
                    IdOperadorComparacionNroTotalLineaCreditoVigenteVencida,
                    NroTotalLineaCreditoVigenteVencida,
                    IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida,
                    MontoTotalLineaCreditoVigenteVencida,
                    IdOperadorComparacionNroTcOtorgada,
                    NroTcOtorgada,
                    IdOperadorComparacionMontoTotalOtorgadoEnTcs,
                    MontoTotalOtorgadoEnTcs,

                    IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc,
                    MontoMaximoOtorgadoEnUnaTc,
                    IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc,
                    MontoMinimoOtorgadoEnUnaTc,
                    IdOperadorComparacionMontoDisponibleTotalEnTcs,
                    MontoDisponibleTotalEnTcs,
                    ExcluirMatriculados,

                    FechaInicioInteraccionSitioWeb,
                    FechaFinInteraccionSitioWeb,
                    IdOperadorComparacionTiempoVisualizacionTotalSitioWeb,
                    TiempoVisualizacionTotalSitioWeb,
                    IdOperadorComparacionNroClickEnlaceTodoSitioWeb,
                    NroClickEnlaceTodoSitioWeb,
                    IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma,
                    TiempoVisualizacionTotalPaginaPrograma,
                    IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas,
                    TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas,

                    IdOperadorComparacionNroClickEnlacePaginaPrograma,
                    NroClickEnlacePaginaPrograma,
                    ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma,
                    ConsiderarClickBotonMatricularmePaginaPrograma,
                    ConsiderarClickBotonVersionPruebaPaginaPrograma,
                    IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus,
                    TiempoVisualizacionTotalPaginaBscampus,
                    IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus,
                    TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus,
                    IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea,

                    NroVisitasDirectorioTagAreaSubArea,
                    IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea,
                    TiempoVisualizacionTotalDirectorioTagAreaSubArea,
                    IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea,
                    NroClickEnlaceDirectorioTagAreaSubArea,
                    IdOperadorComparacionNroVisitasPaginaMisCursos,
                    NroVisitasPaginaMisCursos,
                    IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos,
                    TiempoVisualizacionTotalPaginaMisCursos,
                    IdOperadorComparacionNroClickEnlacePaginaMisCursos,

                    NroClickEnlacePaginaMisCursos,
                    IdOperadorComparacionNroVisitaPaginaCursoDiplomado,
                    NroVisitaPaginaCursoDiplomado,
                    IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado,
                    TiempoVisualizacionTotalPaginaCursoDiplomado,
                    IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado,
                    NroClicksEnlacePaginaCursoDiplomado,
                    ConsiderarClickFiltroPaginaCursoDiplomado,

                    ListaVCAreaCapacitacion,
                    ListaVCSubAreaCapacitacion,
                    ListaVCPGeneralCapacitacion,

                    ListaProbabilidadVentaCruzada,

                    //nuevos filtros
                    IdOperadorComparacionNroSolicitudInformacionPg,
                    NroSolicitudInformacionPg,
                    IdOperadorComparacionNroSolicitudInformacionArea,
                    NroSolicitudInformacionArea,
                    IdOperadorComparacionNroSolicitudInformacionSubArea,
                    NroSolicitudInformacionSubArea,

                    //filtros tabs
                    ConsiderarTabOportunidadHistorica,
                    ConsiderarTabCategoriaDato,
                    ConsiderarTabInteraccionOfflineOnline,
                    ConsiderarTabInteraccionSitioWeb,
                    ConsiderarTabInteraccionFormularios,
                    ConsiderarTabInteraccionChatPw,
                    ConsiderarTabInteraccionCorreo,
                    ConsiderarTabHistorialFinanciero,
                    ConsiderarTabInteraccionWhatsApp,
                    ConsiderarTabInteraccionChatMessenger,

                    ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal,
                    FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal,
                    FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal,

                    ConsiderarConMessengerValido,
                    ConsiderarConWhatsAppValido,
                    ConsiderarConEmailValido,

                    NombreUsuario
                };

                var listadoFiltroSegmentoDB = _dapper.QuerySPDapper("mkt.SP_EjecutarFiltroSegmento", parametros, 55);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Ejecuta el filtro y guarda sus registros en la tabla
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool EjecutarFiltro(FiltroSegmentoDTO obj)
        {
            if (!obj.ConsiderarFiltroGeneral || !obj.ConsiderarFiltroEspecifico)
            {
                throw new Exception("No seleccionó considerar filtro General o Especifico");
            }

            try
            {
                switch (obj.IdFiltroSegmentoTipoContacto)
                {
                    case 1:///alumno - exalumno
                        this.EjecutarFiltroTipoContactoAlumnoExAlumno(obj);
                        break;
                    case 2://docente
                        this.EjecutarFiltroTipoContactoDocente(obj);
                        break;
                    case 6:///prospecto
                        this.EjecutarFiltroTipoContactoProspecto(obj);
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Ejecuta el filtro de Mailing General y guarda sus registros en la tabla de mailchimp correos para ser enviados por la plataforma de mailchimp
        /// </summary>
        /// <param name="obj">Filtros que se usaran para Mailing General</param>
        /// <returns>Cantidad de contactos</returns>
        public int EjecutarFiltroTipoContactoProspectoMailingGeneral(FiltroSegmentoDTO obj)
        {
            try
            {
                var id = obj.Id;
                var listadoAreaCapacitacion = string.Join(",", obj.ListaArea.Select(x => x.Valor));
                var listadoSubAreaCapacitacion = string.Join(",", obj.ListaSubArea.Select(x => x.Valor));
                var listadoPGeneral = string.Join(",", obj.ListaProgramaGeneral.Select(x => x.Valor));
                var listadoPEspecifico = string.Join(",", obj.ListaProgramaEspecifico.Select(x => x.Valor));

                var idOperadorComparacionNroSolicitudInformacion = obj.IdOperadorComparacionNroSolicitudInformacion;
                var nroSolicitudInformacion = obj.NroSolicitudInformacion;
                var idOperadorComparacionNroOportunidades = obj.IdOperadorComparacionNroOportunidades;
                var nroOportunidades = obj.NroOportunidades;

                var esRN2 = obj.EsRn2;
                DateTime? fechaInicioProgramacionUltimaActividadDetalleRn2 = null;
                DateTime? fechaFinProgramacionUltimaActividadDetalleRn2 = null;

                if (esRN2)
                {
                    fechaInicioProgramacionUltimaActividadDetalleRn2 = obj.FechaInicioProgramacionUltimaActividadDetalleRn2.Value.Date;
                    fechaFinProgramacionUltimaActividadDetalleRn2 = obj.FechaFinProgramacionUltimaActividadDetalleRn2.Value.Date;
                }

                var idOperadorComparacionNroSolicitudInformacionPg = obj.IdOperadorComparacionNroSolicitudInformacionPg;
                var nroSolicitudInformacionPg = obj.NroSolicitudInformacionPg;
                var idOperadorComparacionNroSolicitudInformacionArea = obj.IdOperadorComparacionNroSolicitudInformacionArea;
                var nroSolicitudInformacionArea = obj.NroSolicitudInformacionArea;
                var idOperadorComparacionNroSolicitudInformacionSubArea = obj.IdOperadorComparacionNroSolicitudInformacionSubArea;
                var nroSolicitudInformacionSubArea = obj.NroSolicitudInformacionSubArea;
                var idPrioridadMailChimpLista = obj.IdPrioridadMailChimpLista;
                var idCategoriaObjetoFiltro = obj.IdCategoriaObjetoFiltro;

                var fechaInicioCreacionUltimaOportunidad = obj.FechaInicioCreacionUltimaOportunidad;
                var fechaFinCreacionUltimaOportunidad = obj.FechaFinCreacionUltimaOportunidad;

                var fechaInicioModificacionUltimaActividadDetalle = obj.FechaInicioModificacionUltimaActividadDetalle;
                var fechaFinModificacionUltimaActividadDetalle = obj.FechaFinModificacionUltimaActividadDetalle;

                var listadoOportunidadInicialFaseMaxima = string.Join(",", obj.ListaOportunidadInicialFaseMaxima.Select(x => x.Valor));
                var listadoOportunidadInicialFaseActual = string.Join(",", obj.ListaOportunidadInicialFaseActual.Select(x => x.Valor));
                var listadoOportunidadActualFaseMaxima = string.Join(",", obj.ListaOportunidadActualFaseMaxima.Select(x => x.Valor));
                var listadoOportunidadActualFaseActual = string.Join(",", obj.ListaOportunidadActualFaseActual.Select(x => x.Valor));

                var listadoPais = string.Join(",", obj.ListaPais.Select(x => x.Valor));
                var listadoCiudad = string.Join(",", obj.ListaCiudad.Select(x => x.Valor));

                var listadoTipoCategoriaOrigen = string.Join(",", obj.ListaTipoCategoriaOrigen.Select(x => x.Valor));
                var listadoCategoriaOrigen = string.Join(",", obj.ListaCategoriaOrigen.Select(x => x.Valor));

                var listadoCargo = string.Join(",", obj.ListaCargo.Select(x => x.Valor));
                var listadoIndustria = string.Join(",", obj.ListaIndustria.Select(x => x.Valor));
                var listadoAreaFormacion = string.Join(",", obj.ListaAreaFormacion.Select(x => x.Valor));
                var listadoAreaTrabajo = string.Join(",", obj.ListaAreaTrabajo.Select(x => x.Valor));

                var fechaInicioFormulario = obj.FechaInicioFormulario;
                var fechaFinFormulario = obj.FechaFinFormulario;
                var listadoTipoFormulario = string.Join(",", obj.ListaTipoFormulario.Select(x => x.Valor));
                var listadoTipoInteraccionFormulario = string.Join(",", obj.ListaTipoInteraccionFormulario.Select(x => x.Valor));

                var listadoProbabilidadOportunidad = string.Join(",", obj.ListaProbabilidadOportunidad.Select(x => x.Valor));
                var listadoActividadLlamada = string.Join(",", obj.ListaActividadLlamada.Select(x => x.Valor));

                var fechaInicioChatIntegra = obj.FechaInicioChatIntegra;
                var fechaFinChatIntegra = obj.FechaFinChatIntegra;
                var idOperadorComparacionTiempoMaximoRespuestaChatOnline = obj.IdOperadorComparacionTiempoMaximoRespuestaChatOnline;
                var tiempoMaximoRespuestaChatOnline = obj.TiempoMaximoRespuestaChatOnline;
                var idOperadorComparacionNroPalabrasClienteChatOnline = obj.IdOperadorComparacionNroPalabrasClienteChatOnline;
                var nroPalabrasClienteChatOnline = obj.NroPalabrasClienteChatOnline;
                var idOperadorComparacionTiempoPromedioRespuestaChatOnline = obj.IdOperadorComparacionTiempoPromedioRespuestaChatOnline;
                var tiempoPromedioRespuestaChatOnline = obj.TiempoPromedioRespuestaChatOnline;
                var idOperadorComparacionNroPalabrasClienteChatOffline = obj.IdOperadorComparacionNroPalabrasClienteChatOffline;
                var nroPalabrasClienteChatOffline = obj.NroPalabrasClienteChatOffline;

                var fechaInicioCorreo = obj.FechaInicioCorreo;
                var fechaFinCorreo = obj.FechaFinCorreo;
                var idOperadorComparacionNroCorreosAbiertos = obj.IdOperadorComparacionNroCorreosAbiertos;
                var nroCorreosAbiertos = obj.NroCorreosAbiertos;
                var idOperadorComparacionNroCorreosNoAbiertos = obj.IdOperadorComparacionNroCorreosNoAbiertos;
                var nroCorreosNoAbiertos = obj.NroCorreosNoAbiertos;
                var idOperadorComparacionNroClicksEnlace = obj.IdOperadorComparacionNroClicksEnlace;
                var nroClicksEnlace = obj.NroClicksEnlace;
                var esSuscribirme = obj.EsSuscribirme;
                var esDesuscribirme = obj.EsDesuscribirme;

                var idOperadorComparacionNroCorreosAbiertosMailChimp = obj.IdOperadorComparacionNroCorreosAbiertosMailChimp;
                var nroCorreosAbiertosMailChimp = obj.NroCorreosAbiertosMailChimp;
                var idOperadorComparacionNroCorreosNoAbiertosMailChimp = obj.IdOperadorComparacionNroCorreosNoAbiertosMailChimp;
                var nroCorreosNoAbiertosMailChimp = obj.NroCorreosNoAbiertosMailChimp;
                var idOperadorComparacionNroClicksEnlaceMailChimp = obj.IdOperadorComparacionNroClicksEnlaceMailChimp;
                var nroClicksEnlaceMailChimp = obj.NroClicksEnlaceMailChimp;

                var considerarFiltroGeneral = obj.ConsiderarFiltroGeneral;
                var considerarFiltroEspecifico = obj.ConsiderarFiltroEspecifico;
                var tieneVentaCruzada = obj.TieneVentaCruzada;

                var fechaInicioLlamada = obj.FechaInicioLlamada;
                var fechaFinLlamada = obj.FechaFinLlamada;

                var idOperadorComparacionDuracionPromedioLlamadaPorOportunidad = obj.IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad;
                var duracionPromedioLlamadaPorOportunidad = obj.DuracionPromedioLlamadaPorOportunidad;
                var idOperadorComparacionDuracionTotalLlamadaPorOportunidad = obj.IdOperadorComparacionDuracionTotalLlamadaPorOportunidad;
                var duracionTotalLlamadaPorOportunidad = obj.DuracionTotalLlamadaPorOportunidad;
                var idOperadorComparacionNroLlamada = obj.IdOperadorComparacionNroLlamada;
                var nroLlamada = obj.NroLlamada;
                var idOperadorComparacionDuracionLlamada = obj.IdOperadorComparacionDuracionLlamada;
                var duracionLlamada = obj.DuracionLlamada;
                var idOperadorComparacionTasaEjecucionLlamada = obj.IdOperadorComparacionTasaEjecucionLlamada;
                var tasaEjecucionLlamada = obj.TasaEjecucionLlamada;

                var idOperadorComparacionNroTotalLineaCreditoVigente = obj.IdOperadorComparacionNroTotalLineaCreditoVigente;
                var nroTotalLineaCreditoVigente = obj.NroTotalLineaCreditoVigente;
                var idOperadorComparacionMontoTotalLineaCreditoVigente = obj.IdOperadorComparacionMontoTotalLineaCreditoVigente;
                var montoTotalLineaCreditoVigente = obj.MontoTotalLineaCreditoVigente;
                var idOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente = obj.IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente;
                var montoMaximoOtorgadoLineaCreditoVigente = obj.MontoMaximoOtorgadoLineaCreditoVigente;
                var idOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente = obj.IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente;
                var montoMinimoOtorgadoLineaCreditoVigente = obj.MontoMinimoOtorgadoLineaCreditoVigente;
                var idOperadorComparacionNroTotalLineaCreditoVigenteVencida = obj.IdOperadorComparacionNroTotalLineaCreditoVigenteVencida;
                var nroTotalLineaCreditoVigenteVencida = obj.NroTotalLineaCreditoVigenteVencida;
                var idOperadorComparacionMontoTotalLineaCreditoVigenteVencida = obj.IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida;
                var montoTotalLineaCreditoVigenteVencida = obj.MontoTotalLineaCreditoVigenteVencida;
                var idOperadorComparacionNroTcOtorgada = obj.IdOperadorComparacionNroTcOtorgada;
                var nroTcOtorgada = obj.NroTcOtorgada;
                var idOperadorComparacionMontoTotalOtorgadoEnTcs = obj.IdOperadorComparacionMontoTotalOtorgadoEnTcs;
                var montoTotalOtorgadoEnTcs = obj.MontoTotalOtorgadoEnTcs;

                var idOperadorComparacionMontoMaximoOtorgadoEnUnaTc = obj.IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc;
                var montoMaximoOtorgadoEnUnaTc = obj.MontoMaximoOtorgadoEnUnaTc;
                var idOperadorComparacionMontoMinimoOtorgadoEnUnaTc = obj.IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc;
                var montoMinimoOtorgadoEnUnaTc = obj.MontoMinimoOtorgadoEnUnaTc;
                var idOperadorComparacionMontoDisponibleTotalEnTcs = obj.IdOperadorComparacionMontoDisponibleTotalEnTcs;
                var montoDisponibleTotalEnTcs = obj.MontoDisponibleTotalEnTcs;


                var fechaInicioInteraccionSitioWeb = obj.FechaInicioInteraccionSitioWeb;
                var fechaFinInteraccionSitioWeb = obj.FechaFinInteraccionSitioWeb;
                var idOperadorComparacionTiempoVisualizacionTotalSitioWeb = obj.IdOperadorComparacionTiempoVisualizacionTotalSitioWeb;
                var tiempoVisualizacionTotalSitioWeb = obj.TiempoVisualizacionTotalSitioWeb;
                var idOperadorComparacionNroClickEnlaceTodoSitioWeb = obj.IdOperadorComparacionNroClickEnlaceTodoSitioWeb;
                var nroClickEnlaceTodoSitioWeb = obj.NroClickEnlaceTodoSitioWeb;
                var idOperadorComparacionTiempoVisualizacionTotalPaginaPrograma = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma;
                var tiempoVisualizacionTotalPaginaPrograma = obj.TiempoVisualizacionTotalPaginaPrograma;
                var idOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = obj.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas;
                var tiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = obj.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas;
                var idOperadorComparacionNroClickEnlacePaginaPrograma = obj.IdOperadorComparacionNroClickEnlacePaginaPrograma;
                var nroClickEnlacePaginaPrograma = obj.NroClickEnlacePaginaPrograma;
                var considerarVisualizacionVideoVistaPreviaPaginaPrograma = obj.ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma;
                var considerarClickBotonMatricularmePaginaPrograma = obj.ConsiderarClickBotonMatricularmePaginaPrograma;
                var considerarClickBotonVersionPruebaPaginaPrograma = obj.ConsiderarClickBotonVersionPruebaPaginaPrograma;
                var idOperadorComparacionTiempoVisualizacionTotalPaginaBscampus = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus;
                var tiempoVisualizacionTotalPaginaBscampus = obj.TiempoVisualizacionTotalPaginaBscampus;
                var idOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = obj.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus;
                var tiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = obj.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus;

                var idOperadorComparacionNroVisitasDirectorioTagAreaSubArea = obj.IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea;
                var nroVisitasDirectorioTagAreaSubArea = obj.NroVisitasDirectorioTagAreaSubArea;
                var idOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea = obj.IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea;
                var tiempoVisualizacionTotalDirectorioTagAreaSubArea = obj.TiempoVisualizacionTotalDirectorioTagAreaSubArea;
                var idOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea = obj.IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea;
                var nroClickEnlaceDirectorioTagAreaSubArea = obj.NroClickEnlaceDirectorioTagAreaSubArea;
                var idOperadorComparacionNroVisitasPaginaMisCursos = obj.IdOperadorComparacionNroVisitasPaginaMisCursos;
                var nroVisitasPaginaMisCursos = obj.NroVisitasPaginaMisCursos;
                var idOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos;
                var tiempoVisualizacionTotalPaginaMisCursos = obj.TiempoVisualizacionTotalPaginaMisCursos;
                var idOperadorComparacionNroClickEnlacePaginaMisCursos = obj.IdOperadorComparacionNroClickEnlacePaginaMisCursos;
                var nroClickEnlacePaginaMisCursos = obj.NroClickEnlacePaginaMisCursos;
                var idOperadorComparacionNroVisitaPaginaCursoDiplomado = obj.IdOperadorComparacionNroVisitaPaginaCursoDiplomado;
                var nroVisitaPaginaCursoDiplomado = obj.NroVisitaPaginaCursoDiplomado;
                var idOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado;
                var tiempoVisualizacionTotalPaginaCursoDiplomado = obj.TiempoVisualizacionTotalPaginaCursoDiplomado;
                var idOperadorComparacionNroClicksEnlacePaginaCursoDiplomado = obj.IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado;
                var nroClicksEnlacePaginaCursoDiplomado = obj.NroClicksEnlacePaginaCursoDiplomado;
                var considerarClickFiltroPaginaCursoDiplomado = obj.ConsiderarClickFiltroPaginaCursoDiplomado;

                var campaniaMailing = obj.IdCampaniaMailing;
                var campaniaMailingLista = obj.IdCampaniaMailingLista;

                var listadoVCAreaCapacitacion = string.Join(",", obj.ListaVCArea.Select(x => x.Valor));
                var listadoVCSubAreaCapacitacion = string.Join(",", obj.ListaVCSubArea.Select(x => x.Valor));

                var excluirMatriculados = obj.ExcluirMatriculados;

                var listadoProbabilidadVentaCruzada = string.Join(",", obj.ListaProbabilidadVentaCruzada.Select(x => x.Valor));

                var cantidadPeriodoSinRecibirCorreo = obj.CantidadPeriodoSinRecibirCorreo;
                var tipoPeriodoSinRecibirCorreo = obj.TipoPeriodoSinRecibirCorreo;
                var idProbabilidadOportunidad = obj.IdProbabilidadOportunidad;
                var numeroSegmento = obj.NumeroSegmento;
                var tipoAsociacion = obj.TipoAsociacion;
                var idCampaniaGeneral = obj.IdCampaniaGeneral;

                var considerarTabOportunidadHistorica = obj.ConsiderarOportunidadHistorica;
                var considerarTabCategoriaDato = obj.ConsiderarCategoriaDato;
                var considerarTabInteraccionOfflineOnline = obj.ConsiderarInteraccionOfflineOnline;
                var considerarTabInteraccionSitioWeb = obj.ConsiderarInteraccionSitioWeb;
                var considerarTabInteraccionFormularios = obj.ConsiderarInteraccionFormularios;
                var considerarTabInteraccionChatPw = obj.ConsiderarInteraccionChatPw;
                var considerarTabInteraccionCorreo = obj.ConsiderarInteraccionCorreo;
                var considerarTabHistorialFinanciero = obj.ConsiderarHistorialFinanciero;
                var considerarTabInteraccionWhatsApp = obj.ConsiderarInteraccionWhatsApp;
                var considerarTabInteraccionChatMessenger = obj.ConsiderarInteraccionChatMessenger;
                var considerarTabEnvioAutomatico = obj.ConsiderarEnvioAutomatico;

                var parametros = new
                {
                    Id = id,
                    ListadoAreaCapacitacion = listadoAreaCapacitacion,
                    ListadoSubAreaCapacitacion = listadoSubAreaCapacitacion,
                    ListadoPGeneral = listadoPGeneral,
                    ListadoPEspecifico = listadoPEspecifico,

                    IdOperadorComparacionNroSolicitudInformacion = idOperadorComparacionNroSolicitudInformacion,
                    NroSolicitudInformacion = nroSolicitudInformacion,
                    IdOperadorComparacionNroOportunidades = idOperadorComparacionNroOportunidades,
                    NroOportunidades = nroOportunidades,

                    EsRN2 = esRN2,
                    FechaInicioProgramacionUltimaActividadDetalleRn2 = fechaInicioProgramacionUltimaActividadDetalleRn2,
                    FechaFinProgramacionUltimaActividadDetalleRn2 = fechaFinProgramacionUltimaActividadDetalleRn2,

                    FechaInicioCreacionUltimaOportunidad = fechaInicioCreacionUltimaOportunidad,
                    FechaFinCreacionUltimaOportunidad = fechaFinCreacionUltimaOportunidad,

                    FechaInicioModificacionUltimaActividadDetalle = fechaInicioModificacionUltimaActividadDetalle,
                    FechaFinModificacionUltimaActividadDetalle = fechaFinModificacionUltimaActividadDetalle,

                    ListadoOportunidadInicialFaseMaxima = listadoOportunidadInicialFaseMaxima,
                    ListadoOportunidadInicialFaseActual = listadoOportunidadInicialFaseActual,
                    ListadoOportunidadActualFaseMaxima = listadoOportunidadActualFaseMaxima,
                    ListadoOportunidadActualFaseActual = listadoOportunidadActualFaseActual,

                    ListadoPais = listadoPais,
                    ListadoCiudad = listadoCiudad,

                    ListadoTipoCategoriaOrigen = listadoTipoCategoriaOrigen,
                    ListadoCategoriaOrigen = listadoCategoriaOrigen,

                    ListadoCargo = listadoCargo,
                    ListadoIndustria = listadoIndustria,
                    ListadoAreaFormacion = listadoAreaFormacion,
                    ListadoAreaTrabajo = listadoAreaTrabajo,

                    FechaInicioFormulario = fechaInicioFormulario,
                    FechaFinFormulario = fechaFinFormulario,
                    ListadoTipoFormulario = listadoTipoFormulario,
                    ListadoTipoInteraccionFormulario = listadoTipoInteraccionFormulario,

                    FechaInicioChatIntegra = fechaInicioChatIntegra,
                    FechaFinChatIntegra = fechaFinChatIntegra,
                    IdOperadorComparacionTiempoMaximoRespuestaChatOnline = idOperadorComparacionTiempoMaximoRespuestaChatOnline,
                    TiempoMaximoRespuestaChatOnline = tiempoMaximoRespuestaChatOnline,
                    
                    IdOperadorComparacionNroPalabrasClienteChatOnline = idOperadorComparacionNroPalabrasClienteChatOnline,
                    NroPalabrasClienteChatOnline = nroPalabrasClienteChatOnline,
                    IdOperadorComparacionTiempoPromedioRespuestaChatOnline = idOperadorComparacionTiempoPromedioRespuestaChatOnline,
                    TiempoPromedioRespuestaChatOnline = tiempoPromedioRespuestaChatOnline,
                    IdOperadorComparacionNroPalabrasClienteChatOffline = idOperadorComparacionNroPalabrasClienteChatOffline,
                    NroPalabrasClienteChatOffline = nroPalabrasClienteChatOffline,

                    FechaInicioCorreo = fechaInicioCorreo,
                    FechaFinCorreo = fechaFinCorreo,
                    IdOperadorComparacionNroCorreosAbiertos = idOperadorComparacionNroCorreosAbiertos,
                    NroCorreosAbiertos = nroCorreosAbiertos,
                    IdOperadorComparacionNroCorreosNoAbiertos = idOperadorComparacionNroCorreosNoAbiertos,
                    NroCorreosNoAbiertos = nroCorreosNoAbiertos,
                    IdOperadorComparacionNroClicksEnlace = idOperadorComparacionNroClicksEnlace,
                    NroClicksEnlace = nroClicksEnlace,
                    EsSuscribirme = esSuscribirme,
                    EsDesuscribirme = esDesuscribirme,

                    IdOperadorComparacionNroCorreosAbiertosMailChimp = idOperadorComparacionNroCorreosAbiertosMailChimp,
                    NroCorreosAbiertosMailChimp = nroCorreosAbiertosMailChimp,
                    IdOperadorComparacionNroCorreosNoAbiertosMailChimp = idOperadorComparacionNroCorreosNoAbiertosMailChimp,
                    NroCorreosNoAbiertosMailChimp = nroCorreosNoAbiertosMailChimp,
                    IdOperadorComparacionNroClicksEnlaceMailChimp = idOperadorComparacionNroClicksEnlaceMailChimp,
                    NroClicksEnlaceMailChimp = nroClicksEnlaceMailChimp,

                    ConsiderarFiltroGeneral = considerarFiltroGeneral,
                    ConsiderarFiltroEspecifico = considerarFiltroEspecifico,
                    TieneVentaCruzada = tieneVentaCruzada,

                    ConsiderarTabOportunidadHistorica = considerarTabOportunidadHistorica,
                    ConsiderarTabCategoriaDato = considerarTabCategoriaDato,
                    ConsiderarTabInteraccionOfflineOnline = considerarTabInteraccionOfflineOnline,
                    ConsiderarTabInteraccionSitioWeb = considerarTabInteraccionSitioWeb,
                    ConsiderarTabInteraccionFormularios = considerarTabInteraccionFormularios,
                    ConsiderarTabInteraccionChatPw = considerarTabInteraccionChatPw,
                    ConsiderarTabInteraccionCorreo = considerarTabInteraccionCorreo,
                    ConsiderarTabHistorialFinanciero = considerarTabHistorialFinanciero,
                    ConsiderarTabInteraccionWhatsApp = considerarTabInteraccionWhatsApp,
                    ConsiderarTabInteraccionChatMessenger = considerarTabInteraccionChatMessenger,
                    ConsiderarTabEnvioAutomatico = considerarTabEnvioAutomatico,

                    ListadoActividadLlamada = listadoActividadLlamada,

                    FechaInicioLlamada = fechaInicioLlamada,
                    FechaFinLlamada = fechaFinLlamada,

                    IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad = idOperadorComparacionDuracionPromedioLlamadaPorOportunidad,
                    DuracionPromedioLlamadaPorOportunidad = duracionPromedioLlamadaPorOportunidad,
                    IdOperadorComparacionDuracionTotalLlamadaPorOportunidad = idOperadorComparacionDuracionTotalLlamadaPorOportunidad,
                    DuracionTotalLlamadaPorOportunidad = duracionTotalLlamadaPorOportunidad,
                    IdOperadorComparacionNroLlamada = idOperadorComparacionNroLlamada,
                    NroLlamada = nroLlamada,
                    IdOperadorComparacionDuracionLlamada = idOperadorComparacionDuracionLlamada,
                    DuracionLlamada = duracionLlamada,
                    IdOperadorComparacionTasaEjecucionLlamada = idOperadorComparacionTasaEjecucionLlamada,
                    TasaEjecucionLlamada = tasaEjecucionLlamada,

                    IdOperadorComparacionNroTotalLineaCreditoVigente = idOperadorComparacionNroTotalLineaCreditoVigente,
                    NroTotalLineaCreditoVigente = nroTotalLineaCreditoVigente,
                    IdOperadorComparacionMontoTotalLineaCreditoVigente = idOperadorComparacionMontoTotalLineaCreditoVigente,
                    MontoTotalLineaCreditoVigente = montoTotalLineaCreditoVigente,
                    IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente = idOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente,
                    MontoMaximoOtorgadoLineaCreditoVigente = montoMaximoOtorgadoLineaCreditoVigente,
                    IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente = idOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente,
                    MontoMinimoOtorgadoLineaCreditoVigente = montoMinimoOtorgadoLineaCreditoVigente,
                    IdOperadorComparacionNroTotalLineaCreditoVigenteVencida = idOperadorComparacionNroTotalLineaCreditoVigenteVencida,
                    NroTotalLineaCreditoVigenteVencida = nroTotalLineaCreditoVigenteVencida,
                    IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida = idOperadorComparacionMontoTotalLineaCreditoVigenteVencida,
                    MontoTotalLineaCreditoVigenteVencida = montoTotalLineaCreditoVigenteVencida,
                    IdOperadorComparacionNroTcOtorgada = idOperadorComparacionNroTcOtorgada,
                    NroTcOtorgada = nroTcOtorgada,
                    IdOperadorComparacionMontoTotalOtorgadoEnTcs = idOperadorComparacionMontoTotalOtorgadoEnTcs,
                    MontoTotalOtorgadoEnTcs = montoTotalOtorgadoEnTcs,

                    IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc = idOperadorComparacionMontoMaximoOtorgadoEnUnaTc,
                    MontoMaximoOtorgadoEnUnaTc = montoMaximoOtorgadoEnUnaTc,
                    IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc = idOperadorComparacionMontoMinimoOtorgadoEnUnaTc,
                    MontoMinimoOtorgadoEnUnaTc = montoMinimoOtorgadoEnUnaTc,
                    IdOperadorComparacionMontoDisponibleTotalEnTcs = idOperadorComparacionMontoDisponibleTotalEnTcs,
                    MontoDisponibleTotalEnTcs = montoDisponibleTotalEnTcs,

                    IdPrioridadMailChimpLista = idPrioridadMailChimpLista,
                    IdCategoriaObjetoFiltro = idCategoriaObjetoFiltro,

                    FechaInicioInteraccionSitioWeb = fechaInicioInteraccionSitioWeb,
                    FechaFinInteraccionSitioWeb = fechaFinInteraccionSitioWeb,
                    IdOperadorComparacionTiempoVisualizacionTotalSitioWeb = idOperadorComparacionTiempoVisualizacionTotalSitioWeb,
                    TiempoVisualizacionTotalSitioWeb = tiempoVisualizacionTotalSitioWeb,
                    IdOperadorComparacionNroClickEnlaceTodoSitioWeb = idOperadorComparacionNroClickEnlaceTodoSitioWeb,
                    NroClickEnlaceTodoSitioWeb = nroClickEnlaceTodoSitioWeb,
                    IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma = idOperadorComparacionTiempoVisualizacionTotalPaginaPrograma,
                    TiempoVisualizacionTotalPaginaPrograma = tiempoVisualizacionTotalPaginaPrograma,
                    IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = idOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas,
                    TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = tiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas,

                    IdOperadorComparacionNroClickEnlacePaginaPrograma = idOperadorComparacionNroClickEnlacePaginaPrograma,
                    NroClickEnlacePaginaPrograma = nroClickEnlacePaginaPrograma,
                    ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma = considerarVisualizacionVideoVistaPreviaPaginaPrograma,
                    ConsiderarClickBotonMatricularmePaginaPrograma = considerarClickBotonMatricularmePaginaPrograma,
                    ConsiderarClickBotonVersionPruebaPaginaPrograma = considerarClickBotonVersionPruebaPaginaPrograma,
                    IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus = idOperadorComparacionTiempoVisualizacionTotalPaginaBscampus,
                    TiempoVisualizacionTotalPaginaBscampus = tiempoVisualizacionTotalPaginaBscampus,
                    IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = idOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus,
                    TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = tiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus,
                    IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea = idOperadorComparacionNroVisitasDirectorioTagAreaSubArea,

                    NroVisitasDirectorioTagAreaSubArea = nroVisitasDirectorioTagAreaSubArea,
                    IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea = idOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea,
                    TiempoVisualizacionTotalDirectorioTagAreaSubArea = tiempoVisualizacionTotalDirectorioTagAreaSubArea,
                    IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea = idOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea,
                    NroClickEnlaceDirectorioTagAreaSubArea = nroClickEnlaceDirectorioTagAreaSubArea,
                    IdOperadorComparacionNroVisitasPaginaMisCursos = idOperadorComparacionNroVisitasPaginaMisCursos,
                    NroVisitasPaginaMisCursos = nroVisitasPaginaMisCursos,
                    IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos = idOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos,
                    TiempoVisualizacionTotalPaginaMisCursos = tiempoVisualizacionTotalPaginaMisCursos,
                    IdOperadorComparacionNroClickEnlacePaginaMisCursos = idOperadorComparacionNroClickEnlacePaginaMisCursos,

                    NroClickEnlacePaginaMisCursos = nroClickEnlacePaginaMisCursos,
                    IdOperadorComparacionNroVisitaPaginaCursoDiplomado = idOperadorComparacionNroVisitaPaginaCursoDiplomado,
                    NroVisitaPaginaCursoDiplomado = nroVisitaPaginaCursoDiplomado,
                    IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado = idOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado,
                    TiempoVisualizacionTotalPaginaCursoDiplomado = tiempoVisualizacionTotalPaginaCursoDiplomado,
                    IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado = idOperadorComparacionNroClicksEnlacePaginaCursoDiplomado,
                    NroClicksEnlacePaginaCursoDiplomado = nroClicksEnlacePaginaCursoDiplomado,
                    ConsiderarClickFiltroPaginaCursoDiplomado = considerarClickFiltroPaginaCursoDiplomado,

                    CantidadPeriodoSinRecibirCorreo = cantidadPeriodoSinRecibirCorreo,
                    TipoPeriodoSinRecibirCorreo = tipoPeriodoSinRecibirCorreo,
                    IdProbabilidadOportunidad = idProbabilidadOportunidad,
                    NumeroSegmento = numeroSegmento,
                    TipoAsociacion = tipoAsociacion,
                    CampaniaGeneral = idCampaniaGeneral,

                    ListadoVCAreaCapacitacion = listadoVCAreaCapacitacion,
                    ListadoVCSubAreaCapacitacion = listadoVCSubAreaCapacitacion,

                    ListadoProbabilidadVentaCruzada = listadoProbabilidadVentaCruzada,

                    //nuevos filtros
                    IdOperadorComparacionNroSolicitudInformacionPg = idOperadorComparacionNroSolicitudInformacionPg,
                    NroSolicitudInformacionPg = nroSolicitudInformacionPg,
                    IdOperadorComparacionNroSolicitudInformacionArea = idOperadorComparacionNroSolicitudInformacionArea,
                    NroSolicitudInformacionArea = nroSolicitudInformacionArea,
                    IdOperadorComparacionNroSolicitudInformacionSubArea = idOperadorComparacionNroSolicitudInformacionSubArea,
                    NroSolicitudInformacionSubArea = nroSolicitudInformacionSubArea,
                };

                var listadoFiltroSegmentoDB = _dapper.QuerySPFirstOrDefault("mkt.SP_EjecutarFiltroSegmentoMailingGeneral", parametros);
                var respuesta = JsonConvert.DeserializeObject<Dictionary<string, int>>(listadoFiltroSegmentoDB);
                return respuesta.Select(x => x.Value).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Ejecuta el filtro y guarda sus registros en la tabla de mailchimp correos para ser enviados por la plataforma d emailchimp
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int EjecutarFiltroTipoContactoProspectoMailchimp(FiltroSegmentoDTO obj)
        {
            try
            {
                var Id = obj.Id;
                var ListadoAreaCapacitacion = string.Join(",", obj.ListaArea.Select(x => x.Valor));
                var ListadoSubAreaCapacitacion = string.Join(",", obj.ListaSubArea.Select(x => x.Valor));
                var ListadoPGeneral = string.Join(",", obj.ListaProgramaGeneral.Select(x => x.Valor));
                var ListadoPEspecifico = string.Join(",", obj.ListaProgramaEspecifico.Select(x => x.Valor));

                var IdOperadorComparacionNroSolicitudInformacion = obj.IdOperadorComparacionNroSolicitudInformacion;
                var NroSolicitudInformacion = obj.NroSolicitudInformacion;
                var IdOperadorComparacionNroOportunidades = obj.IdOperadorComparacionNroOportunidades;
                var NroOportunidades = obj.NroOportunidades;

                var EsRN2 = obj.EsRn2;
                DateTime? FechaInicioProgramacionUltimaActividadDetalleRn2 = null;
                DateTime? FechaFinProgramacionUltimaActividadDetalleRn2 = null;

                if (EsRN2)
                {
                    FechaInicioProgramacionUltimaActividadDetalleRn2 = obj.FechaInicioProgramacionUltimaActividadDetalleRn2.Value.Date;
                    FechaFinProgramacionUltimaActividadDetalleRn2 = obj.FechaFinProgramacionUltimaActividadDetalleRn2.Value.Date;
                }

                var IdOperadorComparacionNroSolicitudInformacionPg = obj.IdOperadorComparacionNroSolicitudInformacionPg;
                var NroSolicitudInformacionPg = obj.NroSolicitudInformacionPg;
                var IdOperadorComparacionNroSolicitudInformacionArea = obj.IdOperadorComparacionNroSolicitudInformacionArea;
                var NroSolicitudInformacionArea = obj.NroSolicitudInformacionArea;
                var IdOperadorComparacionNroSolicitudInformacionSubArea = obj.IdOperadorComparacionNroSolicitudInformacionSubArea;
                var NroSolicitudInformacionSubArea = obj.NroSolicitudInformacionSubArea;

                var FechaInicioCreacionUltimaOportunidad = obj.FechaInicioCreacionUltimaOportunidad;
                var FechaFinCreacionUltimaOportunidad = obj.FechaFinCreacionUltimaOportunidad;

                var FechaInicioModificacionUltimaActividadDetalle = obj.FechaInicioModificacionUltimaActividadDetalle;
                var FechaFinModificacionUltimaActividadDetalle = obj.FechaFinModificacionUltimaActividadDetalle;

                var ListadoOportunidadInicialFaseMaxima = string.Join(",", obj.ListaOportunidadInicialFaseMaxima.Select(x => x.Valor));
                var ListadoOportunidadInicialFaseActual = string.Join(",", obj.ListaOportunidadInicialFaseActual.Select(x => x.Valor));
                var ListadoOportunidadActualFaseMaxima = string.Join(",", obj.ListaOportunidadActualFaseMaxima.Select(x => x.Valor));
                var ListadoOportunidadActualFaseActual = string.Join(",", obj.ListaOportunidadActualFaseActual.Select(x => x.Valor));

                var ListadoPais = string.Join(",", obj.ListaPais.Select(x => x.Valor));
                var ListadoCiudad = string.Join(",", obj.ListaCiudad.Select(x => x.Valor));

                var ListadoTipoCategoriaOrigen = string.Join(",", obj.ListaTipoCategoriaOrigen.Select(x => x.Valor));
                var ListadoCategoriaOrigen = string.Join(",", obj.ListaCategoriaOrigen.Select(x => x.Valor));

                var ListadoCargo = string.Join(",", obj.ListaCargo.Select(x => x.Valor));
                var ListadoIndustria = string.Join(",", obj.ListaIndustria.Select(x => x.Valor));
                var ListadoAreaFormacion = string.Join(",", obj.ListaAreaFormacion.Select(x => x.Valor));
                var ListadoAreaTrabajo = string.Join(",", obj.ListaAreaTrabajo.Select(x => x.Valor));

                var FechaInicioFormulario = obj.FechaInicioFormulario;
                var FechaFinFormulario = obj.FechaFinFormulario;
                var ListadoTipoFormulario = string.Join(",", obj.ListaTipoFormulario.Select(x => x.Valor));
                var ListadoTipoInteraccionFormulario = string.Join(",", obj.ListaTipoInteraccionFormulario.Select(x => x.Valor));

                var ListadoProbabilidadOportunidad = string.Join(",", obj.ListaProbabilidadOportunidad.Select(x => x.Valor));
                var ListadoActividadLlamada = string.Join(",", obj.ListaActividadLlamada.Select(x => x.Valor));

                var FechaInicioChatIntegra = obj.FechaInicioChatIntegra;
                var FechaFinChatIntegra = obj.FechaFinChatIntegra;
                var IdOperadorComparacionTiempoMaximoRespuestaChatOnline = obj.IdOperadorComparacionTiempoMaximoRespuestaChatOnline;
                var TiempoMaximoRespuestaChatOnline = obj.TiempoMaximoRespuestaChatOnline;
                var IdOperadorComparacionNroPalabrasClienteChatOnline = obj.IdOperadorComparacionNroPalabrasClienteChatOnline;
                var NroPalabrasClienteChatOnline = obj.NroPalabrasClienteChatOnline;
                var IdOperadorComparacionTiempoPromedioRespuestaChatOnline = obj.IdOperadorComparacionTiempoPromedioRespuestaChatOnline;
                var TiempoPromedioRespuestaChatOnline = obj.TiempoPromedioRespuestaChatOnline;
                var IdOperadorComparacionNroPalabrasClienteChatOffline = obj.IdOperadorComparacionNroPalabrasClienteChatOffline;
                var NroPalabrasClienteChatOffline = obj.NroPalabrasClienteChatOffline;
                var ExcluirMatriculados = obj.ExcluirMatriculados;

                var FechaInicioCorreo = obj.FechaInicioCorreo;
                var FechaFinCorreo = obj.FechaFinCorreo;
                var IdOperadorComparacionNroCorreosAbiertos = obj.IdOperadorComparacionNroCorreosAbiertos;
                var NroCorreosAbiertos = obj.NroCorreosAbiertos;
                var IdOperadorComparacionNroCorreosNoAbiertos = obj.IdOperadorComparacionNroCorreosNoAbiertos;
                var NroCorreosNoAbiertos = obj.NroCorreosNoAbiertos;
                var IdOperadorComparacionNroClicksEnlace = obj.IdOperadorComparacionNroClicksEnlace;
                var NroClicksEnlace = obj.NroClicksEnlace;
                var EsSuscribirme = obj.EsSuscribirme;
                var EsDesuscribirme = obj.EsDesuscribirme;

                var IdOperadorComparacionNroCorreosAbiertosMailChimp = obj.IdOperadorComparacionNroCorreosAbiertosMailChimp;
                var NroCorreosAbiertosMailChimp = obj.NroCorreosAbiertosMailChimp;
                var IdOperadorComparacionNroCorreosNoAbiertosMailChimp = obj.IdOperadorComparacionNroCorreosNoAbiertosMailChimp;
                var NroCorreosNoAbiertosMailChimp = obj.NroCorreosNoAbiertosMailChimp;
                var IdOperadorComparacionNroClicksEnlaceMailChimp = obj.IdOperadorComparacionNroClicksEnlaceMailChimp;
                var NroClicksEnlaceMailChimp = obj.NroClicksEnlaceMailChimp;

                var ConsiderarFiltroGeneral = obj.ConsiderarFiltroGeneral;
                var ConsiderarFiltroEspecifico = obj.ConsiderarFiltroEspecifico;
                var TieneVentaCruzada = obj.TieneVentaCruzada;

                var FechaInicioLlamada = obj.FechaInicioLlamada;
                var FechaFinLlamada = obj.FechaFinLlamada;

                var IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad = obj.IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad;
                var DuracionPromedioLlamadaPorOportunidad = obj.DuracionPromedioLlamadaPorOportunidad;
                var IdOperadorComparacionDuracionTotalLlamadaPorOportunidad = obj.IdOperadorComparacionDuracionTotalLlamadaPorOportunidad;
                var DuracionTotalLlamadaPorOportunidad = obj.DuracionTotalLlamadaPorOportunidad;
                var IdOperadorComparacionNroLlamada = obj.IdOperadorComparacionNroLlamada;
                var NroLlamada = obj.NroLlamada;
                var IdOperadorComparacionDuracionLlamada = obj.IdOperadorComparacionDuracionLlamada;
                var DuracionLlamada = obj.DuracionLlamada;
                var IdOperadorComparacionTasaEjecucionLlamada = obj.IdOperadorComparacionTasaEjecucionLlamada;
                var TasaEjecucionLlamada = obj.TasaEjecucionLlamada;

                var IdOperadorComparacionNroTotalLineaCreditoVigente = obj.IdOperadorComparacionNroTotalLineaCreditoVigente;
                var NroTotalLineaCreditoVigente = obj.NroTotalLineaCreditoVigente;
                var IdOperadorComparacionMontoTotalLineaCreditoVigente = obj.IdOperadorComparacionMontoTotalLineaCreditoVigente;
                var MontoTotalLineaCreditoVigente = obj.MontoTotalLineaCreditoVigente;
                var IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente = obj.IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente;
                var MontoMaximoOtorgadoLineaCreditoVigente = obj.MontoMaximoOtorgadoLineaCreditoVigente;
                var IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente = obj.IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente;
                var MontoMinimoOtorgadoLineaCreditoVigente = obj.MontoMinimoOtorgadoLineaCreditoVigente;
                var IdOperadorComparacionNroTotalLineaCreditoVigenteVencida = obj.IdOperadorComparacionNroTotalLineaCreditoVigenteVencida;
                var NroTotalLineaCreditoVigenteVencida = obj.NroTotalLineaCreditoVigenteVencida;
                var IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida = obj.IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida;
                var MontoTotalLineaCreditoVigenteVencida = obj.MontoTotalLineaCreditoVigenteVencida;
                var IdOperadorComparacionNroTcOtorgada = obj.IdOperadorComparacionNroTcOtorgada;
                var NroTcOtorgada = obj.NroTcOtorgada;
                var IdOperadorComparacionMontoTotalOtorgadoEnTcs = obj.IdOperadorComparacionMontoTotalOtorgadoEnTcs;
                var MontoTotalOtorgadoEnTcs = obj.MontoTotalOtorgadoEnTcs;

                var IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc = obj.IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc;
                var MontoMaximoOtorgadoEnUnaTc = obj.MontoMaximoOtorgadoEnUnaTc;
                var IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc = obj.IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc;
                var MontoMinimoOtorgadoEnUnaTc = obj.MontoMinimoOtorgadoEnUnaTc;
                var IdOperadorComparacionMontoDisponibleTotalEnTcs = obj.IdOperadorComparacionMontoDisponibleTotalEnTcs;
                var MontoDisponibleTotalEnTcs = obj.MontoDisponibleTotalEnTcs;

                var FechaInicioInteraccionSitioWeb = obj.FechaInicioInteraccionSitioWeb;
                var FechaFinInteraccionSitioWeb = obj.FechaFinInteraccionSitioWeb;
                var IdOperadorComparacionTiempoVisualizacionTotalSitioWeb = obj.IdOperadorComparacionTiempoVisualizacionTotalSitioWeb;
                var TiempoVisualizacionTotalSitioWeb = obj.TiempoVisualizacionTotalSitioWeb;
                var IdOperadorComparacionNroClickEnlaceTodoSitioWeb = obj.IdOperadorComparacionNroClickEnlaceTodoSitioWeb;
                var NroClickEnlaceTodoSitioWeb = obj.NroClickEnlaceTodoSitioWeb;
                var IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma;
                var TiempoVisualizacionTotalPaginaPrograma = obj.TiempoVisualizacionTotalPaginaPrograma;
                var IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = obj.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas;
                var TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = obj.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas;
                var IdOperadorComparacionNroClickEnlacePaginaPrograma = obj.IdOperadorComparacionNroClickEnlacePaginaPrograma;
                var NroClickEnlacePaginaPrograma = obj.NroClickEnlacePaginaPrograma;
                var ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma = obj.ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma;
                var ConsiderarClickBotonMatricularmePaginaPrograma = obj.ConsiderarClickBotonMatricularmePaginaPrograma;
                var ConsiderarClickBotonVersionPruebaPaginaPrograma = obj.ConsiderarClickBotonVersionPruebaPaginaPrograma;
                var IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus;
                var TiempoVisualizacionTotalPaginaBscampus = obj.TiempoVisualizacionTotalPaginaBscampus;
                var IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = obj.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus;
                var TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = obj.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus;

                var IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea = obj.IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea;
                var NroVisitasDirectorioTagAreaSubArea = obj.NroVisitasDirectorioTagAreaSubArea;
                var IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea = obj.IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea;
                var TiempoVisualizacionTotalDirectorioTagAreaSubArea = obj.TiempoVisualizacionTotalDirectorioTagAreaSubArea;
                var IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea = obj.IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea;
                var NroClickEnlaceDirectorioTagAreaSubArea = obj.NroClickEnlaceDirectorioTagAreaSubArea;
                var IdOperadorComparacionNroVisitasPaginaMisCursos = obj.IdOperadorComparacionNroVisitasPaginaMisCursos;
                var NroVisitasPaginaMisCursos = obj.NroVisitasPaginaMisCursos;
                var IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos;
                var TiempoVisualizacionTotalPaginaMisCursos = obj.TiempoVisualizacionTotalPaginaMisCursos;
                var IdOperadorComparacionNroClickEnlacePaginaMisCursos = obj.IdOperadorComparacionNroClickEnlacePaginaMisCursos;
                var NroClickEnlacePaginaMisCursos = obj.NroClickEnlacePaginaMisCursos;
                var IdOperadorComparacionNroVisitaPaginaCursoDiplomado = obj.IdOperadorComparacionNroVisitaPaginaCursoDiplomado;
                var NroVisitaPaginaCursoDiplomado = obj.NroVisitaPaginaCursoDiplomado;
                var IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado = obj.IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado;
                var TiempoVisualizacionTotalPaginaCursoDiplomado = obj.TiempoVisualizacionTotalPaginaCursoDiplomado;
                var IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado = obj.IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado;
                var NroClicksEnlacePaginaCursoDiplomado = obj.NroClicksEnlacePaginaCursoDiplomado;
                var ConsiderarClickFiltroPaginaCursoDiplomado = obj.ConsiderarClickFiltroPaginaCursoDiplomado;

                var CampaniaMailing = obj.IdCampaniaMailing;
                var CampaniaMailingLista = obj.IdCampaniaMailingLista;

                var ListadoVCAreaCapacitacion = string.Join(",", obj.ListaVCArea.Select(x => x.Valor));
                var ListadoVCSubAreaCapacitacion = string.Join(",", obj.ListaVCSubArea.Select(x => x.Valor));

                var ListadoProbabilidadVentaCruzada = string.Join(",", obj.ListaProbabilidadVentaCruzada.Select(x => x.Valor));

                var FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = obj.FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;
                var FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = obj.FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;

                var ListaExcluirPorCampaniaMailing = string.Join(",", obj.ListaExcluirPorCampaniaMailing.Select(x => x.Valor));

                var ConsiderarTabOportunidadHistorica = obj.ConsiderarOportunidadHistorica;
                var ConsiderarTabCategoriaDato = obj.ConsiderarCategoriaDato;
                var ConsiderarTabInteraccionOfflineOnline = obj.ConsiderarInteraccionOfflineOnline;
                var ConsiderarTabInteraccionSitioWeb = obj.ConsiderarInteraccionSitioWeb;
                var ConsiderarTabInteraccionFormularios = obj.ConsiderarInteraccionFormularios;
                var ConsiderarTabInteraccionChatPw = obj.ConsiderarInteraccionChatPw;
                var ConsiderarTabInteraccionCorreo = obj.ConsiderarInteraccionCorreo;
                var ConsiderarTabHistorialFinanciero = obj.ConsiderarHistorialFinanciero;
                //var ExcluirMatriculados = obj.ExcluirMatriculados;

                var parametros = new
                {
                    Id,
                    ListadoAreaCapacitacion,
                    ListadoSubAreaCapacitacion,
                    ListadoPGeneral,
                    ListadoPEspecifico,

                    IdOperadorComparacionNroSolicitudInformacion,
                    NroSolicitudInformacion,
                    IdOperadorComparacionNroOportunidades,
                    NroOportunidades,

                    EsRN2,
                    FechaInicioProgramacionUltimaActividadDetalleRn2,
                    FechaFinProgramacionUltimaActividadDetalleRn2,

                    FechaInicioCreacionUltimaOportunidad,
                    FechaFinCreacionUltimaOportunidad,

                    FechaInicioModificacionUltimaActividadDetalle,
                    FechaFinModificacionUltimaActividadDetalle,

                    ListadoOportunidadInicialFaseMaxima,
                    ListadoOportunidadInicialFaseActual,
                    ListadoOportunidadActualFaseMaxima,
                    ListadoOportunidadActualFaseActual,

                    ListadoPais,
                    ListadoCiudad,

                    ListadoTipoCategoriaOrigen,
                    ListadoCategoriaOrigen,

                    ConsiderarTabOportunidadHistorica = obj.ConsiderarOportunidadHistorica,
                    ConsiderarTabCategoriaDato = obj.ConsiderarCategoriaDato,
                    ConsiderarTabInteraccionOfflineOnline = obj.ConsiderarInteraccionOfflineOnline,
                    ConsiderarTabInteraccionSitioWeb = obj.ConsiderarInteraccionSitioWeb,
                    ConsiderarTabInteraccionFormularios = obj.ConsiderarInteraccionFormularios,
                    ConsiderarTabInteraccionChatPw = obj.ConsiderarInteraccionChatPw,
                    ConsiderarTabInteraccionCorreo = obj.ConsiderarInteraccionCorreo,
                    ConsiderarTabHistorialFinanciero = obj.ConsiderarHistorialFinanciero,

                    ListadoCargo,
                    ListadoIndustria,
                    ListadoAreaFormacion,
                    ListadoAreaTrabajo,

                    FechaInicioFormulario,
                    FechaFinFormulario,
                    ListadoTipoFormulario,
                    ListadoTipoInteraccionFormulario,

                    FechaInicioChatIntegra,
                    FechaFinChatIntegra,
                    IdOperadorComparacionTiempoMaximoRespuestaChatOnline,
                    TiempoMaximoRespuestaChatOnline,
                    IdOperadorComparacionNroPalabrasClienteChatOnline,
                    NroPalabrasClienteChatOnline,
                    IdOperadorComparacionTiempoPromedioRespuestaChatOnline,
                    TiempoPromedioRespuestaChatOnline,
                    IdOperadorComparacionNroPalabrasClienteChatOffline,
                    NroPalabrasClienteChatOffline,

                    FechaInicioCorreo,
                    FechaFinCorreo,
                    IdOperadorComparacionNroCorreosAbiertos,
                    NroCorreosAbiertos,
                    IdOperadorComparacionNroCorreosNoAbiertos,
                    NroCorreosNoAbiertos,
                    IdOperadorComparacionNroClicksEnlace,
                    NroClicksEnlace,
                    EsSuscribirme,
                    EsDesuscribirme,
                    ExcluirMatriculados,

                    IdOperadorComparacionNroCorreosAbiertosMailChimp,
                    NroCorreosAbiertosMailChimp,
                    IdOperadorComparacionNroCorreosNoAbiertosMailChimp,
                    NroCorreosNoAbiertosMailChimp,
                    IdOperadorComparacionNroClicksEnlaceMailChimp,
                    NroClicksEnlaceMailChimp,

                    ConsiderarFiltroGeneral,
                    ConsiderarFiltroEspecifico,
                    TieneVentaCruzada,

                    ListadoProbabilidadOportunidad,
                    ListadoActividadLlamada,


                    FechaInicioLlamada,
                    FechaFinLlamada,

                    IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad,
                    DuracionPromedioLlamadaPorOportunidad,
                    IdOperadorComparacionDuracionTotalLlamadaPorOportunidad,
                    DuracionTotalLlamadaPorOportunidad,
                    IdOperadorComparacionNroLlamada,
                    NroLlamada,
                    IdOperadorComparacionDuracionLlamada,
                    DuracionLlamada,
                    IdOperadorComparacionTasaEjecucionLlamada,
                    TasaEjecucionLlamada,

                    IdOperadorComparacionNroTotalLineaCreditoVigente,
                    NroTotalLineaCreditoVigente,
                    IdOperadorComparacionMontoTotalLineaCreditoVigente,
                    MontoTotalLineaCreditoVigente,
                    IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente,
                    MontoMaximoOtorgadoLineaCreditoVigente,
                    IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente,
                    MontoMinimoOtorgadoLineaCreditoVigente,
                    IdOperadorComparacionNroTotalLineaCreditoVigenteVencida,
                    NroTotalLineaCreditoVigenteVencida,
                    IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida,
                    MontoTotalLineaCreditoVigenteVencida,
                    IdOperadorComparacionNroTcOtorgada,
                    NroTcOtorgada,
                    IdOperadorComparacionMontoTotalOtorgadoEnTcs,
                    MontoTotalOtorgadoEnTcs,

                    IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc,
                    MontoMaximoOtorgadoEnUnaTc,
                    IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc,
                    MontoMinimoOtorgadoEnUnaTc,
                    IdOperadorComparacionMontoDisponibleTotalEnTcs,
                    MontoDisponibleTotalEnTcs,


                    FechaInicioInteraccionSitioWeb,
                    FechaFinInteraccionSitioWeb,
                    IdOperadorComparacionTiempoVisualizacionTotalSitioWeb,
                    TiempoVisualizacionTotalSitioWeb,
                    IdOperadorComparacionNroClickEnlaceTodoSitioWeb,
                    NroClickEnlaceTodoSitioWeb,
                    IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma,
                    TiempoVisualizacionTotalPaginaPrograma,
                    IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas,
                    TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas,

                    IdOperadorComparacionNroClickEnlacePaginaPrograma,
                    NroClickEnlacePaginaPrograma,
                    ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma,
                    ConsiderarClickBotonMatricularmePaginaPrograma,
                    ConsiderarClickBotonVersionPruebaPaginaPrograma,
                    IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus,
                    TiempoVisualizacionTotalPaginaBscampus,
                    IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus,
                    TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus,
                    IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea,

                    NroVisitasDirectorioTagAreaSubArea,
                    IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea,
                    TiempoVisualizacionTotalDirectorioTagAreaSubArea,
                    IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea,
                    NroClickEnlaceDirectorioTagAreaSubArea,
                    IdOperadorComparacionNroVisitasPaginaMisCursos,
                    NroVisitasPaginaMisCursos,
                    IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos,
                    TiempoVisualizacionTotalPaginaMisCursos,
                    IdOperadorComparacionNroClickEnlacePaginaMisCursos,

                    NroClickEnlacePaginaMisCursos,
                    IdOperadorComparacionNroVisitaPaginaCursoDiplomado,
                    NroVisitaPaginaCursoDiplomado,
                    IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado,
                    TiempoVisualizacionTotalPaginaCursoDiplomado,
                    IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado,
                    NroClicksEnlacePaginaCursoDiplomado,
                    ConsiderarClickFiltroPaginaCursoDiplomado,

                    CampaniaMailing,
                    CampaniaMailingLista,

                    ListadoVCAreaCapacitacion,
                    ListadoVCSubAreaCapacitacion,

                    ListadoProbabilidadVentaCruzada,

                    //nuevos filtros
                    IdOperadorComparacionNroSolicitudInformacionPg,
                    NroSolicitudInformacionPg,
                    IdOperadorComparacionNroSolicitudInformacionArea,
                    NroSolicitudInformacionArea,
                    IdOperadorComparacionNroSolicitudInformacionSubArea,
                    NroSolicitudInformacionSubArea,

                    FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal,
                    FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal,
                    ListaExcluirPorCampaniaMailing,

                    //ConsiderarTabOportunidadHistorica,
                    //ConsiderarTabCategoriaDato,
                    //ConsiderarTabInteraccionOfflineOnline,
                    //ConsiderarTabInteraccionSitioWeb ,
                    //ConsiderarTabInteraccionFormularios,
                    //ConsiderarTabInteraccionChatPw ,
                    //ConsiderarTabInteraccionCorreo,
                    //ConsiderarTabHistorialFinanciero,
                    //ExcluirMatriculados
                };

                var listadoFiltroSegmentoDB = _dapper.QuerySPFirstOrDefault("mkt.SP_EjecutarFiltroSegmentoMailchimpNuevoModelo", parametros);
                var respuesta = JsonConvert.DeserializeObject<Dictionary<string, int>>(listadoFiltroSegmentoDB);
                return respuesta.Select(x => x.Value).FirstOrDefault();


            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Obtiene el id y nombre delos filtros segmento
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltro()
        {
            try
            {
                return this.GetBy(x => x.Estado, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el id, nombre de los filtros segmento y tabs (editable)
        /// </summary>
        /// <returns></returns>
        public List<FiltroDTO> ObtenerTodoFiltroTabs()
        {
            try
            {
                return this.GetBy(x => x.Estado, x => new FiltroDTO { Id = x.Id, Nombre = x.Nombre, ConsiderarEnvioAutomatico = x.ConsiderarEnvioAutomatico }).ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Ejecuta el filtro para conjunto lista de tipo prospecto
        /// </summary>
        /// <param name="configuracionFiltro">DTO con la configuracion del filtro segmento</param>
        /// <returns>Booleano true si la ejecucion fue exitosa, caso contrario lanza una excepcion</returns>
        public bool EjecutarFiltroConjuntoListaTipoProspecto(FiltroSegmentoDTO configuracionFiltro)
        {
            try
            {
                var idFiltroSegmento = configuracionFiltro.Id;
                var listaAreaCapacitacion = string.Join(",", configuracionFiltro.ListaArea.Select(x => x.Valor));
                var listaSubAreaCapacitacion = string.Join(",", configuracionFiltro.ListaSubArea.Select(x => x.Valor));
                var listaPGeneral = string.Join(",", configuracionFiltro.ListaProgramaGeneral.Select(x => x.Valor));
                var listaPEspecifico = string.Join(",", configuracionFiltro.ListaProgramaEspecifico.Select(x => x.Valor));

                var idOperadorComparacionNroSolicitudInformacion = configuracionFiltro.IdOperadorComparacionNroSolicitudInformacion;
                var nroSolicitudInformacion = configuracionFiltro.NroSolicitudInformacion;
                var idOperadorComparacionNroOportunidades = configuracionFiltro.IdOperadorComparacionNroOportunidades;
                var nroOportunidades = configuracionFiltro.NroOportunidades;

                var esRN2 = configuracionFiltro.EsRn2;
                DateTime? fechaInicioProgramacionUltimaActividadDetalleRn2 = null;
                DateTime? fechaFinProgramacionUltimaActividadDetalleRn2 = null;

                if (esRN2)
                {
                    fechaInicioProgramacionUltimaActividadDetalleRn2 = configuracionFiltro.FechaInicioProgramacionUltimaActividadDetalleRn2.Value.Date;
                    fechaFinProgramacionUltimaActividadDetalleRn2 = configuracionFiltro.FechaFinProgramacionUltimaActividadDetalleRn2.Value.Date;
                }

                var idOperadorComparacionNroSolicitudInformacionPg = configuracionFiltro.IdOperadorComparacionNroSolicitudInformacionPg;
                var nroSolicitudInformacionPg = configuracionFiltro.NroSolicitudInformacionPg;
                var idOperadorComparacionNroSolicitudInformacionArea = configuracionFiltro.IdOperadorComparacionNroSolicitudInformacionArea;
                var nroSolicitudInformacionArea = configuracionFiltro.NroSolicitudInformacionArea;
                var idOperadorComparacionNroSolicitudInformacionSubArea = configuracionFiltro.IdOperadorComparacionNroSolicitudInformacionSubArea;
                var nroSolicitudInformacionSubArea = configuracionFiltro.NroSolicitudInformacionSubArea;

                var fechaInicioCreacionUltimaOportunidad = configuracionFiltro.FechaInicioCreacionUltimaOportunidad;
                var fechaFinCreacionUltimaOportunidad = configuracionFiltro.FechaFinCreacionUltimaOportunidad;

                var fechaInicioModificacionUltimaActividadDetalle = configuracionFiltro.FechaInicioModificacionUltimaActividadDetalle;
                var fechaFinModificacionUltimaActividadDetalle = configuracionFiltro.FechaFinModificacionUltimaActividadDetalle;

                var listaOportunidadInicialFaseMaxima = string.Join(",", configuracionFiltro.ListaOportunidadInicialFaseMaxima.Select(x => x.Valor));
                var listaOportunidadInicialFaseActual = string.Join(",", configuracionFiltro.ListaOportunidadInicialFaseActual.Select(x => x.Valor));
                var listaOportunidadActualFaseMaxima = string.Join(",", configuracionFiltro.ListaOportunidadActualFaseMaxima.Select(x => x.Valor));
                var listaOportunidadActualFaseActual = string.Join(",", configuracionFiltro.ListaOportunidadActualFaseActual.Select(x => x.Valor));
                var listaEnvioAutomaticoOportunidadFaseActual = string.Join(",", configuracionFiltro.ListaEnvioAutomaticoOportunidadFaseActual.Select(x => x.Valor));

                var listaPais = string.Join(",", configuracionFiltro.ListaPais.Select(x => x.Valor));
                var listaCiudad = string.Join(",", configuracionFiltro.ListaCiudad.Select(x => x.Valor));

                var listaTipoCategoriaOrigen = string.Join(",", configuracionFiltro.ListaTipoCategoriaOrigen.Select(x => x.Valor));
                var listaCategoriaOrigen = string.Join(",", configuracionFiltro.ListaCategoriaOrigen.Select(x => x.Valor));


                var listaCargo = string.Join(",", configuracionFiltro.ListaCargo.Select(x => x.Valor));
                var listaIndustria = string.Join(",", configuracionFiltro.ListaIndustria.Select(x => x.Valor));
                var listaAreaFormacion = string.Join(",", configuracionFiltro.ListaAreaFormacion.Select(x => x.Valor));
                var listaAreaTrabajo = string.Join(",", configuracionFiltro.ListaAreaTrabajo.Select(x => x.Valor));

                var fechaInicioFormulario = configuracionFiltro.FechaInicioFormulario;
                var fechaFinFormulario = configuracionFiltro.FechaFinFormulario;
                var listaTipoFormulario = string.Join(",", configuracionFiltro.ListaTipoFormulario.Select(x => x.Valor));
                var listaTipoInteraccionFormulario = string.Join(",", configuracionFiltro.ListaTipoInteraccionFormulario.Select(x => x.Valor));

                var listaProbabilidadOportunidad = string.Join(",", configuracionFiltro.ListaProbabilidadOportunidad.Select(x => x.Valor));
                var listaActividadLlamada = string.Join(",", configuracionFiltro.ListaActividadLlamada.Select(x => x.Valor));

                var fechaInicioChatIntegra = configuracionFiltro.FechaInicioChatIntegra;
                var fechaFinChatIntegra = configuracionFiltro.FechaFinChatIntegra;
                var idOperadorComparacionTiempoMaximoRespuestaChatOnline = configuracionFiltro.IdOperadorComparacionTiempoMaximoRespuestaChatOnline;
                var tiempoMaximoRespuestaChatOnline = configuracionFiltro.TiempoMaximoRespuestaChatOnline;
                var idOperadorComparacionNroPalabrasClienteChatOnline = configuracionFiltro.IdOperadorComparacionNroPalabrasClienteChatOnline;
                var nroPalabrasClienteChatOnline = configuracionFiltro.NroPalabrasClienteChatOnline;
                var idOperadorComparacionTiempoPromedioRespuestaChatOnline = configuracionFiltro.IdOperadorComparacionTiempoPromedioRespuestaChatOnline;
                var tiempoPromedioRespuestaChatOnline = configuracionFiltro.TiempoPromedioRespuestaChatOnline;
                var idOperadorComparacionNroPalabrasClienteChatOffline = configuracionFiltro.IdOperadorComparacionNroPalabrasClienteChatOffline;
                var nroPalabrasClienteChatOffline = configuracionFiltro.NroPalabrasClienteChatOffline;

                var fechaInicioCorreo = configuracionFiltro.FechaInicioCorreo;
                var fechaFinCorreo = configuracionFiltro.FechaFinCorreo;
                var idOperadorComparacionNroCorreosAbiertos = configuracionFiltro.IdOperadorComparacionNroCorreosAbiertos;
                var nroCorreosAbiertos = configuracionFiltro.NroCorreosAbiertos;
                var idOperadorComparacionNroCorreosNoAbiertos = configuracionFiltro.IdOperadorComparacionNroCorreosNoAbiertos;
                var nroCorreosNoAbiertos = configuracionFiltro.NroCorreosNoAbiertos;
                var idOperadorComparacionNroClicksEnlace = configuracionFiltro.IdOperadorComparacionNroClicksEnlace;
                var nroClicksEnlace = configuracionFiltro.NroClicksEnlace;
                var esSuscribirme = configuracionFiltro.EsSuscribirme;
                var esDesuscribirme = configuracionFiltro.EsDesuscribirme;

                var idOperadorComparacionNroCorreosAbiertosMailChimp = configuracionFiltro.IdOperadorComparacionNroCorreosAbiertosMailChimp;
                var nroCorreosAbiertosMailChimp = configuracionFiltro.NroCorreosAbiertosMailChimp;
                var idOperadorComparacionNroCorreosNoAbiertosMailChimp = configuracionFiltro.IdOperadorComparacionNroCorreosNoAbiertosMailChimp;
                var nroCorreosNoAbiertosMailChimp = configuracionFiltro.NroCorreosNoAbiertosMailChimp;
                var idOperadorComparacionNroClicksEnlaceMailChimp = configuracionFiltro.IdOperadorComparacionNroClicksEnlaceMailChimp;
                var nroClicksEnlaceMailChimp = configuracionFiltro.NroClicksEnlaceMailChimp;

                var considerarFiltroGeneral = configuracionFiltro.ConsiderarFiltroGeneral;
                var considerarFiltroEspecifico = configuracionFiltro.ConsiderarFiltroEspecifico;
                var tieneVentaCruzada = configuracionFiltro.TieneVentaCruzada;

                var fechaInicioLlamada = configuracionFiltro.FechaInicioLlamada;
                var fechaFinLlamada = configuracionFiltro.FechaFinLlamada;

                var idOperadorComparacionDuracionPromedioLlamadaPorOportunidad = configuracionFiltro.IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad;
                var duracionPromedioLlamadaPorOportunidad = configuracionFiltro.DuracionPromedioLlamadaPorOportunidad;
                var idOperadorComparacionDuracionTotalLlamadaPorOportunidad = configuracionFiltro.IdOperadorComparacionDuracionTotalLlamadaPorOportunidad;
                var duracionTotalLlamadaPorOportunidad = configuracionFiltro.DuracionTotalLlamadaPorOportunidad;
                var idOperadorComparacionNroLlamada = configuracionFiltro.IdOperadorComparacionNroLlamada;
                var nroLlamada = configuracionFiltro.NroLlamada;
                var idOperadorComparacionDuracionLlamada = configuracionFiltro.IdOperadorComparacionDuracionLlamada;
                var duracionLlamada = configuracionFiltro.DuracionLlamada;
                var idOperadorComparacionTasaEjecucionLlamada = configuracionFiltro.IdOperadorComparacionTasaEjecucionLlamada;
                var tasaEjecucionLlamada = configuracionFiltro.TasaEjecucionLlamada;

                var idOperadorComparacionNroTotalLineaCreditoVigente = configuracionFiltro.IdOperadorComparacionNroTotalLineaCreditoVigente;
                var nroTotalLineaCreditoVigente = configuracionFiltro.NroTotalLineaCreditoVigente;
                var idOperadorComparacionMontoTotalLineaCreditoVigente = configuracionFiltro.IdOperadorComparacionMontoTotalLineaCreditoVigente;
                var montoTotalLineaCreditoVigente = configuracionFiltro.MontoTotalLineaCreditoVigente;
                var idOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente = configuracionFiltro.IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente;
                var montoMaximoOtorgadoLineaCreditoVigente = configuracionFiltro.MontoMaximoOtorgadoLineaCreditoVigente;
                var idOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente = configuracionFiltro.IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente;
                var montoMinimoOtorgadoLineaCreditoVigente = configuracionFiltro.MontoMinimoOtorgadoLineaCreditoVigente;
                var idOperadorComparacionNroTotalLineaCreditoVigenteVencida = configuracionFiltro.IdOperadorComparacionNroTotalLineaCreditoVigenteVencida;
                var nroTotalLineaCreditoVigenteVencida = configuracionFiltro.NroTotalLineaCreditoVigenteVencida;
                var idOperadorComparacionMontoTotalLineaCreditoVigenteVencida = configuracionFiltro.IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida;
                var montoTotalLineaCreditoVigenteVencida = configuracionFiltro.MontoTotalLineaCreditoVigenteVencida;
                var idOperadorComparacionNroTcOtorgada = configuracionFiltro.IdOperadorComparacionNroTcOtorgada;
                var nroTcOtorgada = configuracionFiltro.NroTcOtorgada;
                var idOperadorComparacionMontoTotalOtorgadoEnTcs = configuracionFiltro.IdOperadorComparacionMontoTotalOtorgadoEnTcs;
                var montoTotalOtorgadoEnTcs = configuracionFiltro.MontoTotalOtorgadoEnTcs;

                var idOperadorComparacionMontoMaximoOtorgadoEnUnaTc = configuracionFiltro.IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc;
                var montoMaximoOtorgadoEnUnaTc = configuracionFiltro.MontoMaximoOtorgadoEnUnaTc;
                var idOperadorComparacionMontoMinimoOtorgadoEnUnaTc = configuracionFiltro.IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc;
                var montoMinimoOtorgadoEnUnaTc = configuracionFiltro.MontoMinimoOtorgadoEnUnaTc;
                var idOperadorComparacionMontoDisponibleTotalEnTcs = configuracionFiltro.IdOperadorComparacionMontoDisponibleTotalEnTcs;
                var montoDisponibleTotalEnTcs = configuracionFiltro.MontoDisponibleTotalEnTcs;


                var fechaInicioInteraccionSitioWeb = configuracionFiltro.FechaInicioInteraccionSitioWeb;
                var fechaFinInteraccionSitioWeb = configuracionFiltro.FechaFinInteraccionSitioWeb;
                var idOperadorComparacionTiempoVisualizacionTotalSitioWeb = configuracionFiltro.IdOperadorComparacionTiempoVisualizacionTotalSitioWeb;
                var tiempoVisualizacionTotalSitioWeb = configuracionFiltro.TiempoVisualizacionTotalSitioWeb;
                var idOperadorComparacionNroClickEnlaceTodoSitioWeb = configuracionFiltro.IdOperadorComparacionNroClickEnlaceTodoSitioWeb;
                var nroClickEnlaceTodoSitioWeb = configuracionFiltro.NroClickEnlaceTodoSitioWeb;
                var idOperadorComparacionTiempoVisualizacionTotalPaginaPrograma = configuracionFiltro.IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma;
                var tiempoVisualizacionTotalPaginaPrograma = configuracionFiltro.TiempoVisualizacionTotalPaginaPrograma;
                var idOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = configuracionFiltro.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas;
                var tiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = configuracionFiltro.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas;
                var idOperadorComparacionNroClickEnlacePaginaPrograma = configuracionFiltro.IdOperadorComparacionNroClickEnlacePaginaPrograma;
                var nroClickEnlacePaginaPrograma = configuracionFiltro.NroClickEnlacePaginaPrograma;
                var considerarVisualizacionVideoVistaPreviaPaginaPrograma = configuracionFiltro.ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma;
                var considerarClickBotonMatricularmePaginaPrograma = configuracionFiltro.ConsiderarClickBotonMatricularmePaginaPrograma;
                var considerarClickBotonVersionPruebaPaginaPrograma = configuracionFiltro.ConsiderarClickBotonVersionPruebaPaginaPrograma;
                var idOperadorComparacionTiempoVisualizacionTotalPaginaBscampus = configuracionFiltro.IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus;
                var tiempoVisualizacionTotalPaginaBscampus = configuracionFiltro.TiempoVisualizacionTotalPaginaBscampus;
                var idOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = configuracionFiltro.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus;
                var tiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = configuracionFiltro.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus;

                var idOperadorComparacionNroVisitasDirectorioTagAreaSubArea = configuracionFiltro.IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea;
                var nroVisitasDirectorioTagAreaSubArea = configuracionFiltro.NroVisitasDirectorioTagAreaSubArea;
                var idOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea = configuracionFiltro.IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea;
                var tiempoVisualizacionTotalDirectorioTagAreaSubArea = configuracionFiltro.TiempoVisualizacionTotalDirectorioTagAreaSubArea;
                var idOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea = configuracionFiltro.IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea;
                var nroClickEnlaceDirectorioTagAreaSubArea = configuracionFiltro.NroClickEnlaceDirectorioTagAreaSubArea;
                var idOperadorComparacionNroVisitasPaginaMisCursos = configuracionFiltro.IdOperadorComparacionNroVisitasPaginaMisCursos;
                var nroVisitasPaginaMisCursos = configuracionFiltro.NroVisitasPaginaMisCursos;
                var idOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos = configuracionFiltro.IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos;
                var tiempoVisualizacionTotalPaginaMisCursos = configuracionFiltro.TiempoVisualizacionTotalPaginaMisCursos;
                var idOperadorComparacionNroClickEnlacePaginaMisCursos = configuracionFiltro.IdOperadorComparacionNroClickEnlacePaginaMisCursos;
                var nroClickEnlacePaginaMisCursos = configuracionFiltro.NroClickEnlacePaginaMisCursos;
                var idOperadorComparacionNroVisitaPaginaCursoDiplomado = configuracionFiltro.IdOperadorComparacionNroVisitaPaginaCursoDiplomado;
                var nroVisitaPaginaCursoDiplomado = configuracionFiltro.NroVisitaPaginaCursoDiplomado;
                var idOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado = configuracionFiltro.IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado;
                var tiempoVisualizacionTotalPaginaCursoDiplomado = configuracionFiltro.TiempoVisualizacionTotalPaginaCursoDiplomado;
                var idOperadorComparacionNroClicksEnlacePaginaCursoDiplomado = configuracionFiltro.IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado;
                var nroClicksEnlacePaginaCursoDiplomado = configuracionFiltro.NroClicksEnlacePaginaCursoDiplomado;
                var considerarClickFiltroPaginaCursoDiplomado = configuracionFiltro.ConsiderarClickFiltroPaginaCursoDiplomado;

                var nroMedidaTiempoCreacionOportunidad = configuracionFiltro.NroMedidaTiempoCreacionOportunidad;
                var idOperadorMedidaTiempoCreacionOportunidad = configuracionFiltro.IdOperadorMedidaTiempoCreacionOportunidad;
                var nroMedidaTiempoUltimaActividadEjecutada = configuracionFiltro.NroMedidaTiempoUltimaActividadEjecutada;
                var idOperadorMedidaTiempoUltimaActividadEjecutada = configuracionFiltro.IdOperadorMedidaTiempoUltimaActividadEjecutada;
                var envioAutomaticoEstadoActividadDetalle = configuracionFiltro.EnvioAutomaticoEstadoActividadDetalle;
                var considerarYaEnviados = configuracionFiltro.ConsiderarEnvioAutomatico;
                var excluirMatriculados = configuracionFiltro.ExcluirMatriculados;
                var aplicaSobreCreacionOportunidad = configuracionFiltro.AplicaSobreCreacionOportunidad;
                var aplicaSobreUltimaActividad = configuracionFiltro.AplicaSobreUltimaActividad;

                var listaVCAreaCapacitacion = string.Join(",", configuracionFiltro.ListaVCArea.Select(x => x.Valor));
                var listaVCSubAreaCapacitacion = string.Join(",", configuracionFiltro.ListaVCSubArea.Select(x => x.Valor));
                var listaProbabilidadVentaCruzada = string.Join(",", configuracionFiltro.ListaProbabilidadVentaCruzada.Select(x => x.Valor));

                var idConjuntoListaDetalle = configuracionFiltro.IdConjuntoListaDetalle;
                var nroListasRepeticionContacto = configuracionFiltro.NroListasRepeticionContacto;
                var nroEjecucion = configuracionFiltro.NroEjecucion;

                var considerarTabOportunidadHistorica = configuracionFiltro.ConsiderarOportunidadHistorica;
                var considerarTabCategoriaDato = configuracionFiltro.ConsiderarCategoriaDato;
                var considerarTabInteraccionOfflineOnline = configuracionFiltro.ConsiderarInteraccionOfflineOnline;
                var considerarTabInteraccionSitioWeb = configuracionFiltro.ConsiderarInteraccionSitioWeb;
                var considerarTabInteraccionFormularios = configuracionFiltro.ConsiderarInteraccionFormularios;
                var considerarTabInteraccionChatPw = configuracionFiltro.ConsiderarInteraccionChatPw;
                var considerarTabInteraccionCorreo = configuracionFiltro.ConsiderarInteraccionCorreo;
                var considerarTabHistorialFinanciero = configuracionFiltro.ConsiderarHistorialFinanciero;
                var considerarTabInteraccionWhatsApp = configuracionFiltro.ConsiderarInteraccionWhatsApp;
                var considerarTabInteraccionChatMessenger = configuracionFiltro.ConsiderarInteraccionChatMessenger;
                var considerarTabEnvioAutomatico = configuracionFiltro.ConsiderarEnvioAutomatico;

                var excluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = configuracionFiltro.ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;
                var fechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = configuracionFiltro.FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;
                var fechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = configuracionFiltro.FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;

                var considerarConMessengerValido = configuracionFiltro.ConsiderarConMessengerValido;
                var considerarConWhatsAppValido = configuracionFiltro.ConsiderarConWhatsAppValido;
                var considerarConEmailValido = configuracionFiltro.ConsiderarConEmailValido;

                var nombreUsuario = configuracionFiltro.NombreUsuario;

                var parametros = new
                {
                    IdFiltroSegmento = idFiltroSegmento,
                    ListaAreaCapacitacion = listaAreaCapacitacion,
                    ListaSubAreaCapacitacion = listaSubAreaCapacitacion,
                    ListaPGeneral = listaPGeneral,
                    ListaPEspecifico = listaPEspecifico,

                    IdOperadorComparacionNroSolicitudInformacion = idOperadorComparacionNroSolicitudInformacion,
                    NroSolicitudInformacion = nroSolicitudInformacion,
                    IdOperadorComparacionNroOportunidades = idOperadorComparacionNroOportunidades,
                    NroOportunidades = nroOportunidades,

                    EsRN2 = esRN2,
                    FechaInicioProgramacionUltimaActividadDetalleRn2 = fechaInicioProgramacionUltimaActividadDetalleRn2,
                    FechaFinProgramacionUltimaActividadDetalleRn2 = fechaFinProgramacionUltimaActividadDetalleRn2,

                    FechaInicioCreacionUltimaOportunidad = fechaInicioCreacionUltimaOportunidad,
                    FechaFinCreacionUltimaOportunidad = fechaFinCreacionUltimaOportunidad,

                    FechaInicioModificacionUltimaActividadDetalle = fechaInicioModificacionUltimaActividadDetalle,
                    FechaFinModificacionUltimaActividadDetalle = fechaFinModificacionUltimaActividadDetalle,

                    ListaOportunidadInicialFaseMaxima = listaOportunidadInicialFaseMaxima,
                    ListaOportunidadInicialFaseActual = listaOportunidadInicialFaseActual,
                    ListaOportunidadActualFaseMaxima = listaOportunidadActualFaseMaxima,
                    ListaOportunidadActualFaseActual = listaOportunidadActualFaseActual,
                    ListaEnvioAutomaticoOportunidadFaseActual = listaEnvioAutomaticoOportunidadFaseActual,

                    ListaPais = listaPais,
                    ListaCiudad = listaCiudad,

                    ListaTipoCategoriaOrigen = listaTipoCategoriaOrigen,
                    ListaCategoriaOrigen = listaCategoriaOrigen,

                    ListaCargo = listaCargo,
                    ListaIndustria = listaIndustria,
                    ListaAreaFormacion = listaAreaFormacion,
                    ListaAreaTrabajo = listaAreaTrabajo,

                    FechaInicioFormulario = fechaInicioFormulario,
                    FechaFinFormulario = fechaFinFormulario,
                    ListaTipoFormulario = listaTipoFormulario,
                    ListaTipoInteraccionFormulario = listaTipoInteraccionFormulario,

                    FechaInicioChatIntegra = fechaInicioChatIntegra,
                    FechaFinChatIntegra = fechaFinChatIntegra,
                    IdOperadorComparacionTiempoMaximoRespuestaChatOnline = idOperadorComparacionTiempoMaximoRespuestaChatOnline,
                    TiempoMaximoRespuestaChatOnline = tiempoMaximoRespuestaChatOnline,
                    IdOperadorComparacionNroPalabrasClienteChatOnline = idOperadorComparacionNroPalabrasClienteChatOnline,
                    NroPalabrasClienteChatOnline = nroPalabrasClienteChatOnline,
                    IdOperadorComparacionTiempoPromedioRespuestaChatOnline = idOperadorComparacionTiempoPromedioRespuestaChatOnline,
                    TiempoPromedioRespuestaChatOnline = tiempoPromedioRespuestaChatOnline,
                    IdOperadorComparacionNroPalabrasClienteChatOffline = idOperadorComparacionNroPalabrasClienteChatOffline,
                    NroPalabrasClienteChatOffline = nroPalabrasClienteChatOffline,

                    FechaInicioCorreo = fechaInicioCorreo,
                    FechaFinCorreo = fechaFinCorreo,
                    IdOperadorComparacionNroCorreosAbiertos = idOperadorComparacionNroCorreosAbiertos,
                    NroCorreosAbiertos = nroCorreosAbiertos,
                    IdOperadorComparacionNroCorreosNoAbiertos = idOperadorComparacionNroCorreosNoAbiertos,
                    NroCorreosNoAbiertos = nroCorreosNoAbiertos,
                    IdOperadorComparacionNroClicksEnlace = idOperadorComparacionNroClicksEnlace,
                    NroClicksEnlace = nroClicksEnlace,
                    EsSuscribirme = esSuscribirme,
                    EsDesuscribirme = esDesuscribirme,

                    IdOperadorComparacionNroCorreosAbiertosMailChimp = idOperadorComparacionNroCorreosAbiertosMailChimp,
                    NroCorreosAbiertosMailChimp = nroCorreosAbiertosMailChimp,
                    IdOperadorComparacionNroCorreosNoAbiertosMailChimp = idOperadorComparacionNroCorreosNoAbiertosMailChimp,
                    NroCorreosNoAbiertosMailChimp = nroCorreosNoAbiertosMailChimp,
                    IdOperadorComparacionNroClicksEnlaceMailChimp = idOperadorComparacionNroClicksEnlaceMailChimp,
                    NroClicksEnlaceMailChimp = nroClicksEnlaceMailChimp,

                    ConsiderarFiltroGeneral = considerarFiltroGeneral,
                    ConsiderarFiltroEspecifico = considerarFiltroEspecifico,
                    TieneVentaCruzada = tieneVentaCruzada,

                    ListaProbabilidadOportunidad = listaProbabilidadOportunidad,
                    ListaActividadLlamada = listaActividadLlamada,


                    FechaInicioLlamada = fechaInicioLlamada,
                    FechaFinLlamada = fechaFinLlamada,

                    IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad = idOperadorComparacionDuracionPromedioLlamadaPorOportunidad,
                    DuracionPromedioLlamadaPorOportunidad = duracionPromedioLlamadaPorOportunidad,
                    IdOperadorComparacionDuracionTotalLlamadaPorOportunidad = idOperadorComparacionDuracionTotalLlamadaPorOportunidad,
                    DuracionTotalLlamadaPorOportunidad = duracionTotalLlamadaPorOportunidad,
                    IdOperadorComparacionNroLlamada = idOperadorComparacionNroLlamada,
                    NroLlamada = nroLlamada,
                    IdOperadorComparacionDuracionLlamada = idOperadorComparacionDuracionLlamada,
                    DuracionLlamada = duracionLlamada,
                    IdOperadorComparacionTasaEjecucionLlamada = idOperadorComparacionTasaEjecucionLlamada,
                    TasaEjecucionLlamada = tasaEjecucionLlamada,

                    IdOperadorComparacionNroTotalLineaCreditoVigente = idOperadorComparacionNroTotalLineaCreditoVigente,
                    NroTotalLineaCreditoVigente = nroTotalLineaCreditoVigente,
                    IdOperadorComparacionMontoTotalLineaCreditoVigente = idOperadorComparacionMontoTotalLineaCreditoVigente,
                    MontoTotalLineaCreditoVigente = montoTotalLineaCreditoVigente,
                    IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente = idOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente,
                    MontoMaximoOtorgadoLineaCreditoVigente = montoMaximoOtorgadoLineaCreditoVigente,
                    IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente = idOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente,
                    MontoMinimoOtorgadoLineaCreditoVigente = montoMinimoOtorgadoLineaCreditoVigente,
                    IdOperadorComparacionNroTotalLineaCreditoVigenteVencida = idOperadorComparacionNroTotalLineaCreditoVigenteVencida,
                    NroTotalLineaCreditoVigenteVencida = nroTotalLineaCreditoVigenteVencida,
                    IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida = idOperadorComparacionMontoTotalLineaCreditoVigenteVencida,
                    MontoTotalLineaCreditoVigenteVencida = montoTotalLineaCreditoVigenteVencida,
                    IdOperadorComparacionNroTcOtorgada = idOperadorComparacionNroTcOtorgada,
                    NroTcOtorgada = nroTcOtorgada,
                    IdOperadorComparacionMontoTotalOtorgadoEnTcs = idOperadorComparacionMontoTotalOtorgadoEnTcs,
                    MontoTotalOtorgadoEnTcs = montoTotalOtorgadoEnTcs,

                    IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc = idOperadorComparacionMontoMaximoOtorgadoEnUnaTc,
                    MontoMaximoOtorgadoEnUnaTc = montoMaximoOtorgadoEnUnaTc,
                    IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc = idOperadorComparacionMontoMinimoOtorgadoEnUnaTc,
                    MontoMinimoOtorgadoEnUnaTc = montoMinimoOtorgadoEnUnaTc,
                    IdOperadorComparacionMontoDisponibleTotalEnTcs = idOperadorComparacionMontoDisponibleTotalEnTcs,
                    MontoDisponibleTotalEnTcs = montoDisponibleTotalEnTcs,


                    FechaInicioInteraccionSitioWeb = fechaInicioInteraccionSitioWeb,
                    FechaFinInteraccionSitioWeb = fechaFinInteraccionSitioWeb,
                    IdOperadorComparacionTiempoVisualizacionTotalSitioWeb = idOperadorComparacionTiempoVisualizacionTotalSitioWeb,
                    TiempoVisualizacionTotalSitioWeb = tiempoVisualizacionTotalSitioWeb,
                    IdOperadorComparacionNroClickEnlaceTodoSitioWeb = idOperadorComparacionNroClickEnlaceTodoSitioWeb,
                    NroClickEnlaceTodoSitioWeb = nroClickEnlaceTodoSitioWeb,
                    IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma = idOperadorComparacionTiempoVisualizacionTotalPaginaPrograma,
                    TiempoVisualizacionTotalPaginaPrograma = tiempoVisualizacionTotalPaginaPrograma,
                    IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = idOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas,
                    TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = tiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas,

                    IdOperadorComparacionNroClickEnlacePaginaPrograma = idOperadorComparacionNroClickEnlacePaginaPrograma,
                    NroClickEnlacePaginaPrograma = nroClickEnlacePaginaPrograma,
                    ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma = considerarVisualizacionVideoVistaPreviaPaginaPrograma,
                    ConsiderarClickBotonMatricularmePaginaPrograma = considerarClickBotonMatricularmePaginaPrograma,
                    ConsiderarClickBotonVersionPruebaPaginaPrograma = considerarClickBotonVersionPruebaPaginaPrograma,
                    IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus = idOperadorComparacionTiempoVisualizacionTotalPaginaBscampus,
                    TiempoVisualizacionTotalPaginaBscampus = tiempoVisualizacionTotalPaginaBscampus,
                    IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = idOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus,
                    TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = tiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus,
                    IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea = idOperadorComparacionNroVisitasDirectorioTagAreaSubArea,

                    NroVisitasDirectorioTagAreaSubArea = nroVisitasDirectorioTagAreaSubArea,
                    IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea = idOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea,
                    TiempoVisualizacionTotalDirectorioTagAreaSubArea = tiempoVisualizacionTotalDirectorioTagAreaSubArea,
                    IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea = idOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea,
                    NroClickEnlaceDirectorioTagAreaSubArea = nroClickEnlaceDirectorioTagAreaSubArea,
                    IdOperadorComparacionNroVisitasPaginaMisCursos = idOperadorComparacionNroVisitasPaginaMisCursos,
                    NroVisitasPaginaMisCursos = nroVisitasPaginaMisCursos,
                    IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos = idOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos,
                    TiempoVisualizacionTotalPaginaMisCursos = tiempoVisualizacionTotalPaginaMisCursos,
                    IdOperadorComparacionNroClickEnlacePaginaMisCursos = idOperadorComparacionNroClickEnlacePaginaMisCursos,

                    NroClickEnlacePaginaMisCursos = nroClickEnlacePaginaMisCursos,
                    IdOperadorComparacionNroVisitaPaginaCursoDiplomado = idOperadorComparacionNroVisitaPaginaCursoDiplomado,
                    NroVisitaPaginaCursoDiplomado = nroVisitaPaginaCursoDiplomado,
                    IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado = idOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado,
                    TiempoVisualizacionTotalPaginaCursoDiplomado = tiempoVisualizacionTotalPaginaCursoDiplomado,
                    IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado = idOperadorComparacionNroClicksEnlacePaginaCursoDiplomado,
                    NroClicksEnlacePaginaCursoDiplomado = nroClicksEnlacePaginaCursoDiplomado,
                    ConsiderarClickFiltroPaginaCursoDiplomado = considerarClickFiltroPaginaCursoDiplomado,

                    NroMedidaTiempoCreacionOportunidad = nroMedidaTiempoCreacionOportunidad,
                    IdOperadorMedidaTiempoCreacionOportunidad = idOperadorMedidaTiempoCreacionOportunidad,
                    NroMedidaTiempoUltimaActividadEjecutada = nroMedidaTiempoUltimaActividadEjecutada,
                    IdOperadorMedidaTiempoUltimaActividadEjecutada = idOperadorMedidaTiempoUltimaActividadEjecutada,
                    EnvioAutomaticoEstadoActividadDetalle = envioAutomaticoEstadoActividadDetalle,
                    ConsiderarYaEnviados = considerarYaEnviados,
                    ExcluirMatriculados = excluirMatriculados,
                    AplicaSobreCreacionOportunidad = aplicaSobreCreacionOportunidad,
                    AplicaSobreUltimaActividad = aplicaSobreUltimaActividad,

                    ListaVCAreaCapacitacion = listaVCAreaCapacitacion,
                    ListaVCSubAreaCapacitacion = listaVCSubAreaCapacitacion,

                    ListaProbabilidadVentaCruzada = listaProbabilidadVentaCruzada,

                    // Nuevos filtros
                    IdOperadorComparacionNroSolicitudInformacionPg = idOperadorComparacionNroSolicitudInformacionPg,
                    NroSolicitudInformacionPg = nroSolicitudInformacionPg,
                    IdOperadorComparacionNroSolicitudInformacionArea = idOperadorComparacionNroSolicitudInformacionArea,
                    NroSolicitudInformacionArea = nroSolicitudInformacionArea,
                    IdOperadorComparacionNroSolicitudInformacionSubArea = idOperadorComparacionNroSolicitudInformacionSubArea,
                    NroSolicitudInformacionSubArea = nroSolicitudInformacionSubArea,

                    // Conjunto lista
                    IdConjuntoListaDetalle = idConjuntoListaDetalle,
                    NroListasRepeticionContacto = nroListasRepeticionContacto,
                    NroEjecucion = nroEjecucion,
                    // Filtros tabs
                    ConsiderarTabOportunidadHistorica = considerarTabOportunidadHistorica,
                    ConsiderarTabCategoriaDato = considerarTabCategoriaDato,
                    ConsiderarTabInteraccionOfflineOnline = considerarTabInteraccionOfflineOnline,
                    ConsiderarTabInteraccionSitioWeb = considerarTabInteraccionSitioWeb,
                    ConsiderarTabInteraccionFormularios = considerarTabInteraccionFormularios,
                    ConsiderarTabInteraccionChatPw = considerarTabInteraccionChatPw,
                    ConsiderarTabInteraccionCorreo = considerarTabInteraccionCorreo,
                    ConsiderarTabHistorialFinanciero = considerarTabHistorialFinanciero,
                    ConsiderarTabInteraccionWhatsApp = considerarTabInteraccionWhatsApp,
                    ConsiderarTabInteraccionChatMessenger = considerarTabInteraccionChatMessenger,
                    ConsiderarTabEnvioAutomatico = considerarTabEnvioAutomatico,

                    ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = excluirPorCorreoEnviadoMismoProgramaGeneralPrincipal,
                    FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = fechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal,
                    FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = fechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal,

                    ConsiderarConMessengerValido = considerarConMessengerValido,
                    ConsiderarConWhatsAppValido = considerarConWhatsAppValido,
                    ConsiderarConEmailValido = considerarConEmailValido,

                    NombreUsuario = nombreUsuario
                };
                _dapper.QuerySPDapper("mkt.SP_EjecutarFiltroSegmentoConjuntoListaNuevoModelo", parametros);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        /// <summary>
        /// Ejecuta el filtro para conjunto lista de tipo Alumno/ExAlumno
        /// </summary>
        /// <param name="configuracionFiltro">DTO con la configuracion del filtro segmento</param>
        /// <returns>Booleano true si la ejecucion fue exitosa, caso contrario lanza una excepcion</returns>
        public bool EjecutarFiltroConjuntoListaTipoAlumnoExAlumno(FiltroSegmentoDTO configuracionFiltro)
        {
            try
            {
                var idFiltroSegmento = configuracionFiltro.Id;
                var idConjuntoListaDetalle = configuracionFiltro.IdConjuntoListaDetalle;
                var listaAreaCapacitacion = string.Join(",", configuracionFiltro.ListaArea.Select(x => x.Valor));
                var listaSubAreaCapacitacion = string.Join(",", configuracionFiltro.ListaSubArea.Select(x => x.Valor));
                var listaPGeneral = string.Join(",", configuracionFiltro.ListaProgramaGeneral.Select(x => x.Valor));
                var listaPEspecifico = string.Join(",", configuracionFiltro.ListaProgramaEspecifico.Select(x => x.Valor));
                var listaEstadoMatricula = string.Join(",", configuracionFiltro.ListaEstadoMatricula.Select(x => x.Valor));
                var listaSubEstadoMatricula = string.Join(",", configuracionFiltro.ListaSubEstadoMatricula.Select(x => x.Valor));
                var listaModalidadCurso = string.Join(",", configuracionFiltro.ListaModalidadCurso.Select(x => x.Valor));
                var listaPais = string.Join(",", configuracionFiltro.ListaPais.Select(x => x.Valor));
                var listaCiudad = string.Join(",", configuracionFiltro.ListaCiudad.Select(x => x.Valor));
                var listaDocumentoAlumno = string.Join(",", configuracionFiltro.ListaDocumentoAlumno.Select(x => x.Valor));
                var listaActividadCabecera = string.Join(",", configuracionFiltro.ListaActividadCabecera.Select(x => x.Valor));
                var listaOcurrencia = string.Join(",", configuracionFiltro.ListaOcurrencia.Select(x => x.Valor));
                var cantidadTiempoMatriculaAlumno = configuracionFiltro.CantidadTiempoMatriculaAlumno;
                var idTiempoFrecuenciaMatriculaAlumno = configuracionFiltro.IdTiempoFrecuenciaMatriculaAlumno;
                var fechaInicioMatriculaAlumno = configuracionFiltro.FechaInicioMatriculaAlumno;
                var fechaFinMatriculaAlumno = configuracionFiltro.FechaFinMatriculaAlumno;
                var nroEjecucion = configuracionFiltro.NroEjecucion;
                var cantidadTiempoCumpleaniosContactoDentroDe = configuracionFiltro.CantidadTiempoCumpleaniosContactoDentroDe;
                var idTiempoFrecuenciaCumpleaniosContactoDentroDe = configuracionFiltro.IdTiempoFrecuenciaCumpleaniosContactoDentroDe;
                var nombreUsuario = configuracionFiltro.NombreUsuario;

                var considerarTabEstadoSesion = configuracionFiltro.ListaSesion.Any();
                var considerarTabEstadoAvanceAcademico = configuracionFiltro.ListaEstadoAcademico.Any();
                var considerarTabEstadoPago = configuracionFiltro.ListaEstadoPago.Any();
                var considerarTabEstadoLlamadaTelefonica = configuracionFiltro.ListaEstadoLlamada.Any();
                var considerarTabEstadoSesionWebinar = configuracionFiltro.ListaSesionWebinar.Any();
                var considerarTabEstadoTrabajoAlumno = (configuracionFiltro.ListaTrabajoAlumno.Any() || configuracionFiltro.ListaTrabajoAlumnoFinal.Any()) ? true : false;
                var considerarConMessengerValido = configuracionFiltro.ConsiderarConMessengerValido;
                var considerarConWhatsAppValido = configuracionFiltro.ConsiderarConWhatsAppValido;
                var considerarConEmailValido = configuracionFiltro.ConsiderarConEmailValido;
                var listaTarifario = string.Join(",", configuracionFiltro.ListaTarifario.Select(x => x.Valor));
                var considerarAlumnosAsignacionAutomaticaOperaciones = configuracionFiltro.ConsiderarAlumnosAsignacionAutomaticaOperaciones;

                var parametros = new
                {
                    IdFiltroSegmento = idFiltroSegmento,
                    IdConjuntoListaDetalle = idConjuntoListaDetalle,
                    ListaAreaCapacitacion = listaAreaCapacitacion,
                    ListaSubAreaCapacitacion = listaSubAreaCapacitacion,
                    ListaPGeneral = listaPGeneral,
                    ListaPEspecifico = listaPEspecifico,
                    ListaEstadoMatricula = listaEstadoMatricula,
                    ListaSubEstadoMatricula = listaSubEstadoMatricula,
                    ListaModalidadCurso = listaModalidadCurso,
                    ListaPais = listaPais,
                    ListaCiudad = listaCiudad,
                    ListaDocumentoAlumno = listaDocumentoAlumno,
                    ListaActividadCabecera = listaActividadCabecera,
                    ListaOcurrencia = listaOcurrencia,
                    CantidadTiempoMatriculaAlumno = cantidadTiempoMatriculaAlumno,
                    IdTiempoFrecuenciaMatriculaAlumno = idTiempoFrecuenciaMatriculaAlumno,
                    NroEjecucion = nroEjecucion,
                    FechaInicioMatriculaAlumno = fechaInicioMatriculaAlumno,
                    FechaFinMatriculaAlumno = fechaFinMatriculaAlumno,
                    CantidadTiempoCumpleaniosContactoDentroDe = cantidadTiempoCumpleaniosContactoDentroDe,
                    IdTiempoFrecuenciaCumpleaniosContactoDentroDe = idTiempoFrecuenciaCumpleaniosContactoDentroDe,
                    NombreUsuario = nombreUsuario,
                    ConsiderarTabEstadoSesion = considerarTabEstadoSesion,
                    ConsiderarTabEstadoAvanceAcademico = considerarTabEstadoAvanceAcademico,
                    ConsiderarTabEstadoPago = considerarTabEstadoPago,
                    ConsiderarTabEstadoLlamadaTelefonica = considerarTabEstadoLlamadaTelefonica,
                    ConsiderarTabEstadoSesionWebinar = considerarTabEstadoSesionWebinar,
                    ConsiderarTabEstadoTrabajoAlumno = considerarTabEstadoTrabajoAlumno,
                    ConsiderarConMessengerValido = considerarConMessengerValido,
                    ConsiderarConWhatsAppValido = considerarConWhatsAppValido,
                    ConsiderarConEmailValido = considerarConEmailValido,
                    ListaTarifario = listaTarifario,
                    ConsiderarAlumnosAsignacionAutomaticaOperaciones = considerarAlumnosAsignacionAutomaticaOperaciones
                };

                _dapper.QuerySPDapper("mkt.SP_EjecutarFiltroSegmentoConjuntoListaTipoAlumno", parametros);

                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
