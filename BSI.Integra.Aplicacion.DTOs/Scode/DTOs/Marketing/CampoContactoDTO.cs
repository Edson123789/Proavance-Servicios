using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CampoContactoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string TipoControl { get; set; }
        public int? ValoresPreEstablecidos { get; set; }
        public string Procedimiento { get; set; }
    }

    public class DatosBasicosPortalContactoDTO
    {
        public string IdUsuarioPortalWeb { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int IdContacto { get; set; }
        public int IdAlumno { get; set; }
    }
}
