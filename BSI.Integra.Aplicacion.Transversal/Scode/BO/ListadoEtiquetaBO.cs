using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Helper;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    /// BO: ListadoEtiquetaBO
    /// Autor: Gian Miranda - Edgar Serruto - Jose Villena.
    /// Fecha: 30/04/2021
    /// <summary>
    /// Columnas y funciones de etiquetas
    /// </summary>
    public class ListadoEtiquetaBO
    {
        private readonly integraDBContext _integraDBContext;
        private readonly PespecificoSesionRepositorio _repPespecificoSesion;
        private readonly PlantillaClaveValorRepositorio _repPlantillaClaveValor;
        private readonly PespecificoRepositorio _repPespecifico;
        private readonly PespecificoPadrePespecificoHijoRepositorio _repPespecificoPadrePespecificoHijo;
        private readonly PgeneralRepositorio _repPgeneral;
        private readonly FrecuenciaRepositorio _repFrecuencia;
        private readonly PespecificoFrecuenciaRepositorio _repPespecificoFrecuencia;
        private readonly ExpositorRepositorio _repExpositor;
        private readonly PersonalRepositorio _repPersonal;
        private readonly MontoPagoRepositorio _repMontoPago;
        private readonly MonedaRepositorio _repMoneda;
        private readonly OportunidadRepositorio _repOportunidad;
        private readonly AlumnoRepositorio _repAlumno;
        private readonly VersionProgramaRepositorio _repVersionPrograma;
        private readonly DocumentoSeccionPwRepositorio _repDocumentoSeccionPw;
        private readonly PgeneralConfiguracionBeneficioRepositorio _repPgeneralConfiguracionBeneficio;
        private readonly DocumentosBO Documentos;

        public int idPGeneral { get; set; }

        public ListadoEtiquetaBO()
        {
            _repPespecificoSesion = new PespecificoSesionRepositorio();
            _repPespecificoPadrePespecificoHijo = new PespecificoPadrePespecificoHijoRepositorio();
            _repPlantillaClaveValor = new PlantillaClaveValorRepositorio();
            _repPespecifico = new PespecificoRepositorio();
            _repFrecuencia = new FrecuenciaRepositorio();
            _repPespecificoFrecuencia = new PespecificoFrecuenciaRepositorio();
            _repExpositor = new ExpositorRepositorio();
            _repPersonal = new PersonalRepositorio();
            _repMontoPago = new MontoPagoRepositorio();
            _repMoneda = new MonedaRepositorio();
            _repPgeneral = new PgeneralRepositorio();
            _repPgeneralConfiguracionBeneficio = new PgeneralConfiguracionBeneficioRepositorio();
            _repAlumno = new AlumnoRepositorio();
            _repVersionPrograma = new VersionProgramaRepositorio();
            _repOportunidad = new OportunidadRepositorio();
            _repDocumentoSeccionPw = new DocumentoSeccionPwRepositorio();
            Documentos = new DocumentosBO();
        }

        public ListadoEtiquetaBO(integraDBContext integraDBContext)
        {
            _integraDBContext = integraDBContext;
            _repPespecificoSesion = new PespecificoSesionRepositorio(integraDBContext);
            _repPespecificoPadrePespecificoHijo = new PespecificoPadrePespecificoHijoRepositorio(integraDBContext);
            _repPlantillaClaveValor = new PlantillaClaveValorRepositorio(integraDBContext);
            _repPespecifico = new PespecificoRepositorio(integraDBContext);
            _repFrecuencia = new FrecuenciaRepositorio(integraDBContext);
            _repPespecificoFrecuencia = new PespecificoFrecuenciaRepositorio(integraDBContext);
            _repExpositor = new ExpositorRepositorio(integraDBContext);
            _repPersonal = new PersonalRepositorio(integraDBContext);
            _repMontoPago = new MontoPagoRepositorio(integraDBContext);
            _repMoneda = new MonedaRepositorio(integraDBContext);
            _repPgeneral = new PgeneralRepositorio(integraDBContext);
            _repPgeneralConfiguracionBeneficio = new PgeneralConfiguracionBeneficioRepositorio(integraDBContext);
            _repAlumno = new AlumnoRepositorio(integraDBContext);
            _repVersionPrograma = new VersionProgramaRepositorio(integraDBContext);
            _repOportunidad = new OportunidadRepositorio(integraDBContext);
            _repDocumentoSeccionPw = new DocumentoSeccionPwRepositorio(integraDBContext);
            Documentos = new DocumentosBO(integraDBContext);
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Devuelve la firma del personal asignado con el cuermo HTML
        /// </summary>
        /// <param name="email"> Email </param>
        /// <returns>HTML incrustada la imagen de la firma del personal asignado : string </returns>
        public string EtiquetaUrlFirmaCorreo(string email)
        {
            string firma = string.Empty;

            if (!string.IsNullOrEmpty(email))
            {
                string[] usuario = email.Split("@");

                firma = string.Concat("<img src='https://repositorioweb.blob.core.windows.net/firmas/", usuario[0], ".png' align='left'>");
            }

            return firma;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Devuelve los cursos relacionados al centro de costo indicado
        /// </summary>
        /// <param name="idCentroCosto"> Id Centro de Costo </param>
        /// <returns>HTML con los programas relacionados al centro de costo : string </returns>
        public string EtiquetaCursoRelacionado(int idCentroCosto)
        {
            string valor = string.Empty;
            var urlCursosRelacionados = _repPlantillaClaveValor.ObtenerCursosRelacionadosPlantilla(idCentroCosto);
            valor = urlCursosRelacionados != null ? urlCursosRelacionados.Aggregate(valor, (current, url) => current + "<a href='" + url.UrlPagina + "'>" + url.Nombre + "</a><br/><br/>") : string.Empty;

            return valor;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Obtienen los contenidos de los programas generales por modalidad
        /// </summary>
        /// <param name="modalidades"> Información de modalidades de programa </param>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <param name="initContenido"> Parte Inicial de contenido </param>
        /// <returns> string </returns>
        public string ObtenerContenidoHorario(List<ModalidadProgramaDTO> modalidades, string initContenido, int idPGeneral)
        {
            PGeneralParametroSeoPwRepositorio _repPGeneralParametroSeoPw = new PGeneralParametroSeoPwRepositorio();
            ExcepcionFrecuenciaPwRepositorio _repExcepcionFrecuenciaPw = new ExcepcionFrecuenciaPwRepositorio();

            var parametrosSEO = _repPGeneralParametroSeoPw.ObtenerParametrosSEOPorIdPGeneral(idPGeneral);
            var excepcionesFrecuencia = _repExcepcionFrecuenciaPw.ObtenerTodoProgramaGeneral();
            var padreEspecificoHijo = _repPgeneral.ObtenerPadreHijoEspecificoV2(idPGeneral); //VERSION NUEVA
            var obtenerFrecuencia = _repPgeneral.ObtenerFrecuenciasPorIdPGeneral(idPGeneral);
            var especificoSesion = _repPgeneral.ObtenerSesionesPorProgramaGeneral(idPGeneral);
            var tipo = parametrosSEO.FirstOrDefault(w => w.Nombre.Equals("description"));

            string presencialDatos = string.Empty;
            string sincronicoDatos = string.Empty;
            string extraPresencial = string.Empty;
            string extraSincrono = string.Empty;

            var programaGeneral = modalidades.FirstOrDefault() == null && modalidades.Count() != 0 ? new ModalidadProgramaDTO { Pw_duracion = "0", NombrePG = "" } : modalidades.FirstOrDefault();
            if (programaGeneral != null)
            {
                if (programaGeneral.NombrePG.Contains("Curso") || programaGeneral.NombrePG.Contains("Programa"))
                {
                    // Plantilla
                    presencialDatos = "<p><strong>En la modalidad Presencial:</strong></p><p>El ##CURSODIPLOMA## se desarrolla en el siguiente horario (*): </p>";
                    sincronicoDatos = "<p><strong>En la modalidad online sincrónica (clases en vivo):</strong></p><p> El ##CURSODIPLOMA## tiene una duración de ##DURACIONPE## cronológicas. Las clases se desarrollarán de forma virtual, con una frecuencia ##FRECUENCIA## en el siguiente horario (*): </p>";
                    extraPresencial = "<p><i> (*)Para más detalle sobre fechas y horarios solicite su cronograma de alumnos.</i><p/>";
                    extraSincrono = "<p><i> (*)Para más detalle sobre fechas y horarios solicita tu cronograma.</i><p/>";
                }
                else
                {
                    if (tipo.Descripcion.Contains("Curso"))
                    {
                        // Plantilla
                        presencialDatos = "<p><strong>En la modalidad Presencial:</strong></p><p> El Curso ##CURSODIPLOMA## se desarrolla en el siguiente horario (*): </p>";
                        sincronicoDatos = "<p><strong>En la modalidad online sincrónica (clases en vivo):</strong></p><p> El Curso ##CURSODIPLOMA## tiene una duración de ##DURACIONPE## cronológicas. Las clases se desarrollarán de forma virtual, con una frecuencia ##FRECUENCIA## en el siguiente horario (*): </p>";
                        extraPresencial = "<p><i> (*)Para más detalle sobre fechas y horarios solicite su cronograma de alumnos.</i><p/>";
                        extraSincrono = "<p><i> (*)Para más detalle sobre fechas y horarios solicita tu cronograma.</i><p/>";
                    }
                    if (tipo.Descripcion.Contains("Programa"))
                    {
                        //plantilla
                        presencialDatos = "<p><strong>En la modalidad Presencial:</strong></p><p> El Programa ##CURSODIPLOMA## se desarrolla en el siguiente horario (*): </p>";
                        sincronicoDatos = "<p><strong>En la modalidad online sincrónica (clases en vivo):</strong></p><p> El Programa ##CURSODIPLOMA## tiene una duración de ##DURACIONPE## cronológicas. Las clases se desarrollarán de forma virtual, con una frecuencia ##FRECUENCIA## en el siguiente horario (*): </p>";
                        extraPresencial = "<p><i> (*)Para más detalle sobre fechas y horarios solicite su cronograma de alumnos.</i><p/>";
                        extraSincrono = "<p><i> (*)Para más detalle sobre fechas y horarios solicita tu cronograma.</i><p/>";
                    }
                }
            }

            var programaEspecifico = modalidades;
            string frecuencia = string.Empty;
            string todohtmlpresencial = ObtenerContenidoPresencial(programaEspecifico.Where(w => w.Tipo == "Presencial").ToList(), true, excepcionesFrecuencia, padreEspecificoHijo, obtenerFrecuencia, especificoSesion);
            if (todohtmlpresencial == string.Empty)
            {
                presencialDatos = string.Empty;
                extraPresencial = string.Empty;
            }
            string todohtmlsincrono = ObtenerContenidoPresencial(programaEspecifico.Where(w => w.Tipo == "Online Sincronica").ToList(), false, excepcionesFrecuencia, padreEspecificoHijo, obtenerFrecuencia, especificoSesion);
            if (todohtmlsincrono == string.Empty)
            {
                sincronicoDatos = string.Empty;
                extraSincrono = string.Empty;
            }
            else
            {
                var sincrona = modalidades.FirstOrDefault(x => x.Tipo == "Online Sincronica");
                if (sincrona.NombreESP.Contains("Curso"))
                {
                    var padre = padreEspecificoHijo.FirstOrDefault(w => w.IdPespecificoHijo == sincrona.IdPEspecifico);
                    frecuencia = padre != null ? obtenerFrecuencia.Where(w => w.IdPEspecifico == padre.IdPespecificoHijo).Select(w => w.Nombre).FirstOrDefault() : obtenerFrecuencia.Where(w => w.IdPEspecifico == sincrona.IdPEspecifico).Select(w => w.Nombre).FirstOrDefault();
                }
                else
                    frecuencia = frecuencia = obtenerFrecuencia.Where(w => w.IdPEspecifico == sincrona.IdPEspecifico).Select(w => w.Nombre).FirstOrDefault();
            }
            string seContenido = initContenido;
            seContenido = presencialDatos + todohtmlpresencial + extraPresencial + sincronicoDatos + todohtmlsincrono + extraSincrono + seContenido;

            if (programaEspecifico.Count() != 0)
            {
                var sincronico = programaEspecifico.FirstOrDefault(x => x.Tipo.ToLower().Equals("online sincronica"));
                if (sincronico != null)
                {
                    string temppe = seContenido.Replace("##DURACIONPE##", Convert.ToInt32(double.Parse(programaEspecifico.Where(x => x.Tipo.ToLower().Equals("online sincronica")).FirstOrDefault().Duracion, CultureInfo.InvariantCulture)) + " Horas").Replace("##DURACIONPG##", programaGeneral.Pw_duracion).Replace("##CURSODIPLOMA##", programaGeneral.NombrePG).Replace("##FRECUENCIA##", frecuencia);
                    temppe = temppe.Replace("®", "<sup>®</sup>");
                    seContenido = temppe;
                }
                else
                {
                    string temppe = seContenido.Replace("##DURACIONPE##", Convert.ToInt32(double.Parse(programaEspecifico.FirstOrDefault().Duracion, CultureInfo.InvariantCulture)) + " Horas").Replace("##DURACIONPG##", programaGeneral.Pw_duracion).Replace("##CURSODIPLOMA##", programaGeneral.NombrePG).Replace("##FRECUENCIA##", frecuencia); //Version 1 Para revertir
                    temppe = temppe.Replace("®", "<sup>®</sup>");
                    seContenido = temppe;
                }
            }
            else
            {
                if (seContenido != "" && programaGeneral != null)
                {
                    string temppe = seContenido.Replace("##DURACIONPE##", "" + " Horas").Replace("##DURACIONPG##", programaGeneral.Pw_duracion).Replace("##CURSODIPLOMA##", programaGeneral.NombrePG).Replace("##FRECUENCIA##", frecuencia);
                    temppe = temppe.Replace("®", "<sup>®</sup>");
                    seContenido = temppe;
                }
            }
            return seContenido;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el id del dia
        /// </summary>
        /// <param name="dia"> cadena de Día </param>
        /// <returns>Id del dia : int </returns>
        private int ObtenerIdDia(string dia)
        {
            switch (dia)
            {
                case "Lunes":
                    return 1;
                case "Martes":
                    return 2;
                case "Miércoles":
                    return 3;
                case "Jueves":
                    return 4;
                case "Viernes":
                    return 5;
                case "Sábado":
                    return 6;
                case "Domingo":
                    return 7;
                default:
                    return 0;
            }
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene un string HTML con el contenido presencial
        /// </summary>
        /// <param name="pEspecificos"> Información de modalidad de programa de programas específicos</param>
        /// <param name="presencial"> Validación de curso/programa presencial </param>
        /// <param name="excepcionesFrecuencia"> Información de excepción de frecuancias </param>
        /// <param name="padreEspecificoHijo"> Lista de padre hijo específico </param>
        /// <param name="frecuenciaProgramas"> Frecuencia de Programas </param>
        /// <param name="especificoSesion"> Información de sesiones de programa específico </param>
        /// <returns>HTML con el contenido presencial : string </returns>
        private string ObtenerContenidoPresencial(List<ModalidadProgramaDTO> pEspecificos, bool presencial, List<ExcepcionFrecuenciaPGeneralDTO> excepcionesFrecuencia, List<PadrePespecificoHijoDTO> padreEspecificoHijo, List<FrecuenciaProgramaGeneralDTO> frecuenciaProgramas, List<PEspecificoSesionDTO> especificoSesion)
        {
            string horariosP1 = "<li>##DIA##: ##HORAINICIO## a ##HORAFIN## horas.</li>";
            string horariosP2 = "<li>##DIA##: ##HORAINICIO## a ##HORAFIN## - ##HORAINICIO2## a ##HORAFIN2## horas.</li>";
            string datosCiudad = "<p>En ##CIUDAD## con una frecuencia ##FRECUENCIA##:</p> <ul class='list'>##HORARIOS##</ul>";
            string datosSinCiudad = "<ul class='list'>##HORARIOS##</ul>";
            string[] arreglos = new string[pEspecificos.ToList().Count];
            int contador = 0;
            string strHTML = string.Empty;
            foreach (var item in pEspecificos)
            {
                //trae la lista de idpespecifico de excepciones
                var listaExcepciones = excepcionesFrecuencia.Select(w => w.IdPEspecifico).ToList();
                string frecuencia = "";
                if (listaExcepciones.Contains(item.IdPEspecifico))//lee tabla de excepciones
                {
                    frecuencia = "Diaria";
                }
                else //sigue flujo normal
                {
                    if (item.NombreESP.Contains("Curso"))
                    {
                        var padre = padreEspecificoHijo.FirstOrDefault(w => w.IdPespecificoHijo == item.IdPEspecifico);
                        frecuencia = padre != null ? frecuenciaProgramas.Where(w => w.IdPEspecifico == padre.IdPespecificoPadre).Select(w => w.Nombre).FirstOrDefault() : frecuenciaProgramas.Where(w => w.IdPEspecifico == item.IdPEspecifico).Select(w => w.Nombre).FirstOrDefault();
                    }
                    else
                    {
                        frecuencia = frecuenciaProgramas.Where(w => w.IdPEspecifico == item.IdPEspecifico).Select(w => w.Nombre).FirstOrDefault();
                    }
                }
                var sesionestemp = new List<PEspecificoSesionDTO>();
                if (item.NombreESP.Contains("Curso"))
                {
                    var hijo = item;
                    // Obtener las sesiones del pespcifico
                    sesionestemp = especificoSesion.Where(w => w.IdPEspecifico == hijo.IdPEspecifico && w.Predeterminado == true).ToList();
                }
                else
                {
                    // Obtenemos un pespecifico
                    var hijo = padreEspecificoHijo.Where(w => w.IdPespecificoPadre == item.IdPEspecifico).ToList();
                    if (!hijo.Any())
                    {
                        var hijotemp = item;
                        sesionestemp = especificoSesion.Where(w => w.IdPEspecifico == hijotemp.IdPEspecifico && w.Predeterminado == true).ToList();
                    }
                    else
                    {
                        foreach (var itemHijo in hijo)
                        {
                            var temporal = especificoSesion.Where(w => w.IdPEspecifico == itemHijo.IdPespecificoHijo && w.Predeterminado == true && w.Estado == true).ToList();
                            sesionestemp.AddRange(temporal);
                        }
                    }
                }
                //lista donde se almacenara las sesiones
                var lista = new List<SesionTempDTO>();
                //llenas las sesiones  dia horainicio horafin
                foreach (var item3 in sesionestemp.OrderBy(w => w.FechaHoraInicio))
                {
                    var itemlista = new SesionTempDTO
                    {
                        Dia = new CultureInfo("es-PE", false).TextInfo.ToTitleCase(item3.FechaHoraInicio.Value.ToString("dddd"))
                    };

                    switch (itemlista.Dia)
                    {
                        //itemlista.Dia
                        case "Monday":
                            itemlista.Dia = "Lunes";
                            break;
                        case "Tuesday":
                            itemlista.Dia = "Martes";
                            break;
                        case "Wednesday":
                            itemlista.Dia = "Miércoles";
                            break;
                        case "Thursday":
                            itemlista.Dia = "Jueves";
                            break;
                        case "Friday":
                            itemlista.Dia = "Viernes";
                            break;
                        case "Saturday":
                            itemlista.Dia = "Sábado";
                            break;
                        case "Sunday":
                            itemlista.Dia = "Domingo";
                            break;
                    }
                    //Dia = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(item3.FechaHoraInicio.Value.ToString("dddd"));
                    itemlista.Idciudad = ObtenerIdDia(CultureInfo.InvariantCulture.TextInfo.ToTitleCase(item3.FechaHoraInicio.Value.ToString("dddd")));
                    itemlista.Horainicio = item3.FechaHoraInicio.Value.ToString("HH:mm");
                    itemlista.Veces = 1;
                    itemlista.Horafin = item3.FechaHoraInicio.Value.AddHours(Convert.ToDouble(item3.Duracion)).ToString("HH:mm");
                    itemlista.Ciudad = item.Ciudad;

                    lista.Add(itemlista);
                }
                lista = lista.Distinct().ToList();

                var listaFinal = new List<SesionTempDTO>();
                var itemTempListaFinal = new SesionTempDTO();
                foreach (var item_lista in lista)
                {
                    if (!listaFinal.Any(w => w.Dia == item_lista.Dia && w.Ciudad == item_lista.Ciudad && w.Horainicio == item_lista.Horainicio && w.Horafin == item_lista.Horafin))
                        listaFinal.Add(item_lista);
                    else
                    {
                        itemTempListaFinal = listaFinal.FirstOrDefault(w => w.Dia == item_lista.Dia && w.Ciudad == item_lista.Ciudad && w.Horainicio == item_lista.Horainicio && w.Horafin == item_lista.Horafin);
                        listaFinal.Remove(itemTempListaFinal);
                        itemTempListaFinal.Veces++;
                        listaFinal.Add(itemTempListaFinal);
                    }

                }
                lista = listaFinal;

                var listafinal = new List<SesionTempDTO>();

                foreach (var item_ in lista.OrderBy(w => w.Idciudad))
                {
                    var tempFechas = lista.Where(w => w.Dia == item_.Dia).ToList();
                    //valida
                    var contadorlista = lista.Where(w => w.Veces == 1).ToList().Count();
                    if (!listafinal.Any(w => w.Dia == tempFechas.FirstOrDefault().Dia))
                    {
                        if (tempFechas.Count() == 1)
                        {
                            if (listafinal == null)
                            {
                                listafinal = tempFechas;
                                listafinal.FirstOrDefault().Tipo = false;//tipo normal
                            }
                            else
                            {
                                tempFechas.FirstOrDefault().Tipo = false;
                                listafinal.Add(tempFechas.FirstOrDefault());
                            }
                        }
                        if (tempFechas.Count() == 2)
                        {
                            tempFechas = tempFechas.OrderByDescending(w => w.Veces).ToList();

                            if (Math.Abs(Convert.ToDateTime(tempFechas.First().Horainicio).Hour - Convert.ToDateTime(tempFechas.Last().Horainicio).Hour) < 2)
                            {
                                listafinal.Add(tempFechas.First());
                                continue;
                            }

                            if (tempFechas.First().Veces - tempFechas.Last().Veces > 5)
                            {
                                SesionTempDTO listafinaltempfinal = new SesionTempDTO();
                                listafinaltempfinal = tempFechas.First();
                                listafinaltempfinal.Tipo = false;
                                listafinal.Add(listafinaltempfinal);
                            }
                            else
                            {
                                tempFechas = tempFechas.OrderBy(w => w.Horainicio).ToList();

                                if (tempFechas.First().Dia == tempFechas.Last().Dia)
                                {
                                    SesionTempDTO listafinaltempfinal = new SesionTempDTO();

                                    listafinaltempfinal.Dia = tempFechas.First().Dia;
                                    listafinaltempfinal.Horainicio = tempFechas.First().Horainicio;
                                    listafinaltempfinal.Horafin = tempFechas.First().Horafin;
                                    listafinaltempfinal.Horainicio2 = tempFechas.Last().Horainicio;
                                    listafinaltempfinal.Horafin2 = tempFechas.Last().Horafin;
                                    listafinaltempfinal.Tipo = true;//tipo doble horario
                                    listafinal.Add(listafinaltempfinal);
                                }
                                else
                                {
                                    SesionTempDTO listafinaltempfinal = new SesionTempDTO();
                                    SesionTempDTO listafinaltemp2final = new SesionTempDTO();

                                    listafinaltempfinal.Dia = tempFechas.First().Dia;
                                    listafinaltempfinal.Horainicio = tempFechas.First().Horainicio;
                                    listafinaltempfinal.Horafin = tempFechas.First().Horafin;
                                    listafinaltempfinal.Horainicio2 = tempFechas.Last().Horainicio;
                                    listafinaltempfinal.Horafin2 = tempFechas.Last().Horafin;
                                    listafinaltempfinal.Tipo = false;//tipo doble horario

                                    listafinaltemp2final.Dia = tempFechas.Last().Dia;
                                    listafinaltemp2final.Horainicio = tempFechas.Last().Horainicio;
                                    listafinaltemp2final.Horafin = tempFechas.Last().Horafin;
                                    listafinaltemp2final.Horainicio2 = tempFechas.Last().Horainicio;
                                    listafinaltemp2final.Horafin2 = tempFechas.Last().Horafin;
                                    listafinaltemp2final.Tipo = false;//tipo doble horario

                                    listafinal.Add(listafinaltempfinal);
                                    listafinal.Add(listafinaltemp2final);
                                }
                            }
                        }
                        if (tempFechas.Count() == 3)
                        {
                            tempFechas = tempFechas.OrderByDescending(w => w.Veces).ToList();
                            if (tempFechas.First().Veces - tempFechas.Skip(1).Take(1).First().Veces > 5)
                            {
                                var _tempFechas = tempFechas;
                                tempFechas = new List<SesionTempDTO>();
                                tempFechas.Add(_tempFechas.First());
                            }
                            else
                            {
                                tempFechas.Remove(tempFechas.Last());
                            }
                            SesionTempDTO listafinaltemp = new SesionTempDTO();
                            SesionTempDTO listafinaltemp2 = new SesionTempDTO();
                            string day = tempFechas.First().Dia;
                            var tempa = tempFechas.Where(w => w.Dia == day).ToList();

                            if (tempa.Count == 1)
                            {
                                listafinaltemp.Dia = tempa.First().Dia;
                                listafinaltemp.Horainicio = tempa.First().Horainicio;
                                listafinaltemp.Horafin = tempa.First().Horafin;
                                listafinaltemp.Tipo = false;//tipo doble horario
                                listafinal.Add(listafinaltemp);
                            }
                            if (tempa.Count == 2)
                            {
                                if (Math.Abs(Convert.ToDateTime(tempa.First().Horainicio).Hour - Convert.ToDateTime(tempa.Last().Horainicio).Hour) < 2)
                                {
                                    tempa = tempa.OrderBy(w => w.Veces).ThenBy(w => w.Horainicio).ToList();
                                    listafinaltemp.Dia = tempa.First().Dia;
                                    listafinaltemp.Horainicio = tempa.First().Horainicio;
                                    listafinaltemp.Horafin = tempa.First().Horafin;
                                    listafinaltemp.Tipo = false;//tipo doble horario
                                    listafinal.Add(listafinaltemp);
                                }
                                else
                                {
                                    tempa = tempa.OrderBy(w => w.Horainicio).ToList();
                                    listafinaltemp.Dia = tempa.First().Dia;
                                    listafinaltemp.Horainicio = tempa.First().Horainicio;
                                    listafinaltemp.Horafin = tempa.First().Horafin;
                                    listafinaltemp.Horainicio2 = tempa.Last().Horainicio;
                                    listafinaltemp.Horafin2 = tempa.Last().Horafin;
                                    listafinaltemp.Tipo = true;//tipo doble horario
                                    listafinal.Add(listafinaltemp);
                                }
                            }
                        }
                        if (tempFechas.Count() > 3)
                        {
                            tempFechas = tempFechas.OrderByDescending(w => w.Veces).ToList();
                            SesionTempDTO listafinaltemp = new SesionTempDTO();

                            if (tempFechas.First().Veces - tempFechas.Skip(1).Take(1).First().Veces > 5)
                            {
                                SesionTempDTO listafinaltempfinal = new SesionTempDTO();
                                listafinaltempfinal = tempFechas.First();
                                listafinaltempfinal.Tipo = false;
                                listafinal.Add(listafinaltempfinal);
                            }
                            else
                            {
                                List<SesionTempDTO> tempvarios = new List<SesionTempDTO>();
                                tempvarios.Add(tempFechas.First());
                                tempvarios.Add(tempFechas.Skip(1).Take(1).First());
                                tempvarios = tempvarios.OrderBy(w => w.Horainicio).ToList();
                                listafinaltemp.Dia = tempvarios.First().Dia;
                                listafinaltemp.Horainicio = tempvarios.First().Horainicio;
                                listafinaltemp.Horafin = tempvarios.First().Horafin;
                                listafinaltemp.Horainicio2 = tempvarios.Skip(1).Take(1).First().Horainicio;
                                listafinaltemp.Horafin2 = tempvarios.Skip(1).Take(1).First().Horafin;
                                listafinaltemp.Tipo = true;//tipo doble horario
                                listafinal.Add(listafinaltemp);
                            }
                        }
                    }
                }
                foreach (var variable in listafinal)
                {
                    if (variable.Tipo == true)
                    {
                        arreglos[contador] += horariosP2.Replace("##DIA##", variable.Dia).Replace("##HORAINICIO##", variable.Horainicio).Replace("##HORAFIN##", variable.Horafin).Replace("##HORAINICIO2##", variable.Horainicio2).Replace("##HORAFIN2##", variable.Horafin2);
                    }
                    else
                    {
                        arreglos[contador] += horariosP1.Replace("##DIA##", variable.Dia).Replace("##HORAINICIO##", variable.Horainicio).Replace("##HORAFIN##", variable.Horafin);
                    }
                }
                if (presencial == true)
                    strHTML += datosCiudad.Replace("##CIUDAD##", item.Ciudad).Replace("##FRECUENCIA##", frecuencia).Replace("##HORARIOS##", arreglos[contador]);
                else
                    strHTML += datosSinCiudad.Replace("##HORARIOS##", arreglos[contador]);
                contador++;
            }
            return strHTML;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la duracion y horario logica especial
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <returns>HTML con la duracion y horario segun logica especial : string </returns>
        public string ObtenerDuracionAndHorario(int idPGeneral)
        {
            List<ModalidadProgramaDTO> modalidades = _repPgeneral.ObtenerModalidadesPorProgramaGeneral(idPGeneral);
            var secciones = _repPgeneral.ObtenerSeccionesInformacionProgramaPorProgramaGeneral(idPGeneral);

            return ObtenerContenidoHorario(modalidades, string.Empty, idPGeneral);
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Devuelve la lista de los expositores de un programa general
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <returns>HTML con la lista de expositores de un programa general : string </returns>
        public string EtiquetaExpositor(int idPGeneral)
        {
            var expositores = _repExpositor.ObtenerExpositoresPorProgramaGeneral(idPGeneral);
            string result = string.Empty;

            result = expositores.Any() ? string.Concat("<div class='expositores'>", expositores.Aggregate(result, (current, expositor) => current + "<h3>" + expositor.Nombres + "</h3><p style='text-align: justify; text-justify: inter-word;'>" + expositor.HojaVida + "</p>")) : string.Empty;

            return result;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene Una Tabla en Html de Monto Pago Segun Oportunidad V2, incluyendo beneficios de la vista V_BeniciosPartnerDocumento
        /// </summary>
        /// <param name="oportunidad"> Información de oportunidad </param>
        /// <param name="idPGeneral"> Id de programa general </param>
        /// <returns> string </returns>
        private string ObtenerMontosPagoPaquetesV2(OportunidadBO oportunidad, int idPGeneral)
        {
            var versiones = _repMontoPago.ObtenerVersionesMontoPagoV2(oportunidad.Id);

            if (!versiones.Any())
                return null;

            PespecificoBO pEspecifico = _repPespecifico.FirstBy(x => x.IdCentroCosto == oportunidad.IdCentroCosto);
            AlumnoBO alumno = _repAlumno.FirstBy(x => x.Id == oportunidad.IdAlumno);

            List<VersionProgramaDTO> versionPrograma = _repVersionPrograma.ObtenerTodo();
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

            foreach (VersionProgramaDTO item in versionPrograma)
            {
                listaBeneficios = Documentos.ObtenerBeneficiosConfiguradosProgramaGeneral(pEspecifico.IdProgramaGeneral.Value, alumno.IdCodigoPais, item.Id);
                contadorBeneficios += listaBeneficios.Count;

                if (listaBeneficios.Count > 0)
                {
                    List<MontoPagoEtiquetaDTO> infoVersiones = versiones.Where(x => x.Paquete == item.Id).ToList();

                    tabla += "<tr>";

                    tabla += $"<td style='border: 1px solid #E6E6E6' rowspan='{(infoVersiones.Count != 0 ? infoVersiones.Count : 1)}'>{item.Nombre}</td>";

                    tabla += $"<td style='border: 1px solid #E6E6E6' rowspan='{(infoVersiones.Count != 0 ? infoVersiones.Count : 1)}'><ul>";

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

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene Una Tabla en Html de Monto Pago Segun Oportunidad
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>
        /// <returns> string </returns>
        private string GetMontosPagoPaquetes(int idOportunidad)
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
                    tabla += "<td style='border: 1px solid #E6E6E6' >" + Getpaquete(re.Paquete == null ? "" : re.Paquete.ToString()) + "</td>";
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

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene Los Paquetes Existentes
        /// </summary>
        /// <param name="val">identificador del Paquete</param>
        /// <returns> string </returns>
        private string Getpaquete(string val)
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

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene El Valor de la Etiqueta MontoPago V2 para las Plantillas
        /// </summary>
        /// <param name="oportunidad"> Información de Oportunidad </param>
        /// <param name="idPGeneral"> Id de programa general </param>
        /// <returns> string </returns>
        public string EtiquetaMontosPagoV2(OportunidadBO oportunidad, int idPGeneral)
        {
            string valorTemporal = ObtenerMontosPagoPaquetesV2(oportunidad, idPGeneral);

            return valorTemporal ?? EtiquetaMontosPago(oportunidad.Id);
        }

        /// <summary>
        /// Obtiene El Valor de la Etiqueta MontoPago para las Plantillas
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns></returns>
        public string EtiquetaMontosPago(int idOportunidad)
        {
            var tabla = GetMontosPagoPaquetes(idOportunidad);

            if (tabla == null)
            {
                string precioNormal = string.Empty;

                string precioContado = ObtenerPrecioContado(idOportunidad);
                string precioCuotas = ObtenerPrecioCuotas(idOportunidad);

                if (!string.IsNullOrEmpty(precioContado)) precioNormal = "<b>Al Contado: </b>" + precioContado;

                if (!string.IsNullOrEmpty(precioCuotas)) precioNormal += "<div style=' height:1px;'></div>" + "<b>Financiamiento: </b>" + precioCuotas;

                return precioNormal;
            }
            else
            {
                return tabla;
            }
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Genera una tabla para Precio en Cuotas de un Programa
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>
        /// <returns> string </returns>
        private string ObtenerPrecioCuotas(int idOportunidad)
        {
            string respuesta = "";
            var montopagoCuotas = _repMontoPago.ObtenerMontoPagoPorIdOportunidad(idOportunidad);
            if (montopagoCuotas != null) respuesta = GenerateGridCronogamaPagos(montopagoCuotas);

            return respuesta;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Genera Una Tabla Con las Cuotas de Pago
        /// </summary>
        /// <param name="lista">campos de Cuotas Para Generar tabla</param>
        /// <returns> string </returns>
        public string GeneraHtml(List<PagoCuotaDTO> lista)
        {
            string tabla = "";
            tabla = "<TABLE BORDER CELLPADDING=2 CELLSPACING=0 style='border: 1px solid #E6E6E6;border-collapse: collapse;'>";
            tabla += "<tr>";
            tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Descripcion </th>";
            tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Fecha pago </th>";
            tabla += "<th bgcolor=#FAFAFA style='border: 1px solid #E6E6E6'> Monto cuota con descuento </th>";
            tabla += "</tr>";
            foreach (var re in lista)
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

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Genera Tablas Depagos deacuerdo a Formua
        /// </summary>
        /// <param name="data"> Datos Compuestos de monto Pago </param>
        /// <returns> string </returns>
        public string GenerateGridCronogamaPagos(MontoPagoCompuestoDTO data)
        {
            string tablaRespuesta = "";
            switch (data.tp_formula)
            {
                case 0://sin descuento                     
                    tablaRespuesta = GeneraHtmlPrecioCuotas(data);
                    break;
                case 1: //matricula
                    tablaRespuesta = GeneraHtml(GenerarGridMatricula(data));
                    break;
                case 2: //cuotas
                    tablaRespuesta = GeneraHtmlPrecioCuotas(data);
                    break;
                case 3: //ambos
                    tablaRespuesta = GeneraHtml(GenerarGridAmbos(data));
                    break;
                case 4: //general
                    tablaRespuesta = GeneraHtml(GenerarGridGeneral(data));
                    break;
                case 5:
                    tablaRespuesta = GeneraHtmlPrecioContado(GenerarGridNormal(data));
                    break;
            }
            return tablaRespuesta;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Genera Html del Precio Contado
        /// </summary>
        /// <param name="lista"> Lista de información de pago de cuotas </param>
        /// <returns> string </returns>
        private string GeneraHtmlPrecioContado(List<PagoCuotaDTO> lista)
        {
            string tabla = "";
            tabla = lista[0].SimboloMoneda.Replace(".", " ") + " " + lista[0].montoCuota.ToString();
            return tabla;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Genera Tabla de Cuotas de Pago de un Programa
        /// </summary>
        /// <param name="data"> Dato Compuesto de monto Pago </param>
        /// <returns> List<PagoCuotaDTO> </returns>
        private List<PagoCuotaDTO> GenerarGridGeneral(MontoPagoCompuestoDTO data)
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
            obj.montoCuotaDescuento = TipoDescuentoGeneral(data.mp_matricula, data.tp_porcentaje_general);
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
                obj1.montoCuotaDescuento = TipoDescuentoGeneral(data.mp_cuotas, data.tp_porcentaje_general);
                obj1.ispagado = false;
                obj1.es_matricula = false;
                DateTime fecha = CalcularFechaInicial(data, i);
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
                        obj2.montoCuotaDescuento = TipoDescuentoGeneral(data.mp_cuotas, data.tp_porcentaje_general);
                        obj2.es_matricula = false;
                        obj2.fechapago = CalcularFechaInicial(data, i);
                        lista.Add(obj2);
                        tamanio = tamanio - 1;
                    }
                }
            }
            return lista;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Calcula FechaInicial de la Primera Cuota
        /// </summary>
        /// <param name="obj"> Datos Compuestos de MontoPago</param>
        /// <param name="agregarMeses"> Cantidad de meses agregados </param>
        /// <returns> DateTime </returns>
        private DateTime CalcularFechaInicial(MontoPagoCompuestoDTO obj, int agregarMeses)
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
            myDate = myDate.AddMonths(agregarMeses);
            return myDate;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Genera HTML a partir de la lista de secciones de documento del programa general
        /// </summary>
        /// <param name="listaProgramaGeneralDocumentoSeccion"> Lista de programas por documento sección </param>
        /// <param name="conTitulo"> Validación de Título </param>
        /// <returns>Retorna la estructura HTML de la lista enviada : List<ProgramaGeneralSeccionAnexosHTMLDTO> </returns>
        public List<ProgramaGeneralSeccionAnexosHTMLDTO> GenerarHTMLProgramaGeneralDocumentoSeccion(List<ProgramaGeneralSeccionDocumentoDTO> listaProgramaGeneralDocumentoSeccion, bool conTitulo)
        {
            try
            {
                var lista = new List<ProgramaGeneralSeccionAnexosHTMLDTO>();

                foreach (var item in listaProgramaGeneralDocumentoSeccion)
                {
                    string contenido = string.Empty;
                    conTitulo = item.Seccion == "Estructura Curricular";

                    foreach (var detalleSeccion in item.DetalleSeccion)
                    {
                        contenido += conTitulo ? $"<p><strong>{detalleSeccion.Titulo}</strong></p>" : string.Empty;

                        contenido += (detalleSeccion.Cabecera != string.Empty) ? $"<p>{detalleSeccion.Cabecera}</p><ul>" : "<ul>";
                        contenido = detalleSeccion.DetalleContenido.Aggregate(contenido, (current, contenidoSeccion) => current + "<li>" + contenidoSeccion + "</li>");
                        contenido += (detalleSeccion.PiePagina != string.Empty) ? $"</ul><p>{detalleSeccion.PiePagina}</p>" : "</ul>";
                    }

                    lista.Add(new ProgramaGeneralSeccionAnexosHTMLDTO
                    {
                        Seccion = item.Seccion,
                        Contenido = contenido
                    });
                }

                return lista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// Genera la fecha de inicio del programa V1
        /// </summary>
        /// <param name="idProgramaGeneral"></param>
        /// <param name="idCentroCosto"></param>
        /// <returns>Fecha de inicio del programa V1</returns>
        public string ObtenerFechaInicioPrograma(int idProgramaGeneral, int idCentroCosto)
        {
            string valor;

            var pEspecifico = _repPespecifico.FirstBy(x => x.IdCentroCosto == idCentroCosto);

            if (pEspecifico.Tipo.ToLower() == "online asincronica")
            {
                DateTime fechaAOnline;
                fechaAOnline = DateTime.Now.Day > 25 ? DateTime.Now : DateTime.Now.AddDays(5);
                valor = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fechaAOnline.ToString("MMMM yyyy"));
            }
            else
            {
                var dato_fecha = _repPespecifico.FechaProgramaEspecifico(idProgramaGeneral, pEspecifico.Id);

                valor = dato_fecha != null ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(dato_fecha.FechaHoraInicio) : "Por definir";
            }

            return valor;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene en HTML las fechas de inicio de los programas V2
        /// </summary>
        /// <param name="idPGeneral"> Id de programa General </param>
        /// <returns>Retorna un HTML con las fechas de inicio de los programas : string </returns>
        public string FechaInicioProgramaV2(int idPGeneral)
        {
            var fechas = ObtenerFechaInicioProgramaTodos(idPGeneral);

            if (fechas == null) return "Por definir";

            string resultado = string.Empty;

            foreach (var fecha in fechas)
            {
                if (fecha.Tipo.ToLower() == "online asincronica" && fecha.Nombre.ToLower().Contains("lima"))
                    resultado += $"<b>{fecha.Tipo}:</b> {fecha.FechaInicioTexto}<br />";
                if (fecha.Tipo.ToLower() == "online sincronica" && fecha.Nombre.ToLower().Contains("lima") || fecha.Nombre.ToLower().Contains("aqp"))
                    resultado += $"<b>{fecha.Tipo}:</b> {fecha.FechaInicioTexto}<br />";
            }

            return resultado;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene en un objeto las fechas de las modalidades según la lógica del portal
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General </param>
        /// <returns>Retorna un objeto con las fechas y las sesiones : List<ListaProgramaEspecificoPorIdProgramaDTO> </returns>
        public List<ListaProgramaEspecificoPorIdProgramaDTO> ObtenerFechaInicioProgramaTodos(int idPGeneral)
        {
            var listaPrevioPEspecifico = new List<ListaProgramaEspecificoPorIdProgramaDTO>();
            var listaFechaInicio = _repPespecifico.ObtenerListaFechaInicioSesion(idPGeneral);

            if (listaFechaInicio == null)
                return null;

            int IdCategoria = listaFechaInicio.Select(w => w.IdCategoria).FirstOrDefault();
            var listaPEspecifico = new List<int>();
            var inicioFechas = new List<EspecificoFechasInicioDTO>();

            if (IdCategoria == 4 || IdCategoria == 15 || IdCategoria == 11)
            {
                listaPEspecifico = listaFechaInicio.Select(x => x.Id).ToList();
                inicioFechas = _repPespecifico.PEspecificoSesionInformacionFechaInicio(listaPEspecifico, 2);
            }
            else if (IdCategoria == 3)
            {
                listaPEspecifico = listaFechaInicio.Select(x => x.Id).ToList();
                inicioFechas = _repPespecifico.PEspecificoSesionInformacionFechaInicio(listaPEspecifico, 1);
            }

            foreach (ListaProgramaEspecificoPorIdProgramaDTO item in listaFechaInicio)
            {
                if (item.Tipo.ToLower() == "online asincronica")
                {
                    DateTime fechaAOnine;

                    if (DateTime.Now.Day < 25) { fechaAOnine = DateTime.Now; }
                    else { fechaAOnine = DateTime.Now.AddDays(8); }
                    item.FechaInicio = fechaAOnine;
                    item.FechaInicioTexto = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fechaAOnine.ToString("MMMM yyyy"));
                }
                else
                {
                    if (item.FechaInicio == null)
                    {
                        var rptFecha = inicioFechas.Where(x => x.IdPEspecifico == item.Id && x.FechaHoraInicio.Value > DateTime.Now).OrderBy(x => x.FechaHoraInicio).Select(x => x.FechaHoraInicio).FirstOrDefault();

                        if (rptFecha != null)
                        {
                            item.FechaInicio = rptFecha.Value;
                            item.FechaInicioTexto = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.FechaInicio.Value.ToString("dd MMMM yyyy"));
                        }
                        else item.FechaInicioTexto = "Por definir";
                    }
                    else item.FechaInicioTexto = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(item.FechaInicio.Value.ToString("dd MMMM yyyy"));
                }
                listaPrevioPEspecifico.Add(item);
            }

            var listaCiudades = (from x in listaPrevioPEspecifico
                                 group x by new { x.Tipo, x.Ciudad } into gy
                                 select new
                                 {
                                     Tipo = gy.Key.Tipo,
                                     Ciudad = gy.Key.Ciudad
                                 }).ToList();

            List<ListaProgramaEspecificoPorIdProgramaDTO> listaEspecificos = new List<ListaProgramaEspecificoPorIdProgramaDTO>();

            foreach (var item in listaCiudades)
            {
                var existeLanzamiento = (from x in listaPrevioPEspecifico where x.EstadoPId == 3 && x.Tipo == item.Tipo && x.Ciudad == item.Ciudad && x.IdCategoria != 14 && x.FechaInicio != null select x).FirstOrDefault();
                if (existeLanzamiento != null) listaEspecificos.Add(existeLanzamiento);
                else
                {
                    var existeOtras = listaPrevioPEspecifico.Where(x => x.Tipo == item.Tipo && x.Ciudad == item.Ciudad && x.IdCategoria != 14).OrderBy(x => x.FechaCreacion).FirstOrDefault();
                    if (existeOtras != null) listaEspecificos.Add(existeOtras);
                }
            }

            listaEspecificos.AddRange(listaPrevioPEspecifico.Where(x => x.IdCategoria == 14).ToList());

            listaEspecificos = listaEspecificos.Select(c =>
            {
                c.FechaInicioTexto = c.EstadoPId != 3 ? "Por definir" : c.FechaInicioTexto;
                return c;
            }).ToList();

            return listaEspecificos;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el armado del boton HTML mediante la urlVersion, validando que no sea nulo o vacio
        /// </summary>
        /// <param name="urlVersion"> Cadena de Versión Url </param>
        /// <returns>Boton HTML con la url incrustada de la version : string </returns>
        public string ObtenerUrlVersion(string urlVersion)
        {
            string valor = string.Empty;

            if (!string.IsNullOrEmpty(urlVersion))
                valor = "<a href='" + urlVersion + "' style='background-color: #3e8f3e;border-radius: 10px;padding:10px 10px;line-height: 1.5;text-decoration: none;color: #fff; font-family: Helvetica Neue,Helvetica,Arial,sans-serif;font-size: 16px'>Obtener acceso de prueba gratis</a>";

            return valor;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el armado del boton HTML mediante la urlBrochurePrograma, validando que no sea nulo o vacio
        /// </summary>
        /// <param name="urlBrochurePrograma"> url de brochure de programa </param>
        /// <returns>Boton HTML con la url incrustada del brochure : string </returns>
        public string ObtenerUrlBrochurePrograma(string urlBrochurePrograma)
        {
            string valor = string.Empty;

            if (!string.IsNullOrEmpty(urlBrochurePrograma))
                valor = "<a href='" + urlBrochurePrograma + "' style='background-color: #f5a623;border-radius: 10px;padding: 10px 10px;line-height: 1.5;text-decoration: none;color: #fff; font-family: Helvetica Neue,Helvetica,Arial,sans-serif;font-size: 16px'>Descargar brochure</a>";

            return valor;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene el armado del boton HTML mediante la urlDocumentoCronograma, validando que no sea nulo o vacio
        /// </summary>
        /// <param name="urlDocumentoCronograma"> cadena de url de Documento de Cronograma </param>
        /// <returns>Boton HTML con la url incrustada del cronograma : string </returns>
        public string ObtenerUrlDocumentoCronograma(string urlDocumentoCronograma)
        {
            string valor = string.Empty;

            if (!string.IsNullOrEmpty(urlDocumentoCronograma))
                valor = "<a href='" + urlDocumentoCronograma + "' style='background-color: #f5a623;border-radius: 10px;padding: 10px 10px;line-height: 1.5;text-decoration: none;color: #fff; font-family: Helvetica Neue,Helvetica,Arial,sans-serif;font-size: 16px'>Descargar cronograma</a>";

            return valor;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene la central con el anexo del personal
        /// </summary>
        /// <param name="central"> Central </param>
        /// <param name="anexo3Cx"> Anexo 3CX </param>
        /// <returns>Cadena con la central y el anexo del personal : string </returns>
        public string ObtenerTelefonoPersonal(string central, string anexo3Cx)
        {
            string valor;

            if (central == "192.168.0.20")
            {
                //aqp
                valor = "(51) 54 258787 - Anexo " + anexo3Cx;
            }
            else
            {
                if (central == "192.168.2.20")
                {
                    //lima
                    valor = "(51) 1 207 2770 - Anexo " + anexo3Cx;
                }
                else
                {
                    valor = "No registra central asignada";
                }
            }

            return valor;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene primera Fecha
        /// </summary>
        /// <param name="montName"> Monto Pago primera Cuota </param>
        /// <param name="diaInicio"> Día de inicio</param>
        /// <returns> DateTime </returns>
        private DateTime ObtenerPrimeraFecha(string montName, int diaInicio)
        {
            int tmp = 0;
            DateTime res = new DateTime();
            string[] ssize = montName.Split(new char[0]);
            string[] monthNames = (new System.Globalization.CultureInfo("en-US")).DateTimeFormat.MonthNames;
            for (int i = 0; i <= monthNames.Count() - 1; i++)
            {
                if (!ssize[0].Equals(monthNames[i])) continue;
                tmp = i;
                string tmpp;
                string tmppdia;
                if (tmp < 10)
                {
                    tmp++;
                    tmpp = "0" + tmp;
                    tmppdia = diaInicio < 10 ? "0" + diaInicio : diaInicio.ToString();
                }
                else
                {
                    tmp++;
                    tmpp = tmp.ToString();
                    tmppdia = diaInicio < 10 ? "0" + diaInicio : diaInicio.ToString();
                }
                string validFec = ssize[1] + tmpp + tmppdia;
                res = DateTime.ParseExact(validFec, "yyyyMMdd", CultureInfo.InvariantCulture);
            }
            return res;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Obtiene Tipo de Descuento
        /// </summary>
        /// <param name="va"> Precio matricula </param>
        /// <param name="des"> descuento </param>
        /// <returns> Tipo de descuento general : float </returns>
        private float TipoDescuentoGeneral(double? va, int des)
        {
            float valor = float.Parse(va.ToString());
            float des2 = float.Parse(des.ToString());
            var descuento = float.Parse(Convert.ToString(valor * des2 / 100));
            return (valor - descuento);
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Genera grid normal
        /// </summary>
        /// <param name="data"> Dato Compuesto de monto pago </param>
        /// <returns> List<PagoCuotaDTO> </returns>
        private List<PagoCuotaDTO> GenerarGridNormal(MontoPagoCompuestoDTO data)
        {
            string simbolo = string.Empty;

            var respuesta = _repMoneda.ObtenerMonedaPorId(data.mp_moneda);

            if (respuesta != null)
            {
                simbolo = respuesta.Simbolo;
            }
            List<PagoCuotaDTO> lista = new List<PagoCuotaDTO>();
            PagoCuotaDTO obj = new PagoCuotaDTO
            {
                numeroCuota = 1,
                cuotaDescripcion = "Contado",
                montoCuota = data.mp_cuotas
            };
            DateTime fpag = DateTime.Now;
            obj.fechapago = fpag;
            obj.montoCuotaDescuento = float.Parse(data.mp_cuotas.ToString());
            obj.ispagado = false;
            obj.es_matricula = true;
            obj.SimboloMoneda = simbolo;
            lista.Add(obj);
            return lista;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Genera Tabla De monto Pago pa Contado y Credito
        /// </summary>
        /// <param name="data"> datos Coumpuestos de MontoPago </param>
        /// <returns> List<PagoCuotaDTO> </returns>
        private List<PagoCuotaDTO> GenerarGridAmbos(MontoPagoCompuestoDTO data)
        {
            var numeroco = 1;
            List<PagoCuotaDTO> lista = new List<PagoCuotaDTO>();
            // Matriculas
            var tamanioMatricula = 0;
            tamanioMatricula = data.tp_fracciones_matricula;

            if (tamanioMatricula == 0) tamanioMatricula = 1;
            for (var j = 0; j < tamanioMatricula; j++)
            {
                PagoCuotaDTO obj = new PagoCuotaDTO
                {
                    numeroCuota = numeroco,
                    cuotaDescripcion = "Matricula " + numeroco,
                    montoCuota = data.mp_matricula
                };
                DateTime currentDate = DateTime.Now;
                obj.fechapago = currentDate;
                obj.montoCuotaDescuento = TipoDescuentoGeneral((data.mp_matricula / tamanioMatricula), data.tp_porcentaje_matricula);
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
            var tamanioCuotas = tamanio;
            var sinDescuento = data.mp_precio - data.mp_matricula;

            for (var i = 0; i < tamanio; i++)
            {
                numeroco = numeroco + 1;
                tamanioContador = tamanioContador + 1;
                PagoCuotaDTO obj = new PagoCuotaDTO
                {
                    numeroCuota = numeroco,
                    cuotaDescripcion = "Cuota - " + numeroco,
                    montoCuota = data.mp_cuotas,
                    montoCuotaDescuento =
                        TipoDescuentoGeneral(sinDescuento / tamanioCuotas, data.tp_porcentaje_cuotas),
                    ispagado = false,
                    es_matricula = false
                };
                DateTime fecha = CalcularFechaInicial(data, i);
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
                        obj1.cuotaDescripcion = "Cuota - " + numeroco;
                        obj1.montoCuota = data.mp_cuotas;
                        obj1.montoCuotaDescuento = TipoDescuentoGeneral(sinDescuento / tamanioCuotas, data.tp_porcentaje_cuotas);
                        obj.es_matricula = false;
                        obj1.fechapago = CalcularFechaInicial(data, i);
                        lista.Add(obj1);
                        tamanio = tamanio - 1;
                    }
                }
            }
            return lista;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Genera Html De Precio en Cuotas
        /// </summary>
        /// <param name="data"> información de monto de pago compuesto </param>
        /// <returns> string </returns>
        public string GeneraHtmlPrecioCuotas(MontoPagoCompuestoDTO data)
        {
            string tabla = string.Empty;
            string moneda = string.Empty;

            var respuesta = _repMoneda.ObtenerMonedaPorId(data.mp_moneda);

            if (respuesta != null)
            {
                moneda = respuesta.Simbolo;
            }

            if (!string.IsNullOrEmpty(moneda))
            {
                tabla = "1 Matricula de " + moneda.Replace(".", " ") + " " + data.mp_matricula + " y " + data.mp_nro_cuotas + " cuotas de " + moneda + " " + data.mp_cuotas;
            }
            return tabla;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Genera grid de matrícula
        /// </summary>
        /// <param name="data"> Información de monto pago compuesto </param>
        /// <returns> List<PagoCuotaDTO> </returns>
        private List<PagoCuotaDTO> GenerarGridMatricula(MontoPagoCompuestoDTO data)
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
                obj.montoCuotaDescuento = TipoDescuentoGeneral((data.mp_matricula / tamanioMatricula), data.tp_porcentaje_matricula);
                obj.ispagado = false;
                obj.es_matricula = true;
                lista.Add(obj);
                numeroco = numeroco + 1;
            }
            // Cuotas
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
                obj.montoCuotaDescuento = TipoDescuentoGeneral(data.mp_cuotas, data.tp_porcentaje_cuotas);
                obj.ispagado = false;
                obj.es_matricula = false;
                fecha = CalcularFechaInicial(data, i);
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
                        obj1.montoCuotaDescuento = TipoDescuentoGeneral(data.mp_cuotas, data.tp_porcentaje_cuotas);
                        obj.es_matricula = false;
                        obj1.fechapago = CalcularFechaInicial(data, i);
                        lista.Add(obj1);
                        tamanio = tamanio - 1;
                    }
                }
            }
            return lista;
        }

        /// Autor: _ _ _ _ _ _ .
        /// Fecha: 30/04/2021
        /// Version: 1.0
        /// <summary>
        /// Genera una Tabla Para el Precio al Contado de un Programa 
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad </param>
        /// <returns> string </returns>
        private string ObtenerPrecioContado(int idOportunidad)
        {
            string respuesta = "";
            var contado = _repMontoPago.ObtenerMontoPagoContadoPorIdOportunidad(idOportunidad);
            if (contado != null) respuesta = GenerateGridCronogamaPagos(contado);

            return respuesta;
        }

        /// Autor: Gian Miranda
        /// Fecha: 18/05/2021
        /// Version: 1.0
        /// <summary>
        /// Genera la plantilla basica por etiqueta de curso del area
        /// </summary>
        /// <param name="idOportunidad"> Id de Oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <param name="idEtiqueta">Id de la lista curso area(PK de la tabla mkt.T_ListaCursoAreaEtiqueta)</param>
        /// <returns>String</returns>
        public string EtiquetaListaProgramasPorIdEtiqueta(int idOportunidad, int idEtiqueta)
        {
            string resultado = string.Empty;
            int contador = 1;
            var oportunidad = _repOportunidad.FirstById(idOportunidad);

            if (oportunidad != null)
            {
                var programas = _repPlantillaClaveValor.ObtenerMontosCursosRelacionados(idOportunidad, idEtiqueta);

                if (programas == null) return resultado;

                foreach (var item in programas)
                {
                    try
                    {
                        string url_video = string.Empty;

                        if (item.Url_Video != null)
                        {
                            url_video = "<a href='https://" + item.Url_Video + "' target = '_blank' >" + "Ver Presentaci&oacute;n" + "</a>";
                        }

                        resultado = resultado + "<p><b>" + contador.ToString() + ". " + item.Nombre + "</b></p>";
                        resultado = resultado + "<p><b>Modalidad: </b>" + item.Modalidad + " " + "<b> Duraci&oacute;n: </b>" + " " + item.Duracion + "<br/>";
                        resultado = resultado + "<b>Presentaci&oacute;n: </b>" + url_video + "<br/>";
                        resultado = resultado + "<b>Inversi&oacute;n Desde: </b>" + item.Inversion + "<br/>";
                    }
                    catch (Exception)
                    {
                    }
                    contador++;
                }
            }

            return resultado;
        }

        /// Autor: Edgar Serruto
        /// Fecha: 23/07/2021
        /// Version: 1.0
        /// <summary>
        /// Genera Etiqueta de encabezado por Id de Programa General
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General</param>
        /// <returns>String</returns>
        public string ObtenerEtiquetaEncabezado(int idPGeneral)
        {
            string resultado = string.Empty;
            var encabezado = _repDocumentoSeccionPw.ObtenerDatosComplementariosProgramaGeneralV2(idPGeneral).Where(x => x.Titulo == "Encabezado").Select(x => x.Contenido).FirstOrDefault();
            if (encabezado != null)
            {
                resultado = encabezado;
            }
            return resultado;
        }
        /// Autor: Edgar Serruto
        /// Fecha: 23/07/2021
        /// Version: 1.0
        /// <summary>
        /// Genera Etiqueta de Datos de Empresa por Id de Programa General
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General</param>
        /// <returns>String</returns>
        public string ObtenerEtiquetaDatosEmpresa(int idPGeneral)
        {
            string resultado = string.Empty;
            var encabezado = _repDocumentoSeccionPw.ObtenerDatosComplementariosProgramaGeneralV2(idPGeneral).Where(x => x.Titulo == "Datos Empresa").Select(x => x.Contenido).FirstOrDefault();
            if (encabezado != null)
            {
                resultado = encabezado;
            }
            return resultado;
        }
        /// Autor: Edgar Serruto
        /// Fecha: 23/07/2021
        /// Version: 1.0
        /// <summary>
        /// Genera Etiqueta de Pie de página por Id de Programa General
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General</param>
        /// <returns>String</returns>
        public string ObtenerEtiquetaPiePagina(int idPGeneral)
        {
            string resultado = string.Empty;
            var encabezado = _repDocumentoSeccionPw.ObtenerDatosComplementariosProgramaGeneralV2(idPGeneral).Where(x => x.Titulo == "Pie de p&#225;gina").Select(x => x.Contenido).FirstOrDefault();
            if (encabezado != null)
            {
                resultado = encabezado;
            }
            return resultado;
        }
        /// Autor: Edgar Serruto
        /// Fecha: 23/07/2021
        /// Version: 1.0
        /// <summary>
        /// Genera Etiqueta de Texto Complemento 1 por Id de Programa General
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General</param>
        /// <returns>String</returns>
        public string ObtenerEtiquetaPrimerTextoComplemento(int idPGeneral)
        {
            string resultado = string.Empty;
            var encabezado = _repDocumentoSeccionPw.ObtenerDatosComplementariosProgramaGeneralV2(idPGeneral).Where(x => x.Titulo == "Texto Complemento 1").Select(x => x.Contenido).FirstOrDefault();
            if (encabezado != null)
            {
                resultado = encabezado;
            }
            return resultado;
        }
        /// Autor: Edgar Serruto
        /// Fecha: 23/07/2021
        /// Version: 1.0
        /// <summary>
        /// Genera Etiqueta de Texto Complemento 2 por Id de Programa General
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General</param>
        /// <returns>String</returns>
        public string ObtenerEtiquetaSegundoTextoComplemento(int idPGeneral)
        {
            string resultado = string.Empty;
            var encabezado = _repDocumentoSeccionPw.ObtenerDatosComplementariosProgramaGeneralV2(idPGeneral).Where(x => x.Titulo == "Texto Complemento 2").Select(x => x.Contenido).FirstOrDefault();
            if (encabezado != null)
            {
                resultado = encabezado;
            }
            return resultado;
        }
        /// Autor: Edgar Serruto
        /// Fecha: 23/07/2021
        /// Version: 1.0
        /// <summary>
        /// Genera Etiqueta de Texto Complemento 3 por Id de Programa General
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General</param>
        /// <returns>String</returns>
        public string ObtenerEtiquetaTercerTextoComplemento(int idPGeneral)
        {
            string resultado = string.Empty;
            var encabezado = _repDocumentoSeccionPw.ObtenerDatosComplementariosProgramaGeneralV2(idPGeneral).Where(x => x.Titulo == "Texto Complemento 3").Select(x => x.Contenido).FirstOrDefault();
            if (encabezado != null)
            {
                resultado = encabezado;
            }
            return resultado;
        }
        /// Autor: Edgar Serruto
        /// Fecha: 23/07/2021
        /// Version: 1.0
        /// <summary>
        /// Genera Etiqueta de Imagen de Programa por Id de Programa General
        /// </summary>
        /// <param name="idPGeneral"> Id de Programa General</param>
        /// <returns>String</returns>
        public string ObtenerEtiquetaImagenPrograma(int idPGeneral)
        {
            string resultado = string.Empty;
            var encabezado = _repDocumentoSeccionPw.ObtenerDatosComplementariosProgramaGeneralV2(idPGeneral).Where(x => x.Titulo == "Imagen de programa").Select(x => x.Contenido).FirstOrDefault();
            if (encabezado != null)
            {
                resultado = encabezado;
            }
            return resultado;
        }

        /// Autor: Edgar Serruto
        /// Fecha: 23/07/2021
        /// Version: 1.0
        /// <summary>
        /// Genera Etiqueta de Ícono de Facebook según Id de Pais
        /// </summary>
        /// <returns>String</returns>
        public string ObtenerIconoFacebookUrlRedireccionadoSegunPais(int? idPais)
        {
            string resultado;

            if (idPais == ValorEstatico.IdPaisPeru)
                resultado = "<a href='https://web.facebook.com/BSG-Institute-Per%C3%BA-338770777361855' target='_blank'><img src='https://repositorioweb.blob.core.windows.net/tempemails/mailing/20210723205739-iconoFacebook' style='width:50px'></a>";
            else if (idPais == ValorEstatico.IdPaisColombia)
                resultado = "<a href='https://web.facebook.com/BSG-Institute-Colombia-102432078537107' target='_blank'><img src='https://repositorioweb.blob.core.windows.net/tempemails/mailing/20210723205739-iconoFacebook' style='width:50px'></a>";
            else if (idPais == ValorEstatico.IdPaisBolivia)
                resultado = "<a href='https://web.facebook.com/BSG-Institute-Bolivia-101106472006899' target='_blank'><img src='https://repositorioweb.blob.core.windows.net/tempemails/mailing/20210723205739-iconoFacebook' style='width:50px'></a>";
            else if (idPais == -1)/*Carreras Profesionales*/
                resultado = "<a href='https://web.facebook.com/BSGInstitute.Carreras.Profesionales' target='_blank'><img src='https://repositorioweb.blob.core.windows.net/tempemails/mailing/20210723205739-iconoFacebook' style='width:50px'></a>";
            else //Otro pais o nulo
                resultado = "<a href='https://web.facebook.com/BSGInstituteOficial/' target='_blank'><img src='https://repositorioweb.blob.core.windows.net/tempemails/mailing/20210723205739-iconoFacebook' style='width:50px'></a>";

            return resultado;
        }

        /// Autor: Edgar Serruto
        /// Fecha: 23/07/2021
        /// Version: 1.0
        /// <summary>
        /// Genera Etiqueta de Ícono de Instagram
        /// </summary>
        /// <returns>String</returns>
        public string ObtenerIconoInstagramUrlRedireccionado()
        {
            return "<a href='https://www.instagram.com/bsg_institute/?hl=es-la' target='_blank'><img src='https://repositorioweb.blob.core.windows.net/tempemails/mailing/20210723205829-iconoinstagram.png' style='width:50px'></a>";
        }
        /// Autor: Edgar Serruto
        /// Fecha: 23/07/2021
        /// Version: 1.0
        /// <summary>
        /// Genera Etiqueta de Ícono de Url Estandar de redirección
        /// </summary>
        /// <returns>String</returns>
        public string ObtenerIconoUrlEstandar()
        {
            return "<a href='https://bsginstitute.com/' target='_blank'><img src='https://repositorioweb.blob.core.windows.net/tempemails/mailing/20210723210924-iconourlestandar.png' style='width:50px'></a>";
        }
    }
}
