using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ReporteControlCalidadProcesamientoDTO
    {
        public int IdGrupo { get; set; }
        public string NombreGrupo { get; set; }
        public string NombreAsesor { get; set; }
        public string ApellidoAsesor { get; set; }
        public int TotalISyM { get; set; }
        public int TotalOC { get; set; }
        public double TasaConversion { get; set; }
        public int NumeroMaximo { get; set; }
        public int NumeroAlMomento { get; set; }
        public int NumeroDisponibles { get; set; }
        public int IdAsesor { get; set; }
    }
}
