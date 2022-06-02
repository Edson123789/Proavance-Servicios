using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PagoBancoDTO
    {
        public string _codigousuario { get; set; }
        public string _codigoespecial { get; set; }
        public string _fechavencimiento { get; set; }
        public string _fechapago { get; set; }
        public string _montopago { get; set; }
        public string _montomora { get; set; }
        public string _montototal { get; set; }
        public string _observaciones { get; set; }
        public string _moneda { get; set; }
        public string _cuenta { get; set; }        
    }
}
