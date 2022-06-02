using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CrepListaCuotasSeleccionadasDTO
    {
        public int Id { get; set; }
        public int nroCuota { get; set; }//ok
        public int nroSubcuota { get; set; }//ok
        public string fechaVencimiento { get; set; }//ok
        public string Moneda { get; set; }//ok
        public decimal Cuota { get; set; }//ok
        public decimal Mora { get; set; }//ok
        public decimal Total { get; set; }//ok
        public bool enviado { get; set; }//ok
        public string fechaAnterior { get; set; }//ok
        public string Adicional { get; set; }//ok
        public int Enviar { get; set; }//ok
        public string CodUsuario { get; set; }
        public string CodigoEspecial { get; set; }
    }
}
