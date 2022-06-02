using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{ 
    public class CampaniasMailingPrecioDTO
    {
        public int? IdCampaniaMailingDetalle { get; set; }
        public int? IdPGeneral { get; set; }
        public string EtiquetaPrecio { get; set; }
        public int? Orden { get; set; }
        public string Inversion { get; set; }
        public int CodigoPais { get; set; }
        public int Version { get; set; }
    }

    public class CampaniasGeneralPrecioDTO
    {
        public int? IdCampaniaGeneralDetalle { get; set; }
        public int? IdPGeneral { get; set; }
        public string EtiquetaPrecio { get; set; }
        public int? Orden { get; set; }
        public string Inversion { get; set; }
        public int CodigoPais { get; set; }
        public int Version { get; set; }
    }
}
