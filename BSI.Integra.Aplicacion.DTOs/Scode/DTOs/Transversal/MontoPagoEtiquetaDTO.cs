using System;
using System.Collections.Generic;
using System.Text;


namespace BSI.Integra.Aplicacion.DTOs
{
    public class MontoPagoEtiquetaDTO
    {
        public int? Paquete { get; set; }
        public string tp_nombre { get; set; }
        public int tp_cuotas { get; set; }
        public double mp_precio { get; set; }
        public string Simbolo { get; set; }
        public double mp_matricula { get; set; }
        public int mp_nro_cuotas { get; set; }
        public double mp_cuotas { get; set; }
        public string Titulo { get; set; }
        public int? OrdenBeneficio { get; set; }
    }
}
