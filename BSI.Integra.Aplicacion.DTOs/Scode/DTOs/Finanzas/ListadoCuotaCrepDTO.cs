using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ListadoCuotaCrepDTO
    {
        public int Id { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public string FechaVencimiento { get; set; }
        public string Moneda { get; set; }
        public decimal Cuota { get; set; }
        public decimal Mora { get; set; }
        public decimal Total { get; set; }
        public bool Enviado { get; set; }
        public string FechaAnterior { get; set; }
        public string Adicional { get; set; }
    }
}
