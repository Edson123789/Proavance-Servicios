using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TPreguntaFrecuente
    {
        public TPreguntaFrecuente()
        {
            TPreguntaFrecuenteArea = new HashSet<TPreguntaFrecuenteArea>();
            TPreguntaFrecuentePgeneral = new HashSet<TPreguntaFrecuentePgeneral>();
            TPreguntaFrecuenteSubArea = new HashSet<TPreguntaFrecuenteSubArea>();
            TPreguntaFrecuenteTipo = new HashSet<TPreguntaFrecuenteTipo>();
        }

        public int Id { get; set; }
        public int IdSeccionPreguntaFrecuente { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public int Tipo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }

        public virtual ICollection<TPreguntaFrecuenteArea> TPreguntaFrecuenteArea { get; set; }
        public virtual ICollection<TPreguntaFrecuentePgeneral> TPreguntaFrecuentePgeneral { get; set; }
        public virtual ICollection<TPreguntaFrecuenteSubArea> TPreguntaFrecuenteSubArea { get; set; }
        public virtual ICollection<TPreguntaFrecuenteTipo> TPreguntaFrecuenteTipo { get; set; }
    }
}
