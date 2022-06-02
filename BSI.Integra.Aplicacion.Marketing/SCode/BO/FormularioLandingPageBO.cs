using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class FormularioLandingPageBO : BaseBO
    {
        public int IdFormularioSolicitud { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int Header { get; set; }
        public int Footer { get; set; }
        public int IdPlantillaLandingPage { get; set; }
        public string Mensaje { get; set; }
        public string TextoPopup { get; set; }
        public string TituloPopup { get; set; }
        public string ColorPopup { get; set; }
        public string ColorTitulo { get; set; }
        public string ColorTextoBoton { get; set; }
        public string ColorFondoBoton { get; set; }
        public string ColorDescripcion { get; set; }
        public bool EstadoPopup { get; set; }
        public int? IdPespecifico { get; set; }
        public string ColorFondoHeader { get; set; }
        public string Tipo { get; set; }
        public string Cita1Texto { get; set; }
        public string Cita1Color { get; set; }
        public string Cita3Texto { get; set; }
        public string Cita3Color { get; set; }
        public string Cita4Texto { get; set; }
        public string Cita4Color { get; set; }
        public string Cita1Despues { get; set; }
        public bool MuestraPrograma { get; set; }
        public string UrlImagenPrincipal { get; set; }
        public string ColorPlaceHolder { get; set; }
        public int? IdGmailClienteRemitente { get; set; }
        public int? IdGmailClienteReceptor { get; set; }
        public int? IdPlantilla { get; set; }
        public bool? TesteoAb { get; set; }
        public Guid? IdMigracion { get; set; }
        public bool? TituloProgramaAutomatico { get; set; }
        public bool? DescripcionWebAutomatico { get; set; }

        //Hijos BO
        public List<DatoAdicionalPaginaBO> DatoAdicionalPagina;


        public string ObtenerVistaPreviaPlantilla(FormularioLandingPageDTO formularioLandingPage, List<DatoAdicionalPaginaDTO> datosAdicionales)
        {
            try
            {
                string plantilla_vista = string.Empty;

                PlantillaLandingPageRepositorio plantillaLandingPageRepositorio = new PlantillaLandingPageRepositorio();
                var datos_plantilla = plantillaLandingPageRepositorio.FirstById(formularioLandingPage.IdPlantillaLandingPage);

                FormularioSolicitudRepositorio formularioSolicitudRepositorio = new FormularioSolicitudRepositorio();
                var formulariosolicitud = formularioSolicitudRepositorio.FirstById(formularioLandingPage.IdFormularioSolicitud);

                FormularioSolicitudTextoBotonRepositorio formularioSolicitudTextoBotonRepositorio = new FormularioSolicitudTextoBotonRepositorio();
                var boton = formularioSolicitudTextoBotonRepositorio.FirstById(formulariosolicitud.IdFormularioSolicitudTextoBoton);

                FormularioRespuestaRepositorio formularioRespuestaRepositorio = new FormularioRespuestaRepositorio();
                var form_respuesta = formularioRespuestaRepositorio.FirstById(formulariosolicitud.IdFormularioRespuesta ?? 0);

                PlantillaLandingPagePgeneralAdicionalRepositorio plantillaLandingPagePgeneralAdicionalRepositorio = new PlantillaLandingPagePgeneralAdicionalRepositorio();
                var listaAdicionales = plantillaLandingPagePgeneralAdicionalRepositorio.GetBy(x => x.IdPlantillaLandingPage == formularioLandingPage.IdPlantillaLandingPage).ToList();

                ListaPlantillaRepositorio listaPlantillaRepositorio = new ListaPlantillaRepositorio();
                var listRpta  = listaPlantillaRepositorio.FirstById(datos_plantilla.IdListaPlantilla??0);

                //var boton = _tmk_textobotonesformsolicitudService.GetAll().Where(x => x.Id.Equals(formulariosolicitud.ATR_VAR_TEXTO_BOTON)).FirstOrDefault();
                //var form_respuesta = _tmk_formulariorespuestaService.GetAll().Where(x => x.Id.Equals(formulariosolicitud.FK_PROGRESSIVE_PROFILING)).FirstOrDefault();
                //var listaAdicionales = _TMK_PlantillasLandingPagePGeneralAdicionalesService.GetAll().Where(w => w.FK_PlantillaId == objeto.FK_PLANTILLA).ToList();

                //var datos_adicionales = _tmk_formulariolandingpageRepository.GetFormularioInsertado(objeto.Id).FirstOrDefault();

                //var listRpta = _tmk_listaplantillasService.GetAll().Where(x => x.Id.Equals(datos_plantilla.fk_plantilla)).FirstOrDefault();

                var plantilla = listRpta.Disenho;
                plantilla_vista = plantilla.ToString();

                string variable_boton = "<div class='col-sm-2 text-left'> <input id='cerrarPublicidad' class='cerrarPublicidad' data-dismiss='modal' aria-label='Close' value='[ Cerrar ]' readonly=''> </div>     <div class='col-sm-10 ff_posicion'> <input id='enviarpublicidad' class='btn btn-warning btn-sm' style='background:#ff7300;color:#ffffff;font-size:16px;font-style: normal'='' value='Texto Boton' readonly=''> </div>";
                plantilla_vista = plantilla_vista.Replace(variable_boton, "_posiscionBoton");
                if (datos_plantilla.FormularioBotonPosicion == "text-center")
                {
                    variable_boton = "<div class='col-sm-12 text-center' > <input id='enviarpublicidad' class='btn btn-warning btn-sm btn-block' ff_boton ='' value ='Texto Boton' readonly='' > </div> <div class='col-sm-12 text-center'> <input id='cerrarPublicidad' class='cerrarPublicidad' data-dismiss='modal' aria -label='Close' value ='[ Cerrar ]' readonly='' > </div>";
                }
                else if (datos_plantilla.FormularioBotonPosicion == "text-left")
                {
                    variable_boton = "<div class='col-sm-10 text-left' > <input id='enviarpublicidad' class='btn btn-warning btn-sm' ff_boton ='' value ='Texto Boton' readonly='' > </div> <div class='col-sm-2 text-right'> <input id='cerrarPublicidad' class='cerrarPublicidad' data-dismiss='modal' aria -label='Close' value ='[ Cerrar ]' readonly='' > </div>";
                }
                else if (datos_plantilla.FormularioBotonPosicion == "text-right")
                {
                    variable_boton = "<div class='col-sm-2 text-left' > <input id='cerrarPublicidad' class='cerrarPublicidad' data-dismiss='modal' aria -label='Close' value ='[ Cerrar ]' readonly='' > </div> <div class='col-sm-10 text-right'> <input id='enviarpublicidad' class='btn btn-warning btn-sm' ff_boton ='' value ='Texto Boton' readonly='' > </div>";
                }
                plantilla_vista = plantilla_vista.Replace("_posiscionBoton", variable_boton);

                plantilla_vista = plantilla_vista.Replace("st_titulo", "style='color:" + datos_plantilla.Cita1Color + ";font-size:" + datos_plantilla.TituloTituloTamanhio + ";" + datos_plantilla.TituloTituloFormato + ";'");
                plantilla_vista = plantilla_vista.Replace("st_texto", "style='color:" + datos_plantilla.Cita3Color + ";font-size:" + datos_plantilla.TituloTextoTamanhio + ";" + datos_plantilla.TituloTextoFormato + "'");
                plantilla_vista = plantilla_vista.Replace("Seccion Titulo - Titulo", formularioLandingPage.TituloPopup);
                plantilla_vista = plantilla_vista.Replace("Seccion Titulo - Texto", formularioLandingPage.Cita3Texto);

                plantilla_vista = plantilla_vista.Replace("f_formulario", "style='background:" + datos_plantilla.ColorPopup + ";'");
                plantilla_vista = plantilla_vista.Replace("ff_titulo", "style='color:" + datos_plantilla.ColorTitulo + ";font-size:" + datos_plantilla.FormularioTituloTamanhio + ";" + datos_plantilla.FormularioTituloFormato + "'");
                plantilla_vista = plantilla_vista.Replace("ff_texto", "style='color:" + datos_plantilla.ColorDescripcion + ";font-size:" + datos_plantilla.FormularioTextoTamanhio + ";" + datos_plantilla.FormularioTextoFormato + "'");
                plantilla_vista = plantilla_vista.Replace("ff_boton", "style='background:" + datos_plantilla.ColorFondoBoton + ";color:" + datos_plantilla.ColorTextoBoton + ";font-size:" + datos_plantilla.FormularioBotonTamanhio + ";" + datos_plantilla.FormularioBotonFormato + "'");

                plantilla_vista = plantilla_vista.Replace("Texto que van dentro del formulario de datos", formularioLandingPage.Cita4Texto);
                plantilla_vista = plantilla_vista.Replace("Titulo del formulario", form_respuesta.TextoBotonBrochure);
                plantilla_vista = plantilla_vista.Replace("Texto Boton", boton.TextoBoton);

                plantilla_vista = plantilla_vista.Replace("stexto_titulo", "style='color:" + datos_plantilla.Cita4Color + ";font-size:" + datos_plantilla.TextoTituloTamanhio + ";" + datos_plantilla.TextoTituloFormato + ";'");
                plantilla_vista = plantilla_vista.Replace("stexto_texto", "style='color:" + datos_plantilla.ColorFondoHeader + ";font-size:" + datos_plantilla.TextoTextoTamanhio + ";" + datos_plantilla.TextoTextoFormato + "'");



                string listado_plantilla = string.Empty;
                if (datosAdicionales != null)
                {
                    if (listRpta.Nombre.Contains("PMV"))
                    {
                        foreach (var item in datosAdicionales)
                        {
                            var temp = listaAdicionales.Where(p => p.IdTitulo.Equals(item.IdTitulo)).FirstOrDefault();
                            listado_plantilla += "<hr/>";
                            listado_plantilla += "<div class='row'>";
                            listado_plantilla += "<div class='col-sm-11'>";
                            listado_plantilla += "<div class='titulos-datos'><b style='color:" + temp.ColorTitulo + "'>" + item.NombreTitulo + "</b></div>";
                            listado_plantilla += "<p style='color:" + temp.ColorDescripcion + "'>" + item.Descripcion + "</p>";
                            listado_plantilla += "</div>";
                            listado_plantilla += "</div>";
                        }

                    }
                    else
                    {
                        foreach (var item in datosAdicionales)
                        {
                            var temp = listaAdicionales.Where(p => p.IdTitulo.Equals(item.IdTitulo)).FirstOrDefault();
                            listado_plantilla += "<div class='row'>";
                            listado_plantilla += "<div class='col-sm-1 text-left'>";
                            listado_plantilla += "<picture><img src='https://bsginstitute.com/repositorioweb/img/iconos/check.png'></picture>";
                            listado_plantilla += "</div>";
                            listado_plantilla += "<div class='col-sm-11'>";
                            listado_plantilla += "<div class='titulos-datos'><b style='color:" + temp.ColorTitulo + "'>" + item.NombreTitulo + "</b></div>";
                            listado_plantilla += "<p style='color:" + temp.ColorDescripcion + "'>" + item.Descripcion + "</p>";
                            listado_plantilla += "</div>";
                            listado_plantilla += "</div><br>";
                        }
                    }

                }

                plantilla_vista = plantilla_vista.Replace("Lista_Datos_Programa", listado_plantilla);
                plantilla_vista = plantilla_vista.Replace("tipo-1.png", "img_imagen").Replace("tipo-2.png", "img_imagen").Replace("tipo-3.png", "img_imagen");
                plantilla_vista = plantilla_vista.Replace("img_imagen", datos_plantilla.UrlImagenPrincipal);

                return plantilla_vista;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
