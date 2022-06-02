using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ProyectoAplicacionConsolidadoListadoDTO
    {
        public int IdPgeneralProyectoAplicacionEnvio { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string Alumno { get; set; }
        public string CentroCosto { get; set; }
        public string Programa { get; set; }
        public DateTime FechaEnvio { get; set; }
        public decimal? Nota { get; set; }
        public DateTime? FechaCalificacion { get; set; }

        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public string NombreArchivoRetroalimentacion { get; set; }
        public string RutaArchivoRetroalimentacion { get; set; }
        public string Comentarios { get; set; }
        public bool EsEntregable { get; set; }
    }
}
