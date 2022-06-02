using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ResumenCajaDTO
    {
        public int IdCaja  { get; set; }
        public string CodigoCaja { get; set; }
        public int IdEmpresaAutorizada { get; set; }
        public string Direccion { get; set; }
        public string Central { get; set; }
        public string Ruc { get; set; }
        public int IdEntidadFinanciera { get; set; }
        public int IdCuentaCorriente { get; set; }
        public int IdMoneda { get; set; }
        public int IdCiudad { get; set; }
        public int IdPais { get; set; }
        public string PersonalResponsable { get; set; }
    }
}
