using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AsesorNombreFiltroDTO
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public bool? Activo { get; set; }
        public bool? Estado { get; set; }
        public int? IdJefe { get; set; }

    }
    public class PersonalEnvioMarketingDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }


    }
}
