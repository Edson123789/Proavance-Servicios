using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Transversal.Repositorio;
using BSI.Integra.Persistencia.Models;
using BSI.Integra.Persistencia.SCode.Repository;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class CampaniaMailingDetalleBO : BaseBO
    {
        public int IdCampaniaMailing { get; set; }
        public int Prioridad { get; set; }
        public int Tipo { get; set; }
        public int IdRemitenteMailing { get; set; }
        public int IdPersonal { get; set; }
        public string Subject { get; set; }
        public DateTime FechaEnvio { get; set; }
        public int IdHoraEnvio { get; set; }
        public int Proveedor { get; set; }
        public int EstadoEnvio { get; set; }
        public int? IdFiltroSegmento { get; set; }
        public int IdPlantilla { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public string Campania { get; set; }
        public string CodMailing { get; set; }
        public int? CantidadContactos { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? IdConjuntoListaDetalle { get; set; }
        public int? IdCentroCosto { get; set; }
        public bool? EsSubidaManual { get; set; }

        public List<CampaniaMailingDetalleProgramaBO> listaCampaniaMailingDetalleProgramaBO;
        public List<AreaCampaniaMailingDetalleBO> AreaCampaniaMailingDetalle;
        public List<SubAreaCampaniaMailingDetalleBO> SubAreaCampaniaMailingDetalle;

        integraDBContext contexto;
        private DapperRepository dapperRepository;
        public CampaniaMailingDetalleBO()
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            contexto = new integraDBContext();
            dapperRepository = new DapperRepository(contexto);
        }

        public CampaniaMailingDetalleBO(integraDBContext integraDBContext)
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            contexto = integraDBContext;
            dapperRepository = new DapperRepository(contexto);
        }


        public string GetContenidoHorarios(int idPGeneral) {
            try
            {
                PgeneralRepositorio _repPGeneral = new PgeneralRepositorio();
                var modalidades = _repPGeneral.ObtenerModalidadesPorProgramaGeneral(idPGeneral);
                var secciones = _repPGeneral.ObtenerSeccionesInformacionProgramaPorProgramaGeneral(idPGeneral);
                string contenido = secciones.Where(w => w.Titulo.ToLower().Equals("duración y horarios")).Select(w => w.Contenido).FirstOrDefault();
                return this.GetContenidoHorarios(modalidades, contenido, idPGeneral);
            }
            catch (Exception e)
            {
                return "";
                //throw new Exception(e.Message);
            }
        }
        public string GetContenidoHorarios(List<ModalidadProgramaDTO> modalidades, string initContenido, int idPGeneral)
        {
            PGeneralParametroSeoPwRepositorio _repPGeneralParametroSeoPw = new PGeneralParametroSeoPwRepositorio();
            PgeneralRepositorio _repPGeneral = new PgeneralRepositorio();
            ExcepcionFrecuenciaPwRepositorio _repExcepcionFrecuenciaPw = new ExcepcionFrecuenciaPwRepositorio();
            string _filtroVista = string.Empty;

            var parametrosSEO = _repPGeneralParametroSeoPw.ObtenerParametrosSEOPorIdPGeneral(idPGeneral);
            var excepcionesFrecuencia = _repExcepcionFrecuenciaPw.ObtenerTodoProgramaGeneral();
            var padreEspecificoHijo = _repPGeneral.ObtenerPadreHijoEspecifico(idPGeneral);
            var obtenerFrecuencia = _repPGeneral.ObtenerFrecuenciasPorIdPGeneral(idPGeneral);
            var especificoSesion = _repPGeneral.ObtenerSesionesPorProgramaGeneral(idPGeneral);
            var tipo = parametrosSEO.Where(w => w.Nombre.Equals("description")).FirstOrDefault();
            string presencialDatos = "";
            string sincronicoDatos = "";
            string extraPresencial = "";
            string extraSincrono = "";

            var programaGeneral = modalidades.FirstOrDefault() == null && modalidades.Count() != 0 ? new ModalidadProgramaDTO { Pw_duracion = "0", NombrePG = "" } : modalidades.FirstOrDefault();
            //programaGeneral = programaGeneral == null ? new ModalidadProgramaDTO { } : programaGeneral;
            if (programaGeneral != null)
            {
                if (programaGeneral.NombrePG.Contains("Curso") || programaGeneral.NombrePG.Contains("Programa"))
                {
                    //plantilla
                    presencialDatos = "<p><strong>En la modalidad Presencial:</strong></p><p>El ##CURSODIPLOMA## se desarrolla en el siguiente horario (*): </p>";
                    sincronicoDatos = "<p><strong>En la modalidad Online:</strong></p><p> El ##CURSODIPLOMA## tiene una duración de ##DURACIONPE## cronológicas. Las clases se desarrollarán de forma virtual, con una frecuencia ##FRECUENCIA## en el siguiente horario (*): </p>";
                    extraPresencial = "<p><i> (*)Para más detalle sobre fechas y horarios solicite su cronograma de alumnos.</i><p/>";
                    extraSincrono = "<p><i> (*)Para más detalle sobre fechas y horarios solicite su cronograma de alumnos.</i><p/>";
                }
                else
                {
                    if (tipo.Descripcion.Contains("Curso"))
                    {
                        //plantilla
                        presencialDatos = "<p><strong>En la modalidad Presencial:</strong></p><p> El Curso ##CURSODIPLOMA## se desarrolla en el siguiente horario (*): </p>";
                        sincronicoDatos = "<p><strong>En la modalidad Online:</strong></p><p> El Curso ##CURSODIPLOMA## tiene una duración de ##DURACIONPE## cronológicas. Las clases se desarrollarán de forma virtual, con una frecuencia ##FRECUENCIA## en el siguiente horario (*): </p>";
                        extraPresencial = "<p><i> (*)Para más detalle sobre fechas y horarios solicite su cronograma de alumnos.</i><p/>";
                        extraSincrono = "<p><i> (*)Para más detalle sobre fechas y horarios solicite su cronograma de alumnos.</i><p/>";
                    }
                    if (tipo.Descripcion.Contains("Programa"))
                    {
                        //plantilla
                        presencialDatos = "<p><strong>En la modalidad Presencial:</strong></p><p> El Programa ##CURSODIPLOMA## se desarrolla en el siguiente horario (*): </p>";
                        sincronicoDatos = "<p><strong>En la modalidad Online:</strong></p><p> El Programa ##CURSODIPLOMA## tiene una duración de ##DURACIONPE## cronológicas. Las clases se desarrollarán de forma virtual, con una frecuencia ##FRECUENCIA## en el siguiente horario (*): </p>";
                        extraPresencial = "<p><i> (*)Para más detalle sobre fechas y horarios solicite su cronograma de alumnos.</i><p/>";
                        extraSincrono = "<p><i> (*)Para más detalle sobre fechas y horarios solicite su cronograma de alumnos.</i><p/>";
                    }
                }
            }

            var programaEspecifico = modalidades;
            string frecuencia = "";
            string todohtmlpresencial = this.ObtenerContenidoPresencial(programaEspecifico.Where(w => w.Tipo == "Presencial").ToList(), true, excepcionesFrecuencia, padreEspecificoHijo, obtenerFrecuencia, especificoSesion);
            if (todohtmlpresencial == "")
            {
                presencialDatos = "";
                extraPresencial = "";
            }
            string todohtmlsincrono = this.ObtenerContenidoPresencial(programaEspecifico.Where(w => w.Tipo == "Online Sincronica").ToList(), false, excepcionesFrecuencia, padreEspecificoHijo, obtenerFrecuencia, especificoSesion);
            if (todohtmlsincrono == "")
            {
                sincronicoDatos = "";
                extraSincrono = "";
            }
            else
            {
                var sincrona = modalidades.Where(x => x.Tipo == "Online Sincronica").FirstOrDefault();
                if (sincrona.NombreESP.Contains("Curso"))
                {
                    var padre = padreEspecificoHijo.Where(w => w.IdPespecificoHijo == sincrona.IdPEspecifico).FirstOrDefault();
                    if (padre != null)
                        frecuencia = obtenerFrecuencia.Where(w => w.IdPEspecifico == padre.IdPespecificoHijo).Select(w => w.Nombre).FirstOrDefault();//this.ObtenerFrecuencia(padre.PEspecificoPadreId);
                    else
                        frecuencia = obtenerFrecuencia.Where(w => w.IdPEspecifico == sincrona.IdPEspecifico).Select(w => w.Nombre).FirstOrDefault();//this.ObtenerFrecuencia(sincrona.id);
                }
                else
                    frecuencia = frecuencia = obtenerFrecuencia.Where(w => w.IdPEspecifico == sincrona.IdPEspecifico).Select(w => w.Nombre).FirstOrDefault();
            }
            string seContenido = initContenido;
            seContenido = presencialDatos + todohtmlpresencial + extraPresencial + sincronicoDatos + todohtmlsincrono + extraSincrono + seContenido;

            if (programaEspecifico.Count() != 0)
            {
                string temppe = seContenido.Replace("##DURACIONPE##", Convert.ToInt32(double.Parse(programaEspecifico.FirstOrDefault().Duracion, CultureInfo.InvariantCulture)) + " Horas").Replace("##DURACIONPG##", programaGeneral.Pw_duracion).Replace("##CURSODIPLOMA##", programaGeneral.NombrePG).Replace("##FRECUENCIA##", frecuencia);
                temppe = temppe.Replace("®", "<sup>®</sup>");
                seContenido = temppe;
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

        private string ObtenerContenidoPresencial(List<ModalidadProgramaDTO> pEspecificos, bool presencial, List<ExcepcionFrecuenciaPGeneralDTO> excepcionesFrecuencia, List<PadrePespecificoHijoDTO> padreEspecificoHijo, List<FrecuenciaProgramaGeneralDTO> frecuenciaProgramas, List<PEspecificoSesionDTO> especificoSesion)
        {
            string horariosP1 = "<li>##DIA##: ##HORAINICIO## a ##HORAFIN## horas.</li>";
            string horariosP2 = "<li>##DIA##: ##HORAINICIO## a ##HORAFIN## - ##HORAINICIO2## a ##HORAFIN2## horas.</li>";
            string datosCiudad = "<p>En ##CIUDAD## con una frecuencia ##FRECUENCIA##:</p> <ul class='list'>##HORARIOS##</ul>";
            string datosSinCiudad = "<ul class='list'>##HORARIOS##</ul>";
            string[] arreglos = new string[pEspecificos.ToList().Count];
            int contador = 0;
            string strHTML = "";
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
                        var padre = padreEspecificoHijo.Where(w => w.IdPespecificoHijo == item.IdPEspecifico).FirstOrDefault();
                        if (padre != null)
                            frecuencia = frecuenciaProgramas.Where(w => w.IdPEspecifico == padre.IdPespecificoPadre).Select(w => w.Nombre).FirstOrDefault();
                        else
                        {
                            frecuencia = frecuenciaProgramas.Where(w => w.IdPEspecifico == item.IdPEspecifico).Select(w => w.Nombre).FirstOrDefault();
                        }
                    }
                    else
                    {
                        frecuencia = frecuenciaProgramas.Where(w => w.IdPEspecifico == item.IdPEspecifico).Select(w => w.Nombre).FirstOrDefault();
                    }
                }
                List<PEspecificoSesionDTO> sesionestemp = new List<PEspecificoSesionDTO>();
                if (item.NombreESP.Contains("Curso"))
                {
                    var hijo = item;
                    //obtener las sesiones del pespcifico
                    sesionestemp = especificoSesion.Where(w => w.IdPEspecifico == hijo.IdPEspecifico && w.Predeterminado == true).ToList();
                }
                else
                {
                    //obtenemos un pespecifico
                    var hijo = padreEspecificoHijo.Where(w => w.IdPespecificoPadre == item.IdPEspecifico).ToList();
                    if (hijo.Count() == 0)
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
                List<SesionTempDTO> lista = new List<SesionTempDTO>();
                //llenas las sesiones  dia horainicio horafin
                foreach (var item3 in sesionestemp.OrderBy(w => w.FechaHoraInicio))
                {
                    SesionTempDTO itemlista = new SesionTempDTO();

                    itemlista.Dia = new CultureInfo("es-PE", false).TextInfo.ToTitleCase(item3.FechaHoraInicio.Value.ToString("dddd"));
                    //itemlista.Dia
                    if (itemlista.Dia == "Monday")
                    {
                        itemlista.Dia = "Lunes";
                    }
                    else if (itemlista.Dia == "Tuesday")
                    {
                        itemlista.Dia = "Martes";
                    }
                    else if (itemlista.Dia == "Wednesday")
                    {
                        itemlista.Dia = "Miércoles";
                    }
                    else if (itemlista.Dia == "Thursday")
                    {
                        itemlista.Dia = "Jueves";
                    }
                    else if (itemlista.Dia == "Friday")
                    {
                        itemlista.Dia = "Viernes";
                    }
                    else if (itemlista.Dia == "Saturday")
                    {
                        itemlista.Dia = "Sábado";
                    }
                    else if (itemlista.Dia == "Sunday")
                    {
                        itemlista.Dia = "Domingo";
                    }
                    //Dia = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(item3.FechaHoraInicio.Value.ToString("dddd"));
                    itemlista.Idciudad = this.GetIdDia(CultureInfo.InvariantCulture.TextInfo.ToTitleCase(item3.FechaHoraInicio.Value.ToString("dddd")));
                    itemlista.Horainicio = item3.FechaHoraInicio.Value.ToString("HH:mm");
                    itemlista.Veces = 1;
                    itemlista.Horafin = item3.FechaHoraInicio.Value.AddHours(Convert.ToDouble(item3.Duracion)).ToString("HH:mm");
                    itemlista.Ciudad = item.Ciudad;

                    lista.Add(itemlista);
                }
                lista = lista.Distinct().ToList();

                List<SesionTempDTO> listaFinal = new List<SesionTempDTO>();
                SesionTempDTO itemTempListaFinal = new SesionTempDTO();
                foreach (var item_lista in lista)
                {
                    if (!listaFinal.Any(w => w.Dia == item_lista.Dia && w.Ciudad == item_lista.Ciudad && w.Horainicio == item_lista.Horainicio && w.Horafin == item_lista.Horafin))
                        listaFinal.Add(item_lista);
                    else
                    {
                        itemTempListaFinal = listaFinal.Where(w => w.Dia == item_lista.Dia && w.Ciudad == item_lista.Ciudad && w.Horainicio == item_lista.Horainicio && w.Horafin == item_lista.Horafin).FirstOrDefault();
                        listaFinal.Remove(itemTempListaFinal);
                        itemTempListaFinal.Veces++;
                        listaFinal.Add(itemTempListaFinal);
                    }

                }
                lista = listaFinal;

                List<SesionTempDTO> listafinal = new List<SesionTempDTO>();

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

        private int GetIdDia(string dia)
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

        /// <summary>
        /// Obtiene el contenido 
        /// </summary>
        /// <param name="idCampaniaMailingDetalle"></param>
        /// <returns></returns>
        public List<CampaniaMailingDetalleContenidoEtiquetaDTO> ObtenerContenido(int idCampaniaMailingDetalle) {
            try
            {
                List<CampaniaMailingDetalleContenidoEtiquetaDTO> detalle = new List<CampaniaMailingDetalleContenidoEtiquetaDTO>();
                CampaniaMailingDetalleProgramaRepositorio campaniaMailingDetalleProgramaRepositorio = new CampaniaMailingDetalleProgramaRepositorio();
                var listasPGeneral = campaniaMailingDetalleProgramaRepositorio.GetBy(x => x.IdCampaniaMailingDetalle == idCampaniaMailingDetalle && x.Tipo != "Filtro")
                    .Select(x =>
                       new { IdPGeneral = x.IdPgeneral, Etiqueta = string.Concat("*|PGDURACIONHORARIOS_PP", x.Orden, "|*"), Contenido = "" }
                    ).ToList().Distinct();

                foreach (var programaGeneral in listasPGeneral)
                {
                    detalle.Add(new CampaniaMailingDetalleContenidoEtiquetaDTO()
                    {
                        Etiqueta = programaGeneral.Etiqueta,
                        Contenido = this.GetContenidoHorarios(programaGeneral.IdPGeneral)
                    });
                }
                return detalle;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
                //return "";
            }
        }

        /// <summary>
        /// Reemplaza las etiquetas para el envio de correos de CampaniasMailing
        /// </summary>
        /// <param name="prioridad">Objeto de clase PrioridadesDTO</param>
        /// <param name="usuario">Usuario que realiza la ejecucion</param>
        /// <returns>Objeto de clase CampaniaMailingDetalleProgramaPlantillaDTO</returns>
        public CampaniaMailingDetalleProgramaPlantillaDTO ProcesarPrioridadMailchimp(PrioridadesDTO prioridad, string usuario)
        {
            CampaniaMailingDetalleRepositorio _repCampaniaMailingDetalle = new CampaniaMailingDetalleRepositorio(contexto);
            DocumentoSeccionPwRepositorio _repDocumentoSeccionPw = new DocumentoSeccionPwRepositorio(contexto);
            CampaniaMailingDetalleProgramaRepositorio _repCampaniaMailingDetallePrograma = new CampaniaMailingDetalleProgramaRepositorio(contexto);
            PgeneralExpositorRepositorio _repPGeneralExpositor = new PgeneralExpositorRepositorio(contexto);
            EtiquetaRepositorio _repEtiqueta = new EtiquetaRepositorio(contexto);

            CampaniaMailingDetalleProgramaPlantillaDTO plantillas = _repCampaniaMailingDetalle.ObtenerPlantillasInformacionAsesor(prioridad.Id);

            string telefono = "";
            if (plantillas.CentralPersonal == "192.168.0.20") telefono = "(51) 54 258787 - Anexo " + plantillas.AnexoPersonal;
            else if (plantillas.CentralPersonal == "192.168.2.20") telefono = "(51) 1 207 2770 - Anexo " + plantillas.AnexoPersonal;
            else telefono = "(51) 54 258787";

            plantillas.Contenido = plantillas.Contenido.Replace("*|APHONE|*",telefono);
            var Content = plantillas.Contenido;

            // Botonesprogramas
            var etiquetasWhatsapp = new List<string> { "*|BTNWA_PERU|*", "*|BTNWA_COLOMBIA|*", "*|BTNWA_BOLIVIA|*" };
            if (etiquetasWhatsapp.Any(Content.Contains))
            {
                List<CampaniaMailingDetalleContenidoEtiquetaDTO> botonesWhatsapp = _repCampaniaMailingDetalle.ObtenerBotonesWhatsapp(prioridad.Id);
                botonesWhatsapp = botonesWhatsapp.Where(x => etiquetasWhatsapp.Contains(x.Etiqueta)).ToList();
                foreach (var btnWhatsapp in botonesWhatsapp)
                {
                    Content = Content.Replace(btnWhatsapp.Etiqueta, btnWhatsapp.Contenido);
                }
            }

            var etiquetasMesenger = new List<string> { "*|BTNMSG|*" };
            if (etiquetasMesenger.Any(Content.Contains))
            {
                List<CampaniaMailingDetalleContenidoEtiquetaDTO> botonesMessenger = _repCampaniaMailingDetalle.ObtenerBotonesMessenger(prioridad.Id);
                botonesMessenger = botonesMessenger.Where(x => etiquetasMesenger.Contains(x.Etiqueta)).ToList();

                foreach (var btnMessenger in botonesMessenger)
                {
                    Content = Content.Replace(btnMessenger.Etiqueta, btnMessenger.Contenido);
                }
            }

            // Nuevas etiquetas
            List<CampaniaMailingDetalleContenidoEtiquetaDTO> docentes = _repCampaniaMailingDetalle.ObtenerExpositoresPorProgramaGeneral(prioridad.Id);
            foreach (var docente in docentes)
            {
                string contenidoDocente = string.Concat("<h3><strong>DOCENTES:</strong></h3><ul>", docente.Contenido,"</ul>");
                Content = Content.Replace(docente.Etiqueta, contenidoDocente);
            }

            List<CampaniaMailingDetalleContenidoEtiquetaDTO> duracionHorarios = this.ObtenerContenido(prioridad.Id);
            foreach (var duracionHorario in duracionHorarios)
            {
                string contenidoDuracionHorarios = string.Concat("<h3><strong>HORARIOS:</strong></h3><ul>", duracionHorario.Contenido,"</ul>");
                Content = Content.Replace(duracionHorario.Etiqueta, contenidoDuracionHorarios);
            }

            //fin nuevas etiquetas


            List<CampaniaMailingDetalleContenidoEtiquetaDTO> nombresProgramas = _repCampaniaMailingDetalle.ObtenerProgramaYEtiqueta(prioridad.Id);
            /// Reemplaza los nombres de los programas generales
            foreach (var programa in nombresProgramas)
            {
                Content = Content.Replace(programa.Etiqueta, programa.Contenido);
            }

            // Reemplaza los botones de más información
            List<CampaniaMailingDetalleContenidoEtiquetaDTO> botonesprogramas = _repCampaniaMailingDetalle.ObtenerInformacionBotonesyEtiqueta(prioridad.Id);
            foreach (var da in botonesprogramas)
            {
                Content = Content.Replace(da.Etiqueta, da.Contenido);
            }

            List<CampaniaMailingDetalleContenidoEtiquetaDTO> estructuraProgramas = _repDocumentoSeccionPw.ObtenerContenidoYEtiqueta(prioridad.Id);
            foreach (var estructura in estructuraProgramas)
            {
                var certificacion = "<ul>";
                var html = estructura.Contenido;
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
                Content = Content.Replace(estructura.Etiqueta, certificacion);
            }

            List<CampaniaMailingCertificacionDuracionDTO> certificacionProgramas = _repDocumentoSeccionPw.ObtenerCertificacionDuracionEtiqueta(prioridad.Id);
            foreach (var certificacion in certificacionProgramas)
            {
                certificacion.ContenidoCertificacion = "<h3><strong>CERTIFICACIÓN:</strong></h3><ul>" + certificacion.ContenidoCertificacion + "</ul>";
                certificacion.ContenidoDuracion = "<strong>Duración: </strong>" + certificacion.ContenidoDuracion;
                Content = Content.Replace(certificacion.EtiquetaCertificacion, certificacion.ContenidoCertificacion);
                Content = Content.Replace(certificacion.EtiquetaDuracion, certificacion.ContenidoDuracion);
            }

            List<CampaniaMailingPGeneralEtiquetaDTO> listaPGeneralEtiqueta = _repCampaniaMailingDetallePrograma.ObtenerProgramaYEtiqueta(prioridad.Id);

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
                Content = Content.Replace(pGeneralEtiqueta.Etiqueta, html);
            }

            // Vieja Logica para cambio de etiqueta basado en varios programas generales
            var listaDatosEmpresa = _repCampaniaMailingDetalle.ObtenerContenidoYEtiquetaDatosEmpresa(prioridad.Id);

            foreach (var datosEmpresa in listaDatosEmpresa)
                Content = Content.Replace(datosEmpresa.Etiqueta, datosEmpresa.Contenido);

            var listaEncabezado = _repCampaniaMailingDetalle.ObtenerContenidoYEtiquetaEncabezado(prioridad.Id);
            foreach (var encabezado in listaEncabezado)
                Content = Content.Replace(encabezado.Etiqueta, encabezado.Contenido);

            var listaPieDePagina = _repCampaniaMailingDetalle.ObtenerContenidoYEtiquetaDatosPiePagina(prioridad.Id);
            foreach (var pieDePagina in listaPieDePagina)
                Content = Content.Replace(pieDePagina.Etiqueta, pieDePagina.Contenido);

            var listaRedesSociales = _repCampaniaMailingDetalle.ObtenerContenidoYEtiquetaRedesSociales(prioridad.Id);
            foreach (var redSocial in listaRedesSociales)
                Content = Content.Replace(redSocial.Etiqueta, redSocial.Contenido);

            var listaImagenPrograma = _repCampaniaMailingDetalle.ObtenerContenidoYEtiquetaImagenPrograma(prioridad.Id);
            foreach (var imagenPrograma in listaImagenPrograma)
                Content = Content.Replace(imagenPrograma.Etiqueta, imagenPrograma.Contenido);

            var listaComplemento1 = _repCampaniaMailingDetalle.ObtenerContenidoYEtiquetaTextoComplemento1(prioridad.Id);
            foreach (var complemento1 in listaComplemento1)
                Content = Content.Replace(complemento1.Etiqueta, complemento1.Contenido);

            var listaComplemento2 = _repCampaniaMailingDetalle.ObtenerContenidoYEtiquetaTextoComplemento2(prioridad.Id);
            foreach (var complemento2 in listaComplemento2)
                Content = Content.Replace(complemento2.Etiqueta, complemento2.Contenido);

            var listaComplemento3 = _repCampaniaMailingDetalle.ObtenerContenidoYEtiquetaTextoComplemento3(prioridad.Id);
            foreach (var complemento3 in listaComplemento3)
                Content = Content.Replace(complemento3.Etiqueta, complemento3.Contenido);

            Content = Content.Replace("<vacio></vacio>", string.Empty);

            List<CampaniaMailingNombreProgramaDetalleDTO> listaNombreProgramaDetalle = _repDocumentoSeccionPw.ObtenerNombreContenidoEtiquetaDocumentoSeccion(prioridad.Id);
            List<CampaniaMailingNombreProgramaDetalleDTO> listaExpositor = _repPGeneralExpositor.ObtenerTop5PGeneralExpositor(prioridad.Id);

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
                Content = Content.Replace(pair.Key, pair.Value);
            }

            //eliminamos etiquetas sin reemplazar
            var etiquetasIntegra = _repEtiqueta.ObtenerEtiquetasIntegra();
            foreach (var etiqueta in etiquetasIntegra)
            {
                Content = Content.Replace(etiqueta,"");
            }
            //fin

            //Agregamos la etiqueta de identificador de la tabla T_PrioridadMailChimpListaCorreo
            //var _plantillaEtiqueta = @" <span style='display: none' id='prioridadMailChimpCorreoId'>*|IDPRIORIDADMAILCHIMPLISTACORREO|*</span> ";
            //var _plantillaEtiqueta = "<div id=\"prioridadMailChimpCorreoId\"><span>*|IDPMLC|*</span></div>";
            var _plantillaEtiqueta = "<span  style=\"display: none\" id=\"prioridadMailChimpCorreoId\">*|IDPMLC|*</span>";
            var _plantillaEtiquetaAgregado = "<span  style=\"display: none\" id=\"prioridadMailChimpCorreoId\">IdPrioridadMailChimpListaCorreo*|IDPMLC|*</span>";
            Content += _plantillaEtiqueta;
            Content += _plantillaEtiquetaAgregado;
            plantillas.Contenido = Content;

            return plantillas;
        }
        
        /// <summary>
        /// Obtiene las etiquetas de una plantilla.
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public List<string> ObtenerEtiquetas(string cadena)
        {
            List<string> etiquetas = cadena.Split(new string[] { "*|" }, StringSplitOptions.None).Where(o => o.Contains("|*")).Select(o => o.Split(new string[] { "|*" }, StringSplitOptions.None).First()).ToList();

            //elimina las etiquetas por defecto del mailchimp
            etiquetas.Remove("EMAIL");
            etiquetas.Remove("FNAME");
            etiquetas.Remove("LNAME");
            //etiquetas.Remove("IDPMLC");
            return etiquetas;
        }

        /// <summary>
        /// Resta a la fecha Actual la cantidad indicada por el usuario(dias, fechas, meses, años).
        /// </summary>
        /// <param name="cantidad"></param>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public DateTime GetFechaFiltro(int cantidad, string descripcion)
        {
            DateTime fecha = DateTime.Now;
            switch (descripcion)
            {
                case "Dias":
                    fecha = fecha.AddDays(-(cantidad));
                    break;
                case "Semanas":
                    fecha = fecha.AddDays(-(cantidad * 7));
                    break;
                case "Meses":
                    fecha = fecha.AddMonths(-(cantidad));
                    break;
                case "Anios":
                    fecha = fecha.AddYears(-(cantidad));
                    break;
                case "Todos":
                    fecha = fecha.AddYears(-(20));
                    break;
                default:
                    break;
            }
            return fecha;
        }

        /// <summary>
        /// Obtiene el html para enviar el mesaje correcto
        /// </summary>
        /// <param name="listas"></param>
        /// <returns></returns>
        public string GenerarPlantillaNotificacionProcesamientoCorrecto(List<MensajeProcesarDTO> listas)
        {

            var texto = @"
                <HTML><STYLE TYPE='text/css'><!-@page { margin: 1in }P { margin-bottom: 0.08in; line-height: 107%; text-align: left }P.western { font-family: 'Arial', sans-serif; font-size: 16pt; so-language: en-US }P.cjk { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: en-US }P.ctl { font-family: 'Calibri', sans-serif; font-size: 11pt; so-language: ar-SA }A:link { color: #0563c1 }A.ctl:link { font-family: 'Times New Roman', serif }	--></STYLE>
                <head> <style type='text/css' media='screen'> body{font-family: Arial, sans-serif; font-size: 12pt; line-height: 20px;} </style> </head>";
            texto += $@"
                <BODY LANG='en-US' LINK='#0563c1' DIR='LTR' STYLE='border: none; padding: 0in'>";
            foreach (var item in listas)
            {
                texto += $@"<p style='font-size:10pt;'> { item.Nombre }</p>
                <table style='border: 1px solid #e6e6e6;border-collapse:collapse' border='' cellspacing='0' cellpadding='2'>
                    <tbody>
                        <tr>
                            <th style='border: 1px solid #e6e6e6' bgcolor='#FAFAFA'>Nombre campaña</th>
                            <th style='border: 1px solid #e6e6e6' bgcolor='#FAFAFA'>Nombre lista</th>
                            <th style='border: 1px solid #e6e6e6' bgcolor='#FAFAFA'>Nro intentos</th>
                        </tr>";
                foreach (var detalle in item.ListaDetalle)
                {
                    texto += $@"
                        <tr>
                            <th style='border: 1px solid #e6e6e6; font-weight: 100;'> {detalle.NombreCampania} </th>
                            <th style='border: 1px solid #e6e6e6; font-weight: 100;'> {detalle.NombreLista} </th>
                            <th style='border: 1px solid #e6e6e6; font-weight: 100;'> {detalle.NroIntentos} </th>
                        </tr>";
                }
                texto += $@"
                    </tbody>
                </table> ";
            }
            texto += "</BODY></HTML>";
            return texto;
        }

    }
}
