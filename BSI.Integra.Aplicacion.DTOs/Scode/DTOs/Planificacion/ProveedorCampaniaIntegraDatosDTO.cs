using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProveedorCampaniaIntegraDatosDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool PorDefecto { get; set; }
        public string Usuario { get; set; }
    }
}
