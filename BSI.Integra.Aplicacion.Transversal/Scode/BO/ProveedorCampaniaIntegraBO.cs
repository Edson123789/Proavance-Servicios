using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class ProveedorCampaniaIntegraBO : BaseBO
    
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool PorDefecto { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
