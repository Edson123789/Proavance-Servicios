using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PlantillaLandingPageDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cita1Texto { get; set; }
        public string Cita1Color { get; set; }
        public string Cita2Texto { get; set; }
        public string Cita2Color { get; set; }
        public string Cita3Texto { get; set; }
        public string Cita3Color { get; set; }
        public string UrlImagenPrincipal { get; set; }
        public bool? PorDefecto { get; set; }
        public string Cita4Texto { get; set; }
        public string Cita4Color { get; set; }
        public string ColorPopup { get; set; }
        public string ColorTitulo { get; set; }
        public string ColorTextoBoton { get; set; }
        public string ColorFondoBoton { get; set; }
        public string ColorDescripcion { get; set; }
        public string ColorFondoHeader { get; set; }
        public string Cita1Despues { get; set; }
        public bool? MuestraPrograma { get; set; }
        public string ColorPlaceHolder { get; set; }
        public bool? Plantilla2 { get; set; }
        public int? TipoPlantilla { get; set; }
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
        public string Usuario { get; set; }
    }
}
