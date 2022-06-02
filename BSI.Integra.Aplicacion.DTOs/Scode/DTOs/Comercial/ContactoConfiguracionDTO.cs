using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ContactoConfiguracionDTO
    {
        public int IdTipoDato { get; set; }
        public string DescripcionTipoDato { get; set; }
        public int IdOrigen { get; set; }
        public string NombreOrigen { get; set; }
        public int IdFaseOportunidad { get; set; }
        public string CodigoFaseOportunidad { get; set; }
    }
}
