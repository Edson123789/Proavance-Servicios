using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class PaisDTO
    {
        public int Id { get; set; }
        public int CodigoPais { get; set; }
        public string CodigoIso { get; set; }
        public string NombrePais { get; set; }
        public string Moneda { get; set; }
        public decimal ZonaHoraria { get; set; }
        public int EstadoPublicacion { get; set; }
        public string Usuario { get; set; }
    }
}
