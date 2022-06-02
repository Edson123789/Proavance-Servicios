using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CambioFaseConySinLlamadaDTO
    {
        public int? Numero { get; set; }
        public int? IdOporunidad { get; set; }
        public string FaseOrigen { get; set; }
        public string FaseDestino { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdCategoriaOrigen { get; set; }
        public int? IdTipoDato { get; set; }
        public bool? Estado { get; set; }
        public DateTime? FechaLog { get; set; }
        public int? IdFaseOrigen { get; set; }
        public int? IdFaseDestino { get; set; }
        public string Cambio { get; set; }
        public string EstadoLlamada { get; set; }
    }
}
