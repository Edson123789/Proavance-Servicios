using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPostulanteConexionInternet
    {
        public int Id { get; set; }
        public int IdPostulante { get; set; }
        public string TipoConexion { get; set; }
        public string MedioConexion { get; set; }
        public string VelocidadInternet { get; set; }
        public string ProveedorInternet { get; set; }
        public decimal CostoInternet { get; set; }
        public string ConexionCompartida { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
