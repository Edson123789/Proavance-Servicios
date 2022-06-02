using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class AsignacionAutomaticaOrigenBO : BaseEntity
    {
        public string Nombre { get; set; }
        public byte[] RowVersion { get; set; }

        public const int Scoring = 1;
        public const int PortalWeb = 2;
        public const int CargaMasiva = 3;

        public AsignacionAutomaticaOrigenBO() {
        }
    }
}
