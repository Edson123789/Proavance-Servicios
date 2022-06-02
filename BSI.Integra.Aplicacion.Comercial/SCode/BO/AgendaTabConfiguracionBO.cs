using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Base.Classes;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class AgendaTabConfiguracionBO : BaseBO
    {
        public int IdAgendaTab { get; set; }
        public string IdTipoCategoriaOrigen { get; set; }
        public string IdCategoriaOrigen { get; set; }
        public string IdTipoDato { get; set; }
        public string IdFaseOportunidad { get; set; }
        public string IdEstadoOportunidad { get; set; }
        public string Probabilidad { get; set; }
        public string VistaBaseDatos { get; set; }
        public string CamposVista { get; set; }
    }
}