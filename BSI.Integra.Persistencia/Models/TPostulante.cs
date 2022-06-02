using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPostulante
    {
        public TPostulante()
        {
            TEtapaProcesoSeleccionCalificado = new HashSet<TEtapaProcesoSeleccionCalificado>();
            TExamenAsignadoEvaluador = new HashSet<TExamenAsignadoEvaluador>();
            TPostulanteComparacion = new HashSet<TPostulanteComparacion>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NroDocumento { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Telefono2 { get; set; }
        public string Celular2 { get; set; }
        public string Celular3 { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdTipoDocumento { get; set; }
        public int? IdSexo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public string UrlPerfilFacebook { get; set; }
        public string UrlPerfilLinkedin { get; set; }
        public bool? EsProcesoAnterior { get; set; }
        public int? Edad { get; set; }
        public bool? TieneHijo { get; set; }
        public int? CantidadHijo { get; set; }

        public virtual ICollection<TEtapaProcesoSeleccionCalificado> TEtapaProcesoSeleccionCalificado { get; set; }
        public virtual ICollection<TExamenAsignadoEvaluador> TExamenAsignadoEvaluador { get; set; }
        public virtual ICollection<TPostulanteComparacion> TPostulanteComparacion { get; set; }
    }
}
