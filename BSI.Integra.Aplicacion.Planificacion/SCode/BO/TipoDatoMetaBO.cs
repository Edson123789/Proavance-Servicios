using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class TipoDatoMetaBO : BaseBO
    {
        public int IdFaseOportunidadOrigen { get; set; }
        public int IdFaseOportunidadDestino { get; set; }
        public int IdTipoDato { get; set; }
        public int Meta { get; set; }
        public int MetaGlobal { get; set; }
        public Guid? IdMigracion { get; set; }

    }
}
