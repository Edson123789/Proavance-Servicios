using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class RaCentroCostoDTO
    {

        public int Id { get; set; }
        public int? IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public int? IdPespecifico { get; set; }
        public string NombrePespecifico { get; set; }
        public string ResponsableCoordinacion { get; set; }
        public int? IdRaCentroCostoEstado { get; set; }
        public int? IdRaFrecuencia { get; set; }
        public int NroCursosCertificado { get; set; }
        public string NombreUsuario { get; set; }
        public string Observacion { get; set; }
    }
}
