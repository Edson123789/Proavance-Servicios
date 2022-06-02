using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public partial class FormularioSolicitudDTO
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

    }
}
