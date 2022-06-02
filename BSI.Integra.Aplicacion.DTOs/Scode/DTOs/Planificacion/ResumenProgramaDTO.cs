using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class ResumenProgramaDTO
    {
        public int IdArea { get; set; }
        public int IdSubArea { get; set; }
        public int IdPGeneral { get; set; }
        public string Duracion { get; set; }
        public string Paquete { get; set; }
        public string NombrePrograma { get; set; }
        public string NombreVersion { get; set; }
        public string InversionContado { get; set; }
        public string InversionCredito { get; set; }
        public decimal ContadoDolares { get; set; }
        public int Pais { get; set; }
        public int Orden { get; set; }
        public string Certificacion { get; set; }
        public List<ProgramaVersionDTO> Versiones { get; set; }
    }
}
