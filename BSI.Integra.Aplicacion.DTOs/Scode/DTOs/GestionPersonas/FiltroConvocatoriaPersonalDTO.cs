using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Scode.DTOs.GestionPersonas
{
    public class FiltroConvocatoriaPersonalDTO
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public int IdTipoServicio { get; set; }
        public bool EstadoPTS { get; set; }
        public bool EstadoP { get; set; }
        public string RazonSocial { get; set; }
    }
}
