using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFormularioLandingPage
    {
        public TFormularioLandingPage()
        {
            TDatoAdicionalPagina = new HashSet<TDatoAdicionalPagina>();
            TFormularioPlantilla = new HashSet<TFormularioPlantilla>();
        }

        public int Id { get; set; }
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
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public bool? TituloProgramaAutomatico { get; set; }
        public bool? DescripcionWebAutomatico { get; set; }

        public virtual ICollection<TDatoAdicionalPagina> TDatoAdicionalPagina { get; set; }
        public virtual ICollection<TFormularioPlantilla> TFormularioPlantilla { get; set; }
    }
}
