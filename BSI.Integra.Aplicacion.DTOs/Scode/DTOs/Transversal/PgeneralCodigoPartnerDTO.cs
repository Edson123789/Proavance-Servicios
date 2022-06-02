using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PgeneralCodigoPartnerDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string Codigo { get; set; }
        public string Usuario { get; set; }
        public List<PgeneralCodigoPartnerVersionProgramaDTO> IdVersionPrograma { get; set; }
        public List<PgeneralCodigoPartnerModalidadCursoDTO> IdModalidadCurso { get; set; }
    }
}
