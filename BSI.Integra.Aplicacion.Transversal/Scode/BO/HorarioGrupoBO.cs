using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.Scode.BO
{
    public class HorarioGrupoBO :BaseBO
    {
        public string Nombre { get; set; }
        public List<HorarioGrupoPersonalBO> IdPersonal { get; set; }
        
    }
}
