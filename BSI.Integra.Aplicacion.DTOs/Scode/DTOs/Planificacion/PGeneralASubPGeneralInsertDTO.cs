using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PGeneralASubPGeneralInsertDTO
    {
        public int Id { get; set; }
        public int PGeneralPadre { get; set; }
        public int PGeneralHijo { get; set; }
        public int? Orden { get; set; }
        public string Usuario { get; set; }
        public List<PgeneralASubPgeneralVersionProgramaDTO> listaVersion { get; set; }
    }
}
