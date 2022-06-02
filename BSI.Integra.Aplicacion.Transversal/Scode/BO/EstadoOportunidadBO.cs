using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public  class EstadoOportunidadBO : BaseEntity
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public byte[] RowVersion { get; set; }

        public static int estadoNoProgramada = 2;
        public static int estadoReasignada = 3;
        public static int estadoProgramada = 5;
        public static int estadoEjecutada = 1;
        public static int estadoReasignadaVentaCruzada = 4;

    }
}
