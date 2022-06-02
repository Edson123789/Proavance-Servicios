using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class MonedaBO : BaseEntity
    {
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
        public string NombrePlural { get; set; }
        public string Simbolo { get; set; }
        public string Codigo { get; set; }
        public int IdPais { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
