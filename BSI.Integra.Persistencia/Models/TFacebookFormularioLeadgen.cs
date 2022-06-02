using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFacebookFormularioLeadgen
    {
        public int Id { get; set; }
        public string IdLeadgenFacebook { get; set; }
        public DateTime? FechaCreacionFacebook { get; set; }
        public string IdCampanhaFacebook { get; set; }
        public string NombreCampaniaFacebook { get; set; }
        public string Email { get; set; }
        public string NombreCompleto { get; set; }
        public string AreaFormacion { get; set; }
        public string Cargo { get; set; }
        public string AreaTrabajo { get; set; }
        public string Ciudad { get; set; }
        public string Telefono { get; set; }
        public bool? EsProcesado { get; set; }
        public string Industria { get; set; }
        public string InicioCapacitacion { get; set; }
        public string Excepcion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public string FacebookAnuncioId { get; set; }
        public string FacebookAnuncioNombre { get; set; }
    }
}
