using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPostulanteEquipoComputo
    {
        public int Id { get; set; }
        public int IdPostulante { get; set; }
        public string TipoEquipo { get; set; }
        public string MemoriaRam { get; set; }
        public string SistemaOperativo { get; set; }
        public string Procesador { get; set; }
        public bool Mouse { get; set; }
        public bool Auricular { get; set; }
        public bool Camara { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public bool? EsEquipoTrabajo { get; set; }
    }
}
