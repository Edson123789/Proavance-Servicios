using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TUsuarioZopim
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public int? HoraDescarga { get; set; }
        public int? MinutoDescarga { get; set; }
        public int IdPersonal { get; set; }
        public bool? DescargaHistorial { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
