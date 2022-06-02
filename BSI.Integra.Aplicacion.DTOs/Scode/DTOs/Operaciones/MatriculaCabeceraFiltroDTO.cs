using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MatriculaCabeceraFiltroDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public DateTime FechaCompromiso { get; set; }
        public string Usuario { get; set; }
        public decimal MontoCompromiso { get; set; }
        public int Version { get; set; }
        public int? IdMoneda { get; set; }
        public bool Flag { get; set; }
    }
}
