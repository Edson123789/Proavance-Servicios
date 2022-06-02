using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PgeneralAsubPgeneralDTO
    {
        public int IdTroncalGeneral { get; set; }
        public int? Orden { get; set; }
        public string Nombre { get; set; }
        public int IdCurso { get; set; }
        public List<PgeneralASubPgeneralVersionProgramaDTO> listaVersion { get; set; }
    }
}
