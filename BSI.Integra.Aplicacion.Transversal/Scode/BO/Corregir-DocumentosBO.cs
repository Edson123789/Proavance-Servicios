using System;
using System.Collections.Generic;
using System.Linq;
using BSI.Integra.Aplicacion.Base.BO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.html;
using System.IO;
using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.Transversal.Clases;
using BSI.Integra.Aplicacion.Transversal.Helper;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.pipeline;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using QRCoder;
using BSI.Integra.Persistencia.Models;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: Planificacion/Documentos
    /// Autor: Fischer Valdez - Luis Huallpa - Edgar Serruto - Priscila Pacsi - Gian Miranda
    /// Fecha: 09/02/2021
    /// <summary>
    /// BO para la logica de los documentos
    /// </summary>
    public class DocumentosBO
    {
        /// Propiedades	                                Significado
        /// -----------	                                ------------
        /// ListadoAlertas                              Listado de objetos del tipo AlertDTO
        /// ListaDocumentos                             Listado de objetos del tipo ProgramaDocumentosDTO
        /// PEspecifico                                 Objeto del tipo PespecificoDTO
        /// Oportunidad                                 Objeto del tipo DatosOportunidadDocumentosCompuestoDTO
        /// ContenidoCertificado                        Objeto del tipo CertificadoGeneradoAutomaticoContenidoBO
        /// _repOportunidad                             Repositorio de la tabla com.T_Oportunidad
        /// _repPespecifico                             Repositorio de la tabla pla.T_PEspecifico
        /// _repPgeneral                                Repositorio de la tabla pla.T_PGeneral
        /// _repAlumno                                  Repositorio de la tabla mkt.T_Alumno
        /// _repMontoPagoCronograma                     Repositorio de la tabla com.T_MontoPagoCronograma             
        /// _repMoneda                                  Repositorio de la tabla pla.T_Moneda
        /// _repMontoPago                               Repositorio de la tabla pla.T_MontoPago
        /// _repDocumentoComercialPw                    Repositorio de la tabla pla.T_DocumentacionComercial_PW
        /// _repPais                                    Repositorio de la tabla conf.T_Pais
        /// _repCiudad                                  Repositorio de la tabla conf.T_Ciudad
        /// _repMatriculaCabecera                       Repositorio de la tabla fin.T_MatriculaCabecera
        /// _repDocumentoSeccionPw                      Repositorio de la tabla pla.T_DocumentoSeccion_PW
        /// _repCronogramaPagoDetalleFinal              Repositorio de la tabla fin.T_CronogramaPagoDetalleFinal
        /// _repPgeneralAsubPgeneral                    Repositorio de la tabla pla.T_PGeneralASubPGeneral
        /// _repPersonal                                Repositorio de la tabla gp.T_Personal
        /// _repPlantilla                               Repositorio de la tabla mkt.T_Plantilla
        /// _repOportunidadClasificacionOperaciones     Repositorio de la tabla ope.T_OportunidadClasificacionOperaciones
        /// _repCertificadoGeneradoAutomatico           Repositorio de la tabla pla.T_CertificadoGeneradoAutomatico
        /// _repContenidoCertificadoIrca                Repositorio de la tabla ope.T_ContenidoCertificadoIrca
        /// _repMatriculaCabeceraBeneficios             Repositorio de la tabla com.T_MatriculaCabeceraBeneficios
        /// _repPgeneralConfiguracionBeneficios         Repositorio de la tabla pla.T_ConfiguracionBeneficioProgramaGeneral
        /// path_convenios                              Ruta inicial de los convenios
        /// path_conveniosazure                         Ruta inicial Azure de los convenios
        /// path_condiciones                            Ruta inicial de las condiciones
        /// path_requisitos                             Ruta inicial de los requisitos
        /// direccionSilabo                             Ruta inicial de los silabos
        /// DireccionConvenio                           Ruta inicial de los convenios
        /// DireccionCondicion                          Ruta inicial de las condiciones
        /// DireccionRequisitosSH                       Ruta inicial de los requisitos SH
        /// DireccionSilabo                             Ruta inicial de los silabos
        /// Contenido                                   Ruta inicial de los contenidos
        
        private List<AlertDTO> ListadoAlertas;
        public List<ProgramaDocumentosDTO> ListaDocumentos;
        public PespecificoDTO PEspecifico;
        public DatosOportunidadDocumentosCompuestoDTO Oportunidad;
        public CertificadoGeneradoAutomaticoContenidoBO ContenidoCertificado;

        private OportunidadRepositorio _repOportunidad;
        private PespecificoRepositorio _repPespecifico;
        private PgeneralRepositorio _repPgeneral;
        private AlumnoRepositorio _repAlumno;
        private MontoPagoCronogramaRepositorio _repMontoPagoCronograma;
        private MonedaRepositorio _repMoneda;
        private MontoPagoRepositorio _repMontoPago;
        private DocumentacionComercialPwRepositorio _repDocumentoComercialPw;
        private PaisRepositorio _repPais;
        private CiudadRepositorio _repCiudad;
        private MatriculaCabeceraRepositorio _repMatriculaCabecera;
        private DocumentoSeccionPwRepositorio _repDocumentoSeccionPw;
        private CronogramaPagoDetalleFinalRepositorio _repCronogramaPagoDetalleFinal;
        private PgeneralAsubPgeneralRepositorio _repPgeneralAsubPgeneral;

        private readonly PersonalRepositorio _repPersonal;
        private readonly PlantillaRepositorio _repPlantilla;
        private readonly OportunidadClasificacionOperacionesRepositorio _repOportunidadClasificacionOperaciones;
        private readonly CertificadoGeneradoAutomaticoRepositorio _repCertificadoGeneradoAutomatico;
        private readonly ContenidoCertificadoIrcaRepositorio _repContenidoCertificadoIrca;
        private readonly MatriculaCabeceraBeneficiosRepositorio _repMatriculaCabeceraBeneficios;
        private readonly PgeneralConfiguracionBeneficioRepositorio _repPgeneralConfiguracionBeneficios;

        private string path_convenios = @"~\repositorioweb\documentos\convenios\";
        private string path_conveniosazure = @"https://repositorioweb.blob.core.windows.net/documentos/convenios/";
        private string path_condiciones = @"~\repositorioweb\documentos\condiciones\";
        private string path_requisitos = @"~\repositorioweb\documentos\requisitos-sh\";
        private string direccionSilabo = @"~/repositorioweb/documentos/silabos/";

        private string DireccionConvenio = @"~/repositorioweb/documentos/convenios/";
        private string DireccionCondicion = @"~/repositorioweb/documentos/condiciones/";
        private string DireccionRequisitosSH = @"~/repositorioweb/documentos/requisitos-sh/";
        private string DireccionSilabo = @"~/repositorioweb/documentos/silabos/";
        private string Contenido = string.Empty;

        private EsquemaEvaluacionBO esquemaBO;
        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Crear un objeto DocumentoBO
        /// </summary> 
        public DocumentosBO()
        {
            _repOportunidad = new OportunidadRepositorio();
            _repPespecifico = new PespecificoRepositorio();
            _repPgeneral = new PgeneralRepositorio();
            _repAlumno = new AlumnoRepositorio();
            _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio();
            _repMoneda = new MonedaRepositorio();
            _repMontoPago = new MontoPagoRepositorio();
            _repDocumentoComercialPw = new DocumentacionComercialPwRepositorio();
            _repPais = new PaisRepositorio();
            _repCiudad = new CiudadRepositorio();
            _repMatriculaCabecera = new MatriculaCabeceraRepositorio();
            _repDocumentoSeccionPw = new DocumentoSeccionPwRepositorio();
            _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio();
            _repPgeneralAsubPgeneral = new PgeneralAsubPgeneralRepositorio();

            _repPlantilla = new PlantillaRepositorio();
            _repPersonal = new PersonalRepositorio();
            _repOportunidadClasificacionOperaciones = new OportunidadClasificacionOperacionesRepositorio();
            _repCertificadoGeneradoAutomatico = new CertificadoGeneradoAutomaticoRepositorio();
            _repContenidoCertificadoIrca = new ContenidoCertificadoIrcaRepositorio();
            _repMatriculaCabeceraBeneficios = new MatriculaCabeceraBeneficiosRepositorio();
            _repPgeneralConfiguracionBeneficios = new PgeneralConfiguracionBeneficioRepositorio();
            esquemaBO = new EsquemaEvaluacionBO();
            CargarListaDocumentos();
        }

        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Crear un objeto DocumentoBO
        /// </summary> 
        public DocumentosBO(integraDBContext integraDBContext)
        {
            _repOportunidad = new OportunidadRepositorio(integraDBContext);
            _repPespecifico = new PespecificoRepositorio(integraDBContext);
            _repPgeneral = new PgeneralRepositorio(integraDBContext);
            _repAlumno = new AlumnoRepositorio(integraDBContext);
            _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio(integraDBContext);
            _repMoneda = new MonedaRepositorio(integraDBContext);
            _repMontoPago = new MontoPagoRepositorio(integraDBContext);
            _repDocumentoComercialPw = new DocumentacionComercialPwRepositorio(integraDBContext);
            _repPais = new PaisRepositorio(integraDBContext);
            _repCiudad = new CiudadRepositorio(integraDBContext);
            _repMatriculaCabecera = new MatriculaCabeceraRepositorio(integraDBContext);
            _repDocumentoSeccionPw = new DocumentoSeccionPwRepositorio(integraDBContext);
            _repCronogramaPagoDetalleFinal = new CronogramaPagoDetalleFinalRepositorio(integraDBContext);
            _repPgeneralAsubPgeneral = new PgeneralAsubPgeneralRepositorio(integraDBContext);

            _repPlantilla = new PlantillaRepositorio(integraDBContext);
            _repPersonal = new PersonalRepositorio(integraDBContext);
            _repOportunidadClasificacionOperaciones = new OportunidadClasificacionOperacionesRepositorio(integraDBContext);
            _repCertificadoGeneradoAutomatico = new CertificadoGeneradoAutomaticoRepositorio(integraDBContext);
            _repContenidoCertificadoIrca = new ContenidoCertificadoIrcaRepositorio(integraDBContext);
            _repMatriculaCabeceraBeneficios = new MatriculaCabeceraBeneficiosRepositorio(integraDBContext);
            _repPgeneralConfiguracionBeneficios = new PgeneralConfiguracionBeneficioRepositorio(integraDBContext);
            esquemaBO= new EsquemaEvaluacionBO(integraDBContext);
            CargarListaDocumentos();
        }

        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Obtiene oportunidad por Programa Especifico
        /// </summary> 
        public void ObtenerOportunidadPespecifico(int idActividadDetalle)
        {

            Oportunidad = _repOportunidad.ObtenerDatosCompuestosPorActividades(idActividadDetalle);
            PEspecifico = _repPespecifico.ObtenerPespecificoPorCentroCosto(Oportunidad.IdCentroCosto.Value);
        }

        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Obtiene documentos por una oportunidad.
        /// </summary> 
        public void ObtenerDocumentos(int idActividadDetalle)
        {
            string _queryOportunidad = string.Empty;
            string _queryCentroCosto = string.Empty;
            string _queryPEspecifico = string.Empty;
            string _queryPGeneral = string.Empty;


            Oportunidad = _repOportunidad.ObtenerDatosCompuestosPorActividades(idActividadDetalle);

            PEspecifico = _repPespecifico.ObtenerPespecificoPorCentroCosto(Oportunidad.IdCentroCosto.Value);

            PgeneralDTO PGeneral = _repPgeneral.ObtenerPgeneralporId(PEspecifico.IdProgramaGeneral.Value);

            //Brochure BSG Institute

            ListaDocumentos.Where(w => w.Id == 0).FirstOrDefault().Mensaje = "Correcto";
            ListaDocumentos.Where(w => w.Id == 0).FirstOrDefault().Url = "https://repositorioweb.blob.core.windows.net/repositorioweb/documentos/brochure/BSGINSTITUTE-2019.pdf";
            ListaDocumentos.Where(w => w.Id == 0).FirstOrDefault().MensajeDetalle = "Disponible Para todos";

            //Brochure

            ListaDocumentos.Where(w => w.Id == 1).FirstOrDefault().Mensaje = PGeneral.UrlBrochurePrograma == null ? "Incorrecto" : "Correcto";
            ListaDocumentos.Where(w => w.Id == 1).FirstOrDefault().Url = PGeneral.UrlBrochurePrograma;
            ListaDocumentos.Where(w => w.Id == 1).FirstOrDefault().MensajeDetalle = "Disponible Para <b> todos los programas</b> / No colocaron la URL del brochure en el Programa General";

            //Cronogram Alumnos 

            if (PEspecifico.Tipo != "Online Asincronica")
            {

                if (PEspecifico.UrlDocumentoCronograma != null)
                {
                    ListaDocumentos.Where(w => w.Id == 2).FirstOrDefault().Mensaje = "Correcto";
                    ListaDocumentos.Where(w => w.Id == 2).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial y Online sincrónico</b> / No actualizaron el cronograma de alumnos.";
                    ListaDocumentos.Where(w => w.Id == 2).FirstOrDefault().Url = PEspecifico.UrlDocumentoCronograma;
                }
                else
                {
                    ListaDocumentos.Where(w => w.Id == 2).FirstOrDefault().Mensaje = "Incorrecto";
                    ListaDocumentos.Where(w => w.Id == 2).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial y Online sincrónico</b> / No actualizaron el cronograma de alumnos.";
                }
            }
            else
            {
                ListaDocumentos.Where(w => w.Id == 2).FirstOrDefault().Mensaje = "Incorrecto";
                ListaDocumentos.Where(w => w.Id == 2).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial y Online sincrónico</b> / No actualizaron el cronograma de alumnos.";
            }

            //Requisitos Programas Online

            if (PEspecifico.Tipo == "Online Sincronica")//cambiar online Sincronica
            {

                byte[] DocumentoRequisitos = GenerarRequisitos(Oportunidad, 3);//idDocumento 3
                if (DocumentoRequisitos != null)
                {
                    ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().Mensaje = "Correcto";
                    ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Online sincrónico.</b>";
                    ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().DocumentoByte = DocumentoRequisitos;
                    ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().Url = "archivoGenerado";
                }
                else
                {
                    ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().Mensaje = "Incorrecto";
                    ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Online sincrónico.</b>";
                }
            }
            else
            {
                ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().Mensaje = "Incorrecto";
                ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Online sincrónico.</b>";
            }


            //Pagare Word

            if (PEspecifico.Ciudad != null)
            {
                if (PEspecifico.Ciudad == "BOGOTA")
                {
                    ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().Url = "https://bsginstitute.com/repositorioweb/documentos/pagares/PagarePresencialOnline-Colombia.docx";
                    ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().Mensaje = "Correcto";
                    ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial Perú y Colombia</b>";
                }
                else if (PEspecifico.Ciudad == "SANTA CRUZ" || PEspecifico.Ciudad == "LA PAZ")//bolivia
                {
                    ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().Url = "https://bsginstitute.com/repositorioweb/documentos/pagares/PAGARE-BSGINSTITUTEBOLIVIA.docx";
                    ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().Mensaje = "Correcto";
                    ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial Perú ,Colombia y Bolivia</b>";
                }
                else
                {
                    ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().Url = "https://bsginstitute.com/repositorioweb/documentos/pagares/PagarePresencial-Nacional-Extranjero.docx";
                    ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().Mensaje = "Correcto";
                    ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial Perú y Colombia</b>";
                }
            }
            else
            {
                ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().Mensaje = "Incorrecto";
                ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial Perú y Colombia</b>";
            }

            //Pagare Excel

            if (PEspecifico.Ciudad != null)
            {
                if (PEspecifico.Ciudad.ToUpper() == "AREQUIPA" || PEspecifico.Ciudad.ToUpper() == "LIMA")//PERU
                {
                    ListaDocumentos.Where(w => w.Id == 5).FirstOrDefault().Url = "https://bsginstitute.com/repositorioweb/documentos/pagares/PagareOnline-NacionalExtranjero.xlsx";
                }
                else if (PEspecifico.Ciudad.ToUpper() == "BOGOTA")//COLOMBIA
                {
                    ListaDocumentos.Where(w => w.Id == 5).FirstOrDefault().Url = "https://bsginstitute.com/repositorioweb/documentos/pagares/Pagare-Colombia.xlsx";
                }
                else if (PEspecifico.Ciudad == "LA PAZ" || PEspecifico.Ciudad == "SANTA CRUZ")//BOLIVIA
                {
                    ListaDocumentos.Where(w => w.Id == 5).FirstOrDefault().Url = "https://bsginstitute.com/repositorioweb/documentos/pagares/Pagare-Bolivia.xlsx";
                }
                else
                {
                    ListaDocumentos.Where(w => w.Id == 5).FirstOrDefault().Url = "https://bsginstitute.com/repositorioweb/documentos/pagares/PagareOnline-NacionalExtranjero.xlsx";
                }
                ListaDocumentos.Where(w => w.Id == 5).FirstOrDefault().Mensaje = "Correcto";
                ListaDocumentos.Where(w => w.Id == 5).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Online asincrónico-sincrónico Extranjero</b>";
            }
            else
            {
                ListaDocumentos.Where(w => w.Id == 5).FirstOrDefault().Mensaje = "Incorrecto";
                ListaDocumentos.Where(w => w.Id == 5).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Online asincrónico-sincrónico Extranjero</b>";
            }

            //convenio


            byte[] DocumentoConvenio = GenerarConvenioCondicion(Oportunidad, "Convenio", 6);//idDocumento 6

            if (DocumentoConvenio != null)
            {
                ListaDocumentos.Where(w => w.Id == 6).FirstOrDefault().Mensaje = "Correcto";
                ListaDocumentos.Where(w => w.Id == 6).FirstOrDefault().MensajeDetalle = "Disponible programa <b>Presencial Perú y Colombia, Extranjero Online sincrónico-asincrónico </b>, se genera cuando el cronograma de pagos este aprobado o el alumno está matriculado.";
                ListaDocumentos.Where(w => w.Id == 6).FirstOrDefault().Url = "archivoGenerado";
                ListaDocumentos.Where(w => w.Id == 6).FirstOrDefault().DocumentoByte = DocumentoConvenio;
            }
            else
            {
                ListaDocumentos.Where(w => w.Id == 6).FirstOrDefault().Mensaje = "Incorrecto";
                ListaDocumentos.Where(w => w.Id == 6).FirstOrDefault().MensajeDetalle = "Disponible programa <b>Presencial Perú y Colombia, Extranjero Online sincrónico-asincrónico </b>, se genera cuando el cronograma de pagos este aprobado o el alumno está matriculado.";
            }

            //condiciones

            if (PEspecifico.Tipo == "Online Asincronica" || PEspecifico.Tipo == "Online Sincronica")
            {
                byte[] DocumentoCondicion = GenerarConvenioCondicion(Oportunidad, "Condiciones", 7);//idDocumento 7
                if (DocumentoCondicion != null)
                {
                    ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().Mensaje = "Correcto";
                    ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().MensajeDetalle = "Disponible para programas <b>Perú y Colombia Online sincrónico-asincrónico</b> se genera cuando el cronograma de pagos este aprobado o el alumno está matriculado.";
                    ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().Url = "archivoGenerado";
                    ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().DocumentoByte = DocumentoCondicion;
                }
                else
                {
                    ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().Mensaje = "Incorrecto";
                    ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().MensajeDetalle = "Disponible para programas <b>Perú y Colombia Online sincrónico-asincrónico</b> se genera cuando el cronograma de pagos este aprobado o el alumno está matriculado.";
                }
            }
            else
            {
                ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().Mensaje = "Incorrecto";
                ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().MensajeDetalle = "Disponible para programas <b>Perú y Colombia Online sincrónico-asincrónico</b> se genera cuando el cronograma de pagos este aprobado o el alumno está matriculado.";
            }

            //Silabo

            string DocumentoSilabo = GeneraSilabo(PGeneral);
            if (!string.IsNullOrEmpty(DocumentoSilabo))
            {
                string _urlConcatenada = PEspecifico.IdProgramaGeneral + "*" + DocumentoSilabo;
                var DocumentoSilaboPDF = generar_silabo_bytes(PGeneral);
                ListaDocumentos.Where(w => w.Id == 8).FirstOrDefault().Mensaje = "Correcto";
                ListaDocumentos.Where(w => w.Id == 8).FirstOrDefault().MensajeDetalle = "Disponible para todos los programas / Programa nuevo, pendiente sílabo";
                ListaDocumentos.Where(w => w.Id == 8).FirstOrDefault().Url = _urlConcatenada;
                ListaDocumentos.Where(w => w.Id == 8).FirstOrDefault().DocumentoByte = DocumentoSilaboPDF;
            }
            else
            {
                ListaDocumentos.Where(w => w.Id == 8).FirstOrDefault().Mensaje = "Incorrecto";
                ListaDocumentos.Where(w => w.Id == 8).FirstOrDefault().MensajeDetalle = "Disponible para todos los programas / Programa nuevo, pendiente sílabo";
            }

            //ContratoUsoDatos

            if (PEspecifico.Tipo == "Online Asincronica" || PEspecifico.Tipo == "Online Sincronica" || PEspecifico.Tipo == "Presencial")
            {
                byte[] DocumentoContrato = GenerarContratoUsoDatos(Oportunidad, "Contrato", 9);//idDocumento 9
                if (DocumentoContrato != null)
                {
                    ListaDocumentos.Where(w => w.Id == 9).FirstOrDefault().Mensaje = "Correcto";
                    ListaDocumentos.Where(w => w.Id == 9).FirstOrDefault().MensajeDetalle = "Disponible programa <b>Presencial, Online sincrónico-asincrónico de alumnos de México</b> ,es posible descargar y se genera cuando el cronograma de pagos este aprobado o el alumno está matriculado, deben tener los datos reales generados del cliente.";
                    ListaDocumentos.Where(w => w.Id == 9).FirstOrDefault().Url = "archivoGenerado";
                    ListaDocumentos.Where(w => w.Id == 9).FirstOrDefault().DocumentoByte = DocumentoContrato;
                }
                else
                {
                    ListaDocumentos.Where(w => w.Id == 9).FirstOrDefault().Mensaje = "Incorrecto";
                    ListaDocumentos.Where(w => w.Id == 9).FirstOrDefault().MensajeDetalle = "Disponible programa <b>Presencial, Online sincrónico-asincrónico de alumnos de México</b> ,es posible descargar y se genera cuando el cronograma de pagos este aprobado o el alumno está matriculado, deben tener los datos reales generados del cliente.";
                }
            }
            else
            {
                ListaDocumentos.Where(w => w.Id == 9).FirstOrDefault().Mensaje = "Incorrecto";
                ListaDocumentos.Where(w => w.Id == 9).FirstOrDefault().MensajeDetalle = "Disponible programas <b>sin Modalidad.</b>";
            }

            //Lineamientos de condiciones

            if (PEspecifico.Tipo == "Online Asincronica" || PEspecifico.Tipo == "Online Sincronica" || PEspecifico.Tipo == "Presencial")
            {
                byte[] DocumentoContrato = GenerarContratoUsoDatos(Oportunidad, "Lineamiento", 10);//idDocumento 10
                if (DocumentoContrato != null)
                {
                    ListaDocumentos.Where(w => w.Id == 10).FirstOrDefault().Mensaje = "Correcto";
                    ListaDocumentos.Where(w => w.Id == 10).FirstOrDefault().MensajeDetalle = "Disponible programa Presencial, Online sincrónico-asincrónico de alumnos de México ,es posible descargar y se genera cuando el cronograma de pagos este aprobado o el alumno está matriculado, deben tener los datos reales generados del cliente.";
                    ListaDocumentos.Where(w => w.Id == 10).FirstOrDefault().Url = "archivoGenerado";
                    ListaDocumentos.Where(w => w.Id == 10).FirstOrDefault().DocumentoByte = DocumentoContrato;
                }
                else
                {
                    ListaDocumentos.Where(w => w.Id == 10).FirstOrDefault().Mensaje = "Incorrecto";
                    ListaDocumentos.Where(w => w.Id == 10).FirstOrDefault().MensajeDetalle = "Disponible programa Presencial, Online sincrónico-asincrónico de alumnos de México ,es posible descargar y se genera cuando el cronograma de pagos este aprobado o el alumno está matriculado, deben tener los datos reales generados del cliente.";
                }
            }
            else
            {
                ListaDocumentos.Where(w => w.Id == 10).FirstOrDefault().Mensaje = "Incorrecto";
                ListaDocumentos.Where(w => w.Id == 10).FirstOrDefault().MensajeDetalle = "Disponible programas <b>sin Modalidad.</b>";
            }

            //Convenio de prestación de servicios firma presencial

            if (PEspecifico.Tipo == "Online Asincronica" || PEspecifico.Tipo == "Online Sincronica" || PEspecifico.Tipo == "Presencial")
            {
                byte[] DocumentoContrato = GenerarContratoUsoDatos(Oportunidad, "Convenio Prestacion", 11);//idDocumento 11
                if (DocumentoContrato != null)
                {
                    ListaDocumentos.Where(w => w.Id == 11).FirstOrDefault().Mensaje = "Correcto";
                    ListaDocumentos.Where(w => w.Id == 11).FirstOrDefault().MensajeDetalle = "Disponible programa Presencial, Online sincrónico-asincrónico de alumnos de México ,es posible descargar y se genera cuando el cronograma de pagos este aprobado o el alumno está matriculado, deben tener los datos reales generados del cliente.";
                    ListaDocumentos.Where(w => w.Id == 11).FirstOrDefault().Url = "archivoGenerado";
                    ListaDocumentos.Where(w => w.Id == 11).FirstOrDefault().DocumentoByte = DocumentoContrato;
                }
                else
                {
                    ListaDocumentos.Where(w => w.Id == 11).FirstOrDefault().Mensaje = "Incorrecto";
                    ListaDocumentos.Where(w => w.Id == 11).FirstOrDefault().MensajeDetalle = "Disponible programa Presencial, Online sincrónico-asincrónico de alumnos de México ,es posible descargar y se genera cuando el cronograma de pagos este aprobado o el alumno está matriculado, deben tener los datos reales generados del cliente.";
                }
            }
            else
            {
                ListaDocumentos.Where(w => w.Id == 11).FirstOrDefault().Mensaje = "Incorrecto";
                ListaDocumentos.Where(w => w.Id == 11).FirstOrDefault().MensajeDetalle = "Disponible programas <b>sin Modalidad.</b>";
            }

            //Cronogram Alumnos (Todos los grupos)

            if (PEspecifico.Tipo != "Online Asincronica")
            {

                if (PEspecifico.UrlDocumentoCronogramaGrupos != null)
                {
                    ListaDocumentos.Where(w => w.Id == 12).FirstOrDefault().Mensaje = "Correcto";
                    ListaDocumentos.Where(w => w.Id == 12).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial y Online sincrónico</b> / No actualizaron el cronograma de alumnos.";
                    ListaDocumentos.Where(w => w.Id == 12).FirstOrDefault().Url = PEspecifico.UrlDocumentoCronogramaGrupos;
                }
                else
                {
                    ListaDocumentos.Where(w => w.Id == 12).FirstOrDefault().Mensaje = "Incorrecto";
                    ListaDocumentos.Where(w => w.Id == 12).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial y Online sincrónico</b> / No actualizaron el cronograma de alumnos.";
                }
            }
            else
            {
                ListaDocumentos.Where(w => w.Id == 12).FirstOrDefault().Mensaje = "Incorrecto";
                ListaDocumentos.Where(w => w.Id == 12).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial y Online sincrónico</b> / No actualizaron el cronograma de alumnos.";
            }



        }

        /// <summary>
        /// Agrega las alertar a sus respectivos documentos
        /// En caso los alertas por documentos sean repetidos, solo deja uno
        /// </summary>
        public void ListarAlertarAListadoDocumentos()
        {
            foreach (var documento in ListaDocumentos)
            {
                documento.ListadoAlertas = ListadoAlertas.Where(x => x.IdDocumento == documento.Id).ToList();
                documento.ListadoAlertas = documento.ListadoAlertas.DistinctBy(x => x.Mensaje).ToList();
            }
        }
        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Obtiene documentos por el Id de Alumno
        /// </summary> 
        public void ObtenerDocumentosIdAlumno(int IdAlumno)
        {
            string _queryOportunidad = string.Empty;
            string _queryCentroCosto = string.Empty;
            string _queryPEspecifico = string.Empty;
            string _queryPGeneral = string.Empty;


            var registroDocumentos = _repPgeneral.ObtenerPgeneralparaDocumentosporIdAlumno(IdAlumno);

            Oportunidad = _repOportunidad.ObtenerDatosCompuestosPorActividades(registroDocumentos.IdActividadDetalle);

            //PEspecifico = _repPespecifico.ObtenerPespecificoPorCentroCosto(Oportunidad.IdCentroCosto.Value);

            //PgeneralDTO PGeneral = _repPgeneral.ObtenerPgeneralporId(PEspecifico.IdProgramaGeneral.Value);

            //Brochure BSG Institute

            ListaDocumentos.Where(w => w.Id == 0).FirstOrDefault().Mensaje = "Correcto";
            ListaDocumentos.Where(w => w.Id == 0).FirstOrDefault().Url = "https://repositorioweb.blob.core.windows.net/repositorioweb/documentos/brochure/BSGINSTITUTE-2019.pdf";
            ListaDocumentos.Where(w => w.Id == 0).FirstOrDefault().MensajeDetalle = "Disponible Para todos";

            //Brochure

            ListaDocumentos.Where(w => w.Id == 1).FirstOrDefault().Mensaje = registroDocumentos.UrlBrochurePrograma == null ? "Incorrecto" : "Correcto";
            ListaDocumentos.Where(w => w.Id == 1).FirstOrDefault().Url = registroDocumentos.UrlBrochurePrograma;
            ListaDocumentos.Where(w => w.Id == 1).FirstOrDefault().MensajeDetalle = "Disponible Para <b> todos los programas</b> / No colocaron la URL del brochure en el Programa General";

            //Cronogram Alumnos 

            if (registroDocumentos.Tipo != "Online Asincronica")
            {

                if (registroDocumentos.UrlDocumentoCronograma != null)
                {
                    ListaDocumentos.Where(w => w.Id == 2).FirstOrDefault().Mensaje = "Correcto";
                    ListaDocumentos.Where(w => w.Id == 2).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial y Online sincrónico</b> / No actualizaron el cronograma de alumnos.";
                    ListaDocumentos.Where(w => w.Id == 2).FirstOrDefault().Url = registroDocumentos.UrlDocumentoCronograma;
                }
                else
                {
                    ListaDocumentos.Where(w => w.Id == 2).FirstOrDefault().Mensaje = "Incorrecto";
                    ListaDocumentos.Where(w => w.Id == 2).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial y Online sincrónico</b> / No actualizaron el cronograma de alumnos.";
                }
            }
            else
            {
                ListaDocumentos.Where(w => w.Id == 2).FirstOrDefault().Mensaje = "Incorrecto";
                ListaDocumentos.Where(w => w.Id == 2).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial y Online sincrónico</b> / No actualizaron el cronograma de alumnos.";
            }

            //Requisitos Programas Online

            if (registroDocumentos.Tipo == "Online Sincronica")//cambiar online Sincronica
            {

                byte[] DocumentoRequisitos = GenerarRequisitos(Oportunidad, 3);//idDocumento
                if (DocumentoRequisitos != null)
                {
                    ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().Mensaje = "Correcto";
                    ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Online sincrónico.</b>";
                    ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().DocumentoByte = DocumentoRequisitos;
                    ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().Url = "archivoGenerado";
                }
                else
                {
                    ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().Mensaje = "Incorrecto";
                    ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Online sincrónico.</b>";
                }
            }
            else
            {
                ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().Mensaje = "Incorrecto";
                ListaDocumentos.Where(w => w.Id == 3).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Online sincrónico.</b>";
            }


            //Pagare Word

            if (registroDocumentos.Ciudad != null)
            {
                if (registroDocumentos.Ciudad == "BOGOTA")
                {
                    ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().Url = "https://bsginstitute.com/repositorioweb/documentos/pagares/PagarePresencialOnline-Colombia.docx";
                    ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().Mensaje = "Correcto";
                    ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial Perú y Colombia</b>";
                }
                else if (registroDocumentos.Ciudad == "SANTA CRUZ" || registroDocumentos.Ciudad == "LA PAZ")//bolivia
                {
                    ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().Url = "https://bsginstitute.com/repositorioweb/documentos/pagares/PAGARE-BSGINSTITUTEBOLIVIA.docx";
                    ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().Mensaje = "Correcto";
                    ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial Perú ,Colombia y Bolivia</b>";
                }
                else
                {
                    ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().Url = "https://bsginstitute.com/repositorioweb/documentos/pagares/PagarePresencial-Nacional-Extranjero.docx";
                    ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().Mensaje = "Correcto";
                    ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial Perú y Colombia</b>";
                }
            }
            else
            {
                ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().Mensaje = "Incorrecto";
                ListaDocumentos.Where(w => w.Id == 4).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Presencial Perú y Colombia</b>";
            }

            //Pagare Excel

            if (registroDocumentos.Ciudad != null)
            {
                ListaDocumentos.Where(w => w.Id == 5).FirstOrDefault().Url = "https://bsginstitute.com/repositorioweb/documentos/pagares/PagareOnline-NacionalExtranjero.xlsx";
                ListaDocumentos.Where(w => w.Id == 5).FirstOrDefault().Mensaje = "Correcto";
                ListaDocumentos.Where(w => w.Id == 5).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Online asincrónico-sincrónico Extranjero</b>";
            }
            else
            {
                ListaDocumentos.Where(w => w.Id == 5).FirstOrDefault().Mensaje = "Incorrecto";
                ListaDocumentos.Where(w => w.Id == 5).FirstOrDefault().MensajeDetalle = "Disponible programas <b>Online asincrónico-sincrónico Extranjero</b>";
            }

            //convenio


            var DocumentoConvenio = GenerarConvenioCondicionDocumentoDTO(Oportunidad, "Convenio", 6);//idDocumento 6

            if (DocumentoConvenio != null)
            {
                ListaDocumentos.Where(w => w.Id == 6).FirstOrDefault().Mensaje = "Correcto";
                ListaDocumentos.Where(w => w.Id == 6).FirstOrDefault().MensajeDetalle = "Disponible programa <b>Presencial Perú y Colombia, Extranjero Online sincrónico-asincrónico </b>, se genera cuando el cronograma de pagos este aprobado o el alumno está matriculado.";
                ListaDocumentos.Where(w => w.Id == 6).FirstOrDefault().Url = DocumentoConvenio.NombreArchivo;
                ListaDocumentos.Where(w => w.Id == 6).FirstOrDefault().DocumentoByte = DocumentoConvenio.registroMemoria;
            }
            else
            {
                ListaDocumentos.Where(w => w.Id == 6).FirstOrDefault().Mensaje = "Incorrecto";
                ListaDocumentos.Where(w => w.Id == 6).FirstOrDefault().MensajeDetalle = "Disponible programa <b>Presencial Perú y Colombia, Extranjero Online sincrónico-asincrónico </b>, se genera cuando el cronograma de pagos este aprobado o el alumno está matriculado.";
            }

            //condiciones

            if (registroDocumentos.Tipo == "Online Asincronica" || registroDocumentos.Tipo == "Online Sincronica")
            {
                var DocumentoCondicion = GenerarConvenioCondicionDocumentoDTO(Oportunidad, "Condiciones", 7);//idDocumento
                if (DocumentoCondicion != null)
                {
                    ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().Mensaje = "Correcto";
                    ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().MensajeDetalle = "Disponible para programas <b>Perú y Colombia Online sincrónico-asincrónico</b> se genera cuando el cronograma de pagos este aprobado o el alumno está matriculado.";
                    ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().Url = DocumentoCondicion.NombreArchivo;
                    ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().DocumentoByte = DocumentoCondicion.registroMemoria;
                }
                else
                {
                    ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().Mensaje = "Incorrecto";
                    ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().MensajeDetalle = "Disponible para programas <b>Perú y Colombia Online sincrónico-asincrónico</b> se genera cuando el cronograma de pagos este aprobado o el alumno está matriculado.";
                }
            }
            else
            {
                ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().Mensaje = "Incorrecto";
                ListaDocumentos.Where(w => w.Id == 7).FirstOrDefault().MensajeDetalle = "Disponible para programas <b>Perú y Colombia Online sincrónico-asincrónico</b> se genera cuando el cronograma de pagos este aprobado o el alumno está matriculado.";
            }

            //Silabo

            string DocumentoSilabo = GeneraSilabo(registroDocumentos);
            if (!string.IsNullOrEmpty(DocumentoSilabo))
            {
                string _urlConcatenada = registroDocumentos.IdProgramaGeneral + "*" + DocumentoSilabo;
                var DocumentoSilaboPDF = generar_silabo_bytes(registroDocumentos);
                ListaDocumentos.Where(w => w.Id == 8).FirstOrDefault().Mensaje = "Correcto";
                ListaDocumentos.Where(w => w.Id == 8).FirstOrDefault().MensajeDetalle = "Disponible para todos los programas / Programa nuevo, pendiente sílabo";
                ListaDocumentos.Where(w => w.Id == 8).FirstOrDefault().Url = DocumentoSilaboPDF.NombreArchivo;//_urlConcatenada;
                ListaDocumentos.Where(w => w.Id == 8).FirstOrDefault().DocumentoByte = DocumentoSilaboPDF.registroMemoria;//DocumentoSilaboPDF;
            }
            else
            {
                ListaDocumentos.Where(w => w.Id == 8).FirstOrDefault().Mensaje = "Incorrecto";
                ListaDocumentos.Where(w => w.Id == 8).FirstOrDefault().MensajeDetalle = "Disponible para todos los programas / Programa nuevo, pendiente sílabo";
            }


        }

        /// <summary>
        /// Genera Un Archivo En byte de acuerdo a programa
        /// </summary>
        /// <param name="pGeneral">Campos del programa General </param>
        /// <returns></returns>
        public byte[] generar_silabo_bytes(PgeneralDTO pGeneral)
        {
            byte[] archivo;

            try
            {
                List<PgeneralHijoDTO> listaCursos = _repPgeneralAsubPgeneral.ObtenerPGeneralHijos(pGeneral.Id);

                if (listaCursos != null && listaCursos.Count() > 0)//Si es mayor es un perfil
                {
                    PgeneralDocumentoSeccionDTO registroPrograma = _repPgeneral.ObtenePgeneralDocumentoPorId(pGeneral.Id);

                    foreach (var programa in listaCursos)
                    {
                        List<SeccionDocumentoDTO> contenido = _repDocumentoSeccionPw.ObtenerDocumentoSeccion(pGeneral.Id);
                        if (contenido.Count > 0)//Si hay secciones disponibles para ese programa general
                        {
                            var silaboOrdenado = contenido.OrderBy(o => o.Orden).ToList();
                            programa.ListaSeccion = new List<SeccionDocumentoDTO>();
                            foreach (var variable in silaboOrdenado)
                            {
                                programa.ListaSeccion.Add(variable);
                            }
                        }
                    }
                    archivo = GenerarDocumentoPerfilBytes(listaCursos, registroPrograma);
                }
                else // caso contrario un silabo
                {
                    PgeneralDocumentoSeccionDTO registroPrograma = _repPgeneral.ObtenePgeneralDocumentoPorId(pGeneral.Id);

                    if (registroPrograma != null)
                    {
                        List<SeccionDocumentoDTO> contenido = _repDocumentoSeccionPw.ObtenerDocumentoSeccion(pGeneral.Id);
                        if (contenido.Count > 0)
                        {
                            var silaboOrdenado = contenido.OrderBy(o => o.Orden).ToList();
                            registroPrograma.ListaSeccion = new List<SeccionDocumentoDTO>();
                            foreach (var variable in silaboOrdenado)
                            {
                                registroPrograma.ListaSeccion.Add(variable);
                            }
                        }
                    }
                    archivo = GenerarDocumentoSilaboBytes(registroPrograma);
                }
                return archivo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Genera silabo por Programa General
        /// </summary> 
        public archivosAlumno generar_silabo_bytes(InformacionProgramaDocumentosDTO pGeneral)
        {
            try
            {
                List<PgeneralHijoDTO> listaCursos = _repPgeneralAsubPgeneral.ObtenerPGeneralHijos(pGeneral.IdProgramaGeneral);

                if (listaCursos != null && listaCursos.Count() > 0)//Si es mayor es un perfil
                {
                    PgeneralDocumentoSeccionDTO registroPrograma = _repPgeneral.ObtenePgeneralDocumentoPorId(pGeneral.IdProgramaGeneral);

                    foreach (var programa in listaCursos)
                    {
                        List<SeccionDocumentoDTO> contenido = _repDocumentoSeccionPw.ObtenerDocumentoSeccion(pGeneral.IdProgramaGeneral);
                        if (contenido.Count > 0)
                        {
                            var silaboOrdenado = contenido.OrderBy(o => o.Orden).ToList();
                            programa.ListaSeccion = new List<SeccionDocumentoDTO>();
                            foreach (var variable in silaboOrdenado)
                            {
                                programa.ListaSeccion.Add(variable);
                            }
                        }
                    }
                    var archivo = GenerarDocumentoPerfilBytesDocumentoDTO(listaCursos, registroPrograma);
                    return archivo;
                }
                else // caso contrario un silabo
                {
                    PgeneralDocumentoSeccionDTO registroPrograma = _repPgeneral.ObtenePgeneralDocumentoPorId(pGeneral.IdProgramaGeneral);

                    if (registroPrograma != null)
                    {
                        List<SeccionDocumentoDTO> contenido = _repDocumentoSeccionPw.ObtenerDocumentoSeccion(pGeneral.IdProgramaGeneral);
                        if (contenido.Count > 0)
                        {
                            var silaboOrdenado = contenido.OrderBy(o => o.Orden).ToList();
                            registroPrograma.ListaSeccion = new List<SeccionDocumentoDTO>();
                            foreach (var variable in silaboOrdenado)
                            {
                                registroPrograma.ListaSeccion.Add(variable);
                            }
                        }
                    }
                    var archivo = GenerarDocumentoSilaboBytesDocumentoDTO(registroPrograma);
                    return archivo;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Genera el Contenido del archivo Silabo
        /// </summary>
        /// <param name="modelo"></param>
        /// <param name="programa"></param>
        /// <returns></returns>
        private byte[] GenerarDocumentoPerfilBytes(List<PgeneralHijoDTO> modelo, PgeneralDocumentoSeccionDTO programa)
        {
            string nombreDocumento = string.Empty, nombreDocumentoTemp = string.Empty;
            string subcabecera = string.Empty;
            string nombresDoc = string.Empty;

            using (MemoryStream ms = new MemoryStream())
            {
                FontFactory.RegisterDirectories();
                using (Document _document = new Document(iTextSharp.text.PageSize.A4, 70f, 50f, 80f, 70f))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(_document, ms);
                    pdfWriter.PageEvent = new ITextEventsDocumentos();
                    _document.AddTitle("Silabo-BSGInstitute");

                    nombresDoc = GenerateSlug("Silabo_" + programa.Nombre);
                    nombreDocumento = direccionSilabo + nombresDoc + ".pdf";
                    nombreDocumentoTemp = nombresDoc + ".pdf";

                    HTMLWorker htmlparser = new HTMLWorker(_document);
                    htmlparser.SetStyleSheet(GenerateStyleSheet3());

                    try
                    {
                        _document.Open();
                        Contenido = string.Empty;
                        Contenido += "<h1>" + programa.Nombre + "</h1>";
                        Contenido += "<p style='padding-bottom: 15px;border-bottom-style: solid;border-bottom-width: 1px;'><strong>Duracion: </strong>" + programa.pw_duracion + "</p>";

                        htmlparser.Parse(new StringReader(Contenido));
                        //document.Add(new iTextSharp.text.Paragraph("_______________________________________________________________________"));
                        htmlparser.Parse(new StringReader("<p>_______________________________________________________________________</p>"));
                        htmlparser.Parse(new StringReader("<br/>"));

                        foreach (var programas in modelo)
                        {
                            htmlparser.Parse(new StringReader("<h1>" + programas.Nombre + "</h1>"));
                            htmlparser.Parse(new StringReader("<p><strong>Duracion: </strong>" + programas.pw_duracion + "</p>"));

                            if (programas.ListaSeccion.Count > 0)
                            {
                                foreach (var registro in programas.ListaSeccion)
                                {
                                    if (!(string.IsNullOrEmpty(registro.Contenido)))
                                    {
                                        string _centenido = registro.Contenido.Replace("<table>", "<br/><table>");
                                        _centenido = _centenido.Replace("</table>", "</table><br/>");
                                        _centenido = _centenido.Replace("<li> ", "<li>").Replace(" <li>", "<li>");
                                        _centenido = _centenido.Replace("<li>", "<li> - ");

                                        htmlparser.Parse(new StringReader("<h1>" + registro.Titulo + "</h1>"));
                                        htmlparser.Parse(new StringReader(_centenido));
                                        htmlparser.Parse(new StringReader("<br/>"));
                                    }

                                }
                                _document.NewPage();
                            }
                            else
                            {
                                break;
                            }

                        }

                        _document.Close();
                    }
                    catch (Exception ex)
                    {
                        _document.Close();
                    }
                }
                return ms.ToArray();
            }
        }

        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Genera documento de perfil.
        /// </summary> 
        private archivosAlumno GenerarDocumentoPerfilBytesDocumentoDTO(List<PgeneralHijoDTO> modelo, PgeneralDocumentoSeccionDTO programa)
        {
            string nombreDocumento = string.Empty, nombreDocumentoTemp = string.Empty;
            string subcabecera = string.Empty;
            string nombresDoc = string.Empty;
            string blobDirecion = @"documentos/perfiles";
            string urlDocumento = "https://repositorioweb.blob.core.windows.net/";

            archivosAlumno _respuesta = new archivosAlumno();

            using (MemoryStream ms = new MemoryStream())
            {
                FontFactory.RegisterDirectories();
                using (Document _document = new Document(iTextSharp.text.PageSize.A4, 70f, 50f, 80f, 70f))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(_document, ms);
                    pdfWriter.PageEvent = new ITextEventsDocumentos();
                    _document.AddTitle("Silabo-BSGInstitute");

                    nombresDoc = GenerateSlug("Perfil_" + programa.Nombre);
                    nombreDocumento = direccionSilabo + nombresDoc + ".pdf";
                    nombreDocumentoTemp = nombresDoc + ".pdf";

                    HTMLWorker htmlparser = new HTMLWorker(_document);
                    htmlparser.SetStyleSheet(GenerateStyleSheet3());

                    try
                    {
                        _document.Open();
                        Contenido = string.Empty;
                        Contenido += "<h1>" + programa.Nombre + "</h1>";
                        Contenido += "<p style='padding-bottom: 15px;border-bottom-style: solid;border-bottom-width: 1px;'><strong>Duracion: </strong>" + programa.pw_duracion + "</p>";

                        htmlparser.Parse(new StringReader(Contenido));
                        //document.Add(new iTextSharp.text.Paragraph("_______________________________________________________________________"));
                        htmlparser.Parse(new StringReader("<p>_______________________________________________________________________</p>"));
                        htmlparser.Parse(new StringReader("<br/>"));

                        foreach (var programas in modelo)
                        {
                            htmlparser.Parse(new StringReader("<h1>" + programas.Nombre + "</h1>"));
                            htmlparser.Parse(new StringReader("<p><strong>Duracion: </strong>" + programas.pw_duracion + "</p>"));

                            if (programas.ListaSeccion.Count > 0)
                            {
                                foreach (var registro in programas.ListaSeccion)
                                {
                                    if (!(string.IsNullOrEmpty(registro.Contenido)))
                                    {
                                        string _centenido = registro.Contenido.Replace("<table>", "<br/><table>");
                                        _centenido = _centenido.Replace("</table>", "</table><br/>");
                                        _centenido = _centenido.Replace("<li> ", "<li>").Replace(" <li>", "<li>");
                                        _centenido = _centenido.Replace("<li>", "<li> - ");

                                        htmlparser.Parse(new StringReader("<h1>" + registro.Titulo + "</h1>"));
                                        htmlparser.Parse(new StringReader(_centenido));
                                        htmlparser.Parse(new StringReader("<br/>"));
                                    }

                                }
                                _document.NewPage();
                            }
                            else
                            {
                                break;
                            }

                        }

                        _document.Close();
                    }
                    catch (Exception ex)
                    {
                        _document.Close();
                    }
                }

                var pdf = ms.ToArray();

                registrarDocumentoBlob(pdf, nombresDoc, blobDirecion, "pdf");

                var raiz = ToURLSlug(nombresDoc) + ".pdf";

                _respuesta.NombreArchivo = urlDocumento + blobDirecion + "/" + raiz;
                _respuesta.registroMemoria = pdf;

                return _respuesta;
            }
        }
        /// <summary>
        /// Genera estilos Para los Documentos 
        /// </summary>
        /// <returns></returns>
        private StyleSheet GenerateStyleSheet3()
        {
            FontFactory.RegisterDirectories();

            StyleSheet css = new StyleSheet();

            css.LoadTagStyle("h1", "size", "12pt");
            css.LoadTagStyle("h1", "style", "font-weight:bold;");
            css.LoadTagStyle("p", "style", "text-align:justify;margin-bottom: 10px;");
            css.LoadTagStyle("ul", "style", "display:block;list-style-type:circle;");
            css.LoadTagStyle(HtmlTags.TABLE, HtmlTags.BORDER, "0.1");
            css.LoadTagStyle(HtmlTags.TABLE, HtmlTags.FONTSIZE, "12px");
            css.LoadTagStyle(HtmlTags.TABLE, HtmlTags.FONTSIZE, "12px");
            css.LoadTagStyle(HtmlTags.DIV, HtmlTags.FONTFAMILY, "Verdana");
            css.LoadTagStyle(HtmlTags.DIV, HtmlTags.FONTSIZE, "12px");
            css.LoadTagStyle(HtmlTags.H1, HtmlTags.FONTFAMILY, "Verdana");
            css.LoadTagStyle(HtmlTags.H2, HtmlTags.FONTFAMILY, "Verdana");
            css.LoadTagStyle(HtmlTags.H2, HtmlTags.FONTSIZE, "12px");
            css.LoadTagStyle(HtmlTags.H3, HtmlTags.FONTFAMILY, "Verdana");
            css.LoadTagStyle(HtmlTags.H3, HtmlTags.FONTSIZE, "12px");
            css.LoadTagStyle(HtmlTags.H4, HtmlTags.FONTFAMILY, "Verdana");
            css.LoadTagStyle(HtmlTags.H4, HtmlTags.FONTSIZE, "12px");
            css.LoadTagStyle(HtmlTags.P, HtmlTags.FONTFAMILY, "Verdana");
            css.LoadTagStyle(HtmlTags.P, HtmlTags.FONTSIZE, "12px");
            css.LoadStyle(HtmlTags.UL, HtmlTags.STYLE, "list-style-type: disc;");
            return css;
        }
        /// <summary>
        /// Genera Contenido al Documento Silabo
        /// </summary>
        /// <param name="modelo"></param>
        /// <returns></returns>
        private byte[] GenerarDocumentoSilaboBytes(PgeneralDocumentoSeccionDTO modelo)
        {
            string nombreDocumento = string.Empty, nombreDocumentoTemp = string.Empty;
            string subcabecera = string.Empty;
            string nombresDoc = string.Empty;

            using (MemoryStream ms = new MemoryStream())
            {
                FontFactory.RegisterDirectories();
                using (Document _document = new Document(iTextSharp.text.PageSize.A4, 70f, 50f, 80f, 70f))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(_document, ms);
                    pdfWriter.PageEvent = new ITextEventsDocumentos();
                    _document.AddTitle("Silabo-BSGInstitute");

                    nombresDoc = GenerateSlug("Silabo_" + modelo.Nombre);
                    nombreDocumento = direccionSilabo + nombresDoc + ".pdf";
                    nombreDocumentoTemp = nombresDoc + ".pdf";

                    HTMLWorker htmlparser = new HTMLWorker(_document);
                    htmlparser.SetStyleSheet(GenerateStyleSheet3());

                    try
                    {
                        _document.Open();
                        Contenido = string.Empty;
                        Contenido += "<h1>" + modelo.Nombre + "</h1>";
                        Contenido += "<p style='padding-bottom: 15px;border-bottom-style: solid;border-bottom-width: 1px;'><strong>Duracion: </strong>" + modelo.pw_duracion + "</p>";

                        htmlparser.Parse(new StringReader(Contenido));

                        foreach (var registro in modelo.ListaSeccion)
                        {
                            if (!(string.IsNullOrEmpty(registro.Contenido)))
                            {
                                string _centenido = registro.Contenido.Replace("<table>", "<br/><table>");
                                _centenido = _centenido.Replace("</table>", "</table><br/>");
                                _centenido = _centenido.Replace("<li> ", "<li>").Replace(" <li>", "<li>");
                                _centenido = _centenido.Replace("<li>", "<li> - ");

                                htmlparser.Parse(new StringReader("<h1>" + registro.Titulo + "</h1>"));
                                htmlparser.Parse(new StringReader(_centenido));
                                htmlparser.Parse(new StringReader("<br/>"));
                            }
                        }
                        _document.Close();
                    }
                    catch (Exception ex)
                    {
                        _document.Close();
                    }
                }
                return ms.ToArray();
            }
        }

        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Genera silabo por modelo
        /// </summary> 
        private archivosAlumno GenerarDocumentoSilaboBytesDocumentoDTO(PgeneralDocumentoSeccionDTO modelo)
        {
            string nombreDocumento = string.Empty, nombreDocumentoTemp = string.Empty;
            string subcabecera = string.Empty;
            string nombresDoc = string.Empty;

            string blobDirecion = @"documentos/silabos";
            string urlDocumento = "https://repositorioweb.blob.core.windows.net/";

            archivosAlumno _respuesta = new archivosAlumno();

            using (MemoryStream ms = new MemoryStream())
            {
                FontFactory.RegisterDirectories();
                using (Document _document = new Document(iTextSharp.text.PageSize.A4, 70f, 50f, 80f, 70f))
                {
                    PdfWriter pdfWriter = PdfWriter.GetInstance(_document, ms);
                    pdfWriter.PageEvent = new ITextEventsDocumentos();
                    _document.AddTitle("Silabo-BSGInstitute");

                    nombresDoc = GenerateSlug("Silabo_" + modelo.Nombre);
                    nombreDocumento = direccionSilabo + nombresDoc + ".pdf";
                    nombreDocumentoTemp = nombresDoc + ".pdf";

                    HTMLWorker htmlparser = new HTMLWorker(_document);
                    htmlparser.SetStyleSheet(GenerateStyleSheet3());

                    try
                    {
                        _document.Open();
                        Contenido = string.Empty;
                        Contenido += "<h1>" + modelo.Nombre + "</h1>";
                        Contenido += "<p style='padding-bottom: 15px;border-bottom-style: solid;border-bottom-width: 1px;'><strong>Duracion: </strong>" + modelo.pw_duracion + "</p>";

                        htmlparser.Parse(new StringReader(Contenido));

                        foreach (var registro in modelo.ListaSeccion)
                        {
                            if (!(string.IsNullOrEmpty(registro.Contenido)))
                            {
                                string _centenido = registro.Contenido.Replace("<table>", "<br/><table>");
                                _centenido = _centenido.Replace("</table>", "</table><br/>");
                                _centenido = _centenido.Replace("<li> ", "<li>").Replace(" <li>", "<li>");
                                _centenido = _centenido.Replace("<li>", "<li> - ");

                                htmlparser.Parse(new StringReader("<h1>" + registro.Titulo + "</h1>"));
                                htmlparser.Parse(new StringReader(_centenido));
                                htmlparser.Parse(new StringReader("<br/>"));
                            }
                        }
                        _document.Close();
                    }
                    catch (Exception ex)
                    {
                        _document.Close();
                    }
                }

                var pdf = ms.ToArray();

                registrarDocumentoBlob(pdf, nombresDoc, blobDirecion, "pdf");

                var raiz = ToURLSlug(nombresDoc) + ".pdf";

                _respuesta.NombreArchivo = urlDocumento + blobDirecion + "/" + raiz;
                _respuesta.registroMemoria = pdf;

                return _respuesta;
            }
        }

        /// <summary>
        /// Genera Nombre del Documento Silabo
        /// </summary>
        /// <param name="pgeneral"> capos de Programa General</param>
        /// <returns></returns>
        private string GeneraSilabo(PgeneralDTO pgeneral)
        {
            string archivo = string.Empty;


            try
            {
                List<PgeneralHijoDTO> listaCursos = _repPgeneralAsubPgeneral.ObtenerPGeneralHijos(pgeneral.Id);

                if (listaCursos != null && listaCursos.Count() > 0)
                {
                    PgeneralDocumentoSeccionDTO registroPrograma = _repPgeneral.ObtenePgeneralDocumentoPorId(pgeneral.Id);

                    foreach (var programa in listaCursos)
                    {
                        List<SeccionDocumentoDTO> contenido = _repDocumentoSeccionPw.ObtenerDocumentoSeccion(pgeneral.Id);
                        if (contenido.Count > 0)
                        {
                            var silaboOrdenado = contenido.OrderBy(o => o.Orden).ToList();
                            programa.ListaSeccion = new List<SeccionDocumentoDTO>();
                            foreach (var variable in silaboOrdenado)
                            {
                                programa.ListaSeccion.Add(variable);
                            }
                        }
                    }
                    archivo = ValidarDocumentoPerfil(listaCursos, registroPrograma);
                }
                else
                {
                    PgeneralDocumentoSeccionDTO registroPrograma = _repPgeneral.ObtenePgeneralDocumentoPorId(pgeneral.Id);
                    if (registroPrograma != null)
                    {
                        List<SeccionDocumentoDTO> contenido = _repDocumentoSeccionPw.ObtenerDocumentoSeccion(pgeneral.Id);
                        if (contenido.Count > 0)
                        {
                            var silaboOrdenado = contenido.OrderBy(o => o.Orden).ToList();
                            registroPrograma.ListaSeccion = new List<SeccionDocumentoDTO>();
                            foreach (var variable in silaboOrdenado)
                            {
                                registroPrograma.ListaSeccion.Add(variable);
                            }
                        }
                    }
                    archivo = ValidarDocumentoSilabo(registroPrograma);
                }
                return archivo;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Genera silabo por Programa General
        /// </summary> 
        private string GeneraSilabo(InformacionProgramaDocumentosDTO pgeneral)
        {
            string archivo = string.Empty;


            try
            {
                List<PgeneralHijoDTO> listaCursos = _repPgeneralAsubPgeneral.ObtenerPGeneralHijos(pgeneral.IdProgramaGeneral);

                if (listaCursos != null && listaCursos.Count() > 0)
                {
                    PgeneralDocumentoSeccionDTO registroPrograma = _repPgeneral.ObtenePgeneralDocumentoPorId(pgeneral.IdProgramaGeneral);

                    foreach (var programa in listaCursos)
                    {
                        List<SeccionDocumentoDTO> contenido = _repDocumentoSeccionPw.ObtenerDocumentoSeccion(pgeneral.IdProgramaGeneral);
                        if (contenido.Count > 0)
                        {
                            var silaboOrdenado = contenido.OrderBy(o => o.Orden).ToList();
                            programa.ListaSeccion = new List<SeccionDocumentoDTO>();
                            foreach (var variable in silaboOrdenado)
                            {
                                programa.ListaSeccion.Add(variable);
                            }
                        }
                    }
                    archivo = ValidarDocumentoPerfil(listaCursos, registroPrograma);
                }
                else
                {
                    PgeneralDocumentoSeccionDTO registroPrograma = _repPgeneral.ObtenePgeneralDocumentoPorId(pgeneral.IdProgramaGeneral);

                    if (registroPrograma != null)
                    {
                        List<SeccionDocumentoDTO> contenido = _repDocumentoSeccionPw.ObtenerDocumentoSeccion(pgeneral.IdProgramaGeneral);
                        if (contenido.Count > 0)
                        {
                            var silaboOrdenado = contenido.OrderBy(o => o.Orden).ToList();
                            registroPrograma.ListaSeccion = new List<SeccionDocumentoDTO>();
                            foreach (var variable in silaboOrdenado)
                            {
                                registroPrograma.ListaSeccion.Add(variable);
                            }
                        }
                    }
                    archivo = ValidarDocumentoSilabo(registroPrograma);
                }
                return archivo;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        /// <summary>
        /// Genera Nombre del Documento Silabo
        /// </summary>
        /// <param name="registroPrograma">Campos de programa general</param>
        /// <returns></returns>
        private string ValidarDocumentoSilabo(PgeneralDocumentoSeccionDTO registroPrograma)
        {
            string nombreDocumentoTemp = string.Empty;
            string nombresDoc = string.Empty;
            bool bandera = false;
            try
            {
                nombresDoc = GenerateSlug("Silabo_" + registroPrograma.Nombre);
                nombreDocumentoTemp = nombresDoc + ".pdf";
                bandera = true;

                foreach (var registro in registroPrograma.ListaSeccion)
                {
                    if (string.IsNullOrEmpty(registro.Contenido))
                    {
                        bandera = false;
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                bandera = false;
            }

            if (bandera)
            {
                return nombreDocumentoTemp;
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// Genera Nombre del Documento Silabo porPerfil
        /// </summary>
        /// <param name="listaCursos">lista de programa general</param>
        /// <param name="pgeneral"> campos de un programa general</param>
        /// <returns></returns>
        public string ValidarDocumentoPerfil(List<PgeneralHijoDTO> listaCursos, PgeneralDocumentoSeccionDTO pgeneral)
        {
            string nombreDocumentoTemp = string.Empty;
            string nombresDoc = string.Empty;
            bool bandera = true;

            try
            {
                nombresDoc = GenerateSlug("Silabo_" + pgeneral.Nombre);
                nombreDocumentoTemp = nombresDoc + ".pdf";
                bandera = true;

                foreach (var programas in listaCursos)
                {
                    foreach (var registro in programas.ListaSeccion)
                    {
                        if (string.IsNullOrEmpty(registro.Contenido))
                        {
                            bandera = false;
                            break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                bandera = false;
            }

            if (bandera)
            {
                return nombreDocumentoTemp;
            }
            else
            {
                return "";
            }
        }

        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Elimina tildes.
        /// </summary> 
        private string GenerateSlug(string s)
        {
            string datos = string.Empty;
            datos = s.ToLower();
            datos = datos.Replace("á", "a");
            datos = datos.Replace("é", "e");
            datos = datos.Replace("í", "i");
            datos = datos.Replace("ó", "o");
            datos = datos.Replace("ú", "u");
            datos = datos.Replace("ñ", "n");
            return Regex.Replace(datos, @"[^a-z0-9]+", "-", RegexOptions.IgnoreCase).Trim(new char[] { '-' });
        }
        /// <summary>
        /// Generar Documento Silabo
        /// </summary>
        /// <param name="oportunidad">Campos de Oportunidad</param>
        /// <returns></returns>
        private byte[] GenerarRequisitos(DatosOportunidadDocumentosCompuestoDTO oportunidad, int idDocumento)
        {
            string Modalidad = string.Empty;
            string NombrePrograma = string.Empty;
            string Requisitos = string.Empty;
            string _queryAlumno = string.Empty;
            int Pais = 0, IdProgramaEspecifico = 0;
            byte[] RequisitosByte = new byte[100];

            var al = _repAlumno.FirstBy(x => x.Id == Oportunidad.IdAlumno, x => new { x.IdCiudad, x.IdCodigoPais });
            if (al.IdCiudad == null || al.IdCodigoPais == null)
            {
                if (al.IdCodigoPais == null)
                {
                    ListadoAlertas.Add(new AlertDTO { IdDocumento = idDocumento, Mensaje = ErrorSistema.Instance.MensajeError(207) });
                }
                if (al.IdCiudad == null)
                {
                    ListadoAlertas.Add(new AlertDTO { IdDocumento = idDocumento, Mensaje = ErrorSistema.Instance.MensajeError(208) });
                }
                return null;
            }

            AlumnoCompuestoDocumentoDTO Alumno = _repAlumno.ObtenerDatosAlumnoDocumentoPorId(oportunidad.IdAlumno.Value);
            try
            {
                CuotasProgramaDTO ListaCuotasProgramaDTO = ObtenerCuotasProgramaDTO(oportunidad.Id);
                Modalidad = ListaCuotasProgramaDTO.TipoPrograma;
                NombrePrograma = ListaCuotasProgramaDTO.NombreCurso;
                IdProgramaEspecifico = ListaCuotasProgramaDTO.IdPespecifico;

                if (ListaCuotasProgramaDTO.NombrePespecifico.IndexOf("BOGOTA") != -1)
                {
                    Pais = 57;
                }
                else
                {
                    Pais = 51;
                }
                switch (Modalidad)
                {
                    case "Online Asincronica":
                        RequisitosByte = GenerarDocumentoRequisitosOnline(Alumno, NombrePrograma, "Online Asincronica");
                        break;
                    case "Online Sincronica":
                        RequisitosByte = GenerarDocumentoRequisitosOnline(Alumno, NombrePrograma, "Online Sincronica");
                        break;
                }

                return RequisitosByte;
            }
            catch (Exception e)
            {

            }


            return null;
        }

        /// <summary>
        /// Genera Contenido de Documento Requisitos
        /// </summary>
        /// <param name="Alumno"> campos de Alumno</param>
        /// <param name="NombrePrograma"></param>
        /// <param name="Modalidad">modalidad Online Asincronica,Online Sincronica</param>
        /// <returns></returns>
        private byte[] GenerarDocumentoRequisitosOnline(AlumnoCompuestoDocumentoDTO Alumno, string NombrePrograma, string Modalidad)
        {
            string NombreDocumento = string.Empty;
            string NombreDocumentoTemp = string.Empty;
            string SubCabecera = string.Empty;
            string Nombres, Apellidos;
            string _queryContenido;
            FontFactory.RegisterDirectories();
            Document document = new Document(PageSize.A4, 70, 50, 50, 50);
            try
            {
                if (Modalidad == "Online Asincronica")
                {
                    NombreDocumento = DireccionRequisitosSH + "declaracion-jurada-asincronico " + Alumno.Nombre1 + Alumno.ApellidoPaterno + ".pdf";
                    NombreDocumentoTemp = "declaracion-jurada-asincronico " + Alumno.Nombre1 + Alumno.ApellidoPaterno + ".pdf";

                    DocumentoComercialContenidoDTO _ContenidoDocumento = _repDocumentoComercialPw.DocumentoComercial_contenido("Requisitos", "OnlineAsincronico", 0);

                    Contenido = _ContenidoDocumento.Contenido;
                    SubCabecera = "Declaración jurada para programas asincrónicos";
                }
                else
                {
                    NombreDocumento = DireccionRequisitosSH + "requisitos-hadware-software " + Alumno.Nombre1 + Alumno.ApellidoPaterno + ".pdf";
                    NombreDocumentoTemp = "requisitos-hadware-software " + Alumno.Nombre1 + Alumno.ApellidoPaterno + ".pdf";

                    DocumentoComercialContenidoDTO _ContenidoDocumento = _repDocumentoComercialPw.DocumentoComercial_contenido("Requisitos", "Online", 0);

                    Contenido = _ContenidoDocumento.Contenido;
                    SubCabecera = "Requisitos de hardware y software para participar en programas online";
                }

                //primero seteamos la informacion del alumno
                Nombres = Alumno.Nombre1 + " " + Alumno.Nombre2;
                Apellidos = Alumno.ApellidoPaterno + " " + Alumno.ApellidoMaterno;

                Contenido = Contenido.Replace("##NOMBRESALUMNO##", Nombres + " " + Apellidos).Replace("##NRODOCUMENTO##", Alumno.Dni);
                Contenido = Contenido.Replace("##NOMBREPROGRAMA##", NombrePrograma);

                HTMLWorker htmlparser = new HTMLWorker(document);
                htmlparser.SetStyleSheet(GenerateStyleSheet2());

                //PdfWriter.GetInstance(document, new FileStream(System.Web.HttpContext.Current.Server.MapPath(NombreDocumento), FileMode.OpenOrCreate));
                System.IO.MemoryStream ms = new System.IO.MemoryStream();


                PdfWriter.GetInstance(document, ms);
                document.Open();

                PdfPTable table = new PdfPTable(3);
                table.TotalWidth = 500f;
                int[] firstTablecellwidth = { 60, 350, 90 };
                table.SetWidths(firstTablecellwidth);
                table.LockedWidth = true;

                table.AddCell(" ");

                Chunk titleFont1 = new Chunk("SISTEMA DE GESTIÓN DE LA CALIDAD", FontFactory.GetFont("Times New Roman", 10));
                Chunk titleFont2 = new iTextSharp.text.Chunk(SubCabecera, FontFactory.GetFont("Times New Roman", 10, Font.BOLD));

                Paragraph titulo1 = new Paragraph(titleFont1);
                titulo1.Alignment = Element.ALIGN_CENTER;
                Paragraph titulo2 = new Paragraph(titleFont2);
                titulo2.Alignment = Element.ALIGN_CENTER;

                PdfPTable columna2 = new PdfPTable(1);
                columna2.AddCell(titulo1);
                columna2.AddCell(titulo2);

                PdfPCell columnaFomat1 = new PdfPCell(columna2);
                columnaFomat1.Padding = 0f;

                table.AddCell(columnaFomat1);

                Chunk col31 = new iTextSharp.text.Chunk("RE-LAN-020", FontFactory.GetFont("Times New Roman", 10));
                Chunk col32 = new iTextSharp.text.Chunk("Revisión 00", FontFactory.GetFont("Times New Roman", 10));
                Chunk col33 = new iTextSharp.text.Chunk("14 feb 2012", FontFactory.GetFont("Times New Roman", 10));
                Paragraph titCel1 = new Paragraph(col31);
                Paragraph titCel2 = new Paragraph(col32);
                Paragraph titCel3 = new Paragraph(col33);

                PdfPTable columna3 = new PdfPTable(1);
                columna3.AddCell(titCel1);
                columna3.AddCell(titCel2);
                columna3.AddCell(titCel3);

                PdfPCell columnaFomat2 = new PdfPCell(columna3);
                columnaFomat2.HorizontalAlignment = Element.ALIGN_CENTER;
                columnaFomat2.Padding = 0f;

                table.AddCell(columnaFomat2);


                document.Add(table);

                var fc = new StringReader(Contenido);
                htmlparser.Parse(fc);

                document.Add(new iTextSharp.text.Paragraph(" "));
                document.Add(new iTextSharp.text.Paragraph(" "));
                document.Add(new iTextSharp.text.Paragraph(" "));
                document.Add(new iTextSharp.text.Paragraph(" "));
                document.Add(new iTextSharp.text.Paragraph("_______________"));
                document.Add(new iTextSharp.text.Paragraph("   EL ALUMNO   "));


                document.Close();


                byte[] bytesDocumento = ms.ToArray();


                return bytesDocumento;
            }
            catch (Exception ex)
            {
                document.Close();
                //return "LOG";
                return null;
            }

        }
        /// <summary>
        /// Genera Documentos de Convenio o Condicion segun parametros
        /// </summary>
        /// <param name="Oportunidad"> Parametros de Oportunidad</param>
        /// <param name="TipoDocumento"> Convenio o Condiciones</param>
        /// <param name="idDocumento"> Id del documento a generar</param>
        /// <returns></returns>
        private byte[] GenerarConvenioCondicion(DatosOportunidadDocumentosCompuestoDTO Oportunidad, string TipoDocumento, int idDocumento)
        {
            string Modalidad = string.Empty;
            string NombrePrograma = string.Empty;
            byte[] Convenio = new byte[1000];
            byte[] Condiciones = new byte[1000];
            string _queryAlumno = string.Empty;
            string _queryMontoPagoCronograma = string.Empty;
            string _queryMontoPago = string.Empty;
            int TipoPago = 0, Pais = 0, IdProgramaEspecifico = 0;
            var al = _repAlumno.FirstBy(x => x.Id == Oportunidad.IdAlumno, x => new { x.IdCiudad, x.IdCodigoPais });
            if (al.IdCiudad == null || al.IdCodigoPais == null)
            {
                if (al.IdCodigoPais == null)
                {
                    ListadoAlertas.Add(new AlertDTO { IdDocumento = idDocumento, Mensaje = ErrorSistema.Instance.MensajeError(207) });
                }
                if (al.IdCiudad == null)
                {
                    ListadoAlertas.Add(new AlertDTO { IdDocumento = idDocumento, Mensaje = ErrorSistema.Instance.MensajeError(208) });
                }
                return null;
            }

            AlumnoCompuestoDocumentoDTO Alumno = _repAlumno.ObtenerDatosAlumnoDocumentoPorId(Oportunidad.IdAlumno.Value);

            MontoPagoCronogramaDocumentoDTO MontoPagoCronograma = _repMontoPagoCronograma.ObtenerMontoPagoCronogramaDocumentoPorIdOportunidad(Oportunidad.Id);

            if (MontoPagoCronograma == null)
            {
                MontoPagoCronograma = _repMontoPagoCronograma.ObtenerMontoPagoCronogramaDocumentoPorIdOportunidadOperaciones(Oportunidad.Id);
            }


            if (MontoPagoCronograma != null)
            {
                //Agregado para Etiquetas
                MonedaCostoTotalConDescuentoDTO moneda = _repMoneda.ObtenerMonedaPorId(MontoPagoCronograma.IdMoneda.Value);

                Oportunidad.CostoTotalConDescuento = moneda.Simbolo + " " + MontoPagoCronograma.PrecioDescuento + " " + moneda.NombrePlural;

                MontoPagoPaqueteDTO montoPago = _repMontoPago.ObtenerPaquete(MontoPagoCronograma.IdMontoPago.Value);

                if (montoPago != null)
                {
                    Alumno.Paquete = montoPago.Paquete == null ? "" : montoPago.Paquete.ToString();
                }
                else
                {
                    Alumno.Paquete = "";
                }
            }
            else
            {
                Alumno.Paquete = "";
            }
            try
            {
                CuotasProgramaDTO ListaCuotasProgramaDTO = ObtenerCuotasProgramaDTO(Oportunidad.Id);
                if (ListaCuotasProgramaDTO == null)
                {
                    return null;
                }
                Modalidad = ListaCuotasProgramaDTO.TipoPrograma;
                NombrePrograma = ListaCuotasProgramaDTO.NombreCurso;
                IdProgramaEspecifico = ListaCuotasProgramaDTO.IdPespecifico;

                if (Alumno != null)
                {
                    if (Alumno.IdCodigoPais == 57)
                    {
                        Pais = 57;
                    }
                    else if (Alumno.IdCodigoPais == 591)
                    {
                        Pais = 591;
                    }
                    else if (Alumno.IdCodigoPais == 52)
                    {
                        Pais = 52;
                    }
                    else
                    {
                        Pais = 51;
                    }
                }
                else
                {
                    Pais = 51;
                }
                Alumno.IdOportunidad = Oportunidad.Id;
                if (TipoDocumento == "Convenio")
                {
                    switch (Modalidad)
                    {
                        case "Presencial":
                            Convenio = GenerarPDFConvenioCondiciones(Alumno, ListaCuotasProgramaDTO, TipoPago, Pais, TipoDocumento, "Presencial");
                            break;
                        case "Online Asincronica":
                            Convenio = GenerarPDFConvenioCondiciones(Alumno, ListaCuotasProgramaDTO, TipoPago, Pais, TipoDocumento, "Online");
                            break;
                        case "Online Sincronica":
                            Convenio = GenerarPDFConvenioCondiciones(Alumno, ListaCuotasProgramaDTO, TipoPago, Pais, TipoDocumento, "Online");
                            break;
                    }
                    return Convenio;
                }
                else
                {
                    switch (Modalidad)
                    {
                        case "Online Asincronica":
                            Condiciones = GenerarPDFConvenioCondiciones(Alumno, ListaCuotasProgramaDTO, TipoPago, Pais, TipoDocumento, "Online");

                            break;
                        case "Online Sincronica":
                            Condiciones = GenerarPDFConvenioCondiciones(Alumno, ListaCuotasProgramaDTO, TipoPago, Pais, TipoDocumento, "Online");

                            break;
                    }
                    return Condiciones;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Autor: Carlos Crispin, Jose Villena
        /// Fecha: 08/07/2021 
        /// Genera Documentos de Contrato de uso de datos/lineamientos de condiciones/convenio de prestacion Mexico
        /// </summary>
        /// <param name="Oportunidad"> Parametros de Oportunidad</param>
        /// <param name="TipoDocumento"> Convenio o Condiciones</param>
        /// <param name="idDocumento"> Id del documento a generar</param>
        /// <returns></returns>
        private byte[] GenerarContratoUsoDatos(DatosOportunidadDocumentosCompuestoDTO Oportunidad, string TipoDocumento, int idDocumento)
        {
            string Modalidad = string.Empty;
            string NombrePrograma = string.Empty;
            byte[] Contrato = new byte[1000];
            string _queryAlumno = string.Empty;
            string _queryMontoPagoCronograma = string.Empty;
            string _queryMontoPago = string.Empty;
            int TipoPago = 0, Pais = 0, IdProgramaEspecifico = 0;
            var al = _repAlumno.FirstBy(x => x.Id == Oportunidad.IdAlumno, x => new { x.IdCiudad, x.IdCodigoPais });
            if (al.IdCiudad == null || al.IdCodigoPais == null)
            {
                if (al.IdCodigoPais == null)
                {
                    ListadoAlertas.Add(new AlertDTO { IdDocumento = idDocumento, Mensaje = ErrorSistema.Instance.MensajeError(207) });
                }
                if (al.IdCiudad == null)
                {
                    ListadoAlertas.Add(new AlertDTO { IdDocumento = idDocumento, Mensaje = ErrorSistema.Instance.MensajeError(208) });
                }
                return null;
            }

            AlumnoCompuestoDocumentoDTO Alumno = _repAlumno.ObtenerDatosAlumnoDocumentoPorId(Oportunidad.IdAlumno.Value);

            MontoPagoCronogramaDocumentoDTO MontoPagoCronograma = _repMontoPagoCronograma.ObtenerMontoPagoCronogramaDocumentoPorIdOportunidad(Oportunidad.Id);

            if (MontoPagoCronograma == null)
            {
                MontoPagoCronograma = _repMontoPagoCronograma.ObtenerMontoPagoCronogramaDocumentoPorIdOportunidadOperaciones(Oportunidad.Id);
            }


            if (MontoPagoCronograma != null)
            {
                //Agregado para Etiquetas
                MonedaCostoTotalConDescuentoDTO moneda = _repMoneda.ObtenerMonedaPorId(MontoPagoCronograma.IdMoneda.Value);

                Oportunidad.CostoTotalConDescuento = moneda.Simbolo + " " + MontoPagoCronograma.PrecioDescuento + " " + moneda.NombrePlural;

                MontoPagoPaqueteDTO montoPago = _repMontoPago.ObtenerPaquete(MontoPagoCronograma.IdMontoPago.Value);

                if (montoPago != null)
                {
                    Alumno.Paquete = montoPago.Paquete == null ? "" : montoPago.Paquete.ToString();
                }
                else
                {
                    Alumno.Paquete = "";
                }
            }
            else
            {
                Alumno.Paquete = "";
            }
            try
            {
                CuotasProgramaDTO ListaCuotasProgramaDTO = ObtenerCuotasProgramaDTO(Oportunidad.Id);
                if (ListaCuotasProgramaDTO == null)
                {
                    return null;
                }
                Modalidad = ListaCuotasProgramaDTO.TipoPrograma;
                NombrePrograma = ListaCuotasProgramaDTO.NombreCurso;
                IdProgramaEspecifico = ListaCuotasProgramaDTO.IdPespecifico;

                if (Alumno != null)
                {
                    if (Alumno.IdCodigoPais == 57)
                    {
                        Pais = 57;
                    }
                    else if (Alumno.IdCodigoPais == 591)
                    {
                        Pais = 591;
                    }
                    else if(Alumno.IdCodigoPais == 52)
                    {
                        Pais = 52;
                    }
                    else 
                    {
                        Pais = 51;
                    }
                }
                else
                {
                    Pais = 51;
                }
                Alumno.IdOportunidad = Oportunidad.Id;
              
                switch (Modalidad)
                {
                    case "Presencial":
                        Contrato = GenerarPDFConvenioCondiciones(Alumno, ListaCuotasProgramaDTO, TipoPago, Pais, TipoDocumento, "Presencial");
                        break;
                    case "Online Asincronica":
                        Contrato = GenerarPDFConvenioCondiciones(Alumno, ListaCuotasProgramaDTO, TipoPago, Pais, TipoDocumento, "Online");
                        break;
                    case "Online Sincronica":
                        Contrato = GenerarPDFConvenioCondiciones(Alumno, ListaCuotasProgramaDTO, TipoPago, Pais, TipoDocumento, "Online");
                        break;
                }
                return Contrato;             

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Genera Convenio de condicion.
        /// </summary> 
        private archivosAlumno GenerarConvenioCondicionDocumentoDTO(DatosOportunidadDocumentosCompuestoDTO Oportunidad, string TipoDocumento, int idDocumento)
        {
            string Modalidad = string.Empty;
            string NombrePrograma = string.Empty;
            archivosAlumno ConvenioCondicion = new archivosAlumno();
            string _queryAlumno = string.Empty;
            string _queryMontoPagoCronograma = string.Empty;
            string _queryMontoPago = string.Empty;
            int TipoPago = 0, Pais = 0, IdProgramaEspecifico = 0;
            var al = _repAlumno.FirstBy(x => x.Id == Oportunidad.IdAlumno, x => new { x.IdCiudad, x.IdCodigoPais });

            if (al.IdCiudad == null || al.IdCodigoPais == null)
            {
                if (al.IdCodigoPais == null)
                {
                    ListadoAlertas.Add(new AlertDTO { IdDocumento = idDocumento, Mensaje = ErrorSistema.Instance.MensajeError(207) });
                }
                if (al.IdCiudad == null)
                {
                    ListadoAlertas.Add(new AlertDTO { IdDocumento = idDocumento, Mensaje = ErrorSistema.Instance.MensajeError(208) });
                }
                return null;
            }

            AlumnoCompuestoDocumentoDTO Alumno = _repAlumno.ObtenerDatosAlumnoDocumentoPorId(Oportunidad.IdAlumno.Value);

            MontoPagoCronogramaDocumentoDTO MontoPagoCronograma = _repMontoPagoCronograma.ObtenerMontoPagoCronogramaDocumentoPorIdOportunidad(Oportunidad.Id);

            if (MontoPagoCronograma != null)
            {
                //Agregado para Etiquetas
                MonedaCostoTotalConDescuentoDTO moneda = _repMoneda.ObtenerMonedaPorId(MontoPagoCronograma.IdMoneda.Value);

                Oportunidad.CostoTotalConDescuento = moneda.Simbolo + " " + MontoPagoCronograma.PrecioDescuento + " " + moneda.NombrePlural;

                MontoPagoPaqueteDTO montoPago = _repMontoPago.ObtenerPaquete(MontoPagoCronograma.IdMontoPago.Value);

                if (montoPago != null)
                {
                    Alumno.Paquete = montoPago.Paquete == null ? "" : montoPago.Paquete.ToString();
                }
                else
                {
                    Alumno.Paquete = "";
                }
            }
            else
            {
                Alumno.Paquete = "";
            }
            try
            {
                CuotasProgramaDTO ListaCuotasProgramaDTO = ObtenerCuotasProgramaDTO(Oportunidad.Id);
                if (ListaCuotasProgramaDTO == null)
                {
                    return null;
                }
                Modalidad = ListaCuotasProgramaDTO.TipoPrograma;
                NombrePrograma = ListaCuotasProgramaDTO.NombreCurso;
                IdProgramaEspecifico = ListaCuotasProgramaDTO.IdPespecifico;

                if (Alumno != null)
                {
                    if (Alumno.IdCodigoPais == 57)
                    {
                        Pais = 57;
                    }
                    else if (Alumno.IdCodigoPais == 591)
                    {
                        Pais = 591;
                    }
                    else
                    {
                        Pais = 51;
                    }
                }
                else
                {
                    Pais = 51;
                }
                Alumno.IdOportunidad = Oportunidad.Id;
                if (TipoDocumento == "Convenio")
                {
                    switch (Modalidad)
                    {
                        case "Presencial":
                            ConvenioCondicion = GenerarPDFConvenioCondicionesDocumentoDTO(Alumno, ListaCuotasProgramaDTO, TipoPago, Pais, TipoDocumento, "Presencial");
                            break;
                        case "Online Asincronica":
                            ConvenioCondicion = GenerarPDFConvenioCondicionesDocumentoDTO(Alumno, ListaCuotasProgramaDTO, TipoPago, Pais, TipoDocumento, "Online");
                            break;
                        case "Online Sincronica":
                            ConvenioCondicion = GenerarPDFConvenioCondicionesDocumentoDTO(Alumno, ListaCuotasProgramaDTO, TipoPago, Pais, TipoDocumento, "Online");
                            break;
                    }
                    return ConvenioCondicion;
                }
                else
                {
                    switch (Modalidad)
                    {
                        case "Online Asincronica":
                            ConvenioCondicion = GenerarPDFConvenioCondicionesDocumentoDTO(Alumno, ListaCuotasProgramaDTO, TipoPago, Pais, TipoDocumento, "Online");

                            break;
                        case "Online Sincronica":
                            ConvenioCondicion = GenerarPDFConvenioCondicionesDocumentoDTO(Alumno, ListaCuotasProgramaDTO, TipoPago, Pais, TipoDocumento, "Online");

                            break;
                    }
                    return ConvenioCondicion;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Genera PDF convenio de condiciones.
        /// </summary> 
        private byte[] GenerarPDFConvenioCondiciones(AlumnoCompuestoDocumentoDTO alumno, CuotasProgramaDTO listaCuotasProgramaDTO, int tipoPago, int pais, string tipoDocumento, string modalidad)
        {
            try
            {
                string nombreArchivo = "";
                string blobDirecion = "";
                string raiz = "https://repositorioweb.blob.core.windows.net/documentos/convenios/";             

                byte[] pdf = blobGenerarConvenioCondiciones(alumno, listaCuotasProgramaDTO, tipoPago, pais, tipoDocumento, modalidad);

                //registrarDocumentoBlob(pdf, nombreArchivo, blobDirecion, "pdf");

                raiz = ToURLSlug(nombreArchivo) + ".pdf";
                //return raiz;
                return pdf;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Genera PDF de convenio por condiciones.
        /// </summary> 
        private archivosAlumno GenerarPDFConvenioCondicionesDocumentoDTO(AlumnoCompuestoDocumentoDTO alumno, CuotasProgramaDTO listaCuotasProgramaDTO, int tipoPago, int pais, string tipoDocumento, string modalidad)
        {
            archivosAlumno _respuesta = new archivosAlumno();
            try
            {
                string nombreArchivo = "";
                string blobDirecion = "";
                string raiz = "";
                string urlDocumento = "https://repositorioweb.blob.core.windows.net/";

                if (tipoDocumento == "Convenio")
                {
                    nombreArchivo = "Convenio-capacitacion-formacion-" + alumno.Id + alumno.Nombre1.ToLower();
                    blobDirecion = @"documentos/convenios";
                }
                else
                {
                    nombreArchivo = "Condiciones-caracteristicas-servicio-" + alumno.Id + alumno.Nombre1.ToLower();
                    blobDirecion = @"documentos/condiciones";
                }

                byte[] pdf = blobGenerarConvenioCondiciones(alumno, listaCuotasProgramaDTO, tipoPago, pais, tipoDocumento, modalidad);

                registrarDocumentoBlob(pdf, nombreArchivo, blobDirecion, "pdf");

                raiz = ToURLSlug(nombreArchivo) + ".pdf";
                //return raiz;

                _respuesta.NombreArchivo = urlDocumento + blobDirecion + "/" + raiz;
                _respuesta.registroMemoria = pdf;

                return _respuesta;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Registra un documento en BLOB.
        /// </summary> 
        private void registrarDocumentoBlob(byte[] pdf, string nombreArchivo, string blobDirecion, string tipo)
        {
            string _azureStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=repositorioweb;AccountKey=JurvlnvFAqg4dcGqcDHEj9bkBLoLV3Z/EIxA+8QkdTcuCWTm1iZfgqUOfUOwmDMfnrmrie7Nkkho5mPyVTvIpA==;EndpointSuffix=core.windows.net";//ConfigurationManager.AppSettings["azureStorageConnectionString"];


            try
            {
                //Generar entrada al blob storage
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_azureStorageConnectionString);
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference(blobDirecion);

                CloudBlockBlob blockBlob = container.GetBlockBlobReference(ToURLSlug(nombreArchivo) + "." + tipo);
                blockBlob.Properties.ContentType = "application/" + tipo;
                blockBlob.Metadata["filename"] = ToURLSlug(nombreArchivo) + "." + tipo;
                blockBlob.Metadata["filemime"] = "application/" + tipo;
                Stream stream = new MemoryStream(pdf);
                //AsyncCallback UploadCompleted = new AsyncCallback(OnUploadCompleted);
                blockBlob.UploadFromStreamAsync(stream);

            }
            catch (Exception ex)
            {
                //Logger.Error(ex.ToString());
            }
        }

        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Funcion completar subida de archivo.
        /// </summary> 
        private void OnUploadCompleted(IAsyncResult result)
        {
            try
            {
                CloudBlockBlob blob = (CloudBlockBlob)result.AsyncState;
                blob.SetMetadataAsync();
                //blob.EndUploadFromStream(result);
            }
            catch (Exception ex)
            {
                //Logger.Error(ex.ToString());
            }
        }

        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Convierte nombre de archivo a minusculas y elimina "-".
        /// </summary> 
        private string ToURLSlug(string nombreArchivo)
        {
            return Regex.Replace(nombreArchivo, @"[^a-z0-9]+", "-", RegexOptions.IgnoreCase).Trim(new char[] { '-' }).ToLower();
        }

        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Genera convenio de condiciones, contrato mexico,lineamientos mexico,convenio de mexico.
        /// </summary> 
        private byte[] blobGenerarConvenioCondiciones(AlumnoCompuestoDocumentoDTO alumno, CuotasProgramaDTO listaCuotasProgramaDTO, int tipoPago, int pais, string tipoDocumento, string modalidad)
        {
            DateTime fecha_pago = new DateTime();
            string nombreDocumento = string.Empty, formatoPagos = string.Empty, simboloMoneda = string.Empty, totalPago = string.Empty;
            string nombres, apellidos, nombreMoneda;
            int tipoPais;
            decimal tipoCambio = 0;
            decimal pagoCuota = 0;
            string _queryContenidoDocumento = string.Empty;

            //Creacion del archivo en memoria
            using (MemoryStream ms = new MemoryStream())
            {
                FontFactory.RegisterDirectories();
                using (iTextSharp.text.Document _document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 70, 50, 50, 50))
                {

                    FontFactory.GetFont("Times New Roman", 14, iTextSharp.text.BaseColor.BLACK);

                    var firmas = FontFactory.GetFont("Times New Roman", 14);


                    try
                    {
                        if (pais == 57)
                        {
                            tipoPais = 57;
                        }
                        else if (pais == 591)
                        {
                            tipoPais = 591;
                        }
                        else if (pais == 52)
                        {
                            tipoPais = 52;
                        }
                        else
                        {
                            tipoPais = 51;
                        }
                        DocumentoComercialContenidoDTO _contenidoDocumento = _repDocumentoComercialPw.ObtnerDocumentoComercial_contenido(tipoDocumento, modalidad, tipoPais);
                            
                        if(_contenidoDocumento==null)
                        {
                            return null;
                        }

                        Contenido = _contenidoDocumento.Contenido;


                        if (tipoDocumento == "Convenio" || tipoDocumento == "Contrato" || tipoDocumento == "Lineamiento" || tipoDocumento == "Convenio Prestacion")
                        {
                            nombreDocumento = DireccionConvenio + "Convenio-capacitación-formación-" + alumno.Id + alumno.Nombre1.ToLower() + ".pdf";
                        }
                        else
                        {
                            nombreDocumento = DireccionCondicion + "Condiciones-caracteristicas-servicio-" + alumno.Id + alumno.Nombre1.ToLower() + ".pdf";
                        }

                        nombres = alumno.Nombre1 + " " + alumno.Nombre2;
                        apellidos = alumno.ApellidoPaterno + " " + alumno.ApellidoMaterno;

                        Contenido = Contenido.Replace("##NOMBRESALUMNO##", nombres.ToUpper() + " " + apellidos.ToUpper()).Replace("##TIPONRODOCUMENTO##", alumno.Dni);
                        int codpais = 0;
                        if (alumno.IdCodigoPais != null)
                        {
                            codpais = Convert.ToInt32(alumno.IdCodigoPais);
                        }

                        PaisNombreDTO pais__ = _repPais.ObtenerNombrePaisPorId(codpais);

                        string pais_ = pais__.NombrePais;

                        int codciudad = 0;
                        if (alumno.IdCiudad != null)
                        {
                            codciudad = Convert.ToInt32(alumno.IdCiudad);
                        }

                        CiudadNombreDTO ciudad__ = _repCiudad.ObtenerNombreCiudadPorId(codciudad);

                        string ciudad_ = ciudad__.Nombre;

                        Contenido = Contenido.Replace("##DIRECCION##", alumno.Direccion).Replace("##CIUDAD##", ciudad_);
                        Contenido = Contenido.Replace("##REGION##", alumno.NombreCiudad).Replace("##PAIS##", pais_);
                        Contenido = Contenido.Replace("##CORREO##", alumno.Correo);

                        MatriculaTemporalDTO matricula_ = new MatriculaTemporalDTO();

                        if (alumno.Paquete == null)
                        {
                            alumno.Paquete = "";
                        }

                        CodigoMatriculaDocumentoDTO matricula = _repMatriculaCabecera.CodigoMatriculaPorIdOportunidad(alumno.IdOportunidad.Value);

                        if (matricula != null)
                        {

                            Oportunidad.IdMatricula = matricula.CodigoMatricula;
                            matricula_.IdMatricula = matricula.Id;
                            matricula_.CodigoMatricula = matricula.CodigoMatricula;
                            matricula_.FechaMatricula = matricula.FechaMatricula;
                        }
                        else
                        {
                            matricula_ = _repMatriculaCabecera.ObtenerMatriculaPorOportunidad(alumno.IdOportunidad.Value);

                            Oportunidad.IdMatricula = matricula_.CodigoMatricula;
                        }



                        //var matricula_ = _tcrm_competidoresRepository.GetMatriculaByIdOportunidad(alumno.IdOportunidad);

                        DateTime? fecha_matri = new DateTime(2017, 9, 10);
                        if (matricula_ != null)
                        {
                            fecha_matri = matricula_.FechaMatricula;
                        }

                        DateTime validador_fecha = new DateTime(2017, 9, 11);

                        if (fecha_matri < validador_fecha)
                        {
                            Contenido = Contenido.Replace("##VERSION##", "");
                        }
                        else
                        {
                            Contenido = Contenido.Replace("##VERSION##", GenerarVersion(alumno.Paquete));
                        }

                        int? idBusqueda = 0;

                        //segundo remplazamos los datos del programa y del cronograma

                        idBusqueda = listaCuotasProgramaDTO.IdBusqueda;

                        Contenido = Contenido.Replace("##NOMBREPROGRAMA##", listaCuotasProgramaDTO.NombreCurso);
                        Contenido = Contenido.Replace("##DURACIONDIAS##", listaCuotasProgramaDTO.DuracionPespecifico + " horas");
                        Contenido = Contenido.Replace("##DURACIONMESES##", listaCuotasProgramaDTO.DuracionPGeneral + "");

                        if (listaCuotasProgramaDTO.NumeroCuotas != 1)
                        {
                            Contenido = Contenido.Replace("##FRACCIONADO##,", "en caso de solicitar el pago fraccionado, deberá pagar la suma señalada en el numeral 4 anterior");
                        }
                        else
                        {
                            Contenido = Contenido.Replace("##FRACCIONADO##,", ""); ;
                        }

                        formatoPagos += "<br>";

                        if (true)
                        {
                            switch (listaCuotasProgramaDTO.WebMoneda)
                            {
                                case "0":
                                    simboloMoneda = "S/";
                                    nombreMoneda = "Soles";
                                    totalPago = listaCuotasProgramaDTO.TotalPagar.ToString();
                                    break;
                                case "1":
                                    simboloMoneda = "US$";
                                    nombreMoneda = "Dolares";
                                    totalPago = listaCuotasProgramaDTO.TotalPagar.ToString();
                                    break;
                                case "2":
                                    simboloMoneda = "COP";
                                    nombreMoneda = "Colombianos";
                                    totalPago = listaCuotasProgramaDTO.WebTotalPagar.ToString();
                                    tipoCambio = listaCuotasProgramaDTO.WebTotalPagar.Value;
                                    break;
                                case "3":
                                    simboloMoneda = "BS";
                                    nombreMoneda = "Bolivianos";
                                    totalPago = listaCuotasProgramaDTO.WebTotalPagar.ToString();
                                    tipoCambio = listaCuotasProgramaDTO.WebTotalPagar.Value;
                                    break;
                            }

                            Contenido = Contenido.Replace("##MONTROTOTALCRONOGRAMA##", simboloMoneda + " " + totalPago + "");

                            if ((tipoDocumento == "Contrato" || tipoDocumento == "Lineamiento" || tipoDocumento == "Convenio Prestacion") && codpais == 52)
                            {
                                formatoPagos += "<strong>Nro. Cuota &nbsp;&nbsp; Monto(" + simboloMoneda + " " + totalPago + ") &nbsp;&nbsp; Fecha de vencimiento</strong><br>";
                                foreach (var pagos in listaCuotasProgramaDTO.ListaCuotas)
                                {
                                    if (listaCuotasProgramaDTO.WebMoneda == "2")
                                    {
                                        pagoCuota = pagos.Cuota.Value;
                                    }
                                    else
                                    {
                                        pagoCuota = pagos.Cuota.Value;
                                    }
                                    fecha_pago = pagos.FechaVencimiento.Value;
                                    formatoPagos += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + pagos.NroCuota + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + simboloMoneda + " " + pagoCuota.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + fecha_pago.ToString("dd/MM/yyyy") + "<br>";
                                }

                            }
                            else
                            {
                                if (listaCuotasProgramaDTO.NumeroCuotas == 1)
                                {
                                    formatoPagos = "EL ALUMNO, se obliga a cubrir el costo del PROGRAMA el cual asciende a un valor de " + simboloMoneda + " " + totalPago + " a ser cancelados en una sola cuota antes del ";
                                    foreach (var pagos in listaCuotasProgramaDTO.ListaCuotas)
                                    {
                                        fecha_pago = pagos.FechaVencimiento.Value;
                                    }
                                    formatoPagos += fecha_pago.ToString("dd/MM/yyyy") + ".";
                                }
                                else
                                {
                                    formatoPagos = "EL ALUMNO, se obliga a cubrir el costo del PROGRAMA el cual asciende a un valor de " + simboloMoneda + " " + totalPago + ". Para el caso en concreto, el alumno ha escogido la modalidad de pago en varias cuotas, por lo que se compromete a pagar el siguiente esquema de cuotas:<br><br>";
                                    formatoPagos += "<strong>Nro. Cuota &nbsp;&nbsp; Monto(" + simboloMoneda + " " + totalPago + ") &nbsp;&nbsp; Fecha de vencimiento</strong><br>";
                                    foreach (var pagos in listaCuotasProgramaDTO.ListaCuotas)
                                    {
                                        if (listaCuotasProgramaDTO.WebMoneda == "2")
                                        {
                                            pagoCuota = pagos.Cuota.Value;
                                        }
                                        else
                                        {
                                            pagoCuota = pagos.Cuota.Value;
                                        }
                                        fecha_pago = pagos.FechaVencimiento.Value;
                                        formatoPagos += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + pagos.NroCuota + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + simboloMoneda + " " + pagoCuota.ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + fecha_pago.ToString("dd/MM/yyyy") + "<br>";
                                    }
                                }
                            }

                           
                        }
                        /* VERSION ANTERIOR POR SI HAY QUE REVERTIR
						 
						PgeneralDocumentoSeccionDTO programa = new PgeneralDocumentoSeccionDTO();
						string anexo1 = string.Empty;
						string anexo2 = string.Empty;
						string anexo3CasoExcepcional = string.Empty;
						if (idBusqueda != 0)
						{
							programa = _repPgeneral.ObtenePgeneralPorIdBusqueda(idBusqueda.Value);

							List<SeccionDocumentoDTO> seccion = _repDocumentoSeccionPw.ObtenerSecciones(programa.Id);
							if (programa.ListaSeccion == null)
							{
								programa.ListaSeccion = new List<SeccionDocumentoDTO>();
							}
							foreach (var item in seccion)
							{
								programa.ListaSeccion.Add(item);
							}
							//programa.ListaSecciones = _tcrm_competidoresRepository.GetSeccionesbyIdPrograma(id_programa).ToList();
						}

						foreach (var item in programa.ListaSeccion)
						{
							if (item.Titulo.Contains("Estructura Curricular"))
							{
								anexo1 = "<h1>ANEXO 01</h1><h2>" + item.Titulo + "</h2><br/>";
								anexo1 += item.Contenido;
							}
							if (item.Titulo.Contains("Certificación"))
							{
								anexo2 = "<h1>ANEXO 02</h1><h2>" + item.Titulo + "</h2><br/>";
								anexo2 += item.Contenido;
							}
						}

						*/


                        PgeneralDocumentoSeccionDTO programa = new PgeneralDocumentoSeccionDTO();
                        string anexo1 = string.Empty;
                        string anexo2 = string.Empty;
                        string anexo3CasoExcepcional = string.Empty;
                        if (idBusqueda != 0)
                        {
                            programa = _repPgeneral.ObtenePgeneralPorIdBusqueda(idBusqueda.Value);
                            var listaSecciones = ObtenerListaSeccionDocumentoProgramaGeneral(programa.Id);
                            var seccion = GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones);

                            if (programa.ListaSeccionV2 == null)
                            {
                                programa.ListaSeccionV2 = new List<ProgramaGeneralSeccionAnexosHTMLDTO>();
                            }
                            foreach (var item in seccion)
                            {
                                programa.ListaSeccionV2.Add(item);
                            }
                        }
                        if (programa.ListaSeccionV2.Count > 0) //Imprime estructura curricular de la version v2
                        {
                            foreach (var item in programa.ListaSeccionV2)
                            {
                                if (item.Seccion.Contains("Estructura Curricular"))
                                {
                                    anexo1 = "<h1>ANEXO 01</h1><br><h3>" + item.Seccion + "</h3><br/>";
                                    anexo1 += item.Contenido;
                                }
                                if (item.Seccion.Contains("Certificacion"))
                                {
                                    anexo2 = "<h1>ANEXO 02</h1><br><h3>" + item.Seccion + "</h3><br/>";
                                    anexo2 += item.Contenido;
                                }
                            }
                        }
                        else //Imprime estructura curricular de la version 1
                        {
                            if (idBusqueda != 0)
                            {
                                programa = _repPgeneral.ObtenePgeneralPorIdBusqueda(idBusqueda.Value);

                                List<SeccionDocumentoDTO> seccion = _repDocumentoSeccionPw.ObtenerSecciones(programa.Id);
                                if (programa.ListaSeccion == null)
                                {
                                    programa.ListaSeccion = new List<SeccionDocumentoDTO>();
                                }
                                foreach (var item in seccion)
                                {
                                    programa.ListaSeccion.Add(item);
                                }
                                //programa.ListaSecciones = _tcrm_competidoresRepository.GetSeccionesbyIdPrograma(id_programa).ToList();
                            }

                            foreach (var item in programa.ListaSeccion)
                            {
                                if (item.Titulo.Contains("Estructura Curricular"))
                                {
                                    anexo1 = "<h1>ANEXO 01</h1><h2>" + item.Titulo + "</h2><br/>";
                                    anexo1 += item.Contenido;
                                }
                                if (item.Titulo.Contains("Certificación"))
                                {
                                    anexo2 = "<h1>ANEXO 02</h1><h2>" + item.Titulo + "</h2><br/>";
                                    anexo2 += item.Contenido;
                                }
                            }
                        }


                        int? idPaquete = null;
                        if (!string.IsNullOrEmpty(alumno.Paquete))
                        {
                            idPaquete = Convert.ToInt32(alumno.Paquete);
                        }
                        List<string> listaBeneficios = new List<string>();
                        var anexo3 = "";

						if (matricula_ != null)
						{
							listaBeneficios = ObtenerBeneficiosCongeladosPorMatriculaCabecera(matricula_.IdMatricula);
						}
						if (listaBeneficios.Count == 0)
						{
							listaBeneficios = ObtenerBeneficiosConfiguradosProgramaGeneral(programa.Id, alumno.IdCodigoPais, idPaquete);
						}

                        if (listaBeneficios.Count > 0) //Imprime beneficios de la Version 2
                        {
                            anexo3 += "<h1>ANEXO 03</h1><br/>";
                            switch (alumno.Paquete)
                            {
                                case "1":
                                    anexo3 += "<h4><strong><b>Version Basica</b></strong></h4><br/>";
                                    break;
                                case "2":
                                    anexo3 += "<h4><strong><b>Version Profesional</b></strong></h4><br/>";
                                    break;
                                case "3":
                                    anexo3 += "<h4><strong><b>Version Gerencial</b></strong></h4><br/>";
                                    break;
                                default:
                                    anexo3 += "<h4><strong><b>Beneficios</b></strong></h4><br/>";
                                    break;
                            }

                            anexo3 += "<ul type = 'disc'>";
                            foreach (var item in listaBeneficios)
                            {
                                var beneficio = item;
                                beneficio = beneficio.Replace("<p>", "");
                                beneficio = beneficio.Replace("</p>", "");
                                anexo3 += "<li>&bull;&nbsp;&nbsp;&nbsp;" + beneficio + "</li>";
                            }
                            anexo3 += "</ul>";
                        }
                        else //Imprime beneficios de la version 1
                        {
                            var x = _repMontoPago.ObtenerBeneficiosAnexo03(programa.Id, alumno.IdCodigoPais.Value);
                            if (x.Count > 0)
                            {
                                anexo3 += "<h1>ANEXO 03</h1><br/>";
                                switch (alumno.Paquete)
                                {
                                    case "1":
                                        anexo3 += "<h2>Version Basica</h2><br/><ul>";
                                        break;
                                    case "2":
                                        anexo3 += "<h2>Version Profesional</h2><br/><ul>";
                                        break;
                                    case "3":
                                        anexo3 += "<h2>Version Gerencial</h2><br/><ul>";
                                        break;
                                }
                                foreach (var item in x)
                                {
                                    if (item.Beneficios.Contains("Todos los beneficios"))
                                    {
                                        item.Beneficios = "";
                                    }
                                    if (!string.IsNullOrEmpty(alumno.Paquete) && int.Parse(alumno.Paquete) == 1)
                                    {
                                        if (item.Paquete == 1)
                                        {
                                            anexo3 += "<li>" + item.Beneficios + "</li>";
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(alumno.Paquete) && int.Parse(alumno.Paquete) == 2)
                                    {
                                        if (item.Paquete == 2)
                                        {
                                            anexo3 += "<li>" + item.Beneficios + "</li>";
                                        }
                                    }
                                    if (!string.IsNullOrEmpty(alumno.Paquete) && int.Parse(alumno.Paquete) == 3)
                                    {
                                        if (programa.Id != ValorEstatico.IdProgramaGeneralFFISO27001 && programa.Id != ValorEstatico.IdProgramaGeneralFFISO9001 &&
                                            programa.Id != ValorEstatico.IdProgramaGeneralDSIG && programa.Id != ValorEstatico.IdProgramaGeneralFFISO45001 &&
                                            programa.Id != ValorEstatico.IdProgramaGeneralFFISO37001)
                                            anexo3 += "<li>" + item.Beneficios + "</li>";
                                        else
                                        {
                                            if (item.Paquete == 3)
                                            {
                                                anexo3 += "<li>" + item.Beneficios + "</li>";
                                            }
                                        }
                                    }
                                }
                                anexo3 += "</ul>";
                            }
                        }


                        formatoPagos += "<br>";
                        Contenido = Contenido.Replace("##TIPOPAGO##", formatoPagos);
                        DateTime hoy = DateTime.Now;
                        Contenido = Contenido.Replace("## DATEMONTH ##", hoy.ToString("MMMM", CultureInfo.CreateSpecificCulture("es-ES")));
                        Contenido = Contenido.Replace("##DATEDAYS##", hoy.Day.ToString());
                        Contenido = Contenido.Replace("##DATEYEAR##", hoy.Year.ToString());

                        HTMLWorker htmlparser = new HTMLWorker(_document);
                        htmlparser.SetStyleSheet(GenerateStyleSheet());

                        PdfWriter.GetInstance(_document, ms);
                        _document.Open();

                        htmlparser.Parse(new StringReader(Contenido));

                        //_document.Add(new iTextSharp.text.Paragraph("_______________           ____________                 __________________", firmas));
                        //_document.Add(new iTextSharp.text.Paragraph("   EL ALUMNO                   Huella Digital                    BS GRUPO S.A.C.", firmas));

                        if (codpais == 591)
                        {
                            _document.Add(new iTextSharp.text.Paragraph("_______________          ___________           ________________________", firmas));
                            _document.Add(new iTextSharp.text.Paragraph("   EL ALUMNO                  Huella Digital      BSGINSTITUTE BOLIVIA S.R.L.", firmas));
                        }
                        else if (codpais == 57 )
                        {
                            _document.Add(new iTextSharp.text.Paragraph("_______________          ___________           ________________________", firmas));
                            _document.Add(new iTextSharp.text.Paragraph("   EL ALUMNO                  Huella Digital       BS GRUPO COLOMBIA S.A.S.", firmas));
                        }
                        else if (codpais == 52 )
                        {
                            if (tipoDocumento == "Convenio Prestacion" && codpais == 52)
                            {
                                _document.Add(new iTextSharp.text.Paragraph("_______________          ___________           ________________________", firmas));
                                _document.Add(new iTextSharp.text.Paragraph("   EL ALUMNO                  Huella Digital      BSGINSTITUTE. ", firmas));
                            }
                            else {
                                //No va campos Firma
                            }

                        }
                        else
                        {
                            _document.Add(new iTextSharp.text.Paragraph("_______________           ____________                 __________________", firmas));
                            _document.Add(new iTextSharp.text.Paragraph("   EL ALUMNO                   Huella Digital                    BS GRUPO S.A.C.", firmas));
                        }

                        if(codpais == 52)
                        {
                           if(tipoDocumento == "Contrato" )
                           {
                                //No se adjunto anexo
                           }
                           else if (tipoDocumento ==  "Lineamiento")
                           {
                                if (!string.IsNullOrEmpty(anexo1))
                                {
                                    _document.NewPage();
                                    htmlparser.Parse(new StringReader(anexo1));
                                }
                            }
                           else if (tipoDocumento == "Convenio Prestacion")
                           {
                                if (!string.IsNullOrEmpty(anexo1))
                                {
                                    _document.NewPage();
                                    htmlparser.Parse(new StringReader(anexo1));
                                }
                                if (!string.IsNullOrEmpty(anexo2))
                                {
                                    _document.NewPage();
                                    htmlparser.Parse(new StringReader(anexo2));
                                }
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(anexo1))
                            {
                                _document.NewPage();
                                htmlparser.Parse(new StringReader(anexo1));
                            }
                            if (!string.IsNullOrEmpty(anexo2))
                            {
                                _document.NewPage();
                                htmlparser.Parse(new StringReader(anexo2));
                            }
                            if (!string.IsNullOrEmpty(anexo3))
                            {
                                _document.NewPage();
                                htmlparser.Parse(new StringReader(anexo3));
                            }
                        }

                        
                        //if(programa.Id == ValorEstatico.IdProgramaGeneralDSIG)
                        //{
                        //	switch (alumno.Paquete)
                        //	{
                        //		case "1":
                        //			anexo3CasoExcepcional = "<h1>ANEXO 03</h1><br/><h2>Beneficios</h2><br/><p><strong>Versi&oacute;n B&aacute;sica</strong></p><ul><li>Acceso a las 104 horas cronol&oacute;gicas de clases.</li><li>Certificaci&oacute;n Implementador L&iacute;der en Sistemas Integrados de Gesti&oacute;n ISO 9001, ISO 14001, ISO 45001 emitido por BSG Institute.</li></ul>";
                        //			break;
                        //		case "2":
                        //			anexo3CasoExcepcional = "<h1>ANEXO 03</h1><br/><h2>Beneficios</h2><br/><p><strong>Versi&oacute;n Profesional</strong></p><ul><li>Acceso a las 104 horas cronol&oacute;gicas de clases.</li><li>Certificaci&oacute;n Implementador L&iacute;der en Sistemas Integrados de Gesti&oacute;n ISO 9001, ISO 14001, ISO 45001 emitido por BSG Institute.</li><li>Acceso al Curso Certificado por IRCA: Auditor Interno ISO 9001:2015.</li><li>Registro en IRCA como Auditor Interno ISO 9001:2015. (1)</li></ul>";
                        //			break;
                        //		case "3":
                        //			anexo3CasoExcepcional = "<h1>ANEXO 03</h1><br/><h2>Beneficios</h2><br/><p><strong>Versi&oacute;n Profesional</strong></p><ul><li>Acceso a las 104 horas cronol&oacute;gicas de clases.</li><li>Certificaci&oacute;n Implementador L&iacute;der en Sistemas Integrados de Gesti&oacute;n ISO 9001, ISO 14001, ISO 45001 emitido por BSG Institute.</li><li>Acceso al Curso Certificado por IRCA: Auditor L&iacute;der ISO 9001:2015.</li><li>Registro en IRCA como Auditor L&iacute;der ISO 9001:2015. (1)</li></ul>";
                        //			break;
                        //	}

                        //	if (!string.IsNullOrEmpty(anexo3CasoExcepcional))
                        //	{
                        //		_document.NewPage();
                        //		htmlparser.Parse(new StringReader(anexo3CasoExcepcional));
                        //	}
                        //}

                        _document.Close();

                    }
                    catch (Exception ex)
                    {
                        LoggerMessage.DefineScope(ex.ToString());
                    }

                }

                return ms.ToArray();
            }
        }

        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Genera Version por Id.
        /// </summary> 
        public string GenerarVersion(string Id)
        {
            switch (Id)
            {
                case "1":
                    return " -  VERSION BASICA ";
                case "2":
                    return " -  VERSION PROFESIONAL ";
                case "3":
                    return " -  VERSION GERENCIAL ";
                default:
                    return "";
            }
        }

        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Genera estilos para hoja de calculo.
        /// </summary> 
        private StyleSheet GenerateStyleSheet()
        {
            FontFactory.RegisterDirectories();

            StyleSheet css = new StyleSheet();

            css.LoadTagStyle("h1", "size", "14pt");
            css.LoadTagStyle("h1", "style", "text-align:center;font-weight:bold;");
            css.LoadTagStyle("p", "style", "text-align:justify;");
            css.LoadTagStyle("ul", "style", "display:block;list-style-type:circle;");
            css.LoadTagStyle(HtmlTags.TABLE, HtmlTags.BORDER, "0.1");
            css.LoadTagStyle(HtmlTags.DIV, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.DIV, HtmlTags.FONTSIZE, "14px");
            css.LoadTagStyle(HtmlTags.H1, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H2, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H2, HtmlTags.FONTSIZE, "14px");
            css.LoadTagStyle(HtmlTags.H3, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H3, HtmlTags.FONTSIZE, "14px");
            css.LoadTagStyle(HtmlTags.H4, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H4, HtmlTags.FONTSIZE, "14px");
            css.LoadTagStyle(HtmlTags.P, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.P, HtmlTags.FONTSIZE, "14px");
            return css;
        }
        /// <summary>
        /// Genera estilos Para Documento Pre-requisitos
        /// </summary>
        /// <returns></returns>
        private static StyleSheet GenerateStyleSheet2()
        {
            FontFactory.RegisterDirectories();

            StyleSheet css = new StyleSheet();

            css.LoadTagStyle("h1", "size", "10pt");
            css.LoadTagStyle("h1", "style", "text-align:cen1ter;font-weight:bold;");
            css.LoadTagStyle("p", "style", "text-align:justify;");
            css.LoadTagStyle("div", "size", "10pt");
            css.LoadTagStyle("ul", "style", "display:block;list-style-type:circle;");
            css.LoadTagStyle(HtmlTags.TABLE, HtmlTags.BORDER, "0.1");
            css.LoadTagStyle(HtmlTags.DIV, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H1, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H2, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H2, HtmlTags.FONTSIZE, "10px");
            css.LoadTagStyle(HtmlTags.H3, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H3, HtmlTags.FONTSIZE, "10px");
            css.LoadTagStyle(HtmlTags.H4, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H4, HtmlTags.FONTSIZE, "10px");
            css.LoadTagStyle(HtmlTags.P, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.P, HtmlTags.FONTSIZE, "10px");
            return css;
        }
        /// <summary>
        /// Obtiene Las Cuotas de un programa
        /// </summary>
        /// <param name="IdOportunidad"></param>
        /// <returns></returns>
        private CuotasProgramaDTO ObtenerCuotasProgramaDTO(int IdOportunidad)
        {
            CodigoMatriculaDocumentoDTO IdMatricula = _repMatriculaCabecera.CodigoMatriculaPorIdOportunidad(IdOportunidad);

            if (IdMatricula != null)
            {
                return ObtenerCronograma(IdMatricula.Id);
            }
            else
            {
                MatriculaTemporalDTO IdMatricula2 = _repMatriculaCabecera.ObtenerMatriculaPorOportunidad(IdOportunidad);
                if (IdMatricula2 == null)
                    return null;
                return ObtenerCronograma(IdMatricula2.IdMatricula);
            }
        }
        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Obtiene cronograma.
        /// </summary> 
        public CuotasProgramaDTO ObtenerCronograma(int id)
        {
            CuotasProgramaDTO ListaCuotasProgramaDTO = _repPgeneral.ObtenerProgramaParaCuotas(id);

            

            if (ListaCuotasProgramaDTO != null)
            {
                ListaCuotasProgramaDTO.ListaCuotas = new List<ProgramaListaCuotaDTO>();
                List<ProgramaListaCuotaDTO> CronogramaPagoDetalleMod = _repCronogramaPagoDetalleFinal.ObtenerListaCuotaPrograma(ListaCuotasProgramaDTO.IdMatricula);

                if (CronogramaPagoDetalleMod != null)
                {
                    foreach (var _CronogramaPagoDetalleMod in CronogramaPagoDetalleMod)
                    {
                        ListaCuotasProgramaDTO.ListaCuotas.Add(_CronogramaPagoDetalleMod);
                    }
                }
                decimal Porcentaje = 0.005M;

                foreach (var cuotaT in ListaCuotasProgramaDTO.ListaCuotas)
                {
                    int NroDias = Convert.ToInt32((DateTime.Now.Date - cuotaT.FechaVencimiento.Value).TotalDays);
                    if (NroDias > 0 && cuotaT.Cancelado.Value == false)
                    {
                        cuotaT.Mora = cuotaT.Mora + decimal.Round(((cuotaT.Cuota.Value + cuotaT.Mora.Value) * Porcentaje) * NroDias, 2, MidpointRounding.AwayFromZero);
                    }
                }

                if (ListaCuotasProgramaDTO.WebMoneda == "2")
                {
                    foreach (var cuotaT in ListaCuotasProgramaDTO.ListaCuotas)
                    {
                        cuotaT.Cuota = cuotaT.MontoCuotaDescuento;
                        cuotaT.Mora = Math.Round(cuotaT.Mora.Value * ListaCuotasProgramaDTO.WebTipoCambio.Value, 2, MidpointRounding.AwayFromZero);
                        cuotaT.Moneda = "Pesos Colombianos";
                    }
                }

                return ListaCuotasProgramaDTO;
            }
            else
            {
                return ListaCuotasProgramaDTO;
            }
        }

        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Obtiene una lista de todos los documentos
        /// </summary> 
        public void CargarListaDocumentos()
        {
            ListaDocumentos = new List<ProgramaDocumentosDTO>();
            ListadoAlertas = new List<AlertDTO>();

            ProgramaDocumentosDTO BrochureGeneral = new ProgramaDocumentosDTO();
            BrochureGeneral.Id = 0;
            BrochureGeneral.NombreDocumento = "Brochure BSG Institute";
            BrochureGeneral.Habilitado = true;
            ListaDocumentos.Add(BrochureGeneral);

            ProgramaDocumentosDTO Brochure = new ProgramaDocumentosDTO();
            Brochure.Id = 1;
            Brochure.NombreDocumento = "Brochure";
            Brochure.Habilitado = true;
            ListaDocumentos.Add(Brochure);

            ProgramaDocumentosDTO CronogramaAlumnos = new ProgramaDocumentosDTO();
            CronogramaAlumnos.Id = 2;
            CronogramaAlumnos.NombreDocumento = "Cronograma de Alumnos";
            CronogramaAlumnos.Habilitado = true;
            ListaDocumentos.Add(CronogramaAlumnos);

            ProgramaDocumentosDTO Requisitosprogramasonline = new ProgramaDocumentosDTO();
            Requisitosprogramasonline.Id = 3;
            Requisitosprogramasonline.NombreDocumento = "Requisitos programas online";
            Requisitosprogramasonline.Habilitado = true;
            ListaDocumentos.Add(Requisitosprogramasonline);

            ProgramaDocumentosDTO PagareWord = new ProgramaDocumentosDTO();
            PagareWord.Id = 4;
            PagareWord.NombreDocumento = "Pagare 1 (Word)";
            PagareWord.Habilitado = true;
            ListaDocumentos.Add(PagareWord);

            ProgramaDocumentosDTO PagareExcel = new ProgramaDocumentosDTO();
            PagareExcel.Id = 5;
            PagareExcel.NombreDocumento = "Pagare 2 (Excel)";
            PagareExcel.Habilitado = true;
            ListaDocumentos.Add(PagareExcel);

            ProgramaDocumentosDTO Conveniocapacitacionformacion = new ProgramaDocumentosDTO();
            Conveniocapacitacionformacion.Id = 6;
            Conveniocapacitacionformacion.NombreDocumento = "Convenio de capacitación y/o formación (Firma o grabacion contrato de voz)";
            Conveniocapacitacionformacion.Habilitado = true;
            ListaDocumentos.Add(Conveniocapacitacionformacion);

            ProgramaDocumentosDTO Condicionesaracterísticasserviciocapacitación = new ProgramaDocumentosDTO();
            Condicionesaracterísticasserviciocapacitación.Id = 7;
            Condicionesaracterísticasserviciocapacitación.NombreDocumento = "Condiciones y Características del servicio de capacitación (grabación contrato de voz)";
            Condicionesaracterísticasserviciocapacitación.Habilitado = true;
            ListaDocumentos.Add(Condicionesaracterísticasserviciocapacitación);

            ProgramaDocumentosDTO SilaboPrograma = new ProgramaDocumentosDTO();
            SilaboPrograma.Id = 8;
            SilaboPrograma.NombreDocumento = "Sílabos del Programa";
            SilaboPrograma.Habilitado = false;
            ListaDocumentos.Add(SilaboPrograma);

            ProgramaDocumentosDTO ContratoUsoDatos = new ProgramaDocumentosDTO();
            ContratoUsoDatos.Id = 9;
            ContratoUsoDatos.NombreDocumento = "Contrato Uso de Datos";
            ContratoUsoDatos.Habilitado = true;
            ListaDocumentos.Add(ContratoUsoDatos);

            ProgramaDocumentosDTO LineamientosCondiciones = new ProgramaDocumentosDTO();
            LineamientosCondiciones.Id = 10;
            LineamientosCondiciones.NombreDocumento = "Lineamientos de Condiciones";
            LineamientosCondiciones.Habilitado = true;
            ListaDocumentos.Add(LineamientosCondiciones);

            ProgramaDocumentosDTO ConvenioPrestacion = new ProgramaDocumentosDTO();
            ConvenioPrestacion.Id = 11;
            ConvenioPrestacion.NombreDocumento = "Convenio de Prestacion de Servicios";
            ConvenioPrestacion.Habilitado = true;
            ListaDocumentos.Add(ConvenioPrestacion);

            ProgramaDocumentosDTO CronogramaAlumnosGrupal = new ProgramaDocumentosDTO();
            CronogramaAlumnosGrupal.Id = 12;
            CronogramaAlumnosGrupal.NombreDocumento = "Cronograma de Alumnos(Todos los Grupos)";
            CronogramaAlumnosGrupal.Habilitado = true;
            ListaDocumentos.Add(CronogramaAlumnosGrupal);


        }
        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Genera vista previa del certificado.
        /// </summary> 
        public byte[] GenerarVistaPreviaCertificado(int IdPlantillaF, int IdPlantillaP, int IdOportunidad, ref int Idplantillabase, ref string codigoCertificado)
        {


            ContenidoCertificado = new CertificadoGeneradoAutomaticoContenidoBO();
            int IdPlantillaBase = 0;
            var oportunidadClasificacionOperaciones = _repOportunidadClasificacionOperaciones.FirstBy(x => x.IdOportunidad == IdOportunidad);

            if (!_repMatriculaCabecera.Exist(x => x.Id == oportunidadClasificacionOperaciones.IdMatriculaCabecera))
            {
                throw new Exception("Matricula cabecera no valida!");
            }

            MatriculaCabeceraDatosCertificadoRepositorio repMatriculaCertificado = new MatriculaCabeceraDatosCertificadoRepositorio();
            MatriculaCabeceraDatosCertificadoBO certificados = repMatriculaCertificado.FirstBy(w =>
            w.Estado == true &&
            w.EstadoCambioDatos == false && 
            w.IdMatriculaCabecera == oportunidadClasificacionOperaciones.IdMatriculaCabecera);
            MatriculaCabeceraDatosCertificadoBO NuevoCertificado = new MatriculaCabeceraDatosCertificadoBO();

            var listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();

            var matriculaCabecera = _repMatriculaCabecera.FirstBy(x => x.Id == oportunidadClasificacionOperaciones.IdMatriculaCabecera);
            var detalleMatriculaCabecera = matriculaCabecera.ObtenerDetalleMatricula();
            var plantilla = _repPlantilla.FirstById(IdPlantillaF);


            IdPlantillaBase = plantilla.IdPlantillaBase;
            Idplantillabase = IdPlantillaBase;

            var plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantillaF);
            var alumno = _repAlumno.FirstById(matriculaCabecera.IdAlumno);

            var oportunidad = _repOportunidad.FirstById(detalleMatriculaCabecera.IdOportunidad);
            var DatosCompuestosOportunidad = _repOportunidad.ObtenerDatosCompuestosPorIdOportunidad(oportunidad.Id);
            var personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);

            var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

            foreach (var etiqueta in listaEtiqueta)
            {
                listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = string.Concat("{", etiqueta, "}"), texto = "" });
            }
            List<string> sepacion = new List<string>();
            string FondoFrontalCertificado = "";
            string FondoReversoCertificado = "";
            string FondoFrontalConstancia = "";
            string FondoReversoConstancia = "";
            sepacion = plantillaBase.Cuerpo.Split("###").ToList();
            Dictionary<string, object> map = new Dictionary<string, object>();
            map["font_factory"] = new MyFontFactory();
            //map.put("font_factory", new MyFontFactory());

            using (MemoryStream ms = new MemoryStream())
            {
                PdfSharp.Pdf.PdfDocument pdf = new PdfSharp.Pdf.PdfDocument();
                var config = new PdfGenerateConfig();
                if (IdPlantillaBase == 12)/*Certificado*/
                {
                    config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                    config.PageSize = PdfSharp.PageSize.A4;
                    config.MarginTop = 144;
                    config.MarginBottom = 30;
                    config.MarginLeft = 111;
                    config.MarginRight = 83;

                }
                else
                {
                    config.PageSize = PdfSharp.PageSize.A4;
                    config.MarginLeft = 60;
                    config.MarginRight = 60;
                    config.MarginTop = 60;
                    config.MarginBottom = 25;

                }
                int Conteo = 0;
                List<string> estructura = new List<string>();



                string remplazo = "";
                foreach (var item in sepacion)
                {
                    string direccionCodigoQR = "";
                    string tipoletra = "Times New Roman";
                    remplazo = item;
                    string searchString = "/*";
                    int startIndex = remplazo.IndexOf(searchString);
                    searchString = "*/";
                    int endIndex = 0;
                    string substring = "";

                    if (startIndex != -1)
                    {
                        endIndex = remplazo.IndexOf(searchString, startIndex);
                        substring = remplazo.Substring(startIndex + searchString.Length, endIndex - searchString.Length);
                    }
                    if (substring != null && substring != "")
                    {
                        tipoletra = substring;
                    }
                    else
                    {
                        searchString = "text-align:";
                        startIndex = remplazo.IndexOf(searchString);
                        searchString = ";";

                        if (startIndex != -1)
                        {
                            endIndex = remplazo.IndexOf(searchString, startIndex);
                            substring = remplazo.Substring(startIndex + searchString.Length, endIndex + searchString.Length - startIndex);
                        }
                        if (substring != null && substring != "")
                        {
                            tipoletra = substring;
                        }
                    }
                    if (item.Contains("repositorioweb"))
                    {
                        if (IdPlantillaBase == 12)
                        {
                            FondoFrontalCertificado = item;
                        }
                        else
                        {
                            FondoFrontalConstancia = item;
                        }

                    }
                    if (item.Contains("acute"))
                    {
                        remplazo = remplazo.Replace("&Eacute;", "É").Replace("&Aacute;", "Á").Replace("&Iacute;", "Í").Replace("&Oacute;", "Ó").Replace("&Uacute;", "Ú");
                    }
                    if (item.Contains("CODIGOQR"))
                    {
                        string url = "";
                        if (IdPlantillaBase == 12)/*Certificados*/
                        {
                            url = _repAlumno.ObtenerCodigoCertificado(matriculaCabecera.Id);
                        }
                        else /*13:Constancia*/
                        {
                            url = _repCertificadoGeneradoAutomatico.ObtenerCorrelativoCertificado().ToString();
                        }

                        var urlCodigo = "https://bsginstitute.com/informacionCertificado?cod=" + matriculaCabecera.Id + "." + url;
                        QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                        QRCodeData qrDatos = qRCodeGenerator.CreateQrCode(urlCodigo, QRCodeGenerator.ECCLevel.Q);
                        QRCode qRCodigo = new QRCode(qrDatos);

                        System.Drawing.Bitmap qrImagen = qRCodigo.GetGraphic(7, System.Drawing.Color.Black, System.Drawing.Color.White, false);
                        System.Drawing.Image objImage = (System.Drawing.Image)qrImagen;

                        //var a = System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory);
                        //qrImagen.Save(System.AppDomain.CurrentDomain.BaseDirectory + "codigo.png", System.Drawing.Imaging.ImageFormat.Png);

                        direccionCodigoQR = _repAlumno.guardarArchivosQR(ImageToByte2(objImage), "image/jpeg", url);
                        this.ContenidoCertificado.CodigoQr = direccionCodigoQR;
                        remplazo = remplazo.Replace("CODIGOQR", $@"<img style='vertical-align:baseline' src='{direccionCodigoQR}' width=55 height=55  />");

                    }
                    if (item.Contains("{tPLA_PGeneral.Nombre}"))
                    {
                        var nombrePrograma = "";
                        if (certificados == null)
                        {
                            nombrePrograma = detalleMatriculaCabecera.NombreProgramaGeneral.Replace(" - COLOMBIA", "").ToUpper();
                            NuevoCertificado.NombreCurso = nombrePrograma;
                        }
                        else {
                            nombrePrograma = certificados.NombreCurso;
                        }
                         
                        this.ContenidoCertificado.NombrePrograma = nombrePrograma;
                        if (nombrePrograma.ToUpper().StartsWith("CURSO") || nombrePrograma.ToUpper().StartsWith("PROGRAMA"))
                        {
                            remplazo = remplazo.Replace("{T_Pgeneral.TipoCapacitacion}", "");
                        }
                        else
                        {
                            remplazo = remplazo.Replace("{T_Pgeneral.TipoCapacitacion}", " programa");
                        }
                        remplazo = remplazo.Replace("{tPLA_PGeneral.Nombre}", nombrePrograma);
                    }
                    if (item.Contains("{T_Pgeneral.CodigoPartner}"))
                    {
                        var codigoPartner = _repPgeneral.ObtenerCodigoPartner(matriculaCabecera.Id);
                        if (codigoPartner != null)
                        {
                            this.ContenidoCertificado.CodigoPartner = codigoPartner;
                            remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", codigoPartner);
                        }
                        else
                        {
                            throw new Exception("No se puede Calcular CodigoPartner");
                        }
                    }
                    //reemplazar nombre 1 alumno
                    if (item.Contains("{T_Alumno.NombreCompleto}"))
                    {
                    }
                    if (item.Contains("{tAlumnos.nombre1}"))
                    {
                        string nombreAlumno = "";
                        nombreAlumno = alumno.Nombre1.ToUpper();
                        //var parrafo = new iTextSharp.text.Phrase(alumno.NombreCompleto, fuente);
                        remplazo = remplazo.Replace("{tAlumnos.nombre1}", alumno.Nombre1.ToUpper());

                        if (item.Contains("{tAlumnos.nombre2}"))
                        {
                            nombreAlumno = nombreAlumno + " " + alumno.Nombre2.ToUpper();
                            remplazo = remplazo.Replace("{tAlumnos.nombre2}", alumno.Nombre2.ToUpper());
                            //_document.Add(new iTextSharp.text.Phrase(" " + alumno.Nombre2.ToUpper(), fuente));
                        }
                        if (item.Contains("{tAlumnos.apepaterno}"))
                        {
                            nombreAlumno = nombreAlumno + " " + alumno.ApellidoPaterno.ToUpper();
                            remplazo = remplazo.Replace("{tAlumnos.apepaterno}", alumno.ApellidoPaterno.ToUpper());
                            //_document.Add(new iTextSharp.text.Phrase(" " + alumno.ApellidoPaterno.ToUpper(), fuente));
                        }
                        if (item.Contains("{tAlumnos.apematerno}"))
                        {
                            nombreAlumno = nombreAlumno + " " + alumno.ApellidoMaterno.ToUpper();
                            remplazo = remplazo.Replace("{tAlumnos.apematerno}", alumno.ApellidoMaterno.ToUpper());
                            //_document.Add(new iTextSharp.text.Phrase(" " + alumno.ApellidoMaterno.ToUpper(), fuente));
                        }

                        this.ContenidoCertificado.NombreAlumno = nombreAlumno;
                        //if (IdPlantillaBase == 12)
                        //{
                        //    List<IElement> objects = HTMLWorker.ParseToList(new StringReader(remplazo), GenerateStyleCertificadoAutomatico(tipoletra), map);
                        //    foreach (IElement element in objects)
                        //    {
                        //        _document.Add(element);
                        //    }
                        //}
                        //else
                        //{
                        //    htmlparser.SetStyleSheet(GenerateStyleCertificadoAutomatico(tipoletra));
                        //    htmlparser.Parse(new StringReader(remplazo));
                        //}                                             
                    }
                    if (item.Contains("{tpla_pgeneral.pw_duracion}"))
                    {

                        string Duracion = DatosCompuestosOportunidad.pw_duracion;
                        remplazo = remplazo.Replace("{tpla_pgeneral.pw_duracion}", Duracion);
                    }
                    if (item.Contains("{tCentroCosto.centrocosto}"))
                    {
                        this.ContenidoCertificado.NombreCentroCosto = detalleMatriculaCabecera.NombreCentroCosto.ToUpper();
                        remplazo = remplazo.Replace("{tCentroCosto.centrocosto}", detalleMatriculaCabecera.NombreCentroCosto.ToUpper());
                    }
                    if (item.Contains("{tPEspecifico.ciudad}"))
                    {
                        var PrimeraLetra = detalleMatriculaCabecera.NombreCiudad.Substring(0, 1).ToUpper();
                        var resto = detalleMatriculaCabecera.NombreCiudad.Substring(1).ToLower();
                        this.ContenidoCertificado.Ciudad = PrimeraLetra + resto;
                        if (ContenidoCertificado.Ciudad == null || ContenidoCertificado.Ciudad == "")
                        {
                            throw new Exception("No se Puede Calcular Ciudad");
                        }
                        remplazo = remplazo.Replace("{tPEspecifico.ciudad}", PrimeraLetra + resto);
                    }
                    if (item.Contains("{tPEspecifico.EscalaCalificacion}"))
                    {
                        var escalaCalificacion = detalleMatriculaCabecera.EscalaCalificacion;
                        this.ContenidoCertificado.EscalaCalificacion = escalaCalificacion;
                        if (escalaCalificacion == 0 || escalaCalificacion == null)
                        {
                            throw new Exception("No se Puede Calcular Escala Calificacion");
                        }
                        remplazo = remplazo.Replace("{tPEspecifico.EscalaCalificacion}", escalaCalificacion.ToString());
                    }
                    //reemplazar Fecha Inicio Capacitacion
                    if (item.Contains("{T_Alumno.FechaInicioCapacitacion}"))
                    {
                        var fechaInicioCapacitacion = "";
                        if (certificados == null)
                        {
                            fechaInicioCapacitacion = _repAlumno.ObtenerFechaInicioCapacitacionPortalWeb(matriculaCabecera.Id);
                            try{
                                NuevoCertificado.FechaInicio = repMatriculaCertificado.TranformarCadenaEnFecha(fechaInicioCapacitacion);
                            }
                            catch (Exception e)
                            {
                                NuevoCertificado.FechaInicio = DateTime.Now;
                            }
                            
                        }
                        else
                        {
                            fechaInicioCapacitacion = repMatriculaCertificado.TranformarFechaEnCadena(certificados.FechaInicio);
                        }
                        if (fechaInicioCapacitacion == null || fechaInicioCapacitacion == "")
                        {
                            throw new Exception("No se Puede Calcular FechaInicioCapacitacion");
                        }
                        this.ContenidoCertificado.FechaInicioCapacitacion = fechaInicioCapacitacion;
                        remplazo = remplazo.Replace("{T_Alumno.FechaInicioCapacitacion}", fechaInicioCapacitacion);
                    }

                    //reemplazar Fecha Fin Capacitacion
                    if (item.Contains("{T_Alumno.FechaFinCapacitacion}"))
                    {
                        var fechaFinCapacitacion = "";
                        if (certificados == null)
                        {
                            fechaFinCapacitacion = _repAlumno.ObtenerFechaFinCapacitacionPortalWeb(matriculaCabecera.Id);
                            NuevoCertificado.FechaFinal = repMatriculaCertificado.TranformarCadenaEnFecha(fechaFinCapacitacion);
                        }
                        else
                        {
                            fechaFinCapacitacion = repMatriculaCertificado.TranformarFechaEnCadena(certificados.FechaFinal);
                        }
                        if (fechaFinCapacitacion == null || fechaFinCapacitacion == "")
                        {
                            throw new Exception("No se Puede Calcular FehaFinCapacitacion");
                        }
                        this.ContenidoCertificado.FechaFinCapacitacion = fechaFinCapacitacion;
                        remplazo = remplazo.Replace("{T_Alumno.FechaFinCapacitacion}", fechaFinCapacitacion);
                    }
                    //reemplazar Calificacion Promedio
                    if (item.Contains("{T_Alumno.CalificacionPromedio}"))
                    {
                        var calificacionPromedio = _repAlumno.ObtenerNotaPromedio(matriculaCabecera.Id);
                        if (calificacionPromedio == null || calificacionPromedio == "")
                        {
                            throw new Exception("No se Puede Calcular Calificacion Promedio");
                        }
                        this.ContenidoCertificado.CalificacionPromedio = Convert.ToInt32(calificacionPromedio);
                        remplazo = remplazo.Replace("{T_Alumno.CalificacionPromedio}", (Convert.ToInt32(calificacionPromedio)).ToString());
                    }
                    //reemplazar Fecha Emision Certificado
                    if (item.Contains("{T_Alumno.FechaEmisionCertificado}"))
                    {
                        var fechaEmision = _repAlumno.ObtenerFechaEmision();
                        this.ContenidoCertificado.FechaEmisionCertificado = fechaEmision;
                        remplazo = remplazo.Replace("{T_Alumno.FechaEmisionCertificado}", fechaEmision);
                    }

                    //reemplazar Fecha Codigo Certificado
                    if (item.Contains("{T_Alumno.CodigoCertificado}"))
                    {
                        var CodigoCertificado = _repAlumno.ObtenerCodigoCertificado(matriculaCabecera.Id);
                        if (CodigoCertificado == null || CodigoCertificado == "")
                        {
                            throw new Exception("No se Puede Calcular Codigo Certificado");
                        }
                        codigoCertificado = CodigoCertificado;
                        remplazo = remplazo.Replace("{T_Alumno.CodigoCertificado}", CodigoCertificado);
                    }

                    //reemplazar duracion en horas de Programa Especifico
                    if (item.Contains("{tPEspecifico.duracion}"))
                    {
                        var duracionPespecifico = "";
                        if (certificados == null)
                        {
                            duracionPespecifico = _repPespecifico.ObtenerDuracionProgramaEspecifico(matriculaCabecera.IdPespecifico, matriculaCabecera.Id);
                            NuevoCertificado.Duracion = duracionPespecifico;
                        }
                        else
                        {
                            duracionPespecifico = certificados.Duracion.ToString();
                        }
                        if (duracionPespecifico == null || duracionPespecifico == "")
                        {
                            throw new Exception("No se Puede Calcular Duracion Pespecifico");
                        }
                        this.ContenidoCertificado.DuracionPespecifico = Int32.Parse(duracionPespecifico);
                        remplazo = remplazo.Replace("{tPEspecifico.duracion}", duracionPespecifico);
                    }
                    /*reemplazar Codigo de matricula del alumno */
                    if (item.Contains("{T_MatriculaCabecera.CodigoMatricula}"))
                    {
                        remplazo = remplazo.Replace("{T_MatriculaCabecera.CodigoMatricula}", matriculaCabecera.CodigoMatricula);
                    }
                    /*reemplazar Codigo de matricula del alumno */
                    if (item.Contains("{T_Alumno.CorrelativoConstancia}"))
                    {
                        var correlativoConstancia = _repCertificadoGeneradoAutomatico.ObtenerCorrelativoCertificado();
                        this.ContenidoCertificado.CorrelativoConstancia = correlativoConstancia;
                        remplazo = remplazo.Replace("{T_Alumno.CorrelativoConstancia}", correlativoConstancia.ToString());
                    }
                    /*reemplazar Cronograma de notas del alumno por matricula*/
                    if (item.Contains("{T_Alumno.CronogramaNotas}"))
                    {
                        string tablaNota = "";
                        var cronogramaNota = _repAlumno.ObtenerCronogramaNota(matriculaCabecera.Id);
                        if (cronogramaNota == null || cronogramaNota.Count == 0)
                        {
                            throw new Exception("No se Puede Calcular cronogramaNota");
                        }
                        tablaNota += "<table  style='font-size:11px; border:1px solid #575656;font-family:Arial;color:#575656;border-collapse: collapse;width:100%;' cellspacing='3' cellpadding='3'>";
                        tablaNota += $@"<tr style='border:1px solid #575656;border-collapse: collapse;'>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> CURSO </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> NOTA </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> ESTADO </td>
                                            </tr>";
                        int contador = 0;
                        for (int rows = 0; rows < cronogramaNota.Count; rows++)
                        {
                            tablaNota += "<tr style='border:1px solid #575656;border-collapse: collapse;'>";
                            tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;;text-align:left'> {cronogramaNota[rows].Curso}</td>";
                            tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:center'> {cronogramaNota[rows].Nota}</td>";
                            tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:center'> {cronogramaNota[rows].Estado}</td>";

                            contador = contador + 1;
                            tablaNota += "</tr>";
                        }
                        tablaNota += "</table>";
                        this.ContenidoCertificado.CronogramaNota = tablaNota;
                        remplazo = remplazo.Replace("{T_Alumno.CronogramaNotas}", tablaNota);
                    }
                    /*reemplazar Cronograma de Asistencia del alumno por matricula*/
                    if (item.Contains("{T_Alumno.CronogramaAsistencia}"))
                    {
                        string tablaAsistencia = "";
                        var cronogramaAsistencia = _repAlumno.ObtenerCronogramaAsistencia(matriculaCabecera.Id);
                        if (cronogramaAsistencia == null || cronogramaAsistencia.Count == 0)
                        {
                            throw new Exception("No se Puede Calcular CronogramaAsistencia");
                        }
                        tablaAsistencia += "<table  style='font-size:11px; border:1px solid #575656;font-family:Arial;color:#575656;border-collapse: collapse;width:100%;' cellspacing='3' cellpadding='3'>";
                        tablaAsistencia += $@"<tr style='border:1px solid #575656;border-collapse: collapse;'>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> CURSO </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> ASISTENCIA </td>
                                            </tr>";
                        int contador = 0;
                        for (int rows = 0; rows < cronogramaAsistencia.Count; rows++)
                        {
                            tablaAsistencia += "<tr style='border:1px solid #575656;border-collapse: collapse;'>";
                            tablaAsistencia += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:left;'> {cronogramaAsistencia[rows].Curso}</td>";
                            tablaAsistencia += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:Center;'> {cronogramaAsistencia[rows].PorcentajeAsistencia}</td>";

                            contador = contador + 1;
                            tablaAsistencia += "</tr>";
                        }
                        tablaAsistencia += "</table>";
                        this.ContenidoCertificado.CronogramaAsistencia = tablaAsistencia;
                        remplazo = remplazo.Replace("{T_Alumno.CronogramaAsistencia}", tablaAsistencia);
                    }
                }
                var pdfFrontal = PdfGenerator.GeneratePdf(remplazo, config);
                var temmporal = ImportarPdfDocument(pdfFrontal);
                PdfSharp.Pdf.PdfPage page = temmporal.Pages[0];
                pdf.AddPage(page);

                PdfSharp.Drawing.XGraphics gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(pdf.Pages[0], PdfSharp.Drawing.XGraphicsPdfPageOptions.Prepend);

                if (IdPlantillaBase == 12)
                {
                    System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(FondoFrontalCertificado);
                    webRequest.AllowWriteStreamBuffering = true;
                    System.Net.WebResponse webResponse = webRequest.GetResponse();
                    PdfSharp.Drawing.XImage xImage = PdfSharp.Drawing.XImage.FromStream(webResponse.GetResponseStream());
                    gfx.DrawImage(xImage, 0, 0, 843, 595);
                }
                else
                {
                    System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(FondoFrontalConstancia);
                    webRequest.AllowWriteStreamBuffering = true;
                    System.Net.WebResponse webResponse = webRequest.GetResponse();
                    PdfSharp.Drawing.XImage xImage = PdfSharp.Drawing.XImage.FromStream(webResponse.GetResponseStream());
                    gfx.DrawImage(xImage, 0, 0, 595, 843);
                }

                if (certificados == null 
                    && NuevoCertificado.NombreCurso!=null 
                    && NuevoCertificado.FechaInicio.Year > 1900
                    && NuevoCertificado.FechaFinal.Year > 1900
                    && NuevoCertificado.Duracion != null)
                {
                    NuevoCertificado.IdMatriculaCabecera = oportunidadClasificacionOperaciones.IdMatriculaCabecera;
                    NuevoCertificado.EstadoCambioDatos = false;
                    NuevoCertificado.Estado = true;
                    NuevoCertificado.UsuarioCreacion = "SYSTEM";
                    NuevoCertificado.UsuarioModificacion = "SYSTEM";
                    NuevoCertificado.FechaCreacion = DateTime.Now;
                    NuevoCertificado.FechaModificacion = DateTime.Now;
                    repMatriculaCertificado.Insert(NuevoCertificado);
                    certificados = repMatriculaCertificado.FirstBy(w =>
                     w.Estado == true &&
                     w.EstadoCambioDatos == false &&
                     w.IdMatriculaCabecera == oportunidadClasificacionOperaciones.IdMatriculaCabecera);
                };

                if (IdPlantillaP != 0 && IdPlantillaP != null)
                {
                    config = new PdfGenerateConfig();
                    if (IdPlantillaBase == 12)/*Certificado*/
                    {
                        config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                        config.PageSize = PdfSharp.PageSize.A4;
                        config.MarginLeft = 257;
                        config.MarginRight = 83;
                        config.MarginTop = 49;
                        config.MarginBottom = 0;

                    }
                    string _estructuraCurricular = "";
                    listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();
                    var plantillaP = _repPlantilla.FirstById(IdPlantillaP);
                    var plantillaBaseP = _repPlantilla.ObtenerPlantillaCorreo(IdPlantillaP);

                    var listaEtiquetaP = plantillaBaseP.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

                    foreach (var etiqueta in listaEtiquetaP)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = string.Concat("{", etiqueta, "}"), texto = "" });
                    }
                    sepacion = new List<string>();

                    sepacion = plantillaBaseP.Cuerpo.Split("###").ToList();

                    foreach (var item in sepacion)
                    {
                        remplazo = item;

                        string tipoletra = "Times New Roman";

                        string searchString = "/*";
                        int startIndex = remplazo.IndexOf(searchString);
                        searchString = "*/";
                        int endIndex = 0;
                        string substring = "";
                        if (startIndex != -1)
                        {
                            endIndex = remplazo.IndexOf(searchString, startIndex);
                            substring = remplazo.Substring(startIndex + searchString.Length, endIndex + searchString.Length - startIndex);
                        }
                        if (substring != null && substring != "")
                        {
                            tipoletra = substring;
                        }
                        else
                        {
                            searchString = "text-align:";
                            startIndex = remplazo.IndexOf(searchString);
                            searchString = ";";

                            if (startIndex != -1)
                            {
                                endIndex = remplazo.IndexOf(searchString, startIndex);
                                substring = remplazo.Substring(startIndex + searchString.Length, endIndex + searchString.Length - startIndex);
                            }
                            if (substring != null && substring != "")
                            {
                                tipoletra = substring;
                            }
                        }
                        if (item.Contains("acute"))
                        {
                            remplazo = remplazo.Replace("&Eacute;", "É").Replace("&Aacute;", "Á").Replace("&Iacute;", "Í").Replace("&Oacute;", "Ó").Replace("&Uacute;", "Ú");
                        }
                        if (item.Contains("repositorioweb"))
                        {
                            if (IdPlantillaBase == 12)
                            {
                                FondoReversoCertificado = item;
                            }
                            else
                            {
                                FondoReversoConstancia = item;
                            }

                        }

                        if (item.Contains("{tPLA_PGeneral.Nombre}"))
                        {
                            remplazo = remplazo.Replace("{tPLA_PGeneral.Nombre}", (( certificados!=null)?certificados.NombreCurso: detalleMatriculaCabecera.NombreProgramaGeneral.Replace(" - COLOMBIA", "").ToUpper()));
                        }
                        if (item.Contains("{T_Pgeneral.CodigoPartner}"))
                        {
                            var codigoPartner = _repPgeneral.ObtenerCodigoPartner(detalleMatriculaCabecera.IdProgramaGeneral);
                            if (codigoPartner != null)
                            {
                                remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", codigoPartner);
                                this.ContenidoCertificado.CodigoPartner = codigoPartner;
                            }
                            else
                            {
                                remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", "");
                            }
                        }
                        if (item.Contains("{tCentroCosto.centrocosto}"))
                        {
                            remplazo = remplazo.Replace("{tCentroCosto.centrocosto}", detalleMatriculaCabecera.NombreCentroCosto);
                        }
                        if (item.Contains("{tPEspecifico.ciudad}"))
                        {
                            remplazo = remplazo.Replace("{tPEspecifico.ciudad}", detalleMatriculaCabecera.NombreCiudad.Substring(0, 1).ToUpper() + detalleMatriculaCabecera.NombreCiudad.Substring(1).ToLower()); ;
                        }
                        //reemplazar nombre 1 alumno
                        if (item.Contains("{T_Alumno.NombreCompleto}"))
                        {

                        }
                        if (item.Contains("{tAlumnos.nombre1}"))
                        {
                            remplazo = remplazo.Replace("{tAlumnos.nombre1}", alumno.Nombre1);

                            if (item.Contains("{tAlumnos.nombre2}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.nombre2}", alumno.Nombre2);
                            }
                            if (item.Contains("{tAlumnos.apematerno}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.apematerno}", alumno.ApellidoMaterno);
                            }
                            if (item.Contains("{tAlumnos.apepaterno}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.apepaterno}", alumno.ApellidoPaterno);
                            }

                        }

                        if (item.Contains("{tpla_pgeneral.pw_duracion}"))
                        {
                            remplazo = remplazo.Replace("{tpla_pgeneral.pw_duracion}", ( certificados != null) ? certificados.Duracion.ToString(): DatosCompuestosOportunidad.pw_duracion); 
                        }
                        //reemplazar Fecha Inicio Capacitacion
                        if (item.Contains("{T_Alumno.FechaInicioCapacitacion}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaInicioCapacitacion}", (certificados != null) ? repMatriculaCertificado.TranformarFechaEnCadena(certificados.FechaInicio): _repAlumno.ObtenerFechaInicioCapacitacion(matriculaCabecera.Id));
                        }

                        //reemplazar Fecha Fin Capacitacion
                        if (item.Contains("{T_Alumno.FechaFinCapacitacion}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaFinCapacitacion}", (certificados != null) ? repMatriculaCertificado.TranformarFechaEnCadena(certificados.FechaFinal): _repAlumno.ObtenerFechaFinCapacitacion(matriculaCabecera.Id));
                        }
                        //reemplazar Calificacion Promedio
                        if (item.Contains("{T_Alumno.CalificacionPromedio}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.CalificacionPromedio}", _repAlumno.ObtenerNotaPromedio(matriculaCabecera.Id));
                        }
                        //reemplazar Fecha Emision Certificado
                        if (item.Contains("{T_Alumno.FechaEmisionCertificado}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaEmisionCertificado}", _repAlumno.ObtenerFechaEmision());
                        }

                        //reemplazar Fecha Codigo Certificado
                        if (item.Contains("{T_Alumno.CodigoCertificado}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.CodigoCertificado}", _repAlumno.ObtenerCodigoCertificado(matriculaCabecera.Id));
                        }

                        //reemplazar duracion en horas de Programa Especifico
                        if (item.Contains("{tPEspecifico.duracion}"))
                        {
                            remplazo = remplazo.Replace("{tPEspecifico.duracion}", (certificados != null) ? certificados.Duracion.ToString() : _repPespecifico.ObtenerDuracionProgramaEspecifico(matriculaCabecera.IdPespecifico, matriculaCabecera.Id));
                        }
                        if (item.Contains("{T_Pgeneral.EstructuraCurricular}"))
                        {
                            var estructuraPorVersion = _repPgeneralAsubPgeneral.ObtenerCursosEstrucuraCurricular(matriculaCabecera.Id);
                            if (estructuraPorVersion.Count > 0)
                            {
                                string listaEstructura = $@"<table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>
                                                        <tr style='vertical-align: text-top;'>
                                                        <td style = 'vertical-align: text-top;text-align:left;font-weight: normal;' > ";
                                listaEstructura += "<ul style='margin-top:-20px'>";
                                foreach (var hijo in estructuraPorVersion)
                                {
                                    listaEstructura += $@"<li style='padding-bottom:5px'>{hijo.Nombre.Replace(" - COLOMBIA", "")} ({hijo.Duracion} horas cronológicas)</li>";
                                }


                                listaEstructura += "</ul></td></tr></table>";
                                _estructuraCurricular = listaEstructura;
                                this.ContenidoCertificado.EstructuraCurricular = listaEstructura;
                                remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", listaEstructura);
                            }
                            else
                            {
                                var estructuraCurso = _repDocumentoSeccionPw.ObtenerEstructuraCurso(detalleMatriculaCabecera.IdProgramaGeneral);
                                if (estructuraCurso.Count > 0)
                                {
                                    string listaEstructura = $@"
                                                        <table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>
                                                        <tr style='vertical-align: text-top;'>
                                                        <td style = 'vertical-align: text-top;text-align:left;font-weight: normal;' > ";
                                    listaEstructura += "<ul style='margin-top:-20px'>";

                                    foreach (var capitulo in estructuraCurso)
                                    {
                                        listaEstructura += $@"<li style='padding-bottom:5px'>{capitulo.Contenido.Replace(" - COLOMBIA", "")} </li>";
                                    }

                                    listaEstructura += "</ul></td></tr></table>";
                                    _estructuraCurricular = listaEstructura;
                                    this.ContenidoCertificado.EstructuraCurricular = listaEstructura;
                                    remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", listaEstructura);
                                }
                                else {
                                    remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", "");
                                }
                                
                            }

                        }
                        if (item.Contains("Template"))
                        {

                            var etiq = listaObjetoWhasApp.Where(x => x.codigo.Contains("Template")).ToList();
                            SeccionEtiquetaDTO valor = new SeccionEtiquetaDTO();
                            string etiqueta = "";

                            foreach (var a in etiq)
                            {
                                List<string> ListaPalabras = new List<string>();
                                char[] delimitador = new char[] { '.' };
                                string IdPlantilla = "";
                                string IdColumna = "";
                                string[] array = a.codigo.Split(delimitador, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string s in array)
                                {
                                    ListaPalabras.Add(s);
                                }
                                IdPlantilla = ListaPalabras[3].ToString();
                                IdColumna = ListaPalabras[4].ToString();


                                var prevalor = _repPespecifico.ObtenerContenidoTemplate(new Guid(IdPlantilla), new Guid(IdColumna.Replace("}", "")), oportunidad.IdCentroCosto ?? default(int));

                                if (prevalor != null && _estructuraCurricular == "")
                                {
                                    etiqueta = a.codigo;
                                    valor = prevalor;
                                }
                                else
                                {
                                    remplazo = remplazo.Replace(a.codigo, "");
                                }

                            }
                            if (etiqueta != "")
                            {
                                var tratamiento = valor.Valor.Replace("<p>", "<p id='estructura'<p>");
                                tratamiento = tratamiento.Replace("<li>", "<li id='liseparar'<li>");
                                tratamiento = tratamiento.Replace("&bull", "<li id='liseparar'");
                                estructura = tratamiento.Split("<p id='estructura'").Where(x => x.StartsWith("<p><strong>")).ToList();
                                List<string> todo = new List<string>();
                                int contador = 0;
                                string htmltabla = "";
                                List<string> total = new List<string>();
                                foreach (var li in estructura)
                                {
                                    foreach (var li1 in li.Split("<li id='liseparar'").ToList())
                                    {
                                        if (li1.Contains("<p>"))
                                            total.Add(li1.Replace("p>", "li>").Replace("<li>", "<li style='padding-bottom:5px'>").Replace("strong", "span").Replace("<ul>", "").Replace("</ul>", "").Replace("\"", "'").Replace("<div class='slide'>", "").Replace("<p style='padding-left:30px;'>", ""));
                                       
                                    }

                                }
                                int Cantidadcolumns = total.Count / 25;
                                int residuo = total.Count % 25;
                                if (residuo != 0)
                                {
                                    Cantidadcolumns++;
                                }
                                int reparticion = Cantidadcolumns;

                                PdfPTable PdfTable = new PdfPTable(Cantidadcolumns);
                                PdfTable.WidthPercentage = 100f;
                                PdfPCell PdfPCell = null;
                                string cadena1 = "";
                                string cadena2 = "";
                                string cadena3 = "";
                                string cadena4 = "";
                                List<string> registros = new List<string>();
                                foreach (var concatenar in total)
                                {
                                    if (Conteo < 22)
                                    {
                                        if (Conteo == 0)
                                        {
                                            cadena1 += "<ul style='margin-top:-20px'>";
                                        }
                                        cadena1 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                    }
                                    else
                                    {
                                        if (Conteo < 48)
                                        {
                                            cadena2 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                        }
                                        else
                                        {
                                            if (Conteo < 75)
                                            {
                                                cadena3 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                            }
                                            else
                                            {
                                                cadena4 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                            }
                                        }
                                    }

                                    Conteo++;
                                }

                                htmltabla += "<table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>";
                                contador = 0;
                                for (int rows = 0; rows < 1; rows++)
                                {
                                    htmltabla += "<tr style='vertical-align: text-top;'>";
                                    for (int column = 0; column < Cantidadcolumns; column++)
                                    {
                                        if (column == 0)
                                        {
                                            if (cadena1.TrimEnd(' ').EndsWith("</li>\n"))
                                            {
                                                cadena1 = cadena1 + "</ul>";
                                            }
                                            htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena1}";
                                        }
                                        if (column == 1)
                                        {
                                            if (cadena2.StartsWith("<li>"))
                                            {
                                                cadena2 = "<ul>" + cadena2;
                                            }
                                            if (cadena2.TrimEnd(' ').EndsWith("</li>\n"))
                                            {
                                                cadena2 = cadena2 + "</ul>";
                                            }
                                            htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena2}";
                                        }
                                        if (column == 2)
                                        {
                                            if (cadena3.StartsWith("<li>"))
                                            {
                                                cadena3 = "<ul>" + cadena3;
                                            }
                                            if (cadena3.TrimEnd(' ').EndsWith("</li>\n"))
                                            {
                                                cadena3 = cadena3 + "</ul>";
                                            }
                                            htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena3}";
                                        }
                                        if (column == 3)
                                        {
                                            if (cadena4.StartsWith("<li>"))
                                            {
                                                cadena4 = "<ul>" + cadena4;
                                            }
                                            if (cadena4.TrimEnd(' ').EndsWith("</li>\n"))
                                            {
                                                cadena4 = cadena4 + "</ul>";
                                            }
                                            htmltabla += $@"<th style='vertical-align:top;text-align:left;font-weight: normal;'> {cadena4}";
                                        }


                                        //List list = new List();
                                        ////for (int i = 0; i < estructura[contador].Split("<li id='liseparar'").ToList().Count(); i++)
                                        ////{
                                        //var lista = estructura[contador].Split("<li id='liseparar'").ToList();

                                        //foreach (var li in lista)
                                        //{
                                        //    string cadenaSinTags = Regex.Replace(li, "<.*?>", string.Empty);
                                        //    list.Add(new ListItem(cadenaSinTags));
                                        //}
                                        ////}
                                        ////var parse = new Phrase();
                                        ////parse.Add(list);
                                        ////PdfPCell = new PdfPCell();
                                        ////PdfPCell.AddElement(parse);
                                        ////PdfTable.AddCell(PdfPCell);
                                        contador = contador + 1;
                                        htmltabla += "</td>";
                                    }
                                    htmltabla += "</tr>";
                                }
                                htmltabla += "</table>";
                                PdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table

                                //doc.Add(paragraph);// add paragraph to the document
                                //_document.Add(PdfTable); // add pdf table to the document

                                this.ContenidoCertificado.EstructuraCurricular = htmltabla;
                                remplazo = remplazo.Replace(etiqueta, htmltabla);
                            }
                        }
                        //htmltemplate.SetStyleSheet(GenerateStyleCertificadoAutomatico(tipoletra));
                        //htmltemplate.Parse(new StringReader(remplazo));

                    }
                    var pdfPosterior = PdfGenerator.GeneratePdf(remplazo, config);
                    temmporal = ImportarPdfDocument(pdfPosterior);
                    page = temmporal.Pages[0];
                    pdf.AddPage(page);

                    gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(pdf.Pages[1], PdfSharp.Drawing.XGraphicsPdfPageOptions.Prepend);

                    System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(FondoReversoCertificado); ;
                    webRequest.AllowWriteStreamBuffering = true;
                    System.Net.WebResponse webResponse = webRequest.GetResponse();
                    PdfSharp.Drawing.XImage xImage = PdfSharp.Drawing.XImage.FromStream(webResponse.GetResponseStream());
                    gfx.DrawImage(xImage, 0, 0, 843, 595);
                }

                //_document.Close();
                pdf.Save(ms, false);
                return ms.ToArray();
            }

        }
        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Genera certificado modular.
        /// </summary> 
        public byte[] GenerarCertificadoModular(int IdPlantillaF, int IdPlantillaP, int IdMatriculaCabecera, int TipoCurso, int IdModulo, ref string codigoCertificado, ref int IdPgeneral, int IdProgramaGeneral)
        {
            int idCursoMoodle = 0;
            int idPEspecifico = 0;
            if (TipoCurso == 0)
            {
                idPEspecifico = IdModulo;
            }
            else
            {
                idCursoMoodle = IdModulo;
            }
            ContenidoCertificado = new CertificadoGeneradoAutomaticoContenidoBO();
            int IdPlantillaBase = 0;
            if (!_repOportunidadClasificacionOperaciones.Exist(x => x.IdMatriculaCabecera == IdMatriculaCabecera))
            {
                throw new Exception("Matricula no esta Casificada!");
            }

            if (!_repMatriculaCabecera.Exist(x => x.Id == IdMatriculaCabecera))
            {
                throw new Exception("Matricula cabecera no valida!");
            }

            var listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();

            var matriculaCabecera = _repMatriculaCabecera.FirstBy(x => x.Id == IdMatriculaCabecera);
            var detalleMatriculaCabecera = matriculaCabecera.ObtenerDetalleMatricula();
            var plantilla = _repPlantilla.FirstById(IdPlantillaF);

            IdPgeneral = detalleMatriculaCabecera.IdProgramaGeneral;
            IdPlantillaBase = plantilla.IdPlantillaBase;

            var plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantillaF);
            var alumno = _repAlumno.FirstById(matriculaCabecera.IdAlumno);

            var oportunidad = _repOportunidad.FirstById(detalleMatriculaCabecera.IdOportunidad);
            var DatosCompuestosOportunidad = _repOportunidad.ObtenerDatosCompuestosPorIdOportunidad(oportunidad.Id);
            var personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);

            var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

            foreach (var etiqueta in listaEtiqueta)
            {
                listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = string.Concat("{", etiqueta, "}"), texto = "" });
            }
            List<string> sepacion = new List<string>();
            string FondoFrontalCertificado = "";
            string FondoReversoCertificado = "";
            string FondoFrontalConstancia = "";
            string FondoReversoConstancia = "";
            sepacion = plantillaBase.Cuerpo.Split("###").ToList();

            Dictionary<string, object> map = new Dictionary<string, object>();
            map["font_factory"] = new MyFontFactory();

            using (MemoryStream ms = new MemoryStream())
            {
                PdfSharp.Pdf.PdfDocument pdf = new PdfSharp.Pdf.PdfDocument();
                var config = new PdfGenerateConfig();

                config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                config.PageSize = PdfSharp.PageSize.A4;
                config.MarginTop = 144;
                config.MarginBottom = 30;
                config.MarginLeft = 111;
                config.MarginRight = 83;

                int Conteo = 0;
                List<string> estructura = new List<string>();

                string remplazo = "";
                foreach (var item in sepacion)
                {
                    string direccionCodigoQR = "";
                    remplazo = item;



                    if (item.Contains("repositorioweb"))
                    {
                        FondoFrontalCertificado = item;
                    }
                    if (item.Contains("acute"))
                    {
                        remplazo = remplazo.Replace("&Eacute;", "É").Replace("&Aacute;", "Á").Replace("&Iacute;", "Í").Replace("&Oacute;", "Ó").Replace("&Uacute;", "Ú");
                    }
                    if (item.Contains("CODIGOQR"))
                    {
                        string url = "";

                        url = _repAlumno.ObtenerCodigoCertificadoModular(matriculaCabecera.Id);

                        var urlCodigo = "https://bsginstitute.com/informacionCertificado?cod=" + matriculaCabecera.Id + "." + url;
                        QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                        QRCodeData qrDatos = qRCodeGenerator.CreateQrCode(urlCodigo, QRCodeGenerator.ECCLevel.Q);
                        QRCode qRCodigo = new QRCode(qrDatos);

                        System.Drawing.Bitmap qrImagen = qRCodigo.GetGraphic(7, System.Drawing.Color.Black, System.Drawing.Color.White, false);
                        System.Drawing.Image objImage = (System.Drawing.Image)qrImagen;

                        direccionCodigoQR = _repAlumno.guardarArchivosQR(ImageToByte2(objImage), "image/jpeg", url);
                        this.ContenidoCertificado.CodigoQr = direccionCodigoQR;
                        remplazo = remplazo.Replace("CODIGOQR", $@"<img style='vertical-align:baseline' src='{direccionCodigoQR}' width=55 height=55  />");

                    }
                    if (item.Contains("{tPLA_PGeneral.Nombre}"))
                    {
                        string nombrePrograma = null;
                        if (TipoCurso == 0)
                        {
                            nombrePrograma = _repPgeneral.ObtenerNombrePorIdPespecifico(idPEspecifico);
                        }
                        else
                        {
                            nombrePrograma = _repPgeneral.ObtenerNombrePorIdCursoMoodle(IdProgramaGeneral);
                        }

                        this.ContenidoCertificado.NombrePrograma = nombrePrograma.ToUpper();
                        if (nombrePrograma.ToUpper().StartsWith("CURSO") || nombrePrograma.ToUpper().StartsWith("PROGRAMA"))
                        {
                            remplazo = remplazo.Replace("{T_Pgeneral.TipoCapacitacion}", "");
                        }
                        else
                        {
                            remplazo = remplazo.Replace("{T_Pgeneral.TipoCapacitacion}", " programa");
                        }
                        remplazo = remplazo.Replace("{tPLA_PGeneral.Nombre}", nombrePrograma);
                    }
                    if (item.Contains("{T_Pgeneral.CodigoPartner}"))
                    {
                        var codigoPartner = _repPgeneral.ObtenerCodigoPartner(detalleMatriculaCabecera.IdProgramaGeneral);
                        if (codigoPartner != null)
                        {
                            this.ContenidoCertificado.CodigoPartner = codigoPartner;
                            remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", codigoPartner);
                        }
                        else
                        {
                            throw new Exception("no se puede Calcular codigo Partner");
                        }
                    }
                    //reemplazar nombre 1 alumno
                    if (item.Contains("{T_Alumno.NombreCompleto}"))
                    {
                    }
                    if (item.Contains("{tAlumnos.nombre1}"))
                    {
                        string nombreAlumno = "";
                        nombreAlumno = alumno.Nombre1.ToUpper();
                        remplazo = remplazo.Replace("{tAlumnos.nombre1}", alumno.Nombre1.ToUpper());

                        if (item.Contains("{tAlumnos.nombre2}"))
                        {
                            nombreAlumno = nombreAlumno + " " + alumno.Nombre2.ToUpper();
                            remplazo = remplazo.Replace("{tAlumnos.nombre2}", alumno.Nombre2.ToUpper());
                        }
                        if (item.Contains("{tAlumnos.apepaterno}"))
                        {
                            nombreAlumno = nombreAlumno + " " + alumno.ApellidoPaterno.ToUpper();
                            remplazo = remplazo.Replace("{tAlumnos.apepaterno}", alumno.ApellidoPaterno.ToUpper());
                        }
                        if (item.Contains("{tAlumnos.apematerno}"))
                        {
                            nombreAlumno = nombreAlumno + " " + alumno.ApellidoMaterno.ToUpper();
                            remplazo = remplazo.Replace("{tAlumnos.apematerno}", alumno.ApellidoMaterno.ToUpper());
                        }

                        this.ContenidoCertificado.NombreAlumno = nombreAlumno;

                    }
                    if (item.Contains("{tpla_pgeneral.pw_duracion}"))
                    {
                        remplazo = remplazo.Replace("{tpla_pgeneral.pw_duracion}", DatosCompuestosOportunidad.pw_duracion);
                    }
                    if (item.Contains("{tCentroCosto.centrocosto}"))
                    {
                        this.ContenidoCertificado.NombreCentroCosto = detalleMatriculaCabecera.NombreCentroCosto.ToUpper();
                        remplazo = remplazo.Replace("{tCentroCosto.centrocosto}", detalleMatriculaCabecera.NombreCentroCosto.ToUpper());
                    }
                    if (item.Contains("{tPEspecifico.ciudad}"))
                    {
                        var PrimeraLetra = detalleMatriculaCabecera.NombreCiudad.Substring(0, 1).ToUpper();
                        var resto = detalleMatriculaCabecera.NombreCiudad.Substring(1).ToLower();
                        this.ContenidoCertificado.Ciudad = PrimeraLetra + resto;
                        remplazo = remplazo.Replace("{tPEspecifico.ciudad}", PrimeraLetra + resto);
                    }
                    if (item.Contains("{tPEspecifico.EscalaCalificacion}"))
                    {
                        var escalaCalificacion = detalleMatriculaCabecera.EscalaCalificacion;
                        this.ContenidoCertificado.EscalaCalificacion = escalaCalificacion;
                        remplazo = remplazo.Replace("{tPEspecifico.EscalaCalificacion}", escalaCalificacion.ToString());
                    }
                    //reemplazar Fecha Inicio Capacitacion
                    if (item.Contains("{T_Alumno.FechaInicioCapacitacion}"))
                    {
                        string fechaInicioCapacitacion = null;
                        if (TipoCurso == 0)
                        {
                            fechaInicioCapacitacion = _repAlumno.ObtenerFechaInicioCapacitacionModuloPespecifico(matriculaCabecera.Id, idPEspecifico);
                        }
                        else
                        {
                            fechaInicioCapacitacion = _repAlumno.ObtenerFechaInicioCapacitacionModuloCursoMoodle(matriculaCabecera.Id, idCursoMoodle);
                        }
                        if (fechaInicioCapacitacion == null)
                        {
                            throw new Exception("No se Pudo Calcular FechaInicioCapacitacion!");
                        }
                        this.ContenidoCertificado.FechaInicioCapacitacion = fechaInicioCapacitacion;
                        remplazo = remplazo.Replace("{T_Alumno.FechaInicioCapacitacion}", fechaInicioCapacitacion);
                    }

                    //reemplazar Fecha Fin Capacitacion
                    if (item.Contains("{T_Alumno.FechaFinCapacitacion}"))
                    {
                        string fechaFinCapacitacion = null;
                        if (TipoCurso == 0)
                        {
                            fechaFinCapacitacion = _repAlumno.ObtenerFechaFinCapacitacionModuloPespecifico(matriculaCabecera.Id, idPEspecifico);
                        }
                        else
                        {
                            fechaFinCapacitacion = _repAlumno.ObtenerFechaFinCapacitacionModuloCursoMoodle(matriculaCabecera.Id, idCursoMoodle);
                        }
                        if (fechaFinCapacitacion == null)
                        {
                            throw new Exception("No se Pudo Calcular FechaFinCapacitacion!");
                        }
                        this.ContenidoCertificado.FechaFinCapacitacion = fechaFinCapacitacion;
                        remplazo = remplazo.Replace("{T_Alumno.FechaFinCapacitacion}", fechaFinCapacitacion);
                    }
                    //reemplazar Calificacion Promedio
                    if (item.Contains("{T_Alumno.CalificacionPromedio}"))
                    {
                        string calificacionPromedio = null;
                        if (TipoCurso == 1)
                        {
                            calificacionPromedio = _repAlumno.ObtenerNotaPromedioModulo(matriculaCabecera.Id, idCursoMoodle);
                        }
                        else
                        {
                            calificacionPromedio = _repAlumno.ObtenerNotaPromedioModulo(matriculaCabecera.Id, idPEspecifico);
                        }
                        if (calificacionPromedio == null)
                        {
                            throw new Exception("No se Pudo Calcular Nota Promedio!");
                        }
                        this.ContenidoCertificado.CalificacionPromedio = Convert.ToInt32(calificacionPromedio);
                        remplazo = remplazo.Replace("{T_Alumno.CalificacionPromedio}", (Convert.ToInt32(calificacionPromedio)).ToString());
                    }
                    //reemplazar Fecha Emision Certificado
                    if (item.Contains("{T_Alumno.FechaEmisionCertificado}"))
                    {
                        var fechaEmision = _repAlumno.ObtenerFechaEmision();
                        this.ContenidoCertificado.FechaEmisionCertificado = fechaEmision;
                        remplazo = remplazo.Replace("{T_Alumno.FechaEmisionCertificado}", fechaEmision);
                    }

                    //reemplazar Fecha Codigo Certificado
                    if (item.Contains("{T_Alumno.CodigoCertificado}"))
                    {
                        var CodigoCertificado = _repAlumno.ObtenerCodigoCertificadoModular(matriculaCabecera.Id);
                        codigoCertificado = CodigoCertificado;
                        remplazo = remplazo.Replace("{T_Alumno.CodigoCertificado}", CodigoCertificado);
                    }

                    //reemplazar duracion en horas de Programa Especifico
                    if (item.Contains("{tPEspecifico.duracion}"))
                    {
                        string duracionPespecifico = null;
                        if (TipoCurso == 1)
                        {
                            duracionPespecifico = _repPespecifico.ObtenerDuracionProgramaGeneralVersion(IdProgramaGeneral, matriculaCabecera.Id);
                        }
                        else
                        {
                            duracionPespecifico = _repPespecifico.ObtenerDuracionProgramaEspecificoModulo(idPEspecifico, matriculaCabecera.Id);
                        }
                        if (duracionPespecifico == null)
                        {
                            throw new Exception("No se Pudo Calcular Duracion!");
                        }
                        this.ContenidoCertificado.DuracionPespecifico = Int32.Parse(duracionPespecifico);
                        remplazo = remplazo.Replace("{tPEspecifico.duracion}", duracionPespecifico);
                    }
                    /*reemplazar Codigo de matricula del alumno */
                    if (item.Contains("{T_MatriculaCabecera.CodigoMatricula}"))
                    {
                        remplazo = remplazo.Replace("{T_MatriculaCabecera.CodigoMatricula}", matriculaCabecera.CodigoMatricula);
                    }
                    /*reemplazar Codigo de matricula del alumno */
                    if (item.Contains("{T_Alumno.CorrelativoConstancia}"))
                    {
                        var correlativoConstancia = _repCertificadoGeneradoAutomatico.ObtenerCorrelativoCertificado();
                        this.ContenidoCertificado.CorrelativoConstancia = correlativoConstancia;
                        remplazo = remplazo.Replace("{T_Alumno.CorrelativoConstancia}", correlativoConstancia.ToString());
                    }
                    /*reemplazar Cronograma de notas del alumno por matricula*/
                    if (item.Contains("{T_Alumno.CronogramaNotas}"))
                    {
                        string tablaNota = "";
                        var cronogramaNota = _repAlumno.ObtenerCronogramaNota(matriculaCabecera.Id);
                        tablaNota += "<table  style='font-size:11px; border:1px solid #575656;font-family:Arial;color:#575656;border-collapse: collapse;width:100%;' cellspacing='3' cellpadding='3'>";
                        tablaNota += $@"<tr style='border:1px solid #575656;border-collapse: collapse;'>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> CURSO </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> NOTA </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> ESTADO </td>
                                            </tr>";
                        int contador = 0;
                        for (int rows = 0; rows < cronogramaNota.Count; rows++)
                        {
                            tablaNota += "<tr style='border:1px solid #575656;border-collapse: collapse;'>";
                            tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;;text-align:left'> {cronogramaNota[rows].Curso}</td>";
                            tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:center'> {cronogramaNota[rows].Nota}</td>";
                            tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:center'> {cronogramaNota[rows].Estado}</td>";

                            contador = contador + 1;
                            tablaNota += "</tr>";
                        }
                        tablaNota += "</table>";
                        this.ContenidoCertificado.CronogramaNota = tablaNota;
                        remplazo = remplazo.Replace("{T_Alumno.CronogramaNotas}", tablaNota);
                    }
                    /*reemplazar Cronograma de Asistencia del alumno por matricula*/
                    if (item.Contains("{T_Alumno.CronogramaAsistencia}"))
                    {
                        string tablaAsistencia = "";
                        var cronogramaAsistencia = _repAlumno.ObtenerCronogramaAsistencia(matriculaCabecera.Id);

                        tablaAsistencia += "<table  style='font-size:11px; border:1px solid #575656;font-family:Arial;color:#575656;border-collapse: collapse;width:100%;' cellspacing='3' cellpadding='3'>";
                        tablaAsistencia += $@"<tr style='border:1px solid #575656;border-collapse: collapse;'>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> CURSO </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> ASISTENCIA </td>
                                            </tr>";
                        int contador = 0;
                        for (int rows = 0; rows < cronogramaAsistencia.Count; rows++)
                        {
                            tablaAsistencia += "<tr style='border:1px solid #575656;border-collapse: collapse;'>";
                            tablaAsistencia += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:left;'> {cronogramaAsistencia[rows].Curso}</td>";
                            tablaAsistencia += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:Center;'> {cronogramaAsistencia[rows].PorcentajeAsistencia}</td>";

                            contador = contador + 1;
                            tablaAsistencia += "</tr>";
                        }
                        tablaAsistencia += "</table>";
                        this.ContenidoCertificado.CronogramaAsistencia = tablaAsistencia;
                        remplazo = remplazo.Replace("{T_Alumno.CronogramaAsistencia}", tablaAsistencia);
                    }
                    if (item.Contains("{T_Pgeneral.EstructuraCurricular}"))
                    {
                        var estructuraPorVersion = _repPgeneralAsubPgeneral.ObtenerCursosEstrucuraCurricular(matriculaCabecera.Id);
                        if (estructuraPorVersion.Count > 0)
                        {
                            string listaEstructura = $@"<table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>
                                                        <tr style='vertical-align: text-top;'>
                                                        <td style = 'vertical-align: text-top;text-align:left;font-weight: normal;' > ";
                            listaEstructura += "<ul style='margin-top:-20px'>";
                            foreach (var hijo in estructuraPorVersion)
                            {
                                listaEstructura += $@"<li style='padding-bottom:5px'>{hijo.Nombre.Replace(" - COLOMBIA", "")} ({hijo.Duracion} horas cronológicas)</li>";
                            }


                            listaEstructura += "</ul></td></tr></table>";
                            this.ContenidoCertificado.EstructuraCurricular = listaEstructura;
                            remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", listaEstructura);
                        }
                        else
                        {
                            var estructuraCurso = _repDocumentoSeccionPw.ObtenerEstructuraCurso(detalleMatriculaCabecera.IdProgramaGeneral);
                            if (estructuraCurso.Count > 0)
                            {
                                string listaEstructura = $@"
                                                        <table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>
                                                        <tr style='vertical-align: text-top;'>
                                                        <td style = 'vertical-align: text-top;text-align:left;font-weight: normal;' > ";
                                listaEstructura += "<ul style='margin-top:-20px'>";

                                foreach (var capitulo in estructuraCurso)
                                {
                                    listaEstructura += $@"<li style='padding-bottom:5px'>{capitulo.Contenido.Replace(" - COLOMBIA", "")} </li>";
                                }

                                listaEstructura += "</ul></td></tr></table>";
                                this.ContenidoCertificado.EstructuraCurricular = listaEstructura;
                                remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", listaEstructura);
                            }
                            else
                            {
                                remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", "");
                            }

                        }
                    }
                }
                var pdfFrontal = PdfGenerator.GeneratePdf(remplazo, config);
                var temmporal = ImportarPdfDocument(pdfFrontal);
                PdfSharp.Pdf.PdfPage page = temmporal.Pages[0];
                pdf.AddPage(page);

                PdfSharp.Drawing.XGraphics gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(pdf.Pages[0], PdfSharp.Drawing.XGraphicsPdfPageOptions.Prepend);

                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(FondoFrontalCertificado);
                webRequest.AllowWriteStreamBuffering = true;
                System.Net.WebResponse webResponse = webRequest.GetResponse();
                PdfSharp.Drawing.XImage xImage = PdfSharp.Drawing.XImage.FromStream(webResponse.GetResponseStream());
                gfx.DrawImage(xImage, 0, 0, 843, 595);

                if (IdPlantillaP != 0 && IdPlantillaP != null)
                {
                    config = new PdfGenerateConfig();

                    config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                    config.PageSize = PdfSharp.PageSize.A4;
                    config.MarginLeft = 257;
                    config.MarginRight = 83;
                    config.MarginTop = 49;
                    config.MarginBottom = 0;

                    string _estructuraCurricular = "";
                    listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();
                    var plantillaP = _repPlantilla.FirstById(IdPlantillaP);
                    var plantillaBaseP = _repPlantilla.ObtenerPlantillaCorreo(IdPlantillaP);

                    var listaEtiquetaP = plantillaBaseP.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

                    foreach (var etiqueta in listaEtiquetaP)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = string.Concat("{", etiqueta, "}"), texto = "" });
                    }
                    sepacion = new List<string>();

                    sepacion = plantillaBaseP.Cuerpo.Split("###").ToList();

                    foreach (var item in sepacion)
                    {
                        remplazo = item;

                        if (item.Contains("acute"))
                        {
                            remplazo = remplazo.Replace("&Eacute;", "É").Replace("&Aacute;", "Á").Replace("&Iacute;", "Í").Replace("&Oacute;", "Ó").Replace("&Uacute;", "Ú");
                        }
                        if (item.Contains("repositorioweb"))
                        {
                            FondoReversoCertificado = item;
                        }
                        if (item.Contains("{tPLA_PGeneral.Nombre}"))
                        {
                            string nombrePrograma = null;
                            if (TipoCurso == 0)
                            {
                                nombrePrograma = _repPgeneral.ObtenerNombrePorIdPespecifico(idPEspecifico);
                            }
                            else
                            {
                                nombrePrograma = _repPgeneral.ObtenerNombrePorIdCursoMoodle(IdProgramaGeneral);
                            }
                            remplazo = remplazo.Replace("{tPLA_PGeneral.Nombre}", nombrePrograma.ToUpper());
                        }
                        if (item.Contains("{T_Pgeneral.CodigoPartner}"))
                        {
                            var codigoPartner = _repPgeneral.ObtenerCodigoPartner(detalleMatriculaCabecera.IdProgramaGeneral);
                            if (codigoPartner != null)
                            {
                                this.ContenidoCertificado.CodigoPartner = codigoPartner;
                                remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", codigoPartner);
                            }
                            else
                            {
                                remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", "");
                            }
                        }
                        if (item.Contains("{tCentroCosto.centrocosto}"))
                        {
                            remplazo = remplazo.Replace("{tCentroCosto.centrocosto}", detalleMatriculaCabecera.NombreCentroCosto);
                        }
                        if (item.Contains("{tPEspecifico.ciudad}"))
                        {
                            remplazo = remplazo.Replace("{tPEspecifico.ciudad}", detalleMatriculaCabecera.NombreCiudad.Substring(0, 1).ToUpper() + detalleMatriculaCabecera.NombreCiudad.Substring(1).ToLower()); ;
                        }
                        //reemplazar nombre 1 alumno
                        if (item.Contains("{T_Alumno.NombreCompleto}"))
                        {

                        }
                        if (item.Contains("{tAlumnos.nombre1}"))
                        {
                            remplazo = remplazo.Replace("{tAlumnos.nombre1}", alumno.Nombre1);

                            if (item.Contains("{tAlumnos.nombre2}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.nombre2}", alumno.Nombre2);
                            }
                            if (item.Contains("{tAlumnos.apematerno}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.apematerno}", alumno.ApellidoMaterno);
                            }
                            if (item.Contains("{tAlumnos.apepaterno}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.apepaterno}", alumno.ApellidoPaterno);
                            }

                        }

                        if (item.Contains("{tpla_pgeneral.pw_duracion}"))
                        {
                            remplazo = remplazo.Replace("{tpla_pgeneral.pw_duracion}", DatosCompuestosOportunidad.pw_duracion);
                        }
                        //reemplazar Fecha Inicio Capacitacion
                        if (item.Contains("{T_Alumno.FechaInicioCapacitacion}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaInicioCapacitacion}", _repAlumno.ObtenerFechaInicioCapacitacion(matriculaCabecera.Id));
                        }

                        //reemplazar Fecha Fin Capacitacion
                        if (item.Contains("{T_Alumno.FechaFinCapacitacion}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaFinCapacitacion}", _repAlumno.ObtenerFechaFinCapacitacion(matriculaCabecera.Id));
                        }
                        //reemplazar Calificacion Promedio
                        if (item.Contains("{T_Alumno.CalificacionPromedio}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.CalificacionPromedio}", _repAlumno.ObtenerNotaPromedio(matriculaCabecera.Id));
                        }
                        //reemplazar Fecha Emision Certificado
                        if (item.Contains("{T_Alumno.FechaEmisionCertificado}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaEmisionCertificado}", _repAlumno.ObtenerFechaEmision());
                        }

                        //reemplazar Fecha Codigo Certificado
                        if (item.Contains("{T_Alumno.CodigoCertificado}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.CodigoCertificado}", _repAlumno.ObtenerCodigoCertificado(matriculaCabecera.Id));
                        }

                        //reemplazar duracion en horas de Programa Especifico
                        if (item.Contains("{tPEspecifico.duracion}"))
                        {
                            remplazo = remplazo.Replace("{tPEspecifico.duracion}", _repPespecifico.ObtenerDuracionProgramaEspecifico(matriculaCabecera.IdPespecifico, matriculaCabecera.Id));
                        }
                        if (item.Contains("{T_Pgeneral.EstructuraCurricular}"))
                        {
                            var estructuraPorVersion = _repPgeneralAsubPgeneral.ObtenerCursosEstrucuraCurricular(matriculaCabecera.Id);
                            if (estructuraPorVersion.Count > 0)
                            {
                                string listaEstructura = $@"<table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>
                                                        <tr style='vertical-align: text-top;'>
                                                        <td style = 'vertical-align: text-top;text-align:left;font-weight: normal;' > ";
                                listaEstructura += "<ul style='margin-top:-20px'>";
                                foreach (var hijo in estructuraPorVersion)
                                {
                                    listaEstructura += $@"<li style='padding-bottom:5px'>{hijo.Nombre.Replace(" - COLOMBIA", "")} ({hijo.Duracion} horas cronológicas)</li>";
                                }


                                listaEstructura += "</ul></td></tr></table>";
                                _estructuraCurricular = listaEstructura;
                                this.ContenidoCertificado.EstructuraCurricular = listaEstructura;
                                remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", listaEstructura);
                            }
                            else
                            {
                                var estructuraCurso = _repDocumentoSeccionPw.ObtenerEstructuraCurso(detalleMatriculaCabecera.IdProgramaGeneral);
                                if (estructuraCurso.Count > 0)
                                {
                                    string listaEstructura = $@"
                                                        <table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>
                                                        <tr style='vertical-align: text-top;'>
                                                        <td style = 'vertical-align: text-top;text-align:left;font-weight: normal;' > ";
                                    listaEstructura += "<ul style='margin-top:-20px'>";

                                    foreach (var capitulo in estructuraCurso)
                                    {
                                        listaEstructura += $@"<li style='padding-bottom:5px'>{capitulo.Contenido.Replace(" - COLOMBIA", "")} </li>";
                                    }

                                    listaEstructura += "</ul></td></tr></table>";
                                    _estructuraCurricular = listaEstructura;
                                    this.ContenidoCertificado.EstructuraCurricular = listaEstructura;
                                    remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", listaEstructura);
                                }
                                else
                                {
                                    remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", "");
                                }

                            }

                        }
                        if (item.Contains("Template"))
                        {
                            int idCentroCosto = 0;
                            if (TipoCurso == 1)
                            {
                                idCentroCosto = _repPespecifico.ObtenerCentroCostoporProgramaHijo(IdProgramaGeneral, matriculaCabecera.Id);
                            }
                            else
                            {
                                idCentroCosto = _repPespecifico.ObtenerCentroCostoporProgramaEspecifico(idPEspecifico);
                            }

                            var etiq = listaObjetoWhasApp.Where(x => x.codigo.Contains("Template")).ToList();
                            SeccionEtiquetaDTO valor = new SeccionEtiquetaDTO();
                            string etiqueta = "";

                            foreach (var a in etiq)
                            {
                                List<string> ListaPalabras = new List<string>();
                                char[] delimitador = new char[] { '.' };
                                string IdPlantilla = "";
                                string IdColumna = "";
                                string[] array = a.codigo.Split(delimitador, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string s in array)
                                {
                                    ListaPalabras.Add(s);
                                }
                                IdPlantilla = ListaPalabras[3].ToString();
                                IdColumna = ListaPalabras[4].ToString();


                                var prevalor = _repPespecifico.ObtenerContenidoTemplate(new Guid(IdPlantilla), new Guid(IdColumna.Replace("}", "")), idCentroCosto);

                                if (prevalor != null && _estructuraCurricular == "")
                                {
                                    etiqueta = a.codigo;
                                    valor = prevalor;
                                }
                                else
                                {
                                    remplazo = remplazo.Replace(a.codigo, "");
                                }

                            }
                            if (etiqueta != "")
                            {
                                var tratamiento = valor.Valor.Replace("<p>", "<p id='estructura'<p>");
                                tratamiento = tratamiento.Replace("<li>", "<li id='liseparar'<li>");
                                estructura = tratamiento.Split("<p id='estructura'").Where(x => x.StartsWith("<p><strong>")).ToList();
                                List<string> todo = new List<string>();
                                //PdfContentByte cb = pdfWriter.DirectContent;
                                //ColumnText ct = new ColumnText(cb);

                                //ct.Alignment = Element.ALIGN_JUSTIFIED;
                                int contador = 0;
                                string htmltabla = "";
                                List<string> total = new List<string>();
                                foreach (var li in estructura)
                                {
                                    foreach (var li1 in li.Split("<li id='liseparar'").ToList())
                                    {
                                        if (li1.Contains("<p>"))
                                            total.Add(li1.Replace("p>", "li>").Replace("<li>", "<li style='padding-bottom:5px'>").Replace("strong", "span").Replace("<ul>", "").Replace("</ul>", "").Replace("\"", "'").Replace("<div class='slide'>", ""));
                                        
                                    }
                                }
                                int Cantidadcolumns = total.Count / 25;
                                int residuo = total.Count % 25;
                                if (residuo != 0)
                                {
                                    Cantidadcolumns++;
                                }
                                int reparticion = Cantidadcolumns;

                                PdfPTable PdfTable = new PdfPTable(Cantidadcolumns);
                                PdfTable.WidthPercentage = 100f;
                                PdfPCell PdfPCell = null;
                                string cadena1 = "";
                                string cadena2 = "";
                                string cadena3 = "";
                                string cadena4 = "";
                                List<string> registros = new List<string>();
                                foreach (var concatenar in total)
                                {
                                    //int residuo = Conteo % 27;
                                    if (Conteo < 22)
                                    {
                                        if (Conteo == 0)
                                        {
                                            cadena1 += "<ul style='margin-top:-20px'>";
                                        }
                                        cadena1 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                    }
                                    else
                                    {
                                        if (Conteo < 48)
                                        {
                                            //if (!etiqueta.Contains("Programa") && !concatenar.Contains("li>"))
                                            cadena2 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                        }
                                        else
                                        {
                                            if (Conteo < 75)
                                            {
                                                //if (!etiqueta.Contains("Programa") && !concatenar.Contains("li>"))
                                                cadena3 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                            }
                                            else
                                            {
                                                //if (!etiqueta.Contains("Programa") && !concatenar.Contains("li>"))
                                                cadena4 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                            }
                                        }
                                    }

                                    Conteo++;
                                }

                                htmltabla += "<table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>";
                                contador = 0;
                                for (int rows = 0; rows < 1; rows++)
                                {
                                    htmltabla += "<tr style='vertical-align: text-top;'>";
                                    for (int column = 0; column < Cantidadcolumns; column++)
                                    {
                                        if (column == 0)
                                        {
                                            if (cadena1.TrimEnd(' ').EndsWith("</li>\n"))
                                            {
                                                cadena1 = cadena1 + "</ul>";
                                            }
                                            htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena1}";
                                        }
                                        if (column == 1)
                                        {
                                            if (cadena2.StartsWith("<li>"))
                                            {
                                                cadena2 = "<ul>" + cadena2;
                                            }
                                            if (cadena2.TrimEnd(' ').EndsWith("</li>\n"))
                                            {
                                                cadena2 = cadena2 + "</ul>";
                                            }
                                            htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena2}";
                                        }
                                        if (column == 2)
                                        {
                                            if (cadena3.StartsWith("<li>"))
                                            {
                                                cadena3 = "<ul>" + cadena3;
                                            }
                                            if (cadena3.TrimEnd(' ').EndsWith("</li>\n"))
                                            {
                                                cadena3 = cadena3 + "</ul>";
                                            }
                                            htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena3}";
                                        }
                                        if (column == 3)
                                        {
                                            if (cadena4.StartsWith("<li>"))
                                            {
                                                cadena4 = "<ul>" + cadena4;
                                            }
                                            if (cadena4.TrimEnd(' ').EndsWith("</li>\n"))
                                            {
                                                cadena4 = cadena4 + "</ul>";
                                            }
                                            htmltabla += $@"<th style='vertical-align:top;text-align:left;font-weight: normal;'> {cadena4}";
                                        }
                                        contador = contador + 1;
                                        htmltabla += "</td>";
                                    }
                                    htmltabla += "</tr>";
                                }
                                htmltabla += "</table>";
                                PdfTable.SpacingBefore = 15f;
                                this.ContenidoCertificado.EstructuraCurricular = htmltabla;
                                remplazo = remplazo.Replace(etiqueta, htmltabla);
                            }
                        }
                    }
                    var pdfPosterior = PdfGenerator.GeneratePdf(remplazo, config);
                    temmporal = ImportarPdfDocument(pdfPosterior);
                    page = temmporal.Pages[0];
                    pdf.AddPage(page);

                    gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(pdf.Pages[1], PdfSharp.Drawing.XGraphicsPdfPageOptions.Prepend);

                    webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(FondoReversoCertificado); ;
                    webRequest.AllowWriteStreamBuffering = true;
                    webResponse = webRequest.GetResponse();
                    xImage = PdfSharp.Drawing.XImage.FromStream(webResponse.GetResponseStream());
                    gfx.DrawImage(xImage, 0, 0, 843, 595);
                }
                pdf.Save(ms, false);
                return ms.ToArray();
            }

        }
        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Genera certificado IRCA.
        /// </summary> 
        public byte[] GenerarCertificadoIrca(int IdPlantillaF, int IdContenidoCertificadoIrca, int idPEspecifico, ref string codigoCertificado, ref int IdPgeneral)
        {
            int idCursoMoodle = 0;

            ContenidoCertificado = new CertificadoGeneradoAutomaticoContenidoBO();
            int IdPlantillaBase = 0;

            if (!_repContenidoCertificadoIrca.Exist(IdContenidoCertificadoIrca))
            {
                throw new Exception("Contenido Certificado Irca no Existe!");
            }
            var ContenidoCertificadoIrca = _repContenidoCertificadoIrca.FirstById(IdContenidoCertificadoIrca);

            if (!_repOportunidadClasificacionOperaciones.Exist(x => x.IdMatriculaCabecera == ContenidoCertificadoIrca.IdMatriculaCabecera))
            {
                throw new Exception("Matricula no esta Casificada!");
            }

            if (!_repMatriculaCabecera.Exist(x => x.Id == ContenidoCertificadoIrca.IdMatriculaCabecera))
            {
                throw new Exception("Matricula cabecera no valida!");
            }

            var listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();

            var matriculaCabecera = _repMatriculaCabecera.FirstBy(x => x.Id == ContenidoCertificadoIrca.IdMatriculaCabecera);
            var detalleMatriculaCabecera = matriculaCabecera.ObtenerDetalleMatricula();
            var plantilla = _repPlantilla.FirstById(IdPlantillaF);

            IdPgeneral = detalleMatriculaCabecera.IdProgramaGeneral;
            IdPlantillaBase = plantilla.IdPlantillaBase;

            var plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantillaF);
            var alumno = _repAlumno.FirstById(matriculaCabecera.IdAlumno);

            var oportunidad = _repOportunidad.FirstById(detalleMatriculaCabecera.IdOportunidad);
            var DatosCompuestosOportunidad = _repOportunidad.ObtenerDatosCompuestosPorIdOportunidad(oportunidad.Id);
            var personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);

            var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

            foreach (var etiqueta in listaEtiqueta)
            {
                listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = string.Concat("{", etiqueta, "}"), texto = "" });
            }
            List<string> sepacion = new List<string>();
            string FondoFrontalCertificado = "";
            string FondoReversoCertificado = "";
            string FondoFrontalConstancia = "";
            string FondoReversoConstancia = "";
            sepacion = plantillaBase.Cuerpo.Split("###").ToList();

            Dictionary<string, object> map = new Dictionary<string, object>();
            map["font_factory"] = new MyFontFactory();

            using (MemoryStream ms = new MemoryStream())
            {
                PdfSharp.Pdf.PdfDocument pdf = new PdfSharp.Pdf.PdfDocument();
                var config = new PdfGenerateConfig();

                config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                config.PageSize = PdfSharp.PageSize.A4;
                config.MarginTop = 215;
                config.MarginBottom = 20;
                config.MarginLeft = 105;
                config.MarginRight = 95;

                int Conteo = 0;
                List<string> estructura = new List<string>();

                string remplazo = "";
                foreach (var item in sepacion)
                {
                    string direccionCodigoQR = "";
                    remplazo = item;

                    if (item.Contains("repositorioweb"))
                    {
                        FondoFrontalCertificado = item;
                    }
                    if (item.Contains("acute"))
                    {
                        remplazo = remplazo.Replace("&Eacute;", "É").Replace("&Aacute;", "Á").Replace("&Iacute;", "Í").Replace("&Oacute;", "Ó").Replace("&Uacute;", "Ú");
                    }
                    if (item.Contains("CODIGOQR"))
                    {
                        string url = "";

                        url = _repAlumno.ObtenerCodigoCertificadoIrca(matriculaCabecera.Id);

                        var urlCodigo = "https://bsginstitute.com/informacionCertificado?cod=" + matriculaCabecera.Id + "." + url;
                        QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                        QRCodeData qrDatos = qRCodeGenerator.CreateQrCode(urlCodigo, QRCodeGenerator.ECCLevel.Q);
                        QRCode qRCodigo = new QRCode(qrDatos);

                        System.Drawing.Bitmap qrImagen = qRCodigo.GetGraphic(7, System.Drawing.Color.Black, System.Drawing.Color.White, false);
                        System.Drawing.Image objImage = (System.Drawing.Image)qrImagen;

                        direccionCodigoQR = _repAlumno.guardarArchivosQR(ImageToByte2(objImage), "image/jpeg", url);
                        this.ContenidoCertificado.CodigoQr = direccionCodigoQR;
                        remplazo = remplazo.Replace("CODIGOQR", $@"<img style='vertical-align:baseline' src='{direccionCodigoQR}' width=55 height=55  />");

                    }
                    if (item.Contains("{tPLA_PGeneral.Nombre}"))
                    {
                        string nombrePrograma = null;
                        nombrePrograma = _repPgeneral.ObtenerNombrePorIdPespecifico(idPEspecifico);

                        this.ContenidoCertificado.NombrePrograma = nombrePrograma.ToUpper();
                        if (nombrePrograma.ToUpper().StartsWith("CURSO") || nombrePrograma.ToUpper().StartsWith("PROGRAMA"))
                        {
                            remplazo = remplazo.Replace("{T_Pgeneral.TipoCapacitacion}", "");
                        }
                        else
                        {
                            remplazo = remplazo.Replace("{T_Pgeneral.TipoCapacitacion}", " programa");
                        }
                        remplazo = remplazo.Replace("{tPLA_PGeneral.Nombre}", nombrePrograma);
                    }
                    if (item.Contains("{T_Pgeneral.CodigoPartner}"))
                    {
                        var codigoPartner = _repPgeneral.ObtenerCodigoPartner(detalleMatriculaCabecera.IdProgramaGeneral);
                        if (codigoPartner != null)
                        {
                            this.ContenidoCertificado.CodigoPartner = codigoPartner;
                            remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", codigoPartner);
                        }
                        else
                        {
                            throw new Exception("No se puede Calcular Codigo Partner");
                        }
                    }
                    //reemplazar nombre 1 alumno
                    if (item.Contains("{T_Alumno.NombreCompleto}"))
                    {
                    }
                    if (item.Contains("{tAlumnos.nombre1}"))
                    {
                        string nombreAlumno = "";
                        nombreAlumno = alumno.Nombre1.ToUpper();
                        remplazo = remplazo.Replace("{tAlumnos.nombre1}", alumno.Nombre1.ToUpper());

                        if (item.Contains("{tAlumnos.nombre2}"))
                        {
                            nombreAlumno = nombreAlumno + " " + alumno.Nombre2.ToUpper();
                            remplazo = remplazo.Replace("{tAlumnos.nombre2}", alumno.Nombre2.ToUpper());
                        }
                        if (item.Contains("{tAlumnos.apepaterno}"))
                        {
                            nombreAlumno = nombreAlumno + " " + alumno.ApellidoPaterno.ToUpper();
                            remplazo = remplazo.Replace("{tAlumnos.apepaterno}", alumno.ApellidoPaterno.ToUpper());
                        }
                        if (item.Contains("{tAlumnos.apematerno}"))
                        {
                            nombreAlumno = nombreAlumno + " " + alumno.ApellidoMaterno.ToUpper();
                            remplazo = remplazo.Replace("{tAlumnos.apematerno}", alumno.ApellidoMaterno.ToUpper());
                        }

                        this.ContenidoCertificado.NombreAlumno = nombreAlumno;

                    }
                    if (item.Contains("{tpla_pgeneral.pw_duracion}"))
                    {
                        remplazo = remplazo.Replace("{tpla_pgeneral.pw_duracion}", DatosCompuestosOportunidad.pw_duracion);
                    }
                    if (item.Contains("{tCentroCosto.centrocosto}"))
                    {
                        this.ContenidoCertificado.NombreCentroCosto = detalleMatriculaCabecera.NombreCentroCosto.ToUpper();
                        remplazo = remplazo.Replace("{tCentroCosto.centrocosto}", detalleMatriculaCabecera.NombreCentroCosto.ToUpper());
                    }
                    if (item.Contains("{tPEspecifico.ciudad}"))
                    {
                        var PrimeraLetra = detalleMatriculaCabecera.NombreCiudad.Substring(0, 1).ToUpper();
                        var resto = detalleMatriculaCabecera.NombreCiudad.Substring(1).ToLower();
                        this.ContenidoCertificado.Ciudad = PrimeraLetra + resto;
                        remplazo = remplazo.Replace("{tPEspecifico.ciudad}", PrimeraLetra + resto);
                    }
                    if (item.Contains("{tPEspecifico.EscalaCalificacion}"))
                    {
                        var escalaCalificacion = detalleMatriculaCabecera.EscalaCalificacion;
                        this.ContenidoCertificado.EscalaCalificacion = escalaCalificacion;
                        remplazo = remplazo.Replace("{tPEspecifico.EscalaCalificacion}", escalaCalificacion.ToString());
                    }
                    //reemplazar Fecha Inicio Capacitacion
                    if (item.Contains("{T_Alumno.FechaInicioCapacitacion}"))
                    {
                        string fechaInicioCapacitacion = null;

                        fechaInicioCapacitacion = _repAlumno.ObtenerFechaInicioCapacitacionModuloPespecifico(matriculaCabecera.Id, idPEspecifico);

                        if (fechaInicioCapacitacion == null)
                        {
                            throw new Exception("No se Pudo Calcular FechaInicioCapacitacion!");
                        }
                        this.ContenidoCertificado.FechaInicioCapacitacion = fechaInicioCapacitacion;
                        remplazo = remplazo.Replace("{T_Alumno.FechaInicioCapacitacion}", fechaInicioCapacitacion);
                    }

                    //reemplazar Fecha Fin Capacitacion
                    if (item.Contains("{T_Alumno.FechaFinCapacitacion}"))
                    {
                        string fechaFinCapacitacion = null;

                        fechaFinCapacitacion = _repAlumno.ObtenerFechaFinCapacitacionModuloPespecifico(matriculaCabecera.Id, idPEspecifico);

                        if (fechaFinCapacitacion == null)
                        {
                            throw new Exception("No se Pudo Calcular FechaFinCapacitacion!");
                        }
                        this.ContenidoCertificado.FechaFinCapacitacion = fechaFinCapacitacion;
                        remplazo = remplazo.Replace("{T_Alumno.FechaFinCapacitacion}", fechaFinCapacitacion);
                    }
                    //reemplazar Calificacion Promedio
                    if (item.Contains("{T_Alumno.CalificacionPromedio}"))
                    {
                        string calificacionPromedio = null;

                        calificacionPromedio = _repAlumno.ObtenerNotaPromedioModulo(matriculaCabecera.Id, idPEspecifico);

                        if (calificacionPromedio == null)
                        {
                            throw new Exception("No se Pudo Calcular Nota Promedio!");
                        }
                        this.ContenidoCertificado.CalificacionPromedio = Convert.ToInt32(calificacionPromedio);
                        remplazo = remplazo.Replace("{T_Alumno.CalificacionPromedio}", (Convert.ToInt32(calificacionPromedio)).ToString());
                    }
                    //reemplazar Fecha Emision Certificado
                    if (item.Contains("{T_Alumno.FechaEmisionCertificado}"))
                    {
                        var fechaEmision = _repAlumno.ObtenerFechaEmision();
                        this.ContenidoCertificado.FechaEmisionCertificado = fechaEmision;
                        remplazo = remplazo.Replace("{T_Alumno.FechaEmisionCertificado}", fechaEmision);
                    }

                    //reemplazar Fecha Codigo Certificado
                    if (item.Contains("{T_Alumno.CodigoCertificado}"))
                    {
                        var CodigoCertificado = _repAlumno.ObtenerCodigoCertificadoIrca(matriculaCabecera.Id);
                        codigoCertificado = CodigoCertificado;
                        remplazo = remplazo.Replace("{T_Alumno.CodigoCertificado}", CodigoCertificado);
                    }

                    //reemplazar duracion en horas de Programa Especifico
                    if (item.Contains("{tPEspecifico.duracion}"))
                    {
                        string duracionPespecifico = null;

                        duracionPespecifico = _repPespecifico.ObtenerDuracionProgramaEspecificoModulo(idPEspecifico, matriculaCabecera.Id);

                        if (duracionPespecifico == null)
                        {
                            throw new Exception("No se Pudo Calcular Duracion!");
                        }
                        this.ContenidoCertificado.DuracionPespecifico = Int32.Parse(duracionPespecifico);
                        remplazo = remplazo.Replace("{tPEspecifico.duracion}", duracionPespecifico);
                    }
                    /*reemplazar Codigo de matricula del alumno */
                    if (item.Contains("{T_MatriculaCabecera.CodigoMatricula}"))
                    {
                        remplazo = remplazo.Replace("{T_MatriculaCabecera.CodigoMatricula}", matriculaCabecera.CodigoMatricula);
                    }
                    /*reemplazar Codigo de matricula del alumno */
                    if (item.Contains("{T_Alumno.CorrelativoConstancia}"))
                    {
                        var correlativoConstancia = _repCertificadoGeneradoAutomatico.ObtenerCorrelativoCertificado();
                        this.ContenidoCertificado.CorrelativoConstancia = correlativoConstancia;
                        remplazo = remplazo.Replace("{T_Alumno.CorrelativoConstancia}", correlativoConstancia.ToString());
                    }                    
                    if (item.Contains("{T_Pgeneral.IdCursoIrca}"))
                    {
                        if (ContenidoCertificadoIrca.CursoIrcaId == 0)
                        {
                            throw new Exception("El Registro no tiene IdCursoIrca");
                        }
                        remplazo = remplazo.Replace("{T_Pgeneral.IdCursoIrca}", ContenidoCertificadoIrca.CursoIrcaId.ToString());
                    }
                    if (item.Contains("{T_Pgeneral.NombreCursoIrca}"))
                    {

                        if (ContenidoCertificadoIrca.NombreCurso == "")
                        {
                            throw new Exception("El Registro no tiene NombreCurso");
                        }
                        this.ContenidoCertificado.NombrePrograma = ContenidoCertificadoIrca.NombreCurso;
                        remplazo = remplazo.Replace("{T_Pgeneral.NombreCursoIrca}", ContenidoCertificadoIrca.NombreCurso);
                    }
                    if (item.Contains("{T_Pgeneral.CodigoCursoIrca}"))
                    {
                        if (ContenidoCertificadoIrca.CodigoCurso == "")
                        {
                            throw new Exception("El Registro no tiene CodigoCurso");
                        }
                        remplazo = remplazo.Replace("{T_Pgeneral.CodigoCursoIrca}", ContenidoCertificadoIrca.CodigoCurso);
                    }
                    if (item.Contains("{T_Pgeneral.FechaInicioCursoIrca}"))
                    {
                        this.ContenidoCertificado.FechaInicioCapacitacion = ContenidoCertificadoIrca.FechaInicio.ToString("dd-MM-yyyy");
                        remplazo = remplazo.Replace("{T_Pgeneral.FechaInicioCursoIrca}", ContenidoCertificadoIrca.FechaInicio.ToString("dd-MM-yyyy"));
                    }
                    if (item.Contains("{T_Pgeneral.FechaFinCursoIrca}"))
                    {
                        this.ContenidoCertificado.FechaFinCapacitacion = ContenidoCertificadoIrca.FechaFin.ToString("dd-MM-yyyy");
                        remplazo = remplazo.Replace("{T_Pgeneral.FechaFinCursoIrca}", ContenidoCertificadoIrca.FechaFin.ToString("dd-MM-yyyy"));
                    }
                    if (item.Contains("{T_Pgeneral.DuracionCursoIrca}"))
                    {
                        if (ContenidoCertificadoIrca.DuracionCurso == 0)
                        {
                            throw new Exception("El Registro no tiene DuracionCursoIrca");
                        }
                        this.ContenidoCertificado.DuracionPespecifico = ContenidoCertificadoIrca.DuracionCurso;
                        remplazo = remplazo.Replace("{T_Pgeneral.DuracionCursoIrca}", ContenidoCertificadoIrca.DuracionCurso.ToString());
                    }
                    if (item.Contains("{T_Pgeneral.DescripcionResultadoCursoIrca}"))
                    {
                        string resultadoCursoIrca = null;
                        resultadoCursoIrca = _repContenidoCertificadoIrca.ObtenerDescripcionResultadoIrca(ContenidoCertificadoIrca.Id);
                        if (ContenidoCertificadoIrca.ResultadoCurso == null)
                        {
                            throw new Exception("El Registro no tiene ResultadoCurso");
                        }
                        remplazo = remplazo.Replace("{T_Pgeneral.DescripcionResultadoCursoIrca}", resultadoCursoIrca);
                    }
                }
                var pdfFrontal = PdfGenerator.GeneratePdf(remplazo, config);
                var temmporal = ImportarPdfDocument(pdfFrontal);
                PdfSharp.Pdf.PdfPage page = temmporal.Pages[0];
                pdf.AddPage(page);

                PdfSharp.Drawing.XGraphics gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(pdf.Pages[0], PdfSharp.Drawing.XGraphicsPdfPageOptions.Prepend);

                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(FondoFrontalCertificado);
                webRequest.AllowWriteStreamBuffering = true;
                System.Net.WebResponse webResponse = webRequest.GetResponse();
                PdfSharp.Drawing.XImage xImage = PdfSharp.Drawing.XImage.FromStream(webResponse.GetResponseStream());
                gfx.DrawImage(xImage, 0, 0, 843, 595);

                pdf.Save(ms, false);
                return ms.ToArray();
            }

        }
        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Genera certificado sin Fondo.
        /// </summary> 
        public byte[] GenerarCertificadoSinFondo(ContenidoCertificadoSinFondoDTO contenido)
        {
            ContenidoCertificado = new CertificadoGeneradoAutomaticoContenidoBO();

            var ContenidoCertificadoIrca = _repContenidoCertificadoIrca.FirstBy(w => w.IdMatriculaCabecera == contenido.IdMatriculaCabecera && w.NombreCurso == contenido.NombrePrograma);

            var oportunidadClasificacionOperaciones = _repOportunidadClasificacionOperaciones.FirstBy(x => x.IdOportunidad == contenido.IdOportunidad);

            if (!_repMatriculaCabecera.Exist(x => x.Id == oportunidadClasificacionOperaciones.IdMatriculaCabecera))
            {
                throw new Exception("Matricula cabecera no valida!");
            }

            var listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();

            var plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(contenido.IdPlantillaFrontal);

            var oportunidad = _repOportunidad.FirstById(contenido.IdOportunidad);

            var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

            foreach (var etiqueta in listaEtiqueta)
            {
                listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = string.Concat("{", etiqueta, "}"), texto = "" });
            }
            List<string> sepacion = new List<string>();

            sepacion = plantillaBase.Cuerpo.Split("###").ToList();

            using (MemoryStream ms = new MemoryStream())
            {
                PdfSharp.Pdf.PdfDocument pdf = new PdfSharp.Pdf.PdfDocument();
                var config = new PdfGenerateConfig();
                if (contenido.IdPlantillaFrontal == 1338)
                {
                    config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                    config.PageSize = PdfSharp.PageSize.A4;
                    config.MarginTop = 215;
                    config.MarginBottom = 20;
                    config.MarginLeft = 105;
                    config.MarginRight = 95;
                }
                else
                {
                    config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                    config.PageSize = PdfSharp.PageSize.A4;
                    config.MarginTop = 144;
                    config.MarginBottom = 30;
                    config.MarginLeft = 111;
                    config.MarginRight = 83;
                }

                int Conteo = 0;
                //int cantidadItems = 0;
                List<string> estructura = new List<string>();

                string remplazo = "";
                foreach (var item in sepacion)
                {
                    string direccionCodigoQR = "";
                    string tipoletra = "Times New Roman";
                    remplazo = item;
                    string searchString = "/*";
                    int startIndex = remplazo.IndexOf(searchString);
                    searchString = "*/";
                    int endIndex = 0;
                    string substring = "";

                    if (startIndex != -1)
                    {
                        endIndex = remplazo.IndexOf(searchString, startIndex);
                        substring = remplazo.Substring(startIndex + searchString.Length, endIndex - searchString.Length);
                    }
                    if (substring != null && substring != "")
                    {
                        tipoletra = substring;
                    }
                    else
                    {
                        searchString = "text-align:";
                        startIndex = remplazo.IndexOf(searchString);
                        searchString = ";";

                        if (startIndex != -1)
                        {
                            endIndex = remplazo.IndexOf(searchString, startIndex);
                            substring = remplazo.Substring(startIndex + searchString.Length, endIndex + searchString.Length - startIndex);
                        }
                        if (substring != null && substring != "")
                        {
                            tipoletra = substring;
                        }
                    }
                    if (item.Contains("repositorioweb"))
                    {

                    }
                    if (item.Contains("acute"))
                    {
                        remplazo = remplazo.Replace("&Eacute;", "É").Replace("&Aacute;", "Á").Replace("&Iacute;", "Í").Replace("&Oacute;", "Ó").Replace("&Uacute;", "Ú");
                    }
                    if (item.Contains("CODIGOQR"))
                    {
                        string url = "";
                        if (contenido.CodigoQR != null)
                        {
                            direccionCodigoQR = contenido.CodigoQR;
                        }
                        else
                        {
                            url = contenido.CodigoCertificado;
                            direccionCodigoQR = "https://repositorioweb.blob.core.windows.net/repositorioweb/certificados/CodigoQR/" + url;
                        }

                        this.ContenidoCertificado.CodigoQr = direccionCodigoQR;
                        remplazo = remplazo.Replace("CODIGOQR", $@"<img style='vertical-align:baseline' src='{direccionCodigoQR}' width=55 height=55  />");

                    }
                    if (item.Contains("{tPLA_PGeneral.Nombre}"))
                    {
                        var nombrePrograma = contenido.NombrePrograma.Replace(" - COLOMBIA", "");
                        this.ContenidoCertificado.NombrePrograma = nombrePrograma;
                        if (nombrePrograma.ToUpper().StartsWith("CURSO") || nombrePrograma.ToUpper().StartsWith("PROGRAMA"))
                        {
                            remplazo = remplazo.Replace("{T_Pgeneral.TipoCapacitacion}", "");
                        }
                        else
                        {
                            remplazo = remplazo.Replace("{T_Pgeneral.TipoCapacitacion}", " programa");
                        }
                        remplazo = remplazo.Replace("{tPLA_PGeneral.Nombre}", contenido.NombrePrograma);
                    }
                    if (item.Contains("{T_Pgeneral.CodigoPartner}"))
                    {
                        string codigoPartner = null;
                        if (contenido.CodigoPartner != null)
                        {
                            codigoPartner = contenido.CodigoPartner;
                        }
                        else
                        {
                            codigoPartner = _repPgeneral.ObtenerCodigoPartner(contenido.IdMatriculaCabecera);
                        }

                        if (codigoPartner != null)
                        {
                            this.ContenidoCertificado.CodigoPartner = codigoPartner;
                            remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", codigoPartner);
                        }
                        else
                        {
                            throw new Exception("No se puede Calcular CodigoPartner");
                        }
                    }
                    //reemplazar nombre 1 alumno
                    if (item.Contains("{T_Alumno.NombreCompleto}"))
                    {
                    }
                    if (item.Contains("{tAlumnos.nombre1}"))
                    {
                        remplazo = remplazo.Replace("{tAlumnos.nombre1}", "");

                        if (item.Contains("{tAlumnos.nombre2}"))
                        {
                            remplazo = remplazo.Replace("{tAlumnos.nombre2}", "");
                        }
                        if (item.Contains("{tAlumnos.apepaterno}"))
                        {
                            remplazo = remplazo.Replace("{tAlumnos.apepaterno}", "");
                        }
                        if (item.Contains("{tAlumnos.apematerno}"))
                        {
                            remplazo = remplazo.Replace("{tAlumnos.apematerno}", contenido.NombreAlumno);
                        }
                    }
                    if (item.Contains("{tPEspecifico.ciudad}"))
                    {
                        remplazo = remplazo.Replace("{tPEspecifico.ciudad}", contenido.Ciudad);
                    }
                    //reemplazar Fecha Inicio Capacitacion
                    if (item.Contains("{T_Alumno.FechaInicioCapacitacion}"))
                    {
                        remplazo = remplazo.Replace("{T_Alumno.FechaInicioCapacitacion}", contenido.FechaInicioCapacitacion);
                    }

                    //reemplazar Fecha Fin Capacitacion
                    if (item.Contains("{T_Alumno.FechaFinCapacitacion}"))
                    {
                        remplazo = remplazo.Replace("{T_Alumno.FechaFinCapacitacion}", contenido.FechaFinCapacitacion);
                    }
                    //reemplazar Fecha Codigo Certificado
                    if (item.Contains("{T_Alumno.CodigoCertificado}"))
                    {
                        remplazo = remplazo.Replace("{T_Alumno.CodigoCertificado}", contenido.CodigoCertificado);
                    }

                    //reemplazar duracion en horas de Programa Especifico
                    if (item.Contains("{tPEspecifico.duracion}"))
                    {
                        remplazo = remplazo.Replace("{tPEspecifico.duracion}", contenido.DuracionPespecifico.Value.ToString());
                    }
                    if (item.Contains("{T_Alumno.FechaEmisionCertificado}"))
                    {
                        remplazo = remplazo.Replace("{T_Alumno.FechaEmisionCertificado}", contenido.FechaEmisionCertificado);
                    }

                    if (item.Contains("{T_Pgeneral.IdCursoIrca}"))
                    {
                        if (ContenidoCertificadoIrca.CursoIrcaId == 0)
                        {
                            throw new Exception("El Registro no tiene IdCursoIrca");
                        }
                        remplazo = remplazo.Replace("{T_Pgeneral.IdCursoIrca}", ContenidoCertificadoIrca.CursoIrcaId.ToString());
                    }
                    if (item.Contains("{T_Pgeneral.NombreCursoIrca}"))
                    {

                        if (ContenidoCertificadoIrca.NombreCurso == "")
                        {
                            throw new Exception("El Registro no tiene NombreCurso");
                        }
                        this.ContenidoCertificado.NombrePrograma = ContenidoCertificadoIrca.NombreCurso;
                        remplazo = remplazo.Replace("{T_Pgeneral.NombreCursoIrca}", ContenidoCertificadoIrca.NombreCurso);
                    }
                    if (item.Contains("{T_Pgeneral.CodigoCursoIrca}"))
                    {
                        if (ContenidoCertificadoIrca.CodigoCurso == "")
                        {
                            throw new Exception("El Registro no tiene CodigoCurso");
                        }
                        remplazo = remplazo.Replace("{T_Pgeneral.CodigoCursoIrca}", ContenidoCertificadoIrca.CodigoCurso);
                    }
                    if (item.Contains("{T_Pgeneral.FechaInicioCursoIrca}"))
                    {
                        this.ContenidoCertificado.FechaInicioCapacitacion = ContenidoCertificadoIrca.FechaInicio.ToString("dd-MM-yyyy");
                        remplazo = remplazo.Replace("{T_Pgeneral.FechaInicioCursoIrca}", ContenidoCertificadoIrca.FechaInicio.ToString("dd-MM-yyyy"));
                    }
                    if (item.Contains("{T_Pgeneral.FechaFinCursoIrca}"))
                    {
                        this.ContenidoCertificado.FechaFinCapacitacion = ContenidoCertificadoIrca.FechaFin.ToString("dd-MM-yyyy");
                        remplazo = remplazo.Replace("{T_Pgeneral.FechaFinCursoIrca}", ContenidoCertificadoIrca.FechaFin.ToString("dd-MM-yyyy"));
                    }
                    if (item.Contains("{T_Pgeneral.DuracionCursoIrca}"))
                    {
                        if (ContenidoCertificadoIrca.DuracionCurso == 0)
                        {
                            throw new Exception("El Registro no tiene DuracionCursoIrca");
                        }
                        this.ContenidoCertificado.DuracionPespecifico = ContenidoCertificadoIrca.DuracionCurso;
                        remplazo = remplazo.Replace("{T_Pgeneral.DuracionCursoIrca}", ContenidoCertificadoIrca.DuracionCurso.ToString());
                    }
                    if (item.Contains("{T_Pgeneral.DescripcionResultadoCursoIrca}"))
                    {
                        string resultadoCursoIrca = null;
                        resultadoCursoIrca = _repContenidoCertificadoIrca.ObtenerDescripcionResultadoIrca(ContenidoCertificadoIrca.Id);
                        if (ContenidoCertificadoIrca.ResultadoCurso == null)
                        {
                            throw new Exception("El Registro no tiene ResultadoCurso");
                        }
                        remplazo = remplazo.Replace("{T_Pgeneral.DescripcionResultadoCursoIrca}", resultadoCursoIrca);
                    }
                }
                var pdfFrontal = PdfGenerator.GeneratePdf(remplazo, config);
                var temmporal = ImportarPdfDocument(pdfFrontal);
                PdfSharp.Pdf.PdfPage page = temmporal.Pages[0];
                pdf.AddPage(page);

                if (contenido.IdPlantillaPosterior != 0)
                {
                    config = new PdfGenerateConfig();

                    config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                    config.PageSize = PdfSharp.PageSize.A4;
                    config.MarginLeft = 257;
                    config.MarginRight = 83;
                    config.MarginTop = 49;
                    config.MarginBottom = 0;

                    string _estructuraCurricular = "";
                    listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();
                    var plantillaP = _repPlantilla.FirstById(contenido.IdPlantillaPosterior);
                    var plantillaBaseP = _repPlantilla.ObtenerPlantillaCorreo(contenido.IdPlantillaPosterior);

                    var listaEtiquetaP = plantillaBaseP.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

                    foreach (var etiqueta in listaEtiquetaP)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = string.Concat("{", etiqueta, "}"), texto = "" });
                    }
                    sepacion = new List<string>();

                    sepacion = plantillaBaseP.Cuerpo.Split("###").ToList();

                    foreach (var item in sepacion)
                    {
                        remplazo = item;

                        if (item.Contains("acute"))
                        {
                            remplazo = remplazo.Replace("&Eacute;", "É").Replace("&Aacute;", "Á").Replace("&Iacute;", "Í").Replace("&Oacute;", "Ó").Replace("&Uacute;", "Ú");
                        }
                        if (item.Contains("repositorioweb"))
                        {

                        }

                        if (item.Contains("{tPLA_PGeneral.Nombre}"))
                        {
                            remplazo = remplazo.Replace("{tPLA_PGeneral.Nombre}", contenido.NombrePrograma);
                        }

                        if (item.Contains("{tPEspecifico.ciudad}"))
                        {
                            remplazo = remplazo.Replace("{tPEspecifico.ciudad}", contenido.Ciudad); ;
                        }
                        if (item.Contains("{tAlumnos.nombre1}"))
                        {

                            remplazo = remplazo.Replace("{tAlumnos.nombre1}", "");

                            if (item.Contains("{tAlumnos.nombre2}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.nombre2}", "");
                            }
                            if (item.Contains("{tAlumnos.apematerno}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.apematerno}", "");
                            }
                            if (item.Contains("{tAlumnos.apepaterno}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.apepaterno}", contenido.NombreAlumno);
                            }

                        }

                        //reemplazar Fecha Emision Certificado
                        if (item.Contains("{T_Alumno.FechaEmisionCertificado}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaEmisionCertificado}", contenido.FechaEmisionCertificado);
                        }

                        if (item.Contains("{T_Pgeneral.EstructuraCurricular}"))
                        {
                            if (contenido.EstructuraCurricular != null)
                            {
                                remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", contenido.EstructuraCurricular);
                                _estructuraCurricular = contenido.EstructuraCurricular;
                                this.ContenidoCertificado.EstructuraCurricular = _estructuraCurricular;
                            }
                            else
                            {
                                var estructuraPorVersion = _repPgeneralAsubPgeneral.ObtenerCursosEstrucuraCurricular(ContenidoCertificadoIrca.IdMatriculaCabecera);
                                if (estructuraPorVersion.Count > 0)
                                {
                                    string listaEstructura = $@"<table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>
                                                        <tr style='vertical-align: text-top;'>
                                                        <td style = 'vertical-align: text-top;text-align:left;font-weight: normal;' > ";
                                    listaEstructura += "<ul style='margin-top:-20px'>";
                                    foreach (var hijo in estructuraPorVersion)
                                    {
                                        listaEstructura += $@"<li style='padding-bottom:5px'>{hijo.Nombre.Replace(" - COLOMBIA", "")} ({hijo.Duracion} horas cronológicas)</li>";
                                    }


                                    listaEstructura += "</ul></td></tr></table>";
                                    _estructuraCurricular = listaEstructura;
                                    this.ContenidoCertificado.EstructuraCurricular = listaEstructura;
                                    remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", listaEstructura);
                                }
                                else
                                {                                    
                                    remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", "");

                                }
                            }


                        }
                        if (item.Contains("Template"))
                        {

                            var etiq = listaObjetoWhasApp.Where(x => x.codigo.Contains("Template")).ToList();
                            SeccionEtiquetaDTO valor = new SeccionEtiquetaDTO();
                            string etiqueta = "";

                            foreach (var a in etiq)
                            {
                                List<string> ListaPalabras = new List<string>();
                                char[] delimitador = new char[] { '.' };
                                string IdPlantilla = "";
                                string IdColumna = "";
                                string[] array = a.codigo.Split(delimitador, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string s in array)
                                {
                                    ListaPalabras.Add(s);
                                }
                                IdPlantilla = ListaPalabras[3].ToString();
                                IdColumna = ListaPalabras[4].ToString();


                                var prevalor = _repPespecifico.ObtenerContenidoTemplate(new Guid(IdPlantilla), new Guid(IdColumna.Replace("}", "")), oportunidad.IdCentroCosto ?? default(int));

                                if (prevalor != null && _estructuraCurricular == "")
                                {
                                    etiqueta = a.codigo;
                                    valor = prevalor;
                                }
                                else
                                {
                                    remplazo = remplazo.Replace(a.codigo, "");
                                }

                            }
                            if (etiqueta != "")
                            {
                                if (contenido.EstructuraCurricular != null && contenido.EstructuraCurricular != "")
                                {
                                    remplazo = remplazo.Replace(etiqueta, contenido.EstructuraCurricular);
                                }
                                else
                                {
                                    var tratamiento = valor.Valor.Replace("<p>", "<p id='estructura'<p>");
                                    tratamiento = tratamiento.Replace("<li>", "<li id='liseparar'<li>");
                                    tratamiento = tratamiento.Replace("&bull", "<li id='liseparar'");
                                    estructura = tratamiento.Split("<p id='estructura'").Where(x => x.StartsWith("<p><strong>")).ToList();
                                    List<string> todo = new List<string>();

                                    int contador = 0;
                                    string htmltabla = "";
                                    List<string> total = new List<string>();
                                    foreach (var li in estructura)
                                    {
                                        foreach (var li1 in li.Split("<li id='liseparar'").ToList())
                                        {
                                            if (li1.Contains("<p>"))
                                                total.Add(li1.Replace("p>", "li>").Replace("<li>", "<li style='padding-bottom:5px'>").Replace("strong", "span").Replace("<ul>", "").Replace("</ul>", "").Replace("\"", "'").Replace("<div class='slide'>", "").Replace("<p style='padding-left:30px;'>", ""));

                                        }

                                    }
                                    int Cantidadcolumns = total.Count / 25;
                                    int residuo = total.Count % 25;
                                    if (residuo != 0)
                                    {
                                        Cantidadcolumns++;
                                    }
                                    int reparticion = Cantidadcolumns;

                                    PdfPTable PdfTable = new PdfPTable(Cantidadcolumns);
                                    PdfTable.WidthPercentage = 100f;
                                    PdfPCell PdfPCell = null;
                                    string cadena1 = "";
                                    string cadena2 = "";
                                    string cadena3 = "";
                                    string cadena4 = "";
                                    List<string> registros = new List<string>();
                                    foreach (var concatenar in total)
                                    {
                                        //int residuo = Conteo % 27;
                                        if (Conteo < 22)
                                        {
                                            if (Conteo == 0)
                                            {
                                                cadena1 += "<ul style='margin-top:-20px'>";
                                            }
                                            cadena1 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                        }
                                        else
                                        {
                                            if (Conteo < 48)
                                            {
                                                //if (!etiqueta.Contains("Programa") && !concatenar.Contains("li>"))
                                                cadena2 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                            }
                                            else
                                            {
                                                if (Conteo < 75)
                                                {
                                                    //if (!etiqueta.Contains("Programa") && !concatenar.Contains("li>"))
                                                    cadena3 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                                }
                                                else
                                                {
                                                    //if (!etiqueta.Contains("Programa") && !concatenar.Contains("li>"))
                                                    cadena4 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                                }
                                            }
                                        }

                                        Conteo++;
                                    }

                                    htmltabla += "<table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>";
                                    contador = 0;
                                    for (int rows = 0; rows < 1; rows++)
                                    {
                                        htmltabla += "<tr style='vertical-align: text-top;'>";
                                        for (int column = 0; column < Cantidadcolumns; column++)
                                        {
                                            if (column == 0)
                                            {
                                                if (cadena1.TrimEnd(' ').EndsWith("</li>\n"))
                                                {
                                                    cadena1 = cadena1 + "</ul>";
                                                }
                                                htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena1}";
                                            }
                                            if (column == 1)
                                            {
                                                if (cadena2.StartsWith("<li>"))
                                                {
                                                    cadena2 = "<ul>" + cadena2;
                                                }
                                                if (cadena2.TrimEnd(' ').EndsWith("</li>\n"))
                                                {
                                                    cadena2 = cadena2 + "</ul>";
                                                }
                                                htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena2}";
                                            }
                                            if (column == 2)
                                            {
                                                if (cadena3.StartsWith("<li>"))
                                                {
                                                    cadena3 = "<ul>" + cadena3;
                                                }
                                                if (cadena3.TrimEnd(' ').EndsWith("</li>\n"))
                                                {
                                                    cadena3 = cadena3 + "</ul>";
                                                }
                                                htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena3}";
                                            }
                                            if (column == 3)
                                            {
                                                if (cadena4.StartsWith("<li>"))
                                                {
                                                    cadena4 = "<ul>" + cadena4;
                                                }
                                                if (cadena4.TrimEnd(' ').EndsWith("</li>\n"))
                                                {
                                                    cadena4 = cadena4 + "</ul>";
                                                }
                                                htmltabla += $@"<th style='vertical-align:top;text-align:left;font-weight: normal;'> {cadena4}";
                                            }

                                            contador = contador + 1;
                                            htmltabla += "</td>";
                                        }
                                        htmltabla += "</tr>";
                                    }
                                    htmltabla += "</table>";
                                    PdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table
                                    this.ContenidoCertificado.EstructuraCurricular = htmltabla;
                                    remplazo = remplazo.Replace(etiqueta, htmltabla);
                                }

                            }
                        }
                    }
                    var pdfPosterior = PdfGenerator.GeneratePdf(remplazo, config);
                    temmporal = ImportarPdfDocument(pdfPosterior);
                    page = temmporal.Pages[0];
                    pdf.AddPage(page);
                }

                //_document.Close();
                pdf.Save(ms, false);
                return ms.ToArray();
            }
        }
        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Genera estilos para el certificado.
        /// </summary> 
        private StyleSheet GenerateStyleCertificado()
        {
            FontFactory.RegisterDirectories();

            StyleSheet css = new StyleSheet();

            css.LoadTagStyle("h1", "size", "14pt");
            css.LoadTagStyle("h1", "style", "text-align:center;font-weight:bold;");
            css.LoadTagStyle("span", "style", "text-align:center;font-weight:bold;font-family:'Times New Roman'");
            css.LoadTagStyle("p", "style", "text-align:justify;");
            css.LoadTagStyle("ul", "style", "display:block;list-style-type:circle;");
            //css.LoadTagStyle(HtmlTags.TABLE, HtmlTags.BORDER, "0.1");
            css.LoadTagStyle(HtmlTags.DIV, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.DIV, HtmlTags.FONTSIZE, "14px");
            css.LoadTagStyle(HtmlTags.H1, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H2, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H2, HtmlTags.FONTSIZE, "14px");
            css.LoadTagStyle(HtmlTags.H3, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H3, HtmlTags.FONTSIZE, "14px");
            css.LoadTagStyle(HtmlTags.H4, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.H4, HtmlTags.FONTSIZE, "14px");
            css.LoadTagStyle(HtmlTags.P, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.SPAN, HtmlTags.FONTFAMILY, "Times New Roman");
            css.LoadTagStyle(HtmlTags.P, HtmlTags.FONTSIZE, "14px");
            return css;
        }
        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Genera estilos para certificado automatico.
        /// </summary> 
        private StyleSheet GenerateStyleCertificadoAutomatico(string tipoletra)
        {
            FontFactory.RegisterDirectories();

            StyleSheet css = new StyleSheet();

            css.LoadTagStyle("span", "style", "font-family:'" + tipoletra + "'");
            css.LoadTagStyle("ul", "style", "display:block;list-style-type:circle;");
            //css.LoadTagStyle(HtmlTags.TABLE, HtmlTags.BORDER, "0.1");
            css.LoadTagStyle(HtmlTags.DIV, HtmlTags.FONTFAMILY, tipoletra);
            css.LoadTagStyle(HtmlTags.H1, HtmlTags.FONTFAMILY, tipoletra);
            css.LoadTagStyle(HtmlTags.H2, HtmlTags.FONTFAMILY, tipoletra);
            css.LoadTagStyle(HtmlTags.H3, HtmlTags.FONTFAMILY, tipoletra);
            css.LoadTagStyle(HtmlTags.H4, HtmlTags.FONTFAMILY, tipoletra);
            css.LoadTagStyle(HtmlTags.P, HtmlTags.FONTFAMILY, tipoletra);
            css.LoadTagStyle(HtmlTags.SPAN, HtmlTags.FONTFAMILY, tipoletra);
            return css;
        }
        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Funcion para importar documento PDF.
        /// </summary> 
        public PdfSharp.Pdf.PdfDocument ImportarPdfDocument(PdfSharp.Pdf.PdfDocument pdf1)
        {
            using (var stream = new MemoryStream())
            {
                pdf1.Save(stream, false);
                stream.Position = 0;
                var result = PdfSharp.Pdf.IO.PdfReader.Open(stream, PdfSharp.Pdf.IO.PdfDocumentOpenMode.Import);
                return result;
            }
        }
        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Convierte imagen en Byte.
        /// </summary> 
        public static byte[] ImageToByte2(System.Drawing.Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Metodo que retorna lista de secciones de documento del programa general apuntando a la nueva version
        /// </summary>
        /// <param name="idPGeneral">Id del programa general del cual se desea obtener las secciones del documento (PK de la tabla pla.T_PGeneral)</param>
        /// <returns> List<ProgramaGeneralSeccionDocumentoDTO> </returns>
        public List<ProgramaGeneralSeccionDocumentoDTO> ObtenerListaSeccionDocumentoProgramaGeneral(int idPGeneral)
        {
            try
            {
                ProgramaGeneralDoumentoDTO programa = new ProgramaGeneralDoumentoDTO();

                var listaCursos = _repPgeneralAsubPgeneral.ObtenerPGeneralHijos(idPGeneral);
                if (listaCursos.Count > 0)
                {
                    programa.EsProgramaPadre = true;
                    var secciones = _repDocumentoSeccionPw.ObtenerSeccionDocumento(idPGeneral);
                    if (secciones != null && secciones.Count > 0)
                    {
                        programa.ListaSeccionesContenidosDocumento = secciones;
                    }
                    programa.ListaSeccionesContenidosDocumentoEstructura = new List<RegistroListaSeccionesDocumentoDTO>();

                    foreach (var item in listaCursos)
                    {
                        var seccionEstructura = _repDocumentoSeccionPw.ObtenerSeccionDocumentoEstructuraCurricular(item.Id);

                        if (seccionEstructura != null && seccionEstructura.Count > 0)
                        {
                            programa.ListaSeccionesContenidosDocumentoEstructura.AddRange(seccionEstructura);
                        }
                    }
                }
                else
                {
                    programa.EsProgramaPadre = false;

                    var secciones = _repDocumentoSeccionPw.ObtenerSeccionDocumento(idPGeneral);

                    if (secciones != null && secciones.Count > 0)
                    {
                        programa.ListaSeccionesContenidosDocumento = secciones;
                    }

                    programa.ListaSeccionesContenidosDocumentoEstructura = new List<RegistroListaSeccionesDocumentoDTO>();


                    var seccionEstructura = _repDocumentoSeccionPw.ObtenerSeccionDocumentoEstructuraCurricular(idPGeneral);

                    if (seccionEstructura != null && seccionEstructura.Count > 0)
                    {
                        programa.ListaSeccionesContenidosDocumentoEstructura.AddRange(seccionEstructura);
                    }
                }

                List<ProgramaGeneralEstructuraAgrupadoDTO> contenido = new List<ProgramaGeneralEstructuraAgrupadoDTO>();
                if (programa.ListaSeccionesContenidosDocumentoEstructura != null && programa.ListaSeccionesContenidosDocumentoEstructura.Count > 0)
                {
                    if (programa.EsProgramaPadre)
                    {
                        var listaCursosSecciones = programa.ListaSeccionesContenidosDocumentoEstructura.GroupBy(x => new { x.IdPGeneral, x.NombreCurso, x.Titulo }).Select(x => new { x.Key.IdPGeneral, x.Key.NombreCurso, x.Key.Titulo }).ToList();
                        foreach (var itemCurso in listaCursosSecciones)
                        {
                            var listaCapitulosSecciones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(x => x.IdSeccionTipoDetalle_PW == 12 && x.IdPGeneral == itemCurso.IdPGeneral).GroupBy(x => new { x.Contenido, x.IdSeccionTipoDetalle_PW, x.IdPGeneral, x.Cabecera, x.PiePagina }).ToList();
                            ProgramaGeneralEstructuraAgrupadoDTO obj = new ProgramaGeneralEstructuraAgrupadoDTO();
                            obj.Seccion = itemCurso.Titulo;
                            obj.Titulo = itemCurso.NombreCurso;
                            obj.DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>();
                            foreach (var itemSecion in listaCapitulosSecciones)
                            {
                                ProgramaGeneralEstructuraDetalleDTO tmp = new ProgramaGeneralEstructuraDetalleDTO()
                                {
                                    Contenido = itemSecion.Key.Contenido,
                                    Cabecera = itemSecion.Key.Cabecera,
                                    PiePagina = itemSecion.Key.PiePagina
                                };
                                obj.DetalleContenido.Add(tmp);

                            }
                            contenido.Add(obj);
                        }
                    }
                    else
                    {
                        var listaCapitulosSecciones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(x => x.IdSeccionTipoDetalle_PW == 12).GroupBy(x => new { x.Contenido, x.IdSeccionTipoDetalle_PW, x.Titulo }).ToList();
                        foreach (var item in listaCapitulosSecciones)
                        {
                            ProgramaGeneralEstructuraAgrupadoDTO obj = new ProgramaGeneralEstructuraAgrupadoDTO();
                            var listaSesiones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(s => s.Contenido == item.Key.Contenido).Select(x => x.NumeroFila).ToList();
                            var capituloSesiones = programa.ListaSeccionesContenidosDocumentoEstructura.Where(s => s.IdSeccionTipoDetalle_PW == 13 && listaSesiones.Contains(s.NumeroFila)).GroupBy(x => new { x.Contenido, x.NombreCurso, x.Cabecera, x.PiePagina }).ToList();
                            obj.Seccion = item.Key.Titulo;
                            obj.Titulo = item.Key.Contenido;
                            obj.DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>();

                            if (capituloSesiones != null && capituloSesiones.Count > 0)
                            {
                                foreach (var itemSecion in capituloSesiones)
                                {
                                    ProgramaGeneralEstructuraDetalleDTO tmp = new ProgramaGeneralEstructuraDetalleDTO()
                                    {
                                        Contenido = itemSecion.Key.Contenido,
                                        Cabecera = itemSecion.Key.Cabecera,
                                        PiePagina = itemSecion.Key.PiePagina
                                    };
                                    obj.DetalleContenido.Add(tmp);
                                }
                                contenido.Add(obj);
                            }
                            else
                            {
                                foreach (var itemSecion in capituloSesiones)
                                {
                                    ProgramaGeneralEstructuraDetalleDTO tmp = new ProgramaGeneralEstructuraDetalleDTO()
                                    {
                                        Contenido = item.Key.Contenido
                                    };
                                    obj.DetalleContenido.Add(tmp);
                                }
                                contenido.Add(obj);
                            }
                        }
                    }
                }
                if (programa.ListaSeccionesContenidosDocumento != null && programa.ListaSeccionesContenidosDocumento.Count > 0)
                {
                    var listaCursosSecciones = programa.ListaSeccionesContenidosDocumento.GroupBy(x => new { x.IdSeccionTipoDetalle_PW, x.Titulo }).Select(x => new { x.Key.IdSeccionTipoDetalle_PW, x.Key.Titulo }).ToList();
                    foreach (var itemCurso in listaCursosSecciones)
                    {
                        var listaCapitulosSecciones = programa.ListaSeccionesContenidosDocumento.Where(x => x.IdSeccionTipoDetalle_PW == itemCurso.IdSeccionTipoDetalle_PW).GroupBy(x => new { x.Contenido, x.Cabecera, x.PiePagina }).ToList();
                        ProgramaGeneralEstructuraAgrupadoDTO obj = new ProgramaGeneralEstructuraAgrupadoDTO();
                        obj.Seccion = itemCurso.Titulo;
                        obj.Titulo = "";//itemCurso.Titulo;
                        obj.DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>();
                        foreach (var itemSecion in listaCapitulosSecciones)
                        {
                            ProgramaGeneralEstructuraDetalleDTO tmp = new ProgramaGeneralEstructuraDetalleDTO()
                            {
                                Contenido = itemSecion.Key.Contenido,
                                Cabecera = itemSecion.Key.Cabecera,
                                PiePagina = itemSecion.Key.PiePagina
                            };
                            obj.DetalleContenido.Add(tmp);
                        }
                        contenido.Add(obj);
                    }
                }

                var listaProgramaGeneralDocumentoSeccion = contenido.GroupBy(x => x.Seccion).Select(x => new ProgramaGeneralSeccionDocumentoDTO
                {
                    Seccion = x.Key,
                    DetalleSeccion = x.GroupBy(y => new { y.Titulo, y.DetalleContenido }).Select(y => new ProgramaGeneralSeccionDocumentoDetalleDTO
                    {
                        Titulo = y.Key.Titulo,
                        Cabecera = y.Key.DetalleContenido.GroupBy(z => z.Cabecera).Select(z => z.Key).FirstOrDefault(),
                        PiePagina = y.Key.DetalleContenido.GroupBy(z => z.PiePagina).Select(z => z.Key).FirstOrDefault(),
                        DetalleContenido = y.Key.DetalleContenido.Select(z => z.Contenido).ToList()
                    }).ToList()
                }).ToList();

                return listaProgramaGeneralDocumentoSeccion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public byte[] GenerarVistaModeloCertificado(int IdPlantillaF, int IdPlantillaP, int IdPgeneral)
        {

            MatriculaCabeceraDatosCertificadoRepositorio repMatriculaCertificado = new MatriculaCabeceraDatosCertificadoRepositorio();
            PgeneralRepositorio _repPGeneral = new PgeneralRepositorio();
            PespecificoRepositorio _repPEspecifico = new PespecificoRepositorio();
            var Pgeneral = _repPgeneral.FirstById(IdPgeneral);

            ContenidoCertificado = new CertificadoGeneradoAutomaticoContenidoBO();

            var listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();

            var plantilla = _repPlantilla.FirstById(IdPlantillaF);

            var IdPlantillaBase = plantilla.IdPlantillaBase;

            var plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantillaF);

            var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

            foreach (var etiqueta in listaEtiqueta)
            {
                listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = string.Concat("{", etiqueta, "}"), texto = "" });
            }
            List<string> sepacion = new List<string>();
            string FondoFrontalCertificado = "";
            string FondoReversoCertificado = "";
            string FondoFrontalConstancia = "";
            string FondoReversoConstancia = "";
            sepacion = plantillaBase.Cuerpo.Split("###").ToList();
            Dictionary<string, object> map = new Dictionary<string, object>();
            map["font_factory"] = new MyFontFactory();
            //map.put("font_factory", new MyFontFactory());

            using (MemoryStream ms = new MemoryStream())
            {
                PdfSharp.Pdf.PdfDocument pdf = new PdfSharp.Pdf.PdfDocument();
                var config = new PdfGenerateConfig();
                if (IdPlantillaBase == 12)/*Certificado*/
                {
                    config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                    config.PageSize = PdfSharp.PageSize.A4;
                    config.MarginTop = 144;
                    config.MarginBottom = 30;
                    config.MarginLeft = 111;
                    config.MarginRight = 83;

                }
                else
                {
                    config.PageSize = PdfSharp.PageSize.A4;
                    config.MarginLeft = 60;
                    config.MarginRight = 60;
                    config.MarginTop = 60;
                    config.MarginBottom = 25;

                }
                int Conteo = 0;
                List<string> estructura = new List<string>();



                string remplazo = "";
                foreach (var item in sepacion)
                {
                    string direccionCodigoQR = "";
                    string tipoletra = "Times New Roman";
                    remplazo = item;
                    string searchString = "/*";
                    int startIndex = remplazo.IndexOf(searchString);
                    searchString = "*/";
                    int endIndex = 0;
                    string substring = "";

                    if (startIndex != -1)
                    {
                        endIndex = remplazo.IndexOf(searchString, startIndex);
                        substring = remplazo.Substring(startIndex + searchString.Length, endIndex - searchString.Length);
                    }
                    if (substring != null && substring != "")
                    {
                        tipoletra = substring;
                    }
                    else
                    {
                        searchString = "text-align:";
                        startIndex = remplazo.IndexOf(searchString);
                        searchString = ";";

                        if (startIndex != -1)
                        {
                            endIndex = remplazo.IndexOf(searchString, startIndex);
                            substring = remplazo.Substring(startIndex + searchString.Length, endIndex + searchString.Length - startIndex);
                        }
                        if (substring != null && substring != "")
                        {
                            tipoletra = substring;
                        }
                    }
                    if (item.Contains("repositorioweb"))
                    {
                        if (IdPlantillaBase == 12)
                        {
                            FondoFrontalCertificado = item;
                        }
                        else
                        {
                            FondoFrontalConstancia = item;
                        }

                    }
                    if (item.Contains("acute"))
                    {
                        remplazo = remplazo.Replace("&Eacute;", "É").Replace("&Aacute;", "Á").Replace("&Iacute;", "Í").Replace("&Oacute;", "Ó").Replace("&Uacute;", "Ú");
                    }
                    if (item.Contains("CODIGOQR"))
                    {
                        var url = _repCertificadoGeneradoAutomatico.ObtenerCorrelativoCertificado().ToString();

                        var urlCodigo = "https://bsginstitute.com/informacionCertificado?cod=" + 0 + "." + url;
                        QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                        QRCodeData qrDatos = qRCodeGenerator.CreateQrCode(urlCodigo, QRCodeGenerator.ECCLevel.Q);
                        QRCode qRCodigo = new QRCode(qrDatos);

                        System.Drawing.Bitmap qrImagen = qRCodigo.GetGraphic(7, System.Drawing.Color.Black, System.Drawing.Color.White, false);
                        System.Drawing.Image objImage = (System.Drawing.Image)qrImagen;

                        //var a = System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory);
                        //qrImagen.Save(System.AppDomain.CurrentDomain.BaseDirectory + "codigo.png", System.Drawing.Imaging.ImageFormat.Png);

                        direccionCodigoQR = _repAlumno.guardarArchivosQR(ImageToByte2(objImage), "image/jpeg", url);
                        this.ContenidoCertificado.CodigoQr = direccionCodigoQR;
                        remplazo = remplazo.Replace("CODIGOQR", $@"<img style='vertical-align:baseline' src='{direccionCodigoQR}' width=55 height=55  />");

                    }
                    if (item.Contains("{tPLA_PGeneral.Nombre}"))
                    {
                        remplazo = remplazo.Replace("{tPLA_PGeneral.Nombre}", Pgeneral.Nombre);
                    }
                    if (item.Contains("{T_Pgeneral.CodigoPartner}"))
                    {
                        remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", "BSGI2022");
                       
                    }
                    //reemplazar nombre 1 alumno
                    if (item.Contains("{T_Alumno.NombreCompleto}"))
                    {
                    }
                    if (item.Contains("{tAlumnos.nombre1}"))
                    {
                        remplazo = remplazo.Replace("{tAlumnos.nombre1}", "Fernando");

                        if (item.Contains("{tAlumnos.nombre2}"))
                        {
                            remplazo = remplazo.Replace("{tAlumnos.nombre2}", "Miguel");
                            //_document.Add(new iTextSharp.text.Phrase(" " + alumno.Nombre2.ToUpper(), fuente));
                        }
                        if (item.Contains("{tAlumnos.apepaterno}"))
                        {
                            remplazo = remplazo.Replace("{tAlumnos.apepaterno}", "Rodriguez");
                            //_document.Add(new iTextSharp.text.Phrase(" " + alumno.ApellidoPaterno.ToUpper(), fuente));
                        }
                        if (item.Contains("{tAlumnos.apematerno}"))
                        {
                            remplazo = remplazo.Replace("{tAlumnos.apematerno}", "Delgado");
                            //_document.Add(new iTextSharp.text.Phrase(" " + alumno.ApellidoMaterno.ToUpper(), fuente));
                        }

                        //if (IdPlantillaBase == 12)
                        //{
                        //    List<IElement> objects = HTMLWorker.ParseToList(new StringReader(remplazo), GenerateStyleCertificadoAutomatico(tipoletra), map);
                        //    foreach (IElement element in objects)
                        //    {
                        //        _document.Add(element);
                        //    }
                        //}
                        //else
                        //{
                        //    htmlparser.SetStyleSheet(GenerateStyleCertificadoAutomatico(tipoletra));
                        //    htmlparser.Parse(new StringReader(remplazo));
                        //}                                             
                    }
                    if (item.Contains("{tpla_pgeneral.pw_duracion}"))
                    {
                        remplazo = remplazo.Replace("{tpla_pgeneral.pw_duracion}", "100 horas");
                    }
                    if (item.Contains("{tCentroCosto.centrocosto}"))
                    {
                        remplazo = remplazo.Replace("{tCentroCosto.centrocosto}", "(centro costo)");
                    }
                    if (item.Contains("{tPEspecifico.ciudad}"))
                    {
                        remplazo = remplazo.Replace("{tPEspecifico.ciudad}", "Lima");
                    }
                    if (item.Contains("{tPEspecifico.EscalaCalificacion}"))
                    {
                        remplazo = remplazo.Replace("{tPEspecifico.EscalaCalificacion}", "(escala calificacion)");
                    }
                    //reemplazar Fecha Inicio Capacitacion
                    if (item.Contains("{T_Alumno.FechaInicioCapacitacion}"))
                    {
                        remplazo = remplazo.Replace("{T_Alumno.FechaInicioCapacitacion}", repMatriculaCertificado.TranformarFechaEnCadena(DateTime.Now));
                    }

                    //reemplazar Fecha Fin Capacitacion
                    if (item.Contains("{T_Alumno.FechaFinCapacitacion}"))
                    {
                        remplazo = remplazo.Replace("{T_Alumno.FechaFinCapacitacion}", repMatriculaCertificado.TranformarFechaEnCadena(DateTime.Now));
                    }
                    //reemplazar Calificacion Promedio
                    if (item.Contains("{T_Alumno.CalificacionPromedio}"))
                    {
                        
                        remplazo = remplazo.Replace("{T_Alumno.CalificacionPromedio}", "10.00");
                    }
                    //reemplazar Fecha Emision Certificado
                    if (item.Contains("{T_Alumno.FechaEmisionCertificado}"))
                    {
                        var fechaEmision = _repAlumno.ObtenerFechaEmision();
                        this.ContenidoCertificado.FechaEmisionCertificado = fechaEmision;
                        remplazo = remplazo.Replace("{T_Alumno.FechaEmisionCertificado}", fechaEmision);
                    }

                    //reemplazar Fecha Codigo Certificado
                    if (item.Contains("{T_Alumno.CodigoCertificado}"))
                    {
                        remplazo = remplazo.Replace("{T_Alumno.CodigoCertificado}", "ALUMNO2022");
                    }

                    //reemplazar duracion en horas de Programa Especifico
                    if (item.Contains("{tPEspecifico.duracion}"))
                    {
                        remplazo = remplazo.Replace("{tPEspecifico.duracion}", "100");
                    }
                    /*reemplazar Codigo de matricula del alumno */
                    if (item.Contains("{T_MatriculaCabecera.CodigoMatricula}"))
                    {
                        remplazo = remplazo.Replace("{T_MatriculaCabecera.CodigoMatricula}", "111111A22222");
                    }
                    /*reemplazar Codigo de matricula del alumno */
                    if (item.Contains("{T_Alumno.CorrelativoConstancia}"))
                    {
                        var correlativoConstancia = _repCertificadoGeneradoAutomatico.ObtenerCorrelativoCertificado();
                        this.ContenidoCertificado.CorrelativoConstancia = correlativoConstancia;
                        remplazo = remplazo.Replace("{T_Alumno.CorrelativoConstancia}", correlativoConstancia.ToString());
                    }
                    /*reemplazar Cronograma de notas del alumno por matricula*/
                    if (item.Contains("{T_Alumno.CronogramaNotas}"))
                    {
                        string tablaNota = "";
                        List<CronogramaNotaDTO> cronogramaNota = new List<CronogramaNotaDTO>();
                        cronogramaNota.Add(new CronogramaNotaDTO(){ Curso="curso 1", Nota=20,Estado="Aprovado"});
                        cronogramaNota.Add(new CronogramaNotaDTO() { Curso = "curso 2", Nota = 20, Estado = "Aprovado" });
                        cronogramaNota.Add(new CronogramaNotaDTO() { Curso = "curso 3", Nota = 20, Estado = "Aprovado" });
                        if (cronogramaNota == null || cronogramaNota.Count == 0)
                        {
                            throw new Exception("No se Puede Calcular cronogramaNota");
                        }
                        tablaNota += "<table  style='font-size:11px; border:1px solid #575656;font-family:Arial;color:#575656;border-collapse: collapse;width:100%;' cellspacing='3' cellpadding='3'>";
                        tablaNota += $@"<tr style='border:1px solid #575656;border-collapse: collapse;'>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> CURSO </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> NOTA </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> ESTADO </td>
                                            </tr>";
                        int contador = 0;
                        for (int rows = 0; rows < cronogramaNota.Count; rows++)
                        {
                            tablaNota += "<tr style='border:1px solid #575656;border-collapse: collapse;'>";
                            tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;;text-align:left'> {cronogramaNota[rows].Curso}</td>";
                            tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:center'> {cronogramaNota[rows].Nota}</td>";
                            tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:center'> {cronogramaNota[rows].Estado}</td>";

                            contador = contador + 1;
                            tablaNota += "</tr>";
                        }
                        tablaNota += "</table>";
                        this.ContenidoCertificado.CronogramaNota = tablaNota;
                        remplazo = remplazo.Replace("{T_Alumno.CronogramaNotas}", tablaNota);
                    }
                    /*reemplazar Cronograma de Asistencia del alumno por matricula*/
                    if (item.Contains("{T_Alumno.CronogramaAsistencia}"))
                    {
                        string tablaAsistencia = "";
                        List<CronogramaAsistenciaDTO> cronogramaAsistencia = new List<CronogramaAsistenciaDTO>();
                        cronogramaAsistencia.Add(new CronogramaAsistenciaDTO() { Curso = "curso 1", PorcentajeAsistencia="90%" });
                        cronogramaAsistencia.Add(new CronogramaAsistenciaDTO() { Curso = "curso 2", PorcentajeAsistencia = "90%" });
                        cronogramaAsistencia.Add(new CronogramaAsistenciaDTO() { Curso = "curso 3", PorcentajeAsistencia = "90%" });
                        if (cronogramaAsistencia == null || cronogramaAsistencia.Count == 0)
                        {
                            throw new Exception("No se Puede Calcular CronogramaAsistencia");
                        }
                        tablaAsistencia += "<table  style='font-size:11px; border:1px solid #575656;font-family:Arial;color:#575656;border-collapse: collapse;width:100%;' cellspacing='3' cellpadding='3'>";
                        tablaAsistencia += $@"<tr style='border:1px solid #575656;border-collapse: collapse;'>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> CURSO </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> ASISTENCIA </td>
                                            </tr>";
                        int contador = 0;
                        for (int rows = 0; rows < cronogramaAsistencia.Count; rows++)
                        {
                            tablaAsistencia += "<tr style='border:1px solid #575656;border-collapse: collapse;'>";
                            tablaAsistencia += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:left;'> {cronogramaAsistencia[rows].Curso}</td>";
                            tablaAsistencia += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:Center;'> {cronogramaAsistencia[rows].PorcentajeAsistencia}</td>";

                            contador = contador + 1;
                            tablaAsistencia += "</tr>";
                        }
                        tablaAsistencia += "</table>";
                        this.ContenidoCertificado.CronogramaAsistencia = tablaAsistencia;
                        remplazo = remplazo.Replace("{T_Alumno.CronogramaAsistencia}", tablaAsistencia);
                    }
                }
                var pdfFrontal = PdfGenerator.GeneratePdf(remplazo, config);
                var temmporal = ImportarPdfDocument(pdfFrontal);
                PdfSharp.Pdf.PdfPage page = temmporal.Pages[0];
                pdf.AddPage(page);

                PdfSharp.Drawing.XGraphics gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(pdf.Pages[0], PdfSharp.Drawing.XGraphicsPdfPageOptions.Prepend);

                if (IdPlantillaBase == 12)
                {
                    System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(FondoFrontalCertificado);
                    webRequest.AllowWriteStreamBuffering = true;
                    System.Net.WebResponse webResponse = webRequest.GetResponse();
                    PdfSharp.Drawing.XImage xImage = PdfSharp.Drawing.XImage.FromStream(webResponse.GetResponseStream());
                    gfx.DrawImage(xImage, 0, 0, 843, 595);
                }
                else
                {
                    System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(FondoFrontalConstancia);
                    webRequest.AllowWriteStreamBuffering = true;
                    System.Net.WebResponse webResponse = webRequest.GetResponse();
                    PdfSharp.Drawing.XImage xImage = PdfSharp.Drawing.XImage.FromStream(webResponse.GetResponseStream());
                    gfx.DrawImage(xImage, 0, 0, 595, 843);
                }

              

                if (IdPlantillaP != 0 && IdPlantillaP != null)
                {
                    config = new PdfGenerateConfig();
                    if (IdPlantillaBase == 12)/*Certificado*/
                    {
                        config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                        config.PageSize = PdfSharp.PageSize.A4;
                        config.MarginLeft = 257;
                        config.MarginRight = 83;
                        config.MarginTop = 49;
                        config.MarginBottom = 0;

                    }
                    string _estructuraCurricular = "";
                    listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();
                    var plantillaP = _repPlantilla.FirstById(IdPlantillaP);
                    var plantillaBaseP = _repPlantilla.ObtenerPlantillaCorreo(IdPlantillaP);

                    var listaEtiquetaP = plantillaBaseP.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

                    foreach (var etiqueta in listaEtiquetaP)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = string.Concat("{", etiqueta, "}"), texto = "" });
                    }
                    sepacion = new List<string>();

                    sepacion = plantillaBaseP.Cuerpo.Split("###").ToList();

                    foreach (var item in sepacion)
                    {
                        remplazo = item;

                        string tipoletra = "Times New Roman";

                        string searchString = "/*";
                        int startIndex = remplazo.IndexOf(searchString);
                        searchString = "*/";
                        int endIndex = 0;
                        string substring = "";
                        if (startIndex != -1)
                        {
                            endIndex = remplazo.IndexOf(searchString, startIndex);
                            substring = remplazo.Substring(startIndex + searchString.Length, endIndex + searchString.Length - startIndex);
                        }
                        if (substring != null && substring != "")
                        {
                            tipoletra = substring;
                        }
                        else
                        {
                            searchString = "text-align:";
                            startIndex = remplazo.IndexOf(searchString);
                            searchString = ";";

                            if (startIndex != -1)
                            {
                                endIndex = remplazo.IndexOf(searchString, startIndex);
                                substring = remplazo.Substring(startIndex + searchString.Length, endIndex + searchString.Length - startIndex);
                            }
                            if (substring != null && substring != "")
                            {
                                tipoletra = substring;
                            }
                        }
                        if (item.Contains("acute"))
                        {
                            remplazo = remplazo.Replace("&Eacute;", "É").Replace("&Aacute;", "Á").Replace("&Iacute;", "Í").Replace("&Oacute;", "Ó").Replace("&Uacute;", "Ú");
                        }
                        if (item.Contains("repositorioweb"))
                        {
                            if (IdPlantillaBase == 12)
                            {
                                FondoReversoCertificado = item;
                            }
                            else
                            {
                                FondoReversoConstancia = item;
                            }

                        }

                        if (item.Contains("{tPLA_PGeneral.Nombre}"))
                        {
                            remplazo = remplazo.Replace("{tPLA_PGeneral.Nombre}", Pgeneral.Nombre);
                        }
                        if (item.Contains("{T_Pgeneral.CodigoPartner}"))
                        {
                            remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", "BSGI2022");
                        }
                        if (item.Contains("{tCentroCosto.centrocosto}"))
                        {
                            remplazo = remplazo.Replace("{tCentroCosto.centrocosto}", "-(centro cosoto)-");
                        }
                        if (item.Contains("{tPEspecifico.ciudad}"))
                        {
                            remplazo = remplazo.Replace("{tPEspecifico.ciudad}", "Lima");
                        }
                        //reemplazar nombre 1 alumno
                        if (item.Contains("{T_Alumno.NombreCompleto}"))
                        {

                        }
                        if (item.Contains("{tAlumnos.nombre1}"))
                        {
                            remplazo = remplazo.Replace("{tAlumnos.nombre1}", "Fernando");

                            if (item.Contains("{tAlumnos.nombre2}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.nombre2}", "Miguel");
                            }
                            if (item.Contains("{tAlumnos.apematerno}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.apematerno}", "Rodriguez");
                            }
                            if (item.Contains("{tAlumnos.apepaterno}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.apepaterno}", "Delgado");
                            }

                        }

                        if (item.Contains("{tpla_pgeneral.pw_duracion}"))
                        {
                            remplazo = remplazo.Replace("{tpla_pgeneral.pw_duracion}", "100 horas");
                        }
                        //reemplazar Fecha Inicio Capacitacion
                        if (item.Contains("{T_Alumno.FechaInicioCapacitacion}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaInicioCapacitacion}", repMatriculaCertificado.TranformarFechaEnCadena(DateTime.Now));
                        }

                        //reemplazar Fecha Fin Capacitacion
                        if (item.Contains("{T_Alumno.FechaFinCapacitacion}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaFinCapacitacion}", repMatriculaCertificado.TranformarFechaEnCadena(DateTime.Now));
                        }
                        //reemplazar Calificacion Promedio
                        if (item.Contains("{T_Alumno.CalificacionPromedio}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.CalificacionPromedio}", "10.01");
                        }
                        //reemplazar Fecha Emision Certificado
                        if (item.Contains("{T_Alumno.FechaEmisionCertificado}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaEmisionCertificado}", _repAlumno.ObtenerFechaEmision());
                        }

                        //reemplazar Fecha Codigo Certificado
                        if (item.Contains("{T_Alumno.CodigoCertificado}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.CodigoCertificado}", "ALUMNO2022");
                        }

                        //reemplazar duracion en horas de Programa Especifico
                        if (item.Contains("{tPEspecifico.duracion}"))
                        {
                            remplazo = remplazo.Replace("{tPEspecifico.duracion}", "100");
                        }
                        if (item.Contains("{T_Pgeneral.EstructuraCurricular}"))
                        {
                            
                            var estructuraCurso = _repDocumentoSeccionPw.ObtenerEstructuraCurso(Pgeneral.Id);
                            if (estructuraCurso.Count > 0)
                            {
                                string listaEstructura = $@"
                                                    <table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>
                                                    <tr style='vertical-align: text-top;'>
                                                    <td style = 'vertical-align: text-top;text-align:left;font-weight: normal;' > ";
                                listaEstructura += "<ul style='margin-top:-20px'>";

                                foreach (var capitulo in estructuraCurso)
                                {
                                    listaEstructura += $@"<li style='padding-bottom:5px'>{capitulo.Contenido.Replace(" - COLOMBIA", "")} </li>";
                                }

                                listaEstructura += "</ul></td></tr></table>";
                                _estructuraCurricular = listaEstructura;
                                this.ContenidoCertificado.EstructuraCurricular = listaEstructura;
                                remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", listaEstructura);
                            }
                            else
                            {
                                remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", "");
                            }

                        }
                        if (item.Contains("Template"))
                        {

                            var etiq = listaObjetoWhasApp.Where(x => x.codigo.Contains("Template")).ToList();
                            SeccionEtiquetaDTO valor = new SeccionEtiquetaDTO();
                            string etiqueta = "";

                            foreach (var a in etiq)
                            {
                                List<string> ListaPalabras = new List<string>();
                                char[] delimitador = new char[] { '.' };
                                string IdPlantilla = "";
                                string IdColumna = "";
                                string[] array = a.codigo.Split(delimitador, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string s in array)
                                {
                                    ListaPalabras.Add(s);
                                }
                                IdPlantilla = ListaPalabras[3].ToString();
                                IdColumna = ListaPalabras[4].ToString();


                                var prevalor = _repPespecifico.ObtenerContenidoTemplate(new Guid(IdPlantilla), new Guid(IdColumna.Replace("}", "")), _repPEspecifico.FirstBy(x=>x.IdProgramaGeneral==IdPgeneral).IdCentroCosto ?? default(int));

                                if (prevalor != null && _estructuraCurricular == "")
                                {
                                    etiqueta = a.codigo;
                                    valor = prevalor;
                                }
                                else
                                {
                                    remplazo = remplazo.Replace(a.codigo, "");
                                }

                            }
                            if (etiqueta != "")
                            {
                                var tratamiento = valor.Valor.Replace("<p>", "<p id='estructura'<p>");
                                tratamiento = tratamiento.Replace("<li>", "<li id='liseparar'<li>");
                                tratamiento = tratamiento.Replace("&bull", "<li id='liseparar'");
                                estructura = tratamiento.Split("<p id='estructura'").Where(x => x.StartsWith("<p><strong>")).ToList();
                                List<string> todo = new List<string>();
                                int contador = 0;
                                string htmltabla = "";
                                List<string> total = new List<string>();
                                foreach (var li in estructura)
                                {
                                    foreach (var li1 in li.Split("<li id='liseparar'").ToList())
                                    {
                                        if (li1.Contains("<p>"))
                                            total.Add(li1.Replace("p>", "li>").Replace("<li>", "<li style='padding-bottom:5px'>").Replace("strong", "span").Replace("<ul>", "").Replace("</ul>", "").Replace("\"", "'").Replace("<div class='slide'>", "").Replace("<p style='padding-left:30px;'>", ""));

                                    }

                                }
                                int Cantidadcolumns = total.Count / 25;
                                int residuo = total.Count % 25;
                                if (residuo != 0)
                                {
                                    Cantidadcolumns++;
                                }
                                int reparticion = Cantidadcolumns;

                                PdfPTable PdfTable = new PdfPTable(Cantidadcolumns);
                                PdfTable.WidthPercentage = 100f;
                                PdfPCell PdfPCell = null;
                                string cadena1 = "";
                                string cadena2 = "";
                                string cadena3 = "";
                                string cadena4 = "";
                                List<string> registros = new List<string>();
                                foreach (var concatenar in total)
                                {
                                    if (Conteo < 22)
                                    {
                                        if (Conteo == 0)
                                        {
                                            cadena1 += "<ul style='margin-top:-20px'>";
                                        }
                                        cadena1 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                    }
                                    else
                                    {
                                        if (Conteo < 48)
                                        {
                                            cadena2 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                        }
                                        else
                                        {
                                            if (Conteo < 75)
                                            {
                                                cadena3 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                            }
                                            else
                                            {
                                                cadena4 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                            }
                                        }
                                    }

                                    Conteo++;
                                }

                                htmltabla += "<table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>";
                                contador = 0;
                                for (int rows = 0; rows < 1; rows++)
                                {
                                    htmltabla += "<tr style='vertical-align: text-top;'>";
                                    for (int column = 0; column < Cantidadcolumns; column++)
                                    {
                                        if (column == 0)
                                        {
                                            if (cadena1.TrimEnd(' ').EndsWith("</li>\n"))
                                            {
                                                cadena1 = cadena1 + "</ul>";
                                            }
                                            htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena1}";
                                        }
                                        if (column == 1)
                                        {
                                            if (cadena2.StartsWith("<li>"))
                                            {
                                                cadena2 = "<ul>" + cadena2;
                                            }
                                            if (cadena2.TrimEnd(' ').EndsWith("</li>\n"))
                                            {
                                                cadena2 = cadena2 + "</ul>";
                                            }
                                            htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena2}";
                                        }
                                        if (column == 2)
                                        {
                                            if (cadena3.StartsWith("<li>"))
                                            {
                                                cadena3 = "<ul>" + cadena3;
                                            }
                                            if (cadena3.TrimEnd(' ').EndsWith("</li>\n"))
                                            {
                                                cadena3 = cadena3 + "</ul>";
                                            }
                                            htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena3}";
                                        }
                                        if (column == 3)
                                        {
                                            if (cadena4.StartsWith("<li>"))
                                            {
                                                cadena4 = "<ul>" + cadena4;
                                            }
                                            if (cadena4.TrimEnd(' ').EndsWith("</li>\n"))
                                            {
                                                cadena4 = cadena4 + "</ul>";
                                            }
                                            htmltabla += $@"<th style='vertical-align:top;text-align:left;font-weight: normal;'> {cadena4}";
                                        }


                                        //List list = new List();
                                        ////for (int i = 0; i < estructura[contador].Split("<li id='liseparar'").ToList().Count(); i++)
                                        ////{
                                        //var lista = estructura[contador].Split("<li id='liseparar'").ToList();

                                        //foreach (var li in lista)
                                        //{
                                        //    string cadenaSinTags = Regex.Replace(li, "<.*?>", string.Empty);
                                        //    list.Add(new ListItem(cadenaSinTags));
                                        //}
                                        ////}
                                        ////var parse = new Phrase();
                                        ////parse.Add(list);
                                        ////PdfPCell = new PdfPCell();
                                        ////PdfPCell.AddElement(parse);
                                        ////PdfTable.AddCell(PdfPCell);
                                        contador = contador + 1;
                                        htmltabla += "</td>";
                                    }
                                    htmltabla += "</tr>";
                                }
                                htmltabla += "</table>";
                                PdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table

                                //doc.Add(paragraph);// add paragraph to the document
                                //_document.Add(PdfTable); // add pdf table to the document

                                this.ContenidoCertificado.EstructuraCurricular = htmltabla;
                                remplazo = remplazo.Replace(etiqueta, htmltabla);
                            }
                        }
                        //htmltemplate.SetStyleSheet(GenerateStyleCertificadoAutomatico(tipoletra));
                        //htmltemplate.Parse(new StringReader(remplazo));

                    }
                    var pdfPosterior = PdfGenerator.GeneratePdf(remplazo, config);
                    temmporal = ImportarPdfDocument(pdfPosterior);
                    page = temmporal.Pages[0];
                    pdf.AddPage(page);

                    gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(pdf.Pages[1], PdfSharp.Drawing.XGraphicsPdfPageOptions.Prepend);

                    System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(FondoReversoCertificado); ;
                    webRequest.AllowWriteStreamBuffering = true;
                    System.Net.WebResponse webResponse = webRequest.GetResponse();
                    PdfSharp.Drawing.XImage xImage = PdfSharp.Drawing.XImage.FromStream(webResponse.GetResponseStream());
                    gfx.DrawImage(xImage, 0, 0, 843, 595);
                }

                //_document.Close();
                pdf.Save(ms, false);
                return ms.ToArray();
            }

        }


        /// Autor: Jose Villena
        /// Fecha: 03/05/2021
        /// Version: 1.0
        /// <summary>
        /// Metodo que retorna lista de secciones de documento del programa general apuntando a la nueva version
        /// </summary>
        /// <param name="idPGeneral"> Id Programa General </param>	
        /// <returns>Lista de secciones de documento: List<ProgramaGeneralSeccionDocumentoDTO> </returns> 
        public List<ProgramaGeneralSeccionDocumentoDTO> ObtenerInformacionProgramaGeneral(int idPGeneral)
        {
            try
            {
                DocumentosBO documento = new DocumentosBO();
                var listaPrimera = documento.ObtenerListaSeccionDocumentoProgramaGeneral(idPGeneral);

                var seccionesv2 = _repDocumentoSeccionPw.ObtenerDatosComplementariosProgramaGeneralV2(idPGeneral);
                var seccionesv1 = _repDocumentoSeccionPw.ObtenerDatosComplementariosProgramaGeneralV1(idPGeneral);

                ProgramaGeneralDoumentoDTO programa = new ProgramaGeneralDoumentoDTO();
                ProgramaGeneralDoumentoDTO programav2 = new ProgramaGeneralDoumentoDTO();

                List<RegistroListaSeccionesDocumentoDTO> seccionResultado = new List<RegistroListaSeccionesDocumentoDTO>();
                string[] listaTituloV1 = { "estructura curricular", "beneficios", "pre-requisitos", "certificación", "duración y horarios", "evaluación", "bibliografía", "material del curso", "pautas complementarias", "descripci&#243;n certificacion", "descripci&#243;n estructura", "objetivos", "presentación", "público objetivo", "metodología online de este programa" };
                string[] listaTituloV2 = { "estructura curricular", "beneficios", "prerrequisitos", "certificacion", "duracion y horarios", "evaluacion", "bibliografia", "material del curso", "pautas complementarias", "descripci&#243;n certificacion", "descripci&#243;n estructura", "objetivos", "presentacion", "publico objetivo", "metodolog&#237;a online de este programa" };

                for (var i = 0; i < listaTituloV2.Length; i++)
                {
                    var secEstructuraCurricularV2 = seccionesv2.Where(x => x.Titulo.ToLower() == listaTituloV2[i]).FirstOrDefault();
                    if (secEstructuraCurricularV2 != null)
                    {
                        if (secEstructuraCurricularV2.Contenido == null || secEstructuraCurricularV2.Contenido == "")
                        {
                            var secEstructuraCurricularV1 = seccionesv1.Where(x => x.Titulo.ToLower() == listaTituloV1[i]).FirstOrDefault();
                            if (secEstructuraCurricularV1 != null){ seccionResultado.Add(secEstructuraCurricularV1);}
                        }
                        else
                        {
                            seccionResultado.Add(secEstructuraCurricularV2);
                        }
                    }
                    else
                    {
                        var secEstructuraCurricularV1 = seccionesv1.Where(x => x.Titulo.ToLower() == listaTituloV1[i]).FirstOrDefault();
                        if (secEstructuraCurricularV1 != null) { seccionResultado.Add(secEstructuraCurricularV1); }
                    }
                }

                if(listaPrimera != null && listaPrimera.Count > 0)
                {
                    foreach(var item in listaPrimera)
                    {
                        if(item.Seccion != null)
                        {
                            var temp = seccionResultado.Where(x => x.Titulo.ToLower().Equals(item.Seccion.ToLower())).FirstOrDefault();
                            if (temp != null) { seccionResultado.Remove(temp); }
                            if (item.Seccion.ToLower() == "certificacion")
                            {
                                var temp2 = seccionResultado.Where(x => x.Titulo.ToLower().Equals("certificación")).FirstOrDefault();
                                if (temp2 != null) { seccionResultado.Remove(temp2); }
                            }
                            if (item.Seccion.ToLower() == "prerrequisitos")
                            {
                                var temp3 = seccionResultado.Where(x => x.Titulo.ToLower().Equals("pre-requisitos")).FirstOrDefault();
                                if (temp3 != null) { seccionResultado.Remove(temp3); }
                            }
                        }
                    } 
                }

                programav2.ListaSeccionesContenidosDocumento = seccionResultado;

                List<ProgramaGeneralEstructuraAgrupadoDTO> Contenido = new List<ProgramaGeneralEstructuraAgrupadoDTO>();

                var _expositores = _repDocumentoSeccionPw.ObtenerExpositoresPorIdGeneral(idPGeneral);
                foreach (var item in _expositores)
                {
                    ProgramaGeneralEstructuraAgrupadoDTO temp = new ProgramaGeneralEstructuraAgrupadoDTO();

                    var nombreExpositor = item.PrimerNombre + " " + item.SegundoNombre + " " + item.ApellidoPaterno + " " + item.ApellidoMaterno + " - " +item.NombrePais;

                    temp.Seccion = "Expositores";
                    temp.Titulo = nombreExpositor;
                    temp.DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>();

                    ProgramaGeneralEstructuraDetalleDTO obj = new ProgramaGeneralEstructuraDetalleDTO()
                    {
                        Contenido = item.HojaVidaResumidaPerfil,
                        Cabecera = null,
                        PiePagina = null,
                    };
                    temp.DetalleContenido.Add(obj);

                    Contenido.Add(temp);
                }

                if (programav2.ListaSeccionesContenidosDocumento != null && programav2.ListaSeccionesContenidosDocumento.Count > 0)
                {
                    var _listaCursos2 = programav2.ListaSeccionesContenidosDocumento.GroupBy(x => new { x.Contenido, x.Titulo }).Select(x => new { x.Key.Contenido, x.Key.Titulo }).ToList();
                    foreach (var itemCurso in _listaCursos2)
                    {
                        var _listaCapitulos = programav2.ListaSeccionesContenidosDocumento.Where(x => x.Contenido == itemCurso.Contenido).GroupBy(x => new { x.Contenido, x.Cabecera, x.PiePagina }).ToList();
                        ProgramaGeneralEstructuraAgrupadoDTO obj = new ProgramaGeneralEstructuraAgrupadoDTO();
                        obj.Seccion = itemCurso.Titulo;
                        obj.Titulo = itemCurso.Titulo;
                        obj.DetalleContenido = new List<ProgramaGeneralEstructuraDetalleDTO>();
                        foreach (var itemSecion in _listaCapitulos)
                        {
                            ProgramaGeneralEstructuraDetalleDTO tmp = new ProgramaGeneralEstructuraDetalleDTO()
                            {
                                Contenido = itemSecion.Key.Contenido,
                                Cabecera = itemSecion.Key.Cabecera,
                                PiePagina = itemSecion.Key.PiePagina
                            };
                            obj.DetalleContenido.Add(tmp);

                        }
                        Contenido.Add(obj);
                    }
                }

                var listaProgramaGeneralDocumentoSeccion = Contenido.GroupBy(x => x.Seccion).Select(x => new ProgramaGeneralSeccionDocumentoDTO
                {
                    Seccion = x.Key,
                    DetalleSeccion = x.GroupBy(y => new { y.Titulo, y.DetalleContenido }).Select(y => new ProgramaGeneralSeccionDocumentoDetalleDTO
                    {
                        Titulo = y.Key.Titulo,
                        Cabecera = y.Key.DetalleContenido.GroupBy(z => z.Cabecera).Select(z => z.Key).FirstOrDefault(),
                        PiePagina = y.Key.DetalleContenido.GroupBy(z => z.PiePagina).Select(z => z.Key).FirstOrDefault(),
                        DetalleContenido = y.Key.DetalleContenido.Select(z => z.Contenido).ToList()
                    }).ToList()
                }).ToList();
                listaProgramaGeneralDocumentoSeccion.AddRange(listaPrimera);
                return listaProgramaGeneralDocumentoSeccion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Jose Villena
        /// Fecha: 03/05/2021
        /// Version: 1.0
        /// <summary>
        /// Genera HTML a partir de la lista de secciones de documento del programa general
        /// </summary>
        /// <param name="listaProgramaGeneralDocumentoSeccion">Lista de objetos del tipo ProgramaGeneralSeccionDocumentoDTO</param>	
        /// <returns>Lista secciones documento: List<ProgramaGeneralSeccionAnexosHTMLDTO></returns> 
        public List<ProgramaGeneralSeccionAnexosHTMLDTO> GenerarHTMLProgramaGeneralDocumentoSeccion(List<ProgramaGeneralSeccionDocumentoDTO> listaProgramaGeneralDocumentoSeccion)
        {
            try
            {
                List<ProgramaGeneralSeccionAnexosHTMLDTO> lista = new List<ProgramaGeneralSeccionAnexosHTMLDTO>();
                foreach (var item in listaProgramaGeneralDocumentoSeccion)
                {
                    ProgramaGeneralSeccionAnexosHTMLDTO obj = new ProgramaGeneralSeccionAnexosHTMLDTO();
                    obj.Seccion = item.Seccion;
                    string contenido = "";
                    foreach (var detalleSeccion in item.DetalleSeccion)
                    {
                        contenido += "<h5><strong><b>" + detalleSeccion.Titulo + "</b></strong></h5>";
                        contenido += "<p>" + detalleSeccion.Cabecera + "</p>";
                        contenido += "<ul type='disc'>";
                        foreach (var contenidoSeccion in detalleSeccion.DetalleContenido)
                        {
                            contenido += "<li>&bull;&nbsp;&nbsp;&nbsp;" + contenidoSeccion + "</li>";
                        }
                        contenido += "</ul>";
                        contenido += "<p>" + detalleSeccion.PiePagina + "</p>";
                    }
                    obj.Contenido = contenido;
                    lista.Add(obj);
                }
                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene lista de beneficios de dos formas:
        /// 1. Por IdMatriculaCabecera si es que los beneficios se congelaron
        /// 2. En caso no se congelen los beneficios se busca por la configuracion de beneficios del programa general mediante idpgeneral, idpais e idpaquete
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <param name="idPGeneral"></param>
        /// <param name="idPais"></param>
        /// <param name="idPaquete"></param>
        public List<string> ObtenerBeneficios(int idMatriculaCabecera, int idPGeneral, int idPais, int idPaquete)
        {
            try
            {
                MatriculaCabeceraBeneficiosRepositorio _repMatriculaCabeceraBeneficios = new MatriculaCabeceraBeneficiosRepositorio();
                PgeneralConfiguracionBeneficioRepositorio _repPgeneralConfiguracionBeneficios = new PgeneralConfiguracionBeneficioRepositorio();
                List<string> listaBeneficios = new List<string>();
                listaBeneficios = _repMatriculaCabeceraBeneficios.ObtenerBeneficiosPorMatriculaCabecera(idMatriculaCabecera);
                if (listaBeneficios.Count == 0 || listaBeneficios == null)
                {
                    var listaBeneficiosConfigurado = _repPgeneralConfiguracionBeneficios.ObtenerPgeneralConfiuracionBeneficios(idPGeneral);
                    listaBeneficios = listaBeneficiosConfigurado.Where(x => x.IdVersion.Any(a => a.IdVersion == idPaquete) && x.IdPais.Any(a => a.IdPais == idPais)).Select(x => x.Descripcion).ToList();
                }
                return listaBeneficios;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// Obtiene lista de beneficios por IdMatriculaCabecera si es que los beneficios se congelaron
        /// </summary>
        /// <param name="idMatriculaCabecera"></param>
        /// <returns></returns>
        public List<string> ObtenerBeneficiosCongeladosPorMatriculaCabecera(int idMatriculaCabecera)
        {
            try
            {
                List<string> listaBeneficios = new List<string>();
                listaBeneficios = _repMatriculaCabeceraBeneficios.ObtenerBeneficiosPorMatriculaCabecera(idMatriculaCabecera);
                return listaBeneficios;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene lista de beneficios por la configuracion de beneficios del programa general mediante idpgeneral, idpais e idpaquete
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <param name="idPais"> Id de Pais </param>
        /// <param name="idPaquete"> Id de Paquete </param>
        /// <returns> List<string> </returns>
        public List<string> ObtenerBeneficiosConfiguradosProgramaGeneral(int idPGeneral, int? idPais, int? idPaquete)
        {
            try
            {
                List<string> listaBeneficios = new List<string>();
                if (idPais.HasValue)
                {
                    var listaBeneficiosConfigurado = _repPgeneralConfiguracionBeneficios.ObtenerPgeneralConfiuracionBeneficios(idPGeneral);
                    listaBeneficios = listaBeneficiosConfigurado.Where(x => x.IdVersion.Any(a => a.IdVersion == idPaquete) && x.IdPais.Any(a => a.IdPais == idPais)).Select(x => x.Descripcion).ToList();
                }
                return listaBeneficios;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary> 
        /// Autor: --, Jashin Salazar
        /// Fecha: 28/04/2021 
        /// Version: 1.0 
        /// Descripcion: Genera vista previa del certificado.
        /// </summary> 
        public byte[] GenerarVistaPreviaCertificadoPortalWeb(int IdPlantillaF, int IdPlantillaP, int IdOportunidad, ref int Idplantillabase, ref string codigoCertificado)
        {
            ContenidoCertificado = new CertificadoGeneradoAutomaticoContenidoBO();
            int IdPlantillaBase = 0;
            var oportunidadClasificacionOperaciones = _repOportunidadClasificacionOperaciones.FirstBy(x => x.IdOportunidad == IdOportunidad);

            if (!_repMatriculaCabecera.Exist(x => x.Id == oportunidadClasificacionOperaciones.IdMatriculaCabecera))
            {
                throw new Exception("Matricula cabecera no valida!");
            }

            MatriculaCabeceraDatosCertificadoRepositorio repMatriculaCertificado = new MatriculaCabeceraDatosCertificadoRepositorio();
            MatriculaCabeceraDatosCertificadoBO certificados = repMatriculaCertificado.FirstBy(w =>
            w.Estado == true &&
            w.EstadoCambioDatos == false &&
            w.IdMatriculaCabecera == oportunidadClasificacionOperaciones.IdMatriculaCabecera);
            MatriculaCabeceraDatosCertificadoBO NuevoCertificado = new MatriculaCabeceraDatosCertificadoBO();

            var listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();

            var matriculaCabecera = _repMatriculaCabecera.FirstBy(x => x.Id == oportunidadClasificacionOperaciones.IdMatriculaCabecera);
            var detalleMatriculaCabecera = matriculaCabecera.ObtenerDetalleMatricula();
            var plantilla = _repPlantilla.FirstById(IdPlantillaF);


            IdPlantillaBase = plantilla.IdPlantillaBase;
            Idplantillabase = IdPlantillaBase;

            var plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(IdPlantillaF);
            var alumno = _repAlumno.FirstById(matriculaCabecera.IdAlumno);

            var oportunidad = _repOportunidad.FirstById(detalleMatriculaCabecera.IdOportunidad);
            var DatosCompuestosOportunidad = _repOportunidad.ObtenerDatosCompuestosPorIdOportunidad(oportunidad.Id);
            var personal = _repPersonal.FirstById(oportunidad.IdPersonalAsignado);

            var listaEtiqueta = plantillaBase.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

            foreach (var etiqueta in listaEtiqueta)
            {
                listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = string.Concat("{", etiqueta, "}"), texto = "" });
            }
            /*
            string pathfuente = System.AppDomain.CurrentDomain.BaseDirectory + "IMPRISHA.TTF";
            BaseFont bf = BaseFont.CreateFont(pathfuente, BaseFont.CP1250, BaseFont.EMBEDDED);
            iTextSharp.text.Font fuente = new iTextSharp.text.Font(bf, 18, 0, BaseColor.BLACK);
            */
            List<string> sepacion = new List<string>();
            string FondoFrontalCertificado = "";
            string FondoReversoCertificado = "";
            string FondoFrontalConstancia = "";
            string FondoReversoConstancia = "";

            sepacion = plantillaBase.Cuerpo.Split("###").ToList();

            Dictionary<string, object> map = new Dictionary<string, object>();
            map["font_factory"] = new MyFontFactory();
            //map.put("font_factory", new MyFontFactory());

            using (MemoryStream ms = new MemoryStream())
            {
                PdfSharp.Pdf.PdfDocument pdf = new PdfSharp.Pdf.PdfDocument();
                var config = new PdfGenerateConfig();
                //PdfSharp.Drawing.XFont font = new PdfSharp.Drawing.XFont("Times New Roman", 12, PdfSharp.Drawing.XFontStyle.Regular, options);
                if (IdPlantillaBase == 12)/*Certificado*/
                {
                    config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                    config.PageSize = PdfSharp.PageSize.A4;
                    config.MarginTop = 144;
                    config.MarginBottom = 30;
                    config.MarginLeft = 111;
                    config.MarginRight = 83;

                }
                else
                {
                    config.PageSize = PdfSharp.PageSize.A4;
                    config.MarginLeft = 60;
                    config.MarginRight = 60;
                    config.MarginTop = 60;
                    config.MarginBottom = 25;

                }

                /*FontFactory.RegisterDirectories();

                Document _document = new Document();
                if (IdPlantillaBase == 12)/*Certificado
                {
                    _document = new Document(PageSize.A4.Rotate(), 89, 89, 40, 75);
                }
                else
                {
                    _document = new Document(PageSize.A4, 60, 60, -1, 5);
                }

                //using (Document _document = new Document(PageSize.A4, 0, 0, 0, 0))

                //FontFactory.GetFont("Times New Roman", 14, iTextSharp.text.BaseColor.BLACK);
                PdfWriter pdfWriter = PdfWriter.GetInstance(_document, ms);
                pdfWriter.CloseStream = false;
                HTMLWorker htmlparser = new HTMLWorker(_document);
                HTMLWorker htmltemplate = new HTMLWorker(_document);
                //htmlparser.SetStyleSheet(GenerateStyleCertificado());                        

                //HtmlPipelineContext htmlContext = new HtmlPipelineContext(null);
                //htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());
                //ICSSResolver cssResolver = XMLWorkerHelper.GetInstance().GetDefaultCssResolver(false);

                //cssResolver.AddCss("div{color: red;}", true);
                //cssResolver.AddCss("h1{color: green;}", true);
                //IPipeline pipeline = new CssResolverPipeline(cssResolver, new HtmlPipeline(htmlContext, new PdfWriterPipeline(_document, pdfWriter)));
                //XMLWorker worker = new XMLWorker(pipeline, true);
                //XMLParser xmlParser = new XMLParser(worker);

                _document.Open();*/
                int Conteo = 0;
                //int cantidadItems = 0;
                List<string> estructura = new List<string>();



                string remplazo = "";
                foreach (var item in sepacion)
                {
                    string direccionCodigoQR = "";
                    string tipoletra = "Times New Roman";
                    remplazo = item;
                    string searchString = "/*";
                    int startIndex = remplazo.IndexOf(searchString);
                    searchString = "*/";
                    int endIndex = 0;
                    string substring = "";

                    if (startIndex != -1)
                    {
                        endIndex = remplazo.IndexOf(searchString, startIndex);
                        substring = remplazo.Substring(startIndex + searchString.Length, endIndex - searchString.Length);
                    }
                    if (substring != null && substring != "")
                    {
                        tipoletra = substring;
                    }
                    else
                    {
                        searchString = "text-align:";
                        startIndex = remplazo.IndexOf(searchString);
                        searchString = ";";

                        if (startIndex != -1)
                        {
                            endIndex = remplazo.IndexOf(searchString, startIndex);
                            substring = remplazo.Substring(startIndex + searchString.Length, endIndex + searchString.Length - startIndex);
                        }
                        if (substring != null && substring != "")
                        {
                            tipoletra = substring;
                        }
                    }
                    if (item.Contains("repositorioweb"))
                    {
                        if (IdPlantillaBase == 12)
                        {
                            FondoFrontalCertificado = item;
                        }
                        else
                        {
                            FondoFrontalConstancia = item;
                        }

                    }
                    if (item.Contains("acute"))
                    {
                        remplazo = remplazo.Replace("&Eacute;", "É").Replace("&Aacute;", "Á").Replace("&Iacute;", "Í").Replace("&Oacute;", "Ó").Replace("&Uacute;", "Ú");
                    }
                    if (item.Contains("CODIGOQR"))
                    {
                        string url = "";
                        if (IdPlantillaBase == 12)/*Certificados*/
                        {
                            url = _repAlumno.ObtenerCodigoCertificado(matriculaCabecera.Id);
                        }
                        else /*13:Constancia*/
                        {
                            url = _repCertificadoGeneradoAutomatico.ObtenerCorrelativoCertificado().ToString();
                        }

                        var urlCodigo = "https://bsginstitute.com/informacionCertificado?cod=" + matriculaCabecera.Id + "." + url;
                        QRCodeGenerator qRCodeGenerator = new QRCodeGenerator();
                        QRCodeData qrDatos = qRCodeGenerator.CreateQrCode(urlCodigo, QRCodeGenerator.ECCLevel.Q);
                        QRCode qRCodigo = new QRCode(qrDatos);

                        System.Drawing.Bitmap qrImagen = qRCodigo.GetGraphic(7, System.Drawing.Color.Black, System.Drawing.Color.White, false);
                        System.Drawing.Image objImage = (System.Drawing.Image)qrImagen;

                        //var a = System.IO.File.Exists(System.AppDomain.CurrentDomain.BaseDirectory);
                        //qrImagen.Save(System.AppDomain.CurrentDomain.BaseDirectory + "codigo.png", System.Drawing.Imaging.ImageFormat.Png);

                        direccionCodigoQR = _repAlumno.guardarArchivosQR(ImageToByte2(objImage), "image/jpeg", url);
                        this.ContenidoCertificado.CodigoQr = direccionCodigoQR;
                        remplazo = remplazo.Replace("CODIGOQR", $@"<img style='vertical-align:baseline' src='{direccionCodigoQR}' width=55 height=55  />");

                    }
                    if (item.Contains("{tPLA_PGeneral.Nombre}"))
                    {

                        var nombrePrograma = "";
                        if (certificados == null)
                        {
                            nombrePrograma = detalleMatriculaCabecera.NombreProgramaGeneral.Replace(" - COLOMBIA", "").ToUpper();
                            NuevoCertificado.NombreCurso = nombrePrograma;
                        }
                        else
                        {
                            nombrePrograma = certificados.NombreCurso;
                        }

                        this.ContenidoCertificado.NombrePrograma = nombrePrograma;
                        if (nombrePrograma.ToUpper().StartsWith("CURSO") || nombrePrograma.ToUpper().StartsWith("PROGRAMA"))
                        {
                            remplazo = remplazo.Replace("{T_Pgeneral.TipoCapacitacion}", "");
                        }
                        else
                        {
                            remplazo = remplazo.Replace("{T_Pgeneral.TipoCapacitacion}", " programa");
                        }
                        remplazo = remplazo.Replace("{tPLA_PGeneral.Nombre}", nombrePrograma);
                    }
                    if (item.Contains("{T_Pgeneral.CodigoPartner}"))
                    {
                        var codigoPartner = _repPgeneral.ObtenerCodigoPartner(matriculaCabecera.Id);
                        if (codigoPartner != null)
                        {
                            this.ContenidoCertificado.CodigoPartner = codigoPartner;
                            remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", codigoPartner);
                        }
                        else
                        {
                            throw new Exception("No se puede Calcular CodigoPartner");
                        }
                    }
                    //reemplazar nombre 1 alumno
                    if (item.Contains("{T_Alumno.NombreCompleto}"))
                    {
                    }
                    if (item.Contains("{tAlumnos.nombre1}"))
                    {
                        string nombreAlumno = "";
                        nombreAlumno = alumno.Nombre1.ToUpper();
                        //var parrafo = new iTextSharp.text.Phrase(alumno.NombreCompleto, fuente);
                        remplazo = remplazo.Replace("{tAlumnos.nombre1}", alumno.Nombre1.ToUpper());

                        if (item.Contains("{tAlumnos.nombre2}"))
                        {
                            nombreAlumno = nombreAlumno + " " + alumno.Nombre2.ToUpper();
                            remplazo = remplazo.Replace("{tAlumnos.nombre2}", alumno.Nombre2.ToUpper());
                            //_document.Add(new iTextSharp.text.Phrase(" " + alumno.Nombre2.ToUpper(), fuente));
                        }
                        if (item.Contains("{tAlumnos.apepaterno}"))
                        {
                            nombreAlumno = nombreAlumno + " " + alumno.ApellidoPaterno.ToUpper();
                            remplazo = remplazo.Replace("{tAlumnos.apepaterno}", alumno.ApellidoPaterno.ToUpper());
                            //_document.Add(new iTextSharp.text.Phrase(" " + alumno.ApellidoPaterno.ToUpper(), fuente));
                        }
                        if (item.Contains("{tAlumnos.apematerno}"))
                        {
                            nombreAlumno = nombreAlumno + " " + alumno.ApellidoMaterno.ToUpper();
                            remplazo = remplazo.Replace("{tAlumnos.apematerno}", alumno.ApellidoMaterno.ToUpper());
                            //_document.Add(new iTextSharp.text.Phrase(" " + alumno.ApellidoMaterno.ToUpper(), fuente));
                        }

                        this.ContenidoCertificado.NombreAlumno = nombreAlumno;
                        //if (IdPlantillaBase == 12)
                        //{
                        //    List<IElement> objects = HTMLWorker.ParseToList(new StringReader(remplazo), GenerateStyleCertificadoAutomatico(tipoletra), map);
                        //    foreach (IElement element in objects)
                        //    {
                        //        _document.Add(element);
                        //    }
                        //}
                        //else
                        //{
                        //    htmlparser.SetStyleSheet(GenerateStyleCertificadoAutomatico(tipoletra));
                        //    htmlparser.Parse(new StringReader(remplazo));
                        //}                               
                    }
                    if (item.Contains("{tpla_pgeneral.pw_duracion}"))
                    {
                        
                        string Duracion = DatosCompuestosOportunidad.pw_duracion;
                        remplazo = remplazo.Replace("{tpla_pgeneral.pw_duracion}", Duracion);
                    }
                    if (item.Contains("{tCentroCosto.centrocosto}"))
                    {
                        this.ContenidoCertificado.NombreCentroCosto = detalleMatriculaCabecera.NombreCentroCosto.ToUpper();
                        remplazo = remplazo.Replace("{tCentroCosto.centrocosto}", detalleMatriculaCabecera.NombreCentroCosto.ToUpper());
                    }
                    if (item.Contains("{tPEspecifico.ciudad}"))
                    {
                        var PrimeraLetra = detalleMatriculaCabecera.NombreCiudad.Substring(0, 1).ToUpper();
                        var resto = detalleMatriculaCabecera.NombreCiudad.Substring(1).ToLower();
                        this.ContenidoCertificado.Ciudad = PrimeraLetra + resto;
                        if (ContenidoCertificado.Ciudad == null || ContenidoCertificado.Ciudad == "")
                        {
                            throw new Exception("No se Puede Calcular Ciudad");
                        }
                        remplazo = remplazo.Replace("{tPEspecifico.ciudad}", PrimeraLetra + resto);
                    }
                    if (item.Contains("{tPEspecifico.EscalaCalificacion}"))
                    {
                        var escalaCalificacion = detalleMatriculaCabecera.EscalaCalificacion;
                        this.ContenidoCertificado.EscalaCalificacion = escalaCalificacion;
                        if (escalaCalificacion == 0 || escalaCalificacion == null)
                        {
                            throw new Exception("No se Puede Calcular Escala Calificacion");
                        }
                        remplazo = remplazo.Replace("{tPEspecifico.EscalaCalificacion}", escalaCalificacion.ToString());
                    }
                    //reemplazar Fecha Inicio Capacitacion
                    if (item.Contains("{T_Alumno.FechaInicioCapacitacion}"))
                    {
                        var fechaInicioCapacitacion = "";
                        if (certificados == null)
                        {
                            fechaInicioCapacitacion = _repAlumno.ObtenerFechaInicioCapacitacionPortalWeb(matriculaCabecera.Id);
                            NuevoCertificado.FechaInicio = repMatriculaCertificado.TranformarCadenaEnFecha(fechaInicioCapacitacion);
                        }
                        else
                        {
                            fechaInicioCapacitacion = repMatriculaCertificado.TranformarFechaEnCadena(certificados.FechaInicio);
                        }
                        if (fechaInicioCapacitacion == null || fechaInicioCapacitacion == "")
                        {
                            throw new Exception("No se Puede Calcular FechaInicioCapacitacion");
                        }
                        this.ContenidoCertificado.FechaInicioCapacitacion = fechaInicioCapacitacion;
                        remplazo = remplazo.Replace("{T_Alumno.FechaInicioCapacitacion}", fechaInicioCapacitacion);
                    }

                    //reemplazar Fecha Fin Capacitacion
                    if (item.Contains("{T_Alumno.FechaFinCapacitacion}"))
                    {
                        var fechaFinCapacitacion = "";
                        if (certificados == null)
                        {
                            fechaFinCapacitacion = _repAlumno.ObtenerFechaFinCapacitacionPortalWeb(matriculaCabecera.Id);
                            NuevoCertificado.FechaFinal = repMatriculaCertificado.TranformarCadenaEnFecha(fechaFinCapacitacion);
                        }
                        else
                        {
                            fechaFinCapacitacion = repMatriculaCertificado.TranformarFechaEnCadena(certificados.FechaFinal);
                        }
                        if (fechaFinCapacitacion == null || fechaFinCapacitacion == "")
                        {
                            throw new Exception("No se Puede Calcular FehaFinCapacitacion");
                        }
                        this.ContenidoCertificado.FechaFinCapacitacion = fechaFinCapacitacion;
                        remplazo = remplazo.Replace("{T_Alumno.FechaFinCapacitacion}", fechaFinCapacitacion);
                    }
                    //reemplazar Calificacion Promedio
                    if (item.Contains("{T_Alumno.CalificacionPromedio}"))
                    {
                        //var calificacionPromedio = _repAlumno.ObtenerNotaPromedio(matriculaCabecera.Id);

                        
                        EsquemaEvaluacion_NotaCursoDTO calificacionPromedio = esquemaBO.ObtenerDetalleCalificacionPorCurso(matriculaCabecera.Id, matriculaCabecera.IdPespecifico,1);

                        if (calificacionPromedio == null)
                        {
                            throw new Exception("No se Puede Calcular Calificacion Promedio");
                        }
                        this.ContenidoCertificado.CalificacionPromedio = Convert.ToInt32(calificacionPromedio.NotaCurso);
                        remplazo = remplazo.Replace("{T_Alumno.CalificacionPromedio}", (Convert.ToInt32(calificacionPromedio)).ToString());
                    }
                    //reemplazar Fecha Emision Certificado
                    if (item.Contains("{T_Alumno.FechaEmisionCertificado}"))
                    {
                        var fechaEmision = _repAlumno.ObtenerFechaEmision();
                        this.ContenidoCertificado.FechaEmisionCertificado = fechaEmision;
                        remplazo = remplazo.Replace("{T_Alumno.FechaEmisionCertificado}", fechaEmision);
                    }

                    //reemplazar Fecha Codigo Certificado
                    if (item.Contains("{T_Alumno.CodigoCertificado}"))
                    {
                        var CodigoCertificado = _repAlumno.ObtenerCodigoCertificado(matriculaCabecera.Id);
                        if (CodigoCertificado == null || CodigoCertificado == "")
                        {
                            throw new Exception("No se Puede Calcular Codigo Certificado");
                        }
                        codigoCertificado = CodigoCertificado;
                        remplazo = remplazo.Replace("{T_Alumno.CodigoCertificado}", CodigoCertificado);
                    }

                    //reemplazar duracion en horas de Programa Especifico
                    if (item.Contains("{tPEspecifico.duracion}"))
                    {
                        var duracionPespecifico = "";
                        if (certificados == null)
                        {
                            duracionPespecifico = _repPespecifico.ObtenerDuracionProgramaEspecifico(matriculaCabecera.IdPespecifico, matriculaCabecera.Id);
                            NuevoCertificado.Duracion = duracionPespecifico;
                        }
                        else
                        {
                            duracionPespecifico = certificados.Duracion.ToString();
                        }
                        if (duracionPespecifico == null || duracionPespecifico == "")
                        {
                            throw new Exception("No se Puede Calcular Duracion Pespecifico");
                        }
                        this.ContenidoCertificado.DuracionPespecifico = Int32.Parse(duracionPespecifico);
                        remplazo = remplazo.Replace("{tPEspecifico.duracion}", duracionPespecifico);
                    }
                    /*reemplazar Codigo de matricula del alumno */
                    if (item.Contains("{T_MatriculaCabecera.CodigoMatricula}"))
                    {
                        remplazo = remplazo.Replace("{T_MatriculaCabecera.CodigoMatricula}", matriculaCabecera.CodigoMatricula);
                    }
                    /*reemplazar Codigo de matricula del alumno */
                    if (item.Contains("{T_Alumno.CorrelativoConstancia}"))
                    {
                        var correlativoConstancia = _repCertificadoGeneradoAutomatico.ObtenerCorrelativoCertificado();
                        this.ContenidoCertificado.CorrelativoConstancia = correlativoConstancia;
                        remplazo = remplazo.Replace("{T_Alumno.CorrelativoConstancia}", correlativoConstancia.ToString());
                    }
                    /*reemplazar Cronograma de notas del alumno por matricula*/
                    if (item.Contains("{T_Alumno.CronogramaNotas}"))
                    {
                        string tablaNota = "";
                        var cronogramaNota = _repAlumno.ObtenerCronogramaNota(matriculaCabecera.Id);
                        if (cronogramaNota == null || cronogramaNota.Count == 0)
                        {
                            throw new Exception("No se Puede Calcular cronogramaNota");
                        }
                        tablaNota += "<table  style='font-size:11px; border:1px solid #575656;font-family:Arial;color:#575656;border-collapse: collapse;width:100%;' cellspacing='3' cellpadding='3'>";
                        tablaNota += $@"<tr style='border:1px solid #575656;border-collapse: collapse;'>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> CURSO </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> NOTA </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> ESTADO </td>
                                            </tr>";
                        int contador = 0;
                        for (int rows = 0; rows < cronogramaNota.Count; rows++)
                        {
                            tablaNota += "<tr style='border:1px solid #575656;border-collapse: collapse;'>";
                            tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;;text-align:left'> {cronogramaNota[rows].Curso}</td>";
                            tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:center'> {cronogramaNota[rows].Nota}</td>";
                            tablaNota += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:center'> {cronogramaNota[rows].Estado}</td>";

                            contador = contador + 1;
                            tablaNota += "</tr>";
                        }
                        tablaNota += "</table>";
                        this.ContenidoCertificado.CronogramaNota = tablaNota;
                        remplazo = remplazo.Replace("{T_Alumno.CronogramaNotas}", tablaNota);
                    }
                    /*reemplazar Cronograma de Asistencia del alumno por matricula*/
                    if (item.Contains("{T_Alumno.CronogramaAsistencia}"))
                    {
                        string tablaAsistencia = "";
                        var cronogramaAsistencia = _repAlumno.ObtenerCronogramaAsistencia(matriculaCabecera.Id);
                        if (cronogramaAsistencia == null || cronogramaAsistencia.Count == 0)
                        {
                            throw new Exception("No se Puede Calcular CronogramaAsistencia");
                        }
                        tablaAsistencia += "<table  style='font-size:11px; border:1px solid #575656;font-family:Arial;color:#575656;border-collapse: collapse;width:100%;' cellspacing='3' cellpadding='3'>";
                        tablaAsistencia += $@"<tr style='border:1px solid #575656;border-collapse: collapse;'>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> CURSO </td>
                                                 <td style='border:1px solid #575656;border-collapse: collapse;text-align:center;font-weight: bold;'> ASISTENCIA </td>
                                            </tr>";
                        int contador = 0;
                        for (int rows = 0; rows < cronogramaAsistencia.Count; rows++)
                        {
                            tablaAsistencia += "<tr style='border:1px solid #575656;border-collapse: collapse;'>";
                            tablaAsistencia += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:left;'> {cronogramaAsistencia[rows].Curso}</td>";
                            tablaAsistencia += $@"<td style='vertical-align:top;border:1px solid #575656;border-collapse: collapse;text-align:Center;'> {cronogramaAsistencia[rows].PorcentajeAsistencia}</td>";

                            contador = contador + 1;
                            tablaAsistencia += "</tr>";
                        }
                        tablaAsistencia += "</table>";
                        this.ContenidoCertificado.CronogramaAsistencia = tablaAsistencia;
                        remplazo = remplazo.Replace("{T_Alumno.CronogramaAsistencia}", tablaAsistencia);
                    }
                    if (item.Contains("{T_Pgeneral.EstructuraCurricular}"))
                    {
                        string listaEstructura = "";
                        var EstructuraporVersion = _repPgeneralAsubPgeneral.ObtenerCursosEstrucuraCurricular(matriculaCabecera.Id);
                        listaEstructura = "<ul>";
                        foreach (var hijo in EstructuraporVersion)
                        {
                            listaEstructura += $@"<li>{hijo.Nombre} ({hijo.Duracion} horas cronológicas)</li>";
                        }
                        listaEstructura += "</ul>";
                        this.ContenidoCertificado.EstructuraCurricular = listaEstructura;
                        remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", listaEstructura);
                    }
                    //htmlparser.SetStyleSheet(GenerateStyleCertificadoAutomatico(tipoletra));
                    //htmlparser.Parse(new StringReader(remplazo));
                }
                var pdfFrontal = PdfGenerator.GeneratePdf(remplazo, config);
                var temmporal = ImportarPdfDocument(pdfFrontal);
                PdfSharp.Pdf.PdfPage page = temmporal.Pages[0];
                pdf.AddPage(page);

                PdfSharp.Drawing.XGraphics gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(pdf.Pages[0], PdfSharp.Drawing.XGraphicsPdfPageOptions.Prepend);

                if (IdPlantillaBase == 12)
                {
                    System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(FondoFrontalCertificado);
                    webRequest.AllowWriteStreamBuffering = true;
                    System.Net.WebResponse webResponse = webRequest.GetResponse();
                    PdfSharp.Drawing.XImage xImage = PdfSharp.Drawing.XImage.FromStream(webResponse.GetResponseStream());
                    gfx.DrawImage(xImage, 0, 0, 843, 595);
                }
                else
                {
                    System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(FondoFrontalConstancia);
                    webRequest.AllowWriteStreamBuffering = true;
                    System.Net.WebResponse webResponse = webRequest.GetResponse();
                    PdfSharp.Drawing.XImage xImage = PdfSharp.Drawing.XImage.FromStream(webResponse.GetResponseStream());
                    gfx.DrawImage(xImage, 0, 0, 595, 843);
                }

                if (certificados == null
                    && NuevoCertificado.NombreCurso != null
                    && NuevoCertificado.FechaInicio.Year > 1900
                    && NuevoCertificado.FechaFinal.Year > 1900
                    && NuevoCertificado.Duracion != null)
                {
                    NuevoCertificado.IdMatriculaCabecera = oportunidadClasificacionOperaciones.IdMatriculaCabecera;
                    NuevoCertificado.EstadoCambioDatos = false;
                    NuevoCertificado.Estado = true;
                    NuevoCertificado.UsuarioCreacion = "SYSTEM";
                    NuevoCertificado.UsuarioModificacion = "SYSTEM";
                    NuevoCertificado.FechaCreacion = DateTime.Now;
                    NuevoCertificado.FechaModificacion = DateTime.Now;
                    repMatriculaCertificado.Insert(NuevoCertificado);
                    certificados = repMatriculaCertificado.FirstBy(w =>
                     w.Estado == true &&
                     w.EstadoCambioDatos == false &&
                     w.IdMatriculaCabecera == oportunidadClasificacionOperaciones.IdMatriculaCabecera);
                };

                if (IdPlantillaP != 0 && IdPlantillaP != null)
                {
                    config = new PdfGenerateConfig();
                    if (IdPlantillaBase == 12)/*Certificado*/
                    {
                        config.PageOrientation = PdfSharp.PageOrientation.Landscape;
                        config.PageSize = PdfSharp.PageSize.A4;
                        config.MarginLeft = 257;
                        config.MarginRight = 83;
                        config.MarginTop = 49;
                        config.MarginBottom = 0;

                    }
                    string _estructuraCurricular = "";
                    listaObjetoWhasApp = new List<datoPlantillaWhatsApp>();
                    var plantillaP = _repPlantilla.FirstById(IdPlantillaP);
                    var plantillaBaseP = _repPlantilla.ObtenerPlantillaCorreo(IdPlantillaP);

                    var listaEtiquetaP = plantillaBaseP.Cuerpo.Split(new string[] { "{" }, StringSplitOptions.None).Where(o => o.Contains("}")).Select(o => o.Split(new string[] { "}" }, StringSplitOptions.None).First()).ToList();

                    foreach (var etiqueta in listaEtiquetaP)
                    {
                        listaObjetoWhasApp.Add(new datoPlantillaWhatsApp() { codigo = string.Concat("{", etiqueta, "}"), texto = "" });
                    }
                    sepacion = new List<string>();

                    sepacion = plantillaBaseP.Cuerpo.Split("###").ToList();

                    foreach (var item in sepacion)
                    {
                        remplazo = item;

                        string tipoletra = "Times New Roman";

                        string searchString = "/*";
                        int startIndex = remplazo.IndexOf(searchString);
                        searchString = "*/";
                        int endIndex = 0;
                        string substring = "";
                        if (startIndex != -1)
                        {
                            endIndex = remplazo.IndexOf(searchString, startIndex);
                            substring = remplazo.Substring(startIndex + searchString.Length, endIndex + searchString.Length - startIndex);
                        }
                        if (substring != null && substring != "")
                        {
                            tipoletra = substring;
                        }
                        else
                        {
                            searchString = "text-align:";
                            startIndex = remplazo.IndexOf(searchString);
                            searchString = ";";

                            if (startIndex != -1)
                            {
                                endIndex = remplazo.IndexOf(searchString, startIndex);
                                substring = remplazo.Substring(startIndex + searchString.Length, endIndex + searchString.Length - startIndex);
                            }
                            if (substring != null && substring != "")
                            {
                                tipoletra = substring;
                            }
                        }
                        if (item.Contains("acute"))
                        {
                            remplazo = remplazo.Replace("&Eacute;", "É").Replace("&Aacute;", "Á").Replace("&Iacute;", "Í").Replace("&Oacute;", "Ó").Replace("&Uacute;", "Ú");
                        }
                        if (item.Contains("repositorioweb"))
                        {
                            if (IdPlantillaBase == 12)
                            {
                                FondoReversoCertificado = item;
                            }
                            else
                            {
                                FondoReversoConstancia = item;
                            }

                        }

                        if (item.Contains("{tPLA_PGeneral.Nombre}"))
                        {
                            ////_document.Add(new iTextSharp.text.Paragraph(""));
                            ////var parrafo = new iTextSharp.text.Paragraph(detalleMatriculaCabecera.NombreProgramaGeneral.ToUpper(), fuente);
                            ////_document.Add(new iTextSharp.text.Paragraph(""));
                            ////parrafo.Alignment = Element.ALIGN_CENTER;
                            ////_document.Add(parrafo);
                            //remplazo = detalleMatriculaCabecera.NombreProgramaGeneral.ToUpper();
                            //List<IElement> objects = HTMLWorker.ParseToList(new StringReader(remplazo), GenerateStyleCertificadoAutomatico(tipoletra), map);
                            //foreach (IElement element in objects)
                            //{
                            //    _document.Add(element);
                            //}
                            remplazo = remplazo.Replace("{tPLA_PGeneral.Nombre}", (certificados!=null)?certificados.NombreCurso: detalleMatriculaCabecera.NombreProgramaGeneral.Replace(" - COLOMBIA", "").ToUpper());
                        }
                        if (item.Contains("{T_Pgeneral.CodigoPartner}"))
                        {
                            var codigoPartner = _repPgeneral.ObtenerCodigoPartner(detalleMatriculaCabecera.IdProgramaGeneral);
                            if (codigoPartner != null)
                            {
                                remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", codigoPartner);
                                this.ContenidoCertificado.CodigoPartner = codigoPartner;
                            }
                            else
                            {
                                remplazo = remplazo.Replace("{T_Pgeneral.CodigoPartner}", "");
                            }
                        }
                        if (item.Contains("{tCentroCosto.centrocosto}"))
                        {
                            remplazo = remplazo.Replace("{tCentroCosto.centrocosto}", detalleMatriculaCabecera.NombreCentroCosto);
                        }
                        if (item.Contains("{tPEspecifico.ciudad}"))
                        {
                            remplazo = remplazo.Replace("{tPEspecifico.ciudad}", detalleMatriculaCabecera.NombreCiudad.Substring(0, 1).ToUpper() + detalleMatriculaCabecera.NombreCiudad.Substring(1).ToLower()); ;
                        }
                        //reemplazar nombre 1 alumno
                        if (item.Contains("{T_Alumno.NombreCompleto}"))
                        {

                        }
                        if (item.Contains("{tAlumnos.nombre1}"))
                        {

                            //var parrafo = new iTextSharp.text.Phrase(alumno.NombreCompleto, fuente);
                            remplazo = remplazo.Replace("{tAlumnos.nombre1}", alumno.Nombre1);

                            //_document.Add(new iTextSharp.text.Paragraph(""));
                            //var parrafo = new iTextSharp.text.Phrase("                                  "+alumno.Nombre1.ToUpper(), fuente);
                            ////parrafo = Element.ALIGN_CENTER;
                            //_document.Add(parrafo);
                            if (item.Contains("{tAlumnos.nombre2}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.nombre2}", alumno.Nombre2);
                                //_document.Add(new iTextSharp.text.Phrase(" " + alumno.Nombre2.ToUpper(), fuente));
                            }
                            if (item.Contains("{tAlumnos.apematerno}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.apematerno}", alumno.ApellidoMaterno);
                                //_document.Add(new iTextSharp.text.Phrase(" " + alumno.ApellidoMaterno.ToUpper(), fuente));
                            }
                            if (item.Contains("{tAlumnos.apepaterno}"))
                            {
                                remplazo = remplazo.Replace("{tAlumnos.apepaterno}", alumno.ApellidoPaterno);
                                //_document.Add(new iTextSharp.text.Phrase(" " + alumno.ApellidoPaterno.ToUpper(), fuente));
                            }

                        }

                        if (item.Contains("{tpla_pgeneral.pw_duracion}"))
                        {
                            remplazo = remplazo.Replace("{tpla_pgeneral.pw_duracion}", (certificados != null) ? certificados.Duracion.ToString(): DatosCompuestosOportunidad.pw_duracion);
                        }
                        //reemplazar Fecha Inicio Capacitacion
                        if (item.Contains("{T_Alumno.FechaInicioCapacitacion}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaInicioCapacitacion}", (certificados != null) ? repMatriculaCertificado.TranformarFechaEnCadena(certificados.FechaInicio): _repAlumno.ObtenerFechaInicioCapacitacionPortalWeb(matriculaCabecera.Id));
                        }

                        //reemplazar Fecha Fin Capacitacion
                        if (item.Contains("{T_Alumno.FechaFinCapacitacion}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaFinCapacitacion}", ( certificados != null)?repMatriculaCertificado.TranformarFechaEnCadena(certificados.FechaFinal): _repAlumno.ObtenerFechaFinCapacitacionPortalWeb(matriculaCabecera.Id));
                        }
                        //reemplazar Calificacion Promedio
                        if (item.Contains("{T_Alumno.CalificacionPromedio}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.CalificacionPromedio}", _repAlumno.ObtenerNotaPromedio(matriculaCabecera.Id));
                        }
                        //reemplazar Fecha Emision Certificado
                        if (item.Contains("{T_Alumno.FechaEmisionCertificado}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.FechaEmisionCertificado}", _repAlumno.ObtenerFechaEmision());
                        }

                        //reemplazar Fecha Codigo Certificado
                        if (item.Contains("{T_Alumno.CodigoCertificado}"))
                        {
                            remplazo = remplazo.Replace("{T_Alumno.CodigoCertificado}", _repAlumno.ObtenerCodigoCertificado(matriculaCabecera.Id));
                        }

                        //reemplazar duracion en horas de Programa Especifico
                        if (item.Contains("{tPEspecifico.duracion}"))
                        {
                            remplazo = remplazo.Replace("{tPEspecifico.duracion}", (certificados != null) ? certificados.Duracion.ToString() : _repPespecifico.ObtenerDuracionProgramaEspecifico(matriculaCabecera.IdPespecifico, matriculaCabecera.Id));
                        }
                        if (item.Contains("{T_Pgeneral.EstructuraCurricular}"))
                        {
                            var EstructuraporVersion = _repPgeneralAsubPgeneral.ObtenerCursosEstrucuraCurricular(matriculaCabecera.Id);
                            if (EstructuraporVersion.Count > 0)
                            {
                                string listaEstructura = $@"<table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>
                                                        <tr style='vertical-align: text-top;'>
                                                        <td style = 'vertical-align: text-top;text-align:left;font-weight: normal;' > ";
                                listaEstructura += "<ul style='margin-top:-20px'>";
                                foreach (var hijo in EstructuraporVersion)
                                {
                                    listaEstructura += $@"<li style='padding-bottom:5px'>{hijo.Nombre.Replace(" - COLOMBIA", "")} ({hijo.Duracion} horas cronológicas)</li>";
                                }


                                listaEstructura += "</ul></td></tr></table>";
                                _estructuraCurricular = listaEstructura;
                                this.ContenidoCertificado.EstructuraCurricular = listaEstructura;
                                remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", listaEstructura);
                            }
                            else
                            {
                                var estructuraCurso = _repDocumentoSeccionPw.ObtenerEstructuraCurso(detalleMatriculaCabecera.IdProgramaGeneral);
                                if (estructuraCurso.Count > 0)
                                {
                                    string listaEstructura = $@"
                                                        <table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>
                                                        <tr style='vertical-align: text-top;'>
                                                        <td style = 'vertical-align: text-top;text-align:left;font-weight: normal;' > ";
                                    listaEstructura += "<ul style='margin-top:-20px'>";

                                    foreach (var capitulo in estructuraCurso)
                                    {
                                        listaEstructura += $@"<li style='padding-bottom:5px'>{capitulo.Contenido.Replace(" - COLOMBIA", "")} </li>";
                                    }

                                    listaEstructura += "</ul></td></tr></table>";
                                    _estructuraCurricular = listaEstructura;
                                    this.ContenidoCertificado.EstructuraCurricular = listaEstructura;
                                    remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", listaEstructura);
                                }
                                else
                                {
                                    remplazo = remplazo.Replace("{T_Pgeneral.EstructuraCurricular}", "");
                                }

                            }

                        }
                        if (item.Contains("Template"))
                        {

                            var etiq = listaObjetoWhasApp.Where(x => x.codigo.Contains("Template")).ToList();
                            SeccionEtiquetaDTO valor = new SeccionEtiquetaDTO();
                            string etiqueta = "";

                            foreach (var a in etiq)
                            {
                                List<string> ListaPalabras = new List<string>();
                                char[] delimitador = new char[] { '.' };
                                string IdPlantilla = "";
                                string IdColumna = "";
                                string[] array = a.codigo.Split(delimitador, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string s in array)
                                {
                                    ListaPalabras.Add(s);
                                }
                                IdPlantilla = ListaPalabras[3].ToString();
                                IdColumna = ListaPalabras[4].ToString();


                                var prevalor = _repPespecifico.ObtenerContenidoTemplate(new Guid(IdPlantilla), new Guid(IdColumna.Replace("}", "")), oportunidad.IdCentroCosto ?? default(int));

                                if (prevalor != null && _estructuraCurricular == "")
                                {
                                    etiqueta = a.codigo;
                                    valor = prevalor;
                                }
                                else
                                {
                                    remplazo = remplazo.Replace(a.codigo, "");
                                }

                            }
                            if (etiqueta != "")
                            {
                                var tratamiento = valor.Valor.Replace("<p>", "<p id='estructura'<p>");
                                tratamiento = tratamiento.Replace("<li>", "<li id='liseparar'<li>");
                                tratamiento = tratamiento.Replace("&bull", "<li id='liseparar'");
                                estructura = tratamiento.Split("<p id='estructura'").Where(x => x.StartsWith("<p><strong>")).ToList();
                                List<string> todo = new List<string>();
                                //PdfContentByte cb = pdfWriter.DirectContent;
                                //ColumnText ct = new ColumnText(cb);

                                //ct.Alignment = Element.ALIGN_JUSTIFIED;
                                int contador = 0;
                                string htmltabla = "";
                                List<string> total = new List<string>();
                                foreach (var li in estructura)
                                {
                                    //foreach (var li1 in li.Split("&bull").ToList())
                                    //{
                                    //    //if (etiqueta.Contains("Silabo"))
                                    //    //{
                                    //    //    total.Add(li1);
                                    //    //}
                                    //    //else
                                    //    //{
                                    //    if (li1.Contains("<p>"))
                                    //        total.Add(li1.Replace("p>", "li>").Replace("<li>", "<li style='padding-bottom:5px'>").Replace("strong", "span").Replace("<ul>", "").Replace("</ul>", "").Replace("\"", "'").Replace("<div class='slide'>", ""));
                                    //    //}

                                    //}
                                    foreach (var li1 in li.Split("<li id='liseparar'").ToList())
                                    {
                                        //if (etiqueta.Contains("Silabo"))
                                        //{
                                        //    total.Add(li1);
                                        //}
                                        //else
                                        //{
                                        if (li1.Contains("<p>"))
                                            total.Add(li1.Replace("p>", "li>").Replace("<li>", "<li style='padding-bottom:5px'>").Replace("strong", "span").Replace("<ul>", "").Replace("</ul>", "").Replace("\"", "'").Replace("<div class='slide'>", "").Replace("<p style='padding-left:30px;'>", ""));
                                        //}

                                    }

                                }
                                int Cantidadcolumns = total.Count / 25;
                                int residuo = total.Count % 25;
                                if (residuo != 0)
                                {
                                    Cantidadcolumns++;
                                }
                                int reparticion = Cantidadcolumns;

                                PdfPTable PdfTable = new PdfPTable(Cantidadcolumns);
                                PdfTable.WidthPercentage = 100f;
                                PdfPCell PdfPCell = null;
                                string cadena1 = "";
                                string cadena2 = "";
                                string cadena3 = "";
                                string cadena4 = "";
                                List<string> registros = new List<string>();
                                foreach (var concatenar in total)
                                {
                                    //int residuo = Conteo % 27;
                                    if (Conteo < 22)
                                    {
                                        if (Conteo == 0)
                                        {
                                            cadena1 += "<ul style='margin-top:-20px'>";
                                        }
                                        cadena1 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                    }
                                    else
                                    {
                                        if (Conteo < 48)
                                        {
                                            //if (!etiqueta.Contains("Programa") && !concatenar.Contains("li>"))
                                            cadena2 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                        }
                                        else
                                        {
                                            if (Conteo < 75)
                                            {
                                                //if (!etiqueta.Contains("Programa") && !concatenar.Contains("li>"))
                                                cadena3 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                            }
                                            else
                                            {
                                                //if (!etiqueta.Contains("Programa") && !concatenar.Contains("li>"))
                                                cadena4 += concatenar.Replace("<p>", "<p style='margin-left: 10px;'>");
                                            }
                                        }
                                    }

                                    Conteo++;
                                }

                                htmltabla += "<table style='margin-left: -30px;margin-top:-20px;vertical-align: text-top;padding-top:0px'>";
                                contador = 0;
                                for (int rows = 0; rows < 1; rows++)
                                {
                                    htmltabla += "<tr style='vertical-align: text-top;'>";
                                    for (int column = 0; column < Cantidadcolumns; column++)
                                    {
                                        if (column == 0)
                                        {
                                            if (cadena1.TrimEnd(' ').EndsWith("</li>\n"))
                                            {
                                                cadena1 = cadena1 + "</ul>";
                                            }
                                            htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena1}";
                                        }
                                        if (column == 1)
                                        {
                                            if (cadena2.StartsWith("<li>"))
                                            {
                                                cadena2 = "<ul>" + cadena2;
                                            }
                                            if (cadena2.TrimEnd(' ').EndsWith("</li>\n"))
                                            {
                                                cadena2 = cadena2 + "</ul>";
                                            }
                                            htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena2}";
                                        }
                                        if (column == 2)
                                        {
                                            if (cadena3.StartsWith("<li>"))
                                            {
                                                cadena3 = "<ul>" + cadena3;
                                            }
                                            if (cadena3.TrimEnd(' ').EndsWith("</li>\n"))
                                            {
                                                cadena3 = cadena3 + "</ul>";
                                            }
                                            htmltabla += $@"<td style='vertical-align: text-top;text-align:left;font-weight: normal;'> {cadena3}";
                                        }
                                        if (column == 3)
                                        {
                                            if (cadena4.StartsWith("<li>"))
                                            {
                                                cadena4 = "<ul>" + cadena4;
                                            }
                                            if (cadena4.TrimEnd(' ').EndsWith("</li>\n"))
                                            {
                                                cadena4 = cadena4 + "</ul>";
                                            }
                                            htmltabla += $@"<th style='vertical-align:top;text-align:left;font-weight: normal;'> {cadena4}";
                                        }


                                        //List list = new List();
                                        ////for (int i = 0; i < estructura[contador].Split("<li id='liseparar'").ToList().Count(); i++)
                                        ////{
                                        //var lista = estructura[contador].Split("<li id='liseparar'").ToList();

                                        //foreach (var li in lista)
                                        //{
                                        //    string cadenaSinTags = Regex.Replace(li, "<.*?>", string.Empty);
                                        //    list.Add(new ListItem(cadenaSinTags));
                                        //}
                                        ////}
                                        ////var parse = new Phrase();
                                        ////parse.Add(list);
                                        ////PdfPCell = new PdfPCell();
                                        ////PdfPCell.AddElement(parse);
                                        ////PdfTable.AddCell(PdfPCell);
                                        contador = contador + 1;
                                        htmltabla += "</td>";
                                    }
                                    htmltabla += "</tr>";
                                }
                                htmltabla += "</table>";
                                PdfTable.SpacingBefore = 15f; // Give some space after the text or it may overlap the table

                                //doc.Add(paragraph);// add paragraph to the document
                                //_document.Add(PdfTable); // add pdf table to the document

                                this.ContenidoCertificado.EstructuraCurricular = htmltabla;
                                remplazo = remplazo.Replace(etiqueta, htmltabla);
                            }
                        }
                        //htmltemplate.SetStyleSheet(GenerateStyleCertificadoAutomatico(tipoletra));
                        //htmltemplate.Parse(new StringReader(remplazo));

                    }
                    var pdfPosterior = PdfGenerator.GeneratePdf(remplazo, config);
                    temmporal = ImportarPdfDocument(pdfPosterior);
                    page = temmporal.Pages[0];
                    pdf.AddPage(page);

                    gfx = PdfSharp.Drawing.XGraphics.FromPdfPage(pdf.Pages[1], PdfSharp.Drawing.XGraphicsPdfPageOptions.Prepend);

                    System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(FondoReversoCertificado); ;
                    webRequest.AllowWriteStreamBuffering = true;
                    System.Net.WebResponse webResponse = webRequest.GetResponse();
                    PdfSharp.Drawing.XImage xImage = PdfSharp.Drawing.XImage.FromStream(webResponse.GetResponseStream());
                    gfx.DrawImage(xImage, 0, 0, 843, 595);
                }

                //_document.Close();
                pdf.Save(ms, false);
                return ms.ToArray();
            }

        }


    }

    public class MyFontFactory : IFontProvider
    {
        public Font GetFont(string fontname,
                string encoding, bool embedded, float size,
                int style, BaseColor color)
        {
            BaseFont bf;
            try
            {
                string pathfuente = System.AppDomain.CurrentDomain.BaseDirectory + "IMPRISHA.TTF";

                //bf = BaseFont.CreateFont("c:/windows/fonts/arialuni.ttf","Identity-H", BaseFont.EMBEDDED);
                bf = BaseFont.CreateFont(pathfuente, "Identity-H", BaseFont.EMBEDDED);
            }
            catch (DocumentException e)
            {
                //e.pr();
                return new Font();
            }
            catch (IOException e)
            {
                //e.printStackTrace();
                return new Font();
            }
            return new Font(bf, size);
        }

        public bool IsRegistered(string fontname)
        {
            return false;
        }
    }


}
