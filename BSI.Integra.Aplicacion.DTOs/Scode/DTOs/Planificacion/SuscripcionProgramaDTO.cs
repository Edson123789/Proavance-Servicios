using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{ 
    public class SuscripcionProgramaDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public int? OrdenBeneficio { get; set; }

    }
}
