using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class TestimonioProgramaCompuestoDTO
    {
        public int IdProgramaGeneral { get; set; }
        public List<TestimonioProgramaDTO> ListaTestimoniosAsociados { get; set; }
        public string Usuario { get; set; }
    }
}
