using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AlumnoFiltroAutocompleteDTO
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
    }
    public class AlumnosEnvioMasivoSMSDTO
    {
        public int IdAlumno { get; set; }
        public string CodigoCupon { get; set; }
    }
}
