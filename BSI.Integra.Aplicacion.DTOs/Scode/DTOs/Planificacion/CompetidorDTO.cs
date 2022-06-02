using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.DTOs
{
    public class CompetidorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int DuracionCronologica { get; set; }
        public int CostoNeto { get; set; }
        public int Precio { get; set; }
        public int IdMoneda { get; set; }
        public int IdInstitucionCompetidora { get; set; }
        public int IdPais { get; set; }
        public int IdCiudad { get; set; }
        public int? IdRegionCiudad { get; set; }
        public int IdAeaCapacitacion { get; set; }
        public int IdSubAreaCapacitacion { get; set; }
        public int IdCategoria { get; set; }
    }
}
