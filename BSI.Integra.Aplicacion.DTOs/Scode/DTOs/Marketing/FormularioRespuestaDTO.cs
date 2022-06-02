using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public partial class FormularioRespuestaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int IdPgeneral { get; set; }
        public string ProgramaGeneral { get; set; }
        public string ResumenProgramaGeneral { get; set; }
        public string ColorTextoPgeneral { get; set; }
        public string ColorTextoDescripcionPgeneral { get; set; }
        public string ColorTextoInvitacionBrochure { get; set; }
        public string TextoBotonBrochure { get; set; }
        public string ColorFondoBotonBrochure { get; set; }
        public string ColorTextoBotonBrochure { get; set; }
        public string ColorBordeInferiorBotonBrochure { get; set; }
        public string ColorTextoBotonInvitacion { get; set; }
        public string ColorFondoBotonInvitacion { get; set; }
        public string FondoBotonLadoInvitacion { get; set; }
        public string UrlImagenLadoInvitacion { get; set; }
        public string TextoBotonInvitacionPagina { get; set; }
        public string UrlBotonInvitacionPagina { get; set; }
        public string TextoBotonInvitacionArea { get; set; }
        public string UrlBotonInvitacionArea { get; set; }
        public string ContenidoSeccionTelefonos { get; set; }
        public int? IdFormularioRespuestaPlantilla { get; set; }
        public string Urlbrochure { get; set; }
        public string Urllogotipo { get; set; }
        public string TextoInvitacionBrochure { get; set; }
		public int Total { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
