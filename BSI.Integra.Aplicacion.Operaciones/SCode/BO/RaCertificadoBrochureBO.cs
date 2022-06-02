using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public partial class RaCertificadoBrochureBO : BaseBO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreEnCertificado { get; set; }
        public string Contenido { get; set; }
        public int TotalHoras { get; set; }
        public int? IdRaCertificadoBrochure { get; set; }
        public bool Activo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }

    }
}
