using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TMaterialAdicionalAulaVirtualRegistro
    {
        public int Id { get; set; }
        public int IdMaterialAdicionalAulaVirtual { get; set; }
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public bool EsEnlace { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public bool? SoloLectura { get; set; }

        public virtual TMaterialAdicionalAulaVirtual IdMaterialAdicionalAulaVirtualNavigation { get; set; }
    }
}
