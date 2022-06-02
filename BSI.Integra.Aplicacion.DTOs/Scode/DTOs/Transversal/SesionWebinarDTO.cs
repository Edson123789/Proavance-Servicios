using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class SesionWebinarDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaTermino { get; set; }
        public string LinkWebinar { get; set; }
    }

    public class MaterialDescargarDTO
    {
        public string UrlArchivo { get; set; }
        public string NombreArchivo { get; set; }
    }

    public class TrabajoCursoAlumnoDTO
    {
        public string DescripcionTrabajo { get; set; }
        public string NombreFormaEntrega { get; set; }
        public DateTime FechaEntrega { get; set; }
    }
}
