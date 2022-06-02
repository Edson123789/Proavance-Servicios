using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.BO
{
    public class AccionFormularioBO : BaseBO
    {
        public string Nombre { get; set; }
        public int UltimaLlamadaEjecutada { get; set; }
        public bool CamposSinValores { get; set; }
        public int TiempoRedirecionamiento { get; set; }
        public bool CamposSegunProbabilidad { get; set; }
        public bool TodosCampos { get; set; }
        public int? NumeroClics { get; set; }
        public Guid IdMigracion { get; set; }
        public List<AccionFormularioPorCategoriaOrigenBO> ListaCategoriaOrigen { get; set; }
        public List<AccionFormularioPorCampoContactoBO> ListaCampoContacto { get; set; }

        public AccionFormularioBO()
        {
            ActualesErrores = new Dictionary<string, List<Base.Classes.ErrorInfo>>();
        }
    }
}
