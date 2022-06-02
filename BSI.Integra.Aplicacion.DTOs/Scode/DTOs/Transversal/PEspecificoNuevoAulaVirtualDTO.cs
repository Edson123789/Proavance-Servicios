using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PEspecificoNuevoAulaVirtualDTO
    {
        public int IdPEspecifico { get; set; }
        public string NombrePEspecifico { get; set; }
        public int? IdCentroCosto { get; set; }
        public string EstadoP { get; set; }
        public string Modalidad { get; set; }
        public int? IdPGeneral { get; set; }
        public string Ciudad { get; set; }
        public int? IdCursoMoodle { get; set; }
        public int? IdCursoMoodlePrueba { get; set; }
        public bool Estado { get; set; }
        public int TipoPEspecifico { get; set; }
        public int? IdPEspecificoPadre { get; set; }
        public int IdPEspecificoHijo { get; set; }
        public string NombrePEspecificoHijo { get; set; }
    }

    public class PEspecificoPadreNuevoAulaVirtualDTO
    {
        public int IdPEspecificoPadre { get; set; }
        public List<PEspecificoNuevoAulaVirtualDTO> ListaPEspecificosHijos { get; set; }
    }
}