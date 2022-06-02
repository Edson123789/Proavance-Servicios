using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompuestoRevisionDTO
    {
        public RevisionPwDTO ObjetoRevision { get; set; }
        public List<RevisionNivelPwFiltroDTO> ListaRevision { get; set; }
        public string Usuario { get; set; }
    }
}
