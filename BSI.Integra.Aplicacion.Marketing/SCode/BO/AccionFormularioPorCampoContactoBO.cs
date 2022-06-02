using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class AccionFormularioPorCampoContactoBO : BaseBO
    {
        public int? IdAccionFormulario { get; set; }
        public int IdCampoContacto { get; set; }
        public int Orden { get; set; }
        public string Campo { get; set; }
        public bool EsSiempreVisible { get; set; }
        public bool EsInteligente { get; set; }
        public bool TieneProbabilidad { get; set; }
        public Guid IdMigracion { get; set; }

        public AccionFormularioPorCampoContactoBO()
        {
            ActualesErrores = new Dictionary<string, List<Base.Classes.ErrorInfo>>();
        }
    }
}
