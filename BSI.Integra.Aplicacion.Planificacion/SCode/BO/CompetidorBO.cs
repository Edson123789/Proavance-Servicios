using System;
using System.Collections.Generic;
using System.Text;
using BSI.Integra.Aplicacion.Base.BO;

namespace BSI.Integra.Aplicacion.Planificacion.BO
{
    public class CompetidorBO : BaseBO
    {
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
