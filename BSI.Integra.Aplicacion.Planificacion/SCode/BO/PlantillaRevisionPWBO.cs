using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class PlantillaRevisionPwBO : BaseBO
    {
        public int IdPlantillaPw { get; set; }
        public int IdRevisionNivelPw { get; set; }
        public int IdPersonal { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
