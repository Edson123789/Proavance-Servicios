using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TFlujoOcurrencia
    {
        public int Id { get; set; }
        public int IdFlujoActividad { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; }
        public bool CerrarSeguimiento { get; set; }
        public int? IdFaseDestino { get; set; }
        public int? IdFlujoActividadSiguiente { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
