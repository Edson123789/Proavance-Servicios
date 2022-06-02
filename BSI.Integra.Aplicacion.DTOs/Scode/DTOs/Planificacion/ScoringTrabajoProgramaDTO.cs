using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ScoringTrabajoProgramaDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public string Nombre { get; set; }
        public int IdAreaTrabajo { get; set; }
        public int IdSelect { get; set; }
        public int Valor { get; set; }
        public int Fila { get; set; }
        public int Columna { get; set; }
        public bool Validar { get; set; }
    }
}
