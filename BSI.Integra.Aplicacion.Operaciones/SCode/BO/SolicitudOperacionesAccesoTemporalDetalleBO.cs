using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Operaciones.BO
{
    public class SolicitudOperacionesAccesoTemporalDetalleBO : BaseBO
    {
        public int IdSolicitudOperaciones { get;set;}
        public int IdPEspecifico { get;set;}
        public int? IdMigracion { get; set; }
    }
}
