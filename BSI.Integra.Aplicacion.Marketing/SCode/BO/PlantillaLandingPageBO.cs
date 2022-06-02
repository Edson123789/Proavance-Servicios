using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Aplicacion.Marketing.Repositorio;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class PlantillaLandingPageBO: BaseBO
    {
        public string Nombre { get; set; }
        public string Cita1Texto { get; set; }
        public string Cita1Color { get; set; }
        public string Cita2Texto { get; set; }
        public string Cita2Color { get; set; }
        public string Cita3Texto { get; set; }
        public string Cita3Color { get; set; }
        public string UrlImagenPrincipal { get; set; }
        public bool PorDefecto { get; set; }
        public string Cita4Texto { get; set; }
        public string Cita4Color { get; set; }
        public string ColorPopup { get; set; }
        public string ColorTitulo { get; set; }
        public string ColorTextoBoton { get; set; }
        public string ColorFondoBoton { get; set; }
        public string ColorDescripcion { get; set; }
        public string ColorFondoHeader { get; set; }
        public string Cita1Despues { get; set; }
        public bool MuestraPrograma { get; set; }
        public string ColorPlaceHolder { get; set; }
        public bool Plantilla2 { get; set; }
        public int TipoPlantilla { get; set; }
        public int? IdListaPlantilla { get; set; }
        public string FormularioTituloTamanhio { get; set; }
        public string FormularioTituloFormato { get; set; }
        public string FormularioBotonTamanhio { get; set; }
        public string FormularioBotonFormato { get; set; }
        public string FormularioTextoTamanhio { get; set; }
        public string FormularioTextoFormato { get; set; }
        public string TituloTituloTamanhio { get; set; }
        public string TituloTituloFormato { get; set; }
        public string TituloTextoTamanhio { get; set; }
        public string TituloTextoFormato { get; set; }
        public string TextoTituloTamanhio { get; set; }
        public string TextoTituloFormato { get; set; }
        public string TextoTextoTamanhio { get; set; }
        public string TextoTextoFormato { get; set; }
        public string FormularioBotonPosicion { get; set; }
        public Guid? IdMigracion { get; set; }


        //hijos
        public List<PlantillaLandingPagePgeneralAdicionalBO> PGeneralAdicional { get; set; }

        private PlantillaLandingPageRepositorio _repPlantillaLandingPage;
        public PlantillaLandingPageBO()
        {

        }

        public PlantillaLandingPageBO(int Id)
        {
            ActualesErrores = new Dictionary<string, List<ErrorInfo>>();
            _repPlantillaLandingPage = new PlantillaLandingPageRepositorio();

            var plantilla = _repPlantillaLandingPage.FirstById(Id);

            if (plantilla != null)
            {
                this.Id = plantilla.Id;
                this.Cita1Texto = plantilla.Cita1Texto;
                this.Cita1Color = plantilla.Cita1Color;
                this.Cita2Texto = plantilla.Cita2Texto;
                this.Cita2Color = plantilla.Cita2Color;
                this.Cita3Texto = plantilla.Cita3Texto;
                this.Cita3Color = plantilla.Cita3Color;
                this.UrlImagenPrincipal = plantilla.UrlImagenPrincipal;
                this.PorDefecto = plantilla.PorDefecto;
                this.Cita4Texto = plantilla.Cita4Texto;
                this.Cita4Color = plantilla.Cita4Color;
                this.ColorPopup = plantilla.ColorPopup;
                this.ColorTitulo = plantilla.ColorTitulo;
                this.ColorTextoBoton = plantilla.ColorTextoBoton;
                this.ColorFondoBoton = plantilla.ColorFondoBoton;
                this.ColorDescripcion = plantilla.ColorDescripcion;
                this.ColorFondoHeader = plantilla.ColorFondoHeader;
                this.Cita1Despues = plantilla.Cita1Despues;
                this.MuestraPrograma = plantilla.MuestraPrograma;
                this.ColorPlaceHolder = plantilla.ColorPlaceHolder;
                this.Plantilla2 = plantilla.Plantilla2;
                this.TipoPlantilla = plantilla.TipoPlantilla;
                this.IdListaPlantilla = plantilla.IdListaPlantilla;
                this.FormularioTituloTamanhio = plantilla.FormularioTituloTamanhio;
                this.FormularioTituloFormato = plantilla.FormularioTituloFormato;
                this.FormularioBotonTamanhio = plantilla.FormularioBotonTamanhio;
                this.FormularioBotonFormato = plantilla.FormularioBotonFormato;
                this.FormularioTextoTamanhio = plantilla.FormularioTextoTamanhio;
                this.FormularioTextoFormato = plantilla.FormularioTextoFormato;
                this.TituloTituloTamanhio = plantilla.TituloTituloTamanhio;
                this.TituloTituloFormato = plantilla.TituloTituloFormato;
                this.TituloTextoTamanhio = plantilla.TituloTextoTamanhio;
                this.TituloTextoFormato = plantilla.TituloTextoFormato;
                this.TextoTituloTamanhio = plantilla.TextoTituloTamanhio;
                this.TextoTituloFormato = plantilla.TextoTituloFormato;
                this.TextoTextoTamanhio = plantilla.TextoTextoTamanhio;
                this.TextoTextoFormato = plantilla.TextoTextoFormato;
                this.FormularioBotonPosicion = plantilla.FormularioBotonPosicion;
                this.Estado = plantilla.Estado;
                this.FechaCreacion = plantilla.FechaCreacion;
                this.FechaModificacion = plantilla.FechaModificacion;
                this.UsuarioCreacion = plantilla.UsuarioCreacion;
                this.UsuarioModificacion = plantilla.UsuarioModificacion;
                this.RowVersion = plantilla.RowVersion;
            }
        }

        public string ObtenerVistaPreviaPlantilla2(PlantillaLandingPageDTO plantillaLandingPageDTO, List<PlantillaLandingPagePgeneralAdicionalDTO> adicionales)
        {
            try
            {
                ListaPlantillaRepositorio listaPlantillaRepositorio = new ListaPlantillaRepositorio();
                string plantillaVistaActual = string.Empty;

                var listRpta = listaPlantillaRepositorio.FirstBy(x => x.Id == plantillaLandingPageDTO.IdListaPlantilla);

                var plantilla = listRpta.Disenho;
                plantillaVistaActual = plantilla.ToString();

                string variable_boton = "<div class='col-sm-2 text-left'> <input id='cerrarPublicidad' class='cerrarPublicidad' data-dismiss='modal' aria-label='Close' value='[ Cerrar ]' readonly=''> </div>     <div class='col-sm-10 ff_posicion'> <input id='enviarpublicidad' class='btn btn-warning btn-sm' style='background:#ff7300;color:#ffffff;font-size:16px;font-style: normal'='' value='Texto Boton' readonly=''> </div>";
                plantillaVistaActual = plantillaVistaActual.Replace(variable_boton, "_posiscionBoton");
                if (plantillaLandingPageDTO.FormularioBotonPosicion == "text-center")
                {
                    variable_boton = "<div class='col-sm-12 text-center' > <input id='enviarpublicidad' class='btn btn-warning btn-sm btn-block' ff_boton ='' value ='Texto Boton' readonly='' > </div> <div class='col-sm-12 text-center'> <input id='cerrarPublicidad' class='cerrarPublicidad' data-dismiss='modal' aria -label='Close' value ='[ Cerrar ]' readonly='' > </div>";
                }
                else if (plantillaLandingPageDTO.FormularioBotonPosicion == "text-left")
                {
                    variable_boton = "<div class='col-sm-10 text-left' > <input id='enviarpublicidad' class='btn btn-warning btn-sm' ff_boton ='' value ='Texto Boton' readonly='' > </div> <div class='col-sm-2 text-right'> <input id='cerrarPublicidad' class='cerrarPublicidad' data-dismiss='modal' aria -label='Close' value ='[ Cerrar ]' readonly='' > </div>";
                }
                else if (plantillaLandingPageDTO.FormularioBotonPosicion == "text-right")
                {
                    variable_boton = "<div class='col-sm-2 text-left' > <input id='cerrarPublicidad' class='cerrarPublicidad' data-dismiss='modal' aria -label='Close' value ='[ Cerrar ]' readonly='' > </div> <div class='col-sm-10 text-right'> <input id='enviarpublicidad' class='btn btn-warning btn-sm' ff_boton ='' value ='Texto Boton' readonly='' > </div>";
                }
                plantillaVistaActual = plantillaVistaActual.Replace("_posiscionBoton", variable_boton);

                plantillaVistaActual = plantillaVistaActual.Replace("st_titulo", "style='color:" + plantillaLandingPageDTO.Cita1Color + ";font-size:" + plantillaLandingPageDTO.TituloTituloTamanhio + ";" + plantillaLandingPageDTO.TituloTituloFormato + ";margin: 0;padding-top: 25px;'");
                plantillaVistaActual = plantillaVistaActual.Replace("st_texto", "style='color:" + plantillaLandingPageDTO.Cita3Color + ";font-size:" + plantillaLandingPageDTO.TituloTextoTamanhio + ";" + plantillaLandingPageDTO.TituloTextoFormato + ";padding-top: 20px;padding-bottom: 20px;'");

                plantillaVistaActual = plantillaVistaActual.Replace("f_formulario", "style='background:" + plantillaLandingPageDTO.ColorPopup + ";'");
                plantillaVistaActual = plantillaVistaActual.Replace("ff_titulo", "style='color:" + plantillaLandingPageDTO.ColorTitulo + ";font-size:" + plantillaLandingPageDTO.FormularioTituloTamanhio + ";" + plantillaLandingPageDTO.FormularioTituloFormato + "'");
                plantillaVistaActual = plantillaVistaActual.Replace("ff_texto", "style='color:" + plantillaLandingPageDTO.ColorDescripcion + ";font-size:" + plantillaLandingPageDTO.FormularioTextoTamanhio + ";" + plantillaLandingPageDTO.FormularioTextoFormato + "'");
                plantillaVistaActual = plantillaVistaActual.Replace("ff_boton", "style='background:" + plantillaLandingPageDTO.ColorFondoBoton + ";color:" + plantillaLandingPageDTO.ColorTextoBoton + ";font-size:" + plantillaLandingPageDTO.FormularioBotonTamanhio + ";" + plantillaLandingPageDTO.FormularioBotonFormato + "'");

                plantillaVistaActual = plantillaVistaActual.Replace("stexto_titulo", "style='color:" + plantillaLandingPageDTO.Cita4Color + ";font-size:" + plantillaLandingPageDTO.TextoTituloTamanhio + ";" + plantillaLandingPageDTO.TextoTituloFormato + ";'");
                plantillaVistaActual = plantillaVistaActual.Replace("stexto_texto", "style='color:" + plantillaLandingPageDTO.ColorFondoHeader + ";font-size:" + plantillaLandingPageDTO.TextoTextoTamanhio + ";" + plantillaLandingPageDTO.TextoTextoFormato + "'");

                string listado_plantilla = string.Empty;
                if (listRpta.Nombre.Contains("PMV"))
                {
                    foreach (var item in adicionales)
                    {
                        listado_plantilla += "<hr/>";
                        listado_plantilla += "<div class='row'>";
                        listado_plantilla += "<div class='col-sm-11'>";
                        listado_plantilla += "<div class='titulos-datos'><b style='color:" + item.ColorDescripcion + "'>" + item.NombreTitulo + "</b></div>";
                        listado_plantilla += "<p style='color:" + item.ColorTitulo + "'>Descripcion de la seccion del programa general.</p>";
                        listado_plantilla += "</div>";
                        listado_plantilla += "</div>";
                    }
                }
                else
                {
                    foreach (var item in adicionales)
                    {
                        listado_plantilla += "<div class='row'>";
                        listado_plantilla += "<div class='col-sm-1 text-left'>";
                        listado_plantilla += "<picture><img src='https://bsginstitute.com/repositorioweb/img/iconos/check.png'></picture>";
                        listado_plantilla += "</div>";
                        listado_plantilla += "<div class='col-sm-11'>";
                        listado_plantilla += "<div class='titulos-datos'><b style='color:" + item.ColorDescripcion + "'>" + item.NombreTitulo + "</b></div>";
                        listado_plantilla += "<p style='color:" + item.ColorTitulo + "'>Descripcion de la seccion del programa general.</p>";
                        listado_plantilla += "</div>";
                        listado_plantilla += "</div><br>";
                    }
                }

                plantillaVistaActual = plantillaVistaActual.Replace("Lista_Datos_Programa", listado_plantilla);
                plantillaVistaActual = plantillaVistaActual.Replace("tipo-1.png", "img_imagen").Replace("tipo-2.png", "img_imagen").Replace("tipo-3.png", "img_imagen");
                plantillaVistaActual = plantillaVistaActual.Replace("img_imagen", plantillaLandingPageDTO.UrlImagenPrincipal);

                return plantillaVistaActual;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        
    }
}
