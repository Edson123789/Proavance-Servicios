using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ListadoCuotasModificadasDTO
    {
        public string CodigoEspecial { get; set; }
        public int NroCuota { get; set; }
        public int NroSubCuota { get; set; }
        public decimal Cuota { get; set; }
        public string FechaVencimiento { get; set; }
        public string FechaAnterior { get; set; }
        public bool Enviado { get; set; }
    }
}
