using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AccionFormularioPorCampoContactoDTO
    {
        public int Id { get; set; }
        public int IdCampoContacto { get; set; }
        public int Orden { get; set; }
        public string Campo { get; set; }
        public bool EsSiempreVisible { get; set; }
        public bool EsInteligente { get; set; }
        public bool TieneProbabilidad { get; set; }
    }
}
