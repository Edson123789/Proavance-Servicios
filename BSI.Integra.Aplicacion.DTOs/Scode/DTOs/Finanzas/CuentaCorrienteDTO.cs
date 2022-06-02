using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CuentaCorrienteDTO
    {
        public int IdCta { get; set; }
        public string NumeroCuenta { get; set; }
        public int IdCiudad { get; set; }
        public string Ciudad { get; set; }
        public string NombreEntidadFinanciera { get; set; }
    }
}