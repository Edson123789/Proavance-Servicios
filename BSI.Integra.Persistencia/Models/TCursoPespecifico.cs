using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TCursoPespecifico
    {
        public int Id { get; set; }
        public int? IdPespecifico { get; set; }
        public string Nombre { get; set; }
        public int Duracion { get; set; }
        public int Orden { get; set; }
        public int? IdExpositor { get; set; }
        public int? NroSesiones { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

        public virtual TPespecifico IdPespecificoNavigation { get; set; }
    }
}
