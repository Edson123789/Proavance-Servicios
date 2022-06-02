using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DocumentoEnviadoWebPwBO : BaseBO
    {
        public int Id { get; set; }
        public int IdAlumno { get; set; }
        public int IdPespecifico { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaEnvio { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
