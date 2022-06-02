using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class SubAreaParametroSeoBO : BaseBO
    {
        public string Descripcion { get; set; }
        public int IdSubAreaCapacitacion { get; set; }
        public int IdParametroSeoPw { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
