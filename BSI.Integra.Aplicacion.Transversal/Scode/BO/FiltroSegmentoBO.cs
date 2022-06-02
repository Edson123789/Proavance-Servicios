using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Marketing/FiltroSegmento
    /// Autor: Wilber Choque - Joao Benavente - Gian Miranda
    /// Fecha: 08/02/2021
    /// <summary>
    /// BO para la logica del filtro segmento
    /// </summary>
    public class FiltroSegmentoBO : BaseBO
    {
        /// Propiedades	                                                                    Significado
        /// -----------	                                                                    ------------
        /// Nombre                                                                          Nombre del filtro segmento
        /// Descripcion                                                                     Descripcion del filtro segmento
        /// IdOperadorComparacionNroCorreosAbiertosMailChimp                                Id del tipo de comparacion para Nro de Correos Abiertos MailChimp
        /// IdOperadorComparacionNroCorreosNoAbiertosMailChimp                              Id del tipo de comparacion para Nro de Correos No Abiertos MailChimp
        /// NroClicksEnlaceMailChimp                                                        Entero Nro de Clicks que se han realizado a MailChimp
        /// EsSuscribirme                                                                   Flag para indicar que abarque contactos suscritos
        /// EsDesuscribirme                                                                 Flag para indicar que abarque contactos no suscritos
        /// NroCorreosNoAbiertosMailChimp                                                   Entero Nro de correos no abiertos de MailChimp
        /// NroCorreosAbiertosMailChimp                                                     Entero Nro de correos abiertos de MailChimp
        /// IdOperadorComparacionNroClicksEnlaceMailChimp                                   Id del operaciones de comparacion de Nro de Clicks que se han realizado a enlaces MailChimp
        /// IdMigracion                                                                     Id de Migracion de V3 (Campo nulleable)
        /// IdFiltroSegmentoTipoContacto                                                    Id del tipo de contacto enlazado al filtro segmento
        /// FechaInicioCreacionUltimaOportunidad                                            Fecha de Inicio de la última creación de la oportunidad
        /// FechaFinCreacionUltimaOportunidad                                               Fecha de Fin de la última creación de la oportunidad
        /// FechaFinModificacionUltimaActividadDetalle                                      Fecha de Fin de la última modificación de Actividad Detalle
        /// FechaFinProgramacionUltimaActividadDetalleRn2                                   Fecha de Fin de la última modificación de Actividad Detalle (RN2)
        /// EsRn2                                                                           Flag para indicar si es RN2
        /// FechaInicioModificacionUltimaActividadDetalle                                   Fecha de Inicio de la última modificación de Actividad Detalle
        /// FechaInicioProgramacionUltimaActividadDetalleRn2                                Fecha de Inicio de la última modificación de Actividad Detalle (RN2)
        /// IdOperadorComparacionNroSolicitudInformacion                                    Id del operador de comparacion del Nro de solicitud de informacion
        /// NroSolicitudInformacion                                                         Entero Nro de Solicitud de informacion
        /// IdOperadorComparacionNroOportunidades                                           Id del Operador de Comparacion Nro de Oportunidades
        /// NroOportunidades                                                                Entero Nro de Oportunidades
        /// FechaInicioFormulario                                                           Fecha de Inicio de interaccion con el formulario
        /// FechaFinFormulario                                                              Fecha de Fin de interaccion con el formulario
        /// FechaInicioChatIntegra                                                          Fecha de Inicio de Chat con Integra
        /// FechaFinChatIntegra                                                             Fecha de Fin de Chat con Integra
        /// IdOperadorComparacionTiempoMaximoRespuestaChatOnline                            Id del operador comparacion tiempo maximo respuesta chat online
        /// TiempoMaximoRespuestaChatOnline                                                 Tiempo maximo respuesta en el Chat online
        /// IdOperadorComparacionNroPalabrasClienteChatOnline                               Id del operador de comparacion del Nro palabras cliente Chat Online
        /// NroPalabrasClienteChatOnline                                                    Entero Nro de palabras con el cliente chat online
        /// IdOperadorComparacionTiempoPromedioRespuestaChatOnline                          Id del operador de comparacion del tiempo promedio de respuesta Chat Online
        /// TiempoPromedioRespuestaChatOnline                                               Tiempo promedio de respuesta del chat online
        /// IdOperadorComparacionNroPalabrasClienteChatOffline                              Id del operador de comparacion del Nro de palabras del cliente Chat Offline
        /// NroPalabrasClienteChatOffline                                                   Entero Nro de palabras del cliente en el chat offline
        /// FechaInicioCorreo                                                               Fecha de Inicio del correo
        /// FechaFinCorreo                                                                  Fecha de fin del correo
        /// IdOperadorComparacionNroCorreosAbiertos                                         Id del operador de comparacion de Nro de correos abiertos
        /// NroCorreosAbiertos                                                              Entero Nro de correos abiertos
        /// IdOperadorComparacionNroCorreosNoAbiertos                                       Id del operador de comparacion Nro de correos no abiertos
        /// NroCorreosNoAbiertos                                                            Entero Nro de correos no abiertos
        /// IdOperadorComparacionNroClicksEnlace                                            Id del operador de comparacion Nro de clicks de enlace
        /// NroClicksEnlace                                                                 Nro de clicks hechos en el enlace
        /// ConsiderarFiltroGeneral                                                         Flag para determinar consideracion del filtro general
        /// ConsiderarFiltroEspecifico                                                      Flag para determinar consideracion del filtro especifico
        /// TieneVentaCruzada                                                               Flag para determinar si tiene venta cruzada
        /// IdOperadorComparacionNroTotalLineaCreditoVigente                                Id del operador de comparacion del Nro total de lineas de credito vigentes
        /// NroTotalLineaCreditoVigente                                                     Entero con el Nro total de lineas de credito vigentes
        /// IdOperadorComparacionMontoTotalLineaCreditoVigente                              Id del operador de comparacion del monto total en lineas de credito vigentes
        /// MontoTotalLineaCreditoVigente                                                   Entero Monto total en lineas de credito vigentes
        /// IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente                     Id del operador de comparacion del monto maximo otorgado en lineas de credito vigentes
        /// MontoMaximoOtorgadoLineaCreditoVigente                                          Entero con el monto maximo otorgado en lineas de credito vigentes
        /// IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente                     Id del operador de comparacion del monto minimo otorgado en lineas de credito vigentes
        /// MontoMinimoOtorgadoLineaCreditoVigente                                          Entero con el monto minimo otorgado en lineas de credito vigentes
        /// IdOperadorComparacionNroTotalLineaCreditoVigenteVencida                         Id del operador de comparacion Nro total de lineas de creditos vigentes vencidas
        /// NroTotalLineaCreditoVigenteVencida                                              Entero Nro total de lineas de creditos vigentes vencidas
        /// IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida                       Id del operador de comparacion del monto total de lineas de creditos vigentes vencidas
        /// MontoTotalLineaCreditoVigenteVencida                                            Monto total de las lineas de creditos vigentes vencidas
        /// IdOperadorComparacionNroTcOtorgada                                              Id del operador de comparacion del Nro de TC otorgada
        /// NroTcOtorgada                                                                   Entero con el numero de TC otorgada
        /// IdOperadorComparacionMontoTotalOtorgadoEnTcs                                    Id del operador de comparacion del monto total otorgado en TCs
        /// MontoTotalOtorgadoEnTcs                                                         Monto total otorgado en TCs
        /// IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc                                 Id del operador de comparacion del monto maximo otorgado en una TC
        /// MontoMaximoOtorgadoEnUnaTc                                                      Monto maximo otorgado en una TC
        /// IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc                                 Id del operador de comparacion del monto minimo otorgado en una TC
        /// MontoMinimoOtorgadoEnUnaTc                                                      Monto minimo otorgado en una TC
        /// IdOperadorComparacionMontoDisponibleTotalEnTcs                                  Id del operador de comparacion del monto disponible total en TCs
        /// MontoDisponibleTotalEnTcs                                                       Monto disponible total en TCs
        /// FechaInicioLlamada                                                              Fecha de inicio de la llamada
        /// FechaFinLlamada                                                                 Fecha de finalizacion de la llamada
        /// IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad                      Id del operador de comparacion de la duracion promedio de llamada por oportunidad
        /// DuracionPromedioLlamadaPorOportunidad                                           Duracion promedio de llamada por oportunidad
        /// IdOperadorComparacionDuracionTotalLlamadaPorOportunidad                         Id del operador de comparacion de la duracion total de llamadas por oportunidad
        /// DuracionTotalLlamadaPorOportunidad                                              Duracion total de llamadas por oportunidad
        /// IdOperadorComparacionNroLlamada                                                 Id de comparacion del numero de llamadas realizadas
        /// NroLlamada                                                                      Entero con el numero de llamadas realizadas
        /// IdOperadorComparacionDuracionLlamada                                            Id de comparacion de la duracion de llamadas
        /// DuracionLlamada                                                                 Entero con la duracion de las llamadas
        /// IdOperadorComparacionTasaEjecucionLlamada                                       Id del operador de comparacion de la tasa de ejecucion de llamadas
        /// TasaEjecucionLlamada                                                            Tasa de ejecucion de llamadas
        /// FechaInicioInteraccionSitioWeb                                                  Fecha de inicio de la interaccion del sitio web
        /// FechaFinInteraccionSitioWeb                                                     Fecha de finalizacion de la interaccion del sitio web
        /// IdOperadorComparacionTiempoVisualizacionTotalSitioWeb                           Id del operador de comparacion del tiempo de visualizacion por parte del alumno en el sitio web
        /// TiempoVisualizacionTotalSitioWeb                                                Tiempo de visualizacion por parte del alumno en el sitio web
        /// IdOperadorComparacionNroClickEnlaceTodoSitioWeb                                 Id del operador de comparacion de Nro click enlace de sitio web
        /// NroClickEnlaceTodoSitioWeb                                                      Nro click de enlace todo de sitio web
        /// IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma                     Id de operador comparacion
        /// TiempoVisualizacionTotalPaginaPrograma                                          Tiempo de visualizacion total pagina programa
        /// IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas     Id de operador de comparacion en tiempo de visualizacion maxima en una pagina web
        /// TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas                          Tiempo de visualizacion maximoa en una pagina web de programas
        /// IdOperadorComparacionNroClickEnlacePaginaPrograma                               Id de operador de comparacion en el Nro de clic de enlace de una pagina programada
        /// NroClickEnlacePaginaPrograma                                                    Nro de clicks hechos en paginas de programas
        /// ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma                           Flag para considerar la visualizacion de video vista previa de pagina programa
        /// ConsiderarClickBotonMatricularmePaginaPrograma                                  Flag para considerar los clicks en el boton de matricular de las paginas de programas
        /// ConsiderarClickBotonVersionPruebaPaginaPrograma                                 Flag para considerar los clicks en el boton de matricular en version de prueba de las paginas de programas
        /// IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus                     Id del operador de comparacion en el tiempo de visualizacion total de paginas BScampus
        /// TiempoVisualizacionTotalPaginaBscampus                                          Tiempo de visualizacion total de paginas de BScampus
        /// IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus      Id del operador de comparacion en el tiempo de visualizacion maxima en una pagina de BScampus
        /// TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus                           Tiempo de visualizacion maxima en una pagina de BScampus
        /// IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea                         Id del operador de comparacion para el Nro de visitas de un directorio y tag de area y subarea
        /// NroVisitasDirectorioTagAreaSubArea                                              Nro de visitas de un directorio y tag de area y subarea
        /// IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea           Id del operador de comparacion de tiempo de visualizacion total de un directorio y tag de area y subarea
        /// TiempoVisualizacionTotalDirectorioTagAreaSubArea                                Tiempo de visualizacion total de un directorio y tag de area y subarea
        /// IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea                     Id del operador de comparacion de Nro de Click enlace de directorio, área y subárea
        /// NroClickEnlaceDirectorioTagAreaSubArea                                          Nro de Click enlace de directorio, área y subárea
        /// IdOperadorComparacionNroVisitasPaginaMisCursos                                  Id del operador de comparacion Nro de visitas a la pagina de mis cursos
        /// NroVisitasPaginaMisCursos                                                       Nro de visitas a la pagina de mis cursos
        /// IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos                    Id del operador de comparacion del tiempo de visualizacion total a la pagina de mis cursos
        /// TiempoVisualizacionTotalPaginaMisCursos                                         Tiempo de visualizacion total de la pagina de mis cursos
        /// IdOperadorComparacionNroClickEnlacePaginaMisCursos                              Id del operador de comparacion de Nro de click al enlace de pagina de mis cursos
        /// NroClickEnlacePaginaMisCursos                                                   Nro de Click en el enlace de pagina de mis cursos
        /// IdOperadorComparacionNroVisitaPaginaCursoDiplomado                              Id del operador de comparacion de Nro Visita pagina cursos diplomado
        /// NroVisitaPaginaCursoDiplomado                                                   Nro de visita pagina de cursos diplomado
        /// IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado               Id del operador de comparacion en el tiempo de visualizacion total a la pagina de cursos diplomados
        /// TiempoVisualizacionTotalPaginaCursoDiplomado                                    Tiempo de visualizacion total a la pagina de cursos diplomados
        /// IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado                        Id del operador de comparacion Nro de clicks al enlace de la pagina de cursos de diplomados
        /// NroClicksEnlacePaginaCursoDiplomado                                             Comparacion Nro de clicks al enlace de la pagina de cursos de diplomados
        /// ConsiderarClickFiltroPaginaCursoDiplomado                                       Flag para considerar el blick en el filtro de la pagina de cursos diplomado
        /// IdOperadorComparacionNroSolicitudInformacionPg                                  Id del operador de comparacion Nro de solicitud de informacion de un programa general
        /// NroSolicitudInformacionPg                                                       Nro de solicitud de informacion de un programa general
        /// IdOperadorComparacionNroSolicitudInformacionArea                                Id del operador de comparacion Nro de solicitud informacion de un area de capacitacion
        /// NroSolicitudInformacionArea                                                     Nro de solicitud informacion de un area de capacitacion
        /// IdOperadorComparacionNroSolicitudInformacionSubArea                             Id del operador de comparacion Nro solicitud de informacion de una subarea de capacitacion
        /// NroSolicitudInformacionSubArea                                                  Nro solicitud de informacion de una subarea de capacitacion
        /// ConsiderarOportunidadHistorica                                                  Flag para determinar si se considera el tab de oportunidas historicas
        /// ConsiderarCategoriaDato                                                         Flag para determinar si se considera el tab de categoria dato
        /// ConsiderarInteraccionOfflineOnline                                              Flag para determinar si se considera el tab de interacciones offline/online
        /// ConsiderarInteraccionSitioWeb                                                   Flag para determinar si se considera el tab de interaccion de sitio web
        /// ConsiderarInteraccionFormularios                                                Flag para determinar si se considera el tab de interaccion con formularios
        /// ConsiderarInteraccionChatPw                                                     Flag para determinar si se considera el tab de interaccion de Chat Portal Web
        /// ConsiderarInteraccionCorreo                                                     Flag para determinar si se considera el tab de interaccion con los correos
        /// ConsiderarHistorialFinanciero                                                   Flag para determinar si se considera el tab de historial financiero
        /// ConsiderarInteraccionWhatsApp                                                   Flag para determinar si se considera el tab de interaccion WhatsApp
        /// ConsiderarInteraccionChatMessenger                                              Flag para determinar si se considera el tab de interaccion Chat Messenger
        /// ConsiderarEnvioAutomatico                                                       Flag para determinar si se considera el tab de envio automatico
        /// ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal                            Flag para excluir correos enviados del mismo programa general
        /// FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal                 Fecha de inicio para la exclusion de correos enviados del mismo programa general
        /// FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal                    Fecha de fin para la eclusion de correos enviados del mismo programa general
        /// AplicaSobreCreacionOportunidad                                                  Flag para determinar si se aplica sobre la creacion de la oportunidad
        /// IdOperadorMedidaTiempoCreacionOportunidad                                       Id del operador de medida de tiempo de la creacion de la oportunidad
        /// NroMedidaTiempoCreacionOportunidad                                              Nro de medida tiempo de la creacion de la oportunidad
        /// AplicaSobreUltimaActividad                                                      Flag para ver si aplica sobre la ultima actividad
        /// IdOperadorMedidaTiempoUltimaActividadEjecutada                                  Id del operador de medida de tiempo de la ultima actividad ejecutada
        /// NroMedidaTiempoUltimaActividadEjecutada                                         Nro de medida de tiempo de la ultima actividad ejecutada
        /// EnvioAutomaticoEstadoActividadDetalle                                           Envio automatico del estado de actividad detalle
        /// ConsiderarYaEnviados                                                            Flag para considerar los ya enviados
        /// IdTiempoFrecuenciaMatriculaAlumno                                               Id del tiempo de frecuencia de matricula del alumno
        /// CantidadTiempoMatriculaAlumno                                                   Cantidad de tiempo de matricula de alumno
        /// ConsiderarConMessengerValido                                                    Flag para considerar los que tengan messenger valido
        /// ConsiderarConWhatsAppValido                                                     Flag para considerar los que tengan whatsapp valido
        /// ConsiderarConEmailValido                                                        Flag para considerar los que tegan email valido
        /// IdTiempoFrecuenciaCumpleaniosContactoDentroDe                                   Id del tiempo de frecuencia de cumpleanios del contacto
        /// CantidadTiempoCumpleaniosContactoDentroDe                                       Cantidad de dias transcurrido del cumpleanios para considerar
        /// FechaInicioMatriculaAlumno                                                      Fecha de inicio de matricula de un alumnno especifico
        /// FechaFinMatriculaAlumno                                                         Fecha de finalizacion de matricula de un alumno especifico
        /// ConsiderarAlumnosAsignacionAutomaticaOperaciones                                Flag para considerar los alumnos que esten en asignacion automatica operaciones
        /// ExcluirMatriculados                                                             Flag para excluir a los alumnos con matricula activa
        /// ListaFiltroSegmentoValorTipo                                                    Lista de los valores tipo de los filtros segmentos
        /// ListaFiltroSegmentoDetalle                                                      Lista de los detalles de los filtros segmentos
        /// _repFiltroSegmentoValorTipo                                                     Repositorio de la tabla mkt.T_FiltroSegmentoValorTipo
        /// _repFiltroSegmento                                                              Repositorio de la tabla mkt.T_FiltroSegmento
        /// _repFiltroSegmentoDetalle                                                       Repositorio de la tabla mkt.T_FiltroSegmentoDetalle
        /// _repFiltroSegmentoCalculado                                                     Repositorio de la tabla mkt.T_FiltroSegmentoCalculado
        /// _integraDBContext                                                               Contexto para Entity de la DB Integra

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdOperadorComparacionNroCorreosAbiertosMailChimp { get; set; }
        public int? IdOperadorComparacionNroCorreosNoAbiertosMailChimp { get; set; }
        public int? NroClicksEnlaceMailChimp { get; set; }
        public bool EsSuscribirme { get; set; }
        public bool EsDesuscribirme { get; set; }
        public int? NroCorreosNoAbiertosMailChimp { get; set; }
        public int? NroCorreosAbiertosMailChimp { get; set; }
        public int? IdOperadorComparacionNroClicksEnlaceMailChimp { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdFiltroSegmentoTipoContacto { get; set; }
        public DateTime? FechaInicioCreacionUltimaOportunidad { get; set; }
        public DateTime? FechaFinCreacionUltimaOportunidad { get; set; }
        public DateTime? FechaFinModificacionUltimaActividadDetalle { get; set; }
        public DateTime? FechaFinProgramacionUltimaActividadDetalleRn2 { get; set; }
        public bool EsRn2 { get; set; }
        public DateTime? FechaInicioModificacionUltimaActividadDetalle { get; set; }
        public DateTime? FechaInicioProgramacionUltimaActividadDetalleRn2 { get; set; }
        public int? IdOperadorComparacionNroSolicitudInformacion { get; set; }
        public int? NroSolicitudInformacion { get; set; }
        public int? IdOperadorComparacionNroOportunidades { get; set; }
        public int? NroOportunidades { get; set; }
        public DateTime? FechaInicioFormulario { get; set; }
        public DateTime? FechaFinFormulario { get; set; }
        public DateTime? FechaInicioChatIntegra { get; set; }
        public DateTime? FechaFinChatIntegra { get; set; }
        public int? IdOperadorComparacionTiempoMaximoRespuestaChatOnline { get; set; }
        public int? TiempoMaximoRespuestaChatOnline { get; set; }
        public int? IdOperadorComparacionNroPalabrasClienteChatOnline { get; set; }
        public int? NroPalabrasClienteChatOnline { get; set; }
        public int? IdOperadorComparacionTiempoPromedioRespuestaChatOnline { get; set; }
        public int? TiempoPromedioRespuestaChatOnline { get; set; }
        public int? IdOperadorComparacionNroPalabrasClienteChatOffline { get; set; }
        public int? NroPalabrasClienteChatOffline { get; set; }
        public DateTime? FechaInicioCorreo { get; set; }
        public DateTime? FechaFinCorreo { get; set; }
        public int? IdOperadorComparacionNroCorreosAbiertos { get; set; }
        public int? NroCorreosAbiertos { get; set; }
        public int? IdOperadorComparacionNroCorreosNoAbiertos { get; set; }
        public int? NroCorreosNoAbiertos { get; set; }
        public int? IdOperadorComparacionNroClicksEnlace { get; set; }
        public int? NroClicksEnlace { get; set; }
        public bool ConsiderarFiltroGeneral { get; set; }
        public bool ConsiderarFiltroEspecifico { get; set; }
        public bool TieneVentaCruzada { get; set; }
        public int? IdOperadorComparacionNroTotalLineaCreditoVigente { get; set; }
        public int? NroTotalLineaCreditoVigente { get; set; }
        public int? IdOperadorComparacionMontoTotalLineaCreditoVigente { get; set; }
        public int? MontoTotalLineaCreditoVigente { get; set; }
        public int? IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente { get; set; }
        public int? MontoMaximoOtorgadoLineaCreditoVigente { get; set; }
        public int? IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente { get; set; }
        public int? MontoMinimoOtorgadoLineaCreditoVigente { get; set; }
        public int? IdOperadorComparacionNroTotalLineaCreditoVigenteVencida { get; set; }
        public int? NroTotalLineaCreditoVigenteVencida { get; set; }
        public int? IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida { get; set; }
        public int? MontoTotalLineaCreditoVigenteVencida { get; set; }
        public int? IdOperadorComparacionNroTcOtorgada { get; set; }
        public int? NroTcOtorgada { get; set; }
        public int? IdOperadorComparacionMontoTotalOtorgadoEnTcs { get; set; }
        public int? MontoTotalOtorgadoEnTcs { get; set; }
        public int? IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc { get; set; }
        public int? MontoMaximoOtorgadoEnUnaTc { get; set; }
        public int? IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc { get; set; }
        public int? MontoMinimoOtorgadoEnUnaTc { get; set; }
        public int? IdOperadorComparacionMontoDisponibleTotalEnTcs { get; set; }
        public int? MontoDisponibleTotalEnTcs { get; set; }
        public DateTime? FechaInicioLlamada { get; set; }
        public DateTime? FechaFinLlamada { get; set; }
        public int? IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad { get; set; }
        public int? DuracionPromedioLlamadaPorOportunidad { get; set; }
        public int? IdOperadorComparacionDuracionTotalLlamadaPorOportunidad { get; set; }
        public int? DuracionTotalLlamadaPorOportunidad { get; set; }
        public int? IdOperadorComparacionNroLlamada { get; set; }
        public int? NroLlamada { get; set; }
        public int? IdOperadorComparacionDuracionLlamada { get; set; }
        public int? DuracionLlamada { get; set; }
        public int? IdOperadorComparacionTasaEjecucionLlamada { get; set; }
        public int? TasaEjecucionLlamada { get; set; }
        public DateTime? FechaInicioInteraccionSitioWeb { get; set; }
        public DateTime? FechaFinInteraccionSitioWeb { get; set; }
        public int? IdOperadorComparacionTiempoVisualizacionTotalSitioWeb { get; set; }
        public int? TiempoVisualizacionTotalSitioWeb { get; set; }
        public int? IdOperadorComparacionNroClickEnlaceTodoSitioWeb { get; set; }
        public int? NroClickEnlaceTodoSitioWeb { get; set; }
        public int? IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma { get; set; }
        public int? TiempoVisualizacionTotalPaginaPrograma { get; set; }
        public int? IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas { get; set; }
        public int? TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas { get; set; }
        public int? IdOperadorComparacionNroClickEnlacePaginaPrograma { get; set; }
        public int? NroClickEnlacePaginaPrograma { get; set; }
        public bool? ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma { get; set; }
        public bool? ConsiderarClickBotonMatricularmePaginaPrograma { get; set; }
        public bool? ConsiderarClickBotonVersionPruebaPaginaPrograma { get; set; }
        public int? IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus { get; set; }
        public int? TiempoVisualizacionTotalPaginaBscampus { get; set; }
        public int? IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus { get; set; }
        public int? TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus { get; set; }
        public int? IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea { get; set; }
        public int? NroVisitasDirectorioTagAreaSubArea { get; set; }
        public int? IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea { get; set; }
        public int? TiempoVisualizacionTotalDirectorioTagAreaSubArea { get; set; }
        public int? IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea { get; set; }
        public int? NroClickEnlaceDirectorioTagAreaSubArea { get; set; }
        public int? IdOperadorComparacionNroVisitasPaginaMisCursos { get; set; }
        public int? NroVisitasPaginaMisCursos { get; set; }
        public int? IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos { get; set; }
        public int? TiempoVisualizacionTotalPaginaMisCursos { get; set; }
        public int? IdOperadorComparacionNroClickEnlacePaginaMisCursos { get; set; }
        public int? NroClickEnlacePaginaMisCursos { get; set; }
        public int? IdOperadorComparacionNroVisitaPaginaCursoDiplomado { get; set; }
        public int? NroVisitaPaginaCursoDiplomado { get; set; }
        public int? IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado { get; set; }
        public int? TiempoVisualizacionTotalPaginaCursoDiplomado { get; set; }
        public int? IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado { get; set; }
        public int? NroClicksEnlacePaginaCursoDiplomado { get; set; }
        public bool? ConsiderarClickFiltroPaginaCursoDiplomado { get; set; }
        public int? IdOperadorComparacionNroSolicitudInformacionPg { get; set; }
        public int? NroSolicitudInformacionPg { get; set; }
        public int? IdOperadorComparacionNroSolicitudInformacionArea { get; set; }
        public int? NroSolicitudInformacionArea { get; set; }
        public int? IdOperadorComparacionNroSolicitudInformacionSubArea { get; set; }
        public int? NroSolicitudInformacionSubArea { get; set; }
        public bool? ConsiderarOportunidadHistorica { get; set; }
        public bool? ConsiderarCategoriaDato { get; set; }
        public bool? ConsiderarInteraccionOfflineOnline { get; set; }
        public bool? ConsiderarInteraccionSitioWeb { get; set; }
        public bool? ConsiderarInteraccionFormularios { get; set; }
        public bool? ConsiderarInteraccionChatPw { get; set; }
        public bool? ConsiderarInteraccionCorreo { get; set; }
        public bool? ConsiderarHistorialFinanciero { get; set; }
        public bool? ConsiderarInteraccionWhatsApp { get; set; }
        public bool? ConsiderarInteraccionChatMessenger { get; set; }
        public bool? ConsiderarEnvioAutomatico { get; set; }
        public bool? ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal { get; set; }
        public DateTime? FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal { get; set; }
        public DateTime? FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal { get; set; }

        public bool? AplicaSobreCreacionOportunidad { get; set; }
        public int? IdOperadorMedidaTiempoCreacionOportunidad { get; set; }
        public int? NroMedidaTiempoCreacionOportunidad { get; set; }
        public bool? AplicaSobreUltimaActividad { get; set; }
        public int? IdOperadorMedidaTiempoUltimaActividadEjecutada { get; set; }
        public int? NroMedidaTiempoUltimaActividadEjecutada { get; set; }
        public int? EnvioAutomaticoEstadoActividadDetalle { get; set; }
        public bool? ConsiderarYaEnviados { get; set; }

        public int? IdTiempoFrecuenciaMatriculaAlumno { get; set; }
        public int? CantidadTiempoMatriculaAlumno { get; set; }

        public bool? ConsiderarConMessengerValido { get; set; }
        public bool? ConsiderarConWhatsAppValido { get; set; }
        public bool? ConsiderarConEmailValido { get; set; }

        public int? IdTiempoFrecuenciaCumpleaniosContactoDentroDe { get; set; }
        public int? CantidadTiempoCumpleaniosContactoDentroDe { get; set; }
        public DateTime? FechaInicioMatriculaAlumno { get; set; }
        public DateTime? FechaFinMatriculaAlumno { get; set; }
        public bool? ConsiderarAlumnosAsignacionAutomaticaOperaciones { get; set; }
        public bool? ExcluirMatriculados { get; set; }
        public List<FiltroSegmentoValorTipoBO> ListaFiltroSegmentoValorTipo { get; set; }
        public List<FiltroSegmentoDetalleBO> ListaFiltroSegmentoDetalle { get; set; }

        private FiltroSegmentoValorTipoRepositorio _repFiltroSegmentoValorTipo;
        private FiltroSegmentoRepositorio _repFiltroSegmento;
        private FiltroSegmentoDetalleRepositorio _repFiltroSegmentoDetalle;
        private FiltroSegmentoCalculadoRepositorio _repFiltroSegmentoCalculado;
        private integraDBContext _integraDBContext;

        public FiltroSegmentoBO()
        {
            _integraDBContext = new integraDBContext();
            ListaFiltroSegmentoValorTipo = new List<FiltroSegmentoValorTipoBO>();
            ListaFiltroSegmentoDetalle = new List<FiltroSegmentoDetalleBO>();

            _repFiltroSegmentoValorTipo = new FiltroSegmentoValorTipoRepositorio(_integraDBContext);
            _repFiltroSegmento = new FiltroSegmentoRepositorio(_integraDBContext);
            _repFiltroSegmentoDetalle = new FiltroSegmentoDetalleRepositorio(_integraDBContext);
            _repFiltroSegmentoCalculado = new FiltroSegmentoCalculadoRepositorio(_integraDBContext);
        }
        public FiltroSegmentoBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            ListaFiltroSegmentoValorTipo = new List<FiltroSegmentoValorTipoBO>();
            ListaFiltroSegmentoDetalle = new List<FiltroSegmentoDetalleBO>();
            _repFiltroSegmentoValorTipo = new FiltroSegmentoValorTipoRepositorio(_integraDBContext);
            _repFiltroSegmento = new FiltroSegmentoRepositorio(_integraDBContext);
            _repFiltroSegmentoDetalle = new FiltroSegmentoDetalleRepositorio(_integraDBContext);
            _repFiltroSegmentoCalculado = new FiltroSegmentoCalculadoRepositorio(_integraDBContext);
        }

        public FiltroSegmentoBO(int id, integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            ListaFiltroSegmentoValorTipo = new List<FiltroSegmentoValorTipoBO>();
            ListaFiltroSegmentoDetalle = new List<FiltroSegmentoDetalleBO>();
            _repFiltroSegmentoValorTipo = new FiltroSegmentoValorTipoRepositorio(_integraDBContext);
            _repFiltroSegmento = new FiltroSegmentoRepositorio(_integraDBContext);
            _repFiltroSegmentoDetalle = new FiltroSegmentoDetalleRepositorio(_integraDBContext);
            _repFiltroSegmentoCalculado = new FiltroSegmentoCalculadoRepositorio(_integraDBContext);

            var filtroSegmentoDB = _repFiltroSegmento.FirstById(id);

            this.Id = id;

            this.Nombre = filtroSegmentoDB.Nombre;
            this.Descripcion = filtroSegmentoDB.Descripcion;
            this.IdFiltroSegmentoTipoContacto = filtroSegmentoDB.IdFiltroSegmentoTipoContacto;
            this.FechaInicioCreacionUltimaOportunidad = filtroSegmentoDB.FechaInicioCreacionUltimaOportunidad;
            this.FechaFinCreacionUltimaOportunidad = filtroSegmentoDB.FechaFinCreacionUltimaOportunidad;
            this.FechaInicioModificacionUltimaActividadDetalle = filtroSegmentoDB.FechaInicioModificacionUltimaActividadDetalle;
            this.FechaFinModificacionUltimaActividadDetalle = filtroSegmentoDB.FechaFinModificacionUltimaActividadDetalle;
            this.FechaInicioProgramacionUltimaActividadDetalleRn2 = filtroSegmentoDB.FechaInicioProgramacionUltimaActividadDetalleRn2;
            this.FechaFinProgramacionUltimaActividadDetalleRn2 = filtroSegmentoDB.FechaFinProgramacionUltimaActividadDetalleRn2;
            this.EsRn2 = filtroSegmentoDB.EsRn2;
            this.IdOperadorComparacionNroOportunidades = filtroSegmentoDB.IdOperadorComparacionNroOportunidades;
            this.NroOportunidades = filtroSegmentoDB.NroOportunidades;
            this.IdOperadorComparacionNroSolicitudInformacion = filtroSegmentoDB.IdOperadorComparacionNroSolicitudInformacion;
            this.NroSolicitudInformacion = filtroSegmentoDB.NroSolicitudInformacion;

            //
            this.IdOperadorComparacionNroSolicitudInformacionPg = filtroSegmentoDB.IdOperadorComparacionNroSolicitudInformacionPg;
            this.NroSolicitudInformacionPg = filtroSegmentoDB.NroSolicitudInformacionPg;
            this.IdOperadorComparacionNroSolicitudInformacionArea = filtroSegmentoDB.IdOperadorComparacionNroSolicitudInformacionArea;
            this.NroSolicitudInformacionArea = filtroSegmentoDB.NroSolicitudInformacionArea;
            this.IdOperadorComparacionNroSolicitudInformacionSubArea = filtroSegmentoDB.IdOperadorComparacionNroSolicitudInformacionSubArea;
            this.NroSolicitudInformacionSubArea = filtroSegmentoDB.NroSolicitudInformacionSubArea;

            this.FechaInicioFormulario = filtroSegmentoDB.FechaInicioFormulario;
            this.FechaFinFormulario = filtroSegmentoDB.FechaFinFormulario;
            this.FechaInicioChatIntegra = filtroSegmentoDB.FechaInicioChatIntegra;
            this.FechaFinChatIntegra = filtroSegmentoDB.FechaFinChatIntegra;
            this.IdOperadorComparacionTiempoMaximoRespuestaChatOnline = filtroSegmentoDB.IdOperadorComparacionTiempoMaximoRespuestaChatOnline;
            this.TiempoMaximoRespuestaChatOnline = filtroSegmentoDB.TiempoMaximoRespuestaChatOnline;
            this.IdOperadorComparacionNroPalabrasClienteChatOnline = filtroSegmentoDB.IdOperadorComparacionNroPalabrasClienteChatOnline;
            this.NroPalabrasClienteChatOnline = filtroSegmentoDB.NroPalabrasClienteChatOnline;
            this.IdOperadorComparacionTiempoPromedioRespuestaChatOnline = filtroSegmentoDB.IdOperadorComparacionTiempoPromedioRespuestaChatOnline;
            this.TiempoPromedioRespuestaChatOnline = filtroSegmentoDB.TiempoPromedioRespuestaChatOnline;
            this.IdOperadorComparacionNroPalabrasClienteChatOffline = filtroSegmentoDB.IdOperadorComparacionNroPalabrasClienteChatOffline;
            this.NroPalabrasClienteChatOffline = filtroSegmentoDB.NroPalabrasClienteChatOffline;

            this.FechaInicioCorreo = filtroSegmentoDB.FechaInicioCorreo;
            this.FechaFinCorreo = filtroSegmentoDB.FechaFinCorreo;
            this.IdOperadorComparacionNroCorreosAbiertos = filtroSegmentoDB.IdOperadorComparacionNroCorreosAbiertos;
            this.NroCorreosAbiertos = filtroSegmentoDB.NroCorreosAbiertos;
            this.IdOperadorComparacionNroCorreosNoAbiertos = filtroSegmentoDB.IdOperadorComparacionNroCorreosNoAbiertos;
            this.NroCorreosNoAbiertos = filtroSegmentoDB.NroCorreosNoAbiertos;
            this.IdOperadorComparacionNroClicksEnlace = filtroSegmentoDB.IdOperadorComparacionNroClicksEnlace;
            this.NroClicksEnlace = filtroSegmentoDB.NroClicksEnlace;
            this.EsSuscribirme = filtroSegmentoDB.EsSuscribirme;
            this.EsDesuscribirme = filtroSegmentoDB.EsDesuscribirme;

            this.IdOperadorComparacionNroCorreosAbiertosMailChimp = filtroSegmentoDB.IdOperadorComparacionNroCorreosAbiertosMailChimp;
            this.NroCorreosAbiertosMailChimp = filtroSegmentoDB.NroCorreosAbiertosMailChimp;
            this.IdOperadorComparacionNroCorreosNoAbiertosMailChimp = filtroSegmentoDB.IdOperadorComparacionNroCorreosNoAbiertosMailChimp;
            this.NroCorreosNoAbiertosMailChimp = filtroSegmentoDB.NroCorreosNoAbiertosMailChimp;
            this.IdOperadorComparacionNroClicksEnlaceMailChimp = filtroSegmentoDB.IdOperadorComparacionNroClicksEnlaceMailChimp;
            this.NroClicksEnlaceMailChimp = filtroSegmentoDB.NroClicksEnlaceMailChimp;

            this.ConsiderarFiltroGeneral = filtroSegmentoDB.ConsiderarFiltroGeneral;
            this.ConsiderarFiltroEspecifico = filtroSegmentoDB.ConsiderarFiltroEspecifico;
            this.TieneVentaCruzada = filtroSegmentoDB.TieneVentaCruzada;
            this.IdOperadorComparacionNroTotalLineaCreditoVigente = filtroSegmentoDB.IdOperadorComparacionNroTotalLineaCreditoVigente;
            this.NroTotalLineaCreditoVigente = filtroSegmentoDB.NroTotalLineaCreditoVigente;
            this.IdOperadorComparacionMontoTotalLineaCreditoVigente = filtroSegmentoDB.IdOperadorComparacionMontoTotalLineaCreditoVigente;
            this.MontoTotalLineaCreditoVigente = filtroSegmentoDB.MontoTotalLineaCreditoVigente;
            this.IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente = filtroSegmentoDB.IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente;
            this.MontoMaximoOtorgadoLineaCreditoVigente = filtroSegmentoDB.MontoMaximoOtorgadoLineaCreditoVigente;
            this.IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente = filtroSegmentoDB.IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente;
            this.MontoMinimoOtorgadoLineaCreditoVigente = filtroSegmentoDB.MontoMinimoOtorgadoLineaCreditoVigente;
            this.IdOperadorComparacionNroTotalLineaCreditoVigenteVencida = filtroSegmentoDB.IdOperadorComparacionNroTotalLineaCreditoVigenteVencida;
            this.NroTotalLineaCreditoVigenteVencida = filtroSegmentoDB.NroTotalLineaCreditoVigenteVencida;
            this.IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida = filtroSegmentoDB.IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida;
            this.MontoTotalLineaCreditoVigenteVencida = filtroSegmentoDB.MontoTotalLineaCreditoVigenteVencida;
            this.IdOperadorComparacionNroTcOtorgada = filtroSegmentoDB.IdOperadorComparacionNroTcOtorgada;

            this.NroTcOtorgada = filtroSegmentoDB.NroTcOtorgada;
            this.IdOperadorComparacionMontoTotalOtorgadoEnTcs = filtroSegmentoDB.IdOperadorComparacionMontoTotalOtorgadoEnTcs;
            this.MontoTotalOtorgadoEnTcs = filtroSegmentoDB.MontoTotalOtorgadoEnTcs;
            this.IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc = filtroSegmentoDB.IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc;
            this.MontoMaximoOtorgadoEnUnaTc = filtroSegmentoDB.MontoMaximoOtorgadoEnUnaTc;
            this.IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc = filtroSegmentoDB.IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc;
            this.MontoMinimoOtorgadoEnUnaTc = filtroSegmentoDB.MontoMinimoOtorgadoEnUnaTc;
            this.IdOperadorComparacionMontoDisponibleTotalEnTcs = filtroSegmentoDB.IdOperadorComparacionMontoDisponibleTotalEnTcs;
            this.MontoDisponibleTotalEnTcs = filtroSegmentoDB.MontoDisponibleTotalEnTcs;
            this.FechaInicioLlamada = filtroSegmentoDB.FechaInicioLlamada;
            this.FechaFinLlamada = filtroSegmentoDB.FechaFinLlamada;
            this.IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad = filtroSegmentoDB.IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad;
            this.DuracionPromedioLlamadaPorOportunidad = filtroSegmentoDB.DuracionPromedioLlamadaPorOportunidad;
            this.IdOperadorComparacionDuracionTotalLlamadaPorOportunidad = filtroSegmentoDB.IdOperadorComparacionDuracionTotalLlamadaPorOportunidad;
            this.DuracionTotalLlamadaPorOportunidad = filtroSegmentoDB.DuracionTotalLlamadaPorOportunidad;
            this.IdOperadorComparacionNroLlamada = filtroSegmentoDB.IdOperadorComparacionNroLlamada;
            this.NroLlamada = filtroSegmentoDB.NroLlamada;
            this.IdOperadorComparacionDuracionLlamada = filtroSegmentoDB.IdOperadorComparacionDuracionLlamada;
            this.DuracionLlamada = filtroSegmentoDB.DuracionLlamada;
            this.IdOperadorComparacionTasaEjecucionLlamada = filtroSegmentoDB.IdOperadorComparacionTasaEjecucionLlamada;
            this.TasaEjecucionLlamada = filtroSegmentoDB.TasaEjecucionLlamada;

            this.FechaInicioInteraccionSitioWeb = filtroSegmentoDB.FechaInicioInteraccionSitioWeb;
            this.FechaFinInteraccionSitioWeb = filtroSegmentoDB.FechaFinInteraccionSitioWeb;
            this.IdOperadorComparacionTiempoVisualizacionTotalSitioWeb = filtroSegmentoDB.IdOperadorComparacionTiempoVisualizacionTotalSitioWeb;
            this.TiempoVisualizacionTotalSitioWeb = filtroSegmentoDB.TiempoVisualizacionTotalSitioWeb;
            this.IdOperadorComparacionNroClickEnlaceTodoSitioWeb = filtroSegmentoDB.IdOperadorComparacionNroClickEnlaceTodoSitioWeb;
            this.NroClickEnlaceTodoSitioWeb = filtroSegmentoDB.NroClickEnlaceTodoSitioWeb;
            this.IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma = filtroSegmentoDB.IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma;
            this.TiempoVisualizacionTotalPaginaPrograma = filtroSegmentoDB.TiempoVisualizacionTotalPaginaPrograma;
            this.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = filtroSegmentoDB.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas;
            this.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = filtroSegmentoDB.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas;
            this.IdOperadorComparacionNroClickEnlacePaginaPrograma = filtroSegmentoDB.IdOperadorComparacionNroClickEnlacePaginaPrograma;
            this.NroClickEnlacePaginaPrograma = filtroSegmentoDB.NroClickEnlacePaginaPrograma;
            this.ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma = filtroSegmentoDB.ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma;
            this.ConsiderarClickBotonMatricularmePaginaPrograma = filtroSegmentoDB.ConsiderarClickBotonMatricularmePaginaPrograma;
            this.ConsiderarClickBotonVersionPruebaPaginaPrograma = filtroSegmentoDB.ConsiderarClickBotonVersionPruebaPaginaPrograma;
            this.IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus = filtroSegmentoDB.IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus;

            this.TiempoVisualizacionTotalPaginaBscampus = filtroSegmentoDB.TiempoVisualizacionTotalPaginaBscampus;
            this.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = filtroSegmentoDB.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus;
            this.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = filtroSegmentoDB.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus;
            this.IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea = filtroSegmentoDB.IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea;
            this.NroVisitasDirectorioTagAreaSubArea = filtroSegmentoDB.NroVisitasDirectorioTagAreaSubArea;
            this.IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea = filtroSegmentoDB.IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea;
            this.TiempoVisualizacionTotalDirectorioTagAreaSubArea = filtroSegmentoDB.TiempoVisualizacionTotalDirectorioTagAreaSubArea;
            this.IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea = filtroSegmentoDB.IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea;
            this.NroClickEnlaceDirectorioTagAreaSubArea = filtroSegmentoDB.NroClickEnlaceDirectorioTagAreaSubArea;
            this.IdOperadorComparacionNroVisitasPaginaMisCursos = filtroSegmentoDB.IdOperadorComparacionNroVisitasPaginaMisCursos;
            this.NroVisitasPaginaMisCursos = filtroSegmentoDB.NroVisitasPaginaMisCursos;
            this.IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos = filtroSegmentoDB.IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos;
            this.TiempoVisualizacionTotalPaginaMisCursos = filtroSegmentoDB.TiempoVisualizacionTotalPaginaMisCursos;
            this.IdOperadorComparacionNroClickEnlacePaginaMisCursos = filtroSegmentoDB.IdOperadorComparacionNroClickEnlacePaginaMisCursos;
            this.NroClickEnlacePaginaMisCursos = filtroSegmentoDB.NroClickEnlacePaginaMisCursos;
            this.IdOperadorComparacionNroVisitaPaginaCursoDiplomado = filtroSegmentoDB.IdOperadorComparacionNroVisitaPaginaCursoDiplomado;
            this.NroVisitaPaginaCursoDiplomado = filtroSegmentoDB.NroVisitaPaginaCursoDiplomado;
            this.IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado = filtroSegmentoDB.IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado;
            this.TiempoVisualizacionTotalPaginaCursoDiplomado = filtroSegmentoDB.TiempoVisualizacionTotalPaginaCursoDiplomado;
            this.IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado = filtroSegmentoDB.IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado;
            this.NroClicksEnlacePaginaCursoDiplomado = filtroSegmentoDB.NroClicksEnlacePaginaCursoDiplomado;
            this.ConsiderarClickFiltroPaginaCursoDiplomado = filtroSegmentoDB.ConsiderarClickFiltroPaginaCursoDiplomado;

            this.ConsiderarOportunidadHistorica = filtroSegmentoDB.ConsiderarOportunidadHistorica;
            this.ConsiderarCategoriaDato = filtroSegmentoDB.ConsiderarCategoriaDato;
            this.ConsiderarInteraccionOfflineOnline = filtroSegmentoDB.ConsiderarInteraccionOfflineOnline;
            this.ConsiderarInteraccionSitioWeb = filtroSegmentoDB.ConsiderarInteraccionSitioWeb;
            this.ConsiderarInteraccionFormularios = filtroSegmentoDB.ConsiderarInteraccionFormularios;
            this.ConsiderarInteraccionChatPw = filtroSegmentoDB.ConsiderarInteraccionChatPw;
            this.ConsiderarInteraccionCorreo = filtroSegmentoDB.ConsiderarInteraccionCorreo;
            this.ConsiderarHistorialFinanciero = filtroSegmentoDB.ConsiderarHistorialFinanciero;
            this.ConsiderarInteraccionWhatsApp = filtroSegmentoDB.ConsiderarInteraccionWhatsApp;
            this.ConsiderarInteraccionChatMessenger = filtroSegmentoDB.ConsiderarInteraccionChatMessenger;
            this.ConsiderarEnvioAutomatico = filtroSegmentoDB.ConsiderarEnvioAutomatico;

            this.ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = filtroSegmentoDB.ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;
            this.FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = filtroSegmentoDB.FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;
            this.FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = filtroSegmentoDB.FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;

            this.IdTiempoFrecuenciaMatriculaAlumno = filtroSegmentoDB.IdTiempoFrecuenciaMatriculaAlumno;
            this.CantidadTiempoMatriculaAlumno = filtroSegmentoDB.CantidadTiempoMatriculaAlumno;

            this.ConsiderarConMessengerValido = filtroSegmentoDB.ConsiderarConMessengerValido;
            this.ConsiderarConWhatsAppValido = filtroSegmentoDB.ConsiderarConWhatsAppValido;
            this.ConsiderarConEmailValido = filtroSegmentoDB.ConsiderarConEmailValido;

            this.IdTiempoFrecuenciaCumpleaniosContactoDentroDe = filtroSegmentoDB.IdTiempoFrecuenciaCumpleaniosContactoDentroDe;
            this.CantidadTiempoCumpleaniosContactoDentroDe = filtroSegmentoDB.CantidadTiempoCumpleaniosContactoDentroDe;

            this.FechaInicioMatriculaAlumno = filtroSegmentoDB.FechaInicioMatriculaAlumno;
            this.FechaFinMatriculaAlumno = filtroSegmentoDB.FechaFinMatriculaAlumno;
            this.ConsiderarAlumnosAsignacionAutomaticaOperaciones = filtroSegmentoDB.ConsiderarAlumnosAsignacionAutomaticaOperaciones;
            this.ExcluirMatriculados = filtroSegmentoDB.ExcluirMatriculados;

            this.IdOperadorMedidaTiempoCreacionOportunidad = filtroSegmentoDB.IdOperadorMedidaTiempoCreacionOportunidad;
            this.NroMedidaTiempoCreacionOportunidad = filtroSegmentoDB.NroMedidaTiempoCreacionOportunidad;
            this.IdOperadorMedidaTiempoUltimaActividadEjecutada = filtroSegmentoDB.IdOperadorMedidaTiempoUltimaActividadEjecutada;
            this.NroMedidaTiempoUltimaActividadEjecutada = filtroSegmentoDB.NroMedidaTiempoUltimaActividadEjecutada;
            this.EnvioAutomaticoEstadoActividadDetalle = filtroSegmentoDB.EnvioAutomaticoEstadoActividadDetalle;
            this.ConsiderarYaEnviados = filtroSegmentoDB.ConsiderarYaEnviados;

            this.UsuarioCreacion = filtroSegmentoDB.UsuarioCreacion;
            this.UsuarioModificacion = filtroSegmentoDB.UsuarioModificacion;
            this.Estado = filtroSegmentoDB.Estado;
            this.FechaCreacion = filtroSegmentoDB.FechaCreacion;
            this.FechaModificacion = filtroSegmentoDB.FechaModificacion;
            this.RowVersion = filtroSegmentoDB.RowVersion;
            this.IdMigracion = filtroSegmentoDB.IdMigracion;
        }

        /// <summary>
        /// Obtiene la lista de valores filtros por filtro segmento
        /// </summary>
        /// <param name="idFiltroSegmento"></param>
        /// <returns></returns>
        public FiltroSegmentoDTO ObtenerFiltroValorPorIdFiltroSegmento()
        {
            try
            {
                var filtroSegmento = _repFiltroSegmento.ObtenerFiltroSegmentoDatosPorId(this.Id);
                var lista = _repFiltroSegmentoValorTipo.ObtenerFiltroValorPorIdFiltroSegmento(this.Id);
                var listaDetalle = _repFiltroSegmentoDetalle.ObtenerDetallePorIdFiltroSegmento(this.Id);


                //se agrega usuario ejecucucion
                filtroSegmento.NombreUsuario = this.UsuarioCreacion;

                filtroSegmento.ListaArea = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea).ToList();
                filtroSegmento.ListaSubArea = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSubArea).ToList();
                filtroSegmento.ListaProgramaGeneral = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral).ToList();
                filtroSegmento.ListaProgramaEspecifico = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProgramaEspecifico).ToList();
                //filtroSegmento.ListaScore = lista.Where(s => s.Tipo.Equals(ValorEstatico.SegmentoTipoScore)).ToList();

                filtroSegmento.ListaOportunidadInicialFaseMaxima = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroOpoInicialFaseMaxima).ToList();
                filtroSegmento.ListaOportunidadInicialFaseActual = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroOpoInicialFaseActual).ToList();
                filtroSegmento.ListaOportunidadActualFaseMaxima = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroUltimaOpoFaseMaxima).ToList();
                filtroSegmento.ListaOportunidadActualFaseActual = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroUltimaOpoFaseActual).ToList();

                filtroSegmento.ListaPais = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroPais).ToList();
                filtroSegmento.ListaCiudad = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCiudad).ToList();

                filtroSegmento.ListaTipoCategoriaOrigen = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTipoCategoriaOrigen).ToList();
                filtroSegmento.ListaCategoriaOrigen = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCategoriaOrigen).ToList();

                filtroSegmento.ListaCargo = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCargo).ToList();
                filtroSegmento.ListaIndustria = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroIndustria).ToList();
                filtroSegmento.ListaAreaFormacion = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroAreaFormacion).ToList();
                filtroSegmento.ListaAreaTrabajo = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroAreaTrabajo).ToList();

                filtroSegmento.ListaTipoFormulario = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTipoFormulario).ToList();
                filtroSegmento.ListaTipoInteraccionFormulario = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTipoInteraccionFormulario).ToList();

                filtroSegmento.ListaProbabilidadOportunidad = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProbabilidadOportunidad).ToList();
                filtroSegmento.ListaActividadLlamada = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroActividadesLlamada).ToList();

                filtroSegmento.ListaVCArea = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroVCArea).ToList();
                filtroSegmento.ListaVCSubArea = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroVCSubArea).ToList();
                filtroSegmento.ListaVCPGeneral = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroVCPGeneral).ToList();

                filtroSegmento.ListaProbabilidadVentaCruzada = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProbabilidadVentaCruzada).ToList();

                filtroSegmento.ListaProgramaGeneralPrincipalExcluirPorMismoCorreoEnviado = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroPGeneralPrincipalExcluir).ToList();

                filtroSegmento.ListaExcluirPorFiltroSegmento = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroFiltroSegmento).ToList();
                filtroSegmento.ListaExcluirPorConjuntoLista = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroConjuntoLista).ToList();
                filtroSegmento.ListaExcluirPorCampaniaMailing = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCampaniaMailing).ToList();

                filtroSegmento.ListaActividadCabecera = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroActividadCabecera).ToList();
                filtroSegmento.ListaOcurrencia = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroOcurrencia).ToList();
                filtroSegmento.ListaDocumentoAlumno = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroDocumentoAlumno).ToList();
                filtroSegmento.ListaEstadoMatricula = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroEstadoMatricula).ToList();
                filtroSegmento.ListaSubEstadoMatricula = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSubEstadoMatricula).ToList();
                filtroSegmento.ListaModalidadCurso = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroModalidadCurso).ToList();

                filtroSegmento.ListaEstadoAcademico = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroEstadoAcademico).ToList();
                filtroSegmento.ListaEstadoPago = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroEstadoPago).ToList();
                filtroSegmento.ListaPorcentajeAvance = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroPorcentajeAvance).ToList();
                filtroSegmento.ListaEstadoLlamada = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroEstadoLlamada).ToList();
                filtroSegmento.ListaSesion = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSesion).ToList();

                filtroSegmento.ListaSesionWebinar = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSesionWebinar).ToList();
                filtroSegmento.ListaTrabajoAlumno = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTrabajoAlumno).ToList();
                filtroSegmento.ListaTrabajoAlumnoFinal = listaDetalle.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTrabajoAlumnoFinal).ToList();

                filtroSegmento.ListaTarifario = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTarifario).ToList();

                filtroSegmento.ListaEnvioAutomaticoOportunidadFaseActual = lista.Where(x => x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroFaseOportunidadActual).ToList();

                return filtroSegmento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void LlenarHijoParaInsertar(FiltroSegmentoDTO filtro)
        {
            try
            {
                foreach (var item in filtro.ListaArea)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroArea,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaSubArea)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroSubArea,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaProgramaGeneral)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaProgramaEspecifico)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroProgramaEspecifico,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaOportunidadInicialFaseMaxima)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroOpoInicialFaseMaxima,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaOportunidadInicialFaseActual)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroOpoInicialFaseActual,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaOportunidadActualFaseMaxima)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroUltimaOpoFaseMaxima,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaOportunidadActualFaseActual)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroUltimaOpoFaseActual,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaPais)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroPais,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaCiudad)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroCiudad,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaCargo)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroCargo,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaIndustria)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroIndustria,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaAreaFormacion)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroAreaFormacion,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaAreaTrabajo)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroAreaTrabajo,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaTipoCategoriaOrigen)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroTipoCategoriaOrigen,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaCategoriaOrigen)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroCategoriaOrigen,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaTipoFormulario)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroTipoFormulario,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaTipoInteraccionFormulario)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroTipoInteraccionFormulario,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaProbabilidadOportunidad)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroProbabilidadOportunidad,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaActividadLlamada)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroActividadesLlamada,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaVCArea)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroVCArea,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaVCSubArea)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroVCSubArea,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaVCPGeneral)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroVCPGeneral,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }

                foreach (var item in filtro.ListaProbabilidadVentaCruzada)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroProbabilidadVentaCruzada,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                //excluir
                foreach (var item in filtro.ListaProgramaGeneralPrincipalExcluirPorMismoCorreoEnviado)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroPGeneralPrincipalExcluir,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                //excluir por resultado filtro
                foreach (var item in filtro.ListaExcluirPorFiltroSegmento)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroFiltroSegmento,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }

                //excluir por campanias maling
                foreach (var item in filtro.ListaExcluirPorCampaniaMailing)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroCampaniaMailing,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }

                //excluir por conjunto lista
                foreach (var item in filtro.ListaExcluirPorConjuntoLista)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroConjuntoLista,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }

                foreach (var item in filtro.ListaActividadCabecera)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroActividadCabecera,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaOcurrencia)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroOcurrencia,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaDocumentoAlumno)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroDocumentoAlumno,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaEstadoMatricula)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroEstadoMatricula,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaSubEstadoMatricula)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroSubEstadoMatricula,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaModalidadCurso)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroModalidadCurso,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }

                //Filtro segmento  detalle

                foreach (var item in filtro.ListaSesion)
                {
                    var _new = new FiltroSegmentoDetalleBO
                    {
                        Valor = item.Valor,
                        IdOperadorComparacion = item.IdOperadorComparacion,
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroSesion,
                        IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                        CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoDetalle.Add(_new);
                }
                foreach (var item in filtro.ListaEstadoAcademico)
                {
                    var _new = new FiltroSegmentoDetalleBO
                    {
                        Valor = item.Valor,
                        IdOperadorComparacion = item.IdOperadorComparacion,
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroEstadoAcademico,
                        IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                        CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoDetalle.Add(_new);
                }
                foreach (var item in filtro.ListaEstadoPago)
                {
                    var _new = new FiltroSegmentoDetalleBO
                    {
                        Valor = item.Valor,
                        IdOperadorComparacion = item.IdOperadorComparacion,
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroEstadoPago,
                        IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                        CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoDetalle.Add(_new);
                }
                foreach (var item in filtro.ListaPorcentajeAvance)
                {
                    var _new = new FiltroSegmentoDetalleBO
                    {
                        Valor = item.Valor,
                        IdOperadorComparacion = item.IdOperadorComparacion,
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroPorcentajeAvance,
                        IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                        CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoDetalle.Add(_new);
                }
                foreach (var item in filtro.ListaEstadoLlamada)
                {
                    var _new = new FiltroSegmentoDetalleBO
                    {
                        Valor = item.Valor,
                        IdOperadorComparacion = item.IdOperadorComparacion,
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroEstadoLlamada,
                        IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                        CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoDetalle.Add(_new);
                }

                //nuevos operaciones
                foreach (var item in filtro.ListaSesionWebinar)
                {
                    var _new = new FiltroSegmentoDetalleBO
                    {
                        Valor = item.Valor,
                        IdOperadorComparacion = item.IdOperadorComparacion,
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroSesionWebinar,
                        IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                        CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoDetalle.Add(_new);
                }
                foreach (var item in filtro.ListaTrabajoAlumno)
                {
                    var _new = new FiltroSegmentoDetalleBO
                    {
                        Valor = item.Valor,
                        IdOperadorComparacion = item.IdOperadorComparacion,
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroTrabajoAlumno,
                        IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                        CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoDetalle.Add(_new);
                }
                foreach (var item in filtro.ListaTrabajoAlumnoFinal)
                {
                    var _new = new FiltroSegmentoDetalleBO
                    {
                        Valor = item.Valor,
                        IdOperadorComparacion = item.IdOperadorComparacion,
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroTrabajoAlumnoFinal,
                        IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                        CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoDetalle.Add(_new);
                }
                foreach (var item in filtro.ListaTarifario)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroTarifario,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
                foreach (var item in filtro.ListaEnvioAutomaticoOportunidadFaseActual)
                {
                    var _new = new FiltroSegmentoValorTipoBO
                    {
                        IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroFaseOportunidadActual,
                        Valor = item.Valor,
                        Estado = true,
                        UsuarioCreacion = filtro.NombreUsuario,
                        UsuarioModificacion = filtro.NombreUsuario,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };
                    ListaFiltroSegmentoValorTipo.Add(_new);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Llena los hijos para actualizar
        /// </summary>
        /// <param name="filtro">Objeto de clase FiltroSegmentoDTO</param>
        public void LlenarHijoParaActualizar(FiltroSegmentoDTO filtro)
        {
            try
            {
                _repFiltroSegmentoValorTipo.EliminacionLogica(filtro);
                _repFiltroSegmentoDetalle.EliminacionLogica(filtro);

                FiltroSegmentoValorTipoBO nuevoValorTipo;
                FiltroSegmentoDetalleBO nuevoFiltroSegmentoDetalle;

                foreach (var item in filtro.ListaArea)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroArea);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroArea,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }

                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaSubArea)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSubArea);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroSubArea,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }

                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaProgramaGeneral)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroProgramaGeneral,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaProgramaEspecifico)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProgramaEspecifico);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroProgramaEspecifico,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaOportunidadInicialFaseMaxima)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroOpoInicialFaseMaxima);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroOpoInicialFaseMaxima,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaOportunidadInicialFaseActual)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroOpoInicialFaseActual);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroOpoInicialFaseActual,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaOportunidadActualFaseMaxima)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroUltimaOpoFaseMaxima);

                    if (_repFiltroSegmentoValorTipo.Exist(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroUltimaOpoFaseMaxima))
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroUltimaOpoFaseMaxima,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaOportunidadActualFaseActual)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroUltimaOpoFaseActual);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroUltimaOpoFaseActual,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaPais)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroPais);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroPais,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaCiudad)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCiudad);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroCiudad,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaTipoCategoriaOrigen)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTipoCategoriaOrigen);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroTipoCategoriaOrigen,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaCategoriaOrigen)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCategoriaOrigen);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroCategoriaOrigen,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaCargo)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCargo);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroCargo,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaIndustria)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroIndustria);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroIndustria,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaAreaFormacion)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroAreaFormacion);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroAreaFormacion,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaAreaTrabajo)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroAreaTrabajo);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroAreaTrabajo,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaTipoFormulario)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTipoFormulario);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroTipoFormulario,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaTipoInteraccionFormulario)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTipoInteraccionFormulario);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroTipoInteraccionFormulario,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaProbabilidadOportunidad)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProbabilidadOportunidad);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroProbabilidadOportunidad,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaActividadLlamada)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroActividadesLlamada);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroActividadesLlamada,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaVCArea)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroVCArea);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroVCArea,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaVCSubArea)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroVCSubArea);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroVCSubArea,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }

                foreach (var item in filtro.ListaVCPGeneral)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroVCPGeneral);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroVCPGeneral,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }

                foreach (var item in filtro.ListaProbabilidadVentaCruzada)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroProbabilidadVentaCruzada);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroProbabilidadVentaCruzada,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaProgramaGeneralPrincipalExcluirPorMismoCorreoEnviado)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroPGeneralPrincipalExcluir);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroPGeneralPrincipalExcluir,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaExcluirPorFiltroSegmento)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroFiltroSegmento);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroFiltroSegmento,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaExcluirPorCampaniaMailing)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroCampaniaMailing);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroCampaniaMailing,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaExcluirPorConjuntoLista)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroConjuntoLista);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroConjuntoLista,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaActividadCabecera)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroActividadCabecera);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroActividadCabecera,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaOcurrencia)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroOcurrencia);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroOcurrencia,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaDocumentoAlumno)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroDocumentoAlumno);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroDocumentoAlumno,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaEstadoMatricula)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroEstadoMatricula);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroEstadoMatricula,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaSubEstadoMatricula)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSubEstadoMatricula);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroSubEstadoMatricula,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
                foreach (var item in filtro.ListaModalidadCurso)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroModalidadCurso);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroModalidadCurso,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }

                // Nuevos campos

                foreach (var item in filtro.ListaSesion)
                {
                    nuevoFiltroSegmentoDetalle = _repFiltroSegmentoDetalle.FirstBy(x => x.Valor == item.Valor && x.IdTiempoFrecuencia == item.IdTiempoFrecuencia && x.CantidadTiempoFrecuencia == item.CantidadTiempoFrecuencia && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSesion);

                    if (nuevoFiltroSegmentoDetalle != null)
                    {
                        nuevoFiltroSegmentoDetalle.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoFiltroSegmentoDetalle.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoFiltroSegmentoDetalle = new FiltroSegmentoDetalleBO
                        {
                            Valor = item.Valor,
                            IdOperadorComparacion = item.IdOperadorComparacion,
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroSesion,
                            IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                            CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoDetalle.Add(nuevoFiltroSegmentoDetalle);
                }
                foreach (var item in filtro.ListaEstadoAcademico)
                {
                    nuevoFiltroSegmentoDetalle = _repFiltroSegmentoDetalle.FirstBy(x => x.Valor == item.Valor && x.IdTiempoFrecuencia == item.IdTiempoFrecuencia && x.CantidadTiempoFrecuencia == item.CantidadTiempoFrecuencia && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroEstadoAcademico);

                    if (nuevoFiltroSegmentoDetalle != null)
                    {
                        nuevoFiltroSegmentoDetalle.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoFiltroSegmentoDetalle.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoFiltroSegmentoDetalle = new FiltroSegmentoDetalleBO
                        {
                            Valor = item.Valor,
                            IdOperadorComparacion = item.IdOperadorComparacion,
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroEstadoAcademico,
                            IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                            CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoDetalle.Add(nuevoFiltroSegmentoDetalle);
                }
                foreach (var item in filtro.ListaEstadoPago)
                {
                    nuevoFiltroSegmentoDetalle = _repFiltroSegmentoDetalle.FirstBy(x => x.Valor == item.Valor && x.IdTiempoFrecuencia == item.IdTiempoFrecuencia && x.CantidadTiempoFrecuencia == item.CantidadTiempoFrecuencia && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroEstadoPago);

                    if (nuevoFiltroSegmentoDetalle != null)
                    {
                        nuevoFiltroSegmentoDetalle.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoFiltroSegmentoDetalle.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoFiltroSegmentoDetalle = new FiltroSegmentoDetalleBO
                        {
                            Valor = item.Valor,
                            IdOperadorComparacion = item.IdOperadorComparacion,
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroEstadoPago,
                            IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                            CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoDetalle.Add(nuevoFiltroSegmentoDetalle);
                }
                foreach (var item in filtro.ListaPorcentajeAvance)
                {
                    nuevoFiltroSegmentoDetalle = _repFiltroSegmentoDetalle.FirstBy(x => x.Valor == item.Valor && x.IdTiempoFrecuencia == item.IdTiempoFrecuencia && x.CantidadTiempoFrecuencia == item.CantidadTiempoFrecuencia && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroPorcentajeAvance);

                    if (nuevoFiltroSegmentoDetalle != null)
                    {
                        nuevoFiltroSegmentoDetalle.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoFiltroSegmentoDetalle.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoFiltroSegmentoDetalle = new FiltroSegmentoDetalleBO
                        {
                            Valor = item.Valor,
                            IdOperadorComparacion = item.IdOperadorComparacion,
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroPorcentajeAvance,
                            IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                            CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoDetalle.Add(nuevoFiltroSegmentoDetalle);
                }
                foreach (var item in filtro.ListaEstadoLlamada)
                {
                    nuevoFiltroSegmentoDetalle = _repFiltroSegmentoDetalle.FirstBy(x => x.Valor == item.Valor && x.IdTiempoFrecuencia == item.IdTiempoFrecuencia && x.CantidadTiempoFrecuencia == item.CantidadTiempoFrecuencia && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroEstadoLlamada);

                    if (nuevoFiltroSegmentoDetalle != null)
                    {
                        nuevoFiltroSegmentoDetalle.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoFiltroSegmentoDetalle.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoFiltroSegmentoDetalle = new FiltroSegmentoDetalleBO
                        {
                            Valor = item.Valor,
                            IdOperadorComparacion = item.IdOperadorComparacion,
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroEstadoLlamada,
                            IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                            CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoDetalle.Add(nuevoFiltroSegmentoDetalle);
                }
                //nuevos filtros operaciones
                foreach (var item in filtro.ListaSesionWebinar)
                {
                    nuevoFiltroSegmentoDetalle = _repFiltroSegmentoDetalle.FirstBy(x => x.Valor == item.Valor && x.IdTiempoFrecuencia == item.IdTiempoFrecuencia && x.CantidadTiempoFrecuencia == item.CantidadTiempoFrecuencia && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroSesionWebinar);

                    if (nuevoFiltroSegmentoDetalle != null)
                    {
                        nuevoFiltroSegmentoDetalle.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoFiltroSegmentoDetalle.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoFiltroSegmentoDetalle = new FiltroSegmentoDetalleBO
                        {
                            Valor = item.Valor,
                            IdOperadorComparacion = item.IdOperadorComparacion,
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroSesionWebinar,
                            IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                            CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoDetalle.Add(nuevoFiltroSegmentoDetalle);
                }
                foreach (var item in filtro.ListaTrabajoAlumno)
                {
                    nuevoFiltroSegmentoDetalle = _repFiltroSegmentoDetalle.FirstBy(x => x.Valor == item.Valor && x.IdTiempoFrecuencia == item.IdTiempoFrecuencia && x.CantidadTiempoFrecuencia == item.CantidadTiempoFrecuencia && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTrabajoAlumno);

                    if (nuevoFiltroSegmentoDetalle != null)
                    {
                        nuevoFiltroSegmentoDetalle.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoFiltroSegmentoDetalle.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoFiltroSegmentoDetalle = new FiltroSegmentoDetalleBO
                        {
                            Valor = item.Valor,
                            IdOperadorComparacion = item.IdOperadorComparacion,
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroTrabajoAlumno,
                            IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                            CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoDetalle.Add(nuevoFiltroSegmentoDetalle);
                }
                foreach (var item in filtro.ListaTrabajoAlumnoFinal)
                {
                    nuevoFiltroSegmentoDetalle = _repFiltroSegmentoDetalle.FirstBy(x => x.Valor == item.Valor && x.IdTiempoFrecuencia == item.IdTiempoFrecuencia && x.CantidadTiempoFrecuencia == item.CantidadTiempoFrecuencia && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTrabajoAlumnoFinal);

                    if (nuevoFiltroSegmentoDetalle != null)
                    {
                        nuevoFiltroSegmentoDetalle.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoFiltroSegmentoDetalle.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoFiltroSegmentoDetalle = new FiltroSegmentoDetalleBO
                        {
                            Valor = item.Valor,
                            IdOperadorComparacion = item.IdOperadorComparacion,
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroTrabajoAlumnoFinal,
                            IdTiempoFrecuencia = item.IdTiempoFrecuencia,
                            CantidadTiempoFrecuencia = item.CantidadTiempoFrecuencia,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoDetalle.Add(nuevoFiltroSegmentoDetalle);
                }
                foreach (var item in filtro.ListaTarifario)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroTarifario);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroTarifario,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }

                foreach (var item in filtro.ListaEnvioAutomaticoOportunidadFaseActual)
                {
                    nuevoValorTipo = _repFiltroSegmentoValorTipo.FirstBy(x => x.Valor == item.Valor && x.IdFiltroSegmento == filtro.Id && x.IdCategoriaObjetoFiltro == ValorEstatico.IdCategoriaObjetoFiltroFaseOportunidadActual);

                    if (nuevoValorTipo != null)
                    {
                        nuevoValorTipo.UsuarioModificacion = filtro.NombreUsuario;
                        nuevoValorTipo.FechaModificacion = DateTime.Now;
                    }
                    else
                    {
                        nuevoValorTipo = new FiltroSegmentoValorTipoBO
                        {
                            IdCategoriaObjetoFiltro = ValorEstatico.IdCategoriaObjetoFiltroFaseOportunidadActual,
                            Valor = item.Valor,
                            Estado = true,
                            UsuarioCreacion = filtro.NombreUsuario,
                            UsuarioModificacion = filtro.NombreUsuario,
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now
                        };
                    }
                    ListaFiltroSegmentoValorTipo.Add(nuevoValorTipo);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene los resultados del filtro ejecutado
        /// </summary>
        /// <returns></returns>
        public List<FiltroSegmentoCompuestoDTO> ObtenerResultado()
        {
            try
            {
                return _repFiltroSegmento.ObtenerResultado(this.Id, this.IdFiltroSegmentoTipoContacto.Value);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Ejecuta el filtro y guarda los resultados en la tabla mkt.T_FiltroSegmentoCalculado
        /// </summary>
        /// <returns></returns>
        public bool EjecutarFiltro()
        {
            try
            {
                _repFiltroSegmentoCalculado.EliminarPorFiltroSegmento(this.Id, this.UsuarioCreacion);
                return _repFiltroSegmento.EjecutarFiltro(this.ObtenerFiltroValorPorIdFiltroSegmento());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Ejecuta el filtro segmento para mailchimp
        /// </summary>
        /// <param name="areas"></param>
        /// <param name="subAreas"></param>
        /// <param name="programas"></param>
        /// <param name="idCampaniaMailingLista"></param>
        /// <param name="idCampaniaMailing"></param>
        /// <returns></returns>
        public int EjecutarFiltroMailchimp(List<FiltroSegmentoValorTipoDTO> listaAreas, List<FiltroSegmentoValorTipoDTO> listaSubAreas, List<FiltroSegmentoValorTipoDTO> listaProgramaGeneral, int idCampaniaMailingLista, int idCampaniaMailing, DateTime? fechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal, DateTime? fechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal, List<CampaniaMailingValorTipoDTO> listaExcluirPorCampaniaMailing)
        {
            try
            {
                var filtros = this.ObtenerFiltroValorPorIdFiltroSegmento();
                filtros.ListaArea = listaAreas;
                filtros.ListaSubArea = listaSubAreas;
                filtros.ListaProgramaGeneral = listaProgramaGeneral;
                filtros.IdCampaniaMailing = idCampaniaMailing;
                filtros.IdCampaniaMailingLista = idCampaniaMailingLista;
                filtros.FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = fechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;
                filtros.FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = fechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;
                filtros.ListaProgramaGeneralPrincipalExcluirPorMismoCorreoEnviado = listaProgramaGeneral;
                filtros.ListaExcluirPorCampaniaMailing = listaExcluirPorCampaniaMailing.Select(x => new FiltroSegmentoValorTipoDTO { Id = x.Id, IdCategoriaObjetoFiltro = x.IdCategoriaObjetoFiltro, Valor = x.Valor }).ToList();
                return _repFiltroSegmento.EjecutarFiltroTipoContactoProspectoMailchimp(filtros);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Ejecuta el filtro segmento para mailchimp
        /// </summary>
        /// <param name="listaArea">Lista de areas de capacitacion</param>
        /// <param name="listaSubArea">Lista de subareas de capacitacion</param>
        /// <param name="listaPGeneral">Lista de programas generales</param>
        /// <param name="idPrioridadMailChimpLista">Id de la prioridad mailchimp lista (PK de la tabla mkt.T_PrioridadMailChimpLista)</param>
        /// <param name="idCampaniaGeneral">Id de la campania general</param>
        /// <param name="idProbabilidadOportunidad">Id de probabilidad de la oportunidad (PK de la tabla mkt.T_ProbabilidadRegistro_PW)</param>
        /// <param name="cantidadPeriodoSinRecibirCorreo">Cantidad de periodo sin recibir correo</param>
        /// <param name="tipoPeriodoSinRecibirCorreo">Tipo de periodo sin recibir correo</param>
        /// <param name="tipoAsociacion">Tipo de asociacion (PK de la tabla mkt.T_TipoAsociacion)</param>
        /// <param name="numeroSegmento">Numeros de segmentos</param>
        /// <param name="idCategoriaObjetoFiltro">Id de la categoria objeto filtro</param>
        /// <returns>Entero</returns>
        public int EjecutarFiltroMailingGeneral(List<FiltroSegmentoValorTipoDTO> listaArea, List<FiltroSegmentoValorTipoDTO> listaSubArea, List<FiltroSegmentoValorTipoDTO> listaPGeneral, int idPrioridadMailChimpLista, int idCampaniaGeneral, int? idProbabilidadOportunidad, int? cantidadPeriodoSinRecibirCorreo, int? tipoPeriodoSinRecibirCorreo, int tipoAsociacion, int numeroSegmento, int idCategoriaObjetoFiltro)
        {
            try
            {
                var filtros = this.ObtenerFiltroValorPorIdFiltroSegmento();
                filtros.ListaArea = listaArea;
                filtros.ListaSubArea = listaSubArea;
                filtros.ListaProgramaGeneral = listaPGeneral;
                filtros.IdCampaniaGeneral = idCampaniaGeneral;
                filtros.CantidadPeriodoSinRecibirCorreo = cantidadPeriodoSinRecibirCorreo;
                filtros.TipoPeriodoSinRecibirCorreo = tipoPeriodoSinRecibirCorreo;
                filtros.IdProbabilidadOportunidad = idProbabilidadOportunidad;
                filtros.TipoAsociacion = tipoAsociacion;
                filtros.NumeroSegmento = numeroSegmento;
                filtros.IdPrioridadMailChimpLista = idPrioridadMailChimpLista;
                filtros.IdCategoriaObjetoFiltro = idCategoriaObjetoFiltro;
                return _repFiltroSegmento.EjecutarFiltroTipoContactoProspectoMailingGeneral(filtros);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Ejecuta el filtro segmento para conjunto lista
        /// </summary>
        /// <param name="listaArea"></param>
        /// <param name="listaSubArea"></param>
        /// <param name="listaProgramaGeneral"></param>
        /// <param name="idConjuntoListaDetalle"></param>
        /// <param name="nroListasRepeticionContacto"></param>
        /// <returns></returns>
        public bool EjecutarFiltroSegmentoConjuntoLista(List<FiltroSegmentoValorTipoDTO> listaArea, List<FiltroSegmentoValorTipoDTO> listaSubArea, List<FiltroSegmentoValorTipoDTO> listaProgramaGeneral, int idConjuntoListaDetalle, int nroListasRepeticionContacto, int nroEjecucion, bool considerarYaEnviados)
        {
            // No considerar a los ya enviados
            try
            {
                var filtros = this.ObtenerFiltroValorPorIdFiltroSegmento();
                filtros.ListaArea = listaArea;
                filtros.ListaSubArea = listaSubArea;
                filtros.ListaProgramaGeneral = listaProgramaGeneral;
                filtros.IdConjuntoListaDetalle = idConjuntoListaDetalle;
                filtros.NroListasRepeticionContacto = nroListasRepeticionContacto;
                filtros.NroEjecucion = nroEjecucion;
                filtros.ConsiderarYaEnviados = considerarYaEnviados;

                return this.EjecutarFiltroConjuntoLista(filtros);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Ejecuta el filtro para conjunto lista, dependiendo del FiltroSegmentoTipoContacto
        /// </summary>
        /// <param name="filtros">DTO con la configuracion del filtro segmento</param>
        /// <returns>Booleano true si la ejecucion fue exitosa, caso contrario lanza una excepcion</returns>
        public bool EjecutarFiltroConjuntoLista(FiltroSegmentoDTO filtros)
        {
            try
            {
                switch (filtros.IdFiltroSegmentoTipoContacto)
                {
                    case 1:/// Alumno - Exalumno
                        _repFiltroSegmento.EjecutarFiltroConjuntoListaTipoAlumnoExAlumno(filtros);
                        break;
                    case 2:// Docente
                        break;
                    case 6:// Prospecto
                        _repFiltroSegmento.EjecutarFiltroConjuntoListaTipoProspecto(filtros);
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
        /// Metodo para insertar un filtro segmento
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public bool Insertar(FiltroSegmentoDTO filtro)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    this.Nombre = filtro.Nombre;
                    this.Descripcion = filtro.Descripcion;
                    this.IdFiltroSegmentoTipoContacto = filtro.IdFiltroSegmentoTipoContacto;
                    this.FechaInicioCreacionUltimaOportunidad = filtro.FechaInicioCreacionUltimaOportunidad;
                    this.FechaFinCreacionUltimaOportunidad = filtro.FechaFinCreacionUltimaOportunidad;
                    this.FechaInicioModificacionUltimaActividadDetalle = filtro.FechaInicioModificacionUltimaActividadDetalle;
                    this.FechaFinModificacionUltimaActividadDetalle = filtro.FechaFinModificacionUltimaActividadDetalle;
                    this.FechaInicioProgramacionUltimaActividadDetalleRn2 = filtro.FechaInicioProgramacionUltimaActividadDetalleRn2;
                    this.FechaFinProgramacionUltimaActividadDetalleRn2 = filtro.FechaFinProgramacionUltimaActividadDetalleRn2;
                    this.EsRn2 = filtro.EsRn2;
                    this.IdOperadorComparacionNroOportunidades = filtro.IdOperadorComparacionNroOportunidades;
                    this.NroOportunidades = filtro.NroOportunidades;
                    this.IdOperadorComparacionNroSolicitudInformacion = filtro.IdOperadorComparacionNroSolicitudInformacion;
                    this.NroSolicitudInformacion = filtro.NroSolicitudInformacion;

                    //
                    this.IdOperadorComparacionNroSolicitudInformacionPg = filtro.IdOperadorComparacionNroSolicitudInformacionPg;
                    this.NroSolicitudInformacionPg = filtro.NroSolicitudInformacionPg;
                    this.IdOperadorComparacionNroSolicitudInformacionArea = filtro.IdOperadorComparacionNroSolicitudInformacionArea;
                    this.NroSolicitudInformacionArea = filtro.NroSolicitudInformacionArea;
                    this.IdOperadorComparacionNroSolicitudInformacionSubArea = filtro.IdOperadorComparacionNroSolicitudInformacionSubArea;
                    this.NroSolicitudInformacionSubArea = filtro.NroSolicitudInformacionSubArea;
                    //

                    this.FechaInicioFormulario = filtro.FechaInicioFormulario;
                    this.FechaFinFormulario = filtro.FechaFinFormulario;
                    this.FechaInicioChatIntegra = filtro.FechaInicioChatIntegra;
                    this.FechaFinChatIntegra = filtro.FechaFinChatIntegra;
                    this.IdOperadorComparacionTiempoMaximoRespuestaChatOnline = filtro.IdOperadorComparacionTiempoMaximoRespuestaChatOnline;
                    this.TiempoMaximoRespuestaChatOnline = filtro.TiempoMaximoRespuestaChatOnline;
                    this.IdOperadorComparacionNroPalabrasClienteChatOnline = filtro.IdOperadorComparacionNroPalabrasClienteChatOnline;
                    this.NroPalabrasClienteChatOnline = filtro.NroPalabrasClienteChatOnline;
                    this.IdOperadorComparacionTiempoPromedioRespuestaChatOnline = filtro.IdOperadorComparacionTiempoPromedioRespuestaChatOnline;
                    this.TiempoPromedioRespuestaChatOnline = filtro.TiempoPromedioRespuestaChatOnline;
                    this.IdOperadorComparacionNroPalabrasClienteChatOffline = filtro.IdOperadorComparacionNroPalabrasClienteChatOffline;
                    this.NroPalabrasClienteChatOffline = filtro.NroPalabrasClienteChatOffline;

                    this.FechaInicioCorreo = filtro.FechaInicioCorreo;
                    this.FechaFinCorreo = filtro.FechaFinCorreo;
                    this.IdOperadorComparacionNroCorreosAbiertos = filtro.IdOperadorComparacionNroCorreosAbiertos;
                    this.NroCorreosAbiertos = filtro.NroCorreosAbiertos;
                    this.IdOperadorComparacionNroCorreosNoAbiertos = filtro.IdOperadorComparacionNroCorreosNoAbiertos;
                    this.NroCorreosNoAbiertos = filtro.NroCorreosNoAbiertos;
                    this.IdOperadorComparacionNroClicksEnlace = filtro.IdOperadorComparacionNroClicksEnlace;
                    this.NroClicksEnlace = filtro.NroClicksEnlace;
                    this.EsSuscribirme = filtro.EsSuscribirme;
                    this.EsDesuscribirme = filtro.EsDesuscribirme;

                    this.IdOperadorComparacionNroCorreosAbiertosMailChimp = filtro.IdOperadorComparacionNroCorreosAbiertosMailChimp;
                    this.NroCorreosAbiertosMailChimp = filtro.NroCorreosAbiertosMailChimp;
                    this.IdOperadorComparacionNroCorreosNoAbiertosMailChimp = filtro.IdOperadorComparacionNroCorreosNoAbiertosMailChimp;
                    this.NroCorreosNoAbiertosMailChimp = filtro.NroCorreosNoAbiertosMailChimp;
                    this.IdOperadorComparacionNroClicksEnlaceMailChimp = filtro.IdOperadorComparacionNroClicksEnlaceMailChimp;
                    this.NroClicksEnlaceMailChimp = filtro.NroClicksEnlaceMailChimp;

                    this.ConsiderarFiltroGeneral = filtro.ConsiderarFiltroGeneral;
                    this.ConsiderarFiltroEspecifico = filtro.ConsiderarFiltroEspecifico;
                    this.TieneVentaCruzada = filtro.TieneVentaCruzada;
                    this.IdOperadorComparacionNroTotalLineaCreditoVigente = filtro.IdOperadorComparacionNroTotalLineaCreditoVigente;
                    this.NroTotalLineaCreditoVigente = filtro.NroTotalLineaCreditoVigente;
                    this.IdOperadorComparacionMontoTotalLineaCreditoVigente = filtro.IdOperadorComparacionMontoTotalLineaCreditoVigente;
                    this.MontoTotalLineaCreditoVigente = filtro.MontoTotalLineaCreditoVigente;
                    this.IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente = filtro.IdOperadorComparacionMontoMaximoOtorgadoLineaCreditoVigente;
                    this.MontoMaximoOtorgadoLineaCreditoVigente = filtro.MontoMaximoOtorgadoLineaCreditoVigente;
                    this.IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente = filtro.IdOperadorComparacionMontoMinimoOtorgadoLineaCreditoVigente;
                    this.MontoMinimoOtorgadoLineaCreditoVigente = filtro.MontoMinimoOtorgadoLineaCreditoVigente;
                    this.IdOperadorComparacionNroTotalLineaCreditoVigenteVencida = filtro.IdOperadorComparacionNroTotalLineaCreditoVigenteVencida;
                    this.NroTotalLineaCreditoVigenteVencida = filtro.NroTotalLineaCreditoVigenteVencida;
                    this.IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida = filtro.IdOperadorComparacionMontoTotalLineaCreditoVigenteVencida;
                    this.MontoTotalLineaCreditoVigenteVencida = filtro.MontoTotalLineaCreditoVigenteVencida;
                    this.IdOperadorComparacionNroTcOtorgada = filtro.IdOperadorComparacionNroTcOtorgada;

                    this.NroTcOtorgada = filtro.NroTcOtorgada;
                    this.IdOperadorComparacionMontoTotalOtorgadoEnTcs = filtro.IdOperadorComparacionMontoTotalOtorgadoEnTcs;
                    this.MontoTotalOtorgadoEnTcs = filtro.MontoTotalOtorgadoEnTcs;
                    this.IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc = filtro.IdOperadorComparacionMontoMaximoOtorgadoEnUnaTc;
                    this.MontoMaximoOtorgadoEnUnaTc = filtro.MontoMaximoOtorgadoEnUnaTc;
                    this.IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc = filtro.IdOperadorComparacionMontoMinimoOtorgadoEnUnaTc;
                    this.MontoMinimoOtorgadoEnUnaTc = filtro.MontoMinimoOtorgadoEnUnaTc;
                    this.IdOperadorComparacionMontoDisponibleTotalEnTcs = filtro.IdOperadorComparacionMontoDisponibleTotalEnTcs;
                    this.MontoDisponibleTotalEnTcs = filtro.MontoDisponibleTotalEnTcs;
                    this.FechaInicioLlamada = filtro.FechaInicioLlamada;
                    this.FechaFinLlamada = filtro.FechaFinLlamada;
                    this.IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad = filtro.IdOperadorComparacionDuracionPromedioLlamadaPorOportunidad;
                    this.DuracionPromedioLlamadaPorOportunidad = filtro.DuracionPromedioLlamadaPorOportunidad;
                    this.IdOperadorComparacionDuracionTotalLlamadaPorOportunidad = filtro.IdOperadorComparacionDuracionTotalLlamadaPorOportunidad;
                    this.DuracionTotalLlamadaPorOportunidad = filtro.DuracionTotalLlamadaPorOportunidad;
                    this.IdOperadorComparacionNroLlamada = filtro.IdOperadorComparacionNroLlamada;
                    this.NroLlamada = filtro.NroLlamada;
                    this.IdOperadorComparacionDuracionLlamada = filtro.IdOperadorComparacionDuracionLlamada;
                    this.DuracionLlamada = filtro.DuracionLlamada;
                    this.IdOperadorComparacionTasaEjecucionLlamada = filtro.IdOperadorComparacionTasaEjecucionLlamada;
                    this.TasaEjecucionLlamada = filtro.TasaEjecucionLlamada;

                    this.FechaInicioInteraccionSitioWeb = filtro.FechaInicioInteraccionSitioWeb;
                    this.FechaFinInteraccionSitioWeb = filtro.FechaFinInteraccionSitioWeb;
                    this.IdOperadorComparacionTiempoVisualizacionTotalSitioWeb = filtro.IdOperadorComparacionTiempoVisualizacionTotalSitioWeb;
                    this.TiempoVisualizacionTotalSitioWeb = filtro.TiempoVisualizacionTotalSitioWeb;
                    this.IdOperadorComparacionNroClickEnlaceTodoSitioWeb = filtro.IdOperadorComparacionNroClickEnlaceTodoSitioWeb;
                    this.NroClickEnlaceTodoSitioWeb = filtro.NroClickEnlaceTodoSitioWeb;
                    this.IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma = filtro.IdOperadorComparacionTiempoVisualizacionTotalPaginaPrograma;
                    this.TiempoVisualizacionTotalPaginaPrograma = filtro.TiempoVisualizacionTotalPaginaPrograma;
                    this.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = filtro.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas;
                    this.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas = filtro.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaProgramas;
                    this.IdOperadorComparacionNroClickEnlacePaginaPrograma = filtro.IdOperadorComparacionNroClickEnlacePaginaPrograma;
                    this.NroClickEnlacePaginaPrograma = filtro.NroClickEnlacePaginaPrograma;
                    this.ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma = filtro.ConsiderarVisualizacionVideoVistaPreviaPaginaPrograma;
                    this.ConsiderarClickBotonMatricularmePaginaPrograma = filtro.ConsiderarClickBotonMatricularmePaginaPrograma;
                    this.ConsiderarClickBotonVersionPruebaPaginaPrograma = filtro.ConsiderarClickBotonVersionPruebaPaginaPrograma;
                    this.IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus = filtro.IdOperadorComparacionTiempoVisualizacionTotalPaginaBscampus;

                    this.TiempoVisualizacionTotalPaginaBscampus = filtro.TiempoVisualizacionTotalPaginaBscampus;
                    this.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = filtro.IdOperadorComparacionTiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus;
                    this.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus = filtro.TiempoVisualizacionMaximaEnUnaPaginaWebPaginaBscampus;
                    this.IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea = filtro.IdOperadorComparacionNroVisitasDirectorioTagAreaSubArea;
                    this.NroVisitasDirectorioTagAreaSubArea = filtro.NroVisitasDirectorioTagAreaSubArea;
                    this.IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea = filtro.IdOperadorComparacionTiempoVisualizacionTotalDirectorioTagAreaSubArea;
                    this.TiempoVisualizacionTotalDirectorioTagAreaSubArea = filtro.TiempoVisualizacionTotalDirectorioTagAreaSubArea;
                    this.IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea = filtro.IdOperadorComparacionNroClickEnlaceDirectorioTagAreaSubArea;
                    this.NroClickEnlaceDirectorioTagAreaSubArea = filtro.NroClickEnlaceDirectorioTagAreaSubArea;
                    this.IdOperadorComparacionNroVisitasPaginaMisCursos = filtro.IdOperadorComparacionNroVisitasPaginaMisCursos;
                    this.NroVisitasPaginaMisCursos = filtro.NroVisitasPaginaMisCursos;
                    this.IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos = filtro.IdOperadorComparacionTiempoVisualizacionTotalPaginaMisCursos;
                    this.TiempoVisualizacionTotalPaginaMisCursos = filtro.TiempoVisualizacionTotalPaginaMisCursos;
                    this.IdOperadorComparacionNroClickEnlacePaginaMisCursos = filtro.IdOperadorComparacionNroClickEnlacePaginaMisCursos;
                    this.NroClickEnlacePaginaMisCursos = filtro.NroClickEnlacePaginaMisCursos;
                    this.IdOperadorComparacionNroVisitaPaginaCursoDiplomado = filtro.IdOperadorComparacionNroVisitaPaginaCursoDiplomado;
                    this.NroVisitaPaginaCursoDiplomado = filtro.NroVisitaPaginaCursoDiplomado;
                    this.IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado = filtro.IdOperadorComparacionTiempoVisualizacionTotalPaginaCursoDiplomado;
                    this.TiempoVisualizacionTotalPaginaCursoDiplomado = filtro.TiempoVisualizacionTotalPaginaCursoDiplomado;
                    this.IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado = filtro.IdOperadorComparacionNroClicksEnlacePaginaCursoDiplomado;
                    this.NroClicksEnlacePaginaCursoDiplomado = filtro.NroClicksEnlacePaginaCursoDiplomado;
                    this.ConsiderarClickFiltroPaginaCursoDiplomado = filtro.ConsiderarClickFiltroPaginaCursoDiplomado;

                    this.ConsiderarOportunidadHistorica = filtro.ConsiderarOportunidadHistorica;
                    this.ConsiderarCategoriaDato = filtro.ConsiderarCategoriaDato;
                    this.ConsiderarInteraccionOfflineOnline = filtro.ConsiderarInteraccionOfflineOnline;
                    this.ConsiderarInteraccionSitioWeb = filtro.ConsiderarInteraccionSitioWeb;
                    this.ConsiderarInteraccionFormularios = filtro.ConsiderarInteraccionFormularios;
                    this.ConsiderarInteraccionChatPw = filtro.ConsiderarInteraccionChatPw;
                    this.ConsiderarInteraccionCorreo = filtro.ConsiderarInteraccionCorreo;
                    this.ConsiderarHistorialFinanciero = filtro.ConsiderarHistorialFinanciero;
                    this.ConsiderarInteraccionWhatsApp = filtro.ConsiderarInteraccionWhatsApp;
                    this.ConsiderarInteraccionChatMessenger = filtro.ConsiderarInteraccionChatMessenger;
                    this.ConsiderarEnvioAutomatico = filtro.ConsiderarEnvioAutomatico;

                    this.ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = filtro.ExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;
                    this.FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = filtro.FechaInicioExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;
                    this.FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal = filtro.FechaFinExcluirPorCorreoEnviadoMismoProgramaGeneralPrincipal;

                    this.IdTiempoFrecuenciaMatriculaAlumno = filtro.IdTiempoFrecuenciaMatriculaAlumno;
                    this.CantidadTiempoMatriculaAlumno = filtro.CantidadTiempoMatriculaAlumno;

                    this.ConsiderarConMessengerValido = filtro.ConsiderarConMessengerValido;
                    this.ConsiderarConWhatsAppValido = filtro.ConsiderarConWhatsAppValido;
                    this.ConsiderarConEmailValido = filtro.ConsiderarConEmailValido;

                    this.IdTiempoFrecuenciaCumpleaniosContactoDentroDe = filtro.IdTiempoFrecuenciaCumpleaniosContactoDentroDe;
                    this.CantidadTiempoCumpleaniosContactoDentroDe = filtro.CantidadTiempoCumpleaniosContactoDentroDe;
                    this.FechaInicioMatriculaAlumno = filtro.FechaInicioMatriculaAlumno;
                    this.FechaFinMatriculaAlumno = filtro.FechaFinMatriculaAlumno;
                    this.ConsiderarAlumnosAsignacionAutomaticaOperaciones = filtro.ConsiderarAlumnosAsignacionAutomaticaOperaciones;
                    this.ExcluirMatriculados = filtro.ExcluirMatriculados;

                    this.AplicaSobreCreacionOportunidad = filtro.AplicaSobreCreacionOportunidad;
                    this.IdOperadorMedidaTiempoCreacionOportunidad = filtro.IdOperadorMedidaTiempoCreacionOportunidad;
                    this.NroMedidaTiempoCreacionOportunidad = filtro.NroMedidaTiempoCreacionOportunidad;
                    this.AplicaSobreUltimaActividad = filtro.AplicaSobreUltimaActividad;
                    this.IdOperadorMedidaTiempoUltimaActividadEjecutada = filtro.IdOperadorMedidaTiempoUltimaActividadEjecutada;
                    this.NroMedidaTiempoUltimaActividadEjecutada = filtro.NroMedidaTiempoUltimaActividadEjecutada;
                    this.EnvioAutomaticoEstadoActividadDetalle = filtro.EnvioAutomaticoEstadoActividadDetalle;
                    this.ConsiderarYaEnviados = filtro.ConsiderarYaEnviados;

                    this.UsuarioCreacion = filtro.NombreUsuario;
                    this.UsuarioModificacion = filtro.NombreUsuario;
                    this.FechaCreacion = DateTime.Now;
                    this.FechaModificacion = DateTime.Now;
                    this.Estado = true;
                    this.LlenarHijoParaInsertar(filtro);
                    _repFiltroSegmento.Insert(this);
                    scope.Complete();
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
