using BSI.Integra.Aplicacion.Base.BO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class DocumentoConfiguracionRecepcionBO : BaseBO
    {
        public int IdTipoPersona { get; set; }
        public int IdPais { get; set; }
        public int IdDocumento { get; set; }
        public int IdModalidadCurso { get; set; }
        public int? Padre { get; set; }
        public bool EsActivo { get; set; }
        public int? IdMigracion { get; set; }
    }
}
