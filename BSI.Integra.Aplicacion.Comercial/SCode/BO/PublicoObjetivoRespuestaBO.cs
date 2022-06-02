using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Comercial.BO
{
    public class PublicoObjetivoRespuestaBO: BaseBO
    {
        public int IdOportunidad { get; set; }
        public int IdDocumentoSeccionPw { get; set; }
        public int NivelCumplimiento { get; set; }
        public int? IdMigracion { get; set; }
    }
}
