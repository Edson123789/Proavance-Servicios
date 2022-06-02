using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs.Operaciones
{
    public class ProyectoAplicacionEntregadoDetalleDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string EnlaceProyecto { get; set; }
        public string Version { get; set; }
        public DateTime FechaEnvio { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string TieneCalificacion { get; set; }
    }
}
