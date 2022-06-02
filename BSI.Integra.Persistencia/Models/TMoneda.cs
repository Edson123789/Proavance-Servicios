using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMoneda
    {
        public TMoneda()
        {
            TAnuncioFacebookMetrica = new HashSet<TAnuncioFacebookMetrica>();
            TComisionMontoPago = new HashSet<TComisionMontoPago>();
            TPersonalRecurso = new HashSet<TPersonalRecurso>();
            TTipoCambioMoneda = new HashSet<TTipoCambioMoneda>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public string NombrePlural { get; set; }
        public string Simbolo { get; set; }
        public string Codigo { get; set; }
        public int IdPais { get; set; }
        public int DigitoFinanzas { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
        public bool? ValidaProcesoSeleccion { get; set; }
        public bool? VisualizarTableroComercial { get; set; }
        public bool? VisualizarFinanzas { get; set; }
        public decimal? PorcentajeMora { get; set; }

        public virtual ICollection<TAnuncioFacebookMetrica> TAnuncioFacebookMetrica { get; set; }
        public virtual ICollection<TComisionMontoPago> TComisionMontoPago { get; set; }
        public virtual ICollection<TPersonalRecurso> TPersonalRecurso { get; set; }
        public virtual ICollection<TTipoCambioMoneda> TTipoCambioMoneda { get; set; }
    }
}
