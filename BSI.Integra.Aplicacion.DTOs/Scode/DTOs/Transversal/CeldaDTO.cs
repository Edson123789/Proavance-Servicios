using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CeldaDTO
    {
        public int Fila { get; set; }
        public int Columna { get; set; }
    }

    public class CampoObligatorioCeldaDTO
    {
        public string Campo { get; set; }
        public int Columna { get; set; }
        public bool FlagObligatorio { get; set; }
    }

    public class CampoObligatorioDTO
    {
        public string Campo { get; set; }
        public bool FlagObligatorio { get; set; }
    }
}
