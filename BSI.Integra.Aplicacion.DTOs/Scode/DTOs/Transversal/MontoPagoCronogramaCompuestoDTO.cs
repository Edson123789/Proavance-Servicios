using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class MontoPagoCronogramaCompuestoDTO
    {
        public int Id { get; set; }
        public double mp_precio { get; set; }
        public double mp_precioDescuento { get; set; }
        public string mp_precio_letras { get; set; }
        public string mp_moneda { get; set; }
        public Nullable<double> mp_matricula { get; set; }
        public Nullable<double> mp_cuotas { get; set; }
        public int mp_nro_cuotas { get; set; }
        public int id_programa { get; set; }
        public int id_tp { get; set; }
        public int id_pais { get; set; }
        public int id_tipo_descuento { get; set; }
        public int id_cronograma { get; set; }
        public bool is_aprobado { get; set; }
        public string mp_vencimiento { get; set; }
        public string mp_primeraCuota { get; set; }
        public bool mp_cuotaDoble { get; set; }
        public int tp_formula { get; set; }
        public int tp_porcentaje_general { get; set; }
        public int tp_porcentaje_matricula { get; set; }
        public int tp_fracciones_matricula { get; set; }
        public int tp_porcentaje_cuotas { get; set; }
        public int tp_cuotas_adicionales { get; set; }
        public string NombrePlural { get; set; }
        public int matriculaEnProceso { get; set; }
        public string Simbolo { get; set; }
        public string CodigoMatricula { get; set; }
    }
}
