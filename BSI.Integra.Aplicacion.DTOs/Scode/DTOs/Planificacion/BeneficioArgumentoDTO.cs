using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class BeneficioArgumentoDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
    }
    public class MotivacionArgumentoDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
    }
    public class ProblemaDetalleSolucionDTO
    {
        public int? Id { get; set; }
        public string Detalle { get; set; }
        public string Solucion { get; set; }
    }
    public class CertificacionArgumentoDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
    }
    public class ProblemaArgumentoDTO
    {
        public int? Id { get; set; }
        public string Detalle { get; set; }
        public string Solucion { get; set; }
    }
}
