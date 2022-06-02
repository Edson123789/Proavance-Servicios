using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PublicidadWebFormularioCampoDTO
    {
        public int Id { get; set; }
        public int IdPublicidadWebFormulario { get; set; }
        public int IdCampoContacto { get; set; }
        public string Nombre { get; set; }
        public bool Siempre { get; set; }
        public bool Inteligente { get; set; }
        public bool Probabilidad { get; set; }
        public int Orden { get; set; }
    }
}
