using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Transversal.DTO
{
    public class PgeneralProyectoAplicacionAnexoDTO
    {
        public int Id { get; set; }
        public int IdPgeneral { get; set; }
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public bool EsEnlace { get; set; }
        public bool SoloLectura { get; set; }
        public bool Estado { get; set; }
        public string Usuario { get; set; }
    }
}
