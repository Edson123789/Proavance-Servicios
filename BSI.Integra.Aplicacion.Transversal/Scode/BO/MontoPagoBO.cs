using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Transversal.BO
{
    public class MontoPagoBO:BaseBO
    {
        public decimal Precio { get; set; }
        public string PrecioLetras { get; set; }
        public int IdMoneda { get; set; }
        public decimal? Matricula { get; set; }
        public decimal? Cuotas { get; set; }
        public int? NroCuotas { get; set; }
        public int? IdTipoDescuento { get; set; }
        public int? IdPrograma { get; set; }
        public int? IdTipoPago { get; set; }
        public int? IdPais { get; set; }
        public string Vencimiento { get; set; }
        public string PrimeraCuota { get; set; }
        public bool? CuotaDoble { get; set; }
        public string Descripcion { get; set; }
        public bool? VisibleWeb { get; set; }
        public int? Paquete { get; set; }
        public bool? PorDefecto { get; set; }
        public decimal? MontoDescontado { get; set; }
        public Guid? IdMigracion { get; set; }
        public List<MontoPagoPlataformaBO> MontoPagoPlataforma { get; set; }
        public List<MontoPagoSuscripcionBO> MontoPagoSuscripcion { get; set; }
        
    }
}
