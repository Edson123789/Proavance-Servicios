using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class AlumnoEnvioProyectoAplicacionDTO
    {
        public int IdPgeneralProyectoAplicacion { get; set; }
        public int IdPgeneralProyectoAplicacionEnvio { get; set; }
        public string Alumno { get; set; }
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public DateTime? FechaCalificacion { get; set; }
        public decimal? Nota { get; set; }
        public string Comentarios { get; set; }
        public int IdPEspecifico { get; set; }
        public string PEspecifico { get; set; }
        public string NombreArchivoRetroalimentacion { get; set; }
        public string RutaArchivoRetroalimentacion { get; set; }
        public bool EsEntregable { get; set; }
    }
}
