using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.Marketing
{
    public class InsertarPlantillaFormularioDTO
    {
        public int Id { get; set; }
        public int? IdFormularioRespuesta { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string NombreCampania { get; set; }
        public int? IdCampania { get; set; }
        public string Proveedor { get; set; }
        public int IdFormularioSolicitudTextoBoton { get; set; }
        public int TipoSegmento { get; set; }
        public string CodigoSegmento { get; set; }
        public int TipoEvento { get; set; }
        public string UrlbotonInvitacionPagina { get; set; }
        public string Usuario { get; set; }        
        //2       
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
        public List<datosInsertarCamposDTO> Campo { get; set; }

        public string Formulario { get; set; }
        public DateTime FechaCreacion { get; set; }


    }
}
