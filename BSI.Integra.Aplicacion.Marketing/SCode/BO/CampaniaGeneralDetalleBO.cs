using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

///BO: CampaniaGeneralDetalle
///Autor: Carlos Crispin - Gian Miranda
///Fecha: 12/05/2021
///<summary>
///Columnas y funciones de la tabla mkt.T_CampaniaGeneralDetalle
///</summary>
namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class CampaniaGeneralDetalleBO : BaseBO
    {
        /// Propiedades                         Significado
        /// -----------                         ------------
        /// IdCampaniaGeneral                   Id de la campania general (PK de la tabla mkt.T_CampaniaGeneral)
        /// Nombre                              Nombre de la campania general detalle
        /// Prioridad                           Prioridad de la campania general detalle
        /// Asunto                              Asunto de la campania general detalle
        /// IdPersonal                          Id del personal (PK de la tabla gp.T_Personal)
        /// IdCentroCosto                       Id del centro de costo (PK de la tabla pla.T_CentroCosto)
        /// CantidadContactosMailing            Cantidad de contactos mailing
        /// CantidadContactosWhatsapp           Cantidad de contactos whatsapp
        /// NoIncluyeWhatsaap                   Flag para excluir el calculo de whatsapp
        /// EnEjecucion                         Flag para evitar doble ejecucion
        /// IdConjuntoAnuncio                   Id del conjunto de anuncio (PK de la tabla mkt.T_ConjuntoAnuncio)
        /// IdMigracion                         Id de migracion de V3 (Campo nullable)
        public int IdCampaniaGeneral { get; set; }
        public string Nombre { get; set; }
        public int Prioridad { get; set; }
        public string Asunto { get; set; }
        public int IdPersonal { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? CantidadContactosMailing { get; set; }
        public int? CantidadContactosWhatsapp { get; set; }
        public bool? NoIncluyeWhatsaap { get; set; }
        public bool EnEjecucion { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public int? IdMigracion { get; set; }

        private integraDBContext _integraDBContext;

        private readonly CampaniaGeneralDetalleRepositorio _repCampaniaGeneralDetalle;
        private readonly DocumentoSeccionPwRepositorio _repDocumentoSeccionPw;
        private readonly PgeneralExpositorRepositorio _repPGeneralExpositor;
        private readonly EtiquetaRepositorio _repEtiqueta;
        private readonly CampaniaGeneralDetalleAreaRepositorio _repCampaniaGeneralDetalleArea;
        private readonly CampaniaGeneralDetalleSubAreaRepositorio _repCampaniaGeneralDetalleSubArea;
        private readonly CampaniaGeneralDetalleProgramaRepositorio _repCampaniaGeneralDetallePrograma;

        private DapperRepository dapperRepository;
        private CampaniaMailingDetalleBO CampaniaMailingDetalle;

        public CampaniaGeneralDetalleBO()
        {
            _repCampaniaGeneralDetalle = new CampaniaGeneralDetalleRepositorio();
            _repDocumentoSeccionPw = new DocumentoSeccionPwRepositorio();
            _repCampaniaGeneralDetalleArea = new CampaniaGeneralDetalleAreaRepositorio();
            _repCampaniaGeneralDetalleSubArea = new CampaniaGeneralDetalleSubAreaRepositorio();
            _repCampaniaGeneralDetallePrograma = new CampaniaGeneralDetalleProgramaRepositorio();
            _repPGeneralExpositor = new PgeneralExpositorRepositorio();
            _repEtiqueta = new EtiquetaRepositorio();

            dapperRepository = new DapperRepository();
            CampaniaMailingDetalle = new CampaniaMailingDetalleBO();
        }
        public CampaniaGeneralDetalleBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;

            _repCampaniaGeneralDetalle = new CampaniaGeneralDetalleRepositorio(_integraDBContext);
            _repDocumentoSeccionPw = new DocumentoSeccionPwRepositorio(_integraDBContext);
            _repCampaniaGeneralDetalleArea = new CampaniaGeneralDetalleAreaRepositorio(_integraDBContext);
            _repCampaniaGeneralDetalleSubArea = new CampaniaGeneralDetalleSubAreaRepositorio(_integraDBContext);
            _repCampaniaGeneralDetallePrograma = new CampaniaGeneralDetalleProgramaRepositorio(_integraDBContext);
            _repPGeneralExpositor = new PgeneralExpositorRepositorio(_integraDBContext);
            _repEtiqueta = new EtiquetaRepositorio(_integraDBContext);

            dapperRepository = new DapperRepository(_integraDBContext);

            CampaniaMailingDetalle = new CampaniaMailingDetalleBO(_integraDBContext);
        }

        public void setIntegraDBContext(integraDBContext integraDBContext) { _integraDBContext = integraDBContext; }

        /// <summary>
        /// Reemplaza las etiquetas para el envio de correos de CampaniasMailing
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la Campania General Detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Objeto de clase CampaniaGeneralDetalleProgramaPlantillaDTO</returns>
        public CampaniaGeneralDetalleProgramaPlantillaDTO ProcesarPrioridadMailchimp(int idCampaniaGeneralDetalle)
        {
            ReemplazoEtiquetaPlantillaBO reemplazoEtiquetaPlantilla = new ReemplazoEtiquetaPlantillaBO(_integraDBContext);
            CampaniaGeneralDetalleProgramaPlantillaDTO plantillas = _repCampaniaGeneralDetalle.ObtenerPlantillasInformacionAsesor(idCampaniaGeneralDetalle);

            string telefono = string.Empty;
            if (plantillas.CentralPersonal == "192.168.0.20") telefono = "(51) 54 258787 - Anexo " + plantillas.AnexoPersonal;
            else if (plantillas.CentralPersonal == "192.168.2.20") telefono = "(51) 1 207 2770 - Anexo " + plantillas.AnexoPersonal;
            else telefono = "(51) 54 258787";

            plantillas.Contenido = plantillas.Contenido.Replace("*|APHONE|*", telefono);
            var contenidoPlantilla = plantillas.Contenido;

            // Botones de programas
            var etiquetasWhatsapp = new List<string> { "*|BTNWA_PERU|*", "*|BTNWA_COLOMBIA|*", "*|BTNWA_BOLIVIA|*" };
            if (etiquetasWhatsapp.Any(contenidoPlantilla.Contains))
            {
                List<CampaniaGeneralDetalleContenidoEtiquetaDTO> botonesWhatsapp = _repCampaniaGeneralDetalle.ObtenerBotonesWhatsapp(idCampaniaGeneralDetalle);
                botonesWhatsapp = botonesWhatsapp.Where(x => etiquetasWhatsapp.Contains(x.Etiqueta)).ToList();
                foreach (var btnWhatsapp in botonesWhatsapp)
                {
                    contenidoPlantilla = contenidoPlantilla.Replace(btnWhatsapp.Etiqueta, btnWhatsapp.Contenido);
                }
            }

            // Botones de Messenger
            var etiquetasMesenger = new List<string> { "*|BTNMSG|*" };
            if (etiquetasMesenger.Any(contenidoPlantilla.Contains))
            {
                List<CampaniaGeneralDetalleContenidoEtiquetaDTO> botonesMessenger = _repCampaniaGeneralDetalle.ObtenerBotonesMessenger(idCampaniaGeneralDetalle);
                botonesMessenger = botonesMessenger.Where(x => etiquetasMesenger.Contains(x.Etiqueta)).ToList();

                foreach (var btnMessenger in botonesMessenger)
                {
                    contenidoPlantilla = contenidoPlantilla.Replace(btnMessenger.Etiqueta, btnMessenger.Contenido);
                }
            }

            // Nuevas etiquetas
            List<CampaniaGeneralDetalleContenidoEtiquetaDTO> docentes = _repCampaniaGeneralDetalle.ObtenerExpositoresPorProgramaGeneral(idCampaniaGeneralDetalle);
            foreach (var docente in docentes)
            {
                string contenidoDocente = string.Concat("<h3><strong>DOCENTES:</strong></h3><ul>", docente.Contenido, "</ul>");
                contenidoPlantilla = contenidoPlantilla.Replace(docente.Etiqueta, contenidoDocente);
            }

            List<CampaniaGeneralDetalleContenidoEtiquetaDTO> duracionHorarios = this.ObtenerContenido(idCampaniaGeneralDetalle);
            foreach (var duracionHorario in duracionHorarios)
            {
                string contenidoDuracionHorarios = string.Concat("<h3><strong>HORARIOS:</strong></h3><ul>", duracionHorario.Contenido, "</ul>");
                contenidoPlantilla = contenidoPlantilla.Replace(duracionHorario.Etiqueta, contenidoDuracionHorarios);
            }
            // Fin nuevas etiquetas

            List<CampaniaGeneralDetalleContenidoEtiquetaDTO> nombresProgramas = _repCampaniaGeneralDetalle.ObtenerProgramaYEtiqueta(idCampaniaGeneralDetalle);
            // Reemplaza los nombres de los programas generales
            foreach (var programa in nombresProgramas)
            {
                contenidoPlantilla = contenidoPlantilla.Replace(programa.Etiqueta, programa.Contenido);
            }

            // Reemplaza los botones de mas  información
            List<CampaniaGeneralDetalleContenidoEtiquetaDTO> botonesprogramas = _repCampaniaGeneralDetalle.ObtenerInformacionBotonesyEtiqueta(idCampaniaGeneralDetalle);
            foreach (var da in botonesprogramas)
            {
                contenidoPlantilla = contenidoPlantilla.Replace(da.Etiqueta, da.Contenido);
            }

            List<CampaniaMailingDetalleContenidoEtiquetaDTO> estructuraProgramas = _repDocumentoSeccionPw.ObtenerContenidoYEtiquetaPorCampaniaGeneralDetalle(idCampaniaGeneralDetalle);
            foreach (var estructura in estructuraProgramas)
            {
                var certificacion = "<ul>";
                var html = estructura.Contenido ?? string.Empty;
                var htmlFin = html.Replace("</p>", "|");
                var htmlInicio = htmlFin.Replace("<p>", "|");
                var finalHtml = htmlInicio.Split('|');
                for (var i = 0; i < finalHtml.Length; i++)
                {
                    if (!finalHtml[i].Contains("<li>") && finalHtml[i].Length > 0)
                    {
                        var html2 = finalHtml[i];
                        if (!html2.Equals("\n"))
                        {
                            if (html2.Contains("<strong>"))
                            {
                                html2 = html2.Replace("<strong>", "");
                                html2 = html2.Replace("</strong>", "");
                                certificacion = certificacion + "<li>" + html2 + "</li>";
                            }
                            else
                            {
                                certificacion = certificacion + "<br/>" + html2;
                            }
                        }
                    }
                }
                certificacion = "<h3><strong>ESTRUCTURA CURRICULAR:</strong></h3>" + certificacion + "</ul>";
                contenidoPlantilla = contenidoPlantilla.Replace(estructura.Etiqueta, certificacion);
            }

            List<CampaniaMailingCertificacionDuracionDTO> certificacionProgramas = _repDocumentoSeccionPw.ObtenerCertificacionDuracionEtiquetaCampaniaGeneral(idCampaniaGeneralDetalle);
            foreach (var certificacion in certificacionProgramas)
            {
                certificacion.ContenidoCertificacion = "<h3><strong>CERTIFICACIÓN:</strong></h3><ul>" + certificacion.ContenidoCertificacion + "</ul>";
                certificacion.ContenidoDuracion = "<strong>Duración: </strong>" + certificacion.ContenidoDuracion;
                contenidoPlantilla = contenidoPlantilla.Replace(certificacion.EtiquetaCertificacion, certificacion.ContenidoCertificacion);
                contenidoPlantilla = contenidoPlantilla.Replace(certificacion.EtiquetaDuracion, certificacion.ContenidoDuracion);
            }

            List<CampaniaMailingPGeneralEtiquetaDTO> listaPGeneralEtiqueta = _repCampaniaGeneralDetallePrograma.ObtenerProgramaYEtiqueta(idCampaniaGeneralDetalle);

            string registrosBD;
            foreach (var pGeneralEtiqueta in listaPGeneralEtiqueta)
            {
                registrosBD = dapperRepository.QuerySPDapper("pla.SP_ObtenerModalidadesPorPrograma", new { IdPGeneral = pGeneralEtiqueta.IdPGeneral });
                List<CampaniaMailingModalidadDTO> modalidades = JsonConvert.DeserializeObject<List<CampaniaMailingModalidadDTO>>(registrosBD);

                string html = "<h3><strong>FECHAS DE INICIO:</strong></h3><ul>";

                foreach (var mod in modalidades)
                {
                    html += "<li>" + mod.TipoCiudad + ": " + mod.FechaHoraInicio + "</li>";
                }
                html += "</ul>";
                contenidoPlantilla = contenidoPlantilla.Replace(pGeneralEtiqueta.Etiqueta, html);
            }

            var listaDatosEmpresa = _repCampaniaGeneralDetalle.ObtenerContenidoYEtiquetaDatosEmpresa(idCampaniaGeneralDetalle);

            foreach (var datosEmpresa in listaDatosEmpresa)
                contenidoPlantilla = contenidoPlantilla.Replace(datosEmpresa.Etiqueta, datosEmpresa.Contenido);

            var listaEncabezado = _repCampaniaGeneralDetalle.ObtenerContenidoYEtiquetaEncabezado(idCampaniaGeneralDetalle);
            foreach (var encabezado in listaEncabezado)
                contenidoPlantilla = contenidoPlantilla.Replace(encabezado.Etiqueta, encabezado.Contenido);

            var listaPieDePagina = _repCampaniaGeneralDetalle.ObtenerContenidoYEtiquetaDatosPiePagina(idCampaniaGeneralDetalle);
            foreach (var pieDePagina in listaPieDePagina)
                contenidoPlantilla = contenidoPlantilla.Replace(pieDePagina.Etiqueta, pieDePagina.Contenido);

            // Redes Sociales
            contenidoPlantilla = reemplazoEtiquetaPlantilla.ReemplazarEtiquetasMensajeGenerico(contenidoPlantilla);

            var listaImagenPrograma = _repCampaniaGeneralDetalle.ObtenerContenidoYEtiquetaImagenPrograma(idCampaniaGeneralDetalle);
            foreach (var imagenPrograma in listaImagenPrograma)
                contenidoPlantilla = contenidoPlantilla.Replace(imagenPrograma.Etiqueta, imagenPrograma.Contenido);

            var listaComplemento1 = _repCampaniaGeneralDetalle.ObtenerContenidoYEtiquetaTextoComplemento1(idCampaniaGeneralDetalle);
            foreach (var complemento1 in listaComplemento1)
                contenidoPlantilla = contenidoPlantilla.Replace(complemento1.Etiqueta, complemento1.Contenido);

            var listaComplemento2 = _repCampaniaGeneralDetalle.ObtenerContenidoYEtiquetaTextoComplemento2(idCampaniaGeneralDetalle);
            foreach (var complemento2 in listaComplemento2)
                contenidoPlantilla = contenidoPlantilla.Replace(complemento2.Etiqueta, complemento2.Contenido);

            var listaComplemento3 = _repCampaniaGeneralDetalle.ObtenerContenidoYEtiquetaTextoComplemento3(idCampaniaGeneralDetalle);
            foreach (var complemento3 in listaComplemento3)
                contenidoPlantilla = contenidoPlantilla.Replace(complemento3.Etiqueta, complemento3.Contenido);

            contenidoPlantilla = contenidoPlantilla.Replace("<vacio></vacio>", string.Empty);

            List<CampaniaMailingNombreProgramaDetalleDTO> listaNombreProgramaDetalle = _repDocumentoSeccionPw.ObtenerNombreContenidoEtiquetaDocumentoSeccionCampaniaGeneral(idCampaniaGeneralDetalle);
            List<CampaniaMailingNombreProgramaDetalleDTO> listaExpositor = _repPGeneralExpositor.ObtenerTop5PGeneralExpositorCampaniaGeneral(idCampaniaGeneralDetalle);

            foreach (var expositor in listaExpositor)
            {
                listaNombreProgramaDetalle.Add(expositor);
            }

            var nombreProgramaDetalleAgrupado = from p in listaNombreProgramaDetalle
                                                group p by new
                                                {
                                                    p.IdPGeneral,
                                                    p.Etiqueta,
                                                    p.Nombre,
                                                    p.Titulo
                                                } into g
                                                select new CampaniaMailingNombreProgramaDetalleDTO
                                                {
                                                    IdPGeneral = g.Key.IdPGeneral,
                                                    Etiqueta = g.Key.Etiqueta,
                                                    Nombre = g.Key.Nombre,
                                                    Titulo = g.Key.Titulo,
                                                    Contenido = string.Join("", g.Select(o => o.Contenido).ToList())
                                                };

            Dictionary<string, string> dictionarioContenido = new Dictionary<string, string>();
            nombreProgramaDetalleAgrupado = nombreProgramaDetalleAgrupado.OrderBy(x => x.Titulo);
            foreach (var da in nombreProgramaDetalleAgrupado)
            {
                if (!dictionarioContenido.ContainsKey(da.Etiqueta))
                {
                    string value = da.Nombre + da.Contenido;
                    dictionarioContenido[da.Etiqueta] = value;
                }
                else
                {
                    string guardado = dictionarioContenido[da.Etiqueta];

                    string value = guardado + da.Nombre + da.Contenido;
                    dictionarioContenido[da.Etiqueta] = value;
                }
            }

            foreach (var pair in dictionarioContenido)
            {
                contenidoPlantilla = contenidoPlantilla.Replace(pair.Key, pair.Value);
            }

            // Eliminamos etiquetas sin reemplazar
            var etiquetasIntegra = _repEtiqueta.ObtenerEtiquetasIntegra();
            foreach (var etiqueta in etiquetasIntegra)
            {
                contenidoPlantilla = contenidoPlantilla.Replace(etiqueta, "");
            }
            // Fin

            // Agregamos la etiqueta de identificador de la tabla T_PrioridadMailChimpListaCorreo
            var plantillaEtiqueta = "<span  style=\"display: none\" id=\"prioridadMailChimpCorreoId\">*|IDPMLC|*</span>";
            var plantillaEtiquetaAgregado = "<span  style=\"display: none\" id=\"prioridadMailChimpCorreoId\">IdPrioridadMailChimpListaCorreo*|IDPMLC|*</span>";
            contenidoPlantilla += plantillaEtiqueta;
            contenidoPlantilla += plantillaEtiquetaAgregado;
            plantillas.Contenido = contenidoPlantilla;

            return plantillas;
        }

        /// <summary>
        /// Genera la plantilla de subida de lista a mailchimp
        /// </summary>
        /// <param name="horaInicio">Hora de inicio de la subida</param>
        /// <param name="horaFin">Hora de fin de la subida</param>
        /// <returns>String</returns>
        public string GenerarPlantillaSubidaMailchimp(DateTime horaInicio, DateTime horaFin)
        {
            string texto = $@"<table style='font-family:'Helvetica Neue',Helvetica,Arial,'Lucida Grande',sans-serif; border-collapse: collapse;'>
                <tr style='padding: 5px;'>
                    <td>
                        <div style = 'background-color: #224060; padding: 25px 100px;'>
                            <img src='https://integrav4.bsginstitute.com/Images/Sistema/bsginstitute.png' alt='' />
                        </div>
                    </td>
                </tr>
                <tr><td><br /></td></tr>
                <tr style='text-align: center;'>
                    <td>Ha finalizado correctamente la subida de la campania</td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #CA341D;'>HORA DE INICIO:</h3>
                        <h3>{horaInicio}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #CA341D;'>HORA DE FIN:</h3>
                        <h3>{horaFin}</h3>
                    </td>
                </tr>
            </table>";

            return texto;
        }

        /// <summary>
        /// Genera la plantilla de descarga de desuscritos
        /// </summary>
        /// <param name="horaInicio">Hora de inicio de la subida</param>
        /// <param name="horaFin">Hora de fin de la subida</param>
        /// <param name="cantidadDesuscritos">Cantidad de desuscritos encontrados</param>
        /// <returns>String</returns>
        public string GenerarPlantillaDescargaDesuscritos(DateTime horaInicio, DateTime horaFin, int cantidadDesuscritos)
        {
            string texto = $@"<table style='font-family:'Helvetica Neue',Helvetica,Arial,'Lucida Grande',sans-serif; border-collapse: collapse;'>
                <tr style='padding: 5px;'>
                    <td>
                        <div style = 'background-color: #224060; padding: 25px 100px;'>
                            <img src='https://integrav4.bsginstitute.com/Images/Sistema/bsginstitute.png' alt='' />
                        </div>
                    </td>
                </tr>
                <tr><td><br /></td></tr>
                <tr style='text-align: center;'>
                    <td>Ha finalizado correctamente la descarga de desuscritos:</td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #CA341D;'>HORA DE INICIO:</h3>
                        <h3>{horaInicio}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #CA341D;'>HORA DE FIN:</h3>
                        <h3>{horaFin}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td style='padding-bottom:25px;'>
                        <h3>Se han descargado <span style='color: #4A92DB;'>{cantidadDesuscritos}</span> desuscritos</h3>
                    </td>
                </tr>
            </table>";

            return texto;
        }

        /// <summary>
        /// Genera nueva plantilla de archivado de campania general
        /// </summary>
        /// <param name="etapa">Nombre de la etapa</param>
        /// <param name="campania">Nombre de la campania</param>
        /// <param name="listaCorrecta">Lista de cadenas con las listas correctas</param>
        /// <param name="listaIncorrecta">Lista de cadenas con las listas incorrectas</param>
        /// <returns>Cadena con la plantilla generada</returns>
        public string GenerarNuevaPlantillaArchivadoCampaniaGeneral(string etapa, string campania, List<string> listaCorrecta, List<string> listaIncorrecta)
        {
            string texto = $@"<table style='font-family:'Helvetica Neue',Helvetica,Arial,'Lucida Grande',sans-serif; border-collapse: collapse;'>
                <tr style='padding: 5px;'>
                    <td>
                        <div style = 'background-color: #224060; padding: 25px 100px;'>
                            <img src='https://integrav4.bsginstitute.com/Images/Sistema/bsginstitute.png' alt='' />
                        </div>
                    </td>
                </tr>
                <tr><td><br /></td></tr>
                <tr style='text-align: center;'>
                       <td>Ha finalizado correctamente: <span style='color: #4A92DB;'>{etapa}</span></td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #4A92DB;'>CAMPAÑA:</h3>
                        <h3>{campania}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td style='padding-bottom:25px;'>
                        <h3>Cantidad correctos: <span style='color: #4A92DB;'>{JsonConvert.SerializeObject(listaCorrecta.Count)}</span></h3>
                        <p>{JsonConvert.SerializeObject(new { listaCorrecta })}</p>
                        <h3>Cantidad incorrectos: <span style='color: #4A92DB;'>{JsonConvert.SerializeObject(listaIncorrecta.Count)}</span></h3>
                        <p>{JsonConvert.SerializeObject(new { listaIncorrecta })}</p>
                    </td>
                </tr>
            </table>";

            return texto;
        }

        /// <summary>
        /// Genera nueva plantilla de notificacion de procesamiento de mailing general
        /// </summary>
        /// <param name="lista">Lista de objetos de clase MensajeProcesarDTO</param>
        /// <param name="cantidadMailing">Cantidad de contactos Mailing calculados</param>
        /// <param name="cantidadWhatsApp">Cantidad de contactos WhatsApp calculados</param>
        /// <param name="horaInicio">Hora inicio del procesamiento</param>
        /// <param name="horaFin">Hora fin del procesamiento</param>
        /// <returns>Objeto de clase CampaniaGeneralDetalleProgramaPlantillaDTO</returns>
        public string GenerarNuevaPlantillaNotificacionProcesamientoMailingGeneral(List<MensajeProcesarDTO> lista, int cantidadMailing, int cantidadWhatsApp, string urlFormulario, DateTime horaInicio, DateTime horaFin)
        {
            string texto = $@"<table style='font-family:'Helvetica Neue',Helvetica,Arial,'Lucida Grande',sans-serif; border-collapse: collapse;'>
                <tr style='padding: 5px;'>
                    <td>
                        <div style = 'background-color: #224060; padding: 25px 100px;'>
                            <img src='https://integrav4.bsginstitute.com/Images/Sistema/bsginstitute.png' alt='' />
                        </div>
                    </td>
                </tr>
                <tr><td><br /></td></tr>
                <tr style='text-align: center;'>
                       <td>Ha finalizado correctamente el procesamiento de la prioridad:</td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #4A92DB;'>CAMPAÑA:</h3>
                        <h3>{lista.First(x => x.Nombre == "CORRECTO").ListaDetalle[0].NombreCampania}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #4A92DB;'>PRIORIDAD:</h3>
                        <h3>{lista.First(x => x.Nombre == "CORRECTO").ListaDetalle[0].NombreLista}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #CA341D;'>HORA DE INICIO:</h3>
                        <h3>{horaInicio}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #CA341D;'>HORA DE FIN:</h3>
                        <h3>{horaFin}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td style='padding-bottom:25px;'>
                        <h3><span style='color: #4A92DB;'>INTENTOS:</span> {lista[0].ListaDetalle.Count + lista[1].ListaDetalle.Count}</h3>
                        <h3>Se ha calculado <span style='color: #4A92DB;'>{cantidadMailing}</span> contactos Mailing</h3>
                        <h3>Se ha calculado <span style='color: #4A92DB;'>{cantidadWhatsApp}</span> contactos WhatsApp</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td style='padding-bottom:25px;'>
                        <h3><span style='color: #4A92DB;'>Direccion Formulario</span></h3>
                        <h3>{urlFormulario}</h3>
                    </td>
                </tr>
            </table>";

            return texto;
        }

        /// <summary>
        /// Obtiene el html para enviar el mesaje correcto
        /// </summary>
        /// <param name="nombreCampaniaGeneral">Nombre de la campania general</param>
        /// <param name="nombreCampaniaGeneralDetalle">Nombre del detalle de la campania general</param>
        /// <param name="cantidadWhatsApp">Cantidad de datos de WhatsApp</param>
        /// <param name="horaInicio">Hora de inicio del procesamiento</param>
        /// <param name="horaFin">Hora de fin del procesamiento</param>
        /// <returns>String</returns>
        public string GenerarPlantillaNotificacionFinalizacionWhatsapp(string nombreCampaniaGeneral, string nombreCampaniaGeneralDetalle, int cantidadWhatsApp, DateTime horaInicio, DateTime horaFin)
        {

            string texto = $@"<table style='font-family:'Helvetica Neue',Helvetica,Arial,'Lucida Grande',sans-serif; border-collapse: collapse;'>
                <tr style='padding: 5px;'>
                    <td>
                        <div style = 'background-color: #224060; padding: 25px 100px;'>
                            <img src='https://integrav4.bsginstitute.com/Images/Sistema/bsginstitute.png' alt='' />
                        </div>
                    </td>
                </tr>
                <tr><td><br /></td></tr>
                <tr style='text-align: center;'>
                       <td>Ha finalizado correctamente la preparación de los datos de WhatsApp:</td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #4A92DB;'>CAMPAÑA:</h3>
                        <h3>{nombreCampaniaGeneral}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #4A92DB;'>PRIORIDAD:</h3>
                        <h3>{nombreCampaniaGeneralDetalle}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #CA341D;'>HORA DE INICIO:</h3>
                        <h3>{horaInicio}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td>
                        <h3 style='color: #CA341D;'>HORA DE FIN:</h3>
                        <h3>{horaFin}</h3>
                    </td>
                </tr>
                <tr style='text-align: center;'>
                    <td style='padding-bottom:25px;'>
                        <h3>Se ha preparado <span style='color: #4A92DB;'>{cantidadWhatsApp}</span> contactos WhatsApp</h3>
                        <h3>(Configurados en el módulo)</h3>
                    </td>
                </tr>
            </table>";

            return texto;
        }

        /// <summary>
        /// Obtiene el contenido 
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle">Id de la campania general detalle (PK de la tabla mkt.T_CampaniaGeneralDetalle)</param>
        /// <returns>Lista de objetos de tipo CampaniaGeneralDetalleContenidoEtiquetaDTO</returns>
        public List<CampaniaGeneralDetalleContenidoEtiquetaDTO> ObtenerContenido(int idCampaniaGeneralDetalle)
        {
            try
            {
                List<CampaniaGeneralDetalleContenidoEtiquetaDTO> detalle = new List<CampaniaGeneralDetalleContenidoEtiquetaDTO>();

                var pGeneralDuracion = _repCampaniaGeneralDetalle.ObtenerProgramaGeneralPorIdCampaniaGeneralDetalle(idCampaniaGeneralDetalle);

                detalle.Add(new CampaniaGeneralDetalleContenidoEtiquetaDTO()
                {
                    Etiqueta = "*|PGDURACIONHORARIOS_PP1|*",
                    Contenido = CampaniaMailingDetalle.GetContenidoHorarios(pGeneralDuracion.Id)
                });

                return detalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Obtiene las etiquetas de una plantilla.
        /// </summary>
        /// <param name="cadena">Cadena con la plantilla enviada</param>
        /// <returns>Lista de cadena con las etiquetas</returns>
        public List<string> ObtenerEtiquetas(string cadena)
        {
            List<string> etiquetas = cadena.Split(new string[] { "*|" }, StringSplitOptions.None).Where(o => o.Contains("|*")).Select(o => o.Split(new string[] { "|*" }, StringSplitOptions.None).First()).ToList();

            // Elimina las etiquetas por defecto del mailchimp
            etiquetas.Remove("EMAIL");
            etiquetas.Remove("FNAME");
            etiquetas.Remove("LNAME");
            return etiquetas;
        }


        /// <summary>
        /// Obtiene las listas de configuracion de area, subarea y programa general del filtro segmento
        /// </summary>
        /// <param name="idCampaniaGeneralDetalle"></param>
        /// <param name="idCategoriaObjetoFiltro">Id de la categoria de objeto filtro (PK de la tabla )</param>
        /// <returns>Lista de cadena con las etiquetas</returns>
        public ListaFiltroSegmentoValorTipoDTO ObtenerListasConfiguracionFiltro(int idCampaniaGeneralDetalle, int? idCategoriaObjetoFiltro)
        {
            try
            {
                var listaConfigurada = new ListaFiltroSegmentoValorTipoDTO();

                if(!idCategoriaObjetoFiltro.HasValue)
                {
                    throw new Exception("No se encuentra el nivel de segmentacion configurado");
                }

                if (idCategoriaObjetoFiltro >= 1)
                {
                    listaConfigurada.ListaAreas = _repCampaniaGeneralDetalleArea.ObtenerPorIdCampaniaGeneralDetalle(idCampaniaGeneralDetalle);

                    if (idCategoriaObjetoFiltro >= 2)
                    {
                        listaConfigurada.ListaSubAreas = _repCampaniaGeneralDetalleSubArea.ObtenerPorIdCampaniaGeneralDetalle(idCampaniaGeneralDetalle);

                        if (idCategoriaObjetoFiltro >= 3)
                            listaConfigurada.ListaProgramas = _repCampaniaGeneralDetallePrograma.ObtenerPorIdCampaniaGeneralDetalle(idCampaniaGeneralDetalle);
                    }
                }

                return listaConfigurada;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CampaniaGeneralDetalleProgramaBO> listaCampaniaGeneralDetalleProgramaBO;
        public List<CampaniaGeneralDetalleAreaBO> AreaCampaniaGeneralDetalle;
        public List<CampaniaGeneralDetalleSubAreaBO> SubAreaCampaniaGeneralDetalle;
        public List<CampaniaGeneralDetalleResponsableBO> ResponsableCampaniaGeneralDetalle;
    }
}