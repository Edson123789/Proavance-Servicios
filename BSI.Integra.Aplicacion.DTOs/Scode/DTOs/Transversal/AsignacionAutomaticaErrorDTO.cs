using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AsignacionAutomaticaErrorDTO
    {
        public int Id { get; set; }
        public string Campo { get; set; }
        public int? IdContacto { get; set; }
        public string Descripcion { get; set; }
        public int? IdAsignacionAutomatica { get; set; }
        public int? IdAsignacionAutomaticaTipoError { get; set; }
    }
}
