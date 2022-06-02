using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProveedorCuentasDTO
    {
        public List<ProveedorCuentaBancoDTO> listaCuentaBanco { get; set; }
        public ProveedorDTO proveedor { get; set; }
    }
}
