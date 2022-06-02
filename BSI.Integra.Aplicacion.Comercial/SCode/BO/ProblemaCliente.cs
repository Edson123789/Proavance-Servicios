using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Classes;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class ProblemaClienteBO : BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
