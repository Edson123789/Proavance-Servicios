using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Models
{
    public partial class TInformacionAlumnoChat
    {
        public int Id { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdProgramaGeneralPadre { get; set; }
        public int? IdProgramaGeneralHijo { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdCapitulo { get; set; }
        public int? IdSesion { get; set; }
        public string IdInteraccionSesion { get; set; }
        public int? IdCoordinadora { get; set; }
        public string CodigoMatricula { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
