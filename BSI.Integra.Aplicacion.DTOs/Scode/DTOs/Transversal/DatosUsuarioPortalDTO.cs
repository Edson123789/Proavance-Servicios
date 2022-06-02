using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class DatosUsuarioPortalDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int IdAlumno { get; set; }
    }

    public class DatosMontosComplementariosDTO
    {
        public int Id { get; set; }
        public decimal Precio { get; set; }
        public decimal Matricula { get; set; }
        public decimal Cuotas { get; set; }
        public int NroCuotas { get; set; }
        public string Version { get; set; }
        public string NombreCorto { get; set; }
    }
}
