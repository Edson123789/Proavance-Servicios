using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class TipoAmbienteBO : BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public byte[] RowVersion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
