using BSI.Integra.Aplicacion.Classes;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.BO;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Transversal.Helper;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    /// BO: Planificacion/PlantillaPW
    /// Autor: Fischer Valdez - Wilber Choque - Joao - Priscila Pacsi - Luis Huallpa - Carlos Crispin - Esthephany Tanco - Gian Miranda
    /// Fecha: 09/02/2021
    /// <summary>
    /// BO para la logica de las plantillas del portal web
    /// </summary>
    public class PlantillaPwBO : BaseBO
    {
        /// Propiedades	                                Significado
        /// -----------	                                ------------
        /// Nombre                                      Nombre de la plantilla
        /// Descripcion                                 Descripcion de la plantilla
        /// IdPlantillaMaestroPw                        Id de la plantilla maestro a la que pertenece (FK de la tabla pla.T_PlantillaMaestro_PW)
        /// IdRevisionPw                                Id de la revision de la plantilla (FK de la tabla pla.T_Revision_PW)
        /// IdPlantillaBase                             Id de la plantilla base (FK de la tabla pla.T_PlantillaBase)
        /// IdOportunidad                               Id de la oportunidad enlazada (FK de la tabla com.T_Oportunidad)
        /// IdPlantilla                                 Id de la plantilla (FK de la tabla mkt.T_Plantilla)
        /// IdMigracion                                 Id de migracion (nullable)
        /// EmailReemplazado                            Objeto con el contenido ya reemplazadas las etiquetas
        /// PlantillaRevisionPw                         Lista de objeto del tipo PlantillaRevisionPwBO
        /// PlantillaPlantillaMaestroPw                 Lista de objeto del tipo PlantillaPlantillaMaestroPwBO
        /// PlantillaPais                               Lista de objeto del tipo PlantillaPaisBO
        /// SeccionPw                                   Lista de objeto del tipo SeccionPwBO
        /// DatosEtiquetas                              Lista de objeto del tipo SeccionEtiquetaDTO
        /// ListaCursosRelacionados                     Lista de objeto del tipo CursosRelacionadosDTO
        /// ListaProblemasCausa                         Lista de objeto del tipo ProblemaCausaDTO
        /// UrlCursosRelacionados                       Lista de objeto del tipo PGeneralCursosRelacionadosDTO
        /// ListaTemplateV2ReemplazoEtiqueta            Lista de objeto del tipo TemplateV2ReemplazoEtiquetaDTO
        /// etiquetaMontosPagoPaquetes                  Cadena con la etiqueta de montos pago de paquetes
        /// cronogramaPagos                             Cadena con el cronograma de pagos
        /// FechaInicioPrograma                         Cadena con la fecha de inicio del programa
        /// DatosOportunidadAlumno                      Objeto de tipo OportunidadAlumnoDTO
        /// _repPlantillaClaveValor                     Repositorio de la tabla mkt.T_PlantillaClaveValor 
        /// _repPespecifico                             Repositorio de la tabla pla.T_PEspecifico
        /// _repMontoPago                               Repositorio de la tabla pla.T_MontoPago
        /// _repMoneda                                  Repositorio de la tabla pla.T_Moneda
        /// _repMontoPagoCronograma                     Repositorio de la tabla com.T_MontoPagoCronograma
        /// _repTipoDescuento                           Repositorio de la tabla pla.T_TipoDescuento
        /// _repMontoPagoCronogramaDetalle              Repositorio de la tabla com.T_MontoPagoCronogramaDetalle
        /// _repOportunidad                             Repositorio de la tabla com.T_Oportunidad
        /// _repAlumno                                  Repositorio de la tabla mkt.T_Alumno
        /// _repPgeneral                                Repositorio de la tabla pla.T_PGeneral
        /// _repPespecificoSesion                       Repositorio de la tabla pla.T_PEspecificoSesion
        /// _repDocumentoSeccionPw                      Repositorio de la tabla pla.T_DocumentoSeccion_PW
        /// _repPlantilla                               Repositorio de la tabla mkt.T_Plantilla
        /// _repPgeneralConfiguracionBeneficio          Repositorio de la tabla pla.T_ConfiguracionBeneficioProgramaGeneral
        /// _repVersionPrograma                         Repositorio de la tabla pla.T_VersionPrograma
        /// _repEtiqueta                                Repositorio de la tabla pla.T_Etiqueta
        /// Alumno                                      Objeto de tipo AlumnoBO
        /// Oportunidad                                 Objeto de tipo OportunidadBO
        /// Documentos                                  Objeto de tipo DocumentosBO
        /// ListadoEtiqueta                             Objeto de tipo ListadoEtiquetaBO

        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPlantillaMaestroPw { get; set; }
        public int IdRevisionPw { get; set; }
        public int IdPlantillaBase { get; set; }
        public int IdOportunidad { get; set; }
        public int IdPlantilla { get; set; }
        public Guid? IdMigracion { get; set; }
        public PlantillaEmailMandrillDTO EmailReemplazado;

        public List<PlantillaRevisionPwBO> PlantillaRevisionPw { get; set; }
        public List<PlantillaPlantillaMaestroPwBO> PlantillaPlantillaMaestroPw { get; set; }

        public List<PlantillaPaisBO> PlantillaPais { get; set; }
        public List<SeccionPwBO> SeccionPw { get; set; }
        public List<SeccionEtiquetaDTO> DatosEtiquetas;
        public List<CursosRelacionadosDTO> ListaCursosRelacionados;
        public List<ProblemaCausaDTO> ListaProblemasCausa;
        public List<PGeneralCursosRelacionadosDTO> UrlCursosRelacionados;
        public List<TemplateV2ReemplazoEtiquetaDTO> ListaTemplateV2ReemplazoEtiqueta;
        public string etiquetaMontosPagoPaquetes;
        public string cronogramaPagos;
        public string FechaInicioPrograma;

        public OportunidadAlumnoDTO DatosOportunidadAlumno { get; set; }

        private PlantillaClaveValorRepositorio _repPlantillaClaveValor;
        private PespecificoRepositorio _repPespecifico;
        private MontoPagoRepositorio _repMontoPago;
        private MonedaRepositorio _repMoneda;
        private MontoPagoCronogramaRepositorio _repMontoPagoCronograma;
        private TipoDescuentoRepositorio _repTipoDescuento;
        private MontoPagoCronogramaDetalleRepositorio _repMontoPagoCronogramaDetalle;
        private OportunidadRepositorio _repOportunidad;
        private AlumnoRepositorio _repAlumno;
        private PgeneralRepositorio _repPgeneral;
        private PespecificoSesionRepositorio _repPespecificoSesion;
        private DocumentoSeccionPwRepositorio _repDocumentoSeccionPw;
        private PlantillaRepositorio _repPlantilla;
        private PgeneralConfiguracionBeneficioRepositorio _repPgeneralConfiguracionBeneficio;
        private VersionProgramaRepositorio _repVersionPrograma;
        private EtiquetaRepositorio _repEtiqueta;
        private AlumnoBO Alumno;
        private OportunidadBO Oportunidad;

        private DocumentosBO Documentos;
        private ListadoEtiquetaBO ListadoEtiqueta;

        public PlantillaPwBO()
        {
            _repPlantillaClaveValor = new PlantillaClaveValorRepositorio();
            _repPespecifico = new PespecificoRepositorio();
            _repMontoPago = new MontoPagoRepositorio();
            _repMoneda = new MonedaRepositorio();
            _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio();
            _repTipoDescuento = new TipoDescuentoRepositorio();
            _repMontoPagoCronogramaDetalle = new MontoPagoCronogramaDetalleRepositorio();
            _repOportunidad = new OportunidadRepositorio();
            _repPgeneral = new PgeneralRepositorio();
            _repPespecificoSesion = new PespecificoSesionRepositorio();
            DatosOportunidadAlumno = new OportunidadAlumnoDTO();
            _repAlumno = new AlumnoRepositorio();
            _repDocumentoSeccionPw = new DocumentoSeccionPwRepositorio();
            EmailReemplazado = new PlantillaEmailMandrillDTO();
            _repPlantilla = new PlantillaRepositorio();
            _repVersionPrograma = new VersionProgramaRepositorio();
            _repPgeneralConfiguracionBeneficio = new PgeneralConfiguracionBeneficioRepositorio();
            _repEtiqueta = new EtiquetaRepositorio();
            Alumno = new AlumnoBO();
            Oportunidad = new OportunidadBO();
            ListadoEtiqueta = new ListadoEtiquetaBO();
            Documentos = new DocumentosBO();
        }

        public PlantillaPwBO(integraDBContext integraDBContext)
        {
            _repPlantillaClaveValor = new PlantillaClaveValorRepositorio(integraDBContext);
            _repPespecifico = new PespecificoRepositorio(integraDBContext);
            _repMontoPago = new MontoPagoRepositorio(integraDBContext);
            _repMoneda = new MonedaRepositorio(integraDBContext);
            _repMontoPagoCronograma = new MontoPagoCronogramaRepositorio(integraDBContext);
            _repTipoDescuento = new TipoDescuentoRepositorio(integraDBContext);
            _repMontoPagoCronogramaDetalle = new MontoPagoCronogramaDetalleRepositorio(integraDBContext);
            _repOportunidad = new OportunidadRepositorio(integraDBContext);
            _repPgeneral = new PgeneralRepositorio(integraDBContext);
            _repPespecificoSesion = new PespecificoSesionRepositorio(integraDBContext);
            _repVersionPrograma = new VersionProgramaRepositorio(integraDBContext);
            DatosOportunidadAlumno = new OportunidadAlumnoDTO();
            _repAlumno = new AlumnoRepositorio(integraDBContext);
            _repDocumentoSeccionPw = new DocumentoSeccionPwRepositorio(integraDBContext);
            _repPgeneralConfiguracionBeneficio = new PgeneralConfiguracionBeneficioRepositorio(integraDBContext);
            EmailReemplazado = new PlantillaEmailMandrillDTO();
            _repPlantilla = new PlantillaRepositorio(integraDBContext);
            _repEtiqueta = new EtiquetaRepositorio(integraDBContext);
            Documentos = new DocumentosBO(integraDBContext);
            Oportunidad = new OportunidadBO(integraDBContext);

            Alumno = new AlumnoBO(integraDBContext);
            ListadoEtiqueta = new ListadoEtiquetaBO(integraDBContext);
        }

        /// <summary>
        /// Obtiene Un Compuesto de Valores de Etiqueta para Remplazar en las Plantillas
        /// </summary>
        /// <param name="IdCentroCosto"></param>
        /// <param name="IdFaseOportunidad"></param>
        /// <param name="IdOportunidad"></param>
        public void GetValorEtiqueta(int idCentroCosto, int idFaseOportunidad, int idOportunidad)
        {
            DatosEtiquetas = _repPespecifico.ObtenerSeccionEtiqueta(idCentroCosto);

            //OportunidadBO oportunidad = _repOportunidad.FirstById(idOportunidad);
            Oportunidad = _repOportunidad.FirstById(idOportunidad);
            AlumnoBO alumno = _repAlumno.FirstById(Oportunidad.IdAlumno);

            Alumno.Id = alumno.Id;
            Alumno.IdCodigoPais = alumno.IdCodigoPais;

            //string _queryExpositores = "Select Nombre, Apellido, Nacionalidad,HojaVida from pla.ExpositoresPlantillasByCentroCosto Where IdCentroCosto=@IdCentroCosto";
            //var queryExpositores = _dapperRepository.QueryDapper(_queryExpositores, new { IdCentroCosto});
            //ExpositoresByCentroCosto = JsonConvert.DeserializeObject<List<ExpositoreByProgramaBO>>(queryExpositores);
            ListaProblemasCausa = _repPlantillaClaveValor.ObtenerPlantillaCausaProblema(idOportunidad);
            UrlCursosRelacionados = _repPlantillaClaveValor.ObtenerCursosRelacionadosPlantilla(idCentroCosto);
            //etiquetaMontosPagoPaquetes = etiquetaMontosPago(idOportunidad);
            int idPGeneral = _repPespecifico.FirstBy(x => x.IdCentroCosto == idCentroCosto).IdProgramaGeneral.Value;
            etiquetaMontosPagoPaquetes = etiquetaMontosPagoV2(idPGeneral);

            cronogramaPagos = Generarcronograma(idOportunidad);

            ListaTemplateV2ReemplazoEtiqueta = ObtenerTemplatesV2ReemplazoEtiqueta(idCentroCosto);

            // Logica nuevas etiquetas
            this.DatosOportunidadAlumno.OportunidadAlumno.Nombre1 = alumno.Nombre1;
            this.DatosOportunidadAlumno.OportunidadAlumno.NombreCompleto = alumno.NombreCompleto;
            this.DatosOportunidadAlumno.OportunidadAlumno.Email1 = alumno.Email1;
            this.DatosOportunidadAlumno.OportunidadAlumno.NroDocumento = alumno.Dni == null ? "" : alumno.Dni.ToUpper();
            this.DatosOportunidadAlumno.OportunidadAlumno.Direccion = alumno.Direccion == null ? "" : alumno.Direccion.ToUpper();
            this.DatosOportunidadAlumno.OportunidadAlumno.NombreCiudad = Alumno.NombreCiudadOrigen;
            this.DatosOportunidadAlumno.OportunidadAlumno.NombrePais = Alumno.NombrePaisOrigen;
            this.DatosOportunidadAlumno.OportunidadAlumno.IdCodigoPais = Alumno.IdCodigoPais;
            this.DatosOportunidadAlumno.CronogramaPagoCompleto = _repOportunidad.ObtenerCronogramaPagoCompleto(idOportunidad);
            this.DatosOportunidadAlumno.MontoTotal = _repOportunidad.ObtenerMontoTotal(idOportunidad);
            this.DatosOportunidadAlumno.Version = _repOportunidad.ObtenerVersion(idOportunidad);
            this.DatosOportunidadAlumno.IdPEspecifico = _repPespecifico.FirstBy(x => x.IdCentroCosto == Oportunidad.IdCentroCosto).Id;
        }

        /// <summary>
        /// Obtiene las etiquetas de V2 caso contrario devuelve V1 mediante el idCentroCosto
        /// </summary>
        /// <param name="idCentroCosto"></param>
        /// <returns>Etiquetas V2 en formato de lista</returns>
        public List<TemplateV2ReemplazoEtiquetaDTO> ObtenerTemplatesV2ReemplazoEtiqueta(int idCentroCosto)
        {
            const string ETIQUETAVACIO = "<vacio></vacio>";

            var listaResultado = new List<TemplateV2ReemplazoEtiquetaDTO>();
            string valor = string.Empty;

            var listaetiquetasV2 = _repEtiqueta.GetBy(x => x.IdNodoPadre == ValorEstatico.IdPlantillaMaestroTemplateV2).ToList();
            int idPGeneral = _repPespecifico.FirstBy(x => x.IdCentroCosto == idCentroCosto).IdProgramaGeneral.GetValueOrDefault();
            var listaSecciones = Documentos.ObtenerListaSeccionDocumentoProgramaGeneral(idPGeneral);
            var listaSeccionesDocumentoV2 = _repDocumentoSeccionPw.ObtenerDatosComplementariosProgramaGeneralV2(idPGeneral);

            foreach (var item in listaetiquetasV2)
            {
                valor = string.Empty;

                string[] array = item.Nombre.Split(".");
                string nombreSeccion = array[array.Length - 1];
                bool conTitulo = nombreSeccion == "Estructura Curricular";
                string descripcionAdicional = string.Concat("Descripci&#243;n ", nombreSeccion.Split(" ")[0]);

                var seccion = ListadoEtiqueta.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones.Where(x => x.Seccion == nombreSeccion).ToList(), conTitulo);

                valor = seccion.Aggregate(valor, (current, item01) => current + item01.Contenido);

                // Unir Descripcion adicional de etiquetas que tienen dicho contenido, previa verificacion
                if (listaSeccionesDocumentoV2.Exists(x => x.Titulo == descripcionAdicional))
                {
                    string descripcion = listaSeccionesDocumentoV2.First(x => x.Titulo == descripcionAdicional).Contenido;

                    valor += descripcion != ETIQUETAVACIO ? descripcion.Replace(ETIQUETAVACIO, string.Empty) : string.Empty;
                }

                // Sacar etiquetas no agrupadas de V2
                try
                {
                    valor += valor.Equals(string.Empty) ? listaSeccionesDocumentoV2.First(x => x.Titulo == nombreSeccion).Contenido : string.Empty;
                }
                catch (Exception e)
                {
                    valor += string.Empty;
                }

                // Obtener etiquetas de V1 si en caso no encuentra
                if (valor.Equals(string.Empty))
                {
                    nombreSeccion = nombreSeccion == "Certificacion" ? "Certificación" : nombreSeccion;
                    List<SeccionDocumentoDTO> seccionV1 = _repDocumentoSeccionPw.ObtenerSecciones(idPGeneral).Where(x => x.Titulo == nombreSeccion).ToList();

                    valor = seccionV1.Aggregate(valor, (current, item01) => current + item01.Contenido);
                }

                listaResultado.Add(new TemplateV2ReemplazoEtiquetaDTO()
                {
                    clave = item.Nombre,
                    valor = valor
                });
            }

            return listaResultado;
        }

        public List<ProgramaGeneralSeccionAnexosHTMLDTO> GenerarHTMLProgramaGeneralDocumentoSeccion(List<ProgramaGeneralSeccionDocumentoDTO> listaProgramaGeneralDocumentoSeccion, bool conTitulo)
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
                        if (conTitulo)
                            contenido += "<p><strong>" + detalleSeccion.Titulo + "</p></strong>";

                        contenido += "<p>" + detalleSeccion.Cabecera + "</p>";
                        contenido += "<ul>";
                        foreach (var contenidoSeccion in detalleSeccion.DetalleContenido)
                        {
                            contenido += "<li>" + contenidoSeccion + "</li>";
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

        public string ObtenerFechaInicioPrograma(int idProgramaGeneral, int idCentroCosto)
        {

            string resp = "";
            var pEspecifico = _repPespecifico.FirstBy(x => x.IdCentroCosto == idCentroCosto);

            if (pEspecifico.Tipo.ToLower() == "online asincronica")
            {
                DateTime fechaAOnline;
                if (DateTime.Now.Day > 25)
                {
                    fechaAOnline = DateTime.Now;
                }
                else
                {
                    fechaAOnline = DateTime.Now.AddDays(5);
                }
                resp = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fechaAOnline.ToString("MMMM yyyy"));
            }
            else
            {
                var dato_fecha = _repPespecifico.FechaProgramaEspecifico(idProgramaGeneral, pEspecifico.Id);

                if (dato_fecha != null)
                {
                    resp = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dato_fecha.FechaHoraInicio);
                }
                else
                {
                    resp = "Por definir";
                }
            }
            return resp;
        }
        /// <summary>
        /// Genera Cronograma de Cuotas Para Speech
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        private string Generarcronograma(int idOportunidad)
        {
            string returnn = "";
            var rptaExiste = _repMontoPagoCronograma.FirstBy(w => w.IdOportunidad == idOportunidad);

            if (rptaExiste != null)
            {
                if (rptaExiste.Formula != 5)
                {
                    var coutas = _repMontoPago.ObtenerMontoPagoPorIdOportunidadV2(idOportunidad);
                    var rpt = GetCronogramaDetalleValidado(coutas, idOportunidad);
                    returnn = GeneraCronogramaHtml(rpt);
                }
            }
            return returnn;
        }

        private string GeneraCronogramaHtml(MontoPagoCronogramasDetalleCompuestoDTO Lista)
        {
            string tabla = "";

            string moneda = string.Empty;

            if (Lista != null)
            {
                if (!(string.IsNullOrEmpty(Lista.cronograma.NombrePlural)))
                {
                    switch (Lista.cronograma.NombrePlural.ToLower())
                    {
                        case "soles":
                            moneda = "S/. ";
                            break;
                        case "dolares":
                            moneda = "U$S ";
                            break;
                        case "pesos":
                            moneda = "COL $ ";
                            break;
                        default:
                            moneda = "";
                            break;
                    }
                }
                else
                {
                    moneda = "";
                }

                tabla = "<TABLE BORDER CELLPADDING=2 CELLSPACING=0 style='border: 1px solid #E6E6E6;border-collapse: collapse;'>";
                tabla += "<tr>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Descripcion </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Fecha pago </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Monto cuota con descuento </th>";
                tabla += "</tr>";

                foreach (var re in Lista.listaDetalle)
                {
                    tabla += "<tr>";
                    tabla += "<td style='border: 1px solid #E6E6E6' >" + re.CuotaDescripcion + "</td>";
                    tabla += "<td style='border: 1px solid #E6E6E6'> " + re.FechaPago.ToString("dd/MM/yyyy") + "</td>";
                    tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + moneda + re.MontoCuotaDescuento + "</td>";
                    tabla += "</tr>";
                }
                tabla += "</TABLE>";
            }
            return tabla;
        }

        /// <summary>
        /// Genera Detalle de Cronogrma Para Cuotas Specch
        /// </summary>
        /// <param name="coutas"></param>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        private MontoPagoCronogramasDetalleCompuestoDTO GetCronogramaDetalleValidado(List<MontoPagoCronogramaCompuestoDTO> coutas, int idOportunidad)
        {
            MontoPagoCronogramasDetalleCompuestoDTO rpta = new MontoPagoCronogramasDetalleCompuestoDTO();
            var montoPagoCronograma = _repMontoPagoCronograma.GetBy(w => w.Estado == true && w.IdOportunidad == idOportunidad);
            var tipoDescuento = _repTipoDescuento.GetAll();
            var entityDto = (from t1 in montoPagoCronograma
                             join t2 in tipoDescuento on t1.IdTipoDescuento equals t2.Id
                             join t3 in coutas on t1.IdMontoPago equals t3.Id
                             select new MontoPagoCronogramaCompuestoDTO
                             {
                                 Id = t3.Id,
                                 mp_precio = t3.mp_precio,
                                 mp_precio_letras = t3.mp_precio_letras,
                                 mp_moneda = t3.mp_moneda.ToString(),
                                 mp_matricula = t3.mp_matricula,
                                 mp_cuotas = t3.mp_cuotas,
                                 mp_nro_cuotas = t3.mp_nro_cuotas,
                                 id_programa = t3.id_programa,
                                 id_tp = t3.id_tp,
                                 id_pais = t3.id_pais,
                                 mp_vencimiento = t3.mp_vencimiento,
                                 mp_primeraCuota = t3.mp_primeraCuota,
                                 mp_cuotaDoble = t3.mp_cuotaDoble,
                                 id_tipo_descuento = t1.IdTipoDescuento,
                                 mp_precioDescuento = t1.PrecioDescuento,
                                 id_cronograma = t1.Id,
                                 is_aprobado = t1.EsAprobado,
                                 NombrePlural = t1.NombrePlural,
                                 tp_formula = t2.Formula,
                                 tp_porcentaje_general = t2.PorcentajeGeneral.Value,
                                 tp_porcentaje_matricula = t2.PorcentajeMatricula.Value,
                                 tp_fracciones_matricula = t2.FraccionesMatricula.Value,
                                 tp_porcentaje_cuotas = t2.PorcentajeCuotas.Value,
                                 tp_cuotas_adicionales = t2.CuotasAdicionales.Value,
                                 matriculaEnProceso = t1.MatriculaEnProceso,
                                 Simbolo = t3.Simbolo,
                                 CodigoMatricula = t1.CodigoMatricula
                             }).FirstOrDefault();
            if (entityDto != null)
            {
                var Lista = _repMontoPagoCronogramaDetalle.GetBy(x => x.IdMontoPagoCronograma == entityDto.id_cronograma && !x.Pagado).OrderBy(x => x.NumeroCuota).ToList();
                List<MontoPagoCronogramaDetalleDTO> listaDetalle = new List<MontoPagoCronogramaDetalleDTO>();
                foreach (var item in Lista)
                {
                    MontoPagoCronogramaDetalleDTO detalle = new MontoPagoCronogramaDetalleDTO()
                    {
                        Id = item.Id,
                        NumeroCuota = item.NumeroCuota,
                        CuotaDescripcion = item.CuotaDescripcion,
                        MontoCuota = item.MontoCuota,
                        FechaPago = item.FechaPago,
                        MontoCuotaDescuento = item.MontoCuotaDescuento,
                        Pagado = item.Pagado,
                        Matricula = item.Matricula,
                        Cronograma = item.IdMontoPagoCronograma.ToString()
                    };
                    listaDetalle.Add(detalle);
                }

                rpta.cronograma = entityDto;
                rpta.listaDetalle = listaDetalle;
                return rpta;
            }
            return null;
        }

        /// <summary>
        /// Obtiene Los Valores Para Etiquetas Lista de Programas
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="idAreaEtiqueta">Id del area de la etiqueta (PK de la tabla mkt.T_AreaCampoEtiqueta)</param>
        /// <returns>String</returns>
        public string GetValorEtiquetaListas(int idOportunidad, int idAreaEtiqueta)
        {
            string result = "";
            int contador = 1;
            ListaCursosRelacionados = _repPlantillaClaveValor.ObtenerMontosCursosRelacionados(idOportunidad, idAreaEtiqueta);

            foreach (var item in ListaCursosRelacionados)
            {
                string url_video = "";

                if (item.Url_Video != null)
                {
                    url_video = "<a href='https://" + item.Url_Video + "' target = '_blank' >" + "Ver Presentaci&oacute;n" + "</a>"; //item.Presentacion
                }

                result = result + "<p><b>" + contador.ToString() + ". " + item.Nombre + "</b></p>";
                result = result + "<p><b>Modalidad: </b>" + item.Modalidad + " " + "<b> Duraci&oacute;n: </b>" + " " + item.Duracion + "<br/>";
                result = result + "<b>Presentaci&oacute;n: </b>" + url_video + "<br/>";
                result = result + "<b>Inversi&oacute;n Desde: </b>" + item.Inversion + "<br/>";
                contador++;
            }

            return result;
        }


        /// <summary>
        /// Obtiene Una Tabla en Html de Monto Pago Segun Oportunidad V2
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        private string obtenerMontosPagoPaquetesV2(int idPGeneral)
        {
            List<MontoPagoEtiquetaDTO> versiones = _repMontoPago.ObtenerVersionesMontoPagoV2(Oportunidad.Id);

            if (versiones.Count() == 0)
                return null;
            else
            {
                PespecificoBO PEspecifico = _repPespecifico.FirstBy(x => x.IdCentroCosto == Oportunidad.IdCentroCosto);

                List<VersionProgramaDTO> VersionPrograma = _repVersionPrograma.ObtenerTodo();
                List<string> listaBeneficios;

                string tabla = "";
                int contadorBeneficios = 0;

                tabla = "<table border cellpadding=2 cellspacing=0 style='border: 1px solid #E6E6E6;border-collapse: collapse;'>";
                tabla += "<tr>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Versión </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Beneficios </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Tipo pago </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Inversión </th>";
                tabla += "</tr>";

                foreach (VersionProgramaDTO item in VersionPrograma)
                {
                    listaBeneficios = Documentos.ObtenerBeneficiosConfiguradosProgramaGeneral(PEspecifico.IdProgramaGeneral.Value, Alumno.IdCodigoPais, item.Id);
                    contadorBeneficios += listaBeneficios.Count;

                    if (listaBeneficios.Count > 0)
                    {
                        List<MontoPagoEtiquetaDTO> infoVersiones = versiones.Where(x => x.Paquete == item.Id).ToList();

                        tabla += "<tr>";

                        tabla += $"<td style='border: 1px solid #E6E6E6' rowspan='{infoVersiones.Count}'>{item.Nombre}</td>";

                        tabla += $"<td style='border: 1px solid #E6E6E6' rowspan='{infoVersiones.Count}'><ul>";

                        foreach (string beneficio in listaBeneficios)
                        {
                            tabla += $"<li>{beneficio}</li>";
                        }

                        tabla += "</ul></td>";
                        int i = 0;

                        foreach (var re in infoVersiones)
                        {
                            tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + re.tp_nombre + "</td>";
                            tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + (re.tp_cuotas == 2 ? re.Simbolo.Replace(".", " ") + re.mp_precio.ToString()
                            : "1 matricula de: " + re.Simbolo.Replace(".", "") + re.mp_matricula + " y " + re.mp_nro_cuotas + " cuotas de " + re.Simbolo.Replace(".", "") + re.mp_cuotas)
                            + "</td>";

                            i += 1;

                            if (i < infoVersiones.Count)
                                tabla += "</tr><tr>";
                        }

                        tabla += "</tr>";
                    }
                }

                tabla += "</table>";

                var pieBeneficio = _repPgeneral.SeccionIndividualPGeneral(idPGeneral, "Beneficios");

                if (pieBeneficio != null)
                    tabla += $"<p>{_repPgeneral.SeccionIndividualPGeneral(idPGeneral, "Beneficios").PiePagina}</p>";

                if (contadorBeneficios == 0)
                    tabla = null;

                return tabla;
            }
        }

        /// <summary>
        /// Obtiene El Valor de la Etiqueta MontoPago V2 para las Plantillas
        /// </summary>
        /// <param name="Oportunidad"></param>
        /// <returns></returns>
        public string etiquetaMontosPagoV2(int idPGeneral)
        {
            string valorTemporal = obtenerMontosPagoPaquetesV2(idPGeneral);

            return valorTemporal ?? etiquetaMontosPago(Oportunidad.Id);
        }

        /// <summary>
        /// Obtiene El Valor de la Etiqueta MontoPago para las Plantillas
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public string etiquetaMontosPago(int idOportunidad)
        {
            var tabla = getMontosPagoPaquetes(idOportunidad);

            if (tabla == null)
            {
                string precio_normal = string.Empty;

                string precio_contado = getPrecioContado(idOportunidad);
                string precio_coutas = getPrecioCuotas(idOportunidad);

                string precio_descuento_cuotas = null;
                string precio_descuento_contado = null;

                if (!string.IsNullOrEmpty(precio_contado))
                {
                    precio_normal = "<b>Al Contado: </b>" + precio_contado;

                }

                if (!string.IsNullOrEmpty(precio_coutas))
                {
                    precio_normal += "<div style=' height:1px;'></div>" + "<b>Financiamiento: </b>" + precio_coutas;
                }

                return precio_normal;
            }
            else
            {
                return tabla;
            }
        }

        /// <summary>
        /// Genera una tabla para Precio en Cuotas de un Programa
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        private string getPrecioCuotas(int idOportunidad)
        {
            string returnn = "";
            var montopagoCuotas = _repMontoPago.ObtenerMontoPagoPorIdOportunidad(idOportunidad);
            if (montopagoCuotas != null)
            {
                returnn = generateGridCronogamaPagos(montopagoCuotas);
            }

            return returnn;
        }
        /// <summary>
        /// Genera una Tabla Para el Precio al Contado de un Programa 
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public string getPrecioContado(int idOportunidad)
        {
            string returnn = "";
            var contado = _repMontoPago.ObtenerMontoPagoContadoPorIdOportunidad(idOportunidad);

            if (contado != null)
            {
                returnn = generateGridCronogamaPagos(contado);
            }

            return returnn;
        }
        /// <summary>
        /// Genera Tablas Depagos deacuerdo a Formua
        /// </summary>
        /// <param name="data">Datos Compuestos de monto Pago </param>
        /// <returns></returns>
        public string generateGridCronogamaPagos(MontoPagoCompuestoDTO data)
        {
            string tablaRespuesta = "";
            switch (data.tp_formula)
            {
                case 0://sin descuento                     
                    tablaRespuesta = GeneraHtmlPrecioCuotas(data);
                    break;
                case 1: //matricula
                    tablaRespuesta = GeneraHtml(_generarGridMatricula(data));
                    break;
                case 2: //cuotas
                    tablaRespuesta = GeneraHtmlPrecioCuotas(data);
                    break;
                case 3: //ambos
                    tablaRespuesta = GeneraHtml(_generarGridAmbos(data));
                    break;
                case 4: //general
                    tablaRespuesta = GeneraHtml(_generarGridGeneral(data));
                    break;
                case 5:
                    tablaRespuesta = GeneraHtmlPrecioContado(_generarGridNormal(data));
                    break;
            }
            return tablaRespuesta;
        }


        private List<PagoCuotaDTO> _generarGridNormal(MontoPagoCompuestoDTO data)
        {
            string simbolo = "";

            var respuesta = _repMoneda.ObtenerMonedaPorId(data.mp_moneda);

            if (respuesta != null)
            {
                simbolo = respuesta.Simbolo;
            }
            List<PagoCuotaDTO> lista = new List<PagoCuotaDTO>();
            PagoCuotaDTO obj = new PagoCuotaDTO();
            obj.numeroCuota = 1;
            obj.cuotaDescripcion = "Contado";
            obj.montoCuota = data.mp_cuotas;
            DateTime fpag = DateTime.Now;
            //fpag = fpag.AddMonths(1);
            obj.fechapago = fpag;
            obj.montoCuotaDescuento = float.Parse(data.mp_cuotas.ToString());
            obj.ispagado = false;
            obj.es_matricula = true;
            obj.SimboloMoneda = simbolo;
            lista.Add(obj);
            return lista;
        }
        /// <summary>
        /// Genera Una Tabla Con las Cuotas de Pago
        /// </summary>
        /// <param name="Lista">campos de Cuotas Para Generar tabla</param>
        /// <returns></returns>
        public string GeneraHtml(List<PagoCuotaDTO> Lista)
        {
            string tabla = "";
            tabla = "<TABLE BORDER CELLPADDING=2 CELLSPACING=0 style='border: 1px solid #E6E6E6;border-collapse: collapse;'>";
            tabla += "<tr>";
            tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Descripcion </th>";
            tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Fecha pago </th>";
            tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Monto cuota con descuento </th>";
            tabla += "</tr>";
            foreach (var re in Lista)
            {
                tabla += "<tr>";
                tabla += "<td style='border: 1px solid #E6E6E6' >" + re.cuotaDescripcion + "</td>";
                tabla += "<td style='border: 1px solid #E6E6E6'> " + re.fechapago.ToShortDateString() + "</td>";
                tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + re.montoCuotaDescuento + "</td>";
                tabla += "</tr>";
            }
            tabla += "</TABLE>";
            return tabla;
        }
        /// <summary>
        /// Genera Html del Precio Contado
        /// </summary>
        /// <param name="Lista"></param>
        /// <returns></returns>
        private string GeneraHtmlPrecioContado(List<PagoCuotaDTO> Lista)
        {
            string tabla = "";
            tabla = Lista[0].SimboloMoneda.Replace(".", " ") + " " + Lista[0].montoCuota.ToString();
            return tabla;
        }
        /// <summary>
        /// Genera Tabla de Cuotas de Pago de un Programa
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private List<PagoCuotaDTO> _generarGridGeneral(MontoPagoCompuestoDTO data)
        {
            string test = "";
            var tamanio = 0;
            var tamanioContador = 0;
            tamanio = data.mp_nro_cuotas;
            var numeroco = 1;
            List<PagoCuotaDTO> lista = new List<PagoCuotaDTO>();
            PagoCuotaDTO obj = new PagoCuotaDTO();
            obj.numeroCuota = numeroco;
            obj.cuotaDescripcion = "Matricula ";
            obj.montoCuota = data.mp_matricula;
            DateTime fpag = DateTime.Now;
            fpag = fpag.AddMonths(1);
            obj.fechapago = fpag;
            obj.montoCuotaDescuento = _tipoDescuentoGeneral(data.mp_matricula, data.tp_porcentaje_general);
            obj.ispagado = false;
            obj.es_matricula = true;
            lista.Add(obj);
            for (var i = 0; i < tamanio; i++)
            {
                PagoCuotaDTO obj1 = new PagoCuotaDTO();
                numeroco = numeroco + 1;
                tamanioContador = tamanioContador + 1;
                obj1.numeroCuota = numeroco;
                obj1.cuotaDescripcion = "Cuota - " + (numeroco - 1);
                obj1.montoCuota = data.mp_cuotas;
                obj1.montoCuotaDescuento = _tipoDescuentoGeneral(data.mp_cuotas, data.tp_porcentaje_general);
                obj1.ispagado = false;
                obj1.es_matricula = false;
                DateTime fecha = _calcularFechaInicial(data, i);
                obj1.fechapago = fecha;
                lista.Add(obj1);
                if (tamanioContador != tamanio)
                {
                    var mes = fecha.Month;
                    if (data.mp_cuotaDoble && (mes == 7 || mes == 12))
                    {
                        PagoCuotaDTO obj2 = new PagoCuotaDTO();
                        numeroco = numeroco + 1;
                        obj2.numeroCuota = numeroco;
                        obj2.cuotaDescripcion = "Cuota - " + (numeroco - 1);
                        obj2.montoCuota = data.mp_cuotas;
                        obj2.montoCuotaDescuento = _tipoDescuentoGeneral(data.mp_cuotas, data.tp_porcentaje_general);
                        obj2.es_matricula = false;
                        obj2.fechapago = _calcularFechaInicial(data, i);
                        lista.Add(obj2);
                        tamanio = tamanio - 1;
                    }
                }
            }
            return lista;
        }
        /// <summary>
        /// Genera Tabla De monto Pago pa Contado y Credito
        /// </summary>
        /// <param name="data">datos Coumpuestos de MontoPago</param>
        /// <returns></returns>
        private List<PagoCuotaDTO> _generarGridAmbos(MontoPagoCompuestoDTO data)
        {
            var numeroco = 1;
            List<PagoCuotaDTO> lista = new List<PagoCuotaDTO>();
            //matriculas///////////////////////////////////////////////////7
            var tamanioMatricula = 0;
            tamanioMatricula = data.tp_fracciones_matricula;

            if (tamanioMatricula == 0) tamanioMatricula = 1;
            for (var j = 0; j < tamanioMatricula; j++)
            {
                PagoCuotaDTO obj = new PagoCuotaDTO();
                obj.numeroCuota = numeroco;
                obj.cuotaDescripcion = "Matricula " + (numeroco);
                obj.montoCuota = data.mp_matricula;
                DateTime currentDate = DateTime.Now;
                obj.fechapago = currentDate;
                obj.montoCuotaDescuento = _tipoDescuentoGeneral((data.mp_matricula / tamanioMatricula), data.tp_porcentaje_matricula);
                obj.ispagado = false;
                obj.es_matricula = true;
                lista.Add(obj);
                numeroco = numeroco + 1;
            }

            /////cuotas///////////////////////////////////////////////////
            var tamanio = 0;
            var tamanioContador = 0;
            tamanio = data.mp_nro_cuotas + data.tp_cuotas_adicionales;
            numeroco = numeroco - 1;
            var tamaniocuotas = tamanio;
            var sindescuento = data.mp_precio - data.mp_matricula;

            for (var i = 0; i < tamanio; i++)
            {
                PagoCuotaDTO obj = new PagoCuotaDTO();
                numeroco = numeroco + 1;
                tamanioContador = tamanioContador + 1;
                obj.numeroCuota = numeroco;
                obj.cuotaDescripcion = "Cuota - " + (numeroco);
                obj.montoCuota = data.mp_cuotas;
                obj.montoCuotaDescuento = _tipoDescuentoGeneral(sindescuento / tamaniocuotas, data.tp_porcentaje_cuotas);
                obj.ispagado = false;
                obj.es_matricula = false;
                DateTime fecha = _calcularFechaInicial(data, i);
                obj.fechapago = fecha;
                lista.Add(obj);
                if (tamanioContador != tamanio)
                {
                    var mes = fecha.Month + 1;
                    if (data.mp_cuotaDoble && (mes == 7 || mes == 12))
                    {
                        PagoCuotaDTO obj1 = new PagoCuotaDTO();
                        numeroco = numeroco + 1;
                        obj1.numeroCuota = numeroco;
                        obj1.cuotaDescripcion = "Cuota - " + (numeroco);
                        obj1.montoCuota = data.mp_cuotas;
                        obj1.montoCuotaDescuento = _tipoDescuentoGeneral(sindescuento / tamaniocuotas, data.tp_porcentaje_cuotas);
                        obj.es_matricula = false;
                        obj1.fechapago = _calcularFechaInicial(data, i);
                        lista.Add(obj1);
                        tamanio = tamanio - 1;
                    }
                }
            }
            return lista;
        }

        public string DuracionYHorarios(int? idCentroCosto)
        {
            string result = "";
            var pEspecifico = _repPespecifico.GetBy(w => w.IdCentroCosto == idCentroCosto && w.Estado == true).FirstOrDefault();
            string Encabezado = "<p><span style='font-size: 10pt; font-family: helvetica, arial, sans-serif; color: #000000;'><strong>DURACIÓN Y HORARIOS:</strong></span></p>";
            string presencial_datos = "<p><strong>En la modalidad Presencial:</strong></p><p> El ##CURSODIPLOMA## se desarrolla  el siguiente horario: (*)</p>";
            string sincronico_datos = "<p><strong>En la modalidad Online:</strong></p><p> El ##CURSODIPLOMA## tiene una duración de ##DURACIONPE## cronológicas . Las clases se desarrollarán de forma virtual,con una frecuencia ##FRECUENCIA## en el siguiente horario: (*)</p>";
            string extra_presencial = "<p><i> (*)Para más detalle sobre fechas y horarios solicite su cronograma de alumnos.</i><p/>";
            string extra_sincrono = "<p><i> (*)Para más detalle sobre fechas y horarios solicite su cronograma de alumnos.</i><p/>";

            int IdPG = pEspecifico.IdProgramaGeneral.Value;
            int IdPESP = pEspecifico.Id;
            var PG = _repPgeneral.FirstById(IdPG);
            //var sessiones = _repPespecificoSesion.GetFiltered(x => x.PEspecificoId == pEspecifico.id);

            List<PespecificoBO> lista_temp_pe = new List<PespecificoBO>();
            PespecificoBO item_temo_pe = new PespecificoBO();
            //foreach (var pe in listaEspecifico)
            //{eliminado era lista ahora sera un solo item
            if (pEspecifico.Tipo == "Online Asincronica" || _repPespecificoSesion.GetBy(w => w.IdPespecifico == pEspecifico.Id).FirstOrDefault() == null)
            {
            }
            if (!lista_temp_pe.Any(w => w.Ciudad == pEspecifico.Ciudad))
            {
                lista_temp_pe.Add(pEspecifico);
            }
            else
            {
                item_temo_pe = lista_temp_pe.Where(w => w.Ciudad == pEspecifico.Ciudad).FirstOrDefault();
                if (item_temo_pe.Tipo != pEspecifico.Tipo)
                {
                }

                var item_temo_pe_primersesion = _repPespecificoSesion.GetBy(w => w.IdPespecifico == item_temo_pe.Id).OrderBy(w => w.FechaHoraInicio).First();
                if (item_temo_pe_primersesion == null)
                {
                    //nada porque no tiene sesiones
                }
                else
                {
                    if (item_temo_pe_primersesion.FechaHoraInicio < _repPespecificoSesion.GetBy(w => w.IdPespecifico == pEspecifico.Id).OrderBy(w => w.FechaHoraInicio).First().FechaHoraInicio)
                    {
                        //nada porque la fecha de este PE que tengo es menor  en su fecha de su primera sesion
                    }
                    else
                    {
                        lista_temp_pe.Remove(item_temo_pe);
                        lista_temp_pe.Add(pEspecifico);
                    }
                }

            }
            //}foreach - eliminado era lista ahora sera un solo item


            //var PEsp = lista_temp_pe;
            //string frecuencia = "";

            ////funcion que devulve el html de presencial
            //string todohtmlpresencial = ObtenerContenidoPresencial(PEsp.Where(w => w.Tipo == "Presencial").ToList(), true);
            //if (todohtmlpresencial == "")
            //{
            //    presencial_datos = "";
            //    extra_presencial = "";
            //}
            //string todohtmlsincrono = InformacionProgramaBO.ObtenerContenidoPresencial(PEsp.Where(w => w.Tipo == "Online Sincronica").ToList(), false);
            //if (todohtmlsincrono == "")
            //{
            //    sincronico_datos = "";
            //    extra_sincrono = "";
            //}
            //else
            //{
            //    var sincrona = PEsp.Where(x => x.tipo == "Online Sincronica").FirstOrDefault();
            //    if (sincrona.nombre.Contains("Curso"))
            //    {
            //        var padre = _itpla_pespecificopadrepespecificohijorepository.GetFiltered(w => w.PEspecificoHijoId == sincrona.id).FirstOrDefault();
            //        if (padre != null)
            //            frecuencia = util.ObtenerFrecuencia(padre.PEspecificoPadreId);
            //        else
            //            frecuencia = util.ObtenerFrecuencia(sincrona.id);
            //    }
            //    else
            //        frecuencia = util.ObtenerFrecuencia(sincrona.id);

            //}
            //result = Encabezado + presencial_datos + todohtmlpresencial + extra_presencial + sincronico_datos + todohtmlsincrono + extra_sincrono;
            //result = result.Replace("##DURACIONPE##", PEsp.FirstOrDefault().Duracion + " Horas").Replace("##DURACIONPG##", PG.pw_duracion).Replace("##CURSODIPLOMA##", PG.Nombre).Replace("##FRECUENCIA##", frecuencia);

            return result;
        }

        public string ObtenerCostoTotalConDescuento(int idOportunidad)
        {
            string result = "";
            var Schedule = _repMontoPagoCronograma.FirstBy(w => w.IdOportunidad == idOportunidad, y => new { y.PrecioDescuento, y.IdMoneda });
            if (Schedule != null)
            {
                var paymentCurrency = _repMoneda.FirstBy(w => w.Id == Schedule.IdMoneda, y => new { y.Simbolo, y.NombrePlural });
                result = paymentCurrency.Simbolo + Schedule.PrecioDescuento + " " + paymentCurrency.NombrePlural;
            }
            return result;
        }

        public void ObtenerDatosProgramaGeneral(int idProgramaGeneral)
        {
            var existe = _repPgeneral.Exist(idProgramaGeneral);
            if (existe)
            {
                var pGeneral = _repPgeneral.FirstById(idProgramaGeneral);
                this.DatosOportunidadAlumno.DuracionMesesPGeneral = pGeneral.PwDuracion;
                this.DatosOportunidadAlumno.NombrePGeneral = pGeneral.Nombre;

                var seccionesAntiguas = _repDocumentoSeccionPw.ObtenerSecciones(idProgramaGeneral);

                List<ProgramaGeneralSeccionDocumentoDTO> listaSecciones = Documentos.ObtenerListaSeccionDocumentoProgramaGeneral(idProgramaGeneral);
                var seccionEstructura = Documentos.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones);

                string anexo1EstructuraCurricular = string.Empty;
                string anexo2Certificacion = string.Empty;

                var estructuraV2 = seccionEstructura.Where(x => x.Seccion.ToLower() == "estructura curricular").FirstOrDefault();
                try
                {
                    if (estructuraV2 != null)
                    {
                        anexo1EstructuraCurricular = "<strong>" + estructuraV2.Seccion + "</strong><br>";
                        anexo1EstructuraCurricular += estructuraV2.Contenido;
                        anexo1EstructuraCurricular = anexo1EstructuraCurricular.Replace("&bull;&nbsp;&nbsp;&nbsp;", "");
                    }
                    else
                    {
                        var resultadoEstructuraCurricular = seccionesAntiguas.Where(x => x.Titulo.Contains("Estructura Curricular")).FirstOrDefault();

                        anexo1EstructuraCurricular = string.Empty;
                        if (resultadoEstructuraCurricular != null)
                        {
                            anexo1EstructuraCurricular = "<h2>" + resultadoEstructuraCurricular.Titulo + "</h2>";
                            anexo1EstructuraCurricular += resultadoEstructuraCurricular.Contenido;
                        }
                    }

                    this.DatosOportunidadAlumno.Anexo1EstructuraCurricular = anexo1EstructuraCurricular;
                }
                catch (Exception ex)
                {
                    var resultadoEstructuraCurricular = seccionesAntiguas.Where(x => x.Titulo.Contains("Estructura Curricular")).FirstOrDefault();

                    anexo1EstructuraCurricular = string.Empty;
                    if (resultadoEstructuraCurricular != null)
                    {
                        anexo1EstructuraCurricular = "<h2>" + resultadoEstructuraCurricular.Titulo + "</h2>";
                        anexo1EstructuraCurricular += resultadoEstructuraCurricular.Contenido;
                    }

                    this.DatosOportunidadAlumno.Anexo1EstructuraCurricular = anexo1EstructuraCurricular;
                }


                try
                {
                    var certificacionV2 = seccionEstructura.Where(x => x.Seccion.ToLower() == "certificacion").FirstOrDefault();
                    if (certificacionV2 != null)
                    {
                        anexo2Certificacion = "<strong>" + certificacionV2.Seccion + "</strong><br>";
                        anexo2Certificacion += certificacionV2.Contenido;
                        anexo2Certificacion = anexo2Certificacion.Replace("&bull;&nbsp;&nbsp;&nbsp;", "");
                    }
                    else
                    {
                        var resultadoCertificacion = seccionesAntiguas.Where(x => x.Titulo.Contains("Certificaci&#243;n")).FirstOrDefault();

                        anexo2Certificacion = string.Empty;
                        if (anexo2Certificacion != null)
                        {
                            anexo2Certificacion = "<h2>" + resultadoCertificacion.Titulo + "</h2>";
                            anexo2Certificacion += resultadoCertificacion.Contenido;
                        }
                    }

                    this.DatosOportunidadAlumno.Anexo2Certificacion = anexo2Certificacion;
                }
                catch (Exception ex)
                {
                    var resultadoCertificacion = seccionesAntiguas.Where(x => x.Titulo.Contains("Certificación")).FirstOrDefault();

                    anexo2Certificacion = string.Empty;
                    if (anexo2Certificacion != null)
                    {
                        anexo2Certificacion = "<h2>" + resultadoCertificacion.Titulo + "</h2>";
                        anexo2Certificacion += resultadoCertificacion.Contenido;
                    }

                    this.DatosOportunidadAlumno.Anexo2Certificacion = anexo2Certificacion;
                }                
            }
        }

        public DatosOportunidadDocumentosCompuestoDTO ObtenerDatosOportunidad(int idOportunidad)
        {
            return _repOportunidad.ObtenerDatosCompuestosPorIdOportunidad(idOportunidad);
        }

        private List<PagoCuotaDTO> _generarGridMatricula(MontoPagoCompuestoDTO data)
        {
            var numeroco = 1;
            List<PagoCuotaDTO> lista = new List<PagoCuotaDTO>();
            DateTime fecha = new DateTime();
            var tamanioMatricula = 0;
            tamanioMatricula = data.tp_fracciones_matricula;
            if (tamanioMatricula == 0) tamanioMatricula = 1;

            for (var j = 0; j < tamanioMatricula; j++)
            {
                PagoCuotaDTO obj = new PagoCuotaDTO();
                obj.numeroCuota = numeroco;
                obj.cuotaDescripcion = "Matricula " + (numeroco);
                obj.montoCuota = data.mp_matricula;
                DateTime currentDate = DateTime.Now;
                obj.fechapago = currentDate;
                obj.montoCuotaDescuento = _tipoDescuentoGeneral((data.mp_matricula / tamanioMatricula), data.tp_porcentaje_matricula);
                obj.ispagado = false;
                obj.es_matricula = true;
                lista.Add(obj);
                numeroco = numeroco + 1;
            }
            /////cuotas///////////////////////////////////////////////////
            var tamaniocuotas = 0;
            var tamanioContador = 0;
            var tamanio = data.mp_nro_cuotas;
            numeroco = numeroco - 1;
            tamaniocuotas = tamanio;
            for (var i = 0; i < tamanio; i++)
            {
                PagoCuotaDTO obj = new PagoCuotaDTO();
                numeroco = numeroco + 1;
                tamanioContador = tamanioContador + 1;
                obj.numeroCuota = numeroco;
                obj.cuotaDescripcion = "Cuota - " + (numeroco);
                obj.montoCuota = data.mp_cuotas;
                obj.montoCuotaDescuento = _tipoDescuentoGeneral(data.mp_cuotas, data.tp_porcentaje_cuotas);
                obj.ispagado = false;
                obj.es_matricula = false;
                fecha = _calcularFechaInicial(data, i);
                obj.fechapago = fecha;
                lista.Add(obj);
                if (tamanioContador != tamanio)
                {
                    var mes = fecha.Month;
                    if (data.mp_cuotaDoble && (mes == 7 || mes == 12))
                    {
                        PagoCuotaDTO obj1 = new PagoCuotaDTO();
                        numeroco = numeroco + 1;
                        obj1.numeroCuota = numeroco;
                        obj1.cuotaDescripcion = "Cuota - " + (numeroco);
                        obj1.montoCuota = data.mp_cuotas;
                        obj1.montoCuotaDescuento = _tipoDescuentoGeneral(data.mp_cuotas, data.tp_porcentaje_cuotas);
                        obj.es_matricula = false;
                        obj1.fechapago = _calcularFechaInicial(data, i);
                        lista.Add(obj1);
                        tamanio = tamanio - 1;
                    }
                }
            }
            return lista;
        }
        /// <summary>
        /// Calcula FechaInicial de la Primera Cuota
        /// </summary>
        /// <param name="obj"> Datos Compuestos de MontoPago</param>
        /// <param name="i"></param>
        /// <returns></returns>
        private DateTime _calcularFechaInicial(MontoPagoCompuestoDTO obj, int i)
        {
            var myDate = new DateTime();
            myDate = DateTime.Now;
            // myDate = DateTime.Now;
            var mes = myDate.Month;
            int dia = 0;
            if (!string.IsNullOrEmpty(obj.mp_vencimiento))
            {
                dia = Int32.Parse(obj.mp_vencimiento);
            }
            else
            {
                myDate.AddDays(1);
            }
            if (obj.mp_primeraCuota != null)
            {
                DateTime fec_temp = new DateTime();
                fec_temp = DateTime.Now;
                myDate = ObtenerPrimeraFecha(obj.mp_primeraCuota, dia);
            }
            if (dia < 29)
            {
                myDate = myDate.AddDays(dia);
            }
            else
            {
                myDate = myDate.AddDays(28);
            }
            if (obj.mp_vencimiento != null)
            {
                mes = myDate.Month;
            }
            myDate = myDate.AddMonths(i);
            return myDate;
        }
        /// <summary>
        /// Obtiene primera Fecha
        /// </summary>
        /// <param name="MontName">Monto Pago primera Cuota</param>
        /// <param name="DiaInicio"></param>
        /// <returns></returns>
        private DateTime ObtenerPrimeraFecha(string MontName, int DiaInicio)
        {
            int tmp = 0;
            DateTime res = new DateTime();
            string[] ssize = MontName.Split(new char[0]);
            string[] monthNames = (new System.Globalization.CultureInfo("en-US")).DateTimeFormat.MonthNames;
            for (int i = 0; i <= monthNames.Count() - 1; i++)
            {
                var re = ssize[0];
                var re2 = monthNames[i];
                if (ssize[0].Equals(monthNames[i]))
                {
                    tmp = i;
                    string tmpp = "";
                    string tmppdia = "";
                    if (tmp < 10)
                    {
                        tmp++;
                        tmpp = "0" + tmp.ToString();
                        if (DiaInicio < 10)
                        {
                            tmppdia = "0" + DiaInicio.ToString();
                        }
                        else
                        {
                            tmppdia = DiaInicio.ToString();
                        }

                    }
                    else
                    {
                        tmp++;
                        tmpp = tmp.ToString();
                        if (DiaInicio < 10)
                        {
                            tmppdia = "0" + DiaInicio.ToString();
                        }
                        else
                        {
                            tmppdia = DiaInicio.ToString();
                        }
                    }
                    string validFec = ssize[1] + tmpp + tmppdia;
                    res = DateTime.ParseExact(validFec, "yyyyMMdd", CultureInfo.InvariantCulture);
                }
            }
            return res;
        }
        /// <summary>
        /// Obtiene Tipo de Descuento
        /// </summary>
        /// <param name="va">Precio matricula</param>
        /// <param name="des">descuento</param>
        /// <returns></returns>
        private float _tipoDescuentoGeneral(double? va, int des)
        {
            float valor = float.Parse(va.ToString());
            float des2 = float.Parse(des.ToString());
            var d = float.Parse(Convert.ToString((valor * des2) / 100));
            return (valor - d);
        }
        /// <summary>
        /// Genera Html De Precio en Cuotas
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GeneraHtmlPrecioCuotas(MontoPagoCompuestoDTO data)
        {
            string tabla = "";
            string moneda = "";

            var respuesta = _repMoneda.ObtenerMonedaPorId(data.mp_moneda);

            if (respuesta != null)
            {
                moneda = respuesta.Simbolo;
            }

            if (data != null && !string.IsNullOrEmpty(moneda))
            {
                tabla = "1 Matricula de " + moneda.Replace(".", " ") + " " + data.mp_matricula + " y " + data.mp_nro_cuotas + " cuotas de " + moneda + " " + data.mp_cuotas;
            }
            return tabla;
        }
        /// <summary>
        /// Obtiene Una Tabla en Html de Monto Pago Segun Oportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        private string getMontosPagoPaquetes(int idOportunidad)
        {
            List<MontoPagoEtiquetaDTO> versiones = _repMontoPago.ObtenerVersionesMontoPago(idOportunidad);

            List<MontoPagoEtiquetaAgrupadoDTO> agrupado = (from x in versiones
                                                           orderby x.OrdenBeneficio
                                                           group x by new { x.Paquete, x.tp_nombre, x.tp_cuotas, x.mp_precio, x.Simbolo, x.mp_matricula, x.mp_nro_cuotas, x.mp_cuotas } into gj
                                                           select new MontoPagoEtiquetaAgrupadoDTO
                                                           {
                                                               Paquete = gj.Key.Paquete,
                                                               tp_nombre = gj.Key.tp_nombre,
                                                               tp_cuotas = gj.Key.tp_cuotas,
                                                               mp_precio = gj.Key.mp_precio,
                                                               Simbolo = gj.Key.Simbolo,
                                                               mp_matricula = gj.Key.mp_matricula,
                                                               mp_nro_cuotas = gj.Key.mp_nro_cuotas,
                                                               mp_cuotas = gj.Key.mp_cuotas,
                                                               Beneficios = gj.Select(x => x.Titulo).ToList()
                                                           }).ToList();
            if (agrupado.Count() == 0)
            {
                return null;
            }
            else
            {
                string tabla = "";
                tabla = "<TABLE BORDER CELLPADDING=2 CELLSPACING=0 style='border: 1px solid #E6E6E6;border-collapse: collapse;'>";
                tabla += "<tr>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Versión </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Beneficios </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Tipo pago </th>";
                tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Inversión </th>";
                tabla += "</tr>";
                foreach (var re in agrupado)
                {
                    var credito = agrupado.Where(s => s.Paquete == re.Paquete && s.tp_cuotas != 2).FirstOrDefault(); //add roy
                    tabla += "<tr>";
                    tabla += "<td style='border: 1px solid #E6E6E6' >" + getpaquete(re.Paquete == null ? "" : re.Paquete.ToString()) + "</td>";
                    tabla += "<td style='border: 1px solid #E6E6E6'> " + string.Join(", ", re.Beneficios.Distinct()) + "</td>";
                    tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + re.tp_nombre + "</td>";
                    tabla += "<td style='border: 1px solid #E6E6E6' align=right>" + (re.tp_cuotas == 2 ? re.Simbolo.Replace(".", " ") + re.mp_precio.ToString() /*+ (credito == null? "<br /><span style='color:red;'>" + "Con 25% de descuento:" + re.Simbolo.Replace(".", " ") + Math.Round(((re.mp_precio * 75) / 100), MidpointRounding.AwayFromZero).ToString() + "</span>" : "<br /><span style='color:red;'>" + "Con 25% de descuento:" + credito.Simbolo.Replace(".", " ") + Math.Round(((credito.mp_precio * 75) / 100), MidpointRounding.AwayFromZero).ToString() + "</span>")*///decomnetar para 25 % descuento
                            : "1 matricula de: " + re.Simbolo.Replace(".", "") + re.mp_matricula + " y " + re.mp_nro_cuotas + " cuotas de " + re.Simbolo.Replace(".", "") + re.mp_cuotas)
                            + "</td>";
                    tabla += "</tr>";

                }
                tabla += "</TABLE>";
                return tabla;
            }
        }
        /// <summary>
        /// Obtiene Los Paquetes Existentes
        /// </summary>
        /// <param name="val">identificador del Paquete</param>
        /// <returns></returns>
        private string getpaquete(string val)
        {
            switch (val)
            {
                case "1":
                    return "Versión Basica";
                case "2":
                    return "Versión Profesional";
                case "3":
                    return "Versión Gerencial";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Reemplaza las etiquetas en la plantilla
        /// </summary>
        /// <returns>No devuelve algo, vacio</returns>
        public void ReemplazarEtiquetas()
        {
            try
            {
                var plantilla = _repPlantilla.FirstById(this.IdPlantilla);
                this.IdPlantillaBase = plantilla.IdPlantillaBase;
                var plantillaBase = _repPlantilla.ObtenerPlantillaCorreo(this.IdPlantilla);


                //logica asunto
                if (plantillaBase.Asunto.Contains("{tPLA_PGeneral.Nombre}"))
                {
                    var valor = this.DatosOportunidadAlumno.NombrePGeneral;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Asunto = plantillaBase.Asunto.Replace("{tPLA_PGeneral.Nombre}", valor);
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tpla_pgeneral.pw_duracion}"))
                {
                    var valor = this.DatosOportunidadAlumno.DuracionMesesPGeneral;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tpla_pgeneral.pw_duracion}", valor);
                    }
                }

                //logica cuerpo
                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.MontoTotal}"))
                {
                    var valor = this.DatosOportunidadAlumno.MontoTotal;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.MontoTotal}", valor);
                    }
                    //else if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseWhatsAppFacebook)
                    //{
                    //    listaObjetoWhasApp.Where(x => x.codigo.Equals("{tPersonal.Nombre1}")).FirstOrDefault().texto = valor;
                    //}
                }

                if (plantillaBase.Cuerpo.Contains("{tPLA_PGeneral.Nombre}"))
                {
                    var valor = this.DatosOportunidadAlumno.NombrePGeneral;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tPLA_PGeneral.Nombre}", valor);
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.CronogramaPagoCompletoTabla}"))
                {
                    var valor = this.DatosOportunidadAlumno.CronogramaPagoCompleto;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.CronogramaPagoCompletoTabla}", valor);
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{ValorDinamico.DiaFechaActual}"))
                {
                    var valor = this.DatosOportunidadAlumno.DiaFechaActual;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{ValorDinamico.DiaFechaActual}", valor);
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{ValorDinamico.NombreMesFechaActual}"))
                {
                    var valor = this.DatosOportunidadAlumno.NombreMesFechaActual;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{ValorDinamico.NombreMesFechaActual}", valor);
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{ValorDinamico.AnioFechaActual}"))
                {
                    var valor = this.DatosOportunidadAlumno.AnioFechaActual;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{ValorDinamico.AnioFechaActual}", valor);
                    }
                }

                // Respaldo
                /*
                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.Anexo1EstructuraCurricular}"))
                {
                    var valor = this.DatosOportunidadAlumno.Anexo1EstructuraCurricular;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.Anexo1EstructuraCurricular}", valor);
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.Anexo2Certificacion}"))
                {
                    var valor = this.DatosOportunidadAlumno.Anexo2Certificacion;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.Anexo2Certificacion}", valor);
                    }
                }
                */

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.Version}"))
                {
                    var valor = this.DatosOportunidadAlumno.Version;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.Version}", valor);
                    }
                }

                //datos alumno
                if (plantillaBase.Cuerpo.Contains("{tAlumnos.nombre1}"))
                {
                    var valor = this.DatosOportunidadAlumno.OportunidadAlumno.Nombre1;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.nombre1}", valor);
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_Alumno.NombreCompleto}"))
                {
                    var valor = this.DatosOportunidadAlumno.OportunidadAlumno.NombreCompleto;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.NombreCompleto}", valor);
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_Alumno.NroDocumento}"))
                {
                    var valor = this.DatosOportunidadAlumno.OportunidadAlumno.NroDocumento;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_Alumno.NroDocumento}", valor);
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tAlumnos.direccion}"))
                {
                    var valor = this.DatosOportunidadAlumno.OportunidadAlumno.Direccion;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.direccion}", valor);
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tAlumnos.NombreCiudad}"))
                {
                    var valor = this.DatosOportunidadAlumno.OportunidadAlumno.NombreCiudad;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.NombreCiudad}", valor);
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{tAlumnos.NombrePais}"))
                {
                    var valor = this.DatosOportunidadAlumno.OportunidadAlumno.NombrePais;
                    if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                    {
                        plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{tAlumnos.NombrePais}", valor);
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.Anexo1EstructuraCurricular}"))
                {
                    var oportunidad = _repOportunidad.FirstById(IdOportunidad);
                    var pEspecifico = _repPespecifico.GetBy(x => x.IdCentroCosto == oportunidad.IdCentroCosto && x.Estado == true).FirstOrDefault();

                    List<ProgramaGeneralSeccionDocumentoDTO> listaSecciones = Documentos.ObtenerListaSeccionDocumentoProgramaGeneral(pEspecifico.IdProgramaGeneral.GetValueOrDefault());
                    var seccionEstructura = Documentos.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones);

                    var estructuraV2 = seccionEstructura.Where(x => x.Seccion.ToLower() == "estructura curricular").FirstOrDefault();
                    if (estructuraV2 != null)
                    {
                        var valor = "";
                        valor = "<strong>" + estructuraV2.Seccion + "</strong><br>";
                        valor += estructuraV2.Contenido;
                        valor = valor.Replace("&bull;&nbsp;&nbsp;&nbsp;", "");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.Anexo1EstructuraCurricular}", valor);
                        }
                    }
                    else
                    {
                        var valorFinal = _repDocumentoSeccionPw.ObtenerSecciones(pEspecifico.IdProgramaGeneral.GetValueOrDefault());
                        var detalle = valorFinal.Where(x => x.Titulo.Contains("Estructura Curricular")).FirstOrDefault();
                        var valor = "";
                        if (detalle != null)
                        {
                            valor = "<h2>" + detalle.Titulo + "</h2>";
                            valor += detalle.Contenido;
                        }
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.Anexo1EstructuraCurricular}", valor);
                        }
                    }
                }

                if (plantillaBase.Cuerpo.Contains("{T_MatriculaCabecera.Anexo2Certificacion}"))
                {
                    var oportunidad = _repOportunidad.FirstById(IdOportunidad);
                    var pEspecifico = _repPespecifico.GetBy(x => x.IdCentroCosto == oportunidad.IdCentroCosto && x.Estado == true).FirstOrDefault();

                    List<ProgramaGeneralSeccionDocumentoDTO> listaSecciones = Documentos.ObtenerListaSeccionDocumentoProgramaGeneral(pEspecifico.IdProgramaGeneral.GetValueOrDefault());
                    var seccionEstructura = Documentos.GenerarHTMLProgramaGeneralDocumentoSeccion(listaSecciones);

                    var certificacionV2 = seccionEstructura.Where(x => x.Seccion.ToLower() == "certificacion").FirstOrDefault();
                    if (certificacionV2 != null)
                    {
                        var valor = "";
                        valor = "<strong>" + certificacionV2.Seccion + "</strong><br>";
                        valor += certificacionV2.Contenido;
                        valor = valor.Replace("&bull;&nbsp;&nbsp;&nbsp;", "");
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.Anexo2Certificacion}", valor);
                        }
                    }
                    else
                    {
                        var valorFinal = _repDocumentoSeccionPw.ObtenerSecciones(pEspecifico.IdProgramaGeneral.GetValueOrDefault());
                        var detalle = valorFinal.Where(x => x.Titulo.Contains("Certificación")).FirstOrDefault();
                        var valor = "";
                        if (detalle != null)
                        {
                            valor = "<h2>" + detalle.Titulo + "</h2>";
                            valor += detalle.Contenido;
                        }
                        if (plantilla.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                        {
                            plantillaBase.Cuerpo = plantillaBase.Cuerpo.Replace("{T_MatriculaCabecera.Anexo2Certificacion}", valor);
                        }
                    }
                }

                if (this.IdPlantillaBase == ValorEstatico.IdPlantillaBaseEmail)
                {
                    EmailReemplazado.Asunto = plantillaBase.Asunto;
                    EmailReemplazado.CuerpoHTML = plantillaBase.Cuerpo;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
