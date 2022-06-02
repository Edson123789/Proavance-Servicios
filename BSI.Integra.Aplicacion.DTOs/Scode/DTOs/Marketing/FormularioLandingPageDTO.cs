using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class FormularioLandingPageDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int IdFormularioSolicitud { get; set; }
        public int IdPlantillaLandingPage { get; set; }
        public int? IdPespecifico { get; set; }
        public string TextoPopup { get; set; }
        public string TituloPopup { get; set; }
        public string Cita3Texto { get; set; }
        public string Cita4Texto { get; set; }
        public bool? TesteoAb { get; set; }
        public string NombreFormularioSolicitud { get; set; }
        public string NombrePlantillaLandingPage { get; set; }
        public bool? TituloProgramaAutomatico { get; set; }
        public bool? DescripcionWebAutomatico { get; set; }
    }
}
